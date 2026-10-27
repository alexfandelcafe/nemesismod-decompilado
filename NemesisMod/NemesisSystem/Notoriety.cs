using System;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x0200005D RID: 93
	public static class Notoriety
	{
		// Token: 0x0600119E RID: 4510 RVA: 0x00111394 File Offset: 0x0010F594
		public static int Level(int value)
		{
			int result = 0;
			for (int i = 0; i < Notoriety.Thresholds.Length; i++)
			{
				if (value >= Notoriety.Thresholds[i])
				{
					result = i;
				}
			}
			return result;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x001113C2 File Offset: 0x0010F5C2
		public static string LevelName(int value)
		{
			return Notoriety.Names[Notoriety.Level(value)];
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x001113D0 File Offset: 0x0010F5D0
		public static double Ambush(int value)
		{
			return Notoriety.AmbushMult[Notoriety.Level(value)];
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x001113DE File Offset: 0x0010F5DE
		public static int ForKill(int rank)
		{
			if (rank <= 0)
			{
				return 1;
			}
			if (rank == 1)
			{
				return 1;
			}
			if (rank <= 3)
			{
				return 2;
			}
			if (rank != 4)
			{
				return 6;
			}
			return 4;
		}

		// Token: 0x04000BAB RID: 2987
		private static readonly int[] Thresholds = new int[]
		{
			0,
			10,
			25,
			50,
			75
		};

		// Token: 0x04000BAC RID: 2988
		private static readonly string[] Names = new string[]
		{
			"Desconocido",
			"Visto",
			"Buscado",
			"Perseguido",
			"Deuda de sangre"
		};

		// Token: 0x04000BAD RID: 2989
		private static readonly double[] AmbushMult = new double[]
		{
			0.4,
			1.0,
			1.6,
			2.4,
			3.5
		};

		// Token: 0x04000BAE RID: 2990
		public static readonly int[] LevelStarts = new int[]
		{
			0,
			10,
			25,
			50,
			75
		};

		// Token: 0x04000BAF RID: 2991
		public static readonly string[] LevelNames = new string[]
		{
			"Desconocido",
			"Visto",
			"Buscado",
			"Perseguido",
			"Deuda de sangre"
		};

		// Token: 0x04000BB0 RID: 2992
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int>[] LevelColors = new ValueTuple<int, int, int>[]
		{
			new ValueTuple<int, int, int>(128, 126, 120),
			new ValueTuple<int, int, int>(206, 190, 140),
			new ValueTuple<int, int, int>(218, 152, 62),
			new ValueTuple<int, int, int>(212, 98, 48),
			new ValueTuple<int, int, int>(196, 42, 36)
		};
	}
}
