using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x02000082 RID: 130
	internal static class WelcomePics
	{
		// Token: 0x17000320 RID: 800
		// (get) Token: 0x0600138D RID: 5005 RVA: 0x001364BE File Offset: 0x001346BE
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		private static ValueTuple<int, int, int> RealGold
		{
			[return: TupleElementNames(new string[]
			{
				"r",
				"g",
				"b"
			})]
			get
			{
				return new ValueTuple<int, int, int>(212, 176, 82);
			}
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x001364D4 File Offset: 0x001346D4
		public static void Draw(int page, float x, float y, float w, float h)
		{
			switch (page)
			{
			case 0:
				WelcomePics.Factions(x, y, w, h);
				return;
			case 1:
				WelcomePics.Menu(x, y, w, h);
				return;
			case 2:
				WelcomePics.Grudge(x, y, w, h);
				return;
			case 3:
				WelcomePics.Tempers(x, y, w, h);
				return;
			case 4:
				WelcomePics.Hierarchy(x, y, w, h);
				return;
			case 5:
				WelcomePics.Tension(x, y, w, h);
				return;
			case 6:
				WelcomePics.Interrogate(x, y, w, h);
				return;
			case 7:
				WelcomePics.Camps(x, y, w, h);
				return;
			case 8:
				WelcomePics.World(x, y, w, h);
				return;
			case 9:
				WelcomePics.NameCard(x, y, w, h);
				return;
			case 10:
				WelcomePics.XRay(x, y, w, h);
				return;
			case 11:
				WelcomePics.Wordmark(x, y, w, h);
				return;
			default:
				return;
			}
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0013659C File Offset: 0x0013479C
		private static void TitleC(string s, float cx, float y, float sc, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> c)
		{
			UiDraw.TextFont(s, UiDraw.TitleFont, cx - Theme.TitleWidth(s, sc) / 2f, y, sc, c.Item1, c.Item2, c.Item3, 255, false);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x001365E0 File Offset: 0x001347E0
		private static void BodyC(string s, float cx, float y, float sc, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> c)
		{
			UiDraw.Text(s, cx - Theme.MixedWidth(s, sc, 0.0222f) / 2f, y, sc, c.Item1, c.Item2, c.Item3, 255);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x00136618 File Offset: 0x00134818
		private static void Bar(float x, float y, float w, float h, float frac, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> c)
		{
			UiDraw.Rect(x, y, w, h, Theme.Faint.Item1, Theme.Faint.Item2, Theme.Faint.Item3, 120);
			UiDraw.Rect(x, y, w * Math.Max(0f, Math.Min(1f, frac)), h, c.Item1, c.Item2, c.Item3, 235);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x00136688 File Offset: 0x00134888
		private static void Tick(float x, float y, float h, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> c)
		{
			UiDraw.Rect(x - 0.00052083336f, y - 0.004f, 0.0010416667f, h + 0.008f, c.Item1, c.Item2, c.Item3, 255);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x001366C0 File Offset: 0x001348C0
		private static void Stars(float x, float cy, int rank, float sh)
		{
			float num = sh / UiDraw.Aspect;
			bool flag = UiDraw.EnsureTextureDict("generic_textures");
			for (int i = 0; i < 5; i++)
			{
				float num2 = x + (float)i * (num + 0.0008f) + num / 2f;
				bool flag2 = i < rank;
				if (flag)
				{
					UiDraw.Sprite("generic_textures", "star", num2, cy, num, sh, 0f, flag2 ? Theme.Gold.Item1 : 78, flag2 ? Theme.Gold.Item2 : 75, flag2 ? Theme.Gold.Item3 : 70, flag2 ? 255 : 235);
				}
				else
				{
					UiDraw.Rect(num2 - num * 0.3f, cy - sh * 0.3f, num * 0.6f, sh * 0.6f, flag2 ? Theme.Gold.Item1 : 70, flag2 ? Theme.Gold.Item2 : 68, flag2 ? Theme.Gold.Item3 : 64, 230);
				}
			}
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x001367D0 File Offset: 0x001349D0
		private static void Card(float x, float y, float w, float h, string name, int rank, [TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] ValueTuple<int, int, int> hate, bool selected = false)
		{
			UiDraw.Rect(x, y, w, h, 24, 23, 21, 235);
			Theme.Hairline(x, y, w, h, hate.Item1, hate.Item2, hate.Item3, 255, 2);
			if (selected)
			{
				Theme.RedFrame(x, y, w, h, 0.95f, 5);
			}
			UiDraw.TextFont(name, UiDraw.TitleFont, x + 0.005f, y + 0.004f, 0.18f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255, false);
			if (rank > 0)
			{
				WelcomePics.Stars(x + w - 0.005f - 5f * (0.0115f / UiDraw.Aspect + 0.0008f), y + h - 0.009f, rank, 0.0115f);
			}
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x001368A8 File Offset: 0x00134AA8
		private static void Factions(float x, float y, float w, float h)
		{
			IEnumerable<string> source = new string[]
			{
				"odriscoll",
				"lemoyneraiders",
				"army",
				"wapiti",
				"bronte",
				"klan",
				"murfree",
				"skinners"
			};
			Func<string, Gang> selector;
			if ((selector = WelcomePics.<>O.<0>__ById) == null)
			{
				selector = (WelcomePics.<>O.<0>__ById = new Func<string, Gang>(Gangs.ById));
			}
			List<Gang> list = (from g in source.Select(selector)
			where g != null
			select g).ToList<Gang>();
			WelcomePics.TitleC("OCHO BANDAS: CADA UNA CON SUS PROPIOS RANGOS, CAMPAMENTOS, LÍDER Y GUERRAS", x + w / 2f, y + 0.012f, 0.22f, Theme.Dim);
			float num = w / 4f;
			for (int i = 0; i < list.Count; i++)
			{
				Gang gang = list[i];
				float num2 = x + num * (float)(i % 4) + num / 2f;
				float num3 = y + 0.05f + (float)(i / 4) * 0.042f;
				string s = gang.Name.ToUpperInvariant();
				WelcomePics.TitleC(s, num2, num3, 0.25f, Theme.Title);
				float num4 = Theme.TitleWidth(s, 0.25f);
				UiDraw.Rect(num2 - num4 / 2f, num3 + 0.024f, num4, 0.0027777776f, gang.Accent.Item1, gang.Accent.Item2, gang.Accent.Item3, 255);
			}
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x00136A28 File Offset: 0x00134C28
		private static void Menu(float x, float y, float w, float h)
		{
			float num = x + 0.03f;
			float num2 = y + 0.03f;
			string[] array = new string[]
			{
				"JERARQUÍA",
				"MUNDO",
				"RAYOS X"
			};
			for (int i = 0; i < array.Length; i++)
			{
				ValueTuple<int, int, int> valueTuple = (i == 0) ? Theme.Title : Theme.Dim;
				UiDraw.TextFont(array[i], UiDraw.TitleFont, num, num2, 0.34f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255, i == 0);
				num += Theme.CapsWidth(array[i], 0.34f) + 0.03f;
			}
			WelcomePics.BodyC("un menú, tres páginas: Tab / Vista cambia entre ellas", x + 0.03f + (num - x - 0.06f) / 2f, num2 + 0.05f, 0.22f, Theme.Dim);
			bool recentlyUsed = XPad.RecentlyUsed;
			List<ValueTuple<string, string>> list2;
			if (!recentlyUsed)
			{
				List<ValueTuple<string, string>> list = new List<ValueTuple<string, string>>();
				list.Add(new ValueTuple<string, string>(KeyCombo.MenuText, "Abrir"));
				list.Add(new ValueTuple<string, string>("Tab", "Pestañas"));
				list.Add(new ValueTuple<string, string>("Q/E", "Bandas"));
				list.Add(new ValueTuple<string, string>("Enter", "Su ficha"));
				list2 = list;
				list.Add(new ValueTuple<string, string>("Backspace", "Atrás"));
			}
			else
			{
				List<ValueTuple<string, string>> list3 = new List<ValueTuple<string, string>>();
				list3.Add(KeyCombo.PadOpenHint);
				list3.Add(new ValueTuple<string, string>("Vista", "Pestañas"));
				list3.Add(new ValueTuple<string, string>("LB/RB", "Bandas"));
				list3.Add(new ValueTuple<string, string>("A", "Su ficha"));
				list2 = list3;
				list3.Add(new ValueTuple<string, string>("B", "Atrás"));
			}
			List<ValueTuple<string, string>> hints = list2;
			Theme.HintRow(x + w * 0.52f, x + w - 0.02f, y + 0.04f, hints, 0.23f, Theme.Text);
			List<ValueTuple<string, string>> list5;
			if (!recentlyUsed)
			{
				List<ValueTuple<string, string>> list4 = new List<ValueTuple<string, string>>();
				list4.Add(new ValueTuple<string, string>("N", "Mensajes"));
				list4.Add(new ValueTuple<string, string>("C", "Tarjetas de nombre"));
				list4.Add(new ValueTuple<string, string>("G", "Esta guía"));
				list5 = list4;
				list4.Add(new ValueTuple<string, string>("O", "Ajustes"));
			}
			else
			{
				List<ValueTuple<string, string>> list6 = new List<ValueTuple<string, string>>();
				list6.Add(new ValueTuple<string, string>("LS", "Mensajes"));
				list6.Add(new ValueTuple<string, string>("RS", "Tarjetas de nombre"));
				list6.Add(new ValueTuple<string, string>("Y", "Esta guía"));
				list5 = list6;
				list6.Add(new ValueTuple<string, string>("Empezar", "Ajustes"));
			}
			List<ValueTuple<string, string>> hints2 = list5;
			Theme.HintRow(x + w * 0.52f, x + w - 0.02f, y + 0.085f, hints2, 0.23f, Theme.Text);
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x00136CF0 File Offset: 0x00134EF0
		private static void Grudge(float x, float y, float w, float h)
		{
			float num = x + 0.03f;
			float num2 = w * 0.46f;
			float num3 = y + 0.05f;
			UiDraw.TextFont("EL RENCOR DE UN DESCONOCIDO", UiDraw.TitleFont, num, y + 0.014f, 0.22f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
			WelcomePics.Bar(num, num3, num2, 0.01f, 0.72f, Theme.Blood);
			WelcomePics.Tick(num + num2 * 0.25f, num3, 0.01f, Theme.Dim);
			WelcomePics.Tick(num + num2 * 0.6f, num3, 0.01f, WelcomePics.RealGold);
			UiDraw.Text("puede que lo deje pasar", num + num2 * 0.25f - 0.004f, num3 + 0.016f, 0.19f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			UiDraw.Text("némesis", num + num2 * 0.6f - 0.004f, num3 + 0.016f, 0.19f, WelcomePics.RealGold.Item1, WelcomePics.RealGold.Item2, WelcomePics.RealGold.Item3, 255);
			float num4 = num;
			foreach (ValueTuple<string, ValueTuple<int, int, int>> valueTuple in new ValueTuple<string, ValueTuple<int, int, int>>[]
			{
				new ValueTuple<string, ValueTuple<int, int, int>>("+15 PROVOCADO", Theme.Warn),
				new ValueTuple<string, ValueTuple<int, int, int>>("+45 ROBADO", Theme.Blood),
				new ValueTuple<string, ValueTuple<int, int, int>>("+8 GOLPEADO", Theme.Warn),
				new ValueTuple<string, ValueTuple<int, int, int>>("-6 SALUDADO", Theme.Good)
			})
			{
				string item = valueTuple.Item1;
				ValueTuple<int, int, int> item2 = valueTuple.Item2;
				num4 += Theme.Tag(item, num4, num3 + 0.05f, item2) + 0.008f;
			}
			float num5 = x + w * 0.56f;
			float num6 = w * 0.4f / 3f - 0.008f;
			UiDraw.TextFont("SU ODIO", UiDraw.TitleFont, num5, y + 0.014f, 0.22f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
			ValueTuple<string, HateTier, string>[] array2 = new ValueTuple<string, HateTier, string>[]
			{
				new ValueTuple<string, HateTier, string>("RESENTIDO", HateTier.Resentful, "puños"),
				new ValueTuple<string, HateTier, string>("VENGATIVO", HateTier.Vengeful, "cuchillo, quizá un arma"),
				new ValueTuple<string, HateTier, string>("SANGUINARIO", HateTier.Murderous, "quiere verte muerto")
			};
			for (int j = 0; j < 3; j++)
			{
				float num7 = num5 + (float)j * (num6 + 0.012f);
				Theme.Badge(array2[j].Item1, num7, y + 0.046f, num6, UiDraw.HateColor(array2[j].Item2), true);
				WelcomePics.BodyC(array2[j].Item3, num7 + num6 / 2f, y + 0.08f, 0.2f, Theme.Dim);
			}
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x00137008 File Offset: 0x00135208
		private static void Tempers(float x, float y, float w, float h)
		{
			Personality[] array = new Personality[]
			{
				Personality.Hothead,
				Personality.Proud,
				Personality.Cold,
				Personality.Coward,
				Personality.Greedy,
				Personality.Loyal,
				Personality.Drifter
			};
			float num = w / (float)array.Length;
			for (int i = 0; i < array.Length; i++)
			{
				ValueTuple<int, int, int> valueTuple = NemesisMod.TemperColor(array[i]);
				float num2 = x + num * (float)i + num / 2f;
				float w2 = 0.032f / UiDraw.Aspect;
				float num3 = y + (h - 0.060000002f) / 2f;
				if (UiDraw.EnsureTextureDict("itemtype_textures"))
				{
					UiDraw.Sprite("itemtype_textures", Theme.TemperIcon(new Personality?(array[i])), num2 + 0.0015f, num3 + 0.016f, w2, 0.032f, 0f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255);
				}
				WelcomePics.TitleC(array[i].ToString().ToUpperInvariant(), num2, num3 + 0.032f + 0.01f - 0.004f, 0.24f, valueTuple);
			}
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x00137108 File Offset: 0x00135308
		private static void Hierarchy(float x, float y, float w, float h)
		{
			float num = 0.09f;
			float num2 = 0.032f;
			float num3 = x + w * 0.3f;
			ValueTuple<int, int, int> valueTuple = UiDraw.HateColor(HateTier.Murderous);
			ValueTuple<int, int, int> valueTuple2 = UiDraw.HateColor(HateTier.Vengeful);
			ValueTuple<int, int, int> valueTuple3 = UiDraw.HateColor(HateTier.Resentful);
			float num4 = y + 0.012f;
			UiDraw.Rect(num3 - num / 2f, num4, num, num2, 24, 23, 21, 235);
			Theme.Hairline(num3 - num / 2f, num4, num, num2, WelcomePics.RealGold.Item1, WelcomePics.RealGold.Item2, WelcomePics.RealGold.Item3, 255, 2);
			UiDraw.TextFont("PUESTO VACANTE", UiDraw.TitleFont, num3 - Theme.CapsWidth("PUESTO VACANTE", 0.18f) / 2f, num4 + 0.008f, 0.18f, Theme.Warn.Item1, Theme.Warn.Item2, Theme.Warn.Item3, 255, false);
			float num5 = num4 + num2 + 0.012f;
			float y2 = num5 + num2 + 0.012f;
			UiDraw.Rect(num3 - 0.00052083336f, num4 + num2, 0.0010416667f, 0.008f, Theme.Rule.Item1, Theme.Rule.Item2, Theme.Rule.Item3, 160);
			UiDraw.Rect(num3 - num * 0.6f, num4 + num2 + 0.008f, num * 1.2f, 0.0009259259f, Theme.Rule.Item1, Theme.Rule.Item2, Theme.Rule.Item3, 160);
			WelcomePics.Card(num3 - num * 1.1f, num5, num, num2, "Maj. Stockton", 4, valueTuple, true);
			WelcomePics.Card(num3 + num * 0.1f, num5, num, num2, "Capt. Rourke", 4, valueTuple, false);
			float num6 = num3 - 2f * num - 0.012f;
			UiDraw.Rect(num6 + num / 2f, num5 + num2 + 0.008f, 3f * num + 0.036f, 0.0009259259f, Theme.Rule.Item1, Theme.Rule.Item2, Theme.Rule.Item3, 160);
			string[] array = new string[]
			{
				"Sgt. Denton",
				"Sgt. Dillard",
				"Sgt. Eatlin",
				"Sgt. Ketchum"
			};
			ValueTuple<int, int, int>[] array2 = new ValueTuple<int, int, int>[]
			{
				valueTuple,
				valueTuple2,
				valueTuple2,
				valueTuple3
			};
			int[] array3 = new int[]
			{
				3,
				3,
				2,
				2
			};
			for (int i = 0; i < 4; i++)
			{
				WelcomePics.Card(num6 + (float)i * (num + 0.012f), y2, num, num2, array[i], array3[i], array2[i], false);
			}
			float num7 = x + w * 0.62f;
			float num8 = w * 0.34f;
			float num9 = y + 0.04f;
			UiDraw.TextFont("NOTORIEDAD", UiDraw.TitleFont, num7, num9 - 0.026f, 0.22f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
			int[] array4 = new int[]
			{
				10,
				15,
				25,
				25,
				25
			};
			float num10 = num7;
			for (int j = 0; j < 5; j++)
			{
				float num11 = num8 * (float)array4[j] / 100f - 0.002f;
				ValueTuple<int, int, int> valueTuple4 = Notoriety.LevelColors[j];
				UiDraw.Rect(num10, num9, num11, 0.01f, valueTuple4.Item1, valueTuple4.Item2, valueTuple4.Item3, (j <= 3) ? 235 : 90);
				num10 += num11 + 0.002f;
			}
			string[] array5 = new string[]
			{
				"Desconocido",
				"Visto",
				"Buscado",
				"Perseguido",
				"Deuda de sangre"
			};
			WelcomePics.BodyC("Desconocido - Visto - Buscado - Perseguido - Deuda de sangre", num7 + num8 / 2f, num9 + 0.02f, 0.19f, Theme.Dim);
			WelcomePics.BodyC("mata a sus hombres y aprenderán tu nombre", num7 + num8 / 2f, num9 + 0.05f, 0.2f, Theme.Text);
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x00137530 File Offset: 0x00135730
		private static void Tension(float x, float y, float w, float h)
		{
			float num = x + 0.03f;
			float num2 = w * 0.5f;
			float num3 = y + 0.022f;
			float num4 = 0.09f;
			float num5 = 0.7f;
			UiDraw.TextFont("TENSIÓN", UiDraw.TitleFont, num, y + 0.006f, 0.2f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
			int num6 = 34;
			float num7 = num2 / (float)num6;
			for (int i = 0; i < num6; i++)
			{
				float num8 = (i < 24) ? (0.18f + (float)i * 0.023f) : ((i == 24) ? 0.74f : (0.3f + (float)(i - 25) * 0.02f));
				ValueTuple<int, int, int> valueTuple = (num8 >= num5) ? Theme.Blood : ((num8 >= num5 * 0.75f) ? Theme.Warn : Theme.Faint);
				float num9 = num4 * num8;
				UiDraw.Rect(num + (float)i * num7, num3 + num4 - num9, num7 - 0.0012f, num9, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 230);
			}
			UiDraw.Rect(num, num3 + num4 * (1f - num5), num2, 0.0018518518f, WelcomePics.RealGold.Item1, WelcomePics.RealGold.Item2, WelcomePics.RealGold.Item3, 255);
			UiDraw.Text("alguien aparece", num + num7 * 22f, num3 + num4 * (1f - num5) - 0.024f, 0.19f, WelcomePics.RealGold.Item1, WelcomePics.RealGold.Item2, WelcomePics.RealGold.Item3, 255);
			float num10 = x + w * 0.6f;
			float num11 = y + 0.026f;
			UiDraw.TextFont("EL CARA A CARA", UiDraw.TitleFont, num10, num11 - 0.016f, 0.22f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
			Theme.Tag("SALUDAR", num10, num11 + 0.016f, Theme.Good);
			UiDraw.Text("puede echarse atrás", num10 + 0.08f, num11 + 0.018f, 0.21f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
			Theme.Tag("PROVOCAR", num10, num11 + 0.046f, Theme.Blood);
			UiDraw.Text("empieza la pelea", num10 + 0.08f, num11 + 0.048f, 0.21f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
			Theme.Tag("DESENFUNDAR / DISPARAR", num10, num11 + 0.076f, Theme.Blood);
			UiDraw.Text("empieza la pelea", num10 + 0.08f, num11 + 0.078f, 0.21f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x00137854 File Offset: 0x00135A54
		private static void Interrogate(float x, float y, float w, float h)
		{
			float num = x + 0.04f;
			float num2 = y + 0.022f;
			float num3 = 0.2f;
			UiDraw.Rect(num, num2, num3, 0.098f, 0, 0, 0, 170);
			string[] array = new string[]
			{
				"Recoger",
				"Desatar",
				"Interrogar"
			};
			string[] array2 = new string[]
			{
				"DLeft",
				"DRight",
				"DUp"
			};
			for (int i = 0; i < 3; i++)
			{
				float num4 = num2 + 0.01f + (float)i * 0.026f;
				ValueTuple<int, int, int> valueTuple = (i == 2) ? Theme.Title : Theme.Dim;
				UiDraw.TextFont(array[i], UiDraw.TitleFont, num + 0.012f, num4, 0.22f, valueTuple.Item1, valueTuple.Item2, valueTuple.Item3, 255, false);
				Theme.Glyph(array2[i], num + num3 - 0.012f - Theme.GlyphWidth(array2[i]), num4 - 0.001f);
			}
			UiDraw.Rect(num + 0.008f, num2 + 0.084f, num3 - 0.016f, 0.0009259259f, Theme.Rule.Item1, Theme.Rule.Item2, Theme.Rule.Item3, 170);
			UiDraw.Text("Cruceta arriba - o I en el teclado", num, num2 + 0.104f, 0.19f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			float num5 = x + w * 0.42f;
			float num6 = w * 0.52f;
			float num7 = y + 0.046f;
			UiDraw.TextFont("MIEDO", UiDraw.TitleFont, num5, num7 - 0.03f, 0.24f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			WelcomePics.Bar(num5, num7, num6, 0.008f, 0.58f, Theme.Warn);
			WelcomePics.Tick(num5 + num6 * 0.45f, num7, 0.008f, WelcomePics.RealGold);
			UiDraw.Text("lo bastante asustado para hablar", num5 + num6 * 0.45f - 0.004f, num7 + 0.014f, 0.19f, WelcomePics.RealGold.Item1, WelcomePics.RealGold.Item2, WelcomePics.RealGold.Item3, 255);
			float num8 = num5;
			foreach (ValueTuple<string, ValueTuple<int, int, int>> valueTuple2 in new ValueTuple<string, ValueTuple<int, int, int>>[]
			{
				new ValueTuple<string, ValueTuple<int, int, int>>("DISPARO CERCANO", Theme.Warn),
				new ValueTuple<string, ValueTuple<int, int, int>>("GOLPEADO", Theme.Gold),
				new ValueTuple<string, ValueTuple<int, int, int>>("ENCAÑONADO", Theme.Dim),
				new ValueTuple<string, ValueTuple<int, int, int>>("VIO UNA MUERTE", Theme.Blood)
			})
			{
				string item = valueTuple2.Item1;
				ValueTuple<int, int, int> item2 = valueTuple2.Item2;
				num8 += Theme.Tag(item, num8, num7 + 0.046f, item2) + 0.008f;
			}
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x00137B70 File Offset: 0x00135D70
		private static void Camps(float x, float y, float w, float h)
		{
			bool flag = UiDraw.EnsureTextureDict("blips");
			ValueTuple<string, string, string>[] array = new ValueTuple<string, string, string>[]
			{
				new ValueTuple<string, string, string>("blip_region_hideout", "UN CAMPAMENTO O GUARIDA", "descubierto interrogando"),
				new ValueTuple<string, string, string>("blip_ambient_secret", "UN CAMPO DE BATALLA", "los muertos y los supervivientes")
			};
			for (int i = 0; i < array.Length; i++)
			{
				float num = x + 0.04f;
				float num2 = y + 0.022f + (float)i * 0.052f;
				float num3 = 0.036f;
				if (flag)
				{
					UiDraw.Sprite("blips", array[i].Item1, num + num3 / UiDraw.Aspect / 2f, num2 + num3 / 2f, num3 / UiDraw.Aspect, num3, 0f, 235, 232, 225, 255);
				}
				else
				{
					Theme.Disc(num + num3 / UiDraw.Aspect / 2f, num2 + num3 / 2f, num3 * 0.7f, 200, 60, 50, 255);
				}
				UiDraw.TextFont(array[i].Item2, UiDraw.TitleFont, num + 0.036f, num2 + 0.002f, 0.22f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
				UiDraw.Text(array[i].Item3, num + 0.036f, num2 + 0.02f, 0.19f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			}
			float num4 = x + w * 0.44f;
			float num5 = w * 0.5f;
			float num6 = y + 0.05f;
			UiDraw.TextFont("SOSPECHA", UiDraw.TitleFont, num4, num6 - 0.03f, 0.24f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			WelcomePics.Bar(num4, num6, num5, 0.008f, 0.38f, Theme.Warn);
			WelcomePics.Tick(num4 + num5 * 0.45f, num6, 0.008f, Theme.Warn);
			WelcomePics.Tick(num4 + num5, num6, 0.008f, Theme.Blood);
			UiDraw.Text("un hombre da la voz de alarma", num4 + num5 * 0.45f - 0.03f, num6 + 0.014f, 0.19f, Theme.Warn.Item1, Theme.Warn.Item2, Theme.Warn.Item3, 255);
			UiDraw.Text("en alerta", num4 + num5 - 0.03f, num6 + 0.014f, 0.19f, Theme.Blood.Item1, Theme.Blood.Item2, Theme.Blood.Item3, 255);
			WelcomePics.BodyC("agáchate, acércate de noche o con niebla y mantente fuera de la vista", num4 + num5 / 2f, num6 + 0.048f, 0.21f, Theme.Text);
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x00137E80 File Offset: 0x00136080
		private static void World(float x, float y, float w, float h)
		{
			float num = x + 0.03f;
			float num2 = w * 0.56f;
			float num3 = y + 0.016f;
			ValueTuple<string, string, bool>[] array = new ValueTuple<string, string, bool>[]
			{
				new ValueTuple<string, string, bool>("Feb 23 18:53", "Mataste al soldado Atwood y al soldado Spurlock  -  2 muertos", false),
				new ValueTuple<string, string, bool>("Feb 23 18:07", "El coronel Henry Favours murió en Fort Wallace  -  1 muerto", true),
				new ValueTuple<string, string, bool>("Feb 23 16:40", "La Camada de Murfree atacó a los O'Driscoll a las afueras de Annesburg", false)
			};
			for (int i = 0; i < array.Length; i++)
			{
				float num4 = num3 + (float)i * 0.03f;
				UiDraw.Rect(num, num4, 0.0015625001f, 0.024f, WelcomePics.RealGold.Item1, WelcomePics.RealGold.Item2, WelcomePics.RealGold.Item3, 230);
				if (array[i].Item3)
				{
					Theme.SelectFill(num + 0.004f, num4, num2 - 0.004f, 0.024f, 235);
					Theme.RedFrame(num + 0.004f, num4, num2 - 0.004f, 0.024f, 0.9f, 3);
				}
				UiDraw.Text(array[i].Item1, num + 0.012f, num4 + 0.004f, 0.18f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
				UiDraw.Text(array[i].Item2, num + 0.09f, num4 + 0.004f, 0.2f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
			}
			float num5 = x + w * 0.62f;
			float num6 = w * 0.34f;
			WelcomePics.TitleC("EN GUERRA", num5 + num6 / 2f, y + 0.02f, 0.26f, Theme.Warn);
			WelcomePics.BodyC("Bayou Nwa: Lemoyne Raiders contra Familia Bronte", num5 + num6 / 2f, y + 0.052f, 0.2f, Theme.Warn);
			WelcomePics.TitleC("?", num5 + num6 / 2f, y + 0.074f, 0.4f, Theme.Title);
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x001380B4 File Offset: 0x001362B4
		private static void NameCard(float x, float y, float w, float h)
		{
			float num = x + w * 0.27f;
			WelcomePics.TitleC("Silas Boone", num, y + 0.016f, 0.3f, Theme.Title);
			WelcomePics.TitleC("Impulsivo - Valentine", num, y + 0.046f, 0.22f, Theme.Dim);
			float num2 = 0.12f;
			float num3 = num - num2 / 2f;
			UiDraw.Text("MIEDO", num3 - 0.04f, y + 0.074f, 0.18f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			WelcomePics.Bar(num3, y + 0.079f, num2, 0.006f, 0.35f, new ValueTuple<int, int, int>(110, 150, 205));
			UiDraw.Text("APRECIO", num3 - 0.04f, y + 0.092f, 0.18f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255);
			WelcomePics.Bar(num3, y + 0.097f, num2, 0.006f, 0.6f, Theme.Good);
			float num4 = x + w * 0.58f;
			UiDraw.TextFont("REPUTACIÓN EN EL PUEBLO", UiDraw.TitleFont, num4, y + 0.014f, 0.22f, Theme.Dim.Item1, Theme.Dim.Item2, Theme.Dim.Item3, 255, false);
			ValueTuple<string, string, ValueTuple<int, int, int>>[] array = new ValueTuple<string, string, ValueTuple<int, int, int>>[]
			{
				new ValueTuple<string, string, ValueTuple<int, int, int>>("Valentine", "+34  Apreciado", Theme.Good),
				new ValueTuple<string, string, ValueTuple<int, int, int>>("Rhodes", "+5  Conocido", Theme.Text),
				new ValueTuple<string, string, ValueTuple<int, int, int>>("Saint Denis", "-61  Odiado", Theme.Blood)
			};
			for (int i = 0; i < array.Length; i++)
			{
				float y2 = y + 0.042f + (float)i * 0.026f;
				UiDraw.TextFont(array[i].Item1, UiDraw.TitleFont, num4, y2, 0.22f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255, false);
				UiDraw.TextFont(array[i].Item2, UiDraw.TitleFont, num4 + 0.12f, y2, 0.22f, array[i].Item3.Item1, array[i].Item3.Item2, array[i].Item3.Item3, 255, false);
			}
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x00138354 File Offset: 0x00136554
		private static void XRay(float x, float y, float w, float h)
		{
			WelcomePics.<>c__DisplayClass19_0 CS$<>8__locals1;
			CS$<>8__locals1.cx = x + w * 0.2f;
			CS$<>8__locals1.top = y + 0.012f;
			CS$<>8__locals1.s = 0.11f;
			CS$<>8__locals1.bone = new ValueTuple<int, int, int>(196, 196, 190);
			CS$<>8__locals1.a = UiDraw.Aspect;
			Theme.Disc(WelcomePics.<XRay>g__X|19_0(0f, ref CS$<>8__locals1), WelcomePics.<XRay>g__Y|19_1(0.06f, ref CS$<>8__locals1), 0.02f, CS$<>8__locals1.bone.Item1, CS$<>8__locals1.bone.Item2, CS$<>8__locals1.bone.Item3, 255);
			Theme.Disc(WelcomePics.<XRay>g__X|19_0(0f, ref CS$<>8__locals1), WelcomePics.<XRay>g__Y|19_1(0.06f, ref CS$<>8__locals1), 0.012f, 12, 12, 11, 255);
			WelcomePics.<XRay>g__L|19_2(0f, 0.11f, 0f, 0.52f, ref CS$<>8__locals1);
			for (int i = 0; i < 5; i++)
			{
				float num = 0.2f + (float)i * 0.045f;
				WelcomePics.<XRay>g__L|19_2(0f, num, -0.11f + (float)i * 0.006f, num + 0.025f, ref CS$<>8__locals1);
				WelcomePics.<XRay>g__L|19_2(0f, num, 0.11f - (float)i * 0.006f, num + 0.025f, ref CS$<>8__locals1);
			}
			WelcomePics.<XRay>g__L|19_2(-0.13f, 0.16f, 0.13f, 0.16f, ref CS$<>8__locals1);
			WelcomePics.<XRay>g__L|19_2(-0.13f, 0.16f, -0.17f, 0.36f, ref CS$<>8__locals1);
			WelcomePics.<XRay>g__L|19_2(-0.17f, 0.36f, -0.19f, 0.54f, ref CS$<>8__locals1);
			WelcomePics.<XRay>g__L|19_2(0.13f, 0.16f, 0.17f, 0.36f, ref CS$<>8__locals1);
			WelcomePics.<XRay>g__L|19_2(0.17f, 0.36f, 0.19f, 0.54f, ref CS$<>8__locals1);
			WelcomePics.<XRay>g__L|19_2(-0.08f, 0.52f, 0.08f, 0.52f, ref CS$<>8__locals1);
			WelcomePics.<XRay>g__L|19_2(-0.07f, 0.52f, -0.08f, 0.76f, ref CS$<>8__locals1);
			WelcomePics.<XRay>g__L|19_2(-0.08f, 0.76f, -0.08f, 0.99f, ref CS$<>8__locals1);
			WelcomePics.<XRay>g__L|19_2(0.07f, 0.52f, 0.08f, 0.76f, ref CS$<>8__locals1);
			WelcomePics.<XRay>g__L|19_2(0.08f, 0.76f, 0.08f, 0.99f, ref CS$<>8__locals1);
			UiDraw.Line(WelcomePics.<XRay>g__X|19_0(-0.32f, ref CS$<>8__locals1), WelcomePics.<XRay>g__Y|19_1(0.22f, ref CS$<>8__locals1), WelcomePics.<XRay>g__X|19_0(-0.05f, ref CS$<>8__locals1), WelcomePics.<XRay>g__Y|19_1(0.29f, ref CS$<>8__locals1), 0.0012f, 150, 60, 50, 160, 40);
			UiDraw.Line(WelcomePics.<XRay>g__X|19_0(-0.05f, ref CS$<>8__locals1), WelcomePics.<XRay>g__Y|19_1(0.29f, ref CS$<>8__locals1), WelcomePics.<XRay>g__X|19_0(0.1f, ref CS$<>8__locals1), WelcomePics.<XRay>g__Y|19_1(0.33f, ref CS$<>8__locals1), 0.0024f, 215, 50, 40, 255, 40);
			Theme.Disc(WelcomePics.<XRay>g__X|19_0(-0.05f, ref CS$<>8__locals1), WelcomePics.<XRay>g__Y|19_1(0.29f, ref CS$<>8__locals1), 0.01f, 110, 160, 225, 255);
			Theme.Disc(WelcomePics.<XRay>g__X|19_0(0.1f, ref CS$<>8__locals1), WelcomePics.<XRay>g__Y|19_1(0.33f, ref CS$<>8__locals1), 0.01f, WelcomePics.RealGold.Item1, WelcomePics.RealGold.Item2, WelcomePics.RealGold.Item3, 255);
			float num2 = x + w * 0.42f;
			float num3 = y + 0.016f;
			UiDraw.TextFont("TODAS LAS LESIONES", UiDraw.TitleFont, num2, num3, 0.24f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			ValueTuple<string, string, ValueTuple<int, int, int>>[] array = new ValueTuple<string, string, ValueTuple<int, int, int>>[]
			{
				new ValueTuple<string, string, ValueTuple<int, int, int>>("Pulmón derecho perforado", "GRAVE", Theme.Warn),
				new ValueTuple<string, string, ValueTuple<int, int, int>>("Cuarta costilla fracturada", "GRAVE", Theme.Warn),
				new ValueTuple<string, string, ValueTuple<int, int, int>>("La bala salió por la espalda", "", Theme.Dim)
			};
			for (int j = 0; j < array.Length; j++)
			{
				float num4 = num3 + 0.032f + (float)j * 0.026f;
				UiDraw.Rect(num2, num4 + 0.004f, 0.006f, 0.01f, array[j].Item3.Item1, array[j].Item3.Item2, array[j].Item3.Item3, 255);
				UiDraw.Text(array[j].Item1, num2 + 0.014f, num4, 0.22f, Theme.Text.Item1, Theme.Text.Item2, Theme.Text.Item3, 255);
				if (array[j].Item2 != "")
				{
					UiDraw.TextFont(array[j].Item2, UiDraw.TitleFont, num2 + w * 0.4f, num4, 0.21f, array[j].Item3.Item1, array[j].Item3.Item2, array[j].Item3.Item3, 255, false);
				}
			}
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x001388A8 File Offset: 0x00136AA8
		private static void Wordmark(float x, float y, float w, float h)
		{
			float num = Theme.TitleWidth("en", 0.95f);
			float num2 = Theme.CapsWidth("RAGE", 0.95f);
			float num3 = x + w / 2f - (num + num2) / 2f;
			float y2 = y + h / 2f - 0.038f;
			UiDraw.TextFont("en", UiDraw.TitleFont, num3, y2, 0.95f, Theme.Title.Item1, Theme.Title.Item2, Theme.Title.Item3, 255, false);
			UiDraw.TextFont("RAGE", UiDraw.TitleFont, num3 + num, y2, 0.95f, Theme.NativeRed.Item1, Theme.NativeRed.Item2, Theme.NativeRed.Item3, 255, false);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0013896F File Offset: 0x00136B6F
		[CompilerGenerated]
		internal static float <XRay>g__X|19_0(float u, ref WelcomePics.<>c__DisplayClass19_0 A_1)
		{
			return A_1.cx + u * A_1.s / A_1.a;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x00138987 File Offset: 0x00136B87
		[CompilerGenerated]
		internal static float <XRay>g__Y|19_1(float v, ref WelcomePics.<>c__DisplayClass19_0 A_1)
		{
			return A_1.top + v * A_1.s;
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x00138998 File Offset: 0x00136B98
		[CompilerGenerated]
		internal static void <XRay>g__L|19_2(float u1, float v1, float u2, float v2, ref WelcomePics.<>c__DisplayClass19_0 A_4)
		{
			UiDraw.Line(WelcomePics.<XRay>g__X|19_0(u1, ref A_4), WelcomePics.<XRay>g__Y|19_1(v1, ref A_4), WelcomePics.<XRay>g__X|19_0(u2, ref A_4), WelcomePics.<XRay>g__Y|19_1(v2, ref A_4), 0.0016f, A_4.bone.Item1, A_4.bone.Item2, A_4.bone.Item3, 255, 40);
		}

		// Token: 0x02000477 RID: 1143
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04002033 RID: 8243
			public static Func<string, Gang> <0>__ById;
		}
	}
}
