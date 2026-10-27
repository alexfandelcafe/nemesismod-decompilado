using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000036 RID: 54
	public class TaleRow
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000BD7 RID: 3031 RVA: 0x0010AA91 File Offset: 0x00108C91
		// (set) Token: 0x06000BD8 RID: 3032 RVA: 0x0010AA99 File Offset: 0x00108C99
		public string Kind { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000BD9 RID: 3033 RVA: 0x0010AAA2 File Offset: 0x00108CA2
		// (set) Token: 0x06000BDA RID: 3034 RVA: 0x0010AAAA File Offset: 0x00108CAA
		public string Title { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0010AAB3 File Offset: 0x00108CB3
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0010AABB File Offset: 0x00108CBB
		public List<string> Places { get; set; } = new List<string>();

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0010AAC4 File Offset: 0x00108CC4
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x0010AACC File Offset: 0x00108CCC
		public string Foe { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0010AAD5 File Offset: 0x00108CD5
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x0010AADD File Offset: 0x00108CDD
		public int HeardPercent { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0010AAE6 File Offset: 0x00108CE6
		// (set) Token: 0x06000BE2 RID: 3042 RVA: 0x0010AAEE File Offset: 0x00108CEE
		public string Line { get; set; }
	}
}
