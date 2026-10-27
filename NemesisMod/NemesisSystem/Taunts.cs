using System;
using System.Collections.Generic;
using System.Linq;

namespace NemesisSystem
{
	// Token: 0x02000070 RID: 112
	internal static class Taunts
	{
		// Token: 0x060012A2 RID: 4770 RVA: 0x0012687C File Offset: 0x00124A7C
		public static string Ambush(NemesisRecord rec, bool returningFromDead, Random rng, Func<Gang, NemesisRecord, string> leaderSaidBy = null)
		{
			List<string> list = new List<string>();
			if (returningFromDead)
			{
				list.Add("Me diste por muerto en " + rec.LastDefeatLocation + ". Deberías haberte asegurado.");
				list.Add("Hará falta más que eso para meterme bajo tierra.");
				if (rec.Limp)
				{
					list.Add("Camino cojeando por tu culpa. Pero no voy a necesitar correr.");
				}
				Scar scar = rec.Scars.LastOrDefault<Scar>();
				string item;
				string item2;
				if (scar != null && scar.Wound != null && Taunts.WoundBoasts.TryGetValue(WoundKeys.Base(scar.Wound), out item))
				{
					list.Add(item);
				}
				else if (scar != null && Taunts.ScarBoasts.TryGetValue(scar.DamagePack, out item2))
				{
					list.Add(item2);
				}
			}
			else if (rec.TimesAmbushed == 0 && rec.Origin == GrudgeOrigin.Insulted)
			{
				list.Add("Deberías haber mantenido la boca cerrada en " + rec.KillLocation + ".");
				list.Add("¿Aún tienes algo que decir? Bien que hablaste en " + rec.KillLocation + ".");
			}
			else if (rec.TimesAmbushed == 0 && rec.Origin == GrudgeOrigin.Greeted)
			{
				list.Add("Te tocaste el sombrero ante mí en " + rec.KillLocation + ". Los buenos modales no te salvarán dos veces.");
			}
			else if (rec.TimesAmbushed == 0 && rec.Origin == GrudgeOrigin.GangMember)
			{
				Gang gang = Gangs.ById(rec.Gang);
				string text = (leaderSaidBy != null) ? leaderSaidBy(gang, rec) : (((gang != null) ? gang.LeaderName : null) ?? "The boss");
				if (text != null)
				{
					list.Add(text + " te envía recuerdos.");
				}
				list.Add("Eres uno de los de Dutch, ¿verdad? Ya decía yo.");
				list.Add("The " + (((gang != null) ? gang.Name : null) ?? "boys") + " nunca olvida a un Van der Linde.");
			}
			else if (rec.TimesAmbushed == 0 && rec.Origin == GrudgeOrigin.Wounded)
			{
				list.Add("Me disparaste en " + rec.KillLocation + " y no terminaste el trabajo. Yo sí lo haré.");
				list.Add("Aún llevo tu bala dentro. He venido a devolvértela.");
			}
			else if (rec.TimesAmbushed == 0 && rec.Origin == GrudgeOrigin.Slashed)
			{
				list.Add("Me abriste en canal en " + rec.KillLocation + ". Los puntos aguantaron. Veamos si los tuyos aguantan.");
				list.Add("¿Aún llevas ese cuchillo? Yo traigo algo que llega más lejos.");
			}
			else if (rec.TimesAmbushed == 0 && rec.Origin == GrudgeOrigin.HorseKilled)
			{
				list.Add("Mataste a mi caballo en " + rec.KillLocation + ". El mejor amigo que tuve jamás.");
				list.Add("Crié a ese caballo desde potro. Vas a pagar por cada año.");
			}
			else if (rec.TimesAmbushed == 0 && rec.Origin == GrudgeOrigin.Kidnapped)
			{
				list.Add("Me ataste como a un ternero en " + rec.KillLocation + ". Las quemaduras de la soga aún no han curado.");
				list.Add("Tardé horas en roer aquella soga. Tiempo de sobra para pensar en ti.");
			}
			else if (rec.TimesAmbushed == 0 && rec.Origin == GrudgeOrigin.Robbed)
			{
				list.Add("Me desvalijaste en " + rec.KillLocation + ". Vengo a cobrarme con intereses.");
				list.Add("Me quitaste todo lo que tenía. Ahora te quitaré todo lo que tienes.");
			}
			else if (rec.TimesAmbushed == 0 && rec.Origin == GrudgeOrigin.KnockedOut)
			{
				list.Add("Me desperté boca abajo en el barro en " + rec.KillLocation + ". Te toca dormir a ti.");
				list.Add("La última vez me pusiste a dormir. Esta vez te devuelvo el favor para siempre.");
			}
			else if (rec.TimesAmbushed == 0 && rec.Origin == GrudgeOrigin.Beaten)
			{
				list.Add("Me diste una paliza como a un perro en " + rec.KillLocation + ". He estado practicando.");
				list.Add("¿Aún tienes esos puños? Yo he traído algo mejor.");
			}
			else if (rec.TimesAntagonized > 0)
			{
				list.Add("Tanta palabrería en " + rec.KillLocation + "... vamos a terminarlo.");
				list.Add("¿Sigues teniendo esa lengua tan suelta? Vamos a oírla.");
			}
			else if (rec.TimesKilledPlayer > 1)
			{
				list.Add(string.Format("Ya te he hecho morder el polvo {0} veces, chico.", rec.TimesKilledPlayer));
			}
			else if (rec.TimesKilledPlayer == 0)
			{
				list.Add("Tú y yo aún no hemos terminado.");
				list.Add("¿Pensabas que olvidaría lo de " + rec.KillLocation + "? Yo no olvido nada.");
			}
			else
			{
				list.Add("Remember " + rec.KillLocation + "? Yo no fallo dos veces.");
			}
			if (!returningFromDead && rec.HateTier == HateTier.Resentful)
			{
				list.Add("No voy a malgastar una bala contigo. Con esto bastará.");
			}
			else if (!returningFromDead && rec.HateTier == HateTier.Vengeful)
			{
				list.Add("He traído algo afilado. Pensaba en ti mientras lo afilaba.");
			}
			if (rec.Rank >= 5)
			{
				list.Add("Cuentan historias sobre mí desde aquí hasta Blackwater. Y todas acaban contigo muerto.");
			}
			return list[rng.Next(list.Count)];
		}

