using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace NemesisSystem
{
	// Token: 0x02000043 RID: 67
	public class Acquaintance
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x06000C2E RID: 3118 RVA: 0x0010B83F File Offset: 0x00109A3F
		// (set) Token: 0x06000C2F RID: 3119 RVA: 0x0010B847 File Offset: 0x00109A47
		public long LastScaredMin { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x0010B850 File Offset: 0x00109A50
		// (set) Token: 0x06000C31 RID: 3121 RVA: 0x0010B858 File Offset: 0x00109A58
		public string Id { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000C32 RID: 3122 RVA: 0x0010B861 File Offset: 0x00109A61
		// (set) Token: 0x06000C33 RID: 3123 RVA: 0x0010B869 File Offset: 0x00109A69
		public string Name { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x0010B872 File Offset: 0x00109A72
		// (set) Token: 0x06000C35 RID: 3125 RVA: 0x0010B87A File Offset: 0x00109A7A
		public string Title { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x0010B883 File Offset: 0x00109A83
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x0010B88B File Offset: 0x00109A8B
		public int LifeRules { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x0010B894 File Offset: 0x00109A94
		[JsonIgnore]
		public string FullName
		{
			get
			{
				if (!string.IsNullOrEmpty(this.Title))
				{
					return this.Name + " " + this.Title;
				}
				return this.Name;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x0010B8C0 File Offset: 0x00109AC0
		// (set) Token: 0x06000C3A RID: 3130 RVA: 0x0010B8C8 File Offset: 0x00109AC8
		public uint Model { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000C3B RID: 3131 RVA: 0x0010B8D1 File Offset: 0x00109AD1
		// (set) Token: 0x06000C3C RID: 3132 RVA: 0x0010B8D9 File Offset: 0x00109AD9
		public string Town { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0010B8E2 File Offset: 0x00109AE2
		// (set) Token: 0x06000C3E RID: 3134 RVA: 0x0010B8EA File Offset: 0x00109AEA
		public bool Met { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0010B8F3 File Offset: 0x00109AF3
		// (set) Token: 0x06000C40 RID: 3136 RVA: 0x0010B8FB File Offset: 0x00109AFB
		public string Temper { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000C41 RID: 3137 RVA: 0x0010B904 File Offset: 0x00109B04
		// (set) Token: 0x06000C42 RID: 3138 RVA: 0x0010B90C File Offset: 0x00109B0C
		public int Grudge { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000C43 RID: 3139 RVA: 0x0010B915 File Offset: 0x00109B15
		// (set) Token: 0x06000C44 RID: 3140 RVA: 0x0010B91D File Offset: 0x00109B1D
		public bool GrudgeSettled { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0010B926 File Offset: 0x00109B26
		// (set) Token: 0x06000C46 RID: 3142 RVA: 0x0010B92E File Offset: 0x00109B2E
		public bool? SawFace { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000C47 RID: 3143 RVA: 0x0010B937 File Offset: 0x00109B37
		// (set) Token: 0x06000C48 RID: 3144 RVA: 0x0010B93F File Offset: 0x00109B3F
		public int TimesSeen { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x0010B948 File Offset: 0x00109B48
		// (set) Token: 0x06000C4A RID: 3146 RVA: 0x0010B950 File Offset: 0x00109B50
		public DateTime FirstSeen { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0010B959 File Offset: 0x00109B59
		// (set) Token: 0x06000C4C RID: 3148 RVA: 0x0010B961 File Offset: 0x00109B61
		public DateTime LastSeen { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000C4D RID: 3149 RVA: 0x0010B96A File Offset: 0x00109B6A
		// (set) Token: 0x06000C4E RID: 3150 RVA: 0x0010B972 File Offset: 0x00109B72
		public DateTime LastDealt { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000C4F RID: 3151 RVA: 0x0010B97B File Offset: 0x00109B7B
		// (set) Token: 0x06000C50 RID: 3152 RVA: 0x0010B983 File Offset: 0x00109B83
		public DateTime LastSpawned { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x0010B98C File Offset: 0x00109B8C
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x0010B994 File Offset: 0x00109B94
		public bool Dead { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x0010B99D File Offset: 0x00109B9D
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x0010B9A5 File Offset: 0x00109BA5
		public string Note { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x0010B9AE File Offset: 0x00109BAE
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x0010B9B6 File Offset: 0x00109BB6
		public int Favours { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x0010B9BF File Offset: 0x00109BBF
		// (set) Token: 0x06000C58 RID: 3160 RVA: 0x0010B9C7 File Offset: 0x00109BC7
		public int Fear { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x0010B9D0 File Offset: 0x00109BD0
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x0010B9D8 File Offset: 0x00109BD8
		public List<string> Witnessed { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000C5B RID: 3163 RVA: 0x0010B9E1 File Offset: 0x00109BE1
		// (set) Token: 0x06000C5C RID: 3164 RVA: 0x0010B9E9 File Offset: 0x00109BE9
		public int Like { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x0010B9F2 File Offset: 0x00109BF2
		// (set) Token: 0x06000C5E RID: 3166 RVA: 0x0010B9FA File Offset: 0x00109BFA
		public bool Reported { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000C5F RID: 3167 RVA: 0x0010BA03 File Offset: 0x00109C03
		// (set) Token: 0x06000C60 RID: 3168 RVA: 0x0010BA0B File Offset: 0x00109C0B
		public bool KeptQuiet { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x0010BA14 File Offset: 0x00109C14
		// (set) Token: 0x06000C62 RID: 3170 RVA: 0x0010BA1C File Offset: 0x00109C1C
		public List<string> FavourNotes { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0010BA25 File Offset: 0x00109C25
		// (set) Token: 0x06000C64 RID: 3172 RVA: 0x0010BA2D File Offset: 0x00109C2D
		public string Affil { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000C65 RID: 3173 RVA: 0x0010BA36 File Offset: 0x00109C36
		// (set) Token: 0x06000C66 RID: 3174 RVA: 0x0010BA3E File Offset: 0x00109C3E
		public long LastGameMin { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x0010BA47 File Offset: 0x00109C47
		// (set) Token: 0x06000C68 RID: 3176 RVA: 0x0010BA4F File Offset: 0x00109C4F
		public bool Lookout { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000C69 RID: 3177 RVA: 0x0010BA58 File Offset: 0x00109C58
		// (set) Token: 0x06000C6A RID: 3178 RVA: 0x0010BA60 File Offset: 0x00109C60
		public int LastGreetDay { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x0010BA69 File Offset: 0x00109C69
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x0010BA71 File Offset: 0x00109C71
		public bool LikeSpread { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x0010BA7A File Offset: 0x00109C7A
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x0010BA82 File Offset: 0x00109C82
		public List<string> Circle { get; set; }
	}
}
