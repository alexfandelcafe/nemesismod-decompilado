using System;
using Newtonsoft.Json;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x0200009E RID: 158
	public sealed class Blow
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x00147BBE File Offset: 0x00145DBE
		[JsonIgnore]
		public BlowKind Kind
		{
			get
			{
				if (this.Choke)
				{
					return BlowKind.Choke;
				}
				if (this.Butt)
				{
					return BlowKind.Gun;
				}
				if (!this.Kick && !this.Stomp)
				{
					return BlowKind.Punch;
				}
				return BlowKind.Kick;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00147BE7 File Offset: 0x00145DE7
		[JsonIgnore]
		public int Weight
		{
			get
			{
				if (this.Choke)
				{
					return 0;
				}
				if (this.Stomp)
				{
					return 3;
				}
				if (!this.Butt && !this.Kick)
				{
					return 1;
				}
				return 2;
			}
		}

		// Token: 0x04000E56 RID: 3670
		public int Bone;

		// Token: 0x04000E57 RID: 3671
		public V3 Dir;

		// Token: 0x04000E58 RID: 3672
		public bool Butt;

		// Token: 0x04000E59 RID: 3673
		public bool Kick;

		// Token: 0x04000E5A RID: 3674
		public bool Choke;

		// Token: 0x04000E5B RID: 3675
		public int HeldMs;

		// Token: 0x04000E5C RID: 3676
		public bool Stomp;

		// Token: 0x04000E5D RID: 3677
		public bool Down;
	}
}
