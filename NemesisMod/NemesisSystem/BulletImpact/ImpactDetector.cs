using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using RDR2;
using RDR2.Math;
using RDR2.Native;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x02000098 RID: 152
	internal sealed class ImpactDetector
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600144C RID: 5196 RVA: 0x00143E44 File Offset: 0x00142044
		// (remove) Token: 0x0600144D RID: 5197 RVA: 0x00143E7C File Offset: 0x0014207C
		public event Action<ImpactRecord> Impact;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600144E RID: 5198 RVA: 0x00143EB4 File Offset: 0x001420B4
		// (remove) Token: 0x0600144F RID: 5199 RVA: 0x00143EEC File Offset: 0x001420EC
		public event Action<string> Diagnostic;

		// Token: 0x06001450 RID: 5200 RVA: 0x00143F24 File Offset: 0x00142124
		public unsafe void Poll(Ped player)
		{
			this.TrackBlade(player);
			this.TrackFists(player);
			if (PED.IS_PED_SHOOTING(player.Handle))
			{
				uint playerShotWeapon = 0U;
				if (WEAPON.GET_CURRENT_PED_WEAPON(player.Handle, &playerShotWeapon, true, 0, true))
				{
					this._playerShotAt = Game.GameTime;
					this._playerShotWeapon = playerShotWeapon;
				}
			}
			int num = SCRIPTS.GET_NUMBER_OF_EVENTS(0);
			ulong* ptr = stackalloc ulong[(UIntPtr)72];
			int num2 = 0;
			List<int> list = new List<int>();
			for (int i = 0; i < num; i++)
			{
				uint num3 = SCRIPTS.GET_EVENT_AT_INDEX(0, i);
				if (num3 == 2787876538U)
				{
					*ptr = 0UL;
					if (SCRIPTS.GET_EVENT_DATA(0, i, ptr, 1))
					{
						num2++;
						list.Add((int)(*ptr));
					}
				}
				else if (num3 == 402722103U)
				{
					for (int j = 0; j < 9; j++)
					{
						ptr[j] = 0UL;
					}
					if (SCRIPTS.GET_EVENT_DATA(0, i, ptr, 9))
					{
						if ((int)ptr[1] != player.Handle)
						{
							if ((int)ptr[1] != 0 && (int)ptr[1] != (int)(*ptr))
							{
								this.EndHold((int)(*ptr), "hurt by someone else");
							}
						}
						else
						{
							uint num4 = (uint)ptr[2];
							if (num4 != 0U)
							{
								WeaponClass? weaponClass = ImpactDetector.BladeOf(num4);
								WeaponClass? weaponClass2 = (weaponClass == null) ? ImpactDetector.AreaOf(num4) : null;
								bool flag = weaponClass == null && weaponClass2 == null && WEAPON.GET_WEAPONTYPE_GROUP(num4) == 2685387236U;
								if (weaponClass == null && weaponClass2 == null && !flag && !ImpactDetector.IsProjectileWeapon(num4))
								{
									if (this._unknownLogged.Add(num4))
									{
										Action<string> diagnostic = this.Diagnostic;
										if (diagnostic != null)
										{
											string format = "DAMAGE by you with weapon {0:X8} ({1}) - not X-rayed";
											object arg = num4;
											object arg2;
											if (!Enum.IsDefined(typeof(eWeapon), num4))
											{
												arg2 = "?";
											}
											else
											{
												eWeapon eWeapon = num4;
												arg2 = eWeapon.ToString();
											}
											diagnostic(string.Format(format, arg, arg2));
										}
									}
								}
								else
								{
									bool flag2 = weaponClass == null && weaponClass2 == null && !flag && GunMelee.IsGunMeleeHit(player, (int)(*ptr), num4);
									if (!flag && !flag2)
									{
										this.EndHold((int)(*ptr), (weaponClass != null) ? "cut" : ((weaponClass2 != null) ? "burned / blown up" : "shot"));
									}
									Ped ped = Entity.FromHandle((int)(*ptr)) as Ped;
									bool flag3 = ped != null && !ped.IsHuman && !ped.IsPlayer;
									if (flag3)
									{
										AnimalBones.Probe(ped, ImpactDetector.AmmoNote);
										if (!ImpactDetector.FourLegged(ped))
										{
											goto IL_AA4;
										}
									}
									else if (ped == null || !ped.IsHuman || ped.IsPlayer || Kids.IsChild(ped))
									{
										goto IL_AA4;
									}
									if (flag || flag2)
									{
										int num5;
										if (!flag3 && (!this._blowAt.TryGetValue(ped.Handle, out num5) || Game.GameTime - num5 >= 250))
										{
											ValueTuple<int, float, bool> valueTuple = this.Reach(ped);
											int item = valueTuple.Item1;
											float item2 = valueTuple.Item2;
											bool item3 = valueTuple.Item3;
											float num6 = *(float*)(ptr + 4);
											bool flag4 = false;
											if (flag && PED.IS_PED_ON_MOUNT(player.Handle))
											{
												int num7 = PED.GET_MOUNT(player.Handle);
												float num8 = (num7 != 0) ? ENTITY.GET_ENTITY_SPEED(num7) : 0f;
												flag4 = (item3 && item2 < 0.9f && num8 < 4.5f);
												Action<string> ammoNote = ImpactDetector.AmmoNote;
												if (ammoNote != null)
												{
													ammoNote(string.Format("MOUNTED HIT ped={0} dmg={1:0.#} boot={2} gap={3:0.00} horse {4:0.0} m/s melee={5} - {6}", new object[]
													{
														ped.Handle,
														num6,
														item3,
														item2,
														num8,
														PED.IS_PED_IN_MELEE_COMBAT(player.Handle),
														flag4 ? "una patada desde la montura" : "not a kick (ridden into / no boot near)"
													}));
												}
												if (!flag4)
												{
													this._blowAt[ped.Handle] = Game.GameTime;
													goto IL_AA4;
												}
											}
											else if (flag && item2 > 0.35f && ImpactDetector.Bumped(player))
											{
												goto IL_AA4;
											}
											string text = this.NotABlow(ped);
											if (text == null && flag && (!item3 || item2 >= 0.35f) && !this.Held(ped.Handle) && (num6 < 6f || (ped.IsDead && num6 >= 30f)))
											{
												ValueTuple<bool, float> valueTuple2 = ImpactDetector.Grip(player, ped);
												bool item4 = valueTuple2.Item1;
												float item5 = valueTuple2.Item2;
												bool flag5 = (item4 && item5 < 0.45f) || item5 < 0.3f;
												if (flag5)
												{
													object obj = ped.IsDead && num6 >= 30f;
													ImpactDetector.Hold hold;
													if (!this._holds.TryGetValue(ped.Handle, out hold))
													{
														Dictionary<int, ImpactDetector.Hold> holds = this._holds;
														int handle = ped.Handle;
														ImpactDetector.Hold hold2 = new ImpactDetector.Hold();
														hold2.Since = Game.GameTime;
														hold2.Hp0 = ped.Health + (int)num6;
														hold2.HpLow = ped.Health;
														hold = hold2;
														holds[handle] = hold2;
													}
													object obj2 = obj;
													if (obj2 != null)
													{
														hold.Squeezed = true;
													}
													text = ((obj2 != null) ? "la asfixia (derribo)" : "el agarre (por la espalda)");
												}
												Action<string> ammoNote2 = ImpactDetector.AmmoNote;
												if (ammoNote2 != null)
												{
													ammoNote2(string.Format("TAKEDOWN? ped={0} dmg={1:0.#} behind={2} hands {3:0.00} m from his neck out={4} - {5}", new object[]
													{
														ped.Handle,
														num6,
														item4,
														item5,
														ped.IsDead,
														flag5 ? text : "a punch"
													}));
												}
											}
											if (text == null && flag && this.Held(ped.Handle))
											{
												ValueTuple<bool, float> valueTuple3 = ImpactDetector.Grip(player, ped);
												bool item6 = valueTuple3.Item1;
												float item7 = valueTuple3.Item2;
												bool flag6 = num6 < 8f || item6 || item7 < 0.3f;
												Action<string> ammoNote3 = ImpactDetector.AmmoNote;
												if (ammoNote3 != null)
												{
													ammoNote3(string.Format("HOLD HIT ped={0} dmg={1:0.#} behind={2} hands {3:0.00} m from his neck - {4}", new object[]
													{
														ped.Handle,
														num6,
														item6,
														item7,
														flag6 ? "la asfixia" : "a punch"
													}));
												}
												if (flag6)
												{
													this._holds[ped.Handle].Squeezed = true;
													text = "parte del agarre (la asfixia)";
												}
											}
											if (text != null)
											{
												Action<string> ammoNote4 = ImpactDetector.AmmoNote;
												if (ammoNote4 != null)
												{
													ammoNote4(string.Format("NOT A BLOW ped={0} dmg={1:0.#} - {2}", ped.Handle, num6, text));
												}
												this._blowAt[ped.Handle] = Game.GameTime;
												if (text == "el agarre" && !this._holds.ContainsKey(ped.Handle))
												{
													this._holds[ped.Handle] = new ImpactDetector.Hold
													{
														Since = Game.GameTime,
														Hp0 = ped.Health + (int)num6,
														HpLow = ped.Health
													};
												}
											}
											else
											{
												this._blowAt[ped.Handle] = Game.GameTime;
												ImpactDetector.Hold hold3;
												if (this._holds.TryGetValue(ped.Handle, out hold3))
												{
													hold3.Struck += (int)num6;
													hold3.LastPunchAt = Game.GameTime;
												}
												ImpactRecord impactRecord = this.CaptureBlow(player, ped, num4, flag2, num6, (item2 < 0.35f) ? item : -1, "event", flag4 || (flag && item3 && item2 < 0.35f), flag4, 0, false);
												ImpactRecord impactRecord2 = impactRecord;
												int shotId = this._shotId + 1;
												this._shotId = shotId;
												impactRecord2.ShotId = shotId;
												impactRecord.EventIndex = i;
												Action<ImpactRecord> impact = this.Impact;
												if (impact != null)
												{
													impact(impactRecord);
												}
											}
										}
									}
									else if (weaponClass2 != null)
									{
										int num9 = (weaponClass2.GetValueOrDefault() == WeaponClass.Burn) ? 6000 : 1500;
										int num10;
										if (!this._areaAt.TryGetValue(ped.Handle, out num10) || Game.GameTime - num10 >= num9)
										{
											this._areaAt[ped.Handle] = Game.GameTime;
											Vector3 at;
											at..ctor(*(float*)(ptr + 6), *(float*)(ptr + 7), *(float*)(ptr + 8));
											ImpactRecord impactRecord3 = this.CaptureArea(player, ped, num4, weaponClass2.Value, *(float*)(ptr + 4), at);
											impactRecord3.Animal = flag3;
											ImpactRecord impactRecord4 = impactRecord3;
											int shotId = this._shotId + 1;
											this._shotId = shotId;
											impactRecord4.ShotId = shotId;
											impactRecord3.EventIndex = i;
											Action<string> diagnostic2 = this.Diagnostic;
											if (diagnostic2 != null)
											{
												diagnostic2(string.Format("{0} ped={1} weapon {2}, {3} points, {4:0.0} m from it", new object[]
												{
													(weaponClass2.GetValueOrDefault() == WeaponClass.Burn) ? "BURN" : "BLAST",
													ped.Handle,
													impactRecord3.WeaponName,
													impactRecord3.PelletsOnVictim,
													impactRecord3.Distance
												}));
											}
											Action<ImpactRecord> impact2 = this.Impact;
											if (impact2 != null)
											{
												impact2(impactRecord3);
											}
										}
									}
									else if (weaponClass != null)
									{
										ImpactRecord impactRecord5 = this.CaptureStrike(player, ped, num4, weaponClass.Value, *(float*)(ptr + 4));
										impactRecord5.Animal = flag3;
										ImpactRecord impactRecord6 = impactRecord5;
										int shotId = this._shotId + 1;
										this._shotId = shotId;
										impactRecord6.ShotId = shotId;
										impactRecord5.EventIndex = i;
										Action<ImpactRecord> impact3 = this.Impact;
										if (impact3 != null)
										{
											impact3(impactRecord5);
										}
									}
									else
									{
										if (num4 != this._lastShotWeapon || Game.GameTime - this._lastShotTime > 150)
										{
											this._shotId++;
										}
										this._lastShotWeapon = num4;
										this._lastShotTime = Game.GameTime;
										ImpactRecord impactRecord7 = ImpactDetector.Capture(player, ped, num4, *(float*)(ptr + 4));
										impactRecord7.Animal = flag3;
										impactRecord7.ShotId = this._shotId;
										impactRecord7.EventIndex = i;
										impactRecord7.EventCoord = new Vector3(*(float*)(ptr + 6), *(float*)(ptr + 7), *(float*)(ptr + 8));
										this._damagedAt[ped.Handle] = Game.GameTime;
										Action<ImpactRecord> impact4 = this.Impact;
										if (impact4 != null)
										{
											impact4(impactRecord7);
										}
									}
								}
							}
						}
					}
				}
				IL_AA4:;
			}
			this.WatchBeaten(player);
			this.WatchHolds(player);
			this.WatchStomps(player);
			if (num2 > 0)
			{
				Action<string> diagnostic3 = this.Diagnostic;
				if (diagnostic3 != null)
				{
					diagnostic3(string.Format("EVENT_SHOT_FIRED_BULLET_IMPACT x{0} this frame, entities [{1}]", num2, string.Join<int>(",", list)));
				}
			}
			if (num2 > 0 && Game.GameTime - this._playerShotAt < 600)
			{
				foreach (int num11 in list)
				{
					if (num11 != 0 && !this._pendingImpact.ContainsKey(num11) && ENTITY.DOES_ENTITY_EXIST(num11) && ENTITY.IS_ENTITY_A_PED(num11) && !ENTITY.IS_ENTITY_DEAD(num11))
					{
						this._pendingImpact[num11] = new ValueTuple<int, uint>(Game.GameTime, this._playerShotWeapon);
					}
				}
			}
			foreach (KeyValuePair<int, ValueTuple<int, uint>> keyValuePair in this._pendingImpact.ToList<KeyValuePair<int, ValueTuple<int, uint>>>())
			{
				int num12;
				if (this._damagedAt.TryGetValue(keyValuePair.Key, out num12) && num12 >= keyValuePair.Value.Item1 - 50)
				{
					this._pendingImpact.Remove(keyValuePair.Key);
				}
				else if (Game.GameTime - keyValuePair.Value.Item1 >= 350)
				{
					this._pendingImpact.Remove(keyValuePair.Key);
					Ped ped2 = Entity.FromHandle(keyValuePair.Key) as Ped;
					if (!(ped2 == null) && ped2.Exists() && ped2.IsHuman && !ped2.IsPlayer && !Kids.IsChild(ped2) && keyValuePair.Value.Item2 != 0U && ImpactDetector.IsProjectileWeapon(keyValuePair.Value.Item2))
					{
						if (keyValuePair.Value.Item2 != this._lastShotWeapon || Game.GameTime - this._lastShotTime > 150)
						{
							this._shotId++;
						}
						this._lastShotWeapon = keyValuePair.Value.Item2;
						this._lastShotTime = Game.GameTime;
						ImpactRecord impactRecord8 = ImpactDetector.Capture(player, ped2, keyValuePair.Value.Item2, 0f);
						if (impactRecord8.ImpactOnVictim)
						{
							impactRecord8.ShotId = this._shotId;
							impactRecord8.EventIndex = -1;
							Action<string> diagnostic4 = this.Diagnostic;
							if (diagnostic4 != null)
							{
								diagnostic4(string.Format("FALLBACK capture ped={0}: bullet impact without a damage event (weapon {1:X8})", keyValuePair.Key, keyValuePair.Value.Item2));
							}
							Action<ImpactRecord> impact5 = this.Impact;
							if (impact5 != null)
							{
								impact5(impactRecord8);
							}
						}
					}
				}
			}
			if (this._damagedAt.Count > 64)
			{
				foreach (int key in (from kv in this._damagedAt
				where Game.GameTime - kv.Value > 5000
				select kv.Key).ToList<int>())
				{
					this._damagedAt.Remove(key);
				}
			}
			if (this._areaAt.Count > 64)
			{
				foreach (int key2 in (from kv in this._areaAt
				where Game.GameTime - kv.Value > 10000
				select kv.Key).ToList<int>())
				{
					this._areaAt.Remove(key2);
				}
			}
			if (this._blowAt.Count > 64)
			{
				foreach (int key3 in (from kv in this._blowAt
				where Game.GameTime - kv.Value > 5000
				select kv.Key).ToList<int>())
				{
					this._blowAt.Remove(key3);
				}
			}
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00144F08 File Offset: 0x00143108
		private static bool FourLegged(Ped p)
		{
			ImpactDetector.<>c__DisplayClass21_0 CS$<>8__locals1 = new ImpactDetector.<>c__DisplayClass21_0();
			CS$<>8__locals1.p = p;
			uint hash = (uint)CS$<>8__locals1.p.Model.Hash;
			bool flag;
			if (ImpactDetector._fourLegged.TryGetValue(hash, out flag))
			{
				return flag;
			}
			List<int> list = (from id in Anatomy.AllBones
			where PED.GET_PED_BONE_INDEX(CS$<>8__locals1.p.Handle, id) != -1
			select id).ToList<int>();
			flag = AnimalAnatomy.LooksFourLegged(list);
			ImpactDetector.<>c__DisplayClass21_0 CS$<>8__locals2 = CS$<>8__locals1;
			string mn;
			if (!Enum.IsDefined(typeof(PedHash), hash))
			{
				mn = "";
			}
			else
			{
				PedHash pedHash = hash;
				mn = pedHash.ToString().ToLowerInvariant();
			}
			CS$<>8__locals2.mn = mn;
			bool flag2 = ImpactDetector.Birds.Any((string k) => CS$<>8__locals1.mn.Contains(k));
			if (ImpactDetector.NotAnimals.Any((string k) => CS$<>8__locals1.mn.Contains(k)))
			{
				flag = false;
			}
			else if (flag2)
			{
				flag = (list.Contains(56200) && list.Contains(21030));
			}
			if (flag && !flag2 && list.Contains(56200) && list.Contains(21030))
			{
				Vector3 vector = PED.GET_PED_BONE_COORDS(CS$<>8__locals1.p.Handle, 56200, 0f, 0f, 0f);
				Vector3 vector2 = PED.GET_PED_BONE_COORDS(CS$<>8__locals1.p.Handle, 21030, 0f, 0f, 0f);
				if (vector.DistanceTo(vector2) < 0.25f)
				{
					flag = false;
				}
			}
			ImpactDetector._fourLegged[hash] = flag;
			Action<string> ammoNote = ImpactDetector.AmmoNote;
			if (ammoNote != null)
			{
				string str = "ANIMAL X-RAY ";
				string str2;
				if (!Enum.IsDefined(typeof(PedHash), hash))
				{
					str2 = string.Format("0x{0:X8}", hash);
				}
				else
				{
					PedHash pedHash = hash;
					str2 = pedHash.ToString();
				}
				ammoNote(str + str2 + ": " + (flag ? (flag2 ? "a bird - X-rayed" : "four-legged - X-rayed") : "not a four-legged skeleton - skipped"));
			}
			return flag;
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x001450F6 File Offset: 0x001432F6
		private static bool Bumped(Ped player)
		{
			return !PED.IS_PED_IN_MELEE_COMBAT(player.Handle) && (PED.IS_PED_ON_MOUNT(player.Handle) || ENTITY.GET_ENTITY_SPEED(player.Handle) > 2.6f);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00145128 File Offset: 0x00143328
		private void TrackFists(Ped player)
		{
			Vector3 item = PED.GET_PED_BONE_COORDS(player.Handle, 34606, 0f, 0f, 0f);
			Vector3 vector = PED.GET_PED_BONE_COORDS(player.Handle, 22798, 0f, 0f, 0f);
			int num = WEAPON.GET_CURRENT_PED_WEAPON_ENTITY_INDEX(player.Handle, 0);
			Vector3 item2 = PED.GET_PED_BONE_COORDS(player.Handle, 45454, 0f, 0f, 0f);
			Vector3 item3 = PED.GET_PED_BONE_COORDS(player.Handle, 33646, 0f, 0f, 0f);
			this._fists.Enqueue(new ValueTuple<int, Vector3, Vector3, Vector3, Vector3, Vector3>(Game.GameTime, item, vector, (num != 0) ? ENTITY.GET_ENTITY_COORDS(num, true, true) : vector, item2, item3));
			while (this._fists.Count > 48)
			{
				this._fists.Dequeue();
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00145208 File Offset: 0x00143408
		private static float BoneThickness(int id)
		{
			if (id == 21030)
			{
				return 0.1f;
			}
			if (id == 14412 || id == 14413 || id == 14414 || id == 14415)
			{
				return 0.13f;
			}
			if (id != 14410 && id != 14411 && id != 56200)
			{
				return 0.04f;
			}
			return 0.12f;
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x0014526C File Offset: 0x0014346C
		[return: TupleElementNames(new string[]
		{
			"bone",
			"gap",
			"foot"
		})]
		private ValueTuple<int, float, bool> Reach(Ped victim)
		{
			List<ValueTuple<int, Vector3>> list = new List<ValueTuple<int, Vector3>>();
			foreach (int num in ImpactDetector.StrikeBones)
			{
				if (PED.GET_PED_BONE_INDEX(victim.Handle, num) != -1)
				{
					list.Add(new ValueTuple<int, Vector3>(num, PED.GET_PED_BONE_COORDS(victim.Handle, num, 0f, 0f, 0f)));
				}
			}
			int item = -1;
			float num2 = 99f;
			bool item2 = false;
			foreach (ValueTuple<int, Vector3, Vector3, Vector3, Vector3, Vector3> valueTuple in this._fists)
			{
				if (Game.GameTime - valueTuple.Item1 <= 300)
				{
					foreach (ValueTuple<int, Vector3> valueTuple2 in list)
					{
						int item3 = valueTuple2.Item1;
						Vector3 item4 = valueTuple2.Item2;
						float num3 = ImpactDetector.BoneThickness(item3);
						Vector3 vector = valueTuple.Item2;
						float val = vector.DistanceTo(item4);
						vector = valueTuple.Item3;
						float val2 = vector.DistanceTo(item4);
						vector = valueTuple.Item4;
						float num4 = Math.Min(val, Math.Min(val2, vector.DistanceTo(item4))) - num3;
						vector = valueTuple.Item5;
						float val3 = vector.DistanceTo(item4);
						vector = valueTuple.Item6;
						float num5 = Math.Min(val3, vector.DistanceTo(item4)) - num3;
						if (num4 < num2)
						{
							num2 = num4;
							item = item3;
							item2 = false;
						}
						if (num5 < num2 - 0.05f)
						{
							num2 = num5;
							item = item3;
							item2 = true;
						}
					}
				}
			}
			return new ValueTuple<int, float, bool>(item, num2, item2);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00145448 File Offset: 0x00143648
		private void NoteDownState(Ped ped)
		{
			int handle = ped.Handle;
			string text = string.Format("fatal={0} cause={1:X8} source={2} ragdoll={3} health={4}/{5}", new object[]
			{
				PED.IS_PED_FATALLY_INJURED(handle),
				PED.GET_PED_CAUSE_OF_DEATH(handle),
				PED.GET_PED_SOURCE_OF_DEATH(handle),
				PED.IS_PED_RAGDOLL(handle),
				ped.Health,
				ped.MaxHealth
			});
			string text2;
			if (this._downState.TryGetValue(handle, out text2) && text2 == text)
			{
				return;
			}
			this._downState[handle] = text;
			if (this._downState.Count > 32)
			{
				this._downState.Clear();
			}
			Action<string> ammoNote = ImpactDetector.AmmoNote;
			if (ammoNote == null)
			{
				return;
			}
			ammoNote(string.Format("DOWNSTATE ped={0} {1}{2}", handle, text, (text2 == null) ? " (first)" : (" (was " + text2 + ")")));
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0014553C File Offset: 0x0014373C
		private void WatchStomps(Ped player)
		{
			if (this._struckAt.Count == 0)
			{
				return;
			}
			bool flag = !PED.IS_PED_ON_MOUNT(player.Handle) && !PED.IS_PED_IN_ANY_VEHICLE(player.Handle, false);
			foreach (int num in this._struckAt.Keys.ToList<int>())
			{
				int num2 = Game.GameTime - this._struckAt[num];
				Ped ped = Entity.FromHandle(num) as Ped;
				if (ped == null || !ped.Exists() || num2 > 300000 || Downed.IsFinished(num))
				{
					this._struckAt.Remove(num);
					this._stompAt.Remove(num);
					this._stompNoteAt.Remove(num);
				}
				else if (!ped.IsDead)
				{
					if (num2 > 30000)
					{
						this._struckAt.Remove(num);
					}
				}
				else if (!NemesisMod.LooksKnockedOut(ped))
				{
					if (PED.GET_PED_CAUSE_OF_DEATH(num) != 0U)
					{
						this._struckAt.Remove(num);
						this._stompAt.Remove(num);
						Action<string> ammoNote = ImpactDetector.AmmoNote;
						if (ammoNote != null)
						{
							ammoNote(string.Format("STOMP watch off ped={0} - killed, not out cold", num));
						}
					}
				}
				else
				{
					this.NoteDownState(ped);
					int num3;
					int num4;
					if (flag && ENTITY.GET_ENTITY_SPEED(player.Handle) <= 1.5f && ped.Position.DistanceTo(player.Position) <= 2.5f && (!this._stompAt.TryGetValue(num, out num3) || Game.GameTime - num3 >= 450) && (!this._blowAt.TryGetValue(num, out num4) || Game.GameTime - num4 >= 450))
					{
						ValueTuple<int, float, float, float> valueTuple = this.BootDown(ped);
						int item = valueTuple.Item1;
						float item2 = valueTuple.Item2;
						float item3 = valueTuple.Item3;
						float item4 = valueTuple.Item4;
						if (item >= 0 && item2 <= 0.8f && item4 >= 0.90000004f)
						{
							bool flag2 = item2 < 0.35f && item4 >= 1.5f && item3 >= 0.2f;
							int num5;
							if (flag2 || !this._stompNoteAt.TryGetValue(num, out num5) || Game.GameTime - num5 > 1000)
							{
								if (!flag2)
								{
									this._stompNoteAt[num] = Game.GameTime;
								}
								Action<string> ammoNote2 = ImpactDetector.AmmoNote;
								if (ammoNote2 != null)
								{
									ammoNote2(string.Format("STOMP{0} ped={1} boot {2:0.00} m off {3}, came down {4:0.0} m/s from {5:0.00} m up{6}", new object[]
									{
										flag2 ? "" : "?",
										num,
										item2,
										item,
										item4,
										item3,
										flag2 ? "" : " - not counted"
									}));
								}
							}
							if (flag2)
							{
								this._stompAt[num] = Game.GameTime;
								this._blowAt[num] = Game.GameTime;
								ImpactRecord impactRecord = this.CaptureBlow(player, ped, 2725352035U, false, 0f, item, "pisotón", false, false, 0, true);
								ImpactRecord impactRecord2 = impactRecord;
								int shotId = this._shotId + 1;
								this._shotId = shotId;
								impactRecord2.ShotId = shotId;
								impactRecord.EventIndex = -2;
								Action<ImpactRecord> impact = this.Impact;
								if (impact != null)
								{
									impact(impactRecord);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001458 RID: 5208 RVA: 0x001458E0 File Offset: 0x00143AE0
		[return: TupleElementNames(new string[]
		{
			"bone",
			"gap",
			"lift",
			"speed"
		})]
		private ValueTuple<int, float, float, float> BootDown(Ped victim)
		{
			if (this._fists.Count < 3)
			{
				return new ValueTuple<int, float, float, float>(-1, 99f, 0f, 0f);
			}
			ValueTuple<int, Vector3, Vector3, Vector3, Vector3, Vector3>[] array = this._fists.ToArray();
			ValueTuple<int, Vector3, Vector3, Vector3, Vector3, Vector3> valueTuple = array[array.Length - 1];
			int num = -1;
			float num2 = 99f;
			bool flag = true;
			Vector3 vector = Vector3.Zero;
			foreach (int num3 in ImpactDetector.StrikeBones)
			{
				if (PED.GET_PED_BONE_INDEX(victim.Handle, num3) != -1)
				{
					Vector3 vector2 = PED.GET_PED_BONE_COORDS(victim.Handle, num3, 0f, 0f, 0f);
					float num4 = ImpactDetector.BoneThickness(num3);
					float num5 = valueTuple.Item5.DistanceTo(vector2) - num4;
					float num6 = valueTuple.Item6.DistanceTo(vector2) - num4;
					if (num5 < num2)
					{
						num2 = num5;
						num = num3;
						flag = true;
						vector = vector2;
					}
					if (num6 < num2)
					{
						num2 = num6;
						num = num3;
						flag = false;
						vector = vector2;
					}
				}
			}
			if (num < 0)
			{
				return new ValueTuple<int, float, float, float>(-1, 99f, 0f, 0f);
			}
			float num7 = 0f;
			float num8 = 0f;
			foreach (ValueTuple<int, Vector3, Vector3, Vector3, Vector3, Vector3> valueTuple2 in array)
			{
				if (valueTuple.Item1 - valueTuple2.Item1 <= 400)
				{
					Vector3 vector3 = flag ? valueTuple2.Item5 : valueTuple2.Item6;
					num7 = Math.Max(num7, vector3.Z - vector.Z);
					int num9 = valueTuple.Item1 - valueTuple2.Item1;
					if (num9 >= 100 && num9 <= 200)
					{
						num8 = Math.Max(num8, (vector3.Z - (flag ? valueTuple.Item5 : valueTuple.Item6).Z) / ((float)num9 / 1000f));
					}
				}
			}
			return new ValueTuple<int, float, float, float>(num, num2, num7, num8);
		}

		// Token: 0x06001459 RID: 5209 RVA: 0x00145AD4 File Offset: 0x00143CD4
		[return: TupleElementNames(new string[]
		{
			"behind",
			"hands"
		})]
		private static ValueTuple<bool, float> Grip(Ped player, Ped victim)
		{
			Vector3 vector = ENTITY.GET_ENTITY_FORWARD_VECTOR(victim.Handle);
			Vector3 vector2 = player.Position - victim.Position;
			vector2.Z = 0f;
			bool item = vector2.Length() > 0.05f && (vector.X * vector2.X + vector.Y * vector2.Y) / vector2.Length() < -0.3f;
			Vector3 vector3 = PED.GET_PED_BONE_COORDS(victim.Handle, 14285, 0f, 0f, 0f);
			float val = PED.GET_PED_BONE_COORDS(player.Handle, 34606, 0f, 0f, 0f).DistanceTo(vector3);
			float val2 = PED.GET_PED_BONE_COORDS(player.Handle, 22798, 0f, 0f, 0f).DistanceTo(vector3);
			return new ValueTuple<bool, float>(item, Math.Max(val, val2));
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00145BC8 File Offset: 0x00143DC8
		private bool Held(int h)
		{
			return this._holds.ContainsKey(h) && PED.GET_PED_IS_BEING_GRAPPLED(h);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00145BE0 File Offset: 0x00143DE0
		private void EndHold(int h, string why)
		{
			if (!this._holds.Remove(h))
			{
				return;
			}
			Action<string> ammoNote = ImpactDetector.AmmoNote;
			if (ammoNote == null)
			{
				return;
			}
			ammoNote(string.Format("HOLD ped={0} ended - {1}: not a choke", h, why));
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00145C14 File Offset: 0x00143E14
		private void WatchHolds(Ped player)
		{
			if (this._holds.Count == 0)
			{
				return;
			}
			foreach (int num in this._holds.Keys.ToList<int>())
			{
				ImpactDetector.Hold hold = this._holds[num];
				Ped ped = Entity.FromHandle(num) as Ped;
				if (ped == null || !ped.Exists())
				{
					this._holds.Remove(num);
				}
				else
				{
					bool isDead = ped.IsDead;
					bool flag = PED.GET_PED_IS_BEING_GRAPPLED(num);
					if (flag || isDead)
					{
						hold.HpLow = Math.Min(hold.HpLow, isDead ? 0 : ped.Health);
					}
					int num2 = Game.GameTime - hold.Since;
					if (isDead || (!flag && num2 >= 4000) || num2 >= 60000)
					{
						this._holds.Remove(num);
						int num3 = Math.Max(0, hold.Hp0 - hold.HpLow - hold.Struck);
						bool flag2 = hold.Squeezed && (num3 >= 15 || (isDead && Game.GameTime - hold.LastPunchAt > 1500));
						Action<string> ammoNote = ImpactDetector.AmmoNote;
						if (ammoNote != null)
						{
							ammoNote(string.Format("HOLD ped={0} held {1:0.0} s, health {2} -> {3} (punches {4}){5}{6} - {7}", new object[]
							{
								num,
								(float)num2 / 1000f,
								hold.Hp0,
								hold.HpLow,
								hold.Struck,
								isDead ? ", went limp" : "",
								hold.Squeezed ? ", squeezed" : "",
								flag2 ? "a choke" : "not a choke"
							}));
						}
						if (flag2)
						{
							this._blowAt[num] = Game.GameTime;
							ImpactRecord impactRecord = this.CaptureBlow(player, ped, 2725352035U, false, (float)num3, 14285, "hold", false, false, Math.Max(1, num2), false);
							ImpactRecord impactRecord2 = impactRecord;
							int shotId = this._shotId + 1;
							this._shotId = shotId;
							impactRecord2.ShotId = shotId;
							impactRecord.EventIndex = -2;
							Action<ImpactRecord> impact = this.Impact;
							if (impact != null)
							{
								impact(impactRecord);
							}
						}
					}
				}
			}
		}

		// Token: 0x0600145D RID: 5213 RVA: 0x00145E98 File Offset: 0x00144098
		private string NotABlow(Ped v)
		{
			int handle = v.Handle;
			int num = Function.Call<int>(7030473103003024859UL, new InputArgument[]
			{
				handle
			});
			if (num == 1)
			{
				return "ser atado";
			}
			if (num >= 4)
			{
				return "ser cargado / dejado en el suelo / desatado";
			}
			if (PED.GET_PED_IS_BEING_GRAPPLED(handle))
			{
				int num2;
				if (!this._grabbedSince.TryGetValue(handle, out num2))
				{
					num2 = (this._grabbedSince[handle] = Game.GameTime);
				}
				if (Game.GameTime - num2 < 1500)
				{
					return "el agarre";
				}
			}
			else
			{
				this._grabbedSince.Remove(handle);
			}
			if (this._grabbedSince.Count > 32)
			{
				this._grabbedSince.Clear();
			}
			return null;
		}

		// Token: 0x0600145E RID: 5214 RVA: 0x00145F48 File Offset: 0x00144148
		private static string HowHeWas(Ped v)
		{
			int num = Function.Call<int>(7030473103003024859UL, new InputArgument[]
			{
				v.Handle
			});
			if (v.IsHogtied)
			{
				return ", estaba atado";
			}
			if (PED.GET_PED_IS_BEING_GRAPPLED(v.Handle))
			{
				return ", lo sujetabas";
			}
			if (!v.IsRagdoll && num != 2 && num != 3)
			{
				return "";
			}
			return ", estaba en el suelo";
		}

		// Token: 0x0600145F RID: 5215 RVA: 0x00145FB4 File Offset: 0x001441B4
		private unsafe void WatchBeaten(Ped player)
		{
			if (this._beaten.Count == 0)
			{
				return;
			}
			foreach (int num in this._beaten.Keys.ToList<int>())
			{
				ValueTuple<int, int> valueTuple = this._beaten[num];
				Ped ped = Entity.FromHandle(num) as Ped;
				if (ped == null || !ped.Exists() || ped.IsDead || Game.GameTime - valueTuple.Item2 > 20000)
				{
					this._beaten.Remove(num);
				}
				else
				{
					int health = ped.Health;
					this._beaten[num] = new ValueTuple<int, int>(health, valueTuple.Item2);
					int num2;
					if (health <= valueTuple.Item1 - 2 && (!this._blowAt.TryGetValue(num, out num2) || Game.GameTime - num2 >= 250))
					{
						uint num3 = 0U;
						if (WEAPON.GET_CURRENT_PED_WEAPON(player.Handle, &num3, true, 0, true))
						{
							bool flag = WEAPON.GET_WEAPONTYPE_GROUP(num3) == 2685387236U;
							bool flag2 = !flag && GunMelee.IsGun(num3);
							if ((flag || flag2) && PED.IS_PED_IN_MELEE_COMBAT(player.Handle) && (!flag2 || !GunMelee.PlayerFiredRecently) && this.NotABlow(ped) == null && !this.Held(num))
							{
								ValueTuple<int, float, bool> valueTuple2 = this.Reach(ped);
								int item = valueTuple2.Item1;
								float item2 = valueTuple2.Item2;
								bool item3 = valueTuple2.Item3;
								if (item2 <= 0.3f)
								{
									this._blowAt[num] = Game.GameTime;
									ImpactRecord impactRecord = this.CaptureBlow(player, ped, num3, flag2, (float)(valueTuple.Item1 - health), item, "health", flag && item3, false, 0, false);
									ImpactRecord impactRecord2 = impactRecord;
									int shotId = this._shotId + 1;
									this._shotId = shotId;
									impactRecord2.ShotId = shotId;
									impactRecord.EventIndex = -2;
									Action<ImpactRecord> impact = this.Impact;
									if (impact != null)
									{
										impact(impactRecord);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001460 RID: 5216 RVA: 0x001461E4 File Offset: 0x001443E4
		private unsafe ImpactRecord CaptureBlow(Ped player, Ped victim, uint weapon, bool butt, float damage, int struck, string via, bool kick = false, bool saddle = false, int heldMs = 0, bool stomp = false)
		{
			bool flag = heldMs > 0;
			this._struckAt[victim.Handle] = Game.GameTime;
			ImpactRecord impactRecord = new ImpactRecord();
			impactRecord.PedHandle = victim.Handle;
			impactRecord.PedModel = (uint)victim.Model.Hash;
			impactRecord.VictimDeadAfterHit = victim.IsDead;
			impactRecord.WeaponHash = weapon;
			string weaponName;
			if (!flag)
			{
				if (!butt || !Enum.IsDefined(typeof(eWeapon), weapon))
				{
					weaponName = (((kick || stomp) && !butt) ? "Botas" : "Fists");
				}
				else
				{
					eWeapon eWeapon = weapon;
					weaponName = eWeapon.ToString();
				}
			}
			else
			{
				weaponName = "Manos desnudas";
			}
			impactRecord.WeaponName = weaponName;
			impactRecord.Damage = damage;
			impactRecord.GameTime = Game.GameTime;
			impactRecord.Butt = (butt && !flag);
			impactRecord.Kick = ((kick || stomp) && !butt && !flag);
			impactRecord.Choke = flag;
			impactRecord.HeldMs = heldMs;
			impactRecord.Stomp = stomp;
			ImpactRecord impactRecord2 = impactRecord;
			int num;
			if (PED.GET_PED_LAST_DAMAGE_BONE(victim.Handle, &num))
			{
				impactRecord2.BoneId = num;
				impactRecord2.BoneIndex = PED.GET_PED_BONE_INDEX(victim.Handle, num);
			}
			ImpactRecord impactRecord3 = impactRecord2;
			string boneName;
			if (impactRecord2.BoneId >= 0)
			{
				BoneID boneId = impactRecord2.BoneId;
				boneName = boneId.ToString();
			}
			else
			{
				boneName = "none";
			}
			impactRecord3.BoneName = boneName;
			if (via == "event" && impactRecord2.BoneId >= 0 && impactRecord2.BoneId != 56200 && PED.GET_PED_BONE_INDEX(victim.Handle, impactRecord2.BoneId) != -1)
			{
				struck = -1;
			}
			Vector3 vector;
			if (struck >= 0)
			{
				impactRecord2.BlowBone = struck;
				vector = PED.GET_PED_BONE_COORDS(victim.Handle, struck, 0f, 0f, 0f);
			}
			else
			{
				vector = ((impactRecord2.BoneId >= 0) ? PED.GET_PED_BONE_COORDS(victim.Handle, impactRecord2.BoneId, 0f, 0f, 0f) : (victim.Position + new Vector3(0f, 0f, 0.5f)));
				float num2 = float.MaxValue;
				foreach (int num3 in ImpactDetector.StrikeBones)
				{
					if (PED.GET_PED_BONE_INDEX(victim.Handle, num3) != -1)
					{
						float num4 = PED.GET_PED_BONE_COORDS(victim.Handle, num3, 0f, 0f, 0f).DistanceTo(vector);
						if (num4 < num2)
						{
							num2 = num4;
							impactRecord2.BlowBone = num3;
						}
					}
				}
			}
			if (via == "event" || !this._beaten.ContainsKey(victim.Handle))
			{
				this._beaten[victim.Handle] = new ValueTuple<int, int>(victim.Health, Game.GameTime);
			}
			else
			{
				this._beaten[victim.Handle] = new ValueTuple<int, int>(victim.Health, this._beaten[victim.Handle].Item2);
			}
			Vector3 vector2 = PED.GET_PED_BONE_COORDS(player.Handle, 14413, 0f, 0f, 0f);
			Vector3 vector3 = ImpactDetector.Norm(vector - vector2, victim.Position - player.Position);
			impactRecord2.HasImpact = true;
			impactRecord2.Impact = vector;
			impactRecord2.Muzzle = vector - vector3 * 0.6f;
			int num5 = Function.Call<int>(7030473103003024859UL, new InputArgument[]
			{
				victim.Handle
			});
			bool isHogtied = victim.IsHogtied;
			bool flag2 = victim.IsRagdoll || victim.IsDead || num5 == 2 || num5 == 3;
			int num6;
			bool flag3 = this._lyingAt.TryGetValue(victim.Handle, out num6) && Game.GameTime - num6 < 4000;
			impactRecord2.Down = (stomp || isHogtied || (flag2 && flag3));
			if (stomp || isHogtied || flag2)
			{
				this._lyingAt[victim.Handle] = Game.GameTime;
			}
			else
			{
				this._lyingAt.Remove(victim.Handle);
			}
			if (this._lyingAt.Count > 64)
			{
				foreach (int key in (from kv in this._lyingAt
				where Game.GameTime - kv.Value > 4000
				select kv.Key).ToList<int>())
				{
					this._lyingAt.Remove(key);
				}
			}
			impactRecord2.MuzzleSource = (flag ? string.Format("asfixia, sostenida {0:0.0} s", (float)heldMs / 1000f) : string.Concat(new string[]
			{
				butt ? "culata del arma" : (stomp ? "pisotón" : (impactRecord2.Kick ? (saddle ? "patada desde la montura" : "patada") : "puño")),
				", ",
				via,
				(struck >= 0) ? "" : ", game's bone",
				ImpactDetector.HowHeWas(victim)
			}));
			impactRecord2.Distance = player.Position.DistanceTo(victim.Position);
			impactRecord2.Class = WeaponClass.Blunt;
			ImpactDetector.SnapshotSkeleton(victim, impactRecord2);
			return impactRecord2;
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x0014676C File Offset: 0x0014496C
		private static WeaponClass? AreaOf(uint weapon)
		{
			if (ImpactDetector._hExplosion == 0U)
			{
				ImpactDetector._hExplosion = MISC.GET_HASH_KEY("WEAPON_EXPLOSION");
				ImpactDetector._hFire = MISC.GET_HASH_KEY("WEAPON_FIRE");
				ImpactDetector._hMolotov = MISC.GET_HASH_KEY("WEAPON_THROWN_MOLOTOV");
				ImpactDetector._hDynamite = MISC.GET_HASH_KEY("WEAPON_THROWN_DYNAMITE");
			}
			if (weapon == ImpactDetector._hExplosion || weapon == ImpactDetector._hDynamite)
			{
				return new WeaponClass?(WeaponClass.Blast);
			}
			if (weapon == ImpactDetector._hFire || weapon == ImpactDetector._hMolotov)
			{
				return new WeaponClass?(WeaponClass.Burn);
			}
			string text;
			if (!Enum.IsDefined(typeof(eWeapon), weapon))
			{
				text = "";
			}
			else
			{
				eWeapon eWeapon = weapon;
				text = eWeapon.ToString();
			}
			string text2 = text;
			if (text2.Contains("Dinamita") || text2.Contains("Explos") || text2.Contains("Volatile"))
			{
				return new WeaponClass?(WeaponClass.Blast);
			}
			if (text2.Contains("Molotov") || text2.Contains("FireBottle") || (text2.Contains("Fire") && !text2.Contains("Firework")))
			{
				return new WeaponClass?(WeaponClass.Burn);
			}
			return null;
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00146890 File Offset: 0x00144A90
		private unsafe ImpactRecord CaptureArea(Ped player, Ped victim, uint weapon, WeaponClass kind, float damage, Vector3 at)
		{
			ImpactRecord impactRecord = new ImpactRecord();
			impactRecord.PedHandle = victim.Handle;
			impactRecord.PedModel = (uint)victim.Model.Hash;
			impactRecord.VictimDeadAfterHit = victim.IsDead;
			impactRecord.WeaponHash = weapon;
			string weaponName;
			if (!Enum.IsDefined(typeof(eWeapon), weapon))
			{
				weaponName = ((kind == WeaponClass.Blast) ? "Explosion" : "Fire");
			}
			else
			{
				eWeapon eWeapon = weapon;
				weaponName = eWeapon.ToString();
			}
			impactRecord.WeaponName = weaponName;
			impactRecord.Damage = damage;
			impactRecord.GameTime = Game.GameTime;
			ImpactRecord impactRecord2 = impactRecord;
			int num;
			if (PED.GET_PED_LAST_DAMAGE_BONE(victim.Handle, &num))
			{
				impactRecord2.BoneId = num;
				impactRecord2.BoneIndex = PED.GET_PED_BONE_INDEX(victim.Handle, num);
			}
			ImpactRecord impactRecord3 = impactRecord2;
			string boneName;
			if (impactRecord2.BoneId >= 0)
			{
				BoneID boneId = impactRecord2.BoneId;
				boneName = boneId.ToString();
			}
			else
			{
				boneName = "none";
			}
			impactRecord3.BoneName = boneName;
			Vector3 vector = (impactRecord2.BoneId >= 0) ? PED.GET_PED_BONE_COORDS(victim.Handle, impactRecord2.BoneId, 0f, 0f, 0f) : (victim.Position + new Vector3(0f, 0f, 0.4f));
			Vector3 vector2;
			if (at != Vector3.Zero && at.DistanceTo(victim.Position) < 6f && at.DistanceTo(vector) > 0.3f)
			{
				vector2 = at;
			}
			else
			{
				Vector3 vector3 = ImpactDetector.Norm(player.Position - victim.Position, new Vector3(0f, 1f, 0f));
				vector2 = victim.Position + new Vector3(vector3.X, vector3.Y, 0f) * 1.5f + new Vector3(0f, 0f, (kind == WeaponClass.Burn) ? 0.4f : 0.1f);
			}
			impactRecord2.HasImpact = true;
			impactRecord2.Impact = vector;
			impactRecord2.Muzzle = vector2;
			impactRecord2.MuzzleSource = ((at != Vector3.Zero && vector2 == at) ? "event coords" : "your side of him");
			impactRecord2.Distance = vector2.DistanceTo(vector);
			impactRecord2.Class = kind;
			impactRecord2.PelletsOnVictim = ((kind == WeaponClass.Burn) ? 10 : Math.Max(6, Math.Min(16, 6 + (int)(damage / 15f))));
			ImpactDetector.SnapshotSkeleton(victim, impactRecord2);
			return impactRecord2;
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00146B08 File Offset: 0x00144D08
		private void TrackBlade(Ped player)
		{
			int num = WEAPON.GET_CURRENT_PED_WEAPON_ENTITY_INDEX(player.Handle, 0);
			Vector3 item = (num != 0) ? ENTITY.GET_ENTITY_COORDS(num, true, true) : PED.GET_PED_BONE_COORDS(player.Handle, 22798, 0f, 0f, 0f);
			this._blade.Enqueue(new ValueTuple<int, Vector3>(Game.GameTime, item));
			while (this._blade.Count > 12)
			{
				this._blade.Dequeue();
			}
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00146B84 File Offset: 0x00144D84
		private Vector3 BladeMotion()
		{
			if (this._blade.Count < 2)
			{
				return Vector3.Zero;
			}
			ValueTuple<int, Vector3>[] array = this._blade.ToArray();
			ValueTuple<int, Vector3> valueTuple = array[array.Length - 1];
			ValueTuple<int, Vector3> valueTuple2 = array[0];
			foreach (ValueTuple<int, Vector3> valueTuple3 in array)
			{
				if (valueTuple.Item1 - valueTuple3.Item1 <= 150)
				{
					valueTuple2 = valueTuple3;
					break;
				}
			}
			return valueTuple.Item2 - valueTuple2.Item2;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00146C08 File Offset: 0x00144E08
		private static WeaponClass? BladeOf(uint weapon)
		{
			if (!Enum.IsDefined(typeof(eWeapon), weapon))
			{
				return null;
			}
			eWeapon eWeapon = weapon;
			string text = eWeapon.ToString();
			if (!text.StartsWith("Melee") && !text.StartsWith("Thrown"))
			{
				return null;
			}
			if (text.Contains("Knife") || text.Contains("Knives"))
			{
				return new WeaponClass?(WeaponClass.Stab);
			}
			if (text.Contains("Machete") || text.Contains("Hatchet") || text.Contains("Cleaver") || text.Contains("Tomahawk") || text.Contains("Sword"))
			{
				return new WeaponClass?(WeaponClass.Chop);
			}
			return null;
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x00146CE0 File Offset: 0x00144EE0
		private unsafe ImpactRecord CaptureStrike(Ped player, Ped victim, uint weapon, WeaponClass kind, float damage)
		{
			ImpactRecord impactRecord = new ImpactRecord();
			impactRecord.PedHandle = victim.Handle;
			impactRecord.PedModel = (uint)victim.Model.Hash;
			impactRecord.VictimDeadAfterHit = victim.IsDead;
			impactRecord.WeaponHash = weapon;
			eWeapon eWeapon = weapon;
			impactRecord.WeaponName = eWeapon.ToString();
			impactRecord.Ammo = ImpactDetector.AmmoOf(player, weapon);
			impactRecord.Damage = damage;
			impactRecord.GameTime = Game.GameTime;
			ImpactRecord impactRecord2 = impactRecord;
			int num;
			if (PED.GET_PED_LAST_DAMAGE_BONE(victim.Handle, &num))
			{
				impactRecord2.BoneId = num;
				impactRecord2.BoneIndex = PED.GET_PED_BONE_INDEX(victim.Handle, num);
			}
			ImpactRecord impactRecord3 = impactRecord2;
			string boneName;
			if (impactRecord2.BoneId >= 0)
			{
				BoneID boneId = impactRecord2.BoneId;
				boneName = boneId.ToString();
			}
			else
			{
				boneName = "none";
			}
			impactRecord3.BoneName = boneName;
			Vector3 vector = (impactRecord2.BoneId >= 0) ? PED.GET_PED_BONE_COORDS(victim.Handle, impactRecord2.BoneId, 0f, 0f, 0f) : (victim.Position + new Vector3(0f, 0f, 0.4f));
			Vector3 vector2 = PED.GET_PED_BONE_COORDS(player.Handle, 14413, 0f, 0f, 0f);
			Vector3 vector3 = ImpactDetector.Norm(vector - vector2, victim.Position - player.Position);
			bool flag = impactRecord2.WeaponName.StartsWith("Thrown");
			Vector3 vector4 = flag ? Vector3.Zero : this.BladeMotion();
			bool flag2 = vector4.Length() > 0.05f;
			Vector3 vector5;
			vector5..ctor(0f, 0f, 1f);
			float num2 = Vector3.Dot(vector4, vector3);
			Vector3 vector6 = vector4 - vector3 * num2;
			if (kind == WeaponClass.Stab && !flag && !(flag2 ? (num2 >= vector6.Length() * 0.8f) : (impactRecord2.BoneName.Contains("Spine") || impactRecord2.BoneName.Contains("Pelvis"))))
			{
				kind = WeaponClass.Slash;
			}
			Vector3 vector7;
			if (kind == WeaponClass.Stab)
			{
				vector7 = ((flag2 && num2 > 0f) ? ImpactDetector.Norm(vector3 + ImpactDetector.Norm(vector4, vector3) * 0.4f, vector3) : vector3);
			}
			else if (kind == WeaponClass.Slash)
			{
				vector7 = ImpactDetector.Norm(ImpactDetector.Norm((vector6.Length() > 0.03f) ? vector6 : Vector3.Cross(vector3, vector5), vector3) * 0.85f + vector3 * 0.5f, vector3);
			}
			else
			{
				vector7 = (flag2 ? ImpactDetector.Norm(ImpactDetector.Norm(vector4, vector3) * 0.7f + vector3 * 0.5f, vector3) : ImpactDetector.Norm(vector3 - vector5 * 0.5f, vector3));
			}
			if (kind == WeaponClass.Slash)
			{
				vector -= vector3 * 0.045f;
			}
			else if (kind == WeaponClass.Chop)
			{
				vector -= vector3 * 0.02f;
			}
			impactRecord2.HasImpact = true;
			impactRecord2.Impact = vector;
			impactRecord2.Muzzle = vector - vector7 * 0.6f;
			impactRecord2.MuzzleSource = (flag2 ? string.Format("blade motion {0:0.00} m", vector4.Length()) : "approach only");
			impactRecord2.Distance = player.Position.DistanceTo(victim.Position);
			impactRecord2.Class = kind;
			ImpactDetector.SnapshotSkeleton(victim, impactRecord2);
			return impactRecord2;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00147074 File Offset: 0x00145274
		private static Vector3 Norm(Vector3 v, Vector3 fallback)
		{
			float num = v.Length();
			if (num > 0.0001f)
			{
				return v * (1f / num);
			}
			num = fallback.Length();
			if (num <= 0.0001f)
			{
				return new Vector3(0f, 1f, 0f);
			}
			return fallback * (1f / num);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x001470D4 File Offset: 0x001452D4
		internal static AmmoKind AmmoOf(Ped player, uint weapon)
		{
			uint num = 0U;
			uint num2 = 0U;
			try
			{
				int num3 = WEAPON.GET_CURRENT_PED_WEAPON_ENTITY_INDEX(player.Handle, 0);
				if (num3 != 0)
				{
					num = Function.Call<uint>(9113906463975140883UL, new InputArgument[]
					{
						player.Handle,
						num3
					});
				}
			}
			catch
			{
			}
			try
			{
				num2 = WEAPON.GET_PED_AMMO_TYPE_FROM_WEAPON(player.Handle, weapon);
			}
			catch
			{
			}
			AmmoKind ammoKind = Ammo.FromHash(num);
			if (ammoKind == AmmoKind.Regular)
			{
				ammoKind = Ammo.FromHash(num2);
			}
			string text = string.Format("{0:X8}/{1:X8}", num, num2);
			if (text != ImpactDetector._lastAmmoKey)
			{
				ImpactDetector._lastAmmoKey = text;
				Action<string> ammoNote = ImpactDetector.AmmoNote;
				if (ammoNote != null)
				{
					ammoNote(string.Format("AMMO read: weapon object {0}, weapon {1} -> {2}", ImpactDetector.Name(num), ImpactDetector.Name(num2), ammoKind));
				}
			}
			return ammoKind;
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x001471C0 File Offset: 0x001453C0
		private static string Name(uint h)
		{
			if (h == 0U)
			{
				return "none";
			}
			if (!Enum.IsDefined(typeof(eAmmoType), h))
			{
				return string.Format("0x{0:X8}", h);
			}
			eAmmoType eAmmoType = h;
			return eAmmoType.ToString();
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00147210 File Offset: 0x00145410
		private static bool IsProjectileWeapon(uint weapon)
		{
			uint num = WEAPON.GET_WEAPONTYPE_GROUP(weapon);
			return num == 416676503U || num == 3193669993U || num == 3700405225U || num == 970310034U || num == 3082541095U || num == 860033945U || num == 3053283277U;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00147260 File Offset: 0x00145460
		private unsafe static ImpactRecord Capture(Ped player, Ped victim, uint weapon, float damage)
		{
			ImpactRecord impactRecord = new ImpactRecord();
			impactRecord.PedHandle = victim.Handle;
			impactRecord.PedModel = (uint)victim.Model.Hash;
			impactRecord.VictimDeadAfterHit = victim.IsDead;
			impactRecord.WeaponHash = weapon;
			ImpactRecord impactRecord2 = impactRecord;
			string weaponName;
			if (!Enum.IsDefined(typeof(eWeapon), weapon))
			{
				weaponName = string.Format("0x{0:X8}", weapon);
			}
			else
			{
				eWeapon eWeapon = weapon;
				weaponName = eWeapon.ToString();
			}
			impactRecord2.WeaponName = weaponName;
			impactRecord.IsShotgun = (WEAPON.GET_WEAPONTYPE_GROUP(weapon) == 860033945U);
			impactRecord.Damage = damage;
			impactRecord.GameTime = Game.GameTime;
			impactRecord.PelletsOnVictim = Function.Call<int>(10881604826085319960UL, new InputArgument[]
			{
				victim.Handle,
				false,
				false
			});
			impactRecord.Ammo = ImpactDetector.AmmoOf(player, weapon);
			ImpactRecord impactRecord3 = impactRecord;
			int num;
			if (PED.GET_PED_LAST_DAMAGE_BONE(victim.Handle, &num))
			{
				impactRecord3.BoneId = num;
				impactRecord3.BoneIndex = PED.GET_PED_BONE_INDEX(victim.Handle, num);
			}
			ImpactRecord impactRecord4 = impactRecord3;
			string boneName;
			if (impactRecord3.BoneId >= 0)
			{
				BoneID boneId = impactRecord3.BoneId;
				boneName = boneId.ToString();
			}
			else
			{
				boneName = "none";
			}
			impactRecord4.BoneName = boneName;
			ulong* ptr = stackalloc ulong[(UIntPtr)24];
			impactRecord3.HasImpact = WEAPON.GET_PED_LAST_WEAPON_IMPACT_COORD(player.Handle, (Vector3*)ptr);
			impactRecord3.Impact = ImpactDetector.ReadScrVector(ptr);
			ImpactDetector.ResolveMuzzle(player, out impactRecord3.Muzzle, out impactRecord3.MuzzleSource);
			Vector3 vector = impactRecord3.HasImpact ? impactRecord3.Impact : victim.Position;
			impactRecord3.Distance = impactRecord3.Muzzle.DistanceTo(vector);
			ImpactDetector.SnapshotSkeleton(victim, impactRecord3);
			return impactRecord3;
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x0014741C File Offset: 0x0014561C
		private static void SnapshotSkeleton(Ped victim, ImpactRecord rec)
		{
			if (!Anatomy.IsBlade(rec.Class) && !Anatomy.IsArea(rec.Class) && !Anatomy.IsBlunt(rec.Class))
			{
				rec.Class = ImpactDetector.ClassOf(rec.WeaponHash);
			}
			ImpactDetector.<>c__DisplayClass86_0 CS$<>8__locals1;
			CS$<>8__locals1.origin = victim.Position;
			double num = (double)victim.Heading * 3.141592653589793 / 180.0;
			CS$<>8__locals1.fwd = new V3((float)(-(float)Math.Sin(num)), (float)Math.Cos(num), 0f);
			CS$<>8__locals1.right = new V3((float)Math.Cos(num), (float)Math.Sin(num), 0f);
			float num2 = float.MaxValue;
			IEnumerable<int> enumerable;
			if (victim.IsHuman)
			{
				IEnumerable<int> allBones = Anatomy.AllBones;
				enumerable = allBones;
			}
			else
			{
				enumerable = Anatomy.AllBones.Concat(AnimalAnatomy.TailIds);
			}
			foreach (int num3 in enumerable)
			{
				if (PED.GET_PED_BONE_INDEX(victim.Handle, num3) != -1)
				{
					Vector3 w = PED.GET_PED_BONE_COORDS(victim.Handle, num3, 0f, 0f, 0f);
					rec.BonesLocal.Add(new BonePoint
					{
						Id = num3,
						P = ImpactDetector.<SnapshotSkeleton>g__ToLocal|86_0(w, ref CS$<>8__locals1)
					});
					if (rec.HasImpact)
					{
						num2 = Math.Min(num2, w.DistanceTo(rec.Impact));
					}
				}
			}
			rec.MuzzleLocal = ImpactDetector.<SnapshotSkeleton>g__ToLocal|86_0(rec.Muzzle, ref CS$<>8__locals1);
			Vector3 vector = rec.HasImpact ? rec.Impact : victim.Position;
			rec.DirLocal = ImpactDetector.<SnapshotSkeleton>g__DirToLocal|86_1(vector - rec.Muzzle, ref CS$<>8__locals1);
			rec.EntryLocal = ImpactDetector.<SnapshotSkeleton>g__ToLocal|86_0(vector, ref CS$<>8__locals1);
			rec.ImpactOnVictim = (rec.HasImpact && num2 < 0.45f);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x00147618 File Offset: 0x00145818
		private static WeaponClass ClassOf(uint weapon)
		{
			uint num = WEAPON.GET_WEAPONTYPE_GROUP(weapon);
			if (num == 416676503U)
			{
				return WeaponClass.Pistol;
			}
			if (num == 3193669993U)
			{
				return WeaponClass.Revolver;
			}
			if (num == 3700405225U)
			{
				return WeaponClass.Repeater;
			}
			if (num == 3082541095U)
			{
				return WeaponClass.Sniper;
			}
			if (num == 860033945U)
			{
				return WeaponClass.Shotgun;
			}
			if (num == 3053283277U)
			{
				return WeaponClass.Bow;
			}
			return WeaponClass.Rifle;
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00147669 File Offset: 0x00145869
		private unsafe static Vector3 ReadScrVector(ulong* v)
		{
			return new Vector3(*(float*)v, *(float*)(v + 1), *(float*)(v + 2));
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00147680 File Offset: 0x00145880
		private static void ResolveMuzzle(Ped player, out Vector3 pos, out string source)
		{
			int num = WEAPON.GET_CURRENT_PED_WEAPON_ENTITY_INDEX(player.Handle, 0);
			if (num != 0)
			{
				foreach (string text in ImpactDetector.MuzzleBoneNames)
				{
					int num2 = ENTITY.GET_ENTITY_BONE_INDEX_BY_NAME(num, text);
					if (num2 != -1)
					{
						pos = ENTITY.GET_WORLD_POSITION_OF_ENTITY_BONE(num, num2);
						source = "bone '" + text + "'";
						return;
					}
				}
				pos = ENTITY.GET_ENTITY_COORDS(num, true, true);
				source = "weapon entity";
				return;
			}
			pos = PED.GET_PED_BONE_COORDS(player.Handle, 21030, 0f, 0f, 0f);
			source = "player head";
		}

		// Token: 0x06001472 RID: 5234 RVA: 0x00147A64 File Offset: 0x00145C64
		[CompilerGenerated]
		internal static V3 <SnapshotSkeleton>g__ToLocal|86_0(Vector3 w, ref ImpactDetector.<>c__DisplayClass86_0 A_1)
		{
			V3 v = new V3(w.X - A_1.origin.X, w.Y - A_1.origin.Y, w.Z - A_1.origin.Z);
			return new V3(V3.Dot(v, A_1.right), V3.Dot(v, A_1.fwd), v.Z);
		}

		// Token: 0x06001473 RID: 5235 RVA: 0x00147AD4 File Offset: 0x00145CD4
		[CompilerGenerated]
		internal static V3 <SnapshotSkeleton>g__DirToLocal|86_1(Vector3 w, ref ImpactDetector.<>c__DisplayClass86_0 A_1)
		{
			V3 v = new V3(w.X, w.Y, w.Z);
			return new V3(V3.Dot(v, A_1.right), V3.Dot(v, A_1.fwd), v.Z).Normalized();
		}

		// Token: 0x04000DEE RID: 3566
		private static readonly string[] MuzzleBoneNames = new string[]
		{
			"gun_muzzle",
			"muzzle",
			"Muzzle"
		};

		// Token: 0x04000DEF RID: 3567
		private const int WeaponAttachHandPrimary = 0;

		// Token: 0x04000DF0 RID: 3568
		private const int HeadBoneId = 21030;

		// Token: 0x04000DF1 RID: 3569
		private const int ShotWindowMs = 150;

		// Token: 0x04000DF2 RID: 3570
		private const ulong GetNumberOfBulletsImpactedEntity = 10881604826085319960UL;

		// Token: 0x04000DF3 RID: 3571
		private int _shotId;

		// Token: 0x04000DF4 RID: 3572
		private int _lastShotTime = -100000;

		// Token: 0x04000DF5 RID: 3573
		private const int RightHandBone = 22798;

		// Token: 0x04000DF6 RID: 3574
		private const int ChestBone = 14413;

		// Token: 0x04000DF7 RID: 3575
		[TupleElementNames(new string[]
		{
			"t",
			"p"
		})]
		private readonly Queue<ValueTuple<int, Vector3>> _blade = new Queue<ValueTuple<int, Vector3>>();

		// Token: 0x04000DF8 RID: 3576
		private uint _lastShotWeapon;

		// Token: 0x04000DFB RID: 3579
		private static readonly Dictionary<uint, bool> _fourLegged = new Dictionary<uint, bool>();

		// Token: 0x04000DFC RID: 3580
		private static readonly string[] Birds = new string[]
		{
			"chicken",
			"rooster",
			"turkey",
			"duck",
			"goose",
			"swan",
			"eagle",
			"hawk",
			"owl",
			"crow",
			"raven",
			"pigeon",
			"dove",
			"songbird",
			"sparrow",
			"robin",
			"cardinal",
			"bluejay",
			"oriole",
			"finch",
			"waxwing",
			"warbler",
			"kingfisher",
			"woodpecker",
			"heron",
			"egret",
			"pelican",
			"spoonbill",
			"crane",
			"loon",
			"gull",
			"cormorant",
			"vulture",
			"condor",
			"parrot",
			"parakeet",
			"quail",
			"pheasant",
			"grouse",
			"bird"
		};

		// Token: 0x04000DFD RID: 3581
		private static readonly string[] NotAnimals = new string[]
		{
			"fish",
			"trout",
			"salmon",
			"muskie",
			"sturgeon",
			"snake",
			"turtle",
			"toad",
			"frog",
			"lizard",
			"iguana",
			"gilamonster",
			"lobster",
			"crawfish",
			"squirrel",
			"chipmunk"
		};

		// Token: 0x04000DFE RID: 3582
		private readonly Dictionary<int, int> _damagedAt = new Dictionary<int, int>();

		// Token: 0x04000DFF RID: 3583
		private readonly Dictionary<int, int> _areaAt = new Dictionary<int, int>();

		// Token: 0x04000E00 RID: 3584
		private readonly Dictionary<int, int> _blowAt = new Dictionary<int, int>();

		// Token: 0x04000E01 RID: 3585
		private const int LeftHandBone = 34606;

		// Token: 0x04000E02 RID: 3586
		[TupleElementNames(new string[]
		{
			"t",
			"l",
			"r",
			"w",
			"lf",
			"rf"
		})]
		private readonly Queue<ValueTuple<int, Vector3, Vector3, Vector3, Vector3, Vector3>> _fists = new Queue<ValueTuple<int, Vector3, Vector3, Vector3, Vector3, Vector3>>();

		// Token: 0x04000E03 RID: 3587
		private static readonly int[] StrikeBones = Anatomy.SpineChain.Concat(Anatomy.LimbChains.SelectMany((int[] c) => c)).Distinct<int>().ToArray<int>();

		// Token: 0x04000E04 RID: 3588
		private readonly Dictionary<int, int> _grabbedSince = new Dictionary<int, int>();

		// Token: 0x04000E05 RID: 3589
		private readonly Dictionary<int, int> _lyingAt = new Dictionary<int, int>();

		// Token: 0x04000E06 RID: 3590
		private const int DownChainMs = 4000;

		// Token: 0x04000E07 RID: 3591
		private readonly Dictionary<int, int> _struckAt = new Dictionary<int, int>();

		// Token: 0x04000E08 RID: 3592
		private readonly Dictionary<int, int> _stompAt = new Dictionary<int, int>();

		// Token: 0x04000E09 RID: 3593
		private readonly Dictionary<int, int> _stompNoteAt = new Dictionary<int, int>();

		// Token: 0x04000E0A RID: 3594
		private const float StompSpeed = 1.5f;

		// Token: 0x04000E0B RID: 3595
		private const float StompLift = 0.2f;

		// Token: 0x04000E0C RID: 3596
		private const float StompReach = 0.35f;

		// Token: 0x04000E0D RID: 3597
		private const int StompGapMs = 450;

		// Token: 0x04000E0E RID: 3598
		private readonly Dictionary<int, string> _downState = new Dictionary<int, string>();

		// Token: 0x04000E0F RID: 3599
		private const float SaddleKickReach = 0.9f;

		// Token: 0x04000E10 RID: 3600
		private const float SaddleKickSpeed = 4.5f;

		// Token: 0x04000E11 RID: 3601
		private const float ChokeHands = 0.3f;

		// Token: 0x04000E12 RID: 3602
		private const float TakedownGrabDamage = 6f;

		// Token: 0x04000E13 RID: 3603
		private const float TakedownDamage = 30f;

		// Token: 0x04000E14 RID: 3604
		private const float TakedownHands = 0.45f;

		// Token: 0x04000E15 RID: 3605
		private readonly Dictionary<int, ImpactDetector.Hold> _holds = new Dictionary<int, ImpactDetector.Hold>();

		// Token: 0x04000E16 RID: 3606
		private const int HoldChokeMinDrop = 15;

		// Token: 0x04000E17 RID: 3607
		private const int HoldTickDamage = 8;

		// Token: 0x04000E18 RID: 3608
		[TupleElementNames(new string[]
		{
			"hp",
			"last"
		})]
		private readonly Dictionary<int, ValueTuple<int, int>> _beaten = new Dictionary<int, ValueTuple<int, int>>();

		// Token: 0x04000E19 RID: 3609
		private readonly HashSet<uint> _unknownLogged = new HashSet<uint>();

		// Token: 0x04000E1A RID: 3610
		private static uint _hExplosion;

		// Token: 0x04000E1B RID: 3611
		private static uint _hFire;

		// Token: 0x04000E1C RID: 3612
		private static uint _hMolotov;

		// Token: 0x04000E1D RID: 3613
		private static uint _hDynamite;

		// Token: 0x04000E1E RID: 3614
		[TupleElementNames(new string[]
		{
			"at",
			"weapon"
		})]
		private readonly Dictionary<int, ValueTuple<int, uint>> _pendingImpact = new Dictionary<int, ValueTuple<int, uint>>();

		// Token: 0x04000E1F RID: 3615
		private int _playerShotAt = -100000;

		// Token: 0x04000E20 RID: 3616
		private uint _playerShotWeapon;

		// Token: 0x04000E21 RID: 3617
		private static string _lastAmmoKey;

		// Token: 0x04000E22 RID: 3618
		internal static Action<string> AmmoNote;

		// Token: 0x0200049B RID: 1179
		private sealed class Hold
		{
			// Token: 0x040020AD RID: 8365
			public int Since;

			// Token: 0x040020AE RID: 8366
			public int Hp0;

			// Token: 0x040020AF RID: 8367
			public int HpLow;

			// Token: 0x040020B0 RID: 8368
			public int Struck;

			// Token: 0x040020B1 RID: 8369
			public int LastPunchAt = -1073741824;

			// Token: 0x040020B2 RID: 8370
			public bool Squeezed;
		}
	}
}
