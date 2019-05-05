using System;
using System.Globalization;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public struct Plane : IEquatable<Plane>
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00017D2B File Offset: 0x00015F2B
		public Plane(float a, float b, float c, float d)
		{
			this.Normal.X = a;
			this.Normal.Y = b;
			this.Normal.Z = c;
			this.D = d;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00017D59 File Offset: 0x00015F59
		public Plane(Vector3 normal, float d)
		{
			this.Normal = normal;
			this.D = d;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00017D6C File Offset: 0x00015F6C
		public Plane(Vector4 value)
		{
			this.Normal.X = value.X;
			this.Normal.Y = value.Y;
			this.Normal.Z = value.Z;
			this.D = value.W;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00017DB8 File Offset: 0x00015FB8
		public Plane(Vector3 point1, Vector3 point2, Vector3 point3)
		{
			float num = point2.X - point1.X;
			float num2 = point2.Y - point1.Y;
			float num3 = point2.Z - point1.Z;
			float num4 = point3.X - point1.X;
			float num5 = point3.Y - point1.Y;
			float num6 = point3.Z - point1.Z;
			float num7 = num2 * num6 - num3 * num5;
			float num8 = num3 * num4 - num * num6;
			float num9 = num * num5 - num2 * num4;
			float num10 = num7 * num7 + num8 * num8 + num9 * num9;
			float num11 = 1f / (float)Math.Sqrt((double)num10);
			this.Normal.X = num7 * num11;
			this.Normal.Y = num8 * num11;
			this.Normal.Z = num9 * num11;
			this.D = -(this.Normal.X * point1.X + this.Normal.Y * point1.Y + this.Normal.Z * point1.Z);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00017ED0 File Offset: 0x000160D0
		public bool Equals(Plane other)
		{
			return this.Normal.X == other.Normal.X && this.Normal.Y == other.Normal.Y && this.Normal.Z == other.Normal.Z && this.D == other.D;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00017F38 File Offset: 0x00016138
		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj is Plane)
			{
				result = this.Equals((Plane)obj);
			}
			return result;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00017F5D File Offset: 0x0001615D
		public override int GetHashCode()
		{
			return this.Normal.GetHashCode() + this.D.GetHashCode();
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00017F7C File Offset: 0x0001617C
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{Normal:{0} D:{1}}}", new object[]
			{
				this.Normal.ToString(),
				this.D.ToString(currentCulture)
			});
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00017FC4 File Offset: 0x000161C4
		public void Normalize()
		{
			float num = this.Normal.X * this.Normal.X + this.Normal.Y * this.Normal.Y + this.Normal.Z * this.Normal.Z;
			if (Math.Abs(num - 1f) < 1.1920929E-07f)
			{
				return;
			}
			float num2 = 1f / (float)Math.Sqrt((double)num);
			this.Normal.X = this.Normal.X * num2;
			this.Normal.Y = this.Normal.Y * num2;
			this.Normal.Z = this.Normal.Z * num2;
			this.D *= num2;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00018094 File Offset: 0x00016294
		public static Plane Normalize(Plane value)
		{
			float num = value.Normal.X * value.Normal.X + value.Normal.Y * value.Normal.Y + value.Normal.Z * value.Normal.Z;
			Plane result;
			if (Math.Abs(num - 1f) < 1.1920929E-07f)
			{
				result.Normal = value.Normal;
				result.D = value.D;
				return result;
			}
			float num2 = 1f / (float)Math.Sqrt((double)num);
			result.Normal.X = value.Normal.X * num2;
			result.Normal.Y = value.Normal.Y * num2;
			result.Normal.Z = value.Normal.Z * num2;
			result.D = value.D * num2;
			return result;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00018184 File Offset: 0x00016384
		public static void Normalize(ref Plane value, out Plane result)
		{
			float num = value.Normal.X * value.Normal.X + value.Normal.Y * value.Normal.Y + value.Normal.Z * value.Normal.Z;
			if (Math.Abs(num - 1f) < 1.1920929E-07f)
			{
				result.Normal = value.Normal;
				result.D = value.D;
				return;
			}
			float num2 = 1f / (float)Math.Sqrt((double)num);
			result.Normal.X = value.Normal.X * num2;
			result.Normal.Y = value.Normal.Y * num2;
			result.Normal.Z = value.Normal.Z * num2;
			result.D = value.D * num2;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0001826C File Offset: 0x0001646C
		public static Plane Transform(Plane plane, Matrix matrix)
		{
			Matrix matrix2;
			Matrix.Invert(ref matrix, out matrix2);
			float x = plane.Normal.X;
			float y = plane.Normal.Y;
			float z = plane.Normal.Z;
			float d = plane.D;
			Plane result;
			result.Normal.X = x * matrix2.M11 + y * matrix2.M12 + z * matrix2.M13 + d * matrix2.M14;
			result.Normal.Y = x * matrix2.M21 + y * matrix2.M22 + z * matrix2.M23 + d * matrix2.M24;
			result.Normal.Z = x * matrix2.M31 + y * matrix2.M32 + z * matrix2.M33 + d * matrix2.M34;
			result.D = x * matrix2.M41 + y * matrix2.M42 + z * matrix2.M43 + d * matrix2.M44;
			return result;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0001836C File Offset: 0x0001656C
		public static void Transform(ref Plane plane, ref Matrix matrix, out Plane result)
		{
			Matrix matrix2;
			Matrix.Invert(ref matrix, out matrix2);
			float x = plane.Normal.X;
			float y = plane.Normal.Y;
			float z = plane.Normal.Z;
			float d = plane.D;
			result.Normal.X = x * matrix2.M11 + y * matrix2.M12 + z * matrix2.M13 + d * matrix2.M14;
			result.Normal.Y = x * matrix2.M21 + y * matrix2.M22 + z * matrix2.M23 + d * matrix2.M24;
			result.Normal.Z = x * matrix2.M31 + y * matrix2.M32 + z * matrix2.M33 + d * matrix2.M34;
			result.D = x * matrix2.M41 + y * matrix2.M42 + z * matrix2.M43 + d * matrix2.M44;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00018464 File Offset: 0x00016664
		public float Dot(Vector4 value)
		{
			return this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D * value.W;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000184B8 File Offset: 0x000166B8
		public void Dot(ref Vector4 value, out float result)
		{
			result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D * value.W;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00018510 File Offset: 0x00016710
		public float DotCoordinate(Vector3 value)
		{
			return this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0001855C File Offset: 0x0001675C
		public void DotCoordinate(ref Vector3 value, out float result)
		{
			result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z + this.D;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000185AA File Offset: 0x000167AA
		public float DotNormal(Vector3 value)
		{
			return this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000185E4 File Offset: 0x000167E4
		public void DotNormal(ref Vector3 value, out float result)
		{
			result = this.Normal.X * value.X + this.Normal.Y * value.Y + this.Normal.Z * value.Z;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00018620 File Offset: 0x00016820
		public PlaneIntersectionType Intersects(BoundingBox box)
		{
			Vector3 vector;
			vector.X = ((this.Normal.X >= 0f) ? box.Min.X : box.Max.X);
			vector.Y = ((this.Normal.Y >= 0f) ? box.Min.Y : box.Max.Y);
			vector.Z = ((this.Normal.Z >= 0f) ? box.Min.Z : box.Max.Z);
			Vector3 vector2;
			vector2.X = ((this.Normal.X >= 0f) ? box.Max.X : box.Min.X);
			vector2.Y = ((this.Normal.Y >= 0f) ? box.Max.Y : box.Min.Y);
			vector2.Z = ((this.Normal.Z >= 0f) ? box.Max.Z : box.Min.Z);
			if (this.Normal.X * vector.X + this.Normal.Y * vector.Y + this.Normal.Z * vector.Z + this.D > 0f)
			{
				return PlaneIntersectionType.Front;
			}
			if (this.Normal.X * vector2.X + this.Normal.Y * vector2.Y + this.Normal.Z * vector2.Z + this.D < 0f)
			{
				return PlaneIntersectionType.Back;
			}
			return PlaneIntersectionType.Intersecting;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x000187E4 File Offset: 0x000169E4
		public void Intersects(ref BoundingBox box, out PlaneIntersectionType result)
		{
			Vector3 vector;
			vector.X = ((this.Normal.X >= 0f) ? box.Min.X : box.Max.X);
			vector.Y = ((this.Normal.Y >= 0f) ? box.Min.Y : box.Max.Y);
			vector.Z = ((this.Normal.Z >= 0f) ? box.Min.Z : box.Max.Z);
			Vector3 vector2;
			vector2.X = ((this.Normal.X >= 0f) ? box.Max.X : box.Min.X);
			vector2.Y = ((this.Normal.Y >= 0f) ? box.Max.Y : box.Min.Y);
			vector2.Z = ((this.Normal.Z >= 0f) ? box.Max.Z : box.Min.Z);
			if (this.Normal.X * vector.X + this.Normal.Y * vector.Y + this.Normal.Z * vector.Z + this.D > 0f)
			{
				result = PlaneIntersectionType.Front;
				return;
			}
			if (this.Normal.X * vector2.X + this.Normal.Y * vector2.Y + this.Normal.Z * vector2.Z + this.D < 0f)
			{
				result = PlaneIntersectionType.Back;
				return;
			}
			result = PlaneIntersectionType.Intersecting;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000189AE File Offset: 0x00016BAE
		public static bool operator ==(Plane lhs, Plane rhs)
		{
			return lhs.Equals(rhs);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000189B8 File Offset: 0x00016BB8
		public static bool operator !=(Plane lhs, Plane rhs)
		{
			return lhs.Normal.X != rhs.Normal.X || lhs.Normal.Y != rhs.Normal.Y || lhs.Normal.Z != rhs.Normal.Z || lhs.D != rhs.D;
		}

		// Token: 0x04000151 RID: 337
		public Vector3 Normal;

		// Token: 0x04000152 RID: 338
		public float D;
	}
}
