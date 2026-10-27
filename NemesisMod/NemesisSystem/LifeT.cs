using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000007 RID: 7
	internal class LifeT
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000024CC File Offset: 0x000006CC
		public bool Allows(NemesisRecord r, Backstory.Ctx c)
		{
			return (this.BornFrom == 0 || r.BornYear >= this.BornFrom) && (this.BornTo == 0 || r.BornYear <= this.BornTo) && (string.IsNullOrEmpty(this.BornIn) || (c.Born ?? "").Contains(this.BornIn));
		}

		// Token: 0x04000011 RID: 17
		public string Kind;

		// Token: 0x04000012 RID: 18
		public string Who;

		// Token: 0x04000013 RID: 19
		public string Text;

		// Token: 0x04000014 RID: 20
		public string Fx;

		// Token: 0x04000015 RID: 21
		public string MirrorKind;

		// Token: 0x04000016 RID: 22
		public string MirrorTitle;

		// Token: 0x04000017 RID: 23
		public string TiltText;

		// Token: 0x04000018 RID: 24
		public int MinAge;

		// Token: 0x04000019 RID: 25
		public int MaxAge;

		// Token: 0x0400001A RID: 26
		public int W;

		// Token: 0x0400001B RID: 27
		public bool Public;

		// Token: 0x0400001C RID: 28
		public bool MirrorHeard;

		// Token: 0x0400001D RID: 29
		public int BornFrom;

		// Token: 0x0400001E RID: 30
		public int BornTo;

		// Token: 0x0400001F RID: 31
		public string BornIn;

		// Token: 0x04000020 RID: 32
		public Dictionary<Personality, int> Tilt = new Dictionary<Personality, int>();
	}
}
