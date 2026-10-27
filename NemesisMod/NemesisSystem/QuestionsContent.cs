using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000061 RID: 97
	public class QuestionsContent
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x00111BBE File Offset: 0x0010FDBE
		// (set) Token: 0x060011C3 RID: 4547 RVA: 0x00111BC6 File Offset: 0x0010FDC6
		public string About { get; set; } = "Interrogation questions (Questions.cs). cat: What / When / Where / Why (three each is what the menu is made for). need: fear over his temper's line before he'll answer. odds: + / - on his chance. lies: can he lie (a lie is still shown as ? intel, sometimes a trap). requires: gang named boss officer camps kin (joined by &). answer: orders wants bossnature catch news holedup stores people past worstgrudge bargain (and the older single ones: ride alone habit funeral shakeup camp hideout treasury supplies). requires 'news': his outfit has a burying or a shake-up heir to tell of; 'funeral': a burying. Built-in rows missing from this file are added by Id unless listed in Removed.";

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00111BCF File Offset: 0x0010FDCF
		// (set) Token: 0x060011C5 RID: 4549 RVA: 0x00111BD7 File Offset: 0x0010FDD7
		public int Version { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00111BE0 File Offset: 0x0010FDE0
		// (set) Token: 0x060011C7 RID: 4551 RVA: 0x00111BE8 File Offset: 0x0010FDE8
		public List<QuestionRow> Rows { get; set; } = new List<QuestionRow>();

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x00111BF1 File Offset: 0x0010FDF1
		// (set) Token: 0x060011C9 RID: 4553 RVA: 0x00111BF9 File Offset: 0x0010FDF9
		public List<string> Removed { get; set; } = new List<string>();
	}
}
