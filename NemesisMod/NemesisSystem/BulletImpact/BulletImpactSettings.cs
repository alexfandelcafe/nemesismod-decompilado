using System;
using System.Globalization;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x02000094 RID: 148
	internal sealed class BulletImpactSettings
	{
		// Token: 0x06001440 RID: 5184 RVA: 0x0014344C File Offset: 0x0014164C
		public BulletImpactSettings(string path)
		{
			this._path = path;
			IniFile iniFile = IniFile.Load(path);
			this.Enabled = iniFile.GetValue<bool>("BulletImpact", "Enabled", this.Enabled);
			this.LogHits = iniFile.GetValue<bool>("Debug", "LogHits", this.LogHits);
			this.Crippling = iniFile.GetValue<bool>("Wounds", "Crippling", this.Crippling);
			this.PlayerDamage = iniFile.GetValue<float>("Wounds", "PlayerDamage", this.PlayerDamage);
			this.KillShots = iniFile.GetValue<bool>("Wounds", "KillShots", this.KillShots);
			this.XRayMaxEntries = Math.Max(1, iniFile.GetValue<int>("XRay", "MaxEntries", this.XRayMaxEntries));
			this.XRayQuality = iniFile.GetValue<XRayQuality>("XRay", "Detail", this.XRayQuality);
			this.XRayRectLimit = iniFile.GetValue<int>("XRay", "RectLimit", this.XRayRectLimit);
			this.XRayGlyphCost = Math.Max(0f, Math.Min(4f, iniFile.GetValue<float>("XRay", "GlyphCost", this.XRayGlyphCost)));
			int value = iniFile.GetValue<int>("Meta", "Version", 0);
			if (value < 1 && (this.XRayGlyphCost == 1f || this.XRayGlyphCost == 0.4f || this.XRayGlyphCost == 0.6f))
			{
				this.XRayGlyphCost = 0.8f;
			}
			if (value < 2 && this.XRayRectLimit == 500)
			{
				this.XRayRectLimit = 1200;
			}
			if (value < 3 && (this.XRayRectLimit == 500 || this.XRayRectLimit == 1200))
			{
				this.XRayRectLimit = 1500;
			}
			if (value < 4 && this.XRayMaxEntries == 80)
			{
				this.XRayMaxEntries = 300;
			}
			this.Save();
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x00143678 File Offset: 0x00141878
		public void Save()
		{
			try
			{
				SafeFile.Write(this._path, string.Format("[Meta]\r\nVersion=4\r\n\r\n[BulletImpact]\r\nEnabled={0}\r\n\r\n[Debug]\r\n; True = also write the per-frame DIAG lines to BulletImpact.log (hits, kill shots and crippling are always logged).\r\nLogHits={1}\r\n\r\n[Wounds]\r\n; Shattered spine / pelvis / knee / leg bones put people down for good (they crawl), a broken neck\r\n; paralyzes, a shattered foot leaves a limp. Decided from each hit's X-ray.\r\nCrippling={2}\r\n; Your weapon damage against everyone, 1.0 = vanilla. Lower = people soak more hits before dying.\r\nPlayerDamage={3}\r\n; A shot whose X-ray destroys the brain or pierces the heart kills outright, whatever the damage.\r\nKillShots={4}\r\n\r\n[XRay]\r\n; X-ray log (F11, then Tab): the newest MaxEntries people and the newest MaxEntries animals are kept in enRAGE\\xray_log.json.\r\nMaxEntries={5}\r\n; Detail: Auto (best that fits the rect limit) / High / Medium / Low.\r\nDetail={6}\r\n; The game's per-frame limit on script-drawn rects (it silently drops the rest). The X-ray uses\r\n; whatever the menu leaves under it. Measure it with the calibration screen: K in the X-ray log.\r\nRectLimit={7}\r\n; Every letter of text on screen seems to come out of the same per-frame limit (the X-ray report's text left the skeleton\r\n; half drawn). The X-ray budget takes this many units off per drawn letter. Raise it if bones still go missing.\r\nGlyphCost={8}\r\n", new object[]
				{
					this.Enabled,
					this.LogHits,
					this.Crippling,
					this.PlayerDamage.ToString(CultureInfo.InvariantCulture),
					this.KillShots,
					this.XRayMaxEntries,
					this.XRayQuality,
					this.XRayRectLimit,
					this.XRayGlyphCost.ToString(CultureInfo.InvariantCulture)
				}), false);
			}
			catch
			{
			}
		}

		// Token: 0x04000DD9 RID: 3545
		private readonly string _path;

		// Token: 0x04000DDA RID: 3546
		public bool Enabled = true;

		// Token: 0x04000DDB RID: 3547
		public bool LogHits;

		// Token: 0x04000DDC RID: 3548
		public bool Crippling = true;

		// Token: 0x04000DDD RID: 3549
		public float PlayerDamage = 0.6f;

		// Token: 0x04000DDE RID: 3550
		public bool KillShots = true;

		// Token: 0x04000DDF RID: 3551
		public int XRayMaxEntries = 300;

		// Token: 0x04000DE0 RID: 3552
		public XRayQuality XRayQuality = XRayQuality.Auto;

		// Token: 0x04000DE1 RID: 3553
		public int XRayRectLimit = 1500;

		// Token: 0x04000DE2 RID: 3554
		public float XRayGlyphCost = 0.8f;
	}
}
