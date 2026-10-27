using System;
using Newtonsoft.Json;
using RDR2.Math;

namespace NemesisSystem
{
	// Token: 0x02000049 RID: 73
	internal sealed class StashSpot
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0010BE90 File Offset: 0x0010A090
		// (set) Token: 0x06000CE3 RID: 3299 RVA: 0x0010BE98 File Offset: 0x0010A098
		public string Town { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0010BEA1 File Offset: 0x0010A0A1
		// (set) Token: 0x06000CE5 RID: 3301 RVA: 0x0010BEA9 File Offset: 0x0010A0A9
		public float X { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000CE6 RID: 3302 RVA: 0x0010BEB2 File Offset: 0x0010A0B2
		// (set) Token: 0x06000CE7 RID: 3303 RVA: 0x0010BEBA File Offset: 0x0010A0BA
		public float Y { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0010BEC3 File Offset: 0x0010A0C3
		// (set) Token: 0x06000CE9 RID: 3305 RVA: 0x0010BECB File Offset: 0x0010A0CB
		public float Z { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0010BED4 File Offset: 0x0010A0D4
		// (set) Token: 0x06000CEB RID: 3307 RVA: 0x0010BEDC File Offset: 0x0010A0DC
		public float H { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0010BEE5 File Offset: 0x0010A0E5
		[JsonIgnore]
		public Vector3 Pos
		{
			get
			{
				return new Vector3(this.X, this.Y, this.Z);
			}
		}

		// Token: 0x04000988 RID: 2440
		[JsonIgnore]
		public bool BuiltIn;
	}
}
