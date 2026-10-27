using System;
using System.Collections.Generic;
using RDR2;
using RDR2.Native;

namespace NemesisSystem
{
	// Token: 0x0200001C RID: 28
	internal static class Downed
	{
		// Token: 0x06000A59 RID: 2649 RVA: 0x000F5480 File Offset: 0x000F3680
		public static void Saw(Ped p, bool down)
		{
			int handle = p.Handle;
			if (!down)
			{
				Downed.Since.Remove(handle);
				Downed.HealthWhenDown.Remove(handle);
				if (!p.IsDead && Downed.Kicked.Remove(handle))
				{
					Downed.Finished.Remove(handle);
					Action<string> log = Downed.Log;
					if (log == null)
					{
						return;
					}
					log(string.Format("FINISHED ped={0} undone - he's up again (kicked, not killed)", handle));
				}
				return;
			}
			int health = p.Health;
			if (!Downed.Since.ContainsKey(handle))
			{
				Downed.Since[handle] = Game.GameTime;
				Downed.HealthWhenDown[handle] = health;
				Action<string> log2 = Downed.Log;
				if (log2 == null)
				{
					return;
				}
				log2(string.Format("DOWN ped={0} health={1} dead={2} incapacitated={3}", new object[]
				{
					handle,
					health,
					p.IsDead,
					p.IsIncapacitated
				}));
				return;
			}
			else
			{
				if (Downed.Finished.Contains(handle) || Game.GameTime - Downed.Since[handle] < 800 || health >= Downed.HealthWhenDown[handle] - 3)
				{
					if (health > Downed.HealthWhenDown[handle])
					{
						Downed.HealthWhenDown[handle] = health;
					}
					return;
				}
				Downed.Finished.Add(handle);
				Action<string> log3 = Downed.Log;
				if (log3 == null)
				{
					return;
				}
				log3(string.Format("FINISHED ped={0} (health {1} -> {2})", handle, Downed.HealthWhenDown[handle], health));
				return;
			}
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x000F5600 File Offset: 0x000F3800
		public static void Hit(int handle, string how)
		{
			int num;
			if (Downed.Finished.Contains(handle) || !Downed.Since.TryGetValue(handle, out num) || Game.GameTime - num < 800)
			{
				return;
			}
			Downed.Finished.Add(handle);
			Action<string> log = Downed.Log;
			if (log == null)
			{
				return;
			}
			log(string.Format("FINISHED ped={0} ({1})", handle, how));
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x000F5664 File Offset: 0x000F3864
		public static bool IsFinished(int handle)
		{
			return Downed.Finished.Contains(handle);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x000F5671 File Offset: 0x000F3871
		public static void KickedToDeath(int handle)
		{
			if (!Downed.Finished.Add(handle))
			{
				return;
			}
			Downed.Kicked.Add(handle);
			Action<string> log = Downed.Log;
			if (log == null)
			{
				return;
			}
			log(string.Format("FINISHED ped={0} (kicked to death while he lay there - presumed dead)", handle));
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x000F56AC File Offset: 0x000F38AC
		public static bool WasKicked(int handle)
		{
			return Downed.Kicked.Contains(handle);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x000F56B9 File Offset: 0x000F38B9
		public static void Forget(int handle)
		{
			Downed.Since.Remove(handle);
			Downed.Finished.Remove(handle);
			Downed.HealthWhenDown.Remove(handle);
			Downed.Kicked.Remove(handle);
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x000F56EC File Offset: 0x000F38EC
		public static void Prune()
		{
			foreach (int num in new List<int>(Downed.Since.Keys))
			{
				Ped ped = Entity.FromHandle(num) as Ped;
				if (ped == null || !ped.Exists())
				{
					Downed.Since.Remove(num);
				}
			}
			Downed.Finished.RemoveWhere(delegate(int h)
			{
				Ped ped2 = Entity.FromHandle(h) as Ped;
				return ped2 == null || !ped2.Exists();
			});
			Downed.Kicked.RemoveWhere((int h) => !Downed.Finished.Contains(h));
			foreach (int num2 in new List<int>(Downed.HealthWhenDown.Keys))
			{
				if (!ENTITY.DOES_ENTITY_EXIST(num2))
				{
					Downed.HealthWhenDown.Remove(num2);
				}
			}
		}

		// Token: 0x0400079E RID: 1950
		private const int FinishAfterMs = 800;

		// Token: 0x0400079F RID: 1951
		private static readonly Dictionary<int, int> Since = new Dictionary<int, int>();

		// Token: 0x040007A0 RID: 1952
		private static readonly HashSet<int> Finished = new HashSet<int>();

		// Token: 0x040007A1 RID: 1953
		private static readonly Dictionary<int, int> HealthWhenDown = new Dictionary<int, int>();

		// Token: 0x040007A2 RID: 1954
		public static Action<string> Log;

		// Token: 0x040007A3 RID: 1955
		private static readonly HashSet<int> Kicked = new HashSet<int>();
	}
}
