using System;
using System.Runtime.CompilerServices;

namespace NemesisSystem
{
	// Token: 0x02000059 RID: 89
	public class HistoryEntry
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0010DB24 File Offset: 0x0010BD24
		// (set) Token: 0x06000F55 RID: 3925 RVA: 0x0010DB2C File Offset: 0x0010BD2C
		public DateTime At { get; set; }

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0010DB35 File Offset: 0x0010BD35
		// (set) Token: 0x06000F57 RID: 3927 RVA: 0x0010DB3D File Offset: 0x0010BD3D
		public string When { get; set; }

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0010DB46 File Offset: 0x0010BD46
		// (set) Token: 0x06000F59 RID: 3929 RVA: 0x0010DB4E File Offset: 0x0010BD4E
		public string Text { get; set; }

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0010DB57 File Offset: 0x0010BD57
		// (set) Token: 0x06000F5B RID: 3931 RVA: 0x0010DB5F File Offset: 0x0010BD5F
		[TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})]
		public ValueTuple<int, int, int> Color { [return: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] get; [param: TupleElementNames(new string[]
		{
			"r",
			"g",
			"b"
		})] set; }

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0010DB68 File Offset: 0x0010BD68
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x0010DB70 File Offset: 0x0010BD70
		public bool Secret { get; set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0010DB79 File Offset: 0x0010BD79
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x0010DB81 File Offset: 0x0010BD81
		public bool Cover { get; set; }
	}
}
