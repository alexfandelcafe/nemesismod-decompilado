using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x02000040 RID: 64
	internal sealed class Newspaper
	{
		// Token: 0x040008D5 RID: 2261
		public string Name;

		// Token: 0x040008D6 RID: 2262
		public string Town;

		// Token: 0x040008D7 RID: 2263
		public string Date;

		// Token: 0x040008D8 RID: 2264
		public string Banner = "ÚLTIMAS NOTICIAS";

		// Token: 0x040008D9 RID: 2265
		public int Day;

		// Token: 0x040008DA RID: 2266
		public Article Lead;

		// Token: 0x040008DB RID: 2267
		public readonly List<Article> Stories = new List<Article>();

		// Token: 0x040008DC RID: 2268
		public readonly List<string> About = new List<string>();

		// Token: 0x040008DD RID: 2269
		public readonly List<string> Wanted = new List<string>();

		// Token: 0x040008DE RID: 2270
		public readonly List<string> Wars = new List<string>();

		// Token: 0x040008DF RID: 2271
		public readonly List<string> Further = new List<string>();

		// Token: 0x040008E0 RID: 2272
		[TupleElementNames(new string[]
		{
			"Name",
			"Of",
			"When",
			"Where",
			"Until"
		})]
		public readonly List<ValueTuple<string, string, string, string, string>> Funerals = new List<ValueTuple<string, string, string, string, string>>();

		// Token: 0x040008E1 RID: 2273
		public readonly List<Plan> FuneralPlans = new List<Plan>();

		// Token: 0x040008E2 RID: 2274
		public int Layout;

		// Token: 0x040008E3 RID: 2275
		public string Ad;
	}
}
