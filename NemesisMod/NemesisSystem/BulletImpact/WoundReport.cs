using System;
using System.Collections.Generic;
using System.Linq;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x0200009C RID: 156
	public sealed class WoundReport
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001476 RID: 5238 RVA: 0x00147B55 File Offset: 0x00145D55
		public DamageItem Worst
		{
			get
			{
				return this.Damage.FirstOrDefault<DamageItem>();
			}
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00147B64 File Offset: 0x00145D64
		public bool Has(string key)
		{
			return this.Damage.Any((DamageItem d) => d.Key == key);
		}

		// Token: 0x04000E50 RID: 3664
		public List<DamageItem> Damage = new List<DamageItem>();

		// Token: 0x04000E51 RID: 3665
		public string EntryText = "";

		// Token: 0x04000E52 RID: 3666
		public string WeaponName = "";

		// Token: 0x04000E53 RID: 3667
		public float Distance;
	}
}
