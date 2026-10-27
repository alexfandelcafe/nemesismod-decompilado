using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using RDR2;

namespace NemesisSystem
{
	// Token: 0x02000019 RID: 25
	internal sealed class Gang
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x000F24D8 File Offset: 0x000F06D8
		public bool IsMemberModel(uint model)
		{
			string text = Models.Name(model);
			return text != "" && this.ModelTokens.Any(new Func<string, bool>(text.Contains));
		}

		// Token: 0x04000730 RID: 1840
		public string Id;

		// Token: 0x04000731 RID: 1841
		public string Name;

		// Token: 0x04000732 RID: 1842
		public string[] ModelTokens;

		// Token: 0x04000733 RID: 1843
		public string LeaderName;

		// Token: 0x04000734 RID: 1844
		public string LeaderBlurb;

		// Token: 0x04000735 RID: 1845
		public string LeaderModel;

		// Token: 0x04000736 RID: 1846
		public float[] LeaderSpot;

		// Token: 0x04000737 RID: 1847
		public string LoreLeader;

		// Token: 0x04000738 RID: 1848
		public string LoreBlurb;

		// Token: 0x04000739 RID: 1849
		public string[] FirstNames;

		// Token: 0x0400073A RID: 1850
		public string[] LastNames;

		// Token: 0x0400073B RID: 1851
		public string[] Titles;

		// Token: 0x0400073C RID: 1852
		[TupleElementNames(new string[]
		{
			"Rank",
			"Count",
			"Models"
		})]
		public ValueTuple<int, int, PedHash[]>[] Seed;

		// Token: 0x0400073D RID: 1853
		[TupleElementNames(new string[]
		{
			"Label",
			"MinRank",
			"Seats"
		})]
		public ValueTuple<string, int, int>[] Tiers;

		// Token: 0x0400073E RID: 1854
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public ValueTuple<int, int, int> Accent = new ValueTuple<int, int, int>(206, 200, 188);

		// Token: 0x0400073F RID: 1855
		public bool Placeholder;

		// Token: 0x04000740 RID: 1856
		public double AmbushBase = 1.0;

		// Token: 0x04000741 RID: 1857
		public double EncounterRate = 1.0;

		// Token: 0x04000742 RID: 1858
		public bool OrdersNotFeelings;

		// Token: 0x04000743 RID: 1859
		public int FixedHate = 80;

		// Token: 0x04000744 RID: 1860
		public bool UniformKit;

		// Token: 0x04000745 RID: 1861
		public eWeapon UniformSide = 2075992054;

		// Token: 0x04000746 RID: 1862
		public eWeapon UniformLong = -183018591;

		// Token: 0x04000747 RID: 1863
		public string UniformSideName = "un revólver Schofield";

		// Token: 0x04000748 RID: 1864
		public string UniformLongName = "una carabina";

		// Token: 0x04000749 RID: 1865
		[TupleElementNames(new string[]
		{
			"w",
			"name",
			"weight",
			"minRank"
		})]
		public ValueTuple<eWeapon, string, int, int>[] ExtraSidearms = new ValueTuple<eWeapon, string, int, int>[0];

		// Token: 0x0400074A RID: 1866
		[TupleElementNames(new string[]
		{
			"w",
			"name",
			"weight"
		})]
		public ValueTuple<eWeapon, string, int>[] ExtraLongGuns = new ValueTuple<eWeapon, string, int>[0];

		// Token: 0x0400074B RID: 1867
		public int OldArmyKnifeWeight = 1;

		// Token: 0x0400074C RID: 1868
		public bool NeedsReason;

		// Token: 0x0400074D RID: 1869
		public bool SquadsOnly;

		// Token: 0x0400074E RID: 1870
		public bool RankInName;

		// Token: 0x0400074F RID: 1871
		public bool KeepsToItsOwn;

		// Token: 0x04000750 RID: 1872
		public bool Feuds = true;

		// Token: 0x04000751 RID: 1873
		public bool GangLifeEvents = true;

		// Token: 0x04000752 RID: 1874
		public bool OwnLifeOnly;

		// Token: 0x04000753 RID: 1875
		public bool MixedFootSoldiers;

