using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000044 RID: 68
	internal class Plan
	{
		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0010BA93 File Offset: 0x00109C93
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x0010BA9B File Offset: 0x00109C9B
		public string Id { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0010BAA4 File Offset: 0x00109CA4
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x0010BAAC File Offset: 0x00109CAC
		public string Gang { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0010BAB5 File Offset: 0x00109CB5
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x0010BABD File Offset: 0x00109CBD
		public string Kind { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0010BAC6 File Offset: 0x00109CC6
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x0010BACE File Offset: 0x00109CCE
		public string Title { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0010BAD7 File Offset: 0x00109CD7
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0010BADF File Offset: 0x00109CDF
		public string Place { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0010BAE8 File Offset: 0x00109CE8
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x0010BAF0 File Offset: 0x00109CF0
		public float X { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0010BAF9 File Offset: 0x00109CF9
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0010BB01 File Offset: 0x00109D01
		public float Y { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0010BB0A File Offset: 0x00109D0A
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x0010BB12 File Offset: 0x00109D12
		public float Z { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000C80 RID: 3200 RVA: 0x0010BB1B File Offset: 0x00109D1B
		// (set) Token: 0x06000C81 RID: 3201 RVA: 0x0010BB23 File Offset: 0x00109D23
		public long DueAt { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000C82 RID: 3202 RVA: 0x0010BB2C File Offset: 0x00109D2C
		// (set) Token: 0x06000C83 RID: 3203 RVA: 0x0010BB34 File Offset: 0x00109D34
		public int WindowMin { get; set; } = 180;

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x0010BB3D File Offset: 0x00109D3D
		// (set) Token: 0x06000C85 RID: 3205 RVA: 0x0010BB45 File Offset: 0x00109D45
		public string LeaderId { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x0010BB4E File Offset: 0x00109D4E
		// (set) Token: 0x06000C87 RID: 3207 RVA: 0x0010BB56 File Offset: 0x00109D56
		public int Crew { get; set; } = 3;

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0010BB5F File Offset: 0x00109D5F
		// (set) Token: 0x06000C89 RID: 3209 RVA: 0x0010BB67 File Offset: 0x00109D67
		public bool Known { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0010BB70 File Offset: 0x00109D70
		// (set) Token: 0x06000C8B RID: 3211 RVA: 0x0010BB78 File Offset: 0x00109D78
		public bool Lie { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0010BB81 File Offset: 0x00109D81
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x0010BB89 File Offset: 0x00109D89
		public string ToldBy { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0010BB92 File Offset: 0x00109D92
		// (set) Token: 0x06000C8F RID: 3215 RVA: 0x0010BB9A File Offset: 0x00109D9A
		public string ToldAbout { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0010BBA3 File Offset: 0x00109DA3
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x0010BBAB File Offset: 0x00109DAB
		public bool Live { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x0010BBB4 File Offset: 0x00109DB4
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x0010BBBC File Offset: 0x00109DBC
		public bool Visited { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0010BBC5 File Offset: 0x00109DC5
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x0010BBCD File Offset: 0x00109DCD
		public bool Done { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x0010BBD6 File Offset: 0x00109DD6
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x0010BBDE File Offset: 0x00109DDE
		public string Outcome { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x0010BBE7 File Offset: 0x00109DE7
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0010BBEF File Offset: 0x00109DEF
		public string Habit { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x0010BBF8 File Offset: 0x00109DF8
		// (set) Token: 0x06000C9B RID: 3227 RVA: 0x0010BC00 File Offset: 0x00109E00
		public string Dead { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x0010BC09 File Offset: 0x00109E09
		// (set) Token: 0x06000C9D RID: 3229 RVA: 0x0010BC11 File Offset: 0x00109E11
		public List<string> MoreDead { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x0010BC1A File Offset: 0x00109E1A
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x0010BC22 File Offset: 0x00109E22
		public int ScarsAt { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x0010BC2B File Offset: 0x00109E2B
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x0010BC33 File Offset: 0x00109E33
		public bool Desperate { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x0010BC3C File Offset: 0x00109E3C
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x0010BC44 File Offset: 0x00109E44
		public bool Engaged { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000CA4 RID: 3236 RVA: 0x0010BC4D File Offset: 0x00109E4D
		// (set) Token: 0x06000CA5 RID: 3237 RVA: 0x0010BC55 File Offset: 0x00109E55
		public bool PlotSet { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0010BC5E File Offset: 0x00109E5E
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x0010BC66 File Offset: 0x00109E66
		public float PlotX { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000CA8 RID: 3240 RVA: 0x0010BC6F File Offset: 0x00109E6F
		// (set) Token: 0x06000CA9 RID: 3241 RVA: 0x0010BC77 File Offset: 0x00109E77
		public float PlotY { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000CAA RID: 3242 RVA: 0x0010BC80 File Offset: 0x00109E80
		// (set) Token: 0x06000CAB RID: 3243 RVA: 0x0010BC88 File Offset: 0x00109E88
		public float PlotZ { get; set; }
	}
}
