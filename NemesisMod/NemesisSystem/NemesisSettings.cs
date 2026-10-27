using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using RDR2;

namespace NemesisSystem
{
	// Token: 0x0200005B RID: 91
	internal sealed class NemesisSettings
	{
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000F6E RID: 3950 RVA: 0x0010DC00 File Offset: 0x0010BE00
		public bool GrudgeMessages
		{
			get
			{
				return this.MessageLevel == 3;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000F6F RID: 3951 RVA: 0x0010DC0B File Offset: 0x0010BE0B
		public bool SpeechMessages
		{
			get
			{
				return this.MessageLevel >= 1 && this.MessageLevel <= 3;
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000F70 RID: 3952 RVA: 0x0010DC24 File Offset: 0x0010BE24
		public bool DeathMessages
		{
			get
			{
				return this.MessageLevel >= 2;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000F71 RID: 3953 RVA: 0x0010DC32 File Offset: 0x0010BE32
		public bool WorldKnowAll
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000F72 RID: 3954 RVA: 0x0010DC35 File Offset: 0x0010BE35
		internal string PadMenuText
		{
			get
			{
				if (this.PadMenuOpen != 4)
				{
					return NemesisSettings.PadMenuOpenNames[Math.Max(0, Math.Min(4, this.PadMenuOpen))].ToLowerInvariant().Replace("d-pad", "D-pad");
				}
				return "the keyboard key";
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0010DC72 File Offset: 0x0010BE72
		internal float EncounterMult
		{
			get
			{
				return NemesisSettings.EncounterAmountMult[Math.Max(0, Math.Min(3, this.EncounterAmount))];
			}
		}

		// Token: 0x06000F74 RID: 3956 RVA: 0x0010DC8C File Offset: 0x0010BE8C
		public NemesisSettings(string path, string statePath = null)
		{
			this._path = path;
			this._statePath = statePath;
			this.Load();
		}

		// Token: 0x06000F75 RID: 3957 RVA: 0x0010E24C File Offset: 0x0010C44C
		public void ReadNewFactions()
		{
			IniFile iniFile = IniFile.Load(this._path);
			IniFile iniFile2 = (this._statePath != null && File.Exists(this._statePath)) ? IniFile.Load(this._statePath) : iniFile;
			foreach (Gang gang in Gangs.Defined)
			{
				if (!this.Notoriety.ContainsKey(gang.Id))
				{
					this.Notoriety[gang.Id] = Math.Max(0, Math.Min(100, iniFile2.GetValue<int>("Notoriety", gang.Id, 0)));
					gang.AmbushBase = iniFile.GetValue<double>("Factions", gang.Id + "Encuentros", gang.AmbushBase);
				}
			}
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x0010E314 File Offset: 0x0010C514
		public void Load()
		{
			IniFile iniFile = IniFile.Load(this._path);
			IniFile iniFile2 = (this._statePath != null && File.Exists(this._statePath)) ? IniFile.Load(this._statePath) : iniFile;
			this.ConfigVersion = iniFile.GetValue<int>("Meta", "Version", 0);
			if (this.LoadedVersion < 0)
			{
				this.LoadedVersion = this.ConfigVersion;
			}
			bool flag = this.ConfigVersion < 49;
			foreach (Setting setting in this.Layout())
			{
				Action<IniFile> read = setting.Read;
				if (read != null)
				{
					read(setting.State ? iniFile2 : iniFile);
				}
			}
			this.MessageLevel = (iniFile.GetValue<bool>("Menu", "GrudgeMessages", true) ? this.MessageLevel : 0);
			this.MessageLevel = Math.Max(0, Math.Min(4, iniFile.GetValue<int>("Menu", "Mensajes", this.MessageLevel)));
			foreach (Gang gang in Gangs.Defined)
			{
				gang.AmbushBase = iniFile.GetValue<double>("Factions", gang.Id + "Encuentros", gang.AmbushBase);
			}
			foreach (Gang gang2 in Gangs.Defined)
			{
				this.Notoriety[gang2.Id] = Math.Max(0, Math.Min(100, iniFile2.GetValue<int>("Notoriety", gang2.Id, 0)));
			}
			Hate.MurderousAt = Math.Max(Hate.VengefulAt + 5, Hate.MurderousAt);
			if (flag && this.ConfigVersion < 3)
			{
				if (Math.Abs(this.NamingRadius - 25f) < 0.01f)
				{
					this.NamingRadius = 40f;
				}
				if (this.NamingCap == 120)
				{
					this.NamingCap = 250;
				}
				if (Balance.VengefulHuntAggression == 60)
				{
					Balance.VengefulHuntAggression = 75;
				}
				if ((Balance.InterroBaseBoy == 50 && Balance.InterroBaseCaptain == 35) || (Balance.InterroBaseBoy == 30 && Balance.InterroBaseCaptain == 20))
				{
					Balance.InterroBaseBoy = 12;
					Balance.InterroBaseCaptain = 8;
					Balance.InterroBaseLieutenant = 3;
					Balance.InterroBaseAmbient = 15;
				}
				if (Balance.InterroFamilyFearExtra == 30)
				{
					Balance.InterroFamilyFearExtra = 15;
				}
				if (Math.Abs(Balance.PatrolHearingRange - 300f) < 0.5f)
				{
					Balance.PatrolHearingRange = 600f;
				}
			}
			if (flag && this.ConfigVersion < 4)
			{
				if (this.WorldMinHours == 3 && this.WorldMaxHours == 6)
				{
					this.WorldMinHours = 1;
					this.WorldMaxHours = 1;
				}
				if (this.WorldBatchMin == 1 && this.WorldBatchMax == 5)
				{
					this.WorldBatchMax = 2;
				}
			}
			if (flag && this.ConfigVersion < 6)
			{
				if (Balance.TownTopUpSeconds == 180)
				{
					Balance.TownTopUpSeconds = 90;
				}
				if (Balance.TownTopUpChance == 40)
				{
					Balance.TownTopUpChance = 50;
				}
				if (Balance.TownMaxMen == 3)
				{
					Balance.TownMaxMen = 4;
				}
			}
			if (flag && this.ConfigVersion < 7)
			{
				if (Balance.TownVengefulPct == 60)
				{
					Balance.TownVengefulPct = 35;
				}
				if (Balance.TownMurderousPct == 25)
				{
					Balance.TownMurderousPct = 15;
				}
				if (this.RegularsMax == 3)
				{
					this.RegularsMax = 6;
				}
				if (this.RegularChance == 50)
				{
					this.RegularChance = 60;
				}
			}
			if (flag && this.ConfigVersion < 8)
			{
				this.SocialAmbient = false;
				this.RegularsAmbient = false;
			}
			if (flag && this.ConfigVersion < 9)
			{
				if (Balance.TownTopUpSeconds == 90)
				{
					Balance.TownTopUpSeconds = 60;
				}
				if (Balance.TownTopUpChance == 50)
				{
					Balance.TownTopUpChance = 65;
				}
			}
			if (flag && this.ConfigVersion < 10 && this.CalmGraceSeconds == 90)
			{
				this.CalmGraceSeconds = 15;
			}
			if (flag && this.ConfigVersion < 11 && this.WorldMarkerStyle == "POI")
			{
				this.WorldMarkerStyle = "RCM";
			}
			if (flag && this.ConfigVersion < 12 && this.WorldMarkerStyle == "RCM")
			{
				this.WorldMarkerStyle = "CLUE";
			}
			if (flag && this.ConfigVersion < 13)
			{
				if (this.WorldMarkerStyle == "CLUE")
				{
					this.WorldMarkerStyle = "CLUE_RC";
				}
				this.GameFeed = false;
			}
			if (flag && this.ConfigVersion < 14)
			{
				this.WorldMarkerStyle = "POI_SPRITE";
			}
			if (flag && this.ConfigVersion < 15)
			{
				this.WorldMarkerStyle = "NEUTRAL_SECRET";
			}
			if (flag && this.ConfigVersion < 16 && this.PeopleCap == 300)
			{
				this.PeopleCap = 1000;
			}
			if (flag && this.ConfigVersion < 21 && this.GangAlonePercent == 35)
			{
				this.GangAlonePercent = 15;
			}
			if (flag && this.ConfigVersion < 23)
			{
				this.GsMusic = false;
			}
			if (flag && this.ConfigVersion < 24)
			{
				this.GsGestures = true;
			}
			if (flag && this.ConfigVersion < 22)
			{
				if (Balance.PatrolRespondChance == 35)
				{
					Balance.PatrolRespondChance = 15;
				}
				if (Balance.PatrolRespondCooldown == 180)
				{
					Balance.PatrolRespondCooldown = 600;
				}
				if (Balance.PatrolChance == 20)
				{
					Balance.PatrolChance = 12;
				}
			}
			if (flag && this.ConfigVersion < 17 && Balance.PatrolRespondMin == 250 && Balance.PatrolRespondMax == 350)
			{
				Balance.PatrolRespondMin = 150;
				Balance.PatrolRespondMax = 200;
			}
			if (flag && this.ConfigVersion < 25)
			{
				if (this.SelectFrameInsetX == 42)
				{
					this.SelectFrameInsetX = 28;
				}
				if (this.SelectFrameInsetY == 12)
				{
					this.SelectFrameInsetY = 22;
				}
			}
			if (flag && this.ConfigVersion < 26 && this.SelectFrameInsetY == 22)
			{
				this.SelectFrameInsetY = 12;
			}
			if (flag && this.ConfigVersion < 27)
			{
				if (Math.Abs(this.TensionTownMult - 2.5f) < 0.01f)
				{
					this.TensionTownMult = 1.4f;
				}
				if (Math.Abs(this.TensionRisePerMin - 7.5f) < 0.01f || Math.Abs(this.TensionRisePerMin - 6f) < 0.01f)
				{
					this.TensionRisePerMin = 8.5f;
				}
				if (this.CheatDeathBaseChance == 50 && this.CheatDeathPerRank == 0 && this.CheatDeathMaxChance == 50)
				{
					this.CheatDeathBaseChance = 35;
					this.CheatDeathPerRank = 5;
					this.CheatDeathMaxChance = 60;
				}
			}
			if (flag && this.ConfigVersion < 29)
			{
				this.CardFrameUp = 1;
			}
			if (flag && this.ConfigVersion < 31)
			{
				this.WarHudX = 0.03f;
				this.WarHudY = 0.06f;
			}
			if (flag && this.ConfigVersion < 32 && Balance.GsWounded == 65f)
			{
				Balance.GsWounded = 50f;
			}
			if (flag && this.ConfigVersion < 33 && this.TensionAfterFight == 10f)
			{
				this.TensionAfterFight = 25f;
			}
			if (flag && this.ConfigVersion < 34 && this.CardFrameUp == 1)
			{
				this.CardFrameUp = 0;
			}
			if (flag && this.ConfigVersion < 35 && this.CardFrameUp == 0)
			{
				this.CardFrameUp = 1;
			}
			if (flag && this.ConfigVersion < 36)
			{
				if (this.TensionFireMax == 85f)
				{
					this.TensionFireMax = 70f;
				}
				if (this.TensionAfterFight == 25f)
				{
					this.TensionAfterFight = 15f;
				}
			}
			if (flag && this.ConfigVersion < 37)
			{
				if (this.TensionFireMin == 55f)
				{
					this.TensionFireMin = 45f;
				}
				if (this.TensionFireMax == 70f)
				{
					this.TensionFireMax = 60f;
				}
				if (this.TensionWildFireMin == 40f)
				{
					this.TensionWildFireMin = 30f;
				}
				if (this.TensionWildFireMax == 65f)
				{
					this.TensionWildFireMax = 50f;
				}
				if (this.TensionAfterEncounter == 35f)
				{
					this.TensionAfterEncounter = 25f;
				}
			}
			if (flag && this.ConfigVersion < 38)
			{
				if (this.TensionFireMin == 45f)
				{
					this.TensionFireMin = 55f;
				}
				if (this.TensionFireMax == 60f)
				{
					this.TensionFireMax = 70f;
				}
				if (this.TensionAfterEncounter == 25f)
				{
					this.TensionAfterEncounter = 35f;
				}
			}
			if (flag && this.ConfigVersion < 39 && Math.Abs(this.WarHudX - 0.03f) < 0.001f && Math.Abs(this.WarHudY - 0.06f) < 0.001f)
			{
				this.WarHudX = -1f;
				this.WarHudY = 0.105f;
			}
			if (flag && this.ConfigVersion < 40 && Math.Abs(this.WarHudY - 0.105f) < 0.001f)
			{
				this.WarHudY = 0.13f;
			}
			if (flag && this.ConfigVersion < 41 && Math.Abs(this.WarHudY - 0.13f) < 0.001f)
			{
				this.WarHudY = 0.16f;
			}
			if (flag && this.ConfigVersion < 42 && Math.Abs(this.WarHudY - 0.16f) < 0.001f)
			{
				this.WarHudY = 0.166f;
			}
			if (flag && this.ConfigVersion < 43 && Balance.InterroTriesPerScare == 2)
			{
				Balance.InterroTriesPerScare = 1;
			}
			if (flag && this.ConfigVersion < 44 && this.SuccessionMinDays == 3 && this.SuccessionMaxDays == 5)
			{
				this.SuccessionMinDays = 2;
				this.SuccessionMaxDays = 6;
			}
			if (flag && this.ConfigVersion < 28)
			{
				if (this.MessageLevel == 3)
				{
					this.MessageLevel = 4;
				}
				if (this.NameCards == 0)
				{
					this.NameCards = 2;
				}
			}
			UiDraw.GameFont = this.GameFont;
			UiDraw.FitTo169 = this.FitTo169;
			UiFeed.Enabled = this.GameFeed;
			XPad.IconsSetting = this.PadIcons;
			UiDraw.BodyFont = this.BodyFont;
			UiDraw.TextSlotLimit = this.TextSlotLimit;
			UiDraw.TitleFont = this.TitleFont;
			UiDraw.MarkupUnderline = this.TextUnderline;
			Theme.FrameOn = this.SelectFrame;
			Theme.FrameTint = this.SelectFrameTint;
			Theme.FrameSize = this.SelectFrameSize;
			Theme.FrameOut = this.SelectFrameOut;
			Theme.FrameEdge = this.SelectFrameEdge;
			Theme.FrameInsetX = this.SelectFrameInsetX;
			Theme.FrameInsetY = this.SelectFrameInsetY;
			Theme.CardFrameUp = this.CardFrameUp;
			Theme.InkOn = this.MenuInk;
			Theme.InkDict = this.MenuInkDict;
			Theme.InkTexture = this.MenuInkTexture;
			if (flag && this.ConfigVersion < 45 && Balance.PatrolRespondChance == 15)
			{
				Balance.PatrolRespondChance = 10;
			}
			if (flag && this.ConfigVersion < 45 && Balance.PatrolRespondCooldown == 600)
			{
				Balance.PatrolRespondCooldown = 900;
			}
			if (flag && this.ConfigVersion < 46)
			{
				this.DebugEnabled = false;
			}
			if (flag && this.ConfigVersion < 47 && this.CampClearDays == 3)
			{
				this.CampClearDays = 1;
			}
			if (flag && this.ConfigVersion < 48 && Math.Abs(this.WorldMarkerRadius - 300f) < 0.5f)
			{
				this.WorldMarkerRadius = 150f;
			}
			if (flag && this.ConfigVersion < 49 && !this.RandomAmbushes)
			{
				this.RandomAmbushes = true;
				if (SafeFile.PendingNotice == null)
				{
					SafeFile.PendingNotice = "Los encuentros aleatorios estaban desactivados; vuelven a estar activos. (Para desactivarlos: Alt+F11, Ambush, RandomAmbushes).";
				}
			}
			IniPanel.Dev = this.DebugEnabled;
			if (flag)
			{
				this.ConfigVersion = 49;
			}
			this.Save();
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x0010EE54 File Offset: 0x0010D054
		private static string F(float v)
		{
			return v.ToString("0.0##", CultureInfo.InvariantCulture);
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x0010EE68 File Offset: 0x0010D068
		public void Save()
		{
			StringBuilder stringBuilder = new StringBuilder();
			List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>();
			try
			{
				foreach (Setting setting in this.Layout())
				{
					if (setting.Write != null)
					{
						if (setting.State && this._statePath != null)
						{
							list.Add(new ValueTuple<string, string>(setting.Section, setting.Write()));
						}
						else
						{
							stringBuilder.Append(setting.Write()).Append("\r\n");
						}
					}
				}
			}
			catch (Exception ex)
			{
				Action<string> log = SafeFile.Log;
				if (log != null)
				{
					log("SAVE settings couldn't be put together: " + ex.Message);
				}
				return;
			}
			try
			{
				SafeFile.Write(this._path, stringBuilder.ToString(), false);
				NemesisSettings.WriteWorked(ref this._iniFailed, this._path);
			}
			catch (Exception ex2)
			{
				NemesisSettings.WriteFailed(ref this._iniFailed, this._path, ex2, "your settings won't be kept");
			}
			if (this._statePath == null)
			{
				return;
			}
			try
			{
				StringBuilder stringBuilder2 = new StringBuilder("; ");
				foreach (IGrouping<string, ValueTuple<string, string>> grouping in from x in list
				group x by x.Item1)
				{
					stringBuilder2.Append("\r\n[").Append(grouping.Key).Append("]\r\n");
					foreach (ValueTuple<string, string> valueTuple in grouping)
					{
						stringBuilder2.Append(valueTuple.Item2).Append("\r\n");
					}
				}
				SafeFile.Write(this._statePath, stringBuilder2.ToString(), false);
				NemesisSettings.WriteWorked(ref this._stateFailed, this._statePath);
			}
			catch (Exception ex3)
			{
				NemesisSettings.WriteFailed(ref this._stateFailed, this._statePath, ex3, "notoriety and the outfits' bookkeeping won't be kept");
			}
		}

		// Token: 0x06000F79 RID: 3961 RVA: 0x0010F0C4 File Offset: 0x0010D2C4
		private static void WriteFailed(ref bool failed, string path, Exception ex, string what)
		{
			if (failed || SafeFile.Log == null)
			{
				return;
			}
			failed = true;
			string fileName = Path.GetFileName(path);
			Action<string> log = SafeFile.Log;
			if (log != null)
			{
				log(string.Concat(new string[]
				{
					"SAVE ",
					fileName,
					" failed: ",
					ex.Message,
					" (",
					path,
					") - ",
					what,
					" until it can be written (read-only? an antivirus such as Windows Defender's Controlled folder access?)"
				}));
			}
			if (SafeFile.PendingNotice == null)
			{
				SafeFile.PendingNotice = fileName + " can't be saved, so " + what + ". Is the folder read-only, or blocked by your antivirus? (grudges.log: SAVE)";
			}
		}

		// Token: 0x06000F7A RID: 3962 RVA: 0x0010F15D File Offset: 0x0010D35D
		private static void WriteWorked(ref bool failed, string path)
		{
			if (!failed)
			{
				return;
			}
			failed = false;
			Action<string> log = SafeFile.Log;
			if (log == null)
			{
				return;
			}
			log("SAVE " + Path.GetFileName(path) + " written again");
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x0010F18B File Offset: 0x0010D38B
		private IEnumerable<Setting> Layout()
		{
			yield return Setting.Line("[Ambush]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Ambush", "RandomAmbushes", () => this.RandomAmbushes, delegate(bool v)
			{
				this.RandomAmbushes = v;
			}, () => string.Format("{0}", this.RandomAmbushes));
			yield return Setting.Key<int>("Ambush", "IntervalSeconds", () => this.AmbushIntervalSeconds, delegate(int v)
			{
				this.AmbushIntervalSeconds = Math.Max(10, v);
			}, () => string.Format("{0}", this.AmbushIntervalSeconds));
			yield return Setting.Key<int>("Ambush", "ChancePercent", () => this.AmbushChancePercent, delegate(int v)
			{
				this.AmbushChancePercent = v;
			}, () => string.Format("{0}", this.AmbushChancePercent));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Ambush", "ResentidoRange", () => this.ResentfulRange, delegate(float v)
			{
				this.ResentfulRange = v;
			}, () => NemesisSettings.F(this.ResentfulRange) ?? "");
			yield return Setting.Key<float>("Ambush", "VengadorRange", () => this.VengefulRange, delegate(float v)
			{
				this.VengefulRange = v;
			}, () => NemesisSettings.F(this.VengefulRange) ?? "");
			yield return Setting.Line("");
			yield return Setting.Line("[CheatDeath]");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("CheatDeath", "BaseChance", () => this.CheatDeathBaseChance, delegate(int v)
			{
				this.CheatDeathBaseChance = v;
			}, () => string.Format("{0}", this.CheatDeathBaseChance));
			yield return Setting.Key<int>("CheatDeath", "PerRank", () => this.CheatDeathPerRank, delegate(int v)
			{
				this.CheatDeathPerRank = v;
			}, () => string.Format("{0}", this.CheatDeathPerRank));
			yield return Setting.Key<int>("CheatDeath", "MaxChance", () => this.CheatDeathMaxChance, delegate(int v)
			{
				this.CheatDeathMaxChance = v;
			}, () => string.Format("{0}", this.CheatDeathMaxChance));
			yield return Setting.Key<bool>("CheatDeath", "Always", () => this.CheatDeathAlways, delegate(bool v)
			{
				this.CheatDeathAlways = v;
			}, () => string.Format("{0}", this.CheatDeathAlways));
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("CheatDeath", "FatalWoundsFinal", () => this.FatalWoundsFinal, delegate(bool v)
			{
				this.FatalWoundsFinal = v;
			}, () => string.Format("{0}", this.FatalWoundsFinal));
			yield return Setting.Line("");
			yield return Setting.Line("[Grudges]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Grudges", "RobbedHateGain", () => Balance.RobbedHateGain, delegate(int v)
			{
				Balance.RobbedHateGain = v;
			}, () => string.Format("{0}", Balance.RobbedHateGain));
			yield return Setting.Key<int>("Grudges", "RobbedGreedyExtra", () => Balance.RobbedGreedyExtra, delegate(int v)
			{
				Balance.RobbedGreedyExtra = v;
			}, () => string.Format("{0}", Balance.RobbedGreedyExtra));
			yield return Setting.Key<int>("Grudges", "RobbedPoorShift", () => Balance.RobbedPoorShift, delegate(int v)
			{
				Balance.RobbedPoorShift = v;
			}, () => string.Format("{0}", Balance.RobbedPoorShift));
			yield return Setting.Key<int>("Grudges", "RobbedRichShift", () => Balance.RobbedRichShift, delegate(int v)
			{
				Balance.RobbedRichShift = v;
			}, () => string.Format("{0}", Balance.RobbedRichShift));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Grudges", "BeatingHits", () => this.BeatingHits, delegate(int v)
			{
				this.BeatingHits = v;
			}, () => string.Format("{0}", this.BeatingHits));
			yield return Setting.Key<int>("Grudges", "MaxLivingNemeses", () => this.MaxLivingNemeses, delegate(int v)
			{
				this.MaxLivingNemeses = v;
			}, () => string.Format("{0}", this.MaxLivingNemeses));
			yield return Setting.Line("");
			yield return Setting.Line("[Followers]");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Followers", "Rank1", () => this.FollowersRank1, delegate(int v)
			{
				this.FollowersRank1 = v;
			}, () => string.Format("{0}", this.FollowersRank1));
			yield return Setting.Key<int>("Followers", "Rank2", () => this.FollowersRank2, delegate(int v)
			{
				this.FollowersRank2 = v;
			}, () => string.Format("{0}", this.FollowersRank2));
			yield return Setting.Key<int>("Followers", "Rank3", () => this.FollowersRank3, delegate(int v)
			{
				this.FollowersRank3 = v;
			}, () => string.Format("{0}", this.FollowersRank3));
			yield return Setting.Key<int>("Followers", "Rank4", () => this.FollowersRank4, delegate(int v)
			{
				this.FollowersRank4 = v;
			}, () => string.Format("{0}", this.FollowersRank4));
			yield return Setting.Key<int>("Followers", "Rank5", () => this.FollowersRank5, delegate(int v)
			{
				this.FollowersRank5 = v;
			}, () => string.Format("{0}", this.FollowersRank5));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Followers", "GangAlonePercent", () => this.GangAlonePercent, delegate(int v)
			{
				this.GangAlonePercent = Math.Max(0, Math.Min(100, v));
			}, () => string.Format("{0}", this.GangAlonePercent));
			yield return Setting.Key<int>("Followers", "LonerAlonePercent", () => this.LonerAlonePercent, delegate(int v)
			{
				this.LonerAlonePercent = Math.Max(0, Math.Min(100, v));
			}, () => string.Format("{0}", this.LonerAlonePercent));
			yield return Setting.Line("");
			yield return Setting.Line("[Sightings]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Sightings", "ParalyzedChance", () => this.ParalyzedSightingChance, delegate(int v)
			{
				this.ParalyzedSightingChance = v;
			}, () => string.Format("{0}", this.ParalyzedSightingChance));
			yield return Setting.Line("");
			yield return Setting.Line("[Gangs]");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Gangs", "AmbushWeight", () => this.GangAmbushWeight, delegate(float v)
			{
				this.GangAmbushWeight = v;
			}, () => NemesisSettings.F(this.GangAmbushWeight) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.StateKey<string>("Gangs", "Seeded", () => this.SeededGangs, delegate(string v)
			{
				this.SeededGangs = v;
			}, () => this.SeededGangs ?? "");
			yield return Setting.StateKey<bool>("Gangs", "ArmyTopUp", () => this.ArmyTopUp, delegate(bool v)
			{
				this.ArmyTopUp = v;
			}, () => string.Format("{0}", this.ArmyTopUp));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Gangs", "CrewRosterChance", () => this.CrewRosterChance, delegate(int v)
			{
				this.CrewRosterChance = v;
			}, () => string.Format("{0}", this.CrewRosterChance));
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Gangs", "HiredGunsJoin", () => this.HiredGunsJoin, delegate(bool v)
			{
				this.HiredGunsJoin = v;
			}, () => string.Format("{0}", this.HiredGunsJoin));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Gangs", "Succession", () => this.Succession, delegate(bool v)
			{
				this.Succession = v;
			}, () => string.Format("{0}", this.Succession));
			yield return Setting.Key<int>("Gangs", "SuccessionHour", () => this.SuccessionHour, delegate(int v)
			{
				this.SuccessionHour = v;
			}, () => string.Format("{0}", this.SuccessionHour));
			yield return Setting.Key<int>("Gangs", "SuccessionKinChance", () => this.SuccessionKinChance, delegate(int v)
			{
				this.SuccessionKinChance = v;
			}, () => string.Format("{0}", this.SuccessionKinChance));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Gangs", "KinChance", () => Traits.KinChance, delegate(int v)
			{
				Traits.KinChance = v;
			}, () => string.Format("{0}", Traits.KinChance));
			yield return Setting.StateKey<int>("Gangs", "LastSuccessionDay", () => this.LastSuccessionDay, delegate(int v)
			{
				this.LastSuccessionDay = v;
			}, () => string.Format("{0}", this.LastSuccessionDay));
			yield return Setting.Line("; ");
			yield return Setting.StateKey<int>("Gangs", "NextSuccessionDay", () => this.NextSuccessionDay, delegate(int v)
			{
				this.NextSuccessionDay = v;
			}, () => string.Format("{0}", this.NextSuccessionDay));
			yield return Setting.Key<int>("Gangs", "SuccessionMinDays", () => this.SuccessionMinDays, delegate(int v)
			{
				this.SuccessionMinDays = Math.Max(1, v);
			}, () => string.Format("{0}", this.SuccessionMinDays));
			yield return Setting.Key<int>("Gangs", "SuccessionMaxDays", () => this.SuccessionMaxDays, delegate(int v)
			{
				this.SuccessionMaxDays = Math.Max(this.SuccessionMinDays, v);
			}, () => string.Format("{0}", this.SuccessionMaxDays));
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Gangs", "ArmyGeneral", () => this.ArmyGeneral, delegate(bool v)
			{
				this.ArmyGeneral = v;
			}, () => string.Format("{0}", this.ArmyGeneral));
			yield return Setting.Line("");
			yield return Setting.Line("[Calm]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Tranquilo", "Enabled", () => this.CalmEnabled, delegate(bool v)
			{
				this.CalmEnabled = v;
			}, () => string.Format("{0}", this.CalmEnabled));
			yield return Setting.Key<int>("Tranquilo", "GraceSeconds", () => this.CalmGraceSeconds, delegate(int v)
			{
				this.CalmGraceSeconds = v;
			}, () => string.Format("{0}", this.CalmGraceSeconds));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Tranquilo", "LoadGraceSeconds", () => this.CalmLoadGraceSeconds, delegate(int v)
			{
				this.CalmLoadGraceSeconds = Math.Max(0, v);
			}, () => string.Format("{0}", this.CalmLoadGraceSeconds));
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Tranquilo", "CampRadius", () => this.CalmCampRadius, delegate(float v)
			{
				this.CalmCampRadius = v;
			}, () => NemesisSettings.F(this.CalmCampRadius) ?? "");
			yield return Setting.Key<bool>("Tranquilo", "WhileWanted", () => this.CalmWhileWanted, delegate(bool v)
			{
				this.CalmWhileWanted = v;
			}, () => string.Format("{0}", this.CalmWhileWanted));
			yield return Setting.StateKey<string>("Tranquilo", "GangCamps", () => this.GangCamps, delegate(string v)
			{
				this.GangCamps = v;
			}, () => this.GangCamps ?? "");
			yield return Setting.Line("");
			yield return Setting.Line("[People]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("People", "Remember", () => this.RememberPeople, delegate(bool v)
			{
				this.RememberPeople = v;
			}, () => string.Format("{0}", this.RememberPeople));
			yield return Setting.Key<int>("People", "Cap", () => this.PeopleCap, delegate(int v)
			{
				this.PeopleCap = Math.Max(20, v);
			}, () => string.Format("{0}", this.PeopleCap));
			yield return Setting.Key<int>("People", "RegularsMax", () => this.RegularsMax, delegate(int v)
			{
				this.RegularsMax = v;
			}, () => string.Format("{0}", this.RegularsMax));
			yield return Setting.Key<int>("People", "RegularChance", () => this.RegularChance, delegate(int v)
			{
				this.RegularChance = v;
			}, () => string.Format("{0}", this.RegularChance));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("People", "LinkLooks", () => this.LinkLooks, delegate(bool v)
			{
				this.LinkLooks = v;
			}, () => string.Format("{0}", this.LinkLooks));
			yield return Setting.Key<int>("People", "LookMemoryDays", () => this.LookMemoryDays, delegate(int v)
			{
				this.LookMemoryDays = Math.Max(1, v);
			}, () => string.Format("{0}", this.LookMemoryDays));
			yield return Setting.Key<int>("People", "LookCap", () => this.LookCap, delegate(int v)
			{
				this.LookCap = Math.Max(200, v);
			}, () => string.Format("{0}", this.LookCap));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("People", "RegularsAmbient", () => this.RegularsAmbient, delegate(bool v)
			{
				this.RegularsAmbient = v;
			}, () => string.Format("{0}", this.RegularsAmbient));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("People", "SocialAmbient", () => this.SocialAmbient, delegate(bool v)
			{
				this.SocialAmbient = v;
			}, () => string.Format("{0}", this.SocialAmbient));
			yield return Setting.Line("");
			yield return Setting.Line("[Telegraph]");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Telégrafo", "MinDays", () => this.TelegraphMinDays, delegate(int v)
			{
				this.TelegraphMinDays = Math.Max(1, v);
			}, () => string.Format("{0}", this.TelegraphMinDays));
			yield return Setting.Key<int>("Telégrafo", "MaxDays", () => this.TelegraphMaxDays, delegate(int v)
			{
				this.TelegraphMaxDays = Math.Max(this.TelegraphMinDays, v);
			}, () => string.Format("{0}", this.TelegraphMaxDays));
			yield return Setting.Line("");
			yield return Setting.Line("[SavePoints]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("SavePoints", "Enabled", () => this.SavePointsEnabled, delegate(bool v)
			{
				this.SavePointsEnabled = v;
			}, () => string.Format("{0}", this.SavePointsEnabled));
			yield return Setting.Key<bool>("SavePoints", "AutoRestore", () => this.SavePointAutoRestore, delegate(bool v)
			{
				this.SavePointAutoRestore = v;
			}, () => string.Format("{0}", this.SavePointAutoRestore));
			yield return Setting.Key<int>("SavePoints", "BackMinutes", () => this.SavePointBackMinutes, delegate(int v)
			{
				this.SavePointBackMinutes = Math.Max(10, v);
			}, () => string.Format("{0}", this.SavePointBackMinutes));
			yield return Setting.Key<int>("SavePoints", "EveryHours", () => this.SavePointEveryHours, delegate(int v)
			{
				this.SavePointEveryHours = Math.Max(1, v);
			}, () => string.Format("{0}", this.SavePointEveryHours));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("SavePoints", "MaxMB", () => this.SavePointMaxMB, delegate(int v)
			{
				this.SavePointMaxMB = Math.Max(20, v);
			}, () => string.Format("{0}", this.SavePointMaxMB));
			yield return Setting.Line("");
			yield return Setting.Line("[World]");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("World", "CatchUpHours", () => this.WorldCatchUpMax, delegate(int v)
			{
				this.WorldCatchUpMax = Math.Max(1, v);
			}, () => string.Format("{0}", this.WorldCatchUpMax));
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("World", "Eventos", () => this.WorldEvents, delegate(bool v)
			{
				this.WorldEvents = v;
			}, () => string.Format("{0}", this.WorldEvents));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("World", "MinHours", () => this.WorldMinHours, delegate(int v)
			{
				this.WorldMinHours = Math.Max(1, v);
			}, () => string.Format("{0}", this.WorldMinHours));
			yield return Setting.Key<int>("World", "MaxHours", () => this.WorldMaxHours, delegate(int v)
			{
				this.WorldMaxHours = Math.Max(this.WorldMinHours, v);
			}, () => string.Format("{0}", this.WorldMaxHours));
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("World", "MinDistance", () => this.WorldMinDistance, delegate(float v)
			{
				this.WorldMinDistance = v;
			}, () => NemesisSettings.F(this.WorldMinDistance) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("World", "MarkerRadius", () => this.WorldMarkerRadius, delegate(float v)
			{
				this.WorldMarkerRadius = v;
			}, () => NemesisSettings.F(this.WorldMarkerRadius) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("World", "SurvivorChance", () => this.WorldSurvivorChance, delegate(int v)
			{
				this.WorldSurvivorChance = Math.Max(0, Math.Min(100, v));
			}, () => string.Format("{0}", this.WorldSurvivorChance));
			yield return Setting.Key<float>("World", "SurvivorSpotRange", () => this.WorldSurvivorSpotRange, delegate(float v)
			{
				this.WorldSurvivorSpotRange = Math.Max(5f, v);
			}, () => NemesisSettings.F(this.WorldSurvivorSpotRange) ?? "");
			yield return Setting.Key<string>("World", "MarkerStyle", () => this.WorldMarkerStyle, delegate(string v)
			{
				this.WorldMarkerStyle = (v ?? "NEUTRAL_SECRET").Trim().ToUpperInvariant();
			}, () => this.WorldMarkerStyle ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("World", "AftermathHours", () => this.WorldAftermathHours, delegate(int v)
			{
				this.WorldAftermathHours = v;
			}, () => string.Format("{0}", this.WorldAftermathHours));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("World", "Lethality", () => this.WorldLethality, delegate(int v)
			{
				this.WorldLethality = v;
			}, () => string.Format("{0}", this.WorldLethality));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("World", "BatchMin", () => this.WorldBatchMin, delegate(int v)
			{
				this.WorldBatchMin = Math.Max(1, v);
			}, () => string.Format("{0}", this.WorldBatchMin));
			yield return Setting.Key<int>("World", "BatchMax", () => this.WorldBatchMax, delegate(int v)
			{
				this.WorldBatchMax = Math.Max(this.WorldBatchMin, v);
			}, () => string.Format("{0}", this.WorldBatchMax));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("World", "BigBatchChance", () => this.WorldBigBatchChance, delegate(int v)
			{
				this.WorldBigBatchChance = v;
			}, () => string.Format("{0}", this.WorldBigBatchChance));
			yield return Setting.Key<int>("World", "BigBatchMin", () => this.WorldBigBatchMin, delegate(int v)
			{
				this.WorldBigBatchMin = Math.Max(1, v);
			}, () => string.Format("{0}", this.WorldBigBatchMin));
			yield return Setting.Key<int>("World", "BigBatchMax", () => this.WorldBigBatchMax, delegate(int v)
			{
				this.WorldBigBatchMax = Math.Max(this.WorldBigBatchMin, v);
			}, () => string.Format("{0}", this.WorldBigBatchMax));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("World", "MaxClashesPerDay", () => this.WorldMaxClashesPerDay, delegate(int v)
			{
				this.WorldMaxClashesPerDay = v;
			}, () => string.Format("{0}", this.WorldMaxClashesPerDay));
			yield return Setting.Key<int>("World", "ClashWeight", () => this.WorldClashWeight, delegate(int v)
			{
				this.WorldClashWeight = v;
			}, () => string.Format("{0}", this.WorldClashWeight));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("World", "WarLine", () => this.WarLine, delegate(int v)
			{
				this.WarLine = Math.Max(-100, Math.Min(0, v));
			}, () => string.Format("{0}", this.WarLine));
			yield return Setting.Key<int>("World", "ClashLine", () => this.ClashLine, delegate(int v)
			{
				this.ClashLine = Math.Max(-100, Math.Min(0, v));
			}, () => string.Format("{0}", this.ClashLine));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("World", "DistantBattles", () => this.DistantBattles, delegate(bool v)
			{
				this.DistantBattles = v;
			}, () => string.Format("{0}", this.DistantBattles));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("World", "ExplosionType", () => this.DistantExplosionTag, delegate(int v)
			{
				this.DistantExplosionTag = Math.Max(0, Math.Min(60, v));
			}, () => string.Format("{0}", this.DistantExplosionTag));
			yield return Setting.Key<bool>("World", "WarMarkers", () => this.WarMarkers, delegate(bool v)
			{
				this.WarMarkers = v;
			}, () => string.Format("{0}", this.WarMarkers));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<string>("World", "WarRegionStyle", () => this.WarRegionStyle, delegate(string v)
			{
				this.WarRegionStyle = (string.IsNullOrWhiteSpace(v) ? "BLIP_STYLE_DEBUG_RED" : v.Trim());
			}, () => this.WarRegionStyle ?? "");
			yield return Setting.Key<bool>("World", "TownRepMarkers", () => this.TownRepMarkers, delegate(bool v)
			{
				this.TownRepMarkers = v;
			}, () => string.Format("{0}", this.TownRepMarkers));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("World", "WorldNews", () => this.WorldNews, delegate(int v)
			{
				this.WorldNews = Math.Max(0, Math.Min(3, v));
			}, () => string.Format("{0}", this.WorldNews));
			yield return Setting.Key<int>("World", "FeudWeight", () => this.WorldFeudWeight, delegate(int v)
			{
				this.WorldFeudWeight = v;
			}, () => string.Format("{0}", this.WorldFeudWeight));
			yield return Setting.Key<int>("World", "BrawlWeight", () => this.WorldBrawlWeight, delegate(int v)
			{
				this.WorldBrawlWeight = v;
			}, () => string.Format("{0}", this.WorldBrawlWeight));
			yield return Setting.Key<int>("World", "RivalryWeight", () => this.WorldRivalryWeight, delegate(int v)
			{
				this.WorldRivalryWeight = Math.Max(0, v);
			}, () => string.Format("{0}", this.WorldRivalryWeight));
			yield return Setting.Key<int>("World", "VanDerLindeWeight", () => this.WorldVdlWeight, delegate(int v)
			{
				this.WorldVdlWeight = Math.Max(0, v);
			}, () => string.Format("{0}", this.WorldVdlWeight));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("World", "WordWeight", () => this.WorldWordWeight, delegate(int v)
			{
				this.WorldWordWeight = Math.Max(0, v);
			}, () => string.Format("{0}", this.WorldWordWeight));
			yield return Setting.Key<int>("World", "LifeWeight", () => this.WorldLifeWeight, delegate(int v)
			{
				this.WorldLifeWeight = v;
			}, () => string.Format("{0}", this.WorldLifeWeight));
			yield return Setting.Line("");
			yield return Setting.Line("[Balance]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Balance", "FistCap", () => Balance.FistCap, delegate(int v)
			{
				Balance.FistCap = v;
			}, () => string.Format("{0}", Balance.FistCap));
			yield return Setting.Key<int>("Balance", "FistCapHotheadOrViolent", () => Balance.FistCapHot, delegate(int v)
			{
				Balance.FistCapHot = v;
			}, () => string.Format("{0}", Balance.FistCapHot));
			yield return Setting.Key<int>("Balance", "FistCapViolentHothead", () => Balance.FistCapViolent, delegate(int v)
			{
				Balance.FistCapViolent = v;
			}, () => string.Format("{0}", Balance.FistCapViolent));
			yield return Setting.Key<int>("Balance", "FistCapRagingHothead", () => Balance.FistCapRage, delegate(int v)
			{
				Balance.FistCapRage = v;
			}, () => string.Format("{0}", Balance.FistCapRage));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Balance", "KnockoutHate", () => Balance.KoHate, delegate(int v)
			{
				Balance.KoHate = v;
			}, () => string.Format("{0}", Balance.KoHate));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Balance", "HateVengadorAt", () => Hate.VengefulAt, delegate(int v)
			{
				Hate.VengefulAt = v;
			}, () => string.Format("{0}", Hate.VengefulAt));
			yield return Setting.Key<int>("Balance", "HateHomicida At", () => Hate.MurderousAt, delegate(int v)
			{
				Hate.MurderousAt = v;
			}, () => string.Format("{0}", Hate.MurderousAt));
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Balance", "HateGainScale", () => Balance.HateGainScale, delegate(float v)
			{
				Balance.HateGainScale = v;
			}, () => NemesisSettings.F(Balance.HateGainScale) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Balance", "LonerShiftScale", () => Balance.LonerShiftScale, delegate(float v)
			{
				Balance.LonerShiftScale = v;
			}, () => NemesisSettings.F(Balance.LonerShiftScale) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Balance", "CoolingScale", () => Balance.CoolingScale, delegate(float v)
			{
				Balance.CoolingScale = v;
			}, () => NemesisSettings.F(Balance.CoolingScale) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Balance", "NotorietyGainScale", () => Balance.NotorietyGainScale, delegate(float v)
			{
				Balance.NotorietyGainScale = v;
			}, () => NemesisSettings.F(Balance.NotorietyGainScale) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Balance", "NotorietyDecayPerDay", () => Balance.NotorietyDecayPerDay, delegate(int v)
			{
				Balance.NotorietyDecayPerDay = v;
			}, () => string.Format("{0}", Balance.NotorietyDecayPerDay));
			yield return Setting.Key<int>("Balance", "UnnamedKillsPerNotoriety", () => Balance.UnnamedKillsPerPoint, delegate(int v)
			{
				Balance.UnnamedKillsPerPoint = Math.Max(1, v);
			}, () => string.Format("{0}", Balance.UnnamedKillsPerPoint));
			yield return Setting.StateKey<int>("Balance", "LastNotorietyDecayDay", () => this.LastDecayDay, delegate(int v)
			{
				this.LastDecayDay = v;
			}, () => string.Format("{0}", this.LastDecayDay));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Balance", "InterrogateBaseLowRank", () => Balance.InterroBaseBoy, delegate(int v)
			{
				Balance.InterroBaseBoy = v;
			}, () => string.Format("{0}", Balance.InterroBaseBoy));
			yield return Setting.Key<int>("Balance", "InterrogateBaseMidRank", () => Balance.InterroBaseCaptain, delegate(int v)
			{
				Balance.InterroBaseCaptain = v;
			}, () => string.Format("{0}", Balance.InterroBaseCaptain));
			yield return Setting.Key<int>("Balance", "InterrogateBaseHighRank", () => Balance.InterroBaseLieutenant, delegate(int v)
			{
				Balance.InterroBaseLieutenant = v;
			}, () => string.Format("{0}", Balance.InterroBaseLieutenant));
			yield return Setting.Key<int>("Balance", "InterrogateBaseUnnamed", () => Balance.InterroBaseAmbient, delegate(int v)
			{
				Balance.InterroBaseAmbient = v;
			}, () => string.Format("{0}", Balance.InterroBaseAmbient));
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Balance", "GainClampMin", () => Balance.GainClampMin, delegate(float v)
			{
				Balance.GainClampMin = Math.Max(0.1f, Math.Min(1f, v));
			}, () => NemesisSettings.F(Balance.GainClampMin) ?? "");
			yield return Setting.Key<float>("Balance", "GainClampMax", () => Balance.GainClampMax, delegate(float v)
			{
				Balance.GainClampMax = Math.Max(1f, Math.Min(4f, v));
			}, () => NemesisSettings.F(Balance.GainClampMax) ?? "");
			yield return Setting.Line("");
			yield return Setting.Line("[Fear]");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Fear", "GainScale", () => Balance.FearGainScale, delegate(float v)
			{
				Balance.FearGainScale = v;
			}, () => NemesisSettings.F(Balance.FearGainScale) ?? "");
			yield return Setting.Key<int>("Fear", "DecayPer10Min", () => Balance.FearDecayPer10Min, delegate(int v)
			{
				Balance.FearDecayPer10Min = v;
			}, () => string.Format("{0}", Balance.FearDecayPer10Min));
			yield return Setting.Key<int>("Fear", "PerScar", () => Balance.FearPerScar, delegate(int v)
			{
				Balance.FearPerScar = v;
			}, () => string.Format("{0}", Balance.FearPerScar));
			yield return Setting.Key<int>("Fear", "PerCripple", () => Balance.FearPerCripple, delegate(int v)
			{
				Balance.FearPerCripple = v;
			}, () => string.Format("{0}", Balance.FearPerCripple));
			yield return Setting.Key<int>("Fear", "PerKnockout", () => Balance.FearPerKnockout, delegate(int v)
			{
				Balance.FearPerKnockout = v;
			}, () => string.Format("{0}", Balance.FearPerKnockout));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Fear", "PerFlee", () => Balance.FearPerFlee, delegate(int v)
			{
				Balance.FearPerFlee = v;
			}, () => string.Format("{0}", Balance.FearPerFlee));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Fear", "Gore", () => Balance.GoreFear, delegate(int v)
			{
				Balance.GoreFear = Math.Max(0, v);
			}, () => string.Format("{0}", Balance.GoreFear));
			yield return Setting.Key<int>("Fear", "FledHateCut", () => Balance.FledHateCut, delegate(int v)
			{
				Balance.FledHateCut = v;
			}, () => string.Format("{0}", Balance.FledHateCut));
			yield return Setting.Key<int>("Fear", "PerInterrogation", () => Balance.FearPerInterrogation, delegate(int v)
			{
				Balance.FearPerInterrogation = v;
			}, () => string.Format("{0}", Balance.FearPerInterrogation));
			yield return Setting.Key<int>("Fear", "PerRobbery", () => Balance.FearPerRobbery, delegate(int v)
			{
				Balance.FearPerRobbery = v;
			}, () => string.Format("{0}", Balance.FearPerRobbery));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Fear", "NotorietyDivisor", () => Balance.NotorietyFearDivisor, delegate(int v)
			{
				Balance.NotorietyFearDivisor = v;
			}, () => string.Format("{0}", Balance.NotorietyFearDivisor));
			yield return Setting.Line("");
			yield return Setting.Line("[Intent]");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Intent", "WontHuntFear", () => Balance.WontHuntFear, delegate(int v)
			{
				Balance.WontHuntFear = v;
			}, () => string.Format("{0}", Balance.WontHuntFear));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Intent", "ProvokedOnlyFear", () => Balance.ProvokedOnlyFear, delegate(int v)
			{
				Balance.ProvokedOnlyFear = v;
			}, () => string.Format("{0}", Balance.ProvokedOnlyFear));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Intent", "VengadorHuntAggression", () => Balance.VengefulHuntAggression, delegate(int v)
			{
				Balance.VengefulHuntAggression = v;
			}, () => string.Format("{0}", Balance.VengefulHuntAggression));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Intent", "TownTopUpSeconds", () => Balance.TownTopUpSeconds, delegate(int v)
			{
				Balance.TownTopUpSeconds = v;
			}, () => string.Format("{0}", Balance.TownTopUpSeconds));
			yield return Setting.Key<int>("Intent", "TownTopUpChance", () => Balance.TownTopUpChance, delegate(int v)
			{
				Balance.TownTopUpChance = v;
			}, () => string.Format("{0}", Balance.TownTopUpChance));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Intent", "TownEncounterChance", () => Balance.TownEncounterChance, delegate(int v)
			{
				Balance.TownEncounterChance = v;
			}, () => string.Format("{0}", Balance.TownEncounterChance));
			yield return Setting.Key<int>("Intent", "TownEncounterSeconds", () => Balance.TownEncounterSeconds, delegate(int v)
			{
				Balance.TownEncounterSeconds = Math.Max(10, v);
			}, () => string.Format("{0}", Balance.TownEncounterSeconds));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Intent", "TownSpreadMeters", () => Balance.TownSpreadMeters, delegate(float v)
			{
				Balance.TownSpreadMeters = Math.Max(5f, v);
			}, () => NemesisSettings.F(Balance.TownSpreadMeters) ?? "");
			yield return Setting.Key<int>("Intent", "TownSecondAggressorPct", () => Balance.TownSecondAggressorPct, delegate(int v)
			{
				Balance.TownSecondAggressorPct = Math.Max(0, Math.Min(100, v));
			}, () => string.Format("{0}", Balance.TownSecondAggressorPct));
			yield return Setting.Key<int>("Intent", "TownQuietAfterFight", () => Balance.TownQuietAfterFight, delegate(int v)
			{
				Balance.TownQuietAfterFight = Math.Max(0, v);
			}, () => string.Format("{0}", Balance.TownQuietAfterFight));
			yield return Setting.Key<int>("Intent", "TownWaitOutsideBase", () => Balance.TownWaitOutsideBase, delegate(int v)
			{
				Balance.TownWaitOutsideBase = Math.Max(0, Math.Min(100, v));
			}, () => string.Format("{0}", Balance.TownWaitOutsideBase));
			yield return Setting.Key<int>("Intent", "RoadAmbushMinutes", () => Balance.RoadAmbushMinutes, delegate(int v)
			{
				Balance.RoadAmbushMinutes = Math.Max(1, v);
			}, () => string.Format("{0}", Balance.RoadAmbushMinutes));
			yield return Setting.Key<int>("Intent", "TownHomicida MinHate", () => Balance.TownMurderousMinHate, delegate(int v)
			{
				Balance.TownMurderousMinHate = v;
			}, () => string.Format("{0}", Balance.TownMurderousMinHate));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Intent", "TownPassingPercent", () => Balance.TownPassingPercent, delegate(int v)
			{
				Balance.TownPassingPercent = v;
			}, () => string.Format("{0}", Balance.TownPassingPercent));
			yield return Setting.Key<float>("Intent", "TownPassingRange", () => Balance.TownPassingRange, delegate(float v)
			{
				Balance.TownPassingRange = v;
			}, () => NemesisSettings.F(Balance.TownPassingRange) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Intent", "TownCoreScale", () => Balance.TownCoreScale, delegate(float v)
			{
				Balance.TownCoreScale = v;
			}, () => NemesisSettings.F(Balance.TownCoreScale) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Intent", "TownArrivalMin", () => Balance.TownArrivalMin, delegate(int v)
			{
				Balance.TownArrivalMin = Math.Max(0, v);
			}, () => string.Format("{0}", Balance.TownArrivalMin));
			yield return Setting.Key<int>("Intent", "TownArrivalMax", () => Balance.TownArrivalMax, delegate(int v)
			{
				Balance.TownArrivalMax = Math.Max(Balance.TownArrivalMin, v);
			}, () => string.Format("{0}", Balance.TownArrivalMax));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Intent", "TownMaxMen", () => Balance.TownMaxMen, delegate(int v)
			{
				Balance.TownMaxMen = Math.Max(1, v);
			}, () => string.Format("{0}", Balance.TownMaxMen));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Intent", "TownVengadorPercent", () => Balance.TownVengefulPct, delegate(int v)
			{
				Balance.TownVengefulPct = v;
			}, () => string.Format("{0}", Balance.TownVengefulPct));
			yield return Setting.Key<int>("Intent", "TownHomicida Percent", () => Balance.TownMurderousPct, delegate(int v)
			{
				Balance.TownMurderousPct = v;
			}, () => string.Format("{0}", Balance.TownMurderousPct));
			yield return Setting.Key<int>("Intent", "TownSecondHomicida Percent", () => Balance.TownSecondMurderousPct, delegate(int v)
			{
				Balance.TownSecondMurderousPct = v;
			}, () => string.Format("{0}", Balance.TownSecondMurderousPct));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Intent", "WontHuntSpotRange", () => Balance.WontHuntSpotRange, delegate(int v)
			{
				Balance.WontHuntSpotRange = v;
			}, () => string.Format("{0}", Balance.WontHuntSpotRange));
			yield return Setting.Line("");
			yield return Setting.Line("[Factions]");
			yield return Setting.Line("; ");
			yield return Setting.Line(() => string.Join("\r\n", from g in Gangs.Defined
			select g.Id + "Encounters=" + NemesisSettings.F((float)g.AmbushBase)) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Factions", "ArmyNotorietyGate", () => Balance.ArmyNotorietyGate, delegate(int v)
			{
				Balance.ArmyNotorietyGate = v;
			}, () => string.Format("{0}", Balance.ArmyNotorietyGate));
			yield return Setting.Key<int>("Factions", "ArmyBountyGate", () => Balance.ArmyBountyGate, delegate(int v)
			{
				Balance.ArmyBountyGate = v;
			}, () => string.Format("{0}", Balance.ArmyBountyGate));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Factions", "RaiderShakeupChance", () => Balance.RaiderShakeupChance, delegate(int v)
			{
				Balance.RaiderShakeupChance = v;
			}, () => string.Format("{0}", Balance.RaiderShakeupChance));
			yield return Setting.Line("");
			yield return Setting.Line("[Naming]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Naming", "Enabled", () => this.NamingEnabled, delegate(bool v)
			{
				this.NamingEnabled = v;
			}, () => string.Format("{0}", this.NamingEnabled));
			yield return Setting.Key<float>("Naming", "Radius", () => this.NamingRadius, delegate(float v)
			{
				this.NamingRadius = v;
			}, () => NemesisSettings.F(this.NamingRadius) ?? "");
			yield return Setting.Key<int>("Naming", "Cap", () => this.NamingCap, delegate(int v)
			{
				this.NamingCap = v;
			}, () => string.Format("{0}", this.NamingCap));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Naming", "NicknameGangPercent", () => this.NicknameGangPercent, delegate(int v)
			{
				this.NicknameGangPercent = Math.Max(0, Math.Min(100, v));
			}, () => string.Format("{0}", this.NicknameGangPercent));
			yield return Setting.Key<int>("Naming", "NicknameLonerPercent", () => this.NicknameLonerPercent, delegate(int v)
			{
				this.NicknameLonerPercent = Math.Max(0, Math.Min(100, v));
			}, () => string.Format("{0}", this.NicknameLonerPercent));
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Naming", "AffiliatedPercent", () => this.AffiliatedPercent, delegate(float v)
			{
				this.AffiliatedPercent = Math.Max(0f, Math.Min(100f, v));
			}, () => NemesisSettings.F(this.AffiliatedPercent) ?? "");
			yield return Setting.Line("");
			yield return Setting.Line("[Disguise]");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Disguise", "Enabled", () => this.DisguiseEnabled, delegate(bool v)
			{
				this.DisguiseEnabled = v;
			}, () => string.Format("{0}", this.DisguiseEnabled));
			yield return Setting.Line("; ");
			yield return Setting.Key<string>("Disguise", "Categories", () => this.DisguiseCategories, delegate(string v)
			{
				this.DisguiseCategories = v;
			}, () => this.DisguiseCategories ?? "");
			yield return Setting.Line("");
			yield return Setting.Line("[GrudgeScore]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("GrudgeScore", "Threshold", () => Balance.GsThreshold, delegate(int v)
			{
				Balance.GsThreshold = v;
			}, () => string.Format("{0}", Balance.GsThreshold));
			yield return Setting.Key<int>("GrudgeScore", "Floor", () => Balance.GsFloor, delegate(int v)
			{
				Balance.GsFloor = v;
			}, () => string.Format("{0}", Balance.GsFloor));
			yield return Setting.Key<int>("GrudgeScore", "SettleSeconds", () => Balance.GsSettleSeconds, delegate(int v)
			{
				Balance.GsSettleSeconds = v;
			}, () => string.Format("{0}", Balance.GsSettleSeconds));
			yield return Setting.Key<float>("GrudgeScore", "Greet", () => Balance.GsGreet, delegate(float v)
			{
				Balance.GsGreet = v;
			}, () => NemesisSettings.F(Balance.GsGreet) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("GrudgeScore", "GreetStreakMinutes", () => Balance.GreetStreakMinutes, delegate(int v)
			{
				Balance.GreetStreakMinutes = Math.Max(1, v);
			}, () => string.Format("{0}", Balance.GreetStreakMinutes));
			yield return Setting.Key<float>("GrudgeScore", "GreetStreakBonus", () => Balance.GreetStreakBonus, delegate(float v)
			{
				Balance.GreetStreakBonus = Math.Max(0f, v);
			}, () => NemesisSettings.F(Balance.GreetStreakBonus) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "Defuse", () => Balance.GsDefuse, delegate(float v)
			{
				Balance.GsDefuse = v;
			}, () => NemesisSettings.F(Balance.GsDefuse) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "Provocar", () => Balance.GsInsult, delegate(float v)
			{
				Balance.GsInsult = v;
			}, () => NemesisSettings.F(Balance.GsInsult) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "DrawOn", () => Balance.GsDrawOn, delegate(float v)
			{
				Balance.GsDrawOn = v;
			}, () => NemesisSettings.F(Balance.GsDrawOn) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "Intimidate", () => Balance.GsIntimidate, delegate(float v)
			{
				Balance.GsIntimidate = v;
			}, () => NemesisSettings.F(Balance.GsIntimidate) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "StickUp", () => Balance.GsStickUp, delegate(float v)
			{
				Balance.GsStickUp = v;
			}, () => NemesisSettings.F(Balance.GsStickUp) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "Punch", () => Balance.GsPunch, delegate(float v)
			{
				Balance.GsPunch = v;
			}, () => NemesisSettings.F(Balance.GsPunch) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "KnockOut", () => Balance.GsKnockOut, delegate(float v)
			{
				Balance.GsKnockOut = v;
			}, () => NemesisSettings.F(Balance.GsKnockOut) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "Hogtie", () => Balance.GsHogtie, delegate(float v)
			{
				Balance.GsHogtie = v;
			}, () => NemesisSettings.F(Balance.GsHogtie) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "Robbed", () => Balance.GsRobbed, delegate(float v)
			{
				Balance.GsRobbed = v;
			}, () => NemesisSettings.F(Balance.GsRobbed) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "HorseStolen", () => Balance.GsHorseStolen, delegate(float v)
			{
				Balance.GsHorseStolen = v;
			}, () => NemesisSettings.F(Balance.GsHorseStolen) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "Cut", () => Balance.GsSlashed, delegate(float v)
			{
				Balance.GsSlashed = v;
			}, () => NemesisSettings.F(Balance.GsSlashed) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("GrudgeScore", "ShotAgain", () => Balance.GsShotAgain, delegate(float v)
			{
				Balance.GsShotAgain = v;
			}, () => NemesisSettings.F(Balance.GsShotAgain) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "UnhitCap", () => Balance.GsUnhitCap, delegate(float v)
			{
				Balance.GsUnhitCap = v;
			}, () => NemesisSettings.F(Balance.GsUnhitCap) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("GrudgeScore", "WoundCap", () => Balance.GsWoundCap, delegate(float v)
			{
				Balance.GsWoundCap = Math.Max(0f, Math.Min(100f, v));
			}, () => NemesisSettings.F(Balance.GsWoundCap) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "Reopen", () => Balance.GsReopen, delegate(float v)
			{
				Balance.GsReopen = v;
			}, () => NemesisSettings.F(Balance.GsReopen) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "KindMax", () => Balance.GsKindMax, delegate(float v)
			{
				Balance.GsKindMax = v;
			}, () => NemesisSettings.F(Balance.GsKindMax) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "KindWaitMinutes", () => Balance.GsKindWaitMin, delegate(float v)
			{
				Balance.GsKindWaitMin = v;
			}, () => NemesisSettings.F(Balance.GsKindWaitMin) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "Shot", () => Balance.GsWounded, delegate(float v)
			{
				Balance.GsWounded = v;
			}, () => NemesisSettings.F(Balance.GsWounded) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "FoughtBack", () => Balance.GsFoughtBack, delegate(float v)
			{
				Balance.GsFoughtBack = v;
			}, () => NemesisSettings.F(Balance.GsFoughtBack) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("GrudgeScore", "ShotAt", () => Balance.GsShotAt, delegate(float v)
			{
				Balance.GsShotAt = v;
			}, () => NemesisSettings.F(Balance.GsShotAt) ?? "");
			yield return Setting.Key<float>("GrudgeScore", "ComradeKilled", () => Balance.GsComradeKilled, delegate(float v)
			{
				Balance.GsComradeKilled = v;
			}, () => NemesisSettings.F(Balance.GsComradeKilled) ?? "");
			yield return Setting.Line("");
			yield return Setting.StateLine("Notoriety", () => string.Join("\r\n", Gangs.Defined.Select(delegate(Gang g)
			{
				int num;
				return g.Id + "=" + (this.Notoriety.TryGetValue(g.Id, out num) ? num : 0).ToString();
			})) ?? "");
			yield return Setting.Line("");
			yield return Setting.Line("[Menu]");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Menu", "WarHud", () => this.WarHud, delegate(bool v)
			{
				this.WarHud = v;
			}, () => string.Format("{0}", this.WarHud));
			yield return Setting.Key<float>("Menu", "WarHudX", () => this.WarHudX, delegate(float v)
			{
				this.WarHudX = ((v < 0f) ? -1f : Math.Min(0.8f, v));
			}, () => NemesisSettings.F(this.WarHudX) ?? "");
			yield return Setting.Key<float>("Menu", "WarHudY", () => this.WarHudY, delegate(float v)
			{
				this.WarHudY = Math.Max(0f, Math.Min(0.95f, v));
			}, () => NemesisSettings.F(this.WarHudY) ?? "");
			yield return Setting.Key<float>("Menu", "WarHudW", () => this.WarHudW, delegate(float v)
			{
				this.WarHudW = Math.Max(0.12f, Math.Min(0.5f, v));
			}, () => NemesisSettings.F(this.WarHudW) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Menu", "NameCards", () => this.NameCards, delegate(int v)
			{
				this.NameCards = Math.Max(0, Math.Min(2, v));
			}, () => string.Format("{0}", this.NameCards));
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Menu", "NameCardRange", () => this.NameCardRange, delegate(float v)
			{
				this.NameCardRange = v;
			}, () => NemesisSettings.F(this.NameCardRange) ?? "");
			yield return Setting.Key<float>("Menu", "NameCardKnownRange", () => this.NameCardKnownRange, delegate(float v)
			{
				this.NameCardKnownRange = v;
			}, () => NemesisSettings.F(this.NameCardKnownRange) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Menu", "NameCardSteady", () => this.NameCardSteady, delegate(float v)
			{
				this.NameCardSteady = Math.Max(0.3f, v);
			}, () => NemesisSettings.F(this.NameCardSteady) ?? "");
			yield return Setting.Key<float>("Menu", "NameCardLead", () => this.NameCardLead, delegate(float v)
			{
				this.NameCardLead = Math.Max(0f, Math.Min(3f, v));
			}, () => NemesisSettings.F(this.NameCardLead) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line(() => string.Format("Messages={0}", this.MessageLevel));
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Menu", "PauseWorld", () => this.PauseInMenus, delegate(bool v)
			{
				this.PauseInMenus = v;
			}, () => string.Format("{0}", this.PauseInMenus));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Menu", "FitTo169", () => this.FitTo169, delegate(bool v)
			{
				this.FitTo169 = v;
			}, () => string.Format("{0}", this.FitTo169));
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Menu", "WelcomeSeen", () => this.WelcomeSeen, delegate(bool v)
			{
				this.WelcomeSeen = v;
			}, () => string.Format("{0}", this.WelcomeSeen));
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Menu", "SetupSeen", () => this.SetupSeen, delegate(bool v)
			{
				this.SetupSeen = v;
			}, () => string.Format("{0}", this.SetupSeen));
			yield return Setting.Line("; ");
			yield return Setting.Key<string>("Menu", "WhatsNewSeen", () => this.WhatsNewSeen, delegate(string v)
			{
				this.WhatsNewSeen = (v ?? "");
			}, () => this.WhatsNewSeen ?? "");
			yield return Setting.Key<bool>("Menu", "GameFont", () => this.GameFont, delegate(bool v)
			{
				this.GameFont = v;
			}, () => string.Format("{0}", this.GameFont));
			yield return Setting.Line("; ");
			yield return Setting.Key<string>("Menu", "PadIcons", () => this.PadIcons, delegate(string v)
			{
				this.PadIcons = (v ?? "Auto").Trim();
			}, () => this.PadIcons ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Menu", "GameFeed", () => this.GameFeed, delegate(bool v)
			{
				this.GameFeed = v;
			}, () => string.Format("{0}", this.GameFeed));
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Menu", "MenuArrowText", () => this.MenuArrowText, delegate(bool v)
			{
				this.MenuArrowText = v;
			}, () => string.Format("{0}", this.MenuArrowText));
			yield return Setting.Line("; ");
			yield return Setting.Key<string>("Menu", "TitleFont", () => this.TitleFont, delegate(string v)
			{
				this.TitleFont = v;
			}, () => this.TitleFont ?? "");
			yield return Setting.Key<string>("Menu", "BodyFont", () => this.BodyFont, delegate(string v)
			{
				this.BodyFont = v;
			}, () => this.BodyFont ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Menu", "TextSlotLimit", () => this.TextSlotLimit, delegate(int v)
			{
				this.TextSlotLimit = v;
			}, () => string.Format("{0}", this.TextSlotLimit));
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Menu", "TextUnderline", () => this.TextUnderline, delegate(bool v)
			{
				this.TextUnderline = v;
			}, () => string.Format("{0}", this.TextUnderline));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Menu", "SelectFrame", () => this.SelectFrame, delegate(bool v)
			{
				this.SelectFrame = v;
			}, () => string.Format("{0}", this.SelectFrame));
			yield return Setting.Key<int>("Menu", "SelectFrameSize", () => this.SelectFrameSize, delegate(int v)
			{
				this.SelectFrameSize = Math.Max(2, Math.Min(64, v));
			}, () => string.Format("{0}", this.SelectFrameSize));
			yield return Setting.Key<int>("Menu", "SelectFrameEdge", () => this.SelectFrameEdge, delegate(int v)
			{
				this.SelectFrameEdge = Math.Max(1, Math.Min(64, v));
			}, () => string.Format("{0}", this.SelectFrameEdge));
			yield return Setting.Key<int>("Menu", "SelectFrameOut", () => this.SelectFrameOut, delegate(int v)
			{
				this.SelectFrameOut = Math.Max(-16, Math.Min(32, v));
			}, () => string.Format("{0}", this.SelectFrameOut));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Menu", "SelectFrameInsetX", () => this.SelectFrameInsetX, delegate(int v)
			{
				this.SelectFrameInsetX = Math.Max(0, Math.Min(100, v));
			}, () => string.Format("{0}", this.SelectFrameInsetX));
			yield return Setting.Key<int>("Menu", "SelectFrameInsetY", () => this.SelectFrameInsetY, delegate(int v)
			{
				this.SelectFrameInsetY = Math.Max(0, Math.Min(100, v));
			}, () => string.Format("{0}", this.SelectFrameInsetY));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Menu", "CardFrameUp", () => this.CardFrameUp, delegate(int v)
			{
				this.CardFrameUp = Math.Max(-20, Math.Min(20, v));
			}, () => string.Format("{0}", this.CardFrameUp));
			yield return Setting.Key<bool>("Menu", "SelectFrameTint", () => this.SelectFrameTint, delegate(bool v)
			{
				this.SelectFrameTint = v;
			}, () => string.Format("{0}", this.SelectFrameTint));
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Menu", "Ink", () => this.MenuInk, delegate(bool v)
			{
				this.MenuInk = v;
			}, () => string.Format("{0}", this.MenuInk));
			yield return Setting.Key<string>("Menu", "InkDict", () => this.MenuInkDict, delegate(string v)
			{
				this.MenuInkDict = v;
			}, () => this.MenuInkDict ?? "");
			yield return Setting.Key<string>("Menu", "InkTexture", () => this.MenuInkTexture, delegate(string v)
			{
				this.MenuInkTexture = v;
			}, () => this.MenuInkTexture ?? "");
			yield return Setting.Line("");
			yield return Setting.Line("[Tension]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Tension", "Enabled", () => this.TensionEnabled, delegate(bool v)
			{
				this.TensionEnabled = v;
			}, () => string.Format("{0}", this.TensionEnabled));
			yield return Setting.Key<float>("Tension", "RisePerMin", () => this.TensionRisePerMin, delegate(float v)
			{
				this.TensionRisePerMin = Math.Max(0f, v);
			}, () => NemesisSettings.F(this.TensionRisePerMin) ?? "");
			yield return Setting.Key<float>("Tension", "TownMult", () => this.TensionTownMult, delegate(float v)
			{
				this.TensionTownMult = Math.Max(0f, v);
			}, () => NemesisSettings.F(this.TensionTownMult) ?? "");
			yield return Setting.Key<float>("Tension", "NightBonus", () => this.TensionNightBonus, delegate(float v)
			{
				this.TensionNightBonus = v;
			}, () => NemesisSettings.F(this.TensionNightBonus) ?? "");
			yield return Setting.Key<float>("Tension", "NotorietyBonus", () => this.TensionNotorietyBonus, delegate(float v)
			{
				this.TensionNotorietyBonus = v;
			}, () => NemesisSettings.F(this.TensionNotorietyBonus) ?? "");
			yield return Setting.Key<float>("Tension", "HurtMult", () => this.TensionHurtMult, delegate(float v)
			{
				this.TensionHurtMult = Math.Max(0f, v);
			}, () => NemesisSettings.F(this.TensionHurtMult) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Tension", "FightStartDrop", () => this.TensionFightStartDrop, delegate(float v)
			{
				this.TensionFightStartDrop = Math.Max(0f, v);
			}, () => NemesisSettings.F(this.TensionFightStartDrop) ?? "");
			yield return Setting.Key<float>("Tension", "DrainBase", () => this.TensionDrainBase, delegate(float v)
			{
				this.TensionDrainBase = Math.Max(0f, v);
			}, () => NemesisSettings.F(this.TensionDrainBase) ?? "");
			yield return Setting.Key<float>("Tension", "DrainPerMan", () => this.TensionDrainPerMan, delegate(float v)
			{
				this.TensionDrainPerMan = Math.Max(0f, v);
			}, () => NemesisSettings.F(this.TensionDrainPerMan) ?? "");
			yield return Setting.Key<float>("Tension", "KillDrain", () => this.TensionKillDrain, delegate(float v)
			{
				this.TensionKillDrain = Math.Max(0f, v);
			}, () => NemesisSettings.F(this.TensionKillDrain) ?? "");
			yield return Setting.Key<float>("Tension", "OursMult", () => this.TensionOursMult, delegate(float v)
			{
				this.TensionOursMult = Math.Max(1f, v);
			}, () => NemesisSettings.F(this.TensionOursMult) ?? "");
			yield return Setting.Key<float>("Tension", "FireMin", () => this.TensionFireMin, delegate(float v)
			{
				this.TensionFireMin = Math.Max(5f, Math.Min(100f, v));
			}, () => NemesisSettings.F(this.TensionFireMin) ?? "");
			yield return Setting.Key<float>("Tension", "FireMax", () => this.TensionFireMax, delegate(float v)
			{
				this.TensionFireMax = Math.Max(this.TensionFireMin, Math.Min(100f, v));
			}, () => NemesisSettings.F(this.TensionFireMax) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Tension", "WildFireMin", () => this.TensionWildFireMin, delegate(float v)
			{
				this.TensionWildFireMin = Math.Max(5f, Math.Min(100f, v));
			}, () => NemesisSettings.F(this.TensionWildFireMin) ?? "");
			yield return Setting.Key<float>("Tension", "WildFireMax", () => this.TensionWildFireMax, delegate(float v)
			{
				this.TensionWildFireMax = Math.Max(this.TensionWildFireMin, Math.Min(100f, v));
			}, () => NemesisSettings.F(this.TensionWildFireMax) ?? "");
			yield return Setting.Key<float>("Tension", "AfterEncounter", () => this.TensionAfterEncounter, delegate(float v)
			{
				this.TensionAfterEncounter = Math.Max(0f, Math.Min(100f, v));
			}, () => NemesisSettings.F(this.TensionAfterEncounter) ?? "");
			yield return Setting.Key<float>("Tension", "AfterFight", () => this.TensionAfterFight, delegate(float v)
			{
				this.TensionAfterFight = Math.Max(0f, Math.Min(100f, v));
			}, () => NemesisSettings.F(this.TensionAfterFight) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Tension", "FirstMult", () => this.TensionFirstMult, delegate(float v)
			{
				this.TensionFirstMult = Math.Max(0.05f, Math.Min(1f, v));
			}, () => NemesisSettings.F(this.TensionFirstMult) ?? "");
			yield return Setting.Key<float>("Tension", "FirstMinDays", () => this.TensionFirstMinDays, delegate(float v)
			{
				this.TensionFirstMinDays = Math.Max(0.1f, Math.Min(30f, v));
			}, () => NemesisSettings.F(this.TensionFirstMinDays) ?? "");
			yield return Setting.Key<float>("Tension", "FirstMaxDays", () => this.TensionFirstMaxDays, delegate(float v)
			{
				this.TensionFirstMaxDays = Math.Max(this.TensionFirstMinDays, Math.Min(30f, v));
			}, () => NemesisSettings.F(this.TensionFirstMaxDays) ?? "");
			yield return Setting.Key<float>("Tension", "StrangerKeep", () => this.TensionStrangerKeep, delegate(float v)
			{
				this.TensionStrangerKeep = Math.Max(0f, Math.Min(1f, v));
			}, () => NemesisSettings.F(this.TensionStrangerKeep) ?? "");
			yield return Setting.Line("");
			yield return Setting.Line("[Encounters]");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Encuentros", "Amount", () => this.EncounterAmount, delegate(int v)
			{
				this.EncounterAmount = Math.Max(0, Math.Min(3, v));
			}, () => string.Format("{0}", this.EncounterAmount));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Encuentros", "Ambush", () => this.EncAmbush, delegate(int v)
			{
				this.EncAmbush = v;
			}, () => string.Format("{0}", this.EncAmbush));
			yield return Setting.Key<int>("Encuentros", "Riders", () => this.EncRiders, delegate(int v)
			{
				this.EncRiders = v;
			}, () => string.Format("{0}", this.EncRiders));
			yield return Setting.Key<int>("Encuentros", "Ahead", () => this.EncAhead, delegate(int v)
			{
				this.EncAhead = v;
			}, () => string.Format("{0}", this.EncAhead));
			yield return Setting.Key<int>("Encuentros", "Sniper", () => this.EncSniper, delegate(int v)
			{
				this.EncSniper = v;
			}, () => string.Format("{0}", this.EncSniper));
			yield return Setting.Key<int>("Encuentros", "Callout", () => this.EncCallout, delegate(int v)
			{
				this.EncCallout = v;
			}, () => string.Format("{0}", this.EncCallout));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Encuentros", "SkirmishChance", () => Balance.SkirmishChance, delegate(int v)
			{
				Balance.SkirmishChance = v;
			}, () => string.Format("{0}", Balance.SkirmishChance));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Encuentros", "ArmyPatrolChance", () => Balance.PatrolChance, delegate(int v)
			{
				Balance.PatrolChance = v;
			}, () => string.Format("{0}", Balance.PatrolChance));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Encuentros", "NewNemesisRestHours", () => Balance.NewNemesisRestHours, delegate(int v)
			{
				Balance.NewNemesisRestHours = v;
			}, () => string.Format("{0}", Balance.NewNemesisRestHours));
			yield return Setting.Key<int>("Encuentros", "RespawnQuietSeconds", () => Balance.RespawnQuietSeconds, delegate(int v)
			{
				Balance.RespawnQuietSeconds = v;
			}, () => string.Format("{0}", Balance.RespawnQuietSeconds));
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Encuentros", "WildResentido", () => Balance.WildResentful, delegate(float v)
			{
				Balance.WildResentful = v;
			}, () => NemesisSettings.F(Balance.WildResentful) ?? "");
			yield return Setting.Key<float>("Encuentros", "WildVengador", () => Balance.WildVengeful, delegate(float v)
			{
				Balance.WildVengeful = v;
			}, () => NemesisSettings.F(Balance.WildVengeful) ?? "");
			yield return Setting.Key<float>("Encuentros", "WildHomicida ", () => Balance.WildMurderous, delegate(float v)
			{
				Balance.WildMurderous = v;
			}, () => NemesisSettings.F(Balance.WildMurderous) ?? "");
			yield return Setting.Key<float>("Encuentros", "TownResentido", () => Balance.TownResentful, delegate(float v)
			{
				Balance.TownResentful = v;
			}, () => NemesisSettings.F(Balance.TownResentful) ?? "");
			yield return Setting.Key<float>("Encuentros", "TownVengador", () => Balance.TownVengefulW, delegate(float v)
			{
				Balance.TownVengefulW = v;
			}, () => NemesisSettings.F(Balance.TownVengefulW) ?? "");
			yield return Setting.Key<float>("Encuentros", "TownHomicida ", () => Balance.TownMurderousW, delegate(float v)
			{
				Balance.TownMurderousW = v;
			}, () => NemesisSettings.F(Balance.TownMurderousW) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Encuentros", "Homicida CooldownHours", () => Balance.MurderousCooldownHours, delegate(int v)
			{
				Balance.MurderousCooldownHours = v;
			}, () => string.Format("{0}", Balance.MurderousCooldownHours));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Encuentros", "StandoffResentido", () => Balance.StandoffResentful, delegate(int v)
			{
				Balance.StandoffResentful = v;
			}, () => string.Format("{0}", Balance.StandoffResentful));
			yield return Setting.Key<int>("Encuentros", "StandoffVengador", () => Balance.StandoffVengeful, delegate(int v)
			{
				Balance.StandoffVengeful = v;
			}, () => string.Format("{0}", Balance.StandoffVengeful));
			yield return Setting.Key<int>("Encuentros", "StandoffHomicida ", () => Balance.StandoffMurderous, delegate(int v)
			{
				Balance.StandoffMurderous = v;
			}, () => string.Format("{0}", Balance.StandoffMurderous));
			yield return Setting.Key<int>("Encuentros", "ManCooldownHours", () => Balance.ManCooldownHours, delegate(int v)
			{
				Balance.ManCooldownHours = v;
			}, () => string.Format("{0}", Balance.ManCooldownHours));
			yield return Setting.Line("; ");
			yield return Setting.Key<float>("Encuentros", "ArmyPatrolHearing", () => Balance.PatrolHearingRange, delegate(float v)
			{
				Balance.PatrolHearingRange = v;
			}, () => NemesisSettings.F(Balance.PatrolHearingRange) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Encuentros", "PatrolRespondChance", () => Balance.PatrolRespondChance, delegate(int v)
			{
				Balance.PatrolRespondChance = v;
			}, () => string.Format("{0}", Balance.PatrolRespondChance));
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Encuentros", "PatrolRespondMin", () => Balance.PatrolRespondMin, delegate(int v)
			{
				Balance.PatrolRespondMin = Math.Max(100, v);
			}, () => string.Format("{0}", Balance.PatrolRespondMin));
			yield return Setting.Key<int>("Encuentros", "PatrolRespondMax", () => Balance.PatrolRespondMax, delegate(int v)
			{
				Balance.PatrolRespondMax = Math.Max(Balance.PatrolRespondMin, v);
			}, () => string.Format("{0}", Balance.PatrolRespondMax));
			yield return Setting.Key<int>("Encuentros", "PatrolRespondCooldown", () => Balance.PatrolRespondCooldown, delegate(int v)
			{
				Balance.PatrolRespondCooldown = v;
			}, () => string.Format("{0}", Balance.PatrolRespondCooldown));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Encuentros", "PatrolMountedPercent", () => Balance.PatrolMountedPct, delegate(int v)
			{
				Balance.PatrolMountedPct = v;
			}, () => string.Format("{0}", Balance.PatrolMountedPct));
			yield return Setting.Key<int>("Encuentros", "PatrolHurryPercent", () => Balance.PatrolHurryPct, delegate(int v)
			{
				Balance.PatrolHurryPct = v;
			}, () => string.Format("{0}", Balance.PatrolHurryPct));
			yield return Setting.Line("; ");
			yield return Setting.Key<string>("Encuentros", "Force", () => this.EncounterForce, delegate(string v)
			{
				this.EncounterForce = v;
			}, () => this.EncounterForce ?? "");
			yield return Setting.Line("");
			yield return Setting.Line("[Camps]");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Campamentos", "Enabled", () => this.CampsEnabled, delegate(bool v)
			{
				this.CampsEnabled = v;
			}, () => string.Format("{0}", this.CampsEnabled));
			yield return Setting.Key<float>("Campamentos", "SpawnRange", () => this.CampSpawnRange, delegate(float v)
			{
				this.CampSpawnRange = v;
			}, () => NemesisSettings.F(this.CampSpawnRange) ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Campamentos", "ExtraNamedNone", () => Balance.CampExtraNamed0, delegate(int v)
			{
				Balance.CampExtraNamed0 = v;
			}, () => string.Format("{0}", Balance.CampExtraNamed0));
			yield return Setting.Key<int>("Campamentos", "ExtraNamedOne", () => Balance.CampExtraNamed1, delegate(int v)
			{
				Balance.CampExtraNamed1 = v;
			}, () => string.Format("{0}", Balance.CampExtraNamed1));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Campamentos", "RaidChance", () => Balance.CampRaidChance, delegate(int v)
			{
				Balance.CampRaidChance = v;
			}, () => string.Format("{0}", Balance.CampRaidChance));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Campamentos", "ClearDays", () => this.CampClearDays, delegate(int v)
			{
				this.CampClearDays = v;
			}, () => string.Format("{0}", this.CampClearDays));
			yield return Setting.StateKey<string>("Campamentos", "Cleared", () => this.ClearedCamps, delegate(string v)
			{
				this.ClearedCamps = v;
			}, () => this.ClearedCamps ?? "");
			yield return Setting.Line("");
			yield return Setting.Line("[Keys]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<Keys>("Keys", "RosterMenu", () => this.RosterMenuKey, delegate(Keys v)
			{
				this.RosterMenuKey = v;
				KeyCombo.Current = v;
			}, () => KeyCombo.Name(this.RosterMenuKey));
			yield return Setting.Key<Keys>("Keys", "ForceAmbush", () => this.ForceAmbushKey, delegate(Keys v)
			{
				this.ForceAmbushKey = v;
			}, () => string.Format("{0}", this.ForceAmbushKey));
			yield return Setting.Key<Keys>("Keys", "Interrogar", () => this.InterrogateKey, delegate(Keys v)
			{
				this.InterrogateKey = v;
			}, () => string.Format("{0}", this.InterrogateKey));
			yield return Setting.Key<Keys>("Keys", "Telégrafo", () => this.TelegraphKey, delegate(Keys v)
			{
				this.TelegraphKey = v;
			}, () => string.Format("{0}", this.TelegraphKey));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Keys", "PadMenu", () => this.PadMenuOpen, delegate(int v)
			{
				this.PadMenuOpen = Math.Max(0, Math.Min(4, v));
				KeyCombo.PadMode = this.PadMenuOpen;
			}, () => string.Format("{0}", this.PadMenuOpen));
			yield return Setting.Line("; ");
			yield return Setting.Key<eInputType>("Keys", "LetGoPromptControl", () => this.InterrogateReleaseControl, delegate(eInputType v)
			{
				this.InterrogateReleaseControl = v;
			}, () => string.Format("{0}", this.InterrogateReleaseControl));
			yield return Setting.Line("");
			yield return Setting.Line("[Interrogation]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<string>("Interrogatorio", "HeldPanel", () => this.HeldPanel, delegate(string v)
			{
				this.HeldPanel = v;
			}, () => this.HeldPanel ?? "");
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Interrogatorio", "FearPerHit", () => Balance.TerrorHit, delegate(int v)
			{
				Balance.TerrorHit = v;
			}, () => string.Format("{0}", Balance.TerrorHit));
			yield return Setting.Key<int>("Interrogatorio", "FearPerStab", () => Balance.TerrorStab, delegate(int v)
			{
				Balance.TerrorStab = v;
			}, () => string.Format("{0}", Balance.TerrorStab));
			yield return Setting.Key<int>("Interrogatorio", "FearPerShot", () => Balance.TerrorShot, delegate(int v)
			{
				Balance.TerrorShot = v;
			}, () => string.Format("{0}", Balance.TerrorShot));
			yield return Setting.Key<int>("Interrogatorio", "FearPerShotNearHim", () => Balance.TerrorNearShot, delegate(int v)
			{
				Balance.TerrorNearShot = v;
			}, () => string.Format("{0}", Balance.TerrorNearShot));
			yield return Setting.Key<int>("Interrogatorio", "FearPerThreat", () => Balance.TerrorThreat, delegate(int v)
			{
				Balance.TerrorThreat = v;
			}, () => string.Format("{0}", Balance.TerrorThreat));
			yield return Setting.Key<int>("Interrogatorio", "FearPerSecondAimedAt", () => Balance.TerrorAimPerSec, delegate(int v)
			{
				Balance.TerrorAimPerSec = v;
			}, () => string.Format("{0}", Balance.TerrorAimPerSec));
			yield return Setting.Key<int>("Interrogatorio", "FearPerLoot", () => Balance.TerrorLoot, delegate(int v)
			{
				Balance.TerrorLoot = v;
			}, () => string.Format("{0}", Balance.TerrorLoot));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Interrogatorio", "FamilyFearExtra", () => Balance.InterroFamilyFearExtra, delegate(int v)
			{
				Balance.InterroFamilyFearExtra = v;
			}, () => string.Format("{0}", Balance.InterroFamilyFearExtra));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Interrogatorio", "TriesPerScare", () => Balance.InterroTriesPerScare, delegate(int v)
			{
				Balance.InterroTriesPerScare = v;
			}, () => string.Format("{0}", Balance.InterroTriesPerScare));
			yield return Setting.Key<int>("Interrogatorio", "ScareToRetry", () => Balance.InterroScareToRetry, delegate(int v)
			{
				Balance.InterroScareToRetry = v;
			}, () => string.Format("{0}", Balance.InterroScareToRetry));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("Interrogatorio", "FearPerKillSeen", () => Balance.TerrorComradeKilled, delegate(int v)
			{
				Balance.TerrorComradeKilled = v;
			}, () => string.Format("{0}", Balance.TerrorComradeKilled));
			yield return Setting.Key<int>("Interrogatorio", "SheetFearPerKillSeen", () => Balance.FearPerComradeKilled, delegate(int v)
			{
				Balance.FearPerComradeKilled = v;
			}, () => string.Format("{0}", Balance.FearPerComradeKilled));
			yield return Setting.Line("");
			yield return Setting.Line("[Debug]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Debug", "Enabled", () => this.DebugEnabled, delegate(bool v)
			{
				this.DebugEnabled = v;
			}, () => string.Format("{0}", this.DebugEnabled));
			yield return Setting.Key<int>("Debug", "Seed", () => this.DebugSeed, delegate(int v)
			{
				this.DebugSeed = v;
			}, () => string.Format("{0}", this.DebugSeed));
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("Debug", "Verbose", () => this.Verbose, delegate(bool v)
			{
				this.Verbose = v;
			}, () => string.Format("{0}", this.Verbose));
			yield return Setting.Line("");
			yield return Setting.Line("[GameSystems]");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("GameSystems", "Weather", () => this.GsWeather, delegate(bool v)
			{
				this.GsWeather = v;
			}, () => string.Format("{0}", this.GsWeather));
			yield return Setting.Key<bool>("GameSystems", "Minds", () => this.GsMinds, delegate(bool v)
			{
				this.GsMinds = v;
			}, () => string.Format("{0}", this.GsMinds));
			yield return Setting.Key<bool>("GameSystems", "Body", () => this.GsBody, delegate(bool v)
			{
				this.GsBody = v;
			}, () => string.Format("{0}", this.GsBody));
			yield return Setting.Key<bool>("GameSystems", "Music", () => this.GsMusic, delegate(bool v)
			{
				this.GsMusic = v;
			}, () => string.Format("{0}", this.GsMusic));
			yield return Setting.Key<bool>("GameSystems", "Map", () => this.GsMap, delegate(bool v)
			{
				this.GsMap = v;
			}, () => string.Format("{0}", this.GsMap));
			yield return Setting.Line("; ");
			yield return Setting.Key<bool>("GameSystems", "Gestures", () => this.GsGestures, delegate(bool v)
			{
				this.GsGestures = v;
			}, () => string.Format("{0}", this.GsGestures));
			yield return Setting.Line("; ");
			yield return Setting.Key<int>("GameSystems", "HandshakeVariant", () => this.HandshakeVariant, delegate(int v)
			{
				this.HandshakeVariant = Math.Max(0, Math.Min(5, v));
			}, () => string.Format("{0}", this.HandshakeVariant));
			yield return Setting.Line("");
			yield return Setting.Line("[Meta]");
			yield return Setting.Line("; ");
			yield return Setting.Line(() => string.Format("Version={0}", this.ConfigVersion));
			yield break;
		}

		// Token: 0x04000AE4 RID: 2788
		private readonly string _path;

		// Token: 0x04000AE5 RID: 2789
		private readonly string _statePath;

		// Token: 0x04000AE6 RID: 2790
		public bool RandomAmbushes = true;

		// Token: 0x04000AE7 RID: 2791
		public int AmbushIntervalSeconds = 120;

		// Token: 0x04000AE8 RID: 2792
		public int AmbushChancePercent = 35;

		// Token: 0x04000AE9 RID: 2793
		public int CheatDeathBaseChance = 35;

		// Token: 0x04000AEA RID: 2794
		public int CheatDeathPerRank = 5;

		// Token: 0x04000AEB RID: 2795
		public int CheatDeathMaxChance = 60;

		// Token: 0x04000AEC RID: 2796
		public bool CheatDeathAlways;

		// Token: 0x04000AED RID: 2797
		public bool FatalWoundsFinal = true;

		// Token: 0x04000AEE RID: 2798
		public int BeatingHits = 3;

		// Token: 0x04000AEF RID: 2799
		public float ResentfulRange = 500f;

		// Token: 0x04000AF0 RID: 2800
		public float VengefulRange = 1500f;

		// Token: 0x04000AF1 RID: 2801
		public int MessageLevel = 4;

		// Token: 0x04000AF2 RID: 2802
		public static readonly string[] MessageLevelNames = new string[]
		{
			"Sin mensajes",
			"Solo diálogos",
			"Diálogos y muertes",
			"Todos los mensajes",
			"Solo muertes"
		};

		// Token: 0x04000AF3 RID: 2803
		public static readonly string[] MessageLevelShort = new string[]
		{
			"No",
			"Diálogos",
			"Diálogos y muertes",
			"Todo",
			"Muertes"
		};

		// Token: 0x04000AF4 RID: 2804
		public int MaxLivingNemeses = 100;

		// Token: 0x04000AF5 RID: 2805
		public int FollowersRank1;

		// Token: 0x04000AF6 RID: 2806
		public int FollowersRank2 = 1;

		// Token: 0x04000AF7 RID: 2807
		public int FollowersRank3 = 3;

		// Token: 0x04000AF8 RID: 2808
		public int FollowersRank4 = 5;

		// Token: 0x04000AF9 RID: 2809
		public int FollowersRank5 = 10;

		// Token: 0x04000AFA RID: 2810
		public int GangAlonePercent = 15;

		// Token: 0x04000AFB RID: 2811
		public int LonerAlonePercent = 65;

		// Token: 0x04000AFC RID: 2812
		public int ParalyzedSightingChance = 35;

		// Token: 0x04000AFD RID: 2813
		public float GangAmbushWeight = 0.15f;

		// Token: 0x04000AFE RID: 2814
		public string SeededGangs = "";

		// Token: 0x04000AFF RID: 2815
		public bool ArmyTopUp;

		// Token: 0x04000B00 RID: 2816
		public bool Succession = true;

		// Token: 0x04000B01 RID: 2817
		public int SuccessionHour = 6;

		// Token: 0x04000B02 RID: 2818
		public int SuccessionKinChance = 35;

		// Token: 0x04000B03 RID: 2819
		public int LastSuccessionDay;

		// Token: 0x04000B04 RID: 2820
		public int NextSuccessionDay;

		// Token: 0x04000B05 RID: 2821
		public int SuccessionMinDays = 2;

		// Token: 0x04000B06 RID: 2822
		public int SuccessionMaxDays = 6;

		// Token: 0x04000B07 RID: 2823
		public bool ArmyGeneral = true;

		// Token: 0x04000B08 RID: 2824
		public bool CalmEnabled = true;

		// Token: 0x04000B09 RID: 2825
		public int CalmGraceSeconds = 15;

		// Token: 0x04000B0A RID: 2826
		public int CalmLoadGraceSeconds = 10;

		// Token: 0x04000B0B RID: 2827
		public string WorldMarkerStyle = "NEUTRAL_SECRET";

		// Token: 0x04000B0C RID: 2828
		public int WorldSurvivorChance = 35;

		// Token: 0x04000B0D RID: 2829
		public float WorldSurvivorSpotRange = 30f;

		// Token: 0x04000B0E RID: 2830
		public float CalmCampRadius = 180f;

		// Token: 0x04000B0F RID: 2831
		public bool CalmWhileWanted = true;

		// Token: 0x04000B10 RID: 2832
		public string GangCamps = "";

		// Token: 0x04000B11 RID: 2833
		public int WorldCatchUpMax = 24;

		// Token: 0x04000B12 RID: 2834
		public bool WorldEvents = true;

		// Token: 0x04000B13 RID: 2835
		public int WorldMinHours = 1;

		// Token: 0x04000B14 RID: 2836
		public int WorldMaxHours = 1;

		// Token: 0x04000B15 RID: 2837
		public bool SavePointsEnabled = true;

		// Token: 0x04000B16 RID: 2838
		public bool SavePointAutoRestore = true;

		// Token: 0x04000B17 RID: 2839
		public int SavePointBackMinutes = 60;

		// Token: 0x04000B18 RID: 2840
		public int SavePointEveryHours = 3;

		// Token: 0x04000B19 RID: 2841
		public int SavePointMaxMB = 150;

		// Token: 0x04000B1A RID: 2842
		public Keys TelegraphKey = Keys.T;

		// Token: 0x04000B1B RID: 2843
		public int TelegraphMinDays = 1;

		// Token: 0x04000B1C RID: 2844
		public int TelegraphMaxDays = 7;

		// Token: 0x04000B1D RID: 2845
		public float WorldMinDistance = 600f;

		// Token: 0x04000B1E RID: 2846
		public int WarLine = -35;

		// Token: 0x04000B1F RID: 2847
		public int ClashLine = -25;

		// Token: 0x04000B20 RID: 2848
		public bool WarMarkers = true;

		// Token: 0x04000B21 RID: 2849
		public bool TownRepMarkers = true;

		// Token: 0x04000B22 RID: 2850
		public string WarRegionStyle = "BLIP_STYLE_DEBUG_RED";

		// Token: 0x04000B23 RID: 2851
		public int WorldNews = 2;

		// Token: 0x04000B24 RID: 2852
		public static readonly string[] WorldNewsShort = new string[]
		{
			"No",
			"Muertes",
			"Grandes noticias",
			"Todo"
		};

		// Token: 0x04000B25 RID: 2853
		public float WorldMarkerRadius = 150f;

		// Token: 0x04000B26 RID: 2854
		public int WorldAftermathHours = 24;

		// Token: 0x04000B27 RID: 2855
		public int WorldLethality = 100;

		// Token: 0x04000B28 RID: 2856
		public int WorldBatchMin = 1;

		// Token: 0x04000B29 RID: 2857
		public int WorldBatchMax = 2;

		// Token: 0x04000B2A RID: 2858
		public int WorldBigBatchChance = 10;

		// Token: 0x04000B2B RID: 2859
		public int WorldBigBatchMin = 3;

		// Token: 0x04000B2C RID: 2860
		public int WorldBigBatchMax = 5;

		// Token: 0x04000B2D RID: 2861
		public int WorldMaxClashesPerDay = 3;

		// Token: 0x04000B2E RID: 2862
		public int WorldClashWeight = 20;

		// Token: 0x04000B2F RID: 2863
		public int WorldFeudWeight = 10;

		// Token: 0x04000B30 RID: 2864
		public int WorldBrawlWeight = 10;

		// Token: 0x04000B31 RID: 2865
		public int WorldLifeWeight = 60;

		// Token: 0x04000B32 RID: 2866
		public int WorldRivalryWeight = 8;

		// Token: 0x04000B33 RID: 2867
		public int WorldVdlWeight = 4;

		// Token: 0x04000B34 RID: 2868
		public int WorldWordWeight = 5;

		// Token: 0x04000B35 RID: 2869
		public Dictionary<string, int> Notoriety = new Dictionary<string, int>();

		// Token: 0x04000B36 RID: 2870
		public Keys RosterMenuKey = Keys.F11;

		// Token: 0x04000B37 RID: 2871
		public Keys InterrogateKey = Keys.I;

		// Token: 0x04000B38 RID: 2872
		public string HeldPanel = "Auto";

		// Token: 0x04000B39 RID: 2873
		public eInputType InterrogateReleaseControl = 992265328;

		// Token: 0x04000B3A RID: 2874
		public bool CampsEnabled = true;

		// Token: 0x04000B3B RID: 2875
		public int CrewRosterChance = 40;

		// Token: 0x04000B3C RID: 2876
		public bool HiredGunsJoin = true;

		// Token: 0x04000B3D RID: 2877
		public int EncAmbush = 35;

		// Token: 0x04000B3E RID: 2878
		public int EncRiders = 25;

		// Token: 0x04000B3F RID: 2879
		public int EncAhead = 15;

		// Token: 0x04000B40 RID: 2880
		public int EncSniper = 10;

		// Token: 0x04000B41 RID: 2881
		public int EncCallout = 100;

		// Token: 0x04000B42 RID: 2882
		public bool NamingEnabled = true;

		// Token: 0x04000B43 RID: 2883
		public bool WarHud = true;

		// Token: 0x04000B44 RID: 2884
		public float WarHudX = -1f;

		// Token: 0x04000B45 RID: 2885
		public float WarHudY = 0.166f;

		// Token: 0x04000B46 RID: 2886
		public float WarHudW = 0.215f;

		// Token: 0x04000B47 RID: 2887
		public int NameCards = 1;

		// Token: 0x04000B48 RID: 2888
		public float NameCardRange = 30f;

		// Token: 0x04000B49 RID: 2889
		public float NameCardKnownRange = 75f;

		// Token: 0x04000B4A RID: 2890
		public float NameCardSteady = 2.5f;

		// Token: 0x04000B4B RID: 2891
		public float NameCardLead = 1f;

		// Token: 0x04000B4C RID: 2892
		public int HandshakeVariant = 2;

		// Token: 0x04000B4D RID: 2893
		public bool GsWeather = true;

		// Token: 0x04000B4E RID: 2894
		public bool GsMinds = true;

		// Token: 0x04000B4F RID: 2895
		public bool GsBody = true;

		// Token: 0x04000B50 RID: 2896
		public bool GsMusic;

		// Token: 0x04000B51 RID: 2897
		public bool GsMap = true;

		// Token: 0x04000B52 RID: 2898
		public bool GsGestures = true;

		// Token: 0x04000B53 RID: 2899
		public bool LinkLooks = true;

		// Token: 0x04000B54 RID: 2900
		public int LookMemoryDays = 3;

		// Token: 0x04000B55 RID: 2901
		public int LookCap = 5000;

		// Token: 0x04000B56 RID: 2902
		public bool RememberPeople = true;

		// Token: 0x04000B57 RID: 2903
		public int PeopleCap = 1000;

		// Token: 0x04000B58 RID: 2904
		public int RegularsMax = 6;

		// Token: 0x04000B59 RID: 2905
		public int RegularChance = 60;

		// Token: 0x04000B5A RID: 2906
		public bool RegularsAmbient;

		// Token: 0x04000B5B RID: 2907
		public bool SocialAmbient;

		// Token: 0x04000B5C RID: 2908
		public float NamingRadius = 40f;

		// Token: 0x04000B5D RID: 2909
		public int NamingCap = 250;

		// Token: 0x04000B5E RID: 2910
		public int NicknameGangPercent = 30;

		// Token: 0x04000B5F RID: 2911
		public int NicknameLonerPercent = 12;

		// Token: 0x04000B60 RID: 2912
		public float AffiliatedPercent = 4f;

		// Token: 0x04000B61 RID: 2913
		public bool DisguiseEnabled = true;

		// Token: 0x04000B62 RID: 2914
		public string DisguiseCategories = "MASKS,MASKS_LARGE";

		// Token: 0x04000B63 RID: 2915
		public string EncounterForce = "";

		// Token: 0x04000B64 RID: 2916
		public bool PauseInMenus = true;

		// Token: 0x04000B65 RID: 2917
		public bool WelcomeSeen;

		// Token: 0x04000B66 RID: 2918
		public int PadMenuOpen;

		// Token: 0x04000B67 RID: 2919
		internal static readonly string[] PadMenuOpenNames = new string[]
		{
			"Double-tap D-pad Right",
			"Double-tap D-pad Left",
			"Double-tap D-pad Down",
			"Hold D-pad Right",
			"No"
		};

		// Token: 0x04000B68 RID: 2920
		public bool SetupSeen;

		// Token: 0x04000B69 RID: 2921
		public string WhatsNewSeen = "";

		// Token: 0x04000B6A RID: 2922
		public int EncounterAmount = 1;

		// Token: 0x04000B6B RID: 2923
		internal static readonly string[] EncounterAmountNames = new string[]
		{
			"Menos",
			"Normal",
			"Más",
			"Muchos"
		};

		// Token: 0x04000B6C RID: 2924
		internal static readonly float[] EncounterAmountMult = new float[]
		{
			0.5f,
			1f,
			1.6f,
			2.4f
		};

		// Token: 0x04000B6D RID: 2925
		public bool GameFont = true;

		// Token: 0x04000B6E RID: 2926
		public bool FitTo169 = true;

		// Token: 0x04000B6F RID: 2927
		public string PadIcons = "Auto";

		// Token: 0x04000B70 RID: 2928
		public bool GameFeed;

		// Token: 0x04000B71 RID: 2929
		public bool MenuArrowText = true;

		// Token: 0x04000B72 RID: 2930
		public int TextSlotLimit = 230;

		// Token: 0x04000B73 RID: 2931
		public bool TextUnderline = true;

		// Token: 0x04000B74 RID: 2932
		public bool SelectFrame = true;

		// Token: 0x04000B75 RID: 2933
		public bool SelectFrameTint;

		// Token: 0x04000B76 RID: 2934
		public int SelectFrameSize = 16;

		// Token: 0x04000B77 RID: 2935
		public int SelectFrameOut = 4;

		// Token: 0x04000B78 RID: 2936
		public int SelectFrameEdge = 16;

		// Token: 0x04000B79 RID: 2937
		public int SelectFrameInsetX = 28;

		// Token: 0x04000B7A RID: 2938
		public int SelectFrameInsetY = 12;

		// Token: 0x04000B7B RID: 2939
		public int CardFrameUp = 1;

		// Token: 0x04000B7C RID: 2940
		public bool MenuInk = true;

		// Token: 0x04000B7D RID: 2941
		public string MenuInkDict = "pausemenu_textures";

		// Token: 0x04000B7E RID: 2942
		public string MenuInkTexture = "menu_ink_large1";

		// Token: 0x04000B7F RID: 2943
		public string BodyFont = "$body";

		// Token: 0x04000B80 RID: 2944
		public string TitleFont = "$title";

		// Token: 0x04000B81 RID: 2945
		public int CampClearDays = 1;

		// Token: 0x04000B82 RID: 2946
		public string ClearedCamps = "";

		// Token: 0x04000B83 RID: 2947
		public float CampSpawnRange = 170f;

		// Token: 0x04000B84 RID: 2948
		public Keys ForceAmbushKey = Keys.F10;

		// Token: 0x04000B85 RID: 2949
		public int ConfigVersion;

		// Token: 0x04000B86 RID: 2950
		public int LoadedVersion = -1;

		// Token: 0x04000B87 RID: 2951
		public int LastDecayDay;

		// Token: 0x04000B88 RID: 2952
		public int LastDecayDayBefore;

		// Token: 0x04000B89 RID: 2953
		public int DebugSeed;

		// Token: 0x04000B8A RID: 2954
		public int DistantExplosionTag = 25;

		// Token: 0x04000B8B RID: 2955
		public bool DistantBattles = true;

		// Token: 0x04000B8C RID: 2956
		public bool DebugEnabled;

		// Token: 0x04000B8D RID: 2957
		public bool Verbose;

		// Token: 0x04000B8E RID: 2958
		public bool TensionEnabled = true;

		// Token: 0x04000B8F RID: 2959
		public float TensionRisePerMin = 8.5f;

		// Token: 0x04000B90 RID: 2960
		public float TensionTownMult = 1.4f;

		// Token: 0x04000B91 RID: 2961
		public float TensionNightBonus = 2f;

		// Token: 0x04000B92 RID: 2962
		public float TensionNotorietyBonus = 1f;

		// Token: 0x04000B93 RID: 2963
		public float TensionHurtMult = 0.3f;

		// Token: 0x04000B94 RID: 2964
		public float TensionCombatDrainPerSec = 3f;

		// Token: 0x04000B95 RID: 2965
		public float TensionFightStartDrop = 12f;

		// Token: 0x04000B96 RID: 2966
		public float TensionDrainBase = 0.3f;

		// Token: 0x04000B97 RID: 2967
		public float TensionDrainPerMan = 0.2f;

		// Token: 0x04000B98 RID: 2968
		public float TensionKillDrain = 3f;

		// Token: 0x04000B99 RID: 2969
		public float TensionOursMult = 2f;

		// Token: 0x04000B9A RID: 2970
		public float TensionFireMin = 55f;

		// Token: 0x04000B9B RID: 2971
		public float TensionFireMax = 70f;

		// Token: 0x04000B9C RID: 2972
		public float TensionWildFireMin = 30f;

		// Token: 0x04000B9D RID: 2973
		public float TensionWildFireMax = 50f;

		// Token: 0x04000B9E RID: 2974
		public float TensionAfterEncounter = 35f;

		// Token: 0x04000B9F RID: 2975
		public float TensionAfterFight = 15f;

		// Token: 0x04000BA0 RID: 2976
		public float TensionFirstMult = 0.4f;

		// Token: 0x04000BA1 RID: 2977
		public float TensionFirstMinDays = 1f;

		// Token: 0x04000BA2 RID: 2978
		public float TensionFirstMaxDays = 2f;

		// Token: 0x04000BA3 RID: 2979
		public float TensionStrangerKeep = 0.5f;

		// Token: 0x04000BA4 RID: 2980
		public const int CurrentVersion = 49;

		// Token: 0x04000BA5 RID: 2981
		private bool _iniFailed;

		// Token: 0x04000BA6 RID: 2982
		private bool _stateFailed;
	}
}
