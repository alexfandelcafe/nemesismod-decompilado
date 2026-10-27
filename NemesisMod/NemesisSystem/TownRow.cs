using System;

namespace NemesisSystem
{
	// Token: 0x02000014 RID: 20
	public class TownRow
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000DCFD File Offset: 0x0000BEFD
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x0000DD05 File Offset: 0x0000BF05
		public string Name { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000DD0E File Offset: 0x0000BF0E
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x0000DD16 File Offset: 0x0000BF16
		public float[] Doctor { get; set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000DD1F File Offset: 0x0000BF1F
		// (set) Token: 0x060000BB RID: 187 RVA: 0x0000DD27 File Offset: 0x0000BF27
		public float[] Saloon { get; set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000DD30 File Offset: 0x0000BF30
		// (set) Token: 0x060000BD RID: 189 RVA: 0x0000DD38 File Offset: 0x0000BF38
		public float CoreRadius { get; set; } = 110f;

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000DD41 File Offset: 0x0000BF41
		// (set) Token: 0x060000BF RID: 191 RVA: 0x0000DD49 File Offset: 0x0000BF49
		public float[] Centre { get; set; }
	}
}
