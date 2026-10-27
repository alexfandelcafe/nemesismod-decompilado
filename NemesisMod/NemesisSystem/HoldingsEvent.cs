using System;

namespace NemesisSystem
{
	// Token: 0x02000020 RID: 32
	internal sealed class HoldingsEvent
	{
		// Token: 0x06000A7A RID: 2682 RVA: 0x000F58F0 File Offset: 0x000F3AF0
		public override string ToString()
		{
			return string.Format("{0}{1}{2}", this.Act, (this.Text != null) ? (" " + this.Text) : "", (this.SiteId != null) ? (" @" + this.SiteId) : "");
		}

		// Token: 0x040007AF RID: 1967
		public HoldingsAct Act;

		// Token: 0x040007B0 RID: 1968
		public string Text;

		// Token: 0x040007B1 RID: 1969
		public string SiteId;

		// Token: 0x040007B2 RID: 1970
		public NemesisRecord Who;

		// Token: 0x040007B3 RID: 1971
		public Gang From;

		// Token: 0x040007B4 RID: 1972
		public Gang To;

		// Token: 0x040007B5 RID: 1973
		public int Amount;

		// Token: 0x040007B6 RID: 1974
		public string Place;

		// Token: 0x040007B7 RID: 1975
		public bool Stash;

		// Token: 0x040007B8 RID: 1976
		public bool Big;
	}
}
