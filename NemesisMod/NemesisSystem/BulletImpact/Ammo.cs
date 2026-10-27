using System;
using System.Runtime.CompilerServices;
using RDR2;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x02000089 RID: 137
	public static class Ammo
	{
		// Token: 0x060013CE RID: 5070 RVA: 0x0013C534 File Offset: 0x0013A734
		public static AmmoKind FromHash(uint hash)
		{
			if (hash == 0U)
			{
				return AmmoKind.Regular;
			}
			string text;
			if (!Enum.IsDefined(typeof(eAmmoType), hash))
			{
				text = "";
			}
			else
			{
				eAmmoType eAmmoType = hash;
				text = eAmmoType.ToString();
			}
			string text2 = text;
			if (text2 == "")
			{
				return AmmoKind.Regular;
			}
			if (text2.Contains("Explosive") || text2 == "ArrowDynamite")
			{
				return AmmoKind.Explosive;
			}
			if (text2.Contains("Incendiary"))
			{
				return AmmoKind.Incendiary;
			}
			if (text2.Contains("SplitPoint"))
			{
				return AmmoKind.SplitPoint;
			}
			if (text2.Contains("HighVelocity"))
			{
				return AmmoKind.HighVelocity;
			}
			if (text2.Contains("Express"))
			{
				return AmmoKind.Express;
			}
			if (text2.Contains("Slug"))
			{
				return AmmoKind.Slug;
			}
			if (text2 == "ArrowFire")
			{
				return AmmoKind.FireArrow;
			}
			if (text2 == "ArrowPoison")
			{
				return AmmoKind.PoisonArrow;
			}
			if (text2 == "ArrowSmallGame")
			{
				return AmmoKind.SmallGameArrow;
			}
			if (text2 == "ArrowImproved")
			{
				return AmmoKind.ImprovedArrow;
			}
			return AmmoKind.Regular;
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x0013C62C File Offset: 0x0013A82C
		[return: TupleElementNames(new string[]
		{
			"budget",
			"resist",
			"cord"
		})]
		public static ValueTuple<float, float, float> Mods(AmmoKind a)
		{
			switch (a)
			{
			case AmmoKind.SplitPoint:
				return new ValueTuple<float, float, float>(0.75f, 1.2f, 1f);
			case AmmoKind.Express:
				return new ValueTuple<float, float, float>(1.15f, 0.9f, 0.9f);
			case AmmoKind.HighVelocity:
				return new ValueTuple<float, float, float>(1.4f, 0.7f, 0.6f);
			case AmmoKind.Slug:
				return new ValueTuple<float, float, float>(5f, 0.9f, 0.7f);
			case AmmoKind.SmallGameArrow:
				return new ValueTuple<float, float, float>(0.5f, 1.3f, 1.2f);
			case AmmoKind.ImprovedArrow:
				return new ValueTuple<float, float, float>(1.3f, 0.9f, 0.9f);
			}
			return new ValueTuple<float, float, float>(1f, 1f, 1f);
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x0013C700 File Offset: 0x0013A900
		public static string Label(AmmoKind a)
		{
			switch (a)
			{
			case AmmoKind.SplitPoint:
				return "split point";
			case AmmoKind.Express:
				return "express";
			case AmmoKind.HighVelocity:
				return "high velocity";
			case AmmoKind.Explosive:
				return "explosive";
			case AmmoKind.Incendiary:
				return "incendiary";
			case AmmoKind.Slug:
				return "slug";
			case AmmoKind.FireArrow:
				return "fire arrow";
			case AmmoKind.PoisonArrow:
				return "poison arrow";
			case AmmoKind.SmallGameArrow:
				return "small game arrow";
			case AmmoKind.ImprovedArrow:
				return "improved arrow";
			default:
				return null;
			}
		}
	}
}
