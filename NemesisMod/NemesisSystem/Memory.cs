using System;

namespace NemesisSystem
{
	// Token: 0x02000027 RID: 39
	public class Memory
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x000FF7AD File Offset: 0x000FD9AD
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x000FF7B5 File Offset: 0x000FD9B5
		public string Text { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000AFA RID: 2810 RVA: 0x000FF7BE File Offset: 0x000FD9BE
		// (set) Token: 0x06000AFB RID: 2811 RVA: 0x000FF7C6 File Offset: 0x000FD9C6
		public string Emotion { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000AFC RID: 2812 RVA: 0x000FF7CF File Offset: 0x000FD9CF
		// (set) Token: 0x06000AFD RID: 2813 RVA: 0x000FF7D7 File Offset: 0x000FD9D7
		public int Intensity { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000AFE RID: 2814 RVA: 0x000FF7E0 File Offset: 0x000FD9E0
		// (set) Token: 0x06000AFF RID: 2815 RVA: 0x000FF7E8 File Offset: 0x000FD9E8
		public string Cause { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000B00 RID: 2816 RVA: 0x000FF7F1 File Offset: 0x000FD9F1
		// (set) Token: 0x06000B01 RID: 2817 RVA: 0x000FF7F9 File Offset: 0x000FD9F9
		public long At { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000B02 RID: 2818 RVA: 0x000FF802 File Offset: 0x000FDA02
		// (set) Token: 0x06000B03 RID: 2819 RVA: 0x000FF80A File Offset: 0x000FDA0A
		public bool LongTerm { get; set; }
	}
}
