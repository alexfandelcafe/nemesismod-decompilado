using System;
using System.Collections.Generic;
using System.Linq;

namespace NemesisSystem
{
	// Token: 0x0200004B RID: 75
	internal sealed class District
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0010C098 File Offset: 0x0010A298
		public Dictionary<string, int> Lore
		{
			get
			{
				Dictionary<string, int> result;
				if (Gangs.Era >= 1907)
				{
					if ((result = this.Lore1907) == null && (result = this._lore1907From) == null)
					{
						return this._lore1907From = (from kv in this.Lore1899
						where Gangs.InEra(Gangs.ById(kv.Key))
						select kv).ToDictionary((KeyValuePair<string, int> kv) => kv.Key, (KeyValuePair<string, int> kv) => kv.Value);
					}
				}
				else
				{
					result = this.Lore1899;
				}
				return result;
			}
		}

		// Token: 0x040009A0 RID: 2464
		public string Key;

		// Token: 0x040009A1 RID: 2465
		public string Name;

		// Token: 0x040009A2 RID: 2466
		public uint[] Hashes;

		// Token: 0x040009A3 RID: 2467
		public Dictionary<string, int> Lore1899;

		// Token: 0x040009A4 RID: 2468
		public Dictionary<string, int> Lore1907;

		// Token: 0x040009A5 RID: 2469
		private Dictionary<string, int> _lore1907From;

		// Token: 0x040009A6 RID: 2470
		public float X;

		// Token: 0x040009A7 RID: 2471
		public float Y;

		// Token: 0x040009A8 RID: 2472
		public string Region;
	}
}
