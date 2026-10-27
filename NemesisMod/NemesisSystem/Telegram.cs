using System;

namespace NemesisSystem
{
	// Token: 0x0200004A RID: 74
	public class Telegram
	{
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0010BF06 File Offset: 0x0010A106
		// (set) Token: 0x06000CEF RID: 3311 RVA: 0x0010BF0E File Offset: 0x0010A10E
		public string Id { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0010BF17 File Offset: 0x0010A117
		// (set) Token: 0x06000CF1 RID: 3313 RVA: 0x0010BF1F File Offset: 0x0010A11F
		public string ToId { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0010BF28 File Offset: 0x0010A128
		// (set) Token: 0x06000CF3 RID: 3315 RVA: 0x0010BF30 File Offset: 0x0010A130
		public bool ToNemesis { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0010BF39 File Offset: 0x0010A139
		// (set) Token: 0x06000CF5 RID: 3317 RVA: 0x0010BF41 File Offset: 0x0010A141
		public string ToName { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0010BF4A File Offset: 0x0010A14A
		// (set) Token: 0x06000CF7 RID: 3319 RVA: 0x0010BF52 File Offset: 0x0010A152
		public string Message { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0010BF5B File Offset: 0x0010A15B
		// (set) Token: 0x06000CF9 RID: 3321 RVA: 0x0010BF63 File Offset: 0x0010A163
		public string GiftGang { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0010BF6C File Offset: 0x0010A16C
		// (set) Token: 0x06000CFB RID: 3323 RVA: 0x0010BF74 File Offset: 0x0010A174
		public string Text { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0010BF7D File Offset: 0x0010A17D
		// (set) Token: 0x06000CFD RID: 3325 RVA: 0x0010BF85 File Offset: 0x0010A185
		public string From { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0010BF8E File Offset: 0x0010A18E
		// (set) Token: 0x06000CFF RID: 3327 RVA: 0x0010BF96 File Offset: 0x0010A196
		public long SentAt { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0010BF9F File Offset: 0x0010A19F
		// (set) Token: 0x06000D01 RID: 3329 RVA: 0x0010BFA7 File Offset: 0x0010A1A7
		public long ArriveAt { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0010BFB0 File Offset: 0x0010A1B0
		// (set) Token: 0x06000D03 RID: 3331 RVA: 0x0010BFB8 File Offset: 0x0010A1B8
		public bool Arrived { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0010BFC1 File Offset: 0x0010A1C1
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x0010BFC9 File Offset: 0x0010A1C9
		public bool Read { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0010BFD2 File Offset: 0x0010A1D2
		// (set) Token: 0x06000D07 RID: 3335 RVA: 0x0010BFDA File Offset: 0x0010A1DA
		public string Reply { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000D08 RID: 3336 RVA: 0x0010BFE3 File Offset: 0x0010A1E3
		// (set) Token: 0x06000D09 RID: 3337 RVA: 0x0010BFEB File Offset: 0x0010A1EB
		public string Effect { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x06000D0A RID: 3338 RVA: 0x0010BFF4 File Offset: 0x0010A1F4
		// (set) Token: 0x06000D0B RID: 3339 RVA: 0x0010BFFC File Offset: 0x0010A1FC
		public string Outlook { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x0010C005 File Offset: 0x0010A205
		// (set) Token: 0x06000D0D RID: 3341 RVA: 0x0010C00D File Offset: 0x0010A20D
		public string Result { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x0010C016 File Offset: 0x0010A216
		// (set) Token: 0x06000D0F RID: 3343 RVA: 0x0010C01E File Offset: 0x0010A21E
		public int Money { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0010C027 File Offset: 0x0010A227
		// (set) Token: 0x06000D11 RID: 3345 RVA: 0x0010C02F File Offset: 0x0010A22F
		public string TreasuryGang { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x0010C038 File Offset: 0x0010A238
		// (set) Token: 0x06000D13 RID: 3347 RVA: 0x0010C040 File Offset: 0x0010A240
		public string TreasurySite { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x0010C049 File Offset: 0x0010A249
		// (set) Token: 0x06000D15 RID: 3349 RVA: 0x0010C051 File Offset: 0x0010A251
		public string TrackId { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0010C05A File Offset: 0x0010A25A
		// (set) Token: 0x06000D17 RID: 3351 RVA: 0x0010C062 File Offset: 0x0010A262
		public bool Spontaneous { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0010C06B File Offset: 0x0010A26B
		// (set) Token: 0x06000D19 RID: 3353 RVA: 0x0010C073 File Offset: 0x0010A273
		public string InReplyTo { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000D1A RID: 3354 RVA: 0x0010C07C File Offset: 0x0010A27C
		// (set) Token: 0x06000D1B RID: 3355 RVA: 0x0010C084 File Offset: 0x0010A284
		public bool NoAnswer { get; set; }
	}
}
