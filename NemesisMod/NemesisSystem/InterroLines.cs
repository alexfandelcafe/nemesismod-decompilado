using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x02000024 RID: 36
	internal static class InterroLines
	{
		// Token: 0x06000AD4 RID: 2772 RVA: 0x000F9F48 File Offset: 0x000F8148
		public static string Pick(string situation, Gang gang, Personality? temper, Random rng, string boss = null, string name = null, bool leaderOut = false, bool woman = false)
		{
			List<string> list = new List<string>();
			string text = ((gang != null) ? gang.Id : null) ?? "";
			if (!InterroLines.ByFaction.ContainsKey(text))
			{
				text = "";
			}
			string[] collection;
			if (InterroLines.ByFaction[text].TryGetValue(situation, out collection))
			{
				list.AddRange(collection);
				list.AddRange(collection);
			}
			Dictionary<string, string[]> dictionary;
			string[] collection2;
			if (temper != null && InterroLines.ByTemper.TryGetValue(temper.Value, out dictionary) && dictionary.TryGetValue(situation, out collection2))
			{
				list.AddRange(collection2);
			}
			string[] collection3;
			if (text != "" && InterroLines.ByFaction[""].TryGetValue(situation, out collection3))
			{
				list.AddRange(collection3);
			}
			if (leaderOut)
			{
				list.RemoveAll((string l) => l.Contains("{leader}") || (boss == null && l.Contains("{boss}")) || l.Contains("Colm") || l.Contains("the General") || l.Contains("Favours") || l.Contains("Rains Fall"));
			}
			if (woman)
			{
				list.RemoveAll((string l) => l.Contains("wife") || l.Contains("girl in") || l.Contains("a man go"));
			}
			if (list.Count == 0)
			{
				return null;
			}
			List<string> list2 = (from l in list
			where !InterroLines._recentSet.Contains(l)
			select l).ToList<string>();
			string text2 = ((list2.Count > 0) ? list2 : list)[rng.Next((list2.Count > 0) ? list2.Count : list.Count)];
			InterroLines._recent.AddLast(text2);
			InterroLines._recentSet.Add(text2);
			while (InterroLines._recent.Count > 60)
			{
				InterroLines._recentSet.Remove(InterroLines._recent.First.Value);
				InterroLines._recent.RemoveFirst();
			}
			string text3 = ((gang != null) ? gang.LeaderName : null) ?? "the boss";
			text2 = text2.Replace("{leader}", text3).Replace("{boss}", boss ?? text3).Replace("{name}", name ?? "");
			if (((gang != null) ? gang.Id : null) == "odriscoll" && (gang.LeaderName == null || !gang.LeaderName.Contains("Colm")))
			{
				text2 = text2.Replace("Colm's", InterroLines.<Pick>g__Short|5_3(gang.LeaderName) + "'s").Replace("Colm", InterroLines.<Pick>g__Short|5_3(gang.LeaderName));
			}
			if (((gang != null) ? gang.Id : null) == "lemoyneraiders")
			{
				text2 = text2.Replace("the General", (gang.LeaderName != null) ? InterroLines.<Pick>g__Short|5_3(gang.LeaderName) : "the boss");
			}
			if (((gang != null) ? gang.Id : null) == "army" && (gang.LeaderName == null || !gang.LeaderName.Contains("Favours")))
			{
				text2 = text2.Replace("Colonel Favours", gang.LeaderName ?? "the colonel").Replace("Favours", InterroLines.<Pick>g__Short|5_3(gang.LeaderName));
			}
			if (InterroLines.PlayerName != "Arthur Morgan")
			{
				text2 = text2.Replace("Arthur Morgan", InterroLines.PlayerName).Replace("Dutch's errand boy", "the one that got away");
			}
			return text2;
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000FA2A4 File Offset: 0x000F84A4
		// Note: this type is marked as 'beforefieldinit'.
		static InterroLines()
		{
			Dictionary<string, Dictionary<string, string[]>> dictionary = new Dictionary<string, Dictionary<string, string[]>>();
			Dictionary<string, Dictionary<string, string[]>> dictionary2 = dictionary;
			string key = "odriscoll";
			Dictionary<string, string[]> dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"Quítame las manos de encima, maldito animal.",
				"El perro de Van der Linde. Lo sabía.",
				"Colm se enterará de esto, chico.",
				"Has elegido al irlandés equivocado para agarrar.",
				"Vamos, entonces. ¿Qué quieres?",
				"Por Dios, qué peste echas. ¿Qué pasa?",
				"¿Crees que me asustas? Me han dado palizas mejores un domingo.",
				"¿Te envía Dutch? Claro que sí. Cobarde.",
				"Ah, el famoso Arthur Morgan. El chico de los recados de Dutch.",
				"Suéltame el cuello antes de que te arranque la mano de un mordisco.",
				"Te arrepentirás de haberte cruzado con los chicos, ya lo creo.",
				"¿Y bien? Suéltalo. No tengo todo el día.",
				"¿Así es como saludan ahora los de Dutch?",
				"Tienes ganas de morir si agarras a uno de los de Colm.",
				"Vete al cuerno y muérete en algún rincón tranquilo.",
				"¿Quieres algo? Pídelo por favor y quizá me ría."
			};
			dictionary3["open_scared"] = new string[]
			{
				"Jesús, María y José... no me mates.",
				"¡Tranquilo, tranquilo! ¡Solo soy un chico de Cork, no soy nadie!",
				"Por favor, señor; solo cabalgo con ellos por el dinero.",
				"Madre de Dios. Vale. Está bien, ¿qué quieres?",
				"¡No! ¡No le he hecho nada a Dutch, lo juro!",
				"Glorioso San Patricio... baja el arma, por favor.",
				"¡Soy nuevo, señor, no sé nada de nada!",
				"Ay Dios, ay Dios, no quiero morir en este país."
			};
			dictionary3["refuse"] = new string[]
			{
				"No te diré nada, escoria de Van der Linde.",
				"Colm me arrancaría la lengua. Prefiero que lo hagas tú.",
				"Vete a freír espárragos.",
				"Puedes matarme. Colm te matará más despacio.",
				"Ni una maldita palabra, chico.",
				"Nosotros no nos chivamos. Y menos a los de Dutch.",
				"Pregúntale a tu adorado Dutch. Él sabe mucho de salir corriendo.",
				"He visto a agentes Pinkerton esforzarse más que tú.",
				"Le sacarás más palabras a una oveja muerta.",
				"De mí sacarás lo mismo que los Pinkerton de Colm: nada.",
				"Lárgate de aquí.",
				"Mis labios están más cerrados que la bolsa de un cura.",
				"¿Hablarte a ti? Antes bebería cerveza inglesa.",
				"¿Quieres respuestas? Ve a confesarte.",
				"A Colm lo han cazado media vida. No ha durado tanto teniendo ratas cerca.",
				"Me lo llevaré a la tumba y escupiré en la tuya.",
				"No hay nada que puedas hacerme que Colm no haga peor.",
				"Pregúntale a Dutch por el hermano de Colm. Y a mí no me preguntes nada."
			};
			dictionary3["refuse_scared"] = new string[]
			{
				"¡No puedo! ¡Colm cuelga a los chivatos de los árboles, no lo entiendes!",
				"Por favor, se enteraría. Siempre se entera.",
				"Me colgarán en Six Point si hablo. Por favor...",
				"¡Te lo juro por mi madre, no puedo decirlo!",
				"¡Me desollaría, señor, me desollaría vivo!",
				"¡Por favor, no sabes lo que les hace a los que hablan!",
				"He visto lo que quedó del último que abrió la boca. Por favor.",
				"No me obligues. Colm tiene oídos en todas partes."
			};
			dictionary3["lock"] = new string[]
			{
				"Eso es todo lo que me sacarás sobre ese asunto.",
				"Vuelve a preguntarme eso y se lo preguntarás a un cadáver.",
				"Eso no. Jamás.",
				"Puedes preguntarle al Papa. De mí no sacarás más.",
				"Aquí se acaba el tema, chico.",
				"Tengo la boca cerrada en eso, y así va a seguir."
			};
			dictionary3["hit"] = new string[]
			{
				"¡Ja! Pegas como un protestante.",
				"¿Eso es todo? Mi abuela pega más fuerte.",
				"Pagarás por eso, chico.",
				"¿Nada más? Mi hermana tiene mejor gancho de derecha.",
				"¡Ja! He recibido golpes peores en una taberna de Dublín.",
				"Sigue, me has aflojado un diente. ¿Estás orgulloso?"
			};
			dictionary3["hit_scared"] = new string[]
			{
				"¡Basta! ¡Por Cristo, para!",
				"¡En la cara no, en la cara no!",
				"¡Vale! ¡Está bien, ha quedado claro!",
				"¡Piedad! ¡Virgen Santa, piedad!",
				"¡No más, te lo suplico!",
				"¡Vale, vale, por Dios, está bien!"
			};
			dictionary3["cut"] = new string[]
			{
				"¡Me has cortado, maldito...!",
				"Sangre por sangre. Recuérdalo.",
				"Ah, maldito sucio... eso dejará cicatriz.",
				"Te devolveré eso multiplicado por diez, chico."
			};
			dictionary3["cut_scared"] = new string[]
			{
				"¡Por Dios, estoy sangrando! ¡Para!",
				"¡Guárdalo, guarda ese cuchillo!",
				"¡Dios mío, no deja de sangrar!",
				"¡El cuchillo no, por favor, el cuchillo no!"
			};
			dictionary3["shot"] = new string[]
			{
				"¡Aagh! ¡Me has disparado! ¡Me has disparado, bastardo!",
				"¡Colm te arrancará el corazón por esto!",
				"¡Me has pegado un tiro en la pierna, animal!",
				"Esa bala te la devolveré, Morgan."
			};
			dictionary3["shot_scared"] = new string[]
			{
				"¡Hablaré! ¡No vuelvas a dispararme, hablaré!",
				"Santo Dios... ¡vale, ESTÁ BIEN!",
				"¡Para, para! ¡Madre de Dios, me has dado!",
				"¡Diré lo que quieras! ¡Pero no sigas!"
			};
			dictionary3["near"] = new string[]
			{
				"Has fallado. Siempre fallas.",
				"¿Se supone que eso debe asustarme?",
				"¡Ja! ¿Dutch te enseñó a disparar así?",
				"Cerca. Pero no lo suficiente, chico."
			};
			dictionary3["near_scared"] = new string[]
			{
				"¡Por Dios! ¡Eso casi me arranca la oreja!",
				"¡No! ¡No, te lo suplico!",
				"¡Santo Dios! ¡Cuidado adónde apuntas con eso!",
				"¡Vale! ¡Por Cristo, estás loco!"
			};
			dictionary3["threat_defy"] = new string[]
			{
				"Hazlo entonces. Colm me vengará.",
				"No tienes lo que hay que tener.",
				"Te veré en el infierno, Morgan.",
				"Estarás muerto en una cuneta antes de una semana.",
				"Adelante. Los chicos brindarán por mí esta noche.",
				"Amenaza todo lo que quieras. Soy irlandés; nacemos para sufrir."
			};
			dictionary3["threat_scared"] = new string[]
			{
				"¡No! ¡No lo hagas, por favor, tengo una hermana en Boston!",
				"¡Madre de Dios, no!",
				"¡Por favor, me volveré a Irlanda, jamás volverás a verme!",
				"¡No! ¡No, soy demasiado joven!"
			};
			dictionary3["threat"] = new string[]
			{
				"Muchas palabras para ser un Van der Linde.",
				"¿Ah, sí, chico?",
				"Colm me ha dicho cosas peores en el desayuno.",
				"Ya veremos quién dice eso dentro de un mes."
			};
			dictionary3["bribe_loyal"] = new string[]
			{
				"Quédate tus sucios dólares. Colm es familia.",
				"¿Crees que vendería a los míos por dinero de Van der Linde?",
				"Métete tu dinero por donde no da el sol.",
				"Colm me acogió cuando no tenía nada. Eso no se compra."
			};
			dictionary3["bribe_greedy"] = new string[]
			{
				"Ese sí es un idioma que hablo.",
				"Ah, dinero. Haber empezado por ahí, chico.",
				"Por Dios, eso es más de lo que paga Colm en un mes.",
				"Ahora sí que nos entendemos."
			};
			dictionary3["bribe_rich"] = new string[]
			{
				"¿Crees que eso me impresiona? He robado más a hombres mejores que tú.",
				"Calderilla. Me he bebido más que eso en una sola noche."
			};
			dictionary3["bribe_poor"] = new string[]
			{
				"Dios te bendiga... Llevo dos días sin comer.",
				"Con eso tengo para medio pasaje de barco a casa. Vale, pregunta."
			};
			dictionary3["bribe"] = new string[]
			{
				"El dinero es dinero. Pregunta, pues.",
				"Unos dólares no me convertirán en rata. Pero adelante."
			};
			dictionary3["lead"] = new string[]
			{
				"Bien. Está bien, bastardo.",
				"Vale, está bien...",
				"Esto no lo has oído de mí.",
				"Jura que jamás dirás dónde lo oíste.",
				"Cristo, Colm me matará... está bien.",
				"Aquí tienes la verdad, por si te sirve."
			};
			dictionary3["lead_scared"] = new string[]
			{
				"¡Vale! ¡Te lo diré, te lo diré!",
				"Que Dios me perdone...",
				"No dejes que Colm sepa que fui yo...",
				"¡Te lo contaré todo, pero baja eso!",
				"Vale, vale, Santo Dios...",
				"¡Por favor, es todo lo que sé, lo juro!"
			};
			dictionary3["letgo"] = new string[]
			{
				"¿Me dejas marchar? Eres más blando de lo que dicen.",
				"No olvidaré esto, chico. Aún no sé de qué manera.",
				"Estás loco. Loco... Gracias.",
				"La próxima vez que nos crucemos, fingiré que no te he visto.",
				"No esperes que te invite a una pinta por esto.",
				"Colm me habría pegado un tiro. Qué mundo tan raro."
			};
			dictionary3["letgo_scared"] = new string[]
			{
				"¡Dios te bendiga, Dios te bendiga, me largo!",
				"¡No volverás a verme, lo juro por San Patricio!",
				"¡Gracias, gracias! ¡Me marcho a Boston ahora mismo!",
				"¡Me voy, señor, ya me voy!",
				"Virgen Santa, aún respiro..."
			};
			dictionary3["bargain_pay"] = new string[]
			{
				"Toma: ${dollars}, hasta el último centavo. Cógelo y nunca estuve aquí.",
				"${dollars}. Es el pasaje de barco a mi tierra. Cógelo.",
				"¿Quieres que te pague? ${dollars}. Ahora déjame levantarme, por Dios."
			};
			dictionary3["bargain_inform"] = new string[]
			{
				"Te diré por dónde cabalgan los chicos. Cada golpe. Solo déjame respirar.",
				"Colm me desollaría por esto... sí, te avisaré antes de cada golpe.",
				"Seré tus oídos en el campamento, que Dios me perdone."
			};
			dictionary3["bargain_owe"] = new string[]
			{
				"Entonces te debo una. Solo una, ¿eh?",
				"Un irlandés paga sus deudas. Una vez. Y luego en paz.",
				"Me apartaré la próxima vez, chico. Solo esa vez."
			};
			dictionary3["bargain_leave"] = new string[]
			{
				"Estaré en el próximo barco. Jamás volverás a verme.",
				"He terminado con los chicos y con este país. Lo juro.",
				"Cabalgaré tan lejos que necesitarás un mapa nuevo para odiarme."
			};
			dictionary3["bargain_again"] = new string[]
			{
				"Ya te compré mi vida una vez. Anda, acaba de una vez.",
				"Hicimos un trato la última vez. No hay segundo trato, chico."
			};
			dictionary3["bargain_plead"] = new string[]
			{
				"¡Tengo a mi madre en Cork, señor! ¡Déjame volver con ella!",
				"¡No soy nadie, lo juro! ¡Déjame ir y nunca me verás!",
				"¡Por favor, solo me uní por la comida!"
			};
			dictionary2[key] = dictionary3;
			Dictionary<string, Dictionary<string, string[]>> dictionary4 = dictionary;
			string key2 = "lemoyneraiders";
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"Suéltame, maldito oportunista norteño.",
				"No eres de Lemoyne. Te lo huelo a leguas.",
				"Otro yanqui que se cree dueño de este estado.",
				"Luché en Petersburg. ¿Te crees que me asustas?",
				"El General se enterará de esto.",
				"Estás pisando suelo de Lemoyne, forastero.",
				"Quita tus sucias manos de encima a un veterano.",
				"{leader} te hará colgar en Fort Brennand.",
				"Lemoyne no necesita gente de tu calaña, señor.",
				"¿Eres algún agente yanqui o solo un ladrón?",
				"Me he visto en aprietos peores que este. Pregúntale a Sherman.",
				"¿Y bien? Habla de una vez, norteño."
			};
			dictionary3["open_scared"] = new string[]
			{
				"Tranquilo ahora... tranquilo. Solo soy un granjero, lo juro.",
				"¡No dispares, tengo esposa en Rhodes!",
				"Señor, ten piedad... ¿qué quieres de mí?",
				"Por favor, señor, solo me uní por la comida.",
				"¡Ni siquiera estuve en la guerra, lo juro!",
				"Señor, no dejes que acabe así. Aquí no."
			};
			dictionary3["refuse"] = new string[]
			{
				"Antes besaría la bota de un yanqui.",
				"No hablé ante los hombres de Sherman. Menos voy a hablar contigo.",
				"La Causa no ha muerto, señor. Y mi boca sigue cerrada.",
				"Vuélvete al norte, que es de donde eres.",
				"No sacarás nada de un hijo de Lemoyne.",
				"Mi padre murió en Shiloh con la boca cerrada. Yo haré lo mismo.",
				"El General trata bien a sus hombres. No me harás traicionarlo.",
				"A preguntas de norteño, respuestas para norteño: ninguna.",
				"Mantuve la lengua quieta ante oficiales yanquis. No eres nada al lado de ellos.",
				"Lemoyne guarda sus propios secretos.",
				"Tendrás que pegarme un tiro, yanqui.",
				"{leader} nunca nos falló. Yo no le fallaré a él.",
				"Te diré adónde puedes irte: al norte.",
				"Ni una palabra. Ni para ti ni para ningún yanqui.",
				"Perdimos la guerra, pero no hemos perdido nuestro orgullo.",
				"Pregúntale a los oportunistas del norte. Ellos saben todo lo que está en venta."
			};
			dictionary3["refuse_scared"] = new string[]
			{
				"Me colgarían del roble del juzgado si hablara.",
				"Por favor... el General no perdona.",
				"No puedo. Quemarán mi casa con mi mujer dentro.",
				"Me emplumarían con brea, señor. O algo peor.",
				"¡Por favor, {leader} tiene hombres en cada parroquia!",
				"No puedo hablar. Sabrían que he sido yo."
			};
			dictionary3["lock"] = new string[]
			{
				"Esa es mi última palabra al respecto, yanqui.",
				"Pregunta cuanto quieras. Esa puerta está cerrada.",
				"He terminado de hablar, yanqui.",
				"Eso está enterrado, señor. Tan hondo como la Causa."
			};
			dictionary3["hit"] = new string[]
			{
				"Me ha alcanzado metralla de la Unión. Eso no es nada.",
				"Pegas como un intendente yanqui.",
				"¿Así es como pelean allá en el norte?",
				"Mi madre pegaba más fuerte con una vara.",
				"¿Eso es todo lo que te enseñaron en el Norte?",
				"Sigue. Ya me han saltado los dientes otras veces."
			};
			dictionary3["hit_scared"] = new string[]
			{
				"¡Basta! ¡Señor, basta!",
				"¡Para, para; soy un viejo soldado!",
				"¡Señor, ten piedad! ¡No más!",
				"¡Por favor! ¡Me rindo, me rindo!"
			};
			dictionary3["cut"] = new string[]
			{
				"Te colgarán por eso, muchacho.",
				"Córtame, pues. Lemoyne sangra por los suyos.",
				"Las bayonetas yanquis hicieron cosas peores en Chancellorsville.",
				"Eso te saldrá caro, yanqui. Recuérdalo."
			};
			dictionary3["cut_scared"] = new string[]
			{
				"¡Oh, Dios, cuánta sangre... para!",
				"¡No, por favor! ¡Me voy a desangrar!",
				"¡Señor, me has abierto en canal! ¡Para!",
				"¡Baje ese cuchillo, por favor, señor!"
			};
			dictionary3["shot"] = new string[]
			{
				"¡Argh! ¡El General te desollará!",
				"¡Maldito seas en el infierno!",
				"Tiroteado por un norteño. Qué mala suerte.",
				"¡Argh! ¡Yanqui cobarde!"
			};
			dictionary3["shot_scared"] = new string[]
			{
				"¡Piedad! ¡Lo diré, diré lo que sea!",
				"¡No vuelvas a disparar! ¡Me rindo, me rindo!",
				"¡Señor Jesús, hablaré! ¡Hablaré!",
				"¡No más! ¡No más, me rindo!"
			};
			dictionary3["near"] = new string[]
			{
				"¿A eso lo llamas disparar?",
				"Los chicos en Antietam disparaban más cerca que eso.",
				"Ja. Los chicos de Sherman tampoco sabían apuntar.",
				"Te hará falta más práctica que esa."
			};
			dictionary3["near_scared"] = new string[]
			{
				"¡Jesucristo! ¡No!",
				"¡Vale! ¡Señor, está bien!",
				"¡Señor! ¡Casi me matas!",
				"¡Cuidado! ¡Tranquilo con eso!"
			};
			dictionary3["threat_defy"] = new string[]
			{
				"He mirado a los ojos a cosas peores en Gettysburg.",
				"Haz lo peor que sepas. Lemoyne tiene memoria.",
				"Lemoyne ha enterrado a hombres mejores que tú.",
				"Los yanquis llevan amenazándome desde que tenía quince años."
			};
			dictionary3["threat_scared"] = new string[]
			{
				"¡No, señor! ¡Por favor, señor!",
				"Tengo hijos, señor. Por favor.",
				"¡Por favor, señor, me marcharé del condado!",
				"¡No lo haga, señor! ¡Se lo suplico!"
			};
			dictionary3["threat"] = new string[]
			{
				"Palabrería yanqui.",
				"Hablas muy alto para ser un oportunista norteño.",
				"Ya veremos, señor. Ya lo veremos."
			};
			dictionary3["bribe_loyal"] = new string[]
			{
				"Dólares yanquis. Quédatelos.",
				"No vendo a los míos por ningún precio.",
				"No luché cuatro años para que me compre alguien como tú.",
				"Quédate tu dinero manchado de sangre, yanqui."
			};
			dictionary3["bribe_greedy"] = new string[]
			{
				"Bueno, vaya. El dinero es dinero, venga de quien venga.",
				"Un hombre tiene que comer, incluso en Lemoyne.",
				"Bueno, la Causa no paga mucho últimamente.",
				"Lemoyne es un estado pobre, señor. Lo aceptaré."
			};
			dictionary3["bribe_rich"] = new string[]
			{
				"Mi familia tenía tierras aquí antes de que llegara tu gente. Quédatelo.",
				"No necesito caridad yanqui."
			};
			dictionary3["bribe_poor"] = new string[]
			{
				"Dios sabe que la guerra se lo llevó todo. Está bien.",
				"Eso dará de comer a mis pequeños una semana. Pregunta."
			};
			dictionary3["bribe"] = new string[]
			{
				"Dinero. Bueno, algo es algo.",
				"Está bien. Pero esto no nos hace amigos."
			};
			dictionary3["lead"] = new string[]
			{
				"Vale. Pero no lo has sacado de mí.",
				"Está bien, maldito seas.",
				"Escucha bien, porque no lo repetiré dos veces.",
				"¿Quieres la verdad? Aquí la tienes.",
				"Que Dios me ayude. Está bien.",
				"Que esto quede entre nosotros y el Señor."
			};
			dictionary3["lead_scared"] = new string[]
			{
				"¡Vale, está bien! Que Dios me perdone...",
				"Te lo diré; pero no me hagas más daño.",
				"¡Vale, lo contaré! ¡Pero baja eso!",
				"¡Piedad, te lo contaré todo!"
			};
			dictionary3["letgo"] = new string[]
			{
				"Clemencia de un yanqui. Lo que hay que ver.",
				"Recordaré esto, señor. Aún no sé cómo.",
				"¿Me dejas ir? Vaya. Está bien.",
				"No creas que esto nos convierte en amigos.",
				"El General te habría ahorcado. Necio.",
				"Muy agradecido, supongo."
			};
			dictionary3["letgo_scared"] = new string[]
			{
				"¡Dios le bendiga, señor, Dios le bendiga!",
				"¡Me voy, me voy de vuelta a la granja!",
				"Alabado sea el Señor. Me marcho.",
				"Mil gracias, señor; no volverá a verme.",
				"Rezaré por usted, señor. De verdad que sí."
			};
			dictionary3["bargain_pay"] = new string[]
			{
				"${dollars}. Cada dólar yanqui que tengo. Cógelo.",
				"Aquí tienes ${dollars}; cógelo y deja que un hombre vuelva a casa.",
				"${dollars}. La guerra se llevó el resto."
			};
			dictionary3["bargain_inform"] = new string[]
			{
				"Te diré adónde van los muchachos. Cada golpe, antes de que monten.",
				"Dios me ayude... Te avisaré de lo que planea el General.",
				"Seré tu infiltrado. Solo déjame vivir."
			};
			dictionary3["bargain_owe"] = new string[]
			{
				"Entonces estoy en deuda contigo. Un sureño paga sus deudas.",
				"Una vez, señor. Me retiraré una vez. Y estaremos en paz.",
				"Por mi honor que te debo una. Una sola vez."
			};
			dictionary3["bargain_leave"] = new string[]
			{
				"Me iré al oeste. Lejos de Lemoyne. Tienes mi palabra.",
				"He terminado con la Causa. Dejaré el estado esta noche.",
				"Cogeré a mi familia y me iré. Nunca volverás a verme."
			};
			dictionary3["bargain_again"] = new string[]
			{
				"Ya te compré mi vida una vez. No queda más clemencia.",
				"Hicimos un trato la última vez, yanqui. No abuses."
			};
			dictionary3["bargain_plead"] = new string[]
			{
				"¡Tengo hijos pequeños en Rhodes, señor! ¡Por favor!",
				"¡Solo cabalgaba con ellos por la paga! ¡Déjame ir a casa!",
				"Soy un viejo soldado, señor. Déjeme morir en mi cama."
			};
			dictionary4[key2] = dictionary3;
			Dictionary<string, Dictionary<string, string[]>> dictionary5 = dictionary;
			string key3 = "army";
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"Estás agrediendo a un soldado de los Estados Unidos.",
				"Suéltame y quizá solo te ahorquen una vez.",
				"El coronel Favours pondrá a cada soldado de Fort Wallace tras de ti.",
				"Soldado raso. Eso es todo lo que me sacarás. Soldado raso.",
				"Colgarás de una soga por esto, forajido.",
				"Suéltame. Es una orden.",
				"Acabas de cometer el peor error de tu vida.",
				"Hay una patrulla por aquí cerca. Te encontrarán.",
				"¿Sabes cuál es la pena por esto? ¿Lo sabes?",
				"Soy un soldado, forajido. Me han entrenado para cosas peores.",
				"{leader} cuelga a hombres como tú y luego duerme a pierna suelta.",
				"Quita las manos del uniforme."
			};
			dictionary3["open_scared"] = new string[]
			{
				"¡Tranquilo, tranquilo! ¡Solo soy un soldado raso, solo cumplo órdenes!",
				"¡No dispares, señor, me alisté por la paga!",
				"Por favor... solo tengo diecinueve años.",
				"¡No dispares! ¡Solo llevo un año en filas!",
				"¡Por favor, yo solo conduzco el carro de suministros!",
				"¡Por favor, yo solo cocino para la compañía!"
			};
			dictionary3["refuse"] = new string[]
			{
				"Hice un juramento.",
				"Nombre y rango, forajido. Es todo lo que te debe el Ejército.",
				"No sacarás nada. No de un soldado.",
				"El Coronel me sometería a un consejo de guerra por menos.",
				"Te colgarán de todos modos. No pienso ayudarte.",
				"No hablo con un ladrón común.",
				"Váyase al infierno. Señor.",
				"El Ejército no negocia con bandidos.",
				"Responderé ante el Coronel, no ante ti.",
				"Puedes pegarme. Ya me han pegado los sargentos.",
				"Soldado raso del Ejército de los Estados Unidos. Y punto.",
				"Cada palabra que te diga le costará la vida a un compañero. Ni hablar.",
				"Prefiero el calabozo antes que hablar contigo.",
				"Es el reglamento. No hablo."
			};
			dictionary3["refuse_scared"] = new string[]
			{
				"¡Me harían un consejo de guerra! ¡Me fusilarían!",
				"No puedo. Favours me haría azotar.",
				"Por favor, lo perdería todo: mi paga, mi pensión...",
				"¡Si el Coronel se enterara, estaría encadenado de por vida!",
				"¡Por favor, fusilan a los desertores, me fusilarían a mí también!",
				"¡No puedo! ¡Me expulsarían con deshonor y me colgarían después!"
			};
			dictionary3["lock"] = new string[]
			{
				"Eso es confidencial. Para ti, todo lo es.",
				"Sin más comentarios, forajido.",
				"Eso es todo lo que el Ejército va a decirte.",
				"Petición denegada, forajido."
			};
			dictionary3["hit"] = new string[]
			{
				"Nuestro sargento pega más fuerte en la instrucción.",
				"Golpear a un soldado. Súmalo a tu lista de cargos.",
				"La instrucción básica era peor que esto.",
				"Eso quedará muy bien en tu juicio."
			};
			dictionary3["hit_scared"] = new string[]
			{
				"¡Basta! ¡Por favor, para!",
				"¡Vale! ¡Por Cristo, está bien!",
				"¡Basta! ¡Basta, por el amor de Dios!",
				"¡Por favor! ¡Solo soy un soldado raso!"
			};
			dictionary3["cut"] = new string[]
			{
				"Te colgarán dos veces por eso.",
				"¡Médico! ...Maldito seas.",
				"Otro cargo más en tu contra.",
				"He recibido coces peores de una mula."
			};
			dictionary3["cut_scared"] = new string[]
			{
				"¡Me has cortado, estoy malherido! ¡Para!",
				"¡Por favor! ¡Me desangraré aquí fuera!",
				"¡Cristo, es profundo! ¡Para!",
				"¡Llama al cirujano! ¡Por favor!"
			};
			dictionary3["shot"] = new string[]
			{
				"¡Argh! ¡Has disparado a un soldado de la República!",
				"¡Enviarán a toda la caballería por esto!",
				"¡Acabas de firmar tu propia sentencia!",
				"¡Argh! ¡Todo el regimiento te dará caza!"
			};
			dictionary3["shot_scared"] = new string[]
			{
				"¡No! ¡Te lo diré, te lo diré!",
				"¡Piedad! ¡Solo soy un soldado raso!",
				"¡Para! ¡Hablaré, por el amor de Dios!",
				"¡No dispares! ¡Me rindo, me rindo!"
			};
			dictionary3["near"] = new string[]
			{
				"La peor puntería que he visto al oeste del Dakota.",
				"¿Se supone que eso debe ponerme nervioso?",
				"Nuestros reclutas disparan mejor que eso.",
				"Estás malgastando cartuchos, forajido."
			};
			dictionary3["near_scared"] = new string[]
			{
				"¡Santo cielo! ¡No, por favor!",
				"¡Vale! ¡Está bien, no dispares!",
				"¡Jesús! ¡Alto el fuego!",
				"¡Alto el fuego! ¡Por favor!"
			};
			dictionary3["threat_defy"] = new string[]
			{
				"Mata a un soldado y el Ejército jamás dejará de darte caza.",
				"No te tengo miedo.",
				"Amenazar a un soldado. Otra soga más.",
				"El Ejército ha enterrado a mil como tú."
			};
			dictionary3["threat_scared"] = new string[]
			{
				"¡No! ¡Por favor, tengo una madre en Ohio!",
				"No lo haga, señor. Por favor.",
				"¡Por favor, desertaré, nunca volveré a vestir el uniforme azul!",
				"¡No, señor! ¡Tengo una chica esperándome en casa!"
			};
			dictionary3["threat"] = new string[]
			{
				"¿Es eso una amenaza contra los Estados Unidos?",
				"Tomo nota, forajido.",
				"Ya veremos qué opina el Coronel de eso."
			};
			dictionary3["bribe_loyal"] = new string[]
			{
				"El honor de un soldado no está en venta.",
				"Guárdatelo. No soy ningún soplón de taberna.",
				"Intento de soborno a un soldado. Otro cargo más.",
				"Mi lealtad es hacia la bandera, no hacia tu bolsa."
			};
			dictionary3["bribe_greedy"] = new string[]
			{
				"Trece dólares al mes no dan para mucho. Está bien.",
				"El Ejército apenas me paga. Vale.",
				"El pagador lleva tres meses de retraso. Está bien.",
				"Bueno. Nadie en el fuerte tiene por qué enterarse."
			};
			dictionary3["bribe_rich"] = new string[]
			{
				"Mi familia se dedica a la banca en Filadelfia. Quédatelo.",
				"¿Crees que el hijo de un oficial necesita tu dinero?"
			};
			dictionary3["bribe_poor"] = new string[]
			{
				"Eso es la paga de un mes. Dios me ayude. Pregunta.",
				"Envío cada centavo a casa para mi madre. Está bien."
			};
			dictionary3["bribe"] = new string[]
			{
				"No es un gran soborno, pero vale.",
				"Bien. Pregunta rápido."
			};
			dictionary3["lead"] = new string[]
			{
				"Esto queda entre nosotros.",
				"Vale. Pero que esto nunca llegue a oídos del Coronel.",
				"Está bien. Escucha.",
				"Si alguien pregunta, me lo sacaste a golpes.",
				"Extraoficialmente. ¿Entendido?",
				"Solo lo diré una vez, forajido."
			};
			dictionary3["lead_scared"] = new string[]
			{
				"¡Te lo diré! ¡Pero déjame ir después!",
				"¡Vale, está bien, lo diré!",
				"¡No me hagas daño, te lo contaré todo!",
				"¡Vale, por Dios, está bien, ahí va!"
			};
			dictionary3["letgo"] = new string[]
			{
				"¿Estás... dejando marchar a un soldado?",
				"Esto no limpia tu nombre, forajido.",
				"Aun así tendré que informar de esto.",
				"Diré que te perdí el rastro en el bosque.",
				"Extraña clase de forajido.",
				"Agradecido. No hagas que me arrepienta."
			};
			dictionary3["letgo_scared"] = new string[]
			{
				"¡Gracias, gracias, volveré al fuerte!",
				"¡Ya me voy! ¡No diré que te he visto!",
				"¡Dios te bendiga, señor!",
				"¡He terminado con el ejército, lo juro!",
				"Desertaré. Me iré a casa. Gracias."
			};
			dictionary3["bargain_pay"] = new string[]
			{
				"${dollars}. Mi soldada del mes. Cógela.",
				"Toma: ${dollars}. Es todo lo que tiene un soldado raso.",
				"${dollars}. Cógelo y nunca te he visto."
			};
			dictionary3["bargain_inform"] = new string[]
			{
				"Te diré cuándo salen las patrullas. Todas y cada una.",
				"Te pasaré las órdenes. Que Dios me ayude si se enteran.",
				"Te enviaré aviso desde el fuerte. Solo déjame vivir."
			};
			dictionary3["bargain_owe"] = new string[]
			{
				"Entonces te debo una. Miraré hacia otro lado. Una vez.",
				"Una vez, forajido. Me retiraré una sola vez.",
				"Un soldado honra sus deudas. Una vez."
			};
			dictionary3["bargain_leave"] = new string[]
			{
				"Desertaré esta misma noche. No volverás a ver este uniforme.",
				"He terminado con el ejército. Volveré al Este.",
				"Me quitaré el uniforme azul y me iré a casa. Lo juro."
			};
			dictionary3["bargain_again"] = new string[]
			{
				"Ya me dejaste ir una vez. No habrá una segunda.",
				"Ya hicimos nuestro trato. No me queda nada que darte."
			};
			dictionary3["bargain_plead"] = new string[]
			{
				"¡Solo soy un soldado raso! ¡Me alisté por la paga!",
				"¡Tengo una madre en Ohio, por favor!",
				"¡Ni siquiera le he disparado a nadie nunca, lo juro!"
			};
			dictionary5[key3] = dictionary3;
			Dictionary<string, Dictionary<string, string[]>> dictionary6 = dictionary;
			string key4 = "wapiti";
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"You are no different from the soldiers.",
				"Take your hands off me.",
				"Rains Fall said men like you would come.",
				"What do you want from me?",
				"My people have nothing left for you to take.",
				"Speak. Then let me go.",
				"You come with a gun, like all of them.",
				"Why do you do this?",
				"Let me stand, at least.",
				"The soldiers took our land. What will you take?",
				"Say what you came to say.",
				"There is nothing for you here."
			};
			dictionary3["open_scared"] = new string[]
			{
				"Please. I have a family on the reservation.",
				"I have done nothing to you.",
				"Do not hurt me. I am only a hunter.",
				"Please. My mother is old, and she needs me."
			};
			dictionary3["refuse"] = new string[]
			{
				"I will not speak.",
				"You have taken enough from us.",
				"I will not betray my people to you.",
				"Ask the soldiers. They know everything about us.",
				"My father died rather than talk. I am his son.",
				"Hurt me if you want. I will still say nothing.",
				"My words are not yours to take.",
				"You will learn nothing from me.",
				"Rains Fall would not forgive me. I would not forgive myself.",
				"Many men have asked. None have heard.",
				"I am silent. Do what you will.",
				"We have been lied to for many years. Now you want the truth from us?"
			};
			dictionary3["refuse_scared"] = new string[]
			{
				"If I speak, they will know. Please.",
				"I cannot. They would never forgive me.",
				"If I speak, my people suffer for it.",
				"Please. Do not ask me this."
			};
			dictionary3["lock"] = new string[]
			{
				"I have said all I will say about that.",
				"No. Not that.",
				"That is all.",
				"Do not ask again."
			};
			dictionary3["hit"] = new string[]
			{
				"The soldiers hit harder.",
				"This changes nothing.",
				"I have known worse pain.",
				"You only show who you are."
			};
			dictionary3["hit_scared"] = new string[]
			{
				"Stop. Please stop.",
				"Enough.",
				"Please. No more.",
				"Stop. I am hurt."
			};
			dictionary3["cut"] = new string[]
			{
				"You are like them. All of you.",
				"Is this how you treat a man?",
				"My blood is on your hands now.",
				"You cut a man who cannot fight back."
			};
			dictionary3["cut_scared"] = new string[]
			{
				"Stop! Stop, please!",
				"Enough... enough.",
				"Please, the bleeding...",
				"Stop. I beg you."
			};
			dictionary3["shot"] = new string[]
			{
				"You shot me... for nothing.",
				"My people will remember this.",
				"You are a coward.",
				"Rains Fall will hear what you did."
			};
			dictionary3["shot_scared"] = new string[]
			{
				"Do not shoot again. I will speak.",
				"Please... I will tell you.",
				"Stop. I will tell you what I know.",
				"No more. Please."
			};
			dictionary3["near"] = new string[]
			{
				"You will not frighten me that way.",
				"I have heard guns before.",
				"You waste your bullets.",
				"I am still here."
			};
			dictionary3["near_scared"] = new string[]
			{
				"No! Please!",
				"Stop! I will listen.",
				"Do not! Please!",
				"Enough. I hear you."
			};
			dictionary3["threat_defy"] = new string[]
			{
				"Then do it. I am not afraid to die.",
				"I have seen worse than you.",
				"Many of my people have died. I am no better than them.",
				"Kill me, and you will be no different from the soldiers."
			};
			dictionary3["threat_scared"] = new string[]
			{
				"Please. My children.",
				"No... please.",
				"Do not. My wife is waiting for me.",
				"Please. I want to see my children again."
			};
			dictionary3["threat"] = new string[]
			{
				"Words.",
				"I have heard such words from the soldiers.",
				"You say that easily."
			};
			dictionary3["bribe_loyal"] = new string[]
			{
				"I will not sell my people for your money.",
				"Keep it. It is worth nothing to me.",
				"My people are not for sale.",
				"The government also offered money. We remember what it bought."
			};
			dictionary3["bribe_greedy"] = new string[]
			{
				"Money buys food for the winter. Ask.",
				"...The reservation is hungry. Ask your question.",
				"The agent gives us little. I will take it.",
				"Coin buys blankets. Ask."
			};
			dictionary3["bribe_poor"] = new string[]
			{
				"My family is hungry. I will answer.",
				"This will buy flour. Ask."
			};
			dictionary3["bribe"] = new string[]
			{
				"I will take it. Ask.",
				"Money. Very well."
			};
			dictionary3["lead"] = new string[]
			{
				"I will tell you this once.",
				"Listen, then.",
				"You will leave us alone after.",
				"This, and nothing more.",
				"Hear me, then.",
				"I tell you this for my family's sake."
			};
			dictionary3["lead_scared"] = new string[]
			{
				"I will tell you. Then let me go.",
				"Alright. Alright.",
				"I will tell you. Please, then go.",
				"Listen. I will say it."
			};
			dictionary3["letgo"] = new string[]
			{
				"Me dejas ir. Lo recordaré.",
				"Mm. Entonces no todos vosotros sois iguales.",
				"Sigue tu camino. Yo seguiré el mío.",
				"Mi pueblo sabrá de esto.",
				"Es una extraña clemencia.",
				"No te daré las gracias. Pero no lo olvidaré."
			};
			dictionary3["letgo_scared"] = new string[]
			{
				"Me voy. Me marcho ahora.",
				"Gracias. Volveré a casa con mi familia.",
				"No regresaré por aquí.",
				"Mis hijos volverán a verme. Gracias."
			};
			dictionary3["bargain_pay"] = new string[]
			{
				"${dollars}. Es todo lo que llevo.",
				"Tómalo. ${dollars}. Déjame ir con mi familia."
			};
			dictionary3["bargain_inform"] = new string[]
			{
				"Te diré adónde cabalgan los jóvenes antes de que salgan.",
				"Enviaré aviso. Solo para que no mueran más de los nuestros."
			};
			dictionary3["bargain_owe"] = new string[]
			{
				"Entonces te debo una vida. Lo recordaré una vez.",
				"Una deuda. La pagaré y quedará saldada."
			};
			dictionary3["bargain_leave"] = new string[]
			{
				"Iré al norte. Lejos de aquí.",
				"Me marcharé con los demás. No volverás a verme.",
				"Iré donde no haya soldados."
			};
			dictionary3["bargain_again"] = new string[]
			{
				"Ya te di lo que tenía. No queda nada más.",
				"Me perdonaste una vez. Ese fue el trato."
			};
			dictionary3["bargain_plead"] = new string[]
			{
				"Tengo hijos. Me están esperando.",
				"No soy un guerrero. Déjame ir con mi pueblo.",
				"Por favor. Mi familia ya ha perdido bastante."
			};
			dictionary6[key4] = dictionary3;
			Dictionary<string, Dictionary<string, string[]>> dictionary7 = dictionary;
			string key5 = "klan";
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"You don't know who you're dealing with.",
				"Take your hands off me, boy. We're everywhere.",
				"The High Warden will hear about this.",
				"There's a lot more of us than you think.",
				"You lay a hand on me, you'll answer for it.",
				"We've got friends in high places, mister.",
				"You'll be sorry when the riders come.",
				"I got nothing to say to you."
			};
			dictionary3["open_scared"] = new string[]
			{
				"Don't hurt me! It's - it was just a meeting, that's all!",
				"Please, mister, I just stand at the back!",
				"I ain't really one of 'em, I swear! I just hold the horses!",
				"Please! My brother made me come along!"
			};
			dictionary3["refuse"] = new string[]
			{
				"You'll get nothing from me.",
				"We look after our own.",
				"Ask all you like, the brotherhood don't talk.",
				"You'll find out who we are soon enough.",
				"I ain't saying a damn word.",
				"Some oaths a man keeps.",
				"The High Warden'd know if I talked.",
				"You'll get the same as the sheriff gets. Nothing.",
				"I don't know nothing about no riders.",
				"Ain't nobody talks. Not ever."
			};
			dictionary3["refuse_scared"] = new string[]
			{
				"They'd burn me out if I talked!",
				"Please - they know where I live!",
				"They'd find me. They always find who talks!",
				"Please, mister, I got a family!"
			};
			dictionary3["lock"] = new string[]
			{
				"That's all you're getting.",
				"Never. Not that.",
				"I'm done.",
				"Not another word, you hear?"
			};
			dictionary3["hit"] = new string[]
			{
				"That all you got?",
				"You'll regret that.",
				"That supposed to hurt?",
				"You'll get yours."
			};
			dictionary3["hit_scared"] = new string[]
			{
				"Stop! Please stop!",
				"Alright! Enough!",
				"Please! I'm sorry!",
				"Don't! Don't hit me no more!"
			};
			dictionary3["cut"] = new string[]
			{
				"You're dead, you hear me? Dead!",
				"You'll pay for that!",
				"You crazy? You cut me!",
				"They'll come for you for that!"
			};
			dictionary3["cut_scared"] = new string[]
			{
				"I'm bleeding! Stop, please!",
				"Don't! Don't cut me again!",
				"Oh Lord, oh Lord, I'm bleeding!",
				"Stop! I'll do anything!"
			};
			dictionary3["shot"] = new string[]
			{
				"Argh! You'll burn for this!",
				"You son of a -",
				"You shot me! You actually shot me!",
				"Argh! They'll hang you for this!"
			};
			dictionary3["shot_scared"] = new string[]
			{
				"Don't shoot! I'll talk, I swear!",
				"Mercy! Please!",
				"Stop! Stop! I'll say it!",
				"Don't kill me! Please!"
			};
			dictionary3["near"] = new string[]
			{
				"Missed.",
				"That supposed to scare me?",
				"Ha. Bad shot.",
				"Try that again, I dare you."
			};
			dictionary3["near_scared"] = new string[]
			{
				"God! Don't! Please!",
				"Alright! Alright!",
				"Lord! Don't!",
				"Please! I'll talk!"
			};
			dictionary3["threat_defy"] = new string[]
			{
				"Go on then. They'll come for you.",
				"You won't.",
				"You don't know who my friends are.",
				"There's more of us than bullets in that gun."
			};
			dictionary3["threat_scared"] = new string[]
			{
				"No! Please, no!",
				"I'll do anything, mister!",
				"Please, mister, I ain't even important!",
				"I'll quit 'em! I swear I'll quit!"
			};
			dictionary3["threat"] = new string[]
			{
				"Big words.",
				"You'll be singing a different tune.",
				"Uh-huh."
			};
			dictionary3["bribe_loyal"] = new string[]
			{
				"Keep your money.",
				"Brotherhood ain't for sale.",
				"I don't take money from strangers.",
				"You can't buy a sworn man."
			};
			dictionary3["bribe_greedy"] = new string[]
			{
				"...Money talks, I guess.",
				"Alright. For that, alright.",
				"Well... that's real money.",
				"Alright, alright. Ask quick."
			};
			dictionary3["bribe_poor"] = new string[]
			{
				"I could use that. Ask.",
				"Times are hard. Fine."
			};
			dictionary3["bribe"] = new string[]
			{
				"Fine. But I never seen you.",
				"Alright, I'll take it."
			};
			dictionary3["lead"] = new string[]
			{
				"Fine. Fine!",
				"You didn't hear this from me.",
				"Alright, here's what I know.",
				"Keep my name out of it."
			};
			dictionary3["lead_scared"] = new string[]
			{
				"I'll tell you! Just let me go!",
				"Alright, alright!",
				"Okay! Okay, here it is!",
				"Don't hurt me, I'm telling you!"
			};
			dictionary3["letgo"] = new string[]
			{
				"Sabía que no tenías agallas.",
				"Te arrepentirás de esto. Estamos en todas partes.",
				"Je. Blando. Muy blando.",
				"No creas que con esto estamos en paz.",
				"Nos volveremos a ver. De noche.",
				"Bah. Vale."
			};
			dictionary3["letgo_scared"] = new string[]
			{
				"¡Ya me voy! ¡He terminado con todo esto!",
				"¡Gracias, señor; quemaré la capucha, lo juro!",
				"¡Me largo! ¡Ya me he ido!",
				"Nunca más, señor. Nunca más.",
				"Me iré a casa y no volveré a salir."
			};
			dictionary3["bargain_pay"] = new string[]
			{
				"${dollars}. Cógelo. Cógelo todo.",
				"Toma, ${dollars}; pero no le digas a nadie que me pillaste.",
				"${dollars}. No se lo digas a los demás."
			};
			dictionary3["bargain_inform"] = new string[]
			{
				"Te diré dónde es la próxima reunión. Todas.",
				"Te daré nombres. Solo déjame vivir.",
				"Te avisaré antes de la próxima salida. Lo prometo."
			};
			dictionary3["bargain_owe"] = new string[]
			{
				"Bien. Te debo una. Una vez.",
				"Me echaré atrás una vez. Nada más.",
				"Una vez, señor. Y luego en paz."
			};
			dictionary3["bargain_leave"] = new string[]
			{
				"Me iré del condado. Esta misma noche.",
				"He terminado con ellos. Me iré donde nadie me conozca.",
				"Jamás volverás a verme. Lo juro."
			};
			dictionary3["bargain_again"] = new string[]
			{
				"Ya te pagué una vez. ¿Qué más quieres?",
				"Ya me dejaste ir antes. Ese era el trato."
			};
			dictionary3["bargain_plead"] = new string[]
			{
				"¡Solo fui porque mi hermano me obligó!",
				"¡Nunca he hecho daño a nadie, lo juro!",
				"¡Por favor, señor, tengo esposa!"
			};
			dictionary7[key5] = dictionary3;
			Dictionary<string, Dictionary<string, string[]>> dictionary8 = dictionary;
			string key6 = "bronte";
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"You know who I work for? You're already dead.",
				"{leader} owns this city. Now he owns your funeral.",
				"Hands off the suit, cafone.",
				"You want something? Make an appointment.",
				"Madonna... you got no idea what you just did.",
				"In Saint Denis, nobody touches a man of the family.",
				"What is this, huh? What is this?",
				"The police chief eats at the boss's table. Think about that."
			};
			dictionary3["open_scared"] = new string[]
			{
				"Please, I just collect for the family, that's all!",
				"Dio mio, don't shoot! I got a mother!",
				"I'm nobody, I just stand at the door!",
				"Okay, okay - what you want, huh?"
			};
			dictionary3["refuse"] = new string[]
			{
				"Silence, my friend. That's how the family works.",
				"I don't know nothing. I don't see nothing.",
				"The family don't talk to outsiders.",
				"You ask the wrong man, amico.",
				"I talk, I'm in the river by morning. I don't talk.",
				"{leader} feeds rats to the alligators. Not me.",
				"Go ask the police. They work for us anyway.",
				"Silence is good business, mister."
			};
			dictionary3["refuse_scared"] = new string[]
			{
				"They'd feed me to the gators, I swear to God!",
				"Please, they cut out tongues for this!",
				"My whole family, they'd hurt them, capisci?",
				"I can't, I can't, the boss would know by supper."
			};
			dictionary3["lock"] = new string[]
			{
				"Basta. That's it.",
				"No more about that. Finito.",
				"That's family business. Closed."
			};
			dictionary3["hit"] = new string[]
			{
				"Ha, you hit like a dockworker's wife.",
				"That's all? In the Old Country they hit harder.",
				"You just made it personal, cafone."
			};
			dictionary3["hit_scared"] = new string[]
			{
				"Basta! Basta, please!",
				"Okay! Okay, no more!",
				"Madonna, stop!"
			};
			dictionary3["cut"] = new string[]
			{
				"You cut a man of the family. You're finished.",
				"This suit cost more than your horse."
			};
			dictionary3["cut_scared"] = new string[]
			{
				"Dio, the blood! Stop!",
				"Please, put the knife away!"
			};
			dictionary3["shot"] = new string[]
			{
				"You shot me! The family will bury you!",
				"Argh! You know who I am?"
			};
			dictionary3["shot_scared"] = new string[]
			{
				"Don't shoot! I talk, I talk!",
				"Please! Mercy, I tell you everything!"
			};
			dictionary3["near"] = new string[]
			{
				"In Saint Denis we shoot better.",
				"Missed. Lucky for me."
			};
			dictionary3["near_scared"] = new string[]
			{
				"Madonna! Watch it!",
				"Okay! Okay! Easy!"
			};
			dictionary3["threat_defy"] = new string[]
			{
				"Kill me, and you never sleep again.",
				"The family has a long memory, my friend.",
				"Go ahead. They'll find you face down in the bayou."
			};
			dictionary3["threat_scared"] = new string[]
			{
				"Please, I got kids, little ones!",
				"No, no, no! I do whatever you want!"
			};
			dictionary3["threat"] = new string[]
			{
				"Tough guy, huh?",
				"We'll see, amico.",
				"Big words for a country boy."
			};
			dictionary3["bribe_loyal"] = new string[]
			{
				"You insult the family with this.",
				"Put your money away. I'm no rat."
			};
			dictionary3["bribe_greedy"] = new string[]
			{
				"Now we talk business.",
				"Ah, a man who understands Saint Denis."
			};
			dictionary3["bribe_rich"] = new string[]
			{
				"You think I need your money? Look at my shoes.",
				"That's tip money, my friend."
			};
			dictionary3["bribe_poor"] = new string[]
			{
				"Okay. Okay, I need it. Ask.",
				"The family pays late. Fine."
			};
			dictionary3["bribe"] = new string[]
			{
				"Money is money. Ask.",
				"Alright. This stays between us."
			};
			dictionary3["lead"] = new string[]
			{
				"You didn't hear it from me, capisci?",
				"Okay. Listen good.",
				"This is between you and me."
			};
			dictionary3["lead_scared"] = new string[]
			{
				"Okay, okay, I tell you!",
				"Please, I tell you, then let me go!",
				"Madonna... here it is."
			};
			dictionary3["letgo"] = new string[]
			{
				"¿Me dejas marchar? La familia tiene memoria, amigo mío.",
				"Mm. Respeto. No me lo esperaba.",
				"Molto bene. Aunque no te debo nada.",
				"La próxima vez, quizá yo también te deje marchar.",
				"Extraño. La mayoría habría acabado el trabajo.",
				"Grazie. No se lo digas a nadie."
			};
			dictionary3["letgo_scared"] = new string[]
			{
				"¡Grazie, grazie! ¡Me marcho!",
				"Madonna... ¡gracias, gracias!",
				"¡Me vuelvo a casa con mi madre, no volverás a verme!",
				"¡Me voy! ¡Lo juro por mi madre!",
				"Gracias a Dios. Gracias a Dios."
			};
			dictionary3["bargain_pay"] = new string[]
			{
				"${dollars}. En efectivo. La familia siempre paga.",
				"Toma: ${dollars}. Cógelo, ¿capisci? Nunca nos hemos visto.",
				"${dollars}. Un buen precio por una vida, ¿no?"
			};
			dictionary3["bargain_inform"] = new string[]
			{
				"Te contaré lo que planea la familia. Cada golpe, antes de que ocurra.",
				"¿Quieres oídos en Saint Denis? Ya los tienes. Solo déjame vivir.",
				"Te avisaré. Si se enteran, acabaré en el pantano. Pero te avisaré."
			};
			dictionary3["bargain_owe"] = new string[]
			{
				"Entonces te debo un favor. La familia paga sus favores. Una vez.",
				"Un favor, amigo mío. Y luego en paz.",
				"Una deuda de honor. La pagaré una vez."
			};
			dictionary3["bargain_leave"] = new string[]
			{
				"Tomaré el próximo barco a Nápoles. Lo juro.",
				"He terminado con la familia. Dejo la ciudad esta noche.",
				"No volverás a ver mi cara por Saint Denis."
			};
			dictionary3["bargain_again"] = new string[]
			{
				"Ya te compré mi vida. No hay más tratos, amigo mío.",
				"Hicimos nuestro trato. La familia no paga dos veces."
			};
			dictionary3["bargain_plead"] = new string[]
			{
				"¡Tengo madre y hermana! ¡Por favor!",
				"¡Solo cargo cajas, signore! ¡No soy nadie!",
				"¡Por favor, solo soy un cochero para ellos!"
			};
			dictionary8[key6] = dictionary3;
			Dictionary<string, Dictionary<string, string[]>> dictionary9 = dictionary;
			string key7 = "murfree";
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"Pa's gonna hear you hollerin'.",
				"You smell like town. Smells good.",
				"Ain't nobody comes up the ridge and leaves.",
				"Brood's watchin' you right now, stranger.",
				"Let go a' me. I bite.",
				"Heh. You're a big one.",
				"What you want? Ain't got nothin' but my teeth.",
				"Kin's gonna find you. Then we eat good."
			};
			dictionary3["open_scared"] = new string[]
			{
				"Don't hurt me, I'm just the young'un!",
				"Please, mister, Pa makes me do it!",
				"I ain't done nothin', mister, they made me!",
				"Don't! Don't put me in the pit!"
			};
			dictionary3["refuse"] = new string[]
			{
				"Brood don't talk to outsiders.",
				"You ain't kin. You get nothin'.",
				"Heh. Go ask the ones in the cellar.",
				"Ain't sayin'. Pa'd skin me.",
				"Hills keep secrets. So do I.",
				"{leader}'ll have your hide for askin'.",
				"Go on back down the mountain.",
				"Nothin' for you. Nothin'."
			};
			dictionary3["refuse_scared"] = new string[]
			{
				"Pa'd chain me in the dark if I told!",
				"They'd put me in the pit with the others!",
				"Please, they'd know, they always know!"
			};
			dictionary3["lock"] = new string[]
			{
				"Ain't sayin' more.",
				"That's Brood business. Shut.",
				"No more. Hills got ears."
			};
			dictionary3["hit"] = new string[]
			{
				"Heh. Do it again.",
				"Pa hits harder'n that.",
				"That tickles, stranger."
			};
			dictionary3["hit_scared"] = new string[]
			{
				"Stop! Stop it!",
				"Ow! Please, no more!"
			};
			dictionary3["cut"] = new string[]
			{
				"Heh... I like the red.",
				"You'll bleed worse when kin finds you."
			};
			dictionary3["cut_scared"] = new string[]
			{
				"Ah! It hurts, it hurts!",
				"Stop! I'm leakin'!"
			};
			dictionary3["shot"] = new string[]
			{
				"You shot me! Kin's gonna gut you!",
				"Agh! Brood'll get you for that!"
			};
			dictionary3["shot_scared"] = new string[]
			{
				"Don't shoot! I'll tell, I'll tell!",
				"No more, mister! I'll say it!"
			};
			dictionary3["near"] = new string[]
			{
				"Heh. Missed me.",
				"Can't shoot neither, city man."
			};
			dictionary3["near_scared"] = new string[]
			{
				"Don't! Don't!",
				"Okay! Okay, mister!"
			};
			dictionary3["threat_defy"] = new string[]
			{
				"Kill me. Brood's got plenty more.",
				"Go on. I ain't scared a' dyin'."
			};
			dictionary3["threat_scared"] = new string[]
			{
				"Please! I don't wanna go in the ground!",
				"No! I'll be good, I'll be good!"
			};
			dictionary3["threat"] = new string[]
			{
				"Heh.",
				"Talk all you want.",
				"Pa says worse at supper."
			};
			dictionary3["bribe_loyal"] = new string[]
			{
				"Kin's kin. Keep your money.",
				"Brood don't need town money."
			};
			dictionary3["bribe_greedy"] = new string[]
			{
				"Shiny. Alright.",
				"Heh. I'll take that."
			};
			dictionary3["bribe_poor"] = new string[]
			{
				"Ain't never held that much. Ask.",
				"Alright. Alright, ask."
			};
			dictionary3["bribe"] = new string[]
			{
				"What's that for? ...Fine.",
				"Money. Huh. Ask then."
			};
			dictionary3["lead"] = new string[]
			{
				"Heh. Alright.",
				"You want it? Here.",
				"Don't tell Pa."
			};
			dictionary3["lead_scared"] = new string[]
			{
				"I'll tell! Just don't hurt me!",
				"Okay, okay, here!"
			};
			dictionary3["letgo"] = new string[]
			{
				"¿Me dejas ir? Papá no se lo va a creer.",
				"Je. Volverás. Siempre vuelven.",
				"Vaya. Nadie me había dejado ir antes.",
				"Sigue entonces. Las colinas acabarán contigo de todos modos.",
				"Recordaré tu cara, forastero. Muy bien.",
				"Eres un tipo raro, ¿eh?"
			};
			dictionary3["letgo_scared"] = new string[]
			{
				"¡Me voy a casa! ¡Ya me voy!",
				"¡No se lo digas a papá! ¡Me voy!",
				"¡Gracias, gracias, señor!",
				"¡Me esconderé en la hondonada, nunca me encontrarás!"
			};
			dictionary3["bargain_pay"] = new string[]
			{
				"${dollars}. Se lo quité a un tipo. Ahora es tuyo.",
				"Toma. ${dollars}. No preguntes de dónde salió."
			};
			dictionary3["bargain_inform"] = new string[]
			{
				"Te diré por dónde sale a cazar la familia. Todo.",
				"Te diré dónde los guardan. Solo déjame ir."
			};
			dictionary3["bargain_owe"] = new string[]
			{
				"Entonces te debo una. La familia paga una vez.",
				"Una vez, señor. Después te cazaré como al resto."
			};
			dictionary3["bargain_leave"] = new string[]
			{
				"Me iré lejos. Más allá de las montañas. Lo juro.",
				"Me iré de la cresta. Nunca me verás.",
				"He terminado con la camada. Me largaré para siempre."
			};
			dictionary3["bargain_again"] = new string[]
			{
				"Ya me dejaste ir una vez. No me queda nada con qué negociar.",
				"Ya tuviste tu trato, señor."
			};
			dictionary3["bargain_plead"] = new string[]
			{
				"¡Solo soy el pequeño! ¡Papá me obliga a hacerlo!",
				"¡Por favor, no quiero ir al pozo!",
				"¡Déjame ir, señor! ¡No hice daño a nadie!"
			};
			dictionary9[key7] = dictionary3;
			Dictionary<string, Dictionary<string, string[]>> dictionary10 = dictionary;
			string key8 = "skinners";
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"You'll make a fine pelt.",
				"Tall Trees don't forgive trespass.",
				"My brothers are in the trees. Watching.",
				"Let go. I'll take your skin slow.",
				"You come into our woods? Fool.",
				"Smell of you. Town man.",
				"What do you want, meat?",
				"Brothers'll hang you from a pine."
			};
			dictionary3["open_scared"] = new string[]
			{
				"Don't! I just set the traps!",
				"Please, I ain't the one does the skinning!",
				"Easy... easy, mister.",
				"Don't kill me in the woods. Not here."
			};
			dictionary3["refuse"] = new string[]
			{
				"Skinners don't talk.",
				"You'll get nothin' but my teeth.",
				"Ask the trees.",
				"I'll be skinnin' you before I'm talkin' to you.",
				"Brothers'd cut my tongue for less.",
				"Nothin'.",
				"Go on, cut. I've cut worse.",
				"Woods keep our secrets."
			};
			dictionary3["refuse_scared"] = new string[]
			{
				"They'd hang me up like a buck!",
				"Please... they'd skin me, they'd skin me!",
				"The brothers know. They always know."
			};
			dictionary3["lock"] = new string[]
			{
				"Done talkin'.",
				"That's all. Woods are listenin'.",
				"Nothing more."
			};
			dictionary3["hit"] = new string[]
			{
				"Hah. Bear hits harder.",
				"Do it again, I'll remember.",
				"That's nothin'."
			};
			dictionary3["hit_scared"] = new string[]
			{
				"Stop! Stop!",
				"Enough, enough!"
			};
			dictionary3["cut"] = new string[]
			{
				"Cut me? I cut for a living.",
				"I know a blade. That's a poor one."
			};
			dictionary3["cut_scared"] = new string[]
			{
				"Ah! Stop it!",
				"The blood! Stop!"
			};
			dictionary3["shot"] = new string[]
			{
				"Agh! Brothers'll have your hide!",
				"You'll hang from a tree for that!"
			};
			dictionary3["shot_scared"] = new string[]
			{
				"Don't shoot! I'll say!",
				"No more! I'll tell you!"
			};
			dictionary3["near"] = new string[]
			{
				"Missed. Brothers don't miss.",
				"You'd starve out here."
			};
			dictionary3["near_scared"] = new string[]
			{
				"Whoa! Don't!",
				"Alright! Alright, easy!"
			};
			dictionary3["threat_defy"] = new string[]
			{
				"Kill me. Brothers'll make a coat of you.",
				"I've seen men die harder than you'd ever manage."
			};
			dictionary3["threat_scared"] = new string[]
			{
				"Please! I'll leave the woods!",
				"Don't! I'll talk!"
			};
			dictionary3["threat"] = new string[]
			{
				"Big talk.",
				"Out here, words don't mean much.",
				"Hm."
			};
			dictionary3["bribe_loyal"] = new string[]
			{
				"Brothers ain't for sale.",
				"Keep your coin."
			};
			dictionary3["bribe_greedy"] = new string[]
			{
				"Pelts don't sell much. Alright.",
				"Money. Good. Ask."
			};
			dictionary3["bribe_poor"] = new string[]
			{
				"That's a season of furs. Ask.",
				"Alright. Need it."
			};
			dictionary3["bribe"] = new string[]
			{
				"Coin is coin. Ask.",
				"Huh. Alright."
			};
			dictionary3["lead"] = new string[]
			{
				"Alright. Hear it.",
				"Here.",
				"Tell no one."
			};
			dictionary3["lead_scared"] = new string[]
			{
				"I'll tell! Don't hurt me!",
				"Alright, alright, here it is!"
			};
			dictionary3["letgo"] = new string[]
			{
				"Mm. Sigue, entonces.",
				"Los hermanos no se enterarán de esto.",
				"No tendrás esa suerte dos veces.",
				"Extraño. Un cazador dejando escapar a la presa.",
				"Recordaré tu olor.",
				"Fine."
			};
			dictionary3["letgo_scared"] = new string[]
			{
				"Me voy. Entre los árboles. Desaparezco.",
				"Gracias. Abandonaré estos bosques.",
				"No volveré. Lo juro.",
				"Ya no estoy. No volverás a verme."
			};
			dictionary3["bargain_pay"] = new string[]
			{
				"${dollars}. De las pieles. Cógelo.",
				"Toma. ${dollars}. Ahora déjame en paz."
			};
			dictionary3["bargain_inform"] = new string[]
			{
				"Te diré dónde ponen las trampas los hermanos. Cada sendero.",
				"Te avisaré cuando salgan. Cada vez."
			};
			dictionary3["bargain_owe"] = new string[]
			{
				"Te debo una. Una vez. Y luego en paz.",
				"Una deuda. La pagaré."
			};
			dictionary3["bargain_leave"] = new string[]
			{
				"Me iré al norte. Lejos de Tall Trees.",
				"Dejaré a los hermanos. Esta noche.",
				"No volverás a verme en estos bosques."
			};
			dictionary3["bargain_again"] = new string[]
			{
				"Ya tuviste tu trato. No hay otro.",
				"Ya compré mi vida. No queda nada."
			};
			dictionary3["bargain_plead"] = new string[]
			{
				"Yo solo desuello lo que traen. Déjame ir.",
				"Por favor. No soy nadie para ellos.",
				"Solo cazo conejos con trampa, señor. Por favor."
			};
			dictionary10[key8] = dictionary3;
			Dictionary<string, Dictionary<string, string[]>> dictionary11 = dictionary;
			string key9 = "laramie";
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["letgo"] = new string[]
			{
				"¿Me sueltas? Ja. Error tuyo.",
				"No cuentes con que yo haga lo mismo por ti.",
				"Vaya, que me aspen.",
				"Les diré a los chicos que te has ablandado.",
				"Muy agradecido. Pero no esperes que te dé las gracias.",
				"Por mí perfecto."
			};
			dictionary3["letgo_scared"] = new string[]
			{
				"¡Me marcho! ¡Me voy cabalgando!",
				"¡Gracias, señor; he terminado con la banda!",
				"¡No volverás a verme, te lo juro!",
				"Me largo, señor. Ya me he ido."
			};
			dictionary3["bargain_pay"] = new string[]
			{
				"${dollars}. Toda mi parte. Cógela.",
				"Aquí tienes ${dollars}; con esto hemos terminado.",
				"${dollars}. Es un mes entero robando ganado."
			};
			dictionary3["bargain_inform"] = new string[]
			{
				"Te diré qué rancho atacarán después. Siempre.",
				"Te enviaré aviso antes de que salgan. Solo déjame vivir.",
				"Te mantendré al tanto de la banda. Dios me ayude."
			};
			dictionary3["bargain_owe"] = new string[]
			{
				"Bien. Te debo una. Una sola vez.",
				"Me mantendré al margen una vez. Eso es todo.",
				"Una deuda es una deuda. Una vez."
			};
			dictionary3["bargain_leave"] = new string[]
			{
				"Cabalgaré hacia Montana esta misma noche.",
				"He terminado con esta banda. Lo juro.",
				"Me iré a algún sitio donde nunca hayan oído hablar de mí."
			};
			dictionary3["bargain_again"] = new string[]
			{
				"Ya te pagué una vez. No pienso pagar dos veces.",
				"Teníamos un trato. No habrá otro."
			};
			dictionary3["bargain_plead"] = new string[]
			{
				"¡Yo solo cuido los caballos! ¡Por favor!",
				"¡Tengo una chica en Blackwater, señor!",
				"¡Soy nuevo, aún no he hecho nada!"
			};
			dictionary11[key9] = dictionary3;
			Dictionary<string, Dictionary<string, string[]>> dictionary12 = dictionary;
			string key10 = "delobo";
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["letgo"] = new string[]
			{
				"¿Me dejas ir, gringo? Mm.",
				"Gracias. Pero no creas que somos amigos.",
				"La próxima vez quizá no tenga tanta suerte.",
				"Loco. Estás loco.",
				"Recordaré esto, amigo.",
				"Bueno. Me voy."
			};
			dictionary3["letgo_scared"] = new string[]
			{
				"¡Gracias, gracias! ¡Me marcho!",
				"¡Dios te bendiga! ¡Me vuelvo a casa!",
				"¡Me vuelvo a México, lo juro!",
				"¡Gracias, señor! ¡Nunca volverá a verme!"
			};
			dictionary3["bargain_pay"] = new string[]
			{
				"${dollars}. Todo lo que tengo. Cógelo.",
				"Toma: ${dollars}. Ahora déjame ir.",
				"${dollars}, amigo. Un precio justo, ¿no?"
			};
			dictionary3["bargain_inform"] = new string[]
			{
				"Te diré por dónde cabalgan los Del Lobo. Cada vez.",
				"Te avisaré antes de cada golpe. Solo déjame vivir.",
				"Seré tus oídos, gringo. Dios me perdone."
			};
			dictionary3["bargain_owe"] = new string[]
			{
				"Entonces te debo una, gringo. Una vez.",
				"Una deuda. La pago y estamos en paz.",
				"Una vez. Te debo una."
			};
			dictionary3["bargain_leave"] = new string[]
			{
				"Me voy al sur. De vuelta al otro lado del río.",
				"He terminado con los Del Lobo. Me voy esta noche.",
				"Nunca volverá a verme, señor. Lo juro."
			};
			dictionary3["bargain_again"] = new string[]
			{
				"Ya compré mi vida. No más, gringo.",
				"Teníamos un trato. No hay otro."
			};
			dictionary3["bargain_plead"] = new string[]
			{
				"¡Tengo familia en Chihuahua! ¡Por favor!",
				"¡Yo solo sujeto los caballos, señor!",
				"¡Por favor, no soy nadie para ellos!"
			};
			dictionary12[key10] = dictionary3;
			Dictionary<string, Dictionary<string, string[]>> dictionary13 = dictionary;
			string key11 = "nightfolk";
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["letgo"] = new string[]
			{
				"...Me dejas marchar.",
				"El pantano te recordará.",
				"Je, je... nos veremos en la oscuridad.",
				"...Anda, entonces.",
				"Nos oirás. Por la noche.",
				"...Extraño."
			};
			dictionary3["letgo_scared"] = new string[]
			{
				"...Me voy. Al agua. Desaparezco.",
				"No me sigas. Por favor.",
				"...Gracias. Gracias.",
				"Me adentro en lo profundo. No me verás."
			};
			dictionary3["bargain_pay"] = new string[]
			{
				"${dollars}... de los muertos. Tómalo.",
				"Tómalo. ${dollars}. Déjame en paz."
			};
			dictionary3["bargain_inform"] = new string[]
			{
				"Te diré dónde esperan. En los árboles.",
				"Te susurraré sus senderos. Solo déjame ir."
			};
			dictionary3["bargain_owe"] = new string[]
			{
				"...Entonces te debo una. Una vez.",
				"Una vida por otra. Una vez."
			};
			dictionary3["bargain_leave"] = new string[]
			{
				"Me iré a lo profundo. Más allá del pantano.",
				"Nunca volverás a verme. Nunca.",
				"Dejaré el pantano. Lo juro."
			};
			dictionary3["bargain_again"] = new string[]
			{
				"...Ya tuviste tu trato.",
				"Nada más que dar. Nada."
			};
			dictionary3["bargain_plead"] = new string[]
			{
				"...Por favor. Por favor.",
				"No soy uno de ellos. En realidad no.",
				"Déjame volver al agua."
			};
			dictionary13[key11] = dictionary3;
			Dictionary<string, Dictionary<string, string[]>> dictionary14 = dictionary;
			string key12 = "";
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"What the hell do you want?",
				"Get off me!",
				"You picked the wrong man, mister.",
				"...What.",
				"I ain't done nothing to you.",
				"You got some nerve.",
				"Who the hell are you?",
				"Mister, you got the wrong fella.",
				"What is this? A robbery?",
				"Let go of me, damn you.",
				"You lost, friend?",
				"Make it quick, whatever it is.",
				"I don't want no trouble.",
				"Easy, now. What's this about?"
			};
			dictionary3["open_scared"] = new string[]
			{
				"W-what do you want? I don't know nothin'!",
				"Please, mister, I got money, take it!",
				"Don't kill me, please!",
				"Oh God, oh God, please...",
				"I ain't armed, mister! See?",
				"What'd I do? What'd I do?"
			};
			dictionary3["refuse"] = new string[]
			{
				"I ain't telling you nothing.",
				"Go to hell.",
				"Ask somebody who cares.",
				"I don't know, and I wouldn't tell you if I did.",
				"You're wasting your breath, mister.",
				"Find somebody else to bother.",
				"Not a chance.",
				"You can go to the devil.",
				"I keep my mouth shut. Lived this long doing it.",
				"Don't know, don't care.",
				"Ain't my business, and it sure ain't yours.",
				"I got nothing for you."
			};
			dictionary3["refuse_scared"] = new string[]
			{
				"I don't know! I swear I don't know!",
				"Please, I can't!",
				"They'd kill me, mister!",
				"Please, I'd be a dead man!",
				"I can't tell you, mister, honest to God!",
				"Don't make me say it, please!"
			};
			dictionary3["lock"] = new string[]
			{
				"That's all I'm saying on that.",
				"Don't ask me that again.",
				"That's the end of it.",
				"Leave it be, mister."
			};
			dictionary3["hit"] = new string[]
			{
				"That the best you got?",
				"Hit me again and see what happens.",
				"My old man hit harder.",
				"You'll regret that, mister."
			};
			dictionary3["hit_scared"] = new string[]
			{
				"Okay! OKAY! Stop, please!",
				"Enough! Enough!",
				"Please! No more!",
				"¡Me rindo! ¡Me rindo!"
			};
			dictionary3["cut"] = new string[]
			{
				"You cut me... you cut me!",
				"You crazy son of a -",
				"You're a madman!",
				"Ah! You'll pay for that!"
			};
			dictionary3["cut_scared"] = new string[]
			{
				"No more! Please, no more!",
				"Put it away! Please!",
				"I'm bleeding! Please stop!",
				"Not the knife! Please!"
			};
			dictionary3["shot"] = new string[]
			{
				"Gah! You son of a...",
				"You shot me!",
				"Argh! Damn you!",
				"You crazy bastard, you shot me!"
			};
			dictionary3["shot_scared"] = new string[]
			{
				"AAH! Stop! Stop, I'll talk!",
				"¡No dispares! ¡Por favor!",
				"No more! I'll tell you, I'll tell you!",
				"Please! I don't want to die!"
			};
			dictionary3["near"] = new string[]
			{
				"...You missed.",
				"Ha. Try again.",
				"Can't shoot neither, huh?",
				"Wasting good bullets."
			};
			dictionary3["near_scared"] = new string[]
			{
				"Jesus! Don't! Don't shoot!",
				"Okay! Okay!",
				"God almighty! Stop!",
				"Don't! I'm listening!"
			};
			dictionary3["threat_defy"] = new string[]
			{
				"Go on then. Do it.",
				"...You wouldn't.",
				"I ain't scared of you.",
				"You'd hang for it."
			};
			dictionary3["threat_scared"] = new string[]
			{
				"N-no! No, don't!",
				"Please! I'll do anything!",
				"Please, mister, I'll leave town!",
				"Don't! I got a family!"
			};
			dictionary3["threat"] = new string[]
			{
				"...You wouldn't.",
				"You talk a lot.",
				"Is that so.",
				"Sure you would.",
				"Uh huh.",
				"That right?"
			};
			dictionary3["bribe_loyal"] = new string[]
			{
				"You think I'd sell them out for your money? Keep it.",
				"They're my friends. Your money don't change that."
			};
			dictionary3["bribe_rich"] = new string[]
			{
				"Your money? I've got more than you'll ever see.",
				"Put it away. You can't afford me.",
				"I don't need your money, friend.",
				"That's an insult, mister."
			};
			dictionary3["bribe_greedy"] = new string[]
			{
				"...Now we're talking. Ask your questions.",
				"Money. Finally, some sense.",
				"Well, now. That's a different story.",
				"Money first. Then talk."
			};
			dictionary3["bribe_poor"] = new string[]
			{
				"...That's a week's eating. Alright. Ask.",
				"Lord knows I need it. Ask.",
				"That's rent. God help me. Ask.",
				"I'll take it. Hard times."
			};
			dictionary3["bribe"] = new string[]
			{
				"Money don't buy everything. But it helps.",
				"Alright. Ask, then.",
				"Fine. Ask.",
				"Alright, for that I'll talk."
			};
			dictionary3["lead"] = new string[]
			{
				"Alright. Alright.",
				"Fine.",
				"You want to know? Fine.",
				"Here's the truth of it.",
				"Alright, listen.",
				"Don't say where you heard it."
			};
			dictionary3["lead_scared"] = new string[]
			{
				"Okay! Okay, I'll tell you!",
				"I'll talk! Just stop!",
				"Alright! I'll say it!",
				"Please, here it is, all of it!"
			};
			dictionary3["letgo"] = new string[]
			{
				"...¿Eso es todo? ¿Puedo irme?",
				"Vaya. Gracias, supongo.",
				"Eres un tipo extraño, señor.",
				"No olvidaré esto.",
				"No esperes que te dé las gracias.",
				"Está bien. Me marcho.",
				"La próxima vez, déjame en paz.",
				"Bueno. Siempre hay una primera vez.",
				"Entonces seguiré mi camino.",
				"Mi día de suerte, supongo."
			};
			dictionary3["letgo_scared"] = new string[]
			{
				"¡Gracias! ¡Gracias, señor!",
				"¡Me voy! ¡Ya me voy!",
				"¡Dios te bendiga, señor!",
				"¡No volverás a verme, lo juro!",
				"Oh, gracias al Señor...",
				"Me marcho del pueblo. Esta noche.",
				"¡Nunca volveré a cruzarme en tu camino!"
			};
			dictionary3["bargain_pay"] = new string[]
			{
				"Toma: ${dollars}. Es todo lo que llevo encima. Cógelo y nunca nos hemos visto.",
				"${dollars}. Cógelo. Por favor.",
				"Ahí hay ${dollars}. Cada centavo. Déjame ir.",
				"${dollars}. Barato por una vida, ¿no crees?"
			};
			dictionary3["bargain_inform"] = new string[]
			{
				"I'll tell you what they're planning. Every job, before they ride. Just let me live.",
				"Te enviaré aviso. De cada golpe. Solo déjame ir.",
				"Te mantendré informado, señor. Hagan lo que hagan."
			};
			dictionary3["bargain_owe"] = new string[]
			{
				"Then I owe you. I pay my debts. Once. After that, we're square.",
				"Te debo una. Solo una.",
				"Una vez, señor. Me retiraré una vez."
			};
			dictionary3["bargain_leave"] = new string[]
			{
				"I'll go. Tonight. You'll never see my face again, I swear on my mother.",
				"Dejaré el territorio. Tienes mi palabra.",
				"Lo dejo. Me iré donde nadie me conozca.",
				"Saldré cabalgando esta noche y no volveré jamás."
			};
			dictionary3["bargain_again"] = new string[]
			{
				"I already bought my life off you. You want more, you'll have to kill me.",
				"Teníamos un trato. Ya no me queda nada.",
				"Me dejaste ir una vez. Ese fue el trato."
			};
			dictionary3["bargain_plead"] = new string[]
			{
				"I got a family! Please! I'll ride out of here tonight!",
				"¡Por favor, señor, tengo hijos!",
				"¡No soy nadie! ¡Déjame ir y jamás me verás!",
				"¡Me reformaré, lo juro! ¡Por favor!",
				"Tengo a mi madre enferma, señor. Por favor."
			};
			dictionary14[key12] = dictionary3;
			InterroLines.ByFaction = dictionary;
			Dictionary<Personality, Dictionary<string, string[]>> dictionary15 = new Dictionary<Personality, Dictionary<string, string[]>>();
			Dictionary<Personality, Dictionary<string, string[]>> dictionary16 = dictionary15;
			Personality key13 = Personality.Hothead;
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"Let me up and fight me like a man!",
				"You're dead. You hear me? DEAD.",
				"I'll rip your throat out!",
				"Untie me and we'll see who's tough!",
				"I'll break every bone in your body!",
				"Come closer. Come on!"
			};
			dictionary3["refuse"] = new string[]
			{
				"Hit me again. Go on, I dare you!",
				"That all you got?",
				"I'll gut you for this!",
				"Untie me and say that!",
				"I'll spit in your face first!",
				"You'll get nothin' but my fist!",
				"Make me, you yellow dog!",
				"Nobody scares me. Nobody!"
			};
			dictionary3["hit"] = new string[]
			{
				"I'll kill you for that!",
				"Do it again. Do it again!",
				"Ha! Is that it? Again!",
				"You'll be eatin' through a straw!"
			};
			dictionary3["cut"] = new string[]
			{
				"I'll cut you back twice as deep!",
				"You'll pay for every drop!"
			};
			dictionary3["shot"] = new string[]
			{
				"I'll live just to kill you!",
				"You'll die screamin' for that!"
			};
			dictionary3["threat_defy"] = new string[]
			{
				"Do it! Go on, DO IT!",
				"I'll come back from the grave for you!",
				"Kill me, and you'd better do it right!",
				"Threaten me again. I dare you!"
			};
			dictionary3["letgo"] = new string[]
			{
				"Esto no ha acabado. ¿Me oyes?",
				"La próxima vez no estaré atado.",
				"Te arrepentirás de esto. Te juro que lo harás."
			};
			dictionary16[key13] = dictionary3;
			Dictionary<Personality, Dictionary<string, string[]>> dictionary17 = dictionary15;
			Personality key14 = Personality.Coward;
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"P-please, I ain't nobody!",
				"Don't hurt me! I'll do anything!",
				"Oh Lord, oh Lord...",
				"Oh no, oh no, oh no...",
				"I'll tell you anything, just don't shoot!",
				"Please, I got nothin' to do with nothin'!"
			};
			dictionary3["refuse"] = new string[]
			{
				"Please... I can't, they'll kill me!",
				"Don't hurt me, mister, please!",
				"I- I don't know nothin', I swear!",
				"I want to tell you, I swear, I just can't!",
				"Please don't make me, mister!",
				"They'll know it was me!"
			};
			dictionary3["hit"] = new string[]
			{
				"Okay! OKAY! Stop, please!",
				"Don't! I'll be good!",
				"No! Please! I'm sorry!",
				"Ow! Ow, stop, please!"
			};
			dictionary3["shot_scared"] = new string[]
			{
				"I don't wanna die, I don't wanna die!",
				"Mercy! Mercy!"
			};
			dictionary3["threat_scared"] = new string[]
			{
				"Please! I'll tell you anything!",
				"No no no no...",
				"I'll do whatever you want!",
				"Please, please, please..."
			};
			dictionary3["lead_scared"] = new string[]
			{
				"Okay, okay, here, here it is!",
				"I'll tell you, I'll tell you everything!"
			};
			dictionary3["letgo"] = new string[]
			{
				"¿D-de verdad? ¿Puedo irme?",
				"¿Puedo irme? ¿De verdad puedo irme?"
			};
			dictionary3["letgo_scared"] = new string[]
			{
				"¡Gracias, gracias, gracias!",
				"¡Me voy, me voy, no dispares!"
			};
			dictionary17[key14] = dictionary3;
			Dictionary<Personality, Dictionary<string, string[]>> dictionary18 = dictionary15;
			Personality key15 = Personality.Proud;
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"Get your hands off me.",
				"Do you know who I am?",
				"You'll regret laying a hand on me.",
				"Unhand me, you brute.",
				"I don't speak to men like you.",
				"You've no idea who you've laid hands on."
			};
			dictionary3["refuse"] = new string[]
			{
				"You think fists'll make me crawl? Not me.",
				"I don't bow to the likes of you.",
				"Beat me all you want. I'm better than you.",
				"I won't lower myself to answer.",
				"You'll get nothing from me, ever.",
				"Do your worst. I'll not beg."
			};
			dictionary3["hit"] = new string[]
			{
				"Ha. My sister hits harder.",
				"Is that all? Pathetic.",
				"You strike like a schoolgirl.",
				"I've known stronger drunks."
			};
			dictionary3["threat_defy"] = new string[]
			{
				"You wouldn't dare.",
				"Threats. How common.",
				"I'd sooner die than bow to you.",
				"Your threats bore me."
			};
			dictionary3["bribe"] = new string[]
			{
				"You think I can be bought? How insulting.",
				"Put that away before you embarrass yourself."
			};
			dictionary3["bribe_rich"] = new string[]
			{
				"I've spent more than that on a pair of boots.",
				"Keep your pennies. You need them more than I do."
			};
			dictionary3["letgo"] = new string[]
			{
				"No te daré las gracias por ello.",
				"No esperes gratitud.",
				"Has cometido un error."
			};
			dictionary18[key15] = dictionary3;
			Dictionary<Personality, Dictionary<string, string[]>> dictionary19 = dictionary15;
			Personality key16 = Personality.Cold;
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"...",
				"Get on with it.",
				"I've nothing to say to you.",
				"Say what you want.",
				"Well?",
				"Ask your question."
			};
			dictionary3["refuse"] = new string[]
			{
				"...",
				"You're wasting your time.",
				"I've had worse from better men.",
				"No.",
				"Not today.",
				"Waste of breath.",
				"You won't get it.",
				"Keep going. Nothing changes."
			};
			dictionary3["hit"] = new string[]
			{
				"...Ugh.",
				"Finished?",
				"...That it?",
				"...Again, then."
			};
			dictionary3["threat_defy"] = new string[]
			{
				"Then do it.",
				"I don't scare.",
				"Everyone dies.",
				"Get on with it, then."
			};
			dictionary3["letgo"] = new string[]
			{
				"...",
				"Hm.",
				"Tú mismo."
			};
			dictionary19[key16] = dictionary3;
			Dictionary<Personality, Dictionary<string, string[]>> dictionary20 = dictionary15;
			Personality key17 = Personality.Greedy;
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"Whatever you want, it'll cost you.",
				"Let's talk business, friend.",
				"Everything's for sale. Including me.",
				"You got money? Then we can talk."
			};
			dictionary3["refuse"] = new string[]
			{
				"What's in it for me, huh?",
				"Information ain't free, friend.",
				"A man's gotta eat. Talking don't feed me.",
				"Make it worth my while.",
				"Dollars loosen my tongue. Fists don't.",
				"Pay me and I'll sing like a bird.",
				"Nothing for free, mister.",
				"My memory gets better with money."
			};
			dictionary3["bribe_greedy"] = new string[]
			{
				"That's more like it.",
				"Now you're speaking my language."
			};
			dictionary3["lead"] = new string[]
			{
				"For that? Alright.",
				"Seeing as you're paying...",
				"Pleasure doing business.",
				"Money well spent, friend."
			};
			dictionary3["letgo"] = new string[]
			{
				"Libre y aún conservo mi dinero. Buen día.",
				"¿Sin cobrarme? Qué generoso."
			};
			dictionary3["bargain_pay"] = new string[]
			{
				"${dollars}. Eres duro negociando."
			};
			dictionary20[key17] = dictionary3;
			Dictionary<Personality, Dictionary<string, string[]>> dictionary21 = dictionary15;
			Personality key18 = Personality.Loyal;
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"You'll get nothing. The boss is family.",
				"I'd die before I talk.",
				"I'll not betray my brothers.",
				"Do what you want. I stay true."
			};
			dictionary3["refuse"] = new string[]
			{
				"I ain't no rat. Never.",
				"The boss is family. You ain't.",
				"Kill me then. I ain't talking.",
				"My people trust me. I won't break that.",
				"Some men can't be broken. I'm one.",
				"I'd rather die than be a turncoat.",
				"They'd do the same for me.",
				"You'll never get a name out of me."
			};
			dictionary3["bribe_loyal"] = new string[]
			{
				"You can't buy loyalty, mister.",
				"Not for all the gold in Saint Denis."
			};
			dictionary3["threat_defy"] = new string[]
			{
				"Then I die loyal.",
				"Do it. I'll never betray them.",
				"My people will remember me.",
				"Then I die with my honor."
			};
			dictionary3["letgo"] = new string[]
			{
				"Mi gente seguirá dándote caza.",
				"This changes nothing."
			};
			dictionary21[key18] = dictionary3;
			Dictionary<Personality, Dictionary<string, string[]>> dictionary22 = dictionary15;
			Personality key19 = Personality.Drifter;
			dictionary3 = new Dictionary<string, string[]>();
			dictionary3["open"] = new string[]
			{
				"Ain't my fight, mister.",
				"I just ride where the work is.",
				"Look, I don't even like these people.",
				"I just met these fellas, mister.",
				"Whatever this is, I'm just passing through.",
				"Don't look at me. I'm hired help."
			};
			dictionary3["refuse"] = new string[]
			{
				"Ain't my fight, mister.",
				"Why should I care?",
				"Leave me be.",
				"I don't know them that well, honest.",
				"Why'd I stick my neck out for 'em?",
				"I don't know much, mister. Honest.",
				"I keep my head down. That's all.",
				"Not my problem."
			};
			dictionary3["lead"] = new string[]
			{
				"Ain't my business to protect 'em anyhow.",
				"Fine. I don't owe 'em nothing.",
				"Ain't like I owe 'em loyalty.",
				"Sure. Why not."
			};
			dictionary3["letgo"] = new string[]
			{
				"Por mí bien. De todos modos me marchaba.",
				"Para empezar, ni siquiera era mi pelea."
			};
			dictionary22[key19] = dictionary3;
			InterroLines.ByTemper = dictionary15;
			InterroLines._recent = new LinkedList<string>();
			InterroLines._recentSet = new HashSet<string>();
			InterroLines.PlayerName = "Arthur Morgan";
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000FE574 File Offset: 0x000FC774
		[CompilerGenerated]
		internal static string <Pick>g__Short|5_3(string full)
		{
			if (string.IsNullOrEmpty(full))
			{
				return "the boss";
			}
			string[] array = full.Split(new char[]
			{
				' '
			});
			if (array.Length <= 1 || !array[0].EndsWith("."))
			{
				return array[0];
			}
			return array[array.Length - 1];
		}

		// Token: 0x040007CF RID: 1999
		private static readonly Dictionary<string, Dictionary<string, string[]>> ByFaction;

		// Token: 0x040007D0 RID: 2000
		private static readonly Dictionary<Personality, Dictionary<string, string[]>> ByTemper;

		// Token: 0x040007D1 RID: 2001
		private static readonly LinkedList<string> _recent;

		// Token: 0x040007D2 RID: 2002
		private static readonly HashSet<string> _recentSet;

		// Token: 0x040007D3 RID: 2003
		public static string PlayerName;
	}
}
