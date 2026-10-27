using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000052 RID: 82
	internal class WorldState
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000D7C RID: 3452 RVA: 0x0010C49B File Offset: 0x0010A69B
		// (set) Token: 0x06000D7D RID: 3453 RVA: 0x0010C4A3 File Offset: 0x0010A6A3
		public long NextEventAt { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000D7E RID: 3454 RVA: 0x0010C4AC File Offset: 0x0010A6AC
		// (set) Token: 0x06000D7F RID: 3455 RVA: 0x0010C4B4 File Offset: 0x0010A6B4
		public List<WorldEvent> Events { get; set; } = new List<WorldEvent>();

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000D80 RID: 3456 RVA: 0x0010C4BD File Offset: 0x0010A6BD
		// (set) Token: 0x06000D81 RID: 3457 RVA: 0x0010C4C5 File Offset: 0x0010A6C5
		public bool HistorySeeded { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x0010C4CE File Offset: 0x0010A6CE
		// (set) Token: 0x06000D83 RID: 3459 RVA: 0x0010C4D6 File Offset: 0x0010A6D6
		public int Era { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000D84 RID: 3460 RVA: 0x0010C4DF File Offset: 0x0010A6DF
		// (set) Token: 0x06000D85 RID: 3461 RVA: 0x0010C4E7 File Offset: 0x0010A6E7
		public bool JohnRecordFixed { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000D86 RID: 3462 RVA: 0x0010C4F0 File Offset: 0x0010A6F0
		// (set) Token: 0x06000D87 RID: 3463 RVA: 0x0010C4F8 File Offset: 0x0010A6F8
		public long EraTurnAt { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000D88 RID: 3464 RVA: 0x0010C501 File Offset: 0x0010A701
		// (set) Token: 0x06000D89 RID: 3465 RVA: 0x0010C509 File Offset: 0x0010A709
		public bool EraFuneralsFixed { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000D8A RID: 3466 RVA: 0x0010C512 File Offset: 0x0010A712
		// (set) Token: 0x06000D8B RID: 3467 RVA: 0x0010C51A File Offset: 0x0010A71A
		public bool WoffordFixed { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000D8C RID: 3468 RVA: 0x0010C523 File Offset: 0x0010A723
		// (set) Token: 0x06000D8D RID: 3469 RVA: 0x0010C52B File Offset: 0x0010A72B
		public bool BronteBack { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000D8E RID: 3470 RVA: 0x0010C534 File Offset: 0x0010A734
		// (set) Token: 0x06000D8F RID: 3471 RVA: 0x0010C53C File Offset: 0x0010A73C
		public bool WomenTradesFixed { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x0010C545 File Offset: 0x0010A745
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x0010C54D File Offset: 0x0010A74D
		public bool BronteFull { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x0010C556 File Offset: 0x0010A756
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x0010C55E File Offset: 0x0010A75E
		public int HistoryYear { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0010C567 File Offset: 0x0010A767
		// (set) Token: 0x06000D95 RID: 3477 RVA: 0x0010C56F File Offset: 0x0010A76F
		public List<WorldEvent> Past { get; set; } = new List<WorldEvent>();

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0010C578 File Offset: 0x0010A778
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x0010C580 File Offset: 0x0010A780
		public Dictionary<string, LeaderSeat> Leaders { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0010C589 File Offset: 0x0010A789
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x0010C591 File Offset: 0x0010A791
		public int SaloonFights { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000D9A RID: 3482 RVA: 0x0010C59A File Offset: 0x0010A79A
		// (set) Token: 0x06000D9B RID: 3483 RVA: 0x0010C5A2 File Offset: 0x0010A7A2
		public string GpsCamp { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000D9C RID: 3484 RVA: 0x0010C5AB File Offset: 0x0010A7AB
		// (set) Token: 0x06000D9D RID: 3485 RVA: 0x0010C5B3 File Offset: 0x0010A7B3
		public Dictionary<string, int> ShakeUpHour { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000D9E RID: 3486 RVA: 0x0010C5BC File Offset: 0x0010A7BC
		// (set) Token: 0x06000D9F RID: 3487 RVA: 0x0010C5C4 File Offset: 0x0010A7C4
		public Dictionary<string, int> NextShakeUp { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000DA0 RID: 3488 RVA: 0x0010C5CD File Offset: 0x0010A7CD
		// (set) Token: 0x06000DA1 RID: 3489 RVA: 0x0010C5D5 File Offset: 0x0010A7D5
		public Dictionary<string, int> UnnamedKills { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000DA2 RID: 3490 RVA: 0x0010C5DE File Offset: 0x0010A7DE
		// (set) Token: 0x06000DA3 RID: 3491 RVA: 0x0010C5E6 File Offset: 0x0010A7E6
		public Dictionary<string, long> CompanionHurt { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000DA4 RID: 3492 RVA: 0x0010C5EF File Offset: 0x0010A7EF
		// (set) Token: 0x06000DA5 RID: 3493 RVA: 0x0010C5F7 File Offset: 0x0010A7F7
		public Dictionary<string, int> StoryRegard { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x0010C600 File Offset: 0x0010A800
		// (set) Token: 0x06000DA7 RID: 3495 RVA: 0x0010C608 File Offset: 0x0010A808
		public Dictionary<string, int> StoryRegardEarned { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000DA8 RID: 3496 RVA: 0x0010C611 File Offset: 0x0010A811
		// (set) Token: 0x06000DA9 RID: 3497 RVA: 0x0010C619 File Offset: 0x0010A819
		public Dictionary<string, int> NotorietyGainDay { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x0010C622 File Offset: 0x0010A822
		// (set) Token: 0x06000DAB RID: 3499 RVA: 0x0010C62A File Offset: 0x0010A82A
		public List<Plan> Plans { get; set; } = new List<Plan>();

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0010C633 File Offset: 0x0010A833
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x0010C63B File Offset: 0x0010A83B
		public List<DeathNews> DeathNews { get; set; } = new List<DeathNews>();

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x0010C644 File Offset: 0x0010A844
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x0010C64C File Offset: 0x0010A84C
		public Dictionary<string, Dictionary<string, double>> ControlBase { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x0010C655 File Offset: 0x0010A855
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x0010C65D File Offset: 0x0010A85D
		public Dictionary<string, Dictionary<string, int>> Control { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x0010C666 File Offset: 0x0010A866
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x0010C66E File Offset: 0x0010A86E
		public bool FortCleared { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x0010C677 File Offset: 0x0010A877
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x0010C67F File Offset: 0x0010A87F
		public bool FortKnown { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x0010C688 File Offset: 0x0010A888
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x0010C690 File Offset: 0x0010A890
		public long FortClearedAt { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x0010C699 File Offset: 0x0010A899
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x0010C6A1 File Offset: 0x0010A8A1
		public Dictionary<string, string> CampOwner { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x0010C6AA File Offset: 0x0010A8AA
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x0010C6B2 File Offset: 0x0010A8B2
		public Dictionary<string, int> Grievance { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x0010C6BB File Offset: 0x0010A8BB
		// (set) Token: 0x06000DBD RID: 3517 RVA: 0x0010C6C3 File Offset: 0x0010A8C3
		public Dictionary<string, int> Treasury { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x0010C6CC File Offset: 0x0010A8CC
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x0010C6D4 File Offset: 0x0010A8D4
		public Dictionary<string, int> Men { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x0010C6DD File Offset: 0x0010A8DD
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x0010C6E5 File Offset: 0x0010A8E5
		public Dictionary<string, int> BrokeDays { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x0010C6EE File Offset: 0x0010A8EE
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x0010C6F6 File Offset: 0x0010A8F6
		public bool MenRescaled2 { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0010C6FF File Offset: 0x0010A8FF
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x0010C707 File Offset: 0x0010A907
		public bool MenRescaled { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0010C710 File Offset: 0x0010A910
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x0010C718 File Offset: 0x0010A918
		public List<string> BrokeSaid { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0010C721 File Offset: 0x0010A921
		// (set) Token: 0x06000DC9 RID: 3529 RVA: 0x0010C729 File Offset: 0x0010A929
		public Dictionary<string, Agenda> Agendas { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0010C732 File Offset: 0x0010A932
		// (set) Token: 0x06000DCB RID: 3531 RVA: 0x0010C73A File Offset: 0x0010A93A
		public int WarRollDay { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0010C743 File Offset: 0x0010A943
		// (set) Token: 0x06000DCD RID: 3533 RVA: 0x0010C74B File Offset: 0x0010A94B
		public long LastWarStartAt { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x0010C754 File Offset: 0x0010A954
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x0010C75C File Offset: 0x0010A95C
		public List<string> Bars { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x0010C765 File Offset: 0x0010A965
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x0010C76D File Offset: 0x0010A96D
		public Dictionary<string, CampTakeover> CampTakeovers { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0010C776 File Offset: 0x0010A976
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0010C77E File Offset: 0x0010A97E
		public bool HomeStrongholdsRestored { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x0010C787 File Offset: 0x0010A987
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0010C78F File Offset: 0x0010A98F
		public Dictionary<string, int> EmptyTakeovers { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x0010C798 File Offset: 0x0010A998
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0010C7A0 File Offset: 0x0010A9A0
		public bool TakeoverSpreeFixed { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000DD8 RID: 3544 RVA: 0x0010C7A9 File Offset: 0x0010A9A9
		// (set) Token: 0x06000DD9 RID: 3545 RVA: 0x0010C7B1 File Offset: 0x0010A9B1
		public long WildFreshAt { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0010C7BA File Offset: 0x0010A9BA
		// (set) Token: 0x06000DDB RID: 3547 RVA: 0x0010C7C2 File Offset: 0x0010A9C2
		public bool SelfDefenceFixed { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0010C7CB File Offset: 0x0010A9CB
		// (set) Token: 0x06000DDD RID: 3549 RVA: 0x0010C7D3 File Offset: 0x0010A9D3
		public bool PearseFixed { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0010C7DC File Offset: 0x0010A9DC
		// (set) Token: 0x06000DDF RID: 3551 RVA: 0x0010C7E4 File Offset: 0x0010A9E4
		public bool FuneralsSeeded { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x0010C7ED File Offset: 0x0010A9ED
		// (set) Token: 0x06000DE1 RID: 3553 RVA: 0x0010C7F5 File Offset: 0x0010A9F5
		public bool OutfitFolkCleaned { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x0010C7FE File Offset: 0x0010A9FE
		// (set) Token: 0x06000DE3 RID: 3555 RVA: 0x0010C806 File Offset: 0x0010AA06
		public bool OutfitModelsFixed { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x0010C80F File Offset: 0x0010AA0F
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x0010C817 File Offset: 0x0010AA17
		public bool OutfitModelsFixed2 { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x0010C820 File Offset: 0x0010AA20
		// (set) Token: 0x06000DE7 RID: 3559 RVA: 0x0010C828 File Offset: 0x0010AA28
		public bool RallyKlanFixed { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0010C831 File Offset: 0x0010AA31
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x0010C839 File Offset: 0x0010AA39
		public List<string> TreasuryStash { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0010C842 File Offset: 0x0010AA42
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x0010C84A File Offset: 0x0010AA4A
		public bool DefectorWeeksFixed { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0010C853 File Offset: 0x0010AA53
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x0010C85B File Offset: 0x0010AA5B
		public long ClockShift { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0010C864 File Offset: 0x0010AA64
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x0010C86C File Offset: 0x0010AA6C
		public bool ShiftRepaired { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0010C875 File Offset: 0x0010AA75
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x0010C87D File Offset: 0x0010AA7D
		public bool MixedTimelineCleaned { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x0010C886 File Offset: 0x0010AA86
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x0010C88E File Offset: 0x0010AA8E
		public Dictionary<string, long> Destroyed { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0010C897 File Offset: 0x0010AA97
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x0010C89F File Offset: 0x0010AA9F
		public List<string> Truces { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0010C8A8 File Offset: 0x0010AAA8
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x0010C8B0 File Offset: 0x0010AAB0
		public List<string> Alliances { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0010C8B9 File Offset: 0x0010AAB9
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x0010C8C1 File Offset: 0x0010AAC1
		public HashSet<string> CompanionNever { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0010C8CA File Offset: 0x0010AACA
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x0010C8D2 File Offset: 0x0010AAD2
		public List<string> WarZones { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000DFC RID: 3580 RVA: 0x0010C8DB File Offset: 0x0010AADB
		// (set) Token: 0x06000DFD RID: 3581 RVA: 0x0010C8E3 File Offset: 0x0010AAE3
		public Dictionary<string, int> Relations { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x0010C8EC File Offset: 0x0010AAEC
		// (set) Token: 0x06000DFF RID: 3583 RVA: 0x0010C8F4 File Offset: 0x0010AAF4
		public Dictionary<string, string> RelationWhy { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0010C8FD File Offset: 0x0010AAFD
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x0010C905 File Offset: 0x0010AB05
		public List<WarRecord> Wars { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000E02 RID: 3586 RVA: 0x0010C90E File Offset: 0x0010AB0E
		// (set) Token: 0x06000E03 RID: 3587 RVA: 0x0010C916 File Offset: 0x0010AB16
		public List<WarRecord> PastWars { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000E04 RID: 3588 RVA: 0x0010C91F File Offset: 0x0010AB1F
		// (set) Token: 0x06000E05 RID: 3589 RVA: 0x0010C927 File Offset: 0x0010AB27
		public bool MillemontFix { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000E06 RID: 3590 RVA: 0x0010C930 File Offset: 0x0010AB30
		// (set) Token: 0x06000E07 RID: 3591 RVA: 0x0010C938 File Offset: 0x0010AB38
		public long SleepRollWindow { get; set; } = -1L;

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000E08 RID: 3592 RVA: 0x0010C941 File Offset: 0x0010AB41
		// (set) Token: 0x06000E09 RID: 3593 RVA: 0x0010C949 File Offset: 0x0010AB49
		public Dictionary<string, long> Beaten { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x0010C952 File Offset: 0x0010AB52
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x0010C95A File Offset: 0x0010AB5A
		public Dictionary<string, long> WarEndedAt { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x0010C963 File Offset: 0x0010AB63
		// (set) Token: 0x06000E0D RID: 3597 RVA: 0x0010C96B File Offset: 0x0010AB6B
		public List<DeathNews> PeopleNews { get; set; } = new List<DeathNews>();

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0010C974 File Offset: 0x0010AB74
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x0010C97C File Offset: 0x0010AB7C
		public float Tension { get; set; } = -1f;

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000E10 RID: 3600 RVA: 0x0010C985 File Offset: 0x0010AB85
		// (set) Token: 0x06000E11 RID: 3601 RVA: 0x0010C98D File Offset: 0x0010AB8D
		public float TensionFireAt { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000E12 RID: 3602 RVA: 0x0010C996 File Offset: 0x0010AB96
		// (set) Token: 0x06000E13 RID: 3603 RVA: 0x0010C99E File Offset: 0x0010AB9E
		public Dictionary<string, MercyNote> Mercy { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000E14 RID: 3604 RVA: 0x0010C9A7 File Offset: 0x0010ABA7
		// (set) Token: 0x06000E15 RID: 3605 RVA: 0x0010C9AF File Offset: 0x0010ABAF
		public Dictionary<string, TownTalk> TownWord { get; set; } = new Dictionary<string, TownTalk>();

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0010C9B8 File Offset: 0x0010ABB8
		// (set) Token: 0x06000E17 RID: 3607 RVA: 0x0010C9C0 File Offset: 0x0010ABC0
		public int LastFearFadeDay { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000E18 RID: 3608 RVA: 0x0010C9C9 File Offset: 0x0010ABC9
		// (set) Token: 0x06000E19 RID: 3609 RVA: 0x0010C9D1 File Offset: 0x0010ABD1
		public long LastClockMin { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x0010C9DA File Offset: 0x0010ABDA
		// (set) Token: 0x06000E1B RID: 3611 RVA: 0x0010C9E2 File Offset: 0x0010ABE2
		public int StoryChapter { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0010C9EB File Offset: 0x0010ABEB
		// (set) Token: 0x06000E1D RID: 3613 RVA: 0x0010C9F3 File Offset: 0x0010ABF3
		public long StoryChapterAt { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0010C9FC File Offset: 0x0010ABFC
		// (set) Token: 0x06000E1F RID: 3615 RVA: 0x0010CA04 File Offset: 0x0010AC04
		public List<Telegram> Telegrams { get; set; } = new List<Telegram>();

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0010CA0D File Offset: 0x0010AC0D
		// (set) Token: 0x06000E21 RID: 3617 RVA: 0x0010CA15 File Offset: 0x0010AC15
		public Dictionary<string, int> Supplies { get; set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0010CA1E File Offset: 0x0010AC1E
		// (set) Token: 0x06000E23 RID: 3619 RVA: 0x0010CA26 File Offset: 0x0010AC26
		public Dictionary<string, string> SupplyCamps { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0010CA2F File Offset: 0x0010AC2F
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x0010CA37 File Offset: 0x0010AC37
		public Dictionary<string, long> SupplyCampHitAt { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0010CA40 File Offset: 0x0010AC40
		// (set) Token: 0x06000E27 RID: 3623 RVA: 0x0010CA48 File Offset: 0x0010AC48
		public List<string> KnownSites { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0010CA51 File Offset: 0x0010AC51
		// (set) Token: 0x06000E29 RID: 3625 RVA: 0x0010CA59 File Offset: 0x0010AC59
		public Dictionary<string, long> SupplyCaches { get; set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0010CA62 File Offset: 0x0010AC62
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x0010CA6A File Offset: 0x0010AC6A
		public Dictionary<string, string> SupplyKnownAt { get; set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0010CA73 File Offset: 0x0010AC73
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x0010CA7B File Offset: 0x0010AC7B
		public List<string> KeeperGone { get; set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x0010CA84 File Offset: 0x0010AC84
		// (set) Token: 0x06000E2F RID: 3631 RVA: 0x0010CA8C File Offset: 0x0010AC8C
		public Dictionary<string, string> SupplyCampHitSite { get; set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0010CA95 File Offset: 0x0010AC95
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x0010CA9D File Offset: 0x0010AC9D
		public Dictionary<string, string> Treasuries { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0010CAA6 File Offset: 0x0010ACA6
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x0010CAAE File Offset: 0x0010ACAE
		public List<string> TreasuryKnown { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0010CAB7 File Offset: 0x0010ACB7
		// (set) Token: 0x06000E35 RID: 3637 RVA: 0x0010CABF File Offset: 0x0010ACBF
		public Dictionary<string, long> TreasuryRobbedAt { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x0010CAC8 File Offset: 0x0010ACC8
		// (set) Token: 0x06000E37 RID: 3639 RVA: 0x0010CAD0 File Offset: 0x0010ACD0
		public Dictionary<string, string> TreasuryRobbedSite { get; set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x0010CAD9 File Offset: 0x0010ACD9
		// (set) Token: 0x06000E39 RID: 3641 RVA: 0x0010CAE1 File Offset: 0x0010ACE1
		public Dictionary<string, PenPal> PenPals { get; set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x0010CAEA File Offset: 0x0010ACEA
		// (set) Token: 0x06000E3B RID: 3643 RVA: 0x0010CAF2 File Offset: 0x0010ACF2
		public List<string> RetiredNames { get; set; } = new List<string>();

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0010CAFB File Offset: 0x0010ACFB
		// (set) Token: 0x06000E3D RID: 3645 RVA: 0x0010CB03 File Offset: 0x0010AD03
		public Dictionary<string, float> MaskedWithheld { get; set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x0010CB0C File Offset: 0x0010AD0C
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x0010CB14 File Offset: 0x0010AD14
		public Dictionary<string, float> MaskedNotoriety { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x0010CB1D File Offset: 0x0010AD1D
		// (set) Token: 0x06000E41 RID: 3649 RVA: 0x0010CB25 File Offset: 0x0010AD25
		public Dictionary<string, int> YourCrimes { get; set; } = new Dictionary<string, int>();
	}
}
