using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000085 RID: 133
	public class LifeContent
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x00138C12 File Offset: 0x00136E12
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x00138C1A File Offset: 0x00136E1A
		public string About { get; set; } = "Everyday events of the men on your roster (WorldSim LIFE, WorldLife.cs). who: any / gang / loner / army / a faction id. Tokens {n} him, {t} a town, {g} his gang, {f} a mate, {b} his boss, {l} his leader. weight: + good / - bad mood strength. hours on his mind. when: conditions joined by & (and) and | (or): married single children notarmy knows poor rich dim sharp pet nopet loyal coward hothead proud cold greedy drifter friendly surly devout godless notgodless catholic a quirk (drunk gambler braggart traitor honest superstitious sadist) rank>=N rank<=N hate>=N hate<N (N or vengeful / murderous) fear>=N fear<N aggr<N aggr>=N. odds: how often (10 = usual). effects: rest:H fear:N hate:N child marry single widow scar:beat|blade|claw friend sour pet nopet. Ids keep built-in rows from being added twice; list an Id in Removed to drop a built-in row for good.";

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x00138C23 File Offset: 0x00136E23
		// (set) Token: 0x060013BF RID: 5055 RVA: 0x00138C2B File Offset: 0x00136E2B
		public int Version { get; set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x00138C34 File Offset: 0x00136E34
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x00138C3C File Offset: 0x00136E3C
		public List<LifeContentRow> Rows { get; set; } = new List<LifeContentRow>();

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00138C45 File Offset: 0x00136E45
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x00138C4D File Offset: 0x00136E4D
		public List<string> Removed { get; set; } = new List<string>();
	}
}
