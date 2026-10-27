using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x02000087 RID: 135
	internal static class WorldLife
	{
		// Token: 0x060013C9 RID: 5065 RVA: 0x001392D0 File Offset: 0x001374D0
		public static LifeContent Export()
		{
			LifeContent lifeContent = new LifeContent();
			lifeContent.Version = 2;
			lifeContent.Rows = (from e in WorldLife.BuiltIn
			select new LifeContentRow
			{
				Id = e.Id,
				Who = e.Who,
				Title = e.Title,
				Thought = e.Thought,
				Mood = e.Mood,
				Weight = e.Weight,
				Hours = e.Hours,
				When = e.When,
				Odds = e.Odds,
				Effects = e.Effects.ToList<string>()
			}).ToList<LifeContentRow>();
			return lifeContent;
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x00139320 File Offset: 0x00137520
		public static int Import(LifeContent c)
		{
			if (((c != null) ? c.Rows : null) == null || c.Rows.Count == 0)
			{
				return 0;
			}
			List<LifeEvent> list = (from x in c.Rows
			where !string.IsNullOrEmpty(x.Title)
			select new LifeEvent(x.Id, x.Who ?? "any", x.Title, x.Thought ?? x.Title, x.Mood ?? "Estable", x.Weight, Math.Max(1, x.Hours), x.When, Math.Max(0, (x.Odds == 0) ? 10 : x.Odds), (x.Effects ?? new List<string>()).ToArray())).ToList<LifeEvent>();
			HashSet<string> have = new HashSet<string>(from e in list
			select e.Id);
			HashSet<string> removed = new HashSet<string>(c.Removed ?? new List<string>());
			List<LifeEvent> list2 = (from e in WorldLife.BuiltIn
			where !have.Contains(e.Id) && !removed.Contains(e.Id)
			select e).ToList<LifeEvent>();
			WorldLife.AddedFromBuiltIn = list2.Count;
			list.AddRange(list2);
			WorldLife.All = list.ToArray();
			return WorldLife.All.Length;
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x00139430 File Offset: 0x00137630
		public static LifeEvent Pick(NemesisRecord r, Random rng)
		{
			WorldLife.<>c__DisplayClass7_0 CS$<>8__locals1 = new WorldLife.<>c__DisplayClass7_0();
			CS$<>8__locals1.r = r;
			WorldLife.<>c__DisplayClass7_0 CS$<>8__locals2 = CS$<>8__locals1;
			IEnumerable<HistoryEntry> source = CS$<>8__locals1.r.History ?? new List<HistoryEntry>();
			int val = 0;
			List<HistoryEntry> history = CS$<>8__locals1.r.History;
			CS$<>8__locals2.mine = new HashSet<string>(from h in source.Skip(Math.Max(val, ((history != null) ? history.Count : 0) - 30))
			select h.Text ?? "");
			List<LifeEvent> list = (from e in WorldLife.All
			where e.Fits(CS$<>8__locals1.r) && !WorldLife.Recent.Contains(e.Id) && (WorldLife.<Pick>g__Key|7_1(e).Length < 6 || !CS$<>8__locals1.mine.Any((string t) => t.Contains(WorldLife.<Pick>g__Key|7_1(e))))
			select e).ToList<LifeEvent>();
			if (list.Count == 0)
			{
				list = (from e in WorldLife.All
				where e.Fits(CS$<>8__locals1.r)
				select e).ToList<LifeEvent>();
			}
			if (list.Count == 0)
			{
				return null;
			}
			int maxValue = list.Sum((LifeEvent e) => e.Odds);
			int num = rng.Next(maxValue);
			foreach (LifeEvent lifeEvent in list)
			{
				num -= lifeEvent.Odds;
				if (num < 0)
				{
					return lifeEvent;
				}
			}
			return list[list.Count - 1];
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x0013C4DC File Offset: 0x0013A6DC
		[CompilerGenerated]
		internal static string <Pick>g__Key|7_1(LifeEvent e)
		{
			return e.Title.Replace("{n}'s ", "").Replace("{n} ", "").Replace("{n}", "").Split(new char[]
			{
				'{'
			})[0].Trim();
		}

		// Token: 0x04000D53 RID: 3411
		public static readonly LifeEvent[] BuiltIn = new LifeEvent[]
		{
			new LifeEvent("any", "{n} cayó en cama con fiebre", "en cama con fiebre", "Enfermo", -3, 48, null, 10, new string[]
			{
				"rest:36"
			}),
			new LifeEvent("any", "{n} pilló una tos bajo la lluvia que no se le quita", "una tos que no se quita", "Enfermo", -2, 48, null, 10, new string[]
			{
				"rest:12"
			}),
			new LifeEvent("any", "{n} se rompió dos dedos al caerse de una valla", "dos dedos entablillados", "Dolorido", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} fue derribado por su caballo", "la espalda dolorida por la caída", "Dolorido", -2, 48, null, 10, new string[]
			{
				"rest:12"
			}),
			new LifeEvent("any", "{n} fue mordido por una cascabel", "la pierna hinchada por una mordedura", "Dolorido", -3, 48, null, 8, new string[]
			{
				"rest:24"
			}),
			new LifeEvent("any", "{n} casi se ahoga cruzando un río", "nearly drowned crossing a river", "Conmocionado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} se topó con un puma y sobrevivió", "the cougar's claws", "Conmocionado", -3, 96, null, 6, new string[]
			{
				"scar:claw",
				"rest:24"
			}),
			new LifeEvent("any", "{n} se perdió en una ventisca en las montañas", "el frío de las montañas", "Conmocionado", -2, 48, null, 8, new string[]
			{
				"rest:12"
			}),
			new LifeEvent("any", "{n} sufrió una insolación en el camino", "dolor de cabeza por insolación", "Enfermo", -2, 24, null, 8, new string[]
			{
				"rest:12"
			}),
			new LifeEvent("any", "A {n} le sacaron una muela picada en {t}", "la mandíbula dolorida", "Dolorido", -1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} comió carne en mal estado y pasó dos días enfermo", "el estómago aún revuelto", "Enfermo", -2, 48, null, 10, new string[]
			{
				"rest:24"
			}),
			new LifeEvent("any", "{n} perdió un diente en una pelea de taberna en {t}", "un hueco en la dentadura", "Humillado", -1, 48, null, 8, new string[]
			{
				"scar:beat"
			}),
			new LifeEvent("any", "{n} recibió una paliza de tres borrachos en {t}", "la paliza en {t}", "Humillado", -3, 72, null, 6, new string[]
			{
				"scar:beat"
			}),
			new LifeEvent("any", "{n} se cortó la mano desollando un ciervo", "una mano vendada", "Dolorido", -1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} tuvo pesadillas con los hombres que ha matado", "malos sueños", "Conmocionado", -2, 48, "aggr<60", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} abatió un gran ciervo", "el mayor ciervo que ha abatido", "Orgulloso", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} ahuyentó él solo a una manada de lobos", "los lobos que ahuyentó", "Orgulloso", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} pasó una noche perdido en el bosque", "una noche perdido en el bosque", "Conmocionado", -1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} vio un fantasma en el camino de noche, o eso dice", "algo que vio en el camino de noche", "Conmocionado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} encontró una pepita de oro en un arroyo", "una pepita de oro en el bolsillo", "Acaudalado", 3, 72, null, 5, Array.Empty<string>()),
			new LifeEvent("any", "{n} fue asaltado en el camino", "haber sido robado en el camino", "Amargado", -3, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} vio morir a un hombre de fiebre", "el rostro de un moribundo", "Conmocionado", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} enterró a un desconocido junto al camino", "una tumba junto al camino", "Estable", 0, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} ayudó a una familia cuyo carro se averió", "haber ayudado a una familia en apuros", "Orgulloso", 1, 48, "amistoso", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} encontró un perro callejero que ahora lo sigue", "el perro callejero que lo sigue", "Eufórico", 2, 96, "nopet", 6, new string[]
			{
				"pet"
			}),
			new LifeEvent("any", "Un lobo mató al perro de {n}", "su perro muerto", "De duelo", -3, 96, "pet", 10, new string[]
			{
				"nopet"
			}),
			new LifeEvent("any", "{n} durmió al raso bajo las estrellas", "una noche fría a la intemperie", "Estable", 0, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} presenció un ahorcamiento en {t}", "el ahorcamiento en {t}", "Conmocionado", -2, 48, null, 10, new string[]
			{
				"fear:2"
			}),
			new LifeEvent("any", "{n} ganó una carrera de caballos a las afueras de {t}", "haber ganado la carrera de caballos", "Orgulloso", 3, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} compró un caballo nuevo", "su caballo nuevo", "Orgulloso", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "El caballo de {n} quedó cojo y hubo que sacrificarlo", "haber tenido que sacrificar a su caballo", "Amargado", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} compró un sombrero nuevo en {t}", "un buen sombrero nuevo", "Orgulloso", 1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} se dio un afeitado y un baño caliente en {t}", "un baño caliente", "Eufórico", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} se hizo una fotografía en {t}", "su fotografía", "Orgulloso", 1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} se bebió hasta el agua de los floreros en {t}", "una larga noche en {t}", "Borracho", 1, 18, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} bebió solo en {t} durante tres días", "the bottle", "Borracho", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} juró dejar la bebida", "mantenerse sobrio", "Estable", 1, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} recayó y ha vuelto a beber", "volver a la botella", "Borracho", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} ganó a lo grande al póker en {t}", "un buen bote de póker", "Acaudalado", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} perdió su dinero a las cartas en {t}", "perder a las cartas", "Amargado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} perdió su caballo en una partida de cartas en {t}", "haber perdido su caballo a las cartas", "Amargado", -3, 72, "notarmy", 6, Array.Empty<string>()),
			new LifeEvent("any", "{n} ganó un pulso en {t}", "vencer al grandullón en un pulso", "Orgulloso", 2, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} ganó un concurso de tiro en {t}", "el concurso de tiro que ganó", "Orgulloso", 3, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} perdió un concurso de tiro en {t}", "haber perdido el concurso de tiro", "Humillado", -2, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} se metió en una discusión a gritos en {t}", "la discusión a gritos en {t}", "Amargado", -1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} fue expulsado de la taberna de {t}", "haber sido echado de la taberna", "Humillado", -2, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} pasó una noche en el calabozo de {t}", "una noche en el calabozo", "Encarcelado", -2, 24, "notarmy", 10, new string[]
			{
				"rest:24"
			}),
			new LifeEvent("any", "El sheriff de {t} multó a {n}", "la multa del sheriff", "Amargado", -1, 24, "notarmy", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} se libró hablando de un arresto en {t}", "haber engañado al sheriff", "Orgulloso", 2, 36, "notarmy", 10, Array.Empty<string>()),
			new LifeEvent("any", "Un comerciante estafó a {n} en {t}", "el comerciante tramposo", "Amargado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "A {n} le robaron el arma en {t}", "su arma robada", "Amargado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} compró un buen revólver nuevo en {t}", "su revólver nuevo", "Orgulloso", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} saldó sus deudas", "estar en paz con todos", "Eufórico", 2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} le debe dinero a la gente equivocada", "los hombres a los que debe dinero", "Conmocionado", -3, 96, "notarmy", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} prestó dinero a un tipo que se largó del pueblo", "el dinero que no volverá a ver", "Amargado", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "A {n} le leyeron la fortuna en {t}; no pintaba bien", "lo que le dijo la adivina", "Conmocionado", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} vio un espectáculo ambulante en {t}", "el espectáculo ambulante", "Eufórico", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} celebró su cumpleaños en {t}", "su cumpleaños", "Eufórico", 2, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} empezó a tocar la armónica", "aprender a tocar la armónica", "Eufórico", 1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} leyó una novela barata sobre forajidos", "una novela barata", "Eufórico", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} aprendió a escribir su nombre", "saber escribir su nombre", "Orgulloso", 1, 72, "corto", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} fue a la iglesia en {t}", "el sermón en {t}", "Estable", 1, 48, "notgodless", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} rezó toda la noche", "rezar por su alma", "Estable", 1, 24, "devout", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} maldijo a Dios tras una mala semana", "un Dios que no escucha", "Amargado", -1, 48, "godless", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} confesó sus pecados a un cura en {t}", "la confesión", "Estable", 2, 48, "catholic&notgodless", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} se casó en {t}", "su nueva esposa", "Eufórico", 4, 120, "single", 6, new string[]
			{
				"marry"
			}),
			new LifeEvent("any", "{n} se enamoró de una chica de taberna en {t}", "una chica en {t}", "Eufórico", 2, 72, "single", 10, Array.Empty<string>()),
			new LifeEvent("any", "Una mujer dejó plantado a {n} en {t}", "la mujer que lo dejó", "Amargado", -3, 96, "single", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} volvió a ser padre", "un recién nacido en casa", "Eufórico", 4, 96, "married", 8, new string[]
			{
				"child"
			}),
			new LifeEvent("any", "El hijo de {n} cayó enfermo", "su hijo enfermo", "Nostálgico", -3, 96, "married&children", 10, Array.Empty<string>()),
			new LifeEvent("any", "La esposa de {n} lo abandonó", "el abandono de su esposa", "Amargado", -4, 120, "married", 4, new string[]
			{
				"single"
			}),
			new LifeEvent("any", "La esposa de {n} murió de tisis", "su esposa fallecida", "De duelo", -5, 168, "married", 3, new string[]
			{
				"widow"
			}),
			new LifeEvent("any", "{n} fue a casa a ver a su familia", "ver a su familia", "Eufórico", 3, 72, "married", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} envió dinero a casa para su familia", "el dinero que envió a casa", "Estable", 1, 48, "married", 10, Array.Empty<string>()),
			new LifeEvent("any", "La esposa de {n} cayó enferma", "su esposa enferma, lejos de allí", "Nostálgico", -3, 96, "married", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} recibió una carta de casa: buenas noticias", "buenas noticias de casa", "Eufórico", 2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} recibió una carta de casa: malas noticias", "malas noticias de casa", "Nostálgico", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} se enteró de que su hermano en el Este había muerto", "su hermano fallecido", "De duelo", -3, 120, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} recibió la noticia de que su padre había muerto", "la muerte de su padre", "De duelo", -3, 120, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} visitó la tumba de su madre", "la tumba de su madre", "De duelo", -1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} habló de abandonar el territorio para siempre", "dejar todo esto atrás", "Nostálgico", -1, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} oyó historias sobre lo que has hecho", "las historias sobre ti", "Conmocionado", -2, 72, "knows", 10, new string[]
			{
				"fear:5"
			}),
			new LifeEvent("any", "{n} juró delante de testigos que te mataría", "matarte", "Amargado", -1, 72, "hate>=40", 10, new string[]
			{
				"hate:4"
			}),
			new LifeEvent("any", "{n} contó a todos en {t} cómo pensaba vencerte", "sus fanfarronadas sobre ti", "Orgulloso", 1, 48, "proud|hothead", 10, new string[]
			{
				"hate:2"
			}),
			new LifeEvent("any", "{n} tuvo pesadillas contigo", "tu cara", "Conmocionado", -2, 48, "fear>=30", 10, new string[]
			{
				"fear:4"
			}),
			new LifeEvent("any", "{n} estuvo limpiando y engrasando sus armas toda la tarde", "el día en que te encuentre", "Estable", 1, 24, "hate>=murderous", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} afiló su cuchillo pensando en ti", "tu garganta", "Amargado", 0, 48, "hate>=vengeful&hate<murderous", 10, new string[]
			{
				"hate:2"
			}),
			new LifeEvent("any", "{n} estuvo preguntando por ti en {t}", "por dónde cabalgas", "Estable", 0, 48, "hate>=vengeful&knows", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} decidió que no valía la pena morir por ti", "dejarlo pasar", "Estable", 1, 72, "coward|drifter", 6, new string[]
			{
				"hate:-6"
			}),
			new LifeEvent("any", "{n} vio tu cartel de se busca en {t}", "tu cara en el cartel de se busca", "Estable", 0, 48, "knows", 8, new string[]
			{
				"fear:2"
			}),
			new LifeEvent("gang", "{n} participó en un atraco a una diligencia que dio buen botín", "su parte de la diligencia", "Acaudalado", 2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} participó en un golpe que salió mal", "el golpe que salió mal", "Conmocionado", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} asaltó una granja con los muchachos", "el golpe de la granja", "Acaudalado", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} ayudó a asaltar un tren", "el golpe del tren", "Acaudalado", 3, 72, "rank>=2", 6, Array.Empty<string>()),
			new LifeEvent("gang", "{n} estuvo a punto de ser atrapado por la ley", "la ley pisándole los talones", "Conmocionado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} fue pasado por alto para un ascenso", "el puesto que cree merecer", "Amargado", -3, 120, "rank<=3", 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} fue elogiado por {b}", "el elogio de {b}", "Orgulloso", 2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{b} reprendió a {n} delante de los demás", "la bronca de {b}", "Humillado", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} hizo guardia tres noches seguidas", "demasiadas noches de guardia", "Amargado", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} y {f} se hicieron amigos", "su nuevo amigo {f}", "Eufórico", 1, 48, null, 10, new string[]
			{
				"friend"
			}),
			new LifeEvent("gang", "{n} acusó a {f} de quedarse parte del botín", "{f} y el dinero que falta", "Amargado", -2, 72, null, 10, new string[]
			{
				"sour"
			}),
			new LifeEvent("gang", "{n} enseñó a disparar a un novato", "el novato al que enseña", "Orgulloso", 1, 48, "rank>=2", 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} ganó la partida de cartas del campamento", "el bote que ganó a los muchachos", "Acaudalado", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} perdió su parte a las cartas contra {f}", "{f} y su suerte", "Amargado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} volvió a jurar lealtad a {l}", "su lealtad a {l}", "Estable", 1, 72, "leal", 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} pensó en dejar a {g}", "dejar esta vida", "Amargado", -2, 96, "fear>=40|coward", 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} brindó por los muertos de la banda", "los muchachos que no sobrevivieron", "De duelo", -1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} se quedó con el mejor petate del campamento", "un buen petate", "Eufórico", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("odriscoll", "{n} cantó canciones irlandesas junto al fuego toda la noche", "las viejas canciones", "Eufórico", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("odriscoll", "{n} oyó que {l} quiere resultados", "lo que espera {l}", "Conmocionado", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("odriscoll", "{n} robó un carro de whisky", "el whisky robado", "Acaudalado", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("raider", "{n} alzó un brindis por la vieja causa", "la guerra que nunca terminó", "Orgulloso", 1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("raider", "{n} hizo instrucción con los muchachos como en el 63", "la vieja instrucción", "Estable", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("raider", "{n} maldijo al Ejército botella en mano", "los casacas azules", "Amargado", -1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("klan", "{n} escondió su túnica de su esposa", "guardar su secreto", "Conmocionado", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("klan", "A {n} le dieron la espalda en {t}", "las miradas que recibe en {t}", "Humillado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "{n} trajo un uapití para las familias", "la caza del uapití", "Orgulloso", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "{n} se quedó sin comer cuando las raciones volvieron a escasear", "las raciones escasas", "Amargado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "{n} domó un caballo salvaje", "el caballo nuevo", "Eufórico", 1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "A {n} le negaron la entrada en el puesto comercial de Valentine", "the trading post", "Humillado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "{n} observó a los soldados cabalgar junto a la reserva", "los soldados", "Conmocionado", -1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "{n} se sentó con los ancianos toda la noche", "las palabras de los ancianos", "Estable", 1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "{n} discutió con {f} sobre luchar contra el Ejército", "la discusión con {f}", "Amargado", -1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "{n} perdió un caballo cruzando el río", "el caballo perdido", "De duelo", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "{n} pasó la noche a solas en las montañas", "la noche en las montañas", "Estable", 1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "{n} cayó enfermo con la fiebre que corre por el campamento", "la fiebre", "Enfermo", -2, 48, null, 10, new string[]
			{
				"rest:36"
			}),
			new LifeEvent("wapiti", "{n} ganó una carrera de caballos contra {f}", "la carrera", "Orgulloso", 2, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "{n} estuvo haciendo flechas nuevas toda la tarde", "las flechas nuevas", "Estable", 0, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} colgó un farol en el ciprés y esperó a que algo se acercara", "el farol en el ciprés", "Estable", 1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} oyó que lo llamaban por su nombre desde el agua tres noches seguidas", "la voz en el agua", "Conmocionado", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} volvió del pantano con barro hasta el pecho y no dice dónde estuvo", "dónde estuvo", "Estable", 0, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} enterró algo bajo el suelo de su choza", "lo que hay bajo el suelo", "Estable", 1, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} perdió dos dedos del pie por un caimán en aguas poco profundas", "el caimán", "Dolorido", -3, 72, null, 6, new string[]
			{
				"rest:24",
				"scar:claw"
			}),
			new LifeEvent("nightfolk", "{n} pasó la noche en vela con {l} sin que ninguno dijera palabra", "la noche con {l}", "Estable", 1, 48, "rank>=2", 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} halló el reloj de bolsillo de un hombre de Saint Denis en un nido de garza", "el reloj de bolsillo", "Acaudalado", 1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} talló una cara en un tocón de ciprés y habla con ella", "la cara en el tocón", "Eufórico", 1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} siguió una luz más allá de Lagras hasta el amanecer", "la luz más allá de Lagras", "Conmocionado", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} lleva nueve días sin hablar", "el silencio", "Estable", 0, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} cambió un frasco de dientes por una escopeta", "el trueque", "Orgulloso", 1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "A {n} lo vieron de día, y los demás ya no lo miran", "haber sido visto de día", "Humillado", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} cayó enfermo con la fiebre del pantano", "la fiebre del pantano", "Enfermo", -2, 48, null, 10, new string[]
			{
				"rest:36"
			}),
			new LifeEvent("nightfolk", "{n} cantó a los muertos entre los juncos hasta que callaron las ranas", "el canto", "Estable", 1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} y {f} remaron hasta la vieja plantación y volvieron con un remo menos", "la noche en la plantación", "Conmocionado", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} colgó el sombrero de un Raider en los árboles junto al camino", "el sombrero en los árboles", "Orgulloso", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} discutió con {f} sobre a cuál de los dos favorece el pantano", "{f}", "Amargado", -2, 48, null, 8, new string[]
			{
				"sour"
			}),
			new LifeEvent("nightfolk", "{n} sacó a un ahogado del agua y le registró los bolsillos", "los bolsillos del ahogado", "Acaudalado", 1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} ya no quiere pasar por la vieja iglesia de Shady Belle", "la vieja iglesia", "Conmocionado", -1, 48, "superstitious|coward", 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} silbó en la oscuridad hasta que algo le devolvió el silbido", "lo que devolvió el silbido", "Conmocionado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} acogió a un perro callejero y le puso el nombre de un muerto", "el perro", "Eufórico", 2, 48, "nopet", 6, new string[]
			{
				"pet"
			}),
			new LifeEvent("nightfolk", "{n} se quedó frente a una casa en Saint Denis toda la noche hasta que apagaron las lámparas", "la casa en Saint Denis", "Estable", 0, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} guarda ahora un cráneo de garza junto a su cama para dar suerte", "el cráneo de garza", "Estable", 1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{l} marcó a {n} por algo que jura que no hizo", "la marca de {l}", "Amargado", -3, 72, "rank<=2", 10, Array.Empty<string>()),
			new LifeEvent("nightfolk", "{n} y {f} vieron pasar un barco de vapor y contaron las luces", "el barco de vapor con {f}", "Eufórico", 1, 36, null, 8, new string[]
			{
				"friend"
			}),
			new LifeEvent("laramie", "{n} cambió tan bien la marca de un novillo robado que su dueño se lo volvió a comprar", "el novillo", "Orgulloso", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("laramie", "Un caballo dio una coz a {n} en el corral", "la coz", "Dolorido", -2, 48, null, 8, new string[]
			{
				"rest:24"
			}),
			new LifeEvent("laramie", "{n} cuidó del ganado de noche él solo bajo una tormenta", "la tormenta", "Estable", 1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("laramie", "{n} perdió su parte a las cartas en Blackwater", "la parte perdida", "Amargado", -2, 48, "gambler|greedy", 10, Array.Empty<string>()),
			new LifeEvent("laramie", "{n} vendió un caballo robado en Armadillo por el doble de lo que valía", "la venta", "Acaudalado", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("laramie", "{n} enlazó un mesteño salvaje en Great Plains", "el mesteño", "Eufórico", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("laramie", "{n} vio su propia cara en un cartel de la asociación de ganaderos", "el cartel", "Conmocionado", -1, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("laramie", "{n} y {f} condujeron una manada robada durante la noche hasta Hennigan's Stead", "la conducción con {f}", "Orgulloso", 1, 48, null, 8, new string[]
			{
				"friend"
			}),
			new LifeEvent("laramie", "Un ranchero disparó a {n} al sorprenderlo junto a la valla", "la escopeta del ranchero", "Conmocionado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("laramie", "{n} se quemó la mano con el hierro de marcar por error", "la quemadura", "Dolorido", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("laramie", "{n} oyó que {l} quiere que dirija su propia cuadrilla", "la palabra de {l}", "Orgulloso", 2, 72, "rank<=2", 10, Array.Empty<string>()),
			new LifeEvent("laramie", "{n} discutió con {f} sobre quién perdió la última manada", "{f}", "Amargado", -2, 48, null, 8, new string[]
			{
				"sour"
			}),
			new LifeEvent("delobo", "{n} cruzó el río para ver a su madre", "su madre al otro lado del río", "Nostálgico", -1, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("delobo", "{n} encendió una vela en la misión por los hombres caídos", "la vela por los muertos", "De duelo", -1, 48, "notgodless", 10, Array.Empty<string>()),
			new LifeEvent("delobo", "{n} ganó una carrera de caballos en Tumbleweed contra un americano", "la carrera", "Orgulloso", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("delobo", "{n} se gastó su parte del botín del tren en una sola noche en Armadillo", "la noche en Armadillo", "Eufórico", 1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("delobo", "Una cascabel bajo el petate estuvo a punto de morder a {n}", "la serpiente de cascabel", "Conmocionado", -1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("delobo", "{n} pasó tres días sin agua en Gaptooth Ridge", "los días de sequía", "Enfermo", -2, 48, null, 6, new string[]
			{
				"rest:24"
			}),
			new LifeEvent("delobo", "{n} pasó armas por el río a la luz de la luna", "las armas", "Acaudalado", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("delobo", "{n} tocó la guitarra junto al fuego hasta que el jefe le mandó callar", "la guitarra", "Eufórico", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("delobo", "{n} oyó que los rurales preguntan por él en Nuevo Paraíso", "los rurales", "Conmocionado", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("delobo", "{n} y {f} se escondieron de una patrulla del Ejército en un barranco seco todo el día", "el día en el barranco con {f}", "Estable", 1, 48, null, 8, new string[]
			{
				"friend"
			}),
			new LifeEvent("delobo", "Un tratante de caballos engañó a {n} en Armadillo", "el tratante de caballos", "Amargado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("delobo", "{n} escribió una carta a casa que nunca envió", "la carta", "Nostálgico", -1, 48, "married|children", 10, Array.Empty<string>()),
			new LifeEvent("army", "A {n} le tocó turno extra en Fort Wallace", "el servicio extra", "Amargado", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} cobró su soldada", "el día de paga", "Acaudalado", 1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "El sargento sometió a {n} a una dura instrucción", "la instrucción del sargento", "Amargado", -1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} fue felicitado por su oficial", "la felicitación", "Orgulloso", 2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} fue sancionado por beber de servicio", "haber sido sancionado", "Humillado", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} hizo guardia toda la noche bajo el frío", "la noche fría de guardia", "Enfermo", -1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} escribió una carta a su madre", "el hogar", "Nostálgico", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} perdió dinero apostando en un combate de boxeo en el fuerte", "la apuesta de boxeo", "Amargado", -1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} llevó un despacho a caballo hasta Valentine y volvió", "el viaje con el despacho", "Estable", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} recibió botas nuevas del intendente", "las botas nuevas", "Eufórico", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} fue castigado por perder su fusil", "el fusil perdido", "Humillado", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} persiguió desertores por las colinas", "los desertores", "Estable", 0, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("loner", "{n} llegó a {t} buscando trabajo", "finding work", "Estable", 0, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("loner", "{n} aceptó un trabajo domando caballos cerca de {t}", "breaking horses", "Estable", 1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("loner", "{n} trabajó una semana en un rancho cerca de {t}", "ranch work", "Estable", 1, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("loner", "{n} dejó su trabajo en {t}", "the job he quit", "Amargado", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("loner", "{n} vendió pieles en {t}", "a good price for furs", "Acaudalado", 1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("loner", "{n} jugó a las cartas con desconocidos en {t} y ganó", "the strangers' money", "Acaudalado", 2, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("loner", "{n} robó a un viajero en el camino", "the traveller he robbed", "Acaudalado", 2, 48, "poor", 10, Array.Empty<string>()),
			new LifeEvent("loner", "{n} fue contratado como escolta de una caravana", "the wagon train", "Estable", 1, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("loner", "{n} hizo un amigo en {t}", "a friend in {t}", "Eufórico", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "El viento se llevó el sombrero de {n} en el camino", "his good hat, gone", "Amargado", -1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} encontró un reloj de bolsillo en el barro a las afueras de {t}", "el reloj encontrado", "Eufórico", 1, 36, null, 8, Array.Empty<string>()),
			new LifeEvent("any", "{n} acabó a empujones por una mujer en {t}", "the woman in {t}", "Amargado", -1, 48, "single", 8, new string[]
			{
				"scar:beat"
			}),
			new LifeEvent("any", "{n} pagó a un dentista en {t} para que le sacara una muela picada", "the pulled tooth", "Dolorido", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "A {n} le robaron las botas mientras dormía", "sus botas robadas", "Amargado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} ganó un concurso de tiro a las afueras de {t}", "winning the shooting contest", "Orgulloso", 2, 72, "aggr>=50", 6, Array.Empty<string>()),
			new LifeEvent("any", "{n} perdió un concurso de tiro a las afueras de {t}", "haber perdido el concurso de tiro", "Humillado", -2, 48, null, 6, Array.Empty<string>()),
			new LifeEvent("any", "{n} bebió demasiado y durmió en una cuneta", "the ditch he woke in", "Humillado", -1, 24, "drunk", 14, Array.Empty<string>()),
			new LifeEvent("any", "A {n} lo echaron de una taberna en {t}", "the saloon in {t}", "Humillado", -2, 48, "drunk|hothead", 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} perdió el dinero de una semana jugando al faro en {t}", "the faro table", "Amargado", -2, 72, "gambler", 16, new string[]
			{
				"hate:1"
			}),
			new LifeEvent("any", "{n} desplumó una mesa de cartas en {t}", "the poker pot", "Acaudalado", 3, 48, "gambler", 14, Array.Empty<string>()),
			new LifeEvent("any", "{n} compró un fusil nuevo al armero de {t}", "the new rifle", "Orgulloso", 1, 48, "notarmy", 10, Array.Empty<string>()),
			new LifeEvent("any", "El caballo de {n} perdió una herradura a millas de cualquier parte", "the long walk", "Amargado", -1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} vio un ahorcamiento en {t}", "the hanging", "Conmocionado", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} ayudó a sacar un carro del barro", "a good turn", "Estable", 1, 24, "amistoso", 10, Array.Empty<string>()),
			new LifeEvent("any", "Una mula dio una coz a {n}", "the mule's kick", "Dolorido", -2, 48, null, 6, new string[]
			{
				"rest:12"
			}),
			new LifeEvent("any", "{n} comió carne pasada y estuvo dos días enfermo", "the bad meat", "Enfermo", -2, 48, null, 8, new string[]
			{
				"rest:24"
			}),
			new LifeEvent("any", "{n} jura que vio un fantasma junto a la vieja iglesia", "what he saw by the church", "Conmocionado", -2, 96, "superstitious", 18, new string[]
			{
				"fear:3"
			}),
			new LifeEvent("any", "{n} encendió una vela por su madre en {t}", "his mother", "Nostálgico", -1, 48, "devout|catholic", 8, Array.Empty<string>()),
			new LifeEvent("any", "{n} recibió una carta diciendo que su padre había muerto", "his father, gone", "De duelo", -4, 120, null, 4, Array.Empty<string>()),
			new LifeEvent("any", "{n} leyó sobre ti en el periódico", "what the paper said about you", "Conmocionado", -1, 48, "knows", 8, new string[]
			{
				"fear:2"
			}),
			new LifeEvent("any", "{n} fanfarroneó sobre ti en una taberna y se rieron de él", "the laughing", "Humillado", -2, 48, "braggart&knows", 14, new string[]
			{
				"hate:3"
			}),
			new LifeEvent("any", "{n} pasó toda la noche en vela con una botella, pensando en ti", "you", "Amargado", -1, 48, "hate>=vengeful&drunk", 14, new string[]
			{
				"hate:2"
			}),
			new LifeEvent("any", "{n} le dijo a un desconocido que te había visto, por un dólar", "the dollar", "Acaudalado", 1, 24, "traitor|greedy", 8, Array.Empty<string>()),
			new LifeEvent("any", "{n} se dio un baño y un afeitado en {t}", "a clean shave", "Eufórico", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "A {n} le adivinaron el futuro en {t}", "lo que le dijo la adivina", "Conmocionado", -1, 72, "superstitious", 14, Array.Empty<string>()),
			new LifeEvent("any", "{n} hirió de gravedad a un hombre en una pelea por nada", "the man he hurt", "Estable", 0, 48, "sadist", 16, new string[]
			{
				"scar:beat"
			}),
			new LifeEvent("any", "{n} grabó sus iniciales en un árbol junto al camino", "el hogar", "Estable", 0, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} vendió su viejo caballo y compró uno mejor", "el caballo nuevo", "Eufórico", 2, 48, "rich|greedy", 8, Array.Empty<string>()),
			new LifeEvent("any", "{n} perdió su fusil cruzando un río", "el fusil perdido", "Amargado", -2, 48, null, 6, Array.Empty<string>()),
			new LifeEvent("any", "{n} pilló a un ladrón en su campamento y lo dejó marchar", "the thief he let go", "Estable", 1, 24, "amistoso", 8, Array.Empty<string>()),
			new LifeEvent("any", "{n} pilló a un ladrón en su campamento y lo molió a palos", "the thief he beat", "Orgulloso", 1, 24, "aggr>=60", 8, Array.Empty<string>()),
			new LifeEvent("any", "{n} pasó una noche en una cueva esperando a que pasara la tormenta", "la tormenta", "Conmocionado", -1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("any", "{n} se quedó en vela tras una pesadilla sobre la guerra", "the war", "Conmocionado", -2, 72, "rank>=1", 4, Array.Empty<string>()),
			new LifeEvent("any", "{n} escribió su testamento, por si acaso", "dying", "Conmocionado", -1, 72, "fear>=40", 8, Array.Empty<string>()),
			new LifeEvent("any", "El hijo menor de {n} enfermó en casa", "su hijo enfermo", "De duelo", -3, 96, "married&children", 10, new string[]
			{
				"fear:2"
			}),
			new LifeEvent("any", "{n} envió su paga a casa para su esposa", "his family", "Estable", 1, 48, "married", 10, Array.Empty<string>()),
			new LifeEvent("any", "La esposa de {n} le escribió pidiéndole que volviera a casa", "his wife's letter", "Nostálgico", -2, 96, "married", 10, Array.Empty<string>()),
			new LifeEvent("any", "Un granjero cerca de {t} le dio un perro a {n}", "the new dog", "Eufórico", 2, 72, "nopet", 4, new string[]
			{
				"pet"
			}),
			new LifeEvent("any", "{n} se quedó dormido de guardia y lo pillaron", "falling asleep on watch", "Humillado", -2, 48, "notarmy", 6, Array.Empty<string>()),
			new LifeEvent("gang", "{n} y {f} salieron a robar a un jinete solitario", "the lone rider", "Acaudalado", 1, 36, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "Enviaron a {n} a explorar los caminos cerca de {t}", "scouting", "Estable", 0, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} discutió con {b} por el reparto de un golpe", "the split", "Amargado", -2, 72, null, 8, new string[]
			{
				"hate:1"
			}),
			new LifeEvent("gang", "{n} se ganó el favor de {b} en un golpe", "{b}'s favour", "Orgulloso", 2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} enterró a un hombre de {g} en las colinas", "the man he buried", "De duelo", -2, 96, null, 6, Array.Empty<string>()),
			new LifeEvent("gang", "{n} hizo guardia toda la noche mientras {g} dormía", "the long watch", "Estable", 0, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} robó una caja de whisky de un carro", "the whiskey", "Eufórico", 2, 48, "drunk", 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} se hizo un tatuaje nuevo en {t}", "the new tattoo", "Orgulloso", 1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("gang", "{n} enseñó a disparar a un joven de {g}", "the young one", "Orgulloso", 1, 48, "rank>=2", 10, Array.Empty<string>()),
			new LifeEvent("gang", "Pillaron a {n} sisando del botín de {g}", "being caught skimming", "Humillado", -3, 96, "greedy|traitor", 8, new string[]
			{
				"scar:beat"
			}),
			new LifeEvent("odriscoll", "{n} brindó a la salud de Colm en {t}", "Colm O'Driscoll", "Estable", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("odriscoll", "{n} oyó que Colm buscaba saldar cuentas con Dutch", "the old feud", "Amargado", -1, 48, null, 8, new string[]
			{
				"hate:1"
			}),
			new LifeEvent("odriscoll", "{n} cantó canciones irlandesas en el campamento hasta el alba", "las viejas canciones", "Nostálgico", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("raider", "{n} cabalgó en un desfile de viejos veteranos a las afueras de {t}", "the old cause", "Orgulloso", 2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("raider", "{n} quemó una bandera de la Unión en el campamento", "the old war", "Amargado", 0, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("raider", "{n} discutió por dinero con un hombre de los Braithwaite", "the Braithwaites", "Amargado", -1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("klan", "{n} escondió su túnica de su esposa", "his wife finding out", "Conmocionado", -1, 48, "married", 10, Array.Empty<string>()),
			new LifeEvent("klan", "Se rieron de {n} en {t}; la gente sabe lo que es", "being known", "Humillado", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} hizo instrucción bajo la lluvia toda la mañana", "the drill", "Amargado", -1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "A {n} le prometieron un permiso y luego se lo denegaron", "the leave he lost", "Amargado", -2, 72, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} mató de un tiro a una cascabel estando de patrulla", "the snake", "Estable", 0, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} perdió una apuesta en la carrera de caballos del regimiento", "the horse race", "Amargado", -1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("army", "{n} fue elogiado por el coronel Favours", "the colonel's praise", "Orgulloso", 2, 72, null, 6, Array.Empty<string>()),
			new LifeEvent("army", "{n} custodió prisioneros en el fuerte toda la noche", "the prisoners", "Estable", 0, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("loner", "{n} trabajó un día en los establos de {t}", "the stables", "Estable", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("loner", "{n} durmió al raso a las afueras de {t}", "the cold ground", "Amargado", -1, 24, "poor", 10, Array.Empty<string>()),
			new LifeEvent("loner", "{n} pagó una ronda a unos desconocidos en {t}", "the strangers", "Eufórico", 1, 24, "rich|friendly", 10, Array.Empty<string>()),
			new LifeEvent("loner", "Hicieron trampas a {n} jugando a las cartas en {t}", "the cheat", "Amargado", -2, 48, "gambler|dim", 10, Array.Empty<string>()),
			new LifeEvent("loner", "{n} cazó un ciervo y vendió la piel en {t}", "the hide", "Acaudalado", 1, 24, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "{n} vio a los soldados cruzar el río cerca de la reserva", "los soldados", "Amargado", -2, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "{n} contó las viejas historias a los niños", "the old stories", "Estable", 1, 48, null, 10, Array.Empty<string>()),
			new LifeEvent("wapiti", "{n} trajo un buen uapití", "the elk", "Orgulloso", 2, 48, null, 10, Array.Empty<string>())
		};

		// Token: 0x04000D54 RID: 3412
		public static LifeEvent[] All = WorldLife.BuiltIn;

		// Token: 0x04000D55 RID: 3413
		public const int ContentVersion = 2;

		// Token: 0x04000D56 RID: 3414
		public static int AddedFromBuiltIn;

		// Token: 0x04000D57 RID: 3415
		public static readonly List<string> Recent = new List<string>();
	}
}
