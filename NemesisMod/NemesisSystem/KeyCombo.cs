using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace NemesisSystem
{
	// Token: 0x02000025 RID: 37
	internal static class KeyCombo
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x000FE5C1 File Offset: 0x000FC7C1
		internal static Keys Base(Keys k)
		{
			return k & Keys.KeyCode;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000FE5CA File Offset: 0x000FC7CA
		internal static Keys Mods(Keys k)
		{
			return k & Keys.Modifiers;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x000FE5D4 File Offset: 0x000FC7D4
		internal static string Name(Keys k)
		{
			List<string> list = new List<string>();
			if ((k & Keys.Control) != Keys.None)
			{
				list.Add("Ctrl");
			}
			if ((k & Keys.Shift) != Keys.None)
			{
				list.Add("Shift");
			}
			if ((k & Keys.Alt) != Keys.None)
			{
				list.Add("Alt");
			}
			list.Add(KeyCombo.Base(k).ToString());
			return string.Join("+", list);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000FE648 File Offset: 0x000FC848
		internal static string ForParse(string v)
		{
			return Regex.Replace(v ?? "", "ctrl", "Control", RegexOptions.IgnoreCase).Replace('+', ',');
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000FE66D File Offset: 0x000FC86D
		private static bool Never(Keys b)
		{
			return b == Keys.Insert || b == Keys.Escape || b == Keys.Back;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000FE67F File Offset: 0x000FC87F
		private static bool PlayKey(Keys b)
		{
			return b == Keys.Return || b == Keys.Space || b == Keys.Tab || b == Keys.Up || b == Keys.Down || b == Keys.Left || b == Keys.Right || b == Keys.W || b == Keys.A || b == Keys.S || b == Keys.D;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000FE6BC File Offset: 0x000FC8BC
		internal static string Problem(string what, Keys combo)
		{
			Keys keys = KeyCombo.Base(combo);
			Keys keys2 = KeyCombo.Mods(combo);
			if (keys == Keys.Insert)
			{
				return "Insert reloads every script - pick another key";
			}
			if (KeyCombo.Never(keys))
			{
				return string.Format("{0} closes things - pick another key", keys);
			}
			Func<string, Keys> bound = KeyCombo.Bound;
			Keys k = (bound != null) ? bound("ForceAmbush") : Keys.F10;
			Func<string, Keys> bound2 = KeyCombo.Bound;
			Keys k2 = (bound2 != null) ? bound2("RosterMenu") : Keys.F11;
			if (what == "RosterMenu")
			{
				if (keys == KeyCombo.Base(k) && (keys2 == Keys.None || keys2 == Keys.Control))
				{
					return KeyCombo.Name(combo) + " is the encounter spawner's - pick another";
				}
				if (KeyCombo.PlayKey(keys))
				{
					return string.Format("{0} is used in play and in the menus, even with Shift / Ctrl / Alt - pick another", keys);
				}
				if ((keys2 & Keys.Alt) != Keys.None && (keys == Keys.F4 || keys == Keys.Tab || keys == Keys.Escape))
				{
					return KeyCombo.Name(combo) + " belongs to Windows - pick another";
				}
				if (keys2 == Keys.None)
				{
					if ((keys < Keys.F1 || keys > Keys.F24) && (keys < Keys.NumPad0 || keys > Keys.Divide) && keys != Keys.Apps && keys != Keys.Pause && keys != Keys.Scroll)
					{
						return string.Format("{0} on its own is used in play or in the menus - an F-key, a numpad key, or hold Shift / Ctrl / Alt with it", keys);
					}
					foreach (string text in new string[]
					{
						"Interrogar",
						"Telégrafo"
					})
					{
						Func<string, Keys> bound3 = KeyCombo.Bound;
						Keys? keys3 = (bound3 != null) ? new Keys?(bound3(text)) : null;
						if (keys3 != null)
						{
							Keys valueOrDefault = keys3.GetValueOrDefault();
							if (KeyCombo.Base(valueOrDefault) == keys && KeyCombo.Mods(valueOrDefault) == Keys.None)
							{
								return string.Format("{0} is the {1} key - pick another", keys, text);
							}
						}
					}
				}
				return null;
			}
			else
			{
				if (keys2 != Keys.None)
				{
					return null;
				}
				if (KeyCombo.PlayKey(keys))
				{
					return string.Format("{0} is used in play and in the menus - pick another", keys);
				}
				if (keys == KeyCombo.Base(k2) && KeyCombo.Mods(k2) == Keys.None)
				{
					return string.Format("{0} is the menu key", keys);
				}
				foreach (string text2 in new string[]
				{
					"Interrogar",
					"Telégrafo",
					"ForceAmbush"
				})
				{
					if (text2 != what)
					{
						Func<string, Keys> bound4 = KeyCombo.Bound;
						Keys? keys3 = (bound4 != null) ? new Keys?(bound4(text2)) : null;
						if (keys3 != null)
						{
							Keys valueOrDefault2 = keys3.GetValueOrDefault();
							if (KeyCombo.Base(valueOrDefault2) == keys)
							{
								return string.Format("{0} is the {1} key - pick another", keys, text2);
							}
						}
					}
				}
				return null;
			}
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000FE950 File Offset: 0x000FCB50
		[return: TupleElementNames(new string[]
		{
			"settings",
			"undo",
			"debug"
		})]
		internal static ValueTuple<Keys, Keys, Keys> Extras(Keys combo)
		{
			KeyCombo.<>c__DisplayClass10_0 CS$<>8__locals1 = new KeyCombo.<>c__DisplayClass10_0();
			CS$<>8__locals1.combo = combo;
			CS$<>8__locals1.free = (from m in new List<Keys>
			{
				Keys.Alt,
				Keys.Shift,
				Keys.Control
			}
			where (KeyCombo.Mods(CS$<>8__locals1.combo) & m) == Keys.None
			select m).ToList<Keys>();
			Keys item = CS$<>8__locals1.<Extras>g__Take|1(Keys.Alt);
			Keys item2 = CS$<>8__locals1.<Extras>g__Take|1(Keys.Shift);
			Keys item3 = CS$<>8__locals1.<Extras>g__Take|1(Keys.Control);
			return new ValueTuple<Keys, Keys, Keys>(item, item2, item3);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000FE9D6 File Offset: 0x000FCBD6
		internal static bool Is(KeyEventArgs e, Keys combo, Keys extra = Keys.None)
		{
			return e != null && e.KeyCode == KeyCombo.Base(combo) && (e.Modifiers & Keys.Modifiers) == (KeyCombo.Mods(combo) | extra);
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x000FEA01 File Offset: 0x000FCC01
		internal static string MenuText
		{
			get
			{
				return KeyCombo.Name(KeyCombo.Current);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x000FEA10 File Offset: 0x000FCC10
		internal static ValueTuple<string, string> PadOpenHint
		{
			get
			{
				if (KeyCombo.PadMode == 1)
				{
					return new ValueTuple<string, string>("DLeft", "Double-tap: open");
				}
				if (KeyCombo.PadMode == 2)
				{
					return new ValueTuple<string, string>("DDown", "Double-tap: open");
				}
				if (KeyCombo.PadMode == 3)
				{
					return new ValueTuple<string, string>("DRight", "Hold: open");
				}
				if (KeyCombo.PadMode != 4)
				{
					return new ValueTuple<string, string>("DRight", "Double-tap: open");
				}
				return new ValueTuple<string, string>(KeyCombo.MenuText, "Open (keyboard)");
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x000FEA8C File Offset: 0x000FCC8C
		internal static string SettingsText
		{
			get
			{
				Keys item = KeyCombo.Extras(KeyCombo.Current).Item1;
				if (item != Keys.None)
				{
					return KeyCombo.Name(KeyCombo.Current | item);
				}
				return "(none)";
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x000FEAC0 File Offset: 0x000FCCC0
		internal static string UndoText
		{
			get
			{
				Keys item = KeyCombo.Extras(KeyCombo.Current).Item2;
				if (item != Keys.None)
				{
					return KeyCombo.Name(KeyCombo.Current | item);
				}
				return "(none)";
			}
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000FEAF4 File Offset: 0x000FCCF4
		internal static string Keyed(string s)
		{
			if (string.IsNullOrEmpty(s))
			{
				return s;
			}
			if (KeyCombo.PadMode != 0)
			{
				string text = (KeyCombo.PadMode == 1) ? "double-tap D-pad Left" : ((KeyCombo.PadMode == 2) ? "double-tap D-pad Down" : ((KeyCombo.PadMode == 3) ? "hold D-pad Right" : "the keyboard key only"));
				s = s.Replace("double-tap Right on the D-pad", text).Replace("double-tap D-pad Right", text).Replace("Double-tap D-pad Right", char.ToUpperInvariant(text[0]).ToString() + text.Substring(1));
			}
			if (KeyCombo.Current == Keys.F11)
			{
				return s;
			}
			ValueTuple<Keys, Keys, Keys> valueTuple = KeyCombo.Extras(KeyCombo.Current);
			string newValue = (valueTuple.Item3 == Keys.None) ? "(none)" : KeyCombo.Name(KeyCombo.Current | valueTuple.Item3);
			return s.Replace("Alt+F11", "\u0001").Replace("Shift+F11", "\u0002").Replace("Ctrl+F11", "\u0003").Replace("F11", "\u0004").Replace("\u0001", KeyCombo.SettingsText).Replace("\u0002", KeyCombo.UndoText).Replace("\u0003", newValue).Replace("\u0004", KeyCombo.MenuText);
		}

		// Token: 0x040007D4 RID: 2004
		internal static Keys Current = Keys.F11;

		// Token: 0x040007D5 RID: 2005
		internal static int PadMode;

		// Token: 0x040007D6 RID: 2006
		internal static Func<string, Keys> Bound;
	}
}
