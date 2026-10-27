using System;
using System.ComponentModel;

namespace NemesisSystem
{
	// Token: 0x02000060 RID: 96
	public class QuestionRow
	{
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00111B16 File Offset: 0x0010FD16
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x00111B1E File Offset: 0x0010FD1E
		public string Id { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00111B27 File Offset: 0x0010FD27
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x00111B2F File Offset: 0x0010FD2F
		public string Cat { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x060011B3 RID: 4531 RVA: 0x00111B38 File Offset: 0x0010FD38
		// (set) Token: 0x060011B4 RID: 4532 RVA: 0x00111B40 File Offset: 0x0010FD40
		public string Text { get; set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x00111B49 File Offset: 0x0010FD49
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x00111B51 File Offset: 0x0010FD51
		public int Need { get; set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x00111B5A File Offset: 0x0010FD5A
		// (set) Token: 0x060011B8 RID: 4536 RVA: 0x00111B62 File Offset: 0x0010FD62
		public int Odds { get; set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x00111B6B File Offset: 0x0010FD6B
		// (set) Token: 0x060011BA RID: 4538 RVA: 0x00111B73 File Offset: 0x0010FD73
		public bool Lies { get; set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x00111B7C File Offset: 0x0010FD7C
		// (set) Token: 0x060011BC RID: 4540 RVA: 0x00111B84 File Offset: 0x0010FD84
		public string Requires { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x00111B8D File Offset: 0x0010FD8D
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x00111B95 File Offset: 0x0010FD95
		public string Answer { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x00111B9E File Offset: 0x0010FD9E
		// (set) Token: 0x060011C0 RID: 4544 RVA: 0x00111BA6 File Offset: 0x0010FDA6
		[DefaultValue(true)]
		public bool Once { get; set; } = true;
	}
}
