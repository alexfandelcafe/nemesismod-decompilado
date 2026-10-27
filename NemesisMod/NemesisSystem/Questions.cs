using System;
using System.Collections.Generic;
using System.Linq;

namespace NemesisSystem
{
	// Token: 0x02000062 RID: 98
	internal static class Questions
	{
		// Token: 0x060011CB RID: 4555 RVA: 0x00111C2B File Offset: 0x0010FE2B
		public static QuestionsContent Export()
		{
			return new QuestionsContent
			{
				Version = 4,
				Rows = Questions.BuiltIn.ToList<QuestionRow>()
			};
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00111C4C File Offset: 0x0010FE4C
		public static int Import(QuestionsContent c)
		{
			if (((c != null) ? c.Rows : null) == null || c.Rows.Count == 0)
			{
				return 0;
			}
			List<QuestionRow> list = (from r in c.Rows
			where !string.IsNullOrEmpty(r.Id) && !string.IsNullOrEmpty(r.Text) && Questions.Cats.Contains(r.Cat)
			select r).ToList<QuestionRow>();
			if (c.Version < 2)
			{
				list.RemoveAll((QuestionRow r) => r.Id == "where-money");
			}
			if (c.Version < 3)
			{
				foreach (QuestionRow questionRow in from r in list
				where r.Id == "why-live"
				select r)
				{
					questionRow.Once = false;
				}
			}
			if (c.Version < 4)
			{
				list.RemoveAll((QuestionRow r) => Questions.Merged.ContainsKey(r.Id) || r.Id == "where-camp");
			}
			HashSet<string> have = new HashSet<string>(from r in list
			select r.Id);
			HashSet<string> removed = new HashSet<string>(c.Removed ?? new List<string>());
			List<QuestionRow> list2 = (from r in Questions.BuiltIn
			where !have.Contains(r.Id) && !removed.Contains(r.Id)
			select r).ToList<QuestionRow>();
			Questions.AddedFromBuiltIn = list2.Count;
			list.AddRange(list2);
			Questions.All = list.ToArray();
			return Questions.All.Length;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00111DFC File Offset: 0x0010FFFC
		// Note: this type is marked as 'beforefieldinit'.
		static Questions()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			dictionary["when-ride"] = "when-boss";
			dictionary["when-alone"] = "when-boss";
			dictionary["when-habit"] = "when-boss";
			dictionary["when-shakeup"] = "when-news";
			dictionary["when-funeral"] = "when-news";
			dictionary["where-hideout"] = "where-camp";
			dictionary["where-money"] = "where-stores";
			dictionary["where-supplies"] = "where-stores";
			Questions.Merged = dictionary;
		}

		// Token: 0x04000BC9 RID: 3017
		public static readonly string[] Cats = new string[]
		{
			"Qué",
			"Cuándo",
			"Dónde",
			"Por qué"
		};

		// Token: 0x04000BCA RID: 3018
		public static readonly QuestionRow[] BuiltIn = new QuestionRow[]
		{
			new QuestionRow
			{
				Id = "what-orders",
				Cat = "Qué",
				Text = "\"¿Cuáles son tus órdenes? ¿Qué planea la banda?\"",
				Need = 5,
				Lies = true,
				Requires = "gang",
				Answer = "orders"
			},
			new QuestionRow
			{
				Id = "what-wants",
				Cat = "Qué",
				Text = "\"¿Qué es lo que buscas realmente?\"",
				Need = 0,
				Odds = 10,
				Requires = "named",
				Answer = "wants"
			},
			new QuestionRow
			{
				Id = "what-boss",
				Cat = "Qué",
				Text = "\"¿Qué clase de hombre es tu jefe?\"",
				Need = 5,
				Requires = "boss",
				Answer = "bossnature"
			},
			new QuestionRow
			{
				Id = "when-boss",
				Cat = "Cuándo",
				Text = "\"¿Cuándo puedo pillar a tu jefe? ¿Dónde?\"",
				Need = 10,
				Lies = true,
				Requires = "gang&boss",
				Answer = "catch"
			},
			new QuestionRow
			{
				Id = "when-news",
				Cat = "Cuándo",
				Text = "\"¿Qué se cuece en la banda? ¿Se acerca algo?\"",
				Need = 0,
				Odds = 10,
				Requires = "gang&news",
				Answer = "news"
			},
			new QuestionRow
			{
				Id = "where-camp",
				Cat = "Dónde",
				Text = "\"¿Dónde se esconde tu jefe? Habla.\"",
				Need = 0,
				Lies = true,
				Requires = "gang",
				Answer = "holedup"
			},
			new QuestionRow
			{
				Id = "where-stores",
				Cat = "Dónde",
				Text = "\"¿Dónde guardan el dinero? ¿Las armas y provisiones?\"",
				Need = 15,
				Odds = -5,
				Requires = "gang&named",
				Answer = "stores"
			},
			new QuestionRow
			{
				Id = "where-people",
				Cat = "Dónde",
				Text = "\"Tu familia. Tus amigos. ¿Dónde están?\"",
				Need = 15,
				Requires = "named",
				Answer = "people"
			},
			new QuestionRow
			{
				Id = "why-past",
				Cat = "Por qué",
				Text = "\"¿Por qué cabalgas con estos hombres?\"",
				Need = -10,
				Odds = 15,
				Requires = "named",
				Answer = "past"
			},
			new QuestionRow
			{
				Id = "why-grudge",
				Cat = "Por qué",
				Text = "\"¿Quién es el que más me quiere muerto? ¿Por qué?\"",
				Need = 5,
				Requires = "gang",
				Answer = "worstgrudge"
			},
			new QuestionRow
			{
				Id = "why-live",
				Cat = "Por qué",
				Text = "\"Dame una sola razón para dejarte vivir.\"",
				Need = 10,
				Odds = 5,
				Requires = "",
				Answer = "bargain",
				Once = false
			}
		};

		// Token: 0x04000BCB RID: 3019
		public static QuestionRow[] All = Questions.BuiltIn;

		// Token: 0x04000BCC RID: 3020
		public static readonly Dictionary<string, string> Merged;

		// Token: 0x04000BCD RID: 3021
		public const int ContentVersion = 4;

		// Token: 0x04000BCE RID: 3022
		public static int AddedFromBuiltIn;
	}
}
