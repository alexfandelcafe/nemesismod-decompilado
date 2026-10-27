using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Linq;
using RDR2;

namespace NemesisSystem
{
	// Token: 0x0200001A RID: 26
	internal static class Gangs
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x000F26A0 File Offset: 0x000F08A0
		public static Gang[] Defined
		{
			get
			{
				return Gangs._defined;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x000F26A7 File Offset: 0x000F08A7
		// (set) Token: 0x06000A40 RID: 2624 RVA: 0x000F26AE File Offset: 0x000F08AE
		public static int Era { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x000F26B6 File Offset: 0x000F08B6
		// (set) Token: 0x06000A42 RID: 2626 RVA: 0x000F26BD File Offset: 0x000F08BD
		public static Gang[] All { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x000F26C5 File Offset: 0x000F08C5
		// (set) Token: 0x06000A44 RID: 2628 RVA: 0x000F26CC File Offset: 0x000F08CC
		public static int Version { get; private set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000A45 RID: 2629 RVA: 0x000F26D4 File Offset: 0x000F08D4
		internal static JObject[] BuiltInExport
		{
			get
			{
				JObject[] result;
				if ((result = Gangs._builtInExport) == null)
				{
					IEnumerable<Gang> builtIn = Gangs.BuiltIn;
					Func<Gang, JObject> selector;
					if ((selector = Gangs.<>O.<0>__Export) == null)
					{
						selector = (Gangs.<>O.<0>__Export = new Func<Gang, JObject>(FactionContent.Export));
					}
					result = (Gangs._builtInExport = builtIn.Select(selector).ToArray<JObject>());
				}
				return result;
			}
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000F270F File Offset: 0x000F090F
		internal static void SetAll(Gang[] defined)
		{
			Gangs._defined = defined;
			Gangs.Rebuild();
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x000F271C File Offset: 0x000F091C
		internal static bool SetEra(int era)
		{
			era = ((era >= 1907) ? 1907 : 1899);
			if (era == Gangs.Era)
			{
				return false;
			}
			Gangs.Era = era;
			Gangs.Rebuild();
			return true;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x000F274A File Offset: 0x000F094A
		internal static bool InEra(Gang g)
		{
			return g != null && (g.Era == 0 || g.Era == Gangs.Era);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x000F2768 File Offset: 0x000F0968
		private static void Rebuild()
		{
			IEnumerable<Gang> defined = Gangs._defined;
			Func<Gang, bool> predicate;
			if ((predicate = Gangs.<>O.<1>__InEra) == null)
			{
				predicate = (Gangs.<>O.<1>__InEra = new Func<Gang, bool>(Gangs.InEra));
			}
			Gangs.All = defined.Where(predicate).ToArray<Gang>();
			Dictionary<uint, Gang> byModel = Gangs._byModel;
			lock (byModel)
			{
				Gangs._byModel.Clear();
			}
			Gangs._byId = null;
			Gangs.Version++;
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x000F27EC File Offset: 0x000F09EC
		public static Gang ForModel(uint model)
		{
			Dictionary<uint, Gang> byModel = Gangs._byModel;
			Gang result;
			lock (byModel)
			{
				Gang gang;
				if (Gangs._byModel.TryGetValue(model, out gang))
				{
					result = gang;
				}
				else
				{
					gang = Gangs.All.FirstOrDefault((Gang x) => x.IsMemberModel(model));
					Gangs._byModel[model] = gang;
					result = gang;
				}
			}
			return result;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000F2878 File Offset: 0x000F0A78
		public static Gang ById(string id)
		{
			if (id == null)
			{
				return null;
			}
			Dictionary<string, Gang> dictionary = Gangs._byId;
			if (dictionary == null)
			{
				dictionary = Gangs._defined.ToDictionary((Gang g) => g.Id);
				dictionary[Gangs.VanDerLinde.Id] = Gangs.VanDerLinde;
				Gangs._byId = dictionary;
			}
			Gang result;
			if (!dictionary.TryGetValue(id, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000F28E6 File Offset: 0x000F0AE6
		public static Gang Of(NemesisRecord r)
		{
			return Gangs.ById((r != null) ? r.Gang : null);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x000F28F9 File Offset: 0x000F0AF9
		public static bool Orders(NemesisRecord r)
		{
			Gang gang = Gangs.Of(r);
			return gang != null && gang.OrdersNotFeelings;
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x000F290C File Offset: 0x000F0B0C
		public static bool Squads(NemesisRecord r)
		{
			Gang gang = Gangs.Of(r);
			return gang != null && gang.SquadsOnly;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000F2920 File Offset: 0x000F0B20
		// Note: this type is marked as 'beforefieldinit'.
		static Gangs()
		{
			Gang gang = new Gang();
			gang.CrewExtra = 1;
			gang.Era = 1899;
			gang.Id = "odriscoll";
			gang.Jobs = new ValueTuple<string, string, int>[]
			{
				new ValueTuple<string, string, int>("asaltar la diligencia en el camino de salida de {t}", "el camino de salida de {t}", 4),
				new ValueTuple<string, string, int>("atacar una granja a las afueras de {t}", "una granja a las afueras de {t}", 3),
				new ValueTuple<string, string, int>("robar caballos de un rancho cerca de {t}", "un rancho cerca de {t}", 3),
				new ValueTuple<string, string, int>("asaltar a viajeros en el camino junto a {t}", "el camino junto a {t}", 3),
				new ValueTuple<string, string, int>("saldar cuentas con un hombre en {t}", "las afueras de {t}", 2)
			};
			gang.JobTowns = new string[]
			{
				"Valentine",
				"Strawberry",
				"Annesburg",
				"Van Horn"
			};
			Gang gang2 = gang;
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			dictionary["lemoyneraiders"] = -60;
			dictionary["murfree"] = -45;
			dictionary["skinners"] = -40;
			dictionary["bronte"] = -40;
			dictionary["army"] = -70;
			dictionary["klan"] = -10;
			dictionary["wapiti"] = -20;
			gang2.Relations = dictionary;
			gang.LoreLeaderProtected = true;
			gang.Name = "O'Driscoll Boys";
			gang.ModelTokens = new string[]
			{
				"duster",
				"odriscoll"
			};
			gang.LeaderName = "Colm O'Driscoll";
			gang.LeaderBlurb = "Líder de los O'Driscoll. El enemigo más antiguo de Dutch.";
			gang.LeaderModel = "cs_colmodriscoll";
			gang.FirstNames = NameBank.IrishFirst;
			gang.LastNames = NameBank.IrishLast;
			gang.Titles = NameBank.ODriscollTitles;
			gang.Tiers = new ValueTuple<string, int, int>[]
			{
				new ValueTuple<string, int, int>("TENIENTES", 4, 3),
				new ValueTuple<string, int, int>("CAPITÁN/ES", 2, 5),
				new ValueTuple<string, int, int>("MUCHACHOS", 1, 10)
			};
			gang.Accent = new ValueTuple<int, int, int>(92, 150, 98);
			gang.MixedFootSoldiers = true;
			gang.HasCamps = true;
			gang.WeightNorth = 1.6;
			gang.WeightSouth = 0.2;
			gang.Faiths = new string[]
			{
				"Católico"
			};
			gang.ExtraSidearms = new ValueTuple<eWeapon, string, int, int>[]
			{
				new ValueTuple<eWeapon, string, int, int>(379542007, "un revólver Cattleman", 3, 1)
			};
			Gang gang3 = gang;
			PedHash[] array = new PedHash[4];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.079E6166954058F7032A4F4E034427451639C956B95BCC24B5C5745DE0A01C93).FieldHandle);
			gang3.TroopModels = array;
			Gang gang4 = gang;
			ValueTuple<int, int, PedHash[]>[] array2 = new ValueTuple<int, int, PedHash[]>[4];
			int num = 0;
			int item = 4;
			int item2 = 3;
			PedHash[] array3 = new PedHash[3];
			RuntimeHelpers.InitializeArray(array3, fieldof(<PrivateImplementationDetails>.901ED33D6D4E643F05B7937A6DBF31FE97ED87A6105118CE3C21E3F856C583CD).FieldHandle);
			array2[num] = new ValueTuple<int, int, PedHash[]>(item, item2, array3);
			int num2 = 1;
			int item3 = 3;
			int item4 = 3;
			PedHash[] array4 = new PedHash[3];
			RuntimeHelpers.InitializeArray(array4, fieldof(<PrivateImplementationDetails>.8A9FDD7B90643FD2FAE3A135A5931BFBF8DBDF6D015234F330DF41CE00468B9A).FieldHandle);
			array2[num2] = new ValueTuple<int, int, PedHash[]>(item3, item4, array4);
			array2[2] = new ValueTuple<int, int, PedHash[]>(2, 2, new PedHash[]
			{
				-763072541,
				1336863290
			});
			int num3 = 3;
			int item5 = 1;
			int item6 = 10;
			PedHash[] array5 = new PedHash[5];
			RuntimeHelpers.InitializeArray(array5, fieldof(<PrivateImplementationDetails>.83E9921AA6ABCA2B31369A5A42663524AC7E359CE7A29A6B7C3F7FD47927077B).FieldHandle);
			array2[num3] = new ValueTuple<int, int, PedHash[]>(item5, item6, array5);
			gang4.Seed = array2;
			Gangs.ODriscolls = gang;
			gang = new Gang();
			gang.CrewExtra = 2;
			gang.Id = "lemoyneraiders";
			gang.EncounterRate = 0.3;
			gang.Jobs = new ValueTuple<string, string, int>[]
			{
				new ValueTuple<string, string, int>("quemar una granja a las afueras de {t}", "una granja a las afueras de {t}", 4),
				new ValueTuple<string, string, int>("emboscar a agentes de la ley en el camino a {t}", "el camino a {t}", 4),
				new ValueTuple<string, string, int>("pasar armas de contrabando por el bosque junto a {t}", "el bosque junto a {t}", 3),
				new ValueTuple<string, string, int>("dar una paliza a un oportunista norteño cerca de {t}", "el camino cerca de {t}", 3)
			};
			gang.JobTowns = new string[]
			{
				"Rhodes",
				"Saint Denis"
			};
			Gang gang5 = gang;
			Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
			dictionary2["army"] = -70;
			dictionary2["bronte"] = -55;
			dictionary2["klan"] = -40;
			dictionary2["murfree"] = -30;
			dictionary2["skinners"] = -25;
			dictionary2["wapiti"] = -15;
			gang5.Relations = dictionary2;
			gang.LoreLeaderProtected = true;
			gang.Name = "Lemoyne Raiders";
			gang.ModelTokens = new string[]
			{
				"uniexconfeds",
				"exconfed"
			};
			gang.LeaderName = "Lindsey Wofford";
			gang.LeaderBlurb = "Dirige a los Raiders desde Fort Brennand como si la guerra no hubiera terminado.";
			gang.LeaderModel = "u_m_m_uniexconfedsbounty_01|cs_exconfedsleader_01";
			gang.LeaderSpot = new float[]
			{
				2440f,
				285f,
				500f
			};
			gang.FirstNames = NameBank.SouthernFirst;
			gang.LastNames = NameBank.SouthernLast;
			gang.Titles = NameBank.RaiderTitles;
			gang.Tiers = new ValueTuple<string, int, int>[]
			{
				new ValueTuple<string, int, int>("CAPITÁN/ES", 4, 3),
				new ValueTuple<string, int, int>("SARGENTOS", 2, 4),
				new ValueTuple<string, int, int>("ASALTANTES", 1, 7)
			};
			gang.Seed = new ValueTuple<int, int, PedHash[]>[]
			{
				new ValueTuple<int, int, PedHash[]>(4, 2, new PedHash[]
				{
					-355385988
				}),
				new ValueTuple<int, int, PedHash[]>(3, 2, new PedHash[]
				{
					-628956517,
					-355385988
				}),
				new ValueTuple<int, int, PedHash[]>(2, 2, new PedHash[]
				{
					-969819655
				}),
				new ValueTuple<int, int, PedHash[]>(1, 7, new PedHash[]
				{
					-628956517,
					-969819655
				})
			};
			gang.Accent = new ValueTuple<int, int, int>(126, 142, 168);
			gang.AmbushBase = 0.15;
			gang.MixedFootSoldiers = true;
			gang.HasCamps = true;
			gang.WeightNorth = 0.03;
			gang.WeightSouth = 2.5;
			gang.WeightElsewhere = 0.03;
			gang.CombatMovement = new int?(2);
			gang.CombatAbility = new int?(1);
			gang.CombatRange = new int?(1);
			gang.Faiths = new string[]
			{
				"Bautista",
				"Metodista"
			};
			gang.MemberWealth = new Wealth?(Wealth.Poor);
			gang.MemberTrades = new string[]
			{
				"Exsoldado"
			};
			gang.OldArmyKnifeWeight = 5;
			gang.ExtraSidearms = new ValueTuple<eWeapon, string, int, int>[]
			{
				new ValueTuple<eWeapon, string, int, int>(132728264, "un viejo revólver Navy", 4, 1),
				new ValueTuple<eWeapon, string, int, int>(1529685685, "un revólver LeMat", 3, 3)
			};
			gang.ExtraLongGuns = new ValueTuple<eWeapon, string, int>[]
			{
				new ValueTuple<eWeapon, string, int>(1676963302, "un fusil Springfield", 4)
			};
			Gang gang6 = gang;
			PedHash[] array6 = new PedHash[3];
			RuntimeHelpers.InitializeArray(array6, fieldof(<PrivateImplementationDetails>.1DFEE9847F33128A4A2EF6D6CAD09686071057F1B151CC0828855B4DE1A68F09).FieldHandle);
			gang6.TroopModels = array6;
			gang.PatrolsIn = "south";
			Gangs.LemoyneRaiders = gang;
			gang = new Gang();
			gang.Id = "army";
			gang.EncounterRate = 0.4;
			gang.Jobs = new ValueTuple<string, string, int>[]
			{
				new ValueTuple<string, string, int>("peinar el camino a las afueras de {t} buscando O'Driscolls", "el camino a las afueras de {t}", 4),
				new ValueTuple<string, string, int>("escoltar un carro de suministros hasta {t}", "el camino a {t}", 4),
				new ValueTuple<string, string, int>("cazar desertores en las colinas junto a {t}", "las colinas junto a {t}", 3)
			};
			gang.JobTowns = new string[]
			{
				"Valentine",
				"Strawberry",
				"Annesburg",
				"Rhodes",
				"Van Horn"
			};
			Gang gang7 = gang;
			Dictionary<string, int> dictionary3 = new Dictionary<string, int>();
			dictionary3["klan"] = -35;
			dictionary3["wapiti"] = -45;
			dictionary3["bronte"] = -25;
			dictionary3["murfree"] = -50;
			dictionary3["skinners"] = -50;
			gang7.Relations = dictionary3;
			gang.TurfWars = false;
			gang.LoreLeaderProtected = true;
			gang.Name = "U.S. Army";
			gang.ModelTokens = new string[]
			{
				"army"
			};
			gang.LeaderName = "Col. Henry Favours";
			gang.LeaderBlurb = "Manda en Fort Wallace y mantiene a los Wapiti en su reserva. Quiere enterrar el problema de los forajidos.";
			gang.LeaderModel = "cs_colfavours";
			gang.FirstNames = NameBank.AmericanFirst;
			gang.LastNames = NameBank.ArmyLast;
			gang.Titles = NameBank.ArmyTitles;
			gang.Tiers = new ValueTuple<string, int, int>[]
			{
				new ValueTuple<string, int, int>("OFICIALES", 4, 3),
				new ValueTuple<string, int, int>("SARGENTOS", 2, 5),
				new ValueTuple<string, int, int>("SOLDADOS RASOS", 1, 14)
			};
			gang.Seed = new ValueTuple<int, int, PedHash[]>[]
			{
				new ValueTuple<int, int, PedHash[]>(4, 3, new PedHash[]
				{
					722236839
				}),
				new ValueTuple<int, int, PedHash[]>(3, 2, new PedHash[]
				{
					722236839
				}),
				new ValueTuple<int, int, PedHash[]>(2, 3, new PedHash[]
				{
					722236839,
					-2144902919
				}),
				new ValueTuple<int, int, PedHash[]>(1, 14, new PedHash[]
				{
					-2144902919,
					722236839
				})
			};
			gang.Accent = new ValueTuple<int, int, int>(112, 124, 68);
			gang.AmbushBase = 0.12;
			gang.OrdersNotFeelings = true;
			gang.UniformKit = true;
			gang.NeedsReason = true;
			gang.SquadsOnly = true;
			gang.RankInName = true;
			gang.UnnamedKillsPerPoint = new int?(1);
			gang.FamilyNames = false;
			gang.KeepsToItsOwn = true;
			gang.Feuds = false;
			gang.GangLifeEvents = false;
			gang.WeightNorth = 1.5;
			gang.WeightSouth = 0.3;
			gang.WeightElsewhere = 0.4;
			gang.WeightNearFort = 3.0;
			gang.CombatMovement = new int?(1);
			gang.CombatAbility = new int?(2);
			gang.CombatRange = new int?(2);
			gang.AccuracyMin = 40;
			gang.MemberWealth = new Wealth?(Wealth.Working);
			gang.MemberTrades = new string[]
			{
				"Soldado"
			};
			gang.TroopModels = new PedHash[]
			{
				-2144902919,
				722236839
			};
			gang.TroopLongGuns = true;
			gang.TroopAccuracy = 30;
			gang.PatrolsIn = "north";
			Gangs.Army = gang;
			gang = new Gang();
			gang.Id = "klan";
			gang.EncounterRate = 0.3;
			gang.Jobs = new ValueTuple<string, string, int>[]
			{
				new ValueTuple<string, string, int>("cabalgar de noche a las afueras de {t}", "los campos a las afueras de {t}", 4),
				new ValueTuple<string, string, int>("quemar una cruz a las afueras de {t}", "un campo a las afueras de {t}", 3)
			};
			gang.JobTowns = new string[]
			{
				"Rhodes"
			};
			Gang gang8 = gang;
			Dictionary<string, int> dictionary4 = new Dictionary<string, int>();
			dictionary4["bronte"] = -30;
			dictionary4["murfree"] = -20;
			dictionary4["skinners"] = -10;
			dictionary4["wapiti"] = -40;
			gang8.Relations = dictionary4;
			gang.Name = "The Klan";
			gang.ModelTokens = new string[0];
			gang.LeaderName = "High Warden Jebediah Pruitt";
			gang.LeaderBlurb = "Oculta su rostro, enciende las hogueras y manda a otros a morir.";
			gang.FirstNames = NameBank.SouthernFirst;
			gang.LastNames = NameBank.SouthernLast;
			gang.Titles = NameBank.KlanTitles;
			gang.Tiers = new ValueTuple<string, int, int>[]
			{
				new ValueTuple<string, int, int>("GUARDIANES", 4, 1),
				new ValueTuple<string, int, int>("PORTAANTORCHAS", 2, 2),
				new ValueTuple<string, int, int>("ENCAPUCHADOS", 1, 5)
			};
			gang.Seed = new ValueTuple<int, int, PedHash[]>[]
			{
				new ValueTuple<int, int, PedHash[]>(4, 1, new PedHash[]
				{
					969427509
				}),
				new ValueTuple<int, int, PedHash[]>(2, 2, new PedHash[]
				{
					969427509,
					138961043
				}),
				new ValueTuple<int, int, PedHash[]>(1, 5, new PedHash[]
				{
					138961043,
					969427509
				})
			};
			gang.Accent = new ValueTuple<int, int, int>(226, 222, 210);
			gang.AmbushBase = 0.08;
			gang.OnlyDistricts = new HashSet<uint>
			{
				3430691604U,
				687704893U,
				1308232528U,
				3824751532U
			};
			gang.CombatMovement = new int?(3);
			gang.CombatAbility = new int?(0);
			gang.AccuracyMax = 18;
			gang.MayFlee = true;
			gang.NoTowns = true;
			gang.DayWeight = 0.2;
			gang.NightWeight = 1.5;
			gang.TownModels = new string[]
			{
				"a_m_m_rhdtownfolk_01",
				"a_m_m_rhdtownfolk_02",
				"a_m_m_rhdtownfolk_01_laborer",
				"a_m_m_rhdforeman_01",
				"a_m_m_rhdupperclass_01",
				"a_m_m_rhdobesemen_01",
				"a_m_m_valfarmer_01",
				"a_m_m_vhttownfolk_01"
			};
			gang.Faiths = new string[]
			{
				"Bautista",
				"Metodista"
			};
			gang.MemberTrades = new string[]
			{
				"Granjero",
				"Dependiente de tienda"
			};
			gang.ExtraLongGuns = new ValueTuple<eWeapon, string, int>[]
			{
				new ValueTuple<eWeapon, string, int>(1845102363, "una escopeta de dos cañones", 5)
			};
			gang.TroopModels = new PedHash[]
			{
				138961043,
				969427509
			};
			Gangs.Klan = gang;
			gang = new Gang();
			gang.Id = "wapiti";
			gang.EncounterRate = 0.4;
			gang.Era = 1899;
			gang.Jobs = new ValueTuple<string, string, int>[]
			{
				new ValueTuple<string, string, int>("cazar en las colinas sobre {t}", "las colinas sobre {t}", 3),
				new ValueTuple<string, string, int>("vigilar el camino de los soldados junto a {t}", "el camino junto a {t}", 3)
			};
			gang.JobTowns = new string[]
			{
				"Strawberry",
				"Valentine"
			};
			Gang gang9 = gang;
			Dictionary<string, int> dictionary5 = new Dictionary<string, int>();
			dictionary5["murfree"] = -20;
			dictionary5["skinners"] = -45;
			gang9.Relations = dictionary5;
			gang.TurfWars = false;
			gang.LoreLeaderProtected = true;
			gang.Name = "Wapiti";
			gang.ModelTokens = new string[]
			{
				"wapwarriors"
			};
			gang.LeaderName = "Rains Fall";
			gang.LeaderModel = "cs_rainsfall";
			gang.LeaderBlurb = "Jefe de los Wapiti. Quiere que su pueblo sobreviva en la reserva, no que muera luchando contra el Ejército.";
			gang.FirstNames = NameBank.WapitiFirst;
			gang.LastNames = NameBank.WapitiLast;
			gang.Titles = NameBank.WapitiTitles;
			gang.ExcludedNames = NameBank.WapitiExcluded;
			gang.Tiers = new ValueTuple<string, int, int>[]
			{
				new ValueTuple<string, int, int>("JEFES DE GUERRA", 4, 2),
				new ValueTuple<string, int, int>("EXPLORADORES", 2, 4),
				new ValueTuple<string, int, int>("GUERREROS", 1, 8)
			};
			gang.Seed = new ValueTuple<int, int, PedHash[]>[]
			{
				new ValueTuple<int, int, PedHash[]>(4, 2, new PedHash[]
				{
					830712509
				}),
				new ValueTuple<int, int, PedHash[]>(3, 2, new PedHash[]
				{
					830712509
				}),
				new ValueTuple<int, int, PedHash[]>(2, 2, new PedHash[]
				{
					830712509
				}),
				new ValueTuple<int, int, PedHash[]>(1, 8, new PedHash[]
				{
					830712509
				})
			};
			gang.Accent = new ValueTuple<int, int, int>(178, 112, 74);
			gang.AmbushBase = 0.08;
			gang.HasCamps = true;
			gang.NeedsReason = true;
			gang.ReasonFromBounty = false;
			gang.SquadsOnly = true;
			gang.KeepsToItsOwn = true;
			gang.Feuds = false;
			gang.GangLifeEvents = false;
			gang.FamilyNames = false;
			gang.OwnLives = true;
			gang.TribalKit = true;
			gang.AnswersGunfire = false;
			gang.SeedHate = new ValueTuple<int, int>?(new ValueTuple<int, int>(25, 50));
			gang.OnlyDistricts = new HashSet<uint>
			{
				2311828277U,
				4174810561U,
				1835499550U
			};
			gang.DistrictWeights = new Dictionary<uint, double>
			{
				{
					2311828277U,
					1.5
				},
				{
					4174810561U,
					1.5
				},
				{
					1835499550U,
					0.5
				}
			};
			gang.CombatMovement = new int?(2);
			gang.CombatAbility = new int?(1);
			gang.CombatRange = new int?(1);
			gang.AccuracyMin = 25;
			gang.Faiths = new string[]
			{
				"las viejas costumbres"
			};
			gang.MemberWealth = new Wealth?(Wealth.Poor);
			gang.MemberTrades = new string[]
			{
				"Cazador",
				"Domador de caballos"
			};
			gang.TroopModels = new PedHash[]
			{
				830712509
			};
			gang.TroopAccuracy = 20;
			gang.PatrolsIn = "districts";
			Gangs.Wapiti = gang;
			gang = new Gang();
			gang.Id = "bronte";
			gang.EncounterRate = 0.5;
			gang.Era = 0;
			gang.Jobs = new ValueTuple<string, string, int>[]
			{
				new ValueTuple<string, string, int>("cobrar a un tendero retrasado en los pagos en {t}", "un callejón de {t}", 3),
				new ValueTuple<string, string, int>("descargar cajas de un barco de vapor cerca de {t}", "el camino de los muelles junto a {t}", 4),
				new ValueTuple<string, string, int>("dar una lección a un contrabandista rival a las afueras de {t}", "el camino del pantano a las afueras de {t}", 3)
			};
			gang.JobTowns = new string[]
			{
				"Saint Denis"
			};
			Gang gang10 = gang;
			Dictionary<string, int> dictionary6 = new Dictionary<string, int>();
			dictionary6["murfree"] = -20;
			dictionary6["skinners"] = -10;
			gang10.Relations = dictionary6;
			gang.LoreLeaderProtected = true;
			gang.Name = "Bronte Family";
			gang.ModelTokens = new string[]
			{
				"brontegoon",
				"nbxbronte"
			};
			gang.LeaderName = "Angelo Bronte";
			gang.LeaderModel = "cs_bronte";
			gang.LeaderBlurb = "Dueño de Saint Denis: la policía, los muelles y el oído del alcalde. Sonríe mientras lo hace.";
			gang.FirstNames = NameBank.ItalianFirst;
			gang.LastNames = NameBank.ItalianLast;
			gang.Titles = NameBank.MobTitles;
			gang.Tiers = new ValueTuple<string, int, int>[]
			{
				new ValueTuple<string, int, int>("CAPOS", 4, 2),
				new ValueTuple<string, int, int>("MATONES", 2, 4),
				new ValueTuple<string, int, int>("SOLDADOS", 1, 8)
			};
			gang.Seed = new ValueTuple<int, int, PedHash[]>[]
			{
				new ValueTuple<int, int, PedHash[]>(4, 2, new PedHash[]
				{
					1112571710,
					-390289284
				}),
				new ValueTuple<int, int, PedHash[]>(3, 2, new PedHash[]
				{
					1478983280,
					-1208814201
				}),
				new ValueTuple<int, int, PedHash[]>(2, 2, new PedHash[]
				{
					1478983280
				}),
				new ValueTuple<int, int, PedHash[]>(1, 8, new PedHash[]
				{
					1478983280,
					-1208814201
				})
			};
			gang.Accent = new ValueTuple<int, int, int>(150, 72, 84);
			gang.AmbushBase = 0.12;
			gang.HasCamps = true;
			gang.NeedsReason = true;
			gang.ReasonFromBounty = false;
			gang.HomeTown = "Saint Denis";
			gang.OnlyDistricts = new HashSet<uint>
			{
				2025841068U,
				3374368891U
			};
			gang.DistrictWeight = 2.0;
			gang.DayWeight = 0.8;
			gang.NightWeight = 1.4;
			gang.CombatMovement = new int?(1);
			gang.CombatAbility = new int?(1);
			gang.CombatRange = new int?(1);
			gang.AccuracyMin = 25;
			gang.Faiths = new string[]
			{
				"Católico"
			};
			gang.MemberWealth = new Wealth?(Wealth.Working);
			gang.ExtraSidearms = new ValueTuple<eWeapon, string, int, int>[]
			{
				new ValueTuple<eWeapon, string, int, int>(127400949, "un revólver de doble acción", 4, 1),
				new ValueTuple<eWeapon, string, int, int>(34411519, "una pistola Volcanic", 2, 2),
				new ValueTuple<eWeapon, string, int, int>(-2055158210, "una pistola Mauser", 2, 4)
			};
			gang.ExtraLongGuns = new ValueTuple<eWeapon, string, int>[]
			{
				new ValueTuple<eWeapon, string, int>(392538360, "una escopeta recortada", 5),
				new ValueTuple<eWeapon, string, int>(834124286, "una escopeta de corredera", 2)
			};
			gang.TroopModels = new PedHash[]
			{
				1478983280,
				-1208814201
			};
			gang.TroopAccuracy = 26;
			Gangs.SaintDenisMob = gang;
			gang = new Gang();
			gang.Id = "murfree";
			gang.EncounterRate = 0.6;
			gang.Jobs = new ValueTuple<string, string, int>[]
			{
				new ValueTuple<string, string, int>("secuestrar viajeros en el camino cerca de {t}", "el camino cerca de {t}", 3),
				new ValueTuple<string, string, int>("asaltar una granja en las colinas junto a {t}", "una granja junto a {t}", 4),
				new ValueTuple<string, string, int>("tender una trampa en el sendero de salida de {t}", "el sendero de salida de {t}", 3)
			};
			gang.JobTowns = new string[]
			{
				"Annesburg",
				"Van Horn"
			};
			Gang gang11 = gang;
			Dictionary<string, int> dictionary7 = new Dictionary<string, int>();
			dictionary7["skinners"] = -30;
			gang11.Relations = dictionary7;
			gang.Name = "Murfree Brood";
			gang.ModelTokens = new string[]
			{
				"uniinbred"
			};
			gang.LeaderName = "Old Esau Murfree";
			gang.LeaderBlurb = "Nadie ha visto al jefe de la Camada en años. Aun así, los Murfree le obedecen.";
			gang.FirstNames = NameBank.BackwoodsFirst;
			gang.LastNames = NameBank.MurfreeLast;
			gang.Titles = NameBank.MurfreeTitles;
			gang.Tiers = new ValueTuple<string, int, int>[]
			{
				new ValueTuple<string, int, int>("ANCIANOS", 4, 2),
				new ValueTuple<string, int, int>("CAZADORES", 2, 4),
				new ValueTuple<string, int, int>("CAMADA", 1, 8)
			};
			gang.Seed = new ValueTuple<int, int, PedHash[]>[]
			{
				new ValueTuple<int, int, PedHash[]>(4, 2, new PedHash[]
				{
					363815774
				}),
				new ValueTuple<int, int, PedHash[]>(3, 2, new PedHash[]
				{
					363815774
				}),
				new ValueTuple<int, int, PedHash[]>(2, 2, new PedHash[]
				{
					363815774
				}),
				new ValueTuple<int, int, PedHash[]>(1, 8, new PedHash[]
				{
					363815774
				})
			};
			gang.Accent = new ValueTuple<int, int, int>(124, 116, 72);
			gang.AmbushBase = 0.1;
			gang.MixedFootSoldiers = true;
			gang.HasCamps = true;
			gang.KeepsToItsOwn = true;
			gang.NoTowns = true;
			gang.OnlyDistricts = new HashSet<uint>
			{
				178647645U,
				1921384287U,
				2101155660U
			};
			gang.DistrictWeight = 2.0;
			gang.DayWeight = 0.5;
			gang.NightWeight = 1.8;
			gang.CombatMovement = new int?(2);
			gang.CombatAbility = new int?(1);
			gang.CombatRange = new int?(0);
			gang.AccuracyMax = 30;
			gang.Faiths = new string[]
			{
				"none"
			};
			gang.MemberWealth = new Wealth?(Wealth.Poor);
			gang.MemberTrades = new string[]
			{
				"Trampero",
				"Cazador"
			};
			gang.ExtraLongGuns = new ValueTuple<eWeapon, string, int>[]
			{
				new ValueTuple<eWeapon, string, int>(1845102363, "una escopeta de dos cañones", 6)
			};
			gang.TroopModels = new PedHash[]
			{
				363815774
			};
			gang.TroopAccuracy = 16;
			Gangs.MurfreeBrood = gang;
			Gangs.SkinnerBrothers = new Gang
			{
				Id = "skinners",
				EncounterRate = 0.6,
				Jobs = new ValueTuple<string, string, int>[]
				{
					new ValueTuple<string, string, int>("cazar hombres en los bosques pasado {t}", "los bosques pasado {t}", 3),
					new ValueTuple<string, string, int>("robar un carro en el camino de salida de {t}", "el camino de salida de {t}", 4),
					new ValueTuple<string, string, int>("dejar una advertencia en el sendero junto a {t}", "el sendero junto a {t}", 2)
				},
				JobTowns = new string[]
				{
					"Blackwater",
					"Strawberry"
				},
				Name = "Skinner Brothers",
				ModelTokens = new string[]
				{
					"unimountainmen",
					"skinnerbrother"
				},
				LeaderName = "Elias Skinner",
				LeaderBlurb = "El mayor de los hermanos. Todo hombre que cruza Tall Trees deja parte de sí mismo atrás.",
				FirstNames = NameBank.AmericanFirst,
				LastNames = NameBank.SkinnerLast,
				Titles = NameBank.SkinnerTitles,
				Tiers = new ValueTuple<string, int, int>[]
				{
					new ValueTuple<string, int, int>("HERMANOS", 4, 2),
					new ValueTuple<string, int, int>("TRAMPEROS", 2, 4),
					new ValueTuple<string, int, int>("DESOLLADORES", 1, 8)
				},
				Seed = new ValueTuple<int, int, PedHash[]>[]
				{
					new ValueTuple<int, int, PedHash[]>(4, 2, new PedHash[]
					{
						-1421951598
					}),
					new ValueTuple<int, int, PedHash[]>(3, 2, new PedHash[]
					{
						-1421951598
					}),
					new ValueTuple<int, int, PedHash[]>(2, 2, new PedHash[]
					{
						-1421951598
					}),
					new ValueTuple<int, int, PedHash[]>(1, 8, new PedHash[]
					{
						-1421951598
					})
				},
				Accent = new ValueTuple<int, int, int>(146, 112, 84),
				AmbushBase = 0.1,
				MixedFootSoldiers = true,
				HasCamps = true,
				KeepsToItsOwn = true,
				NoTowns = true,
				OnlyDistricts = new HashSet<uint>
				{
					1684533001U,
					1706859953U,
					210917480U,
					476637847U
				},
				DistrictWeights = new Dictionary<uint, double>
				{
					{
						1684533001U,
						2.0
					},
					{
						1706859953U,
						2.0
					},
					{
						210917480U,
						0.6
					},
					{
						476637847U,
						0.6
					}
				},
				DayWeight = 0.8,
				NightWeight = 1.4,
				CombatMovement = new int?(2),
				CombatAbility = new int?(2),
				CombatRange = new int?(2),
				AccuracyMin = 30,
				Faiths = new string[]
				{
					"none"
				},
				MemberWealth = new Wealth?(Wealth.Poor),
				MemberTrades = new string[]
				{
					"Trampero",
					"Cazador"
				},
				ExtraLongGuns = new ValueTuple<eWeapon, string, int>[]
				{
					new ValueTuple<eWeapon, string, int>(1999408598, "un fusil de cerrojo", 4),
					new ValueTuple<eWeapon, string, int>(-570967010, "un rifle Varmint", 2)
				},
				TroopModels = new PedHash[]
				{
					-1421951598
				},
				TroopLongGuns = true,
				TroopAccuracy = 30
			};
			gang = new Gang();
			gang.Id = "laramie";
			gang.Era = 1907;
			gang.EncounterRate = 0.5;
			gang.CrewExtra = 1;
			gang.Jobs = new ValueTuple<string, string, int>[]
			{
				new ValueTuple<string, string, int>("robar ganado de un rancho cerca de {t}", "un rancho cerca de {t}", 4),
				new ValueTuple<string, string, int>("cambiar las marcas del ganado robado a las afueras de {t}", "un corral a las afueras de {t}", 3),
				new ValueTuple<string, string, int>("atracar la diligencia en el camino de salida de {t}", "el camino de salida de {t}", 4),
				new ValueTuple<string, string, int>("robar una granja a las afueras de {t}", "una granja a las afueras de {t}", 3)
			};
			gang.JobTowns = new string[]
			{
				"Blackwater",
				"Armadillo"
			};
			Gang gang12 = gang;
			Dictionary<string, int> dictionary8 = new Dictionary<string, int>();
			dictionary8["skinners"] = -40;
			dictionary8["delobo"] = -45;
			dictionary8["army"] = -40;
			dictionary8["lemoyneraiders"] = -20;
			dictionary8["murfree"] = -20;
			dictionary8["klan"] = -10;
			gang12.Relations = dictionary8;
			gang.Name = "Laramie Gang";
			gang.ModelTokens = new string[]
			{
				"laramie",
				"uniranchers"
			};
			gang.LeaderName = "Wade Haskell";
			gang.LeaderBlurb = "Dirige a los chicos de Laramie: se quedará con el ganado de un hombre, y con su vida si protesta.";
			gang.LeaderModel = "cs_laramie";
			gang.FirstNames = NameBank.AmericanFirst;
			gang.LastNames = NameBank.AmericanLast;
			gang.Titles = NameBank.LaramieTitles;
			gang.Tiers = new ValueTuple<string, int, int>[]
			{
				new ValueTuple<string, int, int>("CAPATACES", 4, 2),
				new ValueTuple<string, int, int>("DOMADORES", 2, 4),
				new ValueTuple<string, int, int>("CUATREROS", 1, 8)
			};
			gang.Seed = new ValueTuple<int, int, PedHash[]>[]
			{
				new ValueTuple<int, int, PedHash[]>(4, 2, new PedHash[]
				{
					-1090280091
				}),
				new ValueTuple<int, int, PedHash[]>(3, 2, new PedHash[]
				{
					-1090280091
				}),
				new ValueTuple<int, int, PedHash[]>(2, 2, new PedHash[]
				{
					-1090280091
				}),
				new ValueTuple<int, int, PedHash[]>(1, 8, new PedHash[]
				{
					-1090280091
				})
			};
			gang.Accent = new ValueTuple<int, int, int>(184, 140, 80);
			gang.AmbushBase = 0.12;
			gang.MixedFootSoldiers = true;
			gang.HasCamps = true;
			gang.OnlyDistricts = new HashSet<uint>
			{
				892930832U,
				4241435510U,
				210917480U,
				476637847U,
				822658194U,
				3485840081U
			};
			gang.DistrictWeights = new Dictionary<uint, double>
			{
				{
					892930832U,
					2.0
				},
				{
					4241435510U,
					2.0
				},
				{
					210917480U,
					1.6
				},
				{
					476637847U,
					1.6
				},
				{
					822658194U,
					0.5
				},
				{
					3485840081U,
					0.5
				}
			};
			gang.DayWeight = 0.8;
			gang.NightWeight = 1.4;
			gang.CombatMovement = new int?(2);
			gang.CombatAbility = new int?(1);
			gang.CombatRange = new int?(1);
			gang.AccuracyMin = 20;
			gang.MemberWealth = new Wealth?(Wealth.Poor);
			gang.MemberTrades = new string[]
			{
				"Peón de rancho",
				"Boyero"
			};
			gang.ExtraSidearms = new ValueTuple<eWeapon, string, int, int>[]
			{
				new ValueTuple<eWeapon, string, int, int>(379542007, "un revólver Cattleman", 4, 1)
			};
			gang.ExtraLongGuns = new ValueTuple<eWeapon, string, int>[]
			{
				new ValueTuple<eWeapon, string, int>(-183018591, "una carabina", 3)
			};
			gang.TroopModels = new PedHash[]
			{
				-1090280091
			};
			gang.TroopAccuracy = 24;
			Gangs.LaramieGang = gang;
			gang = new Gang();
			gang.Id = "delobo";
			gang.Era = 1907;
			gang.EncounterRate = 0.6;
			gang.CrewExtra = 1;
			gang.Jobs = new ValueTuple<string, string, int>[]
			{
				new ValueTuple<string, string, int>("asaltar la diligencia en el camino de salida de {t}", "el camino de salida de {t}", 4),
				new ValueTuple<string, string, int>("asaltar un rancho a las afueras de {t}", "un rancho a las afueras de {t}", 4),
				new ValueTuple<string, string, int>("atracar un tren cerca de {t}", "las vías cerca de {t}", 4),
				new ValueTuple<string, string, int>("pasar armas por el río junto a {t}", "el río junto a {t}", 3)
			};
			gang.JobTowns = new string[]
			{
				"Tumbleweed",
				"Armadillo"
			};
			Gang gang13 = gang;
			Dictionary<string, int> dictionary9 = new Dictionary<string, int>();
			dictionary9["laramie"] = -45;
			dictionary9["army"] = -50;
			dictionary9["skinners"] = -20;
			gang13.Relations = dictionary9;
			gang.Name = "Del Lobo Gang";
			gang.ModelTokens = new string[]
			{
				"unibanditos"
			};
			gang.LeaderName = "Hector Sandoval";
			gang.LeaderBlurb = "Jefe de los Del Lobo. Cabalga a ambos lados del río y no rinde cuentas a ninguno.";
			gang.FirstNames = NameBank.MexicanFirst;
			gang.LastNames = NameBank.MexicanLast;
			gang.Titles = NameBank.DelLoboTitles;
			gang.Tiers = new ValueTuple<string, int, int>[]
			{
				new ValueTuple<string, int, int>("JEFES", 4, 2),
				new ValueTuple<string, int, int>("PISTOLEROS", 2, 4),
				new ValueTuple<string, int, int>("BANDIDOS", 1, 8)
			};
			gang.Seed = new ValueTuple<int, int, PedHash[]>[]
			{
				new ValueTuple<int, int, PedHash[]>(4, 2, new PedHash[]
				{
					424175505
				}),
				new ValueTuple<int, int, PedHash[]>(3, 2, new PedHash[]
				{
					424175505
				}),
				new ValueTuple<int, int, PedHash[]>(2, 2, new PedHash[]
				{
					424175505
				}),
				new ValueTuple<int, int, PedHash[]>(1, 8, new PedHash[]
				{
					424175505
				})
			};
			gang.Accent = new ValueTuple<int, int, int>(198, 92, 58);
			gang.AmbushBase = 0.14;
			gang.MixedFootSoldiers = true;
			gang.HasCamps = true;
			gang.OnlyDistricts = new HashSet<uint>
			{
				4186119282U,
				736160934U,
				2228727054U,
				3946348553U,
				2148975167U,
				779743808U,
				892930832U,
				4241435510U
			};
			gang.DistrictWeights = new Dictionary<uint, double>
			{
				{
					4186119282U,
					1.5
				},
				{
					736160934U,
					1.5
				},
				{
					2228727054U,
					2.0
				},
				{
					3946348553U,
					2.0
				},
				{
					2148975167U,
					2.0
				},
				{
					779743808U,
					2.0
				},
				{
					892930832U,
					0.4
				},
				{
					4241435510U,
					0.4
				}
			};
			gang.CombatMovement = new int?(2);
			gang.CombatAbility = new int?(1);
			gang.CombatRange = new int?(1);
			gang.AccuracyMin = 20;
			gang.Faiths = new string[]
			{
				"Católico"
			};
			gang.MemberWealth = new Wealth?(Wealth.Poor);
			gang.MemberTrades = new string[]
			{
				"Vaquero",
				"Ladrón de caballos"
			};
			gang.ExtraSidearms = new ValueTuple<eWeapon, string, int, int>[]
			{
				new ValueTuple<eWeapon, string, int, int>(2075992054, "un revólver Schofield", 3, 1)
			};
			gang.ExtraLongGuns = new ValueTuple<eWeapon, string, int>[]
			{
				new ValueTuple<eWeapon, string, int>(-183018591, "una carabina", 4)
			};
			gang.TroopModels = new PedHash[]
			{
				424175505
			};
			gang.TroopAccuracy = 24;
			Gangs.DelLobos = gang;
			gang = new Gang();
			gang.Id = "nightfolk";
			gang.Era = 1907;
			gang.EncounterRate = 0.4;
			gang.Jobs = new ValueTuple<string, string, int>[]
			{
				new ValueTuple<string, string, int>("llevarse a un viajero del camino del pantano cerca de {t}", "el camino del pantano cerca de {t}", 3),
				new ValueTuple<string, string, int>("dejar un aviso colgado de los árboles junto a {t}", "los árboles junto a {t}", 2),
				new ValueTuple<string, string, int>("colgar faroles en los cipreses del camino de salida de {t}", "el camino de salida de {t}", 2),
				new ValueTuple<string, string, int>("desenterrar una tumba reciente en el cementerio de {t}", "el cementerio de {t}", 2),
				new ValueTuple<string, string, int>("robar una barca de pesca en el agua cerca de {t}", "el agua cerca de {t}", 2),
				new ValueTuple<string, string, int>("dejar algo en el umbral de una puerta en {t}", "un umbral en {t}", 1)
			};
			gang.JobTowns = new string[]
			{
				"Saint Denis"
			};
			Gang gang14 = gang;
			Dictionary<string, int> dictionary10 = new Dictionary<string, int>();
			dictionary10["lemoyneraiders"] = -20;
			dictionary10["army"] = -30;
			dictionary10["bronte"] = -35;
			gang14.Relations = dictionary10;
			gang.TurfWars = false;
			gang.Name = "Night Folk";
			gang.ModelTokens = new string[]
			{
				"uniswamp"
			};
			gang.LeaderName = "Silas Thibodeaux";
			gang.LeaderBlurb = "Nadie lo ha visto a la luz del día. El pantano hace lo que él dice de todos modos.";
			gang.FirstNames = NameBank.CajunFirst;
			gang.LastNames = NameBank.CajunLast;
			gang.Titles = NameBank.NightFolkTitles;
			gang.Tiers = new ValueTuple<string, int, int>[]
			{
				new ValueTuple<string, int, int>("ANCIANOS", 4, 1),
				new ValueTuple<string, int, int>("ACECHADORES", 2, 3),
				new ValueTuple<string, int, int>("GENTE DEL PANTANO", 1, 8)
			};
			gang.Seed = new ValueTuple<int, int, PedHash[]>[]
			{
				new ValueTuple<int, int, PedHash[]>(4, 1, new PedHash[]
				{
					-535404896
				}),
				new ValueTuple<int, int, PedHash[]>(2, 3, new PedHash[]
				{
					-535404896
				}),
				new ValueTuple<int, int, PedHash[]>(1, 8, new PedHash[]
				{
					-535404896
				})
			};
			gang.Accent = new ValueTuple<int, int, int>(104, 128, 108);
			gang.AmbushBase = 0.08;
			gang.MixedFootSoldiers = true;
			gang.HasCamps = true;
			gang.KeepsToItsOwn = true;
			gang.NoTowns = true;
			gang.Feuds = false;
			gang.GangLifeEvents = false;
			gang.OwnLifeOnly = true;
			gang.OnlyDistricts = new HashSet<uint>
			{
				2025841068U,
				3374368891U
			};
			gang.DistrictWeight = 1.5;
			gang.DayWeight = 0.2;
			gang.NightWeight = 2.0;
			gang.CombatMovement = new int?(2);
			gang.CombatAbility = new int?(0);
			gang.CombatRange = new int?(0);
			gang.AccuracyMax = 20;
			gang.Faiths = new string[]
			{
				"las viejas costumbres"
			};
			gang.MemberWealth = new Wealth?(Wealth.Poor);
			gang.MemberTrades = new string[]
			{
				"Trampero",
				"Pescador"
			};
			gang.TroopModels = new PedHash[]
			{
				-535404896
			};
			gang.TroopAccuracy = 14;
			Gangs.NightFolk = gang;
			Gangs.BuiltIn = new Gang[]
			{
				Gangs.ODriscolls,
				Gangs.LemoyneRaiders,
				Gangs.Army,
				Gangs.Klan,
				Gangs.Wapiti,
				Gangs.SaintDenisMob,
				Gangs.MurfreeBrood,
				Gangs.SkinnerBrothers,
				Gangs.LaramieGang,
				Gangs.DelLobos,
				Gangs.NightFolk
			};
			Gangs._defined = Gangs.BuiltIn;
			Gangs.Era = 1899;
			Gangs.All = (from g in Gangs.BuiltIn
			where g.Era == 0 || g.Era == 1899
			select g).ToArray<Gang>();
			Gangs._byModel = new Dictionary<uint, Gang>();
			Gangs.VanDerLinde = new Gang
			{
				Id = "vdl",
				Name = "Van der Linde Gang",
				LeaderName = "Dutch van der Linde",
				StoryOnly = true,
				Feuds = false,
				LeaderBlurb = "La banda de Dutch, huyendo desde lo de Blackwater",
				ModelTokens = new string[0],
				FirstNames = new string[0],
				LastNames = new string[0],
				Titles = new string[0],
				Seed = new ValueTuple<int, int, PedHash[]>[0],
				Tiers = new ValueTuple<string, int, int>[]
				{
					new ValueTuple<string, int, int>("LÍDERES", 5, 1),
					new ValueTuple<string, int, int>("VETERANOS", 4, 2),
					new ValueTuple<string, int, int>("PISTOLEROS", 2, 9),
					new ValueTuple<string, int, int>("PEONES DEL CAMPAMENTO", 1, 12)
				},
				Accent = new ValueTuple<int, int, int>(170, 140, 110)
			};
		}

		// Token: 0x04000783 RID: 1923
		public static readonly Gang ODriscolls;

		// Token: 0x04000784 RID: 1924
		public static readonly Gang LemoyneRaiders;

		// Token: 0x04000785 RID: 1925
		public static readonly Gang Army;

		// Token: 0x04000786 RID: 1926
		public static readonly Gang Klan;

		// Token: 0x04000787 RID: 1927
		public static readonly Gang Wapiti;

		// Token: 0x04000788 RID: 1928
		public static readonly Gang SaintDenisMob;

		// Token: 0x04000789 RID: 1929
		public static readonly Gang MurfreeBrood;

		// Token: 0x0400078A RID: 1930
		public static readonly Gang SkinnerBrothers;

		// Token: 0x0400078B RID: 1931
		public static readonly Gang LaramieGang;

		// Token: 0x0400078C RID: 1932
		public static readonly Gang DelLobos;

		// Token: 0x0400078D RID: 1933
		public static readonly Gang NightFolk;

		// Token: 0x0400078E RID: 1934
		private static readonly Gang[] BuiltIn;

		// Token: 0x0400078F RID: 1935
		private static Gang[] _defined;

		// Token: 0x04000793 RID: 1939
		private static JObject[] _builtInExport;

		// Token: 0x04000794 RID: 1940
		private static readonly Dictionary<uint, Gang> _byModel;

		// Token: 0x04000795 RID: 1941
		private static Dictionary<string, Gang> _byId;

		// Token: 0x04000796 RID: 1942
		public static readonly Gang VanDerLinde;

		// Token: 0x020003EA RID: 1002
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001CE3 RID: 7395
			public static Func<Gang, JObject> <0>__Export;

			// Token: 0x04001CE4 RID: 7396
			public static Func<Gang, bool> <1>__InEra;
		}
	}
}
