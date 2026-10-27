using System;

namespace NemesisSystem
{
	// Token: 0x02000042 RID: 66
	public class PenPal
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0010B7F3 File Offset: 0x001099F3
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x0010B7FB File Offset: 0x001099FB
		public int Interest { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0010B804 File Offset: 0x00109A04
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x0010B80C File Offset: 0x00109A0C
		public int Letters { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000C29 RID: 3113 RVA: 0x0010B815 File Offset: 0x00109A15
		// (set) Token: 0x06000C2A RID: 3114 RVA: 0x0010B81D File Offset: 0x00109A1D
		public long LastAt { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x0010B826 File Offset: 0x00109A26
		// (set) Token: 0x06000C2C RID: 3116 RVA: 0x0010B82E File Offset: 0x00109A2E
		public bool Stopped { get; set; }
	}
}
