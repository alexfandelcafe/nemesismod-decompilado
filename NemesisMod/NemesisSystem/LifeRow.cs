using System;

namespace NemesisSystem
{
	// Token: 0x02000008 RID: 8
	public class LifeRow
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002543 File Offset: 0x00000743
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000254B File Offset: 0x0000074B
		public string Id { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002554 File Offset: 0x00000754
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000255C File Offset: 0x0000075C
		public string Kind { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002565 File Offset: 0x00000765
		// (set) Token: 0x06000027 RID: 39 RVA: 0x0000256D File Offset: 0x0000076D
		public string Who { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002576 File Offset: 0x00000776
		// (set) Token: 0x06000029 RID: 41 RVA: 0x0000257E File Offset: 0x0000077E
		public int MinAge { get; set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002587 File Offset: 0x00000787
		// (set) Token: 0x0600002B RID: 43 RVA: 0x0000258F File Offset: 0x0000078F
		public int MaxAge { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002598 File Offset: 0x00000798
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000025A0 File Offset: 0x000007A0
		public int Weight { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000025A9 File Offset: 0x000007A9
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000025B1 File Offset: 0x000007B1
		public string Text { get; set; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000025BA File Offset: 0x000007BA
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000025C2 File Offset: 0x000007C2
		public string Tilt { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000025CB File Offset: 0x000007CB
		// (set) Token: 0x06000033 RID: 51 RVA: 0x000025D3 File Offset: 0x000007D3
		public string Fx { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000025DC File Offset: 0x000007DC
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000025E4 File Offset: 0x000007E4
		public bool Public { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000025ED File Offset: 0x000007ED
		// (set) Token: 0x06000037 RID: 55 RVA: 0x000025F5 File Offset: 0x000007F5
		public int BornFrom { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000025FE File Offset: 0x000007FE
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002606 File Offset: 0x00000806
		public int BornTo { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003A RID: 58 RVA: 0x0000260F File Offset: 0x0000080F
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002617 File Offset: 0x00000817
		public string BornIn { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002620 File Offset: 0x00000820
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002628 File Offset: 0x00000828
		public string MirrorKind { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002631 File Offset: 0x00000831
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002639 File Offset: 0x00000839
		public string MirrorTitle { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002642 File Offset: 0x00000842
		// (set) Token: 0x06000041 RID: 65 RVA: 0x0000264A File Offset: 0x0000084A
		public bool MirrorHeard { get; set; }
	}
}
