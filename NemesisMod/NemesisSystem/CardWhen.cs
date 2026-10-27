using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000032 RID: 50
	public class CardWhen
	{
		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x0010A742 File Offset: 0x00108942
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x0010A74A File Offset: 0x0010894A
		public string Place { get; set; } = "any";

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000B7D RID: 2941 RVA: 0x0010A753 File Offset: 0x00108953
		// (set) Token: 0x06000B7E RID: 2942 RVA: 0x0010A75B File Offset: 0x0010895B
		public bool? Mounted { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000B7F RID: 2943 RVA: 0x0010A764 File Offset: 0x00108964
		// (set) Token: 0x06000B80 RID: 2944 RVA: 0x0010A76C File Offset: 0x0010896C
		public List<string> Tiers { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000B81 RID: 2945 RVA: 0x0010A775 File Offset: 0x00108975
		// (set) Token: 0x06000B82 RID: 2946 RVA: 0x0010A77D File Offset: 0x0010897D
		public List<string> Intents { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x0010A786 File Offset: 0x00108986
		// (set) Token: 0x06000B84 RID: 2948 RVA: 0x0010A78E File Offset: 0x0010898E
		public List<string> Factions { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0010A797 File Offset: 0x00108997
		// (set) Token: 0x06000B86 RID: 2950 RVA: 0x0010A79F File Offset: 0x0010899F
		public List<string> NotFactions { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0010A7A8 File Offset: 0x001089A8
		// (set) Token: 0x06000B88 RID: 2952 RVA: 0x0010A7B0 File Offset: 0x001089B0
		public List<string> Tempers { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0010A7B9 File Offset: 0x001089B9
		// (set) Token: 0x06000B8A RID: 2954 RVA: 0x0010A7C1 File Offset: 0x001089C1
		public int MinRank { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0010A7CA File Offset: 0x001089CA
		// (set) Token: 0x06000B8C RID: 2956 RVA: 0x0010A7D2 File Offset: 0x001089D2
		public int MaxRank { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0010A7DB File Offset: 0x001089DB
		// (set) Token: 0x06000B8E RID: 2958 RVA: 0x0010A7E3 File Offset: 0x001089E3
		public List<int> Hours { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000B8F RID: 2959 RVA: 0x0010A7EC File Offset: 0x001089EC
		// (set) Token: 0x06000B90 RID: 2960 RVA: 0x0010A7F4 File Offset: 0x001089F4
		public List<string> Regions { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0010A7FD File Offset: 0x001089FD
		// (set) Token: 0x06000B92 RID: 2962 RVA: 0x0010A805 File Offset: 0x00108A05
		public int MinNotoriety { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0010A80E File Offset: 0x00108A0E
		// (set) Token: 0x06000B94 RID: 2964 RVA: 0x0010A816 File Offset: 0x00108A16
		public int MinHate { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0010A81F File Offset: 0x00108A1F
		// (set) Token: 0x06000B96 RID: 2966 RVA: 0x0010A827 File Offset: 0x00108A27
		public bool KilledYou { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x0010A830 File Offset: 0x00108A30
		// (set) Token: 0x06000B98 RID: 2968 RVA: 0x0010A838 File Offset: 0x00108A38
		public List<string> LastOutcome { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0010A841 File Offset: 0x00108A41
		// (set) Token: 0x06000B9A RID: 2970 RVA: 0x0010A849 File Offset: 0x00108A49
		public int MinArcStep { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0010A852 File Offset: 0x00108A52
		// (set) Token: 0x06000B9C RID: 2972 RVA: 0x0010A85A File Offset: 0x00108A5A
		public List<string> Weather { get; set; }
	}
}
