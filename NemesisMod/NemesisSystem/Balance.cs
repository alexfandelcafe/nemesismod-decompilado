using System;

namespace NemesisSystem
{
	// Token: 0x0200000C RID: 12
	public static class Balance
	{
		// Token: 0x0400004E RID: 78
		public static float HateGainScale = 1f;

		// Token: 0x0400004F RID: 79
		public static float LonerShiftScale = 1f;

		// Token: 0x04000050 RID: 80
		public static float CoolingScale = 1f;

		// Token: 0x04000051 RID: 81
		public static float FearGainScale = 1f;

		// Token: 0x04000052 RID: 82
		public static int FearDecayPer10Min = 1;

		// Token: 0x04000053 RID: 83
		public static int FearPerScar = 20;

		// Token: 0x04000054 RID: 84
		public static int FearPerCripple = 25;

		// Token: 0x04000055 RID: 85
		public static int FearPerKnockout = 10;

		// Token: 0x04000056 RID: 86
		public static int KoHate = 12;

		// Token: 0x04000057 RID: 87
		public static int FistCap = 48;

		// Token: 0x04000058 RID: 88
		public static int FistCapHot = 50;

		// Token: 0x04000059 RID: 89
		public static int FistCapViolent = 62;

		// Token: 0x0400005A RID: 90
		public static int FistCapRage = 78;

		// Token: 0x0400005B RID: 91
		public static int FearPerFlee = 25;

		// Token: 0x0400005C RID: 92
		public static int FledHateCut = 15;

		// Token: 0x0400005D RID: 93
		public static int FearPerInterrogation = 10;

		// Token: 0x0400005E RID: 94
		public static int FearPerRobbery = 5;

		// Token: 0x0400005F RID: 95
		public static int NotorietyFearDivisor = 4;

		// Token: 0x04000060 RID: 96
		public static int WontHuntFear = 60;

		// Token: 0x04000061 RID: 97
		public static int ProvokedOnlyFear = 75;

		// Token: 0x04000062 RID: 98
		public static int VengefulHuntAggression = 75;

		// Token: 0x04000063 RID: 99
		public static int RobbedHateGain = 10;

		// Token: 0x04000064 RID: 100
		public static int RobbedGreedyExtra = 10;

		// Token: 0x04000065 RID: 101
		public static int RobbedPoorShift = 10;

		// Token: 0x04000066 RID: 102
		public static int RobbedRichShift = 10;

		// Token: 0x04000067 RID: 103
		public static float WildResentful = 1f;

		// Token: 0x04000068 RID: 104
		public static float WildVengeful = 0.8f;

		// Token: 0x04000069 RID: 105
		public static float WildMurderous = 0.45f;

		// Token: 0x0400006A RID: 106
		public static float TownResentful = 1f;

		// Token: 0x0400006B RID: 107
		public static float TownVengefulW = 0.6f;

		// Token: 0x0400006C RID: 108
		public static float TownMurderousW = 0.25f;

		// Token: 0x0400006D RID: 109
		public static int StandoffResentful = 14;

		// Token: 0x0400006E RID: 110
		public static int StandoffVengeful = 10;

		// Token: 0x0400006F RID: 111
		public static int StandoffMurderous = 6;

		// Token: 0x04000070 RID: 112
		public static int MurderousCooldownHours = 6;

		// Token: 0x04000071 RID: 113
		public static int ManCooldownHours = 12;

		// Token: 0x04000072 RID: 114
		public static int TownTopUpSeconds = 60;

		// Token: 0x04000073 RID: 115
		public static int TownTopUpChance = 65;

		// Token: 0x04000074 RID: 116
		public static int TownEncounterChance = 60;

		// Token: 0x04000075 RID: 117
		public static float TownCoreScale = 1f;

		// Token: 0x04000076 RID: 118
		public static int TownArrivalMin = 2;

		// Token: 0x04000077 RID: 119
		public static int TownArrivalMax = 4;

		// Token: 0x04000078 RID: 120
		public static int TownPassingPercent = 35;

		// Token: 0x04000079 RID: 121
		public static float TownPassingRange = 2500f;

		// Token: 0x0400007A RID: 122
		public static int TownArrivalWalkMin = 30;

		// Token: 0x0400007B RID: 123
		public static int TownArrivalWalkMax = 60;

		// Token: 0x0400007C RID: 124
		public static int TownArrivalIdleSeconds = 75;

		// Token: 0x0400007D RID: 125
		public static float TownDotNear = 35f;

