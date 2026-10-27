using System;
using System.Collections.Generic;
using System.Linq;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x02000090 RID: 144
	public static class Anatomy
	{
		// Token: 0x060013DE RID: 5086 RVA: 0x0013C9D0 File Offset: 0x0013ABD0
		public static string RegionOfBone(int boneId)
		{
			if (boneId > 37873)
			{
				if (boneId <= 53081)
				{
					if (boneId > 43312)
					{
						if (boneId != 45454)
						{
							if (boneId == 46065)
							{
								goto IL_15C;
							}
							if (boneId != 53081)
							{
								goto IL_19E;
							}
						}
						return "el pie izquierdo";
					}
					if (boneId == 41273)
					{
						goto IL_198;
					}
					if (boneId != 43312)
					{
						goto IL_19E;
					}
					return "la pierna derecha";
				}
				else if (boneId <= 54802)
				{
					if (boneId == 53675)
					{
						return "el brazo izquierdo";
					}
					if (boneId == 54187)
					{
						return "el brazo derecho";
					}
					if (boneId != 54802)
					{
						goto IL_19E;
					}
				}
				else
				{
					if (boneId == 55120)
					{
						return "la pierna izquierda";
					}
					if (boneId == 56200)
					{
						return "la cadera";
					}
					if (boneId != 65478)
					{
						goto IL_19E;
					}
					return "el muslo izquierdo";
				}
				IL_15C:
				return "el hombro derecho";
			}
			if (boneId > 21030)
			{
				if (boneId <= 30226)
				{
					if (boneId == 22798)
					{
						return "la mano derecha";
					}
					if (boneId != 30226)
					{
						goto IL_19E;
					}
				}
				else
				{
					if (boneId == 33646)
					{
						goto IL_198;
					}
					if (boneId == 34606)
					{
						return "la mano izquierda";
					}
					if (boneId != 37873)
					{
						goto IL_19E;
					}
				}
				return "el hombro izquierdo";
			}
			if (boneId <= 14288)
			{
				if (boneId == 6884)
				{
					return "el muslo derecho";
				}
				if (boneId - 14283 > 5)
				{
					goto IL_19E;
				}
				return "el cuello";
			}
			else
			{
				if (boneId - 14410 <= 2)
				{
					return "el abdomen";
				}
				if (boneId - 14413 <= 2)
				{
					return "el pecho";
				}
				if (boneId != 21030)
				{
					goto IL_19E;
				}
				return "la cabeza";
			}
			IL_198:
			return "el pie derecho";
			IL_19E:
			return "el cuerpo";
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x0013CB80 File Offset: 0x0013AD80
		public static Anatomy.Body Build(Dictionary<int, V3> bones)
		{
			Anatomy.Body body = new Anatomy.Body(bones);
			V3 v;
			body.PelvisPos = (bones.TryGetValue(56200, out v) ? v : Anatomy.Average(bones.Values));
			V3 v2;
			V3 a;
			body.NeckPos = (bones.TryGetValue(14283, out v2) ? v2 : (bones.TryGetValue(21030, out a) ? (a - new V3(0f, 0f, 0.12f)) : (body.PelvisPos + new V3(0f, 0f, 0.5f))));
			body.TorsoLength = Math.Max(0.25f, (body.NeckPos - body.PelvisPos).Length());
			body.Up = (body.NeckPos - body.PelvisPos).Normalized();
			V3 a2 = new V3(0f, 1f, 0f);
			body.Fwd = (a2 - body.Up * V3.Dot(a2, body.Up)).Normalized();
			if (body.Fwd.Length() < 0.5f)
			{
				body.Fwd = new V3(0f, 1f, 0f);
			}
			body.Right = V3.Cross(body.Fwd, body.Up).Normalized();
			V3 v4;
			V3 v3 = bones.TryGetValue(21030, out v4) ? v4 : (body.NeckPos + body.Up * 0.1f);
			body.HeadCenter = v3 + body.Up * 0.07f + body.Fwd * 0.02f;
			Anatomy.AddSoft(body, "la cabeza", new Anatomy.Sphere
			{
				C = body.HeadCenter,
				R = 0.11f
			});
			Anatomy.AddSoft(body, "el cuello", new Anatomy.Capsule
			{
				A = body.NeckPos,
				B = v3,
				R = 0.065f
			});
			V3 a3 = body.PelvisPos - body.Up * 0.05f;
			V3 a4 = body.NeckPos - body.Up * 0.12f;
			Anatomy.AddSoft(body, "torso", new Anatomy.Capsule
			{
				A = a3 - body.Right * 0.07f,
				B = a4 - body.Right * 0.07f,
				R = 0.13f
			});
			Anatomy.AddSoft(body, "torso", new Anatomy.Capsule
			{
				A = a3 + body.Right * 0.07f,
				B = a4 + body.Right * 0.07f,
				R = 0.13f
			});
			Anatomy.Limb(body, "left", 30226, 37873, 53675, 34606, "hombro", "brazo superior", "forearm", "hand", 0.07f, 0.055f, 0.045f, 0.055f);
			Anatomy.Limb(body, "right", 54802, 46065, 54187, 22798, "hombro", "brazo superior", "forearm", "hand", 0.07f, 0.055f, 0.045f, 0.055f);
			Anatomy.Limb(body, "left", -1, 65478, 55120, 45454, "", "muslo", "pierna", "foot", 0f, 0.085f, 0.06f, 0.05f);
			Anatomy.Limb(body, "right", -1, 6884, 43312, 33646, "", "muslo", "pierna", "foot", 0f, 0.085f, 0.06f, 0.05f);
			body.Hard.Add(new Anatomy.Sphere
			{
				Key = "skull",
				Name = "CRÁNEO FRACTURADO",
				Severity = Severity.Severe,
				IsBone = true,
				C = body.HeadCenter,
				R = 0.105f
			});
			body.Hard.Add(new Anatomy.Sphere
			{
				Key = "jaw",
				Name = "MANDÍBULA DESTROZADA",
				Severity = Severity.Severe,
				IsBone = true,
				C = v3 + body.Fwd * 0.07f,
				R = 0.035f
			});
			body.Hard.Add(new Anatomy.Capsule
			{
				Key = "cervical",
				Name = "VÉRTEBRAS CERVICALES DESTROZADAS",
				Severity = Severity.Fatal,
				IsBone = true,
				A = body.NeckPos,
				B = v3,
				R = 0.022f
			});
			body.Hard.Add(new Anatomy.Capsule
			{
				Key = "spine_t",
				Name = "COLUMNA SECCIONADA (ESPALDA ALTA)",
				Severity = Severity.Critical,
				IsBone = true,
				A = body.TorsoPoint(0.45f, 0f, -0.03f),
				B = body.TorsoPoint(1f, 0f, -0.03f),
				R = 0.028f
			});
			body.Hard.Add(new Anatomy.Capsule
			{
				Key = "spine_l",
				Name = "COLUMNA SECCIONADA (ESPALDA BAJA)",
				Severity = Severity.Critical,
				IsBone = true,
				A = body.TorsoPoint(0.05f, 0f, -0.04f),
				B = body.TorsoPoint(0.45f, 0f, -0.04f),
				R = 0.03f
			});
			List<Anatomy.Shape> hard = body.Hard;
			Anatomy.TorsoShell torsoShell = new Anatomy.TorsoShell();
			torsoShell.Key = "ribs_l";
			torsoShell.Name = "COSTILLAS IZQUIERDAS DESTROZADAS";
			torsoShell.Severity = Severity.Moderate;
			torsoShell.IsBone = true;
			torsoShell.Body = body;
			torsoShell.FracMin = 0.42f;
			torsoShell.FracMax = 0.9f;
			torsoShell.RadialMin = 0.085f;
			torsoShell.RadialMax = 0.13f;
			torsoShell.SideTest = ((float r, float f) => r < -0.025f);
			hard.Add(torsoShell);
			List<Anatomy.Shape> hard2 = body.Hard;
			Anatomy.TorsoShell torsoShell2 = new Anatomy.TorsoShell();
			torsoShell2.Key = "ribs_r";
			torsoShell2.Name = "COSTILLAS DERECHAS DESTROZADAS";
			torsoShell2.Severity = Severity.Moderate;
			torsoShell2.IsBone = true;
			torsoShell2.Body = body;
			torsoShell2.FracMin = 0.42f;
			torsoShell2.FracMax = 0.9f;
			torsoShell2.RadialMin = 0.085f;
			torsoShell2.RadialMax = 0.13f;
			torsoShell2.SideTest = ((float r, float f) => r > 0.025f);
			hard2.Add(torsoShell2);
			List<Anatomy.Shape> hard3 = body.Hard;
			Anatomy.TorsoShell torsoShell3 = new Anatomy.TorsoShell();
			torsoShell3.Key = "sternum";
			torsoShell3.Name = "ESTERNÓN FISURADO";
			torsoShell3.Severity = Severity.Moderate;
			torsoShell3.IsBone = true;
			torsoShell3.Body = body;
			torsoShell3.FracMin = 0.55f;
			torsoShell3.FracMax = 0.9f;
			torsoShell3.RadialMin = 0.07f;
			torsoShell3.RadialMax = 0.13f;
			torsoShell3.SideTest = ((float r, float f) => Math.Abs(r) <= 0.03f && f > 0f);
			hard3.Add(torsoShell3);
			List<Anatomy.Shape> hard4 = body.Hard;
			Anatomy.TorsoShell torsoShell4 = new Anatomy.TorsoShell();
			torsoShell4.Key = "scapula_l";
			torsoShell4.Name = "OMÓPLATO IZQUIERDO FISURADO";
			torsoShell4.Severity = Severity.Moderate;
			torsoShell4.IsBone = true;
			torsoShell4.Body = body;
			torsoShell4.FracMin = 0.7f;
			torsoShell4.FracMax = 0.95f;
			torsoShell4.RadialMin = 0.07f;
			torsoShell4.RadialMax = 0.14f;
			torsoShell4.SideTest = ((float r, float f) => f < -0.04f && r < -0.04f);
			hard4.Add(torsoShell4);
			List<Anatomy.Shape> hard5 = body.Hard;
			Anatomy.TorsoShell torsoShell5 = new Anatomy.TorsoShell();
			torsoShell5.Key = "scapula_r";
			torsoShell5.Name = "OMÓPLATO DERECHO FISURADO";
			torsoShell5.Severity = Severity.Moderate;
			torsoShell5.IsBone = true;
			torsoShell5.Body = body;
			torsoShell5.FracMin = 0.7f;
			torsoShell5.FracMax = 0.95f;
			torsoShell5.RadialMin = 0.07f;
			torsoShell5.RadialMax = 0.14f;
			torsoShell5.SideTest = ((float r, float f) => f < -0.04f && r > 0.04f);
			hard5.Add(torsoShell5);
			body.Hard.Add(new Anatomy.Sphere
			{
				Key = "pelvis",
				Name = "PELVIS DESTROZADA",
				Severity = Severity.Severe,
				IsBone = true,
				C = body.PelvisPos - body.Up * 0.02f,
				R = 0.11f
			});
			Anatomy.Bone(body, 30226, 37873, "clavicle_l", "CLAVÍCULA IZQUIERDA PARTIDA", Severity.Moderate, 0.02f);
			Anatomy.Bone(body, 54802, 46065, "clavicle_r", "CLAVÍCULA DERECHA PARTIDA", Severity.Moderate, 0.02f);
			Anatomy.Bone(body, 37873, 53675, "humerus_l", "HÚMERO IZQUIERDO ROTO", Severity.Moderate, 0.022f);
			Anatomy.Bone(body, 46065, 54187, "humerus_r", "HÚMERO DERECHO ROTO", Severity.Moderate, 0.022f);
			Anatomy.Bone(body, 53675, 34606, "forearm_l", "HUESOS DEL ANTEBRAZO IZQUIERDO ROTOS", Severity.Moderate, 0.022f);
			Anatomy.Bone(body, 54187, 22798, "forearm_r", "HUESOS DEL ANTEBRAZO DERECHO ROTOS", Severity.Moderate, 0.022f);
			Anatomy.Ball(body, 55120, "knee_l", "RODILLA IZQUIERDA DESTROZADA", Severity.Severe, 0.042f);
			Anatomy.Ball(body, 43312, "knee_r", "RODILLA DERECHA DESTROZADA", Severity.Severe, 0.042f);
			Anatomy.Bone(body, 65478, 55120, "femur_l", "FÉMUR IZQUIERDO DESTROZADO", Severity.Severe, 0.03f);
			Anatomy.Bone(body, 6884, 43312, "femur_r", "FÉMUR DERECHO DESTROZADO", Severity.Severe, 0.03f);
			Anatomy.Shin(body, 55120, 45454, "tibia_l", "TIBIA IZQUIERDA ROTA");
			Anatomy.Shin(body, 43312, 33646, "tibia_r", "TIBIA DERECHA ROTA");
			Anatomy.Ball(body, 34606, "hand_l", "MANO IZQUIERDA DESTROZADA", Severity.Minor, 0.04f);
			Anatomy.Ball(body, 22798, "hand_r", "MANO DERECHA DESTROZADA", Severity.Minor, 0.04f);
			Anatomy.Ball(body, 45454, "foot_l", "PIE IZQUIERDO DESTROZADO", Severity.Minor, 0.04f);
			Anatomy.Ball(body, 33646, "foot_r", "PIE DERECHO DESTROZADO", Severity.Minor, 0.04f);
			V3 a5 = V3.Lerp(body.NeckPos, v3, 0.5f);
			Anatomy.Organ(body, "brain", "CEREBRO DESTRUIDO", Severity.Fatal, body.HeadCenter, 0.075f);
			Anatomy.Organ(body, "carotid_l", "ARTERIA CARÓTIDA IZQUIERDA SECCIONADA", Severity.Critical, a5 - body.Right * 0.03f + body.Fwd * 0.015f, 0.02f);
			Anatomy.Organ(body, "carotid_r", "ARTERIA CARÓTIDA DERECHA SECCIONADA", Severity.Critical, a5 + body.Right * 0.03f + body.Fwd * 0.015f, 0.02f);
			Anatomy.Organ(body, "windpipe", "TRÁQUEA PERFORADA", Severity.Severe, a5 + body.Fwd * 0.035f, 0.016f);
			Anatomy.Organ(body, "heart", "CORAZÓN PERFORADO", Severity.Fatal, body.TorsoPoint(0.72f, -0.03f, 0.05f), 0.055f);
			Anatomy.Organ(body, "lung_l", "PULMÓN IZQUIERDO PERFORADO", Severity.Critical, body.TorsoPoint(0.7f, -0.075f, 0f), 0.07f);
			Anatomy.Organ(body, "lung_l", "PULMÓN IZQUIERDO PERFORADO", Severity.Critical, body.TorsoPoint(0.83f, -0.07f, 0f), 0.06f);
			Anatomy.Organ(body, "lung_r", "PULMÓN DERECHO PERFORADO", Severity.Critical, body.TorsoPoint(0.7f, 0.075f, 0f), 0.07f);
			Anatomy.Organ(body, "lung_r", "PULMÓN DERECHO PERFORADO", Severity.Critical, body.TorsoPoint(0.83f, 0.07f, 0f), 0.06f);
			Anatomy.Organ(body, "liver", "HÍGADO ROTO", Severity.Severe, body.TorsoPoint(0.5f, 0.06f, 0.03f), 0.07f);
			Anatomy.Organ(body, "stomach", "ESTÓMAGO PERFORADO", Severity.Severe, body.TorsoPoint(0.5f, -0.05f, 0.05f), 0.055f);
			Anatomy.Organ(body, "kidney_l", "RIÑÓN IZQUIERDO DESTRUIDO", Severity.Severe, body.TorsoPoint(0.42f, -0.05f, -0.06f), 0.035f);
			Anatomy.Organ(body, "kidney_r", "RIÑÓN DERECHO DESTRUIDO", Severity.Severe, body.TorsoPoint(0.42f, 0.05f, -0.06f), 0.035f);
			Anatomy.Organ(body, "intestines", "INTESTINOS DESGARRADOS", Severity.Severe, body.TorsoPoint(0.25f, 0f, 0.05f), 0.09f);
			Anatomy.Organ(body, "bladder", "VEJIGA ROTA", Severity.Moderate, body.TorsoPoint(0.04f, 0f, 0.06f), 0.04f);
			if (body.Has(65478) && body.Has(55120))
			{
				body.Hard.Add(new Anatomy.Capsule
				{
					Key = "femoral_l",
					Name = "ARTERIA FEMORAL IZQUIERDA SECCIONADA",
					Severity = Severity.Critical,
					A = body.Get(65478) + body.Fwd * 0.02f,
					B = V3.Lerp(body.Get(65478), body.Get(55120), 0.5f) + body.Fwd * 0.02f,
					R = 0.022f
				});
			}
			if (body.Has(6884) && body.Has(43312))
			{
				body.Hard.Add(new Anatomy.Capsule
				{
					Key = "femoral_r",
					Name = "ARTERIA FEMORAL DERECHA SECCIONADA",
					Severity = Severity.Critical,
					A = body.Get(6884) + body.Fwd * 0.02f,
					B = V3.Lerp(body.Get(6884), body.Get(43312), 0.5f) + body.Fwd * 0.02f,
					R = 0.022f
				});
			}
			return body;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x0013DAA6 File Offset: 0x0013BCA6
		private static void AddSoft(Anatomy.Body b, string region, Anatomy.Shape s)
		{
			b.Soft.Add(new Anatomy.SoftPart
			{
				Shape = s,
				Region = region
			});
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x0013DAC8 File Offset: 0x0013BCC8
		private static void Limb(Anatomy.Body b, string side, int clav, int upper, int lower, int end, string clavName, string upperName, string lowerName, string endName, float rClav, float rUpper, float rLower, float rEnd)
		{
			if (clav >= 0 && b.Has(clav) && b.Has(upper))
			{
				Anatomy.AddSoft(b, "the " + side + " " + clavName, new Anatomy.Capsule
				{
					A = b.Get(clav),
					B = b.Get(upper),
					R = rClav
				});
			}
			if (b.Has(upper) && b.Has(lower))
			{
				Anatomy.AddSoft(b, "the " + side + " " + upperName, new Anatomy.Capsule
				{
					A = b.Get(upper),
					B = b.Get(lower),
					R = rUpper
				});
			}
			if (b.Has(lower) && b.Has(end))
			{
				Anatomy.AddSoft(b, "the " + side + " " + lowerName, new Anatomy.Capsule
				{
					A = b.Get(lower),
					B = b.Get(end),
					R = rLower
				});
			}
			if (b.Has(end))
			{
				Anatomy.AddSoft(b, "the " + side + " " + endName, new Anatomy.Sphere
				{
					C = b.Get(end),
					R = rEnd
				});
			}
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x0013DC10 File Offset: 0x0013BE10
		private static void Bone(Anatomy.Body b, int a, int c, string key, string name, Severity sev, float r)
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

		// Token: 0x060013E3 RID: 5091 RVA: 0x0013DC80 File Offset: 0x0013BE80
		private static void Shin(Anatomy.Body b, int calf, int foot, string key, string name)
		{
			if (b.Has(calf) && b.Has(foot))
			{
				b.Hard.Add(new Anatomy.Capsule
				{
					Key = key,
					Name = name,
					Severity = Severity.Moderate,
					IsBone = true,
					A = b.Get(calf),
					B = V3.Lerp(b.Get(calf), b.Get(foot), 0.82f),
					R = 0.025f
				});
			}
		}

		// Token: 0x060013E4 RID: 5092 RVA: 0x0013DD04 File Offset: 0x0013BF04
		private static void Ball(Anatomy.Body b, int id, string key, string name, Severity sev, float r)
		{
			if (b.Has(id))
			{
				b.Hard.Add(new Anatomy.Sphere
				{
					Key = key,
					Name = name,
					Severity = sev,
					IsBone = true,
					C = b.Get(id),
					R = r
				});
			}
		}

		// Token: 0x060013E5 RID: 5093 RVA: 0x0013DD5C File Offset: 0x0013BF5C
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

		// Token: 0x060013E6 RID: 5094 RVA: 0x0013DD94 File Offset: 0x0013BF94
		public static float Budget(WeaponClass w)
		{
			switch (w)
			{
			case WeaponClass.Pistol:
				return 0.29f;
			case WeaponClass.Revolver:
				return 0.32f;
			case WeaponClass.Repeater:
				return 0.36f;
			case WeaponClass.Rifle:
				return 0.75f;
			case WeaponClass.Sniper:
				return 1.2f;
			case WeaponClass.Shotgun:
				return 0.1f;
			case WeaponClass.Stab:
				return 0.17f;
			case WeaponClass.Slash:
				return 0.1f;
			case WeaponClass.Chop:
				return 0.12f;
			case WeaponClass.Blast:
				return 0.07f;
			case WeaponClass.Burn:
				return 0.012f;
			}
			return 0.26f;
		}

		// Token: 0x060013E7 RID: 5095 RVA: 0x0013DE20 File Offset: 0x0013C020
		public static float CordMissChance(WeaponClass w)
		{
			switch (w)
			{
			case WeaponClass.Pistol:
				return 0.6f;
			case WeaponClass.Revolver:
				return 0.55f;
			case WeaponClass.Repeater:
				return 0.35f;
			case WeaponClass.Rifle:
				return 0.1f;
			case WeaponClass.Sniper:
				return 0f;
			case WeaponClass.Shotgun:
				return 0.7f;
			case WeaponClass.Chop:
				return 0.4f;
			}
			return 0.5f;
		}

		// Token: 0x060013E8 RID: 5096 RVA: 0x0013DE8C File Offset: 0x0013C08C
		private static float Resistance(Anatomy.Shape s)
		{
			if (!s.IsBone)
			{
				return 0f;
			}
			string key = s.Key;
			if (key != null)
			{
				switch (key.Length)
				{
				case 5:
					if (!(key == "skull"))
					{
						goto IL_19B;
					}
					return 0.1f;
				case 6:
				{
					char c = key[5];
					if (c != 'l')
					{
						if (c != 'r')
						{
							if (c != 's')
							{
								goto IL_19B;
							}
							if (!(key == "pelvis"))
							{
								goto IL_19B;
							}
						}
						else
						{
							if (!(key == "ribs_r"))
							{
								goto IL_19B;
							}
							goto IL_195;
						}
					}
					else
					{
						if (!(key == "ribs_l"))
						{
							goto IL_19B;
						}
						goto IL_195;
					}
					break;
				}
				case 7:
				{
					char c = key[6];
					if (c <= 'm')
					{
						if (c != 'l')
						{
							if (c != 'm')
							{
								goto IL_19B;
							}
							if (!(key == "sternum"))
							{
								goto IL_19B;
							}
							goto IL_195;
						}
						else if (!(key == "femur_l"))
						{
							if (!(key == "spine_l"))
							{
								goto IL_19B;
							}
							goto IL_18F;
						}
					}
					else if (c != 'r')
					{
						if (c != 't')
						{
							goto IL_19B;
						}
						if (!(key == "spine_t"))
						{
							goto IL_19B;
						}
						goto IL_18F;
					}
					else if (!(key == "femur_r"))
					{
						goto IL_19B;
					}
					break;
				}
				case 8:
					if (!(key == "cervical"))
					{
						goto IL_19B;
					}
					goto IL_18F;
				case 9:
				{
					char c = key[8];
					if (c != 'l')
					{
						if (c != 'r')
						{
							goto IL_19B;
						}
						if (!(key == "scapula_r"))
						{
							goto IL_19B;
						}
						goto IL_195;
					}
					else
					{
						if (!(key == "scapula_l"))
						{
							goto IL_19B;
						}
						goto IL_195;
					}
					break;
				}
				default:
					goto IL_19B;
				}
				return 0.12f;
				IL_18F:
				return 0.12f;
				IL_195:
				return 0.05f;
			}
			IL_19B:
			return 0.06f;
		}

		// Token: 0x060013E9 RID: 5097 RVA: 0x0013E03C File Offset: 0x0013C23C
		public static PathResult Trace(Anatomy.Body body, V3 origin, V3 dir, WeaponClass weapon, float maxDistance = 25f, AmmoKind ammo = AmmoKind.Regular, float budgetScale = 1f)
		{
			dir = dir.Normalized();
			ValueTuple<float, float, float> valueTuple = Ammo.Mods(ammo);
			float num = Anatomy.Budget(weapon) * valueTuple.Item1 * budgetScale * body.BudgetScale;
			PathResult pathResult = new PathResult();
			bool flag = false;
			V3 v = origin;
			float num2 = 0f;
			for (float num3 = 0f; num3 <= maxDistance; num3 += 0.005f)
			{
				V3 p = origin + dir * num3;
				Anatomy.SoftPart softPart = body.Soft.FirstOrDefault((Anatomy.SoftPart s) => s.Shape.Contains(p));
				bool flag2 = softPart != null;
				if (flag2 && !pathResult.HasEntry)
				{
					pathResult.HasEntry = true;
					pathResult.Entry = p;
					pathResult.Start = p - dir * 0.6f;
					pathResult.EntryText = Anatomy.Describe(body, p, softPart.Region, true);
				}
				if (flag2)
				{
					v = p;
					num -= 0.005f;
					foreach (Anatomy.Shape shape in body.Hard.Concat(body.Organs))
					{
						if (!pathResult.HitKeys.Contains(shape.Key) && shape.Contains(p))
						{
							pathResult.HitKeys.Add(shape.Key);
							float num4 = Anatomy.Resistance(shape) * (Anatomy.IsBlade(weapon) ? 1f : valueTuple.Item2);
							if (Anatomy.IsBlade(weapon) && shape.IsBone)
							{
								bool flag3 = shape.Key.StartsWith("ribs") || shape.Key == "sternum" || shape.Key.StartsWith("scapula");
								if (weapon == WeaponClass.Stab && flag3)
								{
									pathResult.Damage.Add(new DamageItem
									{
										Key = shape.Key,
										Name = Anatomy.NickName(shape.Key, shape.Name),
										Severity = Severity.Minor,
										IsBone = true,
										Chipped = true
									});
									num -= 0.02f;
									continue;
								}
								if (weapon != WeaponClass.Chop)
								{
									pathResult.Damage.Add(new DamageItem
									{
										Key = shape.Key,
										Name = Anatomy.NickName(shape.Key, shape.Name),
										Severity = Severity.Moderate,
										IsBone = true,
										Chipped = true
									});
									num = 0f;
									break;
								}
								num4 *= 0.6f;
							}
							bool flag4 = shape.Key == "spine_t" || shape.Key == "spine_l" || shape.Key == "cervical";
							bool flag5 = shape.IsBone && num < num4;
							bool flag6 = flag4 && !flag5 && Anatomy.Roll(p) < Anatomy.CordMissChance(weapon) * valueTuple.Item3;
							if (flag5 || flag6)
							{
								pathResult.Damage.Add(new DamageItem
								{
									Key = shape.Key,
									Name = Anatomy.ChipName(shape.Key, shape.Name, flag6),
									Severity = Severity.Moderate,
									IsBone = true,
									Chipped = true
								});
								if (flag5)
								{
									num = 0f;
									break;
								}
								num -= num4;
							}
							else
							{
								pathResult.Damage.Add(new DamageItem
								{
									Key = shape.Key,
									Name = shape.Name,
									Severity = shape.Severity,
									IsBone = shape.IsBone
								});
								num -= num4;
							}
						}
					}
					if (num <= 0f)
					{
						pathResult.End = p;
						pathResult.Exited = false;
						pathResult.EndText = (Anatomy.IsBlade(weapon) ? "Hoja detenida en " : "Alojada en ") + Anatomy.Describe(body, p, softPart.Region, false);
						return Anatomy.Finish(pathResult);
					}
				}
				else if (flag && pathResult.HasEntry)
				{
					pathResult.End = v;
					pathResult.Exited = true;
					pathResult.EndText = "Salió por " + Anatomy.Describe(body, v, Anatomy.RegionAt(body, v), false);
				}
				if (pathResult.HasEntry && !flag2)
				{
					num2 += 0.005f;
				}
				if (pathResult.Exited && num2 > 0.6f)
				{
					break;
				}
				flag = flag2;
			}
			if (!pathResult.HasEntry)
			{
				return null;
			}
			return Anatomy.Finish(pathResult);
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x0013E514 File Offset: 0x0013C714
		private static float Roll(V3 p)
		{
			int num = (int)(p.X * 1000f) * 73856093 ^ (int)(p.Y * 1000f) * 19349663 ^ (int)(p.Z * 1000f) * 83492791;
			int num2 = (num ^ num >> 13) * 1540483477;
			return (float)(((num2 ^ num2 >> 15) & int.MaxValue) % 1000) / 1000f;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x0013E57F File Offset: 0x0013C77F
		public static bool IsBlade(WeaponClass w)
		{
			return w == WeaponClass.Stab || w == WeaponClass.Slash || w == WeaponClass.Chop;
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x0013E590 File Offset: 0x0013C790
		public static bool IsArea(WeaponClass w)
		{
			return w == WeaponClass.Blast || w == WeaponClass.Burn;
		}

		// Token: 0x060013ED RID: 5101 RVA: 0x0013E59E File Offset: 0x0013C79E
		public static bool IsBlunt(WeaponClass w)
		{
			return w == WeaponClass.Blunt;
		}

		// Token: 0x060013EE RID: 5102 RVA: 0x0013E5A5 File Offset: 0x0013C7A5
		public static string RegionOf(Anatomy.Body b, V3 p)
		{
			return Anatomy.RegionAt(b, p);
		}

		// Token: 0x060013EF RID: 5103 RVA: 0x0013E5B0 File Offset: 0x0013C7B0
		private static string NickName(string key, string name)
		{
			if (key == "spine_t")
			{
				return "VERTEBRA NICKED (UPPER BACK) - CORD INTACT";
			}
			if (key == "spine_l")
			{
				return "VERTEBRA NICKED (LOWER BACK) - CORD INTACT";
			}
			if (!(key == "cervical"))
			{
				int num = name.LastIndexOf(' ');
				return ((num > 0) ? name.Substring(0, num) : name) + " NICKED";
			}
			return "NECK VERTEBRA NICKED - CORD INTACT";
		}

		// Token: 0x060013F0 RID: 5104 RVA: 0x0013E61C File Offset: 0x0013C81C
		private static string ChipName(string key, string name, bool deflected)
		{
			if (!(key == "spine_t"))
			{
				if (!(key == "spine_l"))
				{
					if (!(key == "cervical"))
					{
						int num = name.LastIndexOf(' ');
						return ((num > 0) ? name.Substring(0, num) : name) + " CHIPPED";
					}
					return "NECK VERTEBRA CHIPPED - CORD INTACT";
				}
				else
				{
					if (!deflected)
					{
						return "BULLET LODGED IN SPINE (LOWER BACK) - CORD INTACT";
					}
					return "VERTEBRA CHIPPED (LOWER BACK) - CORD INTACT";
				}
			}
			else
			{
				if (!deflected)
				{
					return "BULLET LODGED IN SPINE (UPPER BACK) - CORD INTACT";
				}
				return "VERTEBRA CHIPPED (UPPER BACK) - CORD INTACT";
			}
		}

		// Token: 0x060013F1 RID: 5105 RVA: 0x0013E698 File Offset: 0x0013C898
		private static PathResult Finish(PathResult r)
		{
			if (r.Damage.Count == 0)
			{
				r.Damage.Add(new DamageItem
				{
					Key = "flesh",
					Name = "HERIDA SUPERFICIAL",
					Severity = Severity.Minor
				});
			}
			r.Damage.Sort((DamageItem a, DamageItem b) => b.Severity.CompareTo(a.Severity));
			return r;
		}

		// Token: 0x060013F2 RID: 5106 RVA: 0x0013E70C File Offset: 0x0013C90C
		private static string RegionAt(Anatomy.Body b, V3 p)
		{
			return (from s in b.Soft
			where s.Shape.Contains(p)
			select s.Region).FirstOrDefault<string>() ?? (from s in b.Soft
			orderby Anatomy.Dist(s.Shape, p)
			select s).First<Anatomy.SoftPart>().Region;
		}

		// Token: 0x060013F3 RID: 5107 RVA: 0x0013E78C File Offset: 0x0013C98C
		private static float Dist(Anatomy.Shape s, V3 p)
		{
			Anatomy.Capsule capsule = s as Anatomy.Capsule;
			if (capsule != null)
			{
				return Anatomy.DistToSegment(p, capsule.A, capsule.B) - capsule.R;
			}
			Anatomy.Sphere sphere = s as Anatomy.Sphere;
			if (sphere == null)
			{
				return 1000f;
			}
			return (p - sphere.C).Length() - sphere.R;
		}

		// Token: 0x060013F4 RID: 5108 RVA: 0x0013E7E8 File Offset: 0x0013C9E8
		private static string Describe(Anatomy.Body b, V3 p, string region, bool entering)
		{
			if (b.Animal)
			{
				return AnimalAnatomy.Describe(b, p, region);
			}
			if (!(region != "torso"))
			{
				float num;
				float num2;
				float num3;
				b.TorsoCoords(p, out num, out num2, out num3);
				string str = (num > 0.55f) ? "chest" : ((num > 0.18f) ? "stomach" : "cadera");
				if (num3 < -0.03f)
				{
					str = ((num > 0.55f) ? "parte alta de la espalda" : ((num > 0.18f) ? "parte baja de la espalda" : "nalgas"));
				}
				string str2 = (num2 < -0.04f) ? ", lado izquierdo" : ((num2 > 0.04f) ? ", lado derecho" : "");
				return ((num3 < -0.03f) ? "the " : "la parte frontal de ") + str + str2;
			}
			if (!(region == "la cabeza"))
			{
				return region;
			}
			float num4;
			float num5;
			float num6;
			b.TorsoCoords(p, out num4, out num5, out num6);
			if (num6 > 0.03f)
			{
				return "la cara";
			}
			if (num6 >= -0.03f)
			{
				return "el costado de la cabeza";
			}
			return "la nuca";
		}

		// Token: 0x060013F5 RID: 5109 RVA: 0x0013E8F4 File Offset: 0x0013CAF4
		public static float SegmentDistance(V3 p0, V3 p1, V3 q0, V3 q1, out V3 onP)
		{
			V3 v = p1 - p0;
			V3 v2 = q1 - q0;
			V3 b = p0 - q0;
			float num = V3.Dot(v, v);
			float num2 = V3.Dot(v2, v2);
			float num3 = V3.Dot(v2, b);
			if (num < 1E-09f && num2 < 1E-09f)
			{
				onP = p0;
				return b.Length();
			}
			float num4;
			float num5;
			if (num < 1E-09f)
			{
				num4 = 0f;
				num5 = Anatomy.Clamp01(num3 / num2);
			}
			else
			{
				float num6 = V3.Dot(v, b);
				if (num2 < 1E-09f)
				{
					num5 = 0f;
					num4 = Anatomy.Clamp01(-num6 / num);
				}
				else
				{
					float num7 = V3.Dot(v, v2);
					float num8 = num * num2 - num7 * num7;
					num4 = ((num8 > 1E-09f) ? Anatomy.Clamp01((num7 * num3 - num6 * num2) / num8) : 0f);
					num5 = (num7 * num4 + num3) / num2;
					if (num5 < 0f)
					{
						num5 = 0f;
						num4 = Anatomy.Clamp01(-num6 / num);
					}
					else if (num5 > 1f)
					{
						num5 = 1f;
						num4 = Anatomy.Clamp01((num7 - num6) / num);
					}
				}
			}
			onP = p0 + v * num4;
			return (onP - (q0 + v2 * num5)).Length();
		}

		// Token: 0x060013F6 RID: 5110 RVA: 0x0013EA59 File Offset: 0x0013CC59
		private static float Clamp01(float v)
		{
			if (v < 0f)
			{
				return 0f;
			}
			if (v <= 1f)
			{
				return v;
			}
			return 1f;
		}

		// Token: 0x060013F7 RID: 5111 RVA: 0x0013EA78 File Offset: 0x0013CC78
		public static float DistToSegment(V3 p, V3 a, V3 b)
		{
			V3 v = b - a;
			float num = V3.Dot(v, v);
			float s = (num < 1E-08f) ? 0f : Math.Max(0f, Math.Min(1f, V3.Dot(p - a, v) / num));
			return (p - (a + v * s)).Length();
		}

		// Token: 0x060013F8 RID: 5112 RVA: 0x0013EAE4 File Offset: 0x0013CCE4
		private static V3 Average(IEnumerable<V3> pts)
		{
			List<V3> list = pts.ToList<V3>();
			if (list.Count == 0)
			{
				return new V3(0f, 0f, 1f);
			}
			V3 a = new V3(0f, 0f, 0f);
			foreach (V3 b in list)
			{
				a += b;
			}
			return a * (1f / (float)list.Count);
		}

		// Token: 0x04000D91 RID: 3473
		public const int Pelvis = 56200;

		// Token: 0x04000D92 RID: 3474
		public const int Spine0 = 14410;

		// Token: 0x04000D93 RID: 3475
		public const int Spine1 = 14411;

		// Token: 0x04000D94 RID: 3476
		public const int Spine2 = 14412;

		// Token: 0x04000D95 RID: 3477
		public const int Spine3 = 14413;

		// Token: 0x04000D96 RID: 3478
		public const int Spine4 = 14414;

		// Token: 0x04000D97 RID: 3479
		public const int Spine5 = 14415;

		// Token: 0x04000D98 RID: 3480
		public const int Neck0 = 14283;

		// Token: 0x04000D99 RID: 3481
		public const int Neck1 = 14284;

		// Token: 0x04000D9A RID: 3482
		public const int Neck2 = 14285;

		// Token: 0x04000D9B RID: 3483
		public const int Neck3 = 14286;

		// Token: 0x04000D9C RID: 3484
		public const int Neck4 = 14287;

		// Token: 0x04000D9D RID: 3485
		public const int Neck5 = 14288;

		// Token: 0x04000D9E RID: 3486
		public const int Head = 21030;

		// Token: 0x04000D9F RID: 3487
		public const int LClavicle = 30226;

		// Token: 0x04000DA0 RID: 3488
		public const int LUpperArm = 37873;

		// Token: 0x04000DA1 RID: 3489
		public const int LForearm = 53675;

		// Token: 0x04000DA2 RID: 3490
		public const int LHand = 34606;

		// Token: 0x04000DA3 RID: 3491
		public const int RClavicle = 54802;

		// Token: 0x04000DA4 RID: 3492
		public const int RUpperArm = 46065;

		// Token: 0x04000DA5 RID: 3493
		public const int RForearm = 54187;

		// Token: 0x04000DA6 RID: 3494
		public const int RHand = 22798;

		// Token: 0x04000DA7 RID: 3495
		public const int LThigh = 65478;

		// Token: 0x04000DA8 RID: 3496
		public const int LCalf = 55120;

		// Token: 0x04000DA9 RID: 3497
		public const int LFoot = 45454;

		// Token: 0x04000DAA RID: 3498
		public const int LToe = 53081;

		// Token: 0x04000DAB RID: 3499
		public const int RThigh = 6884;

		// Token: 0x04000DAC RID: 3500
		public const int RCalf = 43312;

		// Token: 0x04000DAD RID: 3501
		public const int RFoot = 33646;

		// Token: 0x04000DAE RID: 3502
		public const int RToe = 41273;

		// Token: 0x04000DAF RID: 3503
		public static readonly int[] SpineChain = new int[]
		{
			56200,
			14410,
			14411,
			14412,
			14413,
			14414,
			14415,
			14283,
			14284,
			14285,
			14286,
			14287,
			14288,
			21030
		};

		// Token: 0x04000DB0 RID: 3504
		public static readonly int[][] LimbChains = new int[][]
		{
			new int[]
			{
				30226,
				37873,
				53675,
				34606
			},
			new int[]
			{
				54802,
				46065,
				54187,
				22798
			},
			new int[]
			{
				56200,
				65478,
				55120,
				45454,
				53081
			},
			new int[]
			{
				56200,
				6884,
				43312,
				33646,
				41273
			}
		};

		// Token: 0x04000DB1 RID: 3505
		public const int Jaw = 58559;

		// Token: 0x04000DB2 RID: 3506
		public const int LToe1 = 53082;

		// Token: 0x04000DB3 RID: 3507
		public const int RToe1 = 41274;

		// Token: 0x04000DB4 RID: 3508
		public static readonly int[][] LFingers = new int[][]
		{
			new int[]
			{
				41403,
				41404,
				41405
			},
			new int[]
			{
				41323,
				41324,
				41325,
				41326
			},
			new int[]
			{
				41307,
				41308,
				41309,
				41310
			},
			new int[]
			{
				41355,
				41356,
				41357,
				41358
			},
			new int[]
			{
				41339,
				41340,
				41341,
				41342
			}
		};

		// Token: 0x04000DB5 RID: 3509
		public static readonly int[][] RFingers = new int[][]
		{
			new int[]
			{
				16827,
				16828,
				16829
			},
			new int[]
			{
				16747,
				16748,
				16749,
				16750
			},
			new int[]
			{
				16731,
				16732,
				16733,
				16734
			},
			new int[]
			{
				16779,
				16780,
				16781,
				16782
			},
			new int[]
			{
				16763,
				16764,
				16765,
				16766
			}
		};

		// Token: 0x04000DB6 RID: 3510
		public static readonly int[] AllBones = Anatomy.SpineChain.Concat(Anatomy.LimbChains.SelectMany((int[] c) => c)).Concat(Anatomy.LFingers.SelectMany((int[] f) => f)).Concat(Anatomy.RFingers.SelectMany((int[] f) => f)).Concat(new int[]
		{
			58559,
			53082,
			41274
		}).Distinct<int>().ToArray<int>();

		// Token: 0x02000480 RID: 1152
		public abstract class Shape
		{
			// Token: 0x060025FD RID: 9725
			public abstract bool Contains(V3 p);

			// Token: 0x0400204B RID: 8267
			public string Key;

			// Token: 0x0400204C RID: 8268
			public string Name;

			// Token: 0x0400204D RID: 8269
			public Severity Severity;

			// Token: 0x0400204E RID: 8270
			public bool IsBone;
		}

		// Token: 0x02000481 RID: 1153
		public sealed class Capsule : Anatomy.Shape
		{
			// Token: 0x060025FF RID: 9727 RVA: 0x001824F6 File Offset: 0x001806F6
			public override bool Contains(V3 p)
			{
				return Anatomy.DistToSegment(p, this.A, this.B) <= this.R;
			}

			// Token: 0x0400204F RID: 8271
			public V3 A;

			// Token: 0x04002050 RID: 8272
			public V3 B;

			// Token: 0x04002051 RID: 8273
			public float R;
		}

		// Token: 0x02000482 RID: 1154
		public sealed class Sphere : Anatomy.Shape
		{
			// Token: 0x06002601 RID: 9729 RVA: 0x00182520 File Offset: 0x00180720
			public override bool Contains(V3 p)
			{
				return (p - this.C).Length() <= this.R;
			}

			// Token: 0x04002052 RID: 8274
			public V3 C;

			// Token: 0x04002053 RID: 8275
			public float R;
		}

		// Token: 0x02000483 RID: 1155
		public sealed class TorsoShell : Anatomy.Shape
		{
			// Token: 0x06002603 RID: 9731 RVA: 0x00182554 File Offset: 0x00180754
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
				float num4 = (float)Math.Sqrt((double)(num2 / 1.35f * (num2 / 1.35f) + num3 * num3));
				return num4 >= this.RadialMin && num4 <= this.RadialMax && this.SideTest(num2, num3);
			}

			// Token: 0x04002054 RID: 8276
			public Anatomy.Body Body;

			// Token: 0x04002055 RID: 8277
			public float FracMin;

			// Token: 0x04002056 RID: 8278
			public float FracMax;

			// Token: 0x04002057 RID: 8279
			public float RadialMin;

			// Token: 0x04002058 RID: 8280
			public float RadialMax;

			// Token: 0x04002059 RID: 8281
			public Func<float, float, bool> SideTest;
		}

		// Token: 0x02000484 RID: 1156
		public sealed class SoftPart
		{
			// Token: 0x0400205A RID: 8282
			public Anatomy.Shape Shape;

			// Token: 0x0400205B RID: 8283
			public string Region;
		}

		// Token: 0x02000485 RID: 1157
		public sealed class Body
		{
			// Token: 0x06002606 RID: 9734 RVA: 0x001825D4 File Offset: 0x001807D4
			public Body(Dictionary<int, V3> bones)
			{
				this.Bones = bones;
			}

			// Token: 0x06002607 RID: 9735 RVA: 0x00182625 File Offset: 0x00180825
			public bool Has(int id)
			{
				return this.Bones.ContainsKey(id);
			}

			// Token: 0x06002608 RID: 9736 RVA: 0x00182633 File Offset: 0x00180833
			public V3 Get(int id)
			{
				return this.Bones[id];
			}

			// Token: 0x06002609 RID: 9737 RVA: 0x00182644 File Offset: 0x00180844
			public V3 TorsoPoint(float frac, float right, float fwd)
			{
				return this.PelvisPos + this.Up * (frac * this.TorsoLength) + this.Right * right + this.Fwd * fwd;
			}

			// Token: 0x0600260A RID: 9738 RVA: 0x00182694 File Offset: 0x00180894
			public void TorsoCoords(V3 p, out float frac, out float right, out float fwd)
			{
				V3 a = p - this.PelvisPos;
				frac = V3.Dot(a, this.Up) / this.TorsoLength;
				right = V3.Dot(a, this.Right);
				fwd = V3.Dot(a, this.Fwd);
			}

			// Token: 0x0400205C RID: 8284
			public readonly Dictionary<int, V3> Bones;

			// Token: 0x0400205D RID: 8285
			public V3 PelvisPos;

			// Token: 0x0400205E RID: 8286
			public V3 NeckPos;

			// Token: 0x0400205F RID: 8287
			public V3 HeadCenter;

			// Token: 0x04002060 RID: 8288
			public V3 Up;

			// Token: 0x04002061 RID: 8289
			public V3 Fwd;

			// Token: 0x04002062 RID: 8290
			public V3 Right;

			// Token: 0x04002063 RID: 8291
			public float TorsoLength;

			// Token: 0x04002064 RID: 8292
			public readonly List<Anatomy.SoftPart> Soft = new List<Anatomy.SoftPart>();

			// Token: 0x04002065 RID: 8293
			public readonly List<Anatomy.Shape> Hard = new List<Anatomy.Shape>();

			// Token: 0x04002066 RID: 8294
			public readonly List<Anatomy.Sphere> Organs = new List<Anatomy.Sphere>();

			// Token: 0x04002067 RID: 8295
			public bool Animal;

			// Token: 0x04002068 RID: 8296
			public float Scale = 0.6f;

			// Token: 0x04002069 RID: 8297
			public float Depth;

			// Token: 0x0400206A RID: 8298
			public float HalfWidth;

			// Token: 0x0400206B RID: 8299
			public float BudgetScale = 1f;

			// Token: 0x0400206C RID: 8300
			public V3 Snout;

			// Token: 0x0400206D RID: 8301
			public V3 SnoutTip;
		}
	}
}
