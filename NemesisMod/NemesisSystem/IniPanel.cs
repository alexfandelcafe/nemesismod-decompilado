using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace NemesisSystem
{
	// Token: 0x02000023 RID: 35
	internal sealed class IniPanel
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x000F8B80 File Offset: 0x000F6D80
		// (set) Token: 0x06000AC7 RID: 2759 RVA: 0x000F8B88 File Offset: 0x000F6D88
		public bool IsOpen { get; private set; }

		// Token: 0x06000AC8 RID: 2760 RVA: 0x000F8B91 File Offset: 0x000F6D91
		public IniPanel(string path, Action reload)
		{
			this._path = path;
			this._reload = reload;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000F8BCF File Offset: 0x000F6DCF
		public void Toggle()
		{
			this.IsOpen = !this.IsOpen;
			this._binding = null;
			if (this.IsOpen)
			{
				this.Read();
				this._note = "";
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x000F8C00 File Offset: 0x000F6E00
		public bool Capturing
		{
			get
			{
				return this.IsOpen && this._binding != null;
			}
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000F8C18 File Offset: 0x000F6E18
		public void Capture(Keys code, Keys mods)
		{
			if (this._binding == null)
			{
				return;
			}
			if (code == Keys.ShiftKey || code == Keys.ControlKey || code == Keys.Menu || code == Keys.LShiftKey || code == Keys.RShiftKey || code == Keys.LControlKey || code == Keys.RControlKey || code == Keys.LMenu || code == Keys.RMenu)
			{
				return;
			}
			IniPanel.Entry binding = this._binding;
			if (code == Keys.Escape || code == Keys.Back)
			{
				this._binding = null;
				this._note = binding.Key + ": unchanged";
				return;
			}
			bool flag = binding.Key == "RosterMenu";
			string text = KeyCombo.Problem(binding.Key, flag ? (code | mods) : code);
			if (text != null)
			{
				this._note = text;
				return;
			}
			if (!flag && mods != Keys.None)
			{
				this._note = string.Format("{0} takes one key - {1} it is", binding.Key, code);
			}
			string nv = flag ? KeyCombo.Name(code | mods) : code.ToString();
			this._binding = null;
			this.Write(this._sections[this._sec], binding, nv);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000F8D2C File Offset: 0x000F6F2C
		private void Write(IniPanel.Section sec, IniPanel.Entry e, string nv)
		{
			try
			{
				List<string> list = File.ReadAllLines(this._path).ToList<string>();
				int num = IniPanel.FindLine(list, sec.Name, e.Key);
				if (num < 0)
				{
					this._note = e.Key + " moved - reopen the panel";
				}
				else
				{
					list[num] = e.Key + "=" + nv;
					SafeFile.Write(this._path, string.Join("\r\n", list) + "\r\n", false);
					this._reload();
					int sec2 = this._sec;
					int row = this._row;
					this.Read();
					this._sec = Math.Min(sec2, this._sections.Count - 1);
					this._row = Math.Min(row, this._sections[this._sec].Entries.Count - 1);
					IniPanel.Entry entry = this._sections[this._sec].Entries[this._row];
					this._note = ((entry.Value == nv) ? (e.Key + " = " + nv + "  (applied)") : (e.Key + ": the mod kept " + entry.Value + " (out of range?)"));
				}
			}
			catch (Exception ex)
			{
				this._note = "could not save: " + ex.Message;
			}
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000F8EBC File Offset: 0x000F70BC
		private void Read()
		{
			try
			{
				this._lines = File.ReadAllLines(this._path).ToList<string>();
			}
			catch
			{
				this._lines = new List<string>();
			}
			this._sections = new List<IniPanel.Section>();
			IniPanel.Section section = null;
			List<string> list = new List<string>();
			string help = "";
			for (int i = 0; i < this._lines.Count; i++)
			{
				string text = this._lines[i].Trim();
				if (text.StartsWith("[") && text.EndsWith("]"))
				{
					section = new IniPanel.Section
					{
						Name = text.Substring(1, text.Length - 2)
					};
					this._sections.Add(section);
					list.Clear();
					help = "";
				}
				else if (text.StartsWith(";"))
				{
					list.Add(text.TrimStart(new char[]
					{
						';'
					}).Trim());
				}
				else
				{
					int num = text.IndexOf('=');
					if (section == null || num <= 0)
					{
						if (text.Length == 0)
						{
							list.Clear();
							help = "";
						}
					}
					else
					{
						if (list.Count > 0)
						{
							help = string.Join(" ", list);
						}
						if (IniPanel.Internal(section.Name, text.Substring(0, num).Trim()))
						{
							list.Clear();
						}
						else
						{
							section.Entries.Add(new IniPanel.Entry
							{
								Key = text.Substring(0, num).Trim(),
								Value = text.Substring(num + 1).Trim(),
								Help = help,
								Line = i
							});
							list.Clear();
						}
					}
				}
			}
			this._sections.RemoveAll((IniPanel.Section s) => s.Entries.Count == 0);
			this._sec = Math.Max(0, Math.Min(this._sec, this._sections.Count - 1));
			this._row = 0;
			this._scroll = 0;
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x000F90E4 File Offset: 0x000F72E4
		private static string Step(string v, int dir, bool big)
		{
			bool flag;
			if (bool.TryParse(v, out flag))
			{
				return (!flag).ToString();
			}
			int num;
			if (int.TryParse(v, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
			{
				int num2 = Math.Abs(num);
				int num3 = (num2 >= 1000) ? 50 : ((num2 >= 200) ? 10 : ((num2 >= 50) ? 5 : 1));
				return (num + dir * num3 * (big ? 10 : 1)).ToString(CultureInfo.InvariantCulture);
			}
			float num4;
			if (float.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, out num4))
			{
				float num5 = Math.Abs(num4);
				float num6 = (num5 >= 100f) ? 5f : ((num5 >= 10f) ? 0.5f : ((num5 >= 2f) ? 0.1f : 0.05f));
				return ((float)Math.Round((double)(num4 + (float)dir * num6 * (float)(big ? 10 : 1)), 3)).ToString("0.###", CultureInfo.InvariantCulture);
			}
			return null;
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x000F91E8 File Offset: 0x000F73E8
		private static int FindLine(List<string> lines, string section, string key)
		{
			bool flag = false;
			for (int i = 0; i < lines.Count; i++)
			{
				string text = lines[i].Trim();
				if (text.StartsWith("[") && text.EndsWith("]"))
				{
					flag = (text.Substring(1, text.Length - 2) == section);
				}
				else if (flag && text.StartsWith(key + "="))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000F9260 File Offset: 0x000F7460
		private static bool Internal(string section, string key)
		{
			return section == "Meta" || key.EndsWith("Day") || key == "Seeded" || key == "Cleared" || key == "GangCamps" || key == "ArmyTopUp" || section == "Notoriety" || (!IniPanel.Dev && (section == "Debug" || key == "Music" || key == "GameFeed" || (section == "Encuentros" && key == "Force")));
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000F9320 File Offset: 0x000F7520
		public bool HandleKey(Keys key, bool shift)
		{
			if (!this.IsOpen)
			{
				return false;
			}
			if (this._binding != null)
			{
				this._note = this._binding.Key + ": unchanged";
				this._binding = null;
				return true;
			}
			if (this._sections.Count == 0)
			{
				if (key == Keys.Back)
				{
					this.IsOpen = false;
				}
				return true;
			}
			IniPanel.Section section = this._sections[this._sec];
			key = ((key == Keys.W) ? Keys.Up : ((key == Keys.S) ? Keys.Down : ((key == Keys.A) ? Keys.Left : ((key == Keys.D) ? Keys.Right : key))));
			if (key <= Keys.Escape)
			{
				if (key != Keys.Back)
				{
					if (key != Keys.Return)
					{
						if (key != Keys.Escape)
						{
							return true;
						}
					}
					else
					{
						IniPanel.Entry entry = section.Entries[this._row];
						if (section.Name == "Keys" && IniPanel.Bindable.Contains(entry.Key))
						{
							this._binding = entry;
							this._note = "Pulsa la nueva tecla para " + entry.Key + ((entry.Key == "RosterMenu") ? " (puedes usar combinación manteniendo Shift / Ctrl / Alt)" : "") + " - Escape cancela";
							return true;
						}
						this._note = "Enter cambia teclas (sección [Keys]) - Izq. / Der. cambia el resto";
						return true;
					}
				}
				this.IsOpen = false;
				return true;
			}
			switch (key)
			{
			case Keys.Prior:
				break;
			case Keys.Next:
				goto IL_15C;
			case Keys.End:
			case Keys.Home:
				return true;
			case Keys.Left:
			case Keys.Right:
			{
				IniPanel.Entry entry2 = section.Entries[this._row];
				string text = IniPanel.Step(entry2.Value, (key == Keys.Right) ? 1 : -1, shift);
				if (text == null)
				{
					this._note = ((section.Name == "Keys" && IniPanel.Bindable.Contains(entry2.Key)) ? ("Pulsa Enter y luego la nueva tecla para " + entry2.Key) : (entry2.Key + " es texto; edítalo en NemesisMod.ini"));
					return true;
				}
				this.Write(section, entry2, text);
				return true;
			}
			case Keys.Up:
				this._row = Math.Max(0, this._row - 1);
				return true;
			case Keys.Down:
				this._row = Math.Min(section.Entries.Count - 1, this._row + 1);
				return true;
			default:
				if (key == Keys.E)
				{
					goto IL_15C;
				}
				if (key != Keys.Q)
				{
					return true;
				}
				break;
			}
			this._sec = (this._sec + this._sections.Count - 1) % this._sections.Count;
			this._row = 0;
			this._scroll = 0;
			return true;
			IL_15C:
			this._sec = (this._sec + 1) % this._sections.Count;
			this._row = 0;
			this._scroll = 0;
			return true;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000F95D4 File Offset: 0x000F77D4
		public void Draw()
		{
			if (!this.IsOpen)
			{
				return;
			}
			int num = 22;
			float num2 = 0.09f + (float)num * 0.024f + 0.09f;
			Theme.PanelRect(0.3f, 0.1f, 0.4f, num2, 240);
			UiDraw.TextFont("TODOS LOS AJUSTES", UiDraw.TitleFont, 0.312f, 0.108f, 0.32f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			UiDraw.Text("NemesisMod.ini - los cambios se aplican al instante", 0.312f, 0.14f, 0.18f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			if (this._sections.Count == 0)
			{
				UiDraw.Text("No se pudo leer NemesisMod.ini.", 0.312f, 0.17f, 0.2f, Theme.Blood.Item1, Theme.Blood.Item2, Theme.Blood.Item3, 255);
				return;
			}
			IniPanel.Section section = this._sections[this._sec];
			float num3 = 0.24f;
			float num4 = 0.16f;
			float num5 = 0.016f;
			float num6 = num5 / UiDraw.Aspect;
			float cy = num4 + num3 * 0.04f;
			float num7 = 0.312f;
			Theme.MenuArrow(Theme.ArrowDir.Left, num7 + num6 / 2f, cy, num5, Theme.Gold, 255);
			float num8 = num7 + num6 + 0.006f;
			UiDraw.TextFont(section.Name, UiDraw.TitleFont, num8, num4, num3, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 255, false);
			float num9 = Theme.TitleWidth(section.Name, num3);
			float num10 = num8 + num9 + 0.006f;
			Theme.MenuArrow(Theme.ArrowDir.Right, num10 + num6 / 2f, cy, num5, Theme.Gold, 255);
			UiMouse.KeyRegion(num7 - 0.002f, num4, num6 + 0.004f, 0.022f, Keys.Q);
			UiMouse.KeyRegion(num10 - 0.002f, num4, num6 + 0.004f, 0.022f, Keys.E);
			UiDraw.Text(string.Format("{0} / {1}", this._sec + 1, this._sections.Count), num10 + num6 + 0.012f, num4 + 0.004f, 0.19f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			string name = this._sections[(this._sec + this._sections.Count - 1) % this._sections.Count].Name;
			string name2 = this._sections[(this._sec + 1) % this._sections.Count].Name;
			string text = "Q  " + name + "     E  " + name2;
			UiDraw.Text(text, 0.688f - Theme.MixedWidth(text, 0.17f, 0.0222f), 0.164f, 0.17f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			float num11 = Theme.MixedWidth(text, 0.17f, 0.0222f);
			float num12 = 0.688f - num11;
			float num13 = Theme.MixedWidth("Q  " + name + "  ", 0.17f, 0.0222f);
			UiMouse.KeyRegion(num12 - 0.004f, 0.16f, num13 + 0.004f, 0.022f, Keys.Q);
			UiMouse.KeyRegion(num12 + num13, 0.16f, num11 - num13 + 0.004f, 0.022f, Keys.E);
			if (this._row < this._scroll)
			{
				this._scroll = this._row;
			}
			if (this._row >= this._scroll + num)
			{
				this._scroll = this._row - num + 1;
			}
			float num14 = 0.19f;
			int num15 = this._scroll;
			while (num15 < section.Entries.Count && num15 < this._scroll + num)
			{
				IniPanel.Entry entry = section.Entries[num15];
				bool flag = num15 == this._row;
				int ri = num15;
				UiMouse.Region(0.308f, num14 - 0.002f, 0.192f, 0.024f, delegate
				{
					if (this._row == ri)
					{
						return false;
					}
					this._row = ri;
					return true;
				}, delegate
				{
					if (this._clickRow != ri)
					{
						this._clickRow = ri;
						this._row = ri;
						return;
					}
					UiMouse.Send(Keys.Left);
				});
				UiMouse.Region(0.5f, num14 - 0.002f, 0.192f, 0.024f, delegate
				{
					if (this._row == ri)
					{
						return false;
					}
					this._row = ri;
					return true;
				}, delegate
				{
					if (this._clickRow != ri)
					{
						this._clickRow = ri;
						this._row = ri;
						return;
					}
					UiMouse.Send(Keys.Right);
				});
				if (flag)
				{
					Theme.SelectBox(0.308f, num14 - 0.002f, 0.384f, 0.024f, 235);
				}
				ValueTuple<int, int, int> valueTuple = flag ? Theme.SelectedText : Theme.Text;
				UiDraw.Text(entry.Key, 0.314f, num14 + 0.003f, 0.19f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
				string text2 = (entry.Value.Length > 30) ? (entry.Value.Substring(0, 27) + "...") : entry.Value;
				bool flag2;
				ValueTuple<int, int, int> valueTuple2 = flag ? Theme.SelectedText : (bool.TryParse(entry.Value, out flag2) ? (flag2 ? Theme.Good : Theme.Dim) : Theme.Gold);
				UiDraw.Text(text2, 0.68600005f - Theme.MixedWidth(text2, 0.19f, 0.0222f), num14 + 0.003f, 0.19f, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255);
				num14 += 0.024f;
				num15++;
			}
			Theme.ScrollBar(0.69400007f, 0.19f, (float)Math.Min(num, section.Entries.Count) * 0.024f, this._scroll, num, section.Entries.Count);
			float num16 = 0.19f + (float)num * 0.024f + 0.008f;
			Theme.HRule(0.308f, num16, 0.384f, Theme.Faint.Item1, Theme.Faint.Item2, Theme.Faint.Item3, 200, 1);
			IniPanel.Entry entry2 = section.Entries[Math.Min(this._row, section.Entries.Count - 1)];
			string text3 = string.IsNullOrEmpty(entry2.Help) ? "(sin descripción)" : entry2.Help;
			int num17 = 117;
			string[] array = text3.Split(new char[]
			{
				' '
			});
			List<string> list = new List<string>();
			string text4 = "";
			foreach (string text5 in array)
			{
				if (text4.Length + text5.Length + 1 > num17 && text4.Length > 0)
				{
					list.Add(text4);
					text4 = text5;
				}
				else
				{
					text4 = ((text4.Length == 0) ? text5 : (text4 + " " + text5));
				}
			}
			if (text4.Length > 0)
			{
				list.Add(text4);
			}
			float num18 = num16 + 0.008f;
			foreach (string text6 in list.Take(3))
			{
				UiDraw.Text(text6, 0.312f, num18, 0.18f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
				num18 += 0.018f;
			}
			if (this._note != "")
			{
				UiDraw.Text(this._note, 0.312f, 0.1f + num2 - 0.04f, 0.18f, Theme.Warn.Item1, Theme.Warn.Item2, Theme.Warn.Item3, 255);
			}
			List<ValueTuple<string, string>> list3;
			if (!XPad.RecentlyUsed)
			{
				List<ValueTuple<string, string>> list2 = new List<ValueTuple<string, string>>();
				list2.Add(new ValueTuple<string, string>("Arriba/Abajo", "Navegar"));
				list2.Add(new ValueTuple<string, string>("Izq./Der.", "Cambiar"));
				list2.Add(new ValueTuple<string, string>("Shift", "x10"));
				list2.Add(new ValueTuple<string, string>("Q/E", "Sección"));
				list3 = list2;
				list2.Add(new ValueTuple<string, string>("Backspace", "Cerrar"));
			}
			else
			{
				List<ValueTuple<string, string>> list4 = new List<ValueTuple<string, string>>();
				list4.Add(new ValueTuple<string, string>("D-pad", "Navegar"));
				list4.Add(new ValueTuple<string, string>("LB/RB", "Sección"));
				list4.Add(new ValueTuple<string, string>("X", "Mantener x10"));
				list3 = list4;
				list4.Add(new ValueTuple<string, string>("B", "Cerrar"));
			}
			List<ValueTuple<string, string>> hints = list3;
			Theme.HintRow(0.31f, 0.69000006f, 0.1f + num2 - 0.026f, hints, 0.19f, Theme.Dim);
		}

		// Token: 0x040007C2 RID: 1986
		private readonly string _path;

		// Token: 0x040007C3 RID: 1987
		private readonly Action _reload;

		// Token: 0x040007C4 RID: 1988
		private List<string> _lines = new List<string>();

		// Token: 0x040007C5 RID: 1989
		private List<IniPanel.Section> _sections = new List<IniPanel.Section>();

		// Token: 0x040007C6 RID: 1990
		private int _sec;

		// Token: 0x040007C7 RID: 1991
		private int _row;

		// Token: 0x040007C8 RID: 1992
		private int _scroll;

		// Token: 0x040007C9 RID: 1993
		private string _note = "";

		// Token: 0x040007CB RID: 1995
		private static readonly HashSet<string> Bindable = new HashSet<string>
		{
			"RosterMenu",
			"ForceAmbush",
			"Interrogar",
			"Telégrafo"
		};

		// Token: 0x040007CC RID: 1996
		private IniPanel.Entry _binding;

		// Token: 0x040007CD RID: 1997
		internal static bool Dev;

		// Token: 0x040007CE RID: 1998
		private int _clickRow = -1;

		// Token: 0x020003FC RID: 1020
		private sealed class Entry
		{
			// Token: 0x04001D15 RID: 7445
			public string Key;

			// Token: 0x04001D16 RID: 7446
			public string Value;

			// Token: 0x04001D17 RID: 7447
			public string Help;

			// Token: 0x04001D18 RID: 7448
			public int Line;
		}

		// Token: 0x020003FD RID: 1021
		private sealed class Section
		{
			// Token: 0x04001D19 RID: 7449
			public string Name;

			// Token: 0x04001D1A RID: 7450
			public readonly List<IniPanel.Entry> Entries = new List<IniPanel.Entry>();
		}
	}
}
