using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using RDR2;

namespace NemesisSystem
{
	// Token: 0x02000080 RID: 128
	internal static class UiMouse
	{
		// Token: 0x06001353 RID: 4947
		[DllImport("user32.dll")]
		private static extern bool GetCursorPos(out UiMouse.POINT p);

		// Token: 0x06001354 RID: 4948
		[DllImport("user32.dll")]
		private static extern bool ScreenToClient(IntPtr h, ref UiMouse.POINT p);

		// Token: 0x06001355 RID: 4949
		[DllImport("user32.dll")]
		private static extern bool ClientToScreen(IntPtr h, ref UiMouse.POINT p);

		// Token: 0x06001356 RID: 4950
		[DllImport("user32.dll")]
		private static extern bool GetClientRect(IntPtr h, out UiMouse.RECT r);

		// Token: 0x06001357 RID: 4951
		[DllImport("user32.dll")]
		private static extern bool ClipCursor(ref UiMouse.RECT r);

		// Token: 0x06001358 RID: 4952
		[DllImport("user32.dll", EntryPoint = "ClipCursor")]
		private static extern bool ClipCursorOff(IntPtr none);

		// Token: 0x06001359 RID: 4953
		[DllImport("user32.dll")]
		private static extern bool GetClipCursor(out UiMouse.RECT r);

		// Token: 0x0600135A RID: 4954
		[DllImport("user32.dll")]
		private static extern short GetAsyncKeyState(int vk);