		// Token: 0x0400007E RID: 126
		public static float TownDotFar = 55f;

		// Token: 0x0400007F RID: 127
		public static int TownArrivalGapMin = 8;

		// Token: 0x04000080 RID: 128
		public static int TownArrivalGapMax = 18;

		// Token: 0x04000081 RID: 129
		public static int TownOffLorePercent = 25;

		// Token: 0x04000082 RID: 130
		public static int TownSeatPercent = 55;

		// Token: 0x04000083 RID: 131
		public static int TownStrollPercent = 30;

		// Token: 0x04000084 RID: 132
		public static float MaskedUneaseMult = 3f;

		// Token: 0x04000085 RID: 133
		public static float MaskedUneaseRange = 15f;

		// Token: 0x04000086 RID: 134
		public static float GroupCalm = 0.4f;

		// Token: 0x04000087 RID: 135
		public static int MaskedRunBonus = 35;

		// Token: 0x04000088 RID: 136
		public static int UneaseCrowd = 4;

		// Token: 0x04000089 RID: 137
		public static float MaskedFloor = 0.5f;

		// Token: 0x0400008A RID: 138
		public static float MeleeUneaseMult = 0.5f;

		// Token: 0x0400008B RID: 139
		public static float MaskOnlyFear = 18f;

		// Token: 0x0400008C RID: 140
		public static float MaskOffDrain = 30f;

		// Token: 0x0400008D RID: 141
		public static int MaskedWalkOffCap = 40;

		// Token: 0x0400008E RID: 142
		public static int SaloonFightChance = 12;

		// Token: 0x0400008F RID: 143
		public static int TownMaxMen = 4;

		// Token: 0x04000090 RID: 144
		public static int NewNemesisRestHours = 24;

		// Token: 0x04000091 RID: 145
		public static int RespawnQuietSeconds = 180;

		// Token: 0x04000092 RID: 146
		public static int TownVengefulPct = 35;

		// Token: 0x04000093 RID: 147
		public static int TownMurderousPct = 15;

		// Token: 0x04000094 RID: 148
		public static int TownSecondMurderousPct = 15;

		// Token: 0x04000095 RID: 149
		public static int TownMurderousMinHate = 85;

		// Token: 0x04000096 RID: 150
		public static int TownSecondAggressorPct = 10;

		// Token: 0x04000097 RID: 151
		public static int TownQuietAfterFight = 600;

		// Token: 0x04000098 RID: 152
		public static int TownWaitOutsideBase = 45;

		// Token: 0x04000099 RID: 153
		public static int RoadAmbushMinutes = 15;

		// Token: 0x0400009A RID: 154
		public static float TownSpreadMeters = 25f;

		// Token: 0x0400009B RID: 155
		public static float GainClampMin = 0.5f;

		// Token: 0x0400009C RID: 156
		public static float GainClampMax = 1.8f;

		// Token: 0x0400009D RID: 157
		public static int TownEncounterSeconds = 45;

		// Token: 0x0400009E RID: 158
		public static int WontHuntSpotRange = 25;

		// Token: 0x0400009F RID: 159
		public static float NotorietyGainScale = 1f;

		// Token: 0x040000A0 RID: 160
		public static int NotorietyDecayPerDay = 2;

		// Token: 0x040000A1 RID: 161
		public static int UnnamedKillsPerPoint = 3;

		// Token: 0x040000A2 RID: 162
		public static int CampExtraNamed0 = 55;

		// Token: 0x040000A3 RID: 163
		public static int CampExtraNamed1 = 33;

		// Token: 0x040000A4 RID: 164
		public static int CampRaidChance = 30;

		// Token: 0x040000A5 RID: 165
		public static int WarCampRaidOutside = 18;

		// Token: 0x040000A6 RID: 166
		public static int WarCampRaidChance = 65;

		// Token: 0x040000A7 RID: 167
		public static int PatrolChance = 12;

		// Token: 0x040000A8 RID: 168
		public static float PatrolHearingRange = 600f;

		// Token: 0x040000A9 RID: 169
		public static int PatrolMountedPct = 60;

		// Token: 0x040000AA RID: 170
		public static int PatrolHurryPct = 50;

		// Token: 0x040000AB RID: 171
		public static int PatrolRespondMin = 150;

		// Token: 0x040000AC RID: 172
		public static int PatrolRespondMax = 200;

		// Token: 0x040000AD RID: 173
		public static int PatrolRespondChance = 10;

