using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace NemesisSystem
{
	// Token: 0x02000057 RID: 87
	public class NemesisRecord
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000E50 RID: 3664 RVA: 0x0010CCC9 File Offset: 0x0010AEC9
		// (set) Token: 0x06000E51 RID: 3665 RVA: 0x0010CCD1 File Offset: 0x0010AED1
		public string Id { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0010CCDA File Offset: 0x0010AEDA
		// (set) Token: 0x06000E53 RID: 3667 RVA: 0x0010CCE2 File Offset: 0x0010AEE2
		public string Name { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0010CCEB File Offset: 0x0010AEEB
		// (set) Token: 0x06000E55 RID: 3669 RVA: 0x0010CCF3 File Offset: 0x0010AEF3
		public string Title { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0010CCFC File Offset: 0x0010AEFC
		[JsonIgnore]
		public string FullName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.Title))
				{
					return this.Name + " " + this.Title;
				}
				return this.Name;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0010CD28 File Offset: 0x0010AF28
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x0010CD30 File Offset: 0x0010AF30
		public int Rank { get; set; } = 1;

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0010CD39 File Offset: 0x0010AF39
		// (set) Token: 0x06000E5A RID: 3674 RVA: 0x0010CD41 File Offset: 0x0010AF41
		public int RankXp { get; set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x0010CD4A File Offset: 0x0010AF4A
		// (set) Token: 0x06000E5C RID: 3676 RVA: 0x0010CD52 File Offset: 0x0010AF52
		public uint ModelHash { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x0010CD5B File Offset: 0x0010AF5B
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x0010CD63 File Offset: 0x0010AF63
		public uint OutfitHash { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0010CD6C File Offset: 0x0010AF6C
		// (set) Token: 0x06000E60 RID: 3680 RVA: 0x0010CD74 File Offset: 0x0010AF74
		[JsonIgnore]
		public bool Story { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x0010CD7D File Offset: 0x0010AF7D
		// (set) Token: 0x06000E62 RID: 3682 RVA: 0x0010CD85 File Offset: 0x0010AF85
		[JsonIgnore]
		public bool LeaderPage { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000E63 RID: 3683 RVA: 0x0010CD8E File Offset: 0x0010AF8E
		// (set) Token: 0x06000E64 RID: 3684 RVA: 0x0010CD96 File Offset: 0x0010AF96
		[JsonIgnore]
		public bool Person { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0010CD9F File Offset: 0x0010AF9F
		// (set) Token: 0x06000E66 RID: 3686 RVA: 0x0010CDA7 File Offset: 0x0010AFA7
		[JsonIgnore]
		internal Acquaintance PersonAcq { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000E67 RID: 3687 RVA: 0x0010CDB0 File Offset: 0x0010AFB0
		// (set) Token: 0x06000E68 RID: 3688 RVA: 0x0010CDB8 File Offset: 0x0010AFB8
		public bool CampUnsure { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000E69 RID: 3689 RVA: 0x0010CDC1 File Offset: 0x0010AFC1
		// (set) Token: 0x06000E6A RID: 3690 RVA: 0x0010CDC9 File Offset: 0x0010AFC9
		public int LiedToYou { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000E6B RID: 3691 RVA: 0x0010CDD2 File Offset: 0x0010AFD2
		// (set) Token: 0x06000E6C RID: 3692 RVA: 0x0010CDDA File Offset: 0x0010AFDA
		public bool Informant { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000E6D RID: 3693 RVA: 0x0010CDE3 File Offset: 0x0010AFE3
		// (set) Token: 0x06000E6E RID: 3694 RVA: 0x0010CDEB File Offset: 0x0010AFEB
		public int InformedDay { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000E6F RID: 3695 RVA: 0x0010CDF4 File Offset: 0x0010AFF4
		// (set) Token: 0x06000E70 RID: 3696 RVA: 0x0010CDFC File Offset: 0x0010AFFC
		public bool OwesYou { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000E71 RID: 3697 RVA: 0x0010CE05 File Offset: 0x0010B005
		// (set) Token: 0x06000E72 RID: 3698 RVA: 0x0010CE0D File Offset: 0x0010B00D
		public List<string> Quirks { get; set; } = new List<string>();

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000E73 RID: 3699 RVA: 0x0010CE16 File Offset: 0x0010B016
		// (set) Token: 0x06000E74 RID: 3700 RVA: 0x0010CE1E File Offset: 0x0010B01E
		public bool QuirksRolled { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0010CE27 File Offset: 0x0010B027
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x0010CE2F File Offset: 0x0010B02F
		public List<string> Skills { get; set; } = new List<string>();

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0010CE38 File Offset: 0x0010B038
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x0010CE40 File Offset: 0x0010B040
		public bool SkillsV2 { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0010CE49 File Offset: 0x0010B049
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x0010CE51 File Offset: 0x0010B051
		public int SkillTierRolled { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x0010CE5A File Offset: 0x0010B05A
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x0010CE62 File Offset: 0x0010B062
		public string LastOutcome { get; set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x0010CE6B File Offset: 0x0010B06B
		// (set) Token: 0x06000E7E RID: 3710 RVA: 0x0010CE73 File Offset: 0x0010B073
		public int ArcStep { get; set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0010CE7C File Offset: 0x0010B07C
		// (set) Token: 0x06000E80 RID: 3712 RVA: 0x0010CE84 File Offset: 0x0010B084
		public long SocialDoneAt { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0010CE8D File Offset: 0x0010B08D
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x0010CE95 File Offset: 0x0010B095
		public long BargainAt { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0010CE9E File Offset: 0x0010B09E
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x0010CEA6 File Offset: 0x0010B0A6
		public int IntelGiven { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x0010CEAF File Offset: 0x0010B0AF
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x0010CEB7 File Offset: 0x0010B0B7
		public List<string> Asked { get; set; } = new List<string>();

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x0010CEC0 File Offset: 0x0010B0C0
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x0010CEC8 File Offset: 0x0010B0C8
		public string KillLocation { get; set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000E89 RID: 3721 RVA: 0x0010CED1 File Offset: 0x0010B0D1
		// (set) Token: 0x06000E8A RID: 3722 RVA: 0x0010CED9 File Offset: 0x0010B0D9
		public DateTime KilledPlayerAt { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000E8B RID: 3723 RVA: 0x0010CEE2 File Offset: 0x0010B0E2
		// (set) Token: 0x06000E8C RID: 3724 RVA: 0x0010CEEA File Offset: 0x0010B0EA
		public long RestUntil { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000E8D RID: 3725 RVA: 0x0010CEF3 File Offset: 0x0010B0F3
		// (set) Token: 0x06000E8E RID: 3726 RVA: 0x0010CEFB File Offset: 0x0010B0FB
		public long LastCameAt { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0010CF04 File Offset: 0x0010B104
		// (set) Token: 0x06000E90 RID: 3728 RVA: 0x0010CF0C File Offset: 0x0010B10C
		public DateTime? LastInteraction { get; set; }

		// Token: 0x06000E91 RID: 3729 RVA: 0x0010CF15 File Offset: 0x0010B115
		public void Touch()
		{
			this.LastInteraction = new DateTime?(DateTime.Now);
			if (NemesisRecord.GameClock != null)
			{
				this.LastGameMin = NemesisRecord.GameClock();
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0010CF3E File Offset: 0x0010B13E
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x0010CF46 File Offset: 0x0010B146
		public long LastGameMin { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000E94 RID: 3732 RVA: 0x0010CF4F File Offset: 0x0010B14F
		// (set) Token: 0x06000E95 RID: 3733 RVA: 0x0010CF57 File Offset: 0x0010B157
		public List<HistoryEntry> History { get; set; } = new List<HistoryEntry>();

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0010CF60 File Offset: 0x0010B160
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x0010CF68 File Offset: 0x0010B168
		public List<Thought> Thoughts { get; set; } = new List<Thought>();

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000E98 RID: 3736 RVA: 0x0010CF74 File Offset: 0x0010B174
		[JsonIgnore]
		public DateTime LastDealt
		{
			get
			{
				DateTime? lastInteraction = this.LastInteraction;
				if (lastInteraction == null)
				{
					return this.KilledPlayerAt;
				}
				return lastInteraction.GetValueOrDefault();
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000E99 RID: 3737 RVA: 0x0010CF9F File Offset: 0x0010B19F
		// (set) Token: 0x06000E9A RID: 3738 RVA: 0x0010CFA7 File Offset: 0x0010B1A7
		public bool IsAlive { get; set; } = true;

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000E9B RID: 3739 RVA: 0x0010CFB0 File Offset: 0x0010B1B0
		// (set) Token: 0x06000E9C RID: 3740 RVA: 0x0010CFB8 File Offset: 0x0010B1B8
		public bool KilledByPlayer { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000E9D RID: 3741 RVA: 0x0010CFC1 File Offset: 0x0010B1C1
		// (set) Token: 0x06000E9E RID: 3742 RVA: 0x0010CFC9 File Offset: 0x0010B1C9
		public int TimesKilledPlayer { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000E9F RID: 3743 RVA: 0x0010CFD2 File Offset: 0x0010B1D2
		// (set) Token: 0x06000EA0 RID: 3744 RVA: 0x0010CFDA File Offset: 0x0010B1DA
		public int TimesAntagonized { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x0010CFE3 File Offset: 0x0010B1E3
		// (set) Token: 0x06000EA2 RID: 3746 RVA: 0x0010CFEB File Offset: 0x0010B1EB
		public int TimesAmbushed { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000EA3 RID: 3747 RVA: 0x0010CFF4 File Offset: 0x0010B1F4
		// (set) Token: 0x06000EA4 RID: 3748 RVA: 0x0010CFFC File Offset: 0x0010B1FC
		public int TimesKnockedOutByPlayer { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000EA5 RID: 3749 RVA: 0x0010D005 File Offset: 0x0010B205
		// (set) Token: 0x06000EA6 RID: 3750 RVA: 0x0010D00D File Offset: 0x0010B20D
		public bool Limp { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x0010D016 File Offset: 0x0010B216
		// (set) Token: 0x06000EA8 RID: 3752 RVA: 0x0010D01E File Offset: 0x0010B21E
		public bool Paralyzed { get; set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x0010D027 File Offset: 0x0010B227
		// (set) Token: 0x06000EAA RID: 3754 RVA: 0x0010D02F File Offset: 0x0010B22F
		public string Gang { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x0010D038 File Offset: 0x0010B238
		// (set) Token: 0x06000EAC RID: 3756 RVA: 0x0010D040 File Offset: 0x0010B240
		public string PlainModel { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000EAD RID: 3757 RVA: 0x0010D049 File Offset: 0x0010B249
		// (set) Token: 0x06000EAE RID: 3758 RVA: 0x0010D051 File Offset: 0x0010B251
		public bool CoverBlown { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000EAF RID: 3759 RVA: 0x0010D05A File Offset: 0x0010B25A
		// (set) Token: 0x06000EB0 RID: 3760 RVA: 0x0010D062 File Offset: 0x0010B262
		public string LoreModel { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000EB1 RID: 3761 RVA: 0x0010D06B File Offset: 0x0010B26B
		// (set) Token: 0x06000EB2 RID: 3762 RVA: 0x0010D073 File Offset: 0x0010B273
		public int PlainGreets { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000EB3 RID: 3763 RVA: 0x0010D07C File Offset: 0x0010B27C
		// (set) Token: 0x06000EB4 RID: 3764 RVA: 0x0010D084 File Offset: 0x0010B284
		public string Affiliation { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000EB5 RID: 3765 RVA: 0x0010D08D File Offset: 0x0010B28D
		// (set) Token: 0x06000EB6 RID: 3766 RVA: 0x0010D095 File Offset: 0x0010B295
		public bool SeedMember { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x0010D09E File Offset: 0x0010B29E
		// (set) Token: 0x06000EB8 RID: 3768 RVA: 0x0010D0A6 File Offset: 0x0010B2A6
		public string FormerGang { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x0010D0AF File Offset: 0x0010B2AF
		// (set) Token: 0x06000EBA RID: 3770 RVA: 0x0010D0B7 File Offset: 0x0010B2B7
		public string CampId { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0010D0C0 File Offset: 0x0010B2C0
		// (set) Token: 0x06000EBC RID: 3772 RVA: 0x0010D0C8 File Offset: 0x0010B2C8
		public string BossId { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000EBD RID: 3773 RVA: 0x0010D0D1 File Offset: 0x0010B2D1
		// (set) Token: 0x06000EBE RID: 3774 RVA: 0x0010D0D9 File Offset: 0x0010B2D9
		public bool GaveCamp { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000EBF RID: 3775 RVA: 0x0010D0E2 File Offset: 0x0010B2E2
		// (set) Token: 0x06000EC0 RID: 3776 RVA: 0x0010D0EA File Offset: 0x0010B2EA
		public bool GaveKin { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000EC1 RID: 3777 RVA: 0x0010D0F3 File Offset: 0x0010B2F3
		// (set) Token: 0x06000EC2 RID: 3778 RVA: 0x0010D0FB File Offset: 0x0010B2FB
		public bool CampKnown { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000EC3 RID: 3779 RVA: 0x0010D104 File Offset: 0x0010B304
		// (set) Token: 0x06000EC4 RID: 3780 RVA: 0x0010D10C File Offset: 0x0010B30C
		public int TimesInterrogated { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x0010D115 File Offset: 0x0010B315
		// (set) Token: 0x06000EC6 RID: 3782 RVA: 0x0010D11D File Offset: 0x0010B31D
		public bool HasOrigin { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000EC7 RID: 3783 RVA: 0x0010D126 File Offset: 0x0010B326
		// (set) Token: 0x06000EC8 RID: 3784 RVA: 0x0010D12E File Offset: 0x0010B32E
		public float OriginX { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x0010D137 File Offset: 0x0010B337
		// (set) Token: 0x06000ECA RID: 3786 RVA: 0x0010D13F File Offset: 0x0010B33F
		public float OriginY { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x0010D148 File Offset: 0x0010B348
		// (set) Token: 0x06000ECC RID: 3788 RVA: 0x0010D150 File Offset: 0x0010B350
		public float OriginZ { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x0010D159 File Offset: 0x0010B359
		[JsonIgnore]
		public bool CanHunt
		{
			get
			{
				return this.IsAlive && !this.Paralyzed;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x0010D170 File Offset: 0x0010B370
		// (set) Token: 0x06000ECF RID: 3791 RVA: 0x0010D1C3 File Offset: 0x0010B3C3
		[JsonIgnore]
		public int Hate
		{
			get
			{
				if (this._hate < 0 || this.Gang == null)
				{
					return this._hate;
				}
				Gang gang = Gangs.ById(this.Gang);
				if (gang == null || !gang.OrdersNotFeelings)
				{
					return this._hate;
				}
				return Math.Max(gang.FixedHate, NemesisSystem.Hate.MurderousAt);
			}
			set
			{
				this._hate = value;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x0010D1CC File Offset: 0x0010B3CC
		// (set) Token: 0x06000ED1 RID: 3793 RVA: 0x0010D1D4 File Offset: 0x0010B3D4
		[JsonProperty("Hate")]
		private int RawHate
		{
			get
			{
				return this._hate;
			}
			set
			{
				this._hate = value;
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x0010D1DD File Offset: 0x0010B3DD
		[JsonIgnore]
		public int FeltHate
		{
			get
			{
				return this._hate;
			}
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0010D1E5 File Offset: 0x0010B3E5
		// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x0010D1ED File Offset: 0x0010B3ED
		[JsonConverter(typeof(StringEnumConverter))]
		public Personality? Personality { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0010D1F6 File Offset: 0x0010B3F6
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x0010D1FE File Offset: 0x0010B3FE
		public bool PersonalityKnown { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x0010D207 File Offset: 0x0010B407
		[JsonIgnore]
		public bool TemperKnown
		{
			get
			{
				return this.PersonalityKnown || this.TimesAmbushed > 0 || this.TimesInterrogated > 0;
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x0010D225 File Offset: 0x0010B425
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x0010D22D File Offset: 0x0010B42D
		public bool TraitsRolled { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0010D236 File Offset: 0x0010B436
		// (set) Token: 0x06000EDB RID: 3803 RVA: 0x0010D23E File Offset: 0x0010B43E
		public int Aggression { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x0010D247 File Offset: 0x0010B447
		// (set) Token: 0x06000EDD RID: 3805 RVA: 0x0010D24F File Offset: 0x0010B44F
		[JsonConverter(typeof(StringEnumConverter))]
		public Sociability Sociability { get; set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000EDE RID: 3806 RVA: 0x0010D258 File Offset: 0x0010B458
		// (set) Token: 0x06000EDF RID: 3807 RVA: 0x0010D260 File Offset: 0x0010B460
		[JsonConverter(typeof(StringEnumConverter))]
		public Disposition Disposition { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0010D269 File Offset: 0x0010B469
		// (set) Token: 0x06000EE1 RID: 3809 RVA: 0x0010D271 File Offset: 0x0010B471
		[JsonConverter(typeof(StringEnumConverter))]
		public Smarts Smarts { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000EE2 RID: 3810 RVA: 0x0010D27A File Offset: 0x0010B47A
		// (set) Token: 0x06000EE3 RID: 3811 RVA: 0x0010D282 File Offset: 0x0010B482
		[JsonConverter(typeof(StringEnumConverter))]
		public Wealth Wealth { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000EE4 RID: 3812 RVA: 0x0010D28B File Offset: 0x0010B48B
		// (set) Token: 0x06000EE5 RID: 3813 RVA: 0x0010D293 File Offset: 0x0010B493
		[JsonConverter(typeof(StringEnumConverter))]
		public Devotion Devotion { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000EE6 RID: 3814 RVA: 0x0010D29C File Offset: 0x0010B49C
		// (set) Token: 0x06000EE7 RID: 3815 RVA: 0x0010D2A4 File Offset: 0x0010B4A4
		public string Trade { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000EE8 RID: 3816 RVA: 0x0010D2AD File Offset: 0x0010B4AD
		// (set) Token: 0x06000EE9 RID: 3817 RVA: 0x0010D2B5 File Offset: 0x0010B4B5
		public string Faith { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0010D2BE File Offset: 0x0010B4BE
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x0010D2C6 File Offset: 0x0010B4C6
		public string Family { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x0010D2CF File Offset: 0x0010B4CF
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x0010D2D7 File Offset: 0x0010B4D7
		public int Children { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0010D2E0 File Offset: 0x0010B4E0
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x0010D2E8 File Offset: 0x0010B4E8
		public string KinId { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x0010D2F1 File Offset: 0x0010B4F1
		// (set) Token: 0x06000EF1 RID: 3825 RVA: 0x0010D2F9 File Offset: 0x0010B4F9
		public string KinRelation { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x0010D302 File Offset: 0x0010B502
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x0010D30A File Offset: 0x0010B50A
		public List<string> FriendIds { get; set; } = new List<string>();

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0010D313 File Offset: 0x0010B513
		// (set) Token: 0x06000EF5 RID: 3829 RVA: 0x0010D31B File Offset: 0x0010B51B
		public string FormerAcqId { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000EF6 RID: 3830 RVA: 0x0010D324 File Offset: 0x0010B524
		// (set) Token: 0x06000EF7 RID: 3831 RVA: 0x0010D32C File Offset: 0x0010B52C
		public List<string> CircleAcq { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000EF8 RID: 3832 RVA: 0x0010D335 File Offset: 0x0010B535
		// (set) Token: 0x06000EF9 RID: 3833 RVA: 0x0010D33D File Offset: 0x0010B53D
		public bool FriendsRolled { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x0010D346 File Offset: 0x0010B546
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x0010D34E File Offset: 0x0010B54E
		public string Pet { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x0010D357 File Offset: 0x0010B557
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x0010D35F File Offset: 0x0010B55F
		public List<PastEntry> Past { get; set; } = new List<PastEntry>();

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x0010D368 File Offset: 0x0010B568
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x0010D370 File Offset: 0x0010B570
		public int BornYear { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x0010D379 File Offset: 0x0010B579
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x0010D381 File Offset: 0x0010B581
		public string Birthplace { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x0010D38A File Offset: 0x0010B58A
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x0010D392 File Offset: 0x0010B592
		public bool PastRolled { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x0010D39B File Offset: 0x0010B59B
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x0010D3A3 File Offset: 0x0010B5A3
		public bool Woven { get; set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x0010D3AC File Offset: 0x0010B5AC
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x0010D3B4 File Offset: 0x0010B5B4
		public Dictionary<string, int> Facets { get; set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x0010D3BD File Offset: 0x0010B5BD
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x0010D3C5 File Offset: 0x0010B5C5
		public Dictionary<string, int> Values { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x0010D3CE File Offset: 0x0010B5CE
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x0010D3D6 File Offset: 0x0010B5D6
		public Dictionary<string, int> Needs { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x0010D3DF File Offset: 0x0010B5DF
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x0010D3E7 File Offset: 0x0010B5E7
		public int Stress { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x0010D3F0 File Offset: 0x0010B5F0
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x0010D3F8 File Offset: 0x0010B5F8
		public List<Memory> Memories { get; set; } = new List<Memory>();

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x0010D401 File Offset: 0x0010B601
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x0010D409 File Offset: 0x0010B609
		public List<Tie> Ties { get; set; } = new List<Tie>();

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x0010D412 File Offset: 0x0010B612
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x0010D41A File Offset: 0x0010B61A
		public List<Goal> Goals { get; set; } = new List<Goal>();

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x0010D423 File Offset: 0x0010B623
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x0010D42B File Offset: 0x0010B62B
		public bool MindRolled { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x0010D434 File Offset: 0x0010B634
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x0010D43C File Offset: 0x0010B63C
		public int LifeRules { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x0010D445 File Offset: 0x0010B645
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x0010D44D File Offset: 0x0010B64D
		public List<string> LifeFx { get; set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x0010D456 File Offset: 0x0010B656
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x0010D45E File Offset: 0x0010B65E
		public bool WantsKnown { get; set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000F1C RID: 3868 RVA: 0x0010D467 File Offset: 0x0010B667
		// (set) Token: 0x06000F1D RID: 3869 RVA: 0x0010D46F File Offset: 0x0010B66F
		public bool GoalsSet { get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000F1E RID: 3870 RVA: 0x0010D478 File Offset: 0x0010B678
		// (set) Token: 0x06000F1F RID: 3871 RVA: 0x0010D480 File Offset: 0x0010B680
		public int Fear { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000F20 RID: 3872 RVA: 0x0010D489 File Offset: 0x0010B689
		// (set) Token: 0x06000F21 RID: 3873 RVA: 0x0010D491 File Offset: 0x0010B691
		public int YourNotoriety { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x0010D49A File Offset: 0x0010B69A
		// (set) Token: 0x06000F23 RID: 3875 RVA: 0x0010D4A2 File Offset: 0x0010B6A2
		public int TimesRobbedPlayer { get; set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000F24 RID: 3876 RVA: 0x0010D4AB File Offset: 0x0010B6AB
		// (set) Token: 0x06000F25 RID: 3877 RVA: 0x0010D4B3 File Offset: 0x0010B6B3
		public int TimesSpared { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000F26 RID: 3878 RVA: 0x0010D4BC File Offset: 0x0010B6BC
		// (set) Token: 0x06000F27 RID: 3879 RVA: 0x0010D4C4 File Offset: 0x0010B6C4
		public int TimesHogtied { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000F28 RID: 3880 RVA: 0x0010D4D0 File Offset: 0x0010B6D0
		[JsonIgnore]
		public float HateGainMult
		{
			get
			{
				return Math.Max(Balance.GainClampMin, Math.Min(Balance.GainClampMax, ((this.Personality != null) ? Personalities.GainMult(this.Personality.Value) : 1f) * Traits.HateGain(this) * Mind.HateGain(this)));
			}
		}

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000F29 RID: 3881 RVA: 0x0010D529 File Offset: 0x0010B729
		[JsonIgnore]
		public float FearGainMult
		{
			get
			{
				return Math.Max(Balance.GainClampMin, Math.Min(Balance.GainClampMax, Traits.FearGain(this) * Mind.FearGain(this)));
			}
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x0010D54C File Offset: 0x0010B74C
		public void AddFear(int delta)
		{
			if (delta > 0)
			{
				delta = Math.Max(1, (int)Math.Round((double)((float)delta * this.FearGainMult * Balance.FearGainScale)));
			}
			this.Fear = Math.Max(0, Math.Min(100, this.Fear + delta));
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x0010D58B File Offset: 0x0010B78B
		public void AddYourNotoriety(int delta)
		{
			this.YourNotoriety = Math.Max(0, Math.Min(100, this.YourNotoriety + (int)Math.Round((double)((float)delta * Balance.NotorietyGainScale))));
		}

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000F2C RID: 3884 RVA: 0x0010D5B6 File Offset: 0x0010B7B6
		// (set) Token: 0x06000F2D RID: 3885 RVA: 0x0010D5BE File Offset: 0x0010B7BE
		public bool PresumedDead { get; set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x06000F2E RID: 3886 RVA: 0x0010D5C7 File Offset: 0x0010B7C7
		// (set) Token: 0x06000F2F RID: 3887 RVA: 0x0010D5CF File Offset: 0x0010B7CF
		public int TimesCheatedDeath { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x06000F30 RID: 3888 RVA: 0x0010D5D8 File Offset: 0x0010B7D8
		// (set) Token: 0x06000F31 RID: 3889 RVA: 0x0010D5E0 File Offset: 0x0010B7E0
		public string LastDefeatLocation { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000F32 RID: 3890 RVA: 0x0010D5E9 File Offset: 0x0010B7E9
		// (set) Token: 0x06000F33 RID: 3891 RVA: 0x0010D5F1 File Offset: 0x0010B7F1
		public List<Scar> Scars { get; set; } = new List<Scar>();

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0010D5FA File Offset: 0x0010B7FA
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x0010D602 File Offset: 0x0010B802
		[JsonConverter(typeof(StringEnumConverter))]
		public GrudgeOrigin Origin { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x0010D60B File Offset: 0x0010B80B
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x0010D613 File Offset: 0x0010B813
		public int TimesGreeted { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x0010D61C File Offset: 0x0010B81C
		// (set) Token: 0x06000F39 RID: 3897 RVA: 0x0010D624 File Offset: 0x0010B824
		public int TimesRobbed { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x0010D62D File Offset: 0x0010B82D
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x0010D635 File Offset: 0x0010B835
		public int Fury { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x0010D63E File Offset: 0x0010B83E
		// (set) Token: 0x06000F3D RID: 3901 RVA: 0x0010D646 File Offset: 0x0010B846
		public int LastHandle { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x0010D64F File Offset: 0x0010B84F
		// (set) Token: 0x06000F3F RID: 3903 RVA: 0x0010D657 File Offset: 0x0010B857
		public uint LastHandleModel { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x0010D660 File Offset: 0x0010B860
		// (set) Token: 0x06000F41 RID: 3905 RVA: 0x0010D668 File Offset: 0x0010B868
		public DateTime? LastHandleUtc { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000F42 RID: 3906 RVA: 0x0010D671 File Offset: 0x0010B871
		// (set) Token: 0x06000F43 RID: 3907 RVA: 0x0010D679 File Offset: 0x0010B879
		public List<Habit> Habits { get; set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0010D682 File Offset: 0x0010B882
		// (set) Token: 0x06000F45 RID: 3909 RVA: 0x0010D68A File Offset: 0x0010B88A
		public bool Buried { get; set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000F46 RID: 3910 RVA: 0x0010D693 File Offset: 0x0010B893
		// (set) Token: 0x06000F47 RID: 3911 RVA: 0x0010D69B File Offset: 0x0010B89B
		public string Fate { get; set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000F48 RID: 3912 RVA: 0x0010D6A4 File Offset: 0x0010B8A4
		public string Status
		{
			get
			{
				if (!this.IsAlive)
				{
					string result;
					if ((result = this.StoryGone) == null)
					{
						result = (this.Fate ?? "Muerto");
					}
					return result;
				}
				if (!this.PresumedDead)
				{
					if (!this.Paralyzed)
					{
						return "Vivo";
					}
					return "Paralítico";
				}
				else
				{
					if (!NemesisRecord.ShowSecrets)
					{
						return "Muerto";
					}
					return "Dado por muerto";
				}
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0010D701 File Offset: 0x0010B901
		// (set) Token: 0x06000F4A RID: 3914 RVA: 0x0010D709 File Offset: 0x0010B909
		public long PresumedDeadAt { get; set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000F4B RID: 3915 RVA: 0x0010D712 File Offset: 0x0010B912
		[JsonIgnore]
		public HateTier HateTier
		{
			get
			{
				return NemesisSystem.Hate.TierOf(this.Hate);
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000F4C RID: 3916 RVA: 0x0010D71F File Offset: 0x0010B91F
		[JsonIgnore]
		public bool ScarredByYou
		{
			get
			{
				if (this.Scars != null)
				{
					return this.Scars.Any((Scar s) => s.FromYou.GetValueOrDefault() || (s.FromYou == null && s.Wound != null));
				}
				return false;
			}
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0010D758 File Offset: 0x0010B958
		public void EnsureId()
		{
			if (string.IsNullOrEmpty(this.Id))
			{
				this.Id = Guid.NewGuid().ToString("N").Substring(0, 12);
			}
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x0010D794 File Offset: 0x0010B994
		public void AddHate(int delta)
		{
			if (delta > 0)
			{
				delta = Math.Max(1, (int)Math.Round((double)((float)delta * this.HateGainMult * Balance.HateGainScale)));
				int num = Math.Max(0, this._hate);
				if (num >= 80)
				{
					delta = Math.Max(1, (int)Math.Round((double)((float)delta * (1f - (float)num / 120f))));
				}
			}
			this._hate = Math.Max(0, Math.Min(100, Math.Max(0, this._hate) + delta));
		}

		// Token: 0x06000F4F RID: 3919 RVA: 0x0010D818 File Offset: 0x0010BA18
		public void EnsureHate()
		{
			if (this.Hate >= 0)
			{
				return;
			}
			this.Hate = NemesisSystem.Hate.Initial(this.Origin);
			if (this.TimesKilledPlayer > 0)
			{
				this.Hate = Math.Max(this.Hate, NemesisSystem.Hate.MurderousAt);
			}
			this.AddHate(10 * Math.Max(0, this.TimesKilledPlayer - 1) + 25 * this.TimesCheatedDeath);
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0010D880 File Offset: 0x0010BA80
		[JsonIgnore]
		public string GrudgeSummary
		{
			get
			{
				switch (this.Origin)
				{
				case GrudgeOrigin.Greeted:
					return "lo saludaste en " + this.KillLocation + "; él te disparó";
				case GrudgeOrigin.Insulted:
					return "le faltaste al respeto en " + this.KillLocation;
				case GrudgeOrigin.Threatened:
					return "lo encañonaste en " + this.KillLocation;
				case GrudgeOrigin.Gunfight:
					return "sobrevivió a un tiroteo contigo en " + this.KillLocation;
				case GrudgeOrigin.Beaten:
					return "le diste una paliza sangrienta en " + this.KillLocation;
				case GrudgeOrigin.KnockedOut:
					return "lo noqueaste en " + this.KillLocation;
				case GrudgeOrigin.Robbed:
					return "le robaste en " + this.KillLocation;
				case GrudgeOrigin.FriendKilled:
					return "mataste a su amigo delante de él en " + this.KillLocation;
				case GrudgeOrigin.Kidnapped:
					return "lo ataste y te lo llevaste en " + this.KillLocation;
				case GrudgeOrigin.WagonStolen:
					return "robaste su carro en " + this.KillLocation;
				case GrudgeOrigin.HorseStolen:
					return "robaste su caballo en " + this.KillLocation;
				case GrudgeOrigin.HorseKilled:
					return "mataste a su caballo en " + this.KillLocation;
				case GrudgeOrigin.Slashed:
					return "lo cortaste en " + this.KillLocation + " y lo dejaste sangrando";
				case GrudgeOrigin.Wounded:
					return "le disparaste en " + this.KillLocation + " y lo dejaste respirando";
				case GrudgeOrigin.GangMember:
				{
					string str = "cabalga con ";
					Gang gang = Gangs.ById(this.Gang);
					return str + (((gang != null) ? gang.Name : null) ?? "gang") + "; conoce tu nombre";
				}
				default:
					return "te mató en " + this.KillLocation;
				}
			}
		}

		// Token: 0x04000A7E RID: 2686
		[JsonIgnore]
		public static Func<long> GameClock;

		// Token: 0x04000A81 RID: 2689
		public const int HistoryMax = 60;

		// Token: 0x04000A9C RID: 2716
		private int _hate = -1;

		// Token: 0x04000ACF RID: 2767
		[JsonIgnore]
		public string StoryGone;

		// Token: 0x04000AD6 RID: 2774
		[JsonIgnore]
		public static bool ShowSecrets;
	}
}
