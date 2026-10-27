using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x0200005F RID: 95
	public static class Personalities
	{
		// Token: 0x060011A3 RID: 4515 RVA: 0x00111530 File Offset: 0x0010F730
		public static Personality PickWeighted(Random rng, bool gang, Func<Personality, double> mult)
		{
			ValueTuple<Personality, int>[] array = gang ? Personalities.GangOdds : Personalities.LonerOdds;
			double num = 0.0;
			foreach (ValueTuple<Personality, int> valueTuple in array)
			{
				num += (double)valueTuple.Item2 * Math.Max(0.0, mult(valueTuple.Item1));
			}
			if (num <= 0.0)
			{
				return Personalities.Pick(rng, gang);
			}
			double num2 = rng.NextDouble() * num;
			foreach (ValueTuple<Personality, int> valueTuple2 in array)
			{
				num2 -= (double)valueTuple2.Item2 * Math.Max(0.0, mult(valueTuple2.Item1));
				if (num2 <= 0.0)
				{
					return valueTuple2.Item1;
				}
			}
			return array[array.Length - 1].Item1;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x00111624 File Offset: 0x0010F824
		public static Personality Pick(Random rng, bool gang)
		{
			ValueTuple<Personality, int>[] array = gang ? Personalities.GangOdds : Personalities.LonerOdds;
			int num = 0;
			foreach (ValueTuple<Personality, int> valueTuple in array)
			{
				num += valueTuple.Item2;
			}
			int num2 = rng.Next(num);
			foreach (ValueTuple<Personality, int> valueTuple2 in array)
			{
				num2 -= valueTuple2.Item2;
				if (num2 < 0)
				{
					return valueTuple2.Item1;
				}
			}
			return array[0].Item1;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x001116B3 File Offset: 0x0010F8B3
		private static bool Humiliation(GrudgeOrigin o)
		{
			return o == GrudgeOrigin.Insulted || o == GrudgeOrigin.Beaten || o == GrudgeOrigin.KnockedOut || o == GrudgeOrigin.Kidnapped;
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x001116C8 File Offset: 0x0010F8C8
		public static int StartShift(Personality p, GrudgeOrigin o)
		{
			switch (p)
			{
			case Personality.Hothead:
				return 12;
			case Personality.Coward:
				return -25;
			case Personality.Proud:
				if (!Personalities.Humiliation(o))
				{
					return 0;
				}
				return 15;
			case Personality.Cold:
				return -8;
			case Personality.Greedy:
				if (o != GrudgeOrigin.Robbed && o != GrudgeOrigin.HorseStolen && o != GrudgeOrigin.WagonStolen)
				{
					return -12;
				}
				return 20;
			case Personality.Drifter:
				return -20;
			}
			return 0;
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x00111727 File Offset: 0x0010F927
		public static int InitialHate(Personality p, GrudgeOrigin o, Random rng)
		{
			return Math.Max(10, Math.Min(100, Personalities.StartingHate(p, o) + rng.Next(-4, 5)));
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x00111748 File Offset: 0x0010F948
		public static int StartingHate(Personality p, GrudgeOrigin o)
		{
			float num = (float)Personalities.StartShift(p, o) * Balance.LonerShiftScale;
			if (NemesisMod.FistOrigin(o))
			{
				num /= 2f;
			}
			return Math.Max(10, Math.Min(100, Hate.Initial(o) + (int)Math.Round((double)num)));
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x00111791 File Offset: 0x0010F991
		public static float GainMult(Personality p)
		{
			return Tempers.Of(new Personality?(p)).HateGain;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x001117A3 File Offset: 0x0010F9A3
		public static int CoolPer10Min(Personality p)
		{
			if (p == Personality.Coward)
			{
				return 1;
			}
			if (p != Personality.Drifter)
			{
				return 0;
			}
			return 2;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x001117B4 File Offset: 0x0010F9B4
		public static double AmbushWeight(Personality p)
		{
			switch (p)
			{
			case Personality.Hothead:
				return 1.5;
			case Personality.Coward:
				return 0.5;
			case Personality.Proud:
				return 1.2;
			case Personality.Greedy:
				return 0.8;
			case Personality.Drifter:
				return 0.6;
			}
			return 1.0;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x00111820 File Offset: 0x0010FA20
		[return: TupleElementNames(new string[]
		{
			"bias",
			"hit",
			"threat",
			"round"
		})]
		public static ValueTuple<int, int, int, int> Interrogation(Personality? p)
		{
			if (p != null)
			{
				switch (p.GetValueOrDefault())
				{
				case Personality.Hothead:
					return new ValueTuple<int, int, int, int>(-5, 3, 12, 15);
				case Personality.Coward:
					return new ValueTuple<int, int, int, int>(25, 12, 10, 15);
				case Personality.Proud:
					return new ValueTuple<int, int, int, int>(-10, -4, -2, 25);
				case Personality.Cold:
					return new ValueTuple<int, int, int, int>(-15, 6, 4, 12);
				case Personality.Greedy:
					return new ValueTuple<int, int, int, int>(5, 8, 8, 15);
				case Personality.Loyal:
					return new ValueTuple<int, int, int, int>(-20, 6, 6, 10);
				case Personality.Drifter:
					return new ValueTuple<int, int, int, int>(10, 8, 8, 15);
				}
			}
			return new ValueTuple<int, int, int, int>(0, 8, 8, 15);
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x001118C8 File Offset: 0x0010FAC8
		public static string Describe(Personality p)
		{
			switch (p)
			{
			case Personality.Hothead:
				return "Impulsivo - rápido para enfurecerse, lento para olvidar";
			case Personality.Coward:
				return "Cobarde - se esconde más de lo que caza; se quiebra fácil";
			case Personality.Proud:
				return "Orgulloso - la humillación le duele más; no hablará por intimidación";
			case Personality.Cold:
				return "Frío - firme y calculador; nunca se enfría";
			case Personality.Greedy:
				return "Codicioso - el dinero lo es todo para él";
			case Personality.Loyal:
				return "Leal - preferiría morir antes que delatar a su jefe";
			case Personality.Drifter:
				return "Errante - su rencor se apaga; puede que lo deje pasar";
			default:
				return p.ToString();
			}
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x00111930 File Offset: 0x0010FB30
		// Note: this type is marked as 'beforefieldinit'.
		static Personalities()
		{
			Dictionary<Personality, string[]> dictionary = new Dictionary<Personality, string[]>();
			dictionary[Personality.Hothead] = new string[]
			{
				"Hit me again. Go on, I dare you!",
				"I'll gut you for this, Morgan!",
				"That all you got?"
			};
			dictionary[Personality.Coward] = new string[]
			{
				"Please... I can't, they'll kill me!",
				"Don't hurt me, mister, please!",
				"I- I don't know nothin', I swear!"
			};
			dictionary[Personality.Proud] = new string[]
			{
				"You think fists'll make me crawl? Not me.",
				"I don't bow to the likes of you.",
				"Beat me all you want. I'm better than you."
			};
			dictionary[Personality.Cold] = new string[]
			{
				"...",
				"You're wasting your time.",
				"I've had worse from better men."
			};
			dictionary[Personality.Greedy] = new string[]
			{
				"What's in it for me, huh?",
				"Information ain't free, friend.",
				"A man's gotta eat. Talking don't feed me."
			};
			dictionary[Personality.Loyal] = new string[]
			{
				"I ain't no rat. Never.",
				"The boss is family. You ain't.",
				"Kill me then. I ain't talking."
			};
			dictionary[Personality.Drifter] = new string[]
			{
				"Ain't my fight, mister.",
				"Why should I care?",
				"Leave me be."
			};
			Personalities.Refusals = dictionary;
		}

		// Token: 0x04000BB9 RID: 3001
		[TupleElementNames(new string[]
		{
			"p",
			"w"
		})]
		private static readonly ValueTuple<Personality, int>[] LonerOdds = new ValueTuple<Personality, int>[]
		{
			new ValueTuple<Personality, int>(Personality.Hothead, 18),
			new ValueTuple<Personality, int>(Personality.Coward, 18),
			new ValueTuple<Personality, int>(Personality.Proud, 16),
			new ValueTuple<Personality, int>(Personality.Cold, 14),
			new ValueTuple<Personality, int>(Personality.Greedy, 14),
			new ValueTuple<Personality, int>(Personality.Drifter, 20)
		};

		// Token: 0x04000BBA RID: 3002
		[TupleElementNames(new string[]
		{
			"p",
			"w"
		})]
		private static readonly ValueTuple<Personality, int>[] GangOdds = new ValueTuple<Personality, int>[]
		{
			new ValueTuple<Personality, int>(Personality.Hothead, 22),
			new ValueTuple<Personality, int>(Personality.Coward, 14),
			new ValueTuple<Personality, int>(Personality.Proud, 14),
			new ValueTuple<Personality, int>(Personality.Cold, 16),
			new ValueTuple<Personality, int>(Personality.Greedy, 14),
			new ValueTuple<Personality, int>(Personality.Loyal, 20)
		};

		// Token: 0x04000BBB RID: 3003
		public static readonly Dictionary<Personality, string[]> Refusals;
	}
}
