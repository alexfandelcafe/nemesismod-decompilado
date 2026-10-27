using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000034 RID: 52
	public class EncounterCard
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0010A88F File Offset: 0x00108A8F
		// (set) Token: 0x06000BA2 RID: 2978 RVA: 0x0010A897 File Offset: 0x00108A97
		public string Id { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0010A8A0 File Offset: 0x00108AA0
		// (set) Token: 0x06000BA4 RID: 2980 RVA: 0x0010A8A8 File Offset: 0x00108AA8
		public string Title { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0010A8B1 File Offset: 0x00108AB1
		// (set) Token: 0x06000BA6 RID: 2982 RVA: 0x0010A8B9 File Offset: 0x00108AB9
		public string Notes { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0010A8C2 File Offset: 0x00108AC2
		// (set) Token: 0x06000BA8 RID: 2984 RVA: 0x0010A8CA File Offset: 0x00108ACA
		public bool Enabled { get; set; } = true;

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000BA9 RID: 2985 RVA: 0x0010A8D3 File Offset: 0x00108AD3
		// (set) Token: 0x06000BAA RID: 2986 RVA: 0x0010A8DB File Offset: 0x00108ADB
		public string Stage { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0010A8E4 File Offset: 0x00108AE4
		// (set) Token: 0x06000BAC RID: 2988 RVA: 0x0010A8EC File Offset: 0x00108AEC
		public string Faction { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0010A8F5 File Offset: 0x00108AF5
		// (set) Token: 0x06000BAE RID: 2990 RVA: 0x0010A8FD File Offset: 0x00108AFD
		public CardWhen When { get; set; } = new CardWhen();

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0010A906 File Offset: 0x00108B06
		// (set) Token: 0x06000BB0 RID: 2992 RVA: 0x0010A90E File Offset: 0x00108B0E
		public int Weight { get; set; } = 10;

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000BB1 RID: 2993 RVA: 0x0010A917 File Offset: 0x00108B17
		// (set) Token: 0x06000BB2 RID: 2994 RVA: 0x0010A91F File Offset: 0x00108B1F
		public string IniWeight { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x0010A928 File Offset: 0x00108B28
		// (set) Token: 0x06000BB4 RID: 2996 RVA: 0x0010A930 File Offset: 0x00108B30
		public int Chance { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0010A939 File Offset: 0x00108B39
		// (set) Token: 0x06000BB6 RID: 2998 RVA: 0x0010A941 File Offset: 0x00108B41
		public string IniChance { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000BB7 RID: 2999 RVA: 0x0010A94A File Offset: 0x00108B4A
		// (set) Token: 0x06000BB8 RID: 3000 RVA: 0x0010A952 File Offset: 0x00108B52
		public int CooldownMinutes { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000BB9 RID: 3001 RVA: 0x0010A95B File Offset: 0x00108B5B
		// (set) Token: 0x06000BBA RID: 3002 RVA: 0x0010A963 File Offset: 0x00108B63
		public int TensionMin { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000BBB RID: 3003 RVA: 0x0010A96C File Offset: 0x00108B6C
		// (set) Token: 0x06000BBC RID: 3004 RVA: 0x0010A974 File Offset: 0x00108B74
		public int TensionMax { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0010A97D File Offset: 0x00108B7D
		// (set) Token: 0x06000BBE RID: 3006 RVA: 0x0010A985 File Offset: 0x00108B85
		public CardCast Cast { get; set; } = new CardCast();

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0010A98E File Offset: 0x00108B8E
		// (set) Token: 0x06000BC0 RID: 3008 RVA: 0x0010A996 File Offset: 0x00108B96
		public List<string> Taunts { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000BC1 RID: 3009 RVA: 0x0010A99F File Offset: 0x00108B9F
		// (set) Token: 0x06000BC2 RID: 3010 RVA: 0x0010A9A7 File Offset: 0x00108BA7
		public string Voice { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000BC3 RID: 3011 RVA: 0x0010A9B0 File Offset: 0x00108BB0
		// (set) Token: 0x06000BC4 RID: 3012 RVA: 0x0010A9B8 File Offset: 0x00108BB8
		public bool? Standoff { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000BC5 RID: 3013 RVA: 0x0010A9C1 File Offset: 0x00108BC1
		// (set) Token: 0x06000BC6 RID: 3014 RVA: 0x0010A9C9 File Offset: 0x00108BC9
		public int StandoffSeconds { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000BC7 RID: 3015 RVA: 0x0010A9D2 File Offset: 0x00108BD2
		// (set) Token: 0x06000BC8 RID: 3016 RVA: 0x0010A9DA File Offset: 0x00108BDA
		public float SightRange { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000BC9 RID: 3017 RVA: 0x0010A9E3 File Offset: 0x00108BE3
		// (set) Token: 0x06000BCA RID: 3018 RVA: 0x0010A9EB File Offset: 0x00108BEB
		public string Activity { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000BCB RID: 3019 RVA: 0x0010A9F4 File Offset: 0x00108BF4
		// (set) Token: 0x06000BCC RID: 3020 RVA: 0x0010A9FC File Offset: 0x00108BFC
		public int BackDownHate { get; set; } = -5;

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x0010AA05 File Offset: 0x00108C05
		// (set) Token: 0x06000BCE RID: 3022 RVA: 0x0010AA0D File Offset: 0x00108C0D
		public string History { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x0010AA16 File Offset: 0x00108C16
		// (set) Token: 0x06000BD0 RID: 3024 RVA: 0x0010AA1E File Offset: 0x00108C1E
		public bool EndsPeacefully { get; set; }
	}
}
