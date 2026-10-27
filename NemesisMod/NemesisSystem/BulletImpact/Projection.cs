using System;
using RDR2.Math;
using RDR2.Native;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x020000A6 RID: 166
	internal static class Projection
	{
		// Token: 0x0600151E RID: 5406 RVA: 0x00155638 File Offset: 0x00153838
		public unsafe static bool ToScreen(Vector3 w, out float x, out float y)
		{
			float num;
			float num2;
			bool result = GRAPHICS.GET_SCREEN_COORD_FROM_WORLD_COORD(w.X, w.Y, w.Z, &num, &num2);
			x = num;
			y = num2;
			return result;
		}
	}
}
