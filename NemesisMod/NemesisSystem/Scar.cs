using System;

namespace NemesisSystem
{
	// Token: 0x0200005A RID: 90
	public class Scar
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000F61 RID: 3937 RVA: 0x0010DB92 File Offset: 0x0010BD92
		// (set) Token: 0x06000F62 RID: 3938 RVA: 0x0010DB9A File Offset: 0x0010BD9A
		public string DamagePack { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000F63 RID: 3939 RVA: 0x0010DBA3 File Offset: 0x0010BDA3
		// (set) Token: 0x06000F64 RID: 3940 RVA: 0x0010DBAB File Offset: 0x0010BDAB
		public string Description { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000F65 RID: 3941 RVA: 0x0010DBB4 File Offset: 0x0010BDB4
		// (set) Token: 0x06000F66 RID: 3942 RVA: 0x0010DBBC File Offset: 0x0010BDBC
		public string Wound { get; set; }

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x0010DBC5 File Offset: 0x0010BDC5
		// (set) Token: 0x06000F68 RID: 3944 RVA: 0x0010DBCD File Offset: 0x0010BDCD
		public string Location { get; set; }

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000F69 RID: 3945 RVA: 0x0010DBD6 File Offset: 0x0010BDD6
		// (set) Token: 0x06000F6A RID: 3946 RVA: 0x0010DBDE File Offset: 0x0010BDDE
		public DateTime ReceivedAt { get; set; }

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000F6B RID: 3947 RVA: 0x0010DBE7 File Offset: 0x0010BDE7
		// (set) Token: 0x06000F6C RID: 3948 RVA: 0x0010DBEF File Offset: 0x0010BDEF
		public bool? FromYou { get; set; }
	}
}
