using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace NemesisSystem
{
	// Token: 0x02000064 RID: 100
	internal sealed class RosterMenu
	{
		// Token: 0x060011D7 RID: 4567 RVA: 0x00112E60 File Offset: 0x00111060
		private float DrawCrimeSheet(NemesisRecord n, float rx, float colW, float y, float bottom, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> acc)
		{
			RosterMenu.<>c__DisplayClass1_0 CS$<>8__locals1;
			CS$<>8__locals1.y = y;
			CS$<>8__locals1.bottom = bottom;
			CS$<>8__locals1.rx = rx;
			CS$<>8__locals1.acc = acc;
			CS$<>8__locals1.colW = colW;
			CrimeProfile crimeProfile = this._mod.CrimeSheet(n);
			CS$<>8__locals1.right = CS$<>8__locals1.rx + CS$<>8__locals1.colW;
			CS$<>8__locals1.skip = this._crimeScroll;
			CS$<>8__locals1.total = 0;
			CS$<>8__locals1.drawn = 0;
			CS$<>8__locals1.over = false;
			Theme.Section(crimeProfile.You ? "TU HISTORIAL CRIMINAL" : "PERFIL CRIMINAL", CS$<>8__locals1.rx, CS$<>8__locals1.y, CS$<>8__locals1.right, new ValueTuple<int, int, int>?(CS$<>8__locals1.acc));
			CS$<>8__locals1.y += 0.032f;
			float y2 = CS$<>8__locals1.y;
			if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.066f, 10, ref CS$<>8__locals1))
			{
				UiDraw.Rect(CS$<>8__locals1.rx, CS$<>8__locals1.y, CS$<>8__locals1.colW, 0.06f, Theme.Card.Item1, Theme.Card.Item2, Theme.Card.Item3, 235);
				Theme.Hairline(CS$<>8__locals1.rx, CS$<>8__locals1.y, CS$<>8__locals1.colW, 0.06f, crimeProfile.BannerCol.Item1, crimeProfile.BannerCol.Item2, crimeProfile.BannerCol.Item3, 170, 1);
				UiDraw.Rect(CS$<>8__locals1.rx, CS$<>8__locals1.y, 0.0015625001f, 0.06f, crimeProfile.BannerCol.Item1, crimeProfile.BannerCol.Item2, crimeProfile.BannerCol.Item3, 255);
				UiDraw.TextFont(crimeProfile.BannerLabel, UiDraw.TitleFont, CS$<>8__locals1.rx + 0.014f, CS$<>8__locals1.y + 0.009f, 0.24f, crimeProfile.BannerCol.Item1, crimeProfile.BannerCol.Item2, crimeProfile.BannerCol.Item3, 255, false);
				UiDraw.Text(RosterMenu.FitText(crimeProfile.BannerWord ?? "", 0.19f, 0.15f, false), CS$<>8__locals1.rx + 0.014f, CS$<>8__locals1.y + 0.034f, 0.19f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
				if (!string.IsNullOrEmpty(crimeProfile.BannerValue))
				{
					ValueTuple<int, int, int> valueTuple = crimeProfile.You ? crimeProfile.BannerCol : ((crimeProfile.BannerValue == "-") ? Theme.Dim : RosterMenu.PriceGold);
					UiDraw.TextFont(crimeProfile.BannerValue, UiDraw.TitleFont, CS$<>8__locals1.rx + 0.17f, CS$<>8__locals1.y + 0.03f - 0.017250001f, 0.46f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255, false);
				}
				if (crimeProfile.You && crimeProfile.Rep != -2147483648)
				{
					float num = 0.11f;
					float num2 = CS$<>8__locals1.right - num - 0.012f;
					ValueTuple<int, int, int> valueTuple2 = NemesisMod.RepColor(crimeProfile.Rep);
					UiDraw.TextFont("REPUTATION", UiDraw.TitleFont, num2, CS$<>8__locals1.y + 0.008f, 0.17f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
					string text = NemesisMod.RepWord(crimeProfile.Rep).ToUpperInvariant();
					UiDraw.TextFont(text, UiDraw.TitleFont, num2 + num - Theme.CapsWidth(text, 0.2f), CS$<>8__locals1.y + 0.007f, 0.2f, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255, false);
					RosterMenu.TwoSided(num2, CS$<>8__locals1.y + 0.04f, num, crimeProfile.Rep, valueTuple2);
				}
				else if (!string.IsNullOrEmpty(crimeProfile.Side))
				{
					float num3 = Theme.CapsWidth(crimeProfile.Side, 0.21f) + 0.014f;
					float num4 = CS$<>8__locals1.right - (CS$<>8__locals1.rx + 0.27f) - 0.012f;
					string text2 = (num3 > num4) ? RosterMenu.FitText(crimeProfile.Side, 0.21f, num4 - 0.014f, false) : crimeProfile.Side;
					num3 = Theme.CapsWidth(text2, 0.21f) + 0.014f;
					Theme.Chip(text2, CS$<>8__locals1.right - num3 - 0.012f, CS$<>8__locals1.y + 0.03f - 0.011f, crimeProfile.SideCol, crimeProfile.SideFilled);
				}
				CS$<>8__locals1.y += 0.074f;
			}
			if (!crimeProfile.You)
			{
				if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.06f, 14, ref CS$<>8__locals1))
				{
					RosterMenu.<DrawCrimeSheet>g__Sec|1_2(crimeProfile.CrimesTitle, ref CS$<>8__locals1);
				}
				foreach (ValueTuple<string, string, string> valueTuple3 in crimeProfile.Crimes)
				{
					string item = valueTuple3.Item1;
					string item2 = valueTuple3.Item2;
					string item3 = valueTuple3.Item3;
					if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.027f, 14, ref CS$<>8__locals1))
					{
						UiDraw.Text(item ?? "", CS$<>8__locals1.rx + 0.008f, CS$<>8__locals1.y + 0.002f, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
						float num5 = CS$<>8__locals1.rx + 0.07f;
						UiDraw.Text(RosterMenu.FitText(item2 ?? "", 0.22f, CS$<>8__locals1.right - 0.088f - 0.012f - num5, false), num5, CS$<>8__locals1.y + 0.001f, 0.22f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
						Theme.Badge(item3, CS$<>8__locals1.right - 0.088f - 0.004f, CS$<>8__locals1.y - 0.002f, 0.088f, Theme.Warn, false);
						CS$<>8__locals1.y += 0.028f;
					}
				}
				RosterMenu.<DrawCrimeSheet>g__Note|1_3(crimeProfile.CrimesNote, ref CS$<>8__locals1);
				CS$<>8__locals1.y += 0.008f;
				if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.06f, 14, ref CS$<>8__locals1))
				{
					RosterMenu.<DrawCrimeSheet>g__Sec|1_2("EN EL MUNDO", ref CS$<>8__locals1);
				}
				foreach (ValueTuple<string, ValueTuple<int, int, int>, string, string, ValueTuple<int, int, int>> valueTuple4 in crimeProfile.Events)
				{
					string item4 = valueTuple4.Item1;
					ValueTuple<int, int, int> item5 = valueTuple4.Item2;
					string item6 = valueTuple4.Item3;
					string item7 = valueTuple4.Item4;
					ValueTuple<int, int, int> item8 = valueTuple4.Item5;
					if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.027f, 14, ref CS$<>8__locals1))
					{
						Theme.Badge(item4, CS$<>8__locals1.rx + 0.004f, CS$<>8__locals1.y - 0.002f, 0.078f, item5, false);
						float num6 = CS$<>8__locals1.rx + 0.078f + 0.014f;
						UiDraw.Text(RosterMenu.FitText(item6, 0.21f, CS$<>8__locals1.right - 0.092f - 0.012f - num6, false), num6, CS$<>8__locals1.y + 0.002f, 0.21f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
						if (!string.IsNullOrEmpty(item7))
						{
							Theme.Badge(item7, CS$<>8__locals1.right - 0.092f - 0.004f, CS$<>8__locals1.y - 0.002f, 0.092f, item8, item7 == "KILLED");
						}
						CS$<>8__locals1.y += 0.028f;
					}
				}
				RosterMenu.<DrawCrimeSheet>g__Note|1_3(crimeProfile.EventsNote, ref CS$<>8__locals1);
				CS$<>8__locals1.y += 0.008f;
			}
			if (crimeProfile.You && crimeProfile.Events.Count > 0)
			{
				if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.06f, 14, ref CS$<>8__locals1))
				{
					RosterMenu.<DrawCrimeSheet>g__Sec|1_2("TU HISTORIA", ref CS$<>8__locals1);
				}
				foreach (ValueTuple<string, ValueTuple<int, int, int>, string, string, ValueTuple<int, int, int>> valueTuple5 in crimeProfile.Events)
				{
					string item9 = valueTuple5.Item1;
					ValueTuple<int, int, int> item10 = valueTuple5.Item2;
					string item11 = valueTuple5.Item3;
					string item12 = valueTuple5.Item4;
					if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.027f, 14, ref CS$<>8__locals1))
					{
						Theme.Badge(item9, CS$<>8__locals1.rx + 0.004f, CS$<>8__locals1.y - 0.002f, 0.078f, item10, false);
						float num7 = CS$<>8__locals1.rx + 0.078f + 0.014f;
						UiDraw.Text(RosterMenu.FitText(item11, 0.21f, CS$<>8__locals1.right - 0.06f - 0.012f - num7, false), num7, CS$<>8__locals1.y + 0.002f, 0.21f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
						UiDraw.Text(item12 ?? "", CS$<>8__locals1.right - 0.06f, CS$<>8__locals1.y + 0.003f, 0.19f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
						CS$<>8__locals1.y += 0.028f;
					}
				}
				RosterMenu.<DrawCrimeSheet>g__Note|1_3(crimeProfile.EventsNote, ref CS$<>8__locals1);
				CS$<>8__locals1.y += 0.008f;
			}
			if (crimeProfile.Tiles.Count > 0)
			{
				if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.1f, 20, ref CS$<>8__locals1))
				{
					RosterMenu.<DrawCrimeSheet>g__Sec|1_2(crimeProfile.TilesTitle, ref CS$<>8__locals1);
					CS$<>8__locals1.y = RosterMenu.StatTiles(CS$<>8__locals1.rx, CS$<>8__locals1.colW, CS$<>8__locals1.y, crimeProfile.Tiles, crimeProfile.You ? 4 : 3, CS$<>8__locals1.bottom);
				}
			}
			else if (crimeProfile.You && RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.06f, 14, ref CS$<>8__locals1))
			{
				RosterMenu.<DrawCrimeSheet>g__Sec|1_2(crimeProfile.TilesTitle, ref CS$<>8__locals1);
				if (RosterMenu.<DrawCrimeSheet>g__Room|1_0(0.022f, 6, ref CS$<>8__locals1))
				{
					UiDraw.Text("Nada registrado aún", CS$<>8__locals1.rx + 0.01f, CS$<>8__locals1.y, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
					CS$<>8__locals1.y += 0.022f;
				}
			}
			if (!crimeProfile.You)
			{
				return this.CrimeScrollBar(CS$<>8__locals1.rx, CS$<>8__locals1.colW, y2, CS$<>8__locals1.bottom, ref CS$<>8__locals1.y, CS$<>8__locals1.total, CS$<>8__locals1.drawn, CS$<>8__locals1.over);
			}
			if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.06f, 14, ref CS$<>8__locals1))
			{
				RosterMenu.<DrawCrimeSheet>g__Sec|1_2("MUERTOS CON NOMBRE", ref CS$<>8__locals1);
				if (crimeProfile.Dead.Count == 0)
				{
					if (RosterMenu.<DrawCrimeSheet>g__Room|1_0(0.022f, 6, ref CS$<>8__locals1))
					{
						UiDraw.Text("Nadie en las listas", CS$<>8__locals1.rx + 0.01f, CS$<>8__locals1.y, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
						CS$<>8__locals1.y += 0.022f;
					}
				}
				else
				{
					float num8 = CS$<>8__locals1.rx + 0.006f;
					foreach (string text3 in crimeProfile.Dead)
					{
						string text4 = text3.ToUpperInvariant();
						float num9 = Theme.CapsWidth(text4, 0.21f) + 0.014f;
						if (num8 + num9 > CS$<>8__locals1.right)
						{
							num8 = CS$<>8__locals1.rx + 0.006f;
							CS$<>8__locals1.y += 0.028f;
						}
						if (!RosterMenu.<DrawCrimeSheet>g__Room|1_0(0.028f, 6, ref CS$<>8__locals1))
						{
							break;
						}
						num8 += Theme.Chip(text4, num8, CS$<>8__locals1.y, Theme.Blood, false) + 0.006f;
					}
					if (crimeProfile.DeadMore > 0 && RosterMenu.<DrawCrimeSheet>g__Room|1_0(0.028f, 4, ref CS$<>8__locals1))
					{
						string text5 = string.Format("+{0} MÁS", crimeProfile.DeadMore);
						float num10 = Theme.CapsWidth(text5, 0.21f) + 0.014f;
						if (num8 + num10 > CS$<>8__locals1.right)
						{
							num8 = CS$<>8__locals1.rx + 0.006f;
							CS$<>8__locals1.y += 0.028f;
						}
						Theme.Chip(text5, num8, CS$<>8__locals1.y, Theme.Dim, false);
					}
					CS$<>8__locals1.y += 0.034f;
				}
			}
			if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.1f, 20, ref CS$<>8__locals1))
			{
				RosterMenu.<DrawCrimeSheet>g__Sec|1_2("WITNESSES", ref CS$<>8__locals1);
				CS$<>8__locals1.y = RosterMenu.StatTiles(CS$<>8__locals1.rx, CS$<>8__locals1.colW, CS$<>8__locals1.y, crimeProfile.Witnesses, 3, CS$<>8__locals1.bottom);
			}
			if (crimeProfile.Towns.Count > 0)
			{
				if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.06f, 14, ref CS$<>8__locals1))
				{
					RosterMenu.<DrawCrimeSheet>g__Sec|1_2("POR PUEBLO", ref CS$<>8__locals1);
				}
				foreach (ValueTuple<string, int> valueTuple6 in crimeProfile.Towns)
				{
					string item13 = valueTuple6.Item1;
					int item14 = valueTuple6.Item2;
					if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.026f, 6, ref CS$<>8__locals1))
					{
						ValueTuple<int, int, int> valueTuple7 = NemesisMod.RepColor(item14);
						UiDraw.TextFont(RosterMenu.FitTitle(item13.ToUpperInvariant(), 0.2f, 0.1f, false), UiDraw.TitleFont, CS$<>8__locals1.rx + 0.008f, CS$<>8__locals1.y + 0.002f, 0.2f, Theme.Gold.Item1, Theme.Gold.Item2, Theme.Gold.Item3, 255, false);
						UiDraw.TextFont(NemesisMod.RepWord(item14).ToUpperInvariant(), UiDraw.TitleFont, CS$<>8__locals1.rx + 0.115f, CS$<>8__locals1.y + 0.002f, 0.2f, valueTuple7.Item1, valueTuple7.Item2, valueTuple7.Item3, 255, false);
						RosterMenu.TwoSided(CS$<>8__locals1.rx + 0.195f, CS$<>8__locals1.y + 0.01f, CS$<>8__locals1.right - CS$<>8__locals1.rx - 0.205f, item14, valueTuple7);
						CS$<>8__locals1.y += 0.026f;
					}
				}
				CS$<>8__locals1.y += 0.008f;
			}
			if (crimeProfile.Factions.Count > 0)
			{
				if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.06f, 14, ref CS$<>8__locals1))
				{
					RosterMenu.<DrawCrimeSheet>g__Sec|1_2("FACCIONES", ref CS$<>8__locals1);
				}
				foreach (ValueTuple<string, int, ValueTuple<int, int, int>> valueTuple8 in crimeProfile.Factions)
				{
					string item15 = valueTuple8.Item1;
					int item16 = valueTuple8.Item2;
					ValueTuple<int, int, int> item17 = valueTuple8.Item3;
					if (RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.026f, 6, ref CS$<>8__locals1))
					{
						UiDraw.TextFont(RosterMenu.FitTitle(item15.ToUpperInvariant(), 0.2f, 0.15f, false), UiDraw.TitleFont, CS$<>8__locals1.rx + 0.008f, CS$<>8__locals1.y + 0.002f, 0.2f, item17.Item1, item17.Item2, item17.Item3, 255, false);
						ValueTuple<int, int, int> valueTuple9 = Notoriety.LevelColors[Notoriety.Level(item16)];
						UiDraw.TextFont(Notoriety.LevelName(item16).ToUpperInvariant(), UiDraw.TitleFont, CS$<>8__locals1.rx + 0.165f, CS$<>8__locals1.y + 0.002f, 0.2f, valueTuple9.Item1, valueTuple9.Item2, valueTuple9.Item3, 255, false);
						float num11 = CS$<>8__locals1.rx + 0.255f;
						float num12 = CS$<>8__locals1.right - num11 - 0.01f;
						float h = 0.0046296297f;
						UiDraw.Rect(num11, CS$<>8__locals1.y + 0.008f, num12, h, Theme.CardHi.Item1, Theme.CardHi.Item2, Theme.CardHi.Item3, 235);
						UiDraw.Rect(num11, CS$<>8__locals1.y + 0.008f, num12 * (float)Math.Max(0, Math.Min(100, item16)) / 100f, h, valueTuple9.Item1, valueTuple9.Item2, valueTuple9.Item3, 255);
						CS$<>8__locals1.y += 0.026f;
					}
				}
			}
			return this.CrimeScrollBar(CS$<>8__locals1.rx, CS$<>8__locals1.colW, y2, CS$<>8__locals1.bottom, ref CS$<>8__locals1.y, CS$<>8__locals1.total, CS$<>8__locals1.drawn, CS$<>8__locals1.over);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00113FCC File Offset: 0x001121CC
		private float CrimeScrollBar(float rx, float colW, float top, float bottom, ref float y, int total, int drawn, bool over)
		{
			this._crimeTotal = total;
			if (this._crimeScroll > 0 && drawn == 0)
			{
				this._crimeScroll = Math.Max(0, total - 1);
			}
			if (over || this._crimeScroll > 0)
			{
				Theme.ScrollBar(rx + colW + 0.004f, top, Math.Max(0.02f, bottom - top), this._crimeScroll, Math.Max(1, drawn), Math.Max(1, total));
			}
			return y;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00114044 File Offset: 0x00112244
		private static float StatTiles(float x, float w, float y, [TupleElementNames(new string[]
		{
			"num",
			"label",
			"col",
			"r",
			"g",
			"b"
		})] IList<ValueTuple<string, string, ValueTuple<int, int, int>>> tiles, int perRow, float bottom = 1f)
		{
			int num = Math.Min(perRow, Math.Max(1, tiles.Count));
			float num2 = (w - 0.008f * (float)(num - 1)) / (float)num;
			for (int i = 0; i < tiles.Count; i++)
			{
				int num3 = i % num;
				if (num3 == 0 && i > 0)
				{
					y += 0.064f;
				}
				if (y + 0.056f > bottom || UiDraw.TextSlotsLeft < 6)
				{
					return y;
				}
				float num4 = x + (float)num3 * (num2 + 0.008f);
				ValueTuple<string, string, ValueTuple<int, int, int>> valueTuple = tiles[i];
				string item = valueTuple.Item1;
				string item2 = valueTuple.Item2;
				ValueTuple<int, int, int> item3 = valueTuple.Item3;
				UiDraw.Rect(num4, y, num2, 0.056f, Theme.Card.Item1, Theme.Card.Item2, Theme.Card.Item3, 235);
				Theme.Hairline(num4, y, num2, 0.056f, Theme.CardHi.Item1, Theme.CardHi.Item2, Theme.CardHi.Item3, 235, 1);
				float num5 = 0.17f;
				while (num5 > 0.12f && Theme.CapsWidth(item2, num5) > num2 - 0.01f)
				{
					num5 -= 0.01f;
				}
				float num6 = 0.0285f;
				float num7 = num5 * 0.075f;
				float num8 = y + (0.056f - num6 - 0.002f - num7) / 2f;
				UiDraw.TextFont(item, UiDraw.TitleFont, num4 + num2 / 2f - Theme.CapsWidth(item, 0.38f) / 2f - 0.0008f, num8, 0.38f, item3.Item1, item3.Item2, item3.Item3, 255, false);
				UiDraw.TextFont(item2, UiDraw.TitleFont, num4 + num2 / 2f - Theme.CapsWidth(item2, num5) / 2f - 0.0008f, num8 + num6 + 0.002f, num5, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
			}
			return y + 0.056f + 0.014f;
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x0011425C File Offset: 0x0011245C
		private static void Stars(float x, float cy, int rank)
		{
			float num = 0.016f;
			float num2 = num / UiDraw.Aspect;
			float num3 = 0.0012f;
			bool flag = UiDraw.EnsureTextureDict("generic_textures");
			for (int i = 0; i < 5; i++)
			{
				float num4 = x + (float)i * (num2 + num3) + num2 / 2f;
				bool flag2 = i < Math.Max(0, Math.Min(5, rank));
				if (flag)
				{
					UiDraw.Sprite("generic_textures", "star", num4, cy, num2, num, 0f, flag2 ? Theme.Gold.Item1 : 78, flag2 ? Theme.Gold.Item2 : 75, flag2 ? Theme.Gold.Item3 : 70, flag2 ? 255 : 235);
				}
				else
				{
					UiDraw.Rect(num4 - num2 * 0.3f, cy - num * 0.3f, num2 * 0.6f, num * 0.6f, flag2 ? Theme.Gold.Item1 : 70, flag2 ? Theme.Gold.Item2 : 68, flag2 ? Theme.Gold.Item3 : 64, 230);
				}
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x0011438C File Offset: 0x0011258C
		private static void TwoSided(float x, float y, float w, int value, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> c)
		{
			float num = 0.0046296297f;
			float num2 = x + w / 2f;
			UiDraw.Rect(x, y, w, num, Theme.CardHi.Item1, Theme.CardHi.Item2, Theme.CardHi.Item3, 235);
			float num3 = (float)Math.Max(-100, Math.Min(100, value)) / 100f * (w / 2f);
			if (num3 >= 0f)
			{
				UiDraw.Rect(num2, y, num3, num, c.Item1, c.Item2, c.Item3, 255);
			}
			else
			{
				UiDraw.Rect(num2 + num3, y, -num3, num, c.Item1, c.Item2, c.Item3, 255);
			}
			UiDraw.Rect(num2 - 0.00052083336f, y - 0.0018518518f, 0.0010416667f, num + 0.0037037036f, 235, 230, 220, 220);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x0011447C File Offset: 0x0011267C
		private static string KeyOf(RosterMenu.ListEntry e)
		{
			if (((e != null) ? e.Rec : null) == null)
			{
				string str = "a:";
				object obj;
				if (e == null)
				{
					obj = null;
				}
				else
				{
					Acquaintance acq = e.Acq;
					obj = ((acq != null) ? acq.Id : null);
				}
				object str2;
				if ((str2 = obj) == null)
				{
					if (e == null)
					{
						str2 = null;
					}
					else
					{
						Acquaintance acq2 = e.Acq;
						str2 = ((acq2 != null) ? acq2.Name : null);
					}
				}
				return str + str2;
			}
			return "r:" + (e.Rec.Id ?? e.Rec.Name);
		}

		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x060011DD RID: 4573 RVA: 0x001144FA File Offset: 0x001126FA
		private Gang Faction
		{
			get
			{
				if (this._faction < Gangs.All.Length)
				{
					return Gangs.All[this._faction];
				}
				if (this._faction != Gangs.All.Length)
				{
					return null;
				}
				return Gangs.VanDerLinde;
			}
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060011DE RID: 4574 RVA: 0x0011452E File Offset: 0x0011272E
		private static int FactionCount
		{
			get
			{
				return Gangs.All.Length + 2;
			}
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0011453C File Offset: 0x0011273C
		private List<NemesisRecord> MembersOf(Gang g)
		{
			if (g == null || !g.StoryOnly)
			{
				return this._mod.Roster.Where(delegate(NemesisRecord n)
				{
					string gang = n.Gang;
					Gang g2 = g;
					return gang == ((g2 != null) ? g2.Id : null);
				}).ToList<NemesisRecord>();
			}
			return this._mod.StoryRoster;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00114598 File Offset: 0x00112798
		private static string FactionName(Gang g)
		{
			return ((g != null) ? g.Name : null) ?? "Sin banda";
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x001145AF File Offset: 0x001127AF
		[return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static ValueTuple<int, int, int> AccentOf(Gang g)
		{
			if (g == null)
			{
				return RosterMenu.UnaffiliatedGrey;
			}
			return g.Accent;
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x001145C0 File Offset: 0x001127C0
		private void DrawFactionBar(string status)
		{
			List<string> names = (from g in Gangs.All
			select g.Name).Concat(new string[]
			{
				"Van der Linde",
				"Sin banda"
			}).ToList<string>();
			Theme.FactionBar(0.13499999f, names, this._faction, this.Faction != null, RosterMenu.AccentOf(this.Faction), status, null, false, new Action<int>(this.PickFaction));
			float num = 0.17499998f;
			Gang faction = this.Faction;
			if (faction != null && faction.StoryOnly)
			{
				Theme.NoteBand(num, "Tu propia banda, según la historia: sus vidas y mentes en cada ficha. Nadie de aquí vendrá a por ti.", null);
				Theme.StrengthRow(num + 0.032f, -1, null, null);
				Theme.GroundRow(num + 0.032f + 0.028f, null, null);
				return;
			}
			if (faction != null)
			{
				Theme.NotorietyBand(num, this._mod.NotorietyOf(faction), null, null, null, this._mod.AgendaText(faction, null));
				ValueTuple<double, double, double, double> valueTuple = this._mod.StrengthParts(faction);
				int num2 = this._mod.Strength(faction);
				Theme.StrengthRow(num + 0.032f, num2, NemesisMod.StrengthWord(num2), new ValueTuple<string, double>[]
				{
					new ValueTuple<string, double>("TERRITORIO", valueTuple.Item1),
					new ValueTuple<string, double>("HOMBRES", valueTuple.Item2),
					new ValueTuple<string, double>("DINERO", valueTuple.Item3),
					new ValueTuple<string, double>("SUMINISTROS", valueTuple.Item4)
				});
				ValueTuple<List<string>, List<string>, List<string>> valueTuple2 = this._mod.GroundOf(faction);
				List<string> item = valueTuple2.Item1;
				List<string> item2 = valueTuple2.Item2;
				List<string> item3 = valueTuple2.Item3;
				Theme.GroundRow(num + 0.032f + 0.028f, new ValueTuple<string, List<string>, ValueTuple<int, int, int>>[]
				{
					new ValueTuple<string, List<string>, ValueTuple<int, int, int>>("CONTROLA", item, Theme.Good),
					new ValueTuple<string, List<string>, ValueTuple<int, int, int>>("FUERTE EN", item2, Theme.Text),
					new ValueTuple<string, List<string>, ValueTuple<int, int, int>>("PRESENCIA EN", item3, Theme.Dim)
				}, this._mod.IsDestroyed(faction) ? "Acabados - sin territorio" : "Sin territorio propio");
				return;
			}
			Theme.NoteBand(num, "Aquí no hay notoriedad: cada hombre guarda su propio rencor.", null);
			Theme.StrengthRow(num + 0.032f, -1, null, null);
			Theme.GroundRow(num + 0.032f + 0.028f, null, null);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x00114838 File Offset: 0x00112A38
		private void PickFaction(int i)
		{
			if (i == this._faction)
			{
				return;
			}
			NemesisMod.UiSound((i > this._faction) ? "BUMPER_RIGHT" : "BUMPER_LEFT");
			this._faction = i;
			this._row = -1;
			this._col = 0;
			this._scrollY = 0f;
			Array.Clear(this._offsets, 0, this._offsets.Length);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0011489D File Offset: 0x00112A9D
		private bool HoverCard(int r, int c)
		{
			if (this._row == r && this._col == c)
			{
				return false;
			}
			this._row = r;
			this._col = c;
			return true;
		}

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x001148C2 File Offset: 0x00112AC2
		// (set) Token: 0x060011E6 RID: 4582 RVA: 0x001148CA File Offset: 0x00112ACA
		public bool IsOpen { get; private set; }

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x001148D3 File Offset: 0x00112AD3
		public bool InWorld
		{
			get
			{
				return this.IsOpen && this._worldPage;
			}
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x001148E5 File Offset: 0x00112AE5
		public void ShowWorld()
		{
			this._worldPage = true;
			this._detail = null;
			this._worldPerson = -1;
			this._confirmForget = false;
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x00114904 File Offset: 0x00112B04
		public bool ShowArthur()
		{
			this._mod.MarkYouInStoryGang();
			NemesisRecord nemesisRecord = this._mod.StoryRoster.FirstOrDefault((NemesisRecord r) => r.Id == NemesisMod.YouId);
			if (nemesisRecord == null)
			{
				return false;
			}
			this._worldPage = false;
			this._resetOpen = false;
			this._worldPerson = -1;
			this._confirmForget = false;
			this._faction = Gangs.All.Length;
			this._detail = nemesisRecord;
			this._action = 0;
			this._historyScroll = 0;
			this._crimeScroll = 0;
			return true;
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00114997 File Offset: 0x00112B97
		public void ShowHierarchy()
		{
			this._worldPage = false;
			this._detail = null;
			this._worldPerson = -1;
			this._confirmForget = false;
			this._resetOpen = false;
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x001149BC File Offset: 0x00112BBC
		public void ShowRecord(NemesisRecord r)
		{
			if (r == null)
			{
				return;
			}
			this._worldPage = false;
			this._resetOpen = false;
			this._worldPerson = -1;
			this._confirmForget = false;
			int num = Array.FindIndex<Gang>(Gangs.All, (Gang g) => g.Id == r.Gang);
			this._faction = ((num >= 0) ? num : ((r.Story || r.Gang == Gangs.VanDerLinde.Id) ? Gangs.All.Length : (Gangs.All.Length + 1)));
			this._detail = r;
			this._action = 0;
			this._historyScroll = 0;
			this._crimeScroll = 0;
		}

		// Token: 0x060011EC RID: 4588 RVA: 0x00114A7B File Offset: 0x00112C7B
		public void ShowEvent(WorldEvent e)
		{
			this.ShowWorld();
			this._worldFilter = 0;
			this._worldMouseTop = -1;
			this._worldIndex = Math.Max(0, this._mod.ChronicleFor(0).IndexOf(e));
		}

		// Token: 0x060011ED RID: 4589 RVA: 0x00114AB0 File Offset: 0x00112CB0
		public void ShowWar(WarRecord w)
		{
			this.ShowWorld();
			this._worldFilter = this.WarsFilter;
			this._worldMouseTop = -1;
			this._warSel = Math.Max(0, this._mod.WarList().FindIndex(([TupleElementNames(new string[]
			{
				"w",
				"live"
			})] ValueTuple<WarRecord, bool> p) => p.Item1 == w));
		}

		// Token: 0x060011EE RID: 4590 RVA: 0x00114B0C File Offset: 0x00112D0C
		public RosterMenu(NemesisMod mod)
		{
			this._mod = mod;
		}

		// Token: 0x060011EF RID: 4591 RVA: 0x00114B97 File Offset: 0x00112D97
		public void Open()
		{
			if (!this.IsOpen)
			{
				this.Toggle();
			}
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x00114BA7 File Offset: 0x00112DA7
		public void Close()
		{
			if (this.IsOpen)
			{
				this.Toggle();
			}
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x00114BB8 File Offset: 0x00112DB8
		public void Toggle()
		{
			this.IsOpen = !this.IsOpen;
			if (this.IsOpen)
			{
				this._mod.MarkYouInStoryGang();
			}
			this._detail = null;
			this._worldPage = false;
			this._resetOpen = false;
			this._worldPerson = -1;
			this._row = -1;
			this._confirmForget = false;
			Array.Clear(this._offsets, 0, this._offsets.Length);
		}

		// Token: 0x060011F2 RID: 4594 RVA: 0x00114C25 File Offset: 0x00112E25
		private List<RosterMenu.Row> BuildRows()
		{
			return this.BuildRows(this.Faction);
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x00114C34 File Offset: 0x00112E34
		private List<RosterMenu.Row> BuildRows(Gang gang)
		{
			List<NemesisRecord> source = this.MembersOf(gang);
			string succ = (gang != null) ? this._mod.SuccessorId(gang) : null;
			List<NemesisRecord> list = (from n in source
			where n.IsAlive && !n.PresumedDead && (succ == null || n.Id != succ)
			orderby n.Rank descending, n.Hate descending, n.Name
			select n).ToList<NemesisRecord>();
			List<NemesisRecord> list2 = (from n in source
			where !n.IsAlive || n.PresumedDead
			select n).Reverse<NemesisRecord>().OrderByDescending(delegate(NemesisRecord n)
			{
				if (n.History == null || n.History.Count <= 0)
				{
					return DateTime.MinValue;
				}
				return n.History.Max((HistoryEntry h) => h.At);
			}).ToList<NemesisRecord>();
			List<RosterMenu.Row> list3 = new List<RosterMenu.Row>();
			if (gang != null)
			{
				if (!gang.StoryOnly)
				{
					ValueTuple<string, string, bool> valueTuple = this._mod.LeaderCard(gang);
					string item = valueTuple.Item1;
					string item2 = valueTuple.Item2;
					bool item3 = valueTuple.Item3;
					NemesisRecord nemesisRecord = item3 ? null : this._mod.LeaderPageOf(gang);
					List<RosterMenu.Row> list4 = list3;
					RosterMenu.Row row = new RosterMenu.Row();
					row.Tier = RosterMenu.Tier.Leader;
					row.Label = "LÍDER";
					row.Slots = 1;
					row.FixedSeats = true;
					List<NemesisRecord> items;
					if (nemesisRecord == null)
					{
						items = new List<NemesisRecord>();
					}
					else
					{
						(items = new List<NemesisRecord>()).Add(nemesisRecord);
					}
					row.Items = items;
					row.LoreName = item;
					row.LoreBlurb = item2;
					row.LoreVacant = item3;
					list4.Add(row);
				}
				List<NemesisRecord> list5 = new List<NemesisRecord>(list);
				for (int i = 0; i < gang.Tiers.Length; i++)
				{
					ValueTuple<string, int, int> valueTuple2 = gang.Tiers[i];
					string item4 = valueTuple2.Item1;
					int minRank = valueTuple2.Item2;
					int item5 = valueTuple2.Item3;
					bool flag = i == gang.Tiers.Length - 1;
					List<NemesisRecord> list6 = flag ? list5.ToList<NemesisRecord>() : (from n in list5
					where n.Rank >= minRank
					select n).Take(item5).ToList<NemesisRecord>();
					list5.RemoveAll(new Predicate<NemesisRecord>(list6.Contains));
					string standing = (item4 == "MUCHACHOS") ? "Uno de los muchachos" : (item4.Substring(0, 1) + item4.Substring(1, item4.Length - 2).ToLowerInvariant());
					list3.Add(new RosterMenu.Row
					{
						Tier = RosterMenu.Tier.Members,
						Label = item4,
						Standing = standing,
						Slots = item5,
						FixedSeats = !flag,
						Items = list6
					});
				}
			}
			else
			{
				List<NemesisRecord> boss = (from n in list
				where n.Rank >= 5
				select n).Take(1).ToList<NemesisRecord>();
				List<NemesisRecord> lieutenants = (from n in list
				where !boss.Contains(n) && n.Rank >= 3
				select n).Take(4).ToList<NemesisRecord>();
				List<NemesisRecord> items2 = (from n in list
				where !boss.Contains(n) && !lieutenants.Contains(n)
				select n).ToList<NemesisRecord>();
				list3.Add(new RosterMenu.Row
				{
					Tier = RosterMenu.Tier.Boss,
					Label = "JEFE",
					Standing = "Jefe",
					Slots = 1,
					FixedSeats = true,
					Items = boss
				});
				list3.Add(new RosterMenu.Row
				{
					Tier = RosterMenu.Tier.Members,
					Label = "TENIENTES",
					Standing = "Lieutenant",
					Slots = 4,
					FixedSeats = true,
					Items = lieutenants
				});
				list3.Add(new RosterMenu.Row
				{
					Tier = RosterMenu.Tier.Members,
					Label = "FORAJIDOS",
					Standing = "Forajido",
					Slots = 5,
					Items = items2
				});
			}
			list3.Add(new RosterMenu.Row
			{
				Tier = RosterMenu.Tier.Fallen,
				Label = string.Format("CAÍDOS ({0})", list2.Count),
				Standing = "Caídos",
				Slots = 5,
				Items = list2
			});
			this.Layout(list3);
			return list3;
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x001150A4 File Offset: 0x001132A4
		internal string StandingOf(NemesisRecord rec)
		{
			Gang gang = Gangs.ById(rec.Gang);
			if (gang == null)
			{
				if (rec.IsAlive && !rec.PresumedDead)
				{
					return "Sin banda";
				}
				return "Caídos";
			}
			else
			{
				string text;
				if ((text = rec.Id) == null)
				{
					text = (rec.Name ?? "");
				}
				string key = text;
				ValueTuple<string, int> valueTuple;
				if (this._standingCache.TryGetValue(key, out valueTuple) && UiDraw.RealMs - valueTuple.Item2 < 250)
				{
					return valueTuple.Item1;
				}
				float z = this._z;
				float scrollY = this._scrollY;
				string text2 = "Desconocido";
				foreach (RosterMenu.Row row in this.BuildRows(gang))
				{
					if (row.Items.Contains(rec))
					{
						text2 = ((row.Tier != RosterMenu.Tier.Fallen) ? (row.Standing + " of the " + gang.Name) : row.Standing);
						break;
					}
				}
				this._z = z;
				this._scrollY = scrollY;
				if (this._standingCache.Count > 200)
				{
					this._standingCache.Clear();
				}
				this._standingCache[key] = new ValueTuple<string, int>(text2, UiDraw.RealMs);
				return text2;
			}
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060011F5 RID: 4597 RVA: 0x001151FC File Offset: 0x001133FC
		private float CW
		{
			get
			{
				return 0.13f * this._z;
			}
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060011F6 RID: 4598 RVA: 0x0011520A File Offset: 0x0011340A
		private float CH
		{
			get
			{
				return 0.075f * this._z;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00115218 File Offset: 0x00113418
		private float Gap
		{
			get
			{
				return 0.012f * this._z;
			}
		}

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x00115226 File Offset: 0x00113426
		private float LineGap
		{
			get
			{
				return 0.016f * this._z;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00115234 File Offset: 0x00113434
		private float RowGap
		{
			get
			{
				return Math.Max(0.05f, 0.062f * this._z);
			}
		}

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0011524C File Offset: 0x0011344C
		private int PerLine
		{
			get
			{
				return Math.Max(1, (int)((0.8f + this.Gap) / (this.CW + this.Gap)));
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x0011526F File Offset: 0x0011346F
		private float T(float scale)
		{
			return Math.Max(0.17f, scale * this._z);
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x00115284 File Offset: 0x00113484
		private int Shown(RosterMenu.Row row)
		{
			if (row.Tier == RosterMenu.Tier.Fallen)
			{
				return Math.Min(this.PerLine, row.Items.Count);
			}
			if (!row.FixedSeats)
			{
				return row.Items.Count;
			}
			return Math.Max(row.Slots, row.Items.Count);
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x001152DB File Offset: 0x001134DB
		private int LinesOf(RosterMenu.Row row)
		{
			if (row.Tier != RosterMenu.Tier.Fallen)
			{
				return Math.Max(1, (this.Shown(row) + this.PerLine - 1) / this.PerLine);
			}
			return 1;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00115305 File Offset: 0x00113505
		private float RowHeight(RosterMenu.Row row)
		{
			return (float)this.LinesOf(row) * this.CH + (float)(this.LinesOf(row) - 1) * this.LineGap;
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00115328 File Offset: 0x00113528
		private float Stack(List<RosterMenu.Row> rows)
		{
			float num = 0.31299996f;
			float num2 = num;
			foreach (RosterMenu.Row row in rows)
			{
				if (row.Tier == RosterMenu.Tier.Fallen)
				{
					num += 0.015f;
				}
				row.Y = num;
				num2 = num + this.RowHeight(row);
				num = num2 + this.RowGap;
			}
			return num2;
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x001153A4 File Offset: 0x001135A4
		private void Layout(List<RosterMenu.Row> rows)
		{
			if (this._zoomIndex < 0)
			{
				this._z = 1f;
				while (this._z > 0.5f)
				{
					if (this.Stack(rows) <= 0.848f)
					{
						break;
					}
					this._z -= 0.05f;
				}
			}
			else
			{
				this._z = RosterMenu.ZoomLevels[this._zoomIndex];
			}
			this.Stack(rows);
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x00115410 File Offset: 0x00113610
		private void Zoom(int dir)
		{
			if (this._zoomIndex < 0)
			{
				this._zoomIndex = 0;
				for (int i = 0; i < RosterMenu.ZoomLevels.Length; i++)
				{
					if (RosterMenu.ZoomLevels[i] <= this._z + 0.001f)
					{
						this._zoomIndex = i;
					}
				}
			}
			this._zoomIndex = Math.Max(0, Math.Min(RosterMenu.ZoomLevels.Length - 1, this._zoomIndex + dir));
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x00115480 File Offset: 0x00113680
		private float RowStartX(RosterMenu.Row row)
		{
			int num = Math.Max(1, Math.Min(this.PerLine, this.Shown(row)));
			return 0.5f - ((float)num * this.CW + (float)(num - 1) * this.Gap) / 2f;
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x001154C7 File Offset: 0x001136C7
		private float SeatX(RosterMenu.Row row, int seat)
		{
			return this.RowStartX(row) + (float)((row.Tier == RosterMenu.Tier.Fallen) ? seat : (seat % this.PerLine)) * (this.CW + this.Gap);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x001154F4 File Offset: 0x001136F4
		private float SeatY(RosterMenu.Row row, int seat)
		{
			return row.Y + (float)((row.Tier == RosterMenu.Tier.Fallen) ? 0 : (seat / this.PerLine)) * (this.CH + this.LineGap);
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00115520 File Offset: 0x00113720
		private int SeatOf(RosterMenu.Row row, int r, int item)
		{
			if (row.Tier != RosterMenu.Tier.Fallen)
			{
				return item;
			}
			return item - this._offsets[r];
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00115537 File Offset: 0x00113737
		private int LineOf(RosterMenu.Row row, int seat)
		{
			if (row.Tier != RosterMenu.Tier.Fallen)
			{
				return seat / this.PerLine;
			}
			return 0;
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0011554C File Offset: 0x0011374C
		private float Sy(float y)
		{
			return y - this._scrollY;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00115556 File Offset: 0x00113756
		private static bool Visible(float y, float h)
		{
			return y >= 0.27199998f && y + h <= 0.849f;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00115570 File Offset: 0x00113770
		public bool HandleKey(Keys key)
		{
			if (this._resetOpen)
			{
				return this.HandleResetKey(key);
			}
			if ((key == Keys.Home || key == Keys.A) && this.ShowArthur())
			{
				return true;
			}
			if (this._detail != null)
			{
				return this.HandleDetailKey(key);
			}
			if (this._worldPage)
			{
				return this.HandleWorldKey(key);
			}
			if (this.Faction == null)
			{
				return this.HandleListKey(key);
			}
			List<RosterMenu.Row> list = this.BuildRows();
			this.EnsureSelection(list);
			if (key > Keys.R)
			{
				if (key > Keys.Add)
				{
					if (key != Keys.Subtract)
					{
						if (key == Keys.Oemplus)
						{
							goto IL_341;
						}
						if (key != Keys.OemMinus)
						{
							return false;
						}
					}
					this.Zoom(-1);
					this.Layout(list);
					goto IL_3DA;
				}
				if (key == Keys.Z)
				{
					this._zoomIndex = -1;
					this._scrollY = 0f;
					this.Layout(list);
					goto IL_3DA;
				}
				if (key != Keys.Add)
				{
					return false;
				}
				IL_341:
				this.Zoom(1);
				this.Layout(list);
				goto IL_3DA;
			}
			if (key <= Keys.Return)
			{
				if (key == Keys.Back)
				{
					this.Toggle();
					goto IL_3DA;
				}
				if (key == Keys.Return)
				{
					if (this._row >= 0)
					{
						this._detail = list[this._row].Items[this._col];
						this._action = 0;
						this._confirmForget = false;
						this._historyScroll = 0;
						this._crimeScroll = 0;
						goto IL_3DA;
					}
					goto IL_3DA;
				}
			}
			else
			{
				switch (key)
				{
				case Keys.Left:
					if (this._row >= 0)
					{
						this._col = Math.Max(0, this._col - 1);
						goto IL_3DA;
					}
					goto IL_3DA;
				case Keys.Up:
					this.MoveRow(list, -1);
					goto IL_3DA;
				case Keys.Right:
					if (this._row >= 0)
					{
						this._col = Math.Min(list[this._row].Items.Count - 1, this._col + 1);
						goto IL_3DA;
					}
					goto IL_3DA;
				case Keys.Down:
					this.MoveRow(list, 1);
					goto IL_3DA;
				default:
					switch (key)
					{
					case Keys.C:
						this._mod.CycleNameCards();
						goto IL_3DA;
					case Keys.E:
					case Keys.Q:
						this._faction = (this._faction + ((key == Keys.E) ? 1 : (RosterMenu.FactionCount - 1))) % RosterMenu.FactionCount;
						this._row = -1;
						this._col = 0;
						this._scrollY = 0f;
						Array.Clear(this._offsets, 0, this._offsets.Length);
						return true;
					case Keys.G:
						this._mod.OpenGuide();
						goto IL_3DA;
					case Keys.H:
						if (this._mod.DebugOn && this.Faction != null && !this.Faction.StoryOnly)
						{
							this._mod.AddCampHere(this.Faction);
							goto IL_3DA;
						}
						goto IL_3DA;
					case Keys.N:
						this._mod.CycleMessages();
						goto IL_3DA;
					case Keys.O:
						this._mod.OpenSetupScreen();
						goto IL_3DA;
					case Keys.P:
						if (this._row >= 0 && this._mod.DebugOn && !list[this._row].Items[this._col].Story && !list[this._row].Items[this._col].LeaderPage)
						{
							this.PromoteAndFollow(list[this._row].Items[this._col]);
							goto IL_3DA;
						}
						goto IL_3DA;
					case Keys.R:
						if (this._mod.DebugOn)
						{
							this._mod.SetRandomAmbushes(!this._mod.Config.RandomAmbushes);
							goto IL_3DA;
						}
						this._mod.OpenGuide();
						goto IL_3DA;
					}
					break;
				}
			}
			return false;
			IL_3DA:
			if (this._row >= 0)
			{
				this.KeepVisible(list[this._row], this._row);
			}
			return true;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x0011597C File Offset: 0x00113B7C
		private List<RosterMenu.ListEntry> Entries()
		{
			ValueTuple<bool, int> entriesKey = new ValueTuple<bool, int>(this._listAll, this._mod.Roster.Count);
			if (this._entriesCache != null && entriesKey.Equals(this._entriesKey) && UiDraw.RealMs - this._entriesAt < 250)
			{
				return this._entriesCache;
			}
			this._entriesKey = entriesKey;
			this._entriesAt = UiDraw.RealMs;
			return this._entriesCache = this.EntriesNow();
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x001159F8 File Offset: 0x00113BF8
		private List<RosterMenu.ListEntry> EntriesNow()
		{
			List<NemesisRecord> source = this.Unaffiliated();
			if (!this._listAll)
			{
				return (from r in source
				select new RosterMenu.ListEntry
				{
					Rec = r
				}).ToList<RosterMenu.ListEntry>();
			}
			List<ValueTuple<RosterMenu.ListEntry, DateTime>> list = (from r in source
			select new ValueTuple<RosterMenu.ListEntry, DateTime>(new RosterMenu.ListEntry
			{
				Rec = r
			}, r.LastDealt)).ToList<ValueTuple<RosterMenu.ListEntry, DateTime>>();
			list.AddRange(from a in this._mod.PeopleForList()
			select new ValueTuple<RosterMenu.ListEntry, DateTime>(new RosterMenu.ListEntry
			{
				Acq = a
			}, a.LastDealt));
			return (from t in list.OrderBy(delegate([TupleElementNames(new string[]
			{
				"e",
				"at"
			})] ValueTuple<RosterMenu.ListEntry, DateTime> t)
			{
				if (t.Item1.Rec == null)
				{
					return t.Item1.Acq.Dead;
				}
				return !t.Item1.Rec.IsAlive || t.Item1.Rec.PresumedDead;
			}).ThenByDescending(([TupleElementNames(new string[]
			{
				"e",
				"at"
			})] ValueTuple<RosterMenu.ListEntry, DateTime> t) => t.Item2).ThenBy(delegate([TupleElementNames(new string[]
			{
				"e",
				"at"
			})] ValueTuple<RosterMenu.ListEntry, DateTime> t)
			{
				NemesisRecord rec = t.Item1.Rec;
				return ((rec != null) ? rec.Name : null) ?? t.Item1.Acq.Name;
			}, StringComparer.Ordinal)
			select t.Item1).ToList<RosterMenu.ListEntry>();
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00115B38 File Offset: 0x00113D38
		private int EntryIndexOf(NemesisRecord rec)
		{
			return Math.Max(0, this.Entries().FindIndex((RosterMenu.ListEntry e) => e.Rec == rec));
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00115B70 File Offset: 0x00113D70
		private List<NemesisRecord> Unaffiliated()
		{
			return (from n in this._mod.Roster
			where n.Gang == null
			orderby n.LastDealt descending, n.IsAlive && !n.PresumedDead descending, n.Name
			select n).ToList<NemesisRecord>();
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00115C20 File Offset: 0x00113E20
		private bool HandleListKey(Keys key)
		{
			List<RosterMenu.ListEntry> list = this.Entries();
			int num = Math.Max(0, list.Count - 1);
			if (this._listKey != null)
			{
				int num2 = list.FindIndex((RosterMenu.ListEntry e) => RosterMenu.KeyOf(e) == this._listKey);
				if (num2 >= 0)
				{
					this._listIndex = num2;
				}
			}
			this._listIndex = Math.Max(0, Math.Min(this._listIndex, num));
			if (key > Keys.Up)
			{
				if (key <= Keys.Add)
				{
					if (key == Keys.Down)
					{
						this._listIndex = Math.Min(num, this._listIndex + 1);
						this._listKey = ((list.Count > 0) ? RosterMenu.KeyOf(list[this._listIndex]) : null);
						return true;
					}
					switch (key)
					{
					case Keys.C:
						this._mod.CycleNameCards();
						return true;
					case Keys.D:
					case Keys.H:
					case Keys.I:
					case Keys.J:
					case Keys.K:
					case Keys.L:
					case Keys.M:
						return false;
					case Keys.E:
					case Keys.Q:
						this._faction = (this._faction + ((key == Keys.E) ? 1 : (RosterMenu.FactionCount - 1))) % RosterMenu.FactionCount;
						this._row = -1;
						this._col = 0;
						Array.Clear(this._offsets, 0, this._offsets.Length);
						return true;
					case Keys.F:
						break;
					case Keys.G:
						this._mod.OpenGuide();
						return true;
					case Keys.N:
						this._mod.CycleMessages();
						return true;
					case Keys.O:
						this._mod.OpenSetupScreen();
						return true;
					case Keys.P:
						if (list.Count > 0 && this._mod.DebugOn && list[this._listIndex].Rec != null)
						{
							NemesisRecord rec = list[this._listIndex].Rec;
							this._mod.Promote(rec);
							this._listIndex = this.EntryIndexOf(rec);
							return true;
						}
						return true;
					case Keys.R:
						if (this._mod.DebugOn)
						{
							this._mod.SetRandomAmbushes(!this._mod.Config.RandomAmbushes);
							return true;
						}
						this._mod.OpenGuide();
						return true;
					default:
						if (key != Keys.Add)
						{
							return false;
						}
						break;
					}
				}
				else if (key != Keys.Subtract && key != Keys.Oemplus && key != Keys.OemMinus)
				{
					return false;
				}
				this._listAll = !this._listAll;
				this._listIndex = 0;
				this._listScroll = 0;
				this._listKey = null;
				return true;
			}
			if (key <= Keys.Return)
			{
				if (key == Keys.Back)
				{
					this.Toggle();
					return true;
				}
				if (key == Keys.Return)
				{
					if (list.Count > 0 && list[this._listIndex].Rec != null)
					{
						this._detail = list[this._listIndex].Rec;
						this._action = 0;
						this._confirmForget = false;
						this._historyScroll = 0;
						this._crimeScroll = 0;
						return true;
					}
					if (list.Count <= 0 || list[this._listIndex].Acq == null)
					{
						return true;
					}
					NemesisRecord nemesisRecord = this._mod.PersonRecord(list[this._listIndex].Acq);
					if (nemesisRecord != null)
					{
						this._detail = nemesisRecord;
						this._action = 0;
						this._confirmForget = false;
						this._historyScroll = 0;
						this._crimeScroll = 0;
						return true;
					}
					return true;
				}
			}
			else
			{
				if (key == Keys.Prior)
				{
					this._listIndex = Math.Max(0, this._listIndex - 16);
					this._listKey = ((list.Count > 0) ? RosterMenu.KeyOf(list[this._listIndex]) : null);
					return true;
				}
				if (key == Keys.Next)
				{
					this._listIndex = Math.Min(num, this._listIndex + 16);
					this._listKey = ((list.Count > 0) ? RosterMenu.KeyOf(list[this._listIndex]) : null);
					return true;
				}
				if (key == Keys.Up)
				{
					this._listIndex = Math.Max(0, this._listIndex - 1);
					this._listKey = ((list.Count > 0) ? RosterMenu.KeyOf(list[this._listIndex]) : null);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x0011603C File Offset: 0x0011423C
		private void DrawUnaffiliatedList()
		{
			List<RosterMenu.ListEntry> list = this.Entries();
			if (this._listKey != null)
			{
				int num = list.FindIndex((RosterMenu.ListEntry e) => RosterMenu.KeyOf(e) == this._listKey);
				if (num >= 0)
				{
					this._listIndex = num;
				}
			}
			this._listIndex = Math.Max(0, Math.Min(this._listIndex, list.Count - 1));
			if (list.Count > 0)
			{
				this._listKey = RosterMenu.KeyOf(list[this._listIndex]);
			}
			if (this._listIndex < this._listScroll)
			{
				this._listScroll = this._listIndex;
			}
			if (this._listIndex >= this._listScroll + 16)
			{
				this._listScroll = this._listIndex - 16 + 1;
			}
			this.DrawHeader("SIN BANDA", "Hombres solitarios con rencor: sin banda ni cadena de mando", RosterMenu.UnaffiliatedGrey);
			this.DrawFactionBar(null);
			float num2 = 0.868f;
			Theme.PanelRect(0.07f, 0.26099998f, 0.86f, num2 - 0.26099998f, 215);
			RosterMenu.DrawFooter(num2, this.MainFooter(true));
			float num3 = 0.089999996f;
			float num4 = 0.37f;
			float num5 = 0.5f;
			float num6 = 0.64f;
			float num7 = 0.78f;
			ValueTuple<int, int, int> labelColor = RosterMenu.LabelColor;
			int item = labelColor.Item1;
			int item2 = labelColor.Item2;
			int item3 = labelColor.Item3;
			long num8 = this._mod.GameMinutesNow();
			foreach (ValueTuple<string, float> valueTuple in new ValueTuple<string, float>[]
			{
				new ValueTuple<string, float>("NOMBRE", num3),
				new ValueTuple<string, float>("RANGO  ODIO", num4),
				new ValueTuple<string, float>("ESTADO", num5),
				new ValueTuple<string, float>("DÓNDE OCURRIÓ", num6),
				new ValueTuple<string, float>("VISTO POR ÚLTIMA VEZ", num7)
			})
			{
				string item4 = valueTuple.Item1;
				float item5 = valueTuple.Item2;
				UiDraw.TextFont(item4, UiDraw.TitleFont, item5, 0.291f, 0.22f, item, item2, item3, 255, false);
			}
			Theme.Divider(0.08f, 0.315f, 0.84000003f);
			if (list.Count == 0)
			{
				UiDraw.Text(this._listAll ? "Aún no conoces a nadie por su nombre. Saluda a la gente para conocerla." : "Nadie te guarda rencor aún. Roba, golpea, dispara o insulta a un desconocido —o deja que te mate— y puede que vuelva a por ti.", num3, 0.331f, 0.26f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			}
			float num9 = 0.321f;
			float top = num9;
			int num10 = 5 * (UiDraw.GameFont ? 2 : 1);
			int num11 = Math.Max(3, Math.Min(16, (UiDraw.TextSlotsLeft - 34) / num10));
			if (this._listIndex >= this._listScroll + num11)
			{
				this._listScroll = this._listIndex - num11 + 1;
			}
			float num12 = 0.81799996f - num9;
			this._rowH = Math.Max(0.032f, Math.Min(0.044f, num12 / (float)Math.Max(1, num11)));
			this._rowGrow = (this._rowH - 0.032f) / 0.011999998f;
			int j = this._listScroll;
			while (j < Math.Min(list.Count, this._listScroll + num11))
			{
				NemesisRecord rec = list[j].Rec;
				bool flag = j == this._listIndex;
				int li = j;
				string lk = RosterMenu.KeyOf(list[j]);
				UiMouse.Region(0.08f, num9, 0.84000003f, this._rowH - 0.003f, delegate
				{
					bool result = this._listIndex != li;
					this._listIndex = li;
					this._listKey = lk;
					return result;
				}, delegate
				{
					UiMouse.Send(Keys.Return);
				});
				if (rec == null)
				{
					this.DrawPersonRow(list[j].Acq, j, flag, num9, num3, num4, num5, num6, num7, num8);
				}
				else
				{
					bool flag2 = !rec.IsAlive || rec.PresumedDead;
					if (flag)
					{
						Theme.SelectBox(0.08f, num9, 0.84000003f, this._rowH - 0.003f, 235);
					}
					else if (j % 2 == 1)
					{
						UiDraw.Rect(0.08f, num9, 0.84000003f, this._rowH - 0.003f, 255, 255, 255, 8);
					}
					int num13 = flag2 ? 125 : (flag ? 250 : 228);
					float num14 = 0.27f + 0.04f * this._rowGrow;
					float num15 = 0.25f + 0.035f * this._rowGrow;
					float y = num9 + (this._rowH - 0.003f) / 2f - num14 * 0.0375f;
					float y2 = num9 + (this._rowH - 0.003f) / 2f - num15 * 0.0375f;
					UiDraw.Text(RosterMenu.FitText(rec.FullName ?? "", num14, num4 - num3 - 0.008f, false), num3, y, num14, num13, num13, num13, 255);
					int r;
					int g;
					int b;
					if (!flag2)
					{
						ValueTuple<int, int, int> valueTuple2 = UiDraw.HateColor(rec.HateTier);
						r = valueTuple2.Item1;
						g = valueTuple2.Item2;
						b = valueTuple2.Item3;
					}
					else
					{
						int num16 = num13;
						int i = num13;
						int num17 = num13;
						b = num17;
						g = i;
						r = num16;
					}
					UiDraw.Text(string.Format("{0}/{1}   {2}", rec.Rank, 5, flag2 ? "-" : rec.HateTier.ToString()), num4, y2, num15, r, g, b, 255);
					bool flag3 = !flag2 && this._mod.Resting(rec);
					int r2;
					int g2;
					int b2;
					if (!(rec.Status == "Muerto"))
					{
						if (!(rec.Status == "Dado por muerto"))
						{
							if (!(rec.Status == "Paralítico"))
							{
								if (!flag3)
								{
									r2 = 120;
									g2 = 190;
									b2 = 110;
								}
								else
								{
									r2 = 150;
									g2 = 170;
									b2 = 210;
								}
							}
							else
							{
								r2 = 150;
								g2 = 150;
								b2 = 170;
							}
						}
						else
						{
							r2 = 200;
							g2 = 150;
							b2 = 60;
						}
					}
					else
					{
						r2 = 190;
						g2 = 55;
						b2 = 45;
					}
					UiDraw.Text(RosterMenu.FitText(flag3 ? string.Format("Escondido - {0}h", Math.Max(1L, (rec.RestUntil - num8 + 59L) / 60L)) : (rec.Status + ((rec.Limp && !flag2) ? ", cojea" : "")), num15, num6 - num5 - 0.008f, false), num5, y2, num15, r2, g2, b2, 255);
					UiDraw.Text(RosterMenu.FitText(string.IsNullOrEmpty(rec.KillLocation) ? "-" : rec.KillLocation, num15, num7 - num6 - 0.008f, false), num6, y2, num15, num13 - 20, num13 - 24, num13 - 32, 255);
					UiDraw.Text(RosterMenu.SeenText(rec.LastGameMin, rec.LastDealt, num8), num7, y2, num15, num13 - 30, num13 - 34, num13 - 40, 255);
				}
				j++;
				num9 += this._rowH;
			}
			if (list.Count > 0)
			{
				NemesisRecord rec2 = list[this._listIndex].Rec;
				float num18 = 0.82199997f;
				float num19 = (num18 + 0.868f) / 2f;
				Theme.HRule(0.08f, num18, 0.84000003f, Theme.Faint.Item1, Theme.Faint.Item2, Theme.Faint.Item3, 200, 1);
				string text = (rec2 != null) ? "RENCOR" : "KNOWN";
				UiDraw.TextFont(text, UiDraw.TitleFont, num3, num19 - 0.00975f, 0.26f, item, item2, item3, 255, false);
				float num20 = num3 + Theme.CapsWidth(text, 0.26f) + 0.016f;
				UiDraw.Text(RosterMenu.FitText((rec2 != null) ? Pron.Her(rec2.GrudgeSummary, Models.Female(rec2.ModelHash)) : RosterMenu.PersonLine(list[this._listIndex].Acq), 0.28f, 0.92f - num20, false), num20, num19 - 0.010500001f, 0.28f, 200, 196, 186, 255);
			}
			Theme.ScrollBar(0.919f, top, (float)num11 * this._rowH, this._listScroll, num11, list.Count);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x001168F8 File Offset: 0x00114AF8
		private static string SeenText(long gameMin, DateTime real, long nowMin)
		{
			if (gameMin > 0L)
			{
				DateTime dateTime = GameDate.FromMinutes(gameMin);
				long num = Math.Max(0L, (nowMin - gameMin) / 1440L);
				return string.Format("{0} {1}  -  {2}", GameDate.Month(dateTime.Month), dateTime.Day, (num == 0L) ? "hoy" : ((num == 1L) ? "hace 1 día" : string.Format("hace {0} días", num)));
			}
			if (real.Year <= 1900)
			{
				return "-";
			}
			return real.ToString("MMM d");
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0011698C File Offset: 0x00114B8C
		private void DrawPersonRow(Acquaintance a, int i, bool sel, float y, float cName, float cRank, float cStatus, float cWhere, float cSeen, long nowMin)
		{
			float num = this._rowH - 0.003f;
			if (sel)
			{
				Theme.SelectBox(0.08f, y, 0.84000003f, num, 235);
			}
			else if (i % 2 == 1)
			{
				UiDraw.Rect(0.08f, y, 0.84000003f, num, 255, 255, 255, 8);
			}
			float num2 = 0.27f + 0.04f * this._rowGrow;
			float num3 = 0.25f + 0.035f * this._rowGrow;
			float y2 = y + num / 2f - num2 * 0.0375f;
			float y3 = y + num / 2f - num3 * 0.0375f;
			int num4 = a.Dead ? 115 : (sel ? 235 : 190);
			UiDraw.Text(RosterMenu.FitText(a.FullName ?? "", num2, cRank - cName - 0.008f, false), cName, y2, num2, num4, num4, num4, 255);
			int r;
			int g;
			int b;
			if (!a.Dead && a.Grudge > 0)
			{
				if (a.Grudge < 40)
				{
					r = 205;
					g = 180;
					b = 110;
				}
				else
				{
					r = 215;
					g = 120;
					b = 60;
				}
			}
			else
			{
				int num5 = num4 - 40;
				int num6 = num4 - 40;
				int num7 = num4 - 40;
				b = num7;
				g = num6;
				r = num5;
			}
			UiDraw.Text((a.Grudge > 0) ? string.Format("rencor {0}", a.Grudge) : "sin rencor", cRank, y3, num3, r, g, b, 255);
			string text = a.Dead ? "Muerto" : ((a.Id == null) ? (a.Met ? "Conocido - cerca de ti" : "Cerca de ti") : (a.Met ? "Conocido" : "Visto"));
			int r2;
			int g2;
			int b2;
			if (!a.Dead)
			{
				if (!a.Met)
				{
					r2 = 150;
					g2 = 150;
					b2 = 150;
				}
				else
				{
					r2 = 200;
					g2 = 196;
					b2 = 186;
				}
			}
			else
			{
				r2 = 190;
				g2 = 55;
				b2 = 45;
			}
			UiDraw.Text(text, cStatus, y3, num3, r2, g2, b2, 255);
			UiDraw.Text(RosterMenu.FitText(string.IsNullOrEmpty(a.Town) ? "-" : a.Town, num3, cSeen - cWhere - 0.008f, false), cWhere, y3, num3, num4 - 20, num4 - 24, num4 - 32, 255);
			UiDraw.Text(RosterMenu.SeenText(a.LastGameMin, a.LastSeen, nowMin), cSeen, y3, num3, num4 - 30, num4 - 34, num4 - 40, 255);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00116C40 File Offset: 0x00114E40
		private static string PersonLine(Acquaintance a)
		{
			List<string> list = new List<string>();
			list.Add(a.Met ? "os habéis conocido" : "solo lo has visto");
			if (!string.IsNullOrEmpty(a.Town))
			{
				list.Add("in " + a.Town);
			}
			if (!string.IsNullOrEmpty(a.Note))
			{
				list.Add(a.Note);
			}
			if (a.Fear > 0)
			{
				list.Add(string.Format("te tiene miedo ({0}){1}", a.Fear, (a.Witnessed != null && a.Witnessed.Count > 0) ? (": " + a.Witnessed[a.Witnessed.Count - 1]) : ""));
			}
			if (a.Like > 0)
			{
				list.Add(string.Format("le caes bien ({0})", a.Like));
			}
			if (a.Favours > 0)
			{
				list.Add(string.Format("{0} favor{1}: {2}", a.Favours, (a.Favours == 1) ? "" : "s", string.Join(", ", a.FavourNotes ?? new List<string>())));
			}
			if (!string.IsNullOrEmpty(a.Temper))
			{
				list.Add(a.Temper.ToLowerInvariant());
			}
			if (a.TimesSeen > 1)
			{
				list.Add(string.Format("visto {0} veces", a.TimesSeen));
			}
			if (a.Grudge > 0)
			{
				list.Add(string.Format("rencor {0}", a.Grudge));
			}
			if (a.Dead)
			{
				list.Add("dead");
			}
			return string.Join(", ", list);
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00116E08 File Offset: 0x00115008
		private void PromoteAndFollow(NemesisRecord rec)
		{
			this._mod.Promote(rec);
			List<RosterMenu.Row> list = this.BuildRows();
			for (int i = 0; i < list.Count; i++)
			{
				int num = list[i].Items.IndexOf(rec);
				if (num >= 0)
				{
					this._row = i;
					this._col = num;
					this.KeepVisible(list[i], i);
					return;
				}
			}
		}

		// Token: 0x06001214 RID: 4628 RVA: 0x00116E70 File Offset: 0x00115070
		private void EnsureSelection(List<RosterMenu.Row> rows)
		{
			if (this._row >= 0 && this._row < rows.Count && rows[this._row].Items.Count > 0)
			{
				this._col = Math.Min(this._col, rows[this._row].Items.Count - 1);
				return;
			}
			this._row = rows.FindIndex((RosterMenu.Row r) => r.Items.Count > 0);
			this._col = 0;
		}

		// Token: 0x06001215 RID: 4629 RVA: 0x00116F0C File Offset: 0x0011510C
		private void MoveRow(List<RosterMenu.Row> rows, int dir)
		{
			if (this._row < 0)
			{
				return;
			}
			RosterMenu.Row row = rows[this._row];
			int seat = this.SeatOf(row, this._row, this._col);
			float num = this.SeatX(row, seat) + this.CW / 2f;
			int num2 = this._row;
			int num3 = this.LineOf(row, seat) + dir;
			int i = 0;
			while (i < 64)
			{
				if (num3 < 0 || num3 >= this.LinesOf(rows[num2]))
				{
					num2 += dir;
					if (num2 < 0 || num2 >= rows.Count)
					{
						return;
					}
					num3 = ((dir > 0) ? 0 : (this.LinesOf(rows[num2]) - 1));
				}
				RosterMenu.Row row2 = rows[num2];
				int num4 = -1;
				float num5 = float.MaxValue;
				int j = 0;
				while (j < row2.Items.Count)
				{
					int num6 = this.SeatOf(row2, num2, j);
					int num7 = num6;
					if (row2.Tier == RosterMenu.Tier.Fallen)
					{
						num7 = Math.Max(0, Math.Min(num6, this.PerLine - 1));
						goto IL_F9;
					}
					if (this.LineOf(row2, num6) == num3)
					{
						goto IL_F9;
					}
					IL_13A:
					j++;
					continue;
					IL_F9:
					float num8 = Math.Abs(this.SeatX(row2, num7) + this.CW / 2f - num) + ((num7 != num6) ? 1f : 0f);
					if (num8 < num5)
					{
						num5 = num8;
						num4 = j;
						goto IL_13A;
					}
					goto IL_13A;
				}
				if (num4 >= 0)
				{
					this._row = num2;
					this._col = num4;
					return;
				}
				i++;
				num3 += dir;
			}
		}

		// Token: 0x06001216 RID: 4630 RVA: 0x00117098 File Offset: 0x00115298
		private void KeepVisible(RosterMenu.Row row, int rowIndex)
		{
			if (row.Tier != RosterMenu.Tier.Fallen)
			{
				this._offsets[rowIndex] = 0;
				return;
			}
			if (this._col < this._offsets[rowIndex])
			{
				this._offsets[rowIndex] = this._col;
			}
			if (this._col >= this._offsets[rowIndex] + this.PerLine)
			{
				this._offsets[rowIndex] = this._col - this.PerLine + 1;
			}
		}

		// Token: 0x06001217 RID: 4631 RVA: 0x00117104 File Offset: 0x00115304
		private void ScrollToSelection(List<RosterMenu.Row> rows)
		{
			float num = (rows.Count == 0) ? 0f : (rows[rows.Count - 1].Y + this.RowHeight(rows[rows.Count - 1]));
			if (this._row >= 0)
			{
				RosterMenu.Row row = rows[this._row];
				int seat = this.SeatOf(row, this._row, this._col);
				float num2 = this.SeatY(row, seat) - ((this.LineOf(row, seat) == 0) ? 0.035f : 0.005f);
				float num3 = this.SeatY(row, seat) + this.CH;
				if (num2 - this._scrollY < 0.27299997f)
				{
					this._scrollY = num2 - 0.27299997f;
				}
				if (num3 - this._scrollY > 0.848f)
				{
					this._scrollY = num3 - 0.848f;
				}
			}
			this._scrollY = Math.Max(0f, Math.Min(this._scrollY, Math.Max(0f, num - 0.848f)));
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00117210 File Offset: 0x00115410
		private bool HandleDetailKey(Keys key)
		{
			int count = this.DetailActions().Count;
			if (key <= Keys.M)
			{
				if (key <= Keys.Return)
				{
					if (key == Keys.Back)
					{
						this._detail = null;
						this._confirmForget = false;
						return true;
					}
					if (key != Keys.Return)
					{
						return false;
					}
					this.ActivateDetail();
					return true;
				}
				else
				{
					switch (key)
					{
					case Keys.Prior:
						goto IL_14B;
					case Keys.Next:
						goto IL_17F;
					case Keys.End:
					case Keys.Home:
						return false;
					case Keys.Left:
					case Keys.Up:
						this._action = (this._action - 1 + count) % count;
						this._confirmForget = false;
						return true;
					case Keys.Right:
					case Keys.Down:
						this._action = (this._action + 1) % count;
						this._confirmForget = false;
						return true;
					default:
						if (key != Keys.E)
						{
							if (key != Keys.M)
							{
								return false;
							}
							goto IL_10A;
						}
						break;
					}
				}
			}
			else if (key <= Keys.Add)
			{
				if (key != Keys.Q)
				{
					if (key == Keys.R)
					{
						goto IL_10A;
					}
					if (key != Keys.Add)
					{
						return false;
					}
					goto IL_17F;
				}
			}
			else
			{
				if (key == Keys.Subtract)
				{
					goto IL_14B;
				}
				if (key == Keys.Oemplus)
				{
					goto IL_17F;
				}
				if (key != Keys.OemMinus)
				{
					return false;
				}
				goto IL_14B;
			}
			this.StepPerson((key == Keys.E) ? 1 : -1);
			return true;
			IL_10A:
			if (!this._mindView && !this._crimeView)
			{
				this._mindView = true;
			}
			else if (this._mindView)
			{
				this._mindView = false;
				this._crimeView = true;
			}
			else
			{
				this._crimeView = false;
			}
			this._crimeScroll = 0;
			return true;
			IL_14B:
			if (this._crimeView)
			{
				this._crimeScroll = Math.Max(0, this._crimeScroll - 3);
				return true;
			}
			this._historyScroll = Math.Max(0, this._historyScroll - 3);
			return true;
			IL_17F:
			if (this._crimeView)
			{
				this._crimeScroll = Math.Min(Math.Max(0, this._crimeTotal - 1), this._crimeScroll + 3);
				return true;
			}
			int num;
			int num2;
			this._historyScroll = Math.Min(Math.Max(0, this.DetailLines(this._detail, out num, out num2).Count - 1), this._historyScroll + 3);
			return true;
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x001173FC File Offset: 0x001155FC
		private void StepPerson(int dir)
		{
			List<NemesisRecord> list;
			if (this._worldPage)
			{
				list = this.WorldPeople(this.SelectedEvent());
			}
			else if (this.Faction == null)
			{
				list = (from n in this.Entries().Select(delegate(RosterMenu.ListEntry e)
				{
					NemesisRecord result;
					if ((result = e.Rec) == null)
					{
						if (!this._listAll)
						{
							return null;
						}
						result = this._mod.PersonRecord(e.Acq);
					}
					return result;
				})
				where n != null
				select n).ToList<NemesisRecord>();
			}
			else
			{
				list = (from n in this.BuildRows().SelectMany((RosterMenu.Row r) => r.Items)
				where n != null
				select n).Distinct<NemesisRecord>().ToList<NemesisRecord>();
			}
			if (list.Count < 2)
			{
				return;
			}
			int num = list.IndexOf(this._detail);
			NemesisRecord next = list[(num < 0) ? 0 : ((num + dir + list.Count) % list.Count)];
			this._detail = next;
			this._action = 0;
			this._confirmForget = false;
			this._historyScroll = 0;
			this._crimeScroll = 0;
			if (this._worldPage)
			{
				this._worldPerson = list.IndexOf(next);
				return;
			}
			if (this.Faction == null)
			{
				List<RosterMenu.ListEntry> list2 = this.Entries();
				int num2 = list2.FindIndex((RosterMenu.ListEntry e) => e.Rec == next || (e.Acq != null && next.Person && next.PersonAcq == e.Acq));
				if (num2 >= 0)
				{
					this._listIndex = num2;
					this._listKey = RosterMenu.KeyOf(list2[num2]);
					return;
				}
			}
			else
			{
				List<RosterMenu.Row> list3 = this.BuildRows();
				for (int i = 0; i < list3.Count; i++)
				{
					int num3 = list3[i].Items.IndexOf(next);
					if (num3 >= 0)
					{
						this._row = i;
						this._col = num3;
						return;
					}
				}
			}
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x001175E8 File Offset: 0x001157E8
		private void ActivateDetail()
		{
			this._action = Math.Min(this._action, this.DetailActions().Count - 1);
			string a = this.DetailActions()[this._action];
			if (a == "Ascender")
			{
				this._mod.Promote(this._detail);
				return;
			}
			if (a == "Invocar ahora")
			{
				this._mod.RequestSummon(this._detail);
				this.Toggle();
				return;
			}
			if (a == "Rastrear campamento")
			{
				this._mod.TrackCamp(this._detail);
				this.Toggle();
				return;
			}
			if (a == "Rayos X")
			{
				this._mod.OpenXRays(this._detail);
				return;
			}
			if (!(a == "Olvidarlo") && !(a == "Olvidarla"))
			{
				this._detail = null;
				return;
			}
			if (!this._confirmForget)
			{
				this._confirmForget = true;
				return;
			}
			this._mod.Forget(this._detail);
			this._detail = null;
			this._confirmForget = false;
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00117704 File Offset: 0x00115904
		private List<string> DetailActions()
		{
			List<string> list = new List<string>();
			if (this._detail.Story || this._detail.Person || this._detail.LeaderPage)
			{
				list.Add("Atrás");
				return list;
			}
			if (this._mod.DebugOn && this._detail.IsAlive && this._detail.Rank < 5)
			{
				list.Add("Ascender");
			}
			if (this._mod.DebugOn && this._detail.CanHunt && this._mod.ActiveRecord == null)
			{
				list.Add("Invocar ahora");
			}
			if (this._mod.CanTrackCamp(this._detail))
			{
				list.Add("Rastrear campamento");
			}
			if (this._mod.XRayShotsOn(this._detail) > 0)
			{
				list.Add("Rayos X");
			}
			if (this._mod.DebugOn)
			{
				list.Add(Models.Female(this._detail.ModelHash) ? "Olvidarla" : "Olvidarlo");
			}
			list.Add("Atrás");
			return list;
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x0011782C File Offset: 0x00115A2C
		public void Draw()
		{
			if (this._detail != null && !this._detail.Person && !this._detail.LeaderPage && !this._mod.Roster.Contains(this._detail) && !this._mod.StoryRoster.Contains(this._detail))
			{
				this._detail = null;
			}
			Theme.Window(0.04f, ((this._detail != null) ? 0.868f : 0.868f) + 0.034f);
			if (this._detail != null)
			{
				this.DrawDetail();
			}
			else if (this._worldPage)
			{
				this.DrawWorld();
			}
			else
			{
				this.DrawHierarchy();
			}
			if (this._resetOpen)
			{
				this.DrawResetPanel();
			}
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x001178EC File Offset: 0x00115AEC
		private void DrawHierarchy()
		{
			if (this.Faction == null)
			{
				this.DrawUnaffiliatedList();
				return;
			}
			List<RosterMenu.Row> list = this.BuildRows();
			this.EnsureSelection(list);
			if (this._row >= 0)
			{
				this.KeepVisible(list[this._row], this._row);
			}
			this.ScrollToSelection(list);
			Gang faction = this.Faction;
			List<NemesisRecord> list2 = this.MembersOf(faction);
			if (!faction.StoryOnly)
			{
				list2.Count((NemesisRecord n) => n.CanHunt && !n.PresumedDead);
			}
			if (this._zoomIndex >= 0)
			{
				string.Format("  -  zoom {0}%", (int)Math.Round((double)(this._z * 100f)));
			}
			string str;
			bool flag;
			if (!faction.StoryOnly)
			{
				ValueTuple<string, string, bool> valueTuple = this._mod.LeaderCard(faction);
				str = valueTuple.Item1;
				flag = valueTuple.Item3;
			}
			else
			{
				string leaderName = faction.LeaderName;
				flag = false;
				str = leaderName;
			}
			this.DrawHeader(RosterMenu.FactionName(faction).ToUpperInvariant(), faction.Placeholder ? "Jerarquía  -  próximamente" : (flag ? "Sin líder - el puesto está vacante" : ("Jerarquía bajo " + str)), RosterMenu.AccentOf(faction));
			this._mod.NotorietyOf(faction);
			this.DrawFactionBar(null);
			Theme.PanelRect(0.07f, 0.26099998f, 0.86f, 0.607f, 215);
			if (faction.Placeholder)
			{
				UiDraw.Text("The " + faction.Name + " la jerarquía llegará pronto: sus rangos subirán, bajarán y cambiarán por sí solos. Nadie se ha unido aún.", 0.089999996f, 0.838f, 0.24f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			}
			else if (list2.Count == 0)
			{
				UiDraw.Text("Nadie de " + faction.Name + " se ha cruzado contigo aún. Mata a uno —o deja que te mate— y ocupará su lugar aquí.", 0.089999996f, 0.838f, 0.24f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			}
			RosterMenu.DrawFooter(0.868f, this.MainFooter(false));
			this.DrawConnectors(list);
			int num = 0;
			foreach (RosterMenu.Row row in list)
			{
				if (row.LoreName == null)
				{
					for (int i = 0; i < this.Shown(row); i++)
					{
						if (((row.Tier == RosterMenu.Tier.Fallen) ? (this._offsets[list.IndexOf(row)] + i) : i) < row.Items.Count && RosterMenu.Visible(this.Sy(this.SeatY(row, i)), this.CH))
						{
							num++;
						}
					}
				}
			}
			int num2 = UiDraw.TextSlotsLeft - 40;
			this._leanStat = (faction.OrdersNotFeelings || faction.StoryOnly || num2 < num * 6);
			this._leanTitle = (num2 < num * 4);
			this._rawNames = (num2 < num * 2);
			if ((double)UiDraw.RealMs - this._textLogAt > 5000.0 || this._textLogFor != faction.Id)
			{
				this._textLogAt = (double)UiDraw.RealMs;
				this._textLogFor = faction.Id;
				this._mod.MenuLog(string.Format("TEXT hierarchy {0}: last frame {1}/{2} slots, {3} texts cut, {4} rects ({5} before the cards) - now {6} used before the cards, {7} cards shown, lean{8}{9}{10}", new object[]
				{
					faction.Id,
					UiDraw.LastFrameSlots,
					UiDraw.TextSlotLimit,
					UiDraw.LastFrameSkipped,
					UiDraw.LastFrameRects,
					UiDraw.RectsThisFrame,
					UiDraw.TextSlotsThisFrame,
					num,
					this._leanStat ? " stat" : "",
					this._leanTitle ? " title" : "",
					this._rawNames ? " names" : ""
				}));
			}
			for (int j = 0; j < 5; j++)
			{
				for (int k = 0; k < list.Count; k++)
				{
					this.DrawRow(list[k], k, j);
				}
			}
			ValueTuple<int, int, int> dim = Theme.Dim;
			float num3 = list[list.Count - 1].Y + this.RowHeight(list[list.Count - 1]) - 0.27299997f;
			float num4 = 0.57500005f;
			if (num3 > num4 + 0.001f)
			{
				Theme.ScrollBar(0.919f, 0.27299997f, num4, this._scrollY / num3, num4 / num3);
			}
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00117DB0 File Offset: 0x00115FB0
		private void DrawRow(RosterMenu.Row row, int r, int pass)
		{
			ValueTuple<int, int, int> labelColor = RosterMenu.LabelColor;
			int item = labelColor.Item1;
			int item2 = labelColor.Item2;
			int item3 = labelColor.Item3;
			if (pass > 0)
			{
				if (row.LoreName != null || row.Items.Count == 0)
				{
					return;
				}
				for (int i = 0; i < this.Shown(row); i++)
				{
					int num = (row.Tier == RosterMenu.Tier.Fallen) ? (this._offsets[r] + i) : i;
					float y = this.Sy(this.SeatY(row, i));
					if (num < row.Items.Count && RosterMenu.Visible(y, this.CH))
					{
						this.DrawCard(row.Items[num], this.SeatX(row, i), y, r == this._row && num == this._col, row.Tier == RosterMenu.Tier.Fallen, pass);
					}
				}
				return;
			}
			else
			{
				float num2 = this.Sy(row.Y) - 0.028f;
				if (num2 >= 0.26799998f && num2 + 0.02f <= 0.848f)
				{
					Theme.Section(row.Label, 0.085999995f, num2, 0.07f, new ValueTuple<int, int, int>?((row.Tier == RosterMenu.Tier.Fallen) ? Theme.Faint : RosterMenu.AccentOf(this.Faction)));
				}
				if (row.LoreName != null)
				{
					if (RosterMenu.Visible(this.Sy(row.Y), this.CH))
					{
						float x = this.RowStartX(row);
						float y2 = this.Sy(row.Y);
						this.DrawLoreLeader(row, x, y2, r == this._row && this._col == 0 && row.Items.Count > 0);
						if (row.Items.Count > 0)
						{
							int rr = r;
							UiMouse.Region(x, y2, this.CW, this.CH, () => this.HoverCard(rr, 0), delegate
							{
								UiMouse.Send(Keys.Return);
							});
						}
					}
					return;
				}
				if (!row.FixedSeats && row.Items.Count == 0)
				{
					if (RosterMenu.Visible(this.Sy(row.Y), this.CH))
					{
						UiDraw.Text("Ninguno aún", 0.5f - Theme.MixedWidth("Ninguno aún", 0.26f, 0.0222f) / 2f, this.Sy(row.Y) + this.CH / 2f - 0.012f, 0.26f, Theme.Faint.Item1, Theme.Faint.Item2, Theme.Faint.Item3, 255);
					}
					return;
				}
				int num3 = this.Shown(row);
				for (int j = 0; j < num3; j++)
				{
					int num4 = (row.Tier == RosterMenu.Tier.Fallen) ? (this._offsets[r] + j) : j;
					float x2 = this.SeatX(row, j);
					float y3 = this.Sy(this.SeatY(row, j));
					if (RosterMenu.Visible(y3, this.CH))
					{
						if (num4 < row.Items.Count)
						{
							this.DrawCard(row.Items[num4], x2, y3, r == this._row && num4 == this._col, row.Tier == RosterMenu.Tier.Fallen, 0);
							int ci = num4;
							UiMouse.Region(x2, y3, this.CW, this.CH, () => this.HoverCard(r, ci), delegate
							{
								UiMouse.Send(Keys.Return);
							});
						}
						else
						{
							this.DrawVacant(x2, y3);
						}
					}
				}
				if (row.Tier == RosterMenu.Tier.Fallen && RosterMenu.Visible(this.Sy(row.Y), this.CH))
				{
					if (this._offsets[r] > 0)
					{
						RosterMenu.Chevron(this.SeatX(row, 0) - 0.014f, this.Sy(row.Y) + this.CH / 2f, true, item, item2, item3);
					}
					if (this._offsets[r] + this.PerLine < row.Items.Count)
					{
						RosterMenu.Chevron(this.SeatX(row, num3 - 1) + this.CW + 0.006f, this.Sy(row.Y) + this.CH / 2f, false, item, item2, item3);
					}
				}
				return;
			}
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x0011828C File Offset: 0x0011648C
		private static void Chevron(float x, float cy, bool left, int r, int g, int b)
		{
			if (Theme.MenuArrow(left ? Theme.ArrowDir.Left : Theme.ArrowDir.Right, x + 0.0048f, cy, 0.02f, new ValueTuple<int, int, int>(r, g, b), 230))
			{
				return;
			}
			float num = 0.0016f;
			float num2 = 0.026f;
			for (int i = 0; i < 6; i++)
			{
				float num3 = num2 * (float)(6 - i) / 6f;
				UiDraw.Rect(left ? (x + (float)i * num) : (x + (float)(5 - i) * num), cy - num3 / 2f, num, num3, r, g, b, 230);
			}
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00118318 File Offset: 0x00116518
		private void DrawConnectors(List<RosterMenu.Row> rows)
		{
			ValueTuple<int, int, int> lineColor = RosterMenu.LineColor;
			RosterMenu.<>c__DisplayClass151_0 CS$<>8__locals1;
			CS$<>8__locals1.cr = lineColor.Item1;
			CS$<>8__locals1.cg = lineColor.Item2;
			CS$<>8__locals1.cb = lineColor.Item3;
			int num = 0;
			while (num + 1 < rows.Count)
			{
				RosterMenu.Row row = rows[num];
				RosterMenu.Row row2 = rows[num + 1];
				if (row2.Tier == RosterMenu.Tier.Fallen)
				{
					break;
				}
				int num2 = this.Shown(row2);
				int num3 = Math.Min(this.PerLine, num2);
				if (num3 != 0)
				{
					float num4 = this.Sy(row2.Y) - 0.034f * Math.Max(this._z, 0.75f);
					RosterMenu.<DrawConnectors>g__V|151_0(0.5f, this.Sy(row.Y + this.RowHeight(row)), num4, ref CS$<>8__locals1);
					if (num4 >= 0.27299997f && num4 <= 0.848f)
					{
						UiDraw.HLine(this.SeatX(row2, 0) + this.CW / 2f, this.SeatX(row2, num3 - 1) + this.CW / 2f, num4, CS$<>8__locals1.cr, CS$<>8__locals1.cg, CS$<>8__locals1.cb, 210, 0.0012f);
					}
					for (int i = 0; i < num3; i++)
					{
						RosterMenu.<DrawConnectors>g__V|151_0(this.SeatX(row2, i) + this.CW / 2f, num4, this.Sy(row2.Y), ref CS$<>8__locals1);
					}
					for (int j = this.PerLine; j < num2; j++)
					{
						RosterMenu.<DrawConnectors>g__V|151_0(this.SeatX(row2, j) + this.CW / 2f, this.Sy(this.SeatY(row2, j - this.PerLine)) + this.CH, this.Sy(this.SeatY(row2, j)), ref CS$<>8__locals1);
					}
				}
				num++;
			}
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x001184F8 File Offset: 0x001166F8
		private static string FitText(string s, float scale, float width, bool name)
		{
			return RosterMenu.Fit(s, scale, width, name, (string x) => Theme.MixedWidth(x, scale, 0.0222f));
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x0011852C File Offset: 0x0011672C
		private static string FitTitle(string s, float scale, float width, bool name = true)
		{
			return RosterMenu.Fit(s, scale, width, name, (string x) => Theme.TitleWidth(x, scale));
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00118560 File Offset: 0x00116760
		private static string Fit(string s, float scale, float width, bool name, Func<string, float> w)
		{
			if (string.IsNullOrEmpty(s) || w(s) <= width)
			{
				return s ?? "";
			}
			int num = s.IndexOf(' ');
			if (name && num > 0)
			{
				string text = s[0].ToString() + ". " + s.Substring(num + 1);
				if (w(text) <= width)
				{
					return text;
				}
			}
			if (!name)
			{
				string text2 = Theme.Marquee(s, width, w);
				if (text2 != null)
				{
					return text2;
				}
			}
			string text3 = s;
			while (text3.Length > 4 && w(text3 + "...") > width)
			{
				int num2 = text3.LastIndexOf(' ');
				text3 = (((double)num2 > (double)text3.Length * 0.6) ? text3.Substring(0, num2) : text3.Substring(0, text3.Length - 1));
			}
			return text3.TrimEnd(new char[]
			{
				' ',
				',',
				'-',
				';',
				':'
			}) + "...";
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00118660 File Offset: 0x00116860
		private static List<string> WrapFit(string s, float scale, float width, int max)
		{
			List<string> list = RosterMenu.WrapBody(s, scale, width);
			if (list.Count <= max)
			{
				return list;
			}
			List<string> list2 = list.Take(max).ToList<string>();
			list2[max - 1] = RosterMenu.FitText(list2[max - 1] + " " + string.Join(" ", list.Skip(max)), scale, width, false);
			return list2;
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x001186C4 File Offset: 0x001168C4
		private static string GoneNote(NemesisRecord r)
		{
			if (r == null)
			{
				return null;
			}
			bool flag = r.IsAlive && r.PresumedDead && !NemesisRecord.ShowSecrets;
			if (r.IsAlive && !r.PresumedDead)
			{
				return null;
			}
			if (r.IsAlive && NemesisRecord.ShowSecrets)
			{
				return "dado por muerto";
			}
			if (r.StoryGone != null)
			{
				return r.StoryGone.ToLowerInvariant();
			}
			if (r.Fate != null)
			{
				return r.Fate.ToLowerInvariant();
			}
			List<HistoryEntry> history = r.History;
			HistoryEntry historyEntry;
			if (history == null)
			{
				historyEntry = null;
			}
			else
			{
				historyEntry = history.LastOrDefault((HistoryEntry h) => h.Text != null && h.Text.StartsWith("KILLED BY "));
			}
			HistoryEntry historyEntry2 = historyEntry;
			if ((flag || !r.KilledByPlayer) && (historyEntry2 == null || !historyEntry2.Text.StartsWith("ABATIDO POR TI")))
			{
				return "dead";
			}
			return "muerto - abatido por ti";
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x001187A4 File Offset: 0x001169A4
		internal static string RankText(NemesisRecord n)
		{
			Gang gang = Gangs.Of(n);
			if (gang == null)
			{
				return string.Format("Rango {0}", n.Rank);
			}
			foreach (ValueTuple<string, int, int> valueTuple in gang.Tiers)
			{
				if (n.Rank >= valueTuple.Item2)
				{
					return string.Format("{0} ({1})", Backstory.Singular(valueTuple.Item1), n.Rank);
				}
			}
			return string.Format("Rango {0}", n.Rank);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00118834 File Offset: 0x00116A34
		private static string CardName(NemesisRecord n)
		{
			if (n.Id == "vdl_dutch")
			{
				return "Dutch";
			}
			if (n.Id == "vdl_arthur")
			{
				return "Arthur";
			}
			if (n.Id == "vdl_john")
			{
				return "John";
			}
			if (!(n.Id == "vdl_hosea"))
			{
				return n.Name;
			}
			return "Hosea";
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x001188A8 File Offset: 0x00116AA8
		private void DrawCard(NemesisRecord n, float x, float y, bool selected, bool fallen, int pass)
		{
			float cw = this.CW;
			float ch = this.CH;
			float z = this._z;
			float num = 0.009f * z;
			if (pass == 0)
			{
				ValueTuple<int, int, int> card = Theme.Card;
				UiDraw.Rect(x, y, cw, ch, card.Item1, card.Item2, card.Item3, fallen ? 150 : 240);
				if (!fallen)
				{
					ValueTuple<int, int, int> valueTuple = n.Paralyzed ? new ValueTuple<int, int, int>(150, 150, 175) : UiDraw.HateColor(n.HateTier);
					Theme.Hairline(x, y, cw, ch, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255, 2);
				}
				float num2 = this.CardIcons(n, x, y, cw, num, z, fallen, false);
				ValueTuple<int, int, int> valueTuple2 = fallen ? Theme.Dim : Theme.Title;
				UiDraw.TextFont(RosterMenu.FitTitle(RosterMenu.CardName(n), this.T(0.3f), cw - 2f * num - num2 - 0.004f, true), this._rawNames ? null : UiDraw.TitleFont, x + num, y + 0.007f * z, this.T(0.3f), valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255, false);
				return;
			}
			if (pass == 3)
			{
				if (selected)
				{
					Theme.RedFrame(x - 0.00052083336f, y - (float)Theme.CardFrameUp * 0.0009259259f, cw + 0.0010416667f, ch, 0.95f, 5);
				}
				return;
			}
			if (pass == 4)
			{
				if (!fallen)
				{
					this.CardIcons(n, x, y, cw, num, z, fallen, true);
					this.DrawPips(n, x, y, cw, ch, num, z);
				}
				return;
			}
			if (fallen ? (pass == 2) : (pass == 1))
			{
				if (this._leanTitle && !fallen)
				{
					return;
				}
				ValueTuple<int, int, int> valueTuple3 = fallen ? Theme.Faint : Theme.Dim;
				UiDraw.Text(RosterMenu.FitText(n.Title, this.T(0.21f), cw - 2f * num, false), x + num, y + 0.029f * z, this.T(0.21f), valueTuple3.Item1, valueTuple3.Item2, valueTuple3.Item3, 255);
				return;
			}
			else
			{
				if (fallen ? (pass != 1) : (pass != 2))
				{
					return;
				}
				float y2 = y + ch - 0.021f * z;
				if (fallen)
				{
					bool flag = n.Status == "Dado por muerto";
					string str;
					if (!flag)
					{
						if ((str = n.StoryGone) == null)
						{
							str = (n.Fate ?? "Muerto");
						}
					}
					else
					{
						str = "Dado por muerto";
					}
					UiDraw.Text(RosterMenu.FitText(str + " - " + RosterMenu.RankText(n), this.T(0.21f), cw - 2f * num, false), x + num, y2, this.T(0.21f), flag ? 200 : 190, flag ? 150 : 60, flag ? 60 : 50, 255);
					return;
				}
				if (!n.Paralyzed)
				{
					return;
				}
				int r = 150;
				int g = 150;
				int b = 175;
				UiDraw.TextFont(RosterMenu.FitTitle("PARALIZADO", this.T(0.19f), cw - 2f * num - 0.05f * z, true), UiDraw.TitleFont, x + num, y2, this.T(0.19f), r, g, b, 255, false);
				return;
			}
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00118C10 File Offset: 0x00116E10
		private float CardIcons(NemesisRecord n, float x, float y, float w, float pad, float z, bool fallen, bool draw)
		{
			float num = 0f;
			if (fallen || n.Personality == null || (!n.TemperKnown && !this._mod.DebugOn))
			{
				return 0f;
			}
			float num2 = 0.016f * z;
			float num3 = num2 / UiDraw.Aspect;
			float num4 = x + w - pad - num3;
			float y2 = y + 0.008f * z;
			List<ValueTuple<string, ValueTuple<int, int, int>>> list = new List<ValueTuple<string, ValueTuple<int, int, int>>>
			{
				new ValueTuple<string, ValueTuple<int, int, int>>(Theme.TemperIcon(n.Personality), NemesisMod.TemperColor(n.Personality.Value))
			};
			if (n.TraitsRolled)
			{
				list.Add(new ValueTuple<string, ValueTuple<int, int, int>>(Theme.TraitIcon(Traits.Aggr(n.Aggression)), Theme.Dim));
				string trait = (n.Disposition == Disposition.Friendly || n.Disposition == Disposition.Surly) ? n.Disposition.ToString() : n.Sociability.ToString();
				list.Add(new ValueTuple<string, ValueTuple<int, int, int>>(Theme.TraitIcon(trait), Theme.Dim));
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				if (list[i].Item1 != null)
				{
					if (draw)
					{
						Theme.Icon(list[i].Item1, num4, y2, num2, list[i].Item2, 235);
					}
					num4 -= num3 + 0.003f;
					num += num3 + 0.003f;
				}
			}
			return num;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00118D98 File Offset: 0x00116F98
		private void DrawPips(NemesisRecord n, float x, float y, float w, float h, float pad, float z)
		{
			float num = y + h - 0.021f * z;
			int num2 = Math.Max(0, Math.Min(5, n.Rank));
			float num3 = Math.Max(0.0115f, 0.016f * z);
			float num4 = num3 / UiDraw.Aspect;
			float num5 = Math.Max(0.0004f, 0.0012f * z);
			if (5f * num4 + 4f * num5 > w * 0.42f)
			{
				num5 = 0.0003f;
				num4 = Math.Min(num4, (w * 0.42f - 4f * num5) / 5f);
				num3 = num4 * UiDraw.Aspect;
			}
			float num6 = num + this.T(0.19f) * 0.0375f - 0.004f * z;
			float num7 = x + w - pad - 5f * num4 - 4f * num5;
			bool flag = UiDraw.EnsureTextureDict("generic_textures");
			for (int i = 0; i < 5; i++)
			{
				float num8 = num7 + (float)i * (num4 + num5) + num4 / 2f;
				bool flag2 = i < num2;
				if (flag)
				{
					UiDraw.Sprite("generic_textures", "star", num8, num6, num4, num3, 0f, flag2 ? Theme.Gold.Item1 : 78, flag2 ? Theme.Gold.Item2 : 75, flag2 ? Theme.Gold.Item3 : 70, flag2 ? 255 : 235);
				}
				else
				{
					UiDraw.Rect(num8 - num4 * 0.3f, num6 - num3 * 0.3f, num4 * 0.6f, num3 * 0.6f, flag2 ? Theme.Gold.Item1 : 70, flag2 ? Theme.Gold.Item2 : 68, flag2 ? Theme.Gold.Item3 : 64, 230);
				}
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00118F74 File Offset: 0x00117174
		private void DrawLoreLeader(RosterMenu.Row row, float x, float y, bool selected = false)
		{
			ValueTuple<int, int, int> valueTuple = RosterMenu.AccentOf(this.Faction);
			float z = this._z;
			Theme.CardRect(x, y, this.CW, this.CH, 245, false);
			Theme.Hairline(x, y, this.CW, this.CH, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255, 2);
			Theme.Hairline(x + 0.0020833334f, y + 0.0037037036f, this.CW - 0.004166667f, this.CH - 0.0074074073f, Theme.Border.Item1, Theme.Border.Item2, Theme.Border.Item3, 220, 1);
			ValueTuple<int, int, int> valueTuple2 = row.LoreVacant ? Theme.Faint : Theme.Title;
			UiDraw.TextFont(RosterMenu.FitTitle(row.LoreName, this.T(0.3f), this.CW - 0.016f * z, true), UiDraw.TitleFont, x + 0.009f * z, y + 0.008f * z, this.T(0.3f), valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255, false);
			if (row.LoreVacant)
			{
				UiDraw.TextFont("PUESTO VACANTE", UiDraw.TitleFont, x + 0.009f * z, y + 0.036f * z, this.T(0.2f), Theme.Warn.Item1, Theme.Warn.Item2, Theme.Warn.Item3, 255, false);
			}
			if (selected)
			{
				Theme.RedFrame(x - 0.00052083336f, y - (float)Theme.CardFrameUp * 0.0009259259f, this.CW + 0.0010416667f, this.CH, 0.95f, 5);
			}
			float num = x + this.CW + 0.014f;
			UiDraw.Text(RosterMenu.FitText(row.LoreBlurb ?? "", 0.22f, 0.908f - num, false), num, y + this.CH / 2f - 0.01f, 0.22f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x001191A0 File Offset: 0x001173A0
		private void DrawVacant(float x, float y)
		{
			Theme.Hairline(x, y, this.CW, this.CH, Theme.Faint.Item1, Theme.Faint.Item2, Theme.Faint.Item3, 200, 1);
			UiDraw.Text("Vacante", x + this.CW / 2f - Theme.MixedWidth("Vacante", this.T(0.24f), 0.0222f) / 2f, y + this.CH / 2f - 0.011f * this._z, this.T(0.24f), Theme.Faint.Item1, Theme.Faint.Item2, Theme.Faint.Item3, 255);
		}

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00119266 File Offset: 0x00117466
		private string NextViewName
		{
			get
			{
				if (!this._mindView)
				{
					if (this._crimeView)
					{
						return "Historial";
					}
					if (!this.HerPage)
					{
						return "Su mente";
					}
					return "Su mente";
				}
				else
				{
					if (!this.HerPage)
					{
						return "Sus crímenes";
					}
					return "Sus crímenes";
				}
			}
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x0600122E RID: 4654 RVA: 0x001192A5 File Offset: 0x001174A5
		private bool HerPage
		{
			get
			{
				return this._detail != null && Models.Female(this._detail.ModelHash);
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x001192C4 File Offset: 0x001174C4
		private void DrawDetail()
		{
			RosterMenu.<>c__DisplayClass180_0 CS$<>8__locals1 = new RosterMenu.<>c__DisplayClass180_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.n = this._detail;
			Gang gang = Gangs.ById(CS$<>8__locals1.n.Gang);
			this.DrawHeader(CS$<>8__locals1.n.Name.ToUpperInvariant(), string.IsNullOrEmpty(CS$<>8__locals1.n.Title) ? this.StandingOf(CS$<>8__locals1.n) : CS$<>8__locals1.n.Title, RosterMenu.AccentOf(gang));
			Theme.PanelRect(0.07f, 0.13499999f, 0.86f, 0.733f, 215);
			bool flag = !this._mindView && !this._crimeView;
			UiMouse.WheelHandler = delegate(int d)
			{
				UiMouse.Send((d > 0) ? Keys.Prior : Keys.Next);
			};
			string text = flag ? "Historial" : (this._crimeView ? "Desplazar" : null);
			RosterMenu.DrawFooter(0.868f, RosterMenu.UsingPad ? ("Cruceta  Navegar     A  Seleccionar     LB/RB  Ant./Sig.     Y  " + this.NextViewName + ((text != null) ? ("     LT/RT  " + text) : "") + "     B  Atrás") : ("Flechas  Navegar     Enter  Seleccionar     Q/E  Ant./Sig.     R  " + this.NextViewName + ((text != null) ? ("     PgUp/PgDn  " + text) : "") + "     Retroceso  Atrás"));
			List<string> list = this.DetailActions();
			this._action = Math.Min(this._action, list.Count - 1);
			float num = 0.09f;
			float num2 = 0.81299996f;
			for (int i = 0; i < list.Count; i++)
			{
				string text2 = (list[i].StartsWith("Olvidar a ") && this._confirmForget) ? "Confirmar olvido" : list[i];
				bool flag2 = i == this._action;
				int ai = i;
				UiMouse.Region(num, num2, 0.115f, 0.036f, delegate
				{
					if (CS$<>8__locals1.<>4__this._action == ai)
					{
						return false;
					}
					CS$<>8__locals1.<>4__this._action = ai;
					CS$<>8__locals1.<>4__this._confirmForget = false;
					return true;
				}, delegate
				{
					UiMouse.Send(Keys.Return);
				});
				if (flag2)
				{
					Theme.SelectBox(num, num2, 0.115f, 0.036f, 235);
				}
				else
				{
					UiDraw.Rect(num, num2, 0.115f, 0.036f, Theme.Card.Item1, Theme.Card.Item2, Theme.Card.Item3, 230);
					Theme.Hairline(num, num2, 0.115f, 0.036f, Theme.Border.Item1, Theme.Border.Item2, Theme.Border.Item3, 230, 1);
				}
				int num3 = flag2 ? 245 : 200;
				UiDraw.TextFont(text2, UiDraw.TitleFont, num + 0.01f, num2 + 0.008f, 0.26f, num3, num3, num3, 255, false);
				num += 0.127f;
			}
			CS$<>8__locals1.acc = RosterMenu.AccentOf(gang);
			CS$<>8__locals1.lx = 0.09f;
			float num4 = 0.52f;
			CS$<>8__locals1.y = 0.15499999f;
			ValueTuple<int, int, int> labelColor = RosterMenu.LabelColor;
			CS$<>8__locals1.lr = labelColor.Item1;
			CS$<>8__locals1.lg = labelColor.Item2;
			CS$<>8__locals1.lb = labelColor.Item3;
			if (this._gapFor != CS$<>8__locals1.n.Id)
			{
				this._gapFor = CS$<>8__locals1.n.Id;
				this._rowGap = 0f;
				this._secGap = 0f;
			}
			CS$<>8__locals1.lRows = 0;
			CS$<>8__locals1.lSecs = 0;
			CS$<>8__locals1.lExtra = 0f;
			CS$<>8__locals1.leftLimit = 0.801f;
			this._histHold = ((!this._mindView && !this._crimeView) ? 44 : 0);
			UiDraw.FrameReserve += this._histHold;
			int num5 = this._mod.FollowerCount(CS$<>8__locals1.n.Rank);
			Theme.Chip((gang != null) ? gang.Name.ToUpperInvariant() : "SIN BANDA", CS$<>8__locals1.lx, CS$<>8__locals1.y, CS$<>8__locals1.acc, gang != null);
			CS$<>8__locals1.y += 0.034f;
			if (CS$<>8__locals1.n.Story)
			{
				CS$<>8__locals1.<DrawDetail>g__Sec|2(CS$<>8__locals1.lx, "CON LA BANDA");
				CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "ROL", string.IsNullOrEmpty(CS$<>8__locals1.n.Title) ? this.StandingOf(CS$<>8__locals1.n) : CS$<>8__locals1.n.Title, null, 0.25f);
				List<PastEntry> past = CS$<>8__locals1.n.Past;
				PastEntry pastEntry;
				if (past == null)
				{
					pastEntry = null;
				}
				else
				{
					pastEntry = past.FirstOrDefault((PastEntry e) => e.Kind == "gang");
				}
				PastEntry pastEntry2 = pastEntry;
				if (pastEntry2 != null)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "DESDE", string.Format("{0}  -  {1}", pastEntry2.Year, pastEntry2.Text), null, 0.21f);
				}
				if (CS$<>8__locals1.n.BornYear > 0)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "EDAD", string.Format("{0} (nacido en {1})", NemesisMod.GameYear() - CS$<>8__locals1.n.BornYear, CS$<>8__locals1.n.BornYear), null, 0.22f);
				}
				if (CS$<>8__locals1.n.IsAlive && CS$<>8__locals1.n.Id != NemesisMod.YouId)
				{
					int num6 = this._mod.RegardOfRecord(CS$<>8__locals1.n);
					ValueTuple<int, int, int> valueTuple = (num6 >= 10) ? new ValueTuple<int, int, int>(110, 200, 120) : ((num6 <= -10) ? new ValueTuple<int, int, int>(215, 90, 70) : Theme.Dim);
					CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "HACIA TI", string.Format("{0}  ({1}{2})", NemesisMod.RegardWord(num6), (num6 > 0) ? "+" : "", num6), new ValueTuple<int, int, int>?(valueTuple), 0.23f);
					float num7 = CS$<>8__locals1.lx + 0.095f;
					float num8 = 0.275f;
					float num9 = num7 + num8 / 2f;
					float num10 = 0.0046296297f;
					UiDraw.Rect(num7, CS$<>8__locals1.y - 0.004f, num8, num10, 52, 50, 46, 235);
					float num11 = num8 / 2f * (float)Math.Abs(num6) / 100f;
					UiDraw.Rect((num6 >= 0) ? num9 : (num9 - num11), CS$<>8__locals1.y - 0.004f, num11, num10, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
					UiDraw.Rect(num9 - 0.00052083336f, CS$<>8__locals1.y - 0.004f - 0.0018518518f, 0.0010416667f, num10 + 0.0037037036f, 235, 230, 220, 230);
					CS$<>8__locals1.y += 0.01f;
					string key = CS$<>8__locals1.n.Id.Substring(4);
					int num12 = this._mod.RegardBase(key);
					int num13 = this._mod.RegardEarned(key);
					UiDraw.Text(string.Format("capítulo {0}: por la historia está en {1}{2},  por tus actos {3}{4}", new object[]
					{
						this._mod.RegardChapter,
						(num12 > 0) ? "+" : "",
						num12,
						(num13 > 0) ? "+" : "",
						num13
					}), num7, CS$<>8__locals1.y - 0.002f, 0.19f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
					CS$<>8__locals1.y += 0.018f;
				}
				CS$<>8__locals1.y += 0.01f;
				CS$<>8__locals1.<DrawDetail>g__Sec|2(CS$<>8__locals1.lx, "VÍNCULOS");
				using (IEnumerator<Tie> enumerator = (from t in CS$<>8__locals1.n.Ties ?? new List<Tie>()
				orderby Math.Abs(t.Strength) descending
				select t).Take(6).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Tie tie = enumerator.Current;
						NemesisRecord nemesisRecord = this._mod.FindRecord(tie.Id);
						if (nemesisRecord != null)
						{
							CS$<>8__locals1.<DrawDetail>g__BadgeRow|4(tie.Type.ToUpperInvariant(), nemesisRecord.Name + "  -  " + tie.Note, (tie.Strength < 0) ? Theme.Warn : Theme.Dim, new ValueTuple<int, int, int>?((tie.Strength < 0) ? Theme.Warn : Theme.Text));
						}
					}
					goto IL_1950;
				}
			}
			if (CS$<>8__locals1.n.LeaderPage)
			{
				CS$<>8__locals1.<DrawDetail>g__Sec|2(CS$<>8__locals1.lx, "LIDERA A " + (((gang != null) ? gang.Name : null) ?? "").ToUpperInvariant().Replace("THE ", ""));
				CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "ROL", CS$<>8__locals1.n.Title, null, 0.25f);
				foreach (ValueTuple<string, string> valueTuple2 in this._mod.LeaderFacts(CS$<>8__locals1.n))
				{
					string item = valueTuple2.Item1;
					string item2 = valueTuple2.Item2;
					CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, item, item2, null, 0.21f);
				}
				CS$<>8__locals1.y += 0.01f;
				if (gang != null && !string.IsNullOrEmpty(gang.LoreBlurb ?? gang.LeaderBlurb))
				{
					CS$<>8__locals1.<DrawDetail>g__Sec|2(CS$<>8__locals1.lx, "LO QUE SE DICE DE ÉL");
					CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "", gang.LoreBlurb ?? gang.LeaderBlurb, new ValueTuple<int, int, int>?(Theme.Dim), 0.21f);
				}
			}
			else if (CS$<>8__locals1.n.Person && CS$<>8__locals1.n.PersonAcq != null)
			{
				Acquaintance personAcq = CS$<>8__locals1.n.PersonAcq;
				bool flag3 = Models.Female(personAcq.Model);
				CS$<>8__locals1.<DrawDetail>g__Sec|2(CS$<>8__locals1.lx, flag3 ? "QUIÉN ES ELLA PARA TI" : "QUIÉN ES PARA TI");
				CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "ESTADO", personAcq.Dead ? "Muerto" : (personAcq.Met ? "Os habéis conocido" : (flag3 ? "La conoces de vista" : "Lo conoces de vista")), personAcq.Dead ? new ValueTuple<int, int, int>?(Theme.Dim) : null, 0.25f);
				CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "TOWN", string.IsNullOrEmpty(personAcq.Town) ? "unknown" : personAcq.Town, null, 0.25f);
				string arg = (personAcq.LastGameMin > 0L) ? string.Format("  -  visto por última vez {0} {1}", GameDate.Month(GameDate.FromMinutes(personAcq.LastGameMin).Month), GameDate.FromMinutes(personAcq.LastGameMin).Day) : "";
				CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "KNOWN", string.Format("visto {0} vez/veces{1}{2}", Math.Max(1, personAcq.TimesSeen), (personAcq.TimesSeen == 1) ? "" : "s", arg), null, 0.21f);
				if (!string.IsNullOrEmpty(personAcq.Note))
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "NOTA", personAcq.Note, null, 0.21f);
				}
				if (!string.IsNullOrEmpty(personAcq.Affil) && (personAcq.Met || this._mod.DebugOn))
				{
					RosterMenu.<>c__DisplayClass180_0 CS$<>8__locals3 = CS$<>8__locals1;
					float lx = CS$<>8__locals1.lx;
					string label = "ANDA CON";
					string str = "amistoso con ";
					Gang gang2 = Gangs.ById(personAcq.Affil);
					CS$<>8__locals3.<DrawDetail>g__Row|3(lx, label, str + (((gang2 != null) ? gang2.Name : null) ?? personAcq.Affil) + (personAcq.Met ? "" : " (debug)"), new ValueTuple<int, int, int>?(Theme.Warn), 0.21f);
				}
				CS$<>8__locals1.y += 0.01f;
				CS$<>8__locals1.<DrawDetail>g__Sec|2(CS$<>8__locals1.lx, "SENTIMIENTOS");
				CS$<>8__locals1.<DrawDetail>g__Gauge|7("RENCOR", (personAcq.Grudge >= 60) ? "AMARGADO" : ((personAcq.Grudge >= 30) ? "DOLIDO" : ((personAcq.Grudge > 0) ? "LEVE" : "NINGUNO")), personAcq.Grudge, (personAcq.Grudge > 0) ? new ValueTuple<int, int, int>(215, 120, 70) : Theme.Dim, (personAcq.Grudge > 0 && personAcq.GrudgeSettled) ? "let it go" : null, null);
				CS$<>8__locals1.<DrawDetail>g__Gauge|7("MIEDO", (personAcq.Fear >= 60) ? "ATERRADO" : ((personAcq.Fear >= 30) ? "ASUSTADO" : ((personAcq.Fear > 0) ? "RECELOSO" : "NINGUNO")), personAcq.Fear, new ValueTuple<int, int, int>(120, 150, 210), null, null);
				CS$<>8__locals1.<DrawDetail>g__Gauge|7("LE CAES BIEN", (personAcq.Like >= 60) ? "AMISTOSO" : ((personAcq.Like >= 25) ? "CERCANO" : ((personAcq.Like > 0) ? "UN POCO" : "NO")), personAcq.Like, new ValueTuple<int, int, int>(110, 200, 120), null, null);
				if (personAcq.Favours > 0)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "FAVORES", (personAcq.FavourNotes != null && personAcq.FavourNotes.Count > 0) ? string.Join(",  ", personAcq.FavourNotes.Take(3)) : string.Format("{0}", personAcq.Favours), null, 0.21f);
				}
				if (personAcq.Witnessed != null && personAcq.Witnessed.Count > 0)
				{
					int num14 = 0;
					while (num14 < personAcq.Witnessed.Count && num14 < 3)
					{
						CS$<>8__locals1.<DrawDetail>g__BadgeRow|4((num14 == 0) ? (personAcq.Witnessed[num14].StartsWith("saw") ? "TE VIO" : "OYÓ") : "", personAcq.Witnessed[num14], Theme.Warn, null);
						num14++;
					}
				}
				if (personAcq.Reported)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "", flag3 ? "Te delató ante la ley." : "Te delató ante la ley.", new ValueTuple<int, int, int>?(Theme.Warn), 0.21f);
				}
				if (personAcq.KeptQuiet)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "", flag3 ? "Guarda silencio; te encargaste de ello." : "Guarda silencio; te encargaste de ello.", new ValueTuple<int, int, int>?(Theme.Dim), 0.21f);
				}
			}
			else
			{
				CS$<>8__locals1.<DrawDetail>g__Sec|2(CS$<>8__locals1.lx, "POSICIÓN");
				string text3 = RosterMenu.RankText(CS$<>8__locals1.n);
				int num15 = text3.IndexOf(" (");
				string word = ((num15 > 0) ? text3.Substring(0, num15) : string.Format("Rango {0}", CS$<>8__locals1.n.Rank)).ToUpperInvariant();
				float num16 = CS$<>8__locals1.<DrawDetail>g__WordRow|5("RANGO", word, Theme.Title);
				if (num16 > 0f)
				{
					RosterMenu.Stars(num16, CS$<>8__locals1.y + 0.012f, CS$<>8__locals1.n.Rank);
					if (CS$<>8__locals1.n.Rank < 5)
					{
						string text4 = string.Format("siguiente rango {0}/{1}", CS$<>8__locals1.n.RankXp, NemesisMod.XpNeed(CS$<>8__locals1.n.Rank));
						UiDraw.Text(text4, CS$<>8__locals1.lx + 0.4f - 0.01f - Theme.MixedWidth(text4, 0.2f, 0.0222f), CS$<>8__locals1.y + 0.004f, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
					}
					CS$<>8__locals1.y += 0.03f;
				}
				if (CS$<>8__locals1.n.Gang != null)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, NemesisMod.IsOfficer(CS$<>8__locals1.n) ? "CAMPAMENTO" : "CABALGA CON", this._mod.CampLine(CS$<>8__locals1.n), null, 0.25f);
				}
				ValueTuple<int, int, int> c = (!CS$<>8__locals1.n.IsAlive) ? Theme.Dim : (CS$<>8__locals1.n.PresumedDead ? Theme.Dim : (CS$<>8__locals1.n.Paralyzed ? new ValueTuple<int, int, int>(130, 150, 175) : Theme.Good));
				float num17 = CS$<>8__locals1.<DrawDetail>g__WordRow|5("ESTADO", CS$<>8__locals1.n.Status.ToUpperInvariant(), c);
				if (num17 > 0f)
				{
					if (CS$<>8__locals1.n.IsAlive && this._mod.Resting(CS$<>8__locals1.n))
					{
						num17 += Theme.Chip(string.Format("ESCONDIDO {0}H", Math.Max(1L, (CS$<>8__locals1.n.RestUntil - this._mod.GameMinutesNow() + 59L) / 60L)), num17, CS$<>8__locals1.y + 0.002f, new ValueTuple<int, int, int>(120, 150, 210), false) + 0.006f;
					}
					if (CS$<>8__locals1.n.Limp)
					{
						num17 += Theme.Chip("COJERA", num17, CS$<>8__locals1.y + 0.002f, Theme.Warn, false) + 0.006f;
					}
					CS$<>8__locals1.y += 0.03f;
				}
				if (num5 > 0)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "CUADRILLA", string.Format("cabalga con {0} {1}", num5, (num5 == 1) ? "hombre" : "men"), null, 0.25f);
				}
				CS$<>8__locals1.y += 0.01f;
				CS$<>8__locals1.<DrawDetail>g__Sec|2(CS$<>8__locals1.lx, "SENTIMIENTOS");
				if (CS$<>8__locals1.n.IsAlive)
				{
					List<Thought> item3 = this._mod.MoodOf(CS$<>8__locals1.n).Item2;
					RosterMenu.<>c__DisplayClass180_2 CS$<>8__locals4;
					CS$<>8__locals4.nowM = this._mod.GameMinutesNow();
					if (item3.Count == 0)
					{
						CS$<>8__locals1.<DrawDetail>g__MoodRow|6("ÁNIMO", "ESTABLE", Pron.Her("sin nada especial en mente", Models.Female(CS$<>8__locals1.n.ModelHash)), null, Theme.Dim);
					}
					int num18 = 0;
					while (num18 < item3.Count && num18 < 3)
					{
						Thought thought = item3[num18];
						CS$<>8__locals1.<DrawDetail>g__MoodRow|6((num18 == 0) ? "ÁNIMO" : "", thought.Mood.ToUpperInvariant(), thought.Text, RosterMenu.<DrawDetail>g__Left|180_14(thought, ref CS$<>8__locals4) + ((num18 == 2 && item3.Count > 3) ? string.Format("  +{0}", item3.Count - 3) : ""), NemesisMod.MoodColor(thought.Mood));
						num18++;
					}
					CS$<>8__locals1.y += 0.006f;
				}
				ValueTuple<int, int, int> valueTuple3 = UiDraw.HateColor(CS$<>8__locals1.n.HateTier);
				Intent intent = this._mod.IntentOf(CS$<>8__locals1.n);
				bool flag4 = Gangs.Orders(CS$<>8__locals1.n);
				RosterMenu.<>c__DisplayClass180_0 CS$<>8__locals5 = CS$<>8__locals1;
				string label2 = "ODIO";
				string word2 = CS$<>8__locals1.n.HateTier.ToString().ToUpperInvariant();
				int hate = CS$<>8__locals1.n.Hate;
				ValueTuple<int, int, int> c2 = valueTuple3;
				string note = Loadout.Describe(CS$<>8__locals1.n);
				int[] marks;
				if (!flag4)
				{
					int[] array = new int[2];
					array[0] = Loadout.BladeAt(CS$<>8__locals1.n);
					marks = array;
					array[1] = Loadout.GunAt(CS$<>8__locals1.n);
				}
				else
				{
					marks = null;
				}
				CS$<>8__locals5.<DrawDetail>g__Gauge|7(label2, word2, hate, c2, note, marks);
				if (!flag4 && this._mod.DebugOn)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "", string.Format("cuchillo a {0}, arma a {1}  ({2})  -  aumento de odio x{3:0.00}", new object[]
					{
						Loadout.BladeAt(CS$<>8__locals1.n),
						Loadout.GunAt(CS$<>8__locals1.n),
						Loadout.ShiftWhy(CS$<>8__locals1.n),
						this._mod.HateGainOf(CS$<>8__locals1.n)
					}), new ValueTuple<int, int, int>?(Theme.Dim), 0.2f);
				}
				int num19 = this._mod.EffectiveFear(CS$<>8__locals1.n);
				CS$<>8__locals1.<DrawDetail>g__Gauge|7("MIEDO", (num19 >= 60) ? "ATERRADO" : ((num19 >= 30) ? "ASUSTADO" : ((num19 > 0) ? "RECELOSO" : "NINGUNO")), num19, new ValueTuple<int, int, int>(150, 170, 210), null, null);
				CS$<>8__locals1.y += 0.004f;
				string word3 = (intent == Intent.Hunts) ? "TE DA CAZA" : ((intent == Intent.WontHunt) ? "TE ESPERA" : ((intent == Intent.ProvokedOnly) ? "SOLO SI LO PROVOCAS" : "NINGUNO"));
				ValueTuple<int, int, int> c3 = (intent == Intent.Hunts) ? Theme.Blood : ((intent == Intent.WontHunt) ? Theme.Warn : Theme.Dim);
				if (CS$<>8__locals1.<DrawDetail>g__WordRow|5("INTENCIÓN", word3, c3) > 0f)
				{
					CS$<>8__locals1.y += 0.03f;
					if (intent == Intent.WontHunt || intent == Intent.ProvokedOnly)
					{
						string text5 = Traits.Describe(intent);
						text5 = char.ToUpperInvariant(text5[0]).ToString() + text5.Substring(1);
						if (CS$<>8__locals1.y + 0.022f <= CS$<>8__locals1.leftLimit)
						{
							UiDraw.Text(RosterMenu.FitText(text5, 0.21f, 0.29500002f, false), CS$<>8__locals1.lx + 0.095f, CS$<>8__locals1.y - 0.004f, 0.21f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
							CS$<>8__locals1.y += 0.022f;
						}
					}
				}
				CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "ALCANCE", this._mod.RoamText(CS$<>8__locals1.n), new ValueTuple<int, int, int>?(Theme.Dim), 0.25f);
				CS$<>8__locals1.y += 0.01f;
				if (CS$<>8__locals1.y + 0.03f + 0.056f <= CS$<>8__locals1.leftLimit)
				{
					CS$<>8__locals1.<DrawDetail>g__Sec|2(CS$<>8__locals1.lx, "HISTORIAL");
				}
				List<ValueTuple<string, string, ValueTuple<int, int, int>>> tiles = new List<ValueTuple<string, string, ValueTuple<int, int, int>>>
				{
					RosterMenu.<DrawDetail>g__T|180_15(CS$<>8__locals1.n.TimesKilledPlayer, "TE MATÓ", Theme.Blood),
					RosterMenu.<DrawDetail>g__T|180_15(CS$<>8__locals1.n.TimesAmbushed, "VINO A POR TI", Theme.Text),
					RosterMenu.<DrawDetail>g__T|180_15(CS$<>8__locals1.n.PresumedDead ? 0 : CS$<>8__locals1.n.TimesCheatedDeath, "BURLÓ A LA MUERTE", Theme.Warn)
				};
				List<ValueTuple<string, string, ValueTuple<int, int, int>>> tiles2 = new List<ValueTuple<string, string, ValueTuple<int, int, int>>>
				{
					RosterMenu.<DrawDetail>g__T|180_15(CS$<>8__locals1.n.TimesKnockedOutByPlayer, "NOQUEADO", Theme.Text),
					RosterMenu.<DrawDetail>g__T|180_15(CS$<>8__locals1.n.TimesHogtied, "ATADO", Theme.Text),
					RosterMenu.<DrawDetail>g__T|180_15(CS$<>8__locals1.n.TimesRobbed, "ROBADO", Theme.Text),
					RosterMenu.<DrawDetail>g__T|180_15(CS$<>8__locals1.n.TimesInterrogated, "INTERROGADO", Theme.Text)
				};
				if (CS$<>8__locals1.y + 0.056f <= CS$<>8__locals1.leftLimit)
				{
					CS$<>8__locals1.y = RosterMenu.StatTiles(CS$<>8__locals1.lx, 0.4f, CS$<>8__locals1.y, tiles, 3, 1f);
				}
				if (CS$<>8__locals1.y + 0.056f <= CS$<>8__locals1.leftLimit)
				{
					CS$<>8__locals1.y = RosterMenu.StatTiles(CS$<>8__locals1.lx, 0.4f, CS$<>8__locals1.y, tiles2, 4, 1f);
				}
				if (CS$<>8__locals1.y + 0.03f <= CS$<>8__locals1.leftLimit)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(CS$<>8__locals1.lx, "PALABRAS", string.Format("provocado {0}   saludado {1}", CS$<>8__locals1.n.TimesAntagonized, CS$<>8__locals1.n.TimesGreeted) + (this._mod.DebugOn ? string.Format("   furia {0}/{1}", CS$<>8__locals1.n.Fury, 3) : ""), null, 0.25f);
				}
				this._leftEnd = CS$<>8__locals1.y;
			}
			IL_1950:
			float num20 = CS$<>8__locals1.y - CS$<>8__locals1.lExtra;
			float num21 = CS$<>8__locals1.leftLimit - 0.006f - num20;
			float num22 = (num21 > 0f) ? Math.Min(0.006f, num21 * 0.35f / (float)Math.Max(1, CS$<>8__locals1.lRows)) : 0f;
			float val = (num21 > 0f) ? Math.Min(0.045f, (num21 - num22 * (float)CS$<>8__locals1.lRows) / (float)Math.Max(1, CS$<>8__locals1.lSecs - 1)) : 0f;
			this._rowGap = Math.Max(0f, num22);
			this._secGap = ((CS$<>8__locals1.lSecs > 1) ? Math.Max(0f, val) : 0f);
			CS$<>8__locals1.y = 0.15499999f;
			bool flag5 = CS$<>8__locals1.n.TemperKnown || this._mod.DebugOn;
			if (this._mindView)
			{
				this.DrawMind(CS$<>8__locals1.n, num4, 0.4f, 0.095f, 0.026f, ref CS$<>8__locals1.y, 0.803f, CS$<>8__locals1.acc, flag5);
				return;
			}
			if (this._crimeView)
			{
				CS$<>8__locals1.y = this.DrawCrimeSheet(CS$<>8__locals1.n, num4, 0.4f, CS$<>8__locals1.y, 0.798f, CS$<>8__locals1.acc);
				return;
			}
			CS$<>8__locals1.<DrawDetail>g__Sec|2(num4, "NATURALEZA");
			if (flag5 && CS$<>8__locals1.n.Personality != null)
			{
				UiDraw.TextFont("CARÁCTER", UiDraw.TitleFont, num4 + 0.006f, CS$<>8__locals1.y + 0.002f, 0.2f, CS$<>8__locals1.lr, CS$<>8__locals1.lg, CS$<>8__locals1.lb, 255, false);
				float num23 = num4 + 0.095f;
				num23 += Theme.Icon(Theme.TemperIcon(CS$<>8__locals1.n.Personality), num23, CS$<>8__locals1.y - 0.003f, 0.026f, NemesisMod.TemperColor(CS$<>8__locals1.n.Personality.Value), 255) + 0.006f;
				UiDraw.Text(RosterMenu.FitText(Personalities.Describe(CS$<>8__locals1.n.Personality.Value), 0.23f, 0.26500002f, false), num23, CS$<>8__locals1.y, 0.23f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
				CS$<>8__locals1.y += 0.030000001f;
			}
			else
			{
				CS$<>8__locals1.<DrawDetail>g__Row|3(num4, "CARÁCTER", "Desconocido", null, 0.23f);
			}
			if (flag5 && CS$<>8__locals1.n.TraitsRolled)
			{
				UiDraw.TextFont("RASGOS", UiDraw.TitleFont, num4 + 0.006f, CS$<>8__locals1.y + 0.002f, 0.2f, CS$<>8__locals1.lr, CS$<>8__locals1.lg, CS$<>8__locals1.lb, 255, false);
				float num24 = num4 + 0.095f;
				foreach (string text6 in new string[]
				{
					CS$<>8__locals1.n.Sociability.ToString(),
					CS$<>8__locals1.n.Disposition.ToString(),
					Traits.Aggr(CS$<>8__locals1.n.Aggression),
					CS$<>8__locals1.n.Smarts.ToString()
				})
				{
					num24 += Theme.IconChip(text6, Theme.TraitIcon(text6), num24, CS$<>8__locals1.y - 0.003f, Theme.Dim) + 0.006f;
				}
				CS$<>8__locals1.y += 0.030000001f;
			}
			else
			{
				CS$<>8__locals1.<DrawDetail>g__Row|3(num4, "RASGOS", "Desconocido: enfréntate a él o hazlo hablar", new ValueTuple<int, int, int>?(Theme.Dim), 0.22f);
			}
			if (flag5)
			{
				List<string> quirks = CS$<>8__locals1.n.Quirks;
				if (quirks != null && quirks.Count > 0)
				{
					UiDraw.TextFont("MANÍAS", UiDraw.TitleFont, num4 + 0.006f, CS$<>8__locals1.y + 0.002f, 0.2f, CS$<>8__locals1.lr, CS$<>8__locals1.lg, CS$<>8__locals1.lb, 255, false);
					float num25 = num4 + 0.095f;
					foreach (string q in CS$<>8__locals1.n.Quirks)
					{
						num25 += Theme.IconChip(Quirks.Label(q), Theme.QuirkIcon(q), num25, CS$<>8__locals1.y - 0.003f, new ValueTuple<int, int, int>(170, 150, 220)) + 0.006f;
					}
					CS$<>8__locals1.y += 0.030000001f;
				}
			}
			List<string> skills = CS$<>8__locals1.n.Skills;
			if (skills != null && skills.Count > 0)
			{
				RosterMenu.<>c__DisplayClass180_0 CS$<>8__locals6 = CS$<>8__locals1;
				float x2 = num4;
				string label3 = "HABILIDADES";
				string separator = ",  ";
				IEnumerable<string> skills2 = CS$<>8__locals1.n.Skills;
				Func<string, string> selector;
				if ((selector = RosterMenu.<>O.<0>__SkillText) == null)
				{
					selector = (RosterMenu.<>O.<0>__SkillText = new Func<string, string>(Quirks.SkillText));
				}
				CS$<>8__locals6.<DrawDetail>g__Row|3(x2, label3, string.Join(separator, skills2.Select(selector)), new ValueTuple<int, int, int>?(Theme.Gold), 0.21f);
			}
			if (CS$<>8__locals1.n.LiedToYou > 0 || CS$<>8__locals1.n.Informant || CS$<>8__locals1.n.OwesYou)
			{
				CS$<>8__locals1.<DrawDetail>g__Row|3(num4, "CONTIGO", string.Join(",  ", from x in new string[]
				{
					(CS$<>8__locals1.n.LiedToYou > 0) ? string.Format("te mintió {0} veces", CS$<>8__locals1.n.LiedToYou) : null,
					CS$<>8__locals1.n.Informant ? "tu informante" : null,
					CS$<>8__locals1.n.OwesYou ? "owes you his life" : null
				}
				where x != null
				select x), new ValueTuple<int, int, int>?(Theme.Warn), 0.21f);
			}
			if (!CS$<>8__locals1.n.Story && !CS$<>8__locals1.n.Person)
			{
				string text7 = this._mod.KnownHabitText(CS$<>8__locals1.n);
				if (text7 != null)
				{
					string[] array3 = text7.Split(new string[]
					{
						";  "
					}, StringSplitOptions.RemoveEmptyEntries);
					for (int k = 0; k < array3.Length; k++)
					{
						CS$<>8__locals1.<DrawDetail>g__Row|3(num4, (k == 0) ? "HÁBITOS" : "", char.ToUpperInvariant(array3[k][0]).ToString() + array3[k].Substring(1), new ValueTuple<int, int, int>?(Theme.Gold), 0.21f);
					}
				}
			}
			CS$<>8__locals1.y += 0.01f;
			CS$<>8__locals1.<DrawDetail>g__Sec|2(num4, "ORIGEN");
			if (flag5 && CS$<>8__locals1.n.TraitsRolled)
			{
				if (CS$<>8__locals1.n.BornYear > 0)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(num4, "NACIMIENTO", string.Format("{0}, {1}  -  {2} años", CS$<>8__locals1.n.BornYear, CS$<>8__locals1.n.Birthplace ?? "quién sabe dónde", NemesisMod.GameYear() - CS$<>8__locals1.n.BornYear), null, 0.22f);
				}
				string str2 = (CS$<>8__locals1.n.Wealth == Wealth.Rich) ? "Rich" : ((CS$<>8__locals1.n.Wealth == Wealth.Comfortable) ? "Acomodado" : ((CS$<>8__locals1.n.Wealth == Wealth.Working) ? "Trabajador" : "Pobre"));
				CS$<>8__locals1.<DrawDetail>g__Row|3(num4, "MODO DE VIDA", (CS$<>8__locals1.n.Trade ?? "Errante") + "  -  " + str2, null, 0.25f);
				CS$<>8__locals1.<DrawDetail>g__Row|3(num4, "FE", (CS$<>8__locals1.n.Faith == "none") ? "Ninguna - impío" : (CS$<>8__locals1.n.Faith + ", " + CS$<>8__locals1.n.Devotion.ToString().ToLowerInvariant()), null, 0.25f);
				bool flag6 = Models.Female(CS$<>8__locals1.n.ModelHash);
				CS$<>8__locals1.<DrawDetail>g__Row|3(num4, "FAMILIA", (CS$<>8__locals1.n.Family == "single") ? "Soltero" : (((CS$<>8__locals1.n.Family == "married") ? "Casado" : (flag6 ? "Viuda" : "Viudo")) + ((CS$<>8__locals1.n.Children > 0) ? string.Format(", {0} {1}", CS$<>8__locals1.n.Children, (CS$<>8__locals1.n.Children == 1) ? "child" : "children") : ", sin hijos")), null, 0.25f);
				List<ValueTuple<NemesisRecord, string>> list2 = new List<ValueTuple<NemesisRecord, string>>();
				NemesisRecord nemesisRecord2 = (CS$<>8__locals1.n.KinId != null) ? this._mod.FindRecord(CS$<>8__locals1.n.KinId) : null;
				if (nemesisRecord2 != null)
				{
					list2.Add(new ValueTuple<NemesisRecord, string>(nemesisRecord2, Traits.Word(CS$<>8__locals1.n.KinRelation, flag6) + " de"));
				}
				IEnumerable<NemesisRecord> source = CS$<>8__locals1.n.Story ? this._mod.StoryRoster : this._mod.Roster;
				Func<NemesisRecord, bool> predicate;
				if ((predicate = CS$<>8__locals1.<>9__17) == null)
				{
					predicate = (CS$<>8__locals1.<>9__17 = ((NemesisRecord r) => r.KinId == CS$<>8__locals1.n.Id));
				}
				foreach (NemesisRecord nemesisRecord3 in source.Where(predicate))
				{
					list2.Add(new ValueTuple<NemesisRecord, string>(nemesisRecord3, Traits.Word(Traits.Inverse(nemesisRecord3.KinRelation), flag6) + " de"));
				}
				int num26 = 0;
				while (num26 < list2.Count && num26 < 3)
				{
					string text8 = RosterMenu.GoneNote(list2[num26].Item1);
					CS$<>8__locals1.<DrawDetail>g__Row|3(num4, (num26 == 0) ? "FAMILIA" : "", list2[num26].Item2 + " " + list2[num26].Item1.Name + ((text8 == null) ? "" : (" (" + text8 + ")")), new ValueTuple<int, int, int>?((text8 == null) ? Theme.Text : Theme.Blood), 0.23f);
					num26++;
				}
				List<NemesisRecord> list3 = this._mod.FriendsOf(CS$<>8__locals1.n);
				if (list3.Count > 0)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(num4, "AMIGOS", string.Join(",  ", list3.Take(3).Select(delegate(NemesisRecord f)
					{
						string name = f.Name;
						string text10 = RosterMenu.GoneNote(f);
						string str3 = (text10 != null) ? (" (" + text10 + ")") : "";
						string str4;
						if (!(f.Gang != CS$<>8__locals1.n.Gang))
						{
							str4 = "";
						}
						else
						{
							string str5 = " [";
							Gang gang3 = Gangs.ById(f.Gang);
							str4 = str5 + (((gang3 != null) ? gang3.Name : null) ?? "loner") + "]";
						}
						return name + str3 + str4;
					})), null, 0.22f);
				}
				if (CS$<>8__locals1.n.Pet != null)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(num4, "MASCOTA", CS$<>8__locals1.n.Pet, null, 0.25f);
				}
			}
			else
			{
				CS$<>8__locals1.<DrawDetail>g__Row|3(num4, "", "Desconocido: descubre más enfrentándote a él o haciéndolo hablar", new ValueTuple<int, int, int>?(Theme.Dim), 0.22f);
			}
			CS$<>8__locals1.y += 0.01f;
			if (!CS$<>8__locals1.n.Story && !CS$<>8__locals1.n.Person)
			{
				CS$<>8__locals1.<DrawDetail>g__Sec|2(num4, "RENCOR");
				CS$<>8__locals1.<DrawDetail>g__Row|3(num4, "POR QUÉ", Pron.Her(CS$<>8__locals1.n.GrudgeSummary, Models.Female(CS$<>8__locals1.n.ModelHash)), null, 0.22f);
				List<Scar> list4 = CS$<>8__locals1.n.PresumedDead ? CS$<>8__locals1.n.Scars.Take(CS$<>8__locals1.n.Scars.Count - 1).ToList<Scar>() : CS$<>8__locals1.n.Scars;
				if (list4.Count == 0)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(num4, "CICATRICES", "None", new ValueTuple<int, int, int>?(Theme.Dim), 0.25f);
				}
				int num27 = 0;
				while (num27 < list4.Count && num27 < 3 && CS$<>8__locals1.y < 0.783f)
				{
					CS$<>8__locals1.<DrawDetail>g__Row|3(num4, (num27 == 0) ? "CICATRICES" : "", string.Concat(new string[]
					{
						list4[num27].Description,
						"  (",
						list4[num27].Location,
						")",
						(num27 == 2 && list4.Count > 3) ? string.Format("  +{0} más", list4.Count - 3) : ""
					}), null, 0.22f);
					num27++;
				}
				CS$<>8__locals1.y += 0.01f;
			}
			UiDraw.FrameReserve = Math.Max(0, UiDraw.FrameReserve - this._histHold);
			this._histHold = 0;
			int num28;
			int num29;
			List<HistoryEntry> list5 = this.DetailLines(CS$<>8__locals1.n, out num28, out num29);
			float num30 = (CS$<>8__locals1.n.Story || this._leftEnd <= 0f) ? 0.803f : Math.Min(0.838f, Math.Max(this._leftEnd, CS$<>8__locals1.y + 0.15f));
			num30 = Math.Max(num30, Math.Min(0.838f, CS$<>8__locals1.y + 0.12f));
			this._leftEnd = 0f;
			int num31 = 0;
			if (CS$<>8__locals1.y >= num30 - 0.05f)
			{
				this.PageNote(CS$<>8__locals1.n, string.Format("history not drawn - no room (y {0:0.000}, bottom {1:0.000})", CS$<>8__locals1.y, num30));
			}
			if (CS$<>8__locals1.y < num30 - 0.05f)
			{
				Theme.Section("HISTORIAL", num4, CS$<>8__locals1.y, num4 + 0.4f, new ValueTuple<int, int, int>?(CS$<>8__locals1.acc));
				CS$<>8__locals1.y += 0.03f;
				int num32 = Math.Max(1, Math.Min((int)((num30 - CS$<>8__locals1.y) / 0.0275f), (UiDraw.TextSlotsLeft - 30) / 4));
				int num33 = Math.Max(1, Math.Min(num32, list5.Count));
				float num34 = Math.Max(0.0275f, Math.Min(0.04f, (num30 - CS$<>8__locals1.y) / (float)num33));
				float num35 = (num34 - 0.0275f) / 0.012499999f;
				float num36 = 0.205f + 0.03f * num35;
				float num37 = 0.235f + 0.04f * num35;
				this._historyScroll = Math.Max(0, Math.Min(this._historyScroll, Math.Max(0, list5.Count - 1)));
				float y = CS$<>8__locals1.y;
				if (list5.Count == 0)
				{
					UiDraw.Text(Pron.Her((num29 > 0) ? "Nada aún: tiene un pasado, pero no lo conoces. Enfréntate a él o hazlo hablar." : "Nada aún: lo que le hagas se escribirá aquí.", Models.Female(CS$<>8__locals1.n.ModelHash)), num4 + 0.006f, CS$<>8__locals1.y, 0.21f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
				}
				float num38 = num37 * 0.075f;
				float width = 0.312f;
				int num39 = this._historyScroll;
				while (num39 < list5.Count && num39 < this._historyScroll + num32)
				{
					HistoryEntry historyEntry = list5[num39];
					ValueTuple<int, int, int> color = historyEntry.Color;
					List<string> list6;
					if (UiDraw.TextSlotsLeft <= 80)
					{
						(list6 = new List<string>()).Add(RosterMenu.FitText(historyEntry.Text ?? "", num37, width, false));
					}
					else
					{
						list6 = RosterMenu.WrapFit(historyEntry.Text ?? "", num37, width, 2);
					}
					List<string> list7 = list6;
					float num40 = (list7.Count > 1) ? (num34 + num38) : num34;
					if (CS$<>8__locals1.y + num40 > num30 + 0.002f || UiDraw.TextSlotsLeft < 24)
					{
						if (num31 == 0)
						{
							this.PageNote(CS$<>8__locals1.n, "history rows not drawn - " + ((UiDraw.TextSlotsLeft < 24) ? string.Format("text budget ({0} slots left)", UiDraw.TextSlotsLeft) : "no room"));
							break;
						}
						break;
					}
					else
					{
						num31++;
						float num41 = CS$<>8__locals1.y + (num40 - 0.004f) / 2f;
						UiDraw.Rect(num4, CS$<>8__locals1.y + 0.003f, 0.0015625001f, num40 - 0.008f, color.Item1, color.Item2, color.Item3, 255);
						string text9 = historyEntry.When ?? "";
						int num42 = text9.IndexOf("  ", StringComparison.Ordinal);
						UiDraw.Text((num42 > 0) ? (text9.Substring(0, num42) + " " + text9.Substring(num42 + 2)) : text9, num4 + 0.007f, num41 - num36 * 0.0375f, num36, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
						float num43 = num41 - num37 * 0.0375f - (float)(list7.Count - 1) * num38 / 2f;
						for (int l = 0; l < list7.Count; l++)
						{
							UiDraw.Text(list7[l], num4 + 0.082f, num43 + (float)l * num38, num37, color.Item1, color.Item2, color.Item3, 255);
						}
						if (num39 == list5.Count - 1 && num29 > 0 && !this._mod.DebugOn && CS$<>8__locals1.y + num40 + num34 < num30)
						{
							UiDraw.Text(Pron.Her("... y más de su pasado que aún no conoces", Models.Female(CS$<>8__locals1.n.ModelHash)), num4 + 0.082f, CS$<>8__locals1.y + num40, 0.21f, Theme.Faint.Item1, Theme.Faint.Item2, Theme.Faint.Item3, 255);
						}
						CS$<>8__locals1.y += num40;
						num39++;
					}
				}
				Theme.ScrollBar(num4 + 0.4f + 0.004f, y, Math.Max(num34, CS$<>8__locals1.y - y), this._historyScroll, num32, list5.Count);
				if (list5.Count > 0)
				{
					this.PageNote(CS$<>8__locals1.n, string.Format("drew {0} of {1} history rows (scroll {2}, fit {3}, top {4:0.000}, bottom {5:0.000}, slots left {6}, texts cut last frame {7}, view {8})", new object[]
					{
						num31,
						list5.Count,
						this._historyScroll,
						num32,
						y,
						num30,
						UiDraw.TextSlotsLeft,
						UiDraw.LastFrameSkipped,
						this._mindView ? "mind" : (this._crimeView ? "crimes" : "history")
					}));
				}
			}
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x0011BFE4 File Offset: 0x0011A1E4
		private void PageNote(NemesisRecord n, string why)
		{
			if (n != null && n.Name != null && this._pageNoted.Add(n.Name + why.Split(new char[]
			{
				' '
			})[0]))
			{
				this._mod.MenuLog("PAGE " + n.Name + ": " + why);
			}
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x0011C048 File Offset: 0x0011A248
		private List<HistoryEntry> DetailLines(NemesisRecord n, out int pastCount, out int hidden)
		{
			RosterMenu.<>c__DisplayClass185_0 CS$<>8__locals1 = new RosterMenu.<>c__DisplayClass185_0();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.n = n;
			CS$<>8__locals1.fem = Models.Female(CS$<>8__locals1.n.ModelHash);
			List<HistoryEntry> list = (CS$<>8__locals1.n.History ?? new List<HistoryEntry>()).Where(delegate(HistoryEntry h)
			{
				if (CS$<>8__locals1.<>4__this._mod.DebugOn)
				{
					return true;
				}
				if (!CS$<>8__locals1.n.PresumedDead)
				{
					return !h.Cover;
				}
				return !h.Secret;
			}).Reverse<HistoryEntry>().Select(delegate(HistoryEntry h)
			{
				if (!CS$<>8__locals1.fem)
				{
					return h;
				}
				return new HistoryEntry
				{
					When = h.When,
					Text = Pron.Her(h.Text, true),
					Color = h.Color
				};
			}).ToList<HistoryEntry>();
			IEnumerable<PastEntry> source = CS$<>8__locals1.n.Past ?? new List<PastEntry>();
			Func<PastEntry, bool> predicate;
			if ((predicate = RosterMenu.<>O.<1>__PastShown) == null)
			{
				predicate = (RosterMenu.<>O.<1>__PastShown = new Func<PastEntry, bool>(NemesisMod.PastShown));
			}
			List<PastEntry> source2 = (from e in source.Where(predicate)
			orderby e.Year descending, e.Month descending
			select e).ToList<PastEntry>();
			hidden = source2.Count((PastEntry e) => !base.<DetailLines>g__Learned|4(e));
			List<PastEntry> list2 = (from e in source2
			where CS$<>8__locals1.<>4__this._mod.DebugOn || base.<DetailLines>g__Learned|4(e)
			select e).ToList<PastEntry>();
			pastCount = list2.Count;
			foreach (PastEntry pastEntry in list2)
			{
				list.Add(new HistoryEntry
				{
					When = pastEntry.WhenText,
					Text = Pron.Her(((!CS$<>8__locals1.<DetailLines>g__Learned|4(pastEntry)) ? "* " : "") + pastEntry.Text, CS$<>8__locals1.fem),
					Color = Backstory.KindColor(pastEntry.Kind)
				});
			}
			return list;
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0011C210 File Offset: 0x0011A410
		private void DrawMind(NemesisRecord n, float rx, float colW, float labelW, float line, ref float y, float bottom, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> acc, bool known)
		{
			RosterMenu.<>c__DisplayClass186_0 CS$<>8__locals1 = new RosterMenu.<>c__DisplayClass186_0();
			CS$<>8__locals1.rx = rx;
			CS$<>8__locals1.colW = colW;
			CS$<>8__locals1.acc = acc;
			CS$<>8__locals1.bottom = bottom;
			CS$<>8__locals1.n = n;
			CS$<>8__locals1.known = known;
			ValueTuple<int, int, int> labelColor = RosterMenu.LabelColor;
			int item = labelColor.Item1;
			int item2 = labelColor.Item2;
			int item3 = labelColor.Item3;
			CS$<>8__locals1.yy = y;
			CS$<>8__locals1.fem = Models.Female(CS$<>8__locals1.n.ModelHash);
			if (!CS$<>8__locals1.n.MindRolled)
			{
				CS$<>8__locals1.<DrawMind>g__Sec|0("MENTE");
				CS$<>8__locals1.<DrawMind>g__Text|1("Aún sin leer; dale un momento.", Theme.Dim, 0.22f);
				y = CS$<>8__locals1.yy;
				return;
			}
			CS$<>8__locals1.dbg = this._mod.DebugOn;
			CS$<>8__locals1.<DrawMind>g__Sec|0("MENTE");
			if (!CS$<>8__locals1.known)
			{
				CS$<>8__locals1.<DrawMind>g__Text|1(Pron.Her("Desconocido: enfréntate a él o hazlo hablar.", CS$<>8__locals1.fem), Theme.Dim, 0.22f);
			}
			else
			{
				CS$<>8__locals1.<DrawMind>g__Text|1(Mind.Describe(CS$<>8__locals1.n, 4), Theme.Title, 0.23f);
				int num = 0;
				while (num < Mind.FacetNames.Length && CS$<>8__locals1.yy < CS$<>8__locals1.bottom)
				{
					int num2 = 0;
					while (num2 < 2 && num + num2 < Mind.FacetNames.Length)
					{
						string text = Mind.FacetNames[num + num2];
						int num3 = Mind.F(CS$<>8__locals1.n, text);
						float num4 = CS$<>8__locals1.rx + 0.006f + (float)num2 * (CS$<>8__locals1.colW / 2f);
						float num5 = Theme.Icon(Theme.FacetIcon(text), num4, CS$<>8__locals1.yy - 0.002f, 0.02f, Theme.Dim, 255);
						UiDraw.TextFont(text.ToUpperInvariant(), UiDraw.TitleFont, num4 + num5 + 0.004f, CS$<>8__locals1.yy + 0.001f, 0.18f, item, item2, item3, 255, false);
						float num6 = num4 + 0.075f;
						float num7 = CS$<>8__locals1.colW / 2f - 0.11f;
						UiDraw.Rect(num6, CS$<>8__locals1.yy + 0.009f, num7, 0.0037037036f, Theme.CardHi.Item1, Theme.CardHi.Item2, Theme.CardHi.Item3, 230);
						ValueTuple<int, int, int> valueTuple = (num3 >= 75 || num3 <= 25) ? Theme.Warn : Theme.Dim;
						UiDraw.Rect(num6, CS$<>8__locals1.yy + 0.009f, num7 * (float)num3 / 100f, 0.0037037036f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
						UiDraw.Text(num3.ToString(), num6 + num7 + 0.005f, CS$<>8__locals1.yy + 0.001f, 0.19f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
						num2++;
					}
					CS$<>8__locals1.yy += 0.021f;
					num += 2;
				}
				CS$<>8__locals1.yy += 0.004f;
				CS$<>8__locals1.<DrawMind>g__Text|1("Valores:  " + string.Join("   ", from v in Mind.ValueNames
				select string.Format("{0} {1:+0;-0;0}", v.ToLowerInvariant(), Mind.V(CS$<>8__locals1.n, v))), Theme.Text, 0.21f);
			}
			CS$<>8__locals1.yy += 0.008f;
			CS$<>8__locals1.<DrawMind>g__Sec|0("NECESIDADES Y ESTRÉS");
			if (!CS$<>8__locals1.known && !CS$<>8__locals1.dbg)
			{
				CS$<>8__locals1.<DrawMind>g__Text|1("Desconocido.", Theme.Dim, 0.22f);
			}
			else
			{
				ValueTuple<int, int, int> valueTuple2 = (CS$<>8__locals1.n.Stress >= 70) ? Theme.Blood : ((CS$<>8__locals1.n.Stress >= 45) ? Theme.Warn : Theme.Good);
				CS$<>8__locals1.<DrawMind>g__Text|1(string.Format("Estrés {0}{1}", CS$<>8__locals1.n.Stress, (CS$<>8__locals1.n.Stress >= 85) ? " - a punto de estallar" : ((CS$<>8__locals1.n.Stress >= 70) ? " - al límite" : ((CS$<>8__locals1.n.Stress >= 45) ? " - tenso" : " - estable"))), valueTuple2, 0.22f);
				UiDraw.Rect(CS$<>8__locals1.rx + 0.006f, CS$<>8__locals1.yy - 0.004f, CS$<>8__locals1.colW - 0.03f, 0.0037037036f, Theme.CardHi.Item1, Theme.CardHi.Item2, Theme.CardHi.Item3, 230);
				UiDraw.Rect(CS$<>8__locals1.rx + 0.006f, CS$<>8__locals1.yy - 0.004f, (CS$<>8__locals1.colW - 0.03f) * (float)CS$<>8__locals1.n.Stress / 100f, 0.0037037036f, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255);
				CS$<>8__locals1.yy += 0.008f;
				CS$<>8__locals1.<DrawMind>g__Text|1("Necesidades:  " + string.Join("   ", from x in Mind.NeedNames
				select string.Format("{0} {1}{2}", x.ToLowerInvariant(), Mind.N(CS$<>8__locals1.n, x), (Mind.N(CS$<>8__locals1.n, x) >= 70) ? "!" : "")), Theme.Text, 0.21f);
			}
			CS$<>8__locals1.yy += 0.008f;
			CS$<>8__locals1.<DrawMind>g__Sec|0("LO QUE QUIERE");
			if (!(CS$<>8__locals1.n.WantsKnown | CS$<>8__locals1.dbg))
			{
				RosterMenu.<>c__DisplayClass186_0 CS$<>8__locals2 = CS$<>8__locals1;
				List<Goal> goals = CS$<>8__locals1.n.Goals;
				CS$<>8__locals2.<DrawMind>g__Text|1((goals != null && goals.Count > 0) ? Pron.Her("Quiere algo: hazlo hablar para averiguar qué.", CS$<>8__locals1.fem) : "Nada que tú sepas.", Theme.Dim, 0.22f);
			}
			else if (CS$<>8__locals1.n.Goals == null || CS$<>8__locals1.n.Goals.Count == 0)
			{
				CS$<>8__locals1.<DrawMind>g__Text|1("Nada en particular: solo ir tirando.", Theme.Dim, 0.22f);
			}
			else
			{
				foreach (Goal goal in CS$<>8__locals1.n.Goals)
				{
					CS$<>8__locals1.<DrawMind>g__Text|1(goal.Type.ToUpperInvariant() + "  -  " + goal.Text, (goal.Target == "you") ? Theme.Blood : Theme.Gold, 0.22f);
				}
			}
			CS$<>8__locals1.yy += 0.008f;
			CS$<>8__locals1.<DrawMind>g__Sec|0("QUIÉN ES PARA OTROS");
			List<Tie> list = (from t in CS$<>8__locals1.n.Ties ?? new List<Tie>()
			orderby Math.Abs(t.Strength) descending
			select t).ToList<Tie>();
			List<Tie> list2 = (from t in list
			where (CS$<>8__locals1.dbg | CS$<>8__locals1.known) || t.Type == "kin"
			select t).Take(5).ToList<Tie>();
			if (list2.Count == 0)
			{
				CS$<>8__locals1.<DrawMind>g__Text|1((list.Count > 0) ? "Desconocido." : "Nadie en tu lista.", Theme.Dim, 0.22f);
			}
			foreach (Tie tie in list2)
			{
				NemesisRecord nemesisRecord = this._mod.FindRecord(tie.Id);
				if (nemesisRecord != null)
				{
					ValueTuple<int, int, int> c = (tie.Strength >= 30) ? Theme.Good : ((tie.Strength <= -30) ? Theme.Blood : Theme.Text);
					CS$<>8__locals1.<DrawMind>g__Text|1(string.Format("{0}  {1}{2}  {3:+0;-0;0}{4}", new object[]
					{
						tie.Type,
						nemesisRecord.Name,
						nemesisRecord.IsAlive ? "" : " (muerto)",
						tie.Strength,
						string.IsNullOrEmpty(tie.Note) ? "" : ("  (" + tie.Note + ")")
					}), c, 0.22f);
				}
			}
			CS$<>8__locals1.yy += 0.008f;
			CS$<>8__locals1.<DrawMind>g__Sec|0("LO QUE RECUERDA");
			List<Memory> list3 = (from m in CS$<>8__locals1.n.Memories ?? new List<Memory>()
			where CS$<>8__locals1.dbg || !CS$<>8__locals1.n.PresumedDead || m.At < CS$<>8__locals1.n.PresumedDeadAt
			where (CS$<>8__locals1.dbg || m.Cause == "you") | CS$<>8__locals1.known
			orderby m.Intensity + (m.LongTerm ? 20 : 0) descending
			select m).ToList<Memory>();
			if (list3.Count == 0)
			{
				CS$<>8__locals1.<DrawMind>g__Text|1("Nada que tú sepas.", Theme.Dim, 0.22f);
			}
			foreach (Memory memory in list3)
			{
				if (CS$<>8__locals1.yy > CS$<>8__locals1.bottom)
				{
					break;
				}
				CS$<>8__locals1.<DrawMind>g__Text|1(string.Format("{0} {1}{2}:  {3}{4}", new object[]
				{
					Mind.Felt(memory.Emotion),
					memory.Intensity,
					memory.LongTerm ? " - para siempre" : "",
					memory.Text,
					(memory.Cause == "you") ? "  (tú)" : ""
				}), Mind.EmotionColor(memory.Emotion), 0.22f);
			}
			y = CS$<>8__locals1.yy;
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x0011CBB4 File Offset: 0x0011ADB4
		private void DrawHeader(string title, string subtitle, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> accent)
		{
			Theme.Header(title, subtitle, this._worldPage ? MenuTab.World : MenuTab.Hierarchy, new ValueTuple<int, int, int>?(accent));
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x0011CBD0 File Offset: 0x0011ADD0
		private WorldEvent SelectedEvent()
		{
			List<WorldEvent> list = this._mod.ChronicleFor(this._worldFilter);
			if (list.Count == 0)
			{
				return null;
			}
			this._worldIndex = Math.Max(0, Math.Min(this._worldIndex, list.Count - 1));
			return list[this._worldIndex];
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x0011CC24 File Offset: 0x0011AE24
		private List<NemesisRecord> WorldPeople(WorldEvent ev)
		{
			if (ev != null)
			{
				return (from p in ev.People
				select this._mod.Roster.FirstOrDefault((NemesisRecord r) => r.Id == p.Id) into r
				where r != null
				select r).Distinct<NemesisRecord>().ToList<NemesisRecord>();
			}
			return new List<NemesisRecord>();
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x0011CC80 File Offset: 0x0011AE80
		private bool HandleWorldKey(Keys key)
		{
			if (this._worldFilter == this.WarsFilter && this.HandleWarsKey(key))
			{
				return true;
			}
			List<WorldEvent> list = this._mod.ChronicleFor(this._worldFilter);
			int val = Math.Max(0, list.Count - 1);
			List<NemesisRecord> list2 = this.WorldPeople(this.SelectedEvent());
			if (key == Keys.Up || key == Keys.Down || key == Keys.Prior || key == Keys.Next || key == Keys.Q || key == Keys.E)
			{
				this._worldMouseTop = -1;
			}
			if (key <= Keys.C)
			{
				if (key <= Keys.Return)
				{
					if (key != Keys.Back)
					{
						if (key == Keys.Return)
						{
							if (this._worldPerson < 0)
							{
								if (list2.Count > 0)
								{
									this._worldPerson = 0;
									return true;
								}
								return true;
							}
							else
							{
								if (this._worldPerson < list2.Count)
								{
									this._detail = list2[this._worldPerson];
									this._action = 0;
									this._confirmForget = false;
									this._historyScroll = 0;
									this._crimeScroll = 0;
									return true;
								}
								return true;
							}
						}
					}
					else
					{
						if (this._worldPerson >= 0)
						{
							this._worldPerson = -1;
							return true;
						}
						this.Toggle();
						return true;
					}
				}
				else
				{
					switch (key)
					{
					case Keys.Prior:
						this._worldIndex = Math.Max(0, this._worldIndex - RosterMenu.WorldRows);
						this._worldPerson = -1;
						return true;
					case Keys.Next:
						this._worldIndex = Math.Min(val, this._worldIndex + RosterMenu.WorldRows);
						this._worldPerson = -1;
						return true;
					case Keys.End:
					case Keys.Home:
						break;
					case Keys.Left:
						this._worldPerson = -1;
						return true;
					case Keys.Up:
						if (this._worldPerson >= 0)
						{
							this._worldPerson = Math.Max(0, this._worldPerson - 1);
							return true;
						}
						this._worldIndex = Math.Max(0, this._worldIndex - 1);
						return true;
					case Keys.Right:
						if (this._worldPerson < 0 && list2.Count > 0)
						{
							this._worldPerson = 0;
							return true;
						}
						return true;
					case Keys.Down:
						if (this._worldPerson >= 0)
						{
							this._worldPerson = Math.Min(Math.Max(0, list2.Count - 1), this._worldPerson + 1);
							return true;
						}
						this._worldIndex = Math.Min(val, this._worldIndex + 1);
						return true;
					default:
						if (key == Keys.C)
						{
							this._mod.CycleNameCards();
							return true;
						}
						break;
					}
				}
			}
			else
			{
				if (key <= Keys.R)
				{
					if (key != Keys.E)
					{
						switch (key)
						{
						case Keys.K:
							if (this._mod.DebugOn)
							{
								this._mod.CycleMarkerLook();
								return true;
							}
							return true;
						case Keys.L:
						case Keys.O:
							return false;
						case Keys.M:
						case Keys.P:
						case Keys.R:
							this._mod.ToggleMark(this.SelectedEvent());
							return true;
						case Keys.N:
							if (this._mod.DebugOn && XPad.RecentlyUsed)
							{
								this._resetOpen = true;
								this._resetSel = 0;
								this._resetConfirm = false;
								return true;
							}
							this._mod.CycleWorldNews();
							return true;
						case Keys.Q:
							break;
						default:
							return false;
						}
					}
					int num = Gangs.All.Length + 4;
					this._worldFilter = this.FilterOf((this.DispOf(this._worldFilter) + ((key == Keys.E) ? 1 : (num - 1))) % num);
					this._worldIndex = 0;
					this._worldPerson = -1;
					return true;
				}
				if (key != Keys.Z)
				{
					if (key == Keys.Subtract || key == Keys.OemMinus)
					{
						if (this._mod.DebugOn)
						{
							this._mod.SimulateWorldEventNow();
							this._worldIndex = 0;
							this._worldPerson = -1;
							return true;
						}
						return true;
					}
				}
				else
				{
					if (this._mod.DebugOn)
					{
						this._resetOpen = true;
						this._resetSel = 0;
						this._resetConfirm = false;
						return true;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001237 RID: 4663 RVA: 0x0011D039 File Offset: 0x0011B239
		private string ResetFactionName
		{
			get
			{
				if (this._resetFaction >= Gangs.All.Length)
				{
					return "Sin banda";
				}
				return Gangs.All[this._resetFaction].Name;
			}
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x0011D064 File Offset: 0x0011B264
		private bool HandleResetKey(Keys key)
		{
			if (key <= Keys.Down)
			{
				if (key == Keys.Back)
				{
					goto IL_1F2;
				}
				if (key != Keys.Return)
				{
					switch (key)
					{
					case Keys.Left:
					case Keys.Right:
						break;
					case Keys.Up:
						this._resetSel = (this._resetSel + RosterMenu.ResetRows.Length - 1) % RosterMenu.ResetRows.Length;
						this._resetConfirm = false;
						return true;
					case Keys.Down:
						this._resetSel = (this._resetSel + 1) % RosterMenu.ResetRows.Length;
						this._resetConfirm = false;
						return true;
					default:
						return true;
					}
				}
				else
				{
					if (this._resetSel == RosterMenu.ResetRows.Length - 1)
					{
						this._resetOpen = false;
						return true;
					}
					if (!this._resetConfirm)
					{
						this._resetConfirm = true;
						return true;
					}
					this._resetConfirm = false;
					this._resetOpen = false;
					if (this._resetSel == 0)
					{
						this._mod.DebugReset("all", null);
					}
					else if (this._resetSel == 1)
					{
						this._mod.DebugReset("xray", null);
					}
					else if (this._resetSel == 2)
					{
						this._mod.DebugReset("world", null);
					}
					else if (this._resetSel == 4)
					{
						this._mod.DebugReset("loners", null);
					}
					else if (this._resetSel == 5)
					{
						this._mod.DebugReset("safecamps", null);
					}
					else
					{
						this._mod.DebugReset("faction", (this._resetFaction < Gangs.All.Length) ? Gangs.All[this._resetFaction] : null);
					}
					this._worldIndex = 0;
					this._worldPerson = -1;
					this._row = -1;
					this._listIndex = 0;
					return true;
				}
			}
			else if (key != Keys.E && key != Keys.Q)
			{
				if (key != Keys.Z)
				{
					return true;
				}
				goto IL_1F2;
			}
			if (this._resetSel == 3)
			{
				int num = Gangs.All.Length + 1;
				this._resetFaction = (this._resetFaction + ((key == Keys.Right || key == Keys.E) ? 1 : (num - 1))) % num;
				this._resetConfirm = false;
				return true;
			}
			return true;
			IL_1F2:
			this._resetOpen = false;
			return true;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0011D270 File Offset: 0x0011B470
		private void DrawResetPanel()
		{
			float num = 0.075f + (float)RosterMenu.ResetRows.Length * 0.04f + 0.05f;
			float num2 = 0.3f;
			float num3 = 0.5f - num / 2f;
			UiDraw.Rect(num2, num3, 0.4f, num, 12, 11, 10, 248);
			Theme.Hairline(num2, num3, 0.4f, num, Theme.Blood.Item1, Theme.Blood.Item2, Theme.Blood.Item3, 255, 2);
			UiDraw.TextFont("DEBUG  -  RESET", UiDraw.TitleFont, num2 + 0.016f, num3 + 0.012f, 0.34f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			UiDraw.Text("Rosters are backed up first (nemeses.before-*.json).", num2 + 0.016f, num3 + 0.046f, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			float num4 = num3 + 0.075f;
			for (int i = 0; i < RosterMenu.ResetRows.Length; i++)
			{
				bool flag = i == this._resetSel;
				string text = (i == 3) ? ("Reset one faction:   " + this.ResetFactionName + "   (Left / Right)") : RosterMenu.ResetRows[i];
				if (flag && this._resetConfirm)
				{
					text = ((i == 3) ? ("Reset " + this.ResetFactionName) : RosterMenu.ResetRows[i]) + "  -  press again to confirm";
				}
				if (flag)
				{
					ValueTuple<int, int, int> valueTuple = this._resetConfirm ? Theme.Blood : Theme.Selected;
					UiDraw.Rect(num2 + 0.01f, num4, 0.38f, 0.033999998f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 245);
				}
				ValueTuple<int, int, int> valueTuple2 = flag ? (this._resetConfirm ? Theme.Title : Theme.SelectedText) : ((i == 0) ? Theme.Blood : Theme.Text);
				UiDraw.TextFont(text, UiDraw.TitleFont, num2 + 0.02f, num4 + 0.007f, 0.25f, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255, false);
				num4 += 0.04f;
			}
			UiDraw.Text(RosterMenu.UsingPad ? "D-pad  Navigate     Left/Right  Faction     A  Select (twice)     B  Close" : "Up/Down  Navigate     Left/Right  Faction     Enter  Select (twice)     Backspace  Close", num2 + 0.016f, num3 + num - 0.034f, 0.19f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0011D50B File Offset: 0x0011B70B
		private static int WorldRows
		{
			get
			{
				return 20;
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x0011D50F File Offset: 0x0011B70F
		private static string OutcomeLabel(WorldPart p)
		{
			if (!string.IsNullOrEmpty(p.Outcome))
			{
				return p.Outcome;
			}
			return "";
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x0011D52C File Offset: 0x0011B72C
		[return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static ValueTuple<int, int, int> OutcomeColor(string o)
		{
			if (o == "KILLED" || o == "TE MATÓ" || o == "NEMESIS")
			{
				return Theme.Blood;
			}
			if (o == "HERIDO" || o == "TALKED" || o == "CAMP LOST" || o == "GIVEN UP" || o == "KNOCKED OUT" || o == "ROBADO" || o == "HOGTIED")
			{
				return Theme.Warn;
			}
			if (o == "SURRENDERED")
			{
				return Theme.Good;
			}
			if (o == "DISTINGUIDO" || o == "GANÓ" || o == "PROMOTED" || o == "JOINED")
			{
				return Theme.Good;
			}
			if (!(o == "DE DUELO"))
			{
				return Theme.Dim;
			}
			return new ValueTuple<int, int, int>(150, 170, 210);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x0011D658 File Offset: 0x0011B858
		[return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static ValueTuple<int, int, int> EffectColor(string fx)
		{
			ValueTuple<int, int, int> valueTuple = NemesisMod.MoodColor(fx.Split(new char[]
			{
				' '
			})[0]);
			ValueTuple<int, int, int> valueTuple2 = valueTuple;
			ValueTuple<int, int, int> dim = Theme.Dim;
			if (valueTuple2.Item1 != dim.Item1 || valueTuple2.Item2 != dim.Item2 || valueTuple2.Item3 != dim.Item3)
			{
				return valueTuple;
			}
			if (fx.StartsWith("scar") || fx.StartsWith("limp"))
			{
				return Theme.Blood;
			}
			if (fx.StartsWith("fear"))
			{
				return new ValueTuple<int, int, int>(150, 170, 210);
			}
			if (fx.StartsWith("hate"))
			{
				return Theme.Warn;
			}
			if (fx.StartsWith("laid up"))
			{
				return new ValueTuple<int, int, int>(150, 170, 110);
			}
			if (fx.StartsWith("rank") || fx.StartsWith("married") || fx.Contains("child") || fx.StartsWith("friends with"))
			{
				return Theme.Good;
			}
			return Theme.Gold;
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x0011D768 File Offset: 0x0011B968
		private void DrawWorld()
		{
			RosterMenu.<>c__DisplayClass201_0 CS$<>8__locals1 = new RosterMenu.<>c__DisplayClass201_0();
			CS$<>8__locals1.<>4__this = this;
			List<WorldEvent> list = this._mod.ChronicleFor(this._worldFilter);
			WorldEvent worldEvent = this.SelectedEvent();
			CS$<>8__locals1.people = this.WorldPeople(worldEvent);
			if (this._worldPerson >= CS$<>8__locals1.people.Count)
			{
				this._worldPerson = CS$<>8__locals1.people.Count - 1;
			}
			this.DrawHeader("EL MUNDO", "Lo que ocurre mientras no estás", Theme.Gold);
			List<string> list2 = new List<string>();
			list2.Add("Todo");
			list2.Add("Guerras");
			List<string> names = list2.Concat(from g in Gangs.All
			select g.Name).Concat(new string[]
			{
				"Van der Linde",
				"Sin banda"
			}).ToList<string>();
			ValueTuple<int, int, int> accent = (this._worldFilter == 0) ? Theme.Gold : ((this._worldFilter <= Gangs.All.Length) ? Gangs.All[this._worldFilter - 1].Accent : ((this._worldFilter == Gangs.All.Length + 1) ? NemesisMod.VdlAccent : ((this._worldFilter == this.WarsFilter) ? Theme.NativeRed : RosterMenu.UnaffiliatedGrey)));
			int i;
			Theme.FactionBar(0.13499999f, names, this.DispOf(this._worldFilter), true, accent, null, GameDate.NowText(), true, delegate(int i)
			{
				int num52 = CS$<>8__locals1.<>4__this.FilterOf(i);
				if (num52 == CS$<>8__locals1.<>4__this._worldFilter)
				{
					return;
				}
				NemesisMod.UiSound((i > CS$<>8__locals1.<>4__this.DispOf(CS$<>8__locals1.<>4__this._worldFilter)) ? "BUMPER_RIGHT" : "BUMPER_LEFT");
				CS$<>8__locals1.<>4__this._worldFilter = num52;
				CS$<>8__locals1.<>4__this._worldIndex = 0;
				CS$<>8__locals1.<>4__this._worldPerson = -1;
				CS$<>8__locals1.<>4__this._worldMouseTop = -1;
			});
			float num = 0.17499998f;
			List<ValueTuple<string, Gang, Gang>> list3 = this._mod.WarsNow();
			if (list3.Count > 0)
			{
				string text = "EN GUERRA";
				string text2 = "   -   ";
				int num2 = 0;
				foreach (ValueTuple<string, Gang, Gang> valueTuple in list3)
				{
					string str = string.Concat(new string[]
					{
						valueTuple.Item1,
						": ",
						RosterMenu.<DrawWorld>g__Short|201_8(valueTuple.Item2),
						" vs ",
						RosterMenu.<DrawWorld>g__Short|201_8(valueTuple.Item3)
					});
					string text3 = text + ((num2 == 0) ? text2 : "     ") + str;
					string str2 = (list3.Count - num2 - 1 > 0) ? string.Format("     +{0} more", list3.Count - num2 - 1) : "";
					if (Theme.MixedWidth(text3 + str2, 0.21f, 0.0222f) > 0.82f && num2 > 0)
					{
						text += string.Format("     +{0} more", list3.Count - num2);
						break;
					}
					text = text3;
					num2++;
				}
				Theme.NoteBand(num, text, new ValueTuple<int, int, int>?(Theme.Warn));
				num += 0.032f;
			}
			Theme.PanelRect(0.07f, num, 0.86f, 0.868f - num, 215);
			if (this._worldFilter == this.WarsFilter)
			{
				string str3 = NemesisSettings.WorldNewsShort[this._mod.Config.WorldNews];
				RosterMenu.DrawFooter(0.868f, RosterMenu.UsingPad ? ("Cruceta  Navegar     LT/RT  Eventos     LB/RB  Filtrar     LS  Noticias: " + str3 + "     B  Cerrar") : ("Arriba/Abajo  Navegar     RePág/AvPág  Eventos     Q/E  Filtrar     N  Noticias: " + str3 + "     Retroceso  Cerrar"));
				this.DrawWarsPage(num);
				return;
			}
			string text4 = (worldEvent != null && worldEvent.HasPos) ? (worldEvent.Marked ? "Desmarcar" : "Marcar en el mapa") : null;
			string text5 = NemesisMod.NameCardModes[this._mod.Config.NameCards];
			string text6 = NemesisSettings.WorldNewsShort[this._mod.Config.WorldNews];
			string str4 = string.Concat(new string[]
			{
				"     X  ",
				NemesisMod.YouShort,
				(text4 != null) ? ("     Y  " + text4) : "",
				this._mod.DebugOn ? "" : ("     LS  Noticias: " + text6),
				"     RS  Tarjetas de nombre: ",
				text5,
				this._mod.DebugOn ? "     LT  Evento ahora     LS  Reiniciar" : ""
			});
			string str5 = string.Concat(new string[]
			{
				"     A  ",
				NemesisMod.YouShort,
				(text4 != null) ? ("     P  " + text4) : "",
				"     N  Noticias: ",
				text6,
				"     C  Tarjetas de nombre: ",
				text5,
				this._mod.DebugOn ? "     -  Evento ahora     Z  Reiniciar" : ""
			});
			RosterMenu.DrawFooter(0.868f, RosterMenu.UsingPad ? ((this._worldPerson >= 0) ? "Cruceta  Navegar     A  Ficha     B  Atrás" : ("Cruceta  Navegar     A  La gente     LB/RB  Filtrar" + str4 + "     B  Cerrar")) : ((this._worldPerson >= 0) ? "Arriba/Abajo  Navegar     Enter  Ficha     Retroceso  Atrás" : ("Arriba/Abajo  Navegar     Enter  La gente     Q/E  Filtrar" + str5 + "     Retroceso  Cerrar")));
			float num3 = 0.084f;
			float num4 = 0.455f;
			float num5 = num + 0.012f;
			float num6 = num5;
			float num7 = 0.85599995f;
			int num8 = Math.Max(4, Math.Min((int)((num7 - num6 - 0f) / 0.036f), (UiDraw.TextSlotsLeft - 70) / 4));
			float num9 = num7 - num6 - 0f;
			float num10 = Math.Max(0.032f, Math.Min(0.05f, num9 / (float)Math.Max(1, num8)));
			if (list.Count < num8)
			{
				num10 = Math.Max(0.032f, Math.Min(0.044f, num9 / (float)Math.Max(8, list.Count)));
			}
			float num11 = (num10 - 0.032f) / 0.018f;
			int num12 = Math.Min(num8, list.Count);
			float num13 = num6 + 0f + ((list.Count < num8) ? 0f : Math.Max(0f, (num9 - (float)num12 * num10) / 2f));
			float num14 = 0.19f + 0.035f * num11;
			float num15 = 0.22f + 0.05f * num11;
			if (list.Count == 0)
			{
				UiDraw.Text(this._mod.Config.WorldEvents ? "Nothing yet - the world moves while you ride." : ("World events are switched off in the settings (" + KeyCombo.SettingsText + " - World - Events)."), num3 + 0.006f, num5 + 0.006f, 0.23f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
				return;
			}
			int num16 = Math.Max(0, Math.Min(this._worldIndex - num8 / 2, list.Count - num8));
			if (this._worldMouseTop >= 0 && this._worldIndex >= this._worldMouseTop && this._worldIndex < this._worldMouseTop + num8)
			{
				num16 = Math.Max(0, Math.Min(this._worldMouseTop, Math.Max(0, list.Count - num8)));
			}
			else
			{
				this._worldMouseTop = -1;
			}
			Theme.ScrollBar(num3 + num4 + 0.004f, num13, (float)num12 * num10, num16, num8, list.Count);
			int num17 = num16;
			while (num17 < list.Count && num17 < num16 + num8)
			{
				WorldEvent worldEvent2 = list[num17];
				float num18 = num13 + (float)(num17 - num16) * num10;
				bool flag = num17 == this._worldIndex;
				ValueTuple<int, int, int> valueTuple2 = NemesisMod.KindColor(worldEvent2.Kind);
				int wi = num17;
				int wtop = num16;
				UiMouse.Region(num3 - 0.0031250003f, num18, num4 + 0.0031250003f, num10 - 0.004f, delegate
				{
					if (CS$<>8__locals1.<>4__this._worldIndex == wi)
					{
						return false;
					}
					CS$<>8__locals1.<>4__this._worldIndex = wi;
					CS$<>8__locals1.<>4__this._worldPerson = -1;
					CS$<>8__locals1.<>4__this._worldMouseTop = wtop;
					return true;
				}, delegate
				{
					UiMouse.Send(Keys.Return);
				});
				if (flag && this._worldPerson < 0)
				{
					Theme.SelectFill(num3 - 0.0031250003f, num18, num4 + 0.0031250003f, num10 - 0.004f, 235);
				}
				else if (flag)
				{
					UiDraw.Rect(num3, num18, num4, num10 - 0.004f, Theme.CardHi.Item1, Theme.CardHi.Item2, Theme.CardHi.Item3, 240);
				}
				UiDraw.Rect(num3, num18, 0.0015625001f, num10 - 0.004f, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255);
				if (flag && this._worldPerson < 0)
				{
					Theme.RedFrame(num3 - 0.0031250003f, num18, num4 + 0.0031250003f, num10 - 0.004f, 1f, 0);
				}
				float num19 = num18 + (num10 - 0.004f) / 2f;
				ValueTuple<int, int, int> valueTuple3 = (flag && this._worldPerson < 0) ? Theme.SelectedText : Theme.Text;
				ValueTuple<int, int, int> valueTuple4 = (flag && this._worldPerson < 0) ? Theme.SelectedText : Theme.Dim;
				UiDraw.Text((worldEvent2.When ?? "").Replace("  ", " "), num3 + 0.01f, num19 - num14 * 0.0375f, num14, valueTuple4.Item1, valueTuple4.Item2, valueTuple4.Item3, 255);
				int num20 = worldEvent2.People.Count((WorldPart p) => p.Outcome == "KILLED");
				string text7 = worldEvent2.Title + ((num20 > 0) ? string.Format("  -  {0} dead", num20) : "");
				float num21 = num3 + 0.092f + 0.016f * num11;
				float num22 = num3 + num4 - 0.008f - num21;
				float num23 = num15;
				while (num23 > num15 * 0.85f && Theme.MixedWidth(text7, num23, 0.0222f) > num22)
				{
					num23 -= 0.005f;
				}
				float num24 = Math.Min(num15 * 0.85f, (num10 - 0.006f) / 0.15f);
				if (Theme.MixedWidth(text7, num23, 0.0222f) <= num22)
				{
					UiDraw.Text(text7, num21, num19 - num23 * 0.0375f, num23, valueTuple3.Item1, valueTuple3.Item2, valueTuple3.Item3, 255);
				}
				else if (num24 >= 0.165f)
				{
					List<string> list4 = RosterMenu.WrapFit(text7, num24, num22, 2);
					float num25 = num24 * 0.075f;
					float num26 = num19 - num24 * 0.0375f - (float)(list4.Count - 1) * num25 / 2f;
					for (int m = 0; m < list4.Count; m++)
					{
						UiDraw.Text(list4[m], num21, num26 + (float)m * num25, num24, valueTuple3.Item1, valueTuple3.Item2, valueTuple3.Item3, 255);
					}
				}
				else
				{
					UiDraw.Text(RosterMenu.FitText(text7, num23, num22, false), num21, num19 - num23 * 0.0375f, num23, valueTuple3.Item1, valueTuple3.Item2, valueTuple3.Item3, 255);
				}
				num17++;
			}
			if (worldEvent == null)
			{
				return;
			}
			float num27 = 0.56f;
			CS$<>8__locals1.rw = 0.356f;
			CS$<>8__locals1.ry = num5;
			ValueTuple<int, int, int> valueTuple5 = NemesisMod.KindColor(worldEvent.Kind);
			CS$<>8__locals1.mx = num27 + CS$<>8__locals1.rw / 2f;
			long num28 = this._mod.GameMinutesNow();
			int num29 = Math.Min(2, RosterMenu.Wrap(worldEvent.Title.ToUpperInvariant(), 0.3f, CS$<>8__locals1.rw * 0.92f).Count);
			int num30 = ((worldEvent.UnnamedDead.Count > 0) + (worldEvent.Marked > false) + (worldEvent.AftermathUntil > 0L)) ? 1 : 0;
			List<List<string>> source = (from l in worldEvent.Lines.Take(4)
			select RosterMenu.WrapFit(l, 0.24f, CS$<>8__locals1.rw * 0.94f, 2)).ToList<List<string>>();
			int num31 = Math.Min(5, source.Sum((List<string> l) => l.Count));
			float num32 = 0.066f + (float)num29 * 0.031f + 0.018f + (float)num31 * 0.026f + (float)num30 * 0.023f + 0.01f;
			CS$<>8__locals1.ry = num13 + 0.01f;
			float num33 = 0.85599995f - (CS$<>8__locals1.ry - 0.01f);
			WorldEvent item = worldEvent;
			int? num34;
			if (worldEvent == null)
			{
				num34 = null;
			}
			else
			{
				List<WorldPart> people = worldEvent.People;
				num34 = ((people != null) ? new int?(people.Count) : null);
			}
			int? num35 = num34;
			ValueTuple<WorldEvent, int, bool, long> storyKey = new ValueTuple<WorldEvent, int, bool, long>(item, num35.GetValueOrDefault(), this._mod.DebugOn, (worldEvent != null) ? worldEvent.AftermathUntil : 0L);
			if (this._storyFor != worldEvent || !storyKey.Equals(this._storyKey))
			{
				this._storyFor = worldEvent;
				this._storyKey = storyKey;
				this._story = RosterMenu.WrapBody(string.Join(" ", EventStory.Build(worldEvent, new Func<string, NemesisRecord>(this._mod.FindRecord), num28, this._mod.DebugOn)), 0.225f, CS$<>8__locals1.rw - 0.04f);
			}
			float num36 = (worldEvent.People.Count > 0) ? (0.044f + (float)Math.Min(2, worldEvent.People.Count) * 0.062f) : 0f;
			int num37 = Math.Max(0, Math.Min(this._story.Count, (int)((num33 - num32 - 0.03f - num36 - 0.02f) / 0.0235f)));
			num37 = Math.Min(num37, Math.Max(0, (UiDraw.TextSlotsLeft - 60) / 2));
			if (num37 > 0)
			{
				num32 += 0.012f + (float)num37 * 0.0235f + 0.006f;
			}
			if (worldEvent.People.Count > 0)
			{
				float num38 = 0.044f;
				foreach (WorldPart worldPart in worldEvent.People)
				{
					float num39 = 0.052f;
					List<string> effects = worldPart.Effects;
					float num40 = num39 + ((((effects != null) ? effects.Count : 0) > 0) ? 0.024f : 0f) + 0.008f;
					if (num32 + num38 + num40 > num33 - 0.03f)
					{
						num38 += 0.024f;
						break;
					}
					num38 += num40;
				}
			}
			float y = CS$<>8__locals1.ry - 0.01f;
			UiDraw.Rect(num27, y, CS$<>8__locals1.rw, num32 + 0.01f, 22, 21, 19, 200);
			Theme.Hairline(num27, y, CS$<>8__locals1.rw, num32 + 0.01f, Theme.Border.Item1, Theme.Border.Item2, Theme.Border.Item3, 140, 1);
			bool flag2 = !string.IsNullOrEmpty(worldEvent.Place) && (worldEvent.Title ?? "").IndexOf(worldEvent.Place, StringComparison.OrdinalIgnoreCase) >= 0;
			string s = (worldEvent.When ?? "").Replace("  ", " ") + ((string.IsNullOrEmpty(worldEvent.Place) || flag2) ? "" : ("  -  " + worldEvent.Place));
			string text8 = NemesisMod.KindLabel(worldEvent.Kind);
			float num41 = Theme.CapsWidth(text8, 0.21f) + 0.014f;
			Theme.Chip(text8, CS$<>8__locals1.mx - num41 / 2f, CS$<>8__locals1.ry, valueTuple5, true);
			CS$<>8__locals1.ry += 0.028f;
			CS$<>8__locals1.<DrawWorld>g__Centre|4(s, 0.21f, Theme.Dim, false);
			CS$<>8__locals1.ry += 0.038f;
			foreach (string s2 in RosterMenu.Wrap(worldEvent.Title.ToUpperInvariant(), 0.3f, CS$<>8__locals1.rw * 0.92f).Take(2))
			{
				CS$<>8__locals1.<DrawWorld>g__Centre|4(s2, 0.3f, Theme.Title, true);
				CS$<>8__locals1.ry += 0.031f;
			}
			CS$<>8__locals1.ry += 0.004f;
			Theme.CenterRule(num27, CS$<>8__locals1.ry, CS$<>8__locals1.rw, 0.06f, 130);
			CS$<>8__locals1.ry += 0.014f;
			List<string> list5 = source.SelectMany((List<string> l) => l).ToList<string>();
			for (int j = 0; j < num31; j++)
			{
				bool flag3 = j == num31 - 1 && list5.Count > num31;
				CS$<>8__locals1.<DrawWorld>g__Centre|4(flag3 ? RosterMenu.FitText(list5[j] + " ...", 0.24f, CS$<>8__locals1.rw * 0.94f, false) : list5[j], 0.24f, Theme.Text, false);
				CS$<>8__locals1.ry += 0.026f;
			}
			if (num37 > 0)
			{
				CS$<>8__locals1.ry += 0.004f;
				UiDraw.Rect(num27 + 0.02f, CS$<>8__locals1.ry, CS$<>8__locals1.rw - 0.04f, 0.0009259259f, Theme.Border.Item1, Theme.Border.Item2, Theme.Border.Item3, 160);
				CS$<>8__locals1.ry += 0.008f;
				for (int k = 0; k < num37; k++)
				{
					string text9 = this._story[k];
					if (k == num37 - 1 && num37 < this._story.Count)
					{
						text9 = text9.TrimEnd(new char[]
						{
							'.',
							',',
							' '
						}) + " ...";
					}
					UiDraw.Text(text9, num27 + 0.02f, CS$<>8__locals1.ry, 0.225f, 214, 208, 194, 255);
					CS$<>8__locals1.ry += 0.0235f;
				}
				CS$<>8__locals1.ry += 0.006f;
			}
			if (worldEvent.UnnamedDead.Count > 0)
			{
				string str6 = string.Join(",  ", worldEvent.UnnamedDead.Select(delegate(KeyValuePair<string, int> kv)
				{
					string format = "{0} {1}";
					Gang gang = Gangs.ById(kv.Key);
					return string.Format(format, ((gang != null) ? gang.Name : null) ?? kv.Key, kv.Value);
				}));
				CS$<>8__locals1.<DrawWorld>g__Centre|4(RosterMenu.FitText("Unnamed dead:  " + str6, 0.21f, CS$<>8__locals1.rw * 0.94f, false), 0.21f, Theme.Dim, false);
				CS$<>8__locals1.ry += 0.023f;
			}
			long num42 = num28;
			if (worldEvent.Marked)
			{
				CS$<>8__locals1.<DrawWorld>g__Centre|4("Marked on your map.", 0.21f, Theme.Gold, false);
				CS$<>8__locals1.ry += 0.023f;
			}
			if (worldEvent.AftermathUntil > 0L)
			{
				string s3 = worldEvent.Visited ? "Has visto las secuelas." : ((worldEvent.AftermathUntil > num42) ? "Los cuerpos siguen allí; márcalo para encontrarlo (sale un ? al acercarte)." : "Los cuerpos ya han desaparecido.");
				ValueTuple<int, int, int> c = (!worldEvent.Visited && worldEvent.AftermathUntil > num42) ? Theme.Warn : Theme.Faint;
				CS$<>8__locals1.<DrawWorld>g__Centre|4(RosterMenu.FitText(s3, 0.21f, CS$<>8__locals1.rw * 0.94f, false), 0.21f, c, false);
				CS$<>8__locals1.ry += 0.023f;
			}
			CS$<>8__locals1.ry += 0.024f;
			if (worldEvent.People.Count == 0)
			{
				return;
			}
			Theme.Section("PERSONAS", num27, CS$<>8__locals1.ry, num27 + CS$<>8__locals1.rw, new ValueTuple<int, int, int>?(valueTuple5));
			CS$<>8__locals1.ry += 0.03f;
			float num43 = 0.83399993f;
			if (this._peopleFor != worldEvent)
			{
				this._peopleFor = worldEvent;
				this._peopleStart = 0;
			}
			int num44 = (this._worldPerson >= 0 && this._worldPerson < CS$<>8__locals1.people.Count) ? worldEvent.People.FindIndex((WorldPart wp) => wp.Id == CS$<>8__locals1.people[CS$<>8__locals1.<>4__this._worldPerson].Id) : -1;
			if (num44 >= 0)
			{
				if (num44 < this._peopleStart)
				{
					this._peopleStart = num44;
				}
				while (this._peopleStart < num44)
				{
					if (CS$<>8__locals1.ry + 0.022f + worldEvent.People.Skip(this._peopleStart).Take(num44 - this._peopleStart + 1).Sum(new Func<WorldPart, float>(RosterMenu.<DrawWorld>g__CardH|201_6)) <= num43)
					{
						break;
					}
					this._peopleStart++;
				}
			}
			else
			{
				this._peopleStart = 0;
			}
			int num45 = 0;
			float ry = CS$<>8__locals1.ry;
			using (IEnumerator<WorldPart> enumerator4 = worldEvent.People.Skip(this._peopleStart).GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					WorldPart p = enumerator4.Current;
					List<string> list6 = p.Effects ?? new List<string>();
					float num46 = 0.052f + ((list6.Count > 0) ? 0.024f : 0f);
					if (CS$<>8__locals1.ry + num46 > num43)
					{
						break;
					}
					if (UiDraw.TextSlotsLeft < 18)
					{
						break;
					}
					num45++;
					NemesisRecord nemesisRecord = this._mod.Roster.FirstOrDefault((NemesisRecord r) => r.Id == p.Id);
					bool selected = nemesisRecord != null && this._worldPerson >= 0 && CS$<>8__locals1.people.IndexOf(nemesisRecord) == this._worldPerson;
					string text10 = RosterMenu.OutcomeLabel(p);
					ValueTuple<int, int, int> valueTuple6 = (text10 != "") ? RosterMenu.OutcomeColor(text10) : Theme.Faint;
					Theme.CardRect(num27, CS$<>8__locals1.ry, CS$<>8__locals1.rw, num46, 235, selected);
					if (nemesisRecord != null)
					{
						int pi = CS$<>8__locals1.people.IndexOf(nemesisRecord);
						UiMouse.Region(num27, CS$<>8__locals1.ry, CS$<>8__locals1.rw, num46, delegate
						{
							if (CS$<>8__locals1.<>4__this._worldPerson == pi)
							{
								return false;
							}
							CS$<>8__locals1.<>4__this._worldPerson = pi;
							return true;
						}, delegate
						{
							UiMouse.Send(Keys.Return);
						});
					}
					UiDraw.Rect(num27 + 0.0010416667f, CS$<>8__locals1.ry + 0.0018518518f, 0.0015625001f, num46 - 0.0037037036f, valueTuple6.Item1, valueTuple6.Item2, valueTuple6.Item3, 255);
					float num47 = num27 + 0.012f;
					float width = CS$<>8__locals1.rw - 0.024f - ((text10 != "") ? 0.11f : 0f);
					ValueTuple<int, int, int> valueTuple7 = (nemesisRecord != null) ? Theme.Title : Theme.Dim;
					UiDraw.TextFont(RosterMenu.FitTitle(p.Name + ((nemesisRecord != null && !string.IsNullOrEmpty(nemesisRecord.Title)) ? ("  -  " + nemesisRecord.Title) : ""), 0.24f, width, true), UiDraw.TitleFont, num47, CS$<>8__locals1.ry + 0.006f, 0.24f, valueTuple7.Item1, valueTuple7.Item2, valueTuple7.Item3, 255, false);
					string text11 = p.Note + ((this._mod.DebugOn && p.SecretlyLived) ? "  (debug: secretly lived)" : "");
					if (nemesisRecord != null && nemesisRecord.IsAlive && !nemesisRecord.PresumedDead)
					{
						ValueTuple<string, List<Thought>> valueTuple8 = this._mod.MoodOf(nemesisRecord);
						string item2 = valueTuple8.Item1;
						List<Thought> item3 = valueTuple8.Item2;
						if (item3.Count > 0)
						{
							text11 = string.Concat(new string[]
							{
								text11,
								(text11 != "") ? "   -   " : "",
								item2,
								": ",
								item3[0].Text
							});
						}
					}
					if (text11 != "")
					{
						UiDraw.Text(RosterMenu.FitText(text11, 0.2f, width, false), num47, CS$<>8__locals1.ry + 0.029f, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
					}
					if (text10 != "")
					{
						Theme.Badge(text10, num27 + CS$<>8__locals1.rw - 0.01f - 0.1f, CS$<>8__locals1.ry + (num46 - 0.024f) / 2f, 0.1f, valueTuple6, text10 == "KILLED" || text10 == "TE MATÓ");
					}
					if (list6.Count > 0)
					{
						float num48 = num47;
						float num49 = CS$<>8__locals1.ry + 0.052f - 0.002f;
						float num50 = num27 + CS$<>8__locals1.rw - 0.012f;
						i = 0;
						while (i < list6.Count)
						{
							string text12 = list6[i];
							float num51 = Theme.MixedWidth(text12, 0.17f, 0.0222f) + 0.014f;
							if (num48 + num51 > num50)
							{
								if (i < list6.Count)
								{
									UiDraw.Text(string.Format("+{0}", list6.Count - i), num48, num49 + 0.002f, 0.17f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
									break;
								}
								break;
							}
							else
							{
								num48 += Theme.Tag(text12, num48, num49, RosterMenu.EffectColor(text12)) + 0.005f;
								i++;
							}
						}
					}
					CS$<>8__locals1.ry += num46 + 0.008f;
				}
			}
			Theme.ScrollBar(num27 + CS$<>8__locals1.rw + 0.004f, ry, Math.Max(0.02f, CS$<>8__locals1.ry - ry - 0.008f), this._peopleStart, num45, worldEvent.People.Count);
		}

		// Token: 0x0600123F RID: 4671 RVA: 0x0011F2A0 File Offset: 0x0011D4A0
		private static List<string> WrapBody(string s, float scale, float width)
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

		// Token: 0x06001240 RID: 4672 RVA: 0x0011F344 File Offset: 0x0011D544
		private static List<string> Wrap(string s, float scale, float width)
		{
			int num = Math.Max(8, (int)(width / (scale * 0.0175f)));
			List<string> list = new List<string>();
			string text = "";
			foreach (string text2 in (s ?? "").Split(new char[]
			{
				' '
			}))
			{
				if (text.Length + text2.Length + 1 > num && text.Length > 0)
				{
					list.Add(text);
					text = text2;
				}
				else
				{
					text = ((text.Length == 0) ? text2 : (text + " " + text2));
				}
			}
			if (text.Length > 0)
			{
				list.Add(text);
			}
			return list;
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x0011F3F4 File Offset: 0x0011D5F4
		private string MainFooter(bool list)
		{
			string str = this._mod.Config.RandomAmbushes ? "Sí" : "No";
			string text = NemesisSettings.MessageLevelShort[this._mod.Config.MessageLevel];
			string text2 = NemesisMod.NameCardModes[this._mod.Config.NameCards];
			string text3 = "     X  " + NemesisMod.YouShort + (this._mod.DebugOn ? ("     Y  Ambushes: " + str) : "");
			string text4 = "     A  " + NemesisMod.YouShort + (this._mod.DebugOn ? ("     P  Promote     R  Ambushes: " + str) : "");
			string text5 = this._listAll ? "Todos" : "Némesis";
			string text6 = this._mod.DebugOn ? "" : "     Y  Guía";
			if (RosterMenu.UsingPad)
			{
				if (!list)
				{
					return "Cruceta  Navegar     A  Detalles     Start  Ajustes" + text3 + text6 + "     LT/RT  Zoom     B  Cerrar";
				}
				return string.Concat(new string[]
				{
					"Cruceta  Navegar     A  Detalles     Start  Ajustes     LT/RT  Mostrar: ",
					text5,
					text3,
					text6,
					"     B  Cerrar"
				});
			}
			else
			{
				if (!list)
				{
					return "Flechas  Navegar     Enter  Detalles     O  Ajustes" + text4 + "     G  Guía     +/-  Zoom     Z  Ajustar     Retroceso  Cerrar";
				}
				return "Arriba/Abajo  Navegar     Enter  Detalles     O  Ajustes     F  Mostrar: " + text5 + text4 + "     G  Guía     Retroceso  Cerrar";
			}
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x0011F547 File Offset: 0x0011D747
		private static void DrawFooter(float y, string help)
		{
			Theme.Footer(y, help, false);
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x0011F551 File Offset: 0x0011D751
		private static bool UsingPad
		{
			get
			{
				return XPad.RecentlyUsed;
			}
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x0011F558 File Offset: 0x0011D758
		[return: TupleElementNames(new string[]
		{
			"text",
			"c",
			"r",
			"g",
			"b"
		})]
		private ValueTuple<string, ValueTuple<int, int, int>> WarVerdict(WarRecord w, bool live, Gang a, Gang b, District d)
		{
			if (live)
			{
				int num = ((d != null) ? this._mod.HoldOf(d, w.A) : w.HoldA0) - w.HoldA0;
				int num2 = ((d != null) ? this._mod.HoldOf(d, w.B) : w.HoldB0) - w.HoldB0;
				if (num - num2 >= 3)
				{
					return new ValueTuple<string, ValueTuple<int, int, int>>("EN CURSO  -  " + RosterMenu.<WarVerdict>g__Up|215_0(a) + " EN CABEZA", a.Accent);
				}
				if (num2 - num >= 3)
				{
					return new ValueTuple<string, ValueTuple<int, int, int>>("EN CURSO  -  " + RosterMenu.<WarVerdict>g__Up|215_0(b) + " EN CABEZA", b.Accent);
				}
				return new ValueTuple<string, ValueTuple<int, int, int>>("EN CURSO  -  NINGÚN BANDO EN CABEZA", Theme.Dim);
			}
			else
			{
				string result = w.Result;
				if (result != null)
				{
					switch (result.Length)
					{
					case 3:
						if (result == "met")
						{
							return new ValueTuple<string, ValueTuple<int, int, int>>("GANADA POR " + RosterMenu.<WarVerdict>g__Up|215_0(a), a.Accent);
						}
						break;
					case 4:
						if (result == "moot")
						{
							return new ValueTuple<string, ValueTuple<int, int, int>>("SIN VENCEDOR  -  NADA POR LO QUE LUCHAR", Theme.Dim);
						}
						break;
					case 5:
					{
						char c = result[0];
						if (c != 'p')
						{
							if (c == 's')
							{
								if (result == "spent")
								{
									return new ValueTuple<string, ValueTuple<int, int, int>>("GANADA POR " + RosterMenu.<WarVerdict>g__Up|215_0(b) + "  -  LOS ATACANTES SE QUEDARON SIN HOMBRES", b.Accent);
								}
							}
						}
						else if (result == "peace")
						{
							return new ValueTuple<string, ValueTuple<int, int, int>>("SIN VENCEDOR  -  SE FIRMÓ LA PAZ", Theme.Dim);
						}
						break;
					}
					case 6:
					{
						char c = result[0];
						if (c != 'b')
						{
							if (c == 'm')
							{
								if (result == "merged")
								{
									return new ValueTuple<string, ValueTuple<int, int, int>>("SIN VENCEDOR  -  UNIDA A SU OTRA GUERRA", Theme.Dim);
								}
							}
						}
						else if (result == "broken")
						{
							return new ValueTuple<string, ValueTuple<int, int, int>>(string.Concat(new string[]
							{
								"GANADA POR ",
								RosterMenu.<WarVerdict>g__Up|215_0(a),
								"  -  ",
								RosterMenu.<WarVerdict>g__Up|215_0(b),
								" SE QUEDARON SIN HOMBRES"
							}), a.Accent);
						}
						break;
					}
					case 7:
						if (result == "gave up")
						{
							return new ValueTuple<string, ValueTuple<int, int, int>>("GANADA POR " + RosterMenu.<WarVerdict>g__Up|215_0(b) + "  -  LOS ATACANTES SE RINDIERON", b.Accent);
						}
						break;
					case 8:
						if (result == "defended")
						{
							return new ValueTuple<string, ValueTuple<int, int, int>>("GANADA POR " + RosterMenu.<WarVerdict>g__Up|215_0(b) + "  -  RESISTIERON", b.Accent);
						}
						break;
					}
				}
				if (this._mod.IsDestroyed(b) && !this._mod.IsDestroyed(a))
				{
					return new ValueTuple<string, ValueTuple<int, int, int>>(string.Concat(new string[]
					{
						"GANADA POR ",
						RosterMenu.<WarVerdict>g__Up|215_0(a),
						"  -  THE ",
						b.Name.ToUpperInvariant(),
						" ESTÁN ACABADOS"
					}).Replace("THE THE", "THE"), a.Accent);
				}
				if (this._mod.IsDestroyed(a) && !this._mod.IsDestroyed(b))
				{
					return new ValueTuple<string, ValueTuple<int, int, int>>(string.Concat(new string[]
					{
						"GANADA POR ",
						RosterMenu.<WarVerdict>g__Up|215_0(b),
						"  -  THE ",
						a.Name.ToUpperInvariant(),
						" ESTÁN ACABADOS"
					}).Replace("THE THE", "THE"), b.Accent);
				}
				if (w.EndHoldA >= 0 && w.EndHoldB >= 0)
				{
					int num = w.EndHoldA - w.HoldA0;
					int num2 = w.EndHoldB - w.HoldB0;
					if (num - num2 >= 3)
					{
						return new ValueTuple<string, ValueTuple<int, int, int>>(RosterMenu.<WarVerdict>g__Up|215_0(a) + " SALIÓ VICTORIOSO", a.Accent);
					}
					if (num2 - num >= 3)
					{
						return new ValueTuple<string, ValueTuple<int, int, int>>(RosterMenu.<WarVerdict>g__Up|215_0(b) + " SALIÓ VICTORIOSO", b.Accent);
					}
				}
				return new ValueTuple<string, ValueTuple<int, int, int>>("SIN VENCEDOR CLARO", Theme.Dim);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x0011F992 File Offset: 0x0011DB92
		private int WarsFilter
		{
			get
			{
				return Gangs.All.Length + 3;
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x0011F99D File Offset: 0x0011DB9D
		private int DispOf(int f)
		{
			if (f == this.WarsFilter)
			{
				return 1;
			}
			if (f != 0)
			{
				return f + 1;
			}
			return 0;
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x0011F9B2 File Offset: 0x0011DBB2
		private int FilterOf(int d)
		{
			if (d == 0)
			{
				return 0;
			}
			if (d != 1)
			{
				return d - 1;
			}
			return this.WarsFilter;
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0011F9C8 File Offset: 0x0011DBC8
		private bool HandleWarsKey(Keys key)
		{
			int num = Math.Max(1, this._mod.WarList().Count);
			if (key <= Keys.P)
			{
				if (key <= Keys.Down)
				{
					if (key != Keys.Return)
					{
						switch (key)
						{
						case Keys.Prior:
							goto IL_BD;
						case Keys.Next:
							goto IL_D3;
						case Keys.End:
						case Keys.Home:
							return false;
						case Keys.Left:
						case Keys.Right:
							break;
						case Keys.Up:
							this._warSel = Math.Max(0, this._warSel - 1);
							return true;
						case Keys.Down:
							this._warSel = Math.Min(num - 1, this._warSel + 1);
							return true;
						default:
							return false;
						}
					}
				}
				else if (key != Keys.M && key != Keys.P)
				{
					return false;
				}
				return true;
			}
			if (key <= Keys.Subtract)
			{
				if (key == Keys.Add)
				{
					goto IL_D3;
				}
				if (key != Keys.Subtract)
				{
					return false;
				}
			}
			else
			{
				if (key == Keys.Oemplus)
				{
					goto IL_D3;
				}
				if (key != Keys.OemMinus)
				{
					return false;
				}
			}
			IL_BD:
			this._warEvScroll = Math.Max(0, this._warEvScroll - 3);
			return true;
			IL_D3:
			this._warEvScroll = Math.Min(Math.Max(0, this._warEvCount - 1), this._warEvScroll + 3);
			return true;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x0011FAD0 File Offset: 0x0011DCD0
		private void DrawWarsPage(float bodyTop)
		{
			RosterMenu.<>c__DisplayClass221_0 CS$<>8__locals1 = new RosterMenu.<>c__DisplayClass221_0();
			CS$<>8__locals1.<>4__this = this;
			List<ValueTuple<WarRecord, bool>> list = this._mod.WarList();
			float num = 0.084f;
			float num2 = 0.3f;
			float num3 = bodyTop + 0.014f;
			CS$<>8__locals1.bottom = 0.854f;
			if (list.Count == 0)
			{
				UiDraw.Text("No wars - not yet. When two outfits that hate each other share ground, one may come for the other.", num + 0.006f, num3 + 0.006f, 0.23f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
				return;
			}
			this._warSel = Math.Max(0, Math.Min(this._warSel, list.Count - 1));
			if (this._warEvFor != this._warSel)
			{
				this._warEvFor = this._warSel;
				this._warEvScroll = 0;
			}
			UiMouse.WheelHandler = delegate(int d)
			{
				UiMouse.Send((d > 0) ? Keys.Prior : Keys.Next);
			};
			int num4 = Math.Max(4, Math.Min((int)((CS$<>8__locals1.bottom - num3) / 0.052f), (UiDraw.TextSlotsLeft - 150) / 4));
			int num5 = Math.Max(0, Math.Min(this._warSel - num4 / 2, list.Count - num4));
			Theme.ScrollBar(num + num2 + 0.004f, num3, (float)Math.Min(num4, list.Count) * 0.052f, num5, num4, list.Count);
			int num6 = num5;
			while (num6 < list.Count && num6 < num5 + num4)
			{
				ValueTuple<WarRecord, bool> valueTuple = list[num6];
				WarRecord item = valueTuple.Item1;
				bool item2 = valueTuple.Item2;
				Gang g = Gangs.ById(item.A);
				Gang g2 = Gangs.ById(item.B);
				District district = this._mod.DistrictByKey(item.District);
				float num7 = num3 + (float)(num6 - num5) * 0.052f;
				bool flag = num6 == this._warSel;
				int wi = num6;
				UiMouse.Region(num - 0.0031250003f, num7, num2 + 0.0031250003f, 0.048f, delegate
				{
					if (CS$<>8__locals1.<>4__this._warSel == wi)
					{
						return false;
					}
					CS$<>8__locals1.<>4__this._warSel = wi;
					return true;
				}, delegate
				{
				});
				ValueTuple<int, int, int> valueTuple2 = item2 ? NemesisMod.HeatColor(item.Heat) : Theme.Faint;
				if (flag)
				{
					Theme.SelectFill(num - 0.0031250003f, num7, num2 + 0.0031250003f, 0.048f, 235);
				}
				UiDraw.Rect(num, num7, 0.0015625001f, 0.048f, valueTuple2.Item1, valueTuple2.Item2, valueTuple2.Item3, 255);
				if (flag)
				{
					Theme.RedFrame(num - 0.0031250003f, num7, num2 + 0.0031250003f, 0.048f, 1f, 0);
				}
				ValueTuple<int, int, int> valueTuple3 = flag ? Theme.SelectedText : (item2 ? Theme.Text : Theme.Dim);
				UiDraw.TextFont(RosterMenu.FitTitle(this._mod.WarName(item).ToUpperInvariant(), 0.24f, num2 - 0.024f, true), UiDraw.TitleFont, num + 0.012f, num7 + 0.005f, 0.24f, valueTuple3.Item1, valueTuple3.Item2, valueTuple3.Item3, 255, false);
				UiDraw.Text(RosterMenu.FitText(string.Concat(new string[]
				{
					((district != null) ? district.Name : null) ?? item.District,
					"  -  ",
					RosterMenu.Short(g),
					" vs ",
					RosterMenu.Short(g2),
					"  -  ",
					item2 ? string.Format("Day {0}", this._mod.WarDay(item)) : "Terminada"
				}), 0.2f, num2 - 0.024f, false), num + 0.012f, num7 + 0.028f, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
				num6++;
			}
			ValueTuple<WarRecord, bool> valueTuple4 = list[this._warSel];
			WarRecord item3 = valueTuple4.Item1;
			bool item4 = valueTuple4.Item2;
			Gang gang = Gangs.ById(item3.A);
			Gang gang2 = Gangs.ById(item3.B);
			District district2 = this._mod.DistrictByKey(item3.District);
			if (gang == null || gang2 == null)
			{
				return;
			}
			float num8 = 0.415f;
			float num9 = 0.5f;
			CS$<>8__locals1.ry = num3 + 0.01f;
			CS$<>8__locals1.mx = num8 + num9 / 2f;
			string s = this._mod.WarName(item3).ToUpperInvariant();
			float num10 = 0.38f;
			while (Theme.CapsWidth(s, num10) > num9 * 0.95f && num10 > 0.28f)
			{
				num10 -= 0.01f;
			}
			CS$<>8__locals1.<DrawWarsPage>g__Centre|1(s, num10, Theme.Title, true);
			CS$<>8__locals1.ry += num10 * 0.075f + 0.01f;
			ValueTuple<string, ValueTuple<int, int, int>> valueTuple5 = this.WarVerdict(item3, item4, gang, gang2, district2);
			string item5 = valueTuple5.Item1;
			ValueTuple<int, int, int> item6 = valueTuple5.Item2;
			CS$<>8__locals1.<DrawWarsPage>g__Centre|1(item5, 0.3f, item6, true);
			CS$<>8__locals1.ry += 0.032f;
			long num11 = Math.Max(1L, (((item3.EndMin > 0L) ? item3.EndMin : this._mod.GameMinutesNow()) - item3.StartMin) / 1440L + ((item4 > false) ? 1L : 0L));
			string s2 = item4 ? string.Format("{0}  -  Day {1}, since {2}", ((district2 != null) ? district2.Name : null) ?? item3.District, this._mod.WarDay(item3), NemesisMod.WarSince(item3)) : string.Format("{0}  -  over {1:MMM d} after {2} day{3}", new object[]
			{
				((district2 != null) ? district2.Name : null) ?? item3.District,
				GameDate.FromMinutes(item3.EndMin),
				num11,
				(num11 == 1L) ? "" : "s"
			});
			CS$<>8__locals1.<DrawWarsPage>g__Centre|1(RosterMenu.FitText(s2, 0.24f, num9 * 0.95f, false), 0.24f, Theme.Dim, false);
			CS$<>8__locals1.ry += 0.028f;
			if (!item4 && !string.IsNullOrEmpty(item3.EndHow))
			{
				CS$<>8__locals1.<DrawWarsPage>g__Centre|1(RosterMenu.FitText(RosterMenu.Cap(item3.EndHow), 0.22f, num9 * 0.95f, false), 0.22f, Theme.Text, false);
				CS$<>8__locals1.ry += 0.026f;
			}
			if (!string.IsNullOrEmpty(item3.Cause))
			{
				CS$<>8__locals1.<DrawWarsPage>g__Centre|1(RosterMenu.FitText("Why: " + RosterMenu.Cap(item3.Cause), 0.21f, num9 * 0.95f, false), 0.21f, Theme.Faint, false);
				CS$<>8__locals1.ry += 0.026f;
			}
			CS$<>8__locals1.ry += 0.006f;
			RosterMenu.<DrawWarsPage>g__Rule|221_2(num8, CS$<>8__locals1.ry, num9, 120);
			CS$<>8__locals1.ry += 0.012f;
			float num12 = 0.02f;
			CS$<>8__locals1.half = (num9 - num12) / 2f;
			float ry = CS$<>8__locals1.ry;
			float num13 = CS$<>8__locals1.ry;
			int num14 = (district2 != null) ? this._mod.HoldOf(district2, item3.A) : item3.HoldA0;
			int num15 = (district2 != null) ? this._mod.HoldOf(district2, item3.B) : item3.HoldB0;
			for (int i = 0; i < 2; i++)
			{
				Gang gang3 = (i == 0) ? gang : gang2;
				int num16 = (i == 0) ? num14 : num15;
				int num17 = (i == 0) ? item3.HoldA0 : item3.HoldB0;
				RosterMenu.<>c__DisplayClass221_2 CS$<>8__locals3;
				CS$<>8__locals3.x = num8 + (float)i * (CS$<>8__locals1.half + num12);
				CS$<>8__locals3.y = ry;
				UiDraw.Rect(CS$<>8__locals3.x, CS$<>8__locals3.y, CS$<>8__locals1.half, 0.0027777776f, gang3.Accent.Item1, gang3.Accent.Item2, gang3.Accent.Item3, 255);
				CS$<>8__locals3.y += 0.008f;
				UiDraw.TextFont(RosterMenu.FitTitle(gang3.Name.ToUpperInvariant(), 0.27f, CS$<>8__locals1.half - 0.09f, true), UiDraw.TitleFont, CS$<>8__locals3.x + 0.002f, CS$<>8__locals3.y, 0.27f, gang3.Accent.Item1, gang3.Accent.Item2, gang3.Accent.Item3, 255, false);
				string text = (i == 0) ? "ATTACKING" : "DEFENDING";
				UiDraw.TextFont(text, UiDraw.TitleFont, CS$<>8__locals3.x + CS$<>8__locals1.half - Theme.CapsWidth(text, 0.19f) - 0.002f, CS$<>8__locals3.y + 0.004f, 0.19f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
				CS$<>8__locals3.y += 0.028f;
				if (item4)
				{
					CS$<>8__locals1.<DrawWarsPage>g__Field|8("LÍDER", this._mod.WarLeader(gang3), Theme.Text, ref CS$<>8__locals3);
					CS$<>8__locals1.<DrawWarsPage>g__Field|8("OBJETIVO", this._mod.WarAim(gang3), Theme.Text, ref CS$<>8__locals3);
					CS$<>8__locals1.<DrawWarsPage>g__Field|8("BOLSA", this._mod.WarPurse(gang3), new ValueTuple<int, int, int>(212, 176, 96), ref CS$<>8__locals3);
					CS$<>8__locals1.<DrawWarsPage>g__Field|8("COSTS", this._mod.WarCosts(gang3), Theme.Dim, ref CS$<>8__locals3);
					CS$<>8__locals1.<DrawWarsPage>g__Field|8("HOMBRES", string.Format("{0} of {1}  -  {2}", this._mod.MenOf(gang3), this._mod.MenStart(gang3), NemesisMod.StrengthWord(this._mod.Strength(gang3))), (this._mod.Strength(gang3) < 30) ? Theme.Warn : Theme.Text, ref CS$<>8__locals3);
				}
				else
				{
					CS$<>8__locals1.<DrawWarsPage>g__Field|8("LÍDER", (i == 0) ? item3.EndLeaderA : item3.EndLeaderB, Theme.Text, ref CS$<>8__locals3);
					CS$<>8__locals1.<DrawWarsPage>g__Field|8("OBJETIVO", (i == 0) ? item3.EndAimA : item3.EndAimB, Theme.Text, ref CS$<>8__locals3);
					CS$<>8__locals1.<DrawWarsPage>g__Field|8("BOLSA", ((i == 0) ? item3.EndPurseA : item3.EndPurseB) ?? "not recorded", new ValueTuple<int, int, int>(212, 176, 96), ref CS$<>8__locals3);
					CS$<>8__locals1.<DrawWarsPage>g__Field|8("HOMBRES", ((i == 0) ? item3.EndMenA : item3.EndMenB) ?? "not recorded", Theme.Text, ref CS$<>8__locals3);
				}
				UiDraw.TextFont("GROUND", UiDraw.TitleFont, CS$<>8__locals3.x + 0.002f, CS$<>8__locals3.y, 0.18f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
				float num18 = CS$<>8__locals3.x + 0.06f;
				float num19 = CS$<>8__locals1.half - 0.064f;
				float num20 = CS$<>8__locals3.y + 0.008f;
				UiDraw.Rect(num18, num20, num19, 0.006f, 40, 38, 35, 230);
				UiDraw.Rect(num18, num20, num19 * Math.Max(0.01f, Math.Min(1f, (float)num16 / 60f)), 0.006f, gang3.Accent.Item1, gang3.Accent.Item2, gang3.Accent.Item3, 255);
				UiDraw.Rect(num18 + num19 * Math.Min(1f, (float)num17 / 60f), num20 - 0.004f, 0.0010416667f, 0.014f, 230, 226, 216, 200);
				CS$<>8__locals3.y += 0.024f;
				num13 = Math.Max(num13, CS$<>8__locals3.y);
			}
			CS$<>8__locals1.ry = num13 + 0.006f;
			RosterMenu.<DrawWarsPage>g__Rule|221_2(num8, CS$<>8__locals1.ry, num9, 120);
			CS$<>8__locals1.ry += 0.012f;
			CS$<>8__locals1.lx2 = num8;
			CS$<>8__locals1.cwid = CS$<>8__locals1.half;
			float num21 = num8 + CS$<>8__locals1.half + num12;
			CS$<>8__locals1.ly = CS$<>8__locals1.ry;
			CS$<>8__locals1.ry2 = CS$<>8__locals1.ry;
			List<ValueTuple<string, Gang, string, string>> list2 = this._mod.WarNamedDead(item3);
			int num22 = 6 + Math.Min(list2.Count, 4) * 4;
			if (CS$<>8__locals1.<DrawWarsPage>g__RoomR|4(0.05f, 20))
			{
				List<ValueTuple<WorldEvent, int>> list3 = this._mod.WarFights(item3);
				Theme.Section(string.Format("FIGHTS ({0})", list3.Count), num21, CS$<>8__locals1.ry2, num21 + CS$<>8__locals1.cwid, null);
				CS$<>8__locals1.ry2 += 0.028f;
				this._warEvCount = list3.Count;
				this._warEvScroll = Math.Max(0, Math.Min(this._warEvScroll, Math.Max(0, list3.Count - 1)));
				float ry2 = CS$<>8__locals1.ry2;
				float num23 = (CS$<>8__locals1.bottom - CS$<>8__locals1.ry2) * 0.55f;
				int num24 = 0;
				int num25 = this._warEvScroll;
				while (num25 < list3.Count && CS$<>8__locals1.ry2 - ry2 + 0.04f <= num23 && CS$<>8__locals1.<DrawWarsPage>g__RoomR|4(0.04f, 66 + num22))
				{
					ValueTuple<WorldEvent, int> valueTuple6 = list3[num25];
					WorldEvent item7 = valueTuple6.Item1;
					int item8 = valueTuple6.Item2;
					ValueTuple<int, int, int> valueTuple7 = NemesisMod.KindColor(item7.Kind);
					UiDraw.Rect(num21, CS$<>8__locals1.ry2 + 0.003f, 0.0010416667f, 0.032f, valueTuple7.Item1, valueTuple7.Item2, valueTuple7.Item3, 255);
					UiDraw.Text(GameDate.FromMinutes(item7.GameMin).ToString("MMM d  h:mm tt"), num21 + 0.008f, CS$<>8__locals1.ry2, 0.18f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
					string text2 = (item8 == 0) ? "no dead" : string.Format("{0} dead", item8);
					ValueTuple<int, int, int> valueTuple8 = (item8 == 0) ? Theme.Dim : Theme.Blood;
					UiDraw.TextFont(text2.ToUpperInvariant(), UiDraw.TitleFont, num21 + CS$<>8__locals1.cwid - Theme.CapsWidth(text2.ToUpperInvariant(), 0.18f) - 0.004f, CS$<>8__locals1.ry2, 0.18f, valueTuple8.Item1, valueTuple8.Item2, valueTuple8.Item3, 255, false);
					UiDraw.Text(RosterMenu.FitText(item7.Title ?? "", 0.2f, CS$<>8__locals1.cwid - 0.014f, false), num21 + 0.008f, CS$<>8__locals1.ry2 + 0.017f, 0.2f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
					CS$<>8__locals1.ry2 += 0.04f;
					num24++;
					num25++;
				}
				if (list3.Count == 0)
				{
					UiDraw.Text("No fights yet.", num21 + 0.008f, CS$<>8__locals1.ry2, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
					CS$<>8__locals1.ry2 += 0.024f;
				}
				if (num24 > 0 && num24 < list3.Count)
				{
					Theme.ScrollBar(num21 + CS$<>8__locals1.cwid - 0.002f, ry2, (float)num24 * 0.04f, this._warEvScroll, num24, list3.Count);
				}
				CS$<>8__locals1.ry2 += 0.008f;
			}
			if (CS$<>8__locals1.<DrawWarsPage>g__RoomR|4(0.05f, 70))
			{
				List<ValueTuple<string, Gang, string, string>> list4 = list2;
				Theme.Section(string.Format("NAMED MEN DEAD ({0})", list4.Count), num21, CS$<>8__locals1.ry2, num21 + CS$<>8__locals1.cwid, null);
				CS$<>8__locals1.ry2 += 0.028f;
				int num26 = 0;
				foreach (ValueTuple<string, Gang, string, string> valueTuple9 in list4)
				{
					string item9 = valueTuple9.Item1;
					Gang item10 = valueTuple9.Item2;
					string item11 = valueTuple9.Item3;
					string item12 = valueTuple9.Item4;
					if (!CS$<>8__locals1.<DrawWarsPage>g__RoomR|4(0.024f, 66))
					{
						break;
					}
					ValueTuple<int, int, int> valueTuple10 = (item10 != null) ? item10.Accent : Theme.Dim;
					UiDraw.Rect(num21, CS$<>8__locals1.ry2 + 0.005f, 0.0026041667f, 0.0046296297f, valueTuple10.Item1, valueTuple10.Item2, valueTuple10.Item3, 255);
					UiDraw.Text(RosterMenu.FitText(item9 + (string.IsNullOrEmpty(item11) ? "" : ("  -  " + item11)), 0.2f, CS$<>8__locals1.cwid - 0.07f, false), num21 + 0.01f, CS$<>8__locals1.ry2, 0.2f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
					UiDraw.Text(item12, num21 + CS$<>8__locals1.cwid - Theme.MixedWidth(item12, 0.18f, 0.0222f) - 0.004f, CS$<>8__locals1.ry2 + 0.001f, 0.18f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
					CS$<>8__locals1.ry2 += 0.024f;
					num26++;
				}
				if (list4.Count == 0)
				{
					UiDraw.Text("Ninguno aún.", num21 + 0.008f, CS$<>8__locals1.ry2, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
					CS$<>8__locals1.ry2 += 0.024f;
				}
				else if (num26 < list4.Count && CS$<>8__locals1.<DrawWarsPage>g__RoomR|4(0.022f, 4))
				{
					UiDraw.Text(string.Format("y {0} más", list4.Count - num26), num21 + 0.01f, CS$<>8__locals1.ry2, 0.19f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
					CS$<>8__locals1.ry2 += 0.022f;
				}
			}
			if (item4 && CS$<>8__locals1.<DrawWarsPage>g__RoomL|3(0.05f, 12))
			{
				Theme.Section("THE FRONT", CS$<>8__locals1.lx2, CS$<>8__locals1.ly, CS$<>8__locals1.lx2 + CS$<>8__locals1.cwid, null);
				CS$<>8__locals1.ly += 0.028f;
				ValueTuple<int, int, int> valueTuple11 = NemesisMod.HeatColor(item3.Heat);
				UiDraw.TextFont(NemesisMod.HeatWord(item3.Heat).ToUpperInvariant(), UiDraw.TitleFont, CS$<>8__locals1.lx2 + 0.004f, CS$<>8__locals1.ly, 0.22f, valueTuple11.Item1, valueTuple11.Item2, valueTuple11.Item3, 255, false);
				if (this._mod.DebugOn)
				{
					UiDraw.Text(string.Format("{0}", item3.Heat), CS$<>8__locals1.lx2 + CS$<>8__locals1.cwid - 0.024f, CS$<>8__locals1.ly, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
				}
				float x = CS$<>8__locals1.lx2 + 0.115f;
				float num27 = CS$<>8__locals1.cwid - 0.145f;
				float y = CS$<>8__locals1.ly + 0.009f;
				UiDraw.Rect(x, y, num27, 0.006f, 40, 38, 35, 230);
				UiDraw.Rect(x, y, num27 * Math.Max(0.02f, (float)item3.Heat / 100f), 0.006f, valueTuple11.Item1, valueTuple11.Item2, valueTuple11.Item3, 255);
				CS$<>8__locals1.ly += 0.028f;
				int num28 = this._mod.WarCampsTaken(item3);
				CS$<>8__locals1.<DrawWarsPage>g__LeftLine|5(string.Format("{0} fight{1}, {2} named m{3}n dead{4}", new object[]
				{
					item3.Clashes,
					(item3.Clashes == 1) ? "" : "s",
					item3.Dead,
					(item3.Dead == 1) ? "a" : "e",
					(num28 > 0) ? string.Format(", {0} camp{1} overrun", num28, (num28 == 1) ? "" : "s") : ""
				}), 0.2f, Theme.Dim, 0.022f);
				CS$<>8__locals1.ly += 0.008f;
			}
			if (CS$<>8__locals1.<DrawWarsPage>g__RoomL|3(0.05f, 12))
			{
				Theme.Section("LO QUE " + RosterMenu.Short(gang).ToUpperInvariant() + " QUIEREN", CS$<>8__locals1.lx2, CS$<>8__locals1.ly, CS$<>8__locals1.lx2 + CS$<>8__locals1.cwid, null);
				CS$<>8__locals1.ly += 0.028f;
				CS$<>8__locals1.<DrawWarsPage>g__LeftLine|5(RosterMenu.Cap(item3.GoalText ?? "nadie lo ha dicho"), 0.21f, Theme.Text, 0.022f);
				HashSet<string> seenAims = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
				{
					(item3.GoalText ?? "").Trim()
				};
				IEnumerable<string> source = (item3.GoalLog ?? new List<string>()).AsEnumerable<string>().Reverse<string>();
				Func<string, bool> predicate;
				Func<string, bool> <>9__10;
				if ((predicate = <>9__10) == null)
				{
					predicate = (<>9__10 = ((string l) => seenAims.Add(RosterMenu.<DrawWarsPage>g__AimOf|221_9(l))));
				}
				foreach (string str in source.Where(predicate).Take(2))
				{
					CS$<>8__locals1.<DrawWarsPage>g__LeftLine|5("before - " + str, 0.19f, Theme.Faint, 0.02f);
				}
				CS$<>8__locals1.ly += 0.008f;
			}
			string text3 = this._mod.WarDefenders(item3);
			if (text3 != null && CS$<>8__locals1.<DrawWarsPage>g__RoomL|3(0.05f, 12))
			{
				Theme.Section("THE " + RosterMenu.Short(gang2).ToUpperInvariant(), CS$<>8__locals1.lx2, CS$<>8__locals1.ly, CS$<>8__locals1.lx2 + CS$<>8__locals1.cwid, null);
				CS$<>8__locals1.ly += 0.028f;
				CS$<>8__locals1.<DrawWarsPage>g__LeftLine|5(text3, 0.21f, Theme.Text, 0.022f);
				CS$<>8__locals1.ly += 0.008f;
			}
			string text4 = item4 ? this._mod.WarOutlook(item3) : null;
			if (text4 != null && CS$<>8__locals1.<DrawWarsPage>g__RoomL|3(0.05f, 12))
			{
				Theme.Section("OUTLOOK", CS$<>8__locals1.lx2, CS$<>8__locals1.ly, CS$<>8__locals1.lx2 + CS$<>8__locals1.cwid, null);
				CS$<>8__locals1.ly += 0.028f;
				CS$<>8__locals1.<DrawWarsPage>g__LeftLine|5(text4, 0.21f, Theme.Text, 0.022f);
				CS$<>8__locals1.ly += 0.008f;
			}
			if (CS$<>8__locals1.<DrawWarsPage>g__RoomL|3(0.05f, 12))
			{
				Theme.Section("YOUR PART", CS$<>8__locals1.lx2, CS$<>8__locals1.ly, CS$<>8__locals1.lx2 + CS$<>8__locals1.cwid, null);
				CS$<>8__locals1.ly += 0.028f;
				string t = (item3.YouKilledA + item3.YouKilledB == 0) ? "You've kept out of it." : (string.Format("Has matado a {0} de {1} y a {2} de {3}", new object[]
				{
					item3.YouKilledA,
					RosterMenu.Short(gang),
					item3.YouKilledB,
					RosterMenu.Short(gang2)
				}) + ((item3.OfficersA + item3.OfficersB > 0) ? string.Format(" - {0} de ellos oficiales.", item3.OfficersA + item3.OfficersB) : "."));
				CS$<>8__locals1.<DrawWarsPage>g__LeftLine|5(t, 0.21f, Theme.Text, 0.022f);
				if (item3.FavourSaid)
				{
					Gang g3 = (item3.YouKilledA > item3.YouKilledB) ? gang2 : gang;
					CS$<>8__locals1.<DrawWarsPage>g__LeftLine|5("The " + RosterMenu.Short(g3) + " saben que les has estado ayudando.", 0.2f, Theme.Good, 0.022f);
				}
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x001214A4 File Offset: 0x0011F6A4
		private static string Short(Gang g)
		{
			if (g == null)
			{
				return "?";
			}
			if (!g.Name.StartsWith("The "))
			{
				return g.Name;
			}
			return g.Name.Substring(4);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x001214D4 File Offset: 0x0011F6D4
		private static string Cap(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return char.ToUpperInvariant(s[0]).ToString() + s.Substring(1) + (s.EndsWith(".") ? "" : ".");
			}
			return s;
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x001215CD File Offset: 0x0011F7CD
		[CompilerGenerated]
		internal static bool <DrawCrimeSheet>g__Room|1_0(float need, int slots = 14, ref RosterMenu.<>c__DisplayClass1_0 A_2)
		{
			return A_2.y + need <= A_2.bottom && UiDraw.TextSlotsLeft >= slots;
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x001215EC File Offset: 0x0011F7EC
		[CompilerGenerated]
		internal static bool <DrawCrimeSheet>g__Unit|1_1(float need, int slots = 14, ref RosterMenu.<>c__DisplayClass1_0 A_2)
		{
			int num = A_2.total;
			A_2.total = num + 1;
			if (A_2.skip > 0)
			{
				num = A_2.skip;
				A_2.skip = num - 1;
				return false;
			}
			if (A_2.over || !RosterMenu.<DrawCrimeSheet>g__Room|1_0(need, slots, ref A_2))
			{
				A_2.over = true;
				return false;
			}
			num = A_2.drawn;
			A_2.drawn = num + 1;
			return true;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x00121650 File Offset: 0x0011F850
		[CompilerGenerated]
		internal static void <DrawCrimeSheet>g__Sec|1_2(string title, ref RosterMenu.<>c__DisplayClass1_0 A_1)
		{
			Theme.Section(title, A_1.rx, A_1.y, A_1.right, new ValueTuple<int, int, int>?(A_1.acc));
			A_1.y += 0.03f;
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00121688 File Offset: 0x0011F888
		[CompilerGenerated]
		internal static void <DrawCrimeSheet>g__Note|1_3(string text, ref RosterMenu.<>c__DisplayClass1_0 A_1)
		{
			if (string.IsNullOrEmpty(text) || !RosterMenu.<DrawCrimeSheet>g__Unit|1_1(0.022f, 6, ref A_1))
			{
				return;
			}
			UiDraw.Text(RosterMenu.FitText(text, 0.2f, A_1.colW - 0.02f, false), A_1.rx + 0.01f, A_1.y, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			A_1.y += 0.022f;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0012173C File Offset: 0x0011F93C
		[CompilerGenerated]
		internal static void <DrawConnectors>g__V|151_0(float x, float y1, float y2, ref RosterMenu.<>c__DisplayClass151_0 A_3)
		{
			y1 = Math.Max(y1, 0.27299997f);
			y2 = Math.Min(y2, 0.848f);
			if (y2 > y1)
			{
				UiDraw.VLine(x, y1, y2, A_3.cr, A_3.cg, A_3.cb, 210, 0.0012f);
			}
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x0012178C File Offset: 0x0011F98C
		[CompilerGenerated]
		internal static string <DrawDetail>g__Left|180_14(Thought t, ref RosterMenu.<>c__DisplayClass180_2 A_1)
		{
			long num = t.Until - A_1.nowM;
			if (num < 1440L)
			{
				return string.Format("{0}h", Math.Max(1L, num / 60L));
			}
			return string.Format("{0}d", (num + 720L) / 1440L);
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x001217EA File Offset: 0x0011F9EA
		[CompilerGenerated]
		[return: TupleElementNames(new string[]
		{
			null,
			null,
			null,
			"r",
			"g",
			"b"
		})]
		internal static ValueTuple<string, string, ValueTuple<int, int, int>> <DrawDetail>g__T|180_15(int v, string label, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> on)
		{
			return new ValueTuple<string, string, ValueTuple<int, int, int>>(v.ToString(), label, (v > 0) ? on : Theme.Dim);
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x0012183E File Offset: 0x0011FA3E
		[CompilerGenerated]
		internal static string <DrawWorld>g__Short|201_8(Gang g)
		{
			if (!g.Name.StartsWith("The "))
			{
				return g.Name;
			}
			return g.Name.Substring(4);
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x00121865 File Offset: 0x0011FA65
		[CompilerGenerated]
		internal static float <DrawWorld>g__CardH|201_6(WorldPart wp)
		{
			float num = 0.052f;
			List<string> effects = wp.Effects;
			return num + ((((effects != null) ? effects.Count : 0) > 0) ? 0.024f : 0f) + 0.008f;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00121894 File Offset: 0x0011FA94
		[CompilerGenerated]
		internal static string <WarVerdict>g__Up|215_0(Gang g)
		{
			return UiDraw.TheFix("the " + g.Name).ToUpperInvariant();
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x001218B0 File Offset: 0x0011FAB0
		[CompilerGenerated]
		internal static void <DrawWarsPage>g__Rule|221_2(float x, float y, float w, int alpha = 120)
		{
			UiDraw.Rect(x, y, w, 0.0009259259f, Theme.Rule.Item1, Theme.Rule.Item2, Theme.Rule.Item3, alpha);
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x001218E0 File Offset: 0x0011FAE0
		[CompilerGenerated]
		internal static string <DrawWarsPage>g__AimOf|221_9(string l)
		{
			int num = l.IndexOf(": ");
			string text = (num >= 0) ? l.Substring(num + 2) : l;
			int num2 = text.LastIndexOf(" (");
			return ((num2 > 0) ? text.Substring(0, num2) : text).Trim();
		}

		// Token: 0x04000BD1 RID: 3025
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static readonly ValueTuple<int, int, int> PriceGold = new ValueTuple<int, int, int>(215, 178, 95);

		// Token: 0x04000BD2 RID: 3026
		private int _crimeScroll;

		// Token: 0x04000BD3 RID: 3027
		private int _crimeTotal;

		// Token: 0x04000BD4 RID: 3028
		internal const float StatTileH = 0.056f;

		// Token: 0x04000BD5 RID: 3029
		private const float PanelX = 0.07f;

		// Token: 0x04000BD6 RID: 3030
		private const float PanelW = 0.86f;

		// Token: 0x04000BD7 RID: 3031
		private const float Top = 0.07f;

		// Token: 0x04000BD8 RID: 3032
		private const float HeaderH = 0.065f;

		// Token: 0x04000BD9 RID: 3033
		private const float Pad = 0.01f;

		// Token: 0x04000BDA RID: 3034
		private const float CenterX = 0.5f;

		// Token: 0x04000BDB RID: 3035
		private const float CardW = 0.13f;

		// Token: 0x04000BDC RID: 3036
		private const float CardH = 0.075f;

		// Token: 0x04000BDD RID: 3037
		private const float CardGap = 0.012f;

		// Token: 0x04000BDE RID: 3038
		private const float FooterY = 0.868f;

		// Token: 0x04000BDF RID: 3039
		private const float DetailFooterY = 0.868f;

		// Token: 0x04000BE0 RID: 3040
		private const int BossSeats = 1;

		// Token: 0x04000BE1 RID: 3041
		private const int LieutenantSeats = 4;

		// Token: 0x04000BE2 RID: 3042
		private const int ScrollSlots = 5;

		// Token: 0x04000BE3 RID: 3043
		private const int LieutenantMinRank = 3;

		// Token: 0x04000BE4 RID: 3044
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static readonly ValueTuple<int, int, int> LineColor = Theme.Line;

		// Token: 0x04000BE5 RID: 3045
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static readonly ValueTuple<int, int, int> LabelColor = Theme.Gold;

		// Token: 0x04000BE6 RID: 3046
		private readonly NemesisMod _mod;

		// Token: 0x04000BE7 RID: 3047
		private readonly int[] _offsets = new int[8];

		// Token: 0x04000BE8 RID: 3048
		private int _faction;

		// Token: 0x04000BE9 RID: 3049
		private int _listIndex;

		// Token: 0x04000BEA RID: 3050
		private int _listScroll;

		// Token: 0x04000BEB RID: 3051
		private string _listKey;

		// Token: 0x04000BEC RID: 3052
		private const int ListRows = 16;

		// Token: 0x04000BED RID: 3053
		private const float BarH = 0.12599999f;

		// Token: 0x04000BEE RID: 3054
		private const float BodyTop = 0.26099998f;

		// Token: 0x04000BEF RID: 3055
		private const float ListTop = 0.291f;

		// Token: 0x04000BF0 RID: 3056
		private const float ListRowH = 0.032f;

		// Token: 0x04000BF1 RID: 3057
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static readonly ValueTuple<int, int, int> UnaffiliatedGrey = new ValueTuple<int, int, int>(138, 138, 136);

		// Token: 0x04000BF2 RID: 3058
		private int _row = -1;

		// Token: 0x04000BF3 RID: 3059
		private int _col;

		// Token: 0x04000BF4 RID: 3060
		private NemesisRecord _detail;

		// Token: 0x04000BF5 RID: 3061
		private int _historyScroll;

		// Token: 0x04000BF6 RID: 3062
		private int _action;

		// Token: 0x04000BF7 RID: 3063
		private bool _confirmForget;

		// Token: 0x04000BF9 RID: 3065
		private bool _resetOpen;

		// Token: 0x04000BFA RID: 3066
		private bool _resetConfirm;

		// Token: 0x04000BFB RID: 3067
		private int _resetSel;

		// Token: 0x04000BFC RID: 3068
		private int _resetFaction;

		// Token: 0x04000BFD RID: 3069
		private static readonly string[] ResetRows = new string[]
		{
			"Reset EVERYTHING",
			"Reset the X-ray log",
			"Reset world events",
			"Reset one faction",
			"Add 12 test loners",
			"Forget safe camps (Arthur's gang)",
			"Cerrar"
		};

		// Token: 0x04000BFE RID: 3070
		private bool _worldPage;

		// Token: 0x04000BFF RID: 3071
		private int _worldIndex;

		// Token: 0x04000C00 RID: 3072
		private int _worldFilter;

		// Token: 0x04000C01 RID: 3073
		private int _worldPerson = -1;

		// Token: 0x04000C02 RID: 3074
		private WorldEvent _peopleFor;

		// Token: 0x04000C03 RID: 3075
		private int _peopleStart;

		// Token: 0x04000C04 RID: 3076
		private int _worldMouseTop = -1;

		// Token: 0x04000C05 RID: 3077
		[TupleElementNames(new string[]
		{
			"s",
			"at"
		})]
		private readonly Dictionary<string, ValueTuple<string, int>> _standingCache = new Dictionary<string, ValueTuple<string, int>>();

		// Token: 0x04000C06 RID: 3078
		private static readonly float[] ZoomLevels = new float[]
		{
			0.55f,
			0.65f,
			0.75f,
			0.85f,
			1f,
			1.15f,
			1.3f
		};

		// Token: 0x04000C07 RID: 3079
		private int _zoomIndex = -1;

		// Token: 0x04000C08 RID: 3080
		private float _z = 1f;

		// Token: 0x04000C09 RID: 3081
		private float _scrollY;

		// Token: 0x04000C0A RID: 3082
		private const float AreaBottom = 0.848f;

		// Token: 0x04000C0B RID: 3083
		private const float AreaTop = 0.27299997f;

		// Token: 0x04000C0C RID: 3084
		private bool _listAll;

		// Token: 0x04000C0D RID: 3085
		private List<RosterMenu.ListEntry> _entriesCache;

		// Token: 0x04000C0E RID: 3086
		[TupleElementNames(new string[]
		{
			"all",
			"count"
		})]
		private ValueTuple<bool, int> _entriesKey;

		// Token: 0x04000C0F RID: 3087
		private int _entriesAt = -100000;

		// Token: 0x04000C10 RID: 3088
		private float _rowH = 0.032f;

		// Token: 0x04000C11 RID: 3089
		private float _rowGrow;

		// Token: 0x04000C12 RID: 3090
		private const string StarDict = "generic_textures";

		// Token: 0x04000C13 RID: 3091
		private bool _leanStat;

		// Token: 0x04000C14 RID: 3092
		private bool _leanTitle;

		// Token: 0x04000C15 RID: 3093
		private bool _rawNames;

		// Token: 0x04000C16 RID: 3094
		private double _textLogAt;

		// Token: 0x04000C17 RID: 3095
		private string _textLogFor;

		// Token: 0x04000C18 RID: 3096
		private float _leftEnd;

		// Token: 0x04000C19 RID: 3097
		private bool _mindView;

		// Token: 0x04000C1A RID: 3098
		private string _gapFor;

		// Token: 0x04000C1B RID: 3099
		private float _rowGap;

		// Token: 0x04000C1C RID: 3100
		private float _secGap;

		// Token: 0x04000C1D RID: 3101
		private bool _crimeView;

		// Token: 0x04000C1E RID: 3102
		private const int HistHoldSlots = 44;

		// Token: 0x04000C1F RID: 3103
		private int _histHold;

		// Token: 0x04000C20 RID: 3104
		private readonly HashSet<string> _pageNoted = new HashSet<string>();

		// Token: 0x04000C21 RID: 3105
		private const float WorldRowH = 0.032f;

		// Token: 0x04000C22 RID: 3106
		private WorldEvent _storyFor;

		// Token: 0x04000C23 RID: 3107
		private ValueTuple<WorldEvent, int, bool, long> _storyKey;

		// Token: 0x04000C24 RID: 3108
		private List<string> _story = new List<string>();

		// Token: 0x04000C25 RID: 3109
		private int _warSel;

		// Token: 0x04000C26 RID: 3110
		private int _warEvScroll;

		// Token: 0x04000C27 RID: 3111
		private int _warEvFor = -1;

		// Token: 0x04000C28 RID: 3112
		private int _warEvCount;

		// Token: 0x02000425 RID: 1061
		private enum Tier
		{
			// Token: 0x04001F0A RID: 7946
			Leader,
			// Token: 0x04001F0B RID: 7947
			Boss,
			// Token: 0x04001F0C RID: 7948
			Members,
			// Token: 0x04001F0D RID: 7949
			Fallen
		}

		// Token: 0x02000426 RID: 1062
		private sealed class Row
		{
			// Token: 0x17000385 RID: 901
			// (get) Token: 0x060024F0 RID: 9456 RVA: 0x0017F9A2 File Offset: 0x0017DBA2
			public int VisibleCount
			{
				get
				{
					if (!this.FixedSeats)
					{
						return Math.Min(this.Slots, this.Items.Count);
					}
					return this.Slots;
				}
			}

			// Token: 0x04001F0E RID: 7950
			public RosterMenu.Tier Tier;

			// Token: 0x04001F0F RID: 7951
			public string Label;

			// Token: 0x04001F10 RID: 7952
			public string Standing;

			// Token: 0x04001F11 RID: 7953
			public string LoreName;

			// Token: 0x04001F12 RID: 7954
			public string LoreBlurb;

			// Token: 0x04001F13 RID: 7955
			public bool LoreVacant;

			// Token: 0x04001F14 RID: 7956
			public float Y;

			// Token: 0x04001F15 RID: 7957
			public int Slots;

			// Token: 0x04001F16 RID: 7958
			public bool FixedSeats;

			// Token: 0x04001F17 RID: 7959
			public List<NemesisRecord> Items;
		}

		// Token: 0x02000427 RID: 1063
		private sealed class ListEntry
		{
			// Token: 0x04001F18 RID: 7960
			public NemesisRecord Rec;

			// Token: 0x04001F19 RID: 7961
			public Acquaintance Acq;
		}

		// Token: 0x02000428 RID: 1064
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04001F1A RID: 7962
			public static Func<string, string> <0>__SkillText;

			// Token: 0x04001F1B RID: 7963
			public static Func<PastEntry, bool> <1>__PastShown;
		}
	}
}
