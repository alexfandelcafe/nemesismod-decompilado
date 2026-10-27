using System;
using System.Collections.Generic;
using System.Linq;
using RDR2;
using RDR2.Math;
using RDR2.Native;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x02000092 RID: 146
	internal static class AnimalBones
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001404 RID: 5124 RVA: 0x00140700 File Offset: 0x0013E900
		private static string[] Names
		{
			get
			{
				if (AnimalBones._names != null)
				{
					return AnimalBones._names;
				}
				List<string> list = new List<string>
				{
					"SKEL_ROOT",
					"SKEL_Pelvis",
					"SKEL_Pelvis1",
					"SKEL_Spine_Root",
					"SKEL_Head",
					"SKEL_Jaw",
					"SKEL_Neck_Root",
					"SKEL_L_Ear",
					"SKEL_R_Ear",
					"SKEL_Nose",
					"SKEL_Tail_Root",
					"SKEL_Saddle",
					"SKEL_Spine",
					"SKEL_Neck",
					"SKEL_Tail"
				};
				for (int i = 0; i <= 10; i++)
				{
					list.Add("SKEL_Spine" + i.ToString());
				}
				for (int j = 0; j <= 8; j++)
				{
					list.Add("SKEL_Neck" + j.ToString());
				}
				for (int k = 0; k <= 14; k++)
				{
					list.Add("SKEL_Tail" + k.ToString());
					list.Add("SKEL_Tail_0" + k.ToString());
				}
				string[] array = new string[]
				{
					"Clavicle",
					"Scapula",
					"UpperArm",
					"Forearm",
					"Hand",
					"Finger00",
					"Finger01",
					"Finger02",
					"Finger10",
					"Finger11",
					"Thigh",
					"Calf",
					"Foot",
					"Toe0",
					"Toe1",
					"Toe2",
					"Toe00",
					"Toe01",
					"Humerus",
					"Radius",
					"Ulna",
					"Carpal",
					"Metacarpal",
					"Femur",
					"Tibia",
					"Fibula",
					"Tarsal",
					"Metatarsal",
					"Hoof",
					"FrontLeg",
					"HindLeg",
					"ForeLeg",
					"Shin",
					"Pastern",
					"Fetlock",
					"Cannon",
					"Hock",
					"Knee",
					"Elbow",
					"Wrist",
					"Ankle",
					"Paw",
					"Heel",
					"Wing",
					"Wing0",
					"Wing1",
					"Wing2",
					"Eye",
					"Horn",
					"Antler"
				};
				foreach (string str in new string[]
				{
					"L",
					"R"
				})
				{
					foreach (string str2 in array)
					{
						list.Add("SKEL_" + str + "_" + str2);
					}
				}
				foreach (string arg in new string[]
				{
					"L",
					"R"
				})
				{
					for (int n = 0; n <= 3; n++)
					{
						foreach (string arg2 in new string[]
						{
							"FrontLeg",
							"HindLeg",
							"Arm",
							"Leg"
						})
						{
							list.Add(string.Format("SKEL_{0}_{1}{2}", arg, arg2, n));
						}
					}
				}
				return AnimalBones._names = list.Distinct<string>().ToArray<string>();
			}
		}

		// Token: 0x06001405 RID: 5125 RVA: 0x00140B28 File Offset: 0x0013ED28
		private static string ModelName(Ped p)
		{
			uint hash = (uint)p.Model.Hash;
			if (!Enum.IsDefined(typeof(PedHash), hash))
			{
				return string.Format("0x{0:X8}", hash);
			}
			PedHash pedHash = hash;
			return pedHash.ToString();
		}

		// Token: 0x06001406 RID: 5126 RVA: 0x00140B7C File Offset: 0x0013ED7C
		private static Vector3 Local(Ped p, Vector3 world)
		{
			Vector3 position = p.Position;
			Vector3 vector = ENTITY.GET_ENTITY_FORWARD_VECTOR(p.Handle);
			vector..ctor(vector.X, vector.Y, 0f);
			if (vector.Length() < 0.01f)
			{
				vector..ctor(0f, 1f, 0f);
			}
			vector.Normalize();
			Vector3 vector2;
			vector2..ctor(vector.Y, -vector.X, 0f);
			Vector3 vector3 = world - position;
			return new Vector3(vector3.X * vector2.X + vector3.Y * vector2.Y, vector3.X * vector.X + vector3.Y * vector.Y, vector3.Z);
		}

		// Token: 0x06001407 RID: 5127 RVA: 0x00140C44 File Offset: 0x0013EE44
		internal unsafe static void Probe(Ped p, Action<string> log)
		{
			if (p == null || !p.Exists() || p.IsHuman || log == null)
			{
				return;
			}
			uint hash = (uint)p.Model.Hash;
			string text = AnimalBones.ModelName(p);
			try
			{
				if (AnimalBones._probed.Add(hash))
				{
					List<string> list = new List<string>();
					foreach (string text2 in AnimalBones.Names)
					{
						int num = ENTITY.GET_ENTITY_BONE_INDEX_BY_NAME(p.Handle, text2);
						if (num >= 0)
						{
							Vector3 vector = AnimalBones.Local(p, ENTITY.GET_WORLD_POSITION_OF_ENTITY_BONE(p.Handle, num));
							list.Add(string.Format("{0}#{1} ({2:0.00},{3:0.00},{4:0.00})", new object[]
							{
								text2.Replace("SKEL_", ""),
								num,
								vector.X,
								vector.Y,
								vector.Z
							}));
						}
					}
					log(string.Format("ANIMAL {0}{1}{2}", text, list.Count, AnimalBones.Names.Length));
					for (int j = 0; j < list.Count; j += 8)
					{
						log("ANIMAL BONES " + text + ":   " + string.Join("  ", list.Skip(j).Take(8)));
					}
					List<int> list2 = (from id in Anatomy.AllBones
					where PED.GET_PED_BONE_INDEX(p.Handle, id) >= 0
					select id).ToList<int>();
					log(string.Format("ANIMAL {0}{1}{2}{3}", new object[]
					{
						text,
						list2.Count,
						Anatomy.AllBones.Length,
						(list2.Count > 0) ? (": " + string.Join<int>(", ", list2)) : ""
					}));
				}
				int num2;
				AnimalBones._hitsLogged.TryGetValue(hash, out num2);
				if (num2 < 6)
				{
					AnimalBones._hitsLogged[hash] = num2 + 1;
					int num3 = 0;
					string text3 = "no damage bone";
					if (PED.GET_PED_LAST_DAMAGE_BONE(p.Handle, &num3))
					{
						int bi = PED.GET_PED_BONE_INDEX(p.Handle, num3);
						string text4 = AnimalBones.Names.FirstOrDefault((string x) => ENTITY.GET_ENTITY_BONE_INDEX_BY_NAME(p.Handle, x) == bi && bi >= 0);
						text3 = string.Format("bone {0} = {1} (index {2}{3})", new object[]
						{
							num3,
							num3,
							bi,
							(text4 != null) ? (", " + text4.Replace("SKEL_", "")) : ""
						});
					}
					Vector3 position = p.Position;
					ulong* ptr = stackalloc ulong[(UIntPtr)24];
					if (WEAPON.GET_PED_LAST_WEAPON_IMPACT_COORD(Game.Player.Character.Handle, (Vector3*)ptr))
					{
						position..ctor(*(float*)ptr, *(float*)(ptr + 1), *(float*)(ptr + 2));
					}
					Vector3 vector2 = AnimalBones.Local(p, position);
					log(string.Format("ANIMAL {0}{1}{2}{3}{4}{5}{6}{7}", new object[]
					{
						text,
						text3,
						vector2.X,
						vector2.Y,
						vector2.Z,
						p.Health,
						p.MaxHealth,
						p.IsDead ? ", dead" : ""
					}));
				}
			}
			catch (Exception ex)
			{
				log("ANIMAL BONES " + text + ": probe failed - " + ex.Message);
			}
		}

		// Token: 0x04000DBC RID: 3516
		private static readonly HashSet<uint> _probed = new HashSet<uint>();

		// Token: 0x04000DBD RID: 3517
		private static readonly Dictionary<uint, int> _hitsLogged = new Dictionary<uint, int>();

		// Token: 0x04000DBE RID: 3518
		private static string[] _names;
	}
}
