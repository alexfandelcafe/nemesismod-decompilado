using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000015 RID: 21
	public class PlacesContent
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000DD65 File Offset: 0x0000BF65
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x0000DD6D File Offset: 0x0000BF6D
		public string About { get; set; } = "Districts: Key, Name, X / Y (its middle), Hashes (the zone hashes the game gives for it, hex), Lore (faction id -> starting hold 0-100; a faction's encounter weight there = hold / 18). Towns: Name (the game's zone name), Doctor / Saloon [x, y, z] (either may be missing), CoreRadius (the busy part, m).";

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000DD76 File Offset: 0x0000BF76
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x0000DD7E File Offset: 0x0000BF7E
		public List<DistrictRow> Districts { get; set; } = new List<DistrictRow>();

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000DD87 File Offset: 0x0000BF87
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x0000DD8F File Offset: 0x0000BF8F
		public List<TownRow> Towns { get; set; } = new List<TownRow>();
	}
}
