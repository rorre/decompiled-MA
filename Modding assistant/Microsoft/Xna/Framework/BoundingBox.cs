using System;
using System.Collections.Generic;
using System.Globalization;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000019 RID: 25
	[Serializable]
	public struct BoundingBox : IEquatable<BoundingBox>
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x000110E4 File Offset: 0x0000F2E4
		public Vector3[] GetCorners()
		{
			return new Vector3[]
			{
				new Vector3(this.Min.X, this.Max.Y, this.Max.Z),
				new Vector3(this.Max.X, this.Max.Y, this.Max.Z),
				new Vector3(this.Max.X, this.Min.Y, this.Max.Z),
				new Vector3(this.Min.X, this.Min.Y, this.Max.Z),
				new Vector3(this.Min.X, this.Max.Y, this.Min.Z),
				new Vector3(this.Max.X, this.Max.Y, this.Min.Z),
				new Vector3(this.Max.X, this.Min.Y, this.Min.Z),
				new Vector3(this.Min.X, this.Min.Y, this.Min.Z)
			};
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00011260 File Offset: 0x0000F460
		public void GetCorners(Vector3[] corners)
		{
			if (corners == null)
			{
				throw new ArgumentNullException("corners");
			}
			if (corners.Length < 8)
			{
				throw new ArgumentOutOfRangeException(MathResources.NotEnoughCorners);
			}
			corners[0].X = this.Min.X;
			corners[0].Y = this.Max.Y;
			corners[0].Z = this.Max.Z;
			corners[1].X = this.Max.X;
			corners[1].Y = this.Max.Y;
			corners[1].Z = this.Max.Z;
			corners[2].X = this.Max.X;
			corners[2].Y = this.Min.Y;
			corners[2].Z = this.Max.Z;
			corners[3].X = this.Min.X;
			corners[3].Y = this.Min.Y;
			corners[3].Z = this.Max.Z;
			corners[4].X = this.Min.X;
			corners[4].Y = this.Max.Y;
			corners[4].Z = this.Min.Z;
			corners[5].X = this.Max.X;
			corners[5].Y = this.Max.Y;
			corners[5].Z = this.Min.Z;
			corners[6].X = this.Max.X;
			corners[6].Y = this.Min.Y;
			corners[6].Z = this.Min.Z;
			corners[7].X = this.Min.X;
			corners[7].Y = this.Min.Y;
			corners[7].Z = this.Min.Z;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000114B4 File Offset: 0x0000F6B4
		public BoundingBox(Vector3 min, Vector3 max)
		{
			this.Min = min;
			this.Max = max;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000114C4 File Offset: 0x0000F6C4
		public bool Equals(BoundingBox other)
		{
			return this.Min == other.Min && this.Max == other.Max;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000114EC File Offset: 0x0000F6EC
		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj is BoundingBox)
			{
				result = this.Equals((BoundingBox)obj);
			}
			return result;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00011511 File Offset: 0x0000F711
		public override int GetHashCode()
		{
			return this.Min.GetHashCode() + this.Max.GetHashCode();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00011536 File Offset: 0x0000F736
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{{Min:{0} Max:{1}}}", new object[]
			{
				this.Min.ToString(),
				this.Max.ToString()
			});
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00011578 File Offset: 0x0000F778
		public static BoundingBox CreateMerged(BoundingBox original, BoundingBox additional)
		{
			BoundingBox result;
			Vector3.Min(ref original.Min, ref additional.Min, out result.Min);
			Vector3.Max(ref original.Max, ref additional.Max, out result.Max);
			return result;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000115BC File Offset: 0x0000F7BC
		public static void CreateMerged(ref BoundingBox original, ref BoundingBox additional, out BoundingBox result)
		{
			Vector3 min;
			Vector3.Min(ref original.Min, ref additional.Min, out min);
			Vector3 max;
			Vector3.Max(ref original.Max, ref additional.Max, out max);
			result.Min = min;
			result.Max = max;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00011600 File Offset: 0x0000F800
		public static BoundingBox CreateFromPoints(IEnumerable<Vector3> points)
		{
			if (points == null)
			{
				throw new ArgumentNullException();
			}
			bool flag = false;
			Vector3 min = new Vector3(float.MaxValue);
			Vector3 max = new Vector3(float.MinValue);
			foreach (Vector3 vector in points)
			{
				Vector3.Min(ref min, ref vector, out min);
				Vector3.Max(ref max, ref vector, out max);
				flag = true;
			}
			if (!flag)
			{
				throw new ArgumentException(MathResources.BoundingBoxZeroPoints);
			}
			return new BoundingBox(min, max);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00011694 File Offset: 0x0000F894
		public bool Intersects(BoundingBox box)
		{
			return this.Max.X >= box.Min.X && this.Min.X <= box.Max.X && this.Max.Y >= box.Min.Y && this.Min.Y <= box.Max.Y && this.Max.Z >= box.Min.Z && this.Min.Z <= box.Max.Z;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00011738 File Offset: 0x0000F938
		public void Intersects(ref BoundingBox box, out bool result)
		{
			result = false;
			if (this.Max.X < box.Min.X || this.Min.X > box.Max.X)
			{
				return;
			}
			if (this.Max.Y < box.Min.Y || this.Min.Y > box.Max.Y)
			{
				return;
			}
			if (this.Max.Z < box.Min.Z || this.Min.Z > box.Max.Z)
			{
				return;
			}
			result = true;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000117E0 File Offset: 0x0000F9E0
		public PlaneIntersectionType Intersects(Plane plane)
		{
			Vector3 vector;
			vector.X = ((plane.Normal.X >= 0f) ? this.Min.X : this.Max.X);
			vector.Y = ((plane.Normal.Y >= 0f) ? this.Min.Y : this.Max.Y);
			vector.Z = ((plane.Normal.Z >= 0f) ? this.Min.Z : this.Max.Z);
			Vector3 vector2;
			vector2.X = ((plane.Normal.X >= 0f) ? this.Max.X : this.Min.X);
			vector2.Y = ((plane.Normal.Y >= 0f) ? this.Max.Y : this.Min.Y);
			vector2.Z = ((plane.Normal.Z >= 0f) ? this.Max.Z : this.Min.Z);
			if (plane.Normal.X * vector.X + plane.Normal.Y * vector.Y + plane.Normal.Z * vector.Z + plane.D > 0f)
			{
				return PlaneIntersectionType.Front;
			}
			if (plane.Normal.X * vector2.X + plane.Normal.Y * vector2.Y + plane.Normal.Z * vector2.Z + plane.D < 0f)
			{
				return PlaneIntersectionType.Back;
			}
			return PlaneIntersectionType.Intersecting;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000119A4 File Offset: 0x0000FBA4
		public void Intersects(ref Plane plane, out PlaneIntersectionType result)
		{
			Vector3 vector;
			vector.X = ((plane.Normal.X >= 0f) ? this.Min.X : this.Max.X);
			vector.Y = ((plane.Normal.Y >= 0f) ? this.Min.Y : this.Max.Y);
			vector.Z = ((plane.Normal.Z >= 0f) ? this.Min.Z : this.Max.Z);
			Vector3 vector2;
			vector2.X = ((plane.Normal.X >= 0f) ? this.Max.X : this.Min.X);
			vector2.Y = ((plane.Normal.Y >= 0f) ? this.Max.Y : this.Min.Y);
			vector2.Z = ((plane.Normal.Z >= 0f) ? this.Max.Z : this.Min.Z);
			if (plane.Normal.X * vector.X + plane.Normal.Y * vector.Y + plane.Normal.Z * vector.Z + plane.D > 0f)
			{
				result = PlaneIntersectionType.Front;
				return;
			}
			if (plane.Normal.X * vector2.X + plane.Normal.Y * vector2.Y + plane.Normal.Z * vector2.Z + plane.D < 0f)
			{
				result = PlaneIntersectionType.Back;
				return;
			}
			result = PlaneIntersectionType.Intersecting;
		}

		// Token: 0x0400013B RID: 315
		public const int CornerCount = 8;

		// Token: 0x0400013C RID: 316
		public Vector3 Min;

		// Token: 0x0400013D RID: 317
		public Vector3 Max;
	}
}
