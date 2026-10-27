using System;
using System.Collections.Generic;
using System.Linq;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x0200008F RID: 143
	public sealed class PathResult
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x060013DC RID: 5084 RVA: 0x0013C961 File Offset: 0x0013AB61
		public Severity Worst
		{
			get
			{
				if (this.Damage.Count != 0)
				{
					return this.Damage.Max((DamageItem d) => d.Severity);
				}
				return Severity.Minor;
			}
		}

		// Token: 0x04000D85 RID: 3461
		public V3 Start;

		// Token: 0x04000D86 RID: 3462
		public bool HasEntry;

		// Token: 0x04000D87 RID: 3463
		public V3 Entry;

		// Token: 0x04000D88 RID: 3464
		public V3 End;

		// Token: 0x04000D89 RID: 3465
		public bool Exited;

		// Token: 0x04000D8A RID: 3466
		public bool Estimated;

		// Token: 0x04000D8B RID: 3467
		public bool Blunt;

		// Token: 0x04000D8C RID: 3468
		public BlowKind Blow;

		// Token: 0x04000D8D RID: 3469
		public string EntryText = "";

		// Token: 0x04000D8E RID: 3470
		public string EndText = "";

		// Token: 0x04000D8F RID: 3471
		public readonly List<DamageItem> Damage = new List<DamageItem>();

		// Token: 0x04000D90 RID: 3472
		public readonly HashSet<string> HitKeys = new HashSet<string>();
	}
}
