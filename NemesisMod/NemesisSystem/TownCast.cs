using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x02000076 RID: 118
	internal static class TownCast
	{
		// Token: 0x060012F8 RID: 4856 RVA: 0x0012F0F8 File Offset: 0x0012D2F8
		public static string NameFor(string modelLower)
		{
			if (string.IsNullOrEmpty(modelLower))
			{
				return null;
			}
			string result;
			if (TownCast.KnownNames.TryGetValue(modelLower, out result))
			{
				return result;
			}
			string[] array = modelLower.Split(new char[]
			{
				'_'
			});
			string core;
			if (array[0] == "u" && array.Length >= 4)
			{
				core = array[3];
			}
			else
			{
				if (!(array[0] == "cs") || array.Length < 2)
				{
					return null;
				}
				core = array[1];
			}
			ValueTuple<string, string> valueTuple = TownCast.Jobs.FirstOrDefault(([TupleElementNames(new string[]
			{
				"token",
				"job"
			})] ValueTuple<string, string> j) => core.Contains(j.Item1));
			if (valueTuple.Item1 == null)
			{
				return null;
			}
			ValueTuple<string, string> valueTuple2 = TownCast.Towns.FirstOrDefault(([TupleElementNames(new string[]
			{
				"token",
				"town"
			})] ValueTuple<string, string> t) => core.StartsWith(t.Item1));
			string text = ((valueTuple2.Item1 != null) ? (valueTuple2.Item2 + " ") : "") + valueTuple.Item2;
			return char.ToUpperInvariant(text[0]).ToString() + text.Substring(1);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x0012F204 File Offset: 0x0012D404
		// Note: this type is marked as 'beforefieldinit'.
		static TownCast()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["u_m_m_bht_benedictallbright"] = "Benedict Allbright";
			dictionary["u_m_m_bht_nathankirk"] = "Nathan Kirk";
			dictionary["u_m_m_bht_laramiesleeping"] = "Bart Cavanaugh";
			dictionary["u_m_m_bht_strawberryduel"] = "Joshua Brown";
			dictionary["u_f_m_bht_wife"] = "Ellie Anne Swan";
			dictionary["u_m_m_bht_lover"] = "Ellie Anne Swan's lover";
			dictionary["u_m_m_bht_exconfedcampreturn"] = "Camille de Millemont";
			dictionary["u_m_o_bht_docwormwood"] = "J.P. \"Doc\" Wormwood";
			dictionary["u_m_y_duellistbounty_01"] = "Joshua Brown";
			dictionary["u_m_m_bht_outlawmauled"] = "Jim McGoGarty";
			dictionary["u_m_m_bht_skinnersearch"] = "a wanted man";
			dictionary["u_m_m_bht_blackwaterhunt"] = "a wanted man";
			dictionary["u_m_m_bht_shackescape"] = "a wanted man";
			dictionary["u_m_m_bht_banditomine"] = "a wanted bandito";
			dictionary["u_m_m_bht_banditoshack"] = "a wanted bandito";
			dictionary["u_m_m_bht_mineforeman"] = "a wanted mine foreman";
			dictionary["u_m_m_bht_saintdenissaloon"] = "a wanted man";
			dictionary["u_m_m_bht_oldman"] = "a wanted old man";
			dictionary["u_m_m_htlrancherbounty_01"] = "a wanted rancher";
			dictionary["u_m_m_nbxbankerbounty_01"] = "a wanted banker";
			TownCast.KnownNames = dictionary;
			TownCast.Towns = new ValueTuple<string, string>[]
			{
				new ValueTuple<string, string>("val", "Valentine"),
				new ValueTuple<string, string>("rhd", "Rhodes"),
				new ValueTuple<string, string>("rho", "Rhodes"),
				new ValueTuple<string, string>("nbx", "Saint Denis"),
				new ValueTuple<string, string>("sd", "Saint Denis"),
				new ValueTuple<string, string>("str", "Strawberry"),
				new ValueTuple<string, string>("blw", "Blackwater"),
				new ValueTuple<string, string>("asb", "Annesburg"),
				new ValueTuple<string, string>("vht", "Van Horn"),
				new ValueTuple<string, string>("arm", "Armadillo"),
				new ValueTuple<string, string>("tum", "Tumbleweed"),
				new ValueTuple<string, string>("wal", "Wallace Station"),
				new ValueTuple<string, string>("emr", "Emerald Ranch"),
				new ValueTuple<string, string>("rig", "Riggs Station"),
				new ValueTuple<string, string>("bwm", "Bluewater Marsh")
			};
			TownCast.Jobs = new ValueTuple<string, string>[]
			{
				new ValueTuple<string, string>("generalstoreowner", "general store"),
				new ValueTuple<string, string>("genstoreowner", "general store"),
				new ValueTuple<string, string>("trainstationworker", "station agent"),
				new ValueTuple<string, string>("freightstationowner", "freight station"),
				new ValueTuple<string, string>("exoticsshopkeeper", "exotics shop"),
				new ValueTuple<string, string>("exoticshopkeeper", "exotics shop"),
				new ValueTuple<string, string>("boatticketseller", "ticket seller"),
				new ValueTuple<string, string>("welcomecenter", "welcome centre"),
				new ValueTuple<string, string>("auctionforman", "auctioneer"),
				new ValueTuple<string, string>("hotelowner", "hotel owner"),
				new ValueTuple<string, string>("liveryworker", "stable hand"),
				new ValueTuple<string, string>("stablehand", "stable hand"),
				new ValueTuple<string, string>("policechief", "police chief"),
				new ValueTuple<string, string>("bankguard", "bank guard"),
				new ValueTuple<string, string>("stationclerk", "station clerk"),
				new ValueTuple<string, string>("doctor", "doctor"),
				new ValueTuple<string, string>("gunsmith", "gunsmith"),
				new ValueTuple<string, string>("butcher", "butcher"),
				new ValueTuple<string, string>("barber", "barber"),
				new ValueTuple<string, string>("bartender", "bartender"),
				new ValueTuple<string, string>("sherriff", "sheriff"),
				new ValueTuple<string, string>("sheriff", "sheriff"),
				new ValueTuple<string, string>("undertaker", "undertaker"),
				new ValueTuple<string, string>("preacher", "preacher"),
				new ValueTuple<string, string>("priest", "priest"),
				new ValueTuple<string, string>("photographer", "photographer"),
				new ValueTuple<string, string>("fiddleplayer", "fiddler"),
				new ValueTuple<string, string>("pokerplayer", "card player"),
				new ValueTuple<string, string>("backupdeputy", "deputy"),
				new ValueTuple<string, string>("deputy", "deputy"),
				new ValueTuple<string, string>("customvendor", "tailor"),
				new ValueTuple<string, string>("trapper", "trapper")
			};
		}

		// Token: 0x04000CB2 RID: 3250
		public static readonly Dictionary<string, string> KnownNames;

		// Token: 0x04000CB3 RID: 3251
		[TupleElementNames(new string[]
		{
			"token",
			"town"
		})]
		private static readonly ValueTuple<string, string>[] Towns;

		// Token: 0x04000CB4 RID: 3252
		[TupleElementNames(new string[]
		{
			"token",
			"job"
		})]
		private static readonly ValueTuple<string, string>[] Jobs;
	}
}