		// Token: 0x060012A3 RID: 4771 RVA: 0x00126CED File Offset: 0x00124EED
		public static string EscalateReply(Random rng)
		{
			return Taunts.EscalateReplies[rng.Next(Taunts.EscalateReplies.Length)];
		}

		// Token: 0x060012A4 RID: 4772 RVA: 0x00126D02 File Offset: 0x00124F02
		public static string AntagonizeReply(Random rng)
		{
			return Taunts.AntagonizeReplies[rng.Next(Taunts.AntagonizeReplies.Length)];
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00126D17 File Offset: 0x00124F17
		public static string CampAlarm(NemesisRecord boss, Random rng)
		{
			if (boss.TimesAmbushed <= 0 && boss.TimesKilledPlayer <= 0)
			{
				return Taunts.CampAlarms[rng.Next(Taunts.CampAlarms.Length)];
			}
			return "¿Tú otra vez? ¿Aquí? ¡Tienes ganas de morir!";
		}

		// Token: 0x060012A6 RID: 4774 RVA: 0x00126D44 File Offset: 0x00124F44
		public static string GreetReply(Random rng)
		{
			return Taunts.GreetReplies[rng.Next(Taunts.GreetReplies.Length)];
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00126D59 File Offset: 0x00124F59
		public static string Paralyzed(NemesisRecord rec, Random rng)
		{
			return Taunts.ParalyzedLines[rng.Next(Taunts.ParalyzedLines.Length)];
		}

		// Token: 0x04000C48 RID: 3144
		private static readonly Dictionary<string, string> ScarBoasts = new Dictionary<string, string>
		{
			{
				"PD_Right_cut_cheek",
				"¿Ves esta mejilla? Cada mañana al mirarme al espejo me acuerdo de ti."
			},
			{
				"PD_ODR6_Duster_Slash_Belly",
				"Me cosí mis propias tripas. Tuve mucho tiempo para pensar en ti."
			},
			{
				"PD_Right_bruise_eye_A",
				"Este ojo nunca llegó a curar bien. Y mi mal genio tampoco."
			},
			{
				"PD_Nose_cut",
				"Me rompiste la nariz. Yo te voy a romper todo lo demás."
			},
			{
				"PD_Animal_attack_left_face1_Enemy",
				"Salí arrastrándome de aquella guarida con media cara. Valió la pena por volver a verte."
			},
			{
				"PD_Victim_Dead_Arrow_Wounds1",
				"Me arranqué tus flechas yo mismo. Guardé una para ti."
			},
			{
				"PD_ODR3_Arthur_Wound_Shoulder_L_Cauterized",
				"Me quemaste. No me quemo dos veces."
			},
			{
				"PD_Ped_Throat_slash",
				"Nadie había cortado nunca un cuello sin rematar la faena. Hasta que llegaste tú."
			},
			{
				"PD_ODR3_Arthur_Wound_Shoulder_L",
				"Me saqué tu bala con un cuchillo Bowie. Toma, te la devuelvo."
			}
		};

		// Token: 0x04000C49 RID: 3145
		private static readonly Dictionary<string, string> WoundBoasts = new Dictionary<string, string>
		{
			{
				"lung",
				"¡Me metiste una bala en el pulmón y sobreviví!"
			},
			{
				"liver",
				"Tu bala me atravesó limpiamente el vientre. No bastó."
			},
			{
				"stomach",
				"Me tragué tu plomo y lo digerí. Y sigo con hambre."
			},
			{
				"intestines",
				"Pasé un mes sujetándome las tripas. Pensando en ti todo el tiempo."
			},
			{
				"kidney",
				"Estuve meando sangre un mes por tu culpa."
			},
			{
				"ribs",
				"Me rompiste las costillas. Yo sigo respirando; tú dejarás de hacerlo."
			},
			{
				"sternum",
				"Me partiste el pecho y aquí sigo en pie."
			},
			{
				"scapula",
				"Me disparaste por la espalda. Bala de cobarde. Y ni siquiera me mataste."
			},
			{
				"femur",
				"He venido caminando sobre la pierna que me rompiste."
			},
			{
				"knee",
				"Tengo la rodilla llena de plomo. Y aun así caminé hasta aquí."
			},
			{
				"tibia",
				"He cojeado cada milla del camino hasta ti. Ha valido la pena."
			},
			{
				"femoral",
				"Casi me desangro por la pierna. Casi."
			},
			{
				"humerus",
				"Me hiciste pedazos el brazo. Pero aún funciona bien para esto."
			},
			{
				"forearm",
				"Me hiciste pedazos el brazo. Pero aún funciona bien para esto."
			},
			{
				"hand",
				"Me arrancaste dos dedos. Solo necesito uno para apretar el gatillo."
			},
			{
				"carotid",
				"Una bala en la garganta. Y aún me queda voz para decirte que eres hombre muerto."
			},
			{
				"windpipe",
				"Una bala en la garganta. Y aún me queda voz para decirte que eres hombre muerto."
			},
			{
				"jaw",
				"Llevo la mandíbula con alambres, pero aún puedo decirlo: estás muerto."
			},
			{
				"skull",
				"Tu bala rebotó en mi cráneo. El problema es tu puntería, no yo."
			},
			{
				"pelvis",
				"Meangana la cadera a cada paso. Y a cada paso pienso en ti."
			},
			{
				"spine",
				"El médico dijo que jamás volvería a andar. Se equivocaba."
			}
		};

		// Token: 0x04000C4A RID: 3146
		private static readonly string[] AntagonizeReplies = new string[]
		{
			"Sigue hablando, amigo. Ya te enterré una vez.",
			"Esa bocaza va a hacer que te maten. Otra vez.",
			"Repite eso una vez más."
		};

		// Token: 0x04000C4B RID: 3147
		private static readonly string[] GreetReplies = new string[]
		{
			"Qué educado. No te salvará.",
			"Buenas las tengas. Ahora desenfunda.",
			"No me vengas con 'buenas noches'."
		};

		// Token: 0x04000C4C RID: 3148
		private static readonly string[] EscalateReplies = new string[]
		{
			"¿Traes un arma a una pelea de puños? Muy bien.",
			"Conque esas tenemos. Como tú quieras.",
			"Jugada de cobarde. Ahora haremos esto como es debido."
		};

		// Token: 0x04000C4D RID: 3149
		private static readonly string[] CampAlarms = new string[]
		{
			"¡Tenemos compañía! ¡A por él!",
			"¡Es Morgan! ¡Matadlo!",
			"¡Intruso en el campamento! ¡Cosidlo a tiros!",
			"Te has metido en el campamento equivocado, amigo.",
			"¡Muchachos, coged los hierros!"
		};

		// Token: 0x04000C4E RID: 3150
		public const string EnragedReply = "That's it. No more talkin'.";

		// Token: 0x04000C4F RID: 3151
		private static readonly string[] ParalyzedLines = new string[]
		{
			"Mírame. Mira lo que me hiciste. Vamos, mira.",
			"No siento las piernas. Pero sigo sintiendo el odio.",
			"El médico dice que no volveré a caminar. Como si fuera una novedad.",
			"Termínalo o quítate de mi vista.",
			"¿Has venido a admirar tu obra?"
		};
	}
}
