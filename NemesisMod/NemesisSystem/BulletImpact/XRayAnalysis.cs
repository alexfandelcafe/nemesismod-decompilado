using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x020000A0 RID: 160
	public sealed class XRayAnalysis
	{
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x00147C40 File Offset: 0x00145E40
		public IEnumerable<DamageItem> AllDamage
		{
			get
			{
				return from d in (from p in new PathResult[]
				{
					this.Main
				}.Concat(this.Pellets)
				where p != null
				select p).SelectMany((PathResult p) => p.Damage)
				group d by d.Key into g
				select g.First<DamageItem>() into d
				orderby d.Severity descending
				select d;
			}
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00147D1C File Offset: 0x00145F1C
		public bool HitKey(string key)
		{
			return (this.Main != null && this.Main.HitKeys.Contains(key)) || this.Pellets.Any((PathResult p) => p.HitKeys.Contains(key));
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00147D6F File Offset: 0x00145F6F
		private static float RoundScale(uint weaponHash)
		{
			if (weaponHash != 3724000286U)
			{
				return 1f;
			}
			return 0.33f;
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00147D84 File Offset: 0x00145F84
		public static XRayAnalysis Build(XRayEntry e)
		{
			Dictionary<int, V3> bones = (from b in e.Bones
			group b by b.Id).ToDictionary((IGrouping<int, BonePoint> g) => g.Key, (IGrouping<int, BonePoint> g) => g.First<BonePoint>().P);
			XRayAnalysis xrayAnalysis = new XRayAnalysis
			{
				Body = (e.Animal ? AnimalAnatomy.Build(bones) : Anatomy.Build(bones))
			};
			Random random = new Random(e.Id * 7919);
			if (e.WeaponClass == WeaponClass.Blunt)
			{
				XRayAnalysis.BuildBeating(e, xrayAnalysis);
				return xrayAnalysis;
			}
			float num = (e.WeaponClass == WeaponClass.Shotgun && e.Ammo != AmmoKind.Slug) ? Math.Max(0f, Math.Min(1f, (12f - e.Distance) / 10f)) : 0f;
			float num2 = 1f + 1.5f * num;
			if (e.ImpactOnVictim)
			{
				xrayAnalysis.Main = Anatomy.Trace(xrayAnalysis.Body, e.Entry - e.Dir * 0.6f, e.Dir, e.WeaponClass, 3f, e.Ammo, XRayAnalysis.RoundScale(e.WeaponHash) * num2);
			}
			if (xrayAnalysis.Main == null)
			{
				V3 v;
				V3 a = xrayAnalysis.Body.Bones.TryGetValue(e.RecordedBoneId, out v) ? v : xrayAnalysis.Body.TorsoPoint(0.6f, 0f, 0f);
				V3 v2 = (a - e.Muzzle).Normalized();
				xrayAnalysis.Main = Anatomy.Trace(xrayAnalysis.Body, a - v2 * 1f, v2, e.WeaponClass, 3f, e.Ammo, XRayAnalysis.RoundScale(e.WeaponHash) * num2);
				if (xrayAnalysis.Main != null)
				{
					xrayAnalysis.Main.Estimated = !e.ImpactOnVictim;
				}
			}
			if (Anatomy.IsArea(e.WeaponClass) && xrayAnalysis.Main != null)
			{
				bool flag = e.WeaponClass == WeaponClass.Burn;
				V3 entry = xrayAnalysis.Main.Entry;
				V3 v3 = (entry - e.Muzzle).Normalized();
				V3 a2 = V3.Cross(v3, new V3(0f, 0f, 1f)).Normalized();
				if (a2.Length() < 0.5f)
				{
					a2 = new V3(1f, 0f, 0f);
				}
				V3 a3 = V3.Cross(a2, v3).Normalized();
				float num3 = flag ? 0.45f : 0.35f;
				for (int i = 1; i < Math.Max(2, e.Pellets); i++)
				{
					for (int j = 0; j < 15; j++)
					{
						double num4 = random.NextDouble() * 3.141592653589793 * 2.0;
						double num5 = Math.Sqrt(random.NextDouble()) * (double)num3;
						V3 a4 = entry + a2 * (float)(Math.Cos(num4) * num5) + a3 * (float)(Math.Sin(num4) * num5 * 1.600000023841858);
						V3 v4 = (a4 - e.Muzzle).Normalized();
						PathResult pathResult = Anatomy.Trace(xrayAnalysis.Body, a4 - v4 * 1f, v4, e.WeaponClass, 3f, AmmoKind.Regular, 1f);
						if (pathResult != null)
						{
							pathResult.Estimated = true;
							xrayAnalysis.Pellets.Add(pathResult);
							break;
						}
					}
				}
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				foreach (PathResult pathResult2 in new PathResult[]
				{
					xrayAnalysis.Main
				}.Concat(xrayAnalysis.Pellets))
				{
					string key = Anatomy.RegionOf(xrayAnalysis.Body, pathResult2.Entry);
					int num6;
					dictionary[key] = (dictionary.TryGetValue(key, out num6) ? (num6 + 1) : 1);
				}
				foreach (KeyValuePair<string, int> keyValuePair in from x in dictionary
				orderby x.Value descending
				select x)
				{
					Severity severity = (keyValuePair.Value >= 5) ? Severity.Severe : ((keyValuePair.Value >= 2) ? Severity.Moderate : Severity.Minor);
					List<DamageItem> damage = xrayAnalysis.Main.Damage;
					DamageItem item;
					if (!flag)
					{
						DamageItem damageItem = new DamageItem();
						damageItem.Key = "shrapnel:" + keyValuePair.Key;
						damageItem.Name = string.Format("SHRAPNEL IN THE {0} ({1})", keyValuePair.Key.ToUpperInvariant(), keyValuePair.Value);
						item = damageItem;
						damageItem.Severity = severity;
					}
					else
					{
						DamageItem damageItem2 = new DamageItem();
						damageItem2.Key = "burn:" + keyValuePair.Key;
						damageItem2.Name = keyValuePair.Key.ToUpperInvariant() + " BURNED" + ((keyValuePair.Value >= 5) ? " - DEEP" : "");
						item = damageItem2;
						damageItem2.Severity = severity;
					}
					damage.Add(item);
				}
				if (!flag && e.Distance < 2.5f)
				{
					xrayAnalysis.Main.Damage.Add(new DamageItem
					{
						Key = "concussion",
						Name = "CONCUSSION - THE BLAST WAVE",
						Severity = Severity.Moderate
					});
				}
				xrayAnalysis.Main.Damage.Sort((DamageItem x, DamageItem y) => y.Severity.CompareTo(x.Severity));
			}
			if (e.WeaponClass == WeaponClass.Shotgun && xrayAnalysis.Main != null && e.Pellets > 1 && e.Ammo != AmmoKind.Slug)
			{
				V3 entry2 = xrayAnalysis.Main.Entry;
				V3 v5 = (entry2 - e.Muzzle).Normalized();
				V3 a5 = V3.Cross(v5, new V3(0f, 0f, 1f)).Normalized();
				if (a5.Length() < 0.5f)
				{
					a5 = new V3(1f, 0f, 0f);
				}
				V3 a6 = V3.Cross(a5, v5).Normalized();
				float num7 = Math.Max(0.06f, Math.Min(0.35f, e.Distance * 0.03f));
				for (int k = 1; k < e.Pellets; k++)
				{
					for (int l = 0; l < 15; l++)
					{
						double num8 = random.NextDouble() * 3.141592653589793 * 2.0;
						double num9 = Math.Sqrt(random.NextDouble()) * (double)num7;
						V3 a7 = entry2 + a5 * (float)(Math.Cos(num8) * num9) + a6 * (float)(Math.Sin(num8) * num9);
						V3 v6 = (a7 - e.Muzzle).Normalized();
						PathResult pathResult3 = Anatomy.Trace(xrayAnalysis.Body, a7 - v6 * 1f, v6, WeaponClass.Shotgun, 3f, e.Ammo, num2);
						if (pathResult3 != null)
						{
							pathResult3.Estimated = true;
							xrayAnalysis.Pellets.Add(pathResult3);
							break;
						}
					}
				}
				if (num > 0.3f && !e.Animal)
				{
					Dictionary<string, int> dictionary2 = new Dictionary<string, int>();
					foreach (PathResult pathResult4 in new PathResult[]
					{
						xrayAnalysis.Main
					}.Concat(xrayAnalysis.Pellets))
					{
						string key2 = XRayAnalysis.Bare(Anatomy.RegionOf(xrayAnalysis.Body, pathResult4.Entry));
						int num10;
						dictionary2[key2] = (dictionary2.TryGetValue(key2, out num10) ? (num10 + 1) : 1);
					}
					foreach (KeyValuePair<string, int> keyValuePair2 in from x in dictionary2
					where x.Value >= 4
					orderby x.Value descending
					select x)
					{
						bool flag2 = keyValuePair2.Key.Contains("cabeza") || keyValuePair2.Key.Contains("cara") || keyValuePair2.Key.Contains("neck");
						bool flag3 = keyValuePair2.Key.Contains("arm") || keyValuePair2.Key.Contains("pierna") || keyValuePair2.Key.Contains("muslo") || keyValuePair2.Key.Contains("hand") || keyValuePair2.Key.Contains("foot") || keyValuePair2.Key.Contains("hombro");
						xrayAnalysis.Main.Damage.Add(new DamageItem
						{
							Key = "buckshot:" + keyValuePair2.Key,
							Name = (flag2 ? (keyValuePair2.Key.ToUpperInvariant() + " REVENTADO - PERDIGONES") : (flag3 ? (keyValuePair2.Key.ToUpperInvariant() + " DESTROZADO - PERDIGONES") : (keyValuePair2.Key.ToUpperInvariant() + " DESGARRADO - PERDIGONES"))),
							Severity = (flag2 ? Severity.Fatal : Severity.Critical)
						});
					}
					xrayAnalysis.Main.Damage.Sort((DamageItem x, DamageItem y) => y.Severity.CompareTo(x.Severity));
				}
			}
			if (xrayAnalysis.Main != null)
			{
				XRayAnalysis.ApplyAmmo(e, xrayAnalysis, random);
			}
			if (e.Animal && xrayAnalysis.Main != null)
			{
				AnimalAnatomy.AddVerdicts(xrayAnalysis.Main, xrayAnalysis.Pellets);
			}
			return xrayAnalysis;
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x00148864 File Offset: 0x00146A64
		private static void BuildBeating(XRayEntry e, XRayAnalysis a)
		{
			List<Blow> list;
			if (e.Blows == null || e.Blows.Count <= 0)
			{
				(list = new List<Blow>()).Add(new Blow
				{
					Bone = e.RecordedBoneId,
					Dir = e.Dir
				});
			}
			else
			{
				list = e.Blows;
			}
			List<Blow> list2 = list;
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			XRayAnalysis.<>c__DisplayClass9_0 CS$<>8__locals1;
			CS$<>8__locals1.weight = new Dictionary<string, int>();
			int num = 0;
			int num2 = 0;
			PathResult pathResult = null;
			foreach (Blow blow in list2)
			{
				V3 v = (blow.Dir.Length() > 0.1f) ? blow.Dir.Normalized() : new V3(0f, -1f, 0f);
				string text = XRayAnalysis.BlowRegion(blow.Bone, v);
				V3 v2;
				V3 a2 = a.Body.Bones.TryGetValue(blow.Bone, out v2) ? v2 : a.Body.TorsoPoint(0.6f, 0f, 0f);
				float s = (text == "cara") ? 0.1f : ((text == "garganta") ? 0.06f : ((text == "chest" || text == "stomach" || text == "back") ? 0.13f : 0.05f));
				V3 v3 = a2 - v * s;
				if (text == "chest")
				{
					if (v3.X < 0f)
					{
						num++;
					}
					else
					{
						num2++;
					}
				}
				int num3;
				dictionary[text] = (dictionary.TryGetValue(text, out num3) ? (num3 + 1) : 1);
				int num4;
				CS$<>8__locals1.weight[text] = (CS$<>8__locals1.weight.TryGetValue(text, out num4) ? num4 : 0) + blow.Weight;
				PathResult pathResult2 = new PathResult();
				pathResult2.Blunt = true;
				pathResult2.Blow = blow.Kind;
				pathResult2.HasEntry = true;
				pathResult2.Start = v3 - v * 0.3f;
				pathResult2.Entry = v3;
				pathResult2.End = v3;
				if (pathResult != null)
				{
					a.Pellets.Add(pathResult);
				}
				pathResult = pathResult2;
			}
			a.Main = pathResult;
			foreach (KeyValuePair<string, int> keyValuePair in from x in dictionary
			orderby x.Value descending
			select x)
			{
				a.Blows.Add(new ValueTuple<string, int>(keyValuePair.Key, keyValuePair.Value));
			}
			int count = list2.Count;
			int n = list2.Count((Blow x) => x.Kind == BlowKind.Punch);
			int num5 = list2.Count((Blow x) => x.Kind == BlowKind.Kick);
			int n2 = list2.Count((Blow x) => x.Kind == BlowKind.Gun);
			int num6 = list2.Count((Blow x) => x.Kind == BlowKind.Choke);
			int num7 = (from x in list2
			where x.Choke
			select x).Sum((Blow x) => x.HeldMs);
			int num8 = list2.Count((Blow x) => x.Stomp);
			string text2 = (num5 == 0) ? null : ((num8 == num5) ? XRayAnalysis.<BuildBeating>g__Of|9_7(num8, "pisotón", "pisotones") : ((num8 > 0) ? (XRayAnalysis.<BuildBeating>g__Of|9_7(num5, "patada", "patadas") + " (" + XRayAnalysis.<BuildBeating>g__Of|9_7(num8, "pisotón", "pisotones") + ")") : XRayAnalysis.<BuildBeating>g__Of|9_7(num5, "patada", "patadas")));
			List<string> list3 = (from x in new string[]
			{
				XRayAnalysis.<BuildBeating>g__Of|9_7(n, "punch", "puñetazos"),
				text2,
				XRayAnalysis.<BuildBeating>g__Of|9_7(n2, "culatazo", "culatazos"),
				XRayAnalysis.<BuildBeating>g__Of|9_7(num6, "asfixia", "asfixias")
			}
			where x != null
			select x).ToList<string>();
			a.Main.EntryText = ((count == 1) ? (list3[0] + " - " + a.Blows[0].Item1) : string.Join(", ", list3));
			CS$<>8__locals1.d = a.Main.Damage;
			int num9 = XRayAnalysis.<BuildBeating>g__C|9_10("cara", ref CS$<>8__locals1);
			bool flag = e.KnockedOut && !e.Killed;
			if (e.LeftForDead)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("left_for_dead", "PATEADO EN EL SUELO - DADO POR MUERTO", Severity.Fatal, false, ref CS$<>8__locals1);
			}
			if (num6 > 0)
			{
				if (e.FatalShot)
				{
					XRayAnalysis.<BuildBeating>g__Add|9_9("estrangulado", "ESTRANGULADO", Severity.Fatal, false, ref CS$<>8__locals1);
				}
				else if (flag)
				{
					XRayAnalysis.<BuildBeating>g__Add|9_9("choked_out", "ESTRANGULADO INCONSCIENTE", Severity.Severe, false, ref CS$<>8__locals1);
				}
				if (num7 >= 5000)
				{
					XRayAnalysis.<BuildBeating>g__Add|9_9("windpipe", "TRÁQUEA MAGULLADA - RONQUERA", Severity.Moderate, false, ref CS$<>8__locals1);
				}
				XRayAnalysis.<BuildBeating>g__Add|9_9("bruise:choke", "GARGANTA MAGULLADA - MARCAS DE DEDOS", Severity.Minor, false, ref CS$<>8__locals1);
			}
			else if (flag)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("ko", "DEJADO INCONSCIENTE", Severity.Severe, false, ref CS$<>8__locals1);
			}
			if (num9 >= 8 || (flag && num9 >= 2))
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("concussion", "CONMOCIÓN CEREBRAL", Severity.Severe, false, ref CS$<>8__locals1);
			}
			if (list2.Count((Blow b) => b.Stomp && XRayAnalysis.BlowRegion(b.Bone, b.Dir) == "cara") >= 2)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("skull", "CRÁNEO FRACTURADO", Severity.Critical, true, ref CS$<>8__locals1);
			}
			if (num9 >= 6)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("jaw", "MANDÍBULA FISURADA", Severity.Severe, true, ref CS$<>8__locals1);
			}
			if (num9 >= 4)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("nariz", "NARIZ ROTA", Severity.Moderate, true, ref CS$<>8__locals1);
			}
			if (num9 >= 3)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("ojo", "OJO MORADO", Severity.Moderate, false, ref CS$<>8__locals1);
			}
			if (num9 >= 1)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("bruise:face", "CARA MAGULLADA", Severity.Minor, false, ref CS$<>8__locals1);
			}
			if (XRayAnalysis.<BuildBeating>g__C|9_10("garganta", ref CS$<>8__locals1) >= 3)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("garganta", "GARGANTA HINCHADA - RONQUERA", Severity.Moderate, false, ref CS$<>8__locals1);
			}
			else if (XRayAnalysis.<BuildBeating>g__C|9_10("garganta", ref CS$<>8__locals1) >= 1)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("bruise:throat", "GARGANTA MAGULLADA", Severity.Minor, false, ref CS$<>8__locals1);
			}
			if (XRayAnalysis.<BuildBeating>g__C|9_10("chest", ref CS$<>8__locals1) >= 4)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9((num > num2) ? "ribs_l" : "ribs_r", (num > num2) ? "COSTILLAS IZQUIERDAS FISURADAS" : "COSTILLAS DERECHAS FISURADAS", Severity.Moderate, true, ref CS$<>8__locals1);
			}
			if (XRayAnalysis.<BuildBeating>g__C|9_10("chest", ref CS$<>8__locals1) >= 1)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("bruise:chest", "PECHO MAGULLADO", Severity.Minor, false, ref CS$<>8__locals1);
			}
			if (XRayAnalysis.<BuildBeating>g__C|9_10("stomach", ref CS$<>8__locals1) >= 4)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("vientre", "SIN ALIENTO - VIENTRE MUY MAGULLADO", Severity.Moderate, false, ref CS$<>8__locals1);
			}
			else if (XRayAnalysis.<BuildBeating>g__C|9_10("stomach", ref CS$<>8__locals1) >= 1)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("bruise:stomach", "ESTÓMAGO MAGULLADO", Severity.Minor, false, ref CS$<>8__locals1);
			}
			if (XRayAnalysis.<BuildBeating>g__C|9_10("back", ref CS$<>8__locals1) >= 3)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("bruise:kidneys", "RIÑONES MAGULLADOS", Severity.Moderate, false, ref CS$<>8__locals1);
			}
			else if (XRayAnalysis.<BuildBeating>g__C|9_10("back", ref CS$<>8__locals1) >= 1)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("bruise:back", "ESPALDA MAGULLADA", Severity.Minor, false, ref CS$<>8__locals1);
			}
			foreach (string text3 in new string[]
			{
				"brazo izquierdo",
				"brazo derecho",
				"pierna izquierda",
				"pierna derecha"
			})
			{
				if (XRayAnalysis.<BuildBeating>g__C|9_10(text3, ref CS$<>8__locals1) >= 1)
				{
					XRayAnalysis.<BuildBeating>g__Add|9_9("bruise:" + text3, "MAGULLADURA EN " + text3.ToUpperInvariant(), Severity.Minor, false, ref CS$<>8__locals1);
				}
			}
			if (XRayAnalysis.<BuildBeating>g__C|9_10("body", ref CS$<>8__locals1) >= 1 && CS$<>8__locals1.d.Count == 0)
			{
				XRayAnalysis.<BuildBeating>g__Add|9_9("bruise:body", "MAGULLADO", Severity.Minor, false, ref CS$<>8__locals1);
			}
			CS$<>8__locals1.d.Sort((DamageItem x, DamageItem y) => y.Severity.CompareTo(x.Severity));
		}

		// Token: 0x06001483 RID: 5251 RVA: 0x00149128 File Offset: 0x00147328
		private static string BlowRegion(int bone, V3 dir)
		{
			string text = Anatomy.RegionOfBone(bone);
			bool flag = dir.Y > 0.3f;
			if (text != null)
			{
				switch (text.Length)
				{
				case 8:
				{
					char c = text[6];
					if (c != 'a')
					{
						if (c != 'c')
						{
							if (c != 'p')
							{
								goto IL_280;
							}
							if (!(text == "la cadera"))
							{
								goto IL_280;
							}
						}
						else
						{
							if (!(text == "el cuello"))
							{
								goto IL_280;
							}
							return "garganta";
						}
					}
					else
					{
						if (!(text == "la cabeza"))
						{
							goto IL_280;
						}
						return "cara";
					}
					break;
				}
				case 9:
					if (!(text == "el pecho"))
					{
						goto IL_280;
					}
					if (!flag)
					{
						return "chest";
					}
					return "back";
				case 10:
				case 16:
					goto IL_280;
				case 11:
					if (!(text == "el abdomen"))
					{
						goto IL_280;
					}
					break;
				case 12:
				{
					char c = text[9];
					if (c != 'a')
					{
						if (c != 'l')
						{
							goto IL_280;
						}
						if (!(text == "la pierna izquierda"))
						{
							goto IL_280;
						}
						goto IL_274;
					}
					else
					{
						if (!(text == "el brazo izquierdo"))
						{
							goto IL_280;
						}
						goto IL_268;
					}
					break;
				}
				case 13:
				{
					char c = text[11];
					if (c != 'e')
					{
						switch (c)
						{
						case 'n':
							if (!(text == "la mano izquierda"))
							{
								goto IL_280;
							}
							goto IL_268;
						case 'o':
							if (!(text == "el pie izquierdo"))
							{
								goto IL_280;
							}
							goto IL_274;
						case 'p':
						case 'q':
							goto IL_280;
						case 'r':
							if (!(text == "el brazo derecho"))
							{
								goto IL_280;
							}
							goto IL_26E;
						default:
							goto IL_280;
						}
					}
					else
					{
						if (!(text == "la pierna derecha"))
						{
							goto IL_280;
						}
						goto IL_27A;
					}
					break;
				}
				case 14:
				{
					char c = text[11];
					if (c != 'a')
					{
						if (c != 'i')
						{
							if (c != 'o')
							{
								goto IL_280;
							}
							if (!(text == "el pie derecho"))
							{
								goto IL_280;
							}
							goto IL_27A;
						}
						else
						{
							if (!(text == "el muslo izquierdo"))
							{
								goto IL_280;
							}
							goto IL_274;
						}
					}
					else
					{
						if (!(text == "la mano derecha"))
						{
							goto IL_280;
						}
						goto IL_26E;
					}
					break;
				}
				case 15:
					if (!(text == "el muslo derecho"))
					{
						goto IL_280;
					}
					goto IL_27A;
				case 17:
					if (!(text == "el hombro izquierdo"))
					{
						goto IL_280;
					}
					goto IL_268;
				case 18:
					if (!(text == "el hombro derecho"))
					{
						goto IL_280;
					}
					goto IL_26E;
				default:
					goto IL_280;
				}
				if (!flag)
				{
					return "stomach";
				}
				return "back";
				IL_268:
				return "brazo izquierdo";
				IL_26E:
				return "brazo derecho";
				IL_274:
				return "pierna izquierda";
				IL_27A:
				return "pierna derecha";
			}
			IL_280:
			return "body";
		}

		// Token: 0x06001484 RID: 5252 RVA: 0x001493BC File Offset: 0x001475BC
		private static void ApplyAmmo(XRayEntry e, XRayAnalysis a, Random rng)
		{
			PathResult main = a.Main;
			switch (e.Ammo)
			{
			case AmmoKind.SplitPoint:
			{
				float s = Math.Max(0.02f, Math.Min(0.05f, (main.End - main.Entry).Length() * 0.5f));
				V3 v = main.Entry + e.Dir.Normalized() * s;
				int num = 3 + rng.Next(2);
				int num2 = 0;
				for (int i = 0; i < num; i++)
				{
					V3 v2 = XRayAnalysis.Jitter(e.Dir.Normalized(), 0.7f, rng);
					PathResult pathResult = Anatomy.Trace(a.Body, v - v2 * 0.01f, v2, WeaponClass.Shotgun, 1.5f, AmmoKind.Regular, 2f);
					if (pathResult != null)
					{
						pathResult.Estimated = true;
						pathResult.Entry = v;
						pathResult.Start = v;
						a.Pellets.Add(pathResult);
						num2++;
					}
				}
				main.Damage.Add(new DamageItem
				{
					Key = "ammo:split",
					Name = ((num2 > 0) ? string.Format("BULLET BROKE APART - {0} FRAGMENTS", num2) : "BULLET FLATTENED"),
					Severity = Severity.Moderate
				});
				main.Damage.Add(new DamageItem
				{
					Key = "ammo:channel",
					Name = "WIDE WOUND CHANNEL",
					Severity = Severity.Moderate
				});
				goto IL_4A8;
			}
			case AmmoKind.Express:
			case AmmoKind.HighVelocity:
				goto IL_4A8;
			case AmmoKind.Explosive:
			{
				V3 entry = main.Entry;
				int num3 = 5 + rng.Next(3);
				int num4 = 0;
				for (int j = 0; j < num3; j++)
				{
					V3 v3 = XRayAnalysis.Jitter(e.Dir.Normalized(), 0.9f, rng);
					PathResult pathResult2 = Anatomy.Trace(a.Body, entry - v3 * 0.01f, v3, WeaponClass.Blast, 1.5f, AmmoKind.Regular, 1f);
					if (pathResult2 != null)
					{
						pathResult2.Estimated = true;
						pathResult2.Entry = entry;
						pathResult2.Start = entry;
						a.Pellets.Add(pathResult2);
						num4++;
					}
				}
				string text = XRayAnalysis.Bare(Anatomy.RegionOf(a.Body, entry));
				main.Damage.Add(new DamageItem
				{
					Key = "ammo:explode",
					Name = string.Format("ROUND EXPLODED IN THE WOUND - {0} FRAGMENTS", num4),
					Severity = Severity.Severe
				});
				main.Damage.Add(new DamageItem
				{
					Key = "burn:" + text,
					Name = text.ToUpperInvariant() + " BURNED",
					Severity = Severity.Moderate
				});
				goto IL_4A8;
			}
			case AmmoKind.Incendiary:
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>();
				foreach (PathResult pathResult3 in new PathResult[]
				{
					main
				}.Concat(a.Pellets))
				{
					string key = XRayAnalysis.Bare(Anatomy.RegionOf(a.Body, pathResult3.Entry));
					int num5;
					dictionary[key] = (dictionary.TryGetValue(key, out num5) ? (num5 + 1) : 1);
				}
				using (IEnumerator<KeyValuePair<string, int>> enumerator2 = (from x in dictionary
				orderby x.Value descending
				select x).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						KeyValuePair<string, int> keyValuePair = enumerator2.Current;
						main.Damage.Add(new DamageItem
						{
							Key = "burn:" + keyValuePair.Key,
							Name = keyValuePair.Key.ToUpperInvariant() + " BURNED",
							Severity = ((keyValuePair.Value >= 3) ? Severity.Severe : Severity.Moderate)
						});
					}
					goto IL_4A8;
				}
				break;
			}
			case AmmoKind.Slug:
				break;
			case AmmoKind.FireArrow:
			{
				string text2 = XRayAnalysis.Bare(Anatomy.RegionOf(a.Body, main.Entry));
				main.Damage.Add(new DamageItem
				{
					Key = "burn:" + text2,
					Name = text2.ToUpperInvariant() + " BURNED - FIRE ARROW",
					Severity = Severity.Moderate
				});
				goto IL_4A8;
			}
			case AmmoKind.PoisonArrow:
				main.Damage.Add(new DamageItem
				{
					Key = "ammo:poison",
					Name = "POISON IN THE BLOOD",
					Severity = Severity.Severe
				});
				goto IL_4A8;
			default:
				goto IL_4A8;
			}
			main.Damage.Add(new DamageItem
			{
				Key = "ammo:slug",
				Name = "HIT BY A SLUG",
				Severity = Severity.Moderate
			});
			IL_4A8:
			main.Damage.Sort((DamageItem x, DamageItem y) => y.Severity.CompareTo(x.Severity));
		}

		// Token: 0x06001485 RID: 5253 RVA: 0x001498B8 File Offset: 0x00147AB8
		private static string Bare(string region)
		{
			string result;
			if (region == null || !region.StartsWith("the "))
			{
				result = region;
				if (region == null)
				{
					return "body";
				}
			}
			else
			{
				result = region.Substring(4);
			}
			return result;
		}

		// Token: 0x06001486 RID: 5254 RVA: 0x001498DC File Offset: 0x00147ADC
		private static V3 Jitter(V3 d, float maxRad, Random rng)
		{
			V3 a = V3.Cross(d, new V3(0f, 0f, 1f)).Normalized();
			if (a.Length() < 0.5f)
			{
				a = new V3(1f, 0f, 0f);
			}
			V3 a2 = V3.Cross(a, d).Normalized();
			double num = rng.NextDouble() * 3.141592653589793 * 2.0;
			double num2 = Math.Tan(rng.NextDouble() * (double)maxRad);
			return (d + a * (float)(Math.Cos(num) * num2) + a2 * (float)(Math.Sin(num) * num2)).Normalized();
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x001499BC File Offset: 0x00147BBC
		[CompilerGenerated]
		internal static string <BuildBeating>g__Of|9_7(int n, string one, string many)
		{
			if (n != 0)
			{
				return string.Format("{0} {1}", n, (n == 1) ? one : many);
			}
			return null;
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x001499DB File Offset: 0x00147BDB
		[CompilerGenerated]
		internal static void <BuildBeating>g__Add|9_9(string key, string name, Severity s, bool bone = false, ref XRayAnalysis.<>c__DisplayClass9_0 A_4)
		{
			A_4.d.Add(new DamageItem
			{
				Key = key,
				Name = name,
				Severity = s,
				IsBone = bone
			});
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x00149A0C File Offset: 0x00147C0C
		[CompilerGenerated]
		internal static int <BuildBeating>g__C|9_10(string reg, ref XRayAnalysis.<>c__DisplayClass9_0 A_1)
		{
			int result;
			if (!A_1.weight.TryGetValue(reg, out result))
			{
				return 0;
			}
			return result;
		}

		// Token: 0x04000E7E RID: 3710
		public Anatomy.Body Body;

		// Token: 0x04000E7F RID: 3711
		public PathResult Main;

		// Token: 0x04000E80 RID: 3712
		public readonly List<PathResult> Pellets = new List<PathResult>();

		// Token: 0x04000E81 RID: 3713
		[TupleElementNames(new string[]
		{
			"region",
			"blows"
		})]
		public readonly List<ValueTuple<string, int>> Blows = new List<ValueTuple<string, int>>();
	}
}
