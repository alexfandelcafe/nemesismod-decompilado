using System;

namespace NemesisSystem
{
	// Token: 0x02000056 RID: 86
	public static class Hate
	{
		// Token: 0x06000E4C RID: 3660 RVA: 0x0010CC05 File Offset: 0x0010AE05
		public static HateTier TierOf(int hate)
		{
			if (hate >= Hate.MurderousAt)
			{
				return HateTier.Murderous;
			}
			if (hate < Hate.VengefulAt)
			{
				return HateTier.Resentful;
			}
			return HateTier.Vengeful;
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x0010CC1C File Offset: 0x0010AE1C
		public static int Initial(GrudgeOrigin origin)
		{
			switch (origin)
			{
			case GrudgeOrigin.Greeted:
				return 15;
			case GrudgeOrigin.Insulted:
				return 15;
			case GrudgeOrigin.Threatened:
				return 25;
			case GrudgeOrigin.Gunfight:
				return 40;
			case GrudgeOrigin.Beaten:
				return 27;
			case GrudgeOrigin.KnockedOut:
				return 30;
			case GrudgeOrigin.Robbed:
				return 46;
			case GrudgeOrigin.FriendKilled:
				return 50;
			case GrudgeOrigin.Kidnapped:
				return 55;
			case GrudgeOrigin.WagonStolen:
				return 58;
			case GrudgeOrigin.HorseStolen:
				return 62;
			case GrudgeOrigin.HorseKilled:
				return 72;
			case GrudgeOrigin.Slashed:
				return 65;
			case GrudgeOrigin.Wounded:
				return 75;
			case GrudgeOrigin.GangMember:
				return 70;
			default:
				return 80;
			}
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x0010CC9E File Offset: 0x0010AE9E
		public static string Intent(HateTier tier)
		{
			if (tier == HateTier.Murderous)
			{
				return "quiere verte muerto";
			}
			if (tier != HateTier.Vengeful)
			{
				return "wants a fistfight";
			}
			return "brings a blade";
		}

		// Token: 0x04000A5A RID: 2650
		public const int Max = 100;

		// Token: 0x04000A5B RID: 2651
		public static int VengefulAt = 40;

		// Token: 0x04000A5C RID: 2652
		public static int MurderousAt = 70;
	}
}
