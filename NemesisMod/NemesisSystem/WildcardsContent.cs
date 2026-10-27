using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000016 RID: 22
	public class WildcardsContent
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000DDC1 File Offset: 0x0000BFC1
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x0000DDC9 File Offset: 0x0000BFC9
		public string About { get; set; } = "TURMOIL world events (Sandbox.cs): Weight = how often turmoil comes up next to the [World] weights (clash 20, life 60...), Kinds = each wildcard's weight (0 = never). Incident lines: {a} {b} = the two factions, {p} / {d} = the district.";

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000DDD2 File Offset: 0x0000BFD2
		// (set) Token: 0x060000CB RID: 203 RVA: 0x0000DDDA File Offset: 0x0000BFDA
		public int Weight { get; set; } = 10;

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000DDE3 File Offset: 0x0000BFE3
		// (set) Token: 0x060000CD RID: 205 RVA: 0x0000DDEB File Offset: 0x0000BFEB
		public Dictionary<string, int> Kinds { get; set; } = new Dictionary<string, int>();

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000DDF4 File Offset: 0x0000BFF4
		// (set) Token: 0x060000CF RID: 207 RVA: 0x0000DDFC File Offset: 0x0000BFFC
		public List<string> BadBlood { get; set; } = new List<string>();

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000DE05 File Offset: 0x0000C005
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000DE0D File Offset: 0x0000C00D
		public List<string> Deals { get; set; } = new List<string>();
	}
}
