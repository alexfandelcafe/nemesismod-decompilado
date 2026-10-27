using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x02000047 RID: 71
	internal class SavePointMeta
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0010BD8F File Offset: 0x00109F8F
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x0010BD97 File Offset: 0x00109F97
		public long GameMin { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0010BDA0 File Offset: 0x00109FA0
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x0010BDA8 File Offset: 0x00109FA8
		public string When { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0010BDB1 File Offset: 0x00109FB1
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x0010BDB9 File Offset: 0x00109FB9
		public DateTime Real { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0010BDC2 File Offset: 0x00109FC2
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x0010BDCA File Offset: 0x00109FCA
		public string Kind { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0010BDD3 File Offset: 0x00109FD3
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x0010BDDB File Offset: 0x00109FDB
		public float X { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0010BDE4 File Offset: 0x00109FE4
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x0010BDEC File Offset: 0x00109FEC
		public float Y { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0010BDF5 File Offset: 0x00109FF5
		// (set) Token: 0x06000CD2 RID: 3282 RVA: 0x0010BDFD File Offset: 0x00109FFD
		public Dictionary<string, int> Notoriety { get; set; } = new Dictionary<string, int>();

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x0010BE06 File Offset: 0x0010A006
		// (set) Token: 0x06000CD4 RID: 3284 RVA: 0x0010BE0E File Offset: 0x0010A00E
		public string SeededGangs { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x0010BE17 File Offset: 0x0010A017
		// (set) Token: 0x06000CD6 RID: 3286 RVA: 0x0010BE1F File Offset: 0x0010A01F
		public bool ArmyTopUp { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0010BE28 File Offset: 0x0010A028
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x0010BE30 File Offset: 0x0010A030
		public int LastSuccessionDay { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0010BE39 File Offset: 0x0010A039
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x0010BE41 File Offset: 0x0010A041
		public int NextSuccessionDay { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0010BE4A File Offset: 0x0010A04A
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x0010BE52 File Offset: 0x0010A052
		public int LastDecayDay { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0010BE5B File Offset: 0x0010A05B
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x0010BE63 File Offset: 0x0010A063
		public string ClearedCamps { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0010BE6C File Offset: 0x0010A06C
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x0010BE74 File Offset: 0x0010A074
		public string Slot { get; set; }
	}
}
