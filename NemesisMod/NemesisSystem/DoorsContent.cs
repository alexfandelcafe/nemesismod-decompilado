using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x0200002E RID: 46
	public class DoorsContent
	{
		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x0010A5B3 File Offset: 0x001087B3
		// (set) Token: 0x06000B52 RID: 2898 RVA: 0x0010A5BB File Offset: 0x001087BB
		public string About { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000B53 RID: 2899 RVA: 0x0010A5C4 File Offset: 0x001087C4
		// (set) Token: 0x06000B54 RID: 2900 RVA: 0x0010A5CC File Offset: 0x001087CC
		public List<DoorRow> Doors { get; set; } = new List<DoorRow>();
	}
}
