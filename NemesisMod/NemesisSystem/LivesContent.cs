using System;
using System.Collections.Generic;

namespace NemesisSystem
{
	// Token: 0x0200000A RID: 10
	public class LivesContent
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00002685 File Offset: 0x00000885
		// (set) Token: 0x06000049 RID: 73 RVA: 0x0000268D File Offset: 0x0000088D
		public string About { get; set; } = "Life events for every man's past (Backstory.cs). Edit, add rows, reload the mod (Insert). who: any / man / woman / gang / loner / irish / irishborn / south / north / rich / poor / a faction id / trade:<Trade> (| = or). tilt: temper:points. fx: aggr:+N fear:+N friendly surly devout godless sharp dim. text tokens: {home} {town} {n}. mirrorTitle tokens: {name} {town}.";

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002696 File Offset: 0x00000896
		// (set) Token: 0x0600004B RID: 75 RVA: 0x0000269E File Offset: 0x0000089E
		public Dictionary<string, List<string>> Places { get; set; } = new Dictionary<string, List<string>>();

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000026A7 File Offset: 0x000008A7
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000026AF File Offset: 0x000008AF
		public List<Battle> GreyBattles { get; set; } = new List<Battle>();

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004E RID: 78 RVA: 0x000026B8 File Offset: 0x000008B8
		// (set) Token: 0x0600004F RID: 79 RVA: 0x000026C0 File Offset: 0x000008C0
		public List<Battle> BlueBattles { get; set; } = new List<Battle>();

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000050 RID: 80 RVA: 0x000026C9 File Offset: 0x000008C9
		// (set) Token: 0x06000051 RID: 81 RVA: 0x000026D1 File Offset: 0x000008D1
		public Dictionary<string, List<LifeRow>> Pools { get; set; } = new Dictionary<string, List<LifeRow>>();

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000052 RID: 82 RVA: 0x000026DA File Offset: 0x000008DA
		// (set) Token: 0x06000053 RID: 83 RVA: 0x000026E2 File Offset: 0x000008E2
		public List<string> Removed { get; set; } = new List<string>();
	}
}
