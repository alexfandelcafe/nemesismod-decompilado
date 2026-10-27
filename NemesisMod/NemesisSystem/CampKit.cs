using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using RDR2;
using RDR2.Math;
using RDR2.Native;

namespace NemesisSystem
{
	// Token: 0x0200000D RID: 13
	internal sealed class CampKit
	{
		// Token: 0x06000070 RID: 112 RVA: 0x000085FC File Offset: 0x000067FC
		internal CampKit(NemesisMod mod)
		{
			this._mod = mod;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00008663 File Offset: 0x00006863
		private static CampKit.KitPiece P(string piece, string[] names, int count, CampKit.KitZone zone, float size = 0.7f, bool faces = false, int chance = 100)
		{
			return new CampKit.KitPiece
			{
				Piece = piece,
				Names = names,
				Count = count,
				Zone = zone,
				Size = size,
				Faces = faces,
				Chance = chance
			};
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000086A0 File Offset: 0x000068A0
		internal static CampKit.CampLook LookOf(string gang)
		{
			CampKit.CampLook result;
			if (gang == null || !CampKit.CampLooks.TryGetValue(gang, out result))
			{
				return CampKit.PlainCamp;
			}
			return result;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000086C6 File Offset: 0x000068C6
		internal IEnumerable<string> Keys
		{
			get
			{
				return this._kits.Keys;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000086D3 File Offset: 0x000068D3
		internal bool Has(string key)
		{
			return this._kits.ContainsKey(key);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000086E4 File Offset: 0x000068E4
		internal Vector3? FirstSpot(string key)
		{
			List<ValueTuple<Vector3, float>> list;
			if (!this._spots.TryGetValue(key, out list) || list.Count <= 0)
			{
				return null;
			}
			return new Vector3?(list[0].Item1);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00008725 File Offset: 0x00006925
		internal void ForgetCentre(string siteId)
		{
			this._centres.Remove(siteId);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00008734 File Offset: 0x00006934
		internal bool ClearOf(string siteId, Vector3 at, float r)
		{
			List<ValueTuple<Vector3, float>> source;
			return siteId == null || !this._spots.TryGetValue(siteId, out source) || source.All(([TupleElementNames(new string[]
			{
				"At",
				"R"
			})] ValueTuple<Vector3, float> x) => x.Item1.DistanceTo2D(at) >= x.Item2 + r);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x0000877C File Offset: 0x0000697C
		internal unsafe Vector3 Centre(CampSite site)
		{
			CampKit.<>c__DisplayClass60_0 CS$<>8__locals1;
			CS$<>8__locals1.site = site;
			Vector3 vector;
			vector..ctor(CS$<>8__locals1.site.X, CS$<>8__locals1.site.Y, CS$<>8__locals1.site.Z);
			if (CS$<>8__locals1.site.Imap != 0U || CS$<>8__locals1.site.Hideout)
			{
				return vector;
			}
			Vector3 result;
			if (this._centres.TryGetValue(CS$<>8__locals1.site.Id, out result))
			{
				return result;
			}
			ValueTuple<Vector3, int> valueTuple;
			if (this._held.TryGetValue(CS$<>8__locals1.site.Id, out valueTuple) && Game.GameTime < valueTuple.Item2)
			{
				return valueTuple.Item1;
			}
			float num;
			if (!MISC.GET_GROUND_Z_FOR_3D_COORD(vector.X, vector.Y, CS$<>8__locals1.site.Z + 3f, &num, false))
			{
				STREAMING.REQUEST_COLLISION_AT_COORD(vector.X, vector.Y, vector.Z);
				this._held[CS$<>8__locals1.site.Id] = new ValueTuple<Vector3, int>(vector, Game.GameTime + 120000);
				return vector;
			}
			Vector3? vector2 = CampKit.<Centre>g__Try|60_0(vector, ref CS$<>8__locals1);
			int num2 = 0;
			while (vector2 == null && num2 < 4)
			{
				float num3 = (new float[]
				{
					10f,
					16f,
					22f,
					30f
				})[num2];
				int num4 = 0;
				while (num4 < 12 && vector2 == null)
				{
					double num5 = (double)num4 * 3.141592653589793 / 6.0 + (double)num2 * 0.26;
					vector2 = CampKit.<Centre>g__Try|60_0(vector + new Vector3((float)Math.Cos(num5) * num3, (float)Math.Sin(num5) * num3, 0f), ref CS$<>8__locals1);
					num4++;
				}
				num2++;
			}
			Vector3 valueOrDefault = vector2.GetValueOrDefault(vector);
			if (vector2 != null)
			{
				this._centres[CS$<>8__locals1.site.Id] = valueOrDefault;
			}
			if (vector2 == null)
			{
				this._mod.GrudgeLog("CAMPKIT " + CS$<>8__locals1.site.Id + ": no open ground within 30 m - the site as it is");
			}
			else if (valueOrDefault.DistanceTo2D(vector) > 1f)
			{
				this._mod.GrudgeLog(string.Format("CAMPKIT {0}{1}", CS$<>8__locals1.site.Id, valueOrDefault.DistanceTo2D(vector)));
			}
			return valueOrDefault;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000089D0 File Offset: 0x00006BD0
		[return: TupleElementNames(new string[]
		{
			"At",
			"R"
		})]
		internal static List<ValueTuple<Vector3, float>> Plan(CampKit.CampLook look, Vector3 centre, int seed, bool stores, Func<CampKit.KitPiece, Vector3, float, bool> place)
		{
			CampKit.<>c__DisplayClass61_0 CS$<>8__locals1 = new CampKit.<>c__DisplayClass61_0();
			CS$<>8__locals1.look = look;
			CS$<>8__locals1.stores = stores;
			CS$<>8__locals1.centre = centre;
			CS$<>8__locals1.spots = new List<ValueTuple<Vector3, float>>();
			CS$<>8__locals1.rng = new Random(seed);
			CS$<>8__locals1.a0 = (float)(CS$<>8__locals1.rng.NextDouble() * 3.141592653589793 * 2.0);
			CS$<>8__locals1.dir = new Vector3((float)Math.Cos((double)CS$<>8__locals1.a0), (float)Math.Sin((double)CS$<>8__locals1.a0), 0f);
			CS$<>8__locals1.perp = new Vector3(-CS$<>8__locals1.dir.Y, CS$<>8__locals1.dir.X, 0f);
			CS$<>8__locals1.spots.Add(new ValueTuple<Vector3, float>(CS$<>8__locals1.centre, CS$<>8__locals1.look.Fire ? 1.3f : 0.8f));
			List<ValueTuple<CampKit.KitPiece, int>> list = (from p in CS$<>8__locals1.look.Pieces
			where (CS$<>8__locals1.look.Fire || p.Piece != "wood") && (CS$<>8__locals1.stores || !CampKit.<Plan>g__Store|61_0(p.Piece))
			select new ValueTuple<CampKit.KitPiece, int>(p, (p.Chance >= 100 || CS$<>8__locals1.rng.Next(100) < p.Chance) ? p.Count : 0)).ToList<ValueTuple<CampKit.KitPiece, int>>();
			Dictionary<CampKit.KitZone, int> dictionary = new Dictionary<CampKit.KitZone, int>();
			foreach (ValueTuple<CampKit.KitPiece, int> valueTuple in list)
			{
				CampKit.KitPiece item = valueTuple.Item1;
				int item2 = valueTuple.Item2;
				int num;
				dictionary[item.Zone] = (dictionary.TryGetValue(item.Zone, out num) ? num : 0) + item2;
			}
			Dictionary<CampKit.KitZone, int> dictionary2 = new Dictionary<CampKit.KitZone, int>();
			foreach (ValueTuple<CampKit.KitPiece, int> valueTuple2 in list)
			{
				CampKit.KitPiece item3 = valueTuple2.Item1;
				int item4 = valueTuple2.Item2;
				int i = 0;
				while (i < item4)
				{
					int num3;
					int num2 = dictionary2.TryGetValue(item3.Zone, out num3) ? num3 : 0;
					dictionary2[item3.Zone] = num2 + 1;
					Vector3 vector = CS$<>8__locals1.<Plan>g__Spot|5(item3.Zone, num2, Math.Max(1, dictionary[item3.Zone]));
					if (CS$<>8__locals1.<Plan>g__Free|6(vector, item3.Size))
					{
						goto IL_2A7;
					}
					double num4 = Math.Atan2((double)(vector.Y - CS$<>8__locals1.centre.Y), (double)(vector.X - CS$<>8__locals1.centre.X));
					float num5 = vector.DistanceTo2D(CS$<>8__locals1.centre);
					int num6 = 1;
					while (num6 <= 10 && !CS$<>8__locals1.<Plan>g__Free|6(vector, item3.Size))
					{
						vector = CS$<>8__locals1.<Plan>g__Polar|4(num4 + 0.4 * (double)((num6 + 1) / 2) * (double)((num6 % 2 == 0) ? -1 : 1), num5 + 0.3f * (float)num6);
						num6++;
					}
					if (CS$<>8__locals1.<Plan>g__Free|6(vector, item3.Size))
					{
						goto IL_2A7;
					}
					IL_2F6:
					i++;
					continue;
					IL_2A7:
					if (place(item3, vector, item3.Faces ? CS$<>8__locals1.<Plan>g__Facing|7(vector) : ((float)(CS$<>8__locals1.rng.NextDouble() * 360.0))))
					{
						CS$<>8__locals1.spots.Add(new ValueTuple<Vector3, float>(vector, item3.Size));
						goto IL_2F6;
					}
					goto IL_2F6;
				}
			}
			return CS$<>8__locals1.spots;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00008D38 File Offset: 0x00006F38
		internal void Spawn(CampSite site, string gang, bool supply)
		{
			if (site == null || site.Imap != 0U || site.Hideout || this._kits.ContainsKey(site.Id))
			{
				return;
			}
			List<int> list = new List<int>();
			this._kits[site.Id] = list;
			Vector3 centre = this.Centre(site);
			this.Lay(site.Id, centre, Seeds.Stable("campkit/" + site.Id + "/" + gang), gang, !supply, list);
			if (!supply)
			{
				try
				{
					this.SpawnRig(site.Id, gang, this.Centre(site));
				}
				catch (Exception ex)
				{
					this._mod.GrudgeLog("CAMPKIT rig failed: " + ex.Message);
				}
			}
			this._mod.GrudgeLog(string.Format("CAMPKIT {0}{1}{2}", site.Id, list.Count, CampKit.CampLooks.ContainsKey(gang ?? "") ? (NemesisMod.ShortOutfit(Gangs.ById(gang)) + " look") : "plain look"));
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00008E5C File Offset: 0x0000705C
		internal int LayAt(string key, Vector3 centre, int seed, string gang)
		{
			List<int> list = new List<int>();
			this._kits[key] = list;
			this.Lay(key, centre, seed, gang, true, list);
			return list.Count;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00008E90 File Offset: 0x00007090
		private void Lay(string key, Vector3 centre, int seed, string gang, bool stores, List<int> made)
		{
			CampKit.CampLook campLook = CampKit.LookOf(gang);
			if (campLook.Fire)
			{
				NemesisMod mod = this._mod;
				Vector3 vector = NemesisMod.GroundOnSpot(centre);
				int num = mod.MakeFire((vector != Vector3.Zero) ? vector : centre);
				if (num != 0)
				{
					made.Add(num);
				}
			}
			this._spots[key] = CampKit.Plan(campLook, centre, seed, stores, delegate(CampKit.KitPiece p, Vector3 at, float heading)
			{
				int num2 = this.Prop(p.Piece, p.Names, at, heading);
				if (num2 != 0)
				{
					made.Add(num2);
				}
				return num2 != 0;
			});
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00008F18 File Offset: 0x00007118
		private int Prop(string piece, string[] names, Vector3 at, float heading)
		{
			if (this._logged.Contains(piece))
			{
				return 0;
			}
			Vector3 vector = NemesisMod.GroundOnSpot(at);
			if (vector == Vector3.Zero)
			{
				return 0;
			}
			string text;
			string[] array;
			if (!this._model.TryGetValue(piece, out text))
			{
				array = names;
			}
			else
			{
				(array = new string[1])[0] = text;
			}
			foreach (string text2 in array)
			{
				Model m;
				m..ctor(text2);
				if (m.IsValid && m.IsInCdImage && this._mod.LoadModelSafe(m, 1500))
				{
					Crumbs.Native("camp kit: create " + piece);
					int num = OBJECT.CREATE_OBJECT((uint)m.Hash, vector.X, vector.Y, vector.Z, false, false, false, false, true);
					STREAMING.SET_MODEL_AS_NO_LONGER_NEEDED((uint)m.Hash);
					if (num != 0)
					{
						ENTITY.SET_ENTITY_HEADING(num, heading);
						OBJECT.PLACE_OBJECT_ON_GROUND_PROPERLY(num, false);
						ENTITY.FREEZE_ENTITY_POSITION(num, true);
						ENTITY.SET_ENTITY_AS_MISSION_ENTITY(num, true, true);
						if (!this._model.ContainsKey(piece))
						{
							this._model[piece] = text2;
							this._mod.GrudgeLog("CAMPKIT " + piece + ": " + text2);
						}
						return num;
					}
				}
			}
			if (this._logged.Add(piece))
			{
				this._mod.GrudgeLog(string.Concat(new string[]
				{
					"CAMPKIT ",
					piece,
					": none of ",
					(names.Length <= 6) ? string.Join(", ", names) : string.Format("{0} names ({1}, {2}...)", names.Length, names[0], names[1]),
					" could be made - left out"
				}));
			}
			return 0;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000090D0 File Offset: 0x000072D0
		private void SpawnRig(string siteId, string gang, Vector3 centre)
		{
			CampKit.<>c__DisplayClass66_0 CS$<>8__locals1;
			CS$<>8__locals1.centre = centre;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.siteId = siteId;
			ValueTuple<string[], int, int> valueTuple;
			string[] item;
			int num;
			int item2;
			if (gang == null || !CampKit.CampRigs.TryGetValue(gang, out valueTuple))
			{
				ValueTuple<string[], int, int> valueTuple2 = CampKit.PlainRig;
				item = valueTuple2.Item1;
				num = valueTuple2.Item2;
				item2 = valueTuple2.Item3;
			}
			else
			{
				ValueTuple<string[], int, int> valueTuple2 = valueTuple;
				item = valueTuple2.Item1;
				num = valueTuple2.Item2;
				item2 = valueTuple2.Item3;
			}
			if (item.Length == 0)
			{
				num = 0;
			}
			if (num == 0 && item2 == 0)
			{
				return;
			}
			CampKit.Rig rig = new CampKit.Rig();
			this._rigs[CS$<>8__locals1.siteId] = rig;
			Random random = new Random(Seeds.Stable("camprig/" + CS$<>8__locals1.siteId + "/" + gang));
			double num2 = random.NextDouble() * 3.141592653589793 * 2.0;
			List<string> list = new List<string>();
			for (int i = 0; i < num; i++)
			{
				Vector3 vector = this.<SpawnRig>g__Spot|66_0(num2 + (double)i * 0.9, 12.5f, 3.2f, ref CS$<>8__locals1);
				if (vector != Vector3.Zero)
				{
					foreach (string text in item.Skip(i % item.Length).Concat(item.Take(i % item.Length)))
					{
						uint num3 = MISC.GET_HASH_KEY(text);
						if (STREAMING.IS_MODEL_IN_CDIMAGE(num3) && STREAMING.IS_MODEL_A_VEHICLE(num3) && this._mod.LoadModelSafe(new Model((int)num3), 1500))
						{
							Crumbs.Native("camp rig wagon");
							Vehicle vehicle = NemesisMod.SpawnVehicle(num3, vector + new Vector3(0f, 0f, 0.3f), NemesisMod.HeadingTo(vector, CS$<>8__locals1.centre) + 90f);
							STREAMING.SET_MODEL_AS_NO_LONGER_NEEDED(num3);
							if (!(vehicle == null))
							{
								NemesisMod.MakeOurs(vehicle.Handle);
								VEHICLE.SET_VEHICLE_ON_GROUND_PROPERLY(vehicle.Handle, false);
								rig.Wagons.Add(vehicle);
								list.Add(text);
								List<ValueTuple<Vector3, float>> list2;
								if (this._spots.TryGetValue(CS$<>8__locals1.siteId, out list2))
								{
									list2.Add(new ValueTuple<Vector3, float>(vector, 3.2f));
									break;
								}
								break;
							}
						}
					}
				}
			}
			int num4 = 0;
			for (int j = 0; j < item2; j++)
			{
				Vector3 vector2 = this.<SpawnRig>g__Spot|66_0(num2 + 3.141592653589793 + (double)j * 0.2, 13f, 1.6f, ref CS$<>8__locals1);
				if (!(vector2 == Vector3.Zero))
				{
					Crumbs.Native("camp rig horse");
					Ped ped = NemesisMod.SpawnPed(NemesisMod.RiderHorses[random.Next(NemesisMod.RiderHorses.Length)], vector2, NemesisMod.HeadingTo(vector2, CS$<>8__locals1.centre) + 90f);
					if (!(ped == null))
					{
						NemesisMod.MakeOurs(ped.Handle);
						TASK.TASK_STAND_STILL(ped.Handle, -1);
						rig.Horses.Add(ped);
						num4++;
						List<ValueTuple<Vector3, float>> list3;
						if (this._spots.TryGetValue(CS$<>8__locals1.siteId, out list3))
						{
							list3.Add(new ValueTuple<Vector3, float>(vector2, 1.6f));
						}
					}
				}
			}
			NemesisMod mod = this._mod;
			string format = "CAMPKIT {0}{1}{2}";
			object siteId2 = CS$<>8__locals1.siteId;
			object arg;
			if (list.Count <= 0)
			{
				arg = ((num > 0) ? "no wagon (no room / no model)" : "no wagon");
			}
			else
			{
				arg = string.Join(" y ", from m in list
				select "a " + m) + ((list.Count < num) ? string.Format(" ({0} more: no room / no model)", num - list.Count) : "");
			}
			mod.GrudgeLog(string.Format(format, siteId2, arg, num4));
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000094DC File Offset: 0x000076DC
		internal unsafe void Despawn(string key)
		{
			try
			{
				this.DespawnRig(key);
			}
			catch (Exception ex)
			{
				this._mod.GrudgeLog("CAMPKIT rig delete failed: " + ex.Message);
			}
			List<int> list;
			if (key == null || !this._kits.TryGetValue(key, out list))
			{
				return;
			}
			this._kits.Remove(key);
			this._spots.Remove(key);
			foreach (int num in list)
			{
				if (num != 0 && ENTITY.DOES_ENTITY_EXIST(num))
				{
					Crumbs.Native("camp kit: delete");
					OBJECT.DELETE_OBJECT(&num);
				}
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000095A4 File Offset: 0x000077A4
		private unsafe void DespawnRig(string siteId)
		{
			CampKit.Rig rig;
			if (siteId == null || !this._rigs.TryGetValue(siteId, out rig))
			{
				return;
			}
			this._rigs.Remove(siteId);
			Ped character = Game.Player.Character;
			foreach (Vehicle vehicle in rig.Wagons)
			{
				if (vehicle != null && vehicle.Exists())
				{
					if (!PED.IS_PED_IN_VEHICLE(character.Handle, vehicle.Handle, false) && this._mod.SafeToDeleteVehicle(vehicle))
					{
						vehicle.Delete();
					}
					else
					{
						int handle = vehicle.Handle;
						NemesisMod.ReleaseIfOurs(&handle);
					}
				}
			}
			foreach (Ped ped in rig.Horses)
			{
				if (ped != null && ped.Exists())
				{
					if ((!PED.IS_PED_ON_MOUNT(character.Handle) || PED.GET_MOUNT(character.Handle) != ped.Handle) && !ped.IsDead && this._mod.SafeToDelete(ped))
					{
						ped.Delete();
					}
					else
					{
						int handle2 = ped.Handle;
						NemesisMod.ReleaseIfOurs(&handle2);
					}
				}
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00009710 File Offset: 0x00007910
		// Note: this type is marked as 'beforefieldinit'.
		static CampKit()
		{
			Dictionary<string, CampKit.CampLook> dictionary = new Dictionary<string, CampKit.CampLook>();
			dictionary["odriscoll"] = new CampKit.CampLook
			{
				Shape = CampKit.KitShape.Cluster,
				Pieces = new CampKit.KitPiece[]
				{
					CampKit.P("grass tent", CampKit.KitGrassTent, 2, CampKit.KitZone.Outer, 2.5f, true, 100),
					CampKit.P("bedroll", CampKit.KitBedrolls, 3, CampKit.KitZone.Inner, 0.9f, true, 100),
					CampKit.P("seat", CampKit.KitSeats, 2, CampKit.KitZone.Inner, 0.5f, true, 100),
					CampKit.P("lantern", CampKit.KitLanterns, 1, CampKit.KitZone.Mid, 0.3f, false, 100),
					CampKit.P("wood", CampKit.KitWood, 1, CampKit.KitZone.Mid, 0.9f, false, 100),
					CampKit.P("crate stack", CampKit.KitCrateStack, 3, CampKit.KitZone.Outer, 1.2f, false, 100),
					CampKit.P("chest", CampKit.KitChest, 2, CampKit.KitZone.Mid, 0.8f, false, 100),
					CampKit.P("barrel", CampKit.KitBarrels, 3, CampKit.KitZone.Mid, 0.5f, false, 100),
					CampKit.P("whiskey", CampKit.KitWhiskey, 2, CampKit.KitZone.Inner, 0.3f, false, 100)
				}
			};
			dictionary["lemoyneraiders"] = new CampKit.CampLook
			{
				Shape = CampKit.KitShape.Row,
				Pieces = new CampKit.KitPiece[]
				{
					CampKit.P("army tent", CampKit.KitArmyTent, 2, CampKit.KitZone.Outer, 3f, true, 100),
					CampKit.P("bedroll", CampKit.KitBedrolls, 3, CampKit.KitZone.Inner, 0.9f, true, 100),
					CampKit.P("seat", CampKit.KitSeats, 1, CampKit.KitZone.Inner, 0.5f, true, 100),
					CampKit.P("lantern", CampKit.KitLanterns, 1, CampKit.KitZone.Mid, 0.3f, false, 100),
					CampKit.P("wood", CampKit.KitWood, 1, CampKit.KitZone.Mid, 0.9f, false, 100),
					CampKit.P("still", CampKit.KitStill, 1, CampKit.KitZone.Line, 1.5f, false, 100),
					CampKit.P("barrel", CampKit.KitBarrels, 4, CampKit.KitZone.Line, 0.5f, false, 100),
					CampKit.P("hay stack", CampKit.KitHayStack, 1, CampKit.KitZone.Line, 1.6f, false, 100),
					CampKit.P("jug", CampKit.KitJug, 2, CampKit.KitZone.Line, 0.3f, false, 100)
				}
			};
			dictionary["wapiti"] = new CampKit.CampLook
			{
				Shape = CampKit.KitShape.Ring,
				Pieces = new CampKit.KitPiece[]
				{
					CampKit.P("tipi", CampKit.KitTipi, 2, CampKit.KitZone.Outer, 3f, true, 100),
					CampKit.P("bedroll", CampKit.KitBedrolls, 2, CampKit.KitZone.Inner, 0.9f, true, 100),
					CampKit.P("log seat", CampKit.KitLogSeat, 3, CampKit.KitZone.Inner, 0.9f, true, 100),
					CampKit.P("wood", CampKit.KitWood, 1, CampKit.KitZone.Mid, 0.9f, false, 100),
					CampKit.P("blanket", CampKit.KitBlanket, 2, CampKit.KitZone.Mid, 0.6f, false, 100),
					CampKit.P("pelt", CampKit.KitPelt, 1, CampKit.KitZone.Mid, 0.9f, false, 100)
				}
			};
			dictionary["bronte"] = new CampKit.CampLook
			{
				Shape = CampKit.KitShape.Cluster,
				Pieces = new CampKit.KitPiece[]
				{
					CampKit.P("caravan tent", CampKit.KitCaravan, 1, CampKit.KitZone.Outer, 3.5f, true, 100),
					CampKit.P("table", CampKit.KitTable, 1, CampKit.KitZone.Inner, 1.3f, false, 100),
					CampKit.P("chair", CampKit.KitChair, 4, CampKit.KitZone.Inner, 0.6f, true, 100),
					CampKit.P("lantern", CampKit.KitLanterns, 3, CampKit.KitZone.Mid, 0.3f, false, 100),
					CampKit.P("trunk", CampKit.KitTrunk, 2, CampKit.KitZone.Mid, 0.9f, false, 100),
					CampKit.P("bedroll", CampKit.KitBedrolls, 2, CampKit.KitZone.Mid, 0.9f, true, 100),
					CampKit.P("wood", CampKit.KitWood, 1, CampKit.KitZone.Mid, 0.9f, false, 100)
				}
			};
			dictionary["murfree"] = new CampKit.CampLook
			{
				Shape = CampKit.KitShape.Scatter,
				Pieces = new CampKit.KitPiece[]
				{
					CampKit.P("grave", CampKit.KitGrave, 3, CampKit.KitZone.Outer, 0.8f, true, 100),
					CampKit.P("coffin", CampKit.KitCoffin, 1, CampKit.KitZone.Mid, 1.3f, false, 100),
					CampKit.P("bedroll", CampKit.KitBedrolls, 2, CampKit.KitZone.Inner, 0.9f, true, 100),
					CampKit.P("log seat", CampKit.KitLogSeat, 1, CampKit.KitZone.Inner, 0.9f, true, 100),
					CampKit.P("wood", CampKit.KitWood, 1, CampKit.KitZone.Mid, 0.9f, false, 100),
					CampKit.P("barrel", CampKit.KitBarrels, 2, CampKit.KitZone.Mid, 0.5f, false, 100),
					CampKit.P("lean-to", CampKit.KitLeanTo, 1, CampKit.KitZone.Outer, 2.2f, true, 100),
					CampKit.P("skull", CampKit.KitSkull, 3, CampKit.KitZone.Mid, 0.4f, false, 100)
				}
			};
			dictionary["skinners"] = new CampKit.CampLook
			{
				Shape = CampKit.KitShape.Scatter,
				Pieces = new CampKit.KitPiece[]
				{
					CampKit.P("lean-to", CampKit.KitLeanTo, 1, CampKit.KitZone.Outer, 2.2f, true, 100),
					CampKit.P("grass tent", CampKit.KitGrassTent, 2, CampKit.KitZone.Outer, 2.5f, true, 100),
					CampKit.P("tent", CampKit.KitTents, 1, CampKit.KitZone.Outer, 2.5f, true, 100),
					CampKit.P("pelt", CampKit.KitPelt, 1, CampKit.KitZone.Mid, 0.9f, false, 100),
					CampKit.P("barrel", CampKit.KitBarrels, 3, CampKit.KitZone.Mid, 0.5f, false, 100),
					CampKit.P("crates", CampKit.KitCrates, 1, CampKit.KitZone.Mid, 1f, false, 100),
					CampKit.P("bedroll", CampKit.KitBedrolls, 2, CampKit.KitZone.Inner, 0.9f, true, 100),
					CampKit.P("wood", CampKit.KitWood, 1, CampKit.KitZone.Mid, 0.9f, false, 100)
				}
			};
			dictionary["laramie"] = new CampKit.CampLook
			{
				Shape = CampKit.KitShape.Row,
				Pieces = new CampKit.KitPiece[]
				{
					CampKit.P("tent", CampKit.KitTents, 3, CampKit.KitZone.Outer, 2.5f, true, 100),
					CampKit.P("hay stack", CampKit.KitHayStack, 3, CampKit.KitZone.Line, 1.6f, false, 100),
					CampKit.P("hay", CampKit.KitHay, 2, CampKit.KitZone.Line, 1f, false, 100),
					CampKit.P("saddle", CampKit.KitSaddle, 2, CampKit.KitZone.Mid, 0.6f, false, 100),
					CampKit.P("barrel", CampKit.KitBarrels, 3, CampKit.KitZone.Mid, 0.5f, false, 100),
					CampKit.P("bedroll", CampKit.KitBedrolls, 3, CampKit.KitZone.Inner, 0.9f, true, 100),
					CampKit.P("seat", CampKit.KitSeats, 1, CampKit.KitZone.Inner, 0.5f, true, 100),
					CampKit.P("lantern", CampKit.KitLanterns, 1, CampKit.KitZone.Mid, 0.3f, false, 100),
					CampKit.P("wood", CampKit.KitWood, 1, CampKit.KitZone.Mid, 0.9f, false, 100)
				}
			};
			dictionary["delobo"] = new CampKit.CampLook
			{
				Shape = CampKit.KitShape.Ring,
				Pieces = new CampKit.KitPiece[]
				{
					CampKit.P("bedroll", CampKit.KitBedrolls, 6, CampKit.KitZone.Inner, 0.9f, true, 100),
					CampKit.P("log seat", CampKit.KitLogSeat, 3, CampKit.KitZone.Mid, 0.9f, true, 100),
					CampKit.P("blanket", CampKit.KitBlanket, 2, CampKit.KitZone.Mid, 0.6f, false, 100),
					CampKit.P("trunk", CampKit.KitTrunk, 1, CampKit.KitZone.Mid, 0.9f, false, 100),
					CampKit.P("jug", CampKit.KitJug, 2, CampKit.KitZone.Mid, 0.3f, false, 100),
					CampKit.P("lantern", CampKit.KitLanterns, 2, CampKit.KitZone.Mid, 0.3f, false, 100),
					CampKit.P("wood", CampKit.KitWood, 1, CampKit.KitZone.Mid, 0.9f, false, 100)
				}
			};
			dictionary["nightfolk"] = new CampKit.CampLook
			{
				Fire = false,
				Shape = CampKit.KitShape.Scatter,
				Pieces = new CampKit.KitPiece[]
				{
					CampKit.P("lean-to", CampKit.KitLeanTo, 2, CampKit.KitZone.Outer, 2.2f, true, 100),
					CampKit.P("grave", CampKit.KitGrave, 2, CampKit.KitZone.Outer, 0.8f, true, 100),
					CampKit.P("candles", CampKit.KitCandle, 4, CampKit.KitZone.Inner, 0.3f, false, 100),
					CampKit.P("skull", CampKit.KitSkull, 3, CampKit.KitZone.Mid, 0.4f, false, 100),
					CampKit.P("lantern", CampKit.KitLanterns, 3, CampKit.KitZone.Mid, 0.3f, false, 100),
					CampKit.P("bedroll", CampKit.KitBedrolls, 2, CampKit.KitZone.Inner, 0.9f, true, 100)
				}
			};
			CampKit.CampLooks = dictionary;
			Dictionary<string, ValueTuple<string[], int, int>> dictionary2 = new Dictionary<string, ValueTuple<string[], int, int>>();
			dictionary2["odriscoll"] = new ValueTuple<string[], int, int>(new string[]
			{
				"wagon04x",
				"wagon02x",
				"cart03"
			}, 2, 2);
			dictionary2["lemoyneraiders"] = new ValueTuple<string[], int, int>(new string[]
			{
				"wagon05x",
				"wagon06x",
				"cart06"
			}, 2, 2);
			dictionary2["wapiti"] = new ValueTuple<string[], int, int>(new string[0], 0, 3);
			dictionary2["bronte"] = new ValueTuple<string[], int, int>(new string[]
			{
				"coach2",
				"buggy01",
				"wagon02x"
			}, 2, 2);
			dictionary2["murfree"] = new ValueTuple<string[], int, int>(new string[]
			{
				"cart03",
				"cart01"
			}, 1, 1);
			dictionary2["skinners"] = new ValueTuple<string[], int, int>(new string[]
			{
				"cart01",
				"wagon04x",
				"cart03"
			}, 2, 2);
			dictionary2["laramie"] = new ValueTuple<string[], int, int>(new string[]
			{
				"chuckwagon000x",
				"wagon05x",
				"chuckwagon002x"
			}, 2, 3);
			dictionary2["delobo"] = new ValueTuple<string[], int, int>(new string[]
			{
				"cart06",
				"cart03",
				"wagon04x"
			}, 2, 3);
			dictionary2["nightfolk"] = new ValueTuple<string[], int, int>(new string[0], 0, 0);
			CampKit.CampRigs = dictionary2;
			CampKit.PlainRig = new ValueTuple<string[], int, int>(new string[]
			{
				"wagon04x",
				"cart03"
			}, 1, 2);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000A73C File Offset: 0x0000893C
		[CompilerGenerated]
		internal unsafe static Vector3? <Centre>g__Try|60_0(Vector3 c, ref CampKit.<>c__DisplayClass60_0 A_1)
		{
			float num;
			if (!MISC.GET_GROUND_Z_FOR_3D_COORD(c.X, c.Y, A_1.site.Z + 3f, &num, false))
			{
				return null;
			}
			if (Math.Abs(num - A_1.site.Z) > 4f)
			{
				return null;
			}
			Vector3 vector;
			vector..ctor(c.X, c.Y, num);
			if (!NemesisMod.Outdoors(vector) || !NemesisMod.Dry(vector))
			{
				return null;
			}
			return new Vector3?(vector);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000A7D2 File Offset: 0x000089D2
		[CompilerGenerated]
		internal static bool <Plan>g__Store|61_0(string piece)
		{
			return piece == "crates" || piece == "barrel" || piece == "crate stack";
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000A7FB File Offset: 0x000089FB
		[CompilerGenerated]
		internal static float <Plan>g__R|61_3(CampKit.KitZone z)
		{
			if (z == CampKit.KitZone.Inner)
			{
				return 3.4f;
			}
			if (z == CampKit.KitZone.Mid)
			{
				return 6f;
			}
			if (z != CampKit.KitZone.Outer)
			{
				return 6.5f;
			}
			return 8.8f;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000A820 File Offset: 0x00008A20
		[CompilerGenerated]
		private Vector3 <SpawnRig>g__Spot|66_0(double a, float r0, float room, ref CampKit.<>c__DisplayClass66_0 A_4)
		{
			for (int i = 0; i < 12; i++)
			{
				double num = a + (double)i * 0.35 * (double)((i % 2 == 0) ? 1 : -1);
				Vector3 vector = NemesisMod.GroundOnSpot(A_4.centre + new Vector3((float)Math.Cos(num) * r0, (float)Math.Sin(num) * r0, 0f));
				if (!(vector == Vector3.Zero) && NemesisMod.Dry(vector) && NemesisMod.OpenGround(vector, room) && this.ClearOf(A_4.siteId, vector, room) && Math.Abs(vector.Z - A_4.centre.Z) <= 3f)
				{
					return vector;
				}
			}
			return Vector3.Zero;
		}

		// Token: 0x040000DF RID: 223
		private readonly NemesisMod _mod;

		// Token: 0x040000E0 RID: 224
		private static readonly string[] KitTents = new string[]
		{
			"p_ambtentscrub01b",
			"p_ambtentgrass01b",
			"p_ambtentmulch01b",
			"p_ambtentdirt01x",
			"p_tentarmy01x",
			"s_tentcaravan01x",
			"p_tent_leento01x"
		};

		// Token: 0x040000E1 RID: 225
		private static readonly string[] KitBedrolls = new string[]
		{
			"p_bedrollopen01x",
			"p_bedrollopen02x",
			"p_bedroll01x",
			"p_bedrollclosed01x"
		};

		// Token: 0x040000E2 RID: 226
		private static readonly string[] KitSeats = new string[]
		{
			"p_stool01x",
			"p_woodenchair01x",
			"p_chair05x",
			"p_bench_log01x",
			"p_log02x"
		};

		// Token: 0x040000E3 RID: 227
		private static readonly string[] KitLanterns = new string[]
		{
			"p_lantern05x",
			"p_lantern09x",
			"p_lantern01x",
			"p_lantern02x"
		};

		// Token: 0x040000E4 RID: 228
		private static readonly string[] KitWood = new string[]
		{
			"p_woodpile01x",
			"p_woodpile02x",
			"p_firewood01x",
			"p_logpile01x"
		};

		// Token: 0x040000E5 RID: 229
		private static readonly string[] KitCrates = new string[]
		{
			"p_cratestack01x",
			"p_crate03x",
			"p_crate14x"
		};

		// Token: 0x040000E6 RID: 230
		private static readonly string[] KitBarrels = new string[]
		{
			"p_barrel02x",
			"p_barrel01x",
			"p_barrel_wood01x"
		};

		// Token: 0x040000E7 RID: 231
		private static readonly string[] KitGrassTent = new string[]
		{
			"p_ambtentgrass01b",
			"p_ambtentscrub01b"
		};

		// Token: 0x040000E8 RID: 232
		private static readonly string[] KitArmyTent = new string[]
		{
			"p_tentarmy01x",
			"p_tentarmy02x",
			"p_ambtentscrub01b"
		};

		// Token: 0x040000E9 RID: 233
		private static readonly string[] KitCaravan = new string[]
		{
			"s_tentcaravan01x",
			"s_tentcaravan02x",
			"p_tentarmy01x",
			"p_ambtentscrub01b"
		};

		// Token: 0x040000EA RID: 234
		private static readonly string[] KitLeanTo = new string[]
		{
			"p_tent_leento01x",
			"p_tentleanto01x",
			"p_leanto01x",
			"p_ambtentgrass01b"
		};

		// Token: 0x040000EB RID: 235
		private static readonly string[] KitTipi = new string[]
		{
			"p_tipi01x",
			"p_teepee01x",
			"p_tepee01x",
			"p_ambtentnative01x",
			"p_ambtentscrub01b"
		};

		// Token: 0x040000EC RID: 236
		private static readonly string[] KitLogSeat = new string[]
		{
			"p_bench_log01x",
			"p_log02x",
			"p_stool01x"
		};

		// Token: 0x040000ED RID: 237
		private static readonly string[] KitCrateStack = new string[]
		{
			"p_cratestack02x",
			"p_cratestack01x"
		};

		// Token: 0x040000EE RID: 238
		private static readonly string[] KitHayStack = new string[]
		{
			"p_haybalestack01x",
			"p_haybale02x"
		};

		// Token: 0x040000EF RID: 239
		private static readonly string[] KitHay = new string[]
		{
			"p_haybale01x",
			"p_haybale02x",
			"p_haybale03x"
		};

		// Token: 0x040000F0 RID: 240
		private static readonly string[] KitSaddle = new string[]
		{
			"p_saddle01x",
			"p_saddle02x",
			"p_saddle03x",
			"p_saddlestand01x"
		};

		// Token: 0x040000F1 RID: 241
		private static readonly string[] KitChest = new string[]
		{
			"p_chest01x",
			"p_chest02x",
			"p_trunk01x"
		};

		// Token: 0x040000F2 RID: 242
		private static readonly string[] KitWhiskey = new string[]
		{
			"p_bottlewhiskey01x",
			"p_whiskeybottle01x",
			"p_bottle008x",
			"p_bottle01x"
		};

		// Token: 0x040000F3 RID: 243
		private static readonly string[] KitStill = new string[]
		{
			"p_still01x",
			"p_still02x",
			"p_moonshinestill01x",
			"p_still03x"
		};

		// Token: 0x040000F4 RID: 244
		private static readonly string[] KitJug = new string[]
		{
			"p_jug01x",
			"p_jug02x",
			"p_moonshinejug01x",
			"p_jugmoonshine01x"
		};

		// Token: 0x040000F5 RID: 245
		private static readonly string[] KitPelt = new string[]
		{
			"p_bearskin01x"
		};

		// Token: 0x040000F6 RID: 246
		private static readonly string[] KitSkull = new string[]
		{
			"p_humanskull01x",
			"p_skull01x",
			"p_skullhuman01x",
			"p_skull02x"
		};

		// Token: 0x040000F7 RID: 247
		private static readonly string[] KitCoffin = new string[]
		{
			"p_coffin01x",
			"p_coffin02x"
		};

		// Token: 0x040000F8 RID: 248
		private static readonly string[] KitGrave = new string[]
		{
			"p_gravecross01x",
			"p_gravemarker01x",
			"p_gravemarker02x"
		};

		// Token: 0x040000F9 RID: 249
		private static readonly string[] KitBlanket = new string[]
		{
			"p_blanket01x",
			"p_blanketpile01x",
			"p_blanketfolded01x",
			"p_blankets01x"
		};

		// Token: 0x040000FA RID: 250
		private static readonly string[] KitTable = new string[]
		{
			"p_table01x",
			"p_table05x",
			"p_tablesmall01x",
			"p_tablewood01x"
		};

		// Token: 0x040000FB RID: 251
		private static readonly string[] KitChair = new string[]
		{
			"p_woodenchair01x",
			"p_chair05x",
			"p_stool01x"
		};

		// Token: 0x040000FC RID: 252
		private static readonly string[] KitCandle = new string[]
		{
			"p_candlegroup01x",
			"p_candle01x",
			"p_candlebot01x",
			"p_candles01x"
		};

		// Token: 0x040000FD RID: 253
		private static readonly string[] KitTrunk = new string[]
		{
			"p_trunk01x",
			"p_trunk04x"
		};

		// Token: 0x040000FE RID: 254
		private const CampKit.KitZone ZIn = CampKit.KitZone.Inner;

		// Token: 0x040000FF RID: 255
		private const CampKit.KitZone ZMid = CampKit.KitZone.Mid;

		// Token: 0x04000100 RID: 256
		private const CampKit.KitZone ZOut = CampKit.KitZone.Outer;

		// Token: 0x04000101 RID: 257
		private const CampKit.KitZone ZLine = CampKit.KitZone.Line;

		// Token: 0x04000102 RID: 258
		private static readonly CampKit.CampLook PlainCamp = new CampKit.CampLook
		{
			Shape = CampKit.KitShape.Ring,
			Pieces = new CampKit.KitPiece[]
			{
				CampKit.P("tent", CampKit.KitTents, 1, CampKit.KitZone.Outer, 2.5f, true, 100),
				CampKit.P("tent", CampKit.KitTents, 1, CampKit.KitZone.Outer, 2.5f, true, 60),
				CampKit.P("bedroll", CampKit.KitBedrolls, 3, CampKit.KitZone.Inner, 0.9f, true, 100),
				CampKit.P("seat", CampKit.KitSeats, 1, CampKit.KitZone.Inner, 0.5f, true, 100),
				CampKit.P("lantern", CampKit.KitLanterns, 1, CampKit.KitZone.Mid, 0.3f, false, 100),
				CampKit.P("wood", CampKit.KitWood, 1, CampKit.KitZone.Mid, 0.9f, false, 100),
				CampKit.P("crates", CampKit.KitCrates, 1, CampKit.KitZone.Mid, 1f, false, 100),
				CampKit.P("barrel", CampKit.KitBarrels, 2, CampKit.KitZone.Mid, 0.5f, false, 100)
			}
		};

		// Token: 0x04000103 RID: 259
		private static readonly Dictionary<string, CampKit.CampLook> CampLooks;

		// Token: 0x04000104 RID: 260
		[TupleElementNames(new string[]
		{
			"Wagons",
			"Count",
			"Horses"
		})]
		private static readonly Dictionary<string, ValueTuple<string[], int, int>> CampRigs;

		// Token: 0x04000105 RID: 261
		[TupleElementNames(new string[]
		{
			"Wagons",
			"Count",
			"Horses"
		})]
		private static readonly ValueTuple<string[], int, int> PlainRig;

		// Token: 0x04000106 RID: 262
		private readonly Dictionary<string, List<int>> _kits = new Dictionary<string, List<int>>();

		// Token: 0x04000107 RID: 263
		[TupleElementNames(new string[]
		{
			"At",
			"R"
		})]
		private readonly Dictionary<string, List<ValueTuple<Vector3, float>>> _spots = new Dictionary<string, List<ValueTuple<Vector3, float>>>();

		// Token: 0x04000108 RID: 264
		private readonly Dictionary<string, CampKit.Rig> _rigs = new Dictionary<string, CampKit.Rig>();

		// Token: 0x04000109 RID: 265
		private readonly Dictionary<string, Vector3> _centres = new Dictionary<string, Vector3>();

		// Token: 0x0400010A RID: 266
		[TupleElementNames(new string[]
		{
			"at",
			"until"
		})]
		private readonly Dictionary<string, ValueTuple<Vector3, int>> _held = new Dictionary<string, ValueTuple<Vector3, int>>();

		// Token: 0x0400010B RID: 267
		private readonly Dictionary<string, string> _model = new Dictionary<string, string>();

		// Token: 0x0400010C RID: 268
		private readonly HashSet<string> _logged = new HashSet<string>();

		// Token: 0x020000B3 RID: 179
		internal enum KitZone
		{
			// Token: 0x04000F85 RID: 3973
			Inner,
			// Token: 0x04000F86 RID: 3974
			Mid,
			// Token: 0x04000F87 RID: 3975
			Outer,
			// Token: 0x04000F88 RID: 3976
			Line
		}

		// Token: 0x020000B4 RID: 180
		internal enum KitShape
		{
			// Token: 0x04000F8A RID: 3978
			Ring,
			// Token: 0x04000F8B RID: 3979
			Row,
			// Token: 0x04000F8C RID: 3980
			Cluster,
			// Token: 0x04000F8D RID: 3981
			Scatter
		}

		// Token: 0x020000B5 RID: 181
		internal sealed class KitPiece
		{
			// Token: 0x04000F8E RID: 3982
			public string Piece;

			// Token: 0x04000F8F RID: 3983
			public string[] Names;

			// Token: 0x04000F90 RID: 3984
			public int Count;

			// Token: 0x04000F91 RID: 3985
			public int Chance;

			// Token: 0x04000F92 RID: 3986
			public CampKit.KitZone Zone;

			// Token: 0x04000F93 RID: 3987
			public float Size;

			// Token: 0x04000F94 RID: 3988
			public bool Faces;
		}

		// Token: 0x020000B6 RID: 182
		internal sealed class CampLook
		{
			// Token: 0x04000F95 RID: 3989
			public bool Fire = true;

			// Token: 0x04000F96 RID: 3990
			public CampKit.KitShape Shape;

			// Token: 0x04000F97 RID: 3991
			public CampKit.KitPiece[] Pieces;
		}

		// Token: 0x020000B7 RID: 183
		private sealed class Rig
		{
			// Token: 0x04000F98 RID: 3992
			public readonly List<Vehicle> Wagons = new List<Vehicle>();

			// Token: 0x04000F99 RID: 3993
			public readonly List<Ped> Horses = new List<Ped>();
		}
	}
}
