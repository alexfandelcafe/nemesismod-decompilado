using System;
using System.Collections.Generic;
using RDR2.Math;

namespace NemesisSystem
{
	// Token: 0x0200001E RID: 30
	internal interface IHoldingsWorld
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000A64 RID: 2660
		WorldState State { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000A65 RID: 2661
		long Now { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000A66 RID: 2662
		IEnumerable<Gang> Outfits { get; }

		// Token: 0x06000A67 RID: 2663
		bool Destroyed(Gang g);

		// Token: 0x06000A68 RID: 2664
		CampSite Site(string id);

		// Token: 0x06000A69 RID: 2665
		IEnumerable<CampSite> SitesHeldBy(string gangId);

		// Token: 0x06000A6A RID: 2666
		string Owner(CampSite s);

		// Token: 0x06000A6B RID: 2667
		bool TakeoverPending(CampSite s);

		// Token: 0x06000A6C RID: 2668
		bool Cleared(CampSite s);

		// Token: 0x06000A6D RID: 2669
		bool Wet(CampSite s);

		// Token: 0x06000A6E RID: 2670
		bool InTown(CampSite s);

		// Token: 0x06000A6F RID: 2671
		NemesisRecord Holder(CampSite s);

		// Token: 0x06000A70 RID: 2672
		bool AnyoneCampsAt(string siteId);

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000A71 RID: 2673
		IEnumerable<Vector3> OfficerCamps { get; }

		// Token: 0x06000A72 RID: 2674
		bool LiveSupplies(string siteId);

		// Token: 0x06000A73 RID: 2675
		bool LiveTreasury(string siteId);

		// Token: 0x06000A74 RID: 2676
		NemesisRecord LiveBoss(string siteId);

		// Token: 0x06000A75 RID: 2677
		string LiveCampOf(NemesisRecord n);

		// Token: 0x06000A76 RID: 2678
		IEnumerable<NemesisRecord> FreeMen(Gang g);

		// Token: 0x06000A77 RID: 2679
		int Purse(Gang g);

		// Token: 0x06000A78 RID: 2680
		bool HasPurse(Gang g);

		// Token: 0x06000A79 RID: 2681
		int Roll(int n);
	}
}
