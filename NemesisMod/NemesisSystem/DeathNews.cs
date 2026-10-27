using System;

namespace NemesisSystem
{
	// Token: 0x0200003A RID: 58
	internal class DeathNews
	{
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x0010ABD3 File Offset: 0x00108DD3
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x0010ABDB File Offset: 0x00108DDB
		public string DeadId { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0010ABE4 File Offset: 0x00108DE4
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x0010ABEC File Offset: 0x00108DEC
		public string DeadName { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x0010ABF5 File Offset: 0x00108DF5
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x0010ABFD File Offset: 0x00108DFD
		public string KinId { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x0010AC06 File Offset: 0x00108E06
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x0010AC0E File Offset: 0x00108E0E
		public bool ByYou { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x0010AC17 File Offset: 0x00108E17
		// (set) Token: 0x06000BF5 RID: 3061 RVA: 0x0010AC1F File Offset: 0x00108E1F
		public string By { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0010AC28 File Offset: 0x00108E28
		// (set) Token: 0x06000BF7 RID: 3063 RVA: 0x0010AC30 File Offset: 0x00108E30
		public string EventId { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000BF8 RID: 3064 RVA: 0x0010AC39 File Offset: 0x00108E39
		// (set) Token: 0x06000BF9 RID: 3065 RVA: 0x0010AC41 File Offset: 0x00108E41
		public long DueAt { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0010AC4A File Offset: 0x00108E4A
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x0010AC52 File Offset: 0x00108E52
		public long At { get; set; }
	}
}
