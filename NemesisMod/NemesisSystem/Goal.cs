using System;

namespace NemesisSystem
{
	// Token: 0x02000029 RID: 41
	public class Goal
	{
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x000FF867 File Offset: 0x000FDA67
		// (set) Token: 0x06000B0F RID: 2831 RVA: 0x000FF86F File Offset: 0x000FDA6F
		public string Type { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x000FF878 File Offset: 0x000FDA78
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x000FF880 File Offset: 0x000FDA80
		public string Target { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x000FF889 File Offset: 0x000FDA89
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x000FF891 File Offset: 0x000FDA91
		public string Text { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x000FF89A File Offset: 0x000FDA9A
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x000FF8A2 File Offset: 0x000FDAA2
		public long Since { get; set; }
	}
}
