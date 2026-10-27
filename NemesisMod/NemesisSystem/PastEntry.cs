using System;
using Newtonsoft.Json;

namespace NemesisSystem
{
	// Token: 0x02000005 RID: 5
	public class PastEntry
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002393 File Offset: 0x00000593
		// (set) Token: 0x06000008 RID: 8 RVA: 0x0000239B File Offset: 0x0000059B
		public int Year { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000023A4 File Offset: 0x000005A4
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000023AC File Offset: 0x000005AC
		public int Month { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000023B5 File Offset: 0x000005B5
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000023BD File Offset: 0x000005BD
		public string Text { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000023C6 File Offset: 0x000005C6
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000023CE File Offset: 0x000005CE
		public string Kind { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000023D7 File Offset: 0x000005D7
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000023DF File Offset: 0x000005DF
		public string WorldId { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000023E8 File Offset: 0x000005E8
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000023F0 File Offset: 0x000005F0
		public bool Public { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000023F9 File Offset: 0x000005F9
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00002401 File Offset: 0x00000601
		public bool Known { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x0000240A File Offset: 0x0000060A
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002412 File Offset: 0x00000612
		public string MirrorKind { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x0000241B File Offset: 0x0000061B
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002423 File Offset: 0x00000623
		public string MirrorTitle { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000242C File Offset: 0x0000062C
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00002434 File Offset: 0x00000634
		public string MirrorPlace { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000243D File Offset: 0x0000063D
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00002445 File Offset: 0x00000645
		public bool MirrorHeard { get; set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002450 File Offset: 0x00000650
		[JsonIgnore]
		public string WhenText
		{
			get
			{
				if (this.Month < 1 || this.Month > 12)
				{
					return string.Format("{0}", this.Year);
				}
				return string.Format("{0} {1}", GameDate.Month(this.Month), this.Year);
			}
		}
	}
}
