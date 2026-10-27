using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x020000A5 RID: 165
	internal sealed class XRayViewer
	{
		// Token: 0x1700033E RID: 830
		// (get) Token: 0x060014D9 RID: 5337 RVA: 0x00150AE7 File Offset: 0x0014ECE7
		private XRayQuality Quality
		{
			get
			{
				if (!XRayViewer.DebugOn)
				{
					return XRayQuality.Auto;
				}
				return this._quality;
			}
		}

		// Token: 0x1700033F RID: 831
		// (get) Token: 0x060014DA RID: 5338 RVA: 0x00150AF8 File Offset: 0x0014ECF8
		// (set) Token: 0x060014DB RID: 5339 RVA: 0x00150B00 File Offset: 0x0014ED00
		public bool IsOpen { get; private set; }

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060014DC RID: 5340 RVA: 0x00150B0C File Offset: 0x0014ED0C
		// (remove) Token: 0x060014DD RID: 5341 RVA: 0x00150B44 File Offset: 0x0014ED44
		public event Action SwitchToHierarchy;

		// Token: 0x060014DE RID: 5342 RVA: 0x00150B7C File Offset: 0x0014ED7C
		public XRayViewer(XRayLog log, XRayQuality quality, int rectBudget, Func<string, NemesisFate> fateOf, Action<int> saveBudget, Action<XRayQuality> saveQuality)
		{
			this._saveBudget = saveBudget;
			this._log = log;
			this._quality = quality;
			this._rectLimit = rectBudget;
			this._fateOf = fateOf;
			this._saveQuality = saveQuality;
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00150BDC File Offset: 0x0014EDDC
		public void Open(string nemesisId = null)
		{
			this.IsOpen = true;
			this._confirmClear = false;
			this._renderer.ReleaseAll();
			if (nemesisId != null && this._animals)
			{
				this.SetFilter(false);
			}
			List<XRayViewer.Group> source = this.Groups(null);
			XRayViewer.Group group = (nemesisId != null) ? source.FirstOrDefault((XRayViewer.Group x) => x.NemesisId == nemesisId) : null;
			if (group != null || this._groupKey == null || source.All((XRayViewer.Group x) => x.Key != this._groupKey))
			{
				XRayViewer.Group group2 = group ?? source.FirstOrDefault<XRayViewer.Group>();
				this._groupKey = ((group2 != null) ? group2.Key : null);
				this._shot = -1;
				this._renderer.ResetView();
			}
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00150CA9 File Offset: 0x0014EEA9
		public void Close()
		{
			this.IsOpen = false;
			this._renderer.ReleaseAll();
		}

		// Token: 0x060014E1 RID: 5345 RVA: 0x00150CC0 File Offset: 0x0014EEC0
		[return: TupleElementNames(new string[]
		{
			"entryId",
			"victim",
			"shots",
			"animal",
			"killed"
		})]
		public List<ValueTuple<int, string, int, bool, bool>> SearchRows()
		{
			List<ValueTuple<int, string, int, bool, bool>> list = new List<ValueTuple<int, string, int, bool, bool>>();
			foreach (bool flag in new bool[]
			{
				default(bool),
				true
			})
			{
				foreach (XRayViewer.Group group in this.Groups(new bool?(flag)))
				{
					List<ValueTuple<int, string, int, bool, bool>> list2 = list;
					int id = group.Shots[group.Shots.Count - 1].Id;
					string name = group.Name;
					int count = group.Shots.Count;
					bool item = flag;
					if (!group.Shots.Any((XRayEntry s) => s.Killed))
					{
						goto IL_C8;
					}
					string text = this.ManStatus(group);
					if (text == null || !(text != "BURLÓ A LA MUERTE"))
					{
						goto IL_C8;
					}
					bool item2 = text != "DÁNDOTE CAZA";
					IL_C9:
					list2.Add(new ValueTuple<int, string, int, bool, bool>(id, name, count, item, item2));
					continue;
					IL_C8:
					item2 = false;
					goto IL_C9;
				}
			}
			return list;
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x00150DDC File Offset: 0x0014EFDC
		public void OpenAt(int entryId)
		{
			XRayEntry e = this._log.Entries.FirstOrDefault((XRayEntry x) => x.Id == entryId);
			if (e != null)
			{
				this.SetFilter(e.Animal);
			}
			this.Open(null);
			if (e == null)
			{
				return;
			}
			XRayViewer.Group group = this.Groups(null).FirstOrDefault((XRayViewer.Group x) => x.Shots.Contains(e));
			if (group == null)
			{
				return;
			}
			this._groupKey = group.Key;
			this._shot = -1;
			this._scroll = 0;
			this._renderer.ResetView();
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x00150E8C File Offset: 0x0014F08C
		private List<XRayViewer.Group> Groups(bool? animals = null)
		{
			XRayViewer.<>c__DisplayClass53_0 CS$<>8__locals1 = new XRayViewer.<>c__DisplayClass53_0();
			CS$<>8__locals1.wantAnimals = (animals ?? this._animals);
			CS$<>8__locals1.nemesisOfVictim = new Dictionary<int, string>();
			foreach (XRayEntry xrayEntry in this._log.Entries)
			{
				if (xrayEntry.NemesisId != null && xrayEntry.VictimId > 0)
				{
					CS$<>8__locals1.nemesisOfVictim[xrayEntry.VictimId] = xrayEntry.NemesisId;
				}
			}
			return (from g in (from e in this._log.Entries
			where e.Animal == CS$<>8__locals1.wantAnimals
			select e).GroupBy(new Func<XRayEntry, string>(CS$<>8__locals1.<Groups>g__KeyOf|0)).Select(delegate(IGrouping<string, XRayEntry> g)
			{
				List<XRayEntry> list = (from e in g
				orderby e.Time
				select e).ToList<XRayEntry>();
				XRayEntry xrayEntry2 = list[list.Count - 1];
				XRayEntry xrayEntry3 = list.LastOrDefault((XRayEntry s) => s.NemesisName != null);
				XRayViewer.Group @group = new XRayViewer.Group();
				@group.Key = g.Key;
				@group.Shots = list;
				@group.Last = xrayEntry2.Time;
				@group.NemesisId = ((xrayEntry3 != null) ? xrayEntry3.NemesisId : null);
				@group.FollowerOf = (from s in list
				select s.FollowerOf).FirstOrDefault((string f) => f != null);
				@group.Name = (((xrayEntry3 != null) ? xrayEntry3.NemesisName : null) ?? XRayViewer.ShownVictim(xrayEntry2));
				string sub;
				if (xrayEntry3 == null)
				{
					sub = xrayEntry2.Location;
				}
				else
				{
					Gang gang = Gangs.ForModel(xrayEntry2.VictimModel);
					sub = (((gang != null) ? gang.Name : null) ?? "Sin banda");
				}
				@group.Sub = sub;
				return @group;
			})
			orderby g.Last descending
			select g).ToList<XRayViewer.Group>();
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x00150FB0 File Offset: 0x0014F1B0
		private static string ShownVictim(XRayEntry e)
		{
			if (!string.IsNullOrEmpty(e.Victim) && !(e.Victim == BulletImpactModule.ModelLabel(e.VictimModel)))
			{
				return e.Victim;
			}
			return BulletImpactModule.PrettyVictim(e.VictimModel);
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00150FEC File Offset: 0x0014F1EC
		private static string WhenText(XRayEntry e, bool seconds = false)
		{
			if (e.GameMin <= 0L)
			{
				return e.Time.ToString(seconds ? "HH:mm:ss" : "HH:mm");
			}
			DateTime dateTime = GameDate.FromMinutes(e.GameMin);
			return string.Format("{0} {1}, {2:HH:mm}", GameDate.Month(dateTime.Month), dateTime.Day, dateTime);
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x00151054 File Offset: 0x0014F254
		[return: TupleElementNames(new string[]
		{
			"g",
			"shot"
		})]
		private List<ValueTuple<XRayViewer.Group, int>> Rows(List<XRayViewer.Group> groups)
		{
			List<ValueTuple<XRayViewer.Group, int>> list = new List<ValueTuple<XRayViewer.Group, int>>();
			foreach (XRayViewer.Group group in groups)
			{
				list.Add(new ValueTuple<XRayViewer.Group, int>(group, -1));
				if (group.Key == this._groupKey)
				{
					for (int i = 0; i < group.Shots.Count; i++)
					{
						list.Add(new ValueTuple<XRayViewer.Group, int>(group, i));
					}
				}
			}
			return list;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x001510E8 File Offset: 0x0014F2E8
		public bool HandleKey(Keys key)
		{
			if (this._calibrating)
			{
				if (key <= Keys.Return)
				{
					if (key != Keys.Back)
					{
						if (key != Keys.Return)
						{
							return true;
						}
						this._rectLimit = (this._calRow + 1) * 50;
						this._saveBudget(this._rectLimit);
						this._calibrating = false;
						return true;
					}
				}
				else
				{
					switch (key)
					{
					case Keys.Left:
					case Keys.Right:
						this._calPage = 1 - this._calPage;
						return true;
					case Keys.Up:
						this._calRow = Math.Max(0, this._calRow - 1);
						return true;
					case Keys.Down:
						this._calRow = Math.Min(39, this._calRow + 1);
						return true;
					default:
						if (key != Keys.K)
						{
							return true;
						}
						break;
					}
				}
				this._calibrating = false;
				return true;
			}
			if (key == Keys.K && XRayViewer.DebugOn)
			{
				this._calibrating = true;
				this._calRow = Math.Max(0, Math.Min(39, this._rectLimit / 50 - 1));
				return true;
			}
			if (this._renderer.SetHeld(key, true))
			{
				return true;
			}
			List<XRayViewer.Group> groups = this.Groups(null);
			List<ValueTuple<XRayViewer.Group, int>> list = this.Rows(groups);
			int num = list.FindIndex(([TupleElementNames(new string[]
			{
				"g",
				"shot"
			})] ValueTuple<XRayViewer.Group, int> r) => r.Item1.Key == this._groupKey && r.Item2 == this._shot);
			if (key > Keys.Down)
			{
				if (key <= Keys.E)
				{
					if (key != Keys.Delete)
					{
						if (key != Keys.E)
						{
							return false;
						}
					}
					else
					{
						if (!XRayViewer.DebugOn)
						{
							return false;
						}
						if (!this._confirmClear)
						{
							this._confirmClear = true;
							return true;
						}
						this._log.Clear();
						this._groupKey = null;
						this._builtFor = null;
						goto IL_29D;
					}
				}
				else if (key != Keys.F)
				{
					if (key != Keys.Q)
					{
						if (key != Keys.R)
						{
							return false;
						}
						this._renderer.ResetView();
						goto IL_29D;
					}
				}
				else
				{
					if (XRayViewer.DebugOn)
					{
						this._quality = ((this._quality == XRayQuality.Auto) ? XRayQuality.High : ((this._quality == XRayQuality.Low) ? XRayQuality.Auto : (this._quality - 1)));
						this._builtFor = null;
						this._saveQuality(this._quality);
						goto IL_29D;
					}
					return false;
				}
				this.SetFilter(!this._animals);
				goto IL_29D;
			}
			if (key <= Keys.Tab)
			{
				if (key == Keys.Back)
				{
					this.Close();
					goto IL_29D;
				}
				if (key == Keys.Tab)
				{
					Action switchToHierarchy = this.SwitchToHierarchy;
					if (switchToHierarchy == null)
					{
						goto IL_29D;
					}
					switchToHierarchy();
					goto IL_29D;
				}
			}
			else
			{
				if (key == Keys.Space)
				{
					this._renderer.AutoSpin = !this._renderer.AutoSpin;
					goto IL_29D;
				}
				if (key == Keys.Up)
				{
					this.Select(list, Math.Max(0, num - 1));
					goto IL_29D;
				}
				if (key == Keys.Down)
				{
					this.Select(list, Math.Min(list.Count - 1, num + 1));
					goto IL_29D;
				}
			}
			return false;
			IL_29D:
			this._confirmClear = false;
			return true;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x0015139A File Offset: 0x0014F59A
		public void HandleKeyUp(Keys key)
		{
			this._renderer.SetHeld(key, false);
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x001513AC File Offset: 0x0014F5AC
		private void SetFilter(bool animals)
		{
			if (this._animals == animals)
			{
				return;
			}
			this._animals = animals;
			this._groupKey = null;
			this._shot = -1;
			this._scroll = 0;
			this._builtFor = null;
			this._confirmClear = false;
			this._renderer.ResetView();
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x001513F8 File Offset: 0x0014F5F8
		public void Orbit(float dYaw, float dPitch)
		{
			this._renderer.Orbit(dYaw, dPitch);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00151408 File Offset: 0x0014F608
		private void Select([TupleElementNames(new string[]
		{
			"g",
			"shot"
		})] List<ValueTuple<XRayViewer.Group, int>> rows, int index)
		{
			if (index < 0 || index >= rows.Count)
			{
				return;
			}
			bool flag = rows[index].Item1.Key != this._groupKey;
			this._groupKey = rows[index].Item1.Key;
			this._shot = rows[index].Item2;
			if (flag)
			{
				this._renderer.ResetView();
			}
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00151478 File Offset: 0x0014F678
		public void Draw()
		{
			if (this._calibrating)
			{
				this.DrawCalibration();
				return;
			}
			List<XRayViewer.Group> list = this.Groups(null);
			int num = this._log.Entries.Count((XRayEntry x) => x.Animal == this._animals);
			UiDraw.ScreenRect(0, 0, 0, 150);
			Theme.Window(0.04f, 0.90199995f);
			string text = this._animals ? ((list.Count == 1) ? "animal" : "animals") : ((list.Count == 1) ? "hombre" : "men");
			Theme.Header("RAYOS X", string.Format("{0} {1} disparados  -  {2} herida{3} registrada{3}", new object[]
			{
				list.Count,
				text,
				num,
				(num == 1) ? "" : "s"
			}), MenuTab.XRay, null);
			Theme.FactionBar(0.13499999f, XRayViewer.FilterNames, (this._animals > false) ? 1 : 0, false, Theme.NativeRed, null, null, false, delegate(int i)
			{
				this.SetFilter(i == 1);
			});
			if (list.Count == 0)
			{
				Theme.PanelRect(0.07f, 0.18299998f, 0.86f, 0.677f, 215);
				Theme.Body(this._animals ? "Aún no hay animales registrados. Dispara, corta o vuela a un animal de cuatro patas y se registrará aquí." : "Aún no hay heridas registradas. Cada vez que disparas, cortas o vuelas a alguien, se registra aquí.", 0.085f, 0.20299998f, 0.28f);
				this.DrawFooter();
				return;
			}
			XRayViewer.Group group = list.FirstOrDefault((XRayViewer.Group x) => x.Key == this._groupKey) ?? list[0];
			this._groupKey = group.Key;
			if (this._shot >= group.Shots.Count)
			{
				this._shot = -1;
			}
			string text2 = string.Format("{0}|{1}|{2}|{3}", new object[]
			{
				group.Key,
				this._shot,
				group.Shots.Count,
				this.Quality
			});
			if (text2 != this._builtFor)
			{
				this._composite = ((this._shot < 0) ? XRayComposite.ForShots(group.Shots) : XRayComposite.ForEntry(group.Shots[this._shot]));
				this._renderer.Build(this._composite.Body, this._composite.Paths, this._composite.Keys, this.Quality);
				this._builtFor = text2;
			}
			this.HandleMouse();
			this.DrawFooter();
			this.DrawList(list);
			this.DrawFacts(group);
			if (this._shot < 0)
			{
				this.DrawManSummary(group);
			}
			else
			{
				this.DrawShot(group, this._shot);
			}
			this.DrawXRay(group);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00151734 File Offset: 0x0014F934
		private void Fact(string label, string value, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int>? color = null)
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			ValueTuple<int, int, int> valueTuple = color ?? Theme.Text;
			if (this._ly > 0.83399993f || UiDraw.TextSlotsLeft < 8)
			{
				return;
			}
			Theme.Label(label, this._lx, this._ly + 0.002f, 0.18f);
			float num = Theme.CapsWidth(label, 0.18f) + 0.012f;
			float width = 0.188f - num;
			string text = XRayViewer.Fit(value, 0.23f, width);
			UiDraw.Text(text, 0.269f - Theme.MixedWidth(text, 0.23f, 0.0222f), this._ly, 0.23f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
			UiDraw.Rect(this._lx, this._ly + 0.023f, 0.188f, 0.0009259259f, Theme.Border.Item1, Theme.Border.Item2, Theme.Border.Item3, 110);
			this._ly += 0.027f;
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00151850 File Offset: 0x0014FA50
		private void DrawFacts(XRayViewer.Group g)
		{
			float num = 0.655f;
			Theme.PanelRect(0.07f, num, 0.21f, 0.205f, 215);
			bool flag = this._shot < 0;
			XRayEntry xrayEntry = flag ? null : g.Shots[this._shot];
			bool flag2 = xrayEntry != null && Anatomy.IsBlade(xrayEntry.WeaponClass);
			bool flag3 = xrayEntry != null && Anatomy.IsBlunt(xrayEntry.WeaponClass);
			Theme.Label(flag ? ((g.Shots.Count == 1) ? "EL DISPARO" : "TODOS LOS DISPAROS") : (flag3 ? "ESTA PALIZA" : (flag2 ? "ESTE GOLPE" : "ESTE DISPARO")), 0.078f, num + 0.007f, 0.25f);
			UiDraw.Rect(0.076f, num + 0.032f - 0.004f, 0.198f, 0.0015f, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 200);
			this._lx = 0.081f;
			this._ly = num + 0.032f + 0.004f;
			if (flag)
			{
				string text;
				if (g.Shots.Count != 1)
				{
					text = string.Format("{0:0.0} - {1:0.0} m", g.Shots.Min((XRayEntry s) => s.Distance), g.Shots.Max((XRayEntry s) => s.Distance));
				}
				else
				{
					text = string.Format("{0:0.0} m", g.Shots[0].Distance);
				}
				string value = text;
				string value2 = (g.Shots.Count == 1) ? XRayViewer.WhenText(g.Shots[0], false) : string.Format("{0}  ({1:0} s)", XRayViewer.WhenText(g.Shots[0], false), (g.Last - g.Shots[0].Time).TotalSeconds);
				this.Fact("ARMAS", string.Join(", ", (from s in g.Shots
				select XRayViewer.PrettyWeapon(s.WeaponName)).Distinct<string>()), null);
				this.Fact("DISTANCIA", value, null);
				this.Fact("DÓNDE", g.Shots[0].Location, null);
				this.Fact("CUÁNDO", value2, null);
				string a = this.ManStatus(g);
				XRayEntry xrayEntry2 = g.Shots.LastOrDefault((XRayEntry s) => s.Effect != null);
				XRayEntry xrayEntry3 = g.Shots.LastOrDefault((XRayEntry s) => s.FatalShot);
				if (xrayEntry2 != null)
				{
					this.Fact("ESTADO", xrayEntry2.Effect, new ValueTuple<int, int, int>?(Theme.Warn));
				}
				else if (a == "KNOCKED OUT")
				{
					this.Fact("ESTADO", "Dejado inconsciente", new ValueTuple<int, int, int>?(Theme.Warn));
				}
				if (xrayEntry3 != null)
				{
					XRayAnalysis analysis = xrayEntry3.Analysis;
					DamageItem damageItem;
					if (analysis == null)
					{
						damageItem = null;
					}
					else
					{
						PathResult main = analysis.Main;
						if (main == null)
						{
							damageItem = null;
						}
						else
						{
							damageItem = main.Damage.FirstOrDefault((DamageItem d) => !d.Chipped);
						}
					}
					DamageItem damageItem2 = damageItem;
					string text2 = Anatomy.IsBlunt(xrayEntry3.WeaponClass) ? "Muerto a golpes" : ((damageItem2 != null) ? XRayViewer.Tidy(damageItem2.Name) : "Pérdida de sangre");
					if (a == "DADO POR MUERTO")
					{
						text2 = "Down with " + text2.ToLowerInvariant();
					}
					this.Fact((a == "DADO POR MUERTO") ? "VISTO POR ÚLTIMA VEZ" : "CAUSA", text2, new ValueTuple<int, int, int>?(Theme.Blood));
				}
				return;
			}
			this.Fact("ARMA", XRayViewer.PrettyWeapon(xrayEntry.WeaponName), null);
			if (flag3)
			{
				this.Fact("GOLPES", XRayViewer.BlowKinds(xrayEntry), null);
				if (xrayEntry.KnockedOut && !xrayEntry.Killed)
				{
					this.Fact("RESULTADO", "Dejado inconsciente", new ValueTuple<int, int, int>?(Theme.Warn));
				}
			}
			else
			{
				this.Fact(flag2 ? "GOLPE" : "MUNICIÓN", XRayViewer.RoundName(xrayEntry.WeaponClass, xrayEntry.Ammo), null);
			}
			if (!flag3)
			{
				if (Anatomy.IsArea(xrayEntry.WeaponClass))
				{
					this.Fact("CONSECUENCIA", string.Format("{0:0.0} m  -  {1} {2} (est.)", xrayEntry.Distance, Math.Max(1, xrayEntry.Pellets), (xrayEntry.WeaponClass == WeaponClass.Burn) ? "quemaduras" : "fragmentos"), null);
				}
				else if (xrayEntry.WeaponClass == WeaponClass.Shotgun)
				{
					this.Fact("DISTANCIA", string.Format("{0:0.0} m  -  {1}", xrayEntry.Distance, (xrayEntry.Pellets > 1) ? string.Format("{0} perdigones ({1} est.)", xrayEntry.Pellets, xrayEntry.Pellets - 1) : "1 perdigón"), null);
				}
				else
				{
					this.Fact("DISTANCIA", string.Format("{0:0.0} m", xrayEntry.Distance), null);
				}
			}
			this.Fact("CUÁNDO", XRayViewer.WhenText(xrayEntry, xrayEntry.GameMin <= 0L), null);
			this.Fact("DÓNDE", xrayEntry.Location, null);
			if (xrayEntry.Effect != null)
			{
				this.Fact("EFECTO", xrayEntry.Effect, new ValueTuple<int, int, int>?(Theme.Warn));
			}
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00151E8C File Offset: 0x0015008C
		[return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static ValueTuple<int, int, int> ShotChipColor(XRayEntry e)
		{
			XRayAnalysis analysis = e.Analysis;
			List<DamageItem> list;
			if (analysis == null)
			{
				list = null;
			}
			else
			{
				list = (from d in analysis.AllDamage
				where !d.Chipped
				select d).ToList<DamageItem>();
			}
			List<DamageItem> list2 = list;
			if (list2 == null || list2.Count == 0)
			{
				return Theme.Dim;
			}
			return XRayViewer.SeverityColor(list2.Max((DamageItem d) => d.Severity));
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00151F10 File Offset: 0x00150110
		private void DrawList(List<XRayViewer.Group> groups)
		{
			Theme.PanelRect(0.07f, 0.18299998f, 0.21f, 0.464f, 215);
			Theme.Label(this._animals ? "ANIMALES" : "VÍCTIMAS", 0.078f, 0.18999998f, 0.25f);
			UiDraw.Rect(0.076f, 0.211f, 0.198f, 0.0015f, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 200);
			List<ValueTuple<XRayViewer.Group, int>> list = this.Rows(groups);
			int num = list.FindIndex(([TupleElementNames(new string[]
			{
				"g",
				"shot"
			})] ValueTuple<XRayViewer.Group, int> r) => r.Item1.Key == this._groupKey && r.Item2 == this._shot);
			float num2 = 0.21499999f;
			float num3 = 0.64699996f - num2 - 0.022f;
			int num4 = Math.Max(4, Math.Min((int)(num3 / 0.034f), (UiDraw.TextSlotsLeft - 110) / 2));
			float num5 = Math.Max(0.034f, Math.Min(0.042f, num3 / (float)Math.Max(1, Math.Min(num4, Math.Max(list.Count, 8)))));
			float num6 = 1.18f * Math.Min(1.12f, num5 / 0.034f);
			if (num < this._scroll)
			{
				this._scroll = num;
			}
			if (num >= this._scroll + num4)
			{
				this._scroll = num - num4 + 1;
			}
			this._scroll = Math.Max(0, Math.Min(this._scroll, Math.Max(0, list.Count - num4)));
			float num7 = num2 + 0.002f;
			float num8 = -1f;
			float num9 = -1f;
			int i = this._scroll;
			while (i < Math.Min(list.Count, this._scroll + num4))
			{
				ValueTuple<XRayViewer.Group, int> valueTuple = list[i];
				XRayViewer.Group item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				string gk = item.Key;
				int sh = item2;
				UiMouse.Region(0.073f, num7, 0.198f, num5, () => UiMouse.Moved && this.SelectRow(gk, sh), delegate
				{
					this.SelectRow(gk, sh);
				});
				bool flag = i == num;
				float num10 = num7 + (num5 - 0.002f) / 2f;
				if (item2 < 0)
				{
					ValueTuple<int, int, int> valueTuple2 = XRayViewer.StatusColor(this.ManStatus(item));
					if (flag)
					{
						Theme.SelectFill(0.069875f, num7, 0.19812499f, num5 - 0.002f, 235);
					}
					UiDraw.Rect(0.073f, num7, 0.003f, num5 - 0.002f, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255);
					if (flag)
					{
						Theme.RedFrame(0.069875f, num7, 0.19812499f, num5 - 0.002f, 1f, 0);
					}
					int item3;
					int item4;
					int item5;
					if (!flag)
					{
						if (item.NemesisId == null)
						{
							ValueTuple<int, int, int> valueTuple3 = Theme.Title;
							item3 = valueTuple3.Item1;
							item4 = valueTuple3.Item2;
							item5 = valueTuple3.Item3;
						}
						else
						{
							ValueTuple<int, int, int> valueTuple3 = Theme.Gold;
							item3 = valueTuple3.Item1;
							item4 = valueTuple3.Item2;
							item5 = valueTuple3.Item3;
						}
					}
					else
					{
						ValueTuple<int, int, int> valueTuple3 = Theme.SelectedText;
						item3 = valueTuple3.Item1;
						item4 = valueTuple3.Item2;
						item5 = valueTuple3.Item3;
					}
					string str = (item.NemesisId != null) ? "* " : "";
					float num11 = 0.25f * num6;
					UiDraw.Text(XRayViewer.Fit(str + item.Name, num11, 0.17799999f), 0.081f, num10 - num11 * 0.0375f, num11, item3, item4, item5, 255);
				}
				else
				{
					XRayEntry xrayEntry = item.Shots[item2];
					if (num8 < 0f)
					{
						num8 = num7;
					}
					num9 = num10;
					if (flag)
					{
						Theme.SelectBox(0.086f, num7 + 0.001f, 0.182f, num5 - 0.004f, 235);
					}
					ValueTuple<int, int, int> valueTuple4 = XRayViewer.ShotChipColor(xrayEntry);
					UiDraw.Rect(0.0825f, num10 - 0.0008f, 0.0035f, 0.0016f, Theme.Line.Item1, Theme.Line.Item2, Theme.Line.Item3, 220);
					UiDraw.Rect(0.091f, num10 - 0.005f, 0.004f, 0.01f, valueTuple4.Item1, valueTuple4.Item2, valueTuple4.Item3, 255);
					int num12 = flag ? 245 : 200;
					float num13 = 0.22f * num6;
					XRayAnalysis analysis = xrayEntry.Analysis;
					string text;
					if (analysis == null)
					{
						text = null;
					}
					else
					{
						PathResult main = analysis.Main;
						text = ((main != null) ? main.EntryText : null);
					}
					string arg = XRayViewer.Short(text ?? Anatomy.RegionOfBone(xrayEntry.RecordedBoneId));
					UiDraw.Text(XRayViewer.Fit(string.Format("{0}  {1}", item2 + 1, arg), num13, 0.16399999f), 0.099f, num10 - num13 * 0.0375f, num13, num12, num12 - 5, num12 - 15, 255);
				}
				i++;
				num7 += num5;
			}
			if (num8 >= 0f)
			{
				UiDraw.Rect(0.0818f, num8 - 0.002f, 0.0014f, num9 - num8 + 0.002f, Theme.Line.Item1, Theme.Line.Item2, Theme.Line.Item3, 220);
			}
			Theme.ScrollBar(0.273f, num2 + 0.002f, (float)Math.Min(num4, list.Count) * num5, this._scroll, num4, list.Count);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x0015249C File Offset: 0x0015069C
		private void DrawXRay(XRayViewer.Group g)
		{
			float num = 0.677f;
			UiDraw.Rect(0.286f, 0.18299998f, 0.335f, num, 5, 8, 11, 245);
			for (int i = 1; i < 6; i++)
			{
				UiDraw.Rect(0.286f, 0.18299998f + num * (float)i / 6f, 0.335f, 0.001f, 25, 40, 48, 140);
			}
			for (int j = 1; j < 4; j++)
			{
				UiDraw.Rect(0.286f + 0.335f * (float)j / 4f, 0.18299998f, 0.0007f, num, 25, 40, 48, 140);
			}
			UiDraw.Outline(0.286f, 0.18299998f, 0.335f, num, Theme.Line.Item1, Theme.Line.Item2, Theme.Line.Item3, 220, 0.0012f);
			int num2 = (int)((float)UiDraw.GlyphsThisFrame * XRayViewer.GlyphCost) + 25;
			UiMouse.DrawCursorNow();
			int num3 = Math.Max(350, (this._rectLimit * 85 / 100 - UiDraw.RectsThisFrame - num2) / 50 * 50);
			if (this._heldBudget > 0 && num3 >= this._heldBudget - 40 && num3 <= this._heldBudget + 100)
			{
				num3 = this._heldBudget;
			}
			else
			{
				this._heldBudget = num3;
			}
			int num4 = this._renderer.Draw(0.29000002f, 0.21299998f, 0.32700002f, num - 0.06f, UiDraw.RealDt, UiDraw.RealMs, num3);
			string text = string.Format("{0}|{1}|{2}|{3}|{4}", new object[]
			{
				g.Key,
				this._shot,
				num3,
				this._renderer.ActiveLevel,
				this._renderer.LastCut > 0
			});
			if (UiDraw.RealMs - this._diagAt > 4000 && text != this._diagFor)
			{
				this._diagAt = UiDraw.RealMs;
				this._diagFor = text;
				Action<string> diag = XRayViewer.Diag;
				if (diag != null)
				{
					diag(string.Format("XRAY frame: limit {0}, before the skeleton {1} rects + {2} letters (x{3:0.0}) -> budget {4}, drew {5}, smoothness {6}, level {7}, zoom {8:0.00}, cut {9}, plans so far {10}", new object[]
					{
						this._rectLimit,
						UiDraw.RectsThisFrame - num4,
						UiDraw.GlyphsThisFrame,
						XRayViewer.GlyphCost,
						num3,
						num4,
						this._renderer.LastStepCap,
						this._renderer.ActiveLevel,
						this._renderer.Zoom,
						this._renderer.LastCut,
						this._renderer.PlanCount
					}));
				}
			}
			if (XRayViewer.DebugOn)
			{
				UiDraw.Text(string.Format("{0}/{1} rects   smoothness {2}{3}", new object[]
				{
					num4,
					num3,
					this._renderer.LastStepCap,
					(this._renderer.LastDropped > 0) ? string.Format("   {0} extras skipped", this._renderer.LastDropped) : ""
				}), 0.294f, 0.81399995f, 0.19f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			}
			Theme.Label((this._shot < 0) ? "TODAS LAS HERIDAS" : string.Format("DISPARO {0} DE {1}", this._shot + 1, g.Shots.Count), 0.294f, 0.18899998f, 0.24f);
			if (XRayViewer.DebugOn)
			{
				UiDraw.Text((this._quality == XRayQuality.Auto) ? string.Format("Auto detail: {0}", this._renderer.ActiveLevel) : string.Format("{0} detail", this._quality), 0.5360001f, 0.18899998f, 0.21f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			}
			if (this._composite.Paths.Any(([TupleElementNames(new string[]
			{
				"path",
				"thin"
			})] ValueTuple<PathResult, bool> p) => p.Item1.Estimated))
			{
				UiDraw.Text("thin / dimmed paths are estimated", 0.294f, 0.83599997f, 0.2f, 190, 160, 90, 255);
			}
			UiDraw.Text(this._renderer.AutoSpin ? "spinning" : "", 0.57600003f, 0.83599997f, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x001529A0 File Offset: 0x00150BA0
		private void HandleMouse()
		{
			this._renderer.PanX = (this._renderer.PanY = 0f);
			if (!UiMouse.On)
			{
				this._dragOrbit = false;
				return;
			}
			UiMouse.WheelHandler = delegate(int d)
			{
				this._renderer.ZoomBy((d > 0) ? 1.15f : 0.86956525f);
			};
			if (UiMouse.Clicked && UiMouse.Over(0.286f, 0.18299998f, 0.335f, 0.677f))
			{
				this._dragOrbit = true;
			}
			if (!UiMouse.Down)
			{
				this._dragOrbit = false;
			}
			if (this._dragOrbit && UiMouse.Moved)
			{
				this._renderer.Orbit(-UiMouse.DX * 300f, UiMouse.DY * 200f);
			}
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00152A54 File Offset: 0x00150C54
		private void DrawCalibration()
		{
			if (this._calPage == 1)
			{
				this.DrawSpriteTest();
				return;
			}
			for (int i = 0; i < 40; i++)
			{
				for (int j = 0; j < 50; j++)
				{
					UiDraw.Rect(0.12f + (float)j * 0.0152f, 0.1f + (float)i * 0.02f, 0.01216f, 0.0139999995f, (i == this._calRow) ? 235 : 110, (i == this._calRow) ? 200 : 190, (i == this._calRow) ? 90 : 110, 255);
				}
			}
			UiDraw.Text("PAGE 1/2  RECT LIMIT - find the FIRST row with any missing cell, put the arrows on the row ABOVE it, press Enter.", 0.03f, 0.035f, 0.3f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255);
			UiDraw.Text("Backspace cancels.  Left/Right: page 2 (sprite test).", 0.62f, 0.065f, 0.24f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			UiDraw.Text(string.Format("Currently saved limit: {0}.   Selected: {1}", this._rectLimit, (this._calRow + 1) * 50), 0.03f, 0.065f, 0.26f, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 255);
			for (int k = 0; k < 40; k++)
			{
				bool flag = k == this._calRow;
				ValueTuple<int, int, int> valueTuple = flag ? Theme.Gold : Theme.Dim;
				UiDraw.Text(string.Format("{0}", (k + 1) * 50), 0.07f, 0.1f + (float)k * 0.02f - 0.003f, 0.22f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
				if (flag)
				{
					UiDraw.Text(">>", 0.1f, 0.1f + (float)k * 0.02f - 0.004f, 0.25f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
					UiDraw.Text("<<", 0.885f, 0.1f + (float)k * 0.02f - 0.004f, 0.25f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
				}
			}
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00152CC0 File Offset: 0x00150EC0
		private bool SelectRow(string group, int shot)
		{
			if (group == this._groupKey && shot == this._shot)
			{
				return false;
			}
			if (group != this._groupKey)
			{
				this._renderer.ResetView();
			}
			this._groupKey = group;
			this._shot = shot;
			return true;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00152D10 File Offset: 0x00150F10
		private void DrawSpriteTest()
		{
			bool flag = UiDraw.EnsureTextureDict("generic_textures");
			bool flag2 = UiDraw.EnsureTextureDict("feeds");
			UiDraw.Text("PAGE 2/2  SPRITE TEST - tell me which numbers show clean SOLID bars in columns A, B and C.  Left/Right: page 1.", 0.03f, 0.035f, 0.3f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255);
			UiDraw.Text("textures loaded: generic_textures " + (flag ? "YES" : "no") + ", feeds " + (flag2 ? "YES" : "no"), 0.03f, 0.065f, 0.24f, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 255);
			float[] array = new float[]
			{
				0.32f,
				0.56f,
				0.8f
			};
			string[] array2 = new string[]
			{
				"A: on its own",
				"B: over a dark box",
				"C: dark box + draw order"
			};
			for (int i = 0; i < 3; i++)
			{
				UiDraw.Text(array2[i], array[i] - 0.08f, 0.095f, 0.26f, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 255);
			}
			for (int j = 0; j < XRayViewer.SpriteCandidates.Length; j++)
			{
				float num = 0.16f + (float)j * 0.095f;
				UiDraw.Text(string.Format("{0}  {1}", j + 1, XRayViewer.SpriteCandidates[j][1]), 0.03f, num - 0.012f, 0.25f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255);
				for (int k = 0; k < 3; k++)
				{
					if (k > 0)
					{
						UiDraw.Rect(array[k] - 0.09f, num - 0.04f, 0.18f, 0.08f, 25, 25, 30, 255);
					}
					if (k == 2)
					{
						UiDraw.SetDrawOrder(7);
					}
					foreach (float num2 in new float[]
					{
						0f,
						45f,
						90f
					})
					{
						float cx = array[k] + (num2 - 45f) / 45f * 0.05f;
						UiDraw.Sprite(XRayViewer.SpriteCandidates[j][0], XRayViewer.SpriteCandidates[j][1], cx, num, 0.045f, 0.008f, num2, 255, 255, 255, 255);
					}
					if (k == 2)
					{
						UiDraw.SetDrawOrder(4);
					}
				}
			}
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00152FBF File Offset: 0x001511BF
		private void FieldStart(float y)
		{
			this._fx = 0.63900006f;
			this._fvx = 0.712f;
			this._fy = y;
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00152FE0 File Offset: 0x001511E0
		private void Field(string label, string value, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int>? color = null)
		{
			Theme.Label(label, this._fx, this._fy, 0.23f);
			ValueTuple<int, int, int> valueTuple = color ?? Theme.Text;
			UiDraw.Text(value, this._fvx, this._fy - 0.001f, 0.26f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
			this._fy += 0.026f;
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00153064 File Offset: 0x00151264
		private void Section(string title)
		{
			this._fy += 0.006f;
			UiDraw.Rect(this._fx, this._fy + 0.024f, 0.27899998f, 0.0015f, Theme.Line.Item1, Theme.Line.Item2, Theme.Line.Item3, 200);
			Theme.Label(title, 0.7785f - Theme.CapsWidth(title, 0.27f) / 2f - 0.0008f, this._fy, 0.27f);
			this._fy += 0.032f;
		}

		// Token: 0x17000340 RID: 832
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00153107 File Offset: 0x00151307
		private float RowW
		{
			get
			{
				return 0.27899998f;
			}
		}

		// Token: 0x060014FA RID: 5370 RVA: 0x00153110 File Offset: 0x00151310
		private static string Fit(string text, float scale, float width)
		{
			if (string.IsNullOrEmpty(text))
			{
				return text;
			}
			int num = Math.Max(4, (int)(width / (scale * 0.0175f)));
			if (text.Length > num)
			{
				return text.Substring(0, num - 3).TrimEnd(new char[]
				{
					' ',
					',',
					'-'
				}) + "...";
			}
			return text;
		}

		// Token: 0x060014FB RID: 5371 RVA: 0x00153170 File Offset: 0x00151370
		private void Banner(string text, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> c, string right = null)
		{
			UiDraw.Rect(this._fx, this._fy, this.RowW, 0.038f, c.Item1 / 4, c.Item2 / 4, c.Item3 / 4, 220);
			UiDraw.Rect(this._fx, this._fy, 0.004f, 0.038f, c.Item1, c.Item2, c.Item3, 255);
			if (right != null)
			{
				UiDraw.TextFont(text, UiDraw.TitleFont, this._fx + 0.012f, this._fy + 0.004f, 0.38f, c.Item1, c.Item2, c.Item3, 255, false);
				UiDraw.Text(right, this._fx + this.RowW - 0.01f - Theme.MixedWidth(right, 0.25f, 0.0222f), this._fy + 0.01f, 0.25f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
			}
			else
			{
				UiDraw.TextFont(text, UiDraw.TitleFont, this._fx + this.RowW / 2f - Theme.CapsWidth(text, 0.38f) / 2f - 0.0008f, this._fy + 0.004f, 0.38f, c.Item1, c.Item2, c.Item3, 255, false);
			}
			this._fy += 0.048f;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x00153300 File Offset: 0x00151500
		private void Pair(string l1, string v1, string l2, string v2, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int>? c2 = null)
		{
			float num = this.RowW / 2f;
			Theme.Label(l1, this._fx, this._fy, 0.2f);
			UiDraw.Text(XRayViewer.Fit(v1, 0.25f, (l2 != null) ? (num - 0.008f) : this.RowW), this._fx, this._fy + 0.015f, 0.25f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
			if (l2 != null)
			{
				ValueTuple<int, int, int> valueTuple = c2 ?? Theme.Text;
				Theme.Label(l2, this._fx + num, this._fy, 0.2f);
				UiDraw.Text(XRayViewer.Fit(v2, 0.25f, num), this._fx + num, this._fy + 0.015f, 0.25f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
			}
			this._fy += 0.042f;
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x00153418 File Offset: 0x00151618
		private void WoundTrack(PathResult m, bool blade)
		{
			XRayViewer.<>c__DisplayClass89_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			this.Section(blade ? "RASTRO DE LA HERIDA" : "TRAYECTORIA DE LA BALA");
			float num = (m.End - m.Entry).Length() * 100f;
			CS$<>8__locals1.markW = 0.008f / UiDraw.Aspect * 1.6f;
			float fy = this._fy;
			this.<WoundTrack>g__Mark|89_0(255, 255, 255, ref CS$<>8__locals1);
			Theme.Label(blade ? "ENTRA" : "ENTRADA", this._fx + 0.012f, this._fy, 0.2f);
			UiDraw.Text(XRayViewer.Fit(XRayViewer.Capitalize(m.EntryText) + (m.Estimated ? "  (est.)" : ""), 0.24f, this.RowW - 0.06f), this._fx + 0.06f, this._fy - 0.001f, 0.24f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
			this._fy += 0.022f;
			UiDraw.Text(string.Format("{0:0} cm a través del cuerpo{1}", num, m.Exited ? (blade ? ", corte limpio de lado a lado" : ", sobrepenetración") : ""), this._fx + 0.012f, this._fy - 0.004f, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			this._fy += 0.016f;
			float num2 = 0.0010416667f;
			float num3 = fy + 0.004f + 0.012f + 0.003f;
			float num4 = this._fy + 0.004f - 0.003f;
			UiDraw.Rect(this._fx + CS$<>8__locals1.markW / 2f - num2 / 2f, num3, num2, num4 - num3, Theme.Line.Item1, Theme.Line.Item2, Theme.Line.Item3, 220);
			if (m.Exited)
			{
				this.<WoundTrack>g__Mark|89_0(240, 140, 50, ref CS$<>8__locals1);
			}
			else
			{
				this.<WoundTrack>g__Mark|89_0(255, 215, 70, ref CS$<>8__locals1);
			}
			Theme.Label(m.Exited ? "SALIDA" : (blade ? "DETENIDA" : "ALOJADA"), this._fx + 0.012f, this._fy, 0.2f);
			UiDraw.Text(XRayViewer.Fit(XRayViewer.Capitalize(m.EndText.Replace("Salió por ", "").Replace("Alojada en ", "").Replace("Hoja detenida en ", "")), 0.24f, this.RowW - 0.06f), this._fx + 0.06f, this._fy - 0.001f, 0.24f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
			this._fy += 0.026f;
		}

		// Token: 0x060014FE RID: 5374 RVA: 0x00153760 File Offset: 0x00151960
		private static string BlowKinds(XRayEntry e)
		{
			List<Blow> list = e.Blows ?? new List<Blow>();
			if (list.Count == 0)
			{
				return string.Format("{0}", Math.Max(1, e.Pellets));
			}
			int num = list.Count((Blow x) => x.Stomp);
			string[] array = new string[4];
			array[0] = XRayViewer.<BlowKinds>g__Part|94_0(list.Count((Blow x) => x.Kind == BlowKind.Punch), "punch", "puñetazos");
			int num2 = 1;
			string text;
			if (num <= 0)
			{
				text = XRayViewer.<BlowKinds>g__Part|94_0(list.Count((Blow x) => x.Kind == BlowKind.Kick), "patada", "patadas");
			}
			else
			{
				text = XRayViewer.<BlowKinds>g__Part|94_0(list.Count((Blow x) => x.Kind == BlowKind.Kick), "patada", "patadas") + " (" + XRayViewer.<BlowKinds>g__Part|94_0(num, "pisotón", "pisotones") + ")";
			}
			array[num2] = text;
			array[2] = XRayViewer.<BlowKinds>g__Part|94_0(list.Count((Blow x) => x.Kind == BlowKind.Gun), "culatazo", "culatazos");
			array[3] = XRayViewer.<BlowKinds>g__Part|94_0(list.Count((Blow x) => x.Kind == BlowKind.Choke), "asfixia", "asfixias");
			List<string> list2 = (from p in array
			where p != null
			select p).ToList<string>();
			if (list2.Count != 1)
			{
				return string.Format("{0}: {1}", list.Count, string.Join(", ", list2));
			}
			return list2[0];
		}

		// Token: 0x060014FF RID: 5375 RVA: 0x00153960 File Offset: 0x00151B60
		private void BlowsByRegion(XRayEntry e, XRayAnalysis a)
		{
			this.Section("GOLPES");
			float w = 0.008f / UiDraw.Aspect * 1.6f;
			List<Blow> source = e.Blows ?? new List<Blow>();
			ValueTuple<BlowKind, string, ValueTuple<int, int, int>>[] array = new ValueTuple<BlowKind, string, ValueTuple<int, int, int>>[]
			{
				new ValueTuple<BlowKind, string, ValueTuple<int, int, int>>(BlowKind.Punch, "Puñetazos", XRayViewer.PunchColor),
				new ValueTuple<BlowKind, string, ValueTuple<int, int, int>>(BlowKind.Kick, "Patadas", XRayViewer.KickColor),
				new ValueTuple<BlowKind, string, ValueTuple<int, int, int>>(BlowKind.Gun, "Con el arma", XRayViewer.GunColor),
				new ValueTuple<BlowKind, string, ValueTuple<int, int, int>>(BlowKind.Choke, "Estrangulado", XRayViewer.ChokeColor)
			};
			for (int i = 0; i < array.Length; i++)
			{
				ValueTuple<BlowKind, string, ValueTuple<int, int, int>> valueTuple = array[i];
				BlowKind kind = valueTuple.Item1;
				string item = valueTuple.Item2;
				ValueTuple<int, int, int> item2 = valueTuple.Item3;
				int num = source.Count((Blow x) => x.Kind == kind);
				if (num != 0)
				{
					UiDraw.Rect(this._fx, this._fy + 0.004f, w, 0.012f, item2.Item1, item2.Item2, item2.Item3, 255);
					int num2;
					if (kind != BlowKind.Kick)
					{
						num2 = 0;
					}
					else
					{
						num2 = source.Count((Blow x) => x.Stomp);
					}
					int num3 = num2;
					UiDraw.Text((num3 > 0) ? string.Format("{0} ({1} {2})", item, num3, (num3 == 1) ? "pisotón" : "pisotones") : item, this._fx + 0.012f, this._fy - 0.001f, 0.24f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
					string text = string.Format("{0}", num);
					UiDraw.Text(text, this._fx + this.RowW - Theme.MixedWidth(text, 0.22f, 0.0222f), this._fy, 0.22f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
					this._fy += 0.022f;
				}
			}
			this._fy += 0.004f;
			Theme.Label("DÓNDE", this._fx, this._fy, 0.2f);
			this._fy += 0.02f;
			foreach (ValueTuple<string, int> valueTuple2 in a.Blows.Take(5))
			{
				string item3 = valueTuple2.Item1;
				int item4 = valueTuple2.Item2;
				UiDraw.Rect(this._fx, this._fy + 0.004f, w, 0.012f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
				UiDraw.Text(XRayViewer.Capitalize(item3), this._fx + 0.012f, this._fy - 0.001f, 0.24f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
				string text2 = (item4 == 1) ? "1 golpe" : string.Format("{0} golpes", item4);
				UiDraw.Text(text2, this._fx + this.RowW - Theme.MixedWidth(text2, 0.22f, 0.0222f), this._fy, 0.22f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
				this._fy += 0.022f;
			}
			this._fy += 0.006f;
		}

		// Token: 0x06001500 RID: 5376 RVA: 0x00153D7C File Offset: 0x00151F7C
		private void TraumaMeter(List<DamageItem> dmg)
		{
			dmg = (from d in dmg
			where d.Key != "verdict"
			select d).ToList<DamageItem>();
			int num = Math.Min(100, (from d in dmg
			where !d.Chipped
			select d).Sum((DamageItem d) => XRayViewer.SeverityWeight(d.Severity)) + dmg.Count((DamageItem d) => d.Chipped) * 2);
			ValueTuple<int, int, int> valueTuple = XRayViewer.SeverityColor((from d in dmg
			where !d.Chipped
			select d.Severity).DefaultIfEmpty(Severity.Minor).Max<Severity>());
			Theme.Label("TRAUMA", this._fx, this._fy, 0.23f);
			float num2 = this._fx + 0.066f;
			float num3 = this.RowW - 0.066f - 0.048f;
			UiDraw.Rect(num2, this._fy + 0.005f, num3, 0.011f, 40, 34, 28, 230);
			UiDraw.Rect(num2, this._fy + 0.005f, num3 * (float)num / 100f, 0.011f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
			UiDraw.Text(string.Format("{0}", num), num2 + num3 + 0.008f, this._fy - 0.001f, 0.27f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
			this._fy += 0.03f;
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00153F76 File Offset: 0x00152176
		private static int SeverityWeight(Severity s)
		{
			if (s == Severity.Fatal)
			{
				return 100;
			}
			if (s == Severity.Critical)
			{
				return 35;
			}
			if (s == Severity.Severe)
			{
				return 18;
			}
			if (s != Severity.Moderate)
			{
				return 3;
			}
			return 8;
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00153F94 File Offset: 0x00152194
		private static string SeverityTag(DamageItem d)
		{
			if (!d.Chipped)
			{
				if (d.Severity == Severity.Fatal)
				{
					return "MORTAL";
				}
				if (d.Severity == Severity.Critical)
				{
					return "CRÍTICO";
				}
				if (d.Severity == Severity.Severe)
				{
					return "GRAVE";
				}
				if (d.Severity != Severity.Moderate)
				{
					return "LEVE";
				}
				return "MODERADO";
			}
			else
			{
				if (!d.Name.Contains("ROZADO"))
				{
					return "ASTILLADO";
				}
				return "ROZADO";
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00154008 File Offset: 0x00152208
		private static bool IsVessel(string key)
		{
			return key.StartsWith("carotid") || key.StartsWith("femoral") || key == "windpipe";
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00154034 File Offset: 0x00152234
		private void InjuryList(IEnumerable<DamageItem> items, int max, bool notes)
		{
			List<DamageItem> list = items.ToList<DamageItem>();
			if (list.Count == 0)
			{
				Theme.Body("Solo herida superficial: ningún órgano vital alcanzado", this._fx, this._fy, 0.23f);
				this._fy += 0.024f;
				return;
			}
			ValueTuple<string, List<DamageItem>>[] array = new ValueTuple<string, List<DamageItem>>[5];
			array[0] = new ValueTuple<string, List<DamageItem>>("CÓMO CAYÓ", (from d in list
			where d.Key == "verdict"
			select d).ToList<DamageItem>());
			array[1] = new ValueTuple<string, List<DamageItem>>("ESQUELETO", (from d in list
			where d.IsBone
			select d).ToList<DamageItem>());
			array[2] = new ValueTuple<string, List<DamageItem>>("ÓRGANOS", (from d in list
			where !d.IsBone && !XRayViewer.IsVessel(d.Key) && d.Key != "flesh" && d.Key != "verdict"
			select d).ToList<DamageItem>());
			array[3] = new ValueTuple<string, List<DamageItem>>("VASOS Y VÍAS RESPIRATORIAS", (from d in list
			where !d.IsBone && XRayViewer.IsVessel(d.Key)
			select d).ToList<DamageItem>());
			array[4] = new ValueTuple<string, List<DamageItem>>("TEJIDO BLANDO", (from d in list
			where d.Key == "flesh"
			select d).ToList<DamageItem>());
			int num = 0;
			foreach (ValueTuple<string, List<DamageItem>> valueTuple in array)
			{
				string item = valueTuple.Item1;
				List<DamageItem> item2 = valueTuple.Item2;
				if (item2.Count != 0 && num < max)
				{
					Theme.Label(string.Format("{0}  ({1})", item, item2.Count), this._fx, this._fy, 0.22f);
					this._fy += 0.023f;
					foreach (DamageItem damageItem in item2.OrderByDescending(delegate(DamageItem x)
					{
						if (!x.Chipped)
						{
							return (int)x.Severity;
						}
						return -1;
					}))
					{
						if (num >= max || UiDraw.TextSlotsLeft < 10 || this._fy > 0.81499994f)
						{
							max = num;
							break;
						}
						num++;
						ValueTuple<int, int, int> valueTuple2 = damageItem.Chipped ? Theme.Dim : XRayViewer.SeverityColor(damageItem.Severity);
						UiDraw.Rect(this._fx, this._fy + 0.006f, 0.005f, 0.014f, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255);
						string text = XRayViewer.SeverityTag(damageItem);
						float num2 = Theme.CapsWidth(text, 0.235f);
						UiDraw.Text(XRayViewer.Fit(XRayViewer.Tidy(damageItem.Name), 0.275f, this.RowW - 0.022f - num2), this._fx + 0.011f, this._fy, 0.275f, damageItem.Chipped ? Theme.Dim.Item1 : Theme.Text.Item1, damageItem.Chipped ? Theme.Dim.Item2 : Theme.Text.Item2, damageItem.Chipped ? Theme.Dim.Item3 : Theme.Text.Item3, 255);
						UiDraw.TextFont(text, UiDraw.TitleFont, this._fx + this.RowW - num2 - 0.0008f, this._fy + 0.003f, 0.235f, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255, false);
						this._fy += 0.025f;
						string text2 = notes ? XRayViewer.Consequence(damageItem) : null;
						if (text2 != null)
						{
							UiDraw.Text(text2, this._fx + 0.009f, this._fy - 0.002f, 0.19f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
							this._fy += 0.016f;
						}
					}
					this._fy += 0.004f;
				}
			}
			if (list.Count > num)
			{
				UiDraw.Text(string.Format("+{0} more", list.Count - num), this._fx, this._fy, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
				this._fy += 0.02f;
			}
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x0015451C File Offset: 0x0015271C
		private static string Consequence(DamageItem d)
		{
			if (!d.Chipped)
			{
				string text = (d.Key.Length > 2 && d.Key[d.Key.Length - 2] == '_') ? d.Key.Substring(0, d.Key.Length - 2) : d.Key;
				if (text != null)
				{
					switch (text.Length)
					{
					case 3:
						if (text == "jaw")
						{
							return "No puede hablar ni comer bien";
						}
						break;
					case 4:
					{
						char c = text[0];
						switch (c)
						{
						case 'f':
							if (text == "foot")
							{
								return "Camina cojeando";
							}
							break;
						case 'g':
						case 'i':
						case 'j':
							break;
						case 'h':
							if (text == "hand")
							{
								return "No puede agarrar";
							}
							break;
						case 'k':
							if (text == "knee")
							{
								return "Pierna bloqueada: no puede mantenerse en pie";
							}
							break;
						case 'l':
							if (text == "lung")
							{
								return "El pulmón colapsa: se ahoga en sangre";
							}
							break;
						default:
							if (c == 'r')
							{
								if (text == "ribs")
								{
									return "Cada respiración duele";
								}
							}
							break;
						}
						break;
					}
					case 5:
					{
						char c = text[2];
						if (c <= 'i')
						{
							switch (c)
							{
							case 'a':
								if (text == "brain")
								{
									return "La muerte es instantánea";
								}
								if (text == "heart")
								{
									return "El corazón se detiene en segundos";
								}
								break;
							case 'b':
								if (text == "tibia")
								{
									return "No puede apoyar peso en la pierna";
								}
								break;
							case 'c':
							case 'd':
								break;
							case 'e':
								if (text == "flesh")
								{
									return "Sangra, pero nada vital";
								}
								break;
							default:
								if (c == 'i')
								{
									if (text == "spine")
									{
										return "Paralizado por debajo de la herida";
									}
								}
								break;
							}
						}
						else if (c != 'm')
						{
							if (c != 'u')
							{
								if (c == 'v')
								{
									if (text == "liver")
									{
										return "Fuerte hemorragia interna";
									}
								}
							}
							else if (text == "skull")
							{
								return "Conmoción, probable daño cerebral";
							}
						}
						else if (text == "femur")
						{
							return "La pierna no aguanta peso";
						}
						break;
					}
					case 6:
					{
						char c = text[0];
						if (c != 'k')
						{
							if (c == 'p')
							{
								if (text == "pelvis")
								{
									return "No puede mantenerse en pie";
								}
							}
						}
						else if (text == "kidney")
						{
							return "Hemorragia interna, deterioro lento";
						}
						break;
					}
					case 7:
					{
						char c = text[1];
						if (c <= 'l')
						{
							switch (c)
							{
							case 'a':
								if (text == "carotid")
								{
									return "Se desangra en menos de un minuto";
								}
								break;
							case 'b':
							case 'd':
								break;
							case 'c':
								if (text == "scapula")
								{
									return "Hombro débil, cuesta levantar el brazo";
								}
								break;
							case 'e':
								if (text == "femoral")
								{
									return "Se desangra en minutos sin un torniquete";
								}
								break;
							default:
								if (c == 'l')
								{
									if (text == "bladder")
									{
										return "Doloroso, rara vez mortal";
									}
								}
								break;
							}
						}
						else if (c != 'o')
						{
							if (c != 't')
							{
								if (c == 'u')
								{
									if (text == "humerus")
									{
										return "Brazo inútil";
									}
								}
							}
							else
							{
								if (text == "stomach")
								{
									return "Herida abdominal: infección, muerte agonizante";
								}
								if (text == "sternum")
								{
									return "Pecho aplastado, respiración trabajosa";
								}
							}
						}
						else if (text == "forearm")
						{
							return "No puede sostener un arma con esa mano";
						}
						break;
					}
					case 8:
					{
						char c = text[1];
						if (c != 'e')
						{
							if (c != 'i')
							{
								if (c == 'l')
								{
									if (text == "clavicle")
									{
										return "No puede levantar el brazo";
									}
								}
							}
							else if (text == "windpipe")
							{
								return "Ahogándose en su propia sangre";
							}
						}
						else if (text == "cervical")
						{
							return "Paralizado del cuello para abajo";
						}
						break;
					}
					case 10:
						if (text == "intestines")
						{
							return "Herida abdominal: sepsis en pocos días";
						}
						break;
					}
				}
				return null;
			}
			if (!d.Key.StartsWith("spine") && !(d.Key == "cervical"))
			{
				return "Bone bruised, not broken - still works";
			}
			return "Bone bruised; cord intact - he can still move";
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x001549F0 File Offset: 0x00152BF0
		private void InfoHeader(string name, string sub, bool nemesis)
		{
			Theme.PanelRect(0.62700003f, 0.18299998f, 0.30299997f, 0.677f, 215);
			ValueTuple<int, int, int> valueTuple = nemesis ? Theme.Gold : Theme.Title;
			string text = name.ToUpperInvariant();
			float num = 0.44f;
			while (num > 0.3f && Theme.CapsWidth(text, num) > 0.27299997f)
			{
				num -= 0.02f;
			}
			float num2 = 0.7785f;
			UiDraw.TextFont(text, UiDraw.TitleFont, num2 - Theme.CapsWidth(text, num) / 2f - 0.0008f, 0.19099998f, num, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255, false);
			sub = XRayViewer.Fit(sub, 0.25f, 0.27299997f);
			UiDraw.Text(sub, num2 - Theme.MixedWidth(sub, 0.25f, 0.0222f) / 2f, 0.22899999f, 0.25f, Theme.Subtitle.Item1, Theme.Subtitle.Item2, Theme.Subtitle.Item3, 255);
			this.FieldStart(0.26299998f);
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00154B04 File Offset: 0x00152D04
		private static string ManSub(XRayViewer.Group g)
		{
			if (g.NemesisId != null)
			{
				return "Nemesis  -  " + g.Sub;
			}
			if (g.FollowerOf == null)
			{
				return g.Sub;
			}
			return "Rode with " + g.FollowerOf;
		}

		// Token: 0x06001508 RID: 5384 RVA: 0x00154B40 File Offset: 0x00152D40
		private void DrawManSummary(XRayViewer.Group g)
		{
			this.InfoHeader(g.Name, XRayViewer.ManSub(g), g.NemesisId != null);
			string text = this.ManStatus(g);
			g.Shots.Sum((XRayEntry s) => Math.Max(1, s.Pellets));
			g.Shots.Any((XRayEntry s) => s.WeaponClass == WeaponClass.Shotgun);
			this.Banner(text, XRayViewer.StatusColor(text), null);
			List<DamageItem> list = this._composite.Damage.ToList<DamageItem>();
			this.Section("TODAS LAS LESIONES");
			this.TraumaMeter(list);
			this.InjuryList(list, 16, false);
		}

		// Token: 0x06001509 RID: 5385 RVA: 0x00154C04 File Offset: 0x00152E04
		private void DrawShot(XRayViewer.Group g, int index)
		{
			XRayEntry xrayEntry = g.Shots[index];
			XRayAnalysis analysis = xrayEntry.Analysis;
			bool blade = Anatomy.IsBlade(xrayEntry.WeaponClass);
			this.InfoHeader(g.Name, XRayViewer.ManSub(g), g.NemesisId != null);
			string text = this.ShotVerdict(xrayEntry);
			this.Banner(text, XRayViewer.StatusColor(text), null);
			if (((analysis != null) ? analysis.Main : null) == null)
			{
				this.Section("WOUND");
				Theme.Body("Grazed " + Anatomy.RegionOfBone(xrayEntry.RecordedBoneId) + ".", this._fx, this._fy, 0.25f);
				return;
			}
			if (Anatomy.IsBlunt(xrayEntry.WeaponClass))
			{
				this.BlowsByRegion(xrayEntry, analysis);
			}
			else
			{
				this.WoundTrack(analysis.Main, blade);
			}
			List<DamageItem> list = analysis.AllDamage.ToList<DamageItem>();
			this.Section("DAÑO");
			this.TraumaMeter(list);
			this.InjuryList(list, 11, false);
		}

		// Token: 0x0600150A RID: 5386 RVA: 0x00154CFC File Offset: 0x00152EFC
		private string ManStatus(XRayViewer.Group g)
		{
			if (g.NemesisId != null)
			{
				switch (this._fateOf(g.NemesisId))
				{
				case NemesisFate.Hunting:
					if (!g.Shots.Any((XRayEntry s) => s.FatalShot))
					{
						return "DÁNDOTE CAZA";
					}
					return "BURLÓ A LA MUERTE";
				case NemesisFate.PresumedDead:
					return "DADO POR MUERTO";
				case NemesisFate.CheatedDeath:
					if (!g.Shots.Any((XRayEntry s) => s.FatalShot))
					{
						return "DÁNDOTE CAZA";
					}
					return "BURLÓ A LA MUERTE";
				case NemesisFate.Dead:
					return "MUERTO";
				}
			}
			if (g.Shots.Any((XRayEntry s) => s.Killed))
			{
				return "KILLED";
			}
			if (!g.Shots.Any((XRayEntry s) => s.KnockedOut))
			{
				return "SOBREVIVIÓ";
			}
			return "KNOCKED OUT";
		}

		// Token: 0x0600150B RID: 5387 RVA: 0x00154E28 File Offset: 0x00153028
		private string ShotVerdict(XRayEntry e)
		{
			if (e.NemesisId != null && (e.FatalShot || e.Killed))
			{
				NemesisFate nemesisFate = this._fateOf(e.NemesisId);
				if (e.FatalShot && nemesisFate == NemesisFate.PresumedDead)
				{
					return "DADO POR MUERTO";
				}
				if (nemesisFate == NemesisFate.CheatedDeath || nemesisFate == NemesisFate.Hunting)
				{
					if (!e.FatalShot)
					{
						return "SOBREVIVIÓ";
					}
					return "SOBREVIVIÓ A ESTO";
				}
			}
			if (Anatomy.IsBlunt(e.WeaponClass))
			{
				bool flag;
				if (e.Blows != null && e.Blows.Count > 0)
				{
					flag = e.Blows.All((Blow b) => b.Choke);
				}
				else
				{
					flag = false;
				}
				if (flag)
				{
					if (e.FatalShot)
					{
						return "ESTRANGULADO";
					}
					if (e.Killed)
					{
						return "MURIÓ DESPUÉS";
					}
					if (!e.KnockedOut)
					{
						return "ESTRANGULADO";
					}
					return "ESTRANGULADO INCONSCIENTE";
				}
				else
				{
					if (e.LeftForDead)
					{
						return "DADO POR MUERTO";
					}
					if (e.FatalShot)
					{
						return "MUERTO A GOLPES";
					}
					if (e.Killed)
					{
						return "MURIÓ DESPUÉS";
					}
					if (!e.KnockedOut)
					{
						return "APALEADO";
					}
					return "KNOCKED OUT";
				}
			}
			else
			{
				if (e.FatalShot)
				{
					return "DISPARO MORTAL";
				}
				if (!e.Killed)
				{
					return "SOBREVIVIÓ";
				}
				return "MURIÓ DESPUÉS";
			}
		}

		// Token: 0x0600150C RID: 5388 RVA: 0x00154F74 File Offset: 0x00153174
		[return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static ValueTuple<int, int, int> StatusColor(string status)
		{
			if (status != null)
			{
				switch (status.Length)
				{
				case 4:
					if (!(status == "MUERTO"))
					{
						goto IL_130;
					}
					break;
				case 5:
				case 7:
				case 8:
				case 9:
				case 14:
					goto IL_130;
				case 6:
					if (!(status == "KILLED"))
					{
						goto IL_130;
					}
					break;
				case 10:
					if (!(status == "MURIÓ DESPUÉS"))
					{
						goto IL_130;
					}
					goto IL_124;
				case 11:
				{
					char c = status[0];
					if (c != 'H')
					{
						if (c != 'K')
						{
							goto IL_130;
						}
						if (!(status == "KNOCKED OUT"))
						{
							goto IL_130;
						}
						goto IL_124;
					}
					else
					{
						if (!(status == "DÁNDOTE CAZA"))
						{
							goto IL_130;
						}
						goto IL_12A;
					}
					break;
				}
				case 12:
					if (!(status == "DISPARO MORTAL"))
					{
						goto IL_130;
					}
					break;
				case 13:
				{
					char c = status[0];
					if (c != 'C')
					{
						if (c != 'P')
						{
							goto IL_130;
						}
						if (!(status == "DADO POR MUERTO"))
						{
							goto IL_130;
						}
						goto IL_124;
					}
					else
					{
						if (!(status == "BURLÓ A LA MUERTE"))
						{
							goto IL_130;
						}
						goto IL_12A;
					}
					break;
				}
				case 15:
					if (!(status == "MUERTO A GOLPES"))
					{
						goto IL_130;
					}
					break;
				case 16:
					if (!(status == "SOBREVIVIÓ A ESTO"))
					{
						goto IL_130;
					}
					goto IL_12A;
				default:
					goto IL_130;
				}
				return Theme.Blood;
				IL_124:
				return Theme.Warn;
				IL_12A:
				return Theme.Gold;
			}
			IL_130:
			return Theme.Good;
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x001550B8 File Offset: 0x001532B8
		[return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static ValueTuple<int, int, int> SeverityColor(Severity s)
		{
			switch (s)
			{
			case Severity.Moderate:
				return new ValueTuple<int, int, int>(220, 210, 160);
			case Severity.Severe:
				return new ValueTuple<int, int, int>(230, 175, 75);
			case Severity.Critical:
				return new ValueTuple<int, int, int>(240, 120, 60);
			case Severity.Fatal:
				return new ValueTuple<int, int, int>(255, 75, 60);
			default:
				return new ValueTuple<int, int, int>(190, 185, 175);
			}
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00155138 File Offset: 0x00153338
		private static string RoundName(WeaponClass w, AmmoKind a)
		{
			string text = XRayViewer.Round(w);
			if (a == AmmoKind.Slug)
			{
				return "Posta de escopeta";
			}
			if (a == AmmoKind.Incendiary)
			{
				return "Perdigones incendiarios";
			}
			string text2 = Ammo.Label(a);
			if (text2 == null)
			{
				return text;
			}
			if (text2.EndsWith("arrow"))
			{
				return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(text2);
			}
			return CultureInfo.InvariantCulture.TextInfo.ToTitleCase(text2) + " " + CultureInfo.InvariantCulture.TextInfo.ToTitleCase(text.ToLowerInvariant());
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x001551BC File Offset: 0x001533BC
		private static string Round(WeaponClass w)
		{
			switch (w)
			{
			case WeaponClass.Pistol:
				return "Cartucho de pistola";
			case WeaponClass.Revolver:
				return "Cartucho de revólver";
			case WeaponClass.Repeater:
				return "Cartucho de repetición";
			case WeaponClass.Rifle:
				return "Cartucho de fusil";
			case WeaponClass.Sniper:
				return "Bala de fusil de alta velocidad";
			case WeaponClass.Shotgun:
				return "Buckshot";
			case WeaponClass.Stab:
				return "Hoja - estocada";
			case WeaponClass.Slash:
				return "Hoja - tajo";
			case WeaponClass.Chop:
				return "Hoja pesada - corte";
			case WeaponClass.Blast:
				return "Explosión - metralla";
			case WeaponClass.Burn:
				return "Fire";
			case WeaponClass.Blunt:
				return "Fists";
			}
			return "Arrow";
		}

		// Token: 0x06001510 RID: 5392 RVA: 0x00155254 File Offset: 0x00153454
		internal static string PrettyWeapon(string enumName)
		{
			if (string.IsNullOrEmpty(enumName) || enumName.StartsWith("0x"))
			{
				return "Arma desconocida";
			}
			List<string> list = (from Match m in Regex.Matches(enumName, "[A-Z][a-z0-9]*")
			select m.Value).ToList<string>();
			if (list.Count >= 2)
			{
				return string.Join(" ", list.Skip(1)) + " " + list[0];
			}
			return enumName;
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x001552E4 File Offset: 0x001534E4
		private static string Tidy(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return char.ToUpperInvariant(s[0]).ToString() + s.Substring(1).ToLowerInvariant();
			}
			return s;
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00155320 File Offset: 0x00153520
		private static string Capitalize(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return char.ToUpperInvariant(s[0]).ToString() + s.Substring(1);
			}
			return s;
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00155357 File Offset: 0x00153557
		private static string Short(string region)
		{
			return (region ?? "").Replace("la parte frontal de ", "").Replace("the ", "");
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00155384 File Offset: 0x00153584
		private void DrawFooter()
		{
			if (this._confirmClear)
			{
				Theme.Footer(0.868f, "Press Delete again to erase the whole X-ray log", true);
				return;
			}
			if (XPad.RecentlyUsed)
			{
				Theme.Footer(0.868f, "Cruceta  Navegar     LB/RB  Humanos / Animales     Stick der.  Rotar     LT/RT  Zoom     Y  Girar     RS  Reiniciar     X  Arthur     B  Cerrar", false);
				return;
			}
			Theme.Footer(0.868f, "Arriba/Abajo  Navegar     Q/E  Humanos / Animales     A/D W/S  Rotar     Z/X  Zoom     Espacio  Girar     R  Reiniciar" + (XRayViewer.DebugOn ? "     F  Detalle     K  Calibrar     Supr  Borrar" : "") + "     Retroceso  Cerrar", false);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x001555EA File Offset: 0x001537EA
		[CompilerGenerated]
		private void <WoundTrack>g__Mark|89_0(int r, int g, int b, ref XRayViewer.<>c__DisplayClass89_0 A_4)
		{
			UiDraw.Rect(this._fx, this._fy + 0.004f, A_4.markW, 0.012f, r, g, b, 255);
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00155617 File Offset: 0x00153817
		[CompilerGenerated]
		internal static string <BlowKinds>g__Part|94_0(int n, string one, string many)
		{
			if (n != 0)
			{
				return string.Format("{0} {1}", n, (n == 1) ? one : many);
			}
			return null;
		}

		// Token: 0x04000EC6 RID: 3782
		private const float FilterBarH = 0.04f;

		// Token: 0x04000EC7 RID: 3783
		private const float BodyTop = 0.18299998f;

		// Token: 0x04000EC8 RID: 3784
		private const float BodyBottom = 0.85999995f;

		// Token: 0x04000EC9 RID: 3785
		private const float FooterY = 0.868f;

		// Token: 0x04000ECA RID: 3786
		private const float ListX = 0.07f;

		// Token: 0x04000ECB RID: 3787
		private const float ListW = 0.21f;

		// Token: 0x04000ECC RID: 3788
		private const float ViewX = 0.286f;

		// Token: 0x04000ECD RID: 3789
		private const float ViewW = 0.335f;

		// Token: 0x04000ECE RID: 3790
		private const float InfoX = 0.62700003f;

		// Token: 0x04000ECF RID: 3791
		private const float InfoW = 0.30299997f;

		// Token: 0x04000ED0 RID: 3792
		private const float RowH = 0.034f;

		// Token: 0x04000ED1 RID: 3793
		private readonly XRayLog _log;

		// Token: 0x04000ED2 RID: 3794
		private readonly XRayRenderer _renderer = new XRayRenderer();

		// Token: 0x04000ED3 RID: 3795
		private readonly Func<string, NemesisFate> _fateOf;

		// Token: 0x04000ED4 RID: 3796
		private readonly Action<XRayQuality> _saveQuality;

		// Token: 0x04000ED5 RID: 3797
		private readonly Action<int> _saveBudget;

		// Token: 0x04000ED6 RID: 3798
		private bool _animals;

		// Token: 0x04000ED7 RID: 3799
		private static readonly string[] FilterNames = new string[]
		{
			"Humanos",
			"Animales"
		};

		// Token: 0x04000ED8 RID: 3800
		private string _groupKey;

		// Token: 0x04000ED9 RID: 3801
		private int _shot = -1;

		// Token: 0x04000EDA RID: 3802
		private int _scroll;

		// Token: 0x04000EDB RID: 3803
		private bool _confirmClear;

		// Token: 0x04000EDC RID: 3804
		private string _builtFor;

		// Token: 0x04000EDD RID: 3805
		private XRayComposite _composite;

		// Token: 0x04000EDE RID: 3806
		private XRayQuality _quality;

		// Token: 0x04000EDF RID: 3807
		public static bool DebugOn;

		// Token: 0x04000EE0 RID: 3808
		private int _rectLimit;

		// Token: 0x04000EE1 RID: 3809
		public static float GlyphCost = 0.8f;

		// Token: 0x04000EE2 RID: 3810
		public static Action<string> Diag;

		// Token: 0x04000EE3 RID: 3811
		private int _diagAt = -100000;

		// Token: 0x04000EE4 RID: 3812
		private int _heldBudget;

		// Token: 0x04000EE5 RID: 3813
		private string _diagFor;

		// Token: 0x04000EE6 RID: 3814
		private bool _calibrating;

		// Token: 0x04000EE7 RID: 3815
		private int _calRow;

		// Token: 0x04000EE8 RID: 3816
		private int _calPage;

		// Token: 0x04000EE9 RID: 3817
		private const int CalCols = 50;

		// Token: 0x04000EEA RID: 3818
		private const int CalRows = 40;

		// Token: 0x04000EEB RID: 3819
		private bool _dragOrbit;

		// Token: 0x04000EEE RID: 3822
		private const float ListHeadH = 0.032f;

		// Token: 0x04000EEF RID: 3823
		private const float CardH = 0.205f;

		// Token: 0x04000EF0 RID: 3824
		private const float ListBottom = 0.64699996f;

		// Token: 0x04000EF1 RID: 3825
		private float _lx;

		// Token: 0x04000EF2 RID: 3826
		private float _ly;

		// Token: 0x04000EF3 RID: 3827
		private static readonly string[][] SpriteCandidates = new string[][]
		{
			new string[]
			{
				"generic_textures",
				"weapon_stats_bar"
			},
			new string[]
			{
				"generic_textures",
				"menu_bar"
			},
			new string[]
			{
				"generic_textures",
				"list_item_h_line_narrow"
			},
			new string[]
			{
				"generic_textures",
				"divider_line"
			},
			new string[]
			{
				"generic_textures",
				"selection_box_square"
			},
			new string[]
			{
				"generic_textures",
				"translate_bg_1a"
			},
			new string[]
			{
				"generic_textures",
				"inkroller_1a"
			},
			new string[]
			{
				"feeds",
				"toast_bg"
			}
		};

		// Token: 0x04000EF4 RID: 3828
		private float _fx;

		// Token: 0x04000EF5 RID: 3829
		private float _fvx;

		// Token: 0x04000EF6 RID: 3830
		private float _fy;

		// Token: 0x04000EF7 RID: 3831
		internal static readonly ValueTuple<int, int, int> PunchColor = new ValueTuple<int, int, int>(175, 100, 215);

		// Token: 0x04000EF8 RID: 3832
		internal static readonly ValueTuple<int, int, int> KickColor = new ValueTuple<int, int, int>(215, 140, 60);

		// Token: 0x04000EF9 RID: 3833
		internal static readonly ValueTuple<int, int, int> GunColor = new ValueTuple<int, int, int>(100, 170, 235);

		// Token: 0x04000EFA RID: 3834
		internal static readonly ValueTuple<int, int, int> ChokeColor = new ValueTuple<int, int, int>(225, 75, 85);

		// Token: 0x020004BC RID: 1212
		private sealed class Group
		{
			// Token: 0x04002151 RID: 8529
			public string Key;

			// Token: 0x04002152 RID: 8530
			public string Name;

			// Token: 0x04002153 RID: 8531
			public string Sub;

			// Token: 0x04002154 RID: 8532
			public string NemesisId;

			// Token: 0x04002155 RID: 8533
			public string FollowerOf;

			// Token: 0x04002156 RID: 8534
			public List<XRayEntry> Shots;

			// Token: 0x04002157 RID: 8535
			public DateTime Last;
		}
	}
}
