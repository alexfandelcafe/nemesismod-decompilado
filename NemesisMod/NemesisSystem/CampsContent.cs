using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000030 RID: 48
	public class CampsContent
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000B69 RID: 2921 RVA: 0x0010A689 File Offset: 0x00108889
		// (set) Token: 0x06000B6A RID: 2922 RVA: 0x0010A691 File Offset: 0x00108891
		public string About { get; set; } = "Camp sites for factions other than the O'Driscolls (theirs are the game's own camps). Learned in game where 3+ of a faction's men gather (seen twice), or added with H on a faction's hierarchy page (debug on). Delete a row to remove a site. hideout = officers rank 4+ prefer it.";

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x0010A69A File Offset: 0x0010889A
		// (set) Token: 0x06000B6C RID: 2924 RVA: 0x0010A6A2 File Offset: 0x001088A2
		public List<CampRow> Sites { get; set; } = new List<CampRow>();

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0010A6AB File Offset: 0x001088AB
		// (set) Token: 0x06000B6E RID: 2926 RVA: 0x0010A6B3 File Offset: 0x001088B3
		public List<CampCandidateRow> Candidates { get; set; } = new List<CampCandidateRow>();
	}
}
