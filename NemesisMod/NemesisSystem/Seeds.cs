using System;

namespace NemesisSystem
{
	// Token: 0x02000066 RID: 102
	internal static class Seeds
	{
		// Token: 0x06001271 RID: 4721 RVA: 0x001223A0 File Offset: 0x001205A0
		public static int Stable(string s)
		{
			uint num = 2166136261U;
			foreach (char c in s ?? "")
			{
				num ^= (uint)c;
				num *= 16777619U;
			}
			return (int)num;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x001223E4 File Offset: 0x001205E4
		public static Random For(NemesisRecord r, string salt)
		{
			string str;
			if ((str = ((r != null) ? r.Id : null)) == null)
			{
				str = (((r != null) ? r.Name : null) ?? "");
			}
			return new Random(Seeds.Stable(str + "/" + salt));
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x00122420 File Offset: 0x00120620
		public static int NewSessionSeed()
		{
			return Seeds.Stable(DateTime.Now.Ticks.ToString() + "/" + Environment.TickCount.ToString());
		}
	}
}
