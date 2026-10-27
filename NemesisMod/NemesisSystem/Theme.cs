using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using RDR2.Native;

namespace NemesisSystem
{
	// Token: 0x02000075 RID: 117
	internal static class Theme
	{
		// Token: 0x060012B0 RID: 4784 RVA: 0x0012AD7C File Offset: 0x00128F7C
		[return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static ValueTuple<int, int, int> InkHate(HateTier t)
		{
			if (t == HateTier.Murderous)
			{
				return new ValueTuple<int, int, int>(150, 24, 18);
			}
			if (t != HateTier.Vengeful)
			{
				return new ValueTuple<int, int, int>(112, 92, 22);
			}
			return new ValueTuple<int, int, int>(160, 82, 12);
		}

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060012B1 RID: 4785 RVA: 0x0012ADAF File Offset: 0x00128FAF
		public static bool BackdropOn
		{
			get
			{
				return Theme._backdrop;
			}
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0012ADB8 File Offset: 0x00128FB8
		public static void SetBackdrop(bool on)
		{
			if (on == Theme._backdrop)
			{
				return;
			}
			Theme._backdrop = on;
			Function.Call(on ? 4684433203668451423UL : 13041707943063647124UL, new InputArgument[]
			{
				new InputArgument(UiDraw.CString("PauseMenuIn"))
			});
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x0012AE08 File Offset: 0x00129008
		public static void HRule(float x, float y, float w, int r, int g, int b, int a, int px = 1)
		{
			UiDraw.Rect(x, y, w, 0.0009259259f * (float)px, r, g, b, a);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0012AE22 File Offset: 0x00129022
		public static void VRule(float x, float y, float h, int r, int g, int b, int a, int px = 1)
		{
			UiDraw.Rect(x, y, 0.00052083336f * (float)px, h, r, g, b, a);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0012AE3C File Offset: 0x0012903C
		public static void Hairline(float x, float y, float w, float h, int r, int g, int b, int a, int px = 1)
		{
			Theme.HRule(x, y, w, r, g, b, a, px);
			Theme.HRule(x, y + h - 0.0009259259f * (float)px, w, r, g, b, a, px);
			Theme.VRule(x, y, h, r, g, b, a, px);
			Theme.VRule(x + w - 0.00052083336f * (float)px, y, h, r, g, b, a, px);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x0012AEAC File Offset: 0x001290AC
		public static void Corners(float x, float y, float w, float h, int a = 230, float len = 0.012f)
		{
			float num = len * UiDraw.Aspect;
			ValueTuple<int, int, int> gold = Theme.Gold;
			int item = gold.Item1;
			int item2 = gold.Item2;
			int item3 = gold.Item3;
			Theme.HRule(x, y, len, item, item2, item3, a, 2);
			Theme.VRule(x, y, num, item, item2, item3, a, 2);
			Theme.HRule(x + w - len, y, len, item, item2, item3, a, 2);
			Theme.VRule(x + w - 0.0010416667f, y, num, item, item2, item3, a, 2);
			Theme.HRule(x, y + h - 0.0018518518f, len, item, item2, item3, a, 2);
			Theme.VRule(x, y + h - num, num, item, item2, item3, a, 2);
			Theme.HRule(x + w - len, y + h - 0.0018518518f, len, item, item2, item3, a, 2);
			Theme.VRule(x + w - 0.0010416667f, y + h - num, num, item, item2, item3, a, 2);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x0012AF84 File Offset: 0x00129184
		public static void Disc(float cx, float cy, float d, int r, int g, int b, int a)
		{
			float num = d / 2f;
			float num2 = d / 24f;
			for (int i = 0; i < 24; i++)
			{
				float num3 = -num + num2 * ((float)i + 0.5f);
				float num4 = (float)Math.Sqrt((double)Math.Max(0f, num * num - num3 * num3)) / UiDraw.Aspect;
				UiDraw.Rect(cx - num4, cy - num + num2 * (float)i, num4 * 2f, num2 + 0.00046296295f, r, g, b, a);
			}
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x0012B003 File Offset: 0x00129203
		private static bool InkReady()
		{
			return Theme.InkOn && !string.IsNullOrEmpty(Theme.InkDict) && UiDraw.EnsureTextureDict(Theme.InkDict);
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x0012B024 File Offset: 0x00129224
		private static bool InWindow
		{
			get
			{
				return Theme._windowFrame == UiDraw.FrameNo;
			}
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x0012B034 File Offset: 0x00129234
		public static void InkBehind(float x, float y, float w, float h, int alpha = 255, bool tight = false)
		{
			if (!Theme.InkReady())
			{
				return;
			}
			float w2 = tight ? (w * 1.05f + 0.012f) : (w * 1.1f + 0.03f);
			float h2 = tight ? (h * 1.02f + 0.006f) : (h * 1.16f + 0.045f);
			UiDraw.Sprite(Theme.InkDict, Theme.InkTexture, x + w / 2f, y + h / 2f, w2, h2, 0f, 0, 0, 0, alpha);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x0012B0B8 File Offset: 0x001292B8
		public static bool Window(float y0, float y1)
		{
			if (!Theme.InkReady())
			{
				return false;
			}
			Theme.InkBehind(0.07f, y0, 0.86f, y1 - y0, 255, false);
			Theme._windowFrame = UiDraw.FrameNo;
			return true;
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0012B0E7 File Offset: 0x001292E7
		private static void Bg(float x, float y, float w, float h, int alpha)
		{
			if (Theme.InWindow)
			{
				return;
			}
			UiDraw.Rect(x, y, w, h, 0, 0, 0, alpha);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0012B100 File Offset: 0x00129300
		public static void DebugPanel(float x, float y, float w, float h, int alpha = 215)
		{
			UiDraw.Rect(x, y, w, h, Theme.Panel.Item1, Theme.Panel.Item2, Theme.Panel.Item3, Math.Max(alpha, 205));
			Theme.Hairline(x, y, w, h, Theme.Border.Item1, Theme.Border.Item2, Theme.Border.Item3, 230, 1);
			Theme.Corners(x, y, w, h, 230, 0.012f);
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x0012B180 File Offset: 0x00129380
		public static void PanelRect(float x, float y, float w, float h, int alpha = 215)
		{
			if (Theme.InWindow)
			{
				return;
			}
			if (Theme.InkReady())
			{
				Theme.InkBehind(x, y, w, h, 255, false);
				return;
			}
			UiDraw.Rect(x, y, w, h, 0, 0, 0, Math.Max(alpha, 228));
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x0012B1C4 File Offset: 0x001293C4
		public static void Brush(float x, float y, float w, float h, int alpha)
		{
			if (Theme.InkReady())
			{
				Theme.InkBehind(x, y, w, h, alpha, true);
				if (UiDraw.EnsureTextureDict(Theme.InkDict))
				{
					UiDraw.Sprite(Theme.InkDict, Theme.InkTexture, x + w / 2f, y + h / 2f, w * 1.01f + 0.004f, h * 1.04f + 0.01f, 0f, 0, 0, 0, alpha);
				}
				return;
			}
			UiDraw.Rect(x, y, w, h, 0, 0, 0, alpha * 232 / 255);
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x0012B250 File Offset: 0x00129450
		public static void CardRect(float x, float y, float w, float h, int alpha = 240, bool selected = false)
		{
			ValueTuple<int, int, int> valueTuple = selected ? Theme.CardHi : Theme.Card;
			UiDraw.Rect(x, y, w, h, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, alpha);
			if (selected)
			{
				Theme.RedFrame(x, y, w, h, 1f, 0);
				return;
			}
			Theme.Hairline(x, y, w, h, Theme.Border.Item1, Theme.Border.Item2, Theme.Border.Item3, alpha * 3 / 5, 1);
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x0012B2CD File Offset: 0x001294CD
		public static void SelectBox(float x, float y, float w, float h, int alpha = 235)
		{
			UiDraw.Rect(x, y, w, h, Theme.Selected.Item1, Theme.Selected.Item2, Theme.Selected.Item3, alpha);
			Theme.RedFrame(x, y, w, h, 1f, 0);
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x0012B307 File Offset: 0x00129507
		public static void SelectFill(float x, float y, float w, float h, int alpha = 235)
		{
			UiDraw.Rect(x, y, w, h, Theme.Selected.Item1, Theme.Selected.Item2, Theme.Selected.Item3, alpha);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x0012B334 File Offset: 0x00129534
		public static void RedFrame(float x, float y, float w, float h, float scale = 1f, int extraOut = 0)
		{
			if (!Theme.FrameOn || !UiDraw.EnsureTextureDict("menu_textures"))
			{
				Theme.Hairline(x, y, w, h, Theme.NativeRed.Item1, Theme.NativeRed.Item2, Theme.NativeRed.Item3, 255, (scale > 1.2f) ? 3 : 2);
				return;
			}
			float num = ((float)(Theme.FrameOut + extraOut) + (float)Theme.FrameEdge * scale * (float)Theme.FrameInsetX / 100f) * 0.00052083336f;
			float num2 = (float)(Theme.FrameOut + extraOut) * 0.0009259259f;
			float num3 = x - num;
			float num4 = y - num2;
			float num5 = x + w + num;
			float num6 = y + h + num2 + (float)Theme.FrameEdge * scale * (float)Theme.FrameInsetY / 100f * 0.0009259259f;
			float num7 = (float)Theme.FrameSize * scale * 0.00052083336f;
			float num8 = (float)Theme.FrameSize * scale * 0.0009259259f;
			num7 = Math.Min(num7, (num5 - num3) / 2f);
			num8 = Math.Min(num8, (num6 - num4) / 2f);
			float num9 = (float)Theme.FrameEdge * scale * 0.00052083336f;
			float num10 = (float)Theme.FrameEdge * scale * 0.0009259259f;
			int r = 255;
			int g = 255;
			int b = 255;
			if (Theme.FrameTint)
			{
				r = Theme.NativeRed.Item1;
				g = Theme.NativeRed.Item2;
				b = Theme.NativeRed.Item3;
			}
			float num11 = num5 - num3 - num7;
			float num12 = num6 - num4 - num8;
			if (num11 > 0f)
			{
				Theme.S("crafting_highlight_t", (num3 + num5) / 2f, num4 + num10 / 2f, num11, num10, r, g, b);
				Theme.S("crafting_highlight_b", (num3 + num5) / 2f, num6 - num10 / 2f, num11, num10, r, g, b);
			}
			if (num12 > 0f)
			{
				Theme.S("crafting_highlight_l", num3 + num9 / 2f, (num4 + num6) / 2f, num9, num12, r, g, b);
				Theme.S("crafting_highlight_r", num5 - num9 / 2f, (num4 + num6) / 2f, num9, num12, r, g, b);
			}
			Theme.S("crafting_highlight_tl", num3 + num7 / 2f, num4 + num8 / 2f, num7, num8, r, g, b);
			Theme.S("crafting_highlight_tr", num5 - num7 / 2f, num4 + num8 / 2f, num7, num8, r, g, b);
			Theme.S("crafting_highlight_bl", num3 + num7 / 2f, num6 - num8 / 2f, num7, num8, r, g, b);
			Theme.S("crafting_highlight_br", num5 - num7 / 2f, num6 - num8 / 2f, num7, num8, r, g, b);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0012B60C File Offset: 0x0012980C
		private static void S(string n, float cx, float cy, float w, float h, int r, int g, int b)
		{
			UiDraw.Sprite("menu_textures", n, cx, cy, w, h, 0f, r, g, b, 255);
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x0012B639 File Offset: 0x00129839
		public static void CenterRule(float x, float y, float w, float inset = 0.03f, int alpha = 170)
		{
			UiDraw.Rect(x + inset, y, w - inset * 2f, 0.0018518518f, Theme.Rule.Item1, Theme.Rule.Item2, Theme.Rule.Item3, alpha);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x0012B674 File Offset: 0x00129874
		public static void Divider(float x, float y, float w)
		{
			Theme.HRule(x, y, w, Theme.Line.Item1, Theme.Line.Item2, Theme.Line.Item3, 200, 1);
			Theme.HRule(x, y + 0.0027777776f, w, Theme.Faint.Item1, Theme.Faint.Item2, Theme.Faint.Item3, 160, 1);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0012B6E0 File Offset: 0x001298E0
		public static void Label(string text, float x, float y, float scale = 0.26f)
		{
			UiDraw.TextFont(text, UiDraw.TitleFont, x, y, scale, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 255, false);
		}

		// Token: 0x060012C8 RID: 4808 RVA: 0x0012B71F File Offset: 0x0012991F
		public static void Body(string text, float x, float y, float scale = 0.28f)
		{
			UiDraw.Text(text, x, y, scale, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0012B750 File Offset: 0x00129950
		public static void Section(string text, float x, float y, float right, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int>? accent = null)
		{
			UiDraw.TextFont(text, UiDraw.TitleFont, x + 0.004f, y, 0.25f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			float num = x + 0.004f + Theme.CapsWidth(text, 0.25f) + 0.01f;
			if (right > num)
			{
				Theme.HRule(num, y + 0.011f, right - num, Theme.Rule.Item1, Theme.Rule.Item2, Theme.Rule.Item3, 110, 1);
			}
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x0012B7E8 File Offset: 0x001299E8
		public static float Chip(string text, float x, float y, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> color, bool filled = false)
		{
			float num = Theme.CapsWidth(text, 0.21f);
			float num2 = num + 0.014f;
			float num3 = 0.022f;
			if (filled)
			{
				UiDraw.Rect(x, y, num2, num3, color.Item1, color.Item2, color.Item3, 235);
			}
			else
			{
				Theme.Hairline(x, y, num2, num3, color.Item1, color.Item2, color.Item3, 235, 1);
			}
			ValueTuple<int, int, int> valueTuple = filled ? new ValueTuple<int, int, int>(14, 14, 13) : color;
			UiDraw.TextFont(text, UiDraw.TitleFont, x + (num2 - num) / 2f - 0.0008f, y + num3 / 2f - 0.007875f + 0.0004f, 0.21f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255, false);
			return num2;
		}

		// Token: 0x060012CB RID: 4811 RVA: 0x0012B8B8 File Offset: 0x00129AB8
		public static float Tag(string text, float x, float y, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> color)
		{
			float num = Theme.MixedWidth(text, 0.17f, 0.0222f);
			float num2 = num + 0.014f;
			UiDraw.Rect(x, y, num2, 0.02f, color.Item1 / 7, color.Item2 / 7, color.Item3 / 7, 220);
			Theme.Hairline(x, y, num2, 0.02f, color.Item1, color.Item2, color.Item3, 200, 1);
			UiDraw.Text(text, x + (num2 - num) / 2f - 0.0015f, y + 0.01f - 0.0063750003f - 0.0012f, 0.17f, color.Item1, color.Item2, color.Item3, 255);
			return num2;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x0012B974 File Offset: 0x00129B74
		public static void Badge(string text, float x, float y, float w, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> color, bool filled)
		{
			if (filled)
			{
				UiDraw.Rect(x, y, w, 0.024f, color.Item1, color.Item2, color.Item3, 235);
			}
			else
			{
				UiDraw.Rect(x, y, w, 0.024f, color.Item1 / 6, color.Item2 / 6, color.Item3 / 6, 220);
				Theme.Hairline(x, y, w, 0.024f, color.Item1, color.Item2, color.Item3, 235, 1);
			}
			ValueTuple<int, int, int> valueTuple = filled ? new ValueTuple<int, int, int>(14, 14, 13) : color;
			float num = Theme.CapsWidth(text, 0.2f);
			UiDraw.TextFont(text, UiDraw.TitleFont, x + (w - num) / 2f - 0.0008f, y + 0.012f - 0.0075000003f + 0.0008f, 0.2f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255, false);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0012BA70 File Offset: 0x00129C70
		public static float CapsWidth(string s, float scale)
		{
			if (string.IsNullOrEmpty(s))
			{
				return 0f;
			}
			float num = 0f;
			foreach (char c in s)
			{
				char c2 = char.ToUpperInvariant(c);
				if (c2 != ' ')
				{
					if (c2 != '\'')
					{
						switch (c2)
						{
						case ',':
						case '.':
						case ':':
							num += 0.35f;
							goto IL_1C3;
						case '-':
							num += 0.55f;
							goto IL_1C3;
						case 'B':
						case 'P':
						case 'R':
							num += 0.92f;
							goto IL_1C3;
						case 'C':
						case 'K':
						case 'Y':
							num += 0.97f;
							goto IL_1C3;
						case 'D':
						case 'G':
							num += 1.07f;
							goto IL_1C3;
						case 'E':
						case 'S':
						case 'T':
						case 'Z':
							num += 0.85f;
							goto IL_1C3;
						case 'F':
							num += 0.85f;
							goto IL_1C3;
						case 'I':
							num += 0.55f;
							goto IL_1C3;
						case 'J':
							num += 0.6f;
							goto IL_1C3;
						case 'L':
							num += 0.8f;
							goto IL_1C3;
						case 'M':
							num += 1.3f;
							goto IL_1C3;
						case 'O':
						case 'Q':
							num += 1.06f;
							goto IL_1C3;
						case 'W':
							num += 1.45f;
							goto IL_1C3;
						}
						num += (char.IsDigit(c) ? 0.9f : 1f);
					}
					else
					{
						num += 0.18f;
					}
				}
				else
				{
					num += 0.45f;
				}
				IL_1C3:;
			}
			return num * scale * 0.0205f;
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0012BC5C File Offset: 0x00129E5C
		public static string Marquee(string s, float width, Func<string, float> w)
		{
			if (!Theme.MarqueeOn)
			{
				return null;
			}
			int realMs = UiDraw.RealMs;
			if (Theme._marquee.Count > 400)
			{
				Theme._marquee.Clear();
				Theme._marqueeEnd.Clear();
				Theme._marqueeCut.Clear();
			}
			string key = s + "|" + ((int)(width * 10000f)).ToString();
			ValueTuple<int, int> valueTuple;
			if (!Theme._marquee.TryGetValue(key, out valueTuple) || realMs - valueTuple.Item2 > 500)
			{
				valueTuple = new ValueTuple<int, int>(realMs, realMs);
			}
			valueTuple.Item2 = realMs;
			int num;
			if (!Theme._marqueeEnd.TryGetValue(key, out num))
			{
				num = 1;
				while (num < s.Length - 1 && w("..." + s.Substring(num)) > width)
				{
					num++;
				}
				Theme._marqueeEnd[key] = num;
			}
			int num2 = realMs - valueTuple.Item1;
			if (num2 > 2000 + num * 110 + 2000)
			{
				valueTuple.Item1 = realMs;
				num2 = 0;
			}
			Theme._marquee[key] = valueTuple;
			int num3 = Math.Min(num, Math.Max(0, (num2 - 2000) / 110));
			if (num3 <= 0)
			{
				return null;
			}
			ValueTuple<int, string> valueTuple2;
			if (Theme._marqueeCut.TryGetValue(key, out valueTuple2) && valueTuple2.Item1 == num3)
			{
				return valueTuple2.Item2;
			}
			string text = s.Substring(num3);
			while (text.Length > 1 && w("..." + text) > width)
			{
				text = text.Substring(0, text.Length - 1);
			}
			Theme._marqueeCut[key] = new ValueTuple<int, string>(num3, "..." + text);
			return "..." + text;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0012BE1C File Offset: 0x0012A01C
		public static float MixedWidth(string s, float scale, float unit)
		{
			if (string.IsNullOrEmpty(s))
			{
				return 0f;
			}
			float num = 0f;
			foreach (char c in s)
			{
				if (char.IsUpper(c))
				{
					num += Theme.CapsWidth(c.ToString(), 1f) / 0.0205f;
				}
				else
				{
					if (c <= 'm')
					{
						if (c <= '.')
						{
							if (c == ' ')
							{
								goto IL_DE;
							}
							switch (c)
							{
							case '\'':
							case ',':
							case '.':
								break;
							case '(':
							case ')':
							case '-':
								goto IL_DE;
							case '*':
							case '+':
								goto IL_F2;
							default:
								goto IL_F2;
							}
						}
						else if (c != ':')
						{
							switch (c)
							{
							case 'f':
								goto IL_DE;
							case 'g':
							case 'h':
							case 'k':
								goto IL_F2;
							case 'i':
							case 'j':
							case 'l':
								break;
							case 'm':
								goto IL_E8;
							default:
								goto IL_F2;
							}
						}
					}
					else if (c <= 't')
					{
						if (c != 'r' && c != 't')
						{
							goto IL_F2;
						}
						goto IL_DE;
					}
					else
					{
						if (c == 'w')
						{
							goto IL_E8;
						}
						if (c != '|')
						{
							goto IL_F2;
						}
					}
					num += 0.36f;
					goto IL_109;
					IL_DE:
					num += 0.5f;
					goto IL_109;
					IL_E8:
					num += 1.15f;
					goto IL_109;
					IL_F2:
					num += (char.IsDigit(c) ? 0.82f : 0.78f);
				}
				IL_109:;
			}
			return num * scale * unit;
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0012BF48 File Offset: 0x0012A148
		public static float TitleWidth(string s, float scale)
		{
			if (string.IsNullOrEmpty(s))
			{
				return 0f;
			}
			float num = 0f;
			foreach (char c in s)
			{
				num += (char.IsLower(c) ? (Theme.CapsWidth(c.ToString(), 1f) * 0.8f) : Theme.CapsWidth(c.ToString(), 1f));
			}
			return num * scale;
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0012BFBC File Offset: 0x0012A1BC
		public static float TextWidth(string s, float scale)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return (float)s.Length * scale * 0.0185f;
			}
			return 0f;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0012BFDC File Offset: 0x0012A1DC
		public static void Header(string title, string subtitle, MenuTab active, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int>? accent = null)
		{
			float num = 0.07f;
			float y = 0.04f;
			float h = 0.095f;
			Theme.Bg(num, y, 0.86f, h, 232);
			string text = title.ToUpperInvariant();
			float num2 = 0.54f;
			while (num2 > 0.36f && Theme.CapsWidth(text, num2) > 0.30960003f)
			{
				num2 -= 0.02f;
			}
			float num3 = 0.5f - Theme.CapsWidth(text, num2) / 2f;
			float num4 = 0.060000002f;
			UiDraw.TextFont(text, UiDraw.TitleFont, num3 + 0.00052083336f, num4, num2, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			UiDraw.TextFont(text, UiDraw.TitleFont, num3, num4, num2, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			if (!string.IsNullOrEmpty(subtitle))
			{
				float num5 = 0.344f;
				string text2 = subtitle;
				while (text2.Length > 4 && Theme.MixedWidth(text2, 0.22f, 0.0222f) > num5)
				{
					text2 = text2.Substring(0, text2.Length - 4).TrimEnd(new char[]
					{
						' ',
						'-'
					}) + "...";
				}
				UiDraw.Text(text2, 0.5f - Theme.MixedWidth(text2, 0.22f, 0.0222f) / 2f, num4 + num2 * 0.075f + 0.001f, 0.22f, Theme.Subtitle.Item1, Theme.Subtitle.Item2, Theme.Subtitle.Item3, 255);
			}
			string key = XPad.RecentlyUsed ? "Vista" : "Tab";
			float num6 = num + 0.016f;
			float num7 = 0.06725f;
			num6 += Theme.Glyph(key, num6, num7 + 0.002f) + 0.01f;
			ValueTuple<string, MenuTab>[] array = new ValueTuple<string, MenuTab>[]
			{
				new ValueTuple<string, MenuTab>("JERARQUÍA", MenuTab.Hierarchy),
				new ValueTuple<string, MenuTab>("MUNDO", MenuTab.World),
				new ValueTuple<string, MenuTab>("RAYOS X", MenuTab.XRay)
			};
			for (int i = 0; i < array.Length; i++)
			{
				ValueTuple<string, MenuTab> valueTuple = array[i];
				string item = valueTuple.Item1;
				MenuTab item2 = valueTuple.Item2;
				Theme.DrawTab(item, num6, num7, 0.34f, active == item2);
				MenuTab t2 = item2;
				UiMouse.Region(num6 - 0.005f, num7 - 0.004f, Theme.CapsWidth(item, 0.34f) + 0.01f, 0.034f, null, delegate
				{
					Action<MenuTab> tabClick = UiMouse.TabClick;
					if (tabClick == null)
					{
						return;
					}
					tabClick(t2);
				});
				num6 += Theme.CapsWidth(item, 0.34f) + 0.022f;
			}
			string key2 = XPad.RecentlyUsed ? "Vista" : "Tab";
			string text3 = "MANTENER: BUSCAR";
			float num8 = Theme.GlyphWidth(key2) + 0.008f + Theme.CapsWidth(text3, 0.26f);
			float num9 = num + 0.86f - 0.016f - num8;
			float num10 = 0.07025f;
			num9 += Theme.Glyph(key2, num9, num10 + 0.002f) + 0.008f;
			UiDraw.TextFont(text3, UiDraw.TitleFont, num9, num10 + 0.004f, 0.26f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
			Theme.CenterRule(num, 0.131f, 0.86f, 0.012f, 190);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0012C380 File Offset: 0x0012A580
		private static void DrawTab(string text, float x, float y, float s, bool on)
		{
			ValueTuple<int, int, int> valueTuple = on ? Theme.Title : Theme.Dim;
			bool flag = on && UiDraw.MarkupUnderline && UiDraw.GameFont;
			UiDraw.TextFont(text, UiDraw.TitleFont, x, y, s, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255, flag);
			if (on && !flag)
			{
				float num = Theme.CapsWidth(text, s);
				float num2 = num * 0.86f;
				Theme.HRule(x + (num - num2) / 2f, y + 0.028f, num2, Theme.NativeRed.Item1, Theme.NativeRed.Item2, Theme.NativeRed.Item3, 255, 2);
			}
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x0012C42C File Offset: 0x0012A62C
		internal static bool MenuArrow(Theme.ArrowDir dir, float cx, float cy, float h, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> c, int alpha = 255)
		{
			if (!UiDraw.EnsureTextureDict("menu_textures"))
			{
				return false;
			}
			float w = h / UiDraw.Aspect;
			string name = (dir == Theme.ArrowDir.Left) ? "selection_arrow_left" : "selection_arrow_right";
			UiDraw.Sprite("menu_textures", name, cx, cy, w, h, (dir == Theme.ArrowDir.Down) ? 90f : 0f, c.Item1, c.Item2, c.Item3, alpha);
			return true;
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x0012C498 File Offset: 0x0012A698
		private static void BarChevron(float x, float cy, bool left)
		{
			float num = 0.0015f;
			float num2 = 0.018f;
			for (int i = 0; i < 5; i++)
			{
				float num3 = num2 * (float)(5 - i) / 5f;
				UiDraw.Rect(left ? (x + (float)i * num) : (x + (float)(4 - i) * num), cy - num3 / 2f, num, num3, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 220);
			}
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x0012C510 File Offset: 0x0012A710
		public static void FactionBar(float y, IList<string> names, int active, bool isGang, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> accent, string status, string chipText = null, bool chipAsText = false, Action<int> pick = null)
		{
			Theme.<>c__DisplayClass100_0 CS$<>8__locals1 = new Theme.<>c__DisplayClass100_0();
			CS$<>8__locals1.pick = pick;
			float num = 0.07f;
			float num2 = 0.04f;
			Theme.Bg(num, y, 0.86f, num2, 232);
			Theme.CenterRule(num, y + num2 - 0.0018518518f, 0.86f, 0.012f, 120);
			bool recentlyUsed = XPad.RecentlyUsed;
			string key = recentlyUsed ? "LB" : "Q";
			string key2 = recentlyUsed ? "RB" : "E";
			CS$<>8__locals1.ty = y + 0.008f;
			List<string> list = (from n in names
			select n.ToUpperInvariant()).ToList<string>();
			float num3 = num + 0.86f - 0.014f;
			string text = chipText ?? (isGang ? "JERARQUÍA DE LA BANDA" : "NÉMESIS SOLITARIOS");
			float num4 = chipAsText ? Theme.MixedWidth(text, 0.22f, 0.0205f) : 0f;
			float num5 = num + 0.016f;
			float num6 = (chipAsText ? (num3 - num4 - 0.02f) : (num + 0.86f - 0.016f)) - Theme.GlyphWidth(key2);
			float num7 = num5 + Theme.GlyphWidth(key) + 0.02f;
			float num8 = num6 - 0.02f;
			float num9 = num8 - num7;
			int num10 = 0;
			int num11 = list.Count - 1;
			while (num11 > num10 && Theme.<FactionBar>g__W|100_1(list.GetRange(num10, num11 - num10 + 1)) + 0.016f * (float)(num11 - num10) > num9)
			{
				if (active - num10 > num11 - active)
				{
					num10++;
				}
				else
				{
					num11--;
				}
			}
			List<string> range = list.GetRange(num10, num11 - num10 + 1);
			active -= num10;
			float num12 = (range.Count > 1) ? Math.Max(0.016f, (num9 - Theme.<FactionBar>g__W|100_1(range)) / (float)range.Count) : 0f;
			Theme.Glyph(key, num5, y + 0.006f);
			Theme.Glyph(key2, num6, y + 0.006f);
			UiMouse.KeyRegion(num5 - 0.004f, y + 0.004f, Theme.GlyphWidth(key) + 0.008f, num2 - 0.008f, Keys.Q);
			UiMouse.KeyRegion(num6 - 0.004f, y + 0.004f, Theme.GlyphWidth(key2) + 0.008f, num2 - 0.008f, Keys.E);
			float num13 = num7 + ((range.Count > 1) ? (num12 / 2f) : ((num9 - Theme.<FactionBar>g__W|100_1(range)) / 2f));
			if (num10 > 0)
			{
				CS$<>8__locals1.<FactionBar>g__Arrow|2(true, num5 + Theme.GlyphWidth(key), num7);
			}
			for (int i = 0; i < range.Count; i++)
			{
				bool flag = i == active;
				ValueTuple<int, int, int> valueTuple = flag ? Theme.Title : Theme.Dim;
				bool flag2 = flag && UiDraw.MarkupUnderline && UiDraw.GameFont;
				UiDraw.TextFont(range[i], UiDraw.TitleFont, num13, CS$<>8__locals1.ty, 0.25f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255, flag2);
				if (CS$<>8__locals1.pick != null)
				{
					int fi = num10 + i;
					UiMouse.Region(num13 - num12 / 2f, y + 0.002f, Theme.CapsWidth(range[i], 0.25f) + num12, num2 - 0.004f, null, delegate
					{
						CS$<>8__locals1.pick(fi);
					});
				}
				if (flag && !flag2)
				{
					float num14 = Theme.CapsWidth(range[i], 0.25f);
					float num15 = num14 * 0.86f;
					UiDraw.Rect(num13 + (num14 - num15) / 2f, y + num2 - 0.0046296297f, num15, 0.0027777776f, accent.Item1, accent.Item2, accent.Item3, 255);
				}
				num13 += Theme.CapsWidth(range[i], 0.25f) + num12;
			}
			if (num11 < list.Count - 1)
			{
				CS$<>8__locals1.<FactionBar>g__Arrow|2(false, num8, num6);
			}
			Theme.GlyphWidth(key2);
			if (chipAsText)
			{
				UiDraw.TextFont(text, UiDraw.TitleFont, num3 - num4, y + 0.008f, 0.22f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			}
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0012C974 File Offset: 0x0012AB74
		public static void NotorietyBand(float y, int value, string killValues = null, string purse = null, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int>? purseCol = null, string aim = null)
		{
			if (purse != null || aim != null)
			{
				float num = y + 0.008f;
				if (purse != null)
				{
					ValueTuple<int, int, int> valueTuple = purseCol ?? Theme.Text;
					UiDraw.TextFont("BOLSA", UiDraw.TitleFont, 0.092f, num - 0.0018f, 0.2f, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 255, false);
					UiDraw.TextFont(purse, UiDraw.TitleFont, 0.092f + Theme.CapsWidth("BOLSA", 0.2f) + 0.01f, num - 0.002f, 0.22f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255, false);
				}
				if (aim != null)
				{
					string text = aim.ToUpperInvariant();
					float num2 = 0.908f - Theme.CapsWidth(text, 0.22f);
					UiDraw.TextFont(text, UiDraw.TitleFont, num2, num - 0.002f, 0.22f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255, false);
					UiDraw.TextFont("OBJETIVO", UiDraw.TitleFont, num2 - 0.01f - Theme.CapsWidth("OBJETIVO", 0.2f), num - 0.0018f, 0.2f, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 255, false);
				}
			}
			float x = 0.07f;
			float num3 = 0.032f;
			Theme.Bg(x, y, 0.86f, num3, 232);
			Theme.CenterRule(x, y + num3 - 0.0018518518f, 0.86f, 0.012f, 90);
			int num4 = Notoriety.Level(value);
			ValueTuple<int, int, int> valueTuple2 = Notoriety.LevelColors[num4];
			string text2 = Notoriety.LevelNames[num4].ToUpperInvariant();
			string text3 = (num4 < 4) ? string.Format("{0} / 100  -  {1} a los {2}", value, Notoriety.LevelNames[num4 + 1], Notoriety.LevelStarts[num4 + 1]) : string.Format("{0} / 100  -  nunca te olvidarán", value);
			float num5 = Theme.CapsWidth("NOTORIEDAD", 0.2f);
			float num6 = Theme.CapsWidth(text2, 0.24f);
			float num7 = Theme.MixedWidth(text3, 0.21f, 0.0222f);
			float num8 = num5 + 0.012f + num6 + 0.012f + 0.24f + 0.012f + num7;
			float num9 = 0.5f - num8 / 2f;
			float num10 = y + 0.008f;
			UiDraw.TextFont("NOTORIEDAD", UiDraw.TitleFont, num9, num10, 0.2f, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 255, false);
			num9 += num5 + 0.012f;
			UiDraw.TextFont(text2, UiDraw.TitleFont, num9, num10 - 0.002f, 0.24f, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255, false);
			num9 += num6 + 0.012f;
			float num11 = 0.0064814813f;
			float num12 = y + num3 / 2f - num11 / 2f;
			UiDraw.Rect(num9 - 0.0010416667f, num12 - 0.0018518518f, 0.24208333f, num11 + 0.0037037036f, 0, 0, 0, 230);
			for (int i = 0; i < 5; i++)
			{
				int num13 = Notoriety.LevelStarts[i];
				int num14 = (i < 4) ? Notoriety.LevelStarts[i + 1] : 100;
				float x2 = num9 + 0.24f * (float)num13 / 100f;
				float num15 = 0.24f * (float)(num14 - num13) / 100f - ((i < 4) ? 0.0010416667f : 0f);
				UiDraw.Rect(x2, num12, num15, num11, 40, 38, 35, 255);
				if (value > num13)
				{
					float w = num15 * Math.Min(1f, (float)(value - num13) / (float)(num14 - num13));
					ValueTuple<int, int, int> valueTuple3 = Notoriety.LevelColors[i];
					UiDraw.Rect(x2, num12, w, num11, valueTuple3.Item1, valueTuple3.Item2, valueTuple3.Item3, 255);
					UiDraw.Rect(x2, num12, w, 0.0009259259f, Math.Min(255, valueTuple3.Item1 + 40), Math.Min(255, valueTuple3.Item2 + 40), Math.Min(255, valueTuple3.Item3 + 40), 200);
				}
				if (i > 0)
				{
					UiDraw.Rect(num9 + 0.24f * (float)num13 / 100f - 0.00052083336f - 0.00052083336f, num12 - 0.0027777776f, 0.0015625001f, num11 + 0.005555555f, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 200);
				}
			}
			num9 += 0.252f;
			UiDraw.Text(text3, num9, num10, 0.21f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x0012CEA4 File Offset: 0x0012B0A4
		public static void StrengthRow(float y, int strength, string word, [TupleElementNames(new string[]
		{
			"label",
			"v"
		})] ValueTuple<string, double>[] parts)
		{
			float num = 0.028f;
			Theme.Bg(0.07f, y, 0.86f, num, 232);
			Theme.CenterRule(0.07f, y + num - 0.0018518518f, 0.86f, 0.012f, 70);
			if (strength < 0 || parts == null)
			{
				return;
			}
			ValueTuple<int, int, int> valueTuple = (strength >= 75) ? Theme.Good : ((strength >= 50) ? Theme.Text : ((strength >= 30) ? Theme.Warn : Theme.Blood));
			string text = (word ?? "").ToUpperInvariant();
			float num2 = Theme.CapsWidth("FUERZA:", 0.18f) + 0.01f + Theme.CapsWidth(text, 0.21f) + 0.022f;
			foreach (ValueTuple<string, double> valueTuple2 in parts)
			{
				num2 += Theme.CapsWidth(valueTuple2.Item1, 0.18f) + 0.01f + 0.055f + 0.022f;
			}
			num2 -= 0.022f;
			float num3 = 0.5f - num2 / 2f;
			float num4 = y + 0.006f;
			UiDraw.TextFont("FUERZA:", UiDraw.TitleFont, num3, num4 + 0.001f, 0.18f, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 255, false);
			num3 += Theme.CapsWidth("FUERZA:", 0.18f) + 0.01f;
			UiDraw.TextFont(text, UiDraw.TitleFont, num3, num4 - 0.001f, 0.21f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255, false);
			num3 += Theme.CapsWidth(text, 0.21f) + 0.022f;
			float num5 = 0.0046296297f;
			float num6 = y + num / 2f - num5 / 2f - 0.0009259259f;
			foreach (ValueTuple<string, double> valueTuple3 in parts)
			{
				UiDraw.TextFont(valueTuple3.Item1, UiDraw.TitleFont, num3, num4 + 0.001f, 0.18f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
				num3 += Theme.CapsWidth(valueTuple3.Item1, 0.18f) + 0.01f;
				UiDraw.Rect(num3 - 0.00052083336f, num6 - 0.0009259259f, 0.056041665f, num5 + 0.0018518518f, 0, 0, 0, 230);
				UiDraw.Rect(num3, num6, 0.055f, num5, 40, 38, 35, 255);
				ValueTuple<int, int, int> valueTuple4 = Theme.<StrengthRow>g__Col|104_0(valueTuple3.Item2);
				float num7 = 0.055f * (float)Math.Max(0.0, Math.Min(1.0, valueTuple3.Item2));
				if (num7 > 0f)
				{
					UiDraw.Rect(num3, num6, num7, num5, valueTuple4.Item1, valueTuple4.Item2, valueTuple4.Item3, 255);
				}
				num3 += 0.077f;
			}
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x0012D1C4 File Offset: 0x0012B3C4
		public static void GroundRow(float y, [TupleElementNames(new string[]
		{
			"label",
			"names",
			"c",
			"r",
			"g",
			"b"
		})] ValueTuple<string, List<string>, ValueTuple<int, int, int>>[] groups, string none = null)
		{
			float num = 0.026f;
			Theme.Bg(0.07f, y, 0.86f, num, 232);
			Theme.CenterRule(0.07f, y + num - 0.0018518518f, 0.86f, 0.012f, 70);
			if (groups == null)
			{
				return;
			}
			float num2 = 0.81f;
			Theme.<>c__DisplayClass106_0 CS$<>8__locals1;
			CS$<>8__locals1.gs = (from gr in groups
			select new ValueTuple<string, List<string>, ValueTuple<int, int, int>>(gr.Item1, new List<string>(gr.Item2), gr.Item3) into gr
			where gr.Item2.Count > 0
			select gr).ToList<ValueTuple<string, List<string>, ValueTuple<int, int, int>>>();
			CS$<>8__locals1.dropped = 0;
			while (CS$<>8__locals1.gs.Count > 0 && Theme.<GroundRow>g__Width|106_4(ref CS$<>8__locals1) > num2)
			{
				ValueTuple<string, List<string>, ValueTuple<int, int, int>> valueTuple = CS$<>8__locals1.gs[CS$<>8__locals1.gs.Count - 1];
				valueTuple.Item2.RemoveAt(valueTuple.Item2.Count - 1);
				int dropped = CS$<>8__locals1.dropped;
				CS$<>8__locals1.dropped = dropped + 1;
				if (valueTuple.Item2.Count == 0)
				{
					CS$<>8__locals1.gs.RemoveAt(CS$<>8__locals1.gs.Count - 1);
				}
			}
			float num3 = y + 0.0055f;
			if (CS$<>8__locals1.gs.Count == 0)
			{
				string text = (CS$<>8__locals1.dropped > 0) ? string.Format("TERRITORIO: en {0} distritos", CS$<>8__locals1.dropped) : (none ?? "Sin territorio propio");
				UiDraw.Text(text, 0.5f - Theme.MixedWidth(text, 0.205f, 0.0222f) / 2f, num3, 0.205f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
				return;
			}
			float num4 = 0.5f - Theme.<GroundRow>g__Width|106_4(ref CS$<>8__locals1) / 2f;
			UiDraw.TextFont("TERRITORIO:", UiDraw.TitleFont, num4, num3 + 0.001f, 0.18f, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 255, false);
			num4 += Theme.CapsWidth("TERRITORIO:", 0.18f) + 0.022f;
			for (int i = 0; i < CS$<>8__locals1.gs.Count; i++)
			{
				ValueTuple<string, List<string>, ValueTuple<int, int, int>> valueTuple2 = CS$<>8__locals1.gs[i];
				UiDraw.TextFont(valueTuple2.Item1, UiDraw.TitleFont, num4, num3 + 0.001f, 0.18f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
				num4 += Theme.CapsWidth(valueTuple2.Item1, 0.18f) + 0.008f;
				UiDraw.Text(Theme.<GroundRow>g__Joined|106_2(valueTuple2.Item2) + ((i == CS$<>8__locals1.gs.Count - 1) ? Theme.<GroundRow>g__Tail|106_3(ref CS$<>8__locals1) : ""), num4, num3 - 0.001f, 0.205f, valueTuple2.Item3.Item1, valueTuple2.Item3.Item2, valueTuple2.Item3.Item3, 255);
				num4 += Theme.MixedWidth(Theme.<GroundRow>g__Joined|106_2(valueTuple2.Item2), 0.205f, 0.0222f) + 0.022f;
			}
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0012D51C File Offset: 0x0012B71C
		public static void NoteBand(float y, string text, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int>? color = null)
		{
			float num = 0.032f;
			ValueTuple<int, int, int> valueTuple = color ?? Theme.Dim;
			Theme.Bg(0.07f, y, 0.86f, num, 232);
			Theme.CenterRule(0.07f, y + num - 0.0018518518f, 0.86f, 0.012f, 90);
			UiDraw.Text(text, 0.5f - Theme.MixedWidth(text, 0.21f, 0.0222f) / 2f, y + 0.0065f, 0.21f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0012D5C4 File Offset: 0x0012B7C4
		public static float Icon(string name, float x, float y, float h, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> c, int a = 255)
		{
			if (string.IsNullOrEmpty(name))
			{
				return 0f;
			}
			float num = h / UiDraw.Aspect;
			if (UiDraw.EnsureTextureDict("itemtype_textures"))
			{
				UiDraw.Sprite("itemtype_textures", name, x + num / 2f, y + h / 2f, num, h, 0f, c.Item1, c.Item2, c.Item3, a);
			}
			return num;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x0012D630 File Offset: 0x0012B830
		public static string TemperIcon(Personality? p)
		{
			if (p != null)
			{
				switch (p.GetValueOrDefault())
				{
				case Personality.Hothead:
					return "itemtype_weapons";
				case Personality.Coward:
					return "itemtype_player_stamina";
				case Personality.Proud:
					return "itemtype_gold";
				case Personality.Cold:
					return "itemtype_player_deadeye";
				case Personality.Greedy:
					return "itemtype_cash";
				case Personality.Loyal:
					return "itemtype_camp";
				case Personality.Drifter:
					return "itemtype_horse";
				}
			}
			return null;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0012D6A0 File Offset: 0x0012B8A0
		public static string FacetIcon(string facet)
		{
			if (facet != null)
			{
				switch (facet.Length)
				{
				case 5:
				{
					char c = facet[4];
					if (c <= 'e')
					{
						if (c != 'd')
						{
							if (c == 'e')
							{
								if (facet == "Pride")
								{
									return "itemtype_gold";
								}
							}
						}
						else if (facet == "Greed")
						{
							return "itemtype_cash";
						}
					}
					else if (c != 'r')
					{
						if (c == 'y')
						{
							if (facet == "Piety")
							{
								return "transaction_journal";
							}
						}
					}
					else if (facet == "Anger")
					{
						return "itemtype_weapons";
					}
					break;
				}
				case 6:
					if (facet == "Humour")
					{
						return "itemtype_collectable";
					}
					break;
				case 7:
				{
					char c = facet[0];
					if (c <= 'C')
					{
						if (c != 'B')
						{
							if (c == 'C')
							{
								if (facet == "Cruelty")
								{
									return "transaction_honor_bad";
								}
							}
						}
						else if (facet == "Bravery")
						{
							return "itemtype_player_health";
						}
					}
					else if (c != 'H')
					{
						if (c == 'L')
						{
							if (facet == "Loyalty")
							{
								return "itemtype_camp";
							}
						}
					}
					else if (facet == "Honesty")
					{
						return "transaction_honor_good";
					}
					break;
				}
				case 8:
					if (facet == "Ambition")
					{
						return "transaction_xp";
					}
					break;
				}
			}
			return null;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0012D820 File Offset: 0x0012BA20
		public static float IconChip(string text, string icon, float x, float y, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> color)
		{
			string text2 = text.ToUpperInvariant();
			float num = 0.018f / UiDraw.Aspect;
			float num2 = Theme.CapsWidth(text2, 0.2f);
			float num3 = 0.008f + num + 0.006f + num2 + 0.01f;
			UiDraw.Rect(x, y, num3, 0.026f, color.Item1 / 8, color.Item2 / 8, color.Item3 / 8, 200);
			Theme.Hairline(x, y, num3, 0.026f, color.Item1, color.Item2, color.Item3, 220, 1);
			if (icon != null)
			{
				Theme.Icon(icon, x + 0.008f, y + 0.0040000007f, 0.018f, color, 255);
			}
			float x2 = x + 0.008f + num + 0.006f;
			UiDraw.TextFont(text2, UiDraw.TitleFont, x2, y + 0.013f - 0.0075000003f - 0.001f, 0.2f, color.Item1, color.Item2, color.Item3, 255, false);
			return num3;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0012D92C File Offset: 0x0012BB2C
		public static string QuirkIcon(string q)
		{
			if (q != null)
			{
				switch (q.Length)
				{
				case 5:
					if (q == "drunk")
					{
						return "itemtype_herbs";
					}
					break;
				case 6:
				{
					char c = q[0];
					if (c != 'h')
					{
						if (c == 's')
						{
							if (q == "sadist")
							{
								return "itemtype_weapons";
							}
						}
					}
					else if (q == "honest")
					{
						return "transaction_honor_good";
					}
					break;
				}
				case 7:
				{
					char c = q[0];
					if (c != 'g')
					{
						if (c == 't')
						{
							if (q == "traitor")
							{
								return "transaction_honor_bad";
							}
						}
					}
					else if (q == "gambler")
					{
						return "itemtype_collectable";
					}
					break;
				}
				case 8:
					if (q == "braggart")
					{
						return "transaction_xp";
					}
					break;
				case 10:
					if (q == "padre de familia")
					{
						return "itemtype_camp";
					}
					break;
				case 13:
					if (q == "superstitious")
					{
						return "itemtype_animal";
					}
					break;
				}
			}
			return null;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0012DA54 File Offset: 0x0012BC54
		public static string TraitIcon(string trait)
		{
			if (trait != null)
			{
				switch (trait.Length)
				{
				case 3:
					if (trait == "Corto de luces")
					{
						return "itemtype_loot";
					}
					break;
				case 4:
					if (trait == "Tranquilo")
					{
						return "itemtype_player_health";
					}
					break;
				case 5:
				{
					char c = trait[1];
					if (c != 'h')
					{
						if (c == 'u')
						{
							if (trait == "Hosco")
							{
								return "transaction_honor_bad";
							}
						}
					}
					else if (trait == "Sharp")
					{
						return "itemtype_documents";
					}
					break;
				}
				case 7:
				{
					char c = trait[0];
					if (c != 'N')
					{
						if (c == 'V')
						{
							if (trait == "Violento")
							{
								return "itemtype_weapons";
							}
						}
					}
					else if (trait == "Neutral")
					{
						return "itemtype_player_deadeye";
					}
					break;
				}
				case 8:
				{
					char c = trait[0];
					if (c != 'F')
					{
						if (c != 'O')
						{
							if (c == 'R')
							{
								if (trait == "Inquieto")
								{
									return "itemtype_player_stamina";
								}
							}
						}
						else if (trait == "Extrovertido")
						{
							return "transaction_camp_gang";
						}
					}
					else if (trait == "Amistoso")
					{
						return "transaction_honor_good";
					}
					break;
				}
				case 9:
					if (trait == "Introvertido")
					{
						return "transaction_journal";
					}
					break;
				}
			}
			if (trait == null || (!trait.Contains("Violento") && !trait.Contains("Agresivo")))
			{
				return null;
			}
			return "itemtype_weapons";
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x0012DBF5 File Offset: 0x0012BDF5
		private static string Canon(string key)
		{
			if (key == "Right stick")
			{
				return "RS";
			}
			if (key == "Left stick")
			{
				return "LS";
			}
			if (!(key == "Dpad"))
			{
				return key;
			}
			return "D-pad";
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x0012DC34 File Offset: 0x0012BE34
		public static float GlyphWidth(string key)
		{
			float num = 0f;
			foreach (string k in Theme.Parts(key))
			{
				num += Theme.SingleWidth(k) + 0.003f;
			}
			return Math.Max(0f, num - 0.003f);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0012DCA0 File Offset: 0x0012BEA0
		private static IEnumerable<string> Parts(string key)
		{
			key = Theme.Canon(key.Trim());
			if (key == "+/-")
			{
				yield return "+";
				yield return "-";
				yield break;
			}
			foreach (string text in key.Split(new char[]
			{
				' '
			}))
			{
				foreach (string text2 in text.Split(new char[]
				{
					'/'
				}))
				{
					if (text2.Length > 0)
					{
						yield return Theme.Canon(text2);
					}
				}
				string[] array2 = null;
			}
			string[] array = null;
			yield break;
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x0012DCB0 File Offset: 0x0012BEB0
		private static bool PadStyle(string k)
		{
			return XPad.RecentlyUsed && (Theme.PadFace.Contains(k) || Theme.PadPill.Contains(k) || k == "D-pad" || Theme.PadDir.Contains(k));
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x0012DCF0 File Offset: 0x0012BEF0
		private static float KeyboardLabelW(string k, float s)
		{
			string s2;
			if (k != null)
			{
				s2 = new string(k.Select(delegate(char c)
				{
					if ("iljtfr".IndexOf(c) < 0)
					{
						return c;
					}
					return 'o';
				}).ToArray<char>());
			}
			else
			{
				s2 = null;
			}
			return Theme.MixedWidth(s2, s, 0.0205f);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x0012DD3D File Offset: 0x0012BF3D
		private static float KeyLabelW(string k, float s)
		{
			return Theme.MixedWidth(k, s, 0.0205f);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0012DD4C File Offset: 0x0012BF4C
		private static string PadLabel(string k)
		{
			string result;
			if (!XPad.PlayStation || !Theme.PsNames.TryGetValue(k, out result))
			{
				return k;
			}
			return result;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x0012DD74 File Offset: 0x0012BF74
		private static void PsSymbol(string k, float cx, float cy, float d)
		{
			float aspect = UiDraw.Aspect;
			float num = 0.0024074074f;
			float num2 = d * 0.44f;
			if (k == "A")
			{
				float num3 = num2 * 0.46f;
				UiDraw.Line(cx - num3 / aspect, cy - num3, cx + num3 / aspect, cy + num3, num, 100, 150, 230, 255, 40);
				UiDraw.Line(cx - num3 / aspect, cy + num3, cx + num3 / aspect, cy - num3, num, 100, 150, 230, 255, 40);
				return;
			}
			if (!(k == "B"))
			{
				if (k == "X")
				{
					float num4 = num2 * 0.44f;
					float num5 = num4 / aspect;
					float num6 = num / aspect;
					UiDraw.Rect(cx - num5, cy - num4, 2f * num5, num, 220, 130, 190, 255);
					UiDraw.Rect(cx - num5, cy + num4 - num, 2f * num5, num, 220, 130, 190, 255);
					UiDraw.Rect(cx - num5, cy - num4, num6, 2f * num4, 220, 130, 190, 255);
					UiDraw.Rect(cx + num5 - num6, cy - num4, num6, 2f * num4, 220, 130, 190, 255);
					return;
				}
				if (!(k == "Y"))
				{
					return;
				}
				float num7 = cy - num2 * 0.5f;
				float num8 = cy + num2 * 0.4f;
				float num9 = num2 * 0.54f / aspect;
				UiDraw.Rect(cx - num9, num8 - num, 2f * num9, num, 80, 190, 140, 255);
				UiDraw.Line(cx, num7 + num * 0.5f, cx - num9 + num * 0.5f / aspect, num8 - num * 0.5f, num, 80, 190, 140, 255, 40);
				UiDraw.Line(cx, num7 + num * 0.5f, cx + num9 - num * 0.5f / aspect, num8 - num * 0.5f, num, 80, 190, 140, 255, 40);
				return;
			}
			else
			{
				ValueTuple<int, int, int> keyCap = Theme.KeyCap;
				int item = keyCap.Item1;
				int item2 = keyCap.Item2;
				int item3 = keyCap.Item3;
				float num10 = num2 * 1.05f;
				float num11 = num10 - 2f * num;
				if (UiDraw.EnsureTextureDict("menu_textures"))
				{
					UiDraw.Sprite("menu_textures", "menu_icon_circle", cx, cy, num10 / aspect, num10, 0f, 220, 80, 80, 255);
					UiDraw.Sprite("menu_textures", "menu_icon_circle", cx, cy, num11 / aspect, num11, 0f, item, item2, item3, 255);
					return;
				}
				Theme.Disc(cx, cy, num10, 220, 80, 80, 255);
				Theme.Disc(cx, cy, num11, item, item2, item3, 255);
				return;
			}
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0012E06A File Offset: 0x0012C26A
		private static float KeyLabelY(float y, float h, float s)
		{
			return y + h / 2f - s * 0.0375f;
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x0012E080 File Offset: 0x0012C280
		private static float SingleWidth(string k)
		{
			if (!Theme.PadStyle(k))
			{
				return Theme.KeyFit(k).Item1;
			}
			if (Theme.PadFace.Contains(k) || Theme.PadStick.Contains(k))
			{
				return 0.029f / UiDraw.Aspect;
			}
			if (k == "D-pad" || Theme.PadDir.Contains(k))
			{
				return 0.022f / UiDraw.Aspect;
			}
			return Theme.KeyLabelW(Theme.PadLabel(k), 0.19f) + 0.012f;
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x0012E104 File Offset: 0x0012C304
		[return: TupleElementNames(new string[]
		{
			"w",
			"s"
		})]
		private static ValueTuple<float, float> KeyFit(string k)
		{
			ValueTuple<float, float> result;
			if (k != null && Theme._keyFit.TryGetValue(k, out result))
			{
				return result;
			}
			float num = 0.022f / UiDraw.Aspect;
			float num2 = 0.004f;
			float num3 = 0.21f;
			while (num3 > 0.15f && Theme.KeyboardLabelW(k, num3) + num2 > num)
			{
				num3 -= 0.01f;
			}
			float num4 = Math.Max(num, Theme.KeyboardLabelW(k, num3) + num2);
			if (num4 > num)
			{
				num4 = Theme.KeyboardLabelW(k, num3) + 0.008f;
			}
			ValueTuple<float, float> valueTuple = new ValueTuple<float, float>(num4, num3);
			if (k != null && UiDraw.Aspect > 0f)
			{
				Theme._keyFit[k] = valueTuple;
			}
			return valueTuple;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x0012E1A8 File Offset: 0x0012C3A8
		public static float Glyph(string key, float x, float y)
		{
			float num = x;
			foreach (string k in Theme.Parts(key))
			{
				x += Theme.Single(k, x, y) + 0.003f;
			}
			return Math.Max(0f, x - num - 0.003f);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x0012E218 File Offset: 0x0012C418
		private static float Single(string k, float x, float y)
		{
			float num = Theme.SingleWidth(k);
			float num2 = 0.022f;
			ValueTuple<int, int, int> keyCap = Theme.KeyCap;
			int item = keyCap.Item1;
			int item2 = keyCap.Item2;
			int item3 = keyCap.Item3;
			if (Theme.PadStyle(k))
			{
				if (Theme.PadStick.Contains(k))
				{
					float num3 = 0.029f;
					float num4 = x + num / 2f;
					float num5 = y + 0.011f;
					if (UiDraw.EnsureTextureDict("menu_textures"))
					{
						UiDraw.Sprite("menu_textures", "menu_icon_circle", num4, num5, num3 / UiDraw.Aspect, num3, 0f, item, item2, item3, 255);
					}
					else
					{
						Theme.Disc(num4, num5, num3, item, item2, item3, 255);
					}
					string text = Theme.PadLabel(k);
					UiDraw.TextFont(text, UiDraw.TitleFont, num4 - Theme.KeyLabelW(text, 0.18f) / 2f - 0.0011f, num5 - 0.0067500006f + 0.0006f, 0.18f, 18, 18, 17, 255, false);
				}
				else if (Theme.PadFace.Contains(k))
				{
					float num6 = 0.029f;
					float num7 = x + num / 2f;
					float num8 = y + 0.011f;
					if (UiDraw.EnsureTextureDict("menu_textures"))
					{
						UiDraw.Sprite("menu_textures", "menu_icon_circle", num7, num8, num6 / UiDraw.Aspect, num6, 0f, item, item2, item3, 255);
					}
					else
					{
						Theme.Disc(num7, num8, num6, item, item2, item3, 255);
					}
					if (XPad.PlayStation)
					{
						Theme.PsSymbol(k, num7, num8, num6);
						return num;
					}
					UiDraw.TextFont(k, UiDraw.TitleFont, num7 - Theme.KeyLabelW(k, 0.23f) / 2f - 0.0007f, num8 - 0.008625001f + 0.0008f, 0.23f, 18, 18, 17, 255, false);
				}
				else if (Theme.PadDir.Contains(k))
				{
					float num9 = num * 0.34f;
					float num10 = num2 * 0.34f;
					UiDraw.Rect(x + (num - num9) / 2f, y, num9, num2, 110, 108, 104, 255);
					UiDraw.Rect(x, y + (num2 - num10) / 2f, num, num10, 110, 108, 104, 255);
					float num11 = x + (num - num9) / 2f;
					float num12 = y + (num2 - num10) / 2f;
					if (k == "DUp")
					{
						UiDraw.Rect(num11, y, num9, (num2 - num10) / 2f + num10 * 0.5f, item, item2, item3, 255);
					}
					else if (k == "DDown")
					{
						UiDraw.Rect(num11, num12 + num10 * 0.5f, num9, (num2 - num10) / 2f + num10 * 0.5f, item, item2, item3, 255);
					}
					else if (k == "DLeft")
					{
						UiDraw.Rect(x, num12, (num - num9) / 2f + num9 * 0.5f, num10, item, item2, item3, 255);
					}
					else
					{
						UiDraw.Rect(num11 + num9 * 0.5f, num12, (num - num9) / 2f + num9 * 0.5f, num10, item, item2, item3, 255);
					}
				}
				else if (k == "D-pad")
				{
					float num13 = num * 0.34f;
					float num14 = num2 * 0.34f;
					UiDraw.Rect(x + (num - num13) / 2f, y, num13, num2, item, item2, item3, 255);
					UiDraw.Rect(x, y + (num2 - num14) / 2f, num, num14, item, item2, item3, 255);
				}
				else
				{
					UiDraw.Rect(x, y + 0.001f, num, num2 - 0.002f, item, item2, item3, 255);
					float num15 = (k == "RS" || k == "LS") ? -0.0011f : ((k == "RT" || k == "LT") ? -0.0009f : ((k == "RB" || k == "LB") ? -0.0007f : ((k == "Vista") ? -0.001f : 0f)));
					string text2 = Theme.PadLabel(k);
					UiDraw.TextFont(text2, UiDraw.TitleFont, x + num / 2f - Theme.KeyLabelW(text2, 0.19f) / 2f + num15, Theme.KeyLabelY(y, num2, 0.19f), 0.19f, 18, 18, 17, 255, false);
				}
				return num;
			}
			UiDraw.Rect(x, y, num, num2, item, item2, item3, 255);
			Theme.HRule(x, y + num2 - 0.0018518518f, num, 150, 148, 142, 255, 2);
			float item4 = Theme.KeyFit(k).Item2;
			UiDraw.TextFont(k, UiDraw.TitleFont, x + num / 2f - Theme.KeyboardLabelW(k, item4) / 2f - 0.0010416667f, Theme.KeyLabelY(y, num2 - 0.0018518518f, item4), item4, 14, 14, 13, 255, false);
			return num;
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x0012E738 File Offset: 0x0012C938
		public static void Footer(float y, string help, bool warn = false)
		{
			Theme.Bg(0.07f, y, 0.86f, 0.034f, 232);
			Theme.CenterRule(0.07f, y, 0.86f, 0.012f, 170);
			if (warn)
			{
				UiDraw.Text(help, 0.5f - Theme.MixedWidth(help, 0.25f, 0.0222f) / 2f, y + 0.012f, 0.25f, Theme.Warn.Item1, Theme.Warn.Item2, Theme.Warn.Item3, 255);
				return;
			}
			List<ValueTuple<string, string, float>> list = new List<ValueTuple<string, string, float>>();
			string[] array = help.Split(new string[]
			{
				"     "
			}, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i].Trim();
				int num = text.IndexOf("  ", StringComparison.Ordinal);
				string text2 = (num > 0) ? text.Substring(0, num).Trim() : null;
				string text3 = (num > 0) ? text.Substring(num).Trim() : text;
				float item = ((text2 != null) ? (Theme.GlyphWidth(text2) + 0.006f) : 0f) + Theme.MixedWidth(text3, 0.23f, 0.0205f);
				list.Add(new ValueTuple<string, string, float>(text2, text3, item));
			}
			Theme.HintRow(0.084f, 0.916f, y + 0.01f, (from hh in list
			select new ValueTuple<string, string>(hh.Item1, hh.Item2)).ToList<ValueTuple<string, string>>(), 0.23f, Theme.Text);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0012E8CB File Offset: 0x0012CACB
		public static void ScrollBar(float x, float top, float h, int first, int shown, int total)
		{
			if (total <= shown || shown <= 0 || h <= 0f)
			{
				return;
			}
			Theme.ScrollBar(x, top, h, (float)first / (float)total, (float)shown / (float)total);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0012E8F8 File Offset: 0x0012CAF8
		public static void ScrollBar(float x, float top, float h, float startFrac, float shownFrac)
		{
			if (shownFrac >= 0.999f || h <= 0f)
			{
				return;
			}
			float w = 0.0015625001f;
			float num = Math.Max(0.02f, h * Math.Max(0f, shownFrac));
			float y = top + (h - num) * Math.Max(0f, Math.Min(1f, startFrac / Math.Max(0.0001f, 1f - shownFrac)));
			UiDraw.Rect(x, top, w, h, Theme.Faint.Item1, Theme.Faint.Item2, Theme.Faint.Item3, 70);
			UiDraw.Rect(x, y, w, num, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 210);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x0012E9BC File Offset: 0x0012CBBC
		public static void HintRow(float left, float right, float y, [TupleElementNames(new string[]
		{
			"key",
			"label"
		})] IList<ValueTuple<string, string>> hints, float scale, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> color)
		{
			if (hints == null || hints.Count == 0)
			{
				return;
			}
			List<float> list = (from hh in hints
			select ((hh.Item1 != null) ? (Theme.GlyphWidth(hh.Item1) + 0.006f) : 0f) + Theme.MixedWidth(hh.Item2, scale, 0.0205f)).ToList<float>();
			float num = right - left;
			float num2 = list.Sum();
			List<int> list2 = Enumerable.Range(0, hints.Count).ToList<int>();
			while (list2.Count > 1 && num2 + (float)(list2.Count - 1) * 0.012f > num)
			{
				int index = (list2.Count >= 2) ? (list2.Count - 2) : 0;
				num2 -= list[list2[index]];
				list2.RemoveAt(index);
			}
			int count = list2.Count;
			float num3 = (count > 1) ? Math.Min(0.04f, (num - num2) / (float)(count - 1)) : 0f;
			float num4 = left + (num - (num2 + num3 * (float)(count - 1))) / 2f;
			for (int i = 0; i < count; i++)
			{
				ValueTuple<string, string> valueTuple = hints[list2[i]];
				string item = valueTuple.Item1;
				string item2 = valueTuple.Item2;
				Keys? keys = UiMouse.KeyOfLabel(item);
				if (keys != null)
				{
					UiMouse.KeyRegion(num4 - 0.004f, y - 0.004f, list[list2[i]] + 0.008f, 0.03f, keys.Value);
				}
				if (item != null)
				{
					num4 += Theme.Glyph(item, num4, y) + 0.006f;
				}
				UiDraw.TextFont(item2, UiDraw.TitleFont, num4, y + 0.003f, scale, color.Item1, color.Item2, color.Item3, 255, false);
				num4 += Theme.MixedWidth(item2, scale, 0.0205f) + num3;
			}
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x0012EB94 File Offset: 0x0012CD94
		// Note: this type is marked as 'beforefieldinit'.
		static Theme()
		{
			AssemblyInformationalVersionAttribute customAttribute = typeof(Theme).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
			Theme.ModVersion = (((customAttribute != null) ? customAttribute.InformationalVersion : null) ?? "?").Split(new char[]
			{
				'+'
			})[0];
			Theme.PadFace = new HashSet<string>
			{
				"A",
				"B",
				"X",
				"Y"
			};
			Theme.PadPill = new HashSet<string>
			{
				"LB",
				"RB",
				"LT",
				"RT",
				"Vista",
				"Menu",
				"LS",
				"RS"
			};
			Theme.PadStick = new HashSet<string>
			{
				"LS",
				"RS"
			};
			Theme.PadDir = new HashSet<string>
			{
				"DUp",
				"DDown",
				"DLeft",
				"DRight"
			};
			Theme.PsNames = new Dictionary<string, string>
			{
				{
					"LB",
					"L1"
				},
				{
					"RB",
					"R1"
				},
				{
					"LT",
					"L2"
				},
				{
					"RT",
					"R2"
				},
				{
					"LS",
					"L3"
				},
				{
					"RS",
					"R3"
				},
				{
					"Vista",
					"Share"
				},
				{
					"Menu",
					"Options"
				}
			};
			Theme._keyFit = new Dictionary<string, ValueTuple<float, float>>();
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x0012EFA7 File Offset: 0x0012D1A7
		[CompilerGenerated]
		internal static float <FactionBar>g__W|100_1(List<string> l)
		{
			return l.Sum((string n) => Theme.CapsWidth(n, 0.25f));
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x0012EFCE File Offset: 0x0012D1CE
		[CompilerGenerated]
		[return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		internal static ValueTuple<int, int, int> <StrengthRow>g__Col|104_0(double v)
		{
			if (v < 0.25)
			{
				return Theme.Blood;
			}
			if (v >= 0.5)
			{
				return Theme.Good;
			}
			return Theme.Warn;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0012EFF9 File Offset: 0x0012D1F9
		[CompilerGenerated]
		internal static string <GroundRow>g__Joined|106_2(List<string> n)
		{
			return string.Join(", ", n);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0012F006 File Offset: 0x0012D206
		[CompilerGenerated]
		internal static string <GroundRow>g__Tail|106_3(ref Theme.<>c__DisplayClass106_0 A_0)
		{
			if (A_0.dropped <= 0)
			{
				return "";
			}
			return string.Format("  +{0} más", A_0.dropped);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0012F02C File Offset: 0x0012D22C
		[CompilerGenerated]
		internal static float <GroundRow>g__Width|106_4(ref Theme.<>c__DisplayClass106_0 A_0)
		{
			float num = Theme.CapsWidth("TERRITORIO:", 0.18f) + 0.022f;
			foreach (ValueTuple<string, List<string>, ValueTuple<int, int, int>> valueTuple in A_0.gs)
			{
				num += Theme.CapsWidth(valueTuple.Item1, 0.18f) + 0.008f + Theme.MixedWidth(Theme.<GroundRow>g__Joined|106_2(valueTuple.Item2), 0.205f, 0.0222f) + 0.022f;
			}
			return num - 0.022f + ((A_0.dropped > 0) ? Theme.MixedWidth(Theme.<GroundRow>g__Tail|106_3(ref A_0), 0.205f, 0.0222f) : 0f);
		}

		// Token: 0x04000C6B RID: 3179
		public const float PanelX = 0.07f;

		// Token: 0x04000C6C RID: 3180
		public const float PanelW = 0.86f;

		// Token: 0x04000C6D RID: 3181
		public const float Top = 0.07f;

		// Token: 0x04000C6E RID: 3182
		public const float HeaderH = 0.065f;

		// Token: 0x04000C6F RID: 3183
		public const float Pad = 0.01f;

		// Token: 0x04000C70 RID: 3184
		public const float CenterX = 0.5f;

		// Token: 0x04000C71 RID: 3185
		public const float FooterY = 0.868f;

		// Token: 0x04000C72 RID: 3186
		public const float Px = 0.00052083336f;

		// Token: 0x04000C73 RID: 3187
		public const float PyU = 0.0009259259f;

		// Token: 0x04000C74 RID: 3188
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Gold = new ValueTuple<int, int, int>(206, 200, 188);

		// Token: 0x04000C75 RID: 3189
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Title = new ValueTuple<int, int, int>(245, 244, 240);

		// Token: 0x04000C76 RID: 3190
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Subtitle = new ValueTuple<int, int, int>(196, 193, 186);

		// Token: 0x04000C77 RID: 3191
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Text = new ValueTuple<int, int, int>(226, 224, 218);

		// Token: 0x04000C78 RID: 3192
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Dim = new ValueTuple<int, int, int>(146, 144, 138);

		// Token: 0x04000C79 RID: 3193
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Faint = new ValueTuple<int, int, int>(98, 96, 91);

		// Token: 0x04000C7A RID: 3194
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Panel = new ValueTuple<int, int, int>(10, 10, 9);

		// Token: 0x04000C7B RID: 3195
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Card = new ValueTuple<int, int, int>(27, 26, 24);

		// Token: 0x04000C7C RID: 3196
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> CardHi = new ValueTuple<int, int, int>(52, 50, 46);

		// Token: 0x04000C7D RID: 3197
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Border = new ValueTuple<int, int, int>(84, 81, 76);

		// Token: 0x04000C7E RID: 3198
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Selected = new ValueTuple<int, int, int>(40, 38, 35);

		// Token: 0x04000C7F RID: 3199
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> SelectedText = new ValueTuple<int, int, int>(245, 244, 240);

		// Token: 0x04000C80 RID: 3200
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> NativeRed = new ValueTuple<int, int, int>(176, 26, 24);

		// Token: 0x04000C81 RID: 3201
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> KeyCap = new ValueTuple<int, int, int>(214, 212, 206);

		// Token: 0x04000C82 RID: 3202
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Rule = new ValueTuple<int, int, int>(190, 187, 180);

		// Token: 0x04000C83 RID: 3203
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Line = new ValueTuple<int, int, int>(120, 117, 110);

		// Token: 0x04000C84 RID: 3204
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Blood = new ValueTuple<int, int, int>(205, 55, 45);

		// Token: 0x04000C85 RID: 3205
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Warn = new ValueTuple<int, int, int>(215, 135, 50);

		// Token: 0x04000C86 RID: 3206
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Good = new ValueTuple<int, int, int>(130, 190, 120);

		// Token: 0x04000C87 RID: 3207
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> Ink = new ValueTuple<int, int, int>(22, 21, 19);

		// Token: 0x04000C88 RID: 3208
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> InkSoft = new ValueTuple<int, int, int>(78, 74, 68);

		// Token: 0x04000C89 RID: 3209
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public static readonly ValueTuple<int, int, int> InkFaded = new ValueTuple<int, int, int>(112, 108, 102);

		// Token: 0x04000C8A RID: 3210
		private static bool _backdrop;

		// Token: 0x04000C8B RID: 3211
		private const ulong AnimPostFxPlay = 4684433203668451423UL;

		// Token: 0x04000C8C RID: 3212
		private const ulong AnimPostFxStop = 13041707943063647124UL;

		// Token: 0x04000C8D RID: 3213
		public static string InkDict = "pausemenu_textures";

		// Token: 0x04000C8E RID: 3214
		public static string InkTexture = "menu_ink_large1";

		// Token: 0x04000C8F RID: 3215
		public static bool InkOn = true;

		// Token: 0x04000C90 RID: 3216
		private static int _windowFrame = -1;

		// Token: 0x04000C91 RID: 3217
		public static bool FrameOn = true;

		// Token: 0x04000C92 RID: 3218
		public static bool FrameTint = false;

		// Token: 0x04000C93 RID: 3219
		public static int FrameSize = 16;

		// Token: 0x04000C94 RID: 3220
		public static int FrameOut = 4;

		// Token: 0x04000C95 RID: 3221
		public static int FrameEdge = 16;

		// Token: 0x04000C96 RID: 3222
		public static int FrameInsetX = 28;

		// Token: 0x04000C97 RID: 3223
		public static int FrameInsetY = 12;

		// Token: 0x04000C98 RID: 3224
		public static int CardFrameUp = 1;

		// Token: 0x04000C99 RID: 3225
		private const string FrameDict = "menu_textures";

		// Token: 0x04000C9A RID: 3226
		public const float TitleBearing = 0.0008f;

		// Token: 0x04000C9B RID: 3227
		public const float BodyUnit = 0.0222f;

		// Token: 0x04000C9C RID: 3228
		[TupleElementNames(new string[]
		{
			"first",
			"last"
		})]
		private static readonly Dictionary<string, ValueTuple<int, int>> _marquee = new Dictionary<string, ValueTuple<int, int>>();

		// Token: 0x04000C9D RID: 3229
		private static readonly Dictionary<string, int> _marqueeEnd = new Dictionary<string, int>();

		// Token: 0x04000C9E RID: 3230
		private const int MarqueeWait = 2000;

		// Token: 0x04000C9F RID: 3231
		private const int MarqueeStep = 110;

		// Token: 0x04000CA0 RID: 3232
		private const int MarqueeHold = 2000;

		// Token: 0x04000CA1 RID: 3233
		public static bool MarqueeOn = true;

		// Token: 0x04000CA2 RID: 3234
		[TupleElementNames(new string[]
		{
			"off",
			"text"
		})]
		private static readonly Dictionary<string, ValueTuple<int, string>> _marqueeCut = new Dictionary<string, ValueTuple<int, string>>();

		// Token: 0x04000CA3 RID: 3235
		public const string ModName = "enRAGE";

		// Token: 0x04000CA4 RID: 3236
		public static readonly string ModVersion;

		// Token: 0x04000CA5 RID: 3237
		internal const float ArrowDownHeading = 90f;

		// Token: 0x04000CA6 RID: 3238
		public const float NotorietyBandH = 0.032f;

		// Token: 0x04000CA7 RID: 3239
		public const float StrengthRowH = 0.028f;

		// Token: 0x04000CA8 RID: 3240
		public const float GroundRowH = 0.026f;

		// Token: 0x04000CA9 RID: 3241
		public const string IconDict = "itemtype_textures";

		// Token: 0x04000CAA RID: 3242
		private static readonly HashSet<string> PadFace;

		// Token: 0x04000CAB RID: 3243
		private static readonly HashSet<string> PadPill;

		// Token: 0x04000CAC RID: 3244
		private static readonly HashSet<string> PadStick;

		// Token: 0x04000CAD RID: 3245
		private const float GlyphH = 0.022f;

		// Token: 0x04000CAE RID: 3246
		private const float FaceD = 0.029f;

		// Token: 0x04000CAF RID: 3247
		private static readonly HashSet<string> PadDir;

		// Token: 0x04000CB0 RID: 3248
		private static readonly Dictionary<string, string> PsNames;

		// Token: 0x04000CB1 RID: 3249
		[TupleElementNames(new string[]
		{
			"w",
			"s"
		})]
		private static readonly Dictionary<string, ValueTuple<float, float>> _keyFit;

		// Token: 0x02000457 RID: 1111
		internal enum ArrowDir
		{
			// Token: 0x04001FD4 RID: 8148
			Left,
			// Token: 0x04001FD5 RID: 8149
			Right,
			// Token: 0x04001FD6 RID: 8150
			Down
		}
	}
}
