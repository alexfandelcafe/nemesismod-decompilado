using System;
using System.Collections.Generic;
using System.Linq;
using RDR2;
using RDR2.Native;

namespace NemesisSystem
{
	// Token: 0x0200001B RID: 27
	internal static class GunMelee
	{
		// Token: 0x06000A50 RID: 2640 RVA: 0x000F50E4 File Offset: 0x000F32E4
		public static void Tick(Ped player)
		{
			if (PED.IS_PED_SHOOTING(player.Handle) && WEAPON.GET_WEAPONTYPE_GROUP(WEAPON._GET_PED_CURRENT_HELD_WEAPON(player.Handle)) != 308416707U)
			{
				GunMelee._lastShot = Game.GameTime;
			}
			if (Game.FrameCount % 600 == 0)
			{
				foreach (int num in (from h in GunMelee.Struck.Keys
				where !ENTITY.DOES_ENTITY_EXIST(h)
				select h).ToList<int>())
				{
					GunMelee.Struck.Remove(num);
					GunMelee.OutCold.Remove(num);
				}
				foreach (int key in (from h in GunMelee.Wounded.Keys
				where !ENTITY.DOES_ENTITY_EXIST(h)
				select h).ToList<int>())
				{
					GunMelee.Wounded.Remove(key);
				}
				foreach (int item in (from h in GunMelee.OutCold
				where !ENTITY.DOES_ENTITY_EXIST(h)
				select h).ToList<int>())
				{
					GunMelee.OutCold.Remove(item);
				}
			}
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x000F5298 File Offset: 0x000F3498
		public static bool IsGun(uint weapon)
		{
			uint num = WEAPON.GET_WEAPONTYPE_GROUP(weapon);
			return num == 416676503U || num == 3193669993U || num == 3700405225U || num == 970310034U || num == 3082541095U || num == 860033945U;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x000F52E0 File Offset: 0x000F34E0
		public static bool IsGunMeleeHit(Ped player, int victim, uint weapon)
		{
			if (!GunMelee.IsGun(weapon) || Game.GameTime - GunMelee._lastShot < 500)
			{
				return false;
			}
			Entity entity = Entity.FromHandle(victim);
			if (entity == null || !entity.Exists() || entity.Position.DistanceTo(player.Position) > 3f)
			{
				return false;
			}
			GunMelee.Struck[victim] = Game.GameTime;
			return true;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x000F534F File Offset: 0x000F354F
		public static void MarkWounded(int victim)
		{
			GunMelee.Wounded[victim] = Game.GameTime;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x000F5361 File Offset: 0x000F3561
		public static void Forget(int h)
		{
			GunMelee.Struck.Remove(h);
			GunMelee.Wounded.Remove(h);
			GunMelee.OutCold.Remove(h);
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x000F5387 File Offset: 0x000F3587
		public static bool PlayerFiredRecently
		{
			get
			{
				return Game.GameTime - GunMelee._lastShot < 500;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000A56 RID: 2646 RVA: 0x000F539B File Offset: 0x000F359B
		public static int LastShotAt
		{
			get
			{
				return GunMelee._lastShot;
			}
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x000F53A4 File Offset: 0x000F35A4
		public static bool KnockedOutByGunButt(Ped p)
		{
			int num;
			if (!GunMelee.Struck.TryGetValue(p.Handle, out num))
			{
				return false;
			}
			int num2;
			if (GunMelee.Wounded.TryGetValue(p.Handle, out num2) && num2 > num)
			{
				GunMelee.OutCold.Remove(p.Handle);
				return false;
			}
			if (GunMelee.OutCold.Contains(p.Handle))
			{
				return true;
			}
			if (Game.GameTime - num > 30000)
			{
				return false;
			}
			if (p.IsDead)
			{
				int num3 = PED.GET_PED_SOURCE_OF_DEATH(p.Handle);
				if (num3 != 0 && num3 != Game.Player.Character.Handle)
				{
					return false;
				}
				GunMelee.OutCold.Add(p.Handle);
			}
			return true;
		}

		// Token: 0x04000797 RID: 1943
		private const int ShotWindowMs = 500;

		// Token: 0x04000798 RID: 1944
		private const float ReachMetres = 3f;

		// Token: 0x04000799 RID: 1945
		private const int KnockoutMemoryMs = 30000;

		// Token: 0x0400079A RID: 1946
		private static int _lastShot = -100000;

		// Token: 0x0400079B RID: 1947
		private static readonly Dictionary<int, int> Struck = new Dictionary<int, int>();

		// Token: 0x0400079C RID: 1948
		private static readonly Dictionary<int, int> Wounded = new Dictionary<int, int>();

		// Token: 0x0400079D RID: 1949
		private static readonly HashSet<int> OutCold = new HashSet<int>();
	}
}
