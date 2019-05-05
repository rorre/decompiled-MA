using System;
using System.Globalization;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	public struct Vector4 : IEquatable<Vector4>
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x0001C36E File Offset: 0x0001A56E
		public static Vector4 Zero
		{
			get
			{
				return Vector4._zero;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x0001C375 File Offset: 0x0001A575
		public static Vector4 One
		{
			get
			{
				return Vector4._one;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0001C37C File Offset: 0x0001A57C
		public static Vector4 UnitX
		{
			get
			{
				return Vector4._unitX;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060001FB RID: 507 RVA: 0x0001C383 File Offset: 0x0001A583
		public static Vector4 UnitY
		{
			get
			{
				return Vector4._unitY;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0001C38A File Offset: 0x0001A58A
		public static Vector4 UnitZ
		{
			get
			{
				return Vector4._unitZ;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0001C391 File Offset: 0x0001A591
		public static Vector4 UnitW
		{
			get
			{
				return Vector4._unitW;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0001C398 File Offset: 0x0001A598
		public Vector4(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x0001C3B7 File Offset: 0x0001A5B7
		public Vector4(Vector2 value, float z, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
			this.W = w;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0001C3DF File Offset: 0x0001A5DF
		public Vector4(Vector3 value, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = value.Z;
			this.W = w;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0001C40C File Offset: 0x0001A60C
		public Vector4(float value)
		{
			this.W = value;
			this.Z = value;
			this.Y = value;
			this.X = value;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0001C42C File Offset: 0x0001A62C
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{X:{0} Y:{1} Z:{2} W:{3}}}", new object[]
			{
				this.X.ToString(currentCulture),
				this.Y.ToString(currentCulture),
				this.Z.ToString(currentCulture),
				this.W.ToString(currentCulture)
			});
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0001C48C File Offset: 0x0001A68C
		public bool Equals(Vector4 other)
		{
			return this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.W == other.W;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0001C4C8 File Offset: 0x0001A6C8
		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj is Vector4)
			{
				result = this.Equals((Vector4)obj);
			}
			return result;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0001C4ED File Offset: 0x0001A6ED
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode() + this.W.GetHashCode();
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0001C51E File Offset: 0x0001A71E
		public float Length()
		{
			return (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W));
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0001C55E File Offset: 0x0001A75E
		public float LengthSquared()
		{
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0001C598 File Offset: 0x0001A798
		public static float Distance(Vector4 value1, Vector4 value2)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float num4 = value1.W - value2.W;
			return (float)Math.Sqrt((double)(num * num + num2 * num2 + num3 * num3 + num4 * num4));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0001C5F4 File Offset: 0x0001A7F4
		public static void Distance(ref Vector4 value1, ref Vector4 value2, out float result)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float num4 = value1.W - value2.W;
			float num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
			result = (float)Math.Sqrt((double)num5);
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0001C654 File Offset: 0x0001A854
		public static float DistanceSquared(Vector4 value1, Vector4 value2)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float num4 = value1.W - value2.W;
			return num * num + num2 * num2 + num3 * num3 + num4 * num4;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0001C6A8 File Offset: 0x0001A8A8
		public static void DistanceSquared(ref Vector4 value1, ref Vector4 value2, out float result)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float num4 = value1.W - value2.W;
			result = num * num + num2 * num2 + num3 * num3 + num4 * num4;
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0001C6FE File Offset: 0x0001A8FE
		public static float Dot(Vector4 vector1, Vector4 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0001C737 File Offset: 0x0001A937
		public static void Dot(ref Vector4 vector1, ref Vector4 vector2, out float result)
		{
			result = vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z + vector1.W * vector2.W;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0001C774 File Offset: 0x0001A974
		public void Normalize()
		{
			float num = this.X * this.X + this.Y * this.Y + this.Z * this.Z + this.W * this.W;
			float num2 = 1f / (float)Math.Sqrt((double)num);
			this.X *= num2;
			this.Y *= num2;
			this.Z *= num2;
			this.W *= num2;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x0001C800 File Offset: 0x0001AA00
		public static Vector4 Normalize(Vector4 vector)
		{
			float num = vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W;
			float num2 = 1f / (float)Math.Sqrt((double)num);
			Vector4 result;
			result.X = vector.X * num2;
			result.Y = vector.Y * num2;
			result.Z = vector.Z * num2;
			result.W = vector.W * num2;
			return result;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0001C894 File Offset: 0x0001AA94
		public static void Normalize(ref Vector4 vector, out Vector4 result)
		{
			float num = vector.X * vector.X + vector.Y * vector.Y + vector.Z * vector.Z + vector.W * vector.W;
			float num2 = 1f / (float)Math.Sqrt((double)num);
			result.X = vector.X * num2;
			result.Y = vector.Y * num2;
			result.Z = vector.Z * num2;
			result.W = vector.W * num2;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0001C920 File Offset: 0x0001AB20
		public static Vector4 Min(Vector4 value1, Vector4 value2)
		{
			Vector4 result;
			result.X = ((value1.X < value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
			result.Z = ((value1.Z < value2.Z) ? value1.Z : value2.Z);
			result.W = ((value1.W < value2.W) ? value1.W : value2.W);
			return result;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0001C9BC File Offset: 0x0001ABBC
		public static void Min(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = ((value1.X < value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
			result.Z = ((value1.Z < value2.Z) ? value1.Z : value2.Z);
			result.W = ((value1.W < value2.W) ? value1.W : value2.W);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0001CA54 File Offset: 0x0001AC54
		public static Vector4 Max(Vector4 value1, Vector4 value2)
		{
			Vector4 result;
			result.X = ((value1.X > value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
			result.Z = ((value1.Z > value2.Z) ? value1.Z : value2.Z);
			result.W = ((value1.W > value2.W) ? value1.W : value2.W);
			return result;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0001CAF0 File Offset: 0x0001ACF0
		public static void Max(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = ((value1.X > value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
			result.Z = ((value1.Z > value2.Z) ? value1.Z : value2.Z);
			result.W = ((value1.W > value2.W) ? value1.W : value2.W);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0001CB88 File Offset: 0x0001AD88
		public static Vector4 Clamp(Vector4 value1, Vector4 min, Vector4 max)
		{
			float num = value1.X;
			num = ((num > max.X) ? max.X : num);
			num = ((num < min.X) ? min.X : num);
			float num2 = value1.Y;
			num2 = ((num2 > max.Y) ? max.Y : num2);
			num2 = ((num2 < min.Y) ? min.Y : num2);
			float num3 = value1.Z;
			num3 = ((num3 > max.Z) ? max.Z : num3);
			num3 = ((num3 < min.Z) ? min.Z : num3);
			float num4 = value1.W;
			num4 = ((num4 > max.W) ? max.W : num4);
			num4 = ((num4 < min.W) ? min.W : num4);
			Vector4 result;
			result.X = num;
			result.Y = num2;
			result.Z = num3;
			result.W = num4;
			return result;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0001CC6C File Offset: 0x0001AE6C
		public static void Clamp(ref Vector4 value1, ref Vector4 min, ref Vector4 max, out Vector4 result)
		{
			float num = value1.X;
			num = ((num > max.X) ? max.X : num);
			num = ((num < min.X) ? min.X : num);
			float num2 = value1.Y;
			num2 = ((num2 > max.Y) ? max.Y : num2);
			num2 = ((num2 < min.Y) ? min.Y : num2);
			float num3 = value1.Z;
			num3 = ((num3 > max.Z) ? max.Z : num3);
			num3 = ((num3 < min.Z) ? min.Z : num3);
			float num4 = value1.W;
			num4 = ((num4 > max.W) ? max.W : num4);
			num4 = ((num4 < min.W) ? min.W : num4);
			result.X = num;
			result.Y = num2;
			result.Z = num3;
			result.W = num4;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0001CD4C File Offset: 0x0001AF4C
		public static Vector4 Lerp(Vector4 value1, Vector4 value2, float amount)
		{
			Vector4 result;
			result.X = value1.X + (value2.X - value1.X) * amount;
			result.Y = value1.Y + (value2.Y - value1.Y) * amount;
			result.Z = value1.Z + (value2.Z - value1.Z) * amount;
			result.W = value1.W + (value2.W - value1.W) * amount;
			return result;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0001CDD0 File Offset: 0x0001AFD0
		public static void Lerp(ref Vector4 value1, ref Vector4 value2, float amount, out Vector4 result)
		{
			result.X = value1.X + (value2.X - value1.X) * amount;
			result.Y = value1.Y + (value2.Y - value1.Y) * amount;
			result.Z = value1.Z + (value2.Z - value1.Z) * amount;
			result.W = value1.W + (value2.W - value1.W) * amount;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0001CE50 File Offset: 0x0001B050
		public static Vector4 Barycentric(Vector4 value1, Vector4 value2, Vector4 value3, float amount1, float amount2)
		{
			Vector4 result;
			result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
			result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
			result.Z = value1.Z + amount1 * (value2.Z - value1.Z) + amount2 * (value3.Z - value1.Z);
			result.W = value1.W + amount1 * (value2.W - value1.W) + amount2 * (value3.W - value1.W);
			return result;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0001CF18 File Offset: 0x0001B118
		public static void Barycentric(ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, float amount1, float amount2, out Vector4 result)
		{
			result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
			result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
			result.Z = value1.Z + amount1 * (value2.Z - value1.Z) + amount2 * (value3.Z - value1.Z);
			result.W = value1.W + amount1 * (value2.W - value1.W) + amount2 * (value3.W - value1.W);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0001CFE0 File Offset: 0x0001B1E0
		public static Vector4 SmoothStep(Vector4 value1, Vector4 value2, float amount)
		{
			amount = ((amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount));
			amount = amount * amount * (3f - 2f * amount);
			Vector4 result;
			result.X = value1.X + (value2.X - value1.X) * amount;
			result.Y = value1.Y + (value2.Y - value1.Y) * amount;
			result.Z = value1.Z + (value2.Z - value1.Z) * amount;
			result.W = value1.W + (value2.W - value1.W) * amount;
			return result;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0001D098 File Offset: 0x0001B298
		public static void SmoothStep(ref Vector4 value1, ref Vector4 value2, float amount, out Vector4 result)
		{
			amount = ((amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount));
			amount = amount * amount * (3f - 2f * amount);
			result.X = value1.X + (value2.X - value1.X) * amount;
			result.Y = value1.Y + (value2.Y - value1.Y) * amount;
			result.Z = value1.Z + (value2.Z - value1.Z) * amount;
			result.W = value1.W + (value2.W - value1.W) * amount;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0001D14C File Offset: 0x0001B34C
		public static Vector4 CatmullRom(Vector4 value1, Vector4 value2, Vector4 value3, Vector4 value4, float amount)
		{
			float num = amount * amount;
			float num2 = amount * num;
			Vector4 result;
			result.X = 0.5f * (2f * value2.X + (-value1.X + value3.X) * amount + (2f * value1.X - 5f * value2.X + 4f * value3.X - value4.X) * num + (-value1.X + 3f * value2.X - 3f * value3.X + value4.X) * num2);
			result.Y = 0.5f * (2f * value2.Y + (-value1.Y + value3.Y) * amount + (2f * value1.Y - 5f * value2.Y + 4f * value3.Y - value4.Y) * num + (-value1.Y + 3f * value2.Y - 3f * value3.Y + value4.Y) * num2);
			result.Z = 0.5f * (2f * value2.Z + (-value1.Z + value3.Z) * amount + (2f * value1.Z - 5f * value2.Z + 4f * value3.Z - value4.Z) * num + (-value1.Z + 3f * value2.Z - 3f * value3.Z + value4.Z) * num2);
			result.W = 0.5f * (2f * value2.W + (-value1.W + value3.W) * amount + (2f * value1.W - 5f * value2.W + 4f * value3.W - value4.W) * num + (-value1.W + 3f * value2.W - 3f * value3.W + value4.W) * num2);
			return result;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0001D380 File Offset: 0x0001B580
		public static void CatmullRom(ref Vector4 value1, ref Vector4 value2, ref Vector4 value3, ref Vector4 value4, float amount, out Vector4 result)
		{
			float num = amount * amount;
			float num2 = amount * num;
			result.X = 0.5f * (2f * value2.X + (-value1.X + value3.X) * amount + (2f * value1.X - 5f * value2.X + 4f * value3.X - value4.X) * num + (-value1.X + 3f * value2.X - 3f * value3.X + value4.X) * num2);
			result.Y = 0.5f * (2f * value2.Y + (-value1.Y + value3.Y) * amount + (2f * value1.Y - 5f * value2.Y + 4f * value3.Y - value4.Y) * num + (-value1.Y + 3f * value2.Y - 3f * value3.Y + value4.Y) * num2);
			result.Z = 0.5f * (2f * value2.Z + (-value1.Z + value3.Z) * amount + (2f * value1.Z - 5f * value2.Z + 4f * value3.Z - value4.Z) * num + (-value1.Z + 3f * value2.Z - 3f * value3.Z + value4.Z) * num2);
			result.W = 0.5f * (2f * value2.W + (-value1.W + value3.W) * amount + (2f * value1.W - 5f * value2.W + 4f * value3.W - value4.W) * num + (-value1.W + 3f * value2.W - 3f * value3.W + value4.W) * num2);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0001D5B0 File Offset: 0x0001B7B0
		public static Vector4 Hermite(Vector4 value1, Vector4 tangent1, Vector4 value2, Vector4 tangent2, float amount)
		{
			float num = amount * amount;
			float num2 = amount * num;
			float num3 = 2f * num2 - 3f * num + 1f;
			float num4 = -2f * num2 + 3f * num;
			float num5 = num2 - 2f * num + amount;
			float num6 = num2 - num;
			Vector4 result;
			result.X = value1.X * num3 + value2.X * num4 + tangent1.X * num5 + tangent2.X * num6;
			result.Y = value1.Y * num3 + value2.Y * num4 + tangent1.Y * num5 + tangent2.Y * num6;
			result.Z = value1.Z * num3 + value2.Z * num4 + tangent1.Z * num5 + tangent2.Z * num6;
			result.W = value1.W * num3 + value2.W * num4 + tangent1.W * num5 + tangent2.W * num6;
			return result;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0001D6B4 File Offset: 0x0001B8B4
		public static void Hermite(ref Vector4 value1, ref Vector4 tangent1, ref Vector4 value2, ref Vector4 tangent2, float amount, out Vector4 result)
		{
			float num = amount * amount;
			float num2 = amount * num;
			float num3 = 2f * num2 - 3f * num + 1f;
			float num4 = -2f * num2 + 3f * num;
			float num5 = num2 - 2f * num + amount;
			float num6 = num2 - num;
			result.X = value1.X * num3 + value2.X * num4 + tangent1.X * num5 + tangent2.X * num6;
			result.Y = value1.Y * num3 + value2.Y * num4 + tangent1.Y * num5 + tangent2.Y * num6;
			result.Z = value1.Z * num3 + value2.Z * num4 + tangent1.Z * num5 + tangent2.Z * num6;
			result.W = value1.W * num3 + value2.W * num4 + tangent1.W * num5 + tangent2.W * num6;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0001D7B8 File Offset: 0x0001B9B8
		public static Vector4 Transform(Vector2 position, Matrix matrix)
		{
			float x = position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41;
			float y = position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42;
			float z = position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43;
			float w = position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44;
			Vector4 result;
			result.X = x;
			result.Y = y;
			result.Z = z;
			result.W = w;
			return result;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0001D874 File Offset: 0x0001BA74
		public static void Transform(ref Vector2 position, ref Matrix matrix, out Vector4 result)
		{
			float x = position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41;
			float y = position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42;
			float z = position.X * matrix.M13 + position.Y * matrix.M23 + matrix.M43;
			float w = position.X * matrix.M14 + position.Y * matrix.M24 + matrix.M44;
			result.X = x;
			result.Y = y;
			result.Z = z;
			result.W = w;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0001D92C File Offset: 0x0001BB2C
		public static Vector4 Transform(Vector3 position, Matrix matrix)
		{
			float x = position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41;
			float y = position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42;
			float z = position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43;
			float w = position.X * matrix.M14 + position.Y * matrix.M24 + position.Z * matrix.M34 + matrix.M44;
			Vector4 result;
			result.X = x;
			result.Y = y;
			result.Z = z;
			result.W = w;
			return result;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0001DA20 File Offset: 0x0001BC20
		public static void Transform(ref Vector3 position, ref Matrix matrix, out Vector4 result)
		{
			float x = position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41;
			float y = position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42;
			float z = position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43;
			float w = position.X * matrix.M14 + position.Y * matrix.M24 + position.Z * matrix.M34 + matrix.M44;
			result.X = x;
			result.Y = y;
			result.Z = z;
			result.W = w;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0001DB10 File Offset: 0x0001BD10
		public static Vector4 Transform(Vector4 vector, Matrix matrix)
		{
			float x = vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41;
			float y = vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42;
			float z = vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43;
			float w = vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44;
			Vector4 result;
			result.X = x;
			result.Y = y;
			result.Z = z;
			result.W = w;
			return result;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0001DC20 File Offset: 0x0001BE20
		public static void Transform(ref Vector4 vector, ref Matrix matrix, out Vector4 result)
		{
			float x = vector.X * matrix.M11 + vector.Y * matrix.M21 + vector.Z * matrix.M31 + vector.W * matrix.M41;
			float y = vector.X * matrix.M12 + vector.Y * matrix.M22 + vector.Z * matrix.M32 + vector.W * matrix.M42;
			float z = vector.X * matrix.M13 + vector.Y * matrix.M23 + vector.Z * matrix.M33 + vector.W * matrix.M43;
			float w = vector.X * matrix.M14 + vector.Y * matrix.M24 + vector.Z * matrix.M34 + vector.W * matrix.M44;
			result.X = x;
			result.Y = y;
			result.Z = z;
			result.W = w;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0001DD2C File Offset: 0x0001BF2C
		public static void Transform(Vector4[] sourceArray, ref Matrix matrix, Vector4[] destinationArray)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("values");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("results");
			}
			if (destinationArray.Length < sourceArray.Length)
			{
				throw new IndexOutOfRangeException(MathResources.NotEnoughTargetSize);
			}
			for (int i = 0; i < sourceArray.Length; i++)
			{
				float x = sourceArray[i].X;
				float y = sourceArray[i].Y;
				float z = sourceArray[i].Z;
				float w = sourceArray[i].W;
				destinationArray[i].X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31 + w * matrix.M41;
				destinationArray[i].Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32 + w * matrix.M42;
				destinationArray[i].Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33 + w * matrix.M43;
				destinationArray[i].W = x * matrix.M14 + y * matrix.M24 + z * matrix.M34 + w * matrix.M44;
			}
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0001DE74 File Offset: 0x0001C074
		public static void Transform(Vector4[] sourceArray, int sourceIndex, ref Matrix matrix, Vector4[] destinationArray, int destinationIndex, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("values");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("results");
			}
			if ((long)sourceArray.Length < (long)sourceIndex + (long)length)
			{
				throw new IndexOutOfRangeException(MathResources.NotEnoughSourceSize);
			}
			if ((long)destinationArray.Length < (long)destinationIndex + (long)length)
			{
				throw new IndexOutOfRangeException(MathResources.NotEnoughTargetSize);
			}
			while (length > 0)
			{
				float x = sourceArray[sourceIndex].X;
				float y = sourceArray[sourceIndex].Y;
				float z = sourceArray[sourceIndex].Z;
				float w = sourceArray[sourceIndex].W;
				destinationArray[destinationIndex].X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31 + w * matrix.M41;
				destinationArray[destinationIndex].Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32 + w * matrix.M42;
				destinationArray[destinationIndex].Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33 + w * matrix.M43;
				destinationArray[destinationIndex].W = x * matrix.M14 + y * matrix.M24 + z * matrix.M34 + w * matrix.M44;
				sourceIndex++;
				destinationIndex++;
				length--;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0001DFDC File Offset: 0x0001C1DC
		public static Vector4 Negate(Vector4 value)
		{
			Vector4 result;
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = -value.W;
			return result;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0001E022 File Offset: 0x0001C222
		public static void Negate(ref Vector4 value, out Vector4 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = -value.W;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0001E058 File Offset: 0x0001C258
		public static Vector4 Add(Vector4 value1, Vector4 value2)
		{
			Vector4 result;
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			result.W = value1.W + value2.W;
			return result;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0001E0B8 File Offset: 0x0001C2B8
		public static void Add(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			result.W = value1.W + value2.W;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0001E114 File Offset: 0x0001C314
		public static Vector4 Subtract(Vector4 value1, Vector4 value2)
		{
			Vector4 result;
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			result.W = value1.W - value2.W;
			return result;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0001E174 File Offset: 0x0001C374
		public static void Subtract(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			result.W = value1.W - value2.W;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0001E1D0 File Offset: 0x0001C3D0
		public static Vector4 Multiply(Vector4 value1, Vector4 value2)
		{
			Vector4 result;
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
			result.W = value1.W * value2.W;
			return result;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0001E230 File Offset: 0x0001C430
		public static void Multiply(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
			result.W = value1.W * value2.W;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0001E28C File Offset: 0x0001C48C
		public static Vector4 Multiply(Vector4 value1, float scaleFactor)
		{
			Vector4 result;
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
			result.W = value1.W * scaleFactor;
			return result;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0001E2D6 File Offset: 0x0001C4D6
		public static void Multiply(ref Vector4 value1, float scaleFactor, out Vector4 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
			result.W = value1.W * scaleFactor;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0001E310 File Offset: 0x0001C510
		public static Vector4 Divide(Vector4 value1, Vector4 value2)
		{
			Vector4 result;
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
			result.W = value1.W / value2.W;
			return result;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0001E370 File Offset: 0x0001C570
		public static void Divide(ref Vector4 value1, ref Vector4 value2, out Vector4 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
			result.W = value1.W / value2.W;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0001E3CC File Offset: 0x0001C5CC
		public static Vector4 Divide(Vector4 value1, float divider)
		{
			float num = 1f / divider;
			Vector4 result;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			result.Z = value1.Z * num;
			result.W = value1.W * num;
			return result;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0001E420 File Offset: 0x0001C620
		public static void Divide(ref Vector4 value1, float divider, out Vector4 result)
		{
			float num = 1f / divider;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			result.Z = value1.Z * num;
			result.W = value1.W * num;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0001E470 File Offset: 0x0001C670
		public static Vector4 operator -(Vector4 value)
		{
			Vector4 result;
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			result.W = -value.W;
			return result;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0001C48C File Offset: 0x0001A68C
		public static bool operator ==(Vector4 value1, Vector4 value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z && value1.W == value2.W;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0001E4B6 File Offset: 0x0001C6B6
		public static bool operator !=(Vector4 value1, Vector4 value2)
		{
			return value1.X != value2.X || value1.Y != value2.Y || value1.Z != value2.Z || value1.W != value2.W;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0001E4F8 File Offset: 0x0001C6F8
		public static Vector4 operator +(Vector4 value1, Vector4 value2)
		{
			Vector4 result;
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			result.W = value1.W + value2.W;
			return result;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0001E558 File Offset: 0x0001C758
		public static Vector4 operator -(Vector4 value1, Vector4 value2)
		{
			Vector4 result;
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			result.W = value1.W - value2.W;
			return result;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0001E5B8 File Offset: 0x0001C7B8
		public static Vector4 operator *(Vector4 value1, Vector4 value2)
		{
			Vector4 result;
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
			result.W = value1.W * value2.W;
			return result;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0001E618 File Offset: 0x0001C818
		public static Vector4 operator *(Vector4 value1, float scaleFactor)
		{
			Vector4 result;
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
			result.W = value1.W * scaleFactor;
			return result;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0001E664 File Offset: 0x0001C864
		public static Vector4 operator *(float scaleFactor, Vector4 value1)
		{
			Vector4 result;
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
			result.W = value1.W * scaleFactor;
			return result;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0001E6B0 File Offset: 0x0001C8B0
		public static Vector4 operator /(Vector4 value1, Vector4 value2)
		{
			Vector4 result;
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
			result.W = value1.W / value2.W;
			return result;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0001E710 File Offset: 0x0001C910
		public static Vector4 operator /(Vector4 value1, float divider)
		{
			float num = 1f / divider;
			Vector4 result;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			result.Z = value1.Z * num;
			result.W = value1.W * num;
			return result;
		}

		// Token: 0x04000173 RID: 371
		public float X;

		// Token: 0x04000174 RID: 372
		public float Y;

		// Token: 0x04000175 RID: 373
		public float Z;

		// Token: 0x04000176 RID: 374
		public float W;

		// Token: 0x04000177 RID: 375
		private static Vector4 _zero = default(Vector4);

		// Token: 0x04000178 RID: 376
		private static Vector4 _one = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x04000179 RID: 377
		private static Vector4 _unitX = new Vector4(1f, 0f, 0f, 0f);

		// Token: 0x0400017A RID: 378
		private static Vector4 _unitY = new Vector4(0f, 1f, 0f, 0f);

		// Token: 0x0400017B RID: 379
		private static Vector4 _unitZ = new Vector4(0f, 0f, 1f, 0f);

		// Token: 0x0400017C RID: 380
		private static Vector4 _unitW = new Vector4(0f, 0f, 0f, 1f);
	}
}
