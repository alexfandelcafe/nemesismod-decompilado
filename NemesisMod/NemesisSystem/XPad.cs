using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using RDR2;

namespace NemesisSystem
{
	// Token: 0x0200003C RID: 60
	internal static class XPad
	{
		// Token: 0x06000BFD RID: 3069
		[DllImport("xinput1_4.dll", EntryPoint = "XInputGetState")]
		private static extern uint Get14(uint user, out XPad.State s);

		// Token: 0x06000BFE RID: 3070
		[DllImport("xinput9_1_0.dll", EntryPoint = "XInputGetState")]
		private static extern uint Get910(uint user, out XPad.State s);

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x0010AC63 File Offset: 0x00108E63
		// (set) Token: 0x06000C00 RID: 3072 RVA: 0x0010AC6A File Offset: 0x00108E6A
		public static bool Connected { get; private set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x0010AC72 File Offset: 0x00108E72
		public static bool RecentlyUsed
		{
			get
			{
				return XPad.Connected && XPad._lastActivity - XPad._lastKeyboard > 0;
			}
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0010AC8B File Offset: 0x00108E8B
		public static void NoteKeyboard()
		{
			XPad._lastKeyboard = Environment.TickCount;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0010AC98 File Offset: 0x00108E98
		public static void Poll()
		{
			XPad._prev = XPad._now;
			long timestamp = Stopwatch.GetTimestamp();
			XPad.CheckIcons();
			long timestamp2 = Stopwatch.GetTimestamp();
			XPad.State state;
			bool flag = XPad.PollXInput(out state);
			long timestamp3 = Stopwatch.GetTimestamp();
			if (flag)
			{
				XPad.SetFallback(false);
				XPad.Connected = true;
				XPad._now = state.Pad.Buttons;
				XPad.LX = XPad.Stick(state.Pad.LX);
				XPad.LY = XPad.Stick(state.Pad.LY);
				XPad.RX = XPad.Stick(state.Pad.RX);
				XPad.RY = XPad.Stick(state.Pad.RY);
				XPad.LT = (float)state.Pad.LT / 255f;
				XPad.RT = (float)state.Pad.RT / 255f;
			}
			else
			{
				if (!XPad.FallbackWanted())
				{
					XPad.SetFallback(false);
					XPad.Connected = false;
					XPad._now = 0;
					XPad.LX = (XPad.LY = (XPad.RX = (XPad.RY = (XPad.LT = (XPad.RT = 0f)))));
					XPad.NoteSlow(timestamp, timestamp2, timestamp3);
					return;
				}
				XPad.SetFallback(true);
				XPad.Connected = true;
				XPad.ReadGameInputs();
			}
			XPad.NoteSlow(timestamp, timestamp2, timestamp3);
			if ((XPad._now & ~(XPad._prev != 0)) != 0 || Math.Max(Math.Max(Math.Abs(XPad.LX), Math.Abs(XPad.LY)), Math.Max(Math.Abs(XPad.RX), Math.Abs(XPad.RY))) > 0.6f || Math.Max(XPad.LT, XPad.RT) > 0.5f)
			{
				XPad._lastActivity = Environment.TickCount;
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0010AE44 File Offset: 0x00109044
		private static void NoteSlow(long t0, long t1, long t2)
		{
			long timestamp = Stopwatch.GetTimestamp();
			double num = 1000.0 / (double)Stopwatch.Frequency;
			double num2 = (double)(timestamp - t0) * num;
			if (num2 < 12.0 || (XPad._slowNoted && Environment.TickCount - XPad._slowNotedAt < 60000))
			{
				return;
			}
			XPad._slowNoted = true;
			XPad._slowNotedAt = Environment.TickCount;
			Action<string> log = XPad.Log;
			if (log == null)
			{
				return;
			}
			log(string.Format("PADSLOW {0:0} ms: the Sony check {1:0.0}, XInput {2:0.0} (pad {3}), the rest {4:0.0}", new object[]
			{
				num2,
				(double)(t1 - t0) * num,
				(double)(t2 - t1) * num,
				XPad._xinputUp ? "up" : "none",
				(double)(timestamp - t2) * num
			}));
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0010AF10 File Offset: 0x00109110
		private static bool PollXInput(out XPad.State s)
		{
			s = default(XPad.State);
			if (XPad._unavailable)
			{
				return false;
			}
			if (!XPad._xinputUp)
			{
				int probeFound = XPad._probeFound;
				if (probeFound < 0)
				{
					if (!XPad._probing && Environment.TickCount - XPad._nextProbe >= 0)
					{
						XPad._probing = true;
						XPad._nextProbe = Environment.TickCount + 2000;
						try
						{
							Action action;
							if ((action = XPad.<>O.<0>__ProbeSlots) == null)
							{
								action = (XPad.<>O.<0>__ProbeSlots = new Action(XPad.ProbeSlots));
							}
							Task.Run(action);
						}
						catch
						{
							XPad._probing = false;
						}
					}
					return false;
				}
				XPad._probeFound = -1;
				XPad._slot = (uint)probeFound;
			}
			bool flag = false;
			try
			{
				flag = ((XPad._use910 ? XPad.Get910(XPad._slot, out s) : XPad.Get14(XPad._slot, out s)) == 0U);
			}
			catch (DllNotFoundException)
			{
				if (XPad._use910)
				{
					XPad._unavailable = true;
				}
				else
				{
					XPad._use910 = true;
				}
			}
			catch (EntryPointNotFoundException)
			{
				XPad._unavailable = true;
			}
			catch
			{
			}
			if (!flag && XPad._xinputUp)
			{
				XPad._nextProbe = Environment.TickCount;
			}
			XPad._xinputUp = flag;
			return flag;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0010B054 File Offset: 0x00109254
		private static void ProbeSlots()
		{
			try
			{
				for (uint num = 0U; num < 4U; num += 1U)
				{
					try
					{
						XPad.State state;
						XPad.State state2;
						if ((XPad._use910 ? XPad.Get910(num, out state) : XPad.Get14(num, out state2)) == 0U)
						{
							XPad._probeFound = (int)num;
							break;
						}
					}
					catch (DllNotFoundException)
					{
						if (XPad._use910)
						{
							XPad._unavailable = true;
							break;
						}
						XPad._use910 = true;
						num -= 1U;
					}
					catch (EntryPointNotFoundException)
					{
						XPad._unavailable = true;
						break;
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			finally
			{
				XPad._probing = false;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000C07 RID: 3079 RVA: 0x0010B118 File Offset: 0x00109318
		// (set) Token: 0x06000C08 RID: 3080 RVA: 0x0010B11F File Offset: 0x0010931F
		public static bool PlayStation { get; private set; }

		// Token: 0x06000C09 RID: 3081 RVA: 0x0010B128 File Offset: 0x00109328
		private static void CheckIcons()
		{
			if (Environment.TickCount - XPad._nextSonyCheck >= 0 && !XPad._sonyChecking)
			{
				XPad._nextSonyCheck = Environment.TickCount + 5000;
				XPad._sonyChecking = true;
				try
				{
					Task.Run(delegate()
					{
						try
						{
							XPad._sony = XPad.FindSony();
						}
						catch
						{
						}
						finally
						{
							XPad._sonyChecking = false;
						}
					});
				}
				catch
				{
					XPad._sonyChecking = false;
				}
			}
			string a = (XPad.IconsSetting ?? "").Trim().ToLowerInvariant();
			bool flag = a == "playstation" || a == "ps" || a == "ps4" || a == "ps5";
			bool flag2 = flag || (a != "xbox" && XPad._sony);
			if (flag2 == XPad.PlayStation && XPad._iconsLogged)
			{
				return;
			}
			XPad.PlayStation = flag2;
			XPad._iconsLogged = true;
			Action<string> log = XPad.Log;
			if (log == null)
			{
				return;
			}
			log(flag2 ? (flag ? "PAD icons: PlayStation (setting)" : "PAD icons: PlayStation (Sony controller found)") : ((a == "xbox") ? "PAD icons: Xbox (setting)" : "PAD icons: Xbox"));
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0010B26C File Offset: 0x0010946C
		private static bool FallbackWanted()
		{
			if (!XPad._sony || Environment.TickCount - XPad._lastKeyboard < 500)
			{
				return false;
			}
			bool result;
			try
			{
				result = !Game.IsUsingKeyboardAndMouse;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x0010B2B8 File Offset: 0x001094B8
		private static void SetFallback(bool on)
		{
			if (on == XPad._fallback)
			{
				return;
			}
			XPad._fallback = on;
			Action<string> log = XPad.Log;
			if (log == null)
			{
				return;
			}
			log(on ? "PAD fallback: reading a PlayStation pad through the game's inputs" : "PAD fallback: off");
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x0010B2E8 File Offset: 0x001094E8
		private static void ReadGameInputs()
		{
			XPad.<>c__DisplayClass62_0 CS$<>8__locals1;
			CS$<>8__locals1.b = 0;
			XPad.<ReadGameInputs>g__Bit|62_0(1662638961, 1, ref CS$<>8__locals1);
			XPad.<ReadGameInputs>g__Bit|62_0(97156178, 2, ref CS$<>8__locals1);
			XPad.<ReadGameInputs>g__Bit|62_0(-1503741260, 4, ref CS$<>8__locals1);
			XPad.<ReadGameInputs>g__Bit|62_0(-558677229, 8, ref CS$<>8__locals1);
			XPad.<ReadGameInputs>g__Bit|62_0(-944425974, 4096, ref CS$<>8__locals1);
			XPad.<ReadGameInputs>g__Bit|62_0(359624985, 8192, ref CS$<>8__locals1);
			XPad.<ReadGameInputs>g__Bit|62_0(1840825903, 16384, ref CS$<>8__locals1);
			XPad.<ReadGameInputs>g__Bit|62_0(2080465600, 32768, ref CS$<>8__locals1);
			XPad.<ReadGameInputs>g__Bit|62_0(-393875690, 256, ref CS$<>8__locals1);
			XPad.<ReadGameInputs>g__Bit|62_0(398377320, 512, ref CS$<>8__locals1);
			XPad.<ReadGameInputs>g__Bit|62_0(1137550768, 64, ref CS$<>8__locals1);
			XPad.<ReadGameInputs>g__Bit|62_0(2107936042, 128, ref CS$<>8__locals1);
			XPad.<ReadGameInputs>g__Bit|62_0(387518684, 32, ref CS$<>8__locals1);
			XPad.<ReadGameInputs>g__Bit|62_0(-668070958, 16, ref CS$<>8__locals1);
			XPad._now = CS$<>8__locals1.b;
			XPad.LX = XPad.<ReadGameInputs>g__Dz|62_2(XPad.<ReadGameInputs>g__N|62_1(-78193317));
			XPad.LY = XPad.<ReadGameInputs>g__Dz|62_2(-XPad.<ReadGameInputs>g__N|62_1(152139984));
			XPad.RX = XPad.<ReadGameInputs>g__Dz|62_2(XPad.<ReadGameInputs>g__N|62_1(1025725594));
			XPad.RY = XPad.<ReadGameInputs>g__Dz|62_2(-XPad.<ReadGameInputs>g__N|62_1(-348049185));
			bool flag = false;
			bool flag2 = false;
			try
			{
				flag = Game.IsDisabledControlPressed(1360019509);
				flag2 = Game.IsDisabledControlPressed(1877832124);
			}
			catch
			{
			}
			XPad.LT = Math.Max(flag ? 1f : 0f, Math.Abs(XPad.<ReadGameInputs>g__N|62_1(1360019509)));
			XPad.RT = Math.Max(flag2 ? 1f : 0f, Math.Abs(XPad.<ReadGameInputs>g__N|62_1(1877832124)));
		}

		// Token: 0x06000C0D RID: 3085
		[DllImport("user32.dll")]
		private static extern uint GetRawInputDeviceList([Out] XPad.RawDev[] list, ref uint count, uint size);

		// Token: 0x06000C0E RID: 3086
		[DllImport("user32.dll")]
		private static extern uint GetRawInputDeviceInfo(IntPtr dev, uint cmd, ref XPad.RidInfo info, ref uint size);

		// Token: 0x06000C0F RID: 3087 RVA: 0x0010B4B0 File Offset: 0x001096B0
		private static bool FindSony()
		{
			try
			{
				uint size = (uint)Marshal.SizeOf(typeof(XPad.RawDev));
				uint num = 0U;
				if (XPad.GetRawInputDeviceList(null, ref num, size) == 4294967295U || num == 0U)
				{
					return false;
				}
				XPad.RawDev[] array = new XPad.RawDev[num + 8U];
				num = (uint)array.Length;
				uint rawInputDeviceList = XPad.GetRawInputDeviceList(array, ref num, size);
				if (rawInputDeviceList == 4294967295U)
				{
					return false;
				}
				int num2 = 0;
				while ((long)num2 < (long)((ulong)rawInputDeviceList) && num2 < array.Length)
				{
					if (array[num2].Type == 2U)
					{
						XPad.RidInfo ridInfo = new XPad.RidInfo
						{
							Size = 32U
						};
						uint num3 = 32U;
						if (XPad.GetRawInputDeviceInfo(array[num2].Handle, 536870923U, ref ridInfo, ref num3) != 4294967295U && ridInfo.Type == 2U && ridInfo.Vendor == 1356U && ridInfo.UsagePage == 1 && (ridInfo.Usage == 4 || ridInfo.Usage == 5 || ridInfo.Usage == 8))
						{
							return true;
						}
					}
					num2++;
				}
			}
			catch
			{
			}
			return false;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x0010B5D0 File Offset: 0x001097D0
		private static float Stick(short v)
		{
			float num = (float)v / 32767f;
			if (Math.Abs(num) >= 0.2f)
			{
				return Math.Max(-1f, Math.Min(1f, num));
			}
			return 0f;
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0010B60E File Offset: 0x0010980E
		public static ushort Buttons
		{
			get
			{
				return XPad._now;
			}
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x0010B615 File Offset: 0x00109815
		public static bool Held(ushort b)
		{
			return (XPad._now & b) > 0;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x0010B621 File Offset: 0x00109821
		public static bool Pressed(ushort b)
		{
			return (XPad._now & b) != 0 && (XPad._prev & b) == 0;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0010B68C File Offset: 0x0010988C
		[CompilerGenerated]
		internal static void <ReadGameInputs>g__Bit|62_0(eInputType c, ushort bit, ref XPad.<>c__DisplayClass62_0 A_2)
		{
			try
			{
				if (Game.IsDisabledControlPressed(c))
				{
					A_2.b |= bit;
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x0010B6C8 File Offset: 0x001098C8
		[CompilerGenerated]
		internal static float <ReadGameInputs>g__N|62_1(eInputType c)
		{
			float result;
			try
			{
				result = Game.GetDisabledControlNormal(c);
			}
			catch
			{
				result = 0f;
			}
			return result;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x0010B6F8 File Offset: 0x001098F8
		[CompilerGenerated]
		internal static float <ReadGameInputs>g__Dz|62_2(float f)
		{
			if (Math.Abs(f) >= 0.2f)
			{
				return Math.Max(-1f, Math.Min(1f, f));
			}
			return 0f;
		}

		// Token: 0x04000887 RID: 2183
		public const ushort Up = 1;

		// Token: 0x04000888 RID: 2184
		public const ushort Down = 2;

		// Token: 0x04000889 RID: 2185
		public const ushort Left = 4;

		// Token: 0x0400088A RID: 2186
		public const ushort Right = 8;

		// Token: 0x0400088B RID: 2187
		public const ushort Start = 16;

		// Token: 0x0400088C RID: 2188
		public const ushort Back = 32;

		// Token: 0x0400088D RID: 2189
		public const ushort LS = 64;

		// Token: 0x0400088E RID: 2190
		public const ushort RS = 128;

		// Token: 0x0400088F RID: 2191
		public const ushort LB = 256;

		// Token: 0x04000890 RID: 2192
		public const ushort RB = 512;

		// Token: 0x04000891 RID: 2193
		public const ushort A = 4096;

		// Token: 0x04000892 RID: 2194
		public const ushort B = 8192;

		// Token: 0x04000893 RID: 2195
		public const ushort X = 16384;

		// Token: 0x04000894 RID: 2196
		public const ushort Y = 32768;

		// Token: 0x04000895 RID: 2197
		private static volatile bool _use910;

		// Token: 0x04000896 RID: 2198
		private static volatile bool _unavailable;

		// Token: 0x04000897 RID: 2199
		private static volatile bool _probing;

		// Token: 0x04000898 RID: 2200
		private static volatile bool _sonyChecking;

		// Token: 0x04000899 RID: 2201
		private static volatile int _probeFound = -1;

		// Token: 0x0400089A RID: 2202
		private static uint _slot;

		// Token: 0x0400089B RID: 2203
		private static int _nextProbe = Environment.TickCount;

		// Token: 0x0400089C RID: 2204
		private static ushort _now;

		// Token: 0x0400089D RID: 2205
		private static ushort _prev;

		// Token: 0x0400089E RID: 2206
		private static int _lastActivity = Environment.TickCount - 200000;

		// Token: 0x040008A0 RID: 2208
		public static float LX;

		// Token: 0x040008A1 RID: 2209
		public static float LY;

		// Token: 0x040008A2 RID: 2210
		public static float RX;

		// Token: 0x040008A3 RID: 2211
		public static float RY;

		// Token: 0x040008A4 RID: 2212
		public static float LT;

		// Token: 0x040008A5 RID: 2213
		public static float RT;

		// Token: 0x040008A6 RID: 2214
		private static int _lastKeyboard = Environment.TickCount - 100000;

		// Token: 0x040008A7 RID: 2215
		private static int _slowNotedAt;

		// Token: 0x040008A8 RID: 2216
		private static bool _slowNoted;

		// Token: 0x040008A9 RID: 2217
		private static bool _xinputUp;

		// Token: 0x040008AA RID: 2218
		public static Action<string> Log;

		// Token: 0x040008AB RID: 2219
		public static string IconsSetting = "Auto";

		// Token: 0x040008AD RID: 2221
		private static volatile bool _sony;

		// Token: 0x040008AE RID: 2222
		private static bool _iconsLogged;

		// Token: 0x040008AF RID: 2223
		private static bool _fallback;

		// Token: 0x040008B0 RID: 2224
		private static int _nextSonyCheck = Environment.TickCount;

		// Token: 0x040008B1 RID: 2225
		private const uint RIDI_DEVICEINFO = 536870923U;

		// Token: 0x040008B2 RID: 2226
		private const uint RIM_TYPEHID = 2U;

		// Token: 0x02000410 RID: 1040
		private struct Gamepad
		{
			// Token: 0x04001D4D RID: 7501
			public ushort Buttons;

			// Token: 0x04001D4E RID: 7502
			public byte LT;

			// Token: 0x04001D4F RID: 7503
			public byte RT;

			// Token: 0x04001D50 RID: 7504
			public short LX;

			// Token: 0x04001D51 RID: 7505
			public short LY;

			// Token: 0x04001D52 RID: 7506
			public short RX;

			// Token: 0x04001D53 RID: 7507
			public short RY;
		}

		// Token: 0x02000411 RID: 1041
		private struct State
		{
			// Token: 0x04001D54 RID: 7508
			public uint Packet;

			// Token: 0x04001D55 RID: 7509
			public XPad.Gamepad Pad;
		}

		// Token: 0x02000412 RID: 1042
		private struct RawDev
		{
			// Token: 0x04001D56 RID: 7510
			public IntPtr Handle;

			// Token: 0x04001D57 RID: 7511
			public uint Type;
		}

		// Token: 0x02000413 RID: 1043
		[StructLayout(LayoutKind.Explicit, Size = 32)]
		private struct RidInfo
		{
			// Token: 0x04001D58 RID: 7512
			[FieldOffset(0)]
			public uint Size;

			// Token: 0x04001D59 RID: 7513
			[FieldOffset(4)]
			public uint Type;

			// Token: 0x04001D5A RID: 7514
			[FieldOffset(8)]
			public uint Vendor;

			// Token: 0x04001D5B RID: 7515
			[FieldOffset(12)]
			public uint Product;

			// Token: 0x04001D5C RID: 7516
			[FieldOffset(16)]
			public uint Version;

			// Token: 0x04001D5D RID: 7517
			[FieldOffset(20)]
			public ushort UsagePage;

			// Token: 0x04001D5E RID: 7518
			[FieldOffset(22)]
			public ushort Usage;
		}

		// Token: 0x02000414 RID: 1044
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001D5F RID: 7519
			public static Action <0>__ProbeSlots;
		}
	}
}
