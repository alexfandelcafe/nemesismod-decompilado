using System;

namespace NemesisSystem.BulletImpact
{
	// Token: 0x0200008A RID: 138
	public struct V3
	{
		// Token: 0x060013D1 RID: 5073 RVA: 0x0013C77C File Offset: 0x0013A97C
		public V3(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x0013C793 File Offset: 0x0013A993
		public static V3 operator +(V3 a, V3 b)
		{
			return new V3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x0013C7C1 File Offset: 0x0013A9C1
		public static V3 operator -(V3 a, V3 b)
		{
			return new V3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
		}

		// Token: 0x060013D4 RID: 5076 RVA: 0x0013C7EF File Offset: 0x0013A9EF
		public static V3 operator *(V3 a, float s)
		{
			return new V3(a.X * s, a.Y * s, a.Z * s);
		}

		// Token: 0x060013D5 RID: 5077 RVA: 0x0013C80E File Offset: 0x0013AA0E
		public static float Dot(V3 a, V3 b)
		{
			return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x0013C83C File Offset: 0x0013AA3C
		public static V3 Cross(V3 a, V3 b)
		{
			return new V3(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x0013C89F File Offset: 0x0013AA9F
		public float Length()
		{
			return (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y + this.Z * this.Z));
		}

		// Token: 0x060013D8 RID: 5080 RVA: 0x0013C8D4 File Offset: 0x0013AAD4
		public V3 Normalized()
		{
			float num = this.Length();
			if (num <= 1E-06f)
			{
				return new V3(0f, 0f, 0f);
			}
			return this * (1f / num);
		}

		// Token: 0x060013D9 RID: 5081 RVA: 0x0013C917 File Offset: 0x0013AB17
		public static V3 Lerp(V3 a, V3 b, float t)
		{
			return a + (b - a) * t;
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0013C92C File Offset: 0x0013AB2C
		public override string ToString()
		{
			return string.Format("({0:0.00},{1:0.00},{2:0.00})", this.X, this.Y, this.Z);
		}

		// Token: 0x04000D64 RID: 3428
		public float X;

		// Token: 0x04000D65 RID: 3429
		public float Y;

		// Token: 0x04000D66 RID: 3430
		public float Z;
	}
}
