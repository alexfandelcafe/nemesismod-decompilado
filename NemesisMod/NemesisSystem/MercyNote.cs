using System;
using Newtonsoft.Json;

namespace NemesisSystem
{
	// Token: 0x0200004D RID: 77
	public class MercyNote
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x0010C150 File Offset: 0x0010A350
		// (set) Token: 0x06000D20 RID: 3360 RVA: 0x0010C158 File Offset: 0x0010A358
		public string Gang { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0010C161 File Offset: 0x0010A361
		// (set) Token: 0x06000D22 RID: 3362 RVA: 0x0010C169 File Offset: 0x0010A369
		public int Notoriety { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0010C172 File Offset: 0x0010A372
		// (set) Token: 0x06000D24 RID: 3364 RVA: 0x0010C17A File Offset: 0x0010A37A
		public string Town { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0010C183 File Offset: 0x0010A383
		// (set) Token: 0x06000D26 RID: 3366 RVA: 0x0010C18B File Offset: 0x0010A38B
		public float Good { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x0010C194 File Offset: 0x0010A394
		// (set) Token: 0x06000D28 RID: 3368 RVA: 0x0010C19C File Offset: 0x0010A39C
		public long At { get; set; }

		// Token: 0x040009B6 RID: 2486
		[JsonIgnore]
		public uint Model;
	}
}
