using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace NemesisSystem
{
	// Token: 0x02000012 RID: 18
	public class FactionsContent
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000DC17 File Offset: 0x0000BE17
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x0000DC1F File Offset: 0x0000BE1F
		public string About { get; set; } = "Every faction (Gangs.cs fields by name). Leave a field out to keep the built-in value; a new Id = a new faction. Name lists: a NameBank name (\"IrishFirst\") or a list. Districts: zone hashes as hex. Tiers: Label / MinRank / Seats, top first. Seed: Rank / Count / Models. Relations: other faction id -> -100..100 (either side may say it). Jobs: Title / Place with {t} = the town, Crew; JobTowns: where they happen.";

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000DC28 File Offset: 0x0000BE28
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x0000DC30 File Offset: 0x0000BE30
		public List<JObject> Factions { get; set; } = new List<JObject>();
	}
}
