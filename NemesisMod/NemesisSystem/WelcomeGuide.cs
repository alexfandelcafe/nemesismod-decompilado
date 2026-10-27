using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NemesisSystem
{
	// Token: 0x02000081 RID: 129
	internal sealed class WelcomeGuide
	{
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06001373 RID: 4979 RVA: 0x001335DE File Offset: 0x001317DE
		// (set) Token: 0x06001374 RID: 4980 RVA: 0x001335E6 File Offset: 0x001317E6
		public bool IsOpen { get; private set; }

		// Token: 0x06001375 RID: 4981 RVA: 0x001335EF File Offset: 0x001317EF
		public WelcomeGuide(Action onClose)
		{
			this._onClose = onClose;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x00133614 File Offset: 0x00131814
		public void Open(int page = 0)
		{
			this.IsOpen = true;
			this._setup = false;
			this._whatsNew = false;
			this._readSomething = true;
			this._page = Math.Max(0, Math.Min(WelcomeGuide.Pages.Length - 1, page));
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x00133650 File Offset: 0x00131850
		public void Close()
		{
			if (!this.IsOpen)
			{
				return;
			}
			if (this._warning != null)
			{
				this._warning = null;
				this.IsOpen = false;
				return;
			}
			if (this._whatsNew && this._guideAfterNews)
			{
				this._guideAfterNews = false;
				this._whatsNew = false;
				this._page = 0;
				this._readSomething = true;
				NemesisMod.UiSound("SELECT");
				return;
			}
			bool flag = this._readSomething || this._whatsNew;
			this.IsOpen = false;
			this._setup = false;
			this._whatsNew = false;
			this._capturing = false;
			this._readSomething = false;
			if (flag)
			{
				Action onClose = this._onClose;
				if (onClose == null)
				{
					return;
				}
				onClose();
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06001378 RID: 4984 RVA: 0x00133704 File Offset: 0x00131904
		public bool InWarning
		{
			get
			{
				return this.IsOpen && this._warning != null;
			}
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x0013371B File Offset: 0x0013191B
		public void OpenWarning(string title, string sub, string[] body, string path, string[] steps)
		{
			this._warning = new ValueTuple<string, string, string[], string, string[]>?(new ValueTuple<string, string, string[], string, string[]>(title, sub, body, path, steps));
			this.IsOpen = true;
			this._setup = false;
			this._whatsNew = false;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0013374C File Offset: 0x0013194C
		private static List<string> WrapPath(string p, float scale, float width)
		{
			List<string> list = new List<string>();
			string text = "";
			foreach (string text2 in p.Split(new char[]
			{
				'\\'
			}))
			{
				string text3 = ((text.Length > 0) ? (text + "\\") : "") + text2;
				if (text.Length > 0 && Theme.MixedWidth(text3, scale, 0.0222f) > width)
				{
					list.Add(text + "\\");
					text = text2;
				}
				else
				{
					text = text3;
				}
			}
			if (text.Length > 0)
			{
				list.Add(text);
			}
			return list;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x001337F4 File Offset: 0x001319F4
		private void DrawWarning()
		{
			ValueTuple<string, string, string[], string, string[]> value = this._warning.Value;
			float tw = 0.44f;
			List<string> list = value.Item3.SelectMany((string b) => WelcomeGuide.Wrap(b, 0.27f, tw).Concat(new string[]
			{
				""
			})).ToList<string>();
			List<string> list2 = value.Item5.Select((string s, int i) => string.Format("{0}.  {1}", i + 1, s)).SelectMany((string s) => WelcomeGuide.Wrap(s, 0.27f, tw - 0.01f)).ToList<string>();
			List<string> list3 = (from f in (value.Item4 ?? "").Split(new char[]
			{
				'\n'
			})
			where f.Length > 0
			select f).SelectMany((string f) => WelcomeGuide.WrapPath(f, 0.25f, tw - 0.02f).Concat(new string[]
			{
				""
			})).ToList<string>();
			if (list3.Count > 0)
			{
				list3.RemoveAt(list3.Count - 1);
			}
			float num = 0.15f + (float)list.Count * 0.0245f + (float)list3.Count * 0.024f + 0.04f + (float)list2.Count * 0.0245f + 0.12f;
			float num2 = 0.25f;
			float num3 = Math.Max(0.05f, 0.5f - num / 2f);
			Theme.InkBehind(num2, num3, 0.5f, num, 255, false);
			UiDraw.Rect(num2, num3, 0.5f, num, Theme.Panel.Item1, Theme.Panel.Item2, Theme.Panel.Item3, 240);
			ValueTuple<int, int, int> nativeRed = Theme.NativeRed;
			Theme.Hairline(num2, num3, 0.5f, num, nativeRed.Item1, nativeRed.Item2, nativeRed.Item3, 255, 1);
			Theme.Hairline(num2 + 0.003f, num3 + 0.003f * UiDraw.Aspect, 0.494f, num - 0.006f * UiDraw.Aspect, nativeRed.Item1, nativeRed.Item2, nativeRed.Item3, 200, 1);
			Theme.Corners(num2, num3, 0.5f, num, 230, 0.012f);
			UiDraw.Rect(num2, num3, 0.5f, 0.006f, nativeRed.Item1, nativeRed.Item2, nativeRed.Item3, 255);
			float num4 = num3 + 0.022f;
			string text = "enRAGE".ToUpperInvariant();
			UiDraw.TextFont(text, UiDraw.TitleFont, 0.5f - Theme.CapsWidth(text, 0.21f) / 2f, num4, 0.21f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
			num4 += 0.022f;
			string text2 = value.Item1.ToUpperInvariant();
			float num5 = 0.48f;
			while (num5 > 0.32f && Theme.CapsWidth(text2, num5) > tw)
			{
				num5 -= 0.02f;
			}
			float num6 = 0.5f - Theme.CapsWidth(text2, num5) / 2f;
			UiDraw.TextFont(text2, UiDraw.TitleFont, num6, num4, num5, nativeRed.Item1, nativeRed.Item2, nativeRed.Item3, 255, false);
			UiDraw.TextFont(text2, UiDraw.TitleFont, num6 + 0.00052083336f, num4, num5, nativeRed.Item1, nativeRed.Item2, nativeRed.Item3, 255, false);
			num4 += 0.05f;
			UiDraw.Text(value.Item2, 0.5f - Theme.MixedWidth(value.Item2, 0.25f, 0.0222f) / 2f, num4, 0.25f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255);
			num4 += 0.03f;
			Theme.CenterRule(num2, num4, 0.5f, 0.05f, 150);
			num4 += 0.018f;
			float num7 = num2 + 0.03f;
			foreach (string text3 in list)
			{
				if (UiDraw.TextSlotsLeft < 8)
				{
					break;
				}
				if (text3.Length > 0)
				{
					UiDraw.Text(text3, num7, num4, 0.27f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
				}
				num4 += ((text3.Length > 0) ? 0.0245f : 0.01225f);
			}
			if (list3.Count > 0 && list3[0].Length > 0)
			{
				float h = (float)list3.Count * 0.024f + 0.012f;
				UiDraw.Rect(num7 - 0.006f, num4 - 0.004f, tw + 0.012f, h, 12, 12, 11, 210);
				Theme.Hairline(num7 - 0.006f, num4 - 0.004f, tw + 0.012f, h, Theme.Warn.Item1, Theme.Warn.Item2, Theme.Warn.Item3, 200, 1);
				foreach (string text4 in list3)
				{
					if (text4.Length > 0)
					{
						UiDraw.Text(text4, num7 + 0.004f, num4 + 0.002f, 0.25f, Theme.Warn.Item1, Theme.Warn.Item2, Theme.Warn.Item3, 255);
					}
					num4 += ((text4.Length > 0) ? 0.024f : 0.01f);
				}
				num4 += 0.022f;
			}
			Theme.Section("QUÉ HACER", num7 - 0.004f, num4, num7 + tw, null);
			num4 += 0.03f;
			foreach (string text5 in list2)
			{
				if (UiDraw.TextSlotsLeft < 8)
				{
					break;
				}
				UiDraw.Text(text5, num7 + 0.006f, num4, 0.27f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
				num4 += 0.0245f;
			}
			float num8 = 0.17f;
			float num9 = 0.038f;
			float x = 0.5f - num8 / 2f;
			float num10 = num3 + num - 0.05f - num9;
			UiMouse.Region(x, num10, num8, num9, null, delegate
			{
				NemesisMod.UiSound("SELECT");
				this.Close();
			});
			Theme.SelectBox(x, num10, num8, num9, 235);
			UiDraw.TextFont("ENTENDIDO", UiDraw.TitleFont, 0.5f - Theme.CapsWidth("ENTENDIDO", 0.24f) / 2f, num10 + num9 / 2f - 0.011f, 0.24f, Theme.SelectedText.Item1, Theme.SelectedText.Item2, Theme.SelectedText.Item3, 255, false);
			List<ValueTuple<string, string>> list4;
			if (!XPad.RecentlyUsed)
			{
				(list4 = new List<ValueTuple<string, string>>()).Add(new ValueTuple<string, string>("Enter", "Entendido"));
			}
			else
			{
				(list4 = new List<ValueTuple<string, string>>()).Add(new ValueTuple<string, string>("A", "Entendido"));
			}
			List<ValueTuple<string, string>> hints = list4;
			Theme.CenterRule(num2, num3 + num - 0.04f, 0.5f, 0.012f, 150);
			Theme.HintRow(num7, num7 + tw, num3 + num - 0.032f, hints, 0.21f, Theme.Text);
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600137C RID: 4988 RVA: 0x00134000 File Offset: 0x00132200
		public bool InWhatsNew
		{
			get
			{
				return this.IsOpen && this._whatsNew;
			}
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x00134012 File Offset: 0x00132212
		public void OpenWhatsNew()
		{
			this.IsOpen = true;
			this._setup = false;
			this._whatsNew = true;
			this._page = 0;
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x00134030 File Offset: 0x00132230
		public void ShowWhatsNewFromSetup()
		{
			this._guideAfterNews = this._thenGuide;
			this._setup = false;
			this._capturing = false;
			this._thenGuide = false;
			this._thenWhatsNew = false;
			Action onSetupDone = this.OnSetupDone;
			if (onSetupDone != null)
			{
				onSetupDone();
			}
			this._whatsNew = true;
			NemesisMod.UiSound("SELECT");
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600137F RID: 4991 RVA: 0x00134087 File Offset: 0x00132287
		public bool InSetup
		{
			get
			{
				return this.IsOpen && this._setup;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06001380 RID: 4992 RVA: 0x00134099 File Offset: 0x00132299
		public bool Capturing
		{
			get
			{
				return this.IsOpen && this._setup && this._capturing;
			}
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x001340B4 File Offset: 0x001322B4
		public void OpenSetup(bool thenGuide, bool thenWhatsNew = false)
		{
			Func<List<WelcomeGuide.SetupRow>> setupRows = this.SetupRows;
			this._rows = (((setupRows != null) ? setupRows() : null) ?? new List<WelcomeGuide.SetupRow>());
			this._setup = true;
			this._whatsNew = false;
			this._readSomething = false;
			this._thenGuide = thenGuide;
			this._thenWhatsNew = (thenWhatsNew && !thenGuide);
			this._capturing = false;
			this._row = 0;
			this._setupNote = "";
			this._page = 0;
			this.IsOpen = true;
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x00134138 File Offset: 0x00132338
		public void CloseSetup()
		{
			bool flag = this._thenGuide || this._thenWhatsNew;
			this._thenGuide = false;
			this._thenWhatsNew = false;
			this._setup = false;
			this._capturing = false;
			Action onSetupDone = this.OnSetupDone;
			if (onSetupDone != null)
			{
				onSetupDone();
			}
			if (flag)
			{
				this.IsOpen = false;
				return;
			}
			this.Close();
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x00134194 File Offset: 0x00132394
		private void FinishSetup()
		{
			this._setup = false;
			this._capturing = false;
			Action onSetupDone = this.OnSetupDone;
			if (onSetupDone != null)
			{
				onSetupDone();
			}
			if (this._thenGuide)
			{
				this._page = 0;
				this._readSomething = true;
				NemesisMod.UiSound("SELECT");
				return;
			}
			if (this._thenWhatsNew)
			{
				this._thenWhatsNew = false;
				this._whatsNew = true;
				NemesisMod.UiSound("SELECT");
				return;
			}
			this.Close();
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x00134208 File Offset: 0x00132408
		public void Capture(Keys code, Keys mods)
		{
			if (!this._capturing)
			{
				return;
			}
			if (code == Keys.ShiftKey || code == Keys.ControlKey || code == Keys.Menu || code == Keys.LShiftKey || code == Keys.RShiftKey || code == Keys.LControlKey || code == Keys.RControlKey || code == Keys.LMenu || code == Keys.RMenu)
			{
				return;
			}
			if (code == Keys.Escape || code == Keys.Back)
			{
				this._capturing = false;
				this._setupNote = "Menu key unchanged";
				return;
			}
			string text = KeyCombo.Problem("RosterMenu", code | mods);
			if (text != null)
			{
				this._setupNote = text;
				return;
			}
			this._capturing = false;
			Action<Keys> setMenuKey = this.SetMenuKey;
			if (setMenuKey != null)
			{
				setMenuKey(code | mods);
			}
			this._setupNote = "Menu key: " + KeyCombo.Name(code | mods);
			NemesisMod.UiSound("SELECT");
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x001342D0 File Offset: 0x001324D0
		private bool SetupKey(Keys key)
		{
			if (this._rows.Count == 0)
			{
				this.FinishSetup();
				return true;
			}
			if (this._capturing)
			{
				this._capturing = false;
				this._setupNote = "Menu key unchanged";
				return true;
			}
			WelcomeGuide.SetupRow setupRow = this._rows[Math.Min(this._row, this._rows.Count - 1)];
			if (key <= Keys.Down)
			{
				if (key <= Keys.Return)
				{
					if (key == Keys.Back)
					{
						goto IL_20D;
					}
					if (key != Keys.Return)
					{
						return true;
					}
				}
				else
				{
					if (key == Keys.Escape)
					{
						goto IL_20D;
					}
					switch (key)
					{
					case Keys.Space:
						break;
					case Keys.Prior:
					case Keys.Next:
					case Keys.End:
					case Keys.Home:
						return true;
					case Keys.Left:
						goto IL_148;
					case Keys.Up:
						goto IL_D4;
					case Keys.Right:
						goto IL_16E;
					case Keys.Down:
						goto IL_114;
					default:
						return true;
					}
				}
				if (setupRow.IsStart)
				{
					this.FinishSetup();
					return true;
				}
				if (setupRow.Run != null)
				{
					NemesisMod.UiSound("SELECT");
					setupRow.Run();
					return true;
				}
				if (setupRow.IsKey)
				{
					this._capturing = true;
					this._setupNote = (XPad.RecentlyUsed ? "Press a keyboard key (or Escape) - the controller has its own row below" : "Press the new key - hold Shift / Ctrl / Alt for a combination (Escape cancels)");
					return true;
				}
				if (setupRow.Change != null)
				{
					setupRow.Change(1);
					NemesisMod.UiSound("NAV_RIGHT");
					return true;
				}
				return true;
				IL_20D:
				NemesisMod.UiSound("BACK");
				this.FinishSetup();
				return true;
			}
			if (key <= Keys.D)
			{
				if (key == Keys.A)
				{
					goto IL_148;
				}
				if (key != Keys.D)
				{
					return true;
				}
				goto IL_16E;
			}
			else
			{
				if (key == Keys.S)
				{
					goto IL_114;
				}
				if (key != Keys.W)
				{
					return true;
				}
			}
			IL_D4:
			this._row = (this._row + this._rows.Count - 1) % this._rows.Count;
			NemesisMod.UiSound("NAV_UP");
			this._setupNote = "";
			return true;
			IL_114:
			this._row = (this._row + 1) % this._rows.Count;
			NemesisMod.UiSound("NAV_DOWN");
			this._setupNote = "";
			return true;
			IL_148:
			if (setupRow.Change != null)
			{
				setupRow.Change(-1);
				NemesisMod.UiSound("NAV_LEFT");
				return true;
			}
			return true;
			IL_16E:
			if (setupRow.Change != null)
			{
				setupRow.Change(1);
				NemesisMod.UiSound("NAV_RIGHT");
			}
			return true;
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x001344FC File Offset: 0x001326FC
		private void DrawSetup()
		{
			int num = (from r in this._rows
			where !r.IsStart
			select r.Section).Distinct<string>().Count<string>();
			int num2 = this._rows.Count((WelcomeGuide.SetupRow r) => !r.IsStart);
			float num3 = 0.098f + (float)num * 0.032f + (float)num2 * 0.034f + 0.016f + 0.052f + 0.05f + 0.044f;
			float num4 = 0.32999998f;
			float num5 = Math.Max(0.06f, 0.5f - num3 / 2f);
			Theme.InkBehind(num4, num5, 0.34f, num3, 255, false);
			UiDraw.Rect(num4, num5, 0.34f, num3, Theme.Panel.Item1, Theme.Panel.Item2, Theme.Panel.Item3, 232);
			Theme.Hairline(num4, num5, 0.34f, num3, Theme.Border.Item1, Theme.Border.Item2, Theme.Border.Item3, 230, 1);
			Theme.Corners(num4, num5, 0.34f, num3, 230, 0.012f);
			float num6 = num5 + 0.016f;
			string text = "enRAGE".ToUpperInvariant();
			UiDraw.TextFont(text, UiDraw.TitleFont, 0.5f - Theme.CapsWidth(text, 0.2f) / 2f, num6, 0.2f, Theme.NativeRed.Item1, Theme.NativeRed.Item2, Theme.NativeRed.Item3, 255, false);
			num6 += 0.02f;
			UiDraw.TextFont("AJUSTES", UiDraw.TitleFont, 0.5f - Theme.CapsWidth("AJUSTES", 0.4f) / 2f, num6, 0.4f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			UiDraw.TextFont("AJUSTES", UiDraw.TitleFont, 0.5f - Theme.CapsWidth("AJUSTES", 0.4f) / 2f + 0.00052083336f, num6, 0.4f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			num6 += 0.04f;
			UiDraw.Text("Change any of it later in the settings.", 0.5f - Theme.MixedWidth("Change any of it later in the settings.", 0.21f, 0.0222f) / 2f, num6, 0.21f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			num6 += 0.022f;
			string b = null;
			float num7 = num4 + 0.018f;
			float num8 = 0.30400002f;
			for (int i = 0; i < this._rows.Count; i++)
			{
				WelcomeGuide.SetupRow setupRow = this._rows[i];
				if (!setupRow.IsStart)
				{
					if (setupRow.Section != b)
					{
						b = setupRow.Section;
						Theme.Section((setupRow.Section ?? "").ToUpperInvariant(), num7 - 0.004f, num6 + 0.006f, num7 + num8, null);
						num6 += 0.032f;
					}
					bool flag = i == this._row;
					int ri = i;
					UiMouse.Region(num7, num6, num8, 0.030000001f, delegate
					{
						if (this._row == ri)
						{
							return false;
						}
						this._row = ri;
						return true;
					}, delegate
					{
						this._row = ri;
						this.SetupKey(Keys.Return);
					});
					if (flag)
					{
						Theme.SelectBox(num7, num6, num8, 0.030000001f, 235);
					}
					ValueTuple<int, int, int> valueTuple = flag ? Theme.SelectedText : Theme.Text;
					float y = num6 + 0.015000001f - 0.011f;
					UiDraw.Text(setupRow.Label, num7 + 0.01f, y, 0.25f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
					string text2;
					if (!this._capturing || !flag)
					{
						Func<string> value = setupRow.Value;
						text2 = (((value != null) ? value() : null) ?? "");
					}
					else
					{
						text2 = "press a key...";
					}
					string text3 = text2;
					ValueTuple<int, int, int> valueTuple2 = flag ? Theme.SelectedText : ((text3 == "No") ? Theme.Dim : Theme.Gold);
					float num9 = Theme.MixedWidth(text3, 0.25f, 0.0222f);
					float num10 = num7 + num8 - 0.01f - num9;
					if (!setupRow.IsKey && setupRow.Run == null && (!this._capturing || !flag))
					{
						ValueTuple<int, int, int> c = flag ? Theme.SelectedText : Theme.Faint;
						float num11 = 0.014f;
						float num12 = num11 / UiDraw.Aspect;
						float cy = num6 + 0.015000001f;
						float num13 = num7 + num8 - 0.01f - num12 / 2f;
						Theme.MenuArrow(Theme.ArrowDir.Right, num13, cy, num11, c, 255);
						num10 = num13 - num12 / 2f - 0.006f - num9;
						Theme.MenuArrow(Theme.ArrowDir.Left, num10 - 0.006f - num12 / 2f, cy, num11, c, 255);
					}
					UiDraw.Text(text3, num10, y, 0.25f, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255);
					num6 += 0.034f;
				}
			}
			num6 += 0.01f;
			Theme.HRule(num7, num6, num8, Theme.Faint.Item1, Theme.Faint.Item2, Theme.Faint.Item3, 160, 1);
			num6 += 0.008f;
			WelcomeGuide.SetupRow setupRow2 = (this._rows.Count > 0) ? this._rows[Math.Min(this._row, this._rows.Count - 1)] : null;
			string s = (this._setupNote != "") ? this._setupNote : KeyCombo.Keyed(((setupRow2 != null) ? setupRow2.Help : null) ?? "");
			ValueTuple<int, int, int> valueTuple3 = (this._setupNote != "") ? Theme.Warn : Theme.Dim;
			foreach (string text4 in WelcomeGuide.Wrap(s, 0.21f, num8).Take(2))
			{
				UiDraw.Text(text4, num7, num6, 0.21f, valueTuple3.Item1, valueTuple3.Item2, valueTuple3.Item3, 255);
				num6 += 0.019f;
			}
			num6 = Math.Max(num6, num5 + num3 - 0.094f);
			int si = this._rows.FindIndex((WelcomeGuide.SetupRow r) => r.IsStart);
			if (si >= 0)
			{
				bool flag2 = this._row == si;
				float num14 = 0.16f;
				float num15 = 0.034f;
				float x = 0.5f - num14 / 2f;
				UiMouse.Region(x, num6, num14, num15, delegate
				{
					if (this._row == si)
					{
						return false;
					}
					this._row = si;
					return true;
				}, delegate
				{
					this._row = si;
					this.SetupKey(Keys.Return);
				});
				if (flag2)
				{
					Theme.SelectBox(x, num6, num14, num15, 235);
				}
				else
				{
					Theme.Hairline(x, num6, num14, num15, Theme.Border.Item1, Theme.Border.Item2, Theme.Border.Item3, 230, 1);
				}
				string text5 = this._thenGuide ? "CONFIRM - ON TO THE GUIDE" : "CONFIRMAR";
				float num16 = 0.23f;
				while (num16 > 0.17f && Theme.CapsWidth(text5, num16) > num14 - 0.012f)
				{
					num16 -= 0.01f;
				}
				ValueTuple<int, int, int> valueTuple4 = flag2 ? Theme.SelectedText : Theme.Title;
				UiDraw.TextFont(text5, UiDraw.TitleFont, 0.5f - Theme.CapsWidth(text5, num16) / 2f, num6 + num15 / 2f - 0.01f, num16, valueTuple4.Item1, valueTuple4.Item2, valueTuple4.Item3, 255, false);
			}
			List<ValueTuple<string, string>> list2;
			if (!XPad.RecentlyUsed)
			{
				List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>();
				list.Add(new ValueTuple<string, string>("Arriba/Abajo", "Elegir"));
				list.Add(new ValueTuple<string, string>("Izq./Der.", "Cambiar"));
				list.Add(new ValueTuple<string, string>("Enter", "Seleccionar"));
				list2 = list;
				list.Add(new ValueTuple<string, string>("Backspace", "Confirm & Back"));
			}
			else
			{
				List<ValueTuple<string, string>> list3 = new List<ValueTuple<string, string>>();
				list3.Add(new ValueTuple<string, string>("D-pad", "Elegir"));
				list3.Add(new ValueTuple<string, string>("A", "Seleccionar"));
				list2 = list3;
				list3.Add(new ValueTuple<string, string>("B", "Confirm & Back"));
			}
			List<ValueTuple<string, string>> hints = list2;
			Theme.CenterRule(num4, num5 + num3 - 0.04f, 0.34f, 0.012f, 150);
			Theme.HintRow(num7, num7 + num8, num5 + num3 - 0.032f, hints, 0.21f, Theme.Text);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x00134E98 File Offset: 0x00133098
		private void DrawWhatsNew()
		{
			WelcomeGuide.<>c__DisplayClass45_0 CS$<>8__locals1 = new WelcomeGuide.<>c__DisplayClass45_0();
			CS$<>8__locals1.<>4__this = this;
			WelcomeGuide.Page whatsNewPage = WelcomeGuide.WhatsNewPage;
			CS$<>8__locals1.colW = 0.238f;
			float num = Math.Max(CS$<>8__locals1.<DrawWhatsNew>g__ColH|0(whatsNewPage.Left), CS$<>8__locals1.<DrawWhatsNew>g__ColH|0(whatsNewPage.Right));
			float num2 = 0.112f + num + 0.06f + 0.046f;
			float num3 = 0.22f;
			float num4 = Math.Max(0.05f, 0.5f - num2 / 2f);
			Theme.InkBehind(num3, num4, 0.56f, num2, 255, false);
			UiDraw.Rect(num3, num4, 0.56f, num2, Theme.Panel.Item1, Theme.Panel.Item2, Theme.Panel.Item3, 232);
			Theme.Hairline(num3, num4, 0.56f, num2, Theme.Border.Item1, Theme.Border.Item2, Theme.Border.Item3, 230, 1);
			Theme.Corners(num3, num4, 0.56f, num2, 230, 0.012f);
			CS$<>8__locals1.cy = num4 + 0.018f;
			string text = "enRAGE".ToUpperInvariant();
			UiDraw.TextFont(text, UiDraw.TitleFont, 0.5f - Theme.CapsWidth(text, 0.21f) / 2f, CS$<>8__locals1.cy, 0.21f, Theme.NativeRed.Item1, Theme.NativeRed.Item2, Theme.NativeRed.Item3, 255, false);
			CS$<>8__locals1.cy += 0.021f;
			float num5 = 0.5f - Theme.CapsWidth("NOVEDADES", 0.44f) / 2f;
			UiDraw.TextFont("NOVEDADES", UiDraw.TitleFont, num5, CS$<>8__locals1.cy, 0.44f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			UiDraw.TextFont("NOVEDADES", UiDraw.TitleFont, num5 + 0.00052083336f, CS$<>8__locals1.cy, 0.44f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			CS$<>8__locals1.cy += 0.044f;
			string text2 = "Versión " + Theme.ModVersion + "  -  " + whatsNewPage.Kicker;
			UiDraw.Text(text2, 0.5f - Theme.MixedWidth(text2, 0.23f, 0.0222f) / 2f, CS$<>8__locals1.cy, 0.23f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			CS$<>8__locals1.cy += 0.026f;
			Theme.CenterRule(num3, CS$<>8__locals1.cy, 0.56f, 0.05f, 150);
			CS$<>8__locals1.cy += 0.016f;
			float num6 = num3 + 0.026f;
			float cx = num6 + CS$<>8__locals1.colW + 0.032f;
			CS$<>8__locals1.<DrawWhatsNew>g__Col|1(whatsNewPage.Left, num6);
			CS$<>8__locals1.<DrawWhatsNew>g__Col|1(whatsNewPage.Right, cx);
			Theme.VRule(num6 + CS$<>8__locals1.colW + 0.016f, CS$<>8__locals1.cy, num - 0.016f, Theme.Faint.Item1, Theme.Faint.Item2, Theme.Faint.Item3, 120, 1);
			float num7 = 0.15f;
			float num8 = 0.036f;
			float x = 0.5f - num7 / 2f;
			float num9 = num4 + num2 - 0.046f - num8 - 0.012f;
			UiMouse.Region(x, num9, num7, num8, null, delegate
			{
				NemesisMod.UiSound("SELECT");
				CS$<>8__locals1.<>4__this.Close();
			});
			Theme.SelectBox(x, num9, num7, num8, 235);
			UiDraw.TextFont("CONTINUAR", UiDraw.TitleFont, 0.5f - Theme.CapsWidth("CONTINUAR", 0.24f) / 2f, num9 + num8 / 2f - 0.011f, 0.24f, Theme.SelectedText.Item1, Theme.SelectedText.Item2, Theme.SelectedText.Item3, 255, false);
			List<ValueTuple<string, string>> list2;
			if (!XPad.RecentlyUsed)
			{
				List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>();
				list.Add(new ValueTuple<string, string>("Enter", "Continuar"));
				list2 = list;
				list.Add(new ValueTuple<string, string>("Backspace", "Cerrar"));
			}
			else
			{
				List<ValueTuple<string, string>> list3 = new List<ValueTuple<string, string>>();
				list3.Add(new ValueTuple<string, string>("A", "Continuar"));
				list2 = list3;
				list3.Add(new ValueTuple<string, string>("B", "Cerrar"));
			}
			List<ValueTuple<string, string>> hints = list2;
			Theme.CenterRule(num3, num4 + num2 - 0.042f, 0.56f, 0.012f, 150);
			Theme.HintRow(num6, num6 + 2f * CS$<>8__locals1.colW + 0.032f, num4 + num2 - 0.034f, hints, 0.21f, Theme.Text);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x00135374 File Offset: 0x00133574
		public bool HandleKey(Keys key)
		{
			if (!this.IsOpen)
			{
				return false;
			}
			if (this._warning != null)
			{
				if (key == Keys.Return || key == Keys.Space || key == Keys.Back || key == Keys.Escape)
				{
					NemesisMod.UiSound("SELECT");
					this.Close();
				}
				return true;
			}
			if (this._setup)
			{
				return this.SetupKey(key);
			}
			if (this._whatsNew)
			{
				if (key == Keys.Return || key == Keys.Space)
				{
					NemesisMod.UiSound("SELECT");
					this.Close();
				}
				else if (key == Keys.Back || key == Keys.Escape)
				{
					NemesisMod.UiSound("BACK");
					this.Close();
				}
				return true;
			}
			if (key <= Keys.Right)
			{
				if (key != Keys.Back)
				{
					if (key != Keys.Return)
					{
						switch (key)
						{
						case Keys.Escape:
							goto IL_1A3;
						case Keys.IMEConvert:
						case Keys.IMENonconvert:
						case Keys.IMEAccept:
						case Keys.IMEModeChange:
						case Keys.Up:
							return true;
						case Keys.Space:
							break;
						case Keys.Prior:
						case Keys.Left:
							goto IL_F5;
						case Keys.Next:
						case Keys.Right:
							goto IL_11E;
						case Keys.End:
							this._page = WelcomeGuide.Pages.Length - 1;
							return true;
						case Keys.Home:
							this._page = 0;
							return true;
						default:
							return true;
						}
					}
					if (this._page < WelcomeGuide.Pages.Length - 1)
					{
						this._page++;
						NemesisMod.UiSound("NAV_RIGHT");
						return true;
					}
					NemesisMod.UiSound("SELECT");
					this.Close();
					return true;
				}
				IL_1A3:
				NemesisMod.UiSound("BACK");
				this.Close();
				return true;
			}
			if (key != Keys.A)
			{
				if (key - Keys.D <= 1)
				{
					goto IL_11E;
				}
				if (key != Keys.Q)
				{
					return true;
				}
			}
			IL_F5:
			if (this._page > 0)
			{
				this._page--;
				NemesisMod.UiSound("NAV_LEFT");
				return true;
			}
			return true;
			IL_11E:
			if (this._page < WelcomeGuide.Pages.Length - 1)
			{
				this._page++;
				NemesisMod.UiSound("NAV_RIGHT");
			}
			return true;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x00135538 File Offset: 0x00133738
		public void Draw()
		{
			if (!this.IsOpen)
			{
				return;
			}
			if (this._warning != null)
			{
				this.DrawWarning();
				return;
			}
			if (this._setup)
			{
				this.DrawSetup();
				return;
			}
			if (this._whatsNew)
			{
				this.DrawWhatsNew();
				return;
			}
			WelcomeGuide.Page page = this._whatsNew ? WelcomeGuide.WhatsNewPage : WelcomeGuide.Pages[this._page];
			float num = 0.07f;
			float num2 = 0.86f;
			float num3 = 0.04f;
			float num4 = 0.90199995f;
			if (!Theme.Window(num3, num4))
			{
				Theme.PanelRect(num, num3, num2, num4 - num3, 236);
			}
			string text = page.Title.ToUpperInvariant();
			float num5 = 0.5f;
			while (num5 > 0.34f && Theme.CapsWidth(text, num5) > num2 * 0.7f)
			{
				num5 -= 0.02f;
			}
			float num6 = 0.5f - Theme.CapsWidth(text, num5) / 2f;
			UiDraw.TextFont(text, UiDraw.TitleFont, num6, 0.058f, num5, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			UiDraw.TextFont(text, UiDraw.TitleFont, num6 + 0.00052083336f, 0.058f, num5, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			string text2 = KeyCombo.Keyed(page.Kicker);
			UiDraw.Text(text2, 0.5f - Theme.MixedWidth(text2, 0.25f, 0.0222f) / 2f, 0.1f, 0.25f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			float num7 = 0.0105f;
			float num8 = 0.0075f;
			float num9 = (float)WelcomeGuide.Pages.Length * num7 + (float)(WelcomeGuide.Pages.Length - 1) * num8;
			float num10 = 0.5f - num9 / 2f;
			float num11 = 0.132f;
			int num12 = 0;
			while (num12 < WelcomeGuide.Pages.Length && !this._whatsNew)
			{
				float num13 = num10 + (float)num12 * (num7 + num8);
				ValueTuple<int, int, int> valueTuple = (num12 == this._page) ? Theme.NativeRed : ((num12 < this._page) ? Theme.Text : Theme.Faint);
				UiDraw.Rect(num13, num11, num7, num7 * UiDraw.Aspect, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, (num12 == this._page) ? 255 : 200);
				int pi = num12;
				UiMouse.Region(num13 - 0.002f, num11 - 0.004f, num7 + 0.004f, num7 * UiDraw.Aspect + 0.008f, null, delegate
				{
					this._page = pi;
					NemesisMod.UiSound("NAV_RIGHT");
				});
				num12++;
			}
			Theme.CenterRule(num, 0.16f, num2, 0.05f, 150);
			float x = num + 0.045f;
			float w = num2 - 0.09f;
			float num14 = 0.176f;
			float num15 = 0.15f;
			if (!this._whatsNew)
			{
				UiDraw.Rect(x, num14, w, num15, 12, 12, 11, 170);
				Theme.Hairline(x, num14, w, num15, Theme.Faint.Item1, Theme.Faint.Item2, Theme.Faint.Item3, 160, 1);
				try
				{
					WelcomePics.Draw(this._page, x, num14, w, num15);
				}
				catch
				{
				}
			}
			float y = this._whatsNew ? 0.185f : (num14 + num15 + 0.024f);
			float num16 = 0.04f;
			float num17 = (num2 - 0.09f - num16) / 2f;
			float num18 = num + 0.045f;
			float x2 = num18 + num17 + num16;
			WelcomeGuide.Column(page.Left, num18, y, num17);
			WelcomeGuide.Column(page.Right, x2, y, num17);
			bool recentlyUsed = XPad.RecentlyUsed;
			bool flag = this._page == WelcomeGuide.Pages.Length - 1;
			if (this._whatsNew)
			{
				Theme.Footer(0.868f, recentlyUsed ? "A  Empezar a jugar     B  Cerrar" : "Enter  Empezar a jugar     Retroceso  Cerrar", false);
				return;
			}
			string help = recentlyUsed ? ("LB/RB  Página     A  " + (flag ? "Empezar a jugar" : "Siguiente") + "     B  Cerrar") : ("Izq./Der.  Página     Enter  " + (flag ? "Empezar a jugar" : "Siguiente") + "     Retroceso  Cerrar");
			Theme.Footer(0.868f, help, false);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x001359C8 File Offset: 0x00133BC8
		private static void Column([TupleElementNames(new string[]
		{
			"head",
			"body"
		})] ValueTuple<string, string>[] sections, float x, float y, float width)
		{
			if (sections == null)
			{
				return;
			}
			foreach (ValueTuple<string, string> valueTuple in sections)
			{
				string item = valueTuple.Item1;
				string item2 = valueTuple.Item2;
				if (UiDraw.TextSlotsLeft < 12 || y > 0.80599993f)
				{
					return;
				}
				Theme.Section(item.ToUpperInvariant(), x - 0.004f, y, x + width, null);
				y += 0.03f;
				foreach (string text in KeyCombo.Keyed(item2).Split(new char[]
				{
					'\n'
				}))
				{
					bool flag = text.StartsWith("- ");
					string s = flag ? text.Substring(2) : text;
					float x2 = flag ? (x + 0.014f) : x;
					float width2 = flag ? (width - 0.014f) : width;
					List<string> list = WelcomeGuide.Wrap(s, 0.245f, width2);
					for (int k = 0; k < list.Count; k++)
					{
						if (UiDraw.TextSlotsLeft < 8 || y > 0.8331999f)
						{
							return;
						}
						if (flag && k == 0)
						{
							float num = 0.0042f;
							float num2 = num * UiDraw.Aspect;
							UiDraw.Rect(x + 0.004f, y + 0.009187501f - num2 / 2f, num, num2, Theme.NativeRed.Item1, Theme.NativeRed.Item2, Theme.NativeRed.Item3, 230);
						}
						UiDraw.Text(list[k], x2, y, 0.245f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
						y += 0.0228f;
					}
					y += 0.006f;
				}
				y += 0.014f;
			}
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x00135BA4 File Offset: 0x00133DA4
		private static List<string> Wrap(string s, float scale, float width)
		{
			List<string> list = new List<string>();
			string text = "";
			foreach (string text2 in (s ?? "").Split(new char[]
			{
				' '
			}))
			{
				if (text2.Length != 0)
				{
					string text3 = (text.Length == 0) ? text2 : (text + " " + text2);
					if (text.Length > 0 && Theme.MixedWidth(text3, scale, 0.0222f) > width)
					{
						list.Add(text);
						text = text2;
					}
					else
					{
						text = text3;
					}
				}
			}
			if (text.Length > 0)
			{
				list.Add(text);
			}
			return list;
		}

		// Token: 0x04000D29 RID: 3369
		private int _page;

		// Token: 0x04000D2A RID: 3370
		private readonly Action _onClose;

		// Token: 0x04000D2B RID: 3371
		private bool _readSomething;

		// Token: 0x04000D2C RID: 3372
		[TupleElementNames(new string[]
		{
			"title",
			"sub",
			"body",
			"path",
			"steps"
		})]
		private ValueTuple<string, string, string[], string, string[]>? _warning;

		// Token: 0x04000D2D RID: 3373
		private bool _whatsNew;

		// Token: 0x04000D2E RID: 3374
		private bool _thenWhatsNew;

		// Token: 0x04000D2F RID: 3375
		private bool _guideAfterNews;

		// Token: 0x04000D30 RID: 3376
		private static readonly WelcomeGuide.Page WhatsNewPage = new WelcomeGuide.Page
		{
			Title = "Novedades de la versión 0.9.5.2",
			Kicker = "Un parche de mejoras",
			Left = new ValueTuple<string, string>[]
			{
				new ValueTuple<string, string>("Estrangulamientos y pisotones", "Estrangular a un hombre se registra en los Rayos X (por la espalda también), igual que pisotear a un hombre inconsciente hasta matarlo. Tres pisotones en la cabeza le fracturan el cráneo."),
				new ValueTuple<string, string>("Muerto a patadas", "Patea a un hombre caído hasta matarlo y se le dará por muerto (no por inconsciente), con la probabilidad habitual de que sobreviva en secreto."),
				new ValueTuple<string, string>("Patadas desde la montura", "Una patada a caballo se registra en los Rayos X como patada. Arrollar a alguien a caballo sigue sin contar."),
				new ValueTuple<string, string>("Respuestas a los saludos", "Saluda a los hombres del mod (dolientes, campamentos, conocidos) y tendrás una conversación real: tu saludo y luego el suyo (seco si te odian)."),
				new ValueTuple<string, string>("Apariencias y oficios", "Los chicos de Laramie visten su propia ropa del juego. Hombres y mujeres comparten el trabajo: una mujer puede llevar una forja y un hombre una tienda.")
			},
			Right = new ValueTuple<string, string>[]
			{
				new ValueTuple<string, string>("Sin más desconexiones", "El mod podía desactivarse tras congelarse unos segundos en discos lentos. El autoguardado ya no ralentiza la partida."),
				new ValueTuple<string, string>("Mayor fluidez", "Sin tirones al autoguardar, al cerrar los Rayos X o al buscar un mando. Las patrullas liberan sus modelos en cuanto se marchan."),
				new ValueTuple<string, string>("Guardados asegurados", "La notoriedad y las cuentas de las bandas se guardan incluso si la carpeta scripts tiene permiso de solo lectura."),
				new ValueTuple<string, string>("Corregido", "Aviso de \"funciones ausentes\" en algunas instalaciones. Las peleas a puñetazos ahora son una sola línea en la ficha, no una línea por puñetazo."),
				new ValueTuple<string, string>("También incluido de la 0.9.5.1", "Robos de trenes, alijos ocultos en Saint Denis, suministros y dinero que se trasladan, la familia Bronte en 1907 y peleas en Rayos X.")
			}
		};

		// Token: 0x04000D31 RID: 3377
		public Func<List<WelcomeGuide.SetupRow>> SetupRows;

		// Token: 0x04000D32 RID: 3378
		public Action<Keys> SetMenuKey;

		// Token: 0x04000D33 RID: 3379
		public Action OnSetupDone;

		// Token: 0x04000D34 RID: 3380
		private List<WelcomeGuide.SetupRow> _rows = new List<WelcomeGuide.SetupRow>();

		// Token: 0x04000D35 RID: 3381
		private int _row;

		// Token: 0x04000D36 RID: 3382
		private bool _setup;

		// Token: 0x04000D37 RID: 3383
		private bool _thenGuide;

		// Token: 0x04000D38 RID: 3384
		private bool _capturing;

		// Token: 0x04000D39 RID: 3385
		private string _setupNote = "";

		// Token: 0x04000D3A RID: 3386
		private static readonly WelcomeGuide.Page[] Pages = new WelcomeGuide.Page[]
		{
			new WelcomeGuide.Page
			{
				Title = "Bienvenido a enRAGE",
				Kicker = "Un mundo vivo que recuerda lo que hiciste... y a quién se lo hiciste",
				Left = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Qué es esto", "Cualquiera con quien te cruces en el Oeste puede convertirse en tu NÉMESIS. Róbale, golpéalo, insúltalo o mata a su caballo: puede que no lo olvide. Tendrá un nombre, un carácter, un pasado y un rencor, y un día vendrá a buscarte."),
					new ValueTuple<string, string>("Un mundo que se mueve sin ti", "Ocho facciones —los O'Driscoll, los Lemoyne Raiders, el Ejército de EE. UU., los Wapiti, la familia Bronte, el Klan, la Camada de Murfree y los Hermanos Skinner— tienen sus propios rangos, campamentos, líderes y guerras. Luchan, ascienden y mueren mientras tú cabalgas. En el epílogo han pasado ocho años y la pestaña Mundo cuenta qué fue de todos.")
				},
				Right = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Esta guía", "Unas pocas páginas sobre cada sistema y cómo usarlo. Léela ahora o después; siempre está en el menú.\n- Página sig.: Derecha / E / Enter (mando: RB / A)\n- Página ant.: Izquierda / Q (mando: LB)\n- Cerrar: Retroceso (mando: B)\n- Abrir de nuevo: F11 y luego G (mando: Y)"),
					new ValueTuple<string, string>("Solo modo Historia", "enRAGE nunca se ejecuta en Online. Durante las misiones de historia se mantiene al margen: nada aparece ni interrumpe.")
				}
			},
			new WelcomeGuide.Page
			{
				Title = "El Menú",
				Kicker = "F11 en el teclado, o doble toque a la Derecha en la cruceta",
				Left = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Tres pestañas", "Tab (mando: Vista) cambia entre ellas.\n- JERARQUÍA: los rangos de cada banda, de líder a peón, con los caídos abajo. Los solitarios están en Sin banda.\n- MUNDO: todo lo ocurrido: peleas, disputas, muertes, guerras y tus propios actos.\n- RAYOS X: cada hombre al que has disparado o golpeado, herida a herida y golpe a golpe.\nBUSCAR: mantén Tab (mando: Vista) y escribe un nombre para ir directo a él."),
					new ValueTuple<string, string>("Cómo moverse", "- Q / E (mando: LB / RB): siguiente banda o filtro\n- Flechas / Cruceta: moverse, Enter / A: abrir ficha\n- En su ficha, R (mando: Y): su historial, su mente, sus crímenes\n- A o Inicio (mando: X): tu propia ficha (Arthur o John)\n- Retroceso / B: atrás, F11: cerrar\n- El ratón también funciona: clic y clic derecho para volver")
				},
				Right = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Ajustes rápidos", "- N (mando: LS): Mensajes emergentes en pantalla\n- C (mando: RS): Tarjetas de nombre sobre las cabezas\n- O (mando: Start): Ajustes principales\n- Alt+F11: Todos los ajustes en vivo"),
					new ValueTuple<string, string>("La ficha de un hombre", "Rango y posición, su odio y miedo hacia ti, su carácter y rasgos, parientes y amigos, las cicatrices que le dejaste y el historial entre ambos, además de su biografía desde años antes de conocerte. Su vida lo moldeó: quien huyó de su primera pelea se asusta más fácil; quien se dio a la botella, bebe; quien tuvo una infancia dura se enfurece antes.")
				}
			},
			new WelcomeGuide.Page
			{
				Title = "Haciendo enemigos",
				Kicker = "Todo lo que le hagas a un desconocido cuenta, y también cualquier buen gesto",
				Left = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("El rencor", "Cada desconocido con quien tratas lleva una cuenta silenciosa. Insultos, encañonamientos, puñetazos, robos, atarlo, robar su caballo, dispararle o matar a sus amigos la aumentan. Saludar, calmar las cosas y ser amable la reducen (pero no justo después de hacerle daño).\nSi cruza el límite, se convierte en tu némesis en el acto. Si queda por debajo, lo decidirá al alejarse: cuanto mayor sea, más probable es que te guarde rencor."),
					new ValueTuple<string, string>("Usa una máscara", "Con el pañuelo subido o una máscara puesta: si nunca te vio la cara, no podrá guardarte rencor. Matar con máscara suma solo una cuarta parte de notoriedad y asusta más rápido a la gente. Pero si te quitas la máscara delante de alguien que te vio —o interrogas a cara descubierta—, sabrá que fuiste tú.")
				},
				Right = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Odio", "- Resentido: vendrá a por ti con los puños\n- Vengativo: sacará un cuchillo o quizá un arma\n- Sanguinario: te quiere muerto\nCada hombre tiene su propio límite: un impulsivo echa mano al revólver antes; un errante, después. El odio se apaga con el tiempo en algunos y se encona en otros."),
					new ValueTuple<string, string>("Burlar a la muerte", "Dispara a un némesis y puede que sobreviva con una cicatriz y más furioso. Un tiro en el cerebro o en el corazón, o rematarlo en el suelo, asegura su muerte. Mata a un desconocido y desaparecerá para siempre.")
				}
			},
			new WelcomeGuide.Page
			{
				Title = "Quiénes son",
				Kicker = "Cada némesis es una persona, no una estadística",
				Left = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Carácter", "- Impulsivo: rápido para pelear, lento para perdonar\n- Orgulloso: nunca retrocede, odia ser humillado\n- Frío: paciente, difícil de asustar\n- Cobarde: huye, se rinde o contrata a otros\n- Codicioso: el dinero lo mueve todo\n- Leal: muere por su banda, jamás habla\n- Errante: puede que simplemente lo deje pasar"),
					new ValueTuple<string, string>("Fear", "Las cicatrices, heridas incapacitantes, verte matar a sus amigos y tu sangrienta reputación aterran a los hombres. Los hombres asustados te cazan menos, se rompen en combate, se rinden y hablan.")
				},
				Right = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Mentes y recuerdos", "Los hombres recuerdan lo que hiciste: la vergüenza de una paliza, el duelo por un hermano muerto. Algunos buscan venganza, otros ascender en su banda y otros solo quieren dejar esta vida."),
					new ValueTuple<string, string>("Familia y amigos", "Los hombres tienen hermanos, primos, hijos y amigos dentro y fuera de su banda. Mata a uno y los demás se enterarán en horas o días: llorarán su pérdida o vendrán sedientos de sangre.")
				}
			},
			new WelcomeGuide.Page
			{
				Title = "Bandas y rangos",
				Kicker = "Cada banda es una jerarquía, y puedes desmoronarla",
				Left = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Rangos", "Cada banda tiene un líder y tres escalafones por debajo. Cada pocos días tiene su propia REORGANIZACIÓN: se cubren vacantes, los ambiciosos suben y los supervivientes ascienden. Si a la banda le falta dinero, quedarán puestos vacíos. Mata a un capitán y otro ocupará su lugar; quizá alguien peor."),
					new ValueTuple<string, string>("Líderes", "Los líderes del juego —Colm O'Driscoll, Lindsey Wofford, el coronel Favours, Angelo Bronte, Rains Fall— están en la cúspide. Abre su tarjeta para ver su vida, objetivos, dinero, hombres y guerras. Si matas o capturas a uno, en pocos días su banda elegirá un nuevo líder.")
				},
				Right = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Notoriety", "Mata a los hombres de una banda y aprenderán tu nombre: Desconocido, Visto, Buscado, Perseguido, Deuda de sangre. Cuanto más suba, más a menudo vendrán y con más hombres. Baja con el tiempo o si perdonas vidas, matas a sus enemigos o envías regalos por telégrafo."),
					new ValueTuple<string, string>("Algunos necesitan un motivo", "El Ejército, la familia Bronte y los Wapiti no irán a por ti si no los provocas. Mata a sus hombres —o acumula una gran recompensa— y lo harán. En los caminos te cruzarás sobre todo con rangos bajos.")
				}
			},
			new WelcomeGuide.Page
			{
				Title = "Encuentros",
				Kicker = "Los problemas crecen en silencio... y luego te encuentran",
				Left = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("En campo abierto", "La tensión sube mientras cabalgas —más rápido de noche y con mayor notoriedad— y al llegar al tope aparece alguien: un campamento junto al camino, jinetes tras de ti, un carro o un francotirador. Tras un gran combate la tensión baja del todo. Nada aparece durante misiones, bajo techo o descansando."),
					new ValueTuple<string, string>("Cara a cara", "Muchos encuentros empiezan con palabras. Salúdalo y puede que se eche atrás según su carácter y el miedo que te tenga. Provócalo, desenfunda o dispara y empezará el combate.")
				},
				Right = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("En los pueblos", "La gente que has conocido vuelve a aparecer. Los hombres con rencor beben en la barra, se sientan en los porches y pasean por las calles; la mayoría guarda su rencor y no empezará nada a menos que los provoques, pero de vez en cuando uno se acercará a ajustar cuentas. Como mucho vendrá uno a por ti en cada visita, y los demás no se meterán salvo que los golpees o encañones. Pero si un compañero de su banda, un amigo o un pariente te ve pelear con él, acudirá a apoyarlo. Los oficiales tienen hábitos (una taberna ciertas noches, un camino al atardecer): descúbrelos dos veces o haz hablar a sus hombres y sabrás dónde esperarlos. Si alguien te ataca con puños o cuchillo, un agente de la ley que lo vea se lo llevará al calabozo; si saca un arma, le dispararán. Si golpeas tú primero ante la ley, o sigues pegando a un hombre vencido, la ley irá a por ti."),
					new ValueTuple<string, string>("El mundo a tu alrededor", "En el campo, las patrullas acuden a los disparos. Las bandas rivales chocan en escaramuzas que puedes observar o a las que puedes unirte. En una taberna, los soldados pueden estar bebiendo cuando los Raiders entren por la puerta.")
				}
			},
			new WelcomeGuide.Page
			{
				Title = "Interrogatorio",
				Kicker = "El miedo hace hablar a los hombres",
				Left = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Hacerlo hablar", "Un miembro de banda a tu merced —atado, agarrado, rendido, herido en el suelo o encañonado— puede ser interrogado. Busca INTERROGAR en Cruceta arriba (tecla I en teclado)."),
					new ValueTuple<string, string>("Asustarlo", "El miedo lo es todo. Golpéalo, córtalo, dispara cerca, encañónalo, amenázalo o haz que vea morir a sus amigos. Cada carácter se quiebra distinto: un cobarde cede rápido; un leal quizá no hable nunca.")
				},
				Right = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Qué preguntar", "Las preguntas se agrupan en Qué / Cuándo / Dónde / Por qué: el próximo golpe de su banda, cuándo pillar a su jefe, qué se cuece en la banda, dónde se esconde su jefe, dónde guardan el dinero y suministros, su familia o por qué cabalga con ellos.\n- La primera pregunta es la más difícil, salvo que esté realmente aterrado\n- Si está muerto de miedo, lo más seguro es que hable\n- Cada pregunta muestra la probabilidad de que responda: cuanto más asustado esté, más soltará\n- Cada hombre revela solo uno o dos grandes secretos; si pides más, te dirá por qué no puede\n- Puede mentirte... y acabarás descubriéndolo"),
					new ValueTuple<string, string>("Lo que obtienes", "Campamentos y guaridas marcados en tu mapa, planes que emboscar, hábitos de un oficial, un entierro, parientes y amigos o su pasado. Luego déjalo ir —si está muy asustado puede desertar— o acaba con él.")
				}
			},
			new WelcomeGuide.Page
			{
				Title = "Campamentos y guaridas",
				Kicker = "Encuéntralos, acércate con sigilo y despéjalos",
				Left = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Cómo encontrarlos", "Los oficiales mantienen campamentos y guaridas: los campamentos de bandas del juego, Beaver Hollow, la mansión Bronte, los muelles de Saint Denis o la reserva Wapiti. Pasa cerca de un campamento y lo descubrirás; una guarida solo se marca en tu mapa cuando alguien la delata o combates allí."),
					new ValueTuple<string, string>("Acercarse", "Los campamentos empiezan recelosos, no hostiles. Agáchate, acércate de noche o con niebla y mantente fuera de la vista: la sospecha sube mientras puedan verte. Un disparo o un cadáver que encuentren te delatará."),
					new ValueTuple<string, string>("Tesorerías", "Todas las bandas salvo el Ejército guardan su dinero en un campamento: tiene más guardias y cajas fuertes con gran parte de su fortuna. Pocos de sus hombres saben dónde está —sobre todo los de mayor rango—, pero puedes interrogarlos o recibir un chivatazo por telégrafo. Mantén pulsado para forzar una caja (el campamento lo oirá). Tras un robo, la cambian de sitio."),
					new ValueTuple<string, string>("Supplies", "Cada banda salvo el Ejército guarda también sus suministros en un campamento: cajas, provisiones, un carro y caballos. Destruye sus cajas, vuela las provisiones, roba o destroza el carro y espanta a los caballos para dejarlos sin recursos. También verás carros de suministros escoltados por los caminos y cajas en los pueblos que controlan.")
				},
				Right = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Despejarlos", "Mata o expulsa a todos sus hombres y el campamento quedará vacío unos días. La banda perderá territorio y otra rival podrá ocuparlo. Los forajidos en sus campamentos, cuadrillas y patrullas son blanco legítimo (no tendrás recompensa ni delito por matarlos); los soldados y los Wapiti son otro asunto."),
					new ValueTuple<string, string>("Leer el mapa", "La etiqueta del campamento indica su banda y oficial. (?): te dijeron dónde está pero aún no lo has visto. Un cofre marca suministros y una casa la tesorería; (asaltado) o (robado) indica que ya lo atacaste. Un ? blanco es un campo de batalla; un icono parpadeante es un combate en curso."),
					new ValueTuple<string, string>("Fort Wallace", "El cuartel general del Ejército. Solo un oficial conoce los hábitos del comandante, e incluso él rara vez los revelará. Si matas allí al comandante, el fuerte quedará vacío hasta que el Ejército nombre a otro.")
				}
			},
			new WelcomeGuide.Page
			{
				Title = "El Mundo Vivo",
				Kicker = "La pestaña MUNDO guarda cada historia que los periódicos no publicaron",
				Left = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Cada hora", "Las bandas chocan por campamentos y territorio; los hombres se pelean por cartas y mujeres, se casan, enferman, se emborrachan, van a la cárcel o mueren en duelos. Todo queda registrado en la pestaña MUNDO con las personas implicadas y sus consecuencias."),
					new ValueTuple<string, string>("Territorio y guerras", "Cada distrito está controlado por bandas. Gana combates y el control cambiará. Viejos rencores, roces y la ambición de cada líder inician guerras. Una guerra dura hasta que un bando logra su objetivo, se queda sin hombres o firma la paz. Mata a los oficiales de un bando y su guerra flaqueará; ayuda a uno y lo recordarán."),
					new ValueTuple<string, string>("Fuerza", "Hombres, dinero, suministros y territorio forman la fuerza de una banda. Los golpes llenan su bolsa; robar a sus hombres y arrasar sus campamentos la vacían. Cada baja reduce sus filas: con pocos hombres, sus patrullas y campamentos son menores. Los suministros salen de su territorio y se gastan al luchar: sin suministros, solo llevan revólveres y no pueden iniciar guerras ni reclutar.")
				},
				Right = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Guerras de cerca", "Los distritos en guerra se marcan en el mapa y sufren muchos más combates. Oirás batallas a lo lejos y explosiones de noche. El bando que controla un pueblo monta controles en las entradas y patrulla sus calles: te dejarán pasar salvo que te busquen o la guerra esté al rojo vivo. Mantén Cruceta abajo (o Alt) para ver quién está en guerra; la subpestaña Guerras en MUNDO muestra todos los detalles."),
					new ValueTuple<string, string>("Campos de batalla", "Tras un gran combate, un ? marca el lugar durante un tiempo. Acércate para hallar a los muertos y, a veces, supervivientes. Las bandas entierran a los suyos: un día después de morir un oficial, sus parientes y hombres se reúnen en su tumba unas horas. El periódico publica cuándo y dónde. Observa, presenta tus respetos a pie y desarmado, o líate a tiros: recordarán lo que hagas."),
					new ValueTuple<string, string>("Tu partida guardada es su mundo", "Cada partida guardada conserva su propio mundo. Si cambias de partida guardada, los némesis, el mundo y su historia cambiarán con ella. Shift+F11 deshace el cambio durante dos minutos.")
				}
			},
			new WelcomeGuide.Page
			{
				Title = "Gente y reputación",
				Kicker = "Todo el mundo tiene un nombre... y memoria",
				Left = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Nombres", "Los desconocidos lo son hasta que los saludas. Si saludas a un grupo, los conocerás a todos. La gente que conoces queda registrada y vuelve a aparecer. Su tarjeta muestra \"Desconocido\" hasta que lo saludas, y luego su carácter; si te guarda rencor u odio, mostrará cuánto, y los miembros de banda muestran su rango encima del nombre. Los niños también tienen nombre e historia."),
					new ValueTuple<string, string>("Testigos", "Comete un crimen delante de la gente y lo recordarán: algunos te tendrán miedo, otros avisarán a la ley y a otros podrás intimidarlos para que callen. Los rumores corren por el pueblo. Defenderte de alguien que te atacó primero los asusta mucho menos."),
					new ValueTuple<string, string>("Reputación", "Cada pueblo en el que has actuado te aprecia o te odia, y se muestra en el mapa. Las buenas acciones suben tu reputación (ayudar o rescatar desconocidos, escoltarlos, dar limosna, mostrar clemencia o defender el pueblo de forajidos). Donde tienes mala fama la gente es más desconfiada; donde te aprecian, el miedo baja antes y si alguien te golpea primero, la ley se pondrá de tu parte.")
				},
				Right = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Amabilidad", "A pie, dale a alguien unos dólares (Cruceta arriba) y lo recordará. En una barra, invita a un trago (2 $). Si llevas sombrero, tócatelo para saludar a pie o a caballo (cuenta una vez al día por persona). Saluda cada día: la gente a la que le caes bien hablará bien de ti a sus amigos."),
					new ValueTuple<string, string>("Telegramas", "Visita a cualquier empleado de estación (Cruceta arriba o tecla T) para enviar telegramas a tus némesis —hacer las paces, amenazar, sobornar, preguntar por su jefe— o a conocidos: saludos, regalos, condolencias o favores. Las respuestas tardan unos días. También puedes comprar allí el periódico local (5 centavos) con todas las noticias de la región, guerras, recompensas y próximos funerales.")
				}
			},
			new WelcomeGuide.Page
			{
				Title = "Los Rayos X",
				Kicker = "Cada bala, cada hueso",
				Left = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Herida por herida", "Cada hombre al que disparas queda registrado: la trayectoria de la bala, los huesos rotos, los órganos alcanzados, la entrada y la salida. Abre la pestaña RAYOS X, elige una víctima y un disparo, y gira el esqueleto. Los animales abatidos tienen su propia lista y el veredicto de caza del disparo."),
					new ValueTuple<string, string>("La munición importa", "Las balas de punta dividida se fragmentan, las de alta velocidad atraviesan de lado a lado, las exprés hacen más daño y las explosivas e incendiarias hacen honor a su nombre.")
				},
				Right = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Heridas que perduran", "Un disparo en la columna o el cuello puede dejar inválido a un hombre de por vida; los némesis recuerdan su cojera, su cicatriz o el brazo que nunca soldó bien. Dispara a herir o dispara a matar."),
					new ValueTuple<string, string>("Puños y botas", "Puñetazos, patadas, pisotones y asfixias quedan registrados golpe a golpe, desde un ojo morado hasta un cráneo roto. Si dejas inconsciente a alguien, figurará como noqueado, no muerto."),
					new ValueTuple<string, string>("Controles", "- Arrastrar ratón o stick derecho: girar el esqueleto\n- Rueda o LT / RT: zoom\n- Q / E o LB / RB: Humanos / Animales")
				}
			},
			new WelcomeGuide.Page
			{
				Title = "Estás listo",
				Kicker = "Sal a cabalgar. Haz algunos enemigos.",
				Left = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Algunos consejos", "- Tu primer némesis está a una mala decisión de distancia\n- Las máscaras mantienen limpio tu nombre casi siempre\n- Interroga a los oficiales, no a los peones, para sacar los grandes secretos\n- Revisa la pestaña MUNDO: las bandas no paran aunque no estés\n- Un némesis caído no siempre está muerto: asegúrate con otro tiro")
				},
				Right = new ValueTuple<string, string>[]
				{
					new ValueTuple<string, string>("Hazlo tuyo", "Pulsa O en el menú (mando: Start) para abrir los Ajustes principales, o Alt+F11 para ver todos los ajustes. ¿Otro mod usa la misma tecla? Cámbiala en la fila Tecla del menú. Ajusta los mensajes emergentes y las tarjetas de nombre desde el menú con N y C."),
					new ValueTuple<string, string>("Esta guía", "Pulsa F11 y luego G (mando: Y) para volver a abrir esta guía en cualquier momento.")
				}
			}
		};

		// Token: 0x0200046F RID: 1135
		private sealed class Page
		{
			// Token: 0x04002015 RID: 8213
			public string Title;

			// Token: 0x04002016 RID: 8214
			public string Kicker;

			// Token: 0x04002017 RID: 8215
			[TupleElementNames(new string[]
			{
				"head",
				"body"
			})]
			public ValueTuple<string, string>[] Left;

			// Token: 0x04002018 RID: 8216
			[TupleElementNames(new string[]
			{
				"head",
				"body"
			})]
			public ValueTuple<string, string>[] Right;
		}

		// Token: 0x02000470 RID: 1136
		internal sealed class SetupRow
		{
			// Token: 0x04002019 RID: 8217
			public string Section;

			// Token: 0x0400201A RID: 8218
			public string Label;

			// Token: 0x0400201B RID: 8219
			public string Help;

			// Token: 0x0400201C RID: 8220
			public Func<string> Value;

			// Token: 0x0400201D RID: 8221
			public Action<int> Change;

			// Token: 0x0400201E RID: 8222
			public Action Run;

			// Token: 0x0400201F RID: 8223
			public bool IsKey;

			// Token: 0x04002020 RID: 8224
			public bool IsStart;
		}
	}
}
