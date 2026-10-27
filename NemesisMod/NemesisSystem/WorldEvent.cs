using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x0200004F RID: 79
	public class WorldEvent
	{
		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0010C1E8 File Offset: 0x0010A3E8
		// (set) Token: 0x06000D32 RID: 3378 RVA: 0x0010C1F0 File Offset: 0x0010A3F0
		public string Id { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0010C1F9 File Offset: 0x0010A3F9
		// (set) Token: 0x06000D34 RID: 3380 RVA: 0x0010C201 File Offset: 0x0010A401
		public long GameMin { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0010C20A File Offset: 0x0010A40A
		// (set) Token: 0x06000D36 RID: 3382 RVA: 0x0010C212 File Offset: 0x0010A412
		public long? Shift { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000D37 RID: 3383 RVA: 0x0010C21B File Offset: 0x0010A41B
		// (set) Token: 0x06000D38 RID: 3384 RVA: 0x0010C223 File Offset: 0x0010A423
		public int Day { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000D39 RID: 3385 RVA: 0x0010C22C File Offset: 0x0010A42C
		// (set) Token: 0x06000D3A RID: 3386 RVA: 0x0010C234 File Offset: 0x0010A434
		public string When { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000D3B RID: 3387 RVA: 0x0010C23D File Offset: 0x0010A43D
		// (set) Token: 0x06000D3C RID: 3388 RVA: 0x0010C245 File Offset: 0x0010A445
		public string Kind { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000D3D RID: 3389 RVA: 0x0010C24E File Offset: 0x0010A44E
		// (set) Token: 0x06000D3E RID: 3390 RVA: 0x0010C256 File Offset: 0x0010A456
		public string Title { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000D3F RID: 3391 RVA: 0x0010C25F File Offset: 0x0010A45F
		// (set) Token: 0x06000D40 RID: 3392 RVA: 0x0010C267 File Offset: 0x0010A467
		public string Place { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x0010C270 File Offset: 0x0010A470
		// (set) Token: 0x06000D42 RID: 3394 RVA: 0x0010C278 File Offset: 0x0010A478
		public bool HasPos { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x0010C281 File Offset: 0x0010A481
		// (set) Token: 0x06000D44 RID: 3396 RVA: 0x0010C289 File Offset: 0x0010A489
		public float X { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000D45 RID: 3397 RVA: 0x0010C292 File Offset: 0x0010A492
		// (set) Token: 0x06000D46 RID: 3398 RVA: 0x0010C29A File Offset: 0x0010A49A
		public float Y { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000D47 RID: 3399 RVA: 0x0010C2A3 File Offset: 0x0010A4A3
		// (set) Token: 0x06000D48 RID: 3400 RVA: 0x0010C2AB File Offset: 0x0010A4AB
		public float Z { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000D49 RID: 3401 RVA: 0x0010C2B4 File Offset: 0x0010A4B4
		// (set) Token: 0x06000D4A RID: 3402 RVA: 0x0010C2BC File Offset: 0x0010A4BC
		public List<string> Gangs { get; set; } = new List<string>();

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000D4B RID: 3403 RVA: 0x0010C2C5 File Offset: 0x0010A4C5
		// (set) Token: 0x06000D4C RID: 3404 RVA: 0x0010C2CD File Offset: 0x0010A4CD
		public List<WorldPart> People { get; set; } = new List<WorldPart>();

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000D4D RID: 3405 RVA: 0x0010C2D6 File Offset: 0x0010A4D6
		// (set) Token: 0x06000D4E RID: 3406 RVA: 0x0010C2DE File Offset: 0x0010A4DE
		public List<string> Lines { get; set; } = new List<string>();

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000D4F RID: 3407 RVA: 0x0010C2E7 File Offset: 0x0010A4E7
		// (set) Token: 0x06000D50 RID: 3408 RVA: 0x0010C2EF File Offset: 0x0010A4EF
		public Dictionary<string, int> UnnamedDead { get; set; } = new Dictionary<string, int>();

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000D51 RID: 3409 RVA: 0x0010C2F8 File Offset: 0x0010A4F8
		// (set) Token: 0x06000D52 RID: 3410 RVA: 0x0010C300 File Offset: 0x0010A500
		public bool Heard { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000D53 RID: 3411 RVA: 0x0010C309 File Offset: 0x0010A509
		// (set) Token: 0x06000D54 RID: 3412 RVA: 0x0010C311 File Offset: 0x0010A511
		public long AftermathUntil { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000D55 RID: 3413 RVA: 0x0010C31A File Offset: 0x0010A51A
		// (set) Token: 0x06000D56 RID: 3414 RVA: 0x0010C322 File Offset: 0x0010A522
		public bool Visited { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000D57 RID: 3415 RVA: 0x0010C32B File Offset: 0x0010A52B
		// (set) Token: 0x06000D58 RID: 3416 RVA: 0x0010C333 File Offset: 0x0010A533
		public string Tag { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000D59 RID: 3417 RVA: 0x0010C33C File Offset: 0x0010A53C
		// (set) Token: 0x06000D5A RID: 3418 RVA: 0x0010C344 File Offset: 0x0010A544
		public bool Marked { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000D5B RID: 3419 RVA: 0x0010C34D File Offset: 0x0010A54D
		// (set) Token: 0x06000D5C RID: 3420 RVA: 0x0010C355 File Offset: 0x0010A555
		public bool IsPast { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000D5D RID: 3421 RVA: 0x0010C35E File Offset: 0x0010A55E
		// (set) Token: 0x06000D5E RID: 3422 RVA: 0x0010C366 File Offset: 0x0010A566
		public bool Reached { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000D5F RID: 3423 RVA: 0x0010C36F File Offset: 0x0010A56F
		// (set) Token: 0x06000D60 RID: 3424 RVA: 0x0010C377 File Offset: 0x0010A577
		public bool ForceSurvivors { get; set; }
	}
}
