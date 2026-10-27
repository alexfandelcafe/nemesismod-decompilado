using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using RDR2;
using RDR2.Native;

namespace NemesisSystem
{
	// Token: 0x0200007E RID: 126
	internal static class UiDraw
	{
		// Token: 0x06001315 RID: 4885 RVA: 0x0013135C File Offset: 0x0012F55C
		private static ulong Pin(string text)
		{
			int frame = Game.FrameCount;
			UiDraw.NativeString nativeString;
			if (!UiDraw.Strings.TryGetValue(text, out nativeString))
			{
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				IntPtr intPtr = Marshal.AllocHGlobal(bytes.Length + 1);
				Marshal.Copy(bytes, 0, intPtr, bytes.Length);
				Marshal.WriteByte(intPtr, bytes.Length, 0);
				Dictionary<string, UiDraw.NativeString> strings = UiDraw.Strings;
				UiDraw.NativeString nativeString2 = new UiDraw.NativeString();
				nativeString2.Ptr = intPtr;
				nativeString = nativeString2;
				strings[text] = nativeString2;
			}
			nativeString.LastUsedFrame = frame;
			if (frame >= UiDraw._nextSweepFrame)
			{
				UiDraw._nextSweepFrame = frame + 600;
				IEnumerable<KeyValuePair<string, UiDraw.NativeString>> strings2 = UiDraw.Strings;
				Func<KeyValuePair<string, UiDraw.NativeString>, bool> <>9__0;
				Func<KeyValuePair<string, UiDraw.NativeString>, bool> predicate;
				if ((predicate = <>9__0) == null)
				{
					predicate = (<>9__0 = ((KeyValuePair<string, UiDraw.NativeString> kv) => frame - kv.Value.LastUsedFrame > 300));
				}
				foreach (string key in (from kv in strings2.Where(predicate)
				select kv.Key).ToList<string>())
				{
					Marshal.FreeHGlobal(UiDraw.Strings[key].Ptr);
					UiDraw.Strings.Remove(key);
				}
			}
			return (ulong)nativeString.Ptr.ToInt64();
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x001314B8 File Offset: 0x0012F6B8
		public static ulong CString(string text)
		{
			if (!UiDraw.CountOnly)
			{
				return UiDraw.Pin(text);
			}
			return 0UL;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x001314CC File Offset: 0x0012F6CC
		public static ulong ForeverString(string text)
		{
			if (UiDraw.CountOnly)
			{
				return 0UL;
			}
			IntPtr intPtr;
			if (!UiDraw.Forever.TryGetValue(text, out intPtr))
			{
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				intPtr = Marshal.AllocHGlobal(bytes.Length + 1);
				Marshal.Copy(bytes, 0, intPtr, bytes.Length);
				Marshal.WriteByte(intPtr, bytes.Length, 0);
				UiDraw.Forever[text] = intPtr;
			}
			return (ulong)intPtr.ToInt64();
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00131534 File Offset: 0x0012F734
		public static ulong ForeverVarString(string text)
		{
			if (UiDraw.CountOnly)
			{
				return 0UL;
			}
			text = UiDraw.TheFix(text);
			return Function.Call<ulong>(18055593637008912569UL, new InputArgument[]
			{
				10,
				new InputArgument(UiDraw.ForeverString("LITERAL_STRING")),
				new InputArgument(UiDraw.ForeverString(text))
			});
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00131594 File Offset: 0x0012F794
		public static ulong VarString(string text)
		{
			if (UiDraw.CountOnly)
			{
				return 0UL;
			}
			UiDraw.TextSlotsThisFrame++;
			text = UiDraw.TheFix(text);
			return Function.Call<ulong>(18055593637008912569UL, new InputArgument[]
			{
				10,
				new InputArgument(UiDraw.Pin("LITERAL_STRING")),
				new InputArgument(UiDraw.Pin(text))
			});
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x0600131A RID: 4890 RVA: 0x001315FD File Offset: 0x0012F7FD
		// (set) Token: 0x0600131B RID: 4891 RVA: 0x00131604 File Offset: 0x0012F804
		public static int RectsThisFrame { get; private set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x0600131C RID: 4892 RVA: 0x0013160C File Offset: 0x0012F80C
		// (set) Token: 0x0600131D RID: 4893 RVA: 0x00131613 File Offset: 0x0012F813
		public static int TextSlotsThisFrame { get; private set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0013161B File Offset: 0x0012F81B
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x00131622 File Offset: 0x0012F822
		public static int FrameNo { get; private set; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0013162A File Offset: 0x0012F82A
		public static int TextSlotsLeft
		{
			get
			{
				return UiDraw.TextSlotLimit - UiDraw.FrameReserve - UiDraw.TextSlotsThisFrame;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x0013163D File Offset: 0x0012F83D
		// (set) Token: 0x06001322 RID: 4898 RVA: 0x00131644 File Offset: 0x0012F844
		public static int LastFrameSlots { get; private set; }

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0013164C File Offset: 0x0012F84C
		// (set) Token: 0x06001324 RID: 4900 RVA: 0x00131653 File Offset: 0x0012F853
		public static int LastFrameSkipped { get; private set; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x0013165B File Offset: 0x0012F85B
		// (set) Token: 0x06001326 RID: 4902 RVA: 0x00131662 File Offset: 0x0012F862
		public static int LastFrameRects { get; private set; }

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06001327 RID: 4903 RVA: 0x0013166A File Offset: 0x0012F86A
		// (set) Token: 0x06001328 RID: 4904 RVA: 0x00131671 File Offset: 0x0012F871
		public static int GlyphsThisFrame { get; private set; }

		// Token: 0x06001329 RID: 4905 RVA: 0x0013167C File Offset: 0x0012F87C
		private static void CountGlyphs(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsWhiteSpace(s[i]))
				{
					UiDraw.GlyphsThisFrame++;
				}
			}
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x001316B6 File Offset: 0x0012F8B6
		private static bool TakeSlots(int n)
		{
			if (UiDraw.TextSlotsThisFrame + n > UiDraw.TextSlotLimit - UiDraw.FrameReserve)
			{
				UiDraw._skipped++;
				return false;
			}
			UiDraw.TextSlotsThisFrame += n;
			return true;
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x001316E7 File Offset: 0x0012F8E7
		// (set) Token: 0x0600132C RID: 4908 RVA: 0x001316EE File Offset: 0x0012F8EE
		public static bool CountOnly { get; set; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x001316F6 File Offset: 0x0012F8F6
		public static float Aspect
		{
			get
			{
				if (!UiDraw.FitTo169)
				{
					return UiDraw.ScreenAspect;
				}
				return 1.7777778f;
			}
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0013170C File Offset: 0x0012F90C
		private static void UpdateFit()
		{
			if (!UiDraw.FitTo169 || UiDraw.CountOnly)
			{
				UiDraw._kx = (UiDraw._ky = 1f);
				return;
			}
			UiDraw._kx = (UiDraw._ky = Math.Min(1f, UiDraw.ScreenAspect / 1.7777778f)) * 1.7777778f / UiDraw.ScreenAspect;
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x00131764 File Offset: 0x0012F964
		private static float MX(float x)
		{
			return 0.5f + (x - 0.5f) * UiDraw._kx;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x00131779 File Offset: 0x0012F979
		private static float MY(float y)
		{
			return 0.5f + (y - 0.5f) * UiDraw._ky;
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0013178E File Offset: 0x0012F98E
		public static float FromScreenX(float x)
		{
			if (UiDraw._kx != 0f)
			{
				return 0.5f + (x - 0.5f) / UiDraw._kx;
			}
			return x;
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x001317B1 File Offset: 0x0012F9B1
		public static float FromScreenY(float y)
		{
			if (UiDraw._ky != 0f)
			{
				return 0.5f + (y - 0.5f) / UiDraw._ky;
			}
			return y;
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x001317D4 File Offset: 0x0012F9D4
		public static void ScreenRect(int r, int g, int b, int a)
		{
			UiDraw.RectsThisFrame++;
			if (!UiDraw.CountOnly)
			{
				GRAPHICS.DRAW_RECT(0.5f, 0.5f, 1f, 1f, r, g, b, a, true, false);
				return;
			}
			Action<float, float, float, float, int, int, int, int> rectHook = UiDraw.RectHook;
			if (rectHook == null)
			{
				return;
			}
			rectHook(0f, 0f, 1f, 1f, r, g, b, a);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x0013183C File Offset: 0x0012FA3C
		public static void RealRect(float cx, float cy, float w, float h, int r, int g, int b, int a)
		{
			UiDraw.RectsThisFrame++;
			if (UiDraw.CountOnly)
			{
				return;
			}
			GRAPHICS.DRAW_RECT(cx, cy, w, h, r, g, b, a, true, false);
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06001335 RID: 4917 RVA: 0x00131870 File Offset: 0x0012FA70
		// (set) Token: 0x06001336 RID: 4918 RVA: 0x00131877 File Offset: 0x0012FA77
		public static float RealDt { get; private set; } = 0.016f;

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x0013187F File Offset: 0x0012FA7F
		public static int RealMs
		{
			get
			{
				return (int)UiDraw.Clock.ElapsedMilliseconds;
			}
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x0013188C File Offset: 0x0012FA8C
		public static void BeginFrame()
		{
			double totalSeconds = UiDraw.Clock.Elapsed.TotalSeconds;
			UiDraw.RealDt = (float)Math.Min(0.1, Math.Max(0.0, totalSeconds - UiDraw._lastFrameAt));
			UiDraw._lastFrameAt = totalSeconds;
			UiDraw.LastFrameRects = UiDraw.RectsThisFrame;
			UiDraw.RectsThisFrame = 0;
			UiDraw.FrameNo++;
			UiDraw.LastFrameSlots = UiDraw.TextSlotsThisFrame;
			UiDraw.LastFrameSkipped = UiDraw._skipped;
			UiDraw._skipped = 0;
			UiDraw.TextSlotsThisFrame = 0;
			UiDraw.GlyphsThisFrame = 0;
			UiDraw.FrameReserve = 0;
			if (UiDraw.CountOnly)
			{
				return;
			}
			if (UiDraw._nextAspectCheck-- <= 0)
			{
				UiDraw._nextAspectCheck = 120;
				UiDraw.ScreenAspect = (UiDraw.WindowAspect() ?? UiDraw.ScreenAspect);
			}
			UiDraw.UpdateFit();
		}

		// Token: 0x06001339 RID: 4921
		[DllImport("user32.dll")]
		private static extern bool GetClientRect(IntPtr hWnd, out UiDraw.RECT r);

		// Token: 0x0600133A RID: 4922 RVA: 0x0013196C File Offset: 0x0012FB6C
		private static float? WindowAspect()
		{
			try
			{
				IntPtr mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
				UiDraw.RECT rect;
				if (mainWindowHandle != IntPtr.Zero && UiDraw.GetClientRect(mainWindowHandle, out rect) && rect.Right > 0 && rect.Bottom > 0)
				{
					float num = (float)rect.Right / (float)rect.Bottom;
					if (num > 0.9f && num < 4f)
					{
						return new float?(num);
					}
				}
			}
			catch
			{
			}
			return null;
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x001319F8 File Offset: 0x0012FBF8
		public static bool EnsureTextureDict(string dict)
		{
			if (UiDraw.CountOnly)
			{
				return true;
			}
			if (TXD.HAS_STREAMED_TEXTURE_DICT_LOADED(dict))
			{
				return true;
			}
			TXD.REQUEST_STREAMED_TEXTURE_DICT(dict, false);
			return false;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00131A18 File Offset: 0x0012FC18
		public static void Sprite(string dict, string name, float cx, float cy, float w, float h, float heading, int r, int g, int b, int a)
		{
			UiDraw.RectsThisFrame++;
			if (UiDraw.CountOnly)
			{
				return;
			}
			Function.Call(14521943631680817972UL, new InputArgument[]
			{
				new InputArgument(UiDraw.Pin(dict)),
				new InputArgument(UiDraw.Pin(name)),
				UiDraw.MX(cx),
				UiDraw.MY(cy),
				w * UiDraw._kx,
				h * UiDraw._ky,
				heading,
				r,
				g,
				b,
				a,
				false
			});
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00131AE4 File Offset: 0x0012FCE4
		public static void SetDrawOrder(int order)
		{
			if (!UiDraw.CountOnly)
			{
				GRAPHICS.SET_SCRIPT_GFX_DRAW_ORDER(order);
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x00131AF3 File Offset: 0x0012FCF3
		// (set) Token: 0x0600133F RID: 4927 RVA: 0x00131AFA File Offset: 0x0012FCFA
		public static Action<float, float, float, float, int, int, int, int> RectHook { get; set; }

		// Token: 0x06001340 RID: 4928 RVA: 0x00131B04 File Offset: 0x0012FD04
		public static void Rect(float x, float y, float w, float h, int r, int g, int b, int a)
		{
			UiDraw.RectsThisFrame++;
			if (!UiDraw.CountOnly)
			{
				GRAPHICS.DRAW_RECT(UiDraw.MX(x + w / 2f), UiDraw.MY(y + h / 2f), w * UiDraw._kx, h * UiDraw._ky, r, g, b, a, true, false);
				return;
			}
			Action<float, float, float, float, int, int, int, int> rectHook = UiDraw.RectHook;
			if (rectHook == null)
			{
				return;
			}
			rectHook(x, y, w, h, r, g, b, a);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00131B7C File Offset: 0x0012FD7C
		public static void Text(string text, float x, float y, float scale, int r = 235, int g = 230, int b = 220, int a = 255)
		{
			if (UiDraw.GameFont)
			{
				UiDraw.TextFont(text, UiDraw.BodyFont, x, y, scale, r, g, b, a, false);
				return;
			}
			UiDraw.RawText(text, x, y, scale, r, g, b, a);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00131BBC File Offset: 0x0012FDBC
		private static void RawText(string text, float x, float y, float scale, int r, int g, int b, int a)
		{
			if (string.IsNullOrEmpty(text) || !UiDraw.TakeSlots(1))
			{
				return;
			}
			text = UiDraw.Safe(text);
			UiDraw.CountGlyphs(text);
			UIDEBUG._BG_SET_TEXT_SCALE(scale * UiDraw._ky, scale * UiDraw._ky);
			UIDEBUG._BG_SET_TEXT_COLOR(r, g, b, a);
			ulong num = Function.Call<ulong>(18055593637008912569UL, new InputArgument[]
			{
				10,
				new InputArgument(UiDraw.Pin("LITERAL_STRING")),
				new InputArgument(UiDraw.Pin(text))
			});
			Function.Call(1619411321388727128UL, new InputArgument[]
			{
				new InputArgument(num),
				UiDraw.MX(x),
				UiDraw.MY(y)
			});
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00131C80 File Offset: 0x0012FE80
		internal static string TheFix(string s)
		{
			if (s != null && (s.IndexOf("the the", StringComparison.OrdinalIgnoreCase) >= 0 || s.IndexOf("the  the", StringComparison.OrdinalIgnoreCase) >= 0))
			{
				return UiDraw.DoubleThe.Replace(s, "$1 ");
			}
			return s;
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x00131CB8 File Offset: 0x0012FEB8
		internal static string Safe(string s)
		{
			s = UiDraw.TheFix(s);
			if (s.IndexOfAny(UiDraw.MarkupChars) < 0)
			{
				return s;
			}
			return s.Replace("->", "to").Replace("<-", "from").Replace(">=", "at least").Replace("<=", "at most").Replace(" > ", " over ").Replace(" < ", " under ").Replace("~", "").Replace("<", "(").Replace(">", ")");
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x00131D68 File Offset: 0x0012FF68
		public static void Glyph(string ch, string font, float x, float y, float scale, int r, int g, int b, int a = 255)
		{
			if (string.IsNullOrEmpty(ch) || !UiDraw.TakeSlots(UiDraw.GameFont ? 2 : 1))
			{
				return;
			}
			UiDraw.CountGlyphs(ch);
			UIDEBUG._BG_SET_TEXT_SCALE(scale * UiDraw._ky, scale * UiDraw._ky);
			UIDEBUG._BG_SET_TEXT_COLOR(r, g, b, a);
			ulong num2;
			if (UiDraw.GameFont && !string.IsNullOrEmpty(font))
			{
				ulong num = Function.Call<ulong>(18055593637008912569UL, new InputArgument[]
				{
					10,
					new InputArgument(UiDraw.Pin("LITERAL_STRING")),
					new InputArgument(UiDraw.Pin(string.Concat(new string[]
					{
						"<FONT FACE='",
						font,
						"'>",
						ch,
						"</FONT>"
					})))
				});
				num2 = Function.Call<ulong>(18055593637008912569UL, new InputArgument[]
				{
					42,
					new InputArgument(UiDraw.Pin("COLOR_STRING")),
					0,
					new InputArgument(num)
				});
			}
			else
			{
				num2 = Function.Call<ulong>(18055593637008912569UL, new InputArgument[]
				{
					10,
					new InputArgument(UiDraw.Pin("LITERAL_STRING")),
					new InputArgument(UiDraw.Pin(ch))
				});
			}
			Function.Call(1619411321388727128UL, new InputArgument[]
			{
				new InputArgument(num2),
				UiDraw.MX(x),
				UiDraw.MY(y)
			});
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x00131EF4 File Offset: 0x001300F4
		public static void TextFont(string text, string font, float x, float y, float scale, int r = 235, int g = 230, int b = 220, int a = 255, bool underline = false)
		{
			if (!UiDraw.GameFont || string.IsNullOrEmpty(font))
			{
				UiDraw.RawText(text, x, y, scale, r, g, b, a);
				return;
			}
			if (string.IsNullOrEmpty(text) || !UiDraw.TakeSlots(2))
			{
				return;
			}
			text = UiDraw.Safe(text);
			UiDraw.CountGlyphs(text);
			if (underline)
			{
				text = "<U>" + text + "</U>";
			}
			UIDEBUG._BG_SET_TEXT_SCALE(scale * UiDraw._ky, scale * UiDraw._ky);
			UIDEBUG._BG_SET_TEXT_COLOR(r, g, b, a);
			ulong num = Function.Call<ulong>(18055593637008912569UL, new InputArgument[]
			{
				10,
				new InputArgument(UiDraw.Pin("LITERAL_STRING")),
				new InputArgument(UiDraw.Pin(string.Concat(new string[]
				{
					"<FONT FACE='",
					font,
					"'>",
					text,
					"</FONT>"
				})))
			});
			ulong num2 = Function.Call<ulong>(18055593637008912569UL, new InputArgument[]
			{
				42,
				new InputArgument(UiDraw.Pin("COLOR_STRING")),
				0,
				new InputArgument(num)
			});
			Function.Call(1619411321388727128UL, new InputArgument[]
			{
				new InputArgument(num2),
				UiDraw.MX(x),
				UiDraw.MY(y)
			});
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00132060 File Offset: 0x00130260
		public static void Outline(float x, float y, float w, float h, int r, int g, int b, int a, float t = 0.0012f)
		{
			float num = t * UiDraw.Aspect;
			UiDraw.Rect(x, y, w, num, r, g, b, a);
			UiDraw.Rect(x, y + h - num, w, num, r, g, b, a);
			UiDraw.Rect(x, y, t, h, r, g, b, a);
			UiDraw.Rect(x + w - t, y, t, h, r, g, b, a);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x001320C5 File Offset: 0x001302C5
		public static void HLine(float x1, float x2, float y, int r, int g, int b, int a, float t = 0.0012f)
		{
			UiDraw.Rect(Math.Min(x1, x2), y - t * UiDraw.Aspect / 2f, Math.Abs(x2 - x1), t * UiDraw.Aspect, r, g, b, a);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x001320FA File Offset: 0x001302FA
		public static void VLine(float x, float y1, float y2, int r, int g, int b, int a, float t = 0.0012f)
		{
			UiDraw.Rect(x - t / 2f, Math.Min(y1, y2), t, Math.Abs(y2 - y1), r, g, b, a);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x00132124 File Offset: 0x00130324
		public static void Line(float x1, float y1, float x2, float y2, float t, int r, int g, int b, int a = 255, int maxPieces = 40)
		{
			float aspect = UiDraw.Aspect;
			float num = x1 * aspect;
			float num2 = (x2 - x1) * aspect;
			float num3 = y2 - y1;
			float num4 = (float)Math.Sqrt((double)(num2 * num2 + num3 * num3));
			if (num4 < 1E-05f)
			{
				UiDraw.Rect((num - t / 2f) / aspect, y1 - t / 2f, t / aspect, t, r, g, b, a);
				return;
			}
			float num5 = num2 / num4 * t / 2f;
			float num6 = num3 / num4 * t / 2f;
			num -= num5;
			float num7 = y1 - num6;
			num2 += 2f * num5;
			num3 += 2f * num6;
			bool flag = Math.Abs(num2) >= Math.Abs(num3);
			float num8 = flag ? Math.Abs(num3) : Math.Abs(num2);
			int num9 = Math.Max(1, Math.Min(maxPieces, (int)Math.Ceiling((double)(num8 / t))));
			for (int i = 0; i < num9; i++)
			{
				float num10 = (float)i / (float)num9;
				float num11 = (float)(i + 1) / (float)num9;
				float num12 = (num10 + num11) / 2f;
				float val = num + num2 * num10;
				float val2 = num + num2 * num11;
				float val3 = num7 + num3 * num10;
				float val4 = num7 + num3 * num11;
				float num13;
				float num14;
				float num17;
				float num18;
				if (flag)
				{
					num13 = Math.Min(val, val2);
					num14 = Math.Max(val, val2);
					float num15 = num7 + num3 * num12;
					float num16 = t / 2f;
					num17 = num15 - num16;
					num18 = num15 + num16;
				}
				else
				{
					num17 = Math.Min(val3, val4);
					num18 = Math.Max(val3, val4);
					float num19 = num + num2 * num12;
					float num20 = t / 2f;
					num13 = num19 - num20;
					num14 = num19 + num20;
				}
				UiDraw.Rect(num13 / aspect, num17, (num14 - num13) / aspect, num18 - num17, r, g, b, a);
			}
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x001322F4 File Offset: 0x001304F4
		public static int LineCost(float x1, float y1, float x2, float y2, float t, int maxPieces = 40)
		{
			float num = (x2 - x1) * UiDraw.Aspect;
			float num2 = y2 - y1;
			float num3 = (float)Math.Sqrt((double)(num * num + num2 * num2));
			if (num3 < 1E-05f)
			{
				return 1;
			}
			num = Math.Abs(num) + Math.Abs(num) / num3 * t;
			num2 = Math.Abs(num2) + Math.Abs(num2) / num3 * t;
			return Math.Max(1, Math.Min(maxPieces, (int)Math.Ceiling((double)(Math.Min(num, num2) / t))));
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x0013236C File Offset: 0x0013056C
		public static bool Clip(ref float x1, ref float y1, ref float x2, ref float y2, float minX, float minY, float maxX, float maxY)
		{
			float num = x2 - x1;
			float num2 = y2 - y1;
			float num3 = 0f;
			float num4 = 1f;
			float[] array = new float[]
			{
				-num,
				num,
				-num2,
				num2
			};
			float[] array2 = new float[]
			{
				x1 - minX,
				maxX - x1,
				y1 - minY,
				maxY - y1
			};
			for (int i = 0; i < 4; i++)
			{
				if (Math.Abs(array[i]) < 1E-09f)
				{
					if (array2[i] < 0f)
					{
						return false;
					}
				}
				else
				{
					float num5 = array2[i] / array[i];
					if (array[i] < 0f)
					{
						if (num5 > num4)
						{
							return false;
						}
						if (num5 > num3)
						{
							num3 = num5;
						}
					}
					else
					{
						if (num5 < num3)
						{
							return false;
						}
						if (num5 < num4)
						{
							num4 = num5;
						}
					}
				}
			}
			float num6 = x1 + num * num3;
			float num7 = y1 + num2 * num3;
			x2 = x1 + num * num4;
			y2 = y1 + num2 * num4;
			x1 = num6;
			y1 = num7;
			return true;
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00132464 File Offset: 0x00130664
		public static void Blob(float cx, float cy, float radius, int r, int g, int b, int a)
		{
			float aspect = UiDraw.Aspect;
			UiDraw.Rect(cx - radius * 0.7f / aspect, cy - radius, radius * 1.4f / aspect, radius * 2f, r, g, b, a);
			UiDraw.Rect(cx - radius / aspect, cy - radius * 0.7f, radius * 2f / aspect, radius * 1.4f, r, g, b, a);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x001324CB File Offset: 0x001306CB
		[return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static ValueTuple<int, int, int> HateColor(HateTier tier)
		{
			if (tier == HateTier.Murderous)
			{
				return new ValueTuple<int, int, int>(205, 55, 45);
			}
			if (tier != HateTier.Vengeful)
			{
				return new ValueTuple<int, int, int>(205, 185, 100);
			}
			return new ValueTuple<int, int, int>(215, 135, 50);
		}

		// Token: 0x04000CDB RID: 3291
		private const ulong VarStringHash = 18055593637008912569UL;

		// Token: 0x04000CDC RID: 3292
		private const ulong BgDisplayTextHash = 1619411321388727128UL;

		// Token: 0x04000CDD RID: 3293
		private static readonly Dictionary<string, UiDraw.NativeString> Strings = new Dictionary<string, UiDraw.NativeString>();

		// Token: 0x04000CDE RID: 3294
		private static int _nextSweepFrame;

		// Token: 0x04000CDF RID: 3295
		private static readonly Dictionary<string, IntPtr> Forever = new Dictionary<string, IntPtr>();

		// Token: 0x04000CE2 RID: 3298
		private static readonly char[] MarkupChars = new char[]
		{
			'~',
			'<',
			'>'
		};

		// Token: 0x04000CE3 RID: 3299
		public static int TextSlotLimit = 230;

		// Token: 0x04000CE5 RID: 3301
		public static int FrameReserve;

		// Token: 0x04000CEA RID: 3306
		private static int _skipped;

		// Token: 0x04000CEC RID: 3308
		public static float ScreenAspect = 1.7777778f;

		// Token: 0x04000CED RID: 3309
		public static bool FitTo169 = true;

		// Token: 0x04000CEE RID: 3310
		private static float _kx = 1f;

		// Token: 0x04000CEF RID: 3311
		private static float _ky = 1f;

		// Token: 0x04000CF0 RID: 3312
		private static readonly Stopwatch Clock = Stopwatch.StartNew();

		// Token: 0x04000CF1 RID: 3313
		private static double _lastFrameAt;

		// Token: 0x04000CF3 RID: 3315
		private static int _nextAspectCheck;

		// Token: 0x04000CF4 RID: 3316
		private const ulong DrawSpriteHash = 14521943631680817972UL;

		// Token: 0x04000CF6 RID: 3318
		private static readonly Regex DoubleThe = new Regex("\\b(the)\\s+the\\s+", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

		// Token: 0x04000CF7 RID: 3319
		public static bool GameFont = true;

		// Token: 0x04000CF8 RID: 3320
		public static string BodyFont = "$body";

		// Token: 0x04000CF9 RID: 3321
		public static string TitleFont = "$title";

		// Token: 0x04000CFA RID: 3322
		public static bool MarkupUnderline = true;

		// Token: 0x04000CFB RID: 3323
		private const int MaxLinePieces = 40;

		// Token: 0x02000466 RID: 1126
		private sealed class NativeString
		{
			// Token: 0x04001FFA RID: 8186
			public IntPtr Ptr;

			// Token: 0x04001FFB RID: 8187
			public int LastUsedFrame;
		}

		// Token: 0x02000467 RID: 1127
		private struct RECT
		{
			// Token: 0x04001FFC RID: 8188
			public int Left;

			// Token: 0x04001FFD RID: 8189
			public int Top;

			// Token: 0x04001FFE RID: 8190
			public int Right;

			// Token: 0x04001FFF RID: 8191
			public int Bottom;
		}
	}
}
