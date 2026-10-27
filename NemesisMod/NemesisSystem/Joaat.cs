using System;

namespace NemesisSystem
{
	// Token: 0x02000067 RID: 103
	public static class Joaat
	{
		// Token: 0x06001274 RID: 4724 RVA: 0x00122460 File Offset: 0x00120660
		public static uint Of(string s)
		{
			uint num = 0U;
			foreach (char c in s.ToLowerInvariant())
			{
				num += (uint)c;
				num += num << 10;
				num ^= num >> 6;
			}
			num += num << 3;
			num ^= num >> 11;
			return num + (num << 15);
		}
	}
}
