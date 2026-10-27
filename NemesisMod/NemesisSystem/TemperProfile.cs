using System;

namespace NemesisSystem
{
	// Token: 0x02000072 RID: 114
	public sealed class TemperProfile
	{
		// Token: 0x04000C54 RID: 3156
		public float FearGain = 1f;

		// Token: 0x04000C55 RID: 3157
		public float HateGain = 1f;

		// Token: 0x04000C56 RID: 3158
		public float LikeGain = 1f;

		// Token: 0x04000C57 RID: 3159
		public float FadePerDay = 3f;

		// Token: 0x04000C58 RID: 3160
		public float HealPerDay = 3f;

		// Token: 0x04000C59 RID: 3161
		public int WalkOffAt = 50;

		// Token: 0x04000C5A RID: 3162
		public int RunOdds = 20;

		// Token: 0x04000C5B RID: 3163
		public int BreakLine = 25;

		// Token: 0x04000C5C RID: 3164
		public int Surrender = 25;

		// Token: 0x04000C5D RID: 3165
		public int Nerve;

		// Token: 0x04000C5E RID: 3166
		public bool StaresBack;
	}
}
