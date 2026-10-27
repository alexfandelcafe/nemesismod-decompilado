using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x02000063 RID: 99
	internal static class Quirks
	{
		// Token: 0x060011CE RID: 4558 RVA: 0x00112220 File Offset: 0x00110420
		public static bool Has(NemesisRecord r, string q)
		{
			return ((r != null) ? r.Quirks : null) != null && r.Quirks.Contains(q);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00112240 File Offset: 0x00110440
		public static void Roll(NemesisRecord r, Random rng)
		{
			r.Quirks = (r.Quirks ?? new List<string>());
			if (r.QuirksRolled || r.Quirks.Count > 0)
			{
				r.QuirksRolled = true;
				return;
			}
			r.QuirksRolled = true;
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			dictionary["drunk"] = 8 + ((Mind.N(r, "Drink") >= 60) ? 20 : 0) + ((r.Personality.GetValueOrDefault() == Personality.Drifter) ? 8 : 0);
			dictionary["gambler"] = 6 + ((r.Personality.GetValueOrDefault() == Personality.Greedy) ? 12 : 0) + ((Mind.N(r, "Money") >= 60) ? 6 : 0);
			string key = "braggart";
			int num = 6 + ((r.Personality.GetValueOrDefault() == Personality.Proud) ? 14 : 0);
			Personality? personality = r.Personality;
			Personality personality2 = Personality.Hothead;
			dictionary[key] = num + ((personality.GetValueOrDefault() == personality2 & personality != null) ? 6 : 0);
			dictionary["traitor"] = 3 + ((Mind.F(r, "Loyalty") < 30) ? 14 : 0) + ((r.Personality.GetValueOrDefault() == Personality.Greedy) ? 6 : 0) - ((r.Personality.GetValueOrDefault() == Personality.Loyal) ? 3 : 0);
			dictionary["honest"] = 3 + ((Mind.F(r, "Honesty") >= 70) ? 16 : 0) + ((r.Devotion == Devotion.Devout) ? 6 : 0);
			dictionary["superstitious"] = 5 + ((r.Devotion == Devotion.Devout) ? 6 : 0) + ((r.Smarts == Smarts.Dim) ? 6 : 0);
			dictionary["sadist"] = 2 + ((Mind.F(r, "Cruelty") >= 70) ? 14 : 0) + ((r.Personality.GetValueOrDefault() == Personality.Cold) ? 4 : 0);
			dictionary["padre de familia"] = ((r.TraitsRolled && r.Family == "married" && r.Children > 0) ? 18 : 0);
			Dictionary<string, int> dictionary2 = dictionary;
			if (r.LifeRules >= 2)
			{
				Quirks.LifeLeans(r, dictionary2);
			}
			int num2 = (rng.Next(100) < 55) ? 1 : ((rng.Next(100) < 40) ? 2 : 0);
			Func<KeyValuePair<string, int>, bool> <>9__0;
			for (int i = 0; i < num2; i++)
			{
				IEnumerable<KeyValuePair<string, int>> source = dictionary2;
				Func<KeyValuePair<string, int>, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((KeyValuePair<string, int> kv) => kv.Value > 0 && !r.Quirks.Contains(kv.Key) && (!(kv.Key == "honest") || !r.Quirks.Contains("braggart")) && (!(kv.Key == "braggart") || !r.Quirks.Contains("honest")) && (!(kv.Key == "traitor") || r.Personality.GetValueOrDefault() != Personality.Loyal)));
				}
				List<KeyValuePair<string, int>> list = source.Where(predicate).ToList<KeyValuePair<string, int>>();
				int num3 = list.Sum((KeyValuePair<string, int> kv) => kv.Value);
				if (num3 <= 0)
				{
					break;
				}
				int num4 = rng.Next(num3);
				foreach (KeyValuePair<string, int> keyValuePair in list)
				{
					num4 -= keyValuePair.Value;
					if (num4 < 0)
					{
						r.Quirks.Add(keyValuePair.Key);
						break;
					}
				}
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x001125F0 File Offset: 0x001107F0
		private static void LifeLeans(NemesisRecord r, Dictionary<string, int> w)
		{
			using (List<PastEntry>.Enumerator enumerator = (r.Past ?? new List<PastEntry>()).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PastEntry e = enumerator.Current;
					if (e.Text != null && !(e.Kind == "world") && !(e.Kind == "kin") && !(e.Kind == "friend") && !(e.Kind == "weave"))
					{
						Func<string, bool> <>9__0;
						foreach (ValueTuple<string, string[]> valueTuple in Quirks.LifeWords)
						{
							string item = valueTuple.Item1;
							string[] item2 = valueTuple.Item2;
							if (w.ContainsKey(item))
							{
								IEnumerable<string> source = item2;
								Func<string, bool> predicate;
								if ((predicate = <>9__0) == null)
								{
									predicate = (<>9__0 = ((string x) => e.Text.IndexOf(x, StringComparison.OrdinalIgnoreCase) >= 0));
								}
								if (source.Any(predicate))
								{
									string key = item;
									w[key] += 12;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00112754 File Offset: 0x00110954
		public static string Tell(string q)
		{
			if (q != null)
			{
				switch (q.Length)
				{
				case 5:
					if (q == "drunk")
					{
						return "bebe como un cosaco";
					}
					break;
				case 6:
				{
					char c = q[0];
					if (c != 'h')
					{
						if (c == 's')
						{
							if (q == "sadist")
							{
								return "disfruta haciendo daño a la gente";
							}
						}
					}
					else if (q == "honest")
					{
						return "no sabe mentir ni para salvar el pellejo";
					}
					break;
				}
				case 7:
				{
					char c = q[0];
					if (c != 'g')
					{
						if (c == 't')
						{
							if (q == "traitor")
							{
								return "vendería a su propia madre";
							}
						}
					}
					else if (q == "gambler")
					{
						return "no puede resistirse a una partida de cartas";
					}
					break;
				}
				case 8:
					if (q == "braggart")
					{
						return "nunca deja de hablar de sí mismo";
					}
					break;
				case 10:
					if (q == "padre de familia")
					{
						return "vive por sus hijos";
					}
					break;
				case 13:
					if (q == "superstitious")
					{
						return "le tiene miedo a la oscuridad, aunque cueste creerlo";
					}
					break;
				}
			}
			return q;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0011287C File Offset: 0x00110A7C
		public static string Label(string q)
		{
			return q.ToUpperInvariant();
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00112884 File Offset: 0x00110A84
		public static bool HasSkill(NemesisRecord r, string s)
		{
			return ((r != null) ? r.Skills : null) != null && r.Skills.Contains(s);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x001128A4 File Offset: 0x00110AA4
		public static List<string> EarnSkills(NemesisRecord r, string lastWeaponBand = null)
		{
			Quirks.<>c__DisplayClass8_0 CS$<>8__locals1 = new Quirks.<>c__DisplayClass8_0();
			CS$<>8__locals1.r = r;
			CS$<>8__locals1.r.Skills = (CS$<>8__locals1.r.Skills ?? new List<string>());
			CS$<>8__locals1.got = new List<string>();
			if (!CS$<>8__locals1.r.SkillsV2)
			{
				CS$<>8__locals1.r.Skills.RemoveAll((string s) => (!(s == "marksman") || CS$<>8__locals1.r.TimesKilledPlayer <= 0) && (!(s == "brawler") || CS$<>8__locals1.r.TimesKilledPlayer <= 0) && (!(s == "tough") || CS$<>8__locals1.r.TimesCheatedDeath < 2) && (!(s == "tracker") || CS$<>8__locals1.r.TimesAmbushed < 3) && (!(s == "leader") || CS$<>8__locals1.r.Rank < 4 || CS$<>8__locals1.r.TimesAmbushed < 2));
				CS$<>8__locals1.r.SkillsV2 = true;
			}
			CS$<>8__locals1.<EarnSkills>g__Give|1("marksman", CS$<>8__locals1.r.TimesKilledPlayer > 0 && lastWeaponBand == "Gun");
			CS$<>8__locals1.<EarnSkills>g__Give|1("brawler", CS$<>8__locals1.r.TimesKilledPlayer > 0 && lastWeaponBand == "Fists");
			CS$<>8__locals1.<EarnSkills>g__Give|1("tough", CS$<>8__locals1.r.TimesCheatedDeath >= 2 || (CS$<>8__locals1.r.TimesCheatedDeath == 1 && Seeds.Stable("tough/" + (CS$<>8__locals1.r.Id ?? CS$<>8__locals1.r.Name)) % 100 < 50));
			CS$<>8__locals1.<EarnSkills>g__Give|1("tracker", CS$<>8__locals1.r.TimesAmbushed >= 3);
			CS$<>8__locals1.<EarnSkills>g__Give|1("leader", CS$<>8__locals1.r.Rank >= 4 && CS$<>8__locals1.r.TimesAmbushed >= 2);
			int num = (CS$<>8__locals1.r.Rank >= 4) ? 3 : ((CS$<>8__locals1.r.Rank >= 2) ? 2 : 1);
			for (int i = CS$<>8__locals1.r.SkillTierRolled + 1; i <= num; i++)
			{
				Random random = Seeds.For(CS$<>8__locals1.r, "skilltier" + i.ToString());
				int num2 = (i == 3) ? 100 : ((i == 2) ? 40 : 15);
				if (random.Next(100) < num2)
				{
					IEnumerable<ValueTuple<string, int>> source;
					if (i != 3)
					{
						ValueTuple<string, int>[] array = new ValueTuple<string, int>[3];
						array[0] = new ValueTuple<string, int>("marksman", 35);
						array[1] = new ValueTuple<string, int>("tracker", 35);
						source = array;
						array[2] = new ValueTuple<string, int>("brawler", 30);
					}
					else
					{
						ValueTuple<string, int>[] array2 = new ValueTuple<string, int>[3];
						array2[0] = new ValueTuple<string, int>("leader", 40);
						array2[1] = new ValueTuple<string, int>("marksman", 30);
						source = array2;
						array2[2] = new ValueTuple<string, int>("tracker", 30);
					}
					Func<ValueTuple<string, int>, bool> predicate;
					if ((predicate = CS$<>8__locals1.<>9__2) == null)
					{
						predicate = (CS$<>8__locals1.<>9__2 = ((ValueTuple<string, int> p) => !CS$<>8__locals1.r.Skills.Contains(p.Item1)));
					}
					List<ValueTuple<string, int>> list = source.Where(predicate).ToList<ValueTuple<string, int>>();
					if (list.Count != 0)
					{
						int num3 = random.Next(list.Sum((ValueTuple<string, int> p) => p.Item2));
						foreach (ValueTuple<string, int> valueTuple in list)
						{
							string item = valueTuple.Item1;
							int item2 = valueTuple.Item2;
							num3 -= item2;
							if (num3 < 0)
							{
								CS$<>8__locals1.<EarnSkills>g__Give|1(item, true);
								break;
							}
						}
					}
				}
			}
			CS$<>8__locals1.r.SkillTierRolled = Math.Max(CS$<>8__locals1.r.SkillTierRolled, num);
			return CS$<>8__locals1.got;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x00112C00 File Offset: 0x00110E00
		public static string SkillText(string s)
		{
			if (s == "marksman")
			{
				return "tirador certero (precisión +12)";
			}
			if (s == "brawler")
			{
				return "peleador duro (más difícil de tumbar)";
			}
			if (s == "tough")
			{
				return "duro de pelar (+60 salud)";
			}
			if (s == "tracker")
			{
				return "rastreador (te da caza desde más lejos)";
			}
			if (s == "horseman")
			{
				return "buen jinete (te da alcance a caballo)";
			}
			if (!(s == "leader"))
			{
				return s;
			}
			return "líder de hombres (+1 hombre)";
		}

		// Token: 0x04000BCF RID: 3023
		public static readonly string[] All = new string[]
		{
			"drunk",
			"gambler",
			"braggart",
			"traitor",
			"honest",
			"superstitious",
			"sadist",
			"padre de familia"
		};

		// Token: 0x04000BD0 RID: 3024
		[TupleElementNames(new string[]
		{
			"quirk",
			"words"
		})]
		private static readonly ValueTuple<string, string[]>[] LifeWords = new ValueTuple<string, string[]>[]
		{
			new ValueTuple<string, string[]>("drunk", new string[]
			{
				"drinking",
				"the bottle",
				"drank the lot",
				"moonshine"
			}),
			new ValueTuple<string, string[]>("gambler", new string[]
			{
				"faro",
				"card games",
				"card tables",
				"over cards",
				"card sharp",
				"Owes money"
			}),
			new ValueTuple<string, string[]>("braggart", new string[]
			{
				"bare-knuckle",
				"fifty paces",
				"his first stake"
			}),
			new ValueTuple<string, string[]>("sadist", new string[]
			{
				"Shot a man dead",
				"Burned out",
				"used his fists",
				"end up in the swamp"
			}),
			new ValueTuple<string, string[]>("superstitious", new string[]
			{
				"revival",
				"Snakebit",
				"cave-in",
				"Famine"
			}),
			new ValueTuple<string, string[]>("honest", new string[]
			{
				"God-fearing",
				"Found God",
				"and wept"
			}),
			new ValueTuple<string, string[]>("traitor", new string[]
			{
				"blamed everyone",
				"the wrong people",
				"Ran from his first"
			})
		};
	}
}
