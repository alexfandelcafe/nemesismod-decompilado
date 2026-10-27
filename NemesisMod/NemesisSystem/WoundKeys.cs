using System;

namespace NemesisSystem
{
	// Token: 0x02000058 RID: 88
	public static class WoundKeys
	{
		// Token: 0x06000F52 RID: 3922 RVA: 0x0010DABD File Offset: 0x0010BCBD
		public static string Base(string key)
		{
			if (key == null || key.Length <= 2 || key[key.Length - 2] != '_')
			{
				return key;
			}
			return key.Substring(0, key.Length - 2);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x0010DAEE File Offset: 0x0010BCEE
		public static string Side(string key)
		{
			if (key == null)
			{
				return "";
			}
			if (key.EndsWith("_l"))
			{
				return "left";
			}
			if (!key.EndsWith("_r"))
			{
				return "";
			}
			return "right";
		}
	}
}