		// Token: 0x040000AE RID: 174
		public static int PatrolRespondCooldown = 900;

		// Token: 0x040000AF RID: 175
		public static int ArmyNotorietyGate = 25;

		// Token: 0x040000B0 RID: 176
		public static int ArmyBountyGate = 150;

		// Token: 0x040000B1 RID: 177
		public static int RaiderShakeupChance = 40;

		// Token: 0x040000B2 RID: 178
		public static int GsThreshold = 60;

		// Token: 0x040000B3 RID: 179
		public static int GsFloor = 25;

		// Token: 0x040000B4 RID: 180
		public static int GsSettleSeconds = 20;

		// Token: 0x040000B5 RID: 181
		public static int GreetStreakMinutes = 5;

		// Token: 0x040000B6 RID: 182
		public static float GreetStreakBonus = 3f;

		// Token: 0x040000B7 RID: 183
		public static float GsGreet = -6f;

		// Token: 0x040000B8 RID: 184
		public static float GsDefuse = -12f;

		// Token: 0x040000B9 RID: 185
		public static float GsInsult = 15f;

		// Token: 0x040000BA RID: 186
		public static float GsDrawOn = 6f;

		// Token: 0x040000BB RID: 187
		public static float GsIntimidate = 10f;

		// Token: 0x040000BC RID: 188
		public static float GsStickUp = 20f;

		// Token: 0x040000BD RID: 189
		public static float GsPunch = 8f;

		// Token: 0x040000BE RID: 190
		public static float GsKnockOut = 40f;

		// Token: 0x040000BF RID: 191
		public static float GsHogtie = 35f;

		// Token: 0x040000C0 RID: 192
		public static float GsRobbed = 45f;

		// Token: 0x040000C1 RID: 193
		public static float GsHorseStolen = 40f;

		// Token: 0x040000C2 RID: 194
		public static float GsSlashed = 55f;

		// Token: 0x040000C3 RID: 195
		public static float GsWounded = 50f;

		// Token: 0x040000C4 RID: 196
		public static float GsFoughtBack = 15f;

		// Token: 0x040000C5 RID: 197
		public static float GsShotAt = 10f;

		// Token: 0x040000C6 RID: 198
		public static float GsComradeKilled = 14f;

		// Token: 0x040000C7 RID: 199
		public static float GsShotAgain = 15f;

		// Token: 0x040000C8 RID: 200
		public static float GsUnhitCap = 55f;

		// Token: 0x040000C9 RID: 201
		public static float GsWoundCap = 58f;

		// Token: 0x040000CA RID: 202
		public static float GsReopen = 15f;

		// Token: 0x040000CB RID: 203
		public static float GsKindMax = 8f;

		// Token: 0x040000CC RID: 204
		public static float GsKindWaitMin = 5f;

		// Token: 0x040000CD RID: 205
		public static int SkirmishChance = 15;

		// Token: 0x040000CE RID: 206
		public static int InterroBaseBoy = 12;

		// Token: 0x040000CF RID: 207
		public static int InterroBaseCaptain = 8;

		// Token: 0x040000D0 RID: 208
		public static int InterroBaseLieutenant = 3;

		// Token: 0x040000D1 RID: 209
		public static int InterroBaseAmbient = 15;

		// Token: 0x040000D2 RID: 210
		public static int TerrorComradeKilled = 14;

		// Token: 0x040000D3 RID: 211
		public static int FearPerComradeKilled = 6;

		// Token: 0x040000D4 RID: 212
		public static int GoreFear = 45;

		// Token: 0x040000D5 RID: 213
		public static int TerrorLoot = 12;

		// Token: 0x040000D6 RID: 214
		public static int InterroFamilyFearExtra = 15;

		// Token: 0x040000D7 RID: 215
		public static int InterroTriesPerScare = 1;

		// Token: 0x040000D8 RID: 216
		public static int InterroScareToRetry = 10;

		// Token: 0x040000D9 RID: 217
		public static int TerrorHit = 6;

		// Token: 0x040000DA RID: 218
		public static int TerrorStab = 15;

		// Token: 0x040000DB RID: 219
		public static int TerrorShot = 28;

		// Token: 0x040000DC RID: 220
		public static int TerrorNearShot = 10;

		// Token: 0x040000DD RID: 221
		public static int TerrorThreat = 5;

		// Token: 0x040000DE RID: 222
		public static int TerrorAimPerSec = 2;
	}
}
