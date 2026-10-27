using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x02000039 RID: 57
	internal sealed class CrimeProfile
	{
		// Token: 0x04000862 RID: 2146
		public bool You;

		// Token: 0x04000863 RID: 2147
		public string BannerLabel;

		// Token: 0x04000864 RID: 2148
		public string BannerValue;

		// Token: 0x04000865 RID: 2149
		public string BannerWord;

		// Token: 0x04000866 RID: 2150
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public ValueTuple<int, int, int> BannerCol;

		// Token: 0x04000867 RID: 2151
		public string Side;

		// Token: 0x04000868 RID: 2152
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public ValueTuple<int, int, int> SideCol;

		// Token: 0x04000869 RID: 2153
		public bool SideFilled;

		// Token: 0x0400086A RID: 2154
		public int Rep = int.MinValue;

		// Token: 0x0400086B RID: 2155
		public string CrimesTitle;

		// Token: 0x0400086C RID: 2156
		public string CrimesNote;

		// Token: 0x0400086D RID: 2157
		[TupleElementNames(new string[]
		{
			"when",
			"text",
			"badge"
		})]
		public List<ValueTuple<string, string, string>> Crimes = new List<ValueTuple<string, string, string>>();

		// Token: 0x0400086E RID: 2158
		[TupleElementNames(new string[]
		{
			"kind",
			"kindCol",
			"title",
			"outcome",
			"outCol",
			"r",
			"g",
			"b",
			"r",
			"g",
			"b"
		})]
		public List<ValueTuple<string, ValueTuple<int, int, int>, string, string, ValueTuple<int, int, int>>> Events = new List<ValueTuple<string, ValueTuple<int, int, int>, string, string, ValueTuple<int, int, int>>>();

		// Token: 0x0400086F RID: 2159
		public string EventsNote;

		// Token: 0x04000870 RID: 2160
		public string TilesTitle;

		// Token: 0x04000871 RID: 2161
		[TupleElementNames(new string[]
		{
			"num",
			"label",
			"col",
			"r",
			"g",
			"b"
		})]
		public List<ValueTuple<string, string, ValueTuple<int, int, int>>> Tiles = new List<ValueTuple<string, string, ValueTuple<int, int, int>>>();

		// Token: 0x04000872 RID: 2162
		public List<string> Dead = new List<string>();

		// Token: 0x04000873 RID: 2163
		public int DeadMore;

		// Token: 0x04000874 RID: 2164
		[TupleElementNames(new string[]
		{
			"num",
			"label",
			"col",
			"r",
			"g",
			"b"
		})]
		public List<ValueTuple<string, string, ValueTuple<int, int, int>>> Witnesses = new List<ValueTuple<string, string, ValueTuple<int, int, int>>>();

		// Token: 0x04000875 RID: 2165
		[TupleElementNames(new string[]
		{
			"town",
			"rep"
		})]
		public List<ValueTuple<string, int>> Towns = new List<ValueTuple<string, int>>();

		// Token: 0x04000876 RID: 2166
		[TupleElementNames(new string[]
		{
			"name",
			"value",
			"col",
			"r",
			"g",
			"b"
		})]
		public List<ValueTuple<string, int, ValueTuple<int, int, int>>> Factions = new List<ValueTuple<string, int, ValueTuple<int, int, int>>>();
	}
}
