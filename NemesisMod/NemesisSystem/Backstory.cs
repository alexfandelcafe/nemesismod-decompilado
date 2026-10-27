using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x0200000B RID: 11
	public static class Backstory
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002744 File Offset: 0x00000944
		private static LifeT L(string kind, string who, int minAge, int maxAge, int w, string text, string tilt = "", string fx = "", bool pub = false, int bornFrom = 0, int bornTo = 0, string bornIn = null, string mirrorKind = null, string mirrorTitle = null, bool mirrorHeard = false)
		{
			LifeT lifeT = new LifeT
			{
				Kind = kind,
				Who = who,
				MinAge = minAge,
				MaxAge = maxAge,
				W = w,
				Text = text,
				Fx = fx,
				Public = pub,
				TiltText = tilt,
				BornFrom = bornFrom,
				BornTo = bornTo,
				BornIn = bornIn,
				MirrorKind = mirrorKind,
				MirrorTitle = mirrorTitle,
				MirrorHeard = mirrorHeard
			};
			string[] array = (tilt ?? "").Split(new char[]
			{
				' '
			}, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[]
				{
					':'
				});
				Personality key;
				int value;
				if (array2.Length == 2 && Enum.TryParse<Personality>(array2[0], true, out key) && int.TryParse(array2[1], out value))
				{
					lifeT.Tilt[key] = value;
				}
			}
			return lifeT;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000283C File Offset: 0x00000A3C
		public static LivesContent Export()
		{
			LivesContent livesContent = new LivesContent();
			foreach (ValueTuple<string, Func<string[]>, Action<string[]>> valueTuple in Backstory.PlaceSlots)
			{
				livesContent.Places[valueTuple.Item1] = valueTuple.Item2().ToList<string>();
			}
			livesContent.GreyBattles = (from b in Backstory.GreyBattles
			select new Battle
			{
				Name = b.Item1,
				Year = b.Item2
			}).ToList<Battle>();
			livesContent.BlueBattles = (from b in Backstory.BlueBattles
			select new Battle
			{
				Name = b.Item1,
				Year = b.Item2
			}).ToList<Battle>();
			foreach (ValueTuple<string, Func<LifeT[]>, Action<LifeT[]>> valueTuple2 in Backstory.PoolSlots)
			{
				livesContent.Pools[valueTuple2.Item1] = (from t in valueTuple2.Item2()
				select new LifeRow
				{
					Id = LifeCond.Slug(t.Kind + " " + t.Text),
					Kind = t.Kind,
					Who = t.Who,
					MinAge = t.MinAge,
					MaxAge = t.MaxAge,
					Weight = t.W,
					Text = t.Text,
					Tilt = t.TiltText,
					Fx = t.Fx,
					Public = t.Public,
					BornFrom = t.BornFrom,
					BornTo = t.BornTo,
					BornIn = t.BornIn,
					MirrorKind = t.MirrorKind,
					MirrorTitle = t.MirrorTitle,
					MirrorHeard = t.MirrorHeard
				}).ToList<LifeRow>();
			}
			return livesContent;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000295C File Offset: 0x00000B5C
		public static int Import(LivesContent c)
		{
			int num = 0;
			if (c == null)
			{
				return 0;
			}
			foreach (ValueTuple<string, Func<string[]>, Action<string[]>> valueTuple in Backstory.PlaceSlots)
			{
				List<string> list;
				if (c.Places != null && c.Places.TryGetValue(valueTuple.Item1, out list) && list != null && list.Count > 0)
				{
					valueTuple.Item3(list.ToArray());
				}
			}
			List<Battle> greyBattles = c.GreyBattles;
			if (greyBattles != null && greyBattles.Count > 0)
			{
				Backstory.GreyBattles = (from b in c.GreyBattles
				select new ValueTuple<string, int>(b.Name, b.Year)).ToArray<ValueTuple<string, int>>();
			}
			List<Battle> blueBattles = c.BlueBattles;
			if (blueBattles != null && blueBattles.Count > 0)
			{
				Backstory.BlueBattles = (from b in c.BlueBattles
				select new ValueTuple<string, int>(b.Name, b.Year)).ToArray<ValueTuple<string, int>>();
			}
			ValueTuple<string, Func<LifeT[]>, Action<LifeT[]>>[] poolSlots = Backstory.PoolSlots;
			for (int i = 0; i < poolSlots.Length; i++)
			{
				ValueTuple<string, Func<LifeT[]>, Action<LifeT[]>> p = poolSlots[i];
				List<LifeRow> list2;
				if (c.Pools != null && c.Pools.TryGetValue(p.Item1, out list2) && list2 != null && list2.Count != 0)
				{
					List<LifeT> list3 = (from x in list2
					where !string.IsNullOrEmpty(x.Text)
					select Backstory.L(x.Kind ?? p.Item1, x.Who ?? "any", x.MinAge, Math.Max(x.MinAge, x.MaxAge), Math.Max(0, x.Weight), x.Text, x.Tilt ?? "", x.Fx ?? "", x.Public, x.BornFrom, x.BornTo, x.BornIn, x.MirrorKind, x.MirrorTitle, x.MirrorHeard)).ToList<LifeT>();
					HashSet<string> ids = new HashSet<string>(list2.Select(delegate(LifeRow x)
					{
						if (!string.IsNullOrEmpty(x.Id))
						{
							return x.Id;
						}
						return LifeCond.Slug((x.Kind ?? p.Item1) + " " + x.Text);
					}));
					HashSet<string> removed = new HashSet<string>(c.Removed ?? new List<string>());
					List<LifeT> list4 = p.Item2().Where(delegate(LifeT t)
					{
						string item = LifeCond.Slug(t.Kind + " " + t.Text);
						return !ids.Contains(item) && !removed.Contains(item);
					}).ToList<LifeT>();
					if (list4.Count > 0)
					{
						list3.AddRange(list4);
						Backstory.AddedFromBuiltIn += list4.Count;
					}
					p.Item3(list3.ToArray());
					num += list2.Count;
				}
			}
			return num;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public static bool KinAgeFits(NemesisRecord r, NemesisRecord kin)
		{
			if (kin == null || kin.BornYear <= 0 || r.BornYear <= 0)
			{
				return true;
			}
			int num = r.BornYear - kin.BornYear;
			string kinRelation = r.KinRelation;
			if (kinRelation == "hijo")
			{
				return num >= 16 && num <= 40;
			}
			if (kinRelation == "padre")
			{
				return num <= -16 && num >= -40;
			}
			if (kinRelation == "sobrino")
			{
				return num >= 12 && num <= 32;
			}
			if (kinRelation == "tío")
			{
				return num <= -12 && num >= -32;
			}
			if (!(kinRelation == "hermano"))
			{
				return Math.Abs(num) <= 16;
			}
			return Math.Abs(num) <= 12;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002C98 File Offset: 0x00000E98
		public static string RelationForAge(NemesisRecord r, NemesisRecord kin)
		{
			if (kin == null || kin.BornYear <= 0 || r.BornYear <= 0)
			{
				return r.KinRelation;
			}
			if (Backstory.KinAgeFits(r, kin))
			{
				return r.KinRelation;
			}
			int num = r.BornYear - kin.BornYear;
			if (Math.Abs(num) <= 12)
			{
				return "hermano";
			}
			if (num >= 16 && num <= 40)
			{
				return "hijo";
			}
			if (num <= -16 && num >= -40)
			{
				return "padre";
			}
			if (Math.Abs(num) <= 16)
			{
				return "primo";
			}
			return null;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002D20 File Offset: 0x00000F20
		public static bool FitKinAge(NemesisRecord r, NemesisRecord kin, Random rng, int year)
		{
			Backstory.<>c__DisplayClass27_0 CS$<>8__locals1;
			CS$<>8__locals1.rng = rng;
			CS$<>8__locals1.year = year;
			if (kin == null || kin.BornYear <= 0)
			{
				return false;
			}
			CS$<>8__locals1.kb = kin.BornYear;
			int num = Backstory.<FitKinAge>g__Born|27_0(r.KinRelation ?? "hermano", ref CS$<>8__locals1);
			if (!Backstory.<FitKinAge>g__Fits|27_1(num, ref CS$<>8__locals1))
			{
				r.KinRelation = "hermano";
				num = Backstory.<FitKinAge>g__Born|27_0("hermano", ref CS$<>8__locals1);
			}
			if (!Backstory.<FitKinAge>g__Fits|27_1(num, ref CS$<>8__locals1))
			{
				num = Math.Max(CS$<>8__locals1.year - 74, Math.Min(CS$<>8__locals1.year - 16, num));
			}
			if ((r.KinRelation == "hermano" || r.KinRelation == "hijo" || r.KinRelation == "padre") && kin.Birthplace != null && CS$<>8__locals1.rng.Next(100) < 75)
			{
				r.Birthplace = kin.Birthplace;
			}
			bool result = r.BornYear != num;
			r.BornYear = num;
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002E2C File Offset: 0x0000102C
		public static BackstoryResult Roll(NemesisRecord r, Random rng, int year, int month, bool derive, bool keepBorn = false)
		{
			BackstoryResult backstoryResult = new BackstoryResult();
			r.Past = new List<PastEntry>();
			int num = (r.BornYear > 0 && (!derive || keepBorn)) ? (year - r.BornYear) : Backstory.AgeFor(r, rng);
			r.BornYear = year - num;
			int month2 = 1 + rng.Next(12);
			Backstory.Ctx c = new Backstory.Ctx
			{
				Year = year,
				Month = month,
				Age = num,
				Rng = rng,
				R = r
			};
			c.Irish = (r.Gang == "odriscoll" || (r.Name != null && NameBank.IrishLast.Any((string l) => r.Name.EndsWith(" " + l))));
			c.Born = (r.Birthplace ?? Backstory.BirthplaceFor(r, c, rng));
			r.Birthplace = c.Born;
			c.IrishBorn = (c.Born.Contains("Ireland") || c.Born.StartsWith("County"));
			if (c.IrishBorn && !c.Born.Contains("Ireland"))
			{
				Backstory.Ctx c2 = c;
				c2.Born += ", Irlanda";
				r.Birthplace = c.Born;
			}
			c.South = (r.Gang == "lemoyneraiders" || r.Gang == "klan" || Backstory.Southern.Any((string s) => c.Born.StartsWith(s.Split(new char[]
			{
				','
			})[0])));
			c.North = (!c.South && !c.IrishBorn && (r.Gang == "army" || Backstory.Northern.Any((string s) => c.Born.StartsWith(s.Split(new char[]
			{
				','
			})[0]))));
			c.Home = (c.IrishBorn ? c.Born.Split(new char[]
			{
				','
			})[0] : c.Born.Split(new char[]
			{
				','
			})[0]);
			Backstory.Add(r, backstoryResult, null, r.BornYear, month2, "Nacido en " + c.Born, "born", null, null, true, true);
			Backstory.Pick(r, backstoryResult, c, Backstory.Boyhood, derive, 0);
			if (c.IrishBorn || c.Born.StartsWith("Bavaria"))
			{
				int year2 = Math.Min(year, r.BornYear + 9 + rng.Next(12));
				Backstory.Add(r, backstoryResult, null, year2, 1 + rng.Next(12), c.IrishBorn ? "Cruzó de Queenstown a Nueva York en tercera clase" : "Cruzó de Hamburgo a Nueva York con su familia", "youth", "loyal:1 cold:1", null, true, derive);
			}
			Backstory.Pick(r, backstoryResult, c, Backstory.Youth, derive, 0);
			Backstory.War(r, backstoryResult, c, rng);
			if (r.Gang == null || r.Gang == "klan")
			{
				Backstory.Pick(r, backstoryResult, c, Backstory.Work, derive, 0);
			}
			bool flag = r.Gang == "odriscoll" || r.Gang == "lemoyneraiders" || r.Trade == "Forajido" || r.Trade == "Errante" || r.Trade == "Vagabundo";
			if (r.Gang != "army" && r.Gang != "wapiti" && r.Gang != "nightfolk" && r.Gang != "delobo" && rng.Next(100) < (flag ? 55 : 12))
			{
				Backstory.Pick(r, backstoryResult, c, Backstory.Crime, derive, 0);
			}
			if (rng.Next(100) < 30 && num >= 40 && (r.Trade == "Granjero" || r.Trade == "Ranchero" || r.Trade == "Vecino del pueblo" || r.Trade == "Caballero" || r.Trade == "Dama" || r.Trade == "Tendera" || r.Trade == "Colona" || r.Trade == "Granjera" || r.Gang == "klan") && year >= 1893 && r.BornYear <= 1873)
			{
				Backstory.Add(r, backstoryResult, null, 1893, 5 + rng.Next(6), (r.Trade == "Caballero") ? "Perdió la mitad de su fortuna cuando quebraron los bancos en el Pánico del 93" : "Lo perdió todo cuando quebraron los bancos en el Pánico del 93", "loss", "greedy:3 cold:1", "hosco", false, derive);
			}
			int minYear = Backstory.JoinUp(r, backstoryResult, c, rng);
			if (r.Gang != null)
			{
				Backstory.Pick(r, backstoryResult, c, Backstory.GangLife, derive, minYear);
			}
			Backstory.Family(r, backstoryResult, c, rng, derive);
			int num2 = (num >= 40) ? 2 : 1;
			for (int i = 0; i < num2; i++)
			{
				Backstory.Pick(r, backstoryResult, c, Backstory.Adult, derive, 0);
			}
			foreach (PastEntry pastEntry in r.Past)
			{
				if (pastEntry.Year >= year)
				{
					pastEntry.Year = year;
					if (pastEntry.Month > month)
					{
						pastEntry.Month = month;
					}
				}
			}
			r.Past = (from e in r.Past
			orderby e.Year, e.Month
			select e).ToList<PastEntry>();
			r.PastRolled = true;
			return backstoryResult;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x000035F4 File Offset: 0x000017F4
		private static int AgeFor(NemesisRecord r, Random rng)
		{
			Backstory.<>c__DisplayClass29_0 CS$<>8__locals1;
			CS$<>8__locals1.rng = rng;
			string a = r.Gang;
			if (!(a == "odriscoll"))
			{
				if (!(a == "lemoyneraiders"))
				{
					if (!(a == "army"))
					{
						if (a == "klan")
						{
							return Backstory.<AgeFor>g__Between|29_0(25, 60, ref CS$<>8__locals1);
						}
						if (!(a == "wapiti"))
						{
							a = r.Trade;
							if (a == "Vagabundo")
							{
								return Backstory.<AgeFor>g__Between|29_0(28, 65, ref CS$<>8__locals1);
							}
							if (a == "Caballero" || a == "Médico" || a == "Predicador")
							{
								return Backstory.<AgeFor>g__Between|29_0(30, 62, ref CS$<>8__locals1);
							}
							if (!(a == "Vaquero") && !(a == "Jornalero"))
							{
								return Backstory.<AgeFor>g__Between|29_0(19, 58, ref CS$<>8__locals1);
							}
							return Backstory.<AgeFor>g__Between|29_0(18, 40, ref CS$<>8__locals1);
						}
						else
						{
							if (r.Rank >= 4)
							{
								return Backstory.<AgeFor>g__Between|29_0(36, 58, ref CS$<>8__locals1);
							}
							if (r.Rank < 2)
							{
								return Backstory.<AgeFor>g__Between|29_0(17, 30, ref CS$<>8__locals1);
							}
							return Backstory.<AgeFor>g__Between|29_0(24, 44, ref CS$<>8__locals1);
						}
					}
					else
					{
						if (r.Rank >= 4)
						{
							return Backstory.<AgeFor>g__Between|29_0(32, 55, ref CS$<>8__locals1);
						}
						if (r.Rank < 2)
						{
							return Backstory.<AgeFor>g__Between|29_0(18, 28, ref CS$<>8__locals1);
						}
						return Backstory.<AgeFor>g__Between|29_0(26, 42, ref CS$<>8__locals1);
					}
				}
				else
				{
					if (r.Rank >= 4)
					{
						return Backstory.<AgeFor>g__Between|29_0(45, 62, ref CS$<>8__locals1);
					}
					if (r.Rank >= 2)
					{
						return Backstory.<AgeFor>g__Between|29_0(34, 58, ref CS$<>8__locals1);
					}
					if (CS$<>8__locals1.rng.Next(2) != 0)
					{
						return Backstory.<AgeFor>g__Between|29_0(46, 60, ref CS$<>8__locals1);
					}
					return Backstory.<AgeFor>g__Between|29_0(18, 34, ref CS$<>8__locals1);
				}
			}
			else
			{
				if (r.Rank >= 4)
				{
					return Backstory.<AgeFor>g__Between|29_0(30, 48, ref CS$<>8__locals1);
				}
				if (r.Rank < 2)
				{
					return Backstory.<AgeFor>g__Between|29_0(17, 30, ref CS$<>8__locals1);
				}
				return Backstory.<AgeFor>g__Between|29_0(24, 40, ref CS$<>8__locals1);
			}
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000037E4 File Offset: 0x000019E4
		private static string BirthplaceFor(NemesisRecord r, Backstory.Ctx c, Random rng)
		{
			if (r.Gang == "wapiti")
			{
				return (new string[]
				{
					"las llanuras del norte",
					"las llanuras del norte",
					"los Grizzlies, Ambarino",
					"la cuenca del río Dakota"
				})[rng.Next(4)];
			}
			if (c.Irish)
			{
				if (rng.Next(100) >= 60)
				{
					return Backstory.IrishAmerican[rng.Next(Backstory.IrishAmerican.Length)];
				}
				return Backstory.IrishCounties[rng.Next(Backstory.IrishCounties.Length)] + ", Irlanda";
			}
			else
			{
				if (r.Gang == "lemoyneraiders" || r.Gang == "klan")
				{
					return Backstory.Southern[rng.Next(Backstory.Southern.Length)];
				}
				if (r.Gang == "army")
				{
					if (rng.Next(100) >= 12)
					{
						return Backstory.Northern[rng.Next(Backstory.Northern.Length)];
					}
					return Backstory.IrishCounties[rng.Next(Backstory.IrishCounties.Length)] + ", Irlanda";
				}
				else
				{
					string text = Models.Name(r.ModelHash);
					if (text.Contains("rhd") || text.Contains("sd") || text.Contains("bayou") || text.Contains("lem"))
					{
						return Backstory.Southern[rng.Next(Backstory.Southern.Length)];
					}
					return Backstory.Frontier[rng.Next(Backstory.Frontier.Length)];
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003968 File Offset: 0x00001B68
		private static void War(NemesisRecord r, BackstoryResult res, Backstory.Ctx c, Random rng)
		{
			if (r.Gang == "wapiti")
			{
				if (r.BornYear <= 1862 && rng.Next(100) < 45)
				{
					Backstory.Add(r, res, null, Math.Min(c.Year, r.BornYear + 17 + rng.Next(6)), 1 + rng.Next(12), "Luchó contra los soldados en las llanuras de joven", "war", "cold:2 proud:2", "aggr:+5", true, true);
				}
				if (r.BornYear < 1891)
				{
					Backstory.Add(r, res, null, Math.Min(c.Year, 1891 + rng.Next(4)), 1 + rng.Next(12), "Llegó a la reserva de Ambarino junto con su pueblo", "loss", "cold:2 hothead:1 loyal:1", "hosco", true, true);
				}
				return;
			}
			if (r.BornYear > 1848)
			{
				return;
			}
			bool south = c.South;
			if (Backstory.Woman(r))
			{
				if (rng.Next(100) >= 55)
				{
					return;
				}
				string[] array2;
				if (!south)
				{
					string[] array = new string[4];
					array[0] = "Sacó adelante la granja mientras los hombres estaban en la guerra";
					array[1] = "Cuidó a heridos de la Unión en un hospital de campaña";
					array[2] = "Cosió uniformes para el regimiento en el 62";
					array2 = array;
					array[3] = "Perdió a dos hermanos en la guerra";
				}
				else
				{
					string[] array3 = new string[4];
					array3[0] = "Sacó adelante la granja mientras los hombres estaban en la guerra";
					array3[1] = "Cuidó a heridos confederados en una iglesia convertida en hospital";
					array3[2] = "Vio a los soldados de la Unión quemar el granero en el 64";
					array2 = array3;
					array3[3] = "Perdió a dos hermanos en la guerra";
				}
				string[] array4 = array2;
				Backstory.Add(r, res, null, 1862 + rng.Next(3), 1 + rng.Next(12), array4[rng.Next(array4.Length)], "war", "cold:1 loyal:1", null, false, true);
				if (south && rng.Next(100) < 30)
				{
					Backstory.Add(r, res, null, 1865, 4, "Vio arder la granja y a su gente dispersarse", "loss", "hothead:1 cold:2", "hosco", false, true);
				}
				return;
			}
			else
			{
				int num = (r.Gang == "lemoyneraiders") ? 90 : ((r.Gang == "klan") ? 70 : ((r.Gang == "army") ? 75 : 40));
				if (rng.Next(100) >= num)
				{
					return;
				}
				ValueTuple<string, int>[] array5 = (from b in south ? Backstory.GreyBattles : Backstory.BlueBattles
				where b.Item2 - r.BornYear >= 15
				select b).ToArray<ValueTuple<string, int>>();
				if (array5.Length == 0)
				{
					return;
				}
				ValueTuple<string, int> valueTuple = array5[rng.Next(array5.Length)];
				Backstory.Add(r, res, null, valueTuple.Item2, 3 + rng.Next(8), "Luchó por la " + (south ? "Confederación" : "Unión") + " at " + valueTuple.Item1, "war", "cold:2 proud:2", "aggr:+5", true, true);
				int num2 = rng.Next(100);
				if (num2 < 35)
				{
					ValueTuple<string, int> valueTuple2 = array5[rng.Next(array5.Length)];
					Backstory.Add(r, res, null, Math.Max(valueTuple.Item2, valueTuple2.Item2), 2 + rng.Next(9), "Herido en " + valueTuple2.Item1, "war", "coward:1 hothead:1", "fear:+3", false, true);
				}
				else if (num2 < 50 && south)
				{
					Backstory.Add(r, res, null, 1864, 1 + rng.Next(12), "Capturado y retenido en un campo de prisioneros de la Unión", "war", "hothead:2 cold:1", "hosco", false, true);
				}
				else if (num2 < 62)
				{
					Backstory.Add(r, res, null, 1864 + rng.Next(2), 1 + rng.Next(4), "Desertó antes del final y jamás se lo contó a nadie", "war", "coward:3 drifter:2", null, false, true);
				}
				else if (num2 < 75 && !south && valueTuple.Item2 <= 1864)
				{
					Backstory.Add(r, res, null, 1864, 11, "Marchó con Sherman hacia el mar", "war", "cold:2", null, false, true);
				}
				if (south && rng.Next(100) < 40)
				{
					Backstory.Add(r, res, null, 1865, 4, "Volvió a casa y encontró la granja quemada y a los suyos dispersados", "loss", "hothead:2 cold:2", "hosco", false, true);
				}
				return;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00003DBC File Offset: 0x00001FBC
		private static int JoinUp(NemesisRecord r, BackstoryResult res, Backstory.Ctx c, Random rng)
		{
			Gang gang = Gangs.ById(r.Gang);
			if (gang == null)
			{
				return 0;
			}
			int num = (r.Rank >= 4) ? (5 + rng.Next(8)) : ((r.Rank >= 2) ? (2 + rng.Next(7)) : rng.Next(5));
			int num2 = Math.Max(r.BornYear + 16, c.Year - num);
			if (r.Gang == "wapiti")
			{
				num2 = r.BornYear + 16 + rng.Next(4);
			}
			if (num2 > c.Year)
			{
				num2 = c.Year;
			}
			int num3 = (num2 == c.Year) ? (1 + rng.Next(Math.Max(1, c.Month))) : (1 + rng.Next(12));
			c.JoinMonth = num3;
			string text = (r.Gang == "odriscoll") ? ("Se juntó con los chicos de " + gang.LeaderName + " y sus muchachos") : ((r.Gang == "lemoyneraiders") ? ("Se unió a los Lemoyne Raiders bajo el mando de " + gang.LeaderName) : ((r.Gang == "army") ? ("Se alistó en el Ejército de EE. UU. en " + (new string[]
			{
				"Blackwater",
				"Saint Denis",
				"Fort Wallace",
				"una oficina de reclutamiento en el Este"
			})[rng.Next(4)]) : ((r.Gang == "klan") ? "Se puso la capucha por primera vez" : ((r.Gang == "wapiti") ? "Salió a cabalgar con los guerreros por primera vez" : ("Se unió a " + gang.Name)))));
			Backstory.Add(r, res, null, num2, num3, text, "gang", (r.Gang == "odriscoll") ? "loyal:1" : "", null, r.Gang != "klan", true);
			ValueTuple<string, int, int>[] tiers = gang.Tiers;
			List<string> list = new List<string>();
			for (int i = tiers.Length - 2; i >= 0; i--)
			{
				if (r.Rank >= tiers[i].Item2)
				{
					list.Add(Backstory.Singular(tiers[i].Item1));
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				int num4 = Math.Min(c.Year, num2 + (int)Math.Round((double)((c.Year - num2) * (j + 1)) / (double)(list.Count + 1)));
				string text2 = (r.Gang == "army") ? ((list[j] == "Officer") ? "Nombrado oficial" : ("Made " + list[j].ToLowerInvariant())) : ((r.Gang == "wapiti") ? ("Hombre de confianza como " + list[j].ToLowerInvariant()) : ("Made " + list[j]));
				Backstory.Add(r, res, null, Math.Max(num2, num4), (num4 == c.Year) ? (1 + rng.Next(Math.Max(1, c.Month))) : (1 + rng.Next(12)), text2, "rank", null, null, false, true);
			}
			return num2;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000411C File Offset: 0x0000231C
		private static void Family(NemesisRecord r, BackstoryResult res, Backstory.Ctx c, Random rng, bool derive)
		{
			string text;
			int num2;
			if (derive)
			{
				int num = (c.Age < 20) ? 5 : ((c.Age < 26) ? 30 : ((c.Age < 36) ? 55 : 68));
				if (r.Gang == "odriscoll" && r.Rank == 1)
				{
					num -= 20;
				}
				if (r.Gang == "army" && r.Rank == 1)
				{
					num -= 15;
				}
				if (r.Trade == "Errante" || r.Trade == "Vagabundo")
				{
					num -= 20;
				}
				if (rng.Next(100) >= num)
				{
					text = "single";
				}
				else
				{
					text = ((rng.Next(100) < ((c.Age >= 40) ? 25 : 12)) ? "widower" : "married");
				}
				num2 = ((text == "single") ? 0 : ((rng.Next(100) < 25) ? 0 : (1 + rng.Next(Math.Min(5, Math.Max(1, (c.Age - 18) / 5))))));
				res.Family = text;
				res.Children = num2;
			}
			else
			{
				text = (r.Family ?? "single");
				num2 = r.Children;
			}
			if (text == "single")
			{
				return;
			}
			int num3 = 19 + rng.Next(Math.Max(1, Math.Min(12, c.Age - 19)));
			int num4 = Math.Min(c.Year, r.BornYear + num3);
			bool flag = Backstory.Woman(r);
			string text2 = flag ? ((r.Gang == "wapiti") ? "un hombre de los Wapiti" : (c.Irish ? Backstory.IrishHusbands : Backstory.Husbands)[rng.Next((c.Irish ? Backstory.IrishHusbands : Backstory.Husbands).Length)]) : ((r.Gang == "wapiti") ? "una mujer de los Wapiti" : (c.Irish ? Backstory.IrishWives : Backstory.Wives)[rng.Next((c.Irish ? Backstory.IrishWives : Backstory.Wives).Length)]);
			Backstory.Add(r, res, null, num4, 1 + rng.Next(12), "Se casó con " + text2, "family", "loyal:1", null, false, derive);
			int num5 = int.MaxValue;
			string text3 = null;
			if (text == "widower")
			{
				int num6 = Math.Min(c.Year, Math.Max(num4 + 1, num4 + rng.Next(Math.Max(2, c.Year - num4 + 1))));
				text3 = (new string[]
				{
					"la fiebre",
					"el parto",
					"una tos de invierno",
					"el cólera",
					"una caída desde un carro"
				})[rng.Next(5)];
				num5 = num6 * 12 + rng.Next(12);
			}
			int num7 = num4 * 12 + 9 + rng.Next(12);
			int i = 0;
			while (i < num2)
			{
				if (num7 > num5)
				{
					if (derive)
					{
						res.Children = i;
						break;
					}
					break;
				}
				else
				{
					int year = Math.Min(c.Year, num7 / 12);
					int month = num7 % 12 + 1;
					Backstory.Add(r, res, null, year, month, (rng.Next(2) == 0) ? "Nació un hijo" : "Nació una hija", "family", null, null, false, derive);
					if (text3 == "el parto" && i == num2 - 1)
					{
						num5 = Math.Max(num5, num7);
					}
					num7 += 12 + rng.Next(30);
					i++;
				}
			}
			if (text == "widower")
			{
				if (text3 == "el parto" && num2 == 0)
				{
					text3 = "la fiebre";
				}
				int year2 = Math.Min(c.Year, num5 / 12);
				int month2 = num5 % 12 + 1;
				Backstory.Add(r, res, null, year2, month2, "Lost " + ((r.Gang == "wapiti") ? (flag ? "a su marido" : "a su esposa") : text2) + " to " + ((flag && text3 == "el parto") ? "la fiebre" : text3), "loss", "cold:3 hothead:1", (rng.Next(100) < 35) ? "godless" : "", false, derive);
			}
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000458C File Offset: 0x0000278C
		private static void Pick(NemesisRecord r, BackstoryResult res, Backstory.Ctx c, LifeT[] pool, bool derive, int minYear = 0)
		{
			Personality? personality = r.Personality;
			List<ValueTuple<LifeT, double>> list = new List<ValueTuple<LifeT, double>>();
			for (int i = 0; i < pool.Length; i++)
			{
				LifeT t = pool[i];
				if (Backstory.WhoOk(t.Who, r, c) && (!Backstory.Woman(r) || t.Text == null || (!t.Text.Contains("wife") && !t.Text.Contains("widow") && !t.Text.Contains("enlisted") && !t.Text.Contains("Enlisted"))) && t.MinAge <= c.Age && t.Allows(r, c) && !r.Past.Any((PastEntry e) => e.Text != null && e.Text.StartsWith(t.Text.Split(new char[]
				{
					'{'
				})[0])))
				{
					double num = (double)t.W;
					if (!derive)
					{
						if (Backstory.Contradicts(t.Fx, r))
						{
							goto IL_1A7;
						}
						if (personality != null)
						{
							int num2;
							num *= Math.Max(0.15, Math.Min(3.0, 1.0 + 0.35 * (double)(t.Tilt.TryGetValue(personality.Value, out num2) ? num2 : 0)));
						}
					}
					list.Add(new ValueTuple<LifeT, double>(t, num));
				}
				IL_1A7:;
			}
			if (list.Count == 0)
			{
				return;
			}
			double num3 = c.Rng.NextDouble() * list.Sum(([TupleElementNames(new string[]
			{
				"t",
				"w"
			})] ValueTuple<LifeT, double> x) => x.Item2);
			LifeT item = list[list.Count - 1].Item1;
			foreach (ValueTuple<LifeT, double> valueTuple in list)
			{
				num3 -= valueTuple.Item2;
				if (num3 <= 0.0)
				{
					item = valueTuple.Item1;
					break;
				}
			}
			int num4 = item.MinAge + c.Rng.Next(Math.Max(1, Math.Min(item.MaxAge, c.Age) - item.MinAge + 1));
			int num5 = Math.Min(c.Year, Math.Max(minYear, r.BornYear + num4));
			int num6 = (num5 == c.Year) ? (1 + c.Rng.Next(Math.Max(1, c.Month))) : (1 + c.Rng.Next(12));
			if (minYear > 0 && num5 == minYear && num6 < c.JoinMonth)
			{
				num6 = Math.Min((num5 == c.Year) ? Math.Max(1, c.Month) : 12, c.JoinMonth + c.Rng.Next(3));
			}
			string text = Backstory.Towns[c.Rng.Next(Backstory.Towns.Length)];
			string text2 = Backstory.Fill(item.Text, c, text);
			PastEntry pastEntry = Backstory.Add(r, res, null, num5, num6, text2, item.Kind, null, derive ? item.Fx : null, item.Public, derive);
			foreach (KeyValuePair<Personality, int> keyValuePair in item.Tilt)
			{
				int num7;
				res.Tilt[keyValuePair.Key] = (res.Tilt.TryGetValue(keyValuePair.Key, out num7) ? num7 : 0) + keyValuePair.Value;
			}
			if (item.MirrorKind != null)
			{
				pastEntry.MirrorKind = item.MirrorKind;
				pastEntry.MirrorTitle = Backstory.Fill(item.MirrorTitle, c, text);
				pastEntry.MirrorPlace = ((item.MirrorTitle.Contains("{town}") || item.Text.Contains("{town}")) ? text : (item.Text.Contains("Van Horn") ? "Van Horn" : (item.Text.Contains("Strawberry") ? "Strawberry" : (item.Text.Contains("Sisika") ? "Sisika Penitentiary" : ""))));
				pastEntry.MirrorHeard = item.MirrorHeard;
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004A50 File Offset: 0x00002C50
		private static string Fill(string s, Backstory.Ctx c, string town)
		{
			return (s ?? "").Replace("{home}", c.Home).Replace("{town}", town).Replace("{n}", (2 + c.Rng.Next(5)).ToString());
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00004AA4 File Offset: 0x00002CA4
		private static PastEntry Add(NemesisRecord r, BackstoryResult res, string worldId, int year, int month, string text, string kind, string tilt = null, string fx = null, bool pub = false, bool derive = true)
		{
			if (Backstory.Woman(r))
			{
				text = Pron.Her(text, true);
			}
			PastEntry pastEntry = new PastEntry
			{
				Year = year,
				Month = Math.Max(0, Math.Min(12, month)),
				Text = text,
				Kind = kind,
				WorldId = worldId,
				Public = pub
			};
			r.Past.Add(pastEntry);
			if (tilt != null)
			{
				string[] array = tilt.Split(new char[]
				{
					' '
				}, StringSplitOptions.RemoveEmptyEntries);
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(new char[]
					{
						':'
					});
					Personality key;
					int num;
					if (array2.Length == 2 && Enum.TryParse<Personality>(array2[0], true, out key) && int.TryParse(array2[1], out num))
					{
						int num2;
						res.Tilt[key] = (res.Tilt.TryGetValue(key, out num2) ? num2 : 0) + num;
					}
				}
			}
			if (derive && !string.IsNullOrEmpty(fx))
			{
				res.Fx.AddRange(fx.Split(new char[]
				{
					' '
				}, StringSplitOptions.RemoveEmptyEntries));
			}
			return pastEntry;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00004BBB File Offset: 0x00002DBB
		internal static bool Woman(NemesisRecord r)
		{
			return r != null && Models.Female(r.ModelHash);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004BD0 File Offset: 0x00002DD0
		private static bool WhoOk(string who, NemesisRecord r, Backstory.Ctx c)
		{
			Gang gang = Gangs.ById(r.Gang);
			if (gang != null && gang.OwnLives)
			{
				return who.Split(new char[]
				{
					'|'
				}).Contains(r.Gang);
			}
			string[] array = who.Split(new char[]
			{
				'|'
			});
			if (array.Contains("hombre") && Backstory.Woman(r))
			{
				return false;
			}
			if (array.Contains("mujer") && !Backstory.Woman(r))
			{
				return false;
			}
			if (array.All((string x) => x == "hombre" || x == "mujer"))
			{
				return true;
			}
			foreach (string text in array)
			{
				if (text == "any")
				{
					return true;
				}
				if (text == "gang" && r.Gang != null)
				{
					return true;
				}
				if (text == "loner" && r.Gang == null)
				{
					return true;
				}
				if (text == r.Gang)
				{
					return true;
				}
				if (text == "irish" && c.Irish)
				{
					return true;
				}
				if (text == "irishborn" && c.IrishBorn)
				{
					return true;
				}
				if (text == "south" && c.South)
				{
					return true;
				}
				if (text == "north" && c.North)
				{
					return true;
				}
				if (text == "rich" && r.Wealth == Wealth.Rich)
				{
					return true;
				}
				if (text == "poor" && (r.Wealth == Wealth.Poor || r.Wealth == Wealth.Working))
				{
					return true;
				}
				if (text.StartsWith("trade:") && r.Trade == text.Substring(6))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00004D9C File Offset: 0x00002F9C
		private static bool Contradicts(string fx, NemesisRecord r)
		{
			if (string.IsNullOrEmpty(fx) || !r.TraitsRolled)
			{
				return false;
			}
			foreach (string text in fx.Split(new char[]
			{
				' '
			}))
			{
				if (text == "devout" && (r.Devotion == Devotion.Godless || r.Devotion == Devotion.Lapsed))
				{
					return true;
				}
				if (text == "godless" && (r.Devotion == Devotion.Devout || r.Devotion == Devotion.Churchgoer))
				{
					return true;
				}
				if (text == "amistoso" && r.Disposition == Disposition.Surly)
				{
					return true;
				}
				if (text == "hosco" && r.Disposition == Disposition.Friendly)
				{
					return true;
				}
				if (text == "astuto" && r.Smarts == Smarts.Dim)
				{
					return true;
				}
				if (text == "corto" && r.Smarts == Smarts.Sharp)
				{
					return true;
				}
				if (text.StartsWith("aggr:+") && r.Aggression < 30)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00004EA0 File Offset: 0x000030A0
		public static Personality PickTemper(BackstoryResult res, bool gang, Random rng)
		{
			return Personalities.PickWeighted(rng, gang, delegate(Personality p)
			{
				int num;
				return Math.Max(0.15, 1.0 + 0.15 * (double)(res.Tilt.TryGetValue(p, out num) ? num : 0));
			});
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004ED0 File Offset: 0x000030D0
		public static void Apply(NemesisRecord r, BackstoryResult res)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			foreach (string text in res.Fx)
			{
				int num6;
				int num7;
				if (text.StartsWith("aggr:") && int.TryParse(text.Substring(5), out num6))
				{
					num += num6;
				}
				else if (text.StartsWith("fear:") && int.TryParse(text.Substring(5), out num7))
				{
					num5 += num7;
				}
				else if (text == "amistoso")
				{
					num2++;
				}
				else if (text == "hosco")
				{
					num2--;
				}
				else if (text == "devout")
				{
					num3++;
				}
				else if (text == "godless")
				{
					num3--;
				}
				else if (text == "astuto")
				{
					num4++;
				}
				else if (text == "corto")
				{
					num4--;
				}
			}
			r.Aggression = Math.Max(0, Math.Min(100, r.Aggression + Math.Max(-20, Math.Min(20, num))));
			if (num2 > 0)
			{
				r.Disposition = ((r.Disposition == Disposition.Surly) ? Disposition.Neutral : Disposition.Friendly);
			}
			if (num2 < 0)
			{
				r.Disposition = ((r.Disposition == Disposition.Friendly) ? Disposition.Neutral : Disposition.Surly);
			}
			if (num3 == 0)
			{
				if (res.Fx.Any((string f) => f == "devout" || f == "godless"))
				{
					r.Devotion = Devotion.Lapsed;
				}
			}
			if (num3 > 0)
			{
				r.Devotion = Devotion.Devout;
				if (r.Faith == "none")
				{
					r.Faith = ((r.Gang == "odriscoll") ? "Católico" : "Metodista");
				}
			}
			if (num3 < 0)
			{
				r.Devotion = Devotion.Godless;
				r.Faith = "none";
			}
			if (num4 > 0)
			{
				r.Smarts = Smarts.Sharp;
			}
			if (num4 < 0)
			{
				r.Smarts = Smarts.Dim;
			}
			if (num5 > 0)
			{
				r.Fear = Math.Min(30, r.Fear + num5);
			}
			if (res.Family != null)
			{
				r.Family = res.Family;
				r.Children = res.Children;
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000512C File Offset: 0x0000332C
		internal static string Singular(string label)
		{
			string text = (label.Length > 1 && label.EndsWith("S")) ? label.Substring(0, label.Length - 1) : label;
			if (text.Length != 0)
			{
				return text[0].ToString() + text.Substring(1).ToLowerInvariant();
			}
			return text;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000518C File Offset: 0x0000338C
		[return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static ValueTuple<int, int, int> KindColor(string kind)
		{
			if (kind != null)
			{
				switch (kind.Length)
				{
				case 3:
				{
					char c = kind[0];
					if (c != 'k')
					{
						if (c != 'w')
						{
							goto IL_1D2;
						}
						if (!(kind == "war"))
						{
							goto IL_1D2;
						}
						return new ValueTuple<int, int, int>(176, 150, 116);
					}
					else if (!(kind == "kin"))
					{
						goto IL_1D2;
					}
					break;
				}
				case 4:
				{
					char c = kind[0];
					if (c != 'g')
					{
						if (c != 'l')
						{
							if (c != 'r')
							{
								goto IL_1D2;
							}
							if (!(kind == "rank"))
							{
								goto IL_1D2;
							}
						}
						else
						{
							if (!(kind == "loss"))
							{
								goto IL_1D2;
							}
							return new ValueTuple<int, int, int>(160, 126, 160);
						}
					}
					else if (!(kind == "gang"))
					{
						goto IL_1D2;
					}
					return new ValueTuple<int, int, int>(150, 176, 140);
				}
				case 5:
				{
					char c = kind[0];
					if (c != 'c')
					{
						if (c != 'w')
						{
							goto IL_1D2;
						}
						if (!(kind == "world"))
						{
							goto IL_1D2;
						}
						return new ValueTuple<int, int, int>(196, 170, 120);
					}
					else
					{
						if (!(kind == "crime"))
						{
							goto IL_1D2;
						}
						return new ValueTuple<int, int, int>(214, 150, 80);
					}
					break;
				}
				case 6:
				{
					char c = kind[1];
					if (c != 'a')
					{
						if (c != 'r')
						{
							goto IL_1D2;
						}
						if (!(kind == "friend"))
						{
							goto IL_1D2;
						}
					}
					else
					{
						if (!(kind == "family"))
						{
							goto IL_1D2;
						}
						return new ValueTuple<int, int, int>(150, 172, 196);
					}
					break;
				}
				default:
					goto IL_1D2;
				}
				return new ValueTuple<int, int, int>(140, 180, 196);
			}
			IL_1D2:
			return new ValueTuple<int, int, int>(176, 164, 138);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00008048 File Offset: 0x00006248
		[CompilerGenerated]
		internal static int <FitKinAge>g__Born|27_0(string rel, ref Backstory.<>c__DisplayClass27_0 A_1)
		{
			if (rel == "hijo")
			{
				return A_1.kb + 18 + A_1.rng.Next(20);
			}
			if (rel == "padre")
			{
				return A_1.kb - (20 + A_1.rng.Next(18));
			}
			if (rel == "sobrino")
			{
				return A_1.kb + 14 + A_1.rng.Next(16);
			}
			if (rel == "tío")
			{
				return A_1.kb - (14 + A_1.rng.Next(16));
			}
			if (!(rel == "hermano"))
			{
				return A_1.kb + A_1.rng.Next(-14, 15);
			}
			return A_1.kb + A_1.rng.Next(-10, 11);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00008128 File Offset: 0x00006328
		[CompilerGenerated]
		internal static bool <FitKinAge>g__Fits|27_1(int b, ref Backstory.<>c__DisplayClass27_0 A_1)
		{
			int num = A_1.year - b;
			return num >= 16 && num <= 74;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000814D File Offset: 0x0000634D
		[CompilerGenerated]
		internal static int <AgeFor>g__Between|29_0(int a, int b, ref Backstory.<>c__DisplayClass29_0 A_2)
		{
			return a + A_2.rng.Next(b - a + 1);
		}

		// Token: 0x04000039 RID: 57
		internal static string[] IrishCounties = new string[]
		{
			"County Cork",
			"County Kerry",
			"County Galway",
			"County Mayo",
			"County Clare",
			"County Donegal",
			"County Limerick",
			"County Sligo",
			"County Wexford",
			"County Tipperary"
		};

		// Token: 0x0400003A RID: 58
		internal static string[] IrishAmerican = new string[]
		{
			"Five Points, Nueva York, de padres irlandeses",
			"Boston, de padres irlandeses",
			"Saint Denis, de estibadores irlandeses"
		};

		// Token: 0x0400003B RID: 59
		internal static string[] Southern = new string[]
		{
			"Scarlett Meadows, Lemoyne",
			"Bluewater Marsh, Lemoyne",
			"Bayou Nwa, Lemoyne",
			"Rhodes, Lemoyne",
			"Saint Denis, Lemoyne",
			"Georgia",
			"Alabama",
			"Mississippi",
			"las Carolinas",
			"Tennessee"
		};

		// Token: 0x0400003C RID: 60
		internal static string[] Northern = new string[]
		{
			"Ohio",
			"Pennsylvania",
			"New York",
			"Massachusetts",
			"Indiana",
			"Illinois",
			"Michigan",
			"Kentucky",
			"Blackwater, West Elizabeth",
			"Valentine, New Hanover",
			"Baviera; llegó siendo un niño"
		};

		// Token: 0x0400003D RID: 61
		internal static string[] Frontier = new string[]
		{
			"Valentine, New Hanover",
			"Annesburg, New Hanover",
			"Emerald Ranch, New Hanover",
			"Van Horn, New Hanover",
			"Blackwater, West Elizabeth",
			"Strawberry, West Elizabeth",
			"Big Valley, West Elizabeth",
			"Rhodes, Lemoyne",
			"Saint Denis, Lemoyne",
			"Armadillo, New Austin",
			"Tumbleweed, New Austin",
			"las tierras altas de Ambarino",
			"New York",
			"Boston",
			"Philadelphia",
			"Missouri",
			"Kansas",
			"Ohio"
		};

		// Token: 0x0400003E RID: 62
		internal static string[] Towns = new string[]
		{
			"Valentine",
			"Rhodes",
			"Saint Denis",
			"Strawberry",
			"Blackwater",
			"Van Horn",
			"Annesburg",
			"Armadillo"
		};

		// Token: 0x0400003F RID: 63
		internal static string[] IrishWives = new string[]
		{
			"Mary",
			"Bridget",
			"Kathleen",
			"Nora",
			"Margaret",
			"Eileen",
			"Siobhan",
			"Honora"
		};

		// Token: 0x04000040 RID: 64
		internal static string[] IrishHusbands = new string[]
		{
			"Patrick",
			"Michael",
			"Sean",
			"Thomas",
			"Daniel",
			"Cornelius",
			"Dermot",
			"Owen"
		};

		// Token: 0x04000041 RID: 65
		internal static string[] Husbands = new string[]
		{
			"John",
			"William",
			"Samuel",
			"Henry",
			"Thomas",
			"Charles",
			"Jacob",
			"Walter",
			"Amos",
			"Eli"
		};

		// Token: 0x04000042 RID: 66
		internal static string[] Wives = new string[]
		{
			"Sarah",
			"Martha",
			"Elizabeth",
			"Anna",
			"Clara",
			"Emma",
			"Lucy",
			"Ruth",
			"Hattie",
			"Ida",
			"Mae",
			"Josephine"
		};

		// Token: 0x04000043 RID: 67
		[TupleElementNames(new string[]
		{
			"battle",
			"year"
		})]
		internal static ValueTuple<string, int>[] GreyBattles = new ValueTuple<string, int>[]
		{
			new ValueTuple<string, int>("Shiloh", 1862),
			new ValueTuple<string, int>("Antietam", 1862),
			new ValueTuple<string, int>("Chickamauga", 1863),
			new ValueTuple<string, int>("Gettysburg", 1863),
			new ValueTuple<string, int>("Wilderness", 1864),
			new ValueTuple<string, int>("Franklin", 1864),
			new ValueTuple<string, int>("Atlanta", 1864),
			new ValueTuple<string, int>("Petersburg", 1864)
		};

		// Token: 0x04000044 RID: 68
		[TupleElementNames(new string[]
		{
			"battle",
			"year"
		})]
		internal static ValueTuple<string, int>[] BlueBattles = new ValueTuple<string, int>[]
		{
			new ValueTuple<string, int>("Shiloh", 1862),
			new ValueTuple<string, int>("Antietam", 1862),
			new ValueTuple<string, int>("Vicksburg", 1863),
			new ValueTuple<string, int>("Gettysburg", 1863),
			new ValueTuple<string, int>("Wilderness", 1864),
			new ValueTuple<string, int>("Atlanta", 1864),
			new ValueTuple<string, int>("Petersburg", 1865)
		};

		// Token: 0x04000045 RID: 69
		internal static LifeT[] Boyhood = new LifeT[]
		{
			Backstory.L("youth", "any", 6, 14, 10, "Raised on a hardscrabble farm outside {home}", "greedy:1 cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "hombre", 6, 14, 8, "Su padre bebía y usaba los puños", "hothead:3 coward:2", "surly aggr:+4", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "mujer", 6, 14, 8, "Su padre bebía y golpeaba a quien tuviera más cerca", "hothead:3 coward:2", "surly aggr:+4", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "any", 4, 12, 6, "Quedó huérfano de niño; un tío lo hacía trabajar como una mula", "cold:3 drifter:2 loyal:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "any", 6, 14, 8, "Criado en la iglesia por una madre temerosa de Dios", "coward:1", "devout friendly", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "any", 8, 14, 8, "Se crió asilvestrado con los chicos mayores en {home}", "hothead:2 proud:1", "aggr:+3", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "poor", 6, 14, 7, "Pasó hambre la mayoría de los inviernos", "greedy:3 cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "hombre", 6, 14, 6, "Un niño enfermizo del que abusaban sus hermanos", "coward:3 proud:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "mujer", 6, 14, 6, "Una niña enfermiza de la que se burlaban los demás niños", "coward:3 proud:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "any", 8, 14, 6, "Acertaba a un conejo a cincuenta pasos antes de saber leer", "proud:1", "aggr:+2", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "any", 7, 14, 5, "Una maestra le metió las letras a golpes; lee mejor que la mayoría", "cold:1", "astuto", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "irish", 6, 14, 8, "Creció escuchando historias sobre la Gran Hambruna y los terratenientes", "proud:2 loyal:2", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "south", 5, 14, 9, "Vio a las tropas de la Unión quemar el granero de su familia", "hothead:3 cold:2", "surly aggr:+5", false, 1849, 1859, null, null, null, false),
			Backstory.L("youth", "north|man", 6, 14, 6, "Hijo de un veterano de la Unión que nunca hablaba de la guerra", "cold:2 loyal:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "north|woman", 6, 14, 6, "Hija de un veterano de la Unión que nunca hablaba de la guerra", "cold:2 loyal:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "any", 3, 10, 6, "Perdió a su madre cuando era pequeño", "cold:2 drifter:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "rich", 6, 14, 10, "Nació en cuna rica y nunca dejó que nadie lo olvidara", "proud:4 greedy:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "any", 6, 14, 5, "Su familia lo perdió todo en un incendio", "greedy:2 coward:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "wapiti", 5, 13, 10, "Siguió a las manadas con su familia a través de las llanuras del norte", "drifter:2 proud:1", "", false, 0, 1882, null, null, null, false),
			Backstory.L("youth", "wapiti", 4, 12, 9, "Sabía montar antes de poder caminar lejos", "proud:2", "aggr:+2", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "wapiti", 8, 14, 9, "Aprendió de su tío a cazar uapitíes en los Grizzlies", "cold:1 loyal:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "wapiti", 6, 14, 9, "Creció pasando hambre mientras las manadas menguaban cada año", "cold:2 greedy:1", "hosco", false, 1872, 0, null, null, null, false),
			Backstory.L("youth", "wapiti", 4, 13, 7, "Perdió a su padre a manos de los soldados cuando era niño", "hothead:3 cold:2", "surly aggr:+4", false, 0, 1886, null, null, null, false),
			Backstory.L("youth", "wapiti", 6, 14, 8, "Se sentaba cada noche junto al fuego con los ancianos a escuchar sus historias", "loyal:3", "amistoso", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "wapiti", 8, 14, 5, "Fue enviado al Este a una escuela del gobierno, y se escapó de vuelta a casa", "proud:3 hothead:1", "hosco", false, 1872, 0, null, null, null, false)
		};

		// Token: 0x04000046 RID: 70
		internal static LifeT[] Youth = new LifeT[]
		{
			Backstory.L("youth", "any", 14, 19, 8, "Se fue de casa joven y nunca escribió de vuelta", "drifter:3 cold:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "hombre", 16, 21, 6, "Tragó polvo en la cola de una conducción de ganado hacia los corrales de Valentine", "drifter:1", "aggr:+2", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "hombre", 15, 19, 6, "Ganó su primera pelea a cuchillo antes de cumplir los veinte", "hothead:2 proud:3", "aggr:+5", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "hombre", 15, 20, 5, "Huyó de su primera pelea real y jamás ha logrado olvidarlo", "coward:4 proud:2", "fear:+4", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "hombre", 14, 18, 5, "Aprendiz de herrero en {home}", "cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "hombre", 16, 21, 5, "Perdió a la chica que amaba frente a un hombre más rico", "proud:2 hothead:1", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "any", 13, 17, 5, "Lo pillaron robando gallinas; la paliza no le enseñó nada", "greedy:2 hothead:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "irish", 13, 19, 10, "Anduvo con una banda callejera en Five Points", "loyal:2 hothead:2", "aggr:+4", false, 0, 0, "New York", null, null, false),
			Backstory.L("youth", "army", 15, 18, 10, "Mintió sobre su edad para alistarse", "proud:2 loyal:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "south", 8, 16, 8, "Creció con las historias de su abuelo sobre la Causa Perdida", "proud:3", "hosco", false, 1858, 0, null, null, null, false),
			Backstory.L("youth", "trade:Gambler", 16, 21, 15, "Ganó su primer bote jugando al faro en un barco de vapor de Saint Denis", "greedy:3 proud:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "trade:Miner", 12, 15, 15, "Bajó a trabajar a la mina a los catorce años", "cold:2", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "any", 16, 21, 4, "Se juntó con un espectáculo ambulante de elixires", "drifter:2 greedy:1", "astuto", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "wapiti", 14, 18, 9, "Domó su primer caballo salvaje a los quince años", "proud:2", "aggr:+2", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "wapiti", 14, 20, 8, "Pasó hambre el invierno en que las raciones del gobierno nunca llegaron", "cold:2 hothead:1", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "wapiti", 15, 21, 7, "Comerció con pieles en un puesto del río Dakota", "greedy:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "wapiti", 15, 21, 7, "Vio cómo los soldados mataban a tiros a un amigo suyo", "hothead:3 cold:1", "aggr:+4", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "wapiti", 13, 18, 7, "Ayunó a solas en las montañas para encontrar su camino", "proud:1 cold:1", "devout", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "wapiti", 15, 20, 5, "Huyó de su primer combate contra los soldados y jamás se lo ha perdonado", "coward:3 proud:2", "fear:+4", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "mujer", 12, 18, 8, "Llevó la casa para su padre y hermanos tras morir su madre", "cold:2 loyal:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "mujer", 12, 17, 6, "Aprendió a disparar de su padre, mejor que cualquiera de sus hermanos", "proud:3", "aggr:+3", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "mujer", 15, 19, 5, "Se escapó con una compañía de teatro ambulante a los diecisiete años", "drifter:3 proud:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "mujer", 13, 17, 5, "Empezó a trabajar en una fábrica textil de Saint Denis a los trece años", "cold:2", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "mujer", 10, 16, 4, "Aprendió a leer por sí misma con una Biblia robada", "proud:2", "astuto", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "mujer", 16, 21, 5, "Rechazó la propuesta de un hombre rico; su familia nunca se lo perdonó", "proud:3 hothead:1", "", false, 0, 0, null, null, null, false)
		};

		// Token: 0x04000047 RID: 71
		internal static LifeT[] Work = new LifeT[]
		{
			Backstory.L("work", "trade:Miner", 18, 50, 12, "Trabajó en la mina de carbón de Annesburg año tras año", "cold:2", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Miner", 20, 50, 8, "Sobrevivió a un derrumbe que sepultó a seis hombres", "coward:2 cold:2", "fear:+5", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Farmer", 18, 50, 10, "Trabajó como aparcero unas tierras que jamás serían suyas", "greedy:2", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Farmer", 20, 55, 6, "Perdió toda una cosecha por la sequía", "greedy:1 cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Rancher", 22, 55, 10, "Levantó una pequeña propiedad a las afueras de {home}", "proud:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Rancher", 22, 55, 7, "Echó a tiros a un cuatrero de sus tierras", "hothead:2 proud:2", "aggr:+4", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Cowhand", 18, 45, 12, "Condujo ganado hasta Valentine para los ranchos de las Heartlands", "drifter:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Railroad man", 18, 50, 12, "Tendió vías para el ferrocarril de Cornwall por cuatro centavos", "greedy:2", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Railroad man", 20, 50, 6, "Perdió dos dedos enganchando vagones de tren", "coward:1 cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Dockworker", 18, 50, 12, "Cargó cajas en los muelles de Saint Denis bajo el mando de los hombres de Bronte", "cold:1 greedy:1 coward:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Fisherman", 18, 55, 12, "Se ganaba la vida pescando en el río Lannahechee", "drifter:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Lumberjack", 18, 50, 12, "Taló árboles en los campamentos madereros de Roanoke Ridge", "cold:1", "aggr:+2", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Hunter|trade:Trapper", 18, 55, 12, "Cazaba castores y zorros con trampas allá arriba en los Grizzlies", "cold:2 drifter:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Hunter|trade:Trapper", 20, 55, 6, "Fue destrozado por un oso; aún lleva las marcas", "proud:2", "fear:+4", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Gambler", 20, 55, 12, "Dirigía partidas de cartas en los barcos de vapor de Saint Denis", "greedy:3", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Gambler", 22, 55, 8, "Le debe dinero a la gente equivocada en Saint Denis", "greedy:2 coward:2", "fear:+6", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Gentleman", 22, 60, 12, "Hizo su fortuna con el algodón de Lemoyne", "proud:3 greedy:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Gentleman", 22, 60, 6, "Perdió una fortuna en las mesas de cartas y recuperó la mitad", "greedy:2 proud:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Preacher", 20, 60, 14, "Predicó el fuego del infierno en carpas religiosas por todos los territorios", "proud:2", "devout", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Doctor|man", 22, 60, 14, "Aprendió medicina serrando piernas en hospitales de campaña", "cold:2", "astuto", false, 0, 1845, null, null, null, false),
			Backstory.L("work", "trade:Doctor", 22, 60, 10, "Estudió medicina en el Este; la frontera no era como se la imaginaba", "cold:1 proud:1", "astuto", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Townsman", 20, 60, 10, "Regentó una tienda de ultramarinos en {home}", "greedy:2 coward:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Townsman", 20, 60, 7, "Trabajó de empleado en el banco de {town}", "cold:1", "astuto", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Vagrant", 20, 60, 12, "Perdió su oficio por culpa de la botella", "drifter:3 coward:1", "corto", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Vagrant", 18, 60, 10, "Viajó de polizón en vagones de carga de Blackwater a Annesburg y de vuelta", "drifter:3", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Labourer", 18, 55, 10, "Cavó zanjas y acarreó piedras allí donde pagaran", "drifter:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Outlaw|trade:Drifter", 18, 45, 8, "Cabalgó con una banda de salteadores de caminos allá abajo en New Austin", "hothead:1 cold:1", "aggr:+5", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Drifter", 18, 50, 8, "Vagó de pueblo en pueblo, sin quedarse nunca más de una temporada", "drifter:4", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Blacksmith", 18, 60, 12, "Llevaba la forja en {home} y herró a la mitad de los caballos del condado", "proud:1 cold:1", "aggr:+2", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Blacksmith", 22, 60, 8, "Se hizo cargo de la herrería cuando las manos del viejo herrero no dieron más de sí", "loyal:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Blacksmith", 20, 60, 6, "Arreglaba ruedas de carro y rejas de arado para cada granja de los alrededores de {home}", "cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("youth", "trade:Blacksmith|woman", 12, 17, 15, "Aprendió el oficio de la forja junto a su padre; sabía herrar un caballo antes de aprender a coser", "proud:2", "aggr:+2", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Blacksmith|woman", 20, 60, 8, "Manejó el martillo en {home} hasta que los hombres que se burlaban de ella terminaron llevándole sus caballos", "proud:3 hothead:1", "aggr:+2", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Tailor", 18, 60, 12, "Cortaba trajes para los caballeros de Saint Denis en una sastrería cerca de la avenida", "greedy:1 proud:1", "astuto", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Tailor", 16, 60, 8, "Arreglaba abrigos y pantalones en {home} para cualquiera que pudiera pagar", "cold:1 greedy:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Doctor|woman", 22, 60, 10, "Estudió en el Colegio Médico de Mujeres de Filadelfia y luego vino al oeste, donde un médico es un médico", "proud:2", "astuto", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Seamstress", 16, 60, 12, "Hacía encargos de costura en {home} para mantener un techo sobre su cabeza", "cold:1 greedy:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Seamstress", 18, 60, 8, "Cosía vestidos para las damas de la alta sociedad de Saint Denis", "greedy:1 proud:1", "astuto", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Laundress", 14, 60, 12, "Lavaba la ropa de los mineros en Annesburg por cinco centavos la colada", "cold:2", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Laundress", 16, 60, 8, "Fregaba sábanas en la lavandería de un hotel en {town} de sol a sol", "coward:1 cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Schoolteacher", 18, 50, 12, "Daba clase en la pequeña escuela de una sola aula a las afueras de {home}", "loyal:2", "sharp friendly", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Schoolteacher", 18, 45, 8, "Vino al oeste desde el Este para enseñar en la escuela, y se quedó", "proud:1 drifter:1", "astuto", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Shopkeeper|woman", 22, 65, 12, "Llevaba la tienda general de {town}, y llevaba las cuentas mejor que los hombres", "greedy:2 proud:1", "astuto", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Shopkeeper", 25, 65, 8, "Dirigió la tienda familiar en {home} cuando el corazón del viejo falló", "loyal:2 cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Cook", 16, 60, 12, "Cocinaba en una pensión de {town} para los trabajadores del ferrocarril", "cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Cook|trade:Camp cook", 18, 60, 10, "Cocinó para una cuadrilla ganadera en la ruta desde Texas", "drifter:2", "aggr:+2", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Camp cook", 18, 60, 10, "Alimentaba a los leñadores de Roanoke Ridge con una sola olla de hierro", "cold:1 proud:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Housemaid", 13, 50, 12, "Entró a servir en una gran mansión de Saint Denis a los trece años", "coward:1 loyal:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Housemaid", 16, 50, 7, "Fue despedida sin referencias por un jarrón roto que ella jamás tocó", "hothead:2 cold:1", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Boarding-house keeper", 25, 65, 12, "Regentaba una pensión en {town} para viajantes, errantes y ferroviarios", "greedy:2 cold:1", "astuto", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Milliner", 18, 60, 12, "Adornaba sombreros con plumas para las damas de {town}", "proud:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Telegraph operator", 17, 45, 12, "Maneja el telégrafo en la estación de {town}; se entera de todo el primero", "cold:1", "astuto", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Nurse", 18, 60, 12, "Cuidó a enfermos de fiebre en el hospital de Saint Denis", "loyal:2 cold:1", "amistoso", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Nurse", 20, 60, 8, "Trajo al mundo a la mitad de los bebés nacidos en {home}", "loyal:2", "amistoso", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Nun", 17, 70, 14, "Tomó los hábitos en el convento de Saint Denis", "loyal:2 coward:1", "devout", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Nun", 20, 70, 8, "Dirige el comedor de beneficencia para los pobres de Saint Denis", "loyal:3", "devout friendly", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Preacher's wife", 20, 65, 12, "Siguió el ministerio de su marido de una carpa de predicación a otra", "loyal:2", "devout", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Lady", 18, 65, 12, "Se casó con una familia adinerada del algodón de Lemoyne", "proud:3 greedy:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Lady", 20, 65, 8, "Recaudaba fondos para el orfanato de Saint Denis en sus bailes benéficos", "proud:2", "amistoso", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Lady|trade:Schoolteacher|trade:Seamstress|woman", 20, 65, 6, "Marchó por el voto femenino con las sufragistas en Saint Denis", "proud:3 hothead:1", "astuto", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Saloon girl", 16, 40, 12, "Bailaba a cambio de copas en una taberna de Valentine", "cold:2 greedy:1", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Saloon girl", 17, 40, 8, "Trabajaba en el piso de arriba de una taberna en {town} y ahorraba cada moneda", "cold:2 greedy:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Showgirl", 16, 45, 12, "Cantaba en el escenario de un teatro de Saint Denis", "proud:3", "amistoso", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Showgirl", 17, 45, 8, "Recorrió los pueblos ribereños con un espectáculo ambulante", "drifter:2 proud:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Homesteader", 20, 65, 12, "Sacó adelante una parcela cerca de {home} con sus propias manos", "proud:2 cold:1", "aggr:+2", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Homesteader|trade:Farm wife", 20, 65, 10, "Vendía huevos y mantequilla en {town} los días de mercado", "greedy:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Farm wife", 18, 65, 12, "Trabajaba las tierras de su marido desde el alba y luego cocinaba para los peones", "cold:2 loyal:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Farm wife|trade:Homesteader", 22, 65, 6, "Perdió toda una cosecha por la sequía y volvió a sembrar", "proud:1 cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Market seller", 16, 65, 12, "Vendía ostras y pralinés en el mercado de Saint Denis", "greedy:1", "amistoso", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Market seller", 18, 65, 8, "Vendía pescado en el muelle de Lagras antes del amanecer", "cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Traveller", 20, 65, 12, "Vino al oeste en tren para empezar de cero donde nadie conociera su nombre", "drifter:2 cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("work", "trade:Traveller", 20, 65, 8, "Viajó en tren desde Saint Denis hasta Blackwater para ver a su hermana", "loyal:2", "", false, 0, 0, null, null, null, false)
		};

		// Token: 0x04000048 RID: 72
		internal static LifeT[] Crime = new LifeT[]
		{
			Backstory.L("crime", "any", 18, 50, 10, "Cumplió {n} años en la penitenciaría de Sisika por robo", "hothead:2 cold:2", "hosco", true, 0, 0, null, "CRIME", "{name} enviado a la penitenciaría de Sisika", true),
			Backstory.L("crime", "any", 18, 50, 8, "Mató a tiros a un hombre por una partida de cartas en Van Horn", "hothead:3", "aggr:+6", false, 0, 0, null, "CRIME", "Una partida de cartas en Van Horn acaba en asesinato", false),
			Backstory.L("crime", "any", 18, 45, 6, "Se fugó del calabozo de Strawberry", "proud:2 hothead:1", "", true, 0, 0, null, "CRIME", "Fuga en Strawberry: {name} se escapa", true),
			Backstory.L("crime", "any", 18, 45, 6, "Su cara aparece en un viejo cartel de se busca en New Austin", "cold:1 proud:1", "", true, 0, 0, null, null, null, false),
			Backstory.L("crime", "south", 18, 50, 8, "Pasaba licor ilegal por el pantano para un contrabandista", "greedy:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("crime", "any", 18, 45, 7, "Robaba caballos a lo largo de las Heartlands", "greedy:2 drifter:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("crime", "any", 20, 50, 6, "Se escapó con un carro de nóminas y se lo bebió todo", "greedy:2 hothead:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("crime", "any", 18, 45, 7, "Asaltó una diligencia en el camino a las afueras de Strawberry", "greedy:2 cold:1", "", false, 0, 0, null, "CRIME", "Diligencia atracada a las afueras de Strawberry", true)
		};

		// Token: 0x04000049 RID: 73
		internal static LifeT[] GangLife = new LifeT[]
		{
			Backstory.L("gang", "odriscoll", 18, 50, 8, "Juró lealtad a Colm después de que la guerra contra Van der Linde le costara un amigo", "loyal:3 hothead:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "odriscoll", 18, 50, 7, "Le debe la vida a Colm O'Driscoll, y Colm nunca deja que lo olvide", "loyal:4", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "odriscoll", 18, 50, 6, "Hizo guardia durante todo un invierno arriba en Six Point Cabin", "cold:1 loyal:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "odriscoll", 18, 50, 6, "Vende caballos robados en Valentine para la banda", "greedy:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "lemoyneraiders", 18, 60, 8, "Quemó la tienda de un oportunista norteño en Rhodes", "hothead:2 proud:2", "aggr:+4", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "lemoyneraiders", 18, 60, 7, "Vigila alambiques de licor ilegal en Bluewater Marsh", "cold:1 greedy:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "lemoyneraiders", 18, 60, 7, "Jura que jamás saludará a la bandera de la Unión", "proud:3", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "army", 18, 55, 10, "Destinado en Fort Wallace bajo el mando del coronel Favours", "loyal:2 cold:1", "", true, 0, 0, null, null, null, false),
			Backstory.L("gang", "army", 20, 40, 7, "Cabalgó con la caballería en las campañas de las llanuras", "cold:2 loyal:2", "aggr:+4", false, 0, 1870, null, null, null, false),
			Backstory.L("gang", "army", 18, 50, 5, "Castigado por beber estando de guardia", "hothead:1 coward:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "army", 18, 50, 5, "Escoltó una nómina de Cornwall a través de Roanoke Ridge", "loyal:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "army", 18, 50, 6, "Vio cómo los Raiders abatían a su sargento cerca de Rhodes", "hothead:2 cold:2", "aggr:+4", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "klan", 25, 60, 8, "Perdió sus tierras a manos del banco y culpó a todo el mundo menos a sí mismo", "greedy:2 coward:2", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "klan", 20, 60, 8, "Lleva la capucha porque su padre también la llevaba", "coward:2 loyal:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "laramie", 17, 50, 9, "Fue vaquero en un rancho de Hennigan's Stead hasta que el dueño le estafó la paga de una temporada", "greedy:2 hothead:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "laramie", 18, 50, 8, "Sabe cambiar la marca de una res con el hierro candente más rápido que nadie en las llanuras", "proud:2 cold:1", "", true, 0, 0, null, null, null, false),
			Backstory.L("gang", "laramie", 18, 55, 7, "Subió ganado por la ruta de Texas y nunca regresó", "drifter:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "laramie", 18, 50, 7, "Cabalgó con los cuatreros que robaron la manada de los MacFarlane", "greedy:1 proud:1", "", true, 0, 0, null, null, null, false),
			Backstory.L("gang", "laramie", 20, 55, 6, "Perdió su propio rebaño en un duro invierno y empezó a robar el de otros", "cold:2 greedy:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "laramie", 17, 45, 6, "Siguió los pasos de su hermano mayor uniéndose a los chicos de Laramie", "loyal:3", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "laramie", 20, 55, 5, "Estuvo a punto de ser ahorcado por una partida de ganaderos en Great Plains", "coward:1 cold:1", "", true, 0, 0, null, null, null, false),
			Backstory.L("gang", "delobo", 17, 50, 9, "Cabalgó con bandidos a ambos lados del río desde los quince años", "hothead:1 proud:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "delobo", 18, 55, 8, "Salió de Nuevo Paraíso pisándole los talones los rurales", "coward:1 cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "delobo", 18, 50, 7, "Asaltó su primer tren cerca de Rio Bravo", "greedy:2", "", true, 0, 0, null, null, null, false),
			Backstory.L("gang", "delobo", 18, 55, 7, "Luchó en las revueltas al otro lado de la frontera antes de que lo acogieran los Del Lobo", "cold:2 loyal:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "delobo", 20, 55, 6, "Pasa armas de contrabando por el río para el jefe", "greedy:1 loyal:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "delobo", 17, 45, 6, "Juró lealtad al jefe después de que este le salvara la vida en Gaptooth Ridge", "loyal:3", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "delobo", 20, 55, 6, "Su cara está en los carteles de se busca tanto en Armadillo como en Chuparosa", "proud:2", "", true, 0, 0, null, null, null, false),
			Backstory.L("gang", "delobo", 25, 60, 5, "Perdió su granja al otro lado del río por culpa de los abogados de un rico", "hothead:2 greedy:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "nightfolk", 17, 60, 9, "Nació en una casa flotante en Bayou Nwa y jamás ha dormido en tierra firme", "cold:1 loyal:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "nightfolk", 17, 50, 8, "Fue acogido por la Gente de la Noche de niño, tras llevarse la fiebre a su familia", "loyal:3", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "nightfolk", 20, 60, 7, "Dejó de ir a la iglesia en Saint Denis el año en que aparecieron los primeros faroles", "cold:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "nightfolk", 18, 60, 7, "Prestó juramento al anciano en la oscuridad y jamás le ha visto la cara", "loyal:2 coward:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "nightfolk", 18, 50, 7, "Perdió a un hermano a manos de los Lemoyne Raiders en el camino del pantano", "hothead:2", "aggr:+4", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "nightfolk", 20, 60, 6, "Conoce cada canal desde Lagras hasta Shady Belle solo por el sonido del agua", "cold:1 proud:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "nightfolk", 20, 55, 6, "Dejó una advertencia en los cipreses de la que aún se habla en Saint Denis", "proud:2", "", true, 0, 0, null, null, null, false),
			Backstory.L("gang", "nightfolk", 25, 60, 5, "No ha pronunciado una frase entera desde la noche de la gran inundación", "cold:2", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "wapiti", 17, 45, 9, "Cabalga con los jóvenes que se han cansado de esperar las promesas del Ejército", "hothead:2 proud:2", "aggr:+4", true, 0, 0, null, null, null, false),
			Backstory.L("gang", "wapiti", 18, 55, 8, "Vigila Fort Wallace desde las crestas de las montañas", "cold:2 loyal:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "wapiti", 17, 45, 7, "Recuperó caballos de los corrales del Ejército", "proud:2 greedy:1", "", true, 0, 0, null, null, null, false),
			Backstory.L("gang", "wapiti", 18, 60, 8, "Escucha a Rains Fall, aunque su corazón le pide luchar", "loyal:2 coward:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "wapiti", 18, 60, 8, "Caza para las familias cuando escasean las raciones", "loyal:3", "amistoso", false, 0, 0, null, null, null, false),
			Backstory.L("gang", "wapiti", 18, 55, 6, "Siguió a los topógrafos de los petroleros a través de las tierras Wapiti", "cold:2", "", false, 0, 0, null, null, null, false)
		};

		// Token: 0x0400004A RID: 74
		internal static LifeT[] Adult = new LifeT[]
		{
			Backstory.L("faith", "any", 20, 60, 6, "Encontró a Dios en una carpa de predicación tras un mal año", "coward:1", "devout", false, 0, 0, null, null, null, false),
			Backstory.L("faith", "any", 20, 60, 6, "Dejó de ir a la iglesia después del funeral de su madre", "cold:2", "godless", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "any", 18, 50, 6, "Ganó un concurso de tiro en una feria del condado", "proud:3", "aggr:+2", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "hombre", 18, 50, 6, "Recibió un balazo en una pelea de taberna en {town}", "hothead:2", "aggr:+3", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "any", 18, 60, 6, "Perdió sus ahorros frente a un tahúr tramposo", "greedy:3", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "any", 18, 60, 4, "Sacó del río a un muchacho que se estaba ahogando", "proud:1", "amistoso", false, 0, 0, null, null, null, false),
			Backstory.L("loss", "any", 15, 60, 6, "Perdió a un hermano por la fiebre", "cold:2 coward:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("loss", "any", 18, 55, 6, "Enterró a su mejor amigo después de un tiroteo", "cold:2 hothead:2", "aggr:+4", false, 0, 0, null, null, null, false),
			Backstory.L("faith", "wapiti", 18, 60, 8, "Mantiene las viejas costumbres, digan lo que digan los misioneros", "proud:2", "devout", false, 0, 0, null, null, null, false),
			Backstory.L("faith", "wapiti", 18, 60, 5, "Dejó de rezar el invierno en que murió su madre", "cold:2", "godless", false, 0, 0, null, null, null, false),
			Backstory.L("loss", "wapiti", 15, 60, 8, "Perdió a un hermano por la enfermedad de la tos", "cold:2 hothead:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "wapiti", 18, 50, 7, "Ganó una carrera de caballos contra jinetes de tres bandas", "proud:3", "", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "wapiti", 18, 50, 6, "Fue encarcelado en Valentine por una pelea que él no empezó", "hothead:2 proud:1", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("loss", "wapiti", 18, 60, 6, "Enterró a un amigo al que mataron los soldados", "hothead:2 cold:2", "aggr:+4", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "wapiti", 25, 60, 5, "Enseñó a rastrear a sus sobrinos tal como su tío le enseñó a él", "loyal:2", "amistoso", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "any", 18, 60, 5, "Mordido por una serpiente en el matorral; casi muere", "coward:2", "fear:+4", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "any", 22, 60, 5, "Empezó a beber fuerte y nunca lo dejó", "hothead:1 drifter:1", "corto", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "hombre", 18, 45, 6, "Venció a puño limpio a un hombre más grande en {town}", "proud:3", "aggr:+3", false, 0, 0, null, "BRAWL", "Pelea a puño limpio en {town}: gana {name}", false),
			Backstory.L("adult", "hombre", 18, 50, 5, "Se echó atrás en un duelo en Armadillo", "coward:4", "fear:+3", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "any", 20, 60, 5, "Ahorra cada centavo que gana", "greedy:2 cold:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "any", 18, 50, 5, "Envía dinero a casa a su madre todos los meses", "loyal:2", "amistoso", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "any", 18, 55, 4, "Tuvo que pegar un tiro a su propio caballo cuando se rompió una pata, y lloró", "cold:-1 coward:1", "amistoso", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "any", 20, 60, 4, "Aguantó una ventisca a solas en los Grizzlies durante nueve días", "cold:3 drifter:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "mujer", 20, 60, 6, "Cuidó a medio {town} durante un invierno de difteria", "loyal:2", "amistoso", false, 0, 0, null, null, null, false),
			Backstory.L("faith", "mujer", 22, 60, 5, "Se unió a la liga por la templanza y destrozó a hachazos las botellas de una taberna", "proud:2 hothead:2", "devout", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "mujer", 20, 60, 5, "Guarda una pistola en el delantal desde que un tipo entró en su cocina", "cold:2 coward:1", "aggr:+3", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "mujer", 22, 60, 5, "Sacaba adelante la casa con lo que su marido no se gastaba en bebida", "cold:2 greedy:1", "hosco", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "mujer", 18, 50, 4, "Ganó el concurso de pasteles de la feria de {town} tres años seguidos", "proud:2", "amistoso", false, 0, 0, null, null, null, false),
			Backstory.L("loss", "mujer", 20, 50, 6, "Perdió un bebé por la fiebre y jamás habla de ello", "cold:3 coward:1", "", false, 0, 0, null, null, null, false),
			Backstory.L("adult", "mujer", 20, 55, 4, "Esperó tres años a un amor que nunca regresó de los campos de oro", "loyal:2 cold:1", "", false, 0, 0, null, null, null, false)
		};

		// Token: 0x0400004B RID: 75
		[TupleElementNames(new string[]
		{
			"name",
			"get",
			"set"
		})]
		private static readonly ValueTuple<string, Func<LifeT[]>, Action<LifeT[]>>[] PoolSlots = new ValueTuple<string, Func<LifeT[]>, Action<LifeT[]>>[]
		{
			new ValueTuple<string, Func<LifeT[]>, Action<LifeT[]>>("boyhood", () => Backstory.Boyhood, delegate(LifeT[] v)
			{
				Backstory.Boyhood = v;
			}),
			new ValueTuple<string, Func<LifeT[]>, Action<LifeT[]>>("youth", () => Backstory.Youth, delegate(LifeT[] v)
			{
				Backstory.Youth = v;
			}),
			new ValueTuple<string, Func<LifeT[]>, Action<LifeT[]>>("work", () => Backstory.Work, delegate(LifeT[] v)
			{
				Backstory.Work = v;
			}),
			new ValueTuple<string, Func<LifeT[]>, Action<LifeT[]>>("crime", () => Backstory.Crime, delegate(LifeT[] v)
			{
				Backstory.Crime = v;
			}),
			new ValueTuple<string, Func<LifeT[]>, Action<LifeT[]>>("gang", () => Backstory.GangLife, delegate(LifeT[] v)
			{
				Backstory.GangLife = v;
			}),
			new ValueTuple<string, Func<LifeT[]>, Action<LifeT[]>>("adult", () => Backstory.Adult, delegate(LifeT[] v)
			{
				Backstory.Adult = v;
			})
		};

		// Token: 0x0400004C RID: 76
		[TupleElementNames(new string[]
		{
			"name",
			"get",
			"set"
		})]
		private static readonly ValueTuple<string, Func<string[]>, Action<string[]>>[] PlaceSlots = new ValueTuple<string, Func<string[]>, Action<string[]>>[]
		{
			new ValueTuple<string, Func<string[]>, Action<string[]>>("irishCounties", () => Backstory.IrishCounties, delegate(string[] v)
			{
				Backstory.IrishCounties = v;
			}),
			new ValueTuple<string, Func<string[]>, Action<string[]>>("irishAmerican", () => Backstory.IrishAmerican, delegate(string[] v)
			{
				Backstory.IrishAmerican = v;
			}),
			new ValueTuple<string, Func<string[]>, Action<string[]>>("southern", () => Backstory.Southern, delegate(string[] v)
			{
				Backstory.Southern = v;
			}),
			new ValueTuple<string, Func<string[]>, Action<string[]>>("northern", () => Backstory.Northern, delegate(string[] v)
			{
				Backstory.Northern = v;
			}),
			new ValueTuple<string, Func<string[]>, Action<string[]>>("frontier", () => Backstory.Frontier, delegate(string[] v)
			{
				Backstory.Frontier = v;
			}),
			new ValueTuple<string, Func<string[]>, Action<string[]>>("towns", () => Backstory.Towns, delegate(string[] v)
			{
				Backstory.Towns = v;
			}),
			new ValueTuple<string, Func<string[]>, Action<string[]>>("irishWives", () => Backstory.IrishWives, delegate(string[] v)
			{
				Backstory.IrishWives = v;
			}),
			new ValueTuple<string, Func<string[]>, Action<string[]>>("wives", () => Backstory.Wives, delegate(string[] v)
			{
				Backstory.Wives = v;
			})
		};

		// Token: 0x0400004D RID: 77
		public static int AddedFromBuiltIn;

		// Token: 0x020000A9 RID: 169
		public class Ctx
		{
			// Token: 0x04000F60 RID: 3936
			public int Year;

			// Token: 0x04000F61 RID: 3937
			public int Month;

			// Token: 0x04000F62 RID: 3938
			public int Age;

			// Token: 0x04000F63 RID: 3939
			public int JoinMonth;

			// Token: 0x04000F64 RID: 3940
			public string Born;

			// Token: 0x04000F65 RID: 3941
			public string Home;

			// Token: 0x04000F66 RID: 3942
			public bool Irish;

			// Token: 0x04000F67 RID: 3943
			public bool IrishBorn;

			// Token: 0x04000F68 RID: 3944
			public bool South;

			// Token: 0x04000F69 RID: 3945
			public bool North;

			// Token: 0x04000F6A RID: 3946
			public Random Rng;

			// Token: 0x04000F6B RID: 3947
			public NemesisRecord R;
		}
	}
}
