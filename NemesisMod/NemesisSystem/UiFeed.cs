using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using RDR2;
using RDR2.Native;

namespace NemesisSystem
{
	// Token: 0x0200007F RID: 127
	internal static class UiFeed
	{
		// Token: 0x06001350 RID: 4944 RVA: 0x001325B5 File Offset: 0x001307B5
		public static void Toast(string title, string text, string dict = "generic_textures", string icon = "tick")
		{
			if (!UiFeed.Enabled)
			{
				return;
			}
			if (UiFeed._queue.Count >= 5)
			{
				UiFeed._queue.Dequeue();
			}
			UiFeed._queue.Enqueue(new ValueTuple<string, string, string, string>(title, text, dict, icon));
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x001325EC File Offset: 0x001307EC
		public static void Tick()
		{
			for (int i = UiFeed._live.Count - 1; i >= 0; i--)
			{
				if (Game.GameTime - UiFeed._live[i].Item3 > 15000 || Game.GameTime < UiFeed._live[i].Item3)
				{
					Marshal.FreeHGlobal(UiFeed._live[i].Item1);
					Marshal.FreeHGlobal(UiFeed._live[i].Item2);
					UiFeed._live.RemoveAt(i);
				}
			}
			if (!UiFeed.Enabled || UiFeed._queue.Count == 0 || Game.GameTime < UiFeed._nextAt)
			{
				return;
			}
			ValueTuple<string, string, string, string> valueTuple = UiFeed._queue.Dequeue();
			UiFeed._nextAt = Game.GameTime + 4000;
			Action<string> breadcrumb = UiFeed.Breadcrumb;
			if (breadcrumb != null)
			{
				breadcrumb("FEED toast posting: " + valueTuple.Item1 + " / " + valueTuple.Item2);
			}
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			try
			{
				intPtr = Marshal.AllocHGlobal(56);
				intPtr2 = Marshal.AllocHGlobal(64);
				for (int j = 0; j < 7; j++)
				{
					Marshal.WriteInt64(intPtr, j * 8, 0L);
				}
				for (int k = 0; k < 8; k++)
				{
					Marshal.WriteInt64(intPtr2, k * 8, 0L);
				}
				Marshal.WriteInt64(intPtr, 0, 4000L);
				Marshal.WriteInt64(intPtr2, 8, (long)UiDraw.ForeverVarString(UiDraw.Safe(valueTuple.Item1)));
				Marshal.WriteInt64(intPtr2, 16, (long)UiDraw.ForeverVarString(UiDraw.Safe(valueTuple.Item2)));
				Marshal.WriteInt64(intPtr2, 32, (long)((ulong)MISC.GET_HASH_KEY(valueTuple.Item3)));
				Marshal.WriteInt64(intPtr2, 40, (long)((ulong)MISC.GET_HASH_KEY(valueTuple.Item4)));
				Marshal.WriteInt64(intPtr2, 48, (long)((ulong)MISC.GET_HASH_KEY("COLOR_WHITE")));
				Function.Call(2803616216400160553UL, new InputArgument[]
				{
					intPtr.ToInt64(),
					intPtr2.ToInt64(),
					1,
					1
				});
				UiFeed._live.Add(new ValueTuple<IntPtr, IntPtr, int>(intPtr, intPtr2, Game.GameTime));
				UiFeed.Posted++;
			}
			catch (Exception ex)
			{
				UiFeed.Failed++;
				UiFeed.LastError = ex.Message;
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				if (intPtr2 != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr2);
				}
			}
		}

		// Token: 0x04000CFC RID: 3324
		private const ulong PostSampleToast = 2803616216400160553UL;

		// Token: 0x04000CFD RID: 3325
		public static bool Enabled = false;

		// Token: 0x04000CFE RID: 3326
		public static Action<string> Breadcrumb;

		// Token: 0x04000CFF RID: 3327
		public static int Posted;

		// Token: 0x04000D00 RID: 3328
		public static int Failed;

		// Token: 0x04000D01 RID: 3329
		public static string LastError = "";

		// Token: 0x04000D02 RID: 3330
		[TupleElementNames(new string[]
		{
			"title",
			"text",
			"dict",
			"icon"
		})]
		private static readonly Queue<ValueTuple<string, string, string, string>> _queue = new Queue<ValueTuple<string, string, string, string>>();

		// Token: 0x04000D03 RID: 3331
		[TupleElementNames(new string[]
		{
			"a",
			"b",
			"at"
		})]
		private static readonly List<ValueTuple<IntPtr, IntPtr, int>> _live = new List<ValueTuple<IntPtr, IntPtr, int>>();

		// Token: 0x04000D04 RID: 3332
		private static int _nextAt;
	}
}
