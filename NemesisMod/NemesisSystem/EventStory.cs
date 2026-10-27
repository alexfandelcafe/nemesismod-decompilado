using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x02000011 RID: 17
	internal static class EventStory
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000C0B5 File Offset: 0x0000A2B5
		private static T Pick<T>(Random r, params T[] xs)
		{
			return xs[r.Next(xs.Length)];
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000C0C8 File Offset: 0x0000A2C8
		public static List<string> Build(WorldEvent ev, Func<string, NemesisRecord> find, long now, bool debug)
		{
			EventStory.<>c__DisplayClass1_0 CS$<>8__locals1;
			CS$<>8__locals1.find = find;
			string str = "story/";
			string str2;
			if ((str2 = ev.Id) == null)
			{
				str2 = (ev.Title ?? "");
			}
			CS$<>8__locals1.r = new Random(Seeds.Stable(str + str2 + "/" + ev.GameMin.ToString()));
			List<string> list = new List<string>();
			int h = EventStory.HourOf(ev.When);
			string text = EventStory.TimeOfDay(CS$<>8__locals1.r, h);
			string text2 = EventStory.PlacePhrase(ev.Place);
			IEnumerable<string> gangs = ev.Gangs;
			Func<string, string> selector;
			if ((selector = EventStory.<>O.<0>__SideName) == null)
			{
				selector = (EventStory.<>O.<0>__SideName = new Func<string, string>(EventStory.SideName));
			}
			List<string> list2 = (from x in gangs.Select(selector)
			where x != null
			select x).Distinct<string>().ToList<string>();
			List<WorldPart> list3 = (from p in ev.People
			where p.Outcome == "KILLED"
			select p).ToList<WorldPart>();
			List<WorldPart> list4 = (from p in ev.People
			where p.Outcome == "HERIDO"
			select p).ToList<WorldPart>();
			List<WorldPart> source = (from p in ev.People
			where p.Outcome == "DISTINGUIDO"
			select p).ToList<WorldPart>();
			List<WorldPart> source2 = (from p in ev.People
			where p.Outcome == "FLED" || p.Outcome == "RAN"
			select p).ToList<WorldPart>();
			List<WorldPart> list5 = (from p in ev.People
			where p.Outcome == "DE DUELO"
			select p).ToList<WorldPart>();
			List<WorldPart> list6 = (from p in ev.People
			where p.Outcome == "ILESO" || p.Outcome == "GANÓ"
			select p).ToList<WorldPart>();
			int num = ev.UnnamedDead.Values.Sum();
			string kind = ev.Kind;
			if (kind != null)
			{
				switch (kind.Length)
				{
				case 3:
				{
					char c = kind[0];
					if (c != 'J')
					{
						switch (c)
						{
						case 'V':
							if (!(kind == "VDL"))
							{
								goto IL_EA6;
							}
							if (Gangs.Era >= 1907)
							{
								list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
								{
									"Ocho años después, la banda de Van der Linde es una historia que se cuenta en las tabernas.",
									"La mayoría están muertos, y el resto tienen nombres nuevos.",
									"Parte es verdad. La mayoría se exageró al pasar de boca en boca."
								}));
								goto IL_F0E;
							}
							list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
							{
								"Las historias sobre la banda de Dutch están por todas partes esta primavera.",
								"La gente habla de los chicos de Van der Linde como si fueran una historia de fantasmas.",
								"Parte es cierta. Otra parte es la propia leyenda de Dutch devuelta a sus oídos."
							}));
							goto IL_F0E;
						case 'W':
							if (!(kind == "WAR"))
							{
								goto IL_EA6;
							}
							list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
							{
								EventStory.Cap(text) + ", " + text2 + ", la guerra dio otro giro.",
								string.Concat(new string[]
								{
									"Los combates ",
									text2,
									" continuaron ",
									text,
									", tal como llevan días ocurriendo."
								}),
								((list2.Count > 1) ? (EventStory.Cap(list2[0]) + " y " + list2[1]) : "Ambos bandos") + " siguieron enfrentándose " + text + ".",
								"Nadie en esa región duerme tranquilo mientras esto continúa.",
								"Ahora los granjeros atrancan sus puertas y mantienen a sus hijos dentro de casa."
							}));
							list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
							{
								"Se están cavando tumbas más rápido de lo que nadie puede marcarlas.",
								"Esos caminos no son seguros para nadie que vista los colores equivocados.",
								"Cada hombre metido en ello jura saber cómo va a terminar.",
								"Cada bando asegura que empezó el otro.",
								"En algún lugar, un líder está contando hombres que ya no tiene."
							}));
							goto IL_F0E;
						case 'X':
							goto IL_EA6;
						case 'Y':
							if (!(kind == "YOU"))
							{
								goto IL_EA6;
							}
							list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
							{
								string.Concat(new string[]
								{
									"Eso fue obra tuya, ",
									text,
									" ",
									text2,
									"."
								}),
								EventStory.Cap(text) + ", " + text2 + ": tú estuviste allí, y esto fue lo que dejó.",
								"Tú lo recordarás de una manera. Los que lo vieron lo recordarán de otra.",
								string.Concat(new string[]
								{
									"Ocurrió ",
									text,
									" ",
									text2,
									", y lleva tu firma."
								})
							}));
							goto IL_F0E;
						default:
							goto IL_EA6;
						}
					}
					else
					{
						if (!(kind == "JOB"))
						{
							goto IL_EA6;
						}
						list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
						{
							string.Concat(new string[]
							{
								(list2.Count > 0) ? EventStory.Cap(list2[0]) : "They",
								" llevaban días planeándolo. ",
								EventStory.Cap(text),
								", ",
								text2,
								", lo llevaron a cabo."
							}),
							string.Concat(new string[]
							{
								EventStory.Cap(text),
								", ",
								text2,
								", ",
								(list2.Count > 0) ? list2[0] : "la banda",
								" dieron el golpe del que habían estado hablando."
							}),
							string.Concat(new string[]
							{
								"El rumor corrió por los caminos durante una semana antes de que ocurriera: ",
								text,
								", ",
								text2,
								"."
							})
						}));
						goto IL_F0E;
					}
					break;
				}
				case 4:
				{
					char c = kind[0];
					if (c <= 'F')
					{
						if (c != 'D')
						{
							if (c != 'F')
							{
								goto IL_EA6;
							}
							if (!(kind == "FEUD"))
							{
								goto IL_EA6;
							}
							list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
							{
								string.Concat(new string[]
								{
									"Mala sangre dentro de ",
									(list2.Count > 0) ? list2[0] : "la banda",
									" llegó a las manos ",
									text,
									" ",
									text2,
									"."
								}),
								string.Concat(new string[]
								{
									"La tensión llevaba tiempo acumulándose. ",
									EventStory.Cap(text),
									", ",
									text2,
									", finalmente estalló."
								}),
								string.Concat(new string[]
								{
									"Los hombres que cabalgan juntos no siempre se soportan. ",
									EventStory.Cap(text),
									", ",
									text2,
									", eso quedó bien claro."
								}),
								string.Concat(new string[]
								{
									"Una partida de cartas, una palabra de más, una vieja deuda; el motivo cambia según quién lo cuente. Ocurrió ",
									text,
									" ",
									text2,
									"."
								})
							}));
							list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
							{
								"El resto de la banda se mantuvo al margen y dejó que lo resolvieran.",
								"Alguien intentó separarlos y se llevó un puñetazo por las molestias.",
								"Salieron a relucir los cuchillos antes de que nadie pudiera impedirlo.",
								"Su jefe no ha dicho ni media palabra al respecto desde entonces, lo cual ya dice bastante."
							}));
							goto IL_F0E;
						}
						else
						{
							if (!(kind == "DUEL"))
							{
								goto IL_EA6;
							}
							goto IL_7FE;
						}
					}
					else if (c != 'L')
					{
						if (c != 'R')
						{
							if (c != 'W')
							{
								goto IL_EA6;
							}
							if (!(kind == "WORD"))
							{
								goto IL_EA6;
							}
							list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
							{
								"En esta tierra los rumores viajan más rápido que un caballo.",
								"Alguien habló. Siempre hay alguien que habla.",
								EventStory.Cap(text) + ", en una taberna " + text2 + ", corrió por la sala una historia sobre ti.",
								"Empezó con un hombre que lo había visto y fue creciendo cada vez que se contaba."
							}));
							goto IL_F0E;
						}
						else if (!(kind == "RAID"))
						{
							goto IL_EA6;
						}
					}
					else
					{
						if (!(kind == "LIFE"))
						{
							goto IL_EA6;
						}
						WorldPart worldPart = ev.People.FirstOrDefault<WorldPart>();
						if (worldPart != null)
						{
							list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
							{
								EventStory.<Build>g__Who|1_7(worldPart, ref CS$<>8__locals1) + " - " + EventStory.LifeOpen(CS$<>8__locals1.r, text, text2),
								string.Concat(new string[]
								{
									EventStory.Cap(text),
									", ",
									text2,
									": noticias de ",
									worldPart.Name,
									"."
								}),
								"Esto es lo que le pasa a " + worldPart.Name + " cuando nadie le está disparando."
							}));
							goto IL_F0E;
						}
						goto IL_F0E;
					}
					break;
				}
				case 5:
				{
					char c = kind[0];
					if (c != 'B')
					{
						if (c != 'C')
						{
							if (c != 'R')
							{
								goto IL_EA6;
							}
							if (!(kind == "RANKS"))
							{
								goto IL_EA6;
							}
							list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
							{
								((list2.Count > 0) ? EventStory.Cap(list2[0]) : "La banda") + " reorganizó a sus hombres " + text + ".",
								"Cada banda tiene su jerarquía. Y ha cambiado.",
								"Alguien ascendió. A otro le dijeron que esperara su turno.",
								"Los hombres que fueron ignorados antes vuelven a contar los días."
							}));
							goto IL_F0E;
						}
						else if (!(kind == "CLASH"))
						{
							goto IL_EA6;
						}
					}
					else
					{
						if (!(kind == "BRAWL"))
						{
							goto IL_EA6;
						}
						goto IL_7FE;
					}
					break;
				}
				case 6:
				case 8:
				case 10:
				case 11:
					goto IL_EA6;
				case 7:
				{
					char c = kind[0];
					if (c != 'R')
					{
						if (c != 'T')
						{
							goto IL_EA6;
						}
						if (!(kind == "TURMOIL"))
						{
							goto IL_EA6;
						}
						list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
						{
							string.Concat(new string[]
							{
								"Corrió la voz por ",
								text,
								" de que algo había cambiado ",
								text2,
								"."
							}),
							string.Concat(new string[]
							{
								EventStory.Cap(text),
								", ",
								text2,
								", el terreno se movió bajo los pies de ",
								(list2.Count > 0) ? list2[0] : "las bandas",
								"."
							}),
							"Nadie lo planeó, pero todos tuvieron que adaptarse a ello.",
							"Es la clase de noticia de la que los hombres dejan de hablar cuando entra un forastero.",
							"Las lealtades en esta tierra duran más o menos lo que dura una botella."
						}));
						list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
						{
							"Habrá hombres que saquen tajada de ello y otros que no vivirán para ver cómo acaba.",
							"Se cobraron viejas deudas y se contrajeron otras nuevas.",
							"Algunos lo vieron venir. La mayoría no.",
							"A la mañana siguiente, cada campamento del territorio había oído una versión de lo sucedido."
						}));
						goto IL_F0E;
					}
					else
					{
						if (!(kind == "RIVALRY"))
						{
							goto IL_EA6;
						}
						goto IL_7FE;
					}
					break;
				}
				case 9:
					if (!(kind == "WITNESSED"))
					{
						goto IL_EA6;
					}
					list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
					{
						string.Concat(new string[]
						{
							"Lo viste con tus propios ojos, ",
							text,
							" ",
							text2,
							"."
						}),
						EventStory.Cap(text) + ", " + text2 + ", lo viste ocurrir.",
						"Esta vez no fue por tu culpa; solo fuiste testigo."
					}));
					goto IL_F0E;
				case 12:
					if (!(kind == "CAMP OVERRUN"))
					{
						goto IL_EA6;
					}
					break;
				default:
					goto IL_EA6;
				}
				string text3 = (list2.Count > 0) ? list2[0] : "dos bandas";
				string text4 = (list2.Count > 1) ? list2[1] : "sus rivales";
				list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
				{
					string.Concat(new string[]
					{
						EventStory.Cap(text),
						", ",
						text3,
						" y ",
						text4,
						" se toparon ",
						text2,
						"."
					}),
					string.Concat(new string[]
					{
						"Empezó ",
						text,
						" ",
						text2,
						", cuando ",
						text3,
						" se topó con ",
						text4,
						"."
					}),
					string.Concat(new string[]
					{
						EventStory.Cap(text2),
						", ",
						text,
						": ",
						text3,
						" against ",
						text4,
						"."
					}),
					string.Concat(new string[]
					{
						"Nadie se pone de acuerdo en quién disparó primero. ",
						EventStory.Cap(text),
						", ",
						text2,
						", ",
						text3,
						" y ",
						text4,
						" se estaban disparando entre sí."
					}),
					string.Concat(new string[]
					{
						EventStory.Cap(text3),
						" llevaba buscando a ",
						text4,
						" durante semanas. ",
						EventStory.Cap(text),
						", ",
						text2,
						", dieron con ellos."
					})
				}));
				list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
				{
					"Lo que siguió fue breve y brutal: hombres parapetados tras carretas, rocas o unos detrás de otros.",
					"El tiroteo se alargó más de lo que cualquiera de los presentes admitiría.",
					"Los caballos se desbocaron con los primeros disparos; casi todos los hombres tuvieron que pelear a pie.",
					"El humo de la pólvora quedó flotando a ras de suelo mucho después del último disparo.",
					"Terminó en cuestión de minutos. El silencio posterior duró mucho más."
				}));
				if (ev.Kind == "CAMP OVERRUN")
				{
					list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
					{
						"Al final el campamento había cambiado de manos: tiendas rajadas, la hoguera apagada a patadas y todo lo transportable saqueado.",
						"El campamento no resistió. Quienes quedaron en pie se llevaron lo que pudieron y dejaron arder el resto.",
						"Los defensores se quebraron y se dispersaron entre los árboles; el campamento ya no era suyo."
					}));
					goto IL_F0E;
				}
				goto IL_F0E;
				IL_7FE:
				list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
				{
					EventStory.Cap(text) + ", " + text2 + ", dos hombres decidieron que ya estaban hartos el uno del otro.",
					string.Concat(new string[]
					{
						"Empezó por una tontería, ",
						text,
						" ",
						text2,
						", pero la cosa fue a mayores."
					}),
					string.Concat(new string[]
					{
						"Hacía tiempo que tenían roces entre ellos. ",
						EventStory.Cap(text),
						", ",
						text2,
						", se acabaron las palabras."
					}),
					string.Concat(new string[]
					{
						"Media calle salió a mirar, ",
						text,
						" ",
						text2,
						"."
					})
				}));
				list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
				{
					"La gente aún discute sobre quién lanzó el primer puñetazo.",
					"El sheriff apareció tarde, como hace siempre.",
					"El tabernero barrió los cristales rotos y dijo que ya era la tercera vez ese mes.",
					"Para cuando a alguien se le ocurrió pararlo, ya había terminado."
				}));
				goto IL_F0E;
			}
			IL_EA6:
			list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
			{
				string.Concat(new string[]
				{
					"Ocurrió ",
					text,
					" ",
					text2,
					"."
				}),
				EventStory.Cap(text) + ", " + text2 + ".",
				"No todo el mundo lo recuerda de la misma manera."
			}));
			IL_F0E:
			foreach (WorldPart worldPart2 in list3.Take(3))
			{
				NemesisRecord nemesisRecord = CS$<>8__locals1.find(worldPart2.Id);
				bool flag = debug && worldPart2.SecretlyLived;
				List<string> list7 = list;
				Random r = CS$<>8__locals1.r;
				string[] array = new string[4];
				array[0] = EventStory.<Build>g__Who|1_7(worldPart2, ref CS$<>8__locals1) + " cayó y no volvió a levantarse.";
				array[1] = EventStory.<Build>g__Who|1_7(worldPart2, ref CS$<>8__locals1) + " murió " + EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
				{
					"allí mismo donde estaba",
					"intentando llegar hasta su caballo",
					"en el primer intercambio de disparos",
					"cerca del final",
					"cubriendo a los demás"
				}) + ".";
				array[2] = string.Concat(new string[]
				{
					"Enterraron a ",
					worldPart2.Name,
					" ",
					EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
					{
						"a la mañana siguiente",
						"donde cayó",
						"en una tumba sin nombre",
						"en una fosa poco profunda",
						"con las botas puestas"
					}),
					"."
				});
				int num2 = 3;
				string name = worldPart2.Name;
				string str3 = " murió allí";
				string str4;
				if (nemesisRecord == null || nemesisRecord.Personality.GetValueOrDefault() != Personality.Proud)
				{
					if (nemesisRecord != null)
					{
						Personality? personality = nemesisRecord.Personality;
						Personality personality2 = Personality.Hothead;
						if (personality.GetValueOrDefault() == personality2 & personality != null)
						{
							str4 = ", cargando de frente cuando debería haberse agachado";
							goto IL_10AC;
						}
					}
					str4 = ((nemesisRecord != null && nemesisRecord.Personality.GetValueOrDefault() == Personality.Coward) ? ", abatido por la espalda mientras huía" : "");
				}
				else
				{
					str4 = ", demasiado orgulloso para huir";
				}
				IL_10AC:
				array[num2] = name + str3 + str4 + ".";
				list7.Add(EventStory.Pick<string>(r, array) + (flag ? " (Debug: he lived.)" : ""));
			}
			if (list3.Count > 3)
			{
				list.Add(string.Format("Otros {0} hombres con nombre cayeron junto a ellos.", list3.Count - 3));
			}
			foreach (WorldPart worldPart3 in list4.Take(2))
			{
				list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
				{
					EventStory.<Build>g__Who|1_7(worldPart3, ref CS$<>8__locals1) + " recibió una herida, pero salió andando.",
					worldPart3.Name + " fue alcanzado y le cosieron la herida después; llevará esa marca consigo.",
					worldPart3.Name + " salió de allí sangrando, pero vivo.",
					"Un médico le sacó el plomo del cuerpo a " + worldPart3.Name + " aquella noche."
				}));
			}
			foreach (WorldPart worldPart4 in source.Take(2))
			{
				list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
				{
					EventStory.<Build>g__Who|1_7(worldPart4, ref CS$<>8__locals1) + " se hizo un nombre aquel día.",
					"Aún se cuenta cómo " + worldPart4.Name + " mantuvo su posición.",
					worldPart4.Name + " fue de quien hablarán: no se inmutó ni una vez.",
					"Si alguien salió de aquello mejor parado de lo que entró, fue " + worldPart4.Name + "."
				}));
			}
			foreach (WorldPart worldPart5 in source2.Take(2))
			{
				list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
				{
					worldPart5.Name + " huyó. Nadie que lo viera permitirá que lo olvide.",
					worldPart5.Name + " ya se había largado antes de que se disipara el humo.",
					worldPart5.Name + " puso pies en polvorosa a la primera oportunidad."
				}));
			}
			if (list6.Count > 0 && list3.Count + list4.Count > 0 && CS$<>8__locals1.r.Next(2) == 0)
			{
				list.Add(string.Join(", ", from p in list6.Take(3)
				select p.Name) + " salió sin un rasguño" + ((list6.Count > 3) ? ", entre otros" : "") + ".");
			}
			if (num > 0)
			{
				list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
				{
					string.Format("Otros {0} hombres de los que nadie se molestó en saber el nombre murieron con ellos.", num),
					string.Format("Además de esos, {0} más yacían muertos al terminar.", num),
					string.Format("El enterrador contó {0} más. Nadie vino a reclamarlos.", num)
				}));
			}
			if (list5.Count > 0)
			{
				List<string> list8 = list;
				Random r2 = CS$<>8__locals1.r;
				string[] array2 = new string[3];
				array2[0] = string.Join(" y ", from p in list5.Take(2)
				select p.Name) + " encajó muy mal la noticia.";
				array2[1] = "Lejos de allí, " + list5[0].Name + " se enteró y guardó silencio durante un largo rato.";
				array2[2] = "La noticia llegó a oídos de " + list5[0].Name + " un día después. No ha vuelto a ser el mismo.";
				list8.Add(EventStory.Pick<string>(r2, array2));
			}
			WorldPart worldPart6 = ev.People.FirstOrDefault(delegate(WorldPart p)
			{
				if (p.Effects != null)
				{
					return p.Effects.Any((string e) => e.StartsWith("scar") || e.StartsWith("De duelo") || e.StartsWith("fear") || e.Contains("rank"));
				}
				return false;
			});
			if (worldPart6 != null)
			{
				string text5 = worldPart6.Effects.First((string e) => e.StartsWith("scar") || e.StartsWith("De duelo") || e.StartsWith("fear") || e.Contains("rank"));
				if (text5.StartsWith("scar"))
				{
					list.Add(worldPart6.Name + " carries " + text5.Substring(Math.Min(text5.Length, 6)).Trim().TrimStart(new char[]
					{
						':'
					}).Trim() + " desde entonces.");
				}
				else if (text5.StartsWith("De duelo"))
				{
					list.Add(worldPart6.Name + " está de luto.");
				}
				else if (text5.StartsWith("fear"))
				{
					list.Add("Aquello dejó a " + worldPart6.Name + " más nervioso de lo que solía ser.");
				}
				else
				{
					list.Add(worldPart6.Name + " vio cambiar su posición en la banda por ello.");
				}
			}
			if (ev.AftermathUntil > 0L)
			{
				list.Add(ev.Visited ? "Ya has pasado por ese lugar desde que ocurrió." : ((ev.AftermathUntil > now) ? EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
				{
					"Los cuerpos siguen ahí fuera para cualquiera que vaya a buscarlos.",
					"Nadie ha limpiado aún el lugar.",
					"Los buitres encontraron el sitio antes que la ley."
				}) : "Todo lo que quedó allí ya ha sido retirado."));
			}
			if (ev.Kind == "CLASH" || ev.Kind == "CAMP OVERRUN" || ev.Kind == "FEUD" || ev.Kind == "RAID")
			{
				list.Add(EventStory.Pick<string>(CS$<>8__locals1.r, new string[]
				{
					"Ninguno de los dos bandos lo olvidará.",
					"No será la última vez.",
					"Alguien querrá ajustar las cuentas.",
					"La próxima vez que estos hombres se crucen, será peor.",
					"En esta tierra, una pelea así nunca termina del todo."
				}));
			}
			return list;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000D7AC File Offset: 0x0000B9AC
		private static string LifeOpen(Random r, string tod, string place)
		{
			return EventStory.Pick<string>(r, new string[]
			{
				tod + " " + place + ", esto es lo que andaba haciendo.",
				"la vida siguió su curso para él, como siempre hace.",
				"llegaron noticias suyas desde " + place.Replace("cerca de ", "").Replace("at ", "") + ".",
				"hasta los forajidos tienen una vida que vivir cuando no disparan."
			});
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000D81C File Offset: 0x0000BA1C
		private static string Describe(Random r, WorldPart p, Func<string, NemesisRecord> find)
		{
			NemesisRecord nemesisRecord = find(p.Id);
			if (nemesisRecord == null)
			{
				return p.Name;
			}
			List<string> list = new List<string>();
			if (!string.IsNullOrEmpty(nemesisRecord.Trade) && r.Next(3) > 0)
			{
				list.Add("a " + nemesisRecord.Trade.ToLowerInvariant());
			}
			else if (nemesisRecord.Personality != null && r.Next(2) == 0)
			{
				list.Add(EventStory.TemperWord(nemesisRecord.Personality.Value));
			}
			if (!string.IsNullOrEmpty(nemesisRecord.Birthplace) && r.Next(2) == 0)
			{
				list.Add("natural de " + nemesisRecord.Birthplace);
			}
			if (list.Count != 0)
			{
				return p.Name + ", " + string.Join(" ", list) + ",";
			}
			return p.Name;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000D908 File Offset: 0x0000BB08
		private static string TemperWord(Personality p)
		{
			switch (p)
			{
			case Personality.Hothead:
				return "un impulsivo";
			case Personality.Coward:
				return "nunca el más valiente";
			case Personality.Proud:
				return "un hombre orgulloso";
			case Personality.Cold:
				return "un tipo frío";
			case Personality.Greedy:
				return "siempre contando su dinero";
			case Personality.Loyal:
				return "leal hasta la médula";
			default:
				return "un errante";
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000D960 File Offset: 0x0000BB60
		private static string SideName(string id)
		{
			if (id == null)
			{
				return null;
			}
			if (id == "")
			{
				return "algunos solitarios";
			}
			Gang gang = Gangs.ById(id);
			if (gang == null)
			{
				return null;
			}
			if (!gang.Name.StartsWith("The "))
			{
				return "the " + gang.Name;
			}
			return "t" + gang.Name.Substring(1);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000D9CC File Offset: 0x0000BBCC
		private static string PlacePhrase(string place)
		{
			if (string.IsNullOrEmpty(place))
			{
				return "en pleno campo";
			}
			string text = place.Trim();
			string text2 = text.ToLowerInvariant();
			if (text2.StartsWith("cerca de ") || text2.StartsWith("a las afueras de ") || text2.StartsWith("in ") || text2.StartsWith("at ") || text2.StartsWith("on ") || text2.StartsWith("by "))
			{
				return text;
			}
			return "cerca de " + text;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000DA50 File Offset: 0x0000BC50
		private static int HourOf(string when)
		{
			if (string.IsNullOrEmpty(when))
			{
				return -1;
			}
			int num = when.LastIndexOf(':');
			if (num < 2)
			{
				return -1;
			}
			int result;
			if (!int.TryParse(when.Substring(num - 2, 2).Trim(), out result))
			{
				return -1;
			}
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000DA94 File Offset: 0x0000BC94
		private static string TimeOfDay(Random r, int h)
		{
			if (h < 0)
			{
				return "un día";
			}
			if (h < 5)
			{
				return EventStory.Pick<string>(r, new string[]
				{
					"en plena noche",
					"poco antes del amanecer",
					"de madrugada"
				});
			}
			if (h < 8)
			{
				return EventStory.Pick<string>(r, new string[]
				{
					"con la primera luz del alba",
					"a primera hora de la mañana",
					"al salir el sol"
				});
			}
			if (h < 12)
			{
				return EventStory.Pick<string>(r, new string[]
				{
					"por la mañana",
					"mid-morning",
					"antes del mediodía"
				});
			}
			if (h < 14)
			{
				return EventStory.Pick<string>(r, new string[]
				{
					"hacia el mediodía",
					"en el calor del día",
					"a mediodía"
				});
			}
			if (h < 18)
			{
				return EventStory.Pick<string>(r, new string[]
				{
					"por la tarde",
					"a última hora de la tarde",
					"mientras el sol aún estaba alto"
				});
			}
			if (h < 21)
			{
				return EventStory.Pick<string>(r, new string[]
				{
					"al caer la tarde",
					"mientras se apagaba la luz del día",
					"al ponerse el sol"
				});
			}
			return EventStory.Pick<string>(r, new string[]
			{
				"tras anochecer",
				"a altas horas de la noche",
				"por la noche"
			});
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000DBCC File Offset: 0x0000BDCC
		private static string Cap(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return char.ToUpperInvariant(s[0]).ToString() + s.Substring(1);
			}
			return s;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000DC03 File Offset: 0x0000BE03
		[CompilerGenerated]
		internal static string <Build>g__Who|1_7(WorldPart p, ref EventStory.<>c__DisplayClass1_0 A_1)
		{
			return EventStory.Describe(A_1.r, p, A_1.find);
		}

		// Token: 0x020000C5 RID: 197
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000FB7 RID: 4023
			public static Func<string, string> <0>__SideName;
		}
	}
}
