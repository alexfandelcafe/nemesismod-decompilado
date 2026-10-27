using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000084 RID: 132
	public class LifeContentRow
	{
		// Token: 0x17000321 RID: 801
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x00138B60 File Offset: 0x00136D60
		// (set) Token: 0x060013A8 RID: 5032 RVA: 0x00138B68 File Offset: 0x00136D68
		public string Id { get; set; }

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x00138B71 File Offset: 0x00136D71
		// (set) Token: 0x060013AA RID: 5034 RVA: 0x00138B79 File Offset: 0x00136D79
		public string Who { get; set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060013AB RID: 5035 RVA: 0x00138B82 File Offset: 0x00136D82
		// (set) Token: 0x060013AC RID: 5036 RVA: 0x00138B8A File Offset: 0x00136D8A
		public string Title { get; set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00138B93 File Offset: 0x00136D93
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x00138B9B File Offset: 0x00136D9B
		public string Thought { get; set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00138BA4 File Offset: 0x00136DA4
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x00138BAC File Offset: 0x00136DAC
		public string Mood { get; set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00138BB5 File Offset: 0x00136DB5
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x00138BBD File Offset: 0x00136DBD
		public int Weight { get; set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00138BC6 File Offset: 0x00136DC6
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x00138BCE File Offset: 0x00136DCE
		public int Hours { get; set; }

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00138BD7 File Offset: 0x00136DD7
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x00138BDF File Offset: 0x00136DDF
		public string When { get; set; }

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x00138BE8 File Offset: 0x00136DE8
		// (set) Token: 0x060013B8 RID: 5048 RVA: 0x00138BF0 File Offset: 0x00136DF0
		public int Odds { get; set; }

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x00138BF9 File Offset: 0x00136DF9
		// (set) Token: 0x060013BA RID: 5050 RVA: 0x00138C01 File Offset: 0x00136E01
		public List<string> Effects { get; set; }
	}
}
