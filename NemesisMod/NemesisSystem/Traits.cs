using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x0200007D RID: 125
	public static class Traits
	{
		// Token: 0x060012FA RID: 4858 RVA: 0x0012F79C File Offset: 0x0012D99C
		internal static int AggressionBase(Personality? p)
		{
			Personality? personality = p;
			Personality personality2 = Personality.Hothead;
			if (personality.GetValueOrDefault() == personality2 & personality != null)
			{
				return 70;
			}
			if (p.GetValueOrDefault() == Personality.Coward)
			{
				return 20;
			}
			if (p.GetValueOrDefault() != Personality.Proud)
			{
				return 40;
			}
			return 55;
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x0012F7E0 File Offset: 0x0012D9E0
		public static void Roll(NemesisRecord r, Random rng, IList<NemesisRecord> roster)
		{
			Traits.<>c__DisplayClass1_0 CS$<>8__locals1 = new Traits.<>c__DisplayClass1_0();
			CS$<>8__locals1.r = r;
			CS$<>8__locals1.rng = rng;
			CS$<>8__locals1.roster = roster;
			Personality valueOrDefault = CS$<>8__locals1.r.Personality.GetValueOrDefault(Personality.Cold);
			string m = Models.Name(CS$<>8__locals1.r.ModelHash);
			CS$<>8__locals1.r.Aggression = Traits.Clamp(Traits.AggressionBase(new Personality?(valueOrDefault)) + CS$<>8__locals1.rng.Next(-20, 21));
			CS$<>8__locals1.r.Sociability = ((CS$<>8__locals1.rng.Next(100) < ((valueOrDefault == Personality.Cold || valueOrDefault == Personality.Drifter) ? 70 : ((valueOrDefault == Personality.Hothead || valueOrDefault == Personality.Proud) ? 30 : 50))) ? Sociability.Introvert : Sociability.Outgoing);
			int num = CS$<>8__locals1.rng.Next(100);
			CS$<>8__locals1.r.Disposition = ((valueOrDefault == Personality.Hothead) ? ((num < 15) ? Disposition.Friendly : ((num < 50) ? Disposition.Neutral : Disposition.Surly)) : ((valueOrDefault == Personality.Coward) ? ((num < 40) ? Disposition.Friendly : ((num < 85) ? Disposition.Neutral : Disposition.Surly)) : ((num < 25) ? Disposition.Friendly : ((num < 70) ? Disposition.Neutral : Disposition.Surly))));
			int num2 = CS$<>8__locals1.rng.Next(100);
			CS$<>8__locals1.r.Smarts = ((valueOrDefault == Personality.Cold) ? ((num2 < 10) ? Smarts.Dim : ((num2 < 50) ? Smarts.Average : Smarts.Sharp)) : ((num2 < 25) ? Smarts.Dim : ((num2 < 80) ? Smarts.Average : Smarts.Sharp)));
			if (CS$<>8__locals1.r.Trade == null)
			{
				NemesisRecord r2 = CS$<>8__locals1.r;
				NemesisRecord r3 = CS$<>8__locals1.r;
				ValueTuple<Wealth, string> valueTuple = Traits.FromModel(m, CS$<>8__locals1.r.Gang, CS$<>8__locals1.rng);
				r2.Wealth = valueTuple.Item1;
				r3.Trade = valueTuple.Item2;
			}
			Gang gang = Gangs.ById(CS$<>8__locals1.r.Gang);
			string text = (((gang != null) ? gang.Faiths : null) != null && gang.Faiths.Length != 0) ? gang.Faiths[CS$<>8__locals1.rng.Next(gang.Faiths.Length)] : (NameBank.IrishLast.Any(delegate(string l)
			{
				string name2 = CS$<>8__locals1.r.Name;
				return name2 != null && name2.EndsWith(" " + l);
			}) ? "Católico" : ((CS$<>8__locals1.r.Wealth == Wealth.Rich) ? ((CS$<>8__locals1.rng.Next(3) == 0) ? "Presbiteriano" : "Episcopaliano") : (new string[]
			{
				"Metodista",
				"Bautista",
				"Presbiteriano",
				"Luterano",
				"Católico",
				"Metodista",
				"Bautista"
			})[CS$<>8__locals1.rng.Next(7)]));
			int num3 = CS$<>8__locals1.rng.Next(100);
			CS$<>8__locals1.r.Devotion = ((CS$<>8__locals1.r.Trade == "Predicador") ? Devotion.Devout : ((num3 < 18) ? Devotion.Devout : ((num3 < 55) ? Devotion.Churchgoer : ((num3 < 88) ? Devotion.Lapsed : Devotion.Godless))));
			CS$<>8__locals1.r.Faith = ((CS$<>8__locals1.r.Devotion == Devotion.Godless) ? "none" : text);
			int num4 = CS$<>8__locals1.rng.Next(100);
			CS$<>8__locals1.r.Family = ((num4 < 45) ? "single" : ((num4 < 85) ? "married" : "widower"));
			CS$<>8__locals1.r.Children = ((CS$<>8__locals1.r.Family == "single") ? 0 : CS$<>8__locals1.rng.Next(5));
			if (CS$<>8__locals1.roster != null && CS$<>8__locals1.rng.Next(100) < Traits.KinChance)
			{
				NemesisRecord nemesisRecord = (from n in CS$<>8__locals1.roster
				where n != CS$<>8__locals1.r && n.IsAlive && n.Gang == CS$<>8__locals1.r.Gang && n.Name != null && n.Name.Contains(' ')
				select n into _
				orderby CS$<>8__locals1.rng.Next()
				select _).FirstOrDefault<NemesisRecord>();
				if (nemesisRecord != null)
				{
					string str = NameBank.Surname(nemesisRecord.Name);
					string name = CS$<>8__locals1.r.Name;
					string str2 = ((name != null) ? name.Split(new char[]
					{
						' '
					})[0] : null) ?? "Jim";
					CS$<>8__locals1.r.KinId = nemesisRecord.Id;
					CS$<>8__locals1.r.KinRelation = Traits.RollKin(CS$<>8__locals1.rng);
					Gang gang2 = Gangs.ById(CS$<>8__locals1.r.Gang);
					if ((gang2 == null || gang2.FamilyNames) && Traits.SharesSurname(CS$<>8__locals1.r.KinRelation, CS$<>8__locals1.rng))
					{
						string text2 = str2 + " " + str;
						if (CS$<>8__locals1.<Roll>g__Taken|3(text2) && !Models.Female(CS$<>8__locals1.r.ModelHash))
						{
							Gang gang3 = Gangs.ById(CS$<>8__locals1.r.Gang);
							string[] array = ((gang3 != null) ? gang3.FirstNames : null) ?? NameBank.AmericanFirst;
							int num5 = 0;
							while (num5 < 25 && CS$<>8__locals1.<Roll>g__Taken|3(text2))
							{
								text2 = array[CS$<>8__locals1.rng.Next(array.Length)] + " " + str;
								num5++;
							}
						}
						if (!CS$<>8__locals1.<Roll>g__Taken|3(text2))
						{
							CS$<>8__locals1.r.Name = text2;
						}
					}
				}
			}
			bool flag = CS$<>8__locals1.r.Trade == "Ranchero" || CS$<>8__locals1.r.Trade == "Granjero" || CS$<>8__locals1.r.Trade == "Cazador" || CS$<>8__locals1.r.Trade == "Vaquero" || CS$<>8__locals1.r.Trade == "Trampero" || CS$<>8__locals1.r.Trade == "Colona" || CS$<>8__locals1.r.Trade == "Granjera";
			if (CS$<>8__locals1.rng.Next(100) < (flag ? 45 : 8))
			{
				CS$<>8__locals1.r.Pet = "a " + Traits.DogBreeds[CS$<>8__locals1.rng.Next(Traits.DogBreeds.Length)] + " llamado " + Traits.DogNames[CS$<>8__locals1.rng.Next(Traits.DogNames.Length)];
			}
			CS$<>8__locals1.r.TraitsRolled = true;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0012FDF0 File Offset: 0x0012DFF0
		public static void SetBackground(NemesisRecord r, Random rng)
		{
			ValueTuple<Wealth, string> valueTuple = Traits.FromModel(Models.Name(r.ModelHash), r.Gang, rng);
			r.Wealth = valueTuple.Item1;
			r.Trade = valueTuple.Item2;
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0012FE34 File Offset: 0x0012E034
		private static ValueTuple<Wealth, string> FromModel(string m, string gang, Random rng)
		{
			Traits.<>c__DisplayClass5_0 CS$<>8__locals1;
			CS$<>8__locals1.m = m;
			Gang gang2 = Gangs.ById(gang);
			if (gang2 != null)
			{
				Wealth item = gang2.MemberWealth ?? ((rng.Next(3) == 0) ? Wealth.Working : Wealth.Poor);
				string item2 = (gang2.MemberTrades != null && gang2.MemberTrades.Length != 0) ? gang2.MemberTrades[rng.Next(gang2.MemberTrades.Length)] : "Forajido";
				return new ValueTuple<Wealth, string>(item, item2);
			}
			if (CS$<>8__locals1.m.Contains("_f_"))
			{
				return Traits.Also(Traits.WomanFromModel(CS$<>8__locals1.m, rng), Traits.WomenAlso, rng);
			}
			return Traits.Also(Traits.ManFromModel(CS$<>8__locals1.m, rng), Traits.MenAlso, rng);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x0012FEF0 File Offset: 0x0012E0F0
		private static ValueTuple<Wealth, string> ManFromModel(string m, Random rng)
		{
			Traits.<>c__DisplayClass6_0 CS$<>8__locals1;
			CS$<>8__locals1.m = m;
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"upperclass",
				"fancy",
				"aristocrat",
				"wealthy",
				"gentleman",
				"banker",
				"richtraveller"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Rich, "Caballero");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"preacher",
				"priest",
				"nun",
				"minister"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Comfortable, "Predicador");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"doctor"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Comfortable, "Médico");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"gambler",
				"cardplayer"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Comfortable, "Tahúr");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"rancher"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Working, "Ranchero");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"farmer",
				"farmhand",
				"sharecrop"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Working, "Granjero");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"miner"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Working, "Minero");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"lumber",
				"logger"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Working, "Leñador");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"dock",
				"fisher"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Working, Traits.<ManFromModel>g__Has|6_0(new string[]
				{
					"fisher"
				}, ref CS$<>8__locals1) ? "Pescador" : "Estibador");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"trapper"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Working, "Trampero");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"hunter"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Working, "Cazador");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"cowboy",
				"cattle",
				"drover",
				"wrangler"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Working, "Vaquero");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"rail",
				"train"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Working, "Ferroviario");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"laborer",
				"labourer",
				"worker",
				"bivworker"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Working, "Jornalero");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"vagrant",
				"beggar",
				"tramp",
				"drunk",
				"poor",
				"homeless",
				"lowersd"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Poor, "Vagabundo");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"criminal",
				"bandit",
				"outlaw",
				"thug",
				"bounty"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Poor, "Forajido");
			}
			if (Traits.<ManFromModel>g__Has|6_0(new string[]
			{
				"townfolk",
				"townsfolk",
				"valtown",
				"rhdtown",
				"sdtown",
				"bwmtown"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Comfortable, "Vecino del pueblo");
			}
			return new ValueTuple<Wealth, string>((rng.Next(2) == 0) ? Wealth.Working : Wealth.Poor, "Errante");
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x001302B0 File Offset: 0x0012E4B0
		private static ValueTuple<Wealth, string> Also([TupleElementNames(new string[]
		{
			"w",
			"t"
		})] ValueTuple<Wealth, string> was, [TupleElementNames(new string[]
		{
			"pct",
			"to"
		})] Dictionary<string, ValueTuple<int, string[]>> table, Random rng)
		{
			ValueTuple<int, string[]> valueTuple;
			if (was.Item2 == null || !table.TryGetValue(was.Item2, out valueTuple) || rng.Next(100) >= valueTuple.Item1)
			{
				return was;
			}
			string text = valueTuple.Item2[rng.Next(valueTuple.Item2.Length)];
			Wealth wealth;
			return new ValueTuple<Wealth, string>(Traits.AlsoWealth.TryGetValue(text, out wealth) ? wealth : Wealth.Working, text);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00130318 File Offset: 0x0012E518
		internal static bool IsWomansTrade(string t)
		{
			return Traits.WomenAlso.Values.Any(([TupleElementNames(new string[]
			{
				"pct",
				"to"
			})] ValueTuple<int, string[]> v) => v.Item2.Contains(t)) || Traits.TownWomen.Any(([TupleElementNames(new string[]
			{
				"trade",
				"w"
			})] ValueTuple<string, Wealth> x) => x.Item1 == t) || new string[]
			{
				"Dama",
				"Monja",
				"Esposa de predicador",
				"Enfermera",
				"Chica de salón",
				"Corista",
				"Tahúr",
				"Colona",
				"Granjera",
				"Vendedora del mercado",
				"Cocinera de campamento",
				"Viajera",
				"Vagabundo",
				"Forajido",
				"Errante"
			}.Contains(t);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00130400 File Offset: 0x0012E600
		private static ValueTuple<Wealth, string> WomanFromModel(string m, Random rng)
		{
			Traits.<>c__DisplayClass14_0 CS$<>8__locals1;
			CS$<>8__locals1.m = m;
			CS$<>8__locals1.rng = rng;
			if (Traits.<WomanFromModel>g__Has|14_0(new string[]
			{
				"upperclass",
				"fancy",
				"aristocrat",
				"wealthy",
				"richtraveller",
				"socialite"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Rich, "Dama");
			}
			if (Traits.<WomanFromModel>g__Has|14_0(new string[]
			{
				"nun"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Poor, "Monja");
			}
			if (Traits.<WomanFromModel>g__Has|14_0(new string[]
			{
				"preacher",
				"priest",
				"minister",
				"church"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Comfortable, "Esposa de predicador");
			}
			if (Traits.<WomanFromModel>g__Has|14_0(new string[]
			{
				"doctor",
				"nurse"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Comfortable, "Enfermera");
			}
			if (Traits.<WomanFromModel>g__Has|14_0(new string[]
			{
				"prostitute",
				"whore",
				"showgirl",
				"dancer",
				"dancehall",
				"saloongirl",
				"burlesque",
				"singer"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Poor, Traits.<WomanFromModel>g__Has|14_0(new string[]
				{
					"singer",
					"showgirl",
					"dancer"
				}, ref CS$<>8__locals1) ? "Corista" : "Chica de salón");
			}
			if (Traits.<WomanFromModel>g__Has|14_0(new string[]
			{
				"gambler",
				"cardplayer"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Comfortable, "Tahúr");
			}
			if (Traits.<WomanFromModel>g__Has|14_0(new string[]
			{
				"rancher",
				"farmer",
				"farmhand",
				"sharecrop",
				"homestead",
				"cowboy",
				"cattle",
				"drover",
				"wrangler",
				"hillbilly"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Working, (CS$<>8__locals1.rng.Next(2) == 0) ? "Colona" : "Granjera");
			}
			if (Traits.<WomanFromModel>g__Has|14_0(new string[]
			{
				"dock",
				"fisher",
				"market",
				"vendor",
				"seller"
			}, ref CS$<>8__locals1))
			{
				return new ValueTuple<Wealth, string>(Wealth.Working, "Vendedora del mercado");
			}
			if (Traits.<WomanFromModel>g__Has|14_0(new string[]
			{
				"miner",
				"lumber",
				"logger",
				"laborer",
				"labourer",
				"worker",
				"bivworker"
			}, ref CS$<>8__locals1))
			{
				if (CS$<>8__locals1.rng.Next(2) != 0)
				{
					return new ValueTuple<Wealth, string>(Wealth.Working, "Cocinera de campamento");
				}
				return new ValueTuple<Wealth, string>(Wealth.Poor, "Lavandera");
			}
			else
			{
				if (Traits.<WomanFromModel>g__Has|14_0(new string[]
				{
					"rail",
					"train",
					"passenger",
					"traveller",
					"traveler"
				}, ref CS$<>8__locals1))
				{
					return new ValueTuple<Wealth, string>(Wealth.Comfortable, "Viajera");
				}
				if (Traits.<WomanFromModel>g__Has|14_0(new string[]
				{
					"vagrant",
					"beggar",
					"tramp",
					"drunk",
					"poor",
					"homeless",
					"lowersd"
				}, ref CS$<>8__locals1))
				{
					return new ValueTuple<Wealth, string>(Wealth.Poor, "Vagabundo");
				}
				if (Traits.<WomanFromModel>g__Has|14_0(new string[]
				{
					"criminal",
					"bandit",
					"outlaw",
					"thug",
					"bounty"
				}, ref CS$<>8__locals1))
				{
					return new ValueTuple<Wealth, string>(Wealth.Poor, "Forajido");
				}
				if (Traits.<WomanFromModel>g__Has|14_0(new string[]
				{
					"townfolk",
					"townsfolk",
					"valtown",
					"rhdtown",
					"sdtown",
					"bwmtown",
					"strtown",
					"town"
				}, ref CS$<>8__locals1))
				{
					return Traits.<WomanFromModel>g__Town|14_1(ref CS$<>8__locals1);
				}
				if (CS$<>8__locals1.rng.Next(3) != 0)
				{
					return Traits.<WomanFromModel>g__Town|14_1(ref CS$<>8__locals1);
				}
				return new ValueTuple<Wealth, string>(Wealth.Poor, "Errante");
			}
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00130828 File Offset: 0x0012EA28
		private static int Clamp(int v)
		{
			return Math.Max(0, Math.Min(100, v));
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00130838 File Offset: 0x0012EA38
		public static float HateGain(NemesisRecord r)
		{
			if (!r.TraitsRolled)
			{
				return 1f;
			}
			float num = 0.8f + (float)r.Aggression / 250f;
			if (r.Disposition == Disposition.Friendly)
			{
				num *= 0.85f;
			}
			if (r.Disposition == Disposition.Surly)
			{
				num *= 1.15f;
			}
			if (r.Devotion == Devotion.Devout)
			{
				num *= 0.85f;
			}
			if (r.Devotion == Devotion.Godless)
			{
				num *= 1.1f;
			}
			return num;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x001308AC File Offset: 0x0012EAAC
		public static float FearGain(NemesisRecord r)
		{
			float num = Tempers.Of(r.Personality).FearGain;
			if (r.Smarts == Smarts.Sharp)
			{
				num *= 1.2f;
			}
			if (r.Smarts == Smarts.Dim)
			{
				num *= 0.8f;
			}
			if (r.Wealth == Wealth.Rich)
			{
				num *= 1.2f;
			}
			return num;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x001308FD File Offset: 0x0012EAFD
		public static int CoolPer10Min(NemesisRecord r)
		{
			if (r.Devotion != Devotion.Devout || !r.TraitsRolled)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00130914 File Offset: 0x0012EB14
		public static int InterrogationBias(NemesisRecord r)
		{
			if (r == null || !r.TraitsRolled)
			{
				return 0;
			}
			int num = 0;
			if (r.Smarts == Smarts.Dim)
			{
				num += 10;
			}
			if (r.Smarts == Smarts.Sharp)
			{
				num -= 8;
			}
			if (r.Disposition == Disposition.Friendly)
			{
				num += 6;
			}
			if (r.Disposition == Disposition.Surly)
			{
				num -= 6;
			}
			if (r.Children > 0)
			{
				num += 5;
			}
			return num;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00130971 File Offset: 0x0012EB71
		public static string RollKin(Random rng)
		{
			return Traits.KinKinds[rng.Next(Traits.KinKinds.Length)];
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00130988 File Offset: 0x0012EB88
		public static bool SharesSurname(string rel, Random rng)
		{
			return rel == "hermano" || rel == "hijo" || rel == "padre" || ((rel == "tío" || rel == "sobrino" || rel == "primo") && rng.Next(2) == 0);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x001309F4 File Offset: 0x0012EBF4
		public static string Word(string rel, bool female)
		{
			if (!female || rel == null)
			{
				return rel;
			}
			if (rel == "hermano")
			{
				return "hermana";
			}
			if (rel == "hijo")
			{
				return "hija";
			}
			if (rel == "padre")
			{
				return "madre";
			}
			if (rel == "sobrino")
			{
				return "sobrina";
			}
			if (rel == "tío")
			{
				return "tía";
			}
			if (!(rel == "cuñado"))
			{
				return rel;
			}
			return "cuñada";
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00130A80 File Offset: 0x0012EC80
		public static string Inverse(string rel)
		{
			string result;
			if (!(rel == "hijo"))
			{
				if (rel == "padre")
				{
					return "hijo";
				}
				if (rel == "sobrino")
				{
					return "tío";
				}
				if (rel == "tío")
				{
					return "sobrino";
				}
				result = rel;
				if (rel == null)
				{
					return "kin";
				}
			}
			else
			{
				result = "padre";
			}
			return result;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00130AE4 File Offset: 0x0012ECE4
		public static float KinWeight(string rel)
		{
			if (rel == "hermano" || rel == "hijo" || rel == "padre")
			{
				return 1f;
			}
			if (rel == "tío" || rel == "sobrino")
			{
				return 0.8f;
			}
			if (rel == "primo")
			{
				return 0.7f;
			}
			if (!(rel == "friend"))
			{
				return 0.6f;
			}
			return 0.5f;
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00130B6C File Offset: 0x0012ED6C
		public static Intent IntentOf(NemesisRecord r, int effectiveFear)
		{
			if (!r.CanHunt || r.PresumedDead)
			{
				return Intent.None;
			}
			Personality? personality = r.Personality;
			Personality personality2 = Personality.Hothead;
			bool flag = (personality.GetValueOrDefault() == personality2 & personality != null) || r.Personality.GetValueOrDefault() == Personality.Proud;
			if (r.TimesKilledPlayer > 0 && r.Origin == GrudgeOrigin.Killed)
			{
				if (effectiveFear < 85 || flag)
				{
					return Intent.Hunts;
				}
				return Intent.WontHunt;
			}
			else if (r.Origin == GrudgeOrigin.GangMember)
			{
				if (effectiveFear < Balance.WontHuntFear + 15)
				{
					return Intent.Hunts;
				}
				return Intent.WontHunt;
			}
			else
			{
				if (effectiveFear >= Balance.ProvokedOnlyFear && !flag)
				{
					return Intent.ProvokedOnly;
				}
				HateTier hateTier = r.HateTier;
				if (hateTier != HateTier.Vengeful)
				{
					if (hateTier == HateTier.Murderous)
					{
						if (effectiveFear >= Balance.WontHuntFear && !flag)
						{
							return Intent.WontHunt;
						}
						if (r.Sociability == Sociability.Introvert && r.Aggression < 35 && !flag)
						{
							return Intent.WontHunt;
						}
						return Intent.Hunts;
					}
					else
					{
						personality = r.Personality;
						personality2 = Personality.Hothead;
						if (!(personality.GetValueOrDefault() == personality2 & personality != null) || r.Aggression < 75)
						{
							return Intent.ProvokedOnly;
						}
						return Intent.WontHunt;
					}
				}
				else
				{
					if (flag && r.Aggression >= Balance.VengefulHuntAggression && r.Sociability != Sociability.Introvert && effectiveFear < Balance.WontHuntFear)
					{
						return Intent.Hunts;
					}
					return Intent.WontHunt;
				}
			}
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00130C83 File Offset: 0x0012EE83
		public static string Describe(Intent i)
		{
			switch (i)
			{
			case Intent.Hunts:
				return "te da caza";
			case Intent.WontHunt:
				return "quiere verte muerto, pero no te buscará; si te cruzas con él, peleará";
			case Intent.ProvokedOnly:
				return "no empezará nada, pero si lo provocas en el pueblo, peleará";
			default:
				return "no puede ir tras de ti";
			}
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00130CB0 File Offset: 0x0012EEB0
		public static string Short(Intent i)
		{
			if (i == Intent.Hunts)
			{
				return "Te caza";
			}
			if (i == Intent.WontHunt)
			{
				return "Espera";
			}
			if (i != Intent.ProvokedOnly)
			{
				return "-";
			}
			return "Si lo provocas";
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00130CD4 File Offset: 0x0012EED4
		public static string Aggr(int a)
		{
			if (a >= 67)
			{
				return "Violento";
			}
			if (a < 34)
			{
				return "Tranquilo";
			}
			return "Inquieto";
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00130CF4 File Offset: 0x0012EEF4
		// Note: this type is marked as 'beforefieldinit'.
		static Traits()
		{
			Dictionary<string, ValueTuple<int, string[]>> dictionary = new Dictionary<string, ValueTuple<int, string[]>>();
			dictionary["Costurera"] = new ValueTuple<int, string[]>(8, Traits.TownWomenAlso);
			dictionary["Maestra de escuela"] = new ValueTuple<int, string[]>(6, Traits.TownWomenAlso);
			dictionary["Tendera"] = new ValueTuple<int, string[]>(8, Traits.TownWomenAlso);
			dictionary["Cocinero"] = new ValueTuple<int, string[]>(8, Traits.TownWomenAlso);
			dictionary["Criada"] = new ValueTuple<int, string[]>(8, Traits.TownWomenAlso);
			dictionary["Dueña de pensión"] = new ValueTuple<int, string[]>(6, Traits.TownWomenAlso);
			dictionary["Sombrerera"] = new ValueTuple<int, string[]>(6, Traits.TownWomenAlso);
			dictionary["Operadora de telégrafo"] = new ValueTuple<int, string[]>(6, Traits.TownWomenAlso);
			dictionary["Lavandera"] = new ValueTuple<int, string[]>(8, new string[]
			{
				"Herrero",
				"Jornalero",
				"Minero",
				"Estibador"
			});
			dictionary["Colona"] = new ValueTuple<int, string[]>(15, new string[]
			{
				"Granjero",
				"Ranchero",
				"Ranchero",
				"Vaquero",
				"Cazador",
				"Trampero",
				"Herrero"
			});
			dictionary["Granjera"] = new ValueTuple<int, string[]>(10, new string[]
			{
				"Granjero",
				"Ranchero",
				"Vaquero",
				"Herrero"
			});
			dictionary["Cocinera de campamento"] = new ValueTuple<int, string[]>(12, new string[]
			{
				"Minero",
				"Jornalero",
				"Estibador",
				"Vaquero",
				"Herrero"
			});
			dictionary["Vendedora del mercado"] = new ValueTuple<int, string[]>(8, new string[]
			{
				"Estibador",
				"Jornalero"
			});
			dictionary["Enfermera"] = new ValueTuple<int, string[]>(15, new string[]
			{
				"Médico"
			});
			dictionary["Errante"] = new ValueTuple<int, string[]>(12, new string[]
			{
				"Cazador",
				"Trampero",
				"Vaquero"
			});
			Traits.WomenAlso = dictionary;
			dictionary = new Dictionary<string, ValueTuple<int, string[]>>();
			dictionary["Vecino del pueblo"] = new ValueTuple<int, string[]>(22, new string[]
			{
				"Herrero",
				"Herrero",
				"Herrero",
				"Herrero",
				"Tendera",
				"Maestra de escuela",
				"Operadora de telégrafo",
				"Dueña de pensión",
				"Sastre",
				"Cocinero"
			});
			dictionary["Jornalero"] = new ValueTuple<int, string[]>(22, new string[]
			{
				"Herrero",
				"Herrero",
				"Herrero",
				"Cocinero",
				"Cocinera de campamento",
				"Vendedora del mercado"
			});
			dictionary["Granjero"] = new ValueTuple<int, string[]>(5, new string[]
			{
				"Herrero"
			});
			dictionary["Ranchero"] = new ValueTuple<int, string[]>(5, new string[]
			{
				"Herrero"
			});
			dictionary["Vaquero"] = new ValueTuple<int, string[]>(6, new string[]
			{
				"Cocinera de campamento"
			});
			dictionary["Errante"] = new ValueTuple<int, string[]>(6, new string[]
			{
				"Cocinera de campamento",
				"Cocinero"
			});
			Traits.MenAlso = dictionary;
			Dictionary<string, Wealth> dictionary2 = new Dictionary<string, Wealth>();
			dictionary2["Médico"] = Wealth.Comfortable;
			dictionary2["Predicador"] = Wealth.Comfortable;
			dictionary2["Tendera"] = Wealth.Comfortable;
			dictionary2["Maestra de escuela"] = Wealth.Comfortable;
			Traits.AlsoWealth = dictionary2;
			Traits.KinChance = 22;
			Traits.KinKinds = new string[]
			{
				"hermano",
				"hermano",
				"hermano",
				"primo",
				"primo",
				"hijo",
				"padre",
				"sobrino",
				"tío",
				"cuñado"
			};
			Traits.FriendChanceGang = 45;
			Traits.FriendChanceLoner = 12;
			Traits.FriendOtherFaction = 12;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x001312D5 File Offset: 0x0012F4D5
		[CompilerGenerated]
		internal static bool <FromModel>g__Has|5_0(string[] t, ref Traits.<>c__DisplayClass5_0 A_1)
		{
			return t.Any(new Func<string, bool>(A_1.m.Contains));
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x001312EE File Offset: 0x0012F4EE
		[CompilerGenerated]
		internal static bool <ManFromModel>g__Has|6_0(string[] t, ref Traits.<>c__DisplayClass6_0 A_1)
		{
			return t.Any(new Func<string, bool>(A_1.m.Contains));
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00131307 File Offset: 0x0012F507
		[CompilerGenerated]
		internal static bool <WomanFromModel>g__Has|14_0(string[] t, ref Traits.<>c__DisplayClass14_0 A_1)
		{
			return t.Any(new Func<string, bool>(A_1.m.Contains));
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00131320 File Offset: 0x0012F520
		[CompilerGenerated]
		internal static ValueTuple<Wealth, string> <WomanFromModel>g__Town|14_1(ref Traits.<>c__DisplayClass14_0 A_0)
		{
			ValueTuple<string, Wealth> valueTuple = Traits.TownWomen[A_0.rng.Next(Traits.TownWomen.Length)];
			return new ValueTuple<Wealth, string>(valueTuple.Item2, valueTuple.Item1);
		}

		// Token: 0x04000CCF RID: 3279
		private static readonly string[] DogBreeds = new string[]
		{
			"sabueso",
			"collie",
			"sabueso moteado",
			"perro mestizo",
			"perro perdiguero",
			"perro pastor",
			"terrier",
			"perro leopardo"
		};

		// Token: 0x04000CD0 RID: 3280
		private static readonly string[] DogNames = new string[]
		{
			"Blue",
			"Duke",
			"Shep",
			"Rusty",
			"Bo",
			"Molly",
			"Jack",
			"Rex",
			"Sadie",
			"Tick",
			"Old Dan",
			"Pepper",
			"Biscuit",
			"Ranger"
		};

		// Token: 0x04000CD1 RID: 3281
		[TupleElementNames(new string[]
		{
			"trade",
			"w"
		})]
		private static readonly ValueTuple<string, Wealth>[] TownWomen = new ValueTuple<string, Wealth>[]
		{
			new ValueTuple<string, Wealth>("Costurera", Wealth.Working),
			new ValueTuple<string, Wealth>("Lavandera", Wealth.Poor),
			new ValueTuple<string, Wealth>("Maestra de escuela", Wealth.Comfortable),
			new ValueTuple<string, Wealth>("Tendera", Wealth.Comfortable),
			new ValueTuple<string, Wealth>("Cocinero", Wealth.Working),
			new ValueTuple<string, Wealth>("Criada", Wealth.Poor),
			new ValueTuple<string, Wealth>("Dueña de pensión", Wealth.Working),
			new ValueTuple<string, Wealth>("Sombrerera", Wealth.Working),
			new ValueTuple<string, Wealth>("Operadora de telégrafo", Wealth.Working)
		};

		// Token: 0x04000CD2 RID: 3282
		private static readonly string[] TownWomenAlso = new string[]
		{
			"Herrero",
			"Herrero",
			"Médico",
			"Predicador",
			"Jornalero"
		};

		// Token: 0x04000CD3 RID: 3283
		[TupleElementNames(new string[]
		{
			"pct",
			"to"
		})]
		private static readonly Dictionary<string, ValueTuple<int, string[]>> WomenAlso;

		// Token: 0x04000CD4 RID: 3284
		[TupleElementNames(new string[]
		{
			"pct",
			"to"
		})]
		private static readonly Dictionary<string, ValueTuple<int, string[]>> MenAlso;

		// Token: 0x04000CD5 RID: 3285
		private static readonly Dictionary<string, Wealth> AlsoWealth;

		// Token: 0x04000CD6 RID: 3286
		public static int KinChance;

		// Token: 0x04000CD7 RID: 3287
		private static readonly string[] KinKinds;

		// Token: 0x04000CD8 RID: 3288
		public static int FriendChanceGang;

		// Token: 0x04000CD9 RID: 3289
		public static int FriendChanceLoner;

		// Token: 0x04000CDA RID: 3290
		public static int FriendOtherFaction;
	}
}
