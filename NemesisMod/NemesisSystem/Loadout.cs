using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using RDR2;
using RDR2.Native;

namespace NemesisSystem
{
	// Token: 0x02000026 RID: 38
	internal static class Loadout
	{
		// Token: 0x06000AE6 RID: 2790 RVA: 0x000FEC40 File Offset: 0x000FCE40
		private static int Seed(NemesisRecord r)
		{
			int num = 17;
			string text;
			if ((text = r.Id) == null)
			{
				text = (r.Name ?? "");
			}
			foreach (char c in text)
			{
				num = num * 31 + (int)c;
			}
			return num;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000FEC8B File Offset: 0x000FCE8B
		public static int Luck(NemesisRecord r)
		{
			return new Random(Loadout.Seed(r)).Next(-6, 7);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000FECA0 File Offset: 0x000FCEA0
		public static int TemperShift(NemesisRecord r)
		{
			Personality? personality = r.Personality;
			if (personality != null)
			{
				switch (personality.GetValueOrDefault())
				{
				case Personality.Hothead:
					return -8;
				case Personality.Coward:
					return -6;
				case Personality.Proud:
					return 6;
				case Personality.Cold:
					return -4;
				case Personality.Drifter:
					return 4;
				}
			}
			return 0;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000FECF7 File Offset: 0x000FCEF7
		public static int AggressionShift(NemesisRecord r)
		{
			if (!r.TraitsRolled)
			{
				return 0;
			}
			return (50 - r.Aggression) / 6;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000FED0E File Offset: 0x000FCF0E
		public static int Shift(NemesisRecord r)
		{
			return Loadout.TemperShift(r) + Loadout.AggressionShift(r) + Loadout.Luck(r);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000FED24 File Offset: 0x000FCF24
		public static int BladeAt(NemesisRecord r)
		{
			return Math.Max(20, Math.Min(60, Hate.VengefulAt + Loadout.Shift(r)));
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000FED40 File Offset: 0x000FCF40
		public static int GunAt(NemesisRecord r)
		{
			return Math.Max(Loadout.BladeAt(r) + 12, Math.Min(88, Hate.MurderousAt + Loadout.Shift(r)));
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000FED63 File Offset: 0x000FCF63
		public static Loadout.Band BandOf(NemesisRecord r)
		{
			if (Gangs.Orders(r) || r.Hate >= Loadout.GunAt(r))
			{
				return Loadout.Band.Gun;
			}
			if (r.Hate < Loadout.BladeAt(r))
			{
				return Loadout.Band.Fists;
			}
			return Loadout.Band.Blade;
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x000FED90 File Offset: 0x000FCF90
		public static string ShiftWhy(NemesisRecord r)
		{
			List<string> list = new List<string>();
			int num = Loadout.TemperShift(r);
			int num2 = Loadout.AggressionShift(r);
			int num3 = Loadout.Luck(r);
			if (num != 0)
			{
				Personality? personality;
				list.Add(string.Format("{0} {1:+0;-0}", (r.Personality != null) ? personality.GetValueOrDefault().ToString().ToLowerInvariant() : null, num));
			}
			if (num2 != 0)
			{
				list.Add(string.Format("{0} {1:+0;-0}", Traits.Aggr(r.Aggression).ToLowerInvariant(), num2));
			}
			if (num3 != 0)
			{
				list.Add(string.Format("luck {0:+0;-0}", num3));
			}
			if (list.Count != 0)
			{
				return string.Join(", ", list);
			}
			return "no shift";
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000FEE5C File Offset: 0x000FD05C
		private static T Pick<T>(Random rng, [TupleElementNames(new string[]
		{
			"item",
			"w"
		})] IList<ValueTuple<T, int>> opts)
		{
			int val = opts.Sum(([TupleElementNames(new string[]
			{
				"item",
				"w"
			})] ValueTuple<T, int> o) => Math.Max(0, o.Item2));
			int num = rng.Next(Math.Max(1, val));
			foreach (ValueTuple<T, int> valueTuple in opts)
			{
				T item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				num -= Math.Max(0, item2);
				if (num < 0)
				{
					return item;
				}
			}
			return opts[opts.Count - 1].Item1;
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000FEF08 File Offset: 0x000FD108
		private static bool TradeHas(NemesisRecord r, params string[] words)
		{
			return r.Trade != null && words.Any((string w) => r.Trade.IndexOf(w, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x000FEF44 File Offset: 0x000FD144
		public static Loadout.Kit For(NemesisRecord r)
		{
			Loadout.Band band = Loadout.BandOf(r);
			Gang gang = Gangs.Of(r);
			if (gang != null && gang.TribalKit)
			{
				return new Loadout.Kit
				{
					Band = ((band == Loadout.Band.Gun) ? Loadout.Band.Gun : Loadout.Band.Blade),
					Hand = new eWeapon?(gang.KitBlade),
					HandName = gang.KitBladeName,
					Long = new eWeapon?(gang.KitBow),
					LongName = gang.KitBowName,
					PreferLong = (band == Loadout.Band.Gun)
				};
			}
			Loadout.Kit kit = new Loadout.Kit
			{
				Band = band
			};
			if (band == Loadout.Band.Fists)
			{
				return kit;
			}
			Random rng = new Random((int)(Loadout.Seed(r) + band * (Loadout.Band)7919));
			if (band == Loadout.Band.Blade)
			{
				List<ValueTuple<ValueTuple<eWeapon, string>, int>> list = new List<ValueTuple<ValueTuple<eWeapon, string>, int>>();
				list.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(-618550132, "un cuchillo de caza"), 5));
				list.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(-1448818329, "un cuchillo de comerciante"), 2));
				List<ValueTuple<ValueTuple<eWeapon, string>, int>> list2 = list;
				ValueTuple<eWeapon, string> item = new ValueTuple<eWeapon, string>(-631972525, "un viejo cuchillo militar");
				Gang gang2 = Gangs.Of(r);
				list2.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(item, (gang2 != null) ? gang2.OldArmyKnifeWeight : 1));
				list.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(680856689, "un machete"), (r.Aggression >= 65) ? 4 : 2));
				list.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(165751297, "un hacha de mano"), Loadout.TradeHas(r, new string[]
				{
					"farm",
					"ranch",
					"hunt",
					"wood",
					"trapper",
					"homestead"
				}) ? 5 : 2));
				list.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(-281894307, "una cuchilla de carnicero"), Loadout.TradeHas(r, new string[]
				{
					"butcher",
					"cook"
				}) ? 6 : 1));
				List<ValueTuple<ValueTuple<eWeapon, string>, int>> opts = list;
				ValueTuple<eWeapon, string> valueTuple = Loadout.Pick<ValueTuple<eWeapon, string>>(rng, opts);
				eWeapon item2 = valueTuple.Item1;
				string item3 = valueTuple.Item2;
				kit.Hand = new eWeapon?(item2);
				kit.HandName = item3;
				return kit;
			}
			Gang gang3 = Gangs.Of(r);
			if (gang3 != null && gang3.UniformKit)
			{
				kit.Hand = new eWeapon?(gang3.UniformSide);
				kit.HandName = gang3.UniformSideName;
				kit.Long = new eWeapon?(gang3.UniformLong);
				kit.LongName = gang3.UniformLongName;
				kit.PreferLong = true;
				return kit;
			}
			List<ValueTuple<ValueTuple<eWeapon, string>, int>> list3 = new List<ValueTuple<ValueTuple<eWeapon, string>, int>>();
			switch (r.TraitsRolled ? r.Wealth : Wealth.Working)
			{
			case Wealth.Poor:
				list3.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(379542007, "un Cattleman desgastado"), 5));
				list3.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(127400949, "un revólver de doble acción"), 1));
				goto IL_3ED;
			case Wealth.Comfortable:
				list3.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(2075992054, "un revólver Schofield"), 3));
				list3.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(127400949, "un revólver de doble acción"), 2));
				list3.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(379542007, "un revólver Cattleman"), 1));
				goto IL_3ED;
			case Wealth.Rich:
				list3.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(34411519, "una pistola Volcanic"), 2));
				list3.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(-2055158210, "una pistola Mauser"), 1));
				list3.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(2075992054, "un revólver Schofield"), 2));
				list3.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(-2082646505, "un revólver de doble acción de tahúr"), 1));
				goto IL_3ED;
			}
			list3.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(379542007, "un revólver Cattleman"), 4));
			list3.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(127400949, "un revólver de doble acción"), 2));
			list3.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(2075992054, "un revólver Schofield"), 1));
			IL_3ED:
			if (gang3 != null)
			{
				foreach (ValueTuple<eWeapon, string, int, int> valueTuple2 in gang3.ExtraSidearms)
				{
					if (r.Rank >= valueTuple2.Item4)
					{
						list3.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(valueTuple2.Item1, valueTuple2.Item2), valueTuple2.Item3));
					}
				}
			}
			Loadout.Kit kit2 = kit;
			Loadout.Kit kit3 = kit;
			ValueTuple<eWeapon, string> valueTuple3 = Loadout.Pick<ValueTuple<eWeapon, string>>(rng, list3);
			eWeapon? eWeapon = new eWeapon?(valueTuple3.Item1);
			kit2.Hand = eWeapon;
			kit3.HandName = valueTuple3.Item2;
			bool flag = r.Personality.GetValueOrDefault() == Personality.Coward || r.Personality.GetValueOrDefault() == Personality.Cold;
			if (r.Hate >= Loadout.GunAt(r) + 15 || r.Rank >= 3 || flag)
			{
				List<ValueTuple<ValueTuple<eWeapon, string>, int>> list4 = new List<ValueTuple<ValueTuple<eWeapon, string>, int>>
				{
					new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(-183018591, "una carabina de repetición"), 3),
					new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(-1471716628, "un fusil de repetición Lancaster"), 2),
					new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(1845102363, "una escopeta de dos cañones"), (r.TraitsRolled && r.Wealth == Wealth.Poor) ? 5 : 2),
					new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(834124286, "una escopeta de corredera"), 1),
					new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(-570967010, "un rifle Varmint"), Loadout.TradeHas(r, new string[]
					{
						"hunt",
						"trapper"
					}) ? 4 : 1),
					new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(1999408598, "un fusil de cerrojo"), (r.Personality.GetValueOrDefault() == Personality.Cold) ? 3 : 1)
				};
				if (gang3 != null)
				{
					foreach (ValueTuple<eWeapon, string, int> valueTuple4 in gang3.ExtraLongGuns)
					{
						list4.Add(new ValueTuple<ValueTuple<eWeapon, string>, int>(new ValueTuple<eWeapon, string>(valueTuple4.Item1, valueTuple4.Item2), valueTuple4.Item3));
					}
				}
				kit2 = kit;
				Loadout.Kit kit4 = kit;
				valueTuple3 = Loadout.Pick<ValueTuple<eWeapon, string>>(rng, list4);
				eWeapon = new eWeapon?(valueTuple3.Item1);
				kit2.Long = eWeapon;
				kit4.LongName = valueTuple3.Item2;
				kit.PreferLong = flag;
			}
			return kit;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000FF5A8 File Offset: 0x000FD7A8
		public static string Describe(NemesisRecord r)
		{
			Loadout.Kit kit = Loadout.For(r);
			if (kit.Band == Loadout.Band.Fists)
			{
				return "viene con los puños";
			}
			if (kit.Band == Loadout.Band.Blade)
			{
				return "lleva " + kit.HandName;
			}
			if (kit.Long == null)
			{
				return "lleva " + kit.HandName;
			}
			return "lleva " + kit.HandName + " y " + kit.LongName;
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000FF61D File Offset: 0x000FD81D
		private static bool IsKnife(eWeapon w)
		{
			return w.ToString().StartsWith("MeleeKnife");
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x000FF638 File Offset: 0x000FD838
		private static bool IsHandToHand(eWeapon w)
		{
			string text = w.ToString();
			return text.StartsWith("Melee") || text.StartsWith("Thrown");
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000FF66D File Offset: 0x000FD86D
		public static bool IsBow(eWeapon w)
		{
			return w.ToString().StartsWith("Bow");
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000FF688 File Offset: 0x000FD888
		public static void GiveArrows(Ped ped, int n = 40)
		{
			try
			{
				WEAPON._ADD_AMMO_TO_PED_BY_TYPE(ped.Handle, 954660191U, n, 752097756U);
			}
			catch
			{
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000FF6C0 File Offset: 0x000FD8C0
		public static void Arm(Ped ped, NemesisRecord r, bool drawNow)
		{
			Loadout.Kit kit = Loadout.For(r);
			if (kit.Hand != null)
			{
				bool flag = Loadout.IsHandToHand(kit.Hand.Value);
				eWeaponAttachPoint eWeaponAttachPoint = flag ? (Loadout.IsKnife(kit.Hand.Value) ? 4 : 13) : 2;
				bool flag2 = drawNow && (!kit.PreferLong || kit.Long == null);
				ped.GiveWeapon(kit.Hand.Value, flag ? 1 : 100, eWeaponAttachPoint, flag2, !flag2, false);
			}
			if (kit.Long != null)
			{
				bool flag3 = drawNow && kit.PreferLong;
				bool flag4 = Loadout.IsBow(kit.Long.Value);
				ped.GiveWeapon(kit.Long.Value, 60, flag4 ? 7 : 9, flag3, !flag3, false);
				if (flag4)
				{
					Loadout.GiveArrows(ped, 40);
				}
			}
		}

		// Token: 0x02000403 RID: 1027
		public enum Band
		{
			// Token: 0x04001D26 RID: 7462
			Fists,
			// Token: 0x04001D27 RID: 7463
			Blade,
			// Token: 0x04001D28 RID: 7464
			Gun
		}

		// Token: 0x02000404 RID: 1028
		public sealed class Kit
		{
			// Token: 0x04001D29 RID: 7465
			public Loadout.Band Band;

			// Token: 0x04001D2A RID: 7466
			public eWeapon? Hand;

			// Token: 0x04001D2B RID: 7467
			public string HandName;

			// Token: 0x04001D2C RID: 7468
			public eWeapon? Long;

			// Token: 0x04001D2D RID: 7469
			public string LongName;

			// Token: 0x04001D2E RID: 7470
			public bool PreferLong;
		}
	}
}
