using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000013 RID: 19
	public class DistrictRow
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000DC57 File Offset: 0x0000BE57
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000DC5F File Offset: 0x0000BE5F
		public string Key { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000DC68 File Offset: 0x0000BE68
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x0000DC70 File Offset: 0x0000BE70
		public string Name { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000DC79 File Offset: 0x0000BE79
		// (set) Token: 0x060000AA RID: 170 RVA: 0x0000DC81 File Offset: 0x0000BE81
		public float X { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000DC8A File Offset: 0x0000BE8A
		// (set) Token: 0x060000AC RID: 172 RVA: 0x0000DC92 File Offset: 0x0000BE92
		public float Y { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000DC9B File Offset: 0x0000BE9B
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000DCA3 File Offset: 0x0000BEA3
		public List<string> Hashes { get; set; } = new List<string>();

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000DCAC File Offset: 0x0000BEAC
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000DCB4 File Offset: 0x0000BEB4
		public Dictionary<string, int> Lore { get; set; } = new Dictionary<string, int>();

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000DCBD File Offset: 0x0000BEBD
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000DCC5 File Offset: 0x0000BEC5
		public Dictionary<string, int> Lore1907 { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000DCCE File Offset: 0x0000BECE
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x0000DCD6 File Offset: 0x0000BED6
		public string Region { get; set; }
	}
}
