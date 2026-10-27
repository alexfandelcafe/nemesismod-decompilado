using System;

namespace NemesisSystem
{
	// Token: 0x0200002C RID: 44
	public class Agenda
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000B37 RID: 2871 RVA: 0x0010A4D7 File Offset: 0x001086D7
		// (set) Token: 0x06000B38 RID: 2872 RVA: 0x0010A4DF File Offset: 0x001086DF
		public string Kind { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000B39 RID: 2873 RVA: 0x0010A4E8 File Offset: 0x001086E8
		// (set) Token: 0x06000B3A RID: 2874 RVA: 0x0010A4F0 File Offset: 0x001086F0
		public string Target { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x0010A4F9 File Offset: 0x001086F9
		// (set) Token: 0x06000B3C RID: 2876 RVA: 0x0010A501 File Offset: 0x00108701
		public string By { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x0010A50A File Offset: 0x0010870A
		// (set) Token: 0x06000B3E RID: 2878 RVA: 0x0010A512 File Offset: 0x00108712
		public string ById { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x0010A51B File Offset: 0x0010871B
		// (set) Token: 0x06000B40 RID: 2880 RVA: 0x0010A523 File Offset: 0x00108723
		public int Wins { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0010A52C File Offset: 0x0010872C
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x0010A534 File Offset: 0x00108734
		public long SetAt { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x0010A53D File Offset: 0x0010873D
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x0010A545 File Offset: 0x00108745
		public long ReviewAt { get; set; }
	}
}
