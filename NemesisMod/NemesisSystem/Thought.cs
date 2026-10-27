using System;

namespace NemesisSystem
{
	// Token: 0x02000051 RID: 81
	public class Thought
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x0010C43E File Offset: 0x0010A63E
		// (set) Token: 0x06000D72 RID: 3442 RVA: 0x0010C446 File Offset: 0x0010A646
		public string Mood { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000D73 RID: 3443 RVA: 0x0010C44F File Offset: 0x0010A64F
		// (set) Token: 0x06000D74 RID: 3444 RVA: 0x0010C457 File Offset: 0x0010A657
		public string Text { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0010C460 File Offset: 0x0010A660
		// (set) Token: 0x06000D76 RID: 3446 RVA: 0x0010C468 File Offset: 0x0010A668
		public int Weight { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000D77 RID: 3447 RVA: 0x0010C471 File Offset: 0x0010A671
		// (set) Token: 0x06000D78 RID: 3448 RVA: 0x0010C479 File Offset: 0x0010A679
		public long At { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0010C482 File Offset: 0x0010A682
		// (set) Token: 0x06000D7A RID: 3450 RVA: 0x0010C48A File Offset: 0x0010A68A
		public long Until { get; set; }
	}
}
