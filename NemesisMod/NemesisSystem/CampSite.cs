using System;

namespace NemesisSystem
{
	// Token: 0x0200000E RID: 14
	internal sealed class CampSite
	{
		// Token: 0x06000086 RID: 134 RVA: 0x0000A8E0 File Offset: 0x00008AE0
		public CampSite(string id, uint imap, float x, float y, float z, string region, bool hideout = false, string name = null, string gang = "odriscoll")
		{
			this.Id = id;
			this.Imap = imap;
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.Region = region;
			this.Hideout = hideout;
			this.Name = name;
			this.Gang = gang;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000A938 File Offset: 0x00008B38
		public string Label
		{
			get
			{
				string result;
				if ((result = this.Name) == null)
				{
					Func<CampSite, string> ownerOf = CampSite.OwnerOf;
					Gang gang = Gangs.ById(((ownerOf != null) ? ownerOf(this) : null) ?? this.Gang);
					result = (((gang != null) ? gang.Name : null) ?? "Forajido") + " camp in " + this.Region;
				}
				return result;
			}
		}

		// Token: 0x0400010D RID: 269
		public readonly string Id;

		// Token: 0x0400010E RID: 270
		public readonly uint Imap;

		// Token: 0x0400010F RID: 271
		public readonly float X;

		// Token: 0x04000110 RID: 272
		public readonly float Y;

		// Token: 0x04000111 RID: 273
		public readonly float Z;

		// Token: 0x04000112 RID: 274
		public readonly string Region;

		// Token: 0x04000113 RID: 275
		public readonly bool Hideout;

		// Token: 0x04000114 RID: 276
		public readonly string Name;

		// Token: 0x04000115 RID: 277
		public readonly string Gang;

		// Token: 0x04000116 RID: 278
		public static Func<CampSite, string> OwnerOf;
	}
}
