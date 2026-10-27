using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000045 RID: 69
	internal class WarRecord
	{
		// Token: 0x04000938 RID: 2360
		public string District;

		// Token: 0x04000939 RID: 2361
		public string A;

		// Token: 0x0400093A RID: 2362
		public string B;

		// Token: 0x0400093B RID: 2363
		public long StartMin;

		// Token: 0x0400093C RID: 2364
		public int HoldA0;

		// Token: 0x0400093D RID: 2365
		public int HoldB0;

		// Token: 0x0400093E RID: 2366
		public int Clashes;

		// Token: 0x0400093F RID: 2367
		public int Dead;

		// Token: 0x04000940 RID: 2368
		public int LastReportDay;

		// Token: 0x04000941 RID: 2369
		public int DeadAtReport;

		// Token: 0x04000942 RID: 2370
		public int MenA0;

		// Token: 0x04000943 RID: 2371
		public int MenB0;

		// Token: 0x04000944 RID: 2372
		public int GiveUpDay;

		// Token: 0x04000945 RID: 2373
		public string Cause;

		// Token: 0x04000946 RID: 2374
		public string Goal;

		// Token: 0x04000947 RID: 2375
		public string GoalSite;

		// Token: 0x04000948 RID: 2376
		public string GoalText;

		// Token: 0x04000949 RID: 2377
		public string Result;

		// Token: 0x0400094A RID: 2378
		public int Heat;

		// Token: 0x0400094B RID: 2379
		public bool HeatSet;

		// Token: 0x0400094C RID: 2380
		public bool Paused;

		// Token: 0x0400094D RID: 2381
		public List<string> GoalLog = new List<string>();

		// Token: 0x0400094E RID: 2382
		public long LastBattleAt;

		// Token: 0x0400094F RID: 2383
		public int BattleDay;

		// Token: 0x04000950 RID: 2384
		public int BattlesToday;

		// Token: 0x04000951 RID: 2385
		public int YouKilledA;

		// Token: 0x04000952 RID: 2386
		public int YouKilledB;

		// Token: 0x04000953 RID: 2387
		public int OfficersA;

		// Token: 0x04000954 RID: 2388
		public int OfficersB;

		// Token: 0x04000955 RID: 2389
		public bool LeaderFellA;

		// Token: 0x04000956 RID: 2390
		public bool LeaderFellB;

		// Token: 0x04000957 RID: 2391
		public bool FavourSaid;

		// Token: 0x04000958 RID: 2392
		public long EndMin;

		// Token: 0x04000959 RID: 2393
		public string EndTitle;

		// Token: 0x0400095A RID: 2394
		public string EndHow;

		// Token: 0x0400095B RID: 2395
		public string EndPurseA;

		// Token: 0x0400095C RID: 2396
		public string EndPurseB;

		// Token: 0x0400095D RID: 2397
		public string EndMenA;

		// Token: 0x0400095E RID: 2398
		public string EndMenB;

		// Token: 0x0400095F RID: 2399
		public string EndLeaderA;

		// Token: 0x04000960 RID: 2400
		public string EndLeaderB;

		// Token: 0x04000961 RID: 2401
		public string EndAimA;

		// Token: 0x04000962 RID: 2402
		public string EndAimB;

		// Token: 0x04000963 RID: 2403
		public int EndHoldA = -1;

		// Token: 0x04000964 RID: 2404
		public int EndHoldB = -1;

		// Token: 0x04000965 RID: 2405
		public string Name;
	}
}
