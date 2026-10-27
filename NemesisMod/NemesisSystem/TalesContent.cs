using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000037 RID: 55
	public class TalesContent
	{
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0010AB0A File Offset: 0x00108D0A
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x0010AB12 File Offset: 0x00108D12
		public string About { get; set; } = "The world's past (History.cs). lore: Year (absolute) or YearsAgo, Month, Title, Place, Gang (faction id or null), Lines, When (text instead of a date). tales: per faction id, the raids / fights its men rode in - Kind RAID / CLASH / DUTY, Title with {place}, Places, Foe (faction id or null), HeardPercent, Line.";

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x0010AB1B File Offset: 0x00108D1B
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x0010AB23 File Offset: 0x00108D23
		public List<NemesisMod.LoreRow> Lore { get; set; } = new List<NemesisMod.LoreRow>();

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x0010AB2C File Offset: 0x00108D2C
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x0010AB34 File Offset: 0x00108D34
		public Dictionary<string, List<TaleRow>> Tales { get; set; } = new Dictionary<string, List<TaleRow>>();
	}
}
