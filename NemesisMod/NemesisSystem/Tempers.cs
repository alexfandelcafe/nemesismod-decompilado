using System;

namespace NemesisSystem
{
	// Token: 0x02000073 RID: 115
	public static class Tempers
	{
		// Token: 0x060012AD RID: 4781 RVA: 0x0012AA1C File Offset: 0x00128C1C
		public static TemperProfile Of(Personality? p)
		{
			if (p != null)
			{
				switch (p.GetValueOrDefault())
				{
				case Personality.Hothead:
					return Tempers.Hothead;
				case Personality.Coward:
					return Tempers.Coward;
				case Personality.Proud:
					return Tempers.Proud;
				case Personality.Cold:
					return Tempers.Cold;
				case Personality.Greedy:
					return Tempers.Greedy;
				case Personality.Loyal:
					return Tempers.Loyal;
				case Personality.Drifter:
					return Tempers.Drifter;
				}
			}
			return Tempers.Plain;
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x0012AA90 File Offset: 0x00128C90
		public static TemperProfile Of(string temper)
		{
			Personality value;
			if (string.IsNullOrEmpty(temper) || !Enum.TryParse<Personality>(temper, out value))
			{
				return Tempers.Plain;
			}
			return Tempers.Of(new Personality?(value));
		}

		// Token: 0x04000C5F RID: 3167
		public static readonly TemperProfile Plain = new TemperProfile();

		// Token: 0x04000C60 RID: 3168
		public static readonly TemperProfile Hothead = new TemperProfile
		{
			FearGain = 0.5f,
			HateGain = 1.3f,
			WalkOffAt = 90,
			RunOdds = 5,
			BreakLine = 10,
			Surrender = 0,
			Nerve = 30,
			LikeGain = 0.8f,
			FadePerDay = 4f,
			StaresBack = true,
			HealPerDay = 2f
		};

		// Token: 0x04000C61 RID: 3169
		public static readonly TemperProfile Coward = new TemperProfile
		{
			FearGain = 2f,
			HateGain = 0.6f,
			WalkOffAt = 25,
			RunOdds = 70,
			BreakLine = 40,
			Surrender = 45,
			Nerve = -20,
			LikeGain = 1.1f,
			FadePerDay = 1.5f,
			HealPerDay = 6f
		};

		// Token: 0x04000C62 RID: 3170
		public static readonly TemperProfile Proud = new TemperProfile
		{
			FearGain = 0.6f,
			HateGain = 1.15f,
			WalkOffAt = 75,
			RunOdds = 5,
			BreakLine = 15,
			Surrender = 3,
			Nerve = 25,
			LikeGain = 0.7f,
			FadePerDay = 4f,
			StaresBack = true,
			HealPerDay = 1f
		};

		// Token: 0x04000C63 RID: 3171
		public static readonly TemperProfile Cold = new TemperProfile
		{
			FearGain = 0.8f,
			HateGain = 0.9f,
			WalkOffAt = 65,
			RunOdds = 8,
			BreakLine = 20,
			Surrender = 15,
			Nerve = 15,
			LikeGain = 0.6f,
			StaresBack = true
		};

		// Token: 0x04000C64 RID: 3172
		public static readonly TemperProfile Greedy = new TemperProfile
		{
			FearGain = 1f,
			HateGain = 0.85f,
			WalkOffAt = 45,
			RunOdds = 25,
			BreakLine = 30,
			Surrender = 35,
			LikeGain = 0.9f,
			HealPerDay = 4f
		};

		// Token: 0x04000C65 RID: 3173
		public static readonly TemperProfile Loyal = new TemperProfile
		{
			FearGain = 0.7f,
			WalkOffAt = 50,
			RunOdds = 20,
			BreakLine = 20,
			Surrender = 10,
			Nerve = 10,
			LikeGain = 1.2f,
			HealPerDay = 2f
		};

		// Token: 0x04000C66 RID: 3174
		public static readonly TemperProfile Drifter = new TemperProfile
		{
			FearGain = 1.2f,
			HateGain = 0.7f,
			WalkOffAt = 35,
			RunOdds = 35,
			BreakLine = 35,
			Surrender = 30,
			Nerve = -5,
			FadePerDay = 2f,
			HealPerDay = 6f
		};
	}
}