		// Token: 0x0600135B RID: 4955 RVA: 0x00132898 File Offset: 0x00130A98
		public static bool KeyDownNow(Keys k)
		{
			bool result;
			try
			{
				result = (((int)UiMouse.GetAsyncKeyState((int)k) & 32768) != 0);
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x0600135C RID: 4956
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x001328D0 File Offset: 0x00130AD0
		// (set) Token: 0x0600135E RID: 4958 RVA: 0x001328D7 File Offset: 0x00130AD7
		public static bool On { get; private set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x001328DF File Offset: 0x00130ADF
		// (set) Token: 0x06001360 RID: 4960 RVA: 0x001328E6 File Offset: 0x00130AE6
		public static bool Active { get; private set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x001328EE File Offset: 0x00130AEE
		public static bool Sending
		{
			get
			{
				return UiMouse._sending;
			}
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x001328F5 File Offset: 0x00130AF5
		public static bool Over(float x, float y, float w, float h)
		{
			return UiMouse.Active && UiMouse.X >= x && UiMouse.X <= x + w && UiMouse.Y >= y && UiMouse.Y <= y + h;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x00132928 File Offset: 0x00130B28
		public static void Region(float x, float y, float w, float h, Func<bool> hover, Action click = null)
		{
			if (!UiMouse.On || w <= 0f || h <= 0f)
			{
				return;
			}
			UiMouse._hits.Add(new UiMouse.Hit
			{
				X = x,
				Y = y,
				W = w,
				H = h,
				Hover = hover,
				Click = click
			});
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00132994 File Offset: 0x00130B94
		public static void KeyRegion(float x, float y, float w, float h, Keys key)
		{
			if (UiMouse.On)
			{
				UiMouse.Region(x, y, w, h, null, delegate
				{
					UiMouse.Send(key);
				});
			}
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x001329CC File Offset: 0x00130BCC
		public static void Send(Keys key)
		{
			if (UiMouse.SendKey == null)
			{
				return;
			}
			UiMouse._sending = true;
			try
			{
				UiMouse.SendKey(key);
			}
			finally
			{
				UiMouse._sending = false;
			}
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x00132A0C File Offset: 0x00130C0C
		public static void NoteKey(Keys k)
		{
			if (UiMouse._sending || k == Keys.LButton || k == Keys.RButton || k == Keys.MButton || k == Keys.XButton1 || k == Keys.XButton2)
			{
				return;
			}
			UiMouse._lastKeyAt = UiDraw.RealMs;
			UiMouse.Active = false;
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00132A3C File Offset: 0x00130C3C
		public static Keys? KeyOfLabel(string label)
		{
			if (string.IsNullOrEmpty(label))
			{
				return null;
			}
			if (label != null)
			{
				switch (label.Length)
				{
				case 1:
				{
					char c = label[0];
					if (c == '+')
					{
						return new Keys?(Keys.Oemplus);
					}
					if (c != '-')
					{
						goto IL_215;
					}
					return new Keys?(Keys.OemMinus);
				}
				case 2:
				case 7:
				case 8:
					goto IL_215;
				case 3:
				{
					char c = label[0];
					if (c != 'D')
					{
						if (c != 'E')
						{
							if (c != 'T')
							{
								goto IL_215;
							}
							if (!(label == "Tab"))
							{
								goto IL_215;
							}
							return new Keys?(Keys.Tab);
						}
						else
						{
							if (!(label == "Esc"))
							{
								goto IL_215;
							}
							goto IL_1CF;
						}
					}
					else
					{
						if (!(label == "Del"))
						{
							goto IL_215;
						}
						goto IL_1E7;
					}
					break;
				}
				case 4:
				{
					char c = label[2];
					if (c != 'D')
					{
						if (c != 'U')
						{
							if (c != 'c')
							{
								goto IL_215;
							}
							if (!(label == "Atrás"))
							{
								goto IL_215;
							}
						}
						else
						{
							if (!(label == "PgUp"))
							{
								goto IL_215;
							}
							return new Keys?(Keys.Prior);
						}
					}
					else
					{
						if (!(label == "PgDn"))
						{
							goto IL_215;
						}
						return new Keys?(Keys.Next);
					}
					break;
				}
				case 5:
				{
					char c = label[0];
					if (c != 'E')
					{
						if (c != 'S')
						{
							goto IL_215;
						}
						if (!(label == "Espacio"))
						{
							goto IL_215;
						}
						return new Keys?(Keys.Space);
					}
					else
					{
						if (!(label == "Enter"))
						{
							goto IL_215;
						}
						return new Keys?(Keys.Return);
					}
					break;
				}
				case 6:
				{
					char c = label[0];
					if (c != 'D')
					{
						if (c != 'E')
						{
							goto IL_215;
						}
						if (!(label == "Escape"))
						{
							goto IL_215;
						}
						goto IL_1CF;
					}
					else
					{
						if (!(label == "Borrar"))
						{
							goto IL_215;
						}
						goto IL_1E7;
					}
					break;
				}
				case 9:
					if (!(label == "Backspace"))
					{
						goto IL_215;
					}
					break;
				default:
					goto IL_215;
				}
				return new Keys?(Keys.Back);
				IL_1CF:
				return new Keys?(Keys.Escape);
				IL_1E7:
				return new Keys?(Keys.Delete);
			}
			IL_215:
			if (label.Length == 1 && label[0] >= 'A' && label[0] <= 'Z')
			{
				return new Keys?((Keys)label[0]);
			}
			return null;
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00132C94 File Offset: 0x00130E94
		private static IntPtr Window()
		{
			if (UiMouse._hwnd == IntPtr.Zero || UiDraw.RealMs - UiMouse._hwndAt > 2000)
			{
				UiMouse._hwndAt = UiDraw.RealMs;
				try
				{
					UiMouse._hwnd = Process.GetCurrentProcess().MainWindowHandle;
				}
				catch
				{
				}
			}
			return UiMouse._hwnd;
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x00132CF8 File Offset: 0x00130EF8
		public static void Update(bool open)
		{
			UiMouse.WheelUp = (UiMouse.WheelDown = (UiMouse.Moved = (UiMouse.Clicked = (UiMouse.Released = (UiMouse.RightClicked = false)))));
			UiMouse.DX = (UiMouse.DY = 0f);
			bool flag = ((int)UiMouse.GetAsyncKeyState(1) & 32768) != 0;
			bool flag2 = ((int)UiMouse.GetAsyncKeyState(2) & 32768) != 0;
			IntPtr intPtr = (open && !XPad.RecentlyUsed) ? UiMouse.Window() : IntPtr.Zero;
			UiMouse.RECT rect;
			if (intPtr == IntPtr.Zero || UiMouse.GetForegroundWindow() != intPtr || !UiMouse.GetClientRect(intPtr, out rect) || rect.Right <= 0 || rect.Bottom <= 0)
			{
				UiMouse.On = false;
				UiMouse.Active = false;
				UiMouse._havePrev = false;
				UiMouse._lWas = flag;
				UiMouse._rWas = flag2;
				UiMouse.Release();
				return;
			}
			UiMouse.On = true;
			UiMouse._cw = rect.Right;
			UiMouse._ch = rect.Bottom;
			UiMouse.POINT point;
			if (!UiMouse.GetCursorPos(out point))
			{
				UiMouse.Active = false;
				return;
			}
			UiMouse.ScreenToClient(intPtr, ref point);
			int num = Math.Max(0, Math.Min(UiMouse._cw - 1, point.X));
			int num2 = Math.Max(0, Math.Min(UiMouse._ch - 1, point.Y));
			float num3 = UiDraw.FromScreenX((float)num / (float)UiMouse._cw);
			float num4 = UiDraw.FromScreenY((float)num2 / (float)UiMouse._ch);
			if (UiMouse._havePrev)
			{
				int num5 = Math.Abs(num - UiMouse._cx) + Math.Abs(num2 - UiMouse._cy);
				UiMouse.Moved = (num5 >= 1);
				UiMouse.DX = num3 - UiMouse.X;
				UiMouse.DY = num4 - UiMouse.Y;
				if (num5 >= 3)
				{
					UiMouse.NoteMouse();
				}
			}
			UiMouse._havePrev = true;
			UiMouse._cx = num;
			UiMouse._cy = num2;
			UiMouse.X = num3;
			UiMouse.Y = num4;
			bool flag3 = point.X >= 0 && point.Y >= 0 && point.X < UiMouse._cw && point.Y < UiMouse._ch;
			UiMouse.Clicked = (flag && !UiMouse._lWas && flag3);
			UiMouse.Released = (!flag && UiMouse._lWas);
			UiMouse.RightClicked = (flag2 && !UiMouse._rWas && flag3);
			UiMouse.Down = flag;
			UiMouse._lWas = flag;
			UiMouse._rWas = flag2;
			if (UiMouse.Clicked || UiMouse.RightClicked)
			{
				UiMouse.NoteMouse();
			}
			try
			{
				UiMouse.WheelUp = (Game.IsDisabledControlJustPressed(1652558994) || Game.IsDisabledControlJustPressed(-871336537));
				UiMouse.WheelDown = (Game.IsDisabledControlJustPressed(-1948355517) || Game.IsDisabledControlJustPressed(-49345492));
			}
			catch
			{
			}
			if (UiMouse.WheelUp || UiMouse.WheelDown)
			{
				UiMouse._lastMouseAt = UiDraw.RealMs;
			}
			UiMouse.Active = (UiDraw.RealMs - UiMouse._lastMouseAt < 3000 && UiMouse._lastMouseAt >= UiMouse._lastKeyAt);
			if (!UiMouse.Active)
			{
				UiMouse.Moved = false;
			}
			UiMouse.Clip(intPtr);
			if (UiMouse.Active)
			{
				UiMouse.HoldLook();
			}
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00133014 File Offset: 0x00131214
		private static void NoteMouse()
		{
			UiMouse._lastMouseAt = UiDraw.RealMs;
			XPad.NoteKeyboard();
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00133028 File Offset: 0x00131228
		private static void HoldLook()
		{
			foreach (eInputType eInputType in UiMouse.LookControls)
			{
				try
				{
					Game.DisableControlThisFrame(eInputType);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00133068 File Offset: 0x00131268
		private static void Clip(IntPtr h)
		{
			if (UiDraw.RealMs - UiMouse._clipAt < 250 && UiMouse._clipped)
			{
				return;
			}
			UiMouse._clipAt = UiDraw.RealMs;
			try
			{
				if (!UiMouse._clipped)
				{
					UiMouse._hadOldClip = UiMouse.GetClipCursor(out UiMouse._oldClip);
				}
				UiMouse.POINT point = new UiMouse.POINT
				{
					X = 0,
					Y = 0
				};
				UiMouse.POINT point2 = new UiMouse.POINT
				{
					X = UiMouse._cw,
					Y = UiMouse._ch
				};
				UiMouse.ClientToScreen(h, ref point);
				UiMouse.ClientToScreen(h, ref point2);
				UiMouse.RECT rect = new UiMouse.RECT
				{
					Left = point.X,
					Top = point.Y,
					Right = point2.X,
					Bottom = point2.Y
				};
				UiMouse._clipped = (UiMouse.ClipCursor(ref rect) || UiMouse._clipped);
			}
			catch
			{
			}
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00133168 File Offset: 0x00131368
		public static void Release()
		{
			if (!UiMouse._clipped)
			{
				return;
			}
			UiMouse._clipped = false;
			try
			{
				if (UiMouse._hadOldClip)
				{
					UiMouse.RECT oldClip = UiMouse._oldClip;
					UiMouse.ClipCursor(ref oldClip);
				}
				else
				{
					UiMouse.ClipCursorOff(IntPtr.Zero);
				}
			}
			catch
			{
				try
				{
					UiMouse.ClipCursorOff(IntPtr.Zero);
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x001331D8 File Offset: 0x001313D8
		public static void EndFrame()
		{
			try
			{
				if (UiMouse.On)
				{
					if (UiMouse.Active)
					{
						UiMouse.HoldLook();
						UiMouse.Hit? hit = null;
						for (int i = UiMouse._hits.Count - 1; i >= 0; i--)
						{
							UiMouse.Hit hit2 = UiMouse._hits[i];
							if (UiMouse.X >= hit2.X && UiMouse.X <= hit2.X + hit2.W && UiMouse.Y >= hit2.Y && UiMouse.Y <= hit2.Y + hit2.H)
							{
								hit = new UiMouse.Hit?(hit2);
								break;
							}
						}
						if (hit != null && (UiMouse.Moved || UiMouse.Clicked) && hit.Value.Hover != null)
						{
							bool flag = false;
							try
							{
								flag = hit.Value.Hover();
							}
							catch
							{
							}
							if (flag && !UiMouse.Clicked)
							{
								NemesisMod.UiSound("NAV_DOWN");
							}
						}
						if (hit != null && UiMouse.Clicked && hit.Value.Click != null)
						{
							hit.Value.Click();
						}
						if (UiMouse.RightClicked)
						{
							UiMouse.Send(Keys.Back);
						}
					}
					if (UiMouse.WheelUp || UiMouse.WheelDown)
					{
						int num = UiMouse.WheelUp ? 1 : -1;
						if (UiMouse.WheelHandler != null)
						{
							UiMouse.WheelHandler(num);
						}
						else
						{
							UiMouse.Send((num > 0) ? Keys.Up : Keys.Down);
						}
					}
					if (UiMouse.Active && !UiMouse._drawnEarly)
					{
						UiMouse.DrawCursor();
					}
					UiMouse._drawnEarly = false;
				}
			}
			finally
			{
				UiMouse._hits.Clear();
				UiMouse.WheelHandler = null;
			}
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x001333A8 File Offset: 0x001315A8
		public static void DrawCursorNow()
		{
			if (UiMouse.On && UiMouse.Active && !UiMouse._drawnEarly)
			{
				UiMouse.DrawCursor();
				UiMouse._drawnEarly = true;
			}
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x001333CC File Offset: 0x001315CC
		private static void DrawCursor()
		{
			UiMouse.<>c__DisplayClass70_0 CS$<>8__locals1;
			CS$<>8__locals1.sx = (float)UiMouse._cx / (float)UiMouse._cw;
			CS$<>8__locals1.sy = (float)UiMouse._cy / (float)UiMouse._ch;
			float num = Math.Max(1f, (float)Math.Round((double)((float)UiMouse._ch / 1080f * 2f)));
			CS$<>8__locals1.px = num / (float)UiMouse._cw;
			CS$<>8__locals1.py = num / (float)UiMouse._ch;
			for (int i = 0; i <= 9; i++)
			{
				int num2 = (int)Math.Round((double)((float)i * 0.72f)) + 2;
				UiMouse.<DrawCursor>g__Box|70_0(0f, (float)i, (float)num2, 1f, 12, 235, ref CS$<>8__locals1);
				if (i >= 1 && i <= 8 && num2 - 2 > 0)
				{
					UiMouse.<DrawCursor>g__Box|70_0(1f, (float)i, (float)(num2 - 2), 1f, 250, 255, ref CS$<>8__locals1);
				}
			}
			for (int j = 0; j < 4; j++)
			{
				float num3 = 2.5f + (float)j * 0.8f;
				UiMouse.<DrawCursor>g__Box|70_0(num3, (float)(8 + j), 3.4f, 1.2f, 12, 235, ref CS$<>8__locals1);
				if (j < 3)
				{
					UiMouse.<DrawCursor>g__Box|70_0(num3 + 1f, 8.1f + (float)j, 1.4f, 1f, 250, 255, ref CS$<>8__locals1);
				}
			}
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x00133520 File Offset: 0x00131720
		// Note: this type is marked as 'beforefieldinit'.
		static UiMouse()
		{
			eInputType[] array = new eInputType[7];
			RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.2F7BA10125A7BCF64F639116DBD47DC2C7AA3C9F0BA116824CD7D0A9C7A4D873).FieldHandle);
			UiMouse.LookControls = array;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x00133580 File Offset: 0x00131780
		[CompilerGenerated]
		internal static void <DrawCursor>g__Box|70_0(float x, float y, float w, float hh, int c, int a, ref UiMouse.<>c__DisplayClass70_0 A_6)
		{
			UiDraw.RealRect(A_6.sx + (x + w / 2f) * A_6.px, A_6.sy + (y + hh / 2f) * A_6.py, w * A_6.px, hh * A_6.py, c, c, c, a);
		}

		// Token: 0x04000D07 RID: 3335
		public static float X;

		// Token: 0x04000D08 RID: 3336
		public static float Y;

		// Token: 0x04000D09 RID: 3337
		public static float DX;

		// Token: 0x04000D0A RID: 3338
		public static float DY;

		// Token: 0x04000D0B RID: 3339
		public static bool Moved;

		// Token: 0x04000D0C RID: 3340
		public static bool Down;

		// Token: 0x04000D0D RID: 3341
		public static bool Clicked;

		// Token: 0x04000D0E RID: 3342
		public static bool Released;

		// Token: 0x04000D0F RID: 3343
		public static bool RightClicked;

		// Token: 0x04000D10 RID: 3344
		public static bool WheelUp;

		// Token: 0x04000D11 RID: 3345
		public static bool WheelDown;

		// Token: 0x04000D12 RID: 3346
		public static Action<Keys> SendKey;

		// Token: 0x04000D13 RID: 3347
		public static Action<MenuTab> TabClick;

		// Token: 0x04000D14 RID: 3348
		public static Action<int> WheelHandler;

		// Token: 0x04000D15 RID: 3349
		private static IntPtr _hwnd;

		// Token: 0x04000D16 RID: 3350
		private static int _hwndAt = -100000;

		// Token: 0x04000D17 RID: 3351
		private static int _cx;

		// Token: 0x04000D18 RID: 3352
		private static int _cy;

		// Token: 0x04000D19 RID: 3353
		private static int _cw = 1920;

		// Token: 0x04000D1A RID: 3354
		private static int _ch = 1080;

		// Token: 0x04000D1B RID: 3355
		private static bool _havePrev;

		// Token: 0x04000D1C RID: 3356
		private static bool _lWas;

		// Token: 0x04000D1D RID: 3357
		private static bool _rWas;

		// Token: 0x04000D1E RID: 3358
		private static bool _clipped;

		// Token: 0x04000D1F RID: 3359
		private static bool _hadOldClip;

		// Token: 0x04000D20 RID: 3360
		private static bool _sending;

		// Token: 0x04000D21 RID: 3361
		private static int _lastMouseAt = -100000;

		// Token: 0x04000D22 RID: 3362
		private static int _lastKeyAt;

		// Token: 0x04000D23 RID: 3363
		private static int _clipAt = -100000;

		// Token: 0x04000D24 RID: 3364
		private static UiMouse.RECT _oldClip;

		// Token: 0x04000D25 RID: 3365
		private static readonly List<UiMouse.Hit> _hits = new List<UiMouse.Hit>();

		// Token: 0x04000D26 RID: 3366
		private static readonly eInputType[] LookControls;

		// Token: 0x04000D27 RID: 3367
		private static bool _drawnEarly;

		// Token: 0x0200046A RID: 1130
		private struct POINT
		{
			// Token: 0x04002004 RID: 8196
			public int X;

			// Token: 0x04002005 RID: 8197
			public int Y;
		}

		// Token: 0x0200046B RID: 1131
		private struct RECT
		{
			// Token: 0x04002006 RID: 8198
			public int Left;

			// Token: 0x04002007 RID: 8199
			public int Top;

			// Token: 0x04002008 RID: 8200
			public int Right;

			// Token: 0x04002009 RID: 8201
			public int Bottom;
		}

		// Token: 0x0200046C RID: 1132
		private struct Hit
		{
			// Token: 0x0400200A RID: 8202
			public float X;

			// Token: 0x0400200B RID: 8203
			public float Y;

			// Token: 0x0400200C RID: 8204
			public float W;

			// Token: 0x0400200D RID: 8205
			public float H;

			// Token: 0x0400200E RID: 8206
			public Func<bool> Hover;

			// Token: 0x0400200F RID: 8207
			public Action Click;
		}
	}
}
