using System;
using RDR2.Math;
using RDR2.Native;

namespace NemesisSystem
{
	// Token: 0x0200006A RID: 106
	public static class Ray
	{
		// Token: 0x0600127D RID: 4733 RVA: 0x00122614 File Offset: 0x00120814
		public static bool Blocked(Vector3 from, Vector3 to, int flags, int ignore)
		{
			Vector3 vector;
			return Ray.Hit(from, to, flags, ignore, out vector);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x0012262C File Offset: 0x0012082C
		public unsafe static bool Hit(Vector3 from, Vector3 to, int flags, int ignore, out Vector3 at)
		{
			int num = SHAPETEST.START_EXPENSIVE_SYNCHRONOUS_SHAPE_TEST_LOS_PROBE(from.X, from.Y, from.Z, to.X, to.Y, to.Z, flags, ignore, 7);
			int num2 = 0;
			ulong* ptr = stackalloc ulong[(UIntPtr)32];
			ulong* ptr2 = stackalloc ulong[(UIntPtr)32];
			int num3 = 0;
			SHAPETEST.GET_SHAPE_TEST_RESULT(num, (bool*)(&num2), (Vector3*)ptr, (Vector3*)ptr2, &num3);
			at = ((num2 != 0) ? new Vector3(*(float*)ptr, *(float*)(ptr + 1), *(float*)(ptr + 2)) : to);
			return num2 != 0;
		}
	}
}
