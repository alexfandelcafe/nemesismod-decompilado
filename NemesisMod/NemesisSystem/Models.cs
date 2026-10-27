using System;
using System.Collections.Generic;
using RDR2;

namespace NemesisSystem
{
	// Token: 0x02000068 RID: 104
	public static class Models
	{
		// Token: 0x06001275 RID: 4725 RVA: 0x001224B8 File Offset: 0x001206B8
		public static string Raw(uint model)
		{
			string text;
			if (Models._raw.TryGetValue(model, out text))
			{
				return text;
			}
			string text2;
			if (!Enum.IsDefined(typeof(PedHash), model))
			{
				text2 = "";
			}
			else
			{
				PedHash pedHash = model;
				text2 = pedHash.ToString();
			}
			text = text2;
			Models._raw[model] = text;
			return text;
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00122514 File Offset: 0x00120714
		public static string Name(uint model)
		{
			string text;
			if (Models._lower.TryGetValue(model, out text))
			{
				return text;
			}
			text = Models.Raw(model).ToLowerInvariant();
			Models._lower[model] = text;
			return text;
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x0012254C File Offset: 0x0012074C
		public static string Name(Ped p)
		{
			if (!(p == null))
			{
				return Models.Name((uint)p.Model.Hash);
			}
			return "";
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x0012257B File Offset: 0x0012077B
		public static bool Female(uint model)
		{
			return model != 0U && Models.Name(model).Contains("_f_");
		}

		// Token: 0x04000C35 RID: 3125
		private static readonly Dictionary<uint, string> _raw = new Dictionary<uint, string>();

		// Token: 0x04000C36 RID: 3126
		private static readonly Dictionary<uint, string> _lower = new Dictionary<uint, string>();
	}
}
