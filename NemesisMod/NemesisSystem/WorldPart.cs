using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000050 RID: 80
	public class WorldPart
	{
		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x0010C3B4 File Offset: 0x0010A5B4
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x0010C3BC File Offset: 0x0010A5BC
		public string Id { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x0010C3C5 File Offset: 0x0010A5C5
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x0010C3CD File Offset: 0x0010A5CD
		public string Name { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000D66 RID: 3430 RVA: 0x0010C3D6 File Offset: 0x0010A5D6
		// (set) Token: 0x06000D67 RID: 3431 RVA: 0x0010C3DE File Offset: 0x0010A5DE
		public string Gang { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000D68 RID: 3432 RVA: 0x0010C3E7 File Offset: 0x0010A5E7
		// (set) Token: 0x06000D69 RID: 3433 RVA: 0x0010C3EF File Offset: 0x0010A5EF
		public string Outcome { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000D6A RID: 3434 RVA: 0x0010C3F8 File Offset: 0x0010A5F8
		// (set) Token: 0x06000D6B RID: 3435 RVA: 0x0010C400 File Offset: 0x0010A600
		public string Note { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000D6C RID: 3436 RVA: 0x0010C409 File Offset: 0x0010A609
		// (set) Token: 0x06000D6D RID: 3437 RVA: 0x0010C411 File Offset: 0x0010A611
		public bool SecretlyLived { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000D6E RID: 3438 RVA: 0x0010C41A File Offset: 0x0010A61A
		// (set) Token: 0x06000D6F RID: 3439 RVA: 0x0010C422 File Offset: 0x0010A622
		public List<string> Effects { get; set; } = new List<string>();
	}
}
