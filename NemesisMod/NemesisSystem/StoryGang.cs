using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x0200006F RID: 111
	internal static class StoryGang
	{
		// Token: 0x0600129B RID: 4763 RVA: 0x00124B4C File Offset: 0x00122D4C
		private static NemesisRecord P(string key, string name, string title, int rank, int born, string birthplace, Personality temper, Sociability soc, Disposition disp, int aggr, Smarts smarts, Wealth wealth, string trade, string faith, Devotion dev, string family, int children, string facets, string values, string needs, string[] quirks, [TupleElementNames(new string[]
		{
			"type",
			"target",
			"text"
		})] ValueTuple<string, string, string>[] goals, [TupleElementNames(new string[]
		{
			"y",
			"m",
			"kind",
			"text"
		})] params ValueTuple<int, int, string, string>[] past)
		{
			NemesisRecord nemesisRecord = new NemesisRecord();
			nemesisRecord.Id = "vdl_" + key;
			nemesisRecord.Name = name;
			nemesisRecord.Title = title;
			nemesisRecord.Rank = rank;
			nemesisRecord.Gang = "vdl";
			nemesisRecord.IsAlive = true;
			nemesisRecord.Story = true;
			nemesisRecord.Personality = new Personality?(temper);
			nemesisRecord.PersonalityKnown = true;
			nemesisRecord.TraitsRolled = true;
			nemesisRecord.Sociability = soc;
			nemesisRecord.Disposition = disp;
			nemesisRecord.Aggression = aggr;
			nemesisRecord.Smarts = smarts;
			nemesisRecord.Wealth = wealth;
			nemesisRecord.Trade = trade;
			nemesisRecord.Faith = faith;
			nemesisRecord.Devotion = dev;
			nemesisRecord.Family = family;
			nemesisRecord.Children = children;
			nemesisRecord.BornYear = born;
			nemesisRecord.Birthplace = birthplace;
			nemesisRecord.PastRolled = true;
			nemesisRecord.Woven = true;
			nemesisRecord.MindRolled = true;
			nemesisRecord.QuirksRolled = true;
			nemesisRecord.WantsKnown = true;
			nemesisRecord.Origin = GrudgeOrigin.GangMember;
			nemesisRecord.Hate = 0;
			nemesisRecord.Quirks = (((quirks != null) ? quirks.ToList<string>() : null) ?? new List<string>());
			nemesisRecord.Facets = StoryGang.Parse(facets);
			nemesisRecord.Values = StoryGang.Parse(values);
			nemesisRecord.Needs = StoryGang.Parse(needs);
			List<Goal> list;
			if (goals == null)
			{
				list = null;
			}
			else
			{
				list = (from g in goals
				select new Goal
				{
					Type = g.Item1,
					Target = g.Item2,
					Text = g.Item3
				}).ToList<Goal>();
			}
			nemesisRecord.Goals = (list ?? new List<Goal>());
			nemesisRecord.Past = (from e in past
			select new PastEntry
			{
				Year = e.Item1,
				Month = e.Item2,
				Kind = e.Item3,
				Text = e.Item4,
				Public = true,
				Known = true
			}).ToList<PastEntry>();
			return nemesisRecord;
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00124D00 File Offset: 0x00122F00
		private static Dictionary<string, int> Parse(string s)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			string[] array = (s ?? "").Split(new char[]
			{
				' '
			});
			int num = 0;
			while (num + 1 < array.Length)
			{
				int value;
				if (int.TryParse(array[num + 1], out value))
				{
					dictionary[array[num]] = value;
				}
				num += 2;
			}
			return dictionary;
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00124D57 File Offset: 0x00122F57
		private static ValueTuple<string, string, string>[] G(params ValueTuple<string, string, string>[] g)
		{
			return g;
		}

		// Token: 0x0600129E RID: 4766 RVA: 0x00124D5A File Offset: 0x00122F5A
		private static string[] Q(params string[] q)
		{
			return q;
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00124D60 File Offset: 0x00122F60
		public static List<NemesisRecord> Build()
		{
			Personality temper = Personality.Loyal;
			StoryGang.<>c__DisplayClass5_0 CS$<>8__locals1;
			CS$<>8__locals1.list = new List<NemesisRecord>
			{
				StoryGang.P("dutch", "Dutch van der Linde", "the leader", 5, 1855, null, Personality.Proud, Sociability.Outgoing, Disposition.Friendly, 55, Smarts.Sharp, Wealth.Working, "Forajido", "none", Devotion.Lapsed, "single", 0, "Anger 50 Bravery 80 Greed 45 Loyalty 55 Pride 92 Honesty 30 Piety 20 Cruelty 40 Humour 55 Ambition 90", "Family 1 Gang 3 Money 1 Honour 2 Law -3 God -1", "Drink 30 Company 70 Faith 10 Money 70 Violence 30", StoryGang.Q(new string[]
				{
					"braggart"
				}), StoryGang.G(new ValueTuple<string, string, string>[]
				{
					new ValueTuple<string, string, string>("Rich", null, "un gran golpe más y luego ir a algún lugar donde el gobierno no llegue")
				}), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1855, 0, "born", "Nacimiento"),
					new ValueTuple<int, int, string, string>(1863, 0, "loss", "Perdió a su padre en la guerra"),
					new ValueTuple<int, int, string, string>(1875, 0, "gang", "Conoció a Hosea Matthews; intentaron robarse mutuamente y desde entonces cabalgaron juntos"),
					new ValueTuple<int, int, string, string>(1877, 0, "gang", "Acogió a un huérfano de catorce años, Arthur Morgan"),
					new ValueTuple<int, int, string, string>(1885, 0, "gang", "Salvó de la horca a un John Marston de doce años"),
					new ValueTuple<int, int, string, string>(1889, 0, "loss", "(aprox.) Perdió a Annabelle a manos de los hombres de Colm O'Driscoll; la enemistad nunca acabó"),
					new ValueTuple<int, int, string, string>(1895, 0, "family", "Empezó una relación con Molly O'Shea"),
					new ValueTuple<int, int, string, string>(1899, 2, "crime", "El golpe del ferry de Blackwater salió mal; la banda huyó con la ley pisándole los talones"),
					new ValueTuple<int, int, string, string>(1899, 3, "gang", "Guió a la banda hacia la nieve de los Grizzlies, hasta Colter")
				}),
				StoryGang.P("hosea", "Hosea Matthews", "El amigo más antiguo de Dutch", 4, 1844, null, Personality.Loyal, Sociability.Outgoing, Disposition.Friendly, 20, Smarts.Sharp, Wealth.Working, "Estafador", "none", Devotion.Lapsed, "widower", 0, "Anger 20 Bravery 60 Greed 40 Loyalty 85 Pride 50 Honesty 55 Piety 30 Cruelty 10 Humour 80 Ambition 35", "Family 2 Gang 3 Money 1 Honour 2 Law -1 God 0", "Drink 35 Company 60 Faith 20 Money 40 Violence 5", StoryGang.Q(new string[]
				{
					"gambler"
				}), StoryGang.G(new ValueTuple<string, string, string>[]
				{
					new ValueTuple<string, string, string>("Settle", null, "una última gran estafa y luego un pedazo de tierra tranquilo")
				}), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1844, 0, "born", "Nacimiento"),
					new ValueTuple<int, int, string, string>(1870, 0, "family", "(aprox.) Se casó con Bessie"),
					new ValueTuple<int, int, string, string>(1875, 0, "gang", "Conoció a Dutch van der Linde; intentaron robarse el uno al otro"),
					new ValueTuple<int, int, string, string>(1877, 0, "gang", "Ayudó a criar al joven Arthur Morgan"),
					new ValueTuple<int, int, string, string>(1893, 0, "loss", "(aprox.) Enterró a Bessie"),
					new ValueTuple<int, int, string, string>(1899, 2, "crime", "Huyó de Blackwater con los demás")
				}),
				StoryGang.P("arthur", "Arthur Morgan", "(tú)", 4, 1863, null, Personality.Cold, Sociability.Introvert, Disposition.Surly, 60, Smarts.Sharp, Wealth.Poor, "Forajido", "none", Devotion.Lapsed, "single", 0, "Anger 50 Bravery 85 Greed 30 Loyalty 90 Pride 55 Honesty 50 Piety 20 Cruelty 40 Humour 50 Ambition 25", "Family 1 Gang 3 Money 0 Honour 1 Law -2 God -1", "Drink 40 Company 30 Faith 10 Money 30 Violence 40", StoryGang.Q(Array.Empty<string>()), StoryGang.G(Array.Empty<ValueTuple<string, string, string>>()), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1863, 0, "born", "Nacimiento"),
					new ValueTuple<int, int, string, string>(1874, 0, "loss", "Murió su madre, Beatrice"),
					new ValueTuple<int, int, string, string>(1877, 0, "gang", "Acogido por Dutch y Hosea a los catorce años"),
					new ValueTuple<int, int, string, string>(1878, 0, "loss", "Murió su padre, Lyle, un forajido"),
					new ValueTuple<int, int, string, string>(1889, 0, "family", "(aprox.) Mary Linton rompió con él; su padre no aceptaba a un forajido"),
					new ValueTuple<int, int, string, string>(1890, 0, "loss", "(aprox.) Perdió a Eliza y a su hijo Isaac a manos de unos ladrones por diez dólares"),
					new ValueTuple<int, int, string, string>(1899, 2, "crime", "Blackwater: el golpe del ferry y la huida")
				}),
				StoryGang.P("micah", "Micah Bell", "El nuevo favorito de Dutch", 3, 1860, null, Personality.Hothead, Sociability.Outgoing, Disposition.Surly, 80, Smarts.Average, Wealth.Poor, "Forajido", "none", Devotion.Godless, "single", 0, "Anger 75 Bravery 70 Greed 75 Loyalty 15 Pride 80 Honesty 10 Piety 5 Cruelty 85 Humour 40 Ambition 85", "Family 0 Gang 0 Money 3 Honour -2 Law -3 God -2", "Drink 50 Company 40 Faith 0 Money 80 Violence 75", StoryGang.Q(new string[]
				{
					"sadist",
					"braggart"
				}), StoryGang.G(new ValueTuple<string, string, string>[]
				{
					new ValueTuple<string, string, string>("Rise", null, "El oído de Dutch, y todo lo que venga después")
				}), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1860, 0, "born", "Nació; su padre y su abuelo fueron forajidos antes que él"),
					new ValueTuple<int, int, string, string>(1898, 10, "gang", "Salvó la vida a Dutch en un trato que salió mal; Dutch lo acogió"),
					new ValueTuple<int, int, string, string>(1899, 2, "crime", "Le propuso a Dutch el golpe del ferry de Blackwater")
				}),
				StoryGang.P("john", "John Marston", "a gun", 3, 1873, null, Personality.Drifter, Sociability.Introvert, Disposition.Surly, 55, Smarts.Average, Wealth.Poor, "Forajido", "none", Devotion.Lapsed, "single", 1, "Anger 55 Bravery 75 Greed 30 Loyalty 65 Pride 60 Honesty 45 Piety 15 Cruelty 30 Humour 40 Ambition 30", "Family 1 Gang 2 Money 1 Honour 1 Law -2 God -1", "Drink 40 Company 35 Faith 5 Money 40 Violence 40", StoryGang.Q(Array.Empty<string>()), StoryGang.G(new ValueTuple<string, string, string>[]
				{
					new ValueTuple<string, string, string>("Settle", null, "algo que no dice en voz alta: una vida lejos de todo esto")
				}), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1873, 0, "born", "Nacimiento"),
					new ValueTuple<int, int, string, string>(1885, 0, "gang", "Salvado de la horca a los doce años por Dutch; criado en la banda"),
					new ValueTuple<int, int, string, string>(1895, 0, "family", "Nació su hijo Jack, de Abigail"),
					new ValueTuple<int, int, string, string>(1897, 0, "gang", "(aprox.) Se largó de la banda durante un año y luego volvió"),
					new ValueTuple<int, int, string, string>(1899, 2, "crime", "Herido en el golpe de Blackwater"),
					new ValueTuple<int, int, string, string>(1899, 3, "loss", "Los lobos le desgarraron la cara en los Grizzlies")
				}),
				StoryGang.P("javier", "Javier Escuella", "a gun", 2, 1871, "Mexico", temper, Sociability.Outgoing, Disposition.Friendly, 55, Smarts.Average, Wealth.Poor, "Forajido", "Católico", Devotion.Lapsed, "single", 0, "Anger 45 Bravery 80 Greed 25 Loyalty 92 Pride 65 Honesty 55 Piety 30 Cruelty 35 Humour 60 Ambition 30", "Family 1 Gang 3 Money 0 Honour 2 Law -2 God 0", "Drink 35 Company 60 Faith 20 Money 30 Violence 40", StoryGang.Q(Array.Empty<string>()), StoryGang.G(new ValueTuple<string, string, string>[]
				{
					new ValueTuple<string, string, string>("Protect", "vdl_dutch", "seguir a Dutch; cree en él")
				}), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1871, 0, "born", "Nació en México"),
					new ValueTuple<int, int, string, string>(1894, 0, "crime", "(aprox.) Mató a un oficial del ejército por una mujer y huyó al norte"),
					new ValueTuple<int, int, string, string>(1895, 0, "gang", "Se unió a la banda de Dutch"),
					new ValueTuple<int, int, string, string>(1899, 2, "crime", "Huyó de Blackwater con los demás")
				}),
				StoryGang.P("bill", "Bill Williamson", "a gun", 2, 1866, null, Personality.Hothead, Sociability.Outgoing, Disposition.Surly, 75, Smarts.Dim, Wealth.Poor, "Forajido", "none", Devotion.Lapsed, "single", 0, "Anger 75 Bravery 70 Greed 40 Loyalty 80 Pride 45 Honesty 50 Piety 10 Cruelty 45 Humour 35 Ambition 20", "Family 0 Gang 3 Money 1 Honour 0 Law -2 God -1", "Drink 70 Company 50 Faith 5 Money 40 Violence 60", StoryGang.Q(new string[]
				{
					"drunk"
				}), StoryGang.G(new ValueTuple<string, string, string>[]
				{
					new ValueTuple<string, string, string>("Protect", "vdl_dutch", "mantenerse en gracia con Dutch")
				}), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1866, 0, "born", "Nacido como Marion Williamson"),
					new ValueTuple<int, int, string, string>(1884, 0, "work", "(aprox.) Se alistó en el Ejército"),
					new ValueTuple<int, int, string, string>(1892, 0, "crime", "Expulsado con deshonor por intento de asesinato"),
					new ValueTuple<int, int, string, string>(1892, 0, "gang", "Dutch lo encontró muerto de hambre y lo acogió"),
					new ValueTuple<int, int, string, string>(1899, 2, "crime", "Huyó de Blackwater con los demás")
				}),
				StoryGang.P("charles", "Charles Smith", "a gun, a tracker", 2, 1873, null, temper, Sociability.Introvert, Disposition.Friendly, 40, Smarts.Sharp, Wealth.Poor, "Cazador", "none", Devotion.Lapsed, "single", 0, "Anger 30 Bravery 85 Greed 15 Loyalty 70 Pride 40 Honesty 92 Piety 25 Cruelty 10 Humour 40 Ambition 20", "Family 1 Gang 2 Money 0 Honour 3 Law -1 God 0", "Drink 15 Company 30 Faith 15 Money 20 Violence 25", StoryGang.Q(new string[]
				{
					"honest"
				}), StoryGang.G(Array.Empty<ValueTuple<string, string, string>>()), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1873, 0, "born", "Nació de madre nativa e indígena y padre afroamericano"),
					new ValueTuple<int, int, string, string>(1880, 0, "loss", "(aprox.) Los soldados se llevaron a su madre"),
					new ValueTuple<int, int, string, string>(1898, 9, "gang", "Se unió a la banda seis meses antes de lo de Blackwater")
				}),
				StoryGang.P("lenny", "Lenny Summers", "a gun", 2, 1880, null, temper, Sociability.Outgoing, Disposition.Friendly, 40, Smarts.Sharp, Wealth.Poor, "Forajido", "none", Devotion.Lapsed, "single", 0, "Anger 30 Bravery 70 Greed 25 Loyalty 80 Pride 45 Honesty 70 Piety 20 Cruelty 10 Humour 80 Ambition 40", "Family 2 Gang 2 Money 0 Honour 2 Law -1 God 0", "Drink 30 Company 70 Faith 10 Money 25 Violence 20", StoryGang.Q(Array.Empty<string>()), StoryGang.G(Array.Empty<ValueTuple<string, string, string>>()), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1880, 0, "born", "Nació y fue a la escuela; sabe leer"),
					new ValueTuple<int, int, string, string>(1895, 0, "loss", "Asesinaron a su padre; él mató al responsable"),
					new ValueTuple<int, int, string, string>(1898, 0, "gang", "Tras tres años huyendo, se unió a la banda")
				}),
				StoryGang.P("sean", "Sean MacGuire", "a gun", 2, 1877, null, Personality.Hothead, Sociability.Outgoing, Disposition.Friendly, 60, Smarts.Average, Wealth.Poor, "Forajido", "Católico", Devotion.Lapsed, "single", 0, "Anger 55 Bravery 70 Greed 45 Loyalty 70 Pride 75 Honesty 40 Piety 15 Cruelty 25 Humour 90 Ambition 45", "Family 1 Gang 2 Money 1 Honour 1 Law -3 God 0", "Drink 70 Company 85 Faith 10 Money 50 Violence 40", StoryGang.Q(new string[]
				{
					"braggart",
					"drunk"
				}), StoryGang.G(Array.Empty<ValueTuple<string, string, string>>()), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1877, 0, "born", "Nació en una familia irlandesa de rebeldes y ladrones; mataron a su padre"),
					new ValueTuple<int, int, string, string>(1896, 0, "gang", "Intentó robar a Dutch y acabó uniéndose a él"),
					new ValueTuple<int, int, string, string>(1899, 2, "crime", "Capturado por cazarrecompensas tras lo de Blackwater")
				}),
				StoryGang.P("sadie", "Sadie Adler", "the widow", 2, 1874, "the mountains", Personality.Hothead, Sociability.Introvert, Disposition.Surly, 75, Smarts.Sharp, Wealth.Working, "Ranchero", "none", Devotion.Lapsed, "widower", 0, "Anger 85 Bravery 90 Greed 15 Loyalty 70 Pride 70 Honesty 75 Piety 15 Cruelty 45 Humour 30 Ambition 30", "Family 3 Gang 1 Money 0 Honour 2 Law -1 God 0", "Drink 30 Company 25 Faith 10 Money 15 Violence 70", StoryGang.Q(Array.Empty<string>()), StoryGang.G(new ValueTuple<string, string, string>[]
				{
					new ValueTuple<string, string, string>("Avenge", "odriscoll", "los O'Driscoll que mataron a Jake")
				}), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1874, 0, "born", "Nació y creció cabalgando, disparando y cazando en las montañas"),
					new ValueTuple<int, int, string, string>(1894, 0, "family", "(aprox.) Se casó con Jake Adler; llevaban un rancho juntos"),
					new ValueTuple<int, int, string, string>(1899, 3, "loss", "Los O'Driscoll mataron a Jake; se escondió hasta que la banda la encontró")
				}),
				StoryGang.P("karen", "Karen Jones", "a gun", 2, 1874, null, Personality.Hothead, Sociability.Outgoing, Disposition.Friendly, 55, Smarts.Average, Wealth.Poor, "Estafadora", "none", Devotion.Lapsed, "single", 0, "Anger 50 Bravery 75 Greed 45 Loyalty 70 Pride 55 Honesty 45 Piety 10 Cruelty 25 Humour 75 Ambition 35", "Family 0 Gang 2 Money 1 Honour 1 Law -2 God -1", "Drink 75 Company 80 Faith 5 Money 50 Violence 35", StoryGang.Q(new string[]
				{
					"drunk"
				}), StoryGang.G(Array.Empty<ValueTuple<string, string, string>>()), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1874, 0, "born", "Nacimiento"),
					new ValueTuple<int, int, string, string>(1896, 0, "gang", "(aprox.) Se unió a la banda; ladrona y gran embustera")
				}),
				StoryGang.P("grimshaw", "Susan Grimshaw", "lleva las riendas del campamento", 1, 1857, null, Personality.Cold, Sociability.Outgoing, Disposition.Surly, 60, Smarts.Sharp, Wealth.Poor, "Forajido", "none", Devotion.Lapsed, "single", 0, "Anger 60 Bravery 80 Greed 20 Loyalty 92 Pride 70 Honesty 60 Piety 15 Cruelty 45 Humour 25 Ambition 30", "Family 1 Gang 3 Money 0 Honour 2 Law -2 God -1", "Drink 25 Company 50 Faith 5 Money 20 Violence 25", StoryGang.Q(Array.Empty<string>()), StoryGang.G(new ValueTuple<string, string, string>[]
				{
					new ValueTuple<string, string, string>("Protect", "vdl", "mantener el campamento en orden")
				}), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1857, 0, "born", "Nacimiento"),
					new ValueTuple<int, int, string, string>(1877, 0, "gang", "(aprox.) Cabalgó con Dutch desde los primeros tiempos; fue su mujer")
				}),
				StoryGang.P("pearson", "Simon Pearson", "the cook", 1, 1850, null, Personality.Proud, Sociability.Outgoing, Disposition.Friendly, 30, Smarts.Average, Wealth.Poor, "Cocinero", "none", Devotion.Lapsed, "single", 0, "Anger 35 Bravery 45 Greed 35 Loyalty 70 Pride 70 Honesty 55 Piety 20 Cruelty 15 Humour 55 Ambition 30", "Family 1 Gang 2 Money 1 Honour 1 Law -1 God 0", "Drink 40 Company 60 Faith 10 Money 35 Violence 10", StoryGang.Q(new string[]
				{
					"braggart"
				}), StoryGang.G(Array.Empty<ValueTuple<string, string, string>>()), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1850, 0, "born", "Nació en una familia de balleneros"),
					new ValueTuple<int, int, string, string>(1870, 0, "work", "(aprox.) Sirvió brevemente en la Marina"),
					new ValueTuple<int, int, string, string>(1889, 0, "gang", "(aprox.) Dutch lo encontró con problemas de deudas y lo acogió")
				}),
				StoryGang.P("strauss", "Leopold Strauss", "el prestamista", 1, 1846, "Vienna", Personality.Greedy, Sociability.Introvert, Disposition.Neutral, 15, Smarts.Sharp, Wealth.Working, "Prestamista", "Católico", Devotion.Lapsed, "single", 0, "Anger 20 Bravery 25 Greed 85 Loyalty 65 Pride 45 Honesty 35 Piety 15 Cruelty 35 Humour 20 Ambition 50", "Family 0 Gang 2 Money 3 Honour 0 Law -1 God 0", "Drink 10 Company 25 Faith 15 Money 85 Violence 0", StoryGang.Q(Array.Empty<string>()), StoryGang.G(new ValueTuple<string, string, string>[]
				{
					new ValueTuple<string, string, string>("Rich", null, "el dinero que le deben a la banda, hasta el último centavo")
				}), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1846, 0, "born", "Nació pobre en Viena"),
					new ValueTuple<int, int, string, string>(1870, 0, "work", "(aprox.) Llegó a América y vivió de estafas"),
					new ValueTuple<int, int, string, string>(1887, 0, "gang", "(aprox.) Se unió a Dutch por protección; lleva las cuentas")
				}),
				StoryGang.P("tío", "Uncle", "the old hand", 1, 1843, null, Personality.Coward, Sociability.Outgoing, Disposition.Friendly, 15, Smarts.Average, Wealth.Poor, "Errante", "none", Devotion.Lapsed, "single", 0, "Anger 15 Bravery 20 Greed 45 Loyalty 55 Pride 40 Honesty 20 Piety 10 Cruelty 10 Humour 80 Ambition 5", "Family 0 Gang 2 Money 1 Honour 0 Law -1 God 0", "Drink 85 Company 70 Faith 5 Money 40 Violence 5", StoryGang.Q(new string[]
				{
					"drunk"
				}), StoryGang.G(Array.Empty<ValueTuple<string, string, string>>()), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1843, 0, "born", "Born - he jokes it was between the fall of 1849 and the fall of Rome"),
					new ValueTuple<int, int, string, string>(1887, 0, "gang", "(about) Joined the gang"),
					new ValueTuple<int, int, string, string>(1894, 0, "gang", "Brought Abigail Roberts to the gang")
				}),
				StoryGang.P("swanson", "Reverend Swanson", "the reverend", 1, 1851, null, Personality.Coward, Sociability.Outgoing, Disposition.Friendly, 15, Smarts.Sharp, Wealth.Poor, "Predicador", "Protestante", Devotion.Churchgoer, "single", 0, "Anger 15 Bravery 30 Greed 20 Loyalty 65 Pride 35 Honesty 55 Piety 60 Cruelty 5 Humour 55 Ambition 10", "Family 0 Gang 2 Money 0 Honour 1 Law 0 God 2", "Drink 85 Company 55 Faith 60 Money 20 Violence 0", StoryGang.Q(new string[]
				{
					"drunk"
				}), StoryGang.G(Array.Empty<ValueTuple<string, string, string>>()), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1851, 0, "born", "Nacimiento"),
					new ValueTuple<int, int, string, string>(1880, 0, "faith", "(about) A minister - who lost his way to morphine, then whiskey"),
					new ValueTuple<int, int, string, string>(1890, 0, "gang", "(about) Once saved Dutch's life; rode with the gang after")
				}),
				StoryGang.P("trelawny", "Josiah Trelawny", "the gentleman", 1, 1849, null, Personality.Drifter, Sociability.Outgoing, Disposition.Friendly, 20, Smarts.Sharp, Wealth.Comfortable, "Estafador", "none", Devotion.Lapsed, "single", 0, "Anger 20 Bravery 45 Greed 55 Loyalty 50 Pride 70 Honesty 30 Piety 15 Cruelty 10 Humour 80 Ambition 40", "Family 2 Gang 1 Money 2 Honour 1 Law -1 God 0", "Drink 35 Company 70 Faith 5 Money 55 Violence 5", StoryGang.Q(Array.Empty<string>()), StoryGang.G(Array.Empty<ValueTuple<string, string, string>>()), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1849, 0, "born", "Nacimiento"),
					new ValueTuple<int, int, string, string>(1880, 0, "gang", "(about) Became Dutch's friend; comes and goes as he pleases")
				}),
				StoryGang.P("abigail", "Abigail Roberts", "John's woman", 1, 1877, null, Personality.Proud, Sociability.Outgoing, Disposition.Neutral, 45, Smarts.Sharp, Wealth.Poor, "Forajido", "none", Devotion.Lapsed, "single", 1, "Anger 55 Bravery 70 Greed 20 Loyalty 75 Pride 70 Honesty 70 Piety 20 Cruelty 10 Humour 55 Ambition 40", "Family 3 Gang 1 Money 1 Honour 2 Law -1 God 0", "Drink 20 Company 55 Faith 10 Money 40 Violence 5", StoryGang.Q(new string[]
				{
					"padre de familia"
				}), StoryGang.G(new ValueTuple<string, string, string>[]
				{
					new ValueTuple<string, string, string>("Protect", "vdl_jack", "una vida mejor para Jack")
				}), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1877, 0, "born", "Nació y quedó huérfana de pequeña"),
					new ValueTuple<int, int, string, string>(1894, 0, "gang", "El Tío la trajo a la banda"),
					new ValueTuple<int, int, string, string>(1895, 0, "family", "Nació su hijo Jack")
				}),
				StoryGang.P("jack", "Jack Marston", "the boy", 1, 1895, null, Personality.Loyal, Sociability.Introvert, Disposition.Friendly, 5, Smarts.Sharp, Wealth.Poor, "Niño", "none", Devotion.Lapsed, "single", 0, "Anger 10 Bravery 70 Greed 10 Loyalty 70 Pride 20 Honesty 85 Piety 15 Cruelty 0 Humour 50 Ambition 20", "Family 3 Gang 1 Money 0 Honour 1 Law 0 God 0", "Drink 0 Company 60 Faith 5 Money 0 Violence 0", StoryGang.Q(Array.Empty<string>()), StoryGang.G(Array.Empty<ValueTuple<string, string, string>>()), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1895, 0, "born", "Nació dentro de la banda, hijo de John y Abigail")
				}),
				StoryGang.P("tilly", "Tilly Jackson", "camp", 1, 1876, null, temper, Sociability.Outgoing, Disposition.Friendly, 35, Smarts.Sharp, Wealth.Poor, "Forajido", "none", Devotion.Lapsed, "single", 0, "Anger 35 Bravery 65 Greed 20 Loyalty 80 Pride 55 Honesty 70 Piety 20 Cruelty 10 Humour 60 Ambition 40", "Family 1 Gang 3 Money 0 Honour 2 Law -1 God 0", "Drink 20 Company 70 Faith 10 Money 25 Violence 10", StoryGang.Q(new string[]
				{
					"honest"
				}), StoryGang.G(Array.Empty<ValueTuple<string, string, string>>()), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1876, 0, "born", "Nacimiento"),
					new ValueTuple<int, int, string, string>(1888, 0, "loss", "Taken at twelve by the Foreman Brothers' gang"),
					new ValueTuple<int, int, string, string>(1891, 0, "gang", "(about) Taken in by Dutch, who taught her to read")
				}),
				StoryGang.P("marybeth", "Mary-Beth Gaskill", "camp", 1, 1876, null, temper, Sociability.Outgoing, Disposition.Friendly, 15, Smarts.Sharp, Wealth.Poor, "Carterista", "none", Devotion.Churchgoer, "single", 0, "Anger 15 Bravery 45 Greed 25 Loyalty 75 Pride 40 Honesty 60 Piety 35 Cruelty 5 Humour 65 Ambition 55", "Family 1 Gang 2 Money 1 Honour 2 Law -1 God 1", "Drink 10 Company 65 Faith 30 Money 30 Violence 0", StoryGang.Q(Array.Empty<string>()), StoryGang.G(new ValueTuple<string, string, string>[]
				{
					new ValueTuple<string, string, string>("Settle", null, "escribir sus libros en algún lugar tranquilo")
				}), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1876, 0, "born", "Nacimiento"),
					new ValueTuple<int, int, string, string>(1882, 0, "loss", "(about) Her mother died of typhoid; raised in an orphanage"),
					new ValueTuple<int, int, string, string>(1896, 0, "gang", "(about) Joined the gang - a fine pickpocket who'd rather be reading")
				}),
				StoryGang.P("molly", "Molly O'Shea", "la chica de Dutch", 1, 1873, "Ireland", Personality.Proud, Sociability.Outgoing, Disposition.Surly, 40, Smarts.Average, Wealth.Poor, "Forajido", "Católico", Devotion.Lapsed, "single", 0, "Anger 60 Bravery 40 Greed 35 Loyalty 55 Pride 80 Honesty 50 Piety 20 Cruelty 20 Humour 35 Ambition 50", "Family 1 Gang 1 Money 1 Honour 1 Law -1 God 0", "Drink 50 Company 80 Faith 15 Money 35 Violence 5", StoryGang.Q(Array.Empty<string>()), StoryGang.G(new ValueTuple<string, string, string>[]
				{
					new ValueTuple<string, string, string>("Protect", "vdl_dutch", "mantener la atención de Dutch en ella")
				}), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1873, 0, "born", "Nació en Irlanda"),
					new ValueTuple<int, int, string, string>(1895, 0, "family", "Se juntó con Dutch van der Linde")
				}),
				StoryGang.P("kieran", "Kieran Duffy", "the prisoner", 1, 1875, null, Personality.Coward, Sociability.Introvert, Disposition.Friendly, 20, Smarts.Average, Wealth.Poor, "Jinete", "Católico", Devotion.Lapsed, "single", 0, "Anger 20 Bravery 30 Greed 20 Loyalty 50 Pride 25 Honesty 60 Piety 20 Cruelty 10 Humour 35 Ambition 15", "Family 0 Gang 1 Money 0 Honour 1 Law 0 God 0", "Drink 20 Company 60 Faith 15 Money 20 Violence 5", StoryGang.Q(new string[]
				{
					"superstitious"
				}), StoryGang.G(new ValueTuple<string, string, string>[]
				{
					new ValueTuple<string, string, string>("Settle", null, "ser uno de ellos, no estar atado a un árbol")
				}), new ValueTuple<int, int, string, string>[]
				{
					new ValueTuple<int, int, string, string>(1875, 0, "born", "Nacimiento"),
					new ValueTuple<int, int, string, string>(1897, 0, "gang", "(aprox.) Cabalgó con la banda de Colm O'Driscoll"),
					new ValueTuple<int, int, string, string>(1899, 3, "gang", "Capturado por la banda en los Grizzlies")
				})
			};
			StoryGang.<Build>g__Tie|5_0("dutch", "hosea", "friend", 90, "su amigo más antiguo", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("dutch", "arthur", "protege", 80, "lo crió", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("dutch", "john", "protege", 60, "lo salvó de la soga", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("dutch", "micah", "protege", 60, "su nuevo favorito", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("dutch", "molly", "friend", 40, "su mujer", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("dutch", "grimshaw", "friend", 50, "fue su mujer en otro tiempo", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("hosea", "arthur", "protege", 85, "como un hijo", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("arthur", "john", "rival", -10, "como hermanos que no siempre se llevan bien", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("arthur", "micah", "enemy", -60, "no lo soporta", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("arthur", "charles", "friend", 60, "confía en él", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("arthur", "lenny", "friend", 65, "cuida de él", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("arthur", "marybeth", "friend", 55, "leen juntos", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("john", "abigail", "kin", 60, "la madre de su hijo", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("john", "jack", "kin", 70, "su hijo", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("abigail", "jack", "kin", 95, "su hijo", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("tío", "abigail", "friend", 50, "la trajo a la banda", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("javier", "dutch", "mentor", 85, "cree en él", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("bill", "dutch", "mentor", 80, "se lo debe todo", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("tilly", "dutch", "mentor", 70, "le enseñó a leer", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("swanson", "dutch", "friend", 50, "le salvó la vida una vez", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("trelawny", "dutch", "friend", 55, "viejos amigos", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("grimshaw", "pearson", "friend", 40, "dirigen juntos el campamento", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("sean", "kieran", "enemy", -45, "un O'Driscoll en el campamento", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("tilly", "marybeth", "friend", 70, "muy cercanos", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("tilly", "karen", "friend", 65, "muy cercanos", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("karen", "sean", "friend", 55, "compañeros de trago", ref CS$<>8__locals1);
			StoryGang.<Build>g__Tie|5_0("lenny", "charles", "friend", 45, "llegaron cabalgando juntos", ref CS$<>8__locals1);
			NemesisRecord nemesisRecord = CS$<>8__locals1.list.First((NemesisRecord r) => r.Id == "vdl_jack");
			nemesisRecord.KinId = "vdl_john";
			nemesisRecord.KinRelation = "hijo";
			CS$<>8__locals1.list.Add(StoryGang.Dead("davey", "Davey Callender", "a gun", 2, 1872, "Tiroteado en Blackwater; murió de sus heridas en la nieve de Colter", new ValueTuple<int, int, string, string>[]
			{
				new ValueTuple<int, int, string, string>(1899, 2, "gang", "Herido en el tiroteo de Blackwater"),
				new ValueTuple<int, int, string, string>(1899, 3, "death", "Murió de sus heridas en Colter, en la nieve")
			}));
			CS$<>8__locals1.list.Add(StoryGang.Dead("mac", "Mac Callender", "a gun", 2, 1870, "Perdido en Blackwater; quedó atrás durante el tiroteo", new ValueTuple<int, int, string, string>[]
			{
				new ValueTuple<int, int, string, string>(1899, 2, "death", "Perdido en Blackwater durante el golpe del ferry; quedó atrás")
			}));
			CS$<>8__locals1.list.Add(StoryGang.Dead("jenny", "Jenny Kirk", "camp", 1, 1880, "Muerto en Blackwater", new ValueTuple<int, int, string, string>[]
			{
				new ValueTuple<int, int, string, string>(1899, 2, "death", "Muerto en el tiroteo posterior al golpe del ferry de Blackwater")
			}));
			CS$<>8__locals1.list.Add(StoryGang.Dead("heidi", "Heidi McCourt", "camp", 1, 1871, "Muerto en Blackwater", new ValueTuple<int, int, string, string>[]
			{
				new ValueTuple<int, int, string, string>(1899, 2, "death", "Muerta en Blackwater cuando el golpe del ferry salió mal")
			}));
			CS$<>8__locals1.list.Add(StoryGang.Dead("annabelle", "Annabelle", "Dutch's woman", 1, 1862, "Asesinada por los hombres de Colm O'Driscoll (hacia 1889)", new ValueTuple<int, int, string, string>[]
			{
				new ValueTuple<int, int, string, string>(1889, 0, "death", "Asesinada por los hombres de Colm O'Driscoll; el inicio de la enemistad")
			}));
			return CS$<>8__locals1.list;
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x001266CC File Offset: 0x001248CC
		private static NemesisRecord Dead(string key, string name, string title, int rank, int born, string how, [TupleElementNames(new string[]
		{
			"y",
			"m",
			"kind",
			"text"
		})] params ValueTuple<int, int, string, string>[] past)
		{
			NemesisRecord nemesisRecord = new NemesisRecord();
			nemesisRecord.Id = "vdl_" + key;
			nemesisRecord.Name = name;
			nemesisRecord.Title = title;
			nemesisRecord.Rank = rank;
			nemesisRecord.Gang = "vdl";
			nemesisRecord.IsAlive = false;
			nemesisRecord.Story = true;
			nemesisRecord.BornYear = born;
			nemesisRecord.PastRolled = true;
			nemesisRecord.Woven = true;
			nemesisRecord.MindRolled = true;
			nemesisRecord.QuirksRolled = true;
			nemesisRecord.Origin = GrudgeOrigin.GangMember;
			nemesisRecord.Hate = 0;
			nemesisRecord.Quirks = new List<string>();
			nemesisRecord.Goals = new List<Goal>();
			nemesisRecord.Past = (from e in past
			select new PastEntry
			{
				Year = e.Item1,
				Month = e.Item2,
				Kind = e.Item3,
				Text = e.Item4,
				Public = true,
				Known = true
			}).ToList<PastEntry>();
			return nemesisRecord;
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00126798 File Offset: 0x00124998
		[CompilerGenerated]
		internal static void <Build>g__Tie|5_0(string a, string b, string type, int strength, string note, ref StoryGang.<>c__DisplayClass5_0 A_5)
		{
			NemesisRecord nemesisRecord = A_5.list.First((NemesisRecord r) => r.Id == "vdl_" + a);
			NemesisRecord nemesisRecord2 = A_5.list.First((NemesisRecord r) => r.Id == "vdl_" + b);
			nemesisRecord.Ties.Add(new Tie
			{
				Id = nemesisRecord2.Id,
				Type = type,
				Strength = strength,
				Note = note
			});
			string type2 = (type == "protege") ? "mentor" : ((type == "mentor") ? "protege" : type);
			nemesisRecord2.Ties.Add(new Tie
			{
				Id = nemesisRecord.Id,
				Type = type2,
				Strength = strength,
				Note = note
			});
		}

		// Token: 0x04000C47 RID: 3143
		public const string Id = "vdl";
	}
}
