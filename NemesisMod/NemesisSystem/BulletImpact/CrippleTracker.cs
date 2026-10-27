using System;
using System.Collections.Generic;
using System.Linq;
using RDR2;
using RDR2.Math;
using RDR2.Native;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x02000097 RID: 151
	internal sealed class CrippleTracker
	{
		// Token: 0x06001443 RID: 5187 RVA: 0x001438F1 File Offset: 0x00141AF1
		public CrippleTracker(Action<string> log = null)
		{
			this._log = log;
		}

		// Token: 0x06001444 RID: 5188 RVA: 0x0014390C File Offset: 0x00141B0C
		public CrippleLevel Apply(Ped ped, CrippleLevel level)
		{
			if (level == CrippleLevel.None || ped == null || !ped.Exists() || ped.IsDead || Kids.IsChild(ped) || Kids.Protected(ped))
			{
				return CrippleLevel.None;
			}
			CrippleTracker.State state;
			if (!this._peds.TryGetValue(ped.Handle, out state))
			{
				if (this._peds.Count >= 64)
				{
					Vector3 position = Game.Player.Character.Position;
					int num = 0;
					float num2 = -1f;
					foreach (KeyValuePair<int, CrippleTracker.State> keyValuePair in this._peds)
					{
						Ped ped2 = new Ped(keyValuePair.Key);
						float num3 = ped2.Exists() ? ped2.Position.DistanceTo(position) : float.MaxValue;
						if (num3 > num2)
						{
							num2 = num3;
							num = keyValuePair.Key;
						}
					}
					this._peds.Remove(num);
					Action<string> log = this._log;
					if (log != null)
					{
						log(string.Format("CRIPPLE tracker full ({0}) - let go of ped {1} ({2}) to track ped {3}", new object[]
						{
							64,
							num,
							(num2 == float.MaxValue) ? "gone" : string.Format("{0:0} m off", num2),
							ped.Handle
						}));
					}
				}
				Dictionary<int, CrippleTracker.State> peds = this._peds;
				int handle = ped.Handle;
				CrippleTracker.State state2 = new CrippleTracker.State();
				state2.Model = ped.Model.Hash;
				state = state2;
				peds[handle] = state2;
			}
			if (level <= state.Level)
			{
				return state.Level;
			}
			state.Level = level;
			int handle2 = ped.Handle;
			switch (level)
			{
			case CrippleLevel.Limping:
				CrippleTracker.InjuredGait(handle2);
				PED._SET_PED_DESIRED_LOCO_FOR_MODEL(handle2, "injured_general");
				break;
			case CrippleLevel.Legless:
			{
				CrippleTracker.Drop(ped, state);
				CrippleTracker.InjuredGait(handle2);
				PED.SET_PED_INJURED_ON_GROUND_BEHAVIOUR(handle2, 100000f);
				Action<string> log2 = this._log;
				if (log2 != null)
				{
					log2(string.Format("CRIPPLE ped={0} Legless health={1}", handle2, ped.Health));
				}
				break;
			}
			case CrippleLevel.SpineBroken:
				CrippleTracker.InjuredGait(handle2);
				PED.SET_PED_TO_RAGDOLL(handle2, 10000, 10000, 0, false, false, "");
				break;
			case CrippleLevel.NeckBroken:
				TASK.TASK_KNOCKED_OUT(handle2, 0f, true);
				break;
			}
			return level;
		}

		// Token: 0x06001445 RID: 5189 RVA: 0x00143B70 File Offset: 0x00141D70
		private static void InjuredGait(int h)
		{
			PED.SET_PED_CONFIG_FLAG(h, 336, true);
			PED._FAKE_SET_PED_LOCO_INJURED(h, true);
		}

		// Token: 0x06001446 RID: 5190 RVA: 0x00143B88 File Offset: 0x00141D88
		public CrippleLevel LevelOf(int handle)
		{
			CrippleTracker.State state;
			if (!this._peds.TryGetValue(handle, out state))
			{
				return CrippleLevel.None;
			}
			return state.Level;
		}

		// Token: 0x06001447 RID: 5191 RVA: 0x00143BAD File Offset: 0x00141DAD
		public void Forget(int handle)
		{
			this._peds.Remove(handle);
		}

		// Token: 0x06001448 RID: 5192 RVA: 0x00143BBC File Offset: 0x00141DBC
		public void Clear()
		{
			this._peds.Clear();
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00143BC9 File Offset: 0x00141DC9
		private static void Drop(Ped ped, CrippleTracker.State st)
		{
			PED.SET_PED_TO_RAGDOLL(ped.Handle, 2500, 5000, 0, false, false, "");
			st.NextDropTime = Game.GameTime + 1500;
		}

		// Token: 0x0600144A RID: 5194 RVA: 0x00143BFA File Offset: 0x00141DFA
		private static bool IsDown(Ped ped)
		{
			return ped.IsRagdoll || TASK.IS_PED_IN_WRITHE(ped.Handle) || PED.IS_PED_PRONE(ped.Handle) || ped.HeightAboveGround < 0.7f;
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00143C30 File Offset: 0x00141E30
		public void Tick(Ped player)
		{
			if (this._peds.Count == 0)
			{
				return;
			}
			foreach (int num in this._peds.Keys.ToList<int>())
			{
				CrippleTracker.State state = this._peds[num];
				Ped ped = Entity.FromHandle(num) as Ped;
				if (ped == null || !ped.Exists() || ped.IsDead)
				{
					this._peds.Remove(num);
				}
				else if (state.Model != 0 && ped.Model.Hash != state.Model)
				{
					this._peds.Remove(num);
					Action<string> log = this._log;
					if (log != null)
					{
						log(string.Format("CRIPPLE handle {0} reused by another model - dropped", num));
					}
				}
				else if (!CrippleTracker.Hold && !ENTITY.IS_ENTITY_ATTACHED(num) && !ped.IsHogtied && !PED.IS_PED_ON_MOUNT(num) && !PED.IS_PED_IN_ANY_VEHICLE(num, false) && !Kids.IsChild(ped))
				{
					switch (state.Level)
					{
					case CrippleLevel.Limping:
						PED.SET_PED_MAX_MOVE_BLEND_RATIO(num, 1f);
						break;
					case CrippleLevel.Legless:
						if (!CrippleTracker.IsDown(ped) && Game.GameTime >= state.NextDropTime)
						{
							CrippleTracker.Drop(ped, state);
						}
						break;
					case CrippleLevel.SpineBroken:
						if (ped.IsRagdoll)
						{
							PED.RESET_PED_RAGDOLL_TIMER(num);
						}
						else
						{
							PED.SET_PED_TO_RAGDOLL(num, 10000, 10000, 0, false, false, "");
						}
						break;
					case CrippleLevel.NeckBroken:
						if (Game.GameTime >= state.NextDropTime && !ped.IsRagdoll && ped.HeightAboveGround >= 0.5f)
						{
							TASK.TASK_KNOCKED_OUT(num, 0f, true);
							state.NextDropTime = Game.GameTime + 1500;
						}
						break;
					}
				}
			}
		}

		// Token: 0x04000DE9 RID: 3561
		private const int PcfForceInjuredMovement = 336;

		// Token: 0x04000DEA RID: 3562
		private const int MaxTracked = 64;

		// Token: 0x04000DEB RID: 3563
		public static bool Hold;

		// Token: 0x04000DEC RID: 3564
		private readonly Action<string> _log;

		// Token: 0x04000DED RID: 3565
		private readonly Dictionary<int, CrippleTracker.State> _peds = new Dictionary<int, CrippleTracker.State>();

		// Token: 0x0200049A RID: 1178
		private sealed class State
		{
			// Token: 0x040020AA RID: 8362
			public CrippleLevel Level;

			// Token: 0x040020AB RID: 8363
			public int NextDropTime;

			// Token: 0x040020AC RID: 8364
			public int Model;
		}
	}
}