		// Token: 0x04000754 RID: 1876
		public double WeightNorth = 1.0;

		// Token: 0x04000755 RID: 1877
		public double WeightSouth = 1.0;

		// Token: 0x04000756 RID: 1878
		public double WeightElsewhere = 1.0;

		// Token: 0x04000757 RID: 1879
		public double WeightNearFort = -1.0;

		// Token: 0x04000758 RID: 1880
		public HashSet<uint> OnlyDistricts;

		// Token: 0x04000759 RID: 1881
		public bool NoTowns;

		// Token: 0x0400075A RID: 1882
		public double DayWeight = 1.0;

		// Token: 0x0400075B RID: 1883
		public double NightWeight = 1.0;

		// Token: 0x0400075C RID: 1884
		public string[] TownModels;

		// Token: 0x0400075D RID: 1885
		public int TownChancePercent = 30;

		// Token: 0x0400075E RID: 1886
		public double DistrictWeight = 1.5;

		// Token: 0x0400075F RID: 1887
		public int? CombatMovement;

		// Token: 0x04000760 RID: 1888
		public int? CombatAbility;

		// Token: 0x04000761 RID: 1889
		public int? CombatRange;

		// Token: 0x04000762 RID: 1890
		public int AccuracyMin;

		// Token: 0x04000763 RID: 1891
		public int AccuracyMax = 100;

		// Token: 0x04000764 RID: 1892
		public bool MayFlee;

		// Token: 0x04000765 RID: 1893
		public string[] Faiths;

		// Token: 0x04000766 RID: 1894
		public Wealth? MemberWealth;

		// Token: 0x04000767 RID: 1895
		public string[] MemberTrades;

		// Token: 0x04000768 RID: 1896
		public PedHash[] TroopModels = new PedHash[0];

		// Token: 0x04000769 RID: 1897
		public bool TroopLongGuns;

		// Token: 0x0400076A RID: 1898
		public int TroopAccuracy = 22;

		// Token: 0x0400076B RID: 1899
		public string PatrolsIn;

		// Token: 0x0400076C RID: 1900
		public bool HasCamps;

		// Token: 0x0400076D RID: 1901
		public bool TribalKit;

		// Token: 0x0400076E RID: 1902
		public eWeapon KitBow = -2002235300;

		// Token: 0x0400076F RID: 1903
		public eWeapon KitBlade = 1147288274;

		// Token: 0x04000770 RID: 1904
		public string KitBowName = "un arco";

		// Token: 0x04000771 RID: 1905
		public string KitBladeName = "un tomahawk";

		// Token: 0x04000772 RID: 1906
		public int CrewExtra;

		// Token: 0x04000773 RID: 1907
		public bool StoryOnly;

		// Token: 0x04000774 RID: 1908
		public bool ReasonFromBounty = true;

		// Token: 0x04000775 RID: 1909
		public bool FamilyNames = true;

		// Token: 0x04000776 RID: 1910
		public int? UnnamedKillsPerPoint;

		// Token: 0x04000777 RID: 1911
		[TupleElementNames(new string[]
		{
			"min",
			"max"
		})]
		public ValueTuple<int, int>? SeedHate;

		// Token: 0x04000778 RID: 1912
		public string[] ExcludedNames;

		// Token: 0x04000779 RID: 1913
		public Dictionary<uint, double> DistrictWeights;

		// Token: 0x0400077A RID: 1914
		public bool OwnLives;

		// Token: 0x0400077B RID: 1915
		public bool AnswersGunfire = true;

		// Token: 0x0400077C RID: 1916
		public string HomeTown;

		// Token: 0x0400077D RID: 1917
		[TupleElementNames(new string[]
		{
			"title",
			"place",
			"crew"
		})]
		public ValueTuple<string, string, int>[] Jobs;

		// Token: 0x0400077E RID: 1918
		public string[] JobTowns;

		// Token: 0x0400077F RID: 1919
		public Dictionary<string, int> Relations = new Dictionary<string, int>();

		// Token: 0x04000780 RID: 1920
		public bool TurfWars = true;

		// Token: 0x04000781 RID: 1921
		public bool LoreLeaderProtected;

		// Token: 0x04000782 RID: 1922
		public int Era;
	}
}
