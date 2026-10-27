using System;

namespace NemesisSystem
{
	// Token: 0x0200003E RID: 62
	public class LeaderSeat
	{
		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x0010B722 File Offset: 0x00109922
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x0010B72A File Offset: 0x0010992A
		public string Fallen { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x0010B733 File Offset: 0x00109933
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x0010B73B File Offset: 0x0010993B
		public string OldName { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x0010B744 File Offset: 0x00109944
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x0010B74C File Offset: 0x0010994C
		public long At { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x0010B755 File Offset: 0x00109955
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x0010B75D File Offset: 0x0010995D
		public string Successor { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0010B766 File Offset: 0x00109966
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x0010B76E File Offset: 0x0010996E
		public long FillAt { get; set; }
	}
}
