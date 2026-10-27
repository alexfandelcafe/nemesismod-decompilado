using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using RDR2;

namespace NemesisSystem
{
	// Token: 0x02000017 RID: 23
	internal static class FactionContent
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x0000DE54 File Offset: 0x0000C054
		private static string BankName(string[] arr)
		{
			if (arr == null)
			{
				return null;
			}
			FieldInfo fieldInfo = typeof(NameBank).GetFields(BindingFlags.Static | BindingFlags.Public).FirstOrDefault((FieldInfo f) => f.FieldType == typeof(string[]) && f.GetValue(null) == arr);
			if (fieldInfo == null)
			{
				return null;
			}
			return fieldInfo.Name;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000DEA8 File Offset: 0x0000C0A8
		private static string[] Bank(JToken t)
		{
			if (t == null || t.Type == JTokenType.Null)
			{
				return null;
			}
			if (t.Type == JTokenType.String)
			{
				FieldInfo field = typeof(NameBank).GetField(t.Value<string>(), BindingFlags.Static | BindingFlags.Public);
				return ((field != null) ? field.GetValue(null) : null) as string[];
			}
			return t.ToObject<string[]>();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000DEFD File Offset: 0x0000C0FD
		internal static string Hex(uint h)
		{
			return "0x" + h.ToString("X8");
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000DF18 File Offset: 0x0000C118
		internal static uint Unhex(string s)
		{
			s = (s ?? "").Trim();
			if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
			{
				return Convert.ToUInt32(s.Substring(2), 16);
			}
			uint result;
			if (!uint.TryParse(s, out result))
			{
				return Joaat.Of(s);
			}
			return result;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000DF68 File Offset: 0x0000C168
		internal static JObject Export(Gang g)
		{
			JObject jobject = new JObject();
			foreach (FieldInfo fieldInfo in FactionContent.Fields)
			{
				if (!FactionContent.Special.Contains(fieldInfo.Name))
				{
					object value = fieldInfo.GetValue(g);
					object value2 = fieldInfo.GetValue(FactionContent.Defaults);
					if (fieldInfo.Name == "Id" || fieldInfo.Name == "Name")
					{
						jobject[fieldInfo.Name] = (value as string);
					}
					else if (value != null && !FactionContent.Same(value, value2))
					{
						jobject[fieldInfo.Name] = JToken.FromObject(value, FactionContent.Ser);
					}
				}
			}
			jobject["LeaderName"] = (g.LoreLeader ?? g.LeaderName);
			jobject["LeaderBlurb"] = (g.LoreBlurb ?? g.LeaderBlurb);
			foreach (string text in FactionContent.Banks)
			{
				string[] array = (string[])typeof(Gang).GetField(text).GetValue(g);
				if (array != null)
				{
					string text2 = FactionContent.BankName(array);
					jobject[text] = ((text2 != null) ? text2 : JArray.FromObject(array));
				}
			}
			jobject["Accent"] = new JArray(new object[]
			{
				g.Accent.Item1,
				g.Accent.Item2,
				g.Accent.Item3
			});
			if (g.Tiers != null)
			{
				jobject["Tiers"] = new JArray(g.Tiers.Select(delegate([TupleElementNames(new string[]
				{
					"Label",
					"MinRank",
					"Seats"
				})] ValueTuple<string, int, int> t)
				{
					JObject jobject3 = new JObject();
					jobject3["Label"] = t.Item1;
					jobject3["MinRank"] = t.Item2;
					jobject3["Seats"] = t.Item3;
					return jobject3;
				}));
			}
			if (g.Seed != null)
			{
				jobject["Seed"] = new JArray(g.Seed.Select(delegate([TupleElementNames(new string[]
				{
					"Rank",
					"Count",
					"Models"
				})] ValueTuple<int, int, PedHash[]> s)
				{
					JObject jobject3 = new JObject();
					jobject3["Rank"] = s.Item1;
					jobject3["Count"] = s.Item2;
					jobject3["Models"] = JArray.FromObject(from m in s.Item3
					select m.ToString());
					return jobject3;
				}));
			}
			if (g.ExtraSidearms.Length != 0)
			{
				jobject["ExtraSidearms"] = new JArray(g.ExtraSidearms.Select(delegate([TupleElementNames(new string[]
				{
					"w",
					"name",
					"weight",
					"minRank"
				})] ValueTuple<eWeapon, string, int, int> w)
				{
					JObject jobject3 = new JObject();
					jobject3["Weapon"] = w.Item1.ToString();
					jobject3["Name"] = w.Item2;
					jobject3["Weight"] = w.Item3;
					jobject3["MinRank"] = w.Item4;
					return jobject3;
				}));
			}
			if (g.ExtraLongGuns.Length != 0)
			{
				jobject["ExtraLongGuns"] = new JArray(g.ExtraLongGuns.Select(delegate([TupleElementNames(new string[]
				{
					"w",
					"name",
					"weight"
				})] ValueTuple<eWeapon, string, int> w)
				{
					JObject jobject3 = new JObject();
					jobject3["Weapon"] = w.Item1.ToString();
					jobject3["Name"] = w.Item2;
					jobject3["Weight"] = w.Item3;
					return jobject3;
				}));
			}
			if (g.SeedHate != null)
			{
				jobject["SeedHate"] = new JArray(new object[]
				{
					g.SeedHate.Value.Item1,
					g.SeedHate.Value.Item2
				});
			}
			if (g.OnlyDistricts != null)
			{
				JObject jobject2 = jobject;
				string propertyName = "OnlyDistricts";
				IEnumerable<uint> onlyDistricts = g.OnlyDistricts;
				Func<uint, string> selector;
				if ((selector = FactionContent.<>O.<0>__Hex) == null)
				{
					selector = (FactionContent.<>O.<0>__Hex = new Func<uint, string>(FactionContent.Hex));
				}
				jobject2[propertyName] = JArray.FromObject(onlyDistricts.Select(selector));
			}
			if (g.DistrictWeights != null)
			{
				jobject["DistrictWeights"] = JObject.FromObject(g.DistrictWeights.ToDictionary((KeyValuePair<uint, double> kv) => FactionContent.Hex(kv.Key), (KeyValuePair<uint, double> kv) => kv.Value));
			}
			if (g.Jobs != null && g.Jobs.Length != 0)
			{
				jobject["Jobs"] = new JArray(g.Jobs.Select(delegate([TupleElementNames(new string[]
				{
					"title",
					"place",
					"crew"
				})] ValueTuple<string, string, int> j)
				{
					JObject jobject3 = new JObject();
					jobject3["Title"] = j.Item1;
					jobject3["Place"] = j.Item2;
					jobject3["Crew"] = j.Item3;
					return jobject3;
				}));
			}
			return jobject;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000E360 File Offset: 0x0000C560
		private static bool Same(object a, object b)
		{
			if (a == null || b == null)
			{
				return a == b;
			}
			Array array = a as Array;
			if (array != null)
			{
				Array array2 = b as Array;
				if (array2 != null)
				{
					return array.Length == 0 && array2.Length == 0;
				}
			}
			IDictionary dictionary = a as IDictionary;
			if (dictionary != null)
			{
				IDictionary dictionary2 = b as IDictionary;
				if (dictionary2 != null)
				{
					return dictionary.Count == 0 && dictionary2.Count == 0;
				}
			}
			return a.Equals(b);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000E3D0 File Offset: 0x0000C5D0
		internal static string Apply(Gang g, JObject o)
		{
			List<string> bad = new List<string>();
			Func<JToken, ValueTuple<int, int, PedHash[]>> <>9__1;
			foreach (JProperty jproperty in o.Properties())
			{
				try
				{
					string name = jproperty.Name;
					if (name != null)
					{
						switch (name.Length)
						{
						case 4:
						{
							char c = name[0];
							if (c != 'J')
							{
								if (c != 'S')
								{
									goto IL_5B0;
								}
								if (!(name == "Seed"))
								{
									goto IL_5B0;
								}
								IEnumerable<JToken> value = jproperty.Value;
								Func<JToken, ValueTuple<int, int, PedHash[]>> selector;
								if ((selector = <>9__1) == null)
								{
									selector = (<>9__1 = ((JToken s) => new ValueTuple<int, int, PedHash[]>((int)s["Rank"], (int)s["Count"], FactionContent.Models(s["Models"], bad))));
								}
								g.Seed = value.Select(selector).ToArray<ValueTuple<int, int, PedHash[]>>();
								continue;
							}
							else
							{
								if (!(name == "Jobs"))
								{
									goto IL_5B0;
								}
								g.Jobs = (from j in jproperty.Value
								select new ValueTuple<string, string, int>((string)j["Title"], ((string)j["Place"]) ?? "el camino junto a {t}", ((int?)j["Crew"]).GetValueOrDefault(3))).ToArray<ValueTuple<string, string, int>>();
								continue;
							}
							break;
						}
						case 5:
							if (!(name == "Tiers"))
							{
								goto IL_5B0;
							}
							g.Tiers = (from t in jproperty.Value
							select new ValueTuple<string, int, int>((string)t["Label"], (int)t["MinRank"], (int)t["Seats"])).ToArray<ValueTuple<string, int, int>>();
							continue;
						case 6:
						{
							char c = name[0];
							if (c != 'A')
							{
								if (c != 'T')
								{
									goto IL_5B0;
								}
								if (!(name == "Titles"))
								{
									goto IL_5B0;
								}
							}
							else
							{
								if (!(name == "Accent"))
								{
									goto IL_5B0;
								}
								int[] array = jproperty.Value.ToObject<int[]>();
								if (array.Length >= 3)
								{
									g.Accent = new ValueTuple<int, int, int>(array[0], array[1], array[2]);
									continue;
								}
								continue;
							}
							break;
						}
						case 7:
						case 12:
						case 14:
							goto IL_5B0;
						case 8:
						{
							if (!(name == "SeedHate"))
							{
								goto IL_5B0;
							}
							int[] array2 = jproperty.Value.ToObject<int[]>();
							g.SeedHate = ((array2.Length >= 2) ? new ValueTuple<int, int>?(new ValueTuple<int, int>(array2[0], array2[1])) : null);
							continue;
						}
						case 9:
							if (!(name == "LastNames"))
							{
								goto IL_5B0;
							}
							break;
						case 10:
						{
							char c = name[0];
							if (c != 'F')
							{
								if (c != 'L')
								{
									goto IL_5B0;
								}
								if (!(name == "LeaderName"))
								{
									goto IL_5B0;
								}
								g.LeaderName = (g.LoreLeader = jproperty.Value.Value<string>());
								continue;
							}
							else if (!(name == "FirstNames"))
							{
								goto IL_5B0;
							}
							break;
						}
						case 11:
						{
							char c = name[0];
							if (c != 'L')
							{
								if (c != 'T')
								{
									goto IL_5B0;
								}
								if (!(name == "TroopModels"))
								{
									goto IL_5B0;
								}
								g.TroopModels = FactionContent.Models(jproperty.Value, bad);
								continue;
							}
							else
							{
								if (!(name == "LeaderBlurb"))
								{
									goto IL_5B0;
								}
								g.LeaderBlurb = (g.LoreBlurb = jproperty.Value.Value<string>());
								continue;
							}
							break;
						}
						case 13:
						{
							char c = name[5];
							if (c <= 'S')
							{
								if (c != 'L')
								{
									if (c != 'S')
									{
										goto IL_5B0;
									}
									if (!(name == "ExtraSidearms"))
									{
										goto IL_5B0;
									}
									g.ExtraSidearms = (from w in jproperty.Value
									select new ValueTuple<eWeapon, string, int, int>(FactionContent.Weapon(w["Weapon"]), (string)w["Name"], (int)w["Weight"], ((int?)w["MinRank"]).GetValueOrDefault(1))).ToArray<ValueTuple<eWeapon, string, int, int>>();
									continue;
								}
								else
								{
									if (!(name == "ExtraLongGuns"))
									{
										goto IL_5B0;
									}
									g.ExtraLongGuns = (from w in jproperty.Value
									select new ValueTuple<eWeapon, string, int>(FactionContent.Weapon(w["Weapon"]), (string)w["Name"], (int)w["Weight"])).ToArray<ValueTuple<eWeapon, string, int>>();
									continue;
								}
							}
							else if (c != 'd')
							{
								if (c != 'i')
								{
									goto IL_5B0;
								}
								if (!(name == "OnlyDistricts"))
								{
									goto IL_5B0;
								}
								HashSet<uint> onlyDistricts;
								if (jproperty.Value.Type != JTokenType.Null)
								{
									onlyDistricts = new HashSet<uint>(from t in jproperty.Value
									select FactionContent.Unhex((string)t));
								}
								else
								{
									onlyDistricts = null;
								}
								g.OnlyDistricts = onlyDistricts;
								continue;
							}
							else if (!(name == "ExcludedNames"))
							{
								goto IL_5B0;
							}
							break;
						}
						case 15:
						{
							if (!(name == "DistrictWeights"))
							{
								goto IL_5B0;
							}
							Dictionary<uint, double> districtWeights;
							if (jproperty.Value.Type != JTokenType.Null)
							{
								districtWeights = ((JObject)jproperty.Value).Properties().ToDictionary((JProperty x) => FactionContent.Unhex(x.Name), (JProperty x) => (double)x.Value);
							}
							else
							{
								districtWeights = null;
							}
							g.DistrictWeights = districtWeights;
							continue;
						}
						default:
							goto IL_5B0;
						}
						string[] array3 = FactionContent.Bank(jproperty.Value);
						if (array3 == null && jproperty.Value.Type == JTokenType.String)
						{
							bad.Add(string.Format("{0}: no name bank '{1}'", jproperty.Name, jproperty.Value));
							continue;
						}
						typeof(Gang).GetField(jproperty.Name).SetValue(g, array3);
						continue;
					}
					IL_5B0:
					FieldInfo field = typeof(Gang).GetField(jproperty.Name, BindingFlags.Instance | BindingFlags.Public);
					if (field == null)
					{
						bad.Add("unknown field '" + jproperty.Name + "'");
					}
					else
					{
						field.SetValue(g, (jproperty.Value.Type == JTokenType.Null) ? null : jproperty.Value.ToObject(field.FieldType, FactionContent.Ser));
					}
				}
				catch (Exception ex)
				{
					bad.Add(jproperty.Name + ": " + ex.Message);
				}
			}
			if (bad.Count != 0)
			{
				return string.Join("; ", bad);
			}
			return null;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000EA98 File Offset: 0x0000CC98
		private static PedHash[] Models(JToken t, List<string> bad)
		{
			List<PedHash> list = new List<PedHash>();
			foreach (JToken value in ((IEnumerable<JToken>)(t ?? new JArray())))
			{
				string text = (string)value;
				PedHash item;
				if (Enum.TryParse<PedHash>(text, true, out item))
				{
					list.Add(item);
				}
				else
				{
					bad.Add("no ped model '" + text + "'");
				}
			}
			return list.ToArray();
		}

		// Token: 0x060000DB RID: 219 RVA: 0x0000EB20 File Offset: 0x0000CD20
		private static eWeapon Weapon(JToken t)
		{
			return (eWeapon)Enum.Parse(typeof(eWeapon), (string)t, true);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000EB40 File Offset: 0x0000CD40
		internal static void FillNew(Gang g)
		{
			g.ModelTokens = (g.ModelTokens ?? new string[0]);
			g.FirstNames = (g.FirstNames ?? NameBank.AmericanFirst);
			g.LastNames = (g.LastNames ?? NameBank.AmericanLast);
			g.Titles = (g.Titles ?? NameBank.AmericanTitles);
			g.LeaderName = (g.LeaderName ?? ("the head of the " + g.Name));
			g.LoreLeader = (g.LoreLeader ?? g.LeaderName);
			g.LeaderBlurb = (g.LeaderBlurb ?? "");
			g.LoreBlurb = (g.LoreBlurb ?? g.LeaderBlurb);
			if (g.Tiers == null || g.Tiers.Length == 0)
			{
				g.Tiers = new ValueTuple<string, int, int>[]
				{
					new ValueTuple<string, int, int>("OFICIALES", 4, 2),
					new ValueTuple<string, int, int>("PEONES", 2, 4),
					new ValueTuple<string, int, int>("HOMBRES", 1, 8)
				};
			}
			if (g.Seed == null || g.Seed.Length == 0)
			{
				FactionContent.<>c__DisplayClass14_0 CS$<>8__locals1 = new FactionContent.<>c__DisplayClass14_0();
				FactionContent.<>c__DisplayClass14_0 CS$<>8__locals2 = CS$<>8__locals1;
				PedHash[] m;
				if (g.TroopModels == null || g.TroopModels.Length == 0)
				{
					(m = new PedHash[1])[0] = 347599949;
				}
				else
				{
					m = g.TroopModels;
				}
				CS$<>8__locals2.m = m;
				g.Seed = (from t in g.Tiers
				select new ValueTuple<int, int, PedHash[]>(t.Item2, t.Item3, CS$<>8__locals1.m)).ToArray<ValueTuple<int, int, PedHash[]>>();
			}
		}

		// Token: 0x0400013E RID: 318
		private static readonly JsonSerializer Ser = JsonSerializer.Create(new JsonSerializerSettings
		{
			Converters = 
			{
				new StringEnumConverter()
			},
			NullValueHandling = NullValueHandling.Ignore
		});

		// Token: 0x0400013F RID: 319
		private static readonly HashSet<string> Special = new HashSet<string>
		{
			"Seed",
			"Tiers",
			"Accent",
			"ExtraSidearms",
			"ExtraLongGuns",
			"SeedHate",
			"OnlyDistricts",
			"DistrictWeights",
			"Jobs",
			"FirstNames",
			"LastNames",
			"Titles",
			"ExcludedNames",
			"LeaderName",
			"LeaderBlurb",
			"LoreLeader",
			"LoreBlurb"
		};

		// Token: 0x04000140 RID: 320
		private static readonly string[] Banks = new string[]
		{
			"FirstNames",
			"LastNames",
			"Titles",
			"ExcludedNames"
		};

		// Token: 0x04000141 RID: 321
		private static readonly FieldInfo[] Fields = typeof(Gang).GetFields(BindingFlags.Instance | BindingFlags.Public);

		// Token: 0x04000142 RID: 322
		private static readonly Gang Defaults = new Gang();

		// Token: 0x020000C8 RID: 200
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000FC7 RID: 4039
			public static Func<uint, string> <0>__Hex;
		}
	}
}
