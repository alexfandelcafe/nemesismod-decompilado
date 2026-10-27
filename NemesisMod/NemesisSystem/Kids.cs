using System;
using System.Collections.Generic;
using RDR2;
using RDR2.Native;

namespace NemesisSystem
{
	// Token: 0x0200001D RID: 29
	internal static class Kids
	{
		// Token: 0x06000A61 RID: 2657 RVA: 0x000F583A File Offset: 0x000F3A3A
		public static bool Protected(Ped p)
		{
			return p != null && Kids.StoryProtected.Contains(p.Handle);
		}

		// Token: 0x06000A62 RID: 2658 RVA: 0x000F5858 File Offset: 0x000F3A58
		public static bool IsChild(Ped p)
		{
			if (p == null || !p.Exists())
			{
				return false;
			}
			if (PED._IS_PED_CHILD(p.Handle))
			{
				return true;
			}
			uint hash = (uint)p.Model.Hash;
			bool flag;
			if (Kids.ByModel.TryGetValue(hash, out flag))
			{
				return flag;
			}
			flag = false;
			string text = Models.Name(hash);
			flag = (text.Contains("kid") || text.Contains("child"));
			Kids.ByModel[hash] = flag;
			return flag;
		}

		// Token: 0x040007A4 RID: 1956
		public static readonly HashSet<int> StoryProtected = new HashSet<int>();

		// Token: 0x040007A5 RID: 1957
		private static readonly Dictionary<uint, bool> ByModel = new Dictionary<uint, bool>();
	}
}
