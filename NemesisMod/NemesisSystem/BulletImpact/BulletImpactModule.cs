using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Newtonsoft.Json;
using RDR2;
using RDR2.Math;
using RDR2.Native;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x02000093 RID: 147
	public sealed class BulletImpactModule
	{
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0014109A File Offset: 0x0013F29A
		public bool ViewerOpen
		{
			get
			{
				return this._viewer.IsOpen;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600140A RID: 5130 RVA: 0x001410A8 File Offset: 0x0013F2A8
		// (remove) Token: 0x0600140B RID: 5131 RVA: 0x001410E0 File Offset: 0x0013F2E0
		public event Action SwitchToHierarchy;

		// Token: 0x0600140C RID: 5132 RVA: 0x00141118 File Offset: 0x0013F318
		public BulletImpactModule(string scriptsDir, string dataDir, Func<Vector3, string> zoneOf, Func<int, NemesisTag> nemesisOf, Func<string, NemesisFate> fateOf, Action<string, CrippleLevel> nemesisCrippled)
		{
			this._nemesisCrippled = nemesisCrippled;
			this._cripple = new CrippleTracker(delegate(string line)
			{
				this.Log(line);
			});
			this._cfg = new BulletImpactSettings(Path.Combine(scriptsDir, "BulletImpact.ini"));
			this._logPath = Path.Combine(dataDir, "BulletImpact.log");
			this._zoneOf = zoneOf;
			this._nemesisOf = nemesisOf;
			try
			{
				AnimalAnatomy.TailIds = (from BoneID b in Enum.GetValues(typeof(BoneID))
				select new ValueTuple<BoneID, Match>(b, Regex.Match(b.ToString(), "^ID_SKEL_TAIL(\\d+)$", RegexOptions.IgnoreCase)) into x
				where x.Item2.Success
				orderby int.Parse(x.Item2.Groups[1].Value)
				select x.Item1).Distinct<int>().ToArray<int>();
			}
			catch
			{
			}
			this._xray = new XRayLog(Path.Combine(dataDir, "xray_log.json"), this._cfg.XRayMaxEntries);
			this.DropKillShotEchoes();
			this._nextVictimId = (from e in this._xray.Entries
			select e.VictimId).DefaultIfEmpty(0).Max() + 1;
			XRayViewer.GlyphCost = this._cfg.XRayGlyphCost;
			XRayViewer.Diag = delegate(string line)
			{
				this.Log(line);
			};
			ImpactDetector.AmmoNote = delegate(string line)
			{
				this.Log(line);
			};
			XRayRenderer.Diag = delegate(string line)
			{
				this.Log(line);
			};
			this._viewer = new XRayViewer(this._xray, this._cfg.XRayQuality, this._cfg.XRayRectLimit, fateOf, delegate(int n)
			{
				this._cfg.XRayRectLimit = n;
				this._cfg.Save();
			}, delegate(XRayQuality q)
			{
				this._cfg.XRayQuality = q;
				this._cfg.Save();
			});
			this._viewer.SwitchToHierarchy += delegate()
			{
				Action switchToHierarchy = this.SwitchToHierarchy;
				if (switchToHierarchy == null)
				{
					return;
				}
				switchToHierarchy();
			};
			this._detector.Impact += this.OnImpact;
			this._detector.Diagnostic += delegate(string d)
			{
				if (this._cfg.LogHits)
				{
					this.Log("DIAG " + d);
				}
			};
		}

		// Token: 0x0600140D RID: 5133 RVA: 0x001413C8 File Offset: 0x0013F5C8
		private bool Spared(int handle)
		{
			bool result;
			try
			{
				result = (this.HandsOff != null && this.HandsOff(handle));
			}
			catch
			{
				result = true;
			}
			return result;
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00141408 File Offset: 0x0013F608
		public void Tick(Ped player)
		{
			this.FlushLog(false);
			if (!this._cfg.Enabled)
			{
				return;
			}
			try
			{
				if (Game.FrameCount % 120 == 0)
				{
					PLAYER.SET_PLAYER_WEAPON_DAMAGE_MODIFIER(Game.Player.Handle, MISC.GET_MISSION_FLAG() ? 1f : this._cfg.PlayerDamage);
				}
				this._detector.Poll(player);
				if (this._cfg.Crippling)
				{
					this._cripple.Tick(player);
				}
				this.UpdatePendingDeaths();
				this._xray.SaveIfDirty(false);
				if (Game.FrameCount % 600 == 0)
				{
					this.PruneVictimIds();
				}
			}
			catch (Exception ex)
			{
				this.LogError(ex);
			}
		}

		// Token: 0x0600140F RID: 5135 RVA: 0x001414C4 File Offset: 0x0013F6C4
		public void Draw()
		{
			if (!this._cfg.Enabled)
			{
				return;
			}
			try
			{
				if (this._viewer.IsOpen)
				{
					Game.DisableAllControlsThisFrame();
					this._viewer.Draw();
				}
			}
			catch (Exception ex)
			{
				this.LogError(ex);
			}
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x0014151C File Offset: 0x0013F71C
		private void LogError(Exception ex)
		{
			int num = this._errorCount + 1;
			this._errorCount = num;
			if (num <= 20)
			{
				this.Log(string.Concat(new string[]
				{
					"ERROR ",
					ex.GetType().Name,
					": ",
					ex.Message,
					"\n",
					ex.StackTrace
				}));
			}
		}

		// Token: 0x06001411 RID: 5137 RVA: 0x00141587 File Offset: 0x0013F787
		public void OpenViewer(string nemesisId = null)
		{
			if (this._cfg.Enabled)
			{
				this._viewer.Open(nemesisId);
			}
		}

		// Token: 0x06001412 RID: 5138 RVA: 0x001415A2 File Offset: 0x0013F7A2
		public void OpenViewerAt(int entryId)
		{
			if (this._cfg.Enabled)
			{
				this._viewer.OpenAt(entryId);
			}
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x001415BD File Offset: 0x0013F7BD
		[return: TupleElementNames(new string[]
		{
			"entryId",
			"victim",
			"shots",
			"animal",
			"killed"
		})]
		public List<ValueTuple<int, string, int, bool, bool>> SearchXRays()
		{
			if (!this._cfg.Enabled)
			{
				return new List<ValueTuple<int, string, int, bool, bool>>();
			}
			return this._viewer.SearchRows();
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x001415DD File Offset: 0x0013F7DD
		public void ClearXRayLog()
		{
			this._xray.Clear();
			this._xray.SaveIfDirty(true);
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x001415F8 File Offset: 0x0013F7F8
		public int ShiftGameMinutes(long delta)
		{
			int num = 0;
			foreach (XRayEntry xrayEntry in this._xray.Entries)
			{
				if (xrayEntry.GameMin > 0L)
				{
					xrayEntry.GameMin += delta;
					num++;
				}
			}
			if (num > 0)
			{
				this._xray.MarkDirty();
			}
			return num;
		}

		// Token: 0x06001416 RID: 5142 RVA: 0x00141678 File Offset: 0x0013F878
		public void SaveXRayNow()
		{
			this._xray.SaveIfDirty(true);
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00141686 File Offset: 0x0013F886
		public void SaveXRayLater()
		{
			this._xray.SaveInBackground();
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00141693 File Offset: 0x0013F893
		public void ReloadXRayLog()
		{
			this._viewer.Close();
			this._xray.Reload();
			this.DropKillShotEchoes();
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x001416B4 File Offset: 0x0013F8B4
		public int MoveForeignAside(Func<string, bool> onRoster, string toPath, out int men)
		{
			men = 0;
			int result;
			try
			{
				IEnumerable<IGrouping<int, XRayEntry>> enumerable = from e in this._xray.Entries
				where e.GameMin == 0L
				group e by e.ShootoutId;
				List<XRayEntry> list = new List<XRayEntry>();
				Func<XRayEntry, bool> <>9__3;
				foreach (IGrouping<int, XRayEntry> grouping in enumerable)
				{
					IEnumerable<XRayEntry> source = grouping;
					Func<XRayEntry, bool> predicate;
					if ((predicate = <>9__3) == null)
					{
						predicate = (<>9__3 = ((XRayEntry e) => e.NemesisId != null && onRoster(e.NemesisId)));
					}
					if (!source.Any(predicate))
					{
						list.AddRange(grouping);
					}
				}
				if (list.Count == 0)
				{
					result = 0;
				}
				else
				{
					men = list.Select(delegate(XRayEntry e)
					{
						if (e.VictimId == 0)
						{
							return e.Victim;
						}
						return e.VictimId.ToString();
					}).Distinct<string>().Count<string>();
					SafeFile.Write(toPath, JsonConvert.SerializeObject(list), false);
					HashSet<XRayEntry> @object = new HashSet<XRayEntry>(list);
					this._xray.Entries.RemoveAll(new Predicate<XRayEntry>(@object.Contains));
					this._xray.MarkDirty();
					this._xray.SaveIfDirty(true);
					this.Log(string.Format("XRAY {0} shots on {1} men from another playthrough moved to {2}", list.Count, men, Path.GetFileName(toPath)));
					result = list.Count;
				}
			}
			catch (Exception ex)
			{
				this.Log("XRAY move-aside failed: " + ex.Message);
				result = 0;
			}
			return result;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00141890 File Offset: 0x0013FA90
		private void DropKillShotEchoes()
		{
			try
			{
				List<XRayEntry> kills = (from e in this._xray.Entries
				where e.Effect != null && e.Effect.StartsWith("Muerto en el acto")
				select e).ToList<XRayEntry>();
				int num = this._xray.Entries.RemoveAll((XRayEntry e) => !e.ImpactOnVictim && e.Effect == null && kills.Any((XRayEntry k) => k != e && ((k.VictimId != 0) ? (k.VictimId == e.VictimId) : (k.PedHandle == e.PedHandle)) && (e.Time - k.Time).TotalMilliseconds >= 0.0 && (e.Time - k.Time).TotalMilliseconds < 1000.0));
				if (num > 0)
				{
					this._xray.MarkDirty();
					this.Log(string.Format("XRAY {0} echo entries of kill shots removed (the kill shot's own damage, logged as a second shot)", num));
				}
			}
			catch (Exception ex)
			{
				this.Log("XRAY echo clean-up failed: " + ex.Message);
			}
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0014194C File Offset: 0x0013FB4C
		public void CloseViewer(bool now = false)
		{
			this._viewer.Close();
			if (now)
			{
				this._xray.SaveIfDirty(true);
				return;
			}
			this._xray.SaveInBackground();
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00141974 File Offset: 0x0013FB74
		public bool HandleKey(Keys key, bool allowOpen)
		{
			if (this._viewer.IsOpen)
			{
				this._viewer.HandleKey(key);
				if (!this._viewer.IsOpen)
				{
					this._xray.SaveInBackground();
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x001419AB File Offset: 0x0013FBAB
		public void Orbit(float dYaw, float dPitch)
		{
			if (this._viewer.IsOpen)
			{
				this._viewer.Orbit(dYaw, dPitch);
			}
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x001419C7 File Offset: 0x0013FBC7
		public void HandleKeyUp(Keys key)
		{
			if (this._viewer.IsOpen)
			{
				this._viewer.HandleKeyUp(key);
			}
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x001419E4 File Offset: 0x0013FBE4
		public WoundReport LatestWound(int pedHandle)
		{
			XRayEntry xrayEntry = this._xray.Entries.LastOrDefault((XRayEntry x) => x.PedHandle == pedHandle && x.WeaponClass != WeaponClass.Blunt && (DateTime.Now - x.Time).TotalSeconds < 120.0);
			if (xrayEntry == null)
			{
				return null;
			}
			if (xrayEntry.Analysis == null)
			{
				xrayEntry.Analysis = XRayAnalysis.Build(xrayEntry);
			}
			WoundReport woundReport = new WoundReport
			{
				WeaponName = XRayViewer.PrettyWeapon(xrayEntry.WeaponName),
				Distance = xrayEntry.Distance
			};
			if (xrayEntry.Analysis.Main != null)
			{
				woundReport.EntryText = xrayEntry.Analysis.Main.EntryText;
			}
			woundReport.Damage.AddRange(xrayEntry.Analysis.AllDamage);
			return woundReport;
		}

		// Token: 0x06001420 RID: 5152 RVA: 0x00141A91 File Offset: 0x0013FC91
		public CrippleLevel CrippleLevelOf(int handle)
		{
			return this._cripple.LevelOf(handle);
		}

		// Token: 0x06001421 RID: 5153 RVA: 0x00141A9F File Offset: 0x0013FC9F
		public void ForgetCripples()
		{
			this._cripple.Clear();
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x00141AAC File Offset: 0x0013FCAC
		public void ApplyCondition(Ped ped, CrippleLevel level)
		{
			if (this._cfg.Crippling)
			{
				this._cripple.Apply(ped, level);
			}
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00141ACC File Offset: 0x0013FCCC
		public int ShotsOnNemesis(string nemesisId)
		{
			return this._xray.Entries.Count((XRayEntry e) => e.NemesisId == nemesisId);
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00141B04 File Offset: 0x0013FD04
		private void UpdatePendingDeaths()
		{
			for (int i = this._pendingDeaths.Count - 1; i >= 0; i--)
			{
				ValueTuple<XRayEntry, int, int> valueTuple = this._pendingDeaths[i];
				int handle = valueTuple.Item2;
				int item = valueTuple.Item3;
				Ped ped = Entity.FromHandle(handle) as Ped;
				bool flag = ped != null && ped.Exists() && ped.IsDead;
				int num = 0;
				XRayEntry xrayEntry = flag ? this.LatestBeating(handle, out num) : null;
				int num2;
				if (xrayEntry != null && (!this._shotAt.TryGetValue(handle, out num2) || num2 < num) && !Downed.IsFinished(handle))
				{
					if (!xrayEntry.KnockedOut)
					{
						xrayEntry.KnockedOut = true;
						xrayEntry.Analysis = null;
						this._xray.MarkDirty();
						this.Log(string.Format("KNOCKOUT ped={0} - out cold, not dead", handle));
					}
					if (Game.GameTime > item || !ped.Exists())
					{
						this._pendingDeaths.RemoveAt(i);
					}
				}
				else
				{
					if (flag)
					{
						IEnumerable<XRayEntry> entries = this._xray.Entries;
						Func<XRayEntry, bool> predicate;
						Func<XRayEntry, bool> <>9__2;
						if ((predicate = <>9__2) == null)
						{
							predicate = (<>9__2 = ((XRayEntry x) => x.PedHandle == handle));
						}
						foreach (XRayEntry xrayEntry2 in entries.Where(predicate))
						{
							xrayEntry2.Killed = true;
						}
						XRayEntry xrayEntry3 = this._xray.Entries.LastOrDefault((XRayEntry x) => x.PedHandle == handle && x.LeftForDead) ?? this._xray.Entries.LastOrDefault((XRayEntry x) => x.PedHandle == handle);
						if (xrayEntry3 != null)
						{
							xrayEntry3.FatalShot = true;
						}
						this._xray.MarkDirty();
					}
					if (flag || ped == null || !ped.Exists() || Game.GameTime > item)
					{
						this._pendingDeaths.RemoveAt(i);
					}
				}
			}
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x00141D24 File Offset: 0x0013FF24
		private void Cripple(XRayEntry entry, int handle)
		{
			Ped ped = Entity.FromHandle(handle) as Ped;
			if (ped == null || !ped.Exists() || ped.IsDead || Kids.Protected(ped) || this.Spared(handle))
			{
				return;
			}
			eRelationshipType relationshipWithPed = ped.GetRelationshipWithPed(Game.Player.Character);
			if (relationshipWithPed == 2 || relationshipWithPed == 1)
			{
				return;
			}
			if (entry.Analysis == null)
			{
				entry.Analysis = XRayAnalysis.Build(entry);
			}
			string effect;
			CrippleLevel crippleLevel = CrippleRules.Assess(entry.Analysis.AllDamage, out effect);
			if (crippleLevel == CrippleLevel.None)
			{
				return;
			}
			CrippleLevel crippleLevel2 = this._cripple.LevelOf(handle);
			CrippleLevel crippleLevel3 = this._cripple.Apply(ped, crippleLevel);
			if (crippleLevel3 <= crippleLevel2)
			{
				return;
			}
			entry.Effect = effect;
			NemesisTag nemesisTag = this._nemesisOf(handle);
			if (nemesisTag != null && !nemesisTag.IsFollower)
			{
				Action<string, CrippleLevel> nemesisCrippled = this._nemesisCrippled;
				if (nemesisCrippled == null)
				{
					return;
				}
				nemesisCrippled(nemesisTag.Id, crippleLevel3);
			}
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x00141E0C File Offset: 0x0014000C
		private bool KillShot(XRayEntry entry, ImpactRecord rec)
		{
			if (entry.Analysis == null)
			{
				entry.Analysis = XRayAnalysis.Build(entry);
			}
			DamageItem damageItem = entry.Analysis.AllDamage.FirstOrDefault((DamageItem d) => !d.Chipped && BulletImpactModule.KillKeys.Contains(d.Key));
			if (damageItem == null)
			{
				return false;
			}
			Ped ped = Entity.FromHandle(rec.PedHandle) as Ped;
			if (ped == null || !ped.Exists() || ped.IsDead || Kids.Protected(ped) || this.Spared(ped.Handle))
			{
				return false;
			}
			eRelationshipType relationshipWithPed = ped.GetRelationshipWithPed(Game.Player.Character);
			if (relationshipWithPed == 2 || relationshipWithPed == 1)
			{
				return false;
			}
			int handle = Game.Player.Character.Handle;
			this._killShotAt[ped.Handle] = Game.GameTime;
			PED.APPLY_DAMAGE_TO_PED(ped.Handle, ped.Health + 500, 0, rec.BoneId, handle);
			if (!ped.IsDead)
			{
				ped.Health = 0;
			}
			entry.Effect = ((damageItem.Key == "brain") ? "Muerto en el acto - cerebro destruido" : "Muerto en el acto - corazón perforado");
			entry.Killed = (entry.FatalShot = true);
			this.Log(string.Format("KILLSHOT ped={0} {1}", rec.PedHandle, damageItem.Name));
			return true;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00141F6C File Offset: 0x0014016C
		public void TagVictim(int pedHandle, string nemesisId, string name)
		{
			int vid;
			if (!this._victimIds.TryGetValue(pedHandle, out vid))
			{
				return;
			}
			bool flag = false;
			IEnumerable<XRayEntry> entries = this._xray.Entries;
			Func<XRayEntry, bool> <>9__0;
			Func<XRayEntry, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((XRayEntry x) => x.VictimId == vid && x.NemesisId == null && x.FollowerOf == null));
			}
			foreach (XRayEntry xrayEntry in entries.Where(predicate))
			{
				xrayEntry.NemesisId = nemesisId;
				xrayEntry.NemesisName = name;
				flag = true;
			}
			if (flag)
			{
				this._xray.MarkDirty();
			}
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00142014 File Offset: 0x00140214
		public int RenameVictims(Func<string, uint, string> rename)
		{
			int num = 0;
			foreach (XRayEntry xrayEntry in this._xray.Entries)
			{
				if (xrayEntry != null && xrayEntry.NemesisId == null)
				{
					string text = rename(xrayEntry.Victim, xrayEntry.VictimModel);
					if (text != xrayEntry.Victim)
					{
						xrayEntry.Victim = text;
						num++;
					}
				}
			}
			if (num > 0)
			{
				this._xray.MarkDirty();
				this._xray.SaveIfDirty(true);
			}
			return num;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x001420BC File Offset: 0x001402BC
		public void NameVictim(int pedHandle, string name)
		{
			int vid;
			if (string.IsNullOrEmpty(name) || !this._victimIds.TryGetValue(pedHandle, out vid))
			{
				return;
			}
			bool flag = false;
			IEnumerable<XRayEntry> entries = this._xray.Entries;
			Func<XRayEntry, bool> <>9__0;
			Func<XRayEntry, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((XRayEntry x) => x.VictimId == vid && x.Victim != name));
			}
			foreach (XRayEntry xrayEntry in entries.Where(predicate))
			{
				xrayEntry.Victim = name;
				flag = true;
			}
			if (flag)
			{
				this._xray.MarkDirty();
			}
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x00142178 File Offset: 0x00140378
		private int VictimIdFor(int handle)
		{
			int result;
			if (!this._victimIds.TryGetValue(handle, out result))
			{
				Dictionary<int, int> victimIds = this._victimIds;
				int nextVictimId = this._nextVictimId;
				this._nextVictimId = nextVictimId + 1;
				result = (victimIds[handle] = nextVictimId);
			}
			return result;
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x001421B8 File Offset: 0x001403B8
		private void PruneVictimIds()
		{
			foreach (int num in this._victimIds.Keys.ToList<int>())
			{
				Ped ped = Entity.FromHandle(num) as Ped;
				if (ped == null || !ped.Exists())
				{
					this._victimIds.Remove(num);
				}
			}
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00142238 File Offset: 0x00140438
		internal static string PrettyVictim(uint model)
		{
			Gang gang = Gangs.ForModel(model);
			if (gang == null)
			{
				return "Desconocido";
			}
			return (gang.Name.StartsWith("The ") ? gang.Name.Substring(4) : gang.Name) + " man";
		}

		// Token: 0x0600142D RID: 5165 RVA: 0x00142288 File Offset: 0x00140488
		internal static string AnimalName(uint model)
		{
			if (!Enum.IsDefined(typeof(PedHash), model))
			{
				return "Animal";
			}
			PedHash pedHash = model;
			string text = pedHash.ToString().ToLowerInvariant();
			if (text.StartsWith("a_c_"))
			{
				text = text.Substring(4);
			}
			if (text.StartsWith("horse") || text.StartsWith("horsemule"))
			{
				if (!text.Contains("mule"))
				{
					return "Horse";
				}
				return "Mule";
			}
			else
			{
				if (text.StartsWith("dog"))
				{
					return "Dog";
				}
				string key = string.Join("_", text.Split(new char[]
				{
					'_'
				}).Where(delegate(string t)
				{
					if (t.Length > 0)
					{
						Func<char, bool> predicate;
						if ((predicate = BulletImpactModule.<>O.<0>__IsDigit) == null)
						{
							predicate = (BulletImpactModule.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
						}
						return !t.All(predicate);
					}
					return false;
				}));
				string result;
				if (BulletImpactModule.AnimalNames.TryGetValue(key, out result))
				{
					return result;
				}
				string text2 = text.Split(new char[]
				{
					'_'
				})[0];
				if (BulletImpactModule.AnimalNames.TryGetValue(text2, out result))
				{
					return result;
				}
				if (text2.Length <= 0)
				{
					return "Animal";
				}
				return char.ToUpperInvariant(text2[0]).ToString() + text2.Substring(1);
			}
		}

		// Token: 0x0600142E RID: 5166 RVA: 0x001423CC File Offset: 0x001405CC
		internal static string ModelLabel(uint model)
		{
			if (!Enum.IsDefined(typeof(PedHash), model))
			{
				return "Desconocido";
			}
			PedHash pedHash = model;
			List<string> list = pedHash.ToString().Split(new char[]
			{
				'_'
			}).Where(delegate(string p)
			{
				if (p.Length > 0)
				{
					Func<char, bool> predicate;
					if ((predicate = BulletImpactModule.<>O.<0>__IsDigit) == null)
					{
						predicate = (BulletImpactModule.<>O.<0>__IsDigit = new Func<char, bool>(char.IsDigit));
					}
					return !p.All(predicate);
				}
				return false;
			}).ToList<string>();
			IEnumerable<string> source = (list.Count >= 4 && list[0].Length <= 2) ? list.Skip(3) : list.Skip(1);
			string text = string.Join(" ", from w in source
			select char.ToUpperInvariant(w[0]).ToString() + w.Substring(1));
			if (text.Length <= 0)
			{
				return "Desconocido";
			}
			return text;
		}

		// Token: 0x0600142F RID: 5167 RVA: 0x001424A8 File Offset: 0x001406A8
		private void OnImpact(ImpactRecord rec)
		{
			int num;
			if (this._killShotAt.TryGetValue(rec.PedHandle, out num))
			{
				if (Game.GameTime - num < 600)
				{
					this.Log(string.Format("HIT ped={0} dmg={1:0.#} - the kill shot's own damage, not a shot (skipped)", rec.PedHandle, rec.Damage));
					return;
				}
				this._killShotAt.Remove(rec.PedHandle);
			}
			if (rec.Class == WeaponClass.Blunt)
			{
				this.OnBlow(rec);
				return;
			}
			this._shotAt[rec.PedHandle] = Game.GameTime;
			if (this._shotAt.Count > 64)
			{
				foreach (int key in (from kv in this._shotAt
				where Game.GameTime - kv.Value > 60000
				select kv.Key).ToList<int>())
				{
					this._shotAt.Remove(key);
				}
			}
			if (rec.BonesLocal.Count > 0)
			{
				XRayEntry xrayEntry = this.NewEntry(rec);
				this._xray.Add(xrayEntry);
				if (!rec.VictimDeadAfterHit)
				{
					this._pendingDeaths.Add(new ValueTuple<XRayEntry, int, int>(xrayEntry, rec.PedHandle, Game.GameTime + 30000));
				}
				bool flag = rec.EventIndex == -1 || rec.Animal;
				if ((flag || rec.VictimDeadAfterHit || !this._cfg.KillShots || !this.KillShot(xrayEntry, rec)) && !flag && this._cfg.Crippling && !rec.VictimDeadAfterHit)
				{
					this.Cripple(xrayEntry, rec.PedHandle);
				}
			}
			string text;
			if (!Enum.IsDefined(typeof(PedHash), rec.PedModel))
			{
				text = string.Format("0x{0:X8}", rec.PedModel);
			}
			else
			{
				PedHash pedModel = rec.PedModel;
				text = pedModel.ToString();
			}
			string text2 = text;
			this.Log(string.Concat(new string[]
			{
				string.Format("HIT shot={0} ev={1} pellets={2} ped={3} model={4} bone={5} name={6} index={7} ", new object[]
				{
					rec.ShotId,
					rec.EventIndex,
					rec.PelletsOnVictim,
					rec.PedHandle,
					text2,
					rec.BoneId,
					rec.BoneName,
					rec.BoneIndex
				}),
				string.Format("weapon={0} dmg={1:0.#} dist={2:0.0}m dead={3} ", new object[]
				{
					rec.WeaponName,
					rec.Damage,
					rec.Distance,
					rec.VictimDeadAfterHit
				}),
				"impact=",
				rec.HasImpact ? BulletImpactModule.Fmt(rec.Impact) : "none",
				" muzzle=",
				BulletImpactModule.Fmt(rec.Muzzle),
				" via ",
				rec.MuzzleSource
			}));
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x001427F4 File Offset: 0x001409F4
		private XRayEntry LatestBeating(int ped, out int at)
		{
			at = int.MinValue;
			XRayEntry result = null;
			foreach (KeyValuePair<ValueTuple<int, BlowKind>, ValueTuple<XRayEntry, int>> keyValuePair in this._beatings)
			{
				if (keyValuePair.Key.Item1 == ped && keyValuePair.Value.Item2 > at && this._xray.Entries.Contains(keyValuePair.Value.Item1))
				{
					at = keyValuePair.Value.Item2;
					result = keyValuePair.Value.Item1;
				}
			}
			return result;
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x001428A4 File Offset: 0x00140AA4
		private void OnBlow(ImpactRecord rec)
		{
			if (rec.BonesLocal.Count == 0)
			{
				return;
			}
			Blow blow = new Blow
			{
				Bone = rec.BlowBone,
				Dir = rec.DirLocal,
				Butt = rec.Butt,
				Kick = rec.Kick,
				Choke = rec.Choke,
				HeldMs = rec.HeldMs,
				Stomp = rec.Stomp,
				Down = rec.Down
			};
			ValueTuple<int, BlowKind> key = new ValueTuple<int, BlowKind>(rec.PedHandle, blow.Kind);
			ValueTuple<XRayEntry, int> valueTuple;
			XRayEntry xrayEntry;
			if (this._beatings.TryGetValue(key, out valueTuple) && Game.GameTime - valueTuple.Item2 < 20000 && !valueTuple.Item1.Killed && this._xray.Entries.Contains(valueTuple.Item1))
			{
				xrayEntry = valueTuple.Item1.Copy();
				xrayEntry.Blows = new List<Blow>(valueTuple.Item1.Blows ?? new List<Blow>());
				Blow blow2 = (xrayEntry.Blows.Count > 0) ? xrayEntry.Blows[xrayEntry.Blows.Count - 1] : null;
				if (blow.Choke && blow2 != null && blow2.Choke)
				{
					xrayEntry.Blows[xrayEntry.Blows.Count - 1] = new Blow
					{
						Bone = blow2.Bone,
						Dir = blow2.Dir,
						Choke = true,
						HeldMs = blow2.HeldMs + blow.HeldMs
					};
				}
				else
				{
					xrayEntry.Blows.Add(blow);
				}
				this._xray.Replace(valueTuple.Item1, xrayEntry);
			}
			else
			{
				xrayEntry = this.NewEntry(rec);
				xrayEntry.Blows = new List<Blow>
				{
					blow
				};
				this._xray.Add(xrayEntry);
			}
			xrayEntry.Pellets = xrayEntry.Blows.Count;
			xrayEntry.Killed = (xrayEntry.FatalShot = false);
			if (rec.VictimDeadAfterHit && !xrayEntry.KnockedOut)
			{
				xrayEntry.KnockedOut = true;
			}
			this._beatings[key] = new ValueTuple<XRayEntry, int>(xrayEntry, Game.GameTime);
			this.KickedToDeath(xrayEntry, rec);
			this._pendingDeaths.RemoveAll(([TupleElementNames(new string[]
			{
				"entry",
				"handle",
				"until"
			})] ValueTuple<XRayEntry, int, int> p) => p.Item2 == rec.PedHandle);
			this._pendingDeaths.Add(new ValueTuple<XRayEntry, int, int>(xrayEntry, rec.PedHandle, Game.GameTime + 30000));
			if (this._beatings.Count > 32)
			{
				foreach (ValueTuple<int, BlowKind> key2 in (from kv in this._beatings
				where Game.GameTime - kv.Value.Item2 > 20000
				select kv.Key).ToList<ValueTuple<int, BlowKind>>())
				{
					this._beatings.Remove(key2);
				}
			}
			this.Log(string.Format("BLOW ped={0} {1} game bone={2} ({3}) -> landed on {4} ({5}) dmg={6:0.#} via {7} - {8} {9} on him{10}", new object[]
			{
				rec.PedHandle,
				rec.Butt ? ("culatazo " + rec.WeaponName) : (rec.Choke ? "asfixia" : (rec.Stomp ? "pisotón" : (rec.Kick ? "patada" : "punch"))),
				rec.BoneId,
				rec.BoneName,
				rec.BlowBone,
				Anatomy.RegionOfBone(rec.BlowBone),
				rec.Damage,
				rec.MuzzleSource,
				(blow.Kind == BlowKind.Kick) ? "patada" : ((blow.Kind == BlowKind.Gun) ? "culatazo" : ((blow.Kind == BlowKind.Choke) ? "asfixia" : "punch")),
				xrayEntry.Blows.Count,
				xrayEntry.KnockedOut ? ", inconsciente" : ""
			}));
		}

		// Token: 0x06001432 RID: 5170 RVA: 0x00142D8C File Offset: 0x00140F8C
		private void KickedToDeath(XRayEntry e, ImpactRecord rec)
		{
			if (e.LeftForDead || !rec.VictimDeadAfterHit || Downed.IsFinished(rec.PedHandle) || e.Blows == null)
			{
				return;
			}
			if (this.Spared(rec.PedHandle))
			{
				return;
			}
			int num = 0;
			for (int i = e.Blows.Count - 1; i >= 0; i--)
			{
				Blow blow = e.Blows[i];
				if (blow.Kind != BlowKind.Kick || !blow.Down)
				{
					break;
				}
				num += (blow.Stomp ? 2 : 1);
			}
			if (num < 4)
			{
				return;
			}
			e.LeftForDead = true;
			Downed.KickedToDeath(rec.PedHandle);
			this.Log(string.Format("LEFT FOR DEAD ped={0} - {1} worth of kicks / stomps while he lay there: presumed dead", rec.PedHandle, num));
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x00142E50 File Offset: 0x00141050
		private XRayEntry NewEntry(ImpactRecord rec)
		{
			NemesisTag nemesisTag = this._nemesisOf(rec.PedHandle);
			XRayEntry xrayEntry = new XRayEntry();
			xrayEntry.Time = DateTime.Now;
			Func<long> gameClock = NemesisRecord.GameClock;
			xrayEntry.GameMin = ((gameClock != null) ? gameClock() : 0L);
			xrayEntry.Location = this._zoneOf(rec.Muzzle);
			xrayEntry.Victim = (rec.Animal ? BulletImpactModule.AnimalName(rec.PedModel) : (((nemesisTag != null) ? nemesisTag.PersonName : null) ?? BulletImpactModule.PrettyVictim(rec.PedModel)));
			xrayEntry.Animal = rec.Animal;
			xrayEntry.VictimModel = rec.PedModel;
			xrayEntry.VictimId = this.VictimIdFor(rec.PedHandle);
			xrayEntry.NemesisId = ((nemesisTag != null && !nemesisTag.IsFollower) ? nemesisTag.Id : null);
			xrayEntry.NemesisName = ((nemesisTag != null && !nemesisTag.IsFollower && nemesisTag.Id != null) ? nemesisTag.Name : null);
			xrayEntry.FollowerOf = ((nemesisTag != null && nemesisTag.IsFollower) ? nemesisTag.Name : null);
			xrayEntry.WeaponHash = rec.WeaponHash;
			xrayEntry.WeaponName = rec.WeaponName;
			xrayEntry.WeaponClass = rec.Class;
			xrayEntry.Ammo = rec.Ammo;
			xrayEntry.Pellets = rec.PelletsOnVictim;
			xrayEntry.Distance = rec.Distance;
			xrayEntry.RecordedBoneId = rec.BoneId;
			xrayEntry.ImpactOnVictim = rec.ImpactOnVictim;
			xrayEntry.Entry = rec.EntryLocal;
			xrayEntry.Dir = rec.DirLocal;
			xrayEntry.Muzzle = rec.MuzzleLocal;
			xrayEntry.Bones = rec.BonesLocal;
			xrayEntry.PedHandle = rec.PedHandle;
			xrayEntry.Killed = rec.VictimDeadAfterHit;
			xrayEntry.FatalShot = rec.VictimDeadAfterHit;
			return xrayEntry;
		}

		// Token: 0x06001434 RID: 5172 RVA: 0x00143018 File Offset: 0x00141218
		public void FlushLog(bool force = false)
		{
			if (this._logBuf.Length == 0 || (!force && this._logBuf.Length < 16000 && Environment.TickCount < this._nextLogFlush))
			{
				return;
			}
			this._nextLogFlush = Environment.TickCount + 1000;
			try
			{
				SafeFile.Append(this._logPath, this._logBuf.ToString());
			}
			catch
			{
			}
			this._logBuf.Clear();
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x001430A0 File Offset: 0x001412A0
		private static string Fmt(Vector3 v)
		{
			return string.Format("({0:0.00},{1:0.00},{2:0.00})", v.X, v.Y, v.Z);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x001430D0 File Offset: 0x001412D0
		private void Log(string line)
		{
			this._logBuf.Append(string.Format("[{0:HH:mm:ss}] {1}{2}", DateTime.Now, line, Environment.NewLine));
			if (this._logBuf.Length > 16000)
			{
				this.FlushLog(false);
			}
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0014311C File Offset: 0x0014131C
		// Note: this type is marked as 'beforefieldinit'.
		static BulletImpactModule()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			dictionary["bearblack"] = "Black Bear";
			dictionary["bear"] = "Bear";
			dictionary["buck"] = "Buck";
			dictionary["deer"] = "Deer";
			dictionary["elk"] = "Elk";
			dictionary["moose"] = "Moose";
			dictionary["bighornram"] = "Bighorn Ram";
			dictionary["buffalo"] = "Bison";
			dictionary["bison"] = "Bison";
			dictionary["pronghorn"] = "Pronghorn";
			dictionary["wolf"] = "Wolf";
			dictionary["wolf_medium"] = "Wolf";
			dictionary["wolf_small"] = "Wolf";
			dictionary["coyote"] = "Coyote";
			dictionary["fox"] = "Fox";
			dictionary["cougar"] = "Cougar";
			dictionary["panther"] = "Panther";
			dictionary["boar"] = "Boar";
			dictionary["boarlegendary"] = "Legendary Boar";
			dictionary["pig"] = "Pig";
			dictionary["cow"] = "Cow";
			dictionary["bull"] = "Bull";
			dictionary["ox"] = "Ox";
			dictionary["sheep"] = "Sheep";
			dictionary["goat"] = "Goat";
			dictionary["rabbit"] = "Rabbit";
			dictionary["possum"] = "Possum";
			dictionary["raccoon"] = "Raccoon";
			dictionary["skunk"] = "Skunk";
			dictionary["badger"] = "Badger";
			dictionary["beaver"] = "Beaver";
			dictionary["armadillo"] = "Armadillo";
			dictionary["alligator"] = "Alligator";
			dictionary["donkey"] = "Donkey";
			dictionary["mule"] = "Mule";
			dictionary["cat"] = "Cat";
			dictionary["javelina"] = "Peccary";
			dictionary["muskrat"] = "Muskrat";
			dictionary["squirrel"] = "Squirrel";
			BulletImpactModule.AnimalNames = dictionary;
		}

		// Token: 0x04000DBF RID: 3519
		private const int OverlayLines = 6;

		// Token: 0x04000DC0 RID: 3520
		private readonly BulletImpactSettings _cfg;

		// Token: 0x04000DC1 RID: 3521
		private readonly ImpactDetector _detector = new ImpactDetector();

		// Token: 0x04000DC2 RID: 3522
		private readonly string _logPath;

		// Token: 0x04000DC3 RID: 3523
		private readonly XRayLog _xray;

		// Token: 0x04000DC4 RID: 3524
		private readonly XRayViewer _viewer;

		// Token: 0x04000DC5 RID: 3525
		private readonly Func<Vector3, string> _zoneOf;

		// Token: 0x04000DC6 RID: 3526
		private readonly Func<int, NemesisTag> _nemesisOf;

		// Token: 0x04000DC7 RID: 3527
		private readonly CrippleTracker _cripple;

		// Token: 0x04000DC8 RID: 3528
		private readonly Action<string, CrippleLevel> _nemesisCrippled;

		// Token: 0x04000DC9 RID: 3529
		private int _errorCount;

		// Token: 0x04000DCA RID: 3530
		[TupleElementNames(new string[]
		{
			"entry",
			"handle",
			"until"
		})]
		private readonly List<ValueTuple<XRayEntry, int, int>> _pendingDeaths = new List<ValueTuple<XRayEntry, int, int>>();

		// Token: 0x04000DCB RID: 3531
		private const int WatchForDeathMs = 30000;

		// Token: 0x04000DCC RID: 3532
		private readonly Dictionary<int, int> _victimIds = new Dictionary<int, int>();

		// Token: 0x04000DCD RID: 3533
		private int _nextVictimId;

		// Token: 0x04000DCF RID: 3535
		public Func<int, bool> HandsOff;

		// Token: 0x04000DD0 RID: 3536
		private static readonly string[] KillKeys = new string[]
		{
			"brain",
			"heart"
		};

		// Token: 0x04000DD1 RID: 3537
		private static readonly Dictionary<string, string> AnimalNames;

		// Token: 0x04000DD2 RID: 3538
		private readonly Dictionary<int, int> _killShotAt = new Dictionary<int, int>();

		// Token: 0x04000DD3 RID: 3539
		private const int BeatingGapMs = 20000;

		// Token: 0x04000DD4 RID: 3540
		[TupleElementNames(new string[]
		{
			"ped",
			"kind",
			"entry",
			"at"
		})]
		private readonly Dictionary<ValueTuple<int, BlowKind>, ValueTuple<XRayEntry, int>> _beatings = new Dictionary<ValueTuple<int, BlowKind>, ValueTuple<XRayEntry, int>>();

		// Token: 0x04000DD5 RID: 3541
		private readonly Dictionary<int, int> _shotAt = new Dictionary<int, int>();

		// Token: 0x04000DD6 RID: 3542
		private const int KickedToDeathWeight = 4;

		// Token: 0x04000DD7 RID: 3543
		private readonly StringBuilder _logBuf = new StringBuilder();

		// Token: 0x04000DD8 RID: 3544
		private int _nextLogFlush;

		// Token: 0x0200048E RID: 1166
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002086 RID: 8326
			public static Func<char, bool> <0>__IsDigit;
		}
	}
}
