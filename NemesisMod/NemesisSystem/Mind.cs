using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x0200002A RID: 42
	public static class Mind
	{
		// Token: 0x06000B17 RID: 2839 RVA: 0x000FF8B4 File Offset: 0x000FDAB4
		public static int F(NemesisRecord r, string facet)
		{
			int result;
			if (((r != null) ? r.Facets : null) == null || !r.Facets.TryGetValue(facet, out result))
			{
				return 50;
			}
			return result;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x000FF8E4 File Offset: 0x000FDAE4
		public static int V(NemesisRecord r, string value)
		{
			int result;
			if (((r != null) ? r.Values : null) == null || !r.Values.TryGetValue(value, out result))
			{
				return 0;
			}
			return result;
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000FF914 File Offset: 0x000FDB14
		public static int N(NemesisRecord r, string need)
		{
			int result;
			if (((r != null) ? r.Needs : null) == null || !r.Needs.TryGetValue(need, out result))
			{
				return 0;
			}
			return result;
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000FF944 File Offset: 0x000FDB44
		public static bool Has(NemesisRecord r, string goal)
		{
			return ((r != null) ? r.Goals : null) != null && r.Goals.Any((Goal g) => g.Type == goal);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x000FF988 File Offset: 0x000FDB88
		public static bool HasAgainstYou(NemesisRecord r, string goal)
		{
			return ((r != null) ? r.Goals : null) != null && r.Goals.Any((Goal g) => g.Type == goal && g.Target == "you");
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000FF9C9 File Offset: 0x000FDBC9
		private static int Clamp(int v, int lo = 0, int hi = 100)
		{
			return Math.Max(lo, Math.Min(hi, v));
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x000FF9D8 File Offset: 0x000FDBD8
		public static void Roll(NemesisRecord r, Random rng, long now)
		{
			Mind.<>c__DisplayClass10_0 CS$<>8__locals1 = new Mind.<>c__DisplayClass10_0();
			CS$<>8__locals1.rng = rng;
			CS$<>8__locals1.f = Mind.FacetNames.ToDictionary((string n) => n, (string n) => 50);
			Personality? personality = r.Personality;
			if (personality != null)
			{
				switch (personality.GetValueOrDefault())
				{
				case Personality.Hothead:
					CS$<>8__locals1.<Roll>g__Add|2("Anger", 30);
					CS$<>8__locals1.<Roll>g__Add|2("Bravery", 10);
					CS$<>8__locals1.<Roll>g__Add|2("Pride", 5);
					CS$<>8__locals1.<Roll>g__Add|2("Humour", -5);
					break;
				case Personality.Coward:
					CS$<>8__locals1.<Roll>g__Add|2("Bravery", -35);
					CS$<>8__locals1.<Roll>g__Add|2("Anger", -10);
					CS$<>8__locals1.<Roll>g__Add|2("Honesty", -5);
					break;
				case Personality.Proud:
					CS$<>8__locals1.<Roll>g__Add|2("Pride", 30);
					CS$<>8__locals1.<Roll>g__Add|2("Bravery", 10);
					CS$<>8__locals1.<Roll>g__Add|2("Ambition", 10);
					break;
				case Personality.Cold:
					CS$<>8__locals1.<Roll>g__Add|2("Anger", -15);
					CS$<>8__locals1.<Roll>g__Add|2("Humour", -20);
					CS$<>8__locals1.<Roll>g__Add|2("Cruelty", 12);
					CS$<>8__locals1.<Roll>g__Add|2("Bravery", 5);
					break;
				case Personality.Greedy:
					CS$<>8__locals1.<Roll>g__Add|2("Greed", 35);
					CS$<>8__locals1.<Roll>g__Add|2("Honesty", -15);
					CS$<>8__locals1.<Roll>g__Add|2("Ambition", 10);
					break;
				case Personality.Loyal:
					CS$<>8__locals1.<Roll>g__Add|2("Loyalty", 35);
					CS$<>8__locals1.<Roll>g__Add|2("Honesty", 10);
					break;
				case Personality.Drifter:
					CS$<>8__locals1.<Roll>g__Add|2("Ambition", -25);
					CS$<>8__locals1.<Roll>g__Add|2("Loyalty", -15);
					CS$<>8__locals1.<Roll>g__Add|2("Humour", 10);
					break;
				}
			}
			if (r.TraitsRolled)
			{
				CS$<>8__locals1.<Roll>g__Add|2("Anger", (r.Aggression - 50) / 2);
				CS$<>8__locals1.<Roll>g__Add|2("Cruelty", (r.Aggression - 50) / 3);
				if (r.Disposition == Disposition.Friendly)
				{
					CS$<>8__locals1.<Roll>g__Add|2("Humour", 15);
					CS$<>8__locals1.<Roll>g__Add|2("Cruelty", -10);
				}
				if (r.Disposition == Disposition.Surly)
				{
					CS$<>8__locals1.<Roll>g__Add|2("Humour", -15);
					CS$<>8__locals1.<Roll>g__Add|2("Anger", 5);
				}
				CS$<>8__locals1.<Roll>g__Add|2("Piety", (r.Devotion == Devotion.Devout) ? 35 : ((r.Devotion == Devotion.Churchgoer) ? 10 : ((r.Devotion == Devotion.Lapsed) ? -10 : -35)));
				if (r.Smarts == Smarts.Sharp)
				{
					CS$<>8__locals1.<Roll>g__Add|2("Ambition", 8);
				}
				if (r.Wealth == Wealth.Rich)
				{
					CS$<>8__locals1.<Roll>g__Add|2("Greed", 10);
					CS$<>8__locals1.<Roll>g__Add|2("Ambition", 10);
				}
				if (r.Wealth == Wealth.Poor)
				{
					CS$<>8__locals1.<Roll>g__Add|2("Greed", 8);
				}
			}
			foreach (PastEntry pastEntry in (r.Past ?? new List<PastEntry>()))
			{
				string kind = pastEntry.Kind;
				if (!(kind == "crime"))
				{
					if (!(kind == "war"))
					{
						if (!(kind == "loss"))
						{
							if (!(kind == "rank"))
							{
								if (!(kind == "gang"))
								{
									if (!(kind == "faith"))
									{
									}
								}
								else if (r.Gang != null)
								{
									CS$<>8__locals1.<Roll>g__Add|2("Loyalty", 6);
								}
							}
							else
							{
								CS$<>8__locals1.<Roll>g__Add|2("Ambition", 6);
							}
						}
						else
						{
							CS$<>8__locals1.<Roll>g__Add|2("Humour", -4);
							CS$<>8__locals1.<Roll>g__Add|2("Anger", 3);
						}
					}
					else
					{
						CS$<>8__locals1.<Roll>g__Add|2("Bravery", 6);
						CS$<>8__locals1.<Roll>g__Add|2("Cruelty", 4);
					}
				}
				else
				{
					CS$<>8__locals1.<Roll>g__Add|2("Honesty", -8);
					CS$<>8__locals1.<Roll>g__Add|2("Cruelty", 4);
				}
			}
			if (r.LifeRules >= 2)
			{
				Mind.LifeMarks(r, new Action<string, int>(CS$<>8__locals1.<Roll>g__Add|2));
			}
			CS$<>8__locals1.<Roll>g__Add|2("Ambition", r.Rank * 3);
			foreach (string key in Mind.FacetNames)
			{
				CS$<>8__locals1.f[key] = Mind.Clamp(CS$<>8__locals1.f[key] + CS$<>8__locals1.rng.Next(-10, 11), 0, 100);
			}
			r.Facets = CS$<>8__locals1.f;
			Dictionary<string, int> dictionary = Mind.ValueNames.ToDictionary((string n) => n, (string n) => 0);
			dictionary["Family"] = ((r.Family == "married") ? ((r.Children > 0) ? 3 : 2) : ((r.Family == "widower") ? 1 : ((r.Personality.GetValueOrDefault() == Personality.Drifter) ? -1 : 0)));
			dictionary["Gang"] = ((r.Gang == null) ? -1 : ((r.Personality.GetValueOrDefault() == Personality.Loyal) ? 3 : ((r.Personality.GetValueOrDefault() == Personality.Drifter) ? -1 : (1 + (CS$<>8__locals1.f["Loyalty"] - 50) / 25))));
			dictionary["Money"] = (CS$<>8__locals1.f["Greed"] - 50) / 16;
			dictionary["Honour"] = (CS$<>8__locals1.f["Pride"] - 50) / 16 + (CS$<>8__locals1.f["Honesty"] - 50) / 25;
			bool flag;
			if (!(r.Gang == "odriscoll") && !(r.Gang == "lemoyneraiders") && !(r.Trade == "Forajido"))
			{
				List<PastEntry> past = r.Past;
				if (past == null)
				{
					flag = false;
				}
				else
				{
					flag = past.Any((PastEntry e) => e.Kind == "crime");
				}
			}
			else
			{
				flag = true;
			}
			bool flag2 = flag;
			dictionary["Law"] = ((r.Gang == "army") ? 3 : (flag2 ? -2 : ((r.Trade == "Vecino del pueblo" || r.Trade == "Caballero" || r.Trade == "Dama" || r.Trade == "Maestra de escuela" || r.Trade == "Monja") ? 1 : 0)));
			dictionary["God"] = (CS$<>8__locals1.f["Piety"] - 50) / 16;
			foreach (string key2 in Mind.ValueNames)
			{
				dictionary[key2] = Mind.Clamp(dictionary[key2], -3, 3);
			}
			r.Values = dictionary;
			r.Needs = Mind.NeedNames.ToDictionary((string n) => n, (string n) => CS$<>8__locals1.rng.Next(10, 45));
			r.Stress = CS$<>8__locals1.rng.Next(5, 25);
			r.Memories = (r.Memories ?? new List<Memory>());
			r.Ties = (r.Ties ?? new List<Tie>());
			r.Goals = new List<Goal>();
			r.GoalsSet = false;
			r.MindRolled = true;
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0010018C File Offset: 0x000FE38C
		private static void LifeMarks(NemesisRecord r, Action<string, int> add)
		{
			if (r.LifeFx == null || r.LifeFx.Count == 0)
			{
				return;
			}
			Dictionary<string, int> dictionary = Mind.FacetNames.ToDictionary((string n) => n, (string n) => 0);
			foreach (string text in r.LifeFx)
			{
				int num;
				int num2;
				if (text.StartsWith("fear:") && int.TryParse(text.Substring(5), out num))
				{
					Dictionary<string, int> dictionary2 = dictionary;
					dictionary2["Bravery"] = dictionary2["Bravery"] - num * 2;
				}
				else if (text.StartsWith("aggr:") && int.TryParse(text.Substring(5), out num2))
				{
					Dictionary<string, int> dictionary2 = dictionary;
					dictionary2["Anger"] = dictionary2["Anger"] + num2;
					dictionary2 = dictionary;
					dictionary2["Cruelty"] = dictionary2["Cruelty"] + num2 / 2;
				}
				else if (text == "amistoso")
				{
					Dictionary<string, int> dictionary2 = dictionary;
					dictionary2["Humour"] = dictionary2["Humour"] + 4;
					dictionary2 = dictionary;
					dictionary2["Cruelty"] = dictionary2["Cruelty"] - 3;
				}
				else if (text == "hosco")
				{
					Dictionary<string, int> dictionary2 = dictionary;
					dictionary2["Humour"] = dictionary2["Humour"] - 4;
					dictionary2 = dictionary;
					dictionary2["Anger"] = dictionary2["Anger"] + 2;
				}
				else if (text == "devout")
				{
					Dictionary<string, int> dictionary2 = dictionary;
					dictionary2["Piety"] = dictionary2["Piety"] + 8;
				}
				else if (text == "godless")
				{
					Dictionary<string, int> dictionary2 = dictionary;
					dictionary2["Piety"] = dictionary2["Piety"] - 8;
				}
				else if (text == "astuto")
				{
					Dictionary<string, int> dictionary2 = dictionary;
					dictionary2["Ambition"] = dictionary2["Ambition"] + 4;
				}
				else if (text == "corto")
				{
					Dictionary<string, int> dictionary2 = dictionary;
					dictionary2["Ambition"] = dictionary2["Ambition"] - 4;
				}
				else if (text.StartsWith("t:"))
				{
					string[] array = text.Split(new char[]
					{
						':'
					});
					int num3;
					if (array.Length == 3 && int.TryParse(array[2], out num3))
					{
						string text2 = array[1];
						if (text2 != null)
						{
							switch (text2.Length)
							{
							case 4:
								if (text2 == "frío")
								{
									Dictionary<string, int> dictionary2 = dictionary;
									dictionary2["Humour"] = dictionary2["Humour"] - num3;
									dictionary2 = dictionary;
									dictionary2["Cruelty"] = dictionary2["Cruelty"] + num3 / 2;
								}
								break;
							case 5:
							{
								char c = text2[0];
								if (c != 'l')
								{
									if (c == 'p')
									{
										if (text2 == "orgulloso")
										{
											Dictionary<string, int> dictionary2 = dictionary;
											dictionary2["Pride"] = dictionary2["Pride"] + num3;
										}
									}
								}
								else if (text2 == "leal")
								{
									Dictionary<string, int> dictionary2 = dictionary;
									dictionary2["Loyalty"] = dictionary2["Loyalty"] + num3;
								}
								break;
							}
							case 6:
							{
								char c = text2[0];
								if (c != 'c')
								{
									if (c == 'g')
									{
										if (text2 == "codicioso")
										{
											Dictionary<string, int> dictionary2 = dictionary;
											dictionary2["Greed"] = dictionary2["Greed"] + num3;
											dictionary2 = dictionary;
											dictionary2["Honesty"] = dictionary2["Honesty"] - num3 / 2;
										}
									}
								}
								else if (text2 == "cobarde")
								{
									Dictionary<string, int> dictionary2 = dictionary;
									dictionary2["Bravery"] = dictionary2["Bravery"] - num3;
								}
								break;
							}
							case 7:
							{
								char c = text2[0];
								if (c != 'd')
								{
									if (c == 'h')
									{
										if (text2 == "impulsivo")
										{
											Dictionary<string, int> dictionary2 = dictionary;
											dictionary2["Anger"] = dictionary2["Anger"] + num3;
											dictionary2 = dictionary;
											dictionary2["Bravery"] = dictionary2["Bravery"] + num3 / 2;
										}
									}
								}
								else if (text2 == "errante")
								{
									Dictionary<string, int> dictionary2 = dictionary;
									dictionary2["Ambition"] = dictionary2["Ambition"] - num3;
									dictionary2 = dictionary;
									dictionary2["Loyalty"] = dictionary2["Loyalty"] - num3 / 2;
								}
								break;
							}
							}
						}
					}
				}
			}
			foreach (KeyValuePair<string, int> keyValuePair in dictionary)
			{
				if (keyValuePair.Value != 0 && Mind.FacetNames.Contains(keyValuePair.Key))
				{
					add(keyValuePair.Key, Math.Max(-15, Math.Min(15, keyValuePair.Value)));
				}
			}
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00100788 File Offset: 0x000FE988
		public static void InitGoals(NemesisRecord r, Random rng, long now)
		{
			List<Goal> list = new List<Goal>();
			int num = (r.BornYear > 0) ? (NemesisMod.GameYear() - r.BornYear) : 30;
			Gang gang = Gangs.Of(r);
			int num2 = (gang != null) ? gang.Tiers[0].Item2 : 5;
			if ((r.TimesKilledPlayer > 0 || r.HateTier == HateTier.Murderous) && Mind.F(r, "Anger") >= 55 && r.Gang != "army")
			{
				list.Add(new Goal
				{
					Type = "Avenge",
					Target = "you",
					Text = "zanjar esto contigo de una vez por todas",
					Since = now
				});
			}
			if (Mind.F(r, "Pride") >= 70 && num < 36 && r.Origin != GrudgeOrigin.GangMember && r.Gang != "army")
			{
				list.Add(new Goal
				{
					Type = "Prove",
					Target = "you",
					Text = "demostrar su valía venciéndote delante de todos",
					Since = now
				});
			}
			if (gang != null && Mind.F(r, "Ambition") >= 60 && r.Rank < num2)
			{
				list.Add(new Goal
				{
					Type = "Rise",
					Text = "ascender en las filas de " + gang.Name,
					Since = now
				});
			}
			if (Mind.F(r, "Greed") >= 70)
			{
				list.Add(new Goal
				{
					Type = "Rich",
					Text = "dar un gran golpe con el que retirarse",
					Since = now
				});
			}
			if (Mind.V(r, "Family") >= 2 && num >= 35 && Mind.F(r, "Ambition") < 55)
			{
				list.Add(new Goal
				{
					Type = "Settle",
					Text = "salir de esta vida y volver con su familia",
					Since = now
				});
			}
			if (r.KinId != null && Mind.F(r, "Loyalty") >= 60)
			{
				list.Add(new Goal
				{
					Type = "Protect",
					Target = r.KinId,
					Text = "mantener viva a su familia",
					Since = now
				});
			}
			r.Goals = list.Take(2).ToList<Goal>();
			r.GoalsSet = true;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x001009D0 File Offset: 0x000FEBD0
		public static bool Remember(NemesisRecord r, string emotion, int intensity, string text, string cause, long now, Random rng)
		{
			if (r == null || !r.MindRolled || intensity <= 0)
			{
				return false;
			}
			r.Memories = (r.Memories ?? new List<Memory>());
			Memory memory = r.Memories.FirstOrDefault((Memory m) => m.Emotion == emotion && m.Cause != null && m.Cause == cause && !m.LongTerm);
			if (memory != null)
			{
				memory.Intensity = Mind.Clamp(memory.Intensity + intensity / 2 + 5, 0, 100);
				memory.At = now;
				memory.Text = text;
			}
			else
			{
				List<Memory> memories = r.Memories;
				Memory memory2 = new Memory();
				memory2.Text = text;
				memory2.Emotion = emotion;
				memory2.Intensity = Mind.Clamp(intensity, 0, 100);
				memory2.Cause = cause;
				memory2.At = now;
				memory = memory2;
				memories.Add(memory2);
			}
			if (Mind.Bad.Contains(emotion))
			{
				r.Stress = Mind.Clamp(r.Stress + intensity / 8, 0, 100);
			}
			else
			{
				r.Stress = Mind.Clamp(r.Stress - intensity / 10, 0, 100);
			}
			bool result = false;
			if (!memory.LongTerm && memory.Intensity >= 65)
			{
				memory.LongTerm = true;
				result = true;
				Mind.Drift(r, emotion, cause);
			}
			if (r.Memories.Count > 12)
			{
				r.Memories = r.Memories.OrderByDescending(delegate(Memory m)
				{
					if (!m.LongTerm)
					{
						return m.Intensity;
					}
					return m.Intensity + 50;
				}).Take(12).ToList<Memory>();
			}
			return result;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00100B68 File Offset: 0x000FED68
		private static void Drift(NemesisRecord r, string emotion, string cause)
		{
			Mind.<>c__DisplayClass14_0 CS$<>8__locals1;
			CS$<>8__locals1.r = r;
			if (!(emotion == "Anger"))
			{
				if (!(emotion == "Fear"))
				{
					if (!(emotion == "Shame"))
					{
						if (!(emotion == "Grief"))
						{
							if (!(emotion == "Joy"))
							{
								if (emotion == "Pride")
								{
									Mind.<Drift>g__Add|14_0("Pride", 3, ref CS$<>8__locals1);
									Mind.<Drift>g__Add|14_0("Bravery", 3, ref CS$<>8__locals1);
								}
							}
							else
							{
								Mind.<Drift>g__Add|14_0("Humour", 4, ref CS$<>8__locals1);
								Mind.<Drift>g__Add|14_0("Anger", -3, ref CS$<>8__locals1);
							}
						}
						else
						{
							Mind.<Drift>g__Add|14_0("Anger", Math.Max(0, Math.Min(3, Mind.Baseline(CS$<>8__locals1.r, "Anger") + 25 - Mind.F(CS$<>8__locals1.r, "Anger"))), ref CS$<>8__locals1);
							Mind.<Drift>g__Add|14_0("Humour", -5, ref CS$<>8__locals1);
							Mind.<Drift>g__Add|14_0("Loyalty", 2, ref CS$<>8__locals1);
						}
					}
					else if (Mind.F(CS$<>8__locals1.r, "Pride") >= 55)
					{
						Mind.<Drift>g__Add|14_0("Pride", 3, ref CS$<>8__locals1);
						Mind.<Drift>g__Add|14_0("Anger", Math.Max(0, Math.Min(4, Mind.Baseline(CS$<>8__locals1.r, "Anger") + 25 - Mind.F(CS$<>8__locals1.r, "Anger"))), ref CS$<>8__locals1);
					}
					else
					{
						Mind.<Drift>g__Add|14_0("Bravery", -4, ref CS$<>8__locals1);
						Mind.<Drift>g__Add|14_0("Pride", -3, ref CS$<>8__locals1);
					}
				}
				else
				{
					Mind.<Drift>g__Add|14_0("Bravery", -6, ref CS$<>8__locals1);
				}
			}
			else
			{
				Mind.<Drift>g__Add|14_0("Anger", Math.Max(0, Math.Min(5, Mind.Baseline(CS$<>8__locals1.r, "Anger") + 25 - Mind.F(CS$<>8__locals1.r, "Anger"))), ref CS$<>8__locals1);
				Mind.<Drift>g__Add|14_0("Cruelty", 2, ref CS$<>8__locals1);
			}
			if (cause == "you" && (emotion == "Anger" || emotion == "Shame" || emotion == "Grief") && Mind.F(CS$<>8__locals1.r, "Anger") >= 50 && !Mind.HasAgainstYou(CS$<>8__locals1.r, "Avenge") && CS$<>8__locals1.r.Gang != "army")
			{
				Mind.SetGoal(CS$<>8__locals1.r, new Goal
				{
					Type = "Avenge",
					Target = "you",
					Text = ((emotion == "Grief") ? "hacerte pagar por quien le arrebataste" : "hacerte pagar por lo que hiciste")
				});
			}
			if (cause == "you" && emotion == "Fear" && Mind.F(CS$<>8__locals1.r, "Bravery") < 35 && !Mind.Has(CS$<>8__locals1.r, "Settle"))
			{
				Mind.SetGoal(CS$<>8__locals1.r, new Goal
				{
					Type = "Settle",
					Text = "get away from you - out of this life"
				});
			}
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00100E78 File Offset: 0x000FF078
		public static void SetGoal(NemesisRecord r, Goal g)
		{
			r.Goals = (r.Goals ?? new List<Goal>());
			r.Goals.RemoveAll((Goal x) => x.Type == g.Type);
			if (g.Type == "Avenge")
			{
				r.Goals.RemoveAll((Goal x) => x.Type == "Settle");
			}
			if (g.Type == "Settle")
			{
				r.Goals.RemoveAll((Goal x) => x.Type == "Prove" || x.Type == "Rise");
			}
			r.Goals.Insert(0, g);
			while (r.Goals.Count > 2)
			{
				r.Goals.RemoveAt(r.Goals.Count - 1);
			}
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x00100F80 File Offset: 0x000FF180
		public static void Fade(NemesisRecord r)
		{
			if (r.Memories == null)
			{
				return;
			}
			foreach (Memory memory in r.Memories)
			{
				memory.Intensity = (memory.LongTerm ? Math.Max(30, memory.Intensity - 1) : (memory.Intensity - 10));
			}
			r.Memories.RemoveAll((Memory m) => m.Intensity <= 0);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x00101028 File Offset: 0x000FF228
		public static Memory Strongest(NemesisRecord r)
		{
			List<Memory> memories = r.Memories;
			if (memories == null)
			{
				return null;
			}
			return (from m in memories
			orderby m.Intensity + (m.LongTerm ? 20 : 0) descending
			select m).FirstOrDefault<Memory>();
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x00101060 File Offset: 0x000FF260
		public static Tie TieTo(NemesisRecord r, string id)
		{
			if (r == null)
			{
				return null;
			}
			List<Tie> ties = r.Ties;
			if (ties == null)
			{
				return null;
			}
			return ties.FirstOrDefault((Tie t) => t.Id == id);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0010109C File Offset: 0x000FF29C
		public static void SetTie(NemesisRecord r, string id, string type, int strength, string note = null)
		{
			if (r == null || id == null || id == r.Id)
			{
				return;
			}
			r.Ties = (r.Ties ?? new List<Tie>());
			Tie tie = r.Ties.FirstOrDefault((Tie x) => x.Id == id);
			if (tie == null)
			{
				List<Tie> ties = r.Ties;
				Tie tie2 = new Tie();
				tie2.Id = id;
				tie = tie2;
				ties.Add(tie2);
			}
			if (!(tie.Type == "kin") || !(type != "kin"))
			{
				tie.Type = type;
			}
			tie.Strength = Mind.Clamp(strength, -100, 100);
			if (note != null)
			{
				tie.Note = note;
			}
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x00101168 File Offset: 0x000FF368
		public static float HateGain(NemesisRecord r)
		{
			if (r != null && r.MindRolled)
			{
				return 1f + Math.Max(-0.2f, Math.Min(0.2f, (float)(Mind.F(r, "Anger") - Mind.Baseline(r, "Anger")) / 100f));
			}
			return 1f;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x001011C0 File Offset: 0x000FF3C0
		public static float FearGain(NemesisRecord r)
		{
			if (r != null && r.MindRolled)
			{
				return 1f - Math.Max(-0.3f, Math.Min(0.3f, (float)(Mind.F(r, "Bravery") - Mind.Baseline(r, "Bravery")) / 100f));
			}
			return 1f;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x00101218 File Offset: 0x000FF418
		public static int Baseline(NemesisRecord r, string facet)
		{
			int num = 50;
			if (facet == "Anger")
			{
				int num2 = num;
				Personality? personality = r.Personality;
				Personality personality2 = Personality.Hothead;
				num = num2 + ((personality.GetValueOrDefault() == personality2 & personality != null) ? 30 : ((r.Personality.GetValueOrDefault() == Personality.Coward) ? -10 : ((r.Personality.GetValueOrDefault() == Personality.Cold) ? -15 : 0)));
				if (r.TraitsRolled)
				{
					num += (r.Aggression - 50) / 2;
					if (r.Disposition == Disposition.Surly)
					{
						num += 5;
					}
				}
			}
			else if (facet == "Bravery")
			{
				int num3 = num;
				Personality? personality = r.Personality;
				Personality personality2 = Personality.Hothead;
				num = num3 + ((personality.GetValueOrDefault() == personality2 & personality != null) ? 10 : ((r.Personality.GetValueOrDefault() == Personality.Coward) ? -35 : ((r.Personality.GetValueOrDefault() == Personality.Proud) ? 10 : ((r.Personality.GetValueOrDefault() == Personality.Cold) ? 5 : 0))));
			}
			return Mind.Clamp(num, 0, 100);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00101328 File Offset: 0x000FF528
		public static double AmbushMult(NemesisRecord r)
		{
			if (r == null || !r.MindRolled)
			{
				return 1.0;
			}
			double num = 1.0;
			if (Mind.HasAgainstYou(r, "Avenge"))
			{
				num *= 1.5;
			}
			if (Mind.HasAgainstYou(r, "Prove"))
			{
				num *= 1.4;
			}
			if (Mind.Has(r, "Settle"))
			{
				num *= 0.5;
			}
			if (r.Stress >= 70)
			{
				num *= 1.2;
			}
			return num;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x001013B8 File Offset: 0x000FF5B8
		public static Intent ShiftIntent(NemesisRecord r, Intent i, int effectiveFear)
		{
			if (r == null || !r.MindRolled || i == Intent.None)
			{
				return i;
			}
			if (Mind.Has(r, "Settle") && i == Intent.Hunts)
			{
				return Intent.WontHunt;
			}
			if ((Mind.HasAgainstYou(r, "Avenge") || Mind.HasAgainstYou(r, "Prove")) && i != Intent.Hunts && effectiveFear < Balance.WontHuntFear && r.HateTier != HateTier.Resentful)
			{
				return Intent.Hunts;
			}
			return i;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00101418 File Offset: 0x000FF618
		public static int BackDownBonus(NemesisRecord r)
		{
			if (r != null && r.MindRolled)
			{
				return (50 - Mind.F(r, "Pride")) / 4 + (50 - Mind.F(r, "Bravery")) / 5 + (Mind.Has(r, "Settle") ? 15 : 0) - (Mind.HasAgainstYou(r, "Prove") ? 15 : 0);
			}
			return 0;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0010147C File Offset: 0x000FF67C
		[return: TupleElementNames(new string[]
		{
			"bias",
			"why"
		})]
		public static ValueTuple<int, string> TalkBias(NemesisRecord r, Func<string, NemesisRecord> byId)
		{
			if (r == null || !r.MindRolled)
			{
				return new ValueTuple<int, string>(0, "");
			}
			int num = (50 - Mind.F(r, "Loyalty")) / 6 + (r.Stress - 40) / 8 - Mind.V(r, "Gang") * 3;
			List<string> list = new List<string>
			{
				string.Format("loyalty {0:+0;-0;0}", (50 - Mind.F(r, "Loyalty")) / 6),
				string.Format("stress {0:+0;-0;0}", (r.Stress - 40) / 8)
			};
			if (Mind.V(r, "Gang") != 0)
			{
				list.Add(string.Format("gang {0:+0;-0;0}", -Mind.V(r, "Gang") * 3));
			}
			NemesisRecord nemesisRecord = (r.BossId != null) ? byId(r.BossId) : null;
			Tie tie = (nemesisRecord != null) ? Mind.TieTo(r, nemesisRecord.Id) : null;
			if (tie != null && tie.Strength <= -30)
			{
				num += 12;
				list.Add("hates his boss +12");
			}
			List<Tie> ties = r.Ties;
			bool flag;
			if (ties == null)
			{
				flag = false;
			}
			else
			{
				flag = ties.Any((Tie t) => t.Type == "rival");
			}
			if (flag)
			{
				num += 5;
				list.Add("a rival to sell out +5");
			}
			return new ValueTuple<int, string>(num, string.Join(" ", list));
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x001015E4 File Offset: 0x000FF7E4
		[return: TupleElementNames(new string[]
		{
			"bonus",
			"why"
		})]
		public static ValueTuple<int, string> StandingBonus(NemesisRecord r)
		{
			if (r == null || !r.MindRolled)
			{
				return new ValueTuple<int, string>(0, "");
			}
			return new ValueTuple<int, string>(Mind.F(r, "Ambition") / 5 - 10 + (Mind.Has(r, "Rise") ? 15 : 0) - (Mind.Has(r, "Settle") ? 15 : 0), Mind.Has(r, "Rise") ? "wants to rise" : ((Mind.F(r, "Ambition") >= 65) ? "ambicioso" : ""));
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00101674 File Offset: 0x000FF874
		public static string Felt(string emotion)
		{
			if (emotion == "Anger")
			{
				return "angry";
			}
			if (emotion == "Fear")
			{
				return "afraid";
			}
			if (emotion == "Shame")
			{
				return "ashamed";
			}
			if (emotion == "Grief")
			{
				return "grieving";
			}
			if (emotion == "Joy")
			{
				return "glad";
			}
			if (emotion == "Pride")
			{
				return "orgulloso";
			}
			if (emotion == null)
			{
				return null;
			}
			return emotion.ToLowerInvariant();
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00101700 File Offset: 0x000FF900
		[return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static ValueTuple<int, int, int> EmotionColor(string emotion)
		{
			if (emotion == "Anger")
			{
				return new ValueTuple<int, int, int>(200, 80, 64);
			}
			if (emotion == "Fear")
			{
				return new ValueTuple<int, int, int>(150, 170, 210);
			}
			if (emotion == "Shame")
			{
				return new ValueTuple<int, int, int>(200, 150, 90);
			}
			if (emotion == "Grief")
			{
				return new ValueTuple<int, int, int>(160, 126, 160);
			}
			if (emotion == "Joy")
			{
				return new ValueTuple<int, int, int>(130, 190, 110);
			}
			if (!(emotion == "Pride"))
			{
				return new ValueTuple<int, int, int>(176, 164, 138);
			}
			return new ValueTuple<int, int, int>(215, 178, 95);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x001017E0 File Offset: 0x000FF9E0
		public static string Describe(NemesisRecord r, int take = 4)
		{
			if (((r != null) ? r.Facets : null) == null)
			{
				return "-";
			}
			return string.Join(", ", from kv in (from kv in r.Facets
			orderby Math.Abs(kv.Value - 50) descending
			select kv).Take(take)
			select ((Math.Abs(kv.Value - 50) >= 35) ? "very " : "") + Mind.<Describe>g__Word|30_0(kv.Key, kv.Value));
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00101963 File Offset: 0x000FFB63
		[CompilerGenerated]
		internal static void <Drift>g__Add|14_0(string n, int d, ref Mind.<>c__DisplayClass14_0 A_2)
		{
			if (A_2.r.Facets != null)
			{
				A_2.r.Facets[n] = Mind.Clamp(Mind.F(A_2.r, n) + d, 0, 100);
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x0010199C File Offset: 0x000FFB9C
		[CompilerGenerated]
		internal static string <Describe>g__Word|30_0(string n, int v)
		{
			bool flag = v >= 50;
			if (n != null)
			{
				switch (n.Length)
				{
				case 5:
				{
					char c = n[4];
					if (c <= 'e')
					{
						if (c != 'd')
						{
							if (c == 'e')
							{
								if (n == "Pride")
								{
									if (!flag)
									{
										return "humilde";
									}
									return "orgulloso";
								}
							}
						}
						else if (n == "Greed")
						{
							if (!flag)
							{
								return "generoso";
							}
							return "codicioso";
						}
					}
					else if (c != 'r')
					{
						if (c == 'y')
						{
							if (n == "Piety")
							{
								if (!flag)
								{
									return "sin fe";
								}
								return "piadoso";
							}
						}
					}
					else if (n == "Anger")
					{
						if (!flag)
						{
							return "tardo para la ira";
						}
						return "rápido para la ira";
					}
					break;
				}
				case 6:
					if (n == "Humour")
					{
						if (!flag)
						{
							return "sin sentido del humor";
						}
						return "bromista";
					}
					break;
				case 7:
				{
					char c = n[0];
					if (c <= 'C')
					{
						if (c != 'B')
						{
							if (c == 'C')
							{
								if (n == "Cruelty")
								{
									if (!flag)
									{
										return "amable";
									}
									return "cruel";
								}
							}
						}
						else if (n == "Bravery")
						{
							if (!flag)
							{
								return "tímido";
							}
							return "valiente";
						}
					}
					else if (c != 'H')
					{
						if (c == 'L')
						{
							if (n == "Loyalty")
							{
								if (!flag)
								{
									return "desleal";
								}
								return "leal";
							}
						}
					}
					else if (n == "Honesty")
					{
						if (!flag)
						{
							return "deshonesto";
						}
						return "honest";
					}
					break;
				}
				case 8:
					if (n == "Ambition")
					{
						if (!flag)
						{
							return "sin ambición";
						}
						return "ambicioso";
					}
					break;
				}
			}
			return n;
		}

		// Token: 0x040007E5 RID: 2021
		public static readonly string[] FacetNames = new string[]
		{
			"Anger",
			"Bravery",
			"Greed",
			"Loyalty",
			"Pride",
			"Honesty",
			"Piety",
			"Cruelty",
			"Humour",
			"Ambition"
		};

		// Token: 0x040007E6 RID: 2022
		public static readonly string[] ValueNames = new string[]
		{
			"Family",
			"Gang",
			"Money",
			"Honour",
			"Law",
			"God"
		};

		// Token: 0x040007E7 RID: 2023
		public static readonly string[] NeedNames = new string[]
		{
			"Drink",
			"Company",
			"Faith",
			"Money",
			"Violence"
		};

		// Token: 0x040007E8 RID: 2024
		public static readonly string[] Bad = new string[]
		{
			"Anger",
			"Fear",
			"Shame",
			"Grief"
		};
	}
}
