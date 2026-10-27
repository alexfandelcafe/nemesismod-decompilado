using System;
using System.Collections.Generic;
using System.Linq;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x02000091 RID: 145
	public static class AnimalAnatomy
	{
		// Token: 0x060013FA RID: 5114 RVA: 0x0013ED64 File Offset: 0x0013CF64
		public static bool LooksFourLegged(ICollection<int> ids)
		{
			return ids.Contains(56200) && ids.Contains(21030) && (ids.Contains(14410) || ids.Contains(14411)) && (ids.Contains(37873) || ids.Contains(46065)) && (ids.Contains(65478) || ids.Contains(6884));
		}

		// Token: 0x060013FB RID: 5115 RVA: 0x0013EDDC File Offset: 0x0013CFDC
		public static Anatomy.Body Build(Dictionary<int, V3> bones)
		{
			AnimalAnatomy.<>c__DisplayClass7_0 CS$<>8__locals1 = new AnimalAnatomy.<>c__DisplayClass7_0();
			CS$<>8__locals1.bones = bones;
			Anatomy.Body body = new Anatomy.Body(CS$<>8__locals1.bones)
			{
				Animal = true
			};
			V3 v11 = CS$<>8__locals1.<Build>g__G|0(56200, AnimalAnatomy.Mean(CS$<>8__locals1.bones.Values));
			V3 v2 = CS$<>8__locals1.bones.ContainsKey(14283) ? CS$<>8__locals1.bones[14283] : (CS$<>8__locals1.bones.ContainsKey(14413) ? CS$<>8__locals1.bones[14413] : (v11 + new V3(0f, 0.55f, 0f)));
			V3 v3 = v2 - v11;
			float num = Math.Max(0.15f, v3.Length());
			V3 v4 = v3.Normalized();
			if (v4.Length() < 0.5f)
			{
				v4 = new V3(0f, 1f, 0f);
			}
			List<V3> list = (from id in new int[]
			{
				34606,
				22798,
				45454,
				33646,
				53675,
				54187,
				55120,
				43312
			}.Where(new Func<int, bool>(CS$<>8__locals1.bones.ContainsKey))
			select CS$<>8__locals1.bones[id]).ToList<V3>();
			V3 b = V3.Lerp(v11, v2, 0.5f);
			V3 a = (list.Count > 0) ? (AnimalAnatomy.Mean(list) - b) : new V3(0f, 0f, -1f);
			V3 v5 = (a - v4 * V3.Dot(a, v4)).Normalized();
			if (v5.Length() < 0.5f)
			{
				V3 a2 = new V3(0f, 0f, -1f);
				v5 = (a2 - v4 * V3.Dot(a2, v4)).Normalized();
			}
			if (v5.Length() < 0.5f)
			{
				v5 = new V3(0f, 0f, -1f);
			}
			V3 v6 = V3.Cross(v5, v4).Normalized();
			CS$<>8__locals1.d = 0.31f * num;
			CS$<>8__locals1.a = 0.21f * num;
			body.Scale = num;
			body.Depth = CS$<>8__locals1.d;
			body.HalfWidth = CS$<>8__locals1.a;
			body.Up = v4;
			body.Fwd = v5;
			body.Right = v6;
			body.PelvisPos = v11 + v5 * (CS$<>8__locals1.d * 0.85f);
			body.NeckPos = v2 + v5 * (CS$<>8__locals1.d * 0.8f);
			body.TorsoLength = num;
			body.BudgetScale = ((num > 0.95f) ? 0.85f : ((num > 0.55f) ? 0.95f : 1f));
			V3 v7 = CS$<>8__locals1.<Build>g__G|0(21030, v2 + v4 * 0.35f * num);
			V3 b2 = (from id in new int[]
			{
				14286,
				14285,
				14284,
				14283
			}.Where(new Func<int, bool>(CS$<>8__locals1.bones.ContainsKey))
			select CS$<>8__locals1.bones[id]).DefaultIfEmpty(v2).First<V3>();
			V3 v8 = (v7 - b2).Normalized();
			V3 v9 = (v8 * 0.3f + v4 + v5 * 0.35f).Normalized();
			if (v9.Length() < 0.5f)
			{
				v9 = v4;
			}
			body.Snout = v9;
			body.SnoutTip = v7 + v9 * (0.42f * num);
			body.HeadCenter = v7 + v9 * (0.08f * num);
			V3 a3 = (v5 * -1f - v9 * V3.Dot(v5 * -1f, v9)).Normalized();
			if (a3.Length() < 0.5f)
			{
				a3 = new V3(0f, 0f, 1f);
			}
			AnimalAnatomy.Soft(body, "la cabeza", new Anatomy.Capsule
			{
				A = v7 - v9 * (0.04f * num),
				B = body.SnoutTip,
				R = 0.1f * num
			});
			AnimalAnatomy.Soft(body, "el cuello", new Anatomy.Capsule
			{
				A = v2,
				B = v7,
				R = 0.13f * num
			});
			foreach (ValueTuple<float, float, string> valueTuple in new ValueTuple<float, float, string>[]
			{
				new ValueTuple<float, float, string>(-0.1f, 0.32f, "the hindquarters"),
				new ValueTuple<float, float, string>(0.32f, 0.62f, "the flank"),
				new ValueTuple<float, float, string>(0.62f, 1.04f, "el pecho")
			})
			{
				float item = valueTuple.Item1;
				float item2 = valueTuple.Item2;
				string item3 = valueTuple.Item3;
				foreach (float num2 in new float[]
				{
					-1f,
					1f
				})
				{
					AnimalAnatomy.Soft(body, item3, new Anatomy.Capsule
					{
						A = body.TorsoPoint(item, 0f, num2 * (CS$<>8__locals1.d - CS$<>8__locals1.a)),
						B = body.TorsoPoint(item2, 0f, num2 * (CS$<>8__locals1.d - CS$<>8__locals1.a)),
						R = CS$<>8__locals1.a
					});
				}
			}
			foreach (ValueTuple<string, float, int, int, int, int, int, ValueTuple<int, int, int, int, int, int>> valueTuple2 in new ValueTuple<string, float, int, int, int, int, int, ValueTuple<int, int, int, int, int, int>>[]
			{
				new ValueTuple<string, float, int, int, int, int, int, ValueTuple<int, int, int, int, int, int>>("left", -1f, 30226, 37873, 53675, 34606, 41403, new ValueTuple<int, int, int, int, int, int>(41404, 65478, 55120, 45454, 53081, 53082)),
				new ValueTuple<string, float, int, int, int, int, int, ValueTuple<int, int, int, int, int, int>>("right", 1f, 54802, 46065, 54187, 22798, 16827, new ValueTuple<int, int, int, int, int, int>(16828, 6884, 43312, 33646, 41273, 41274))
			})
			{
				string item4 = valueTuple2.Item1;
				float item5 = valueTuple2.Item2;
				int item6 = valueTuple2.Item3;
				int item7 = valueTuple2.Item4;
				int item8 = valueTuple2.Item5;
				int item9 = valueTuple2.Item6;
				int item10 = valueTuple2.Item7;
				int item11 = valueTuple2.Rest.Item1;
				int item12 = valueTuple2.Rest.Item2;
				int item13 = valueTuple2.Rest.Item3;
				int item14 = valueTuple2.Rest.Item4;
				int item15 = valueTuple2.Rest.Item5;
				int item16 = valueTuple2.Rest.Item6;
				AnimalAnatomy.SoftSeg(body, item6, item7, "the " + item4 + " shoulder", 0.1f * num);
				AnimalAnatomy.SoftSeg(body, item7, item8, "the " + item4 + " foreleg", 0.07f * num);
				AnimalAnatomy.SoftSeg(body, item8, item9, "the " + item4 + " foreleg", 0.045f * num);
				AnimalAnatomy.SoftSeg(body, item9, item10, "the " + item4 + " front shin", 0.032f * num);
				AnimalAnatomy.SoftSeg(body, item10, item11, "the " + item4 + " front foot", 0.032f * num);
				AnimalAnatomy.SoftSeg(body, item12, item13, "the " + item4 + " haunch", 0.11f * num);
				AnimalAnatomy.SoftSeg(body, item13, item14, "the " + item4 + " hind leg", 0.06f * num);
				AnimalAnatomy.SoftSeg(body, item14, item15, "the " + item4 + " hind shin", 0.035f * num);
				AnimalAnatomy.SoftSeg(body, item15, item16, "the " + item4 + " hind foot", 0.033f * num);
				string str = (item5 < 0f) ? "_l" : "_r";
				string str2 = item4.ToUpperInvariant();
				AnimalAnatomy.HardSeg(body, item6, item7, "scapula" + str, str2 + " SHOULDER BLADE BROKEN", Severity.Moderate, 0.05f * num);
				AnimalAnatomy.HardSeg(body, item7, item8, "humerus" + str, str2 + " UPPER FORELEG BROKEN", Severity.Severe, 0.03f * num);
				AnimalAnatomy.HardSeg(body, item8, item9, "forearm" + str, str2 + " FORELEG BROKEN", Severity.Severe, 0.025f * num);
				AnimalAnatomy.HardSeg(body, item9, item10, "cannon_f" + str, str2 + " FRONT SHIN BONE BROKEN", Severity.Severe, 0.02f * num);
				AnimalAnatomy.HardSeg(body, item10, item11, "foot_f" + str, str2 + " FRONT FOOT SHATTERED", Severity.Moderate, 0.022f * num);
				AnimalAnatomy.HardSeg(body, item12, item13, "femur" + str, str2 + " FEMUR SHATTERED", Severity.Severe, 0.035f * num);
				AnimalAnatomy.HardSeg(body, item13, item14, "tibia" + str, str2 + " HIND LEG BROKEN", Severity.Severe, 0.026f * num);
				AnimalAnatomy.HardSeg(body, item14, item15, "cannon_h" + str, str2 + " HIND SHIN BONE BROKEN", Severity.Severe, 0.02f * num);
				AnimalAnatomy.HardSeg(body, item15, item16, "foot_h" + str, str2 + " HIND FOOT SHATTERED", Severity.Moderate, 0.022f * num);
				if (CS$<>8__locals1.bones.ContainsKey(item12) && CS$<>8__locals1.bones.ContainsKey(item13))
				{
					V3 a4 = CS$<>8__locals1.bones[item12];
					V3 b3 = CS$<>8__locals1.bones[item13];
					body.Hard.Add(new Anatomy.Capsule
					{
						Key = "femoral" + str,
						Name = str2 + " FEMORAL ARTERY SEVERED",
						Severity = Severity.Critical,
						A = a4 - v6 * (item5 * 0.04f * num),
						B = V3.Lerp(a4, b3, 0.6f) - v6 * (item5 * 0.04f * num),
						R = 0.025f * num
					});
				}
			}
			List<V3> list2 = (from id in AnimalAnatomy.TailIds.Where(new Func<int, bool>(CS$<>8__locals1.bones.ContainsKey))
			select CS$<>8__locals1.bones[id]).ToList<V3>();
			if (list2.Count > 0)
			{
				AnimalAnatomy.Soft(body, "the tail", new Anatomy.Capsule
				{
					A = v11,
					B = list2[list2.Count - 1],
					R = 0.04f * num
				});
				body.Hard.Add(new Anatomy.Capsule
				{
					Key = "tail",
					Name = "TAIL BROKEN",
					Severity = Severity.Minor,
					IsBone = true,
					A = v11,
					B = list2[list2.Count - 1],
					R = 0.025f * num
				});
			}
			body.Hard.Add(new Anatomy.Capsule
			{
				Key = "skull",
				Name = "SKULL SHATTERED",
				Severity = Severity.Severe,
				IsBone = true,
				A = v7 - v9 * (0.02f * num),
				B = v7 + v9 * (0.3f * num),
				R = 0.085f * num
			});
			body.Hard.Add(new Anatomy.Capsule
			{
				Key = "jaw",
				Name = "MANDÍBULA DESTROZADA",
				Severity = Severity.Severe,
				IsBone = true,
				A = v7 - a3 * (0.07f * num),
				B = body.SnoutTip - a3 * (0.05f * num),
				R = 0.035f * num
			});
			List<V3> list3 = (from id in new int[]
			{
				14283,
				14284,
				14285,
				14286,
				21030
			}.Where(new Func<int, bool>(CS$<>8__locals1.bones.ContainsKey))
			select CS$<>8__locals1.bones[id]).ToList<V3>();
			int num3 = 0;
			while (num3 + 1 < list3.Count)
			{
				body.Hard.Add(new Anatomy.Capsule
				{
					Key = "cervical",
					Name = "NECK BROKEN",
					Severity = Severity.Fatal,
					IsBone = true,
					A = list3[num3],
					B = list3[num3 + 1],
					R = 0.045f * num
				});
				num3++;
			}
			List<ValueTuple<int, V3>> list4 = (from id in new int[]
			{
				56200,
				14410,
				14411,
				14412,
				14413,
				14283
			}.Where(new Func<int, bool>(CS$<>8__locals1.bones.ContainsKey))
			select new ValueTuple<int, V3>(id, CS$<>8__locals1.bones[id])).ToList<ValueTuple<int, V3>>();
			int num4 = 0;
			while (num4 + 1 < list4.Count)
			{
				bool flag = list4[num4].Item1 == 14412 || list4[num4].Item1 == 14413;
				body.Hard.Add(new Anatomy.Capsule
				{
					Key = (flag ? "spine_t" : "spine_l"),
					Name = (flag ? "SPINE SEVERED (OVER THE SHOULDERS)" : "COLUMNA SECCIONADA (ESPALDA BAJA)"),
					Severity = Severity.Critical,
					IsBone = true,
					A = list4[num4].Item2,
					B = list4[num4 + 1].Item2,
					R = 0.04f * num
				});
				num4++;
			}
			body.Hard.Add(new AnimalAnatomy.TrunkShell
			{
				Key = "ribs_l",
				Name = "COSTILLAS IZQUIERDAS DESTROZADAS",
				Severity = Severity.Moderate,
				IsBone = true,
				Body = body,
				FracMin = 0.48f,
				FracMax = 1.02f,
				E0 = 0.8f,
				E1 = 1.04f,
				SideTest = ((float r, float v) => r < -0.12f * CS$<>8__locals1.a && v < CS$<>8__locals1.d * 0.85f)
			});
			body.Hard.Add(new AnimalAnatomy.TrunkShell
			{
				Key = "ribs_r",
				Name = "COSTILLAS DERECHAS DESTROZADAS",
				Severity = Severity.Moderate,
				IsBone = true,
				Body = body,
				FracMin = 0.48f,
				FracMax = 1.02f,
				E0 = 0.8f,
				E1 = 1.04f,
				SideTest = ((float r, float v) => r > 0.12f * CS$<>8__locals1.a && v < CS$<>8__locals1.d * 0.85f)
			});
			body.Hard.Add(new AnimalAnatomy.TrunkShell
			{
				Key = "sternum",
				Name = "BREASTBONE CRACKED",
				Severity = Severity.Moderate,
				IsBone = true,
				Body = body,
				FracMin = 0.58f,
				FracMax = 1f,
				E0 = 0.78f,
				E1 = 1.04f,
				SideTest = ((float r, float v) => Math.Abs(r) <= 0.25f * CS$<>8__locals1.a && v > CS$<>8__locals1.d * 0.7f)
			});
			body.Hard.Add(new Anatomy.Sphere
			{
				Key = "pelvis",
				Name = "PELVIS DESTROZADA",
				Severity = Severity.Severe,
				IsBone = true,
				C = v11 + v5 * (0.1f * num) - v4 * (0.02f * num),
				R = 0.14f * num
			});
			V3 v10 = V3.Lerp(AnimalAnatomy.Mean((from id in new int[]
			{
				53675,
				54187
			}.Where(new Func<int, bool>(CS$<>8__locals1.bones.ContainsKey))
			select CS$<>8__locals1.bones[id]).DefaultIfEmpty(body.TorsoPoint(0.8f, 0f, CS$<>8__locals1.d))), body.TorsoPoint(0.78f, 0f, 0.35f * CS$<>8__locals1.d), 0.55f);
			if (Anatomy.DistToSegment(v10, body.PelvisPos, body.NeckPos) > CS$<>8__locals1.d * 1.1f)
			{
				v10 = body.TorsoPoint(0.8f, -0.05f * CS$<>8__locals1.a, 0.45f * CS$<>8__locals1.d);
			}
			AnimalAnatomy.Organ(body, "brain", "CEREBRO DESTRUIDO", Severity.Fatal, v7 + v9 * (0.04f * num) + a3 * (0.03f * num), 0.075f * num);
			AnimalAnatomy.Organ(body, "heart", "HEART SHOT", Severity.Fatal, v10, 0.1f * num);
			AnimalAnatomy.Organ(body, "lung_l", "LEFT LUNG HIT", Severity.Critical, body.TorsoPoint(0.84f, -0.5f * CS$<>8__locals1.a, -0.15f * CS$<>8__locals1.d), 0.16f * num);
			AnimalAnatomy.Organ(body, "lung_l", "LEFT LUNG HIT", Severity.Critical, body.TorsoPoint(0.68f, -0.45f * CS$<>8__locals1.a, -0.08f * CS$<>8__locals1.d), 0.14f * num);
			AnimalAnatomy.Organ(body, "lung_r", "RIGHT LUNG HIT", Severity.Critical, body.TorsoPoint(0.84f, 0.5f * CS$<>8__locals1.a, -0.15f * CS$<>8__locals1.d), 0.16f * num);
			AnimalAnatomy.Organ(body, "lung_r", "RIGHT LUNG HIT", Severity.Critical, body.TorsoPoint(0.68f, 0.45f * CS$<>8__locals1.a, -0.08f * CS$<>8__locals1.d), 0.14f * num);
			AnimalAnatomy.Organ(body, "liver", "LIVER HIT", Severity.Severe, body.TorsoPoint(0.58f, 0.25f * CS$<>8__locals1.a, 0.15f * CS$<>8__locals1.d), 0.13f * num);
			AnimalAnatomy.Organ(body, "stomach", "STOMACH HIT", Severity.Severe, body.TorsoPoint(0.47f, -0.25f * CS$<>8__locals1.a, 0.25f * CS$<>8__locals1.d), 0.17f * num);
			AnimalAnatomy.Organ(body, "intestines", "INTESTINOS DESGARRADOS", Severity.Severe, body.TorsoPoint(0.28f, 0f, 0.3f * CS$<>8__locals1.d), 0.17f * num);
			AnimalAnatomy.Organ(body, "kidney_l", "LEFT KIDNEY HIT", Severity.Severe, body.TorsoPoint(0.4f, -0.3f * CS$<>8__locals1.a, -0.7f * CS$<>8__locals1.d), 0.055f * num);
			AnimalAnatomy.Organ(body, "kidney_r", "RIGHT KIDNEY HIT", Severity.Severe, body.TorsoPoint(0.4f, 0.3f * CS$<>8__locals1.a, -0.7f * CS$<>8__locals1.d), 0.055f * num);
			AnimalAnatomy.Organ(body, "bladder", "VEJIGA ROTA", Severity.Moderate, body.TorsoPoint(0.06f, 0f, 0.25f * CS$<>8__locals1.d), 0.07f * num);
			V3 a5 = (v5 - v8 * V3.Dot(v5, v8)).Normalized();
			if (a5.Length() < 0.5f)
			{
				a5 = v5;
			}
			V3 a6 = V3.Lerp(v2, v7, 0.45f) + a5 * (0.07f * num);
			AnimalAnatomy.Organ(body, "carotid_l", "LEFT JUGULAR SEVERED", Severity.Critical, a6 - v6 * (0.045f * num), 0.035f * num);
			AnimalAnatomy.Organ(body, "carotid_r", "RIGHT JUGULAR SEVERED", Severity.Critical, a6 + v6 * (0.045f * num), 0.035f * num);
			AnimalAnatomy.Organ(body, "windpipe", "WINDPIPE HIT", Severity.Severe, a6 + a5 * (0.03f * num), 0.03f * num);
			return body;
		}

		// Token: 0x060013FC RID: 5116 RVA: 0x0014027C File Offset: 0x0013E47C
		public static string Describe(Anatomy.Body b, V3 p, string region)
		{
			if (!(region == "el pecho") && !(region == "the flank") && !(region == "the hindquarters"))
			{
				return region ?? "el cuerpo";
			}
			float num;
			float num2;
			float num3;
			b.TorsoCoords(p, out num, out num2, out num3);
			if (num3 > b.Depth * 0.6f)
			{
				if (!(region == "el pecho"))
				{
					return "the belly";
				}
				return "the brisket";
			}
			else if (num3 < -b.Depth * 0.6f)
			{
				if (!(region == "the hindquarters"))
				{
					return "the back";
				}
				return "the rump";
			}
			else
			{
				string text = (num2 < -0.15f * b.HalfWidth) ? "left" : ((num2 > 0.15f * b.HalfWidth) ? "right" : null);
				if (text != null)
				{
					return "the " + text + " side of " + region.Substring(4);
				}
				return region;
			}
		}

		// Token: 0x060013FD RID: 5117 RVA: 0x00140368 File Offset: 0x0013E568
		public static void AddVerdicts(PathResult m, IEnumerable<PathResult> extra)
		{
			if (m == null)
			{
				return;
			}
			HashSet<string> hashSet = new HashSet<string>(new PathResult[]
			{
				m
			}.Concat(extra ?? Enumerable.Empty<PathResult>()).SelectMany((PathResult x) => x.HitKeys));
			bool flag = hashSet.Contains("brain");
			bool flag2 = hashSet.Contains("heart");
			bool flag3 = hashSet.Contains("lung_l") && hashSet.Contains("lung_r");
			bool flag4 = hashSet.Contains("cervical") || hashSet.Contains("spine_t") || hashSet.Contains("spine_l");
			bool flag5 = hashSet.Contains("stomach") || hashSet.Contains("intestines") || hashSet.Contains("liver");
			if (flag)
			{
				m.Damage.Insert(0, new DamageItem
				{
					Key = "verdict",
					Name = "BRAIN SHOT - DROPPED ON THE SPOT",
					Severity = Severity.Fatal
				});
				return;
			}
			if (flag2)
			{
				m.Damage.Insert(0, new DamageItem
				{
					Key = "verdict",
					Name = "HEART SHOT - A CLEAN KILL",
					Severity = Severity.Fatal
				});
				return;
			}
			if (flag3)
			{
				m.Damage.Insert(0, new DamageItem
				{
					Key = "verdict",
					Name = "BOTH LUNGS - A QUICK DEATH",
					Severity = Severity.Fatal
				});
				return;
			}
			if (flag4)
			{
				m.Damage.Insert(0, new DamageItem
				{
					Key = "verdict",
					Name = "SPINE SHOT - DROPPED WHERE IT STOOD",
					Severity = Severity.Critical
				});
				return;
			}
			if (flag5)
			{
				m.Damage.Insert(0, new DamageItem
				{
					Key = "verdict",
					Name = "GUT SHOT - A SLOW DEATH",
					Severity = Severity.Severe
				});
			}
		}

		// Token: 0x060013FE RID: 5118 RVA: 0x0014053B File Offset: 0x0013E73B
		private static void Soft(Anatomy.Body b, string region, Anatomy.Shape s)
		{
			b.Soft.Add(new Anatomy.SoftPart
			{
				Shape = s,
				Region = region
			});
		}

		// Token: 0x060013FF RID: 5119 RVA: 0x0014055C File Offset: 0x0013E75C
		private static void SoftSeg(Anatomy.Body b, int a, int c, string region, float r)
		{
			if (b.Has(a) && b.Has(c))
			{
				AnimalAnatomy.Soft(b, region, new Anatomy.Capsule
				{
					A = b.Get(a),
					B = b.Get(c),
					R = r
				});
			}
		}

		// Token: 0x06001400 RID: 5120 RVA: 0x001405AC File Offset: 0x0013E7AC
		private static void HardSeg(Anatomy.Body b, int a, int c, string key, string name, Severity sev, float r)
		{
			if (b.Has(a) && b.Has(c))
			{
				b.Hard.Add(new Anatomy.Capsule
				{
					Key = key,
					Name = name,
					Severity = sev,
					IsBone = true,
					A = b.Get(a),
					B = b.Get(c),
					R = r
				});
			}
		}

		// Token: 0x06001401 RID: 5121 RVA: 0x0014061B File Offset: 0x0013E81B
		private static void Organ(Anatomy.Body b, string key, string name, Severity sev, V3 c, float r)
		{
			b.Organs.Add(new Anatomy.Sphere
			{
				Key = key,
				Name = name,
				Severity = sev,
				C = c,
				R = r
			});
		}

		// Token: 0x06001402 RID: 5122 RVA: 0x00140654 File Offset: 0x0013E854
		private static V3 Mean(IEnumerable<V3> pts)
		{
			List<V3> list = pts.ToList<V3>();
			if (list.Count == 0)
			{
				return new V3(0f, 0f, 0f);
			}
			V3 a = new V3(0f, 0f, 0f);
			foreach (V3 b in list)
			{
				a += b;
			}
			return a * (1f / (float)list.Count);
		}

		// Token: 0x04000DB7 RID: 3511
		public static int[] TailIds = new int[0];

		// Token: 0x04000DB8 RID: 3512
		public const int LHoof0 = 41403;

		// Token: 0x04000DB9 RID: 3513
		public const int LHoof1 = 41404;

		// Token: 0x04000DBA RID: 3514
		public const int RHoof0 = 16827;

		// Token: 0x04000DBB RID: 3515
		public const int RHoof1 = 16828;

		// Token: 0x02000489 RID: 1161
		public sealed class TrunkShell : Anatomy.Shape
		{
			// Token: 0x0600261C RID: 9756 RVA: 0x001827C4 File Offset: 0x001809C4
			public override bool Contains(V3 p)
			{
				float num;
				float num2;
				float num3;
				this.Body.TorsoCoords(p, out num, out num2, out num3);
				if (num < this.FracMin || num > this.FracMax)
				{
					return false;
				}
				float num4 = (float)Math.Sqrt((double)(num2 / this.Body.HalfWidth * (num2 / this.Body.HalfWidth) + num3 / this.Body.Depth * (num3 / this.Body.Depth)));
				return num4 >= this.E0 && num4 <= this.E1 && this.SideTest(num2, num3);
			}

			// Token: 0x04002078 RID: 8312
			public Anatomy.Body Body;

			// Token: 0x04002079 RID: 8313
			public float FracMin;

			// Token: 0x0400207A RID: 8314
			public float FracMax;

			// Token: 0x0400207B RID: 8315
			public float E0;

			// Token: 0x0400207C RID: 8316
			public float E1;

			// Token: 0x0400207D RID: 8317
			public Func<float, float, bool> SideTest;
		}
	}
}
