using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000035 RID: 53
	public class CardFile
	{
		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000BD2 RID: 3026 RVA: 0x0010AA5C File Offset: 0x00108C5C
		// (set) Token: 0x06000BD3 RID: 3027 RVA: 0x0010AA64 File Offset: 0x00108C64
		public string About { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0010AA6D File Offset: 0x00108C6D
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0010AA75 File Offset: 0x00108C75
		public List<EncounterCard> Cards { get; set; } = new List<EncounterCard>();
	}
}
