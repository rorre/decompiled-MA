using System;
using System.Globalization;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public struct Vector3 : IEquatable<Vector3>
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0001A3C4 File Offset: 0x000185C4
		public static Vector3 Zero
		{
			get
			{
				return Vector3._zero;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0001A3CB File Offset: 0x000185CB
		public static Vector3 One
		{
			get
			{
				return Vector3._one;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x0001A3D2 File Offset: 0x000185D2
		public static Vector3 UnitX
		{
			get
			{
				return Vector3._unitX;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x0001A3D9 File Offset: 0x000185D9
		public static Vector3 UnitY
		{
			get
			{
				return Vector3._unitY;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060001AA RID: 426 RVA: 0x0001A3E0 File Offset: 0x000185E0
		public static Vector3 UnitZ
		{
			get
			{
				return Vector3._unitZ;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0001A3E7 File Offset: 0x000185E7
		public static Vector3 Up
		{
			get
			{
				return Vector3._up;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0001A3EE File Offset: 0x000185EE
		public static Vector3 Down
		{
			get
			{
				return Vector3._down;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060001AD RID: 429 RVA: 0x0001A3F5 File Offset: 0x000185F5
		public static Vector3 Right
		{
			get
			{
				return Vector3._right;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0001A3FC File Offset: 0x000185FC
		public static Vector3 Left
		{
			get
			{
				return Vector3._left;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0001A403 File Offset: 0x00018603
		public static Vector3 Forward
		{
			get
			{
				return Vector3._forward;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x0001A40A File Offset: 0x0001860A
		public static Vector3 Backward
		{
			get
			{
				return Vector3._backward;
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0001A411 File Offset: 0x00018611
		public Vector3(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0001A428 File Offset: 0x00018628
		public Vector3(float value)
		{
			this.Z = value;
			this.Y = value;
			this.X = value;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0001A43F File Offset: 0x0001863F
		public Vector3(Vector2 value, float z)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0001A460 File Offset: 0x00018660
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{X:{0} Y:{1} Z:{2}}}", new object[]
			{
				this.X.ToString(currentCulture),
				this.Y.ToString(currentCulture),
				this.Z.ToString(currentCulture)
			});
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0001A4B1 File Offset: 0x000186B1
		public bool Equals(Vector3 other)
		{
			return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0001A4E0 File Offset: 0x000186E0
		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj is Vector3)
			{
				result = this.Equals((Vector3)obj);
			}
			return result;
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0001A505 File Offset: 0x00018705
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode() + this.Z.GetHashCode();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0001A52A File Offset: 0x0001872A
		public float Length()
		{
			return (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y + this.Z * this.Z));
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0001A55C File Offset: 0x0001875C
		public float LengthSquared()
		{
			return this.X * this.X + this.Y * this.Y + this.Z * this.Z;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0001A588 File Offset: 0x00018788
		public static float Distance(Vector3 value1, Vector3 value2)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			return (float)Math.Sqrt((double)(num * num + num2 * num2 + num3 * num3));
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0001A5D0 File Offset: 0x000187D0
		public static void Distance(ref Vector3 value1, ref Vector3 value2, out float result)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			float num4 = num * num + num2 * num2 + num3 * num3;
			result = (float)Math.Sqrt((double)num4);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0001A61C File Offset: 0x0001881C
		public static float DistanceSquared(Vector3 value1, Vector3 value2)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			return num * num + num2 * num2 + num3 * num3;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0001A65C File Offset: 0x0001885C
		public static void DistanceSquared(ref Vector3 value1, ref Vector3 value2, out float result)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = value1.Z - value2.Z;
			result = num * num + num2 * num2 + num3 * num3;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0001A6A0 File Offset: 0x000188A0
		public static float Dot(Vector3 vector1, Vector3 vector2)
		{
			return vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0001A6CB File Offset: 0x000188CB
		public static void Dot(ref Vector3 vector1, ref Vector3 vector2, out float result)
		{
			result = vector1.X * vector2.X + vector1.Y * vector2.Y + vector1.Z * vector2.Z;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0001A6F8 File Offset: 0x000188F8
		public void Normalize()
		{
			float num = this.X * this.X + this.Y * this.Y + this.Z * this.Z;
			float num2 = 1f / (float)Math.Sqrt((double)num);
			this.X *= num2;
			this.Y *= num2;
			this.Z *= num2;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0001A768 File Offset: 0x00018968
		public static Vector3 Normalize(Vector3 value)
		{
			float num = value.X * value.X + value.Y * value.Y + value.Z * value.Z;
			float num2 = 1f / (float)Math.Sqrt((double)num);
			Vector3 result;
			result.X = value.X * num2;
			result.Y = value.Y * num2;
			result.Z = value.Z * num2;
			return result;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0001A7DC File Offset: 0x000189DC
		public static void Normalize(ref Vector3 value, out Vector3 result)
		{
			float num = value.X * value.X + value.Y * value.Y + value.Z * value.Z;
			float num2 = 1f / (float)Math.Sqrt((double)num);
			result.X = value.X * num2;
			result.Y = value.Y * num2;
			result.Z = value.Z * num2;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0001A84C File Offset: 0x00018A4C
		public static Vector3 Cross(Vector3 vector1, Vector3 vector2)
		{
			Vector3 result;
			result.X = vector1.Y * vector2.Z - vector1.Z * vector2.Y;
			result.Y = vector1.Z * vector2.X - vector1.X * vector2.Z;
			result.Z = vector1.X * vector2.Y - vector1.Y * vector2.X;
			return result;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0001A8C0 File Offset: 0x00018AC0
		public static void Cross(ref Vector3 vector1, ref Vector3 vector2, out Vector3 result)
		{
			float x = vector1.Y * vector2.Z - vector1.Z * vector2.Y;
			float y = vector1.Z * vector2.X - vector1.X * vector2.Z;
			float z = vector1.X * vector2.Y - vector1.Y * vector2.X;
			result.X = x;
			result.Y = y;
			result.Z = z;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0001A938 File Offset: 0x00018B38
		public static Vector3 Reflect(Vector3 vector, Vector3 normal)
		{
			float num = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
			Vector3 result;
			result.X = vector.X - 2f * num * normal.X;
			result.Y = vector.Y - 2f * num * normal.Y;
			result.Z = vector.Z - 2f * num * normal.Z;
			return result;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0001A9C4 File Offset: 0x00018BC4
		public static void Reflect(ref Vector3 vector, ref Vector3 normal, out Vector3 result)
		{
			float num = vector.X * normal.X + vector.Y * normal.Y + vector.Z * normal.Z;
			result.X = vector.X - 2f * num * normal.X;
			result.Y = vector.Y - 2f * num * normal.Y;
			result.Z = vector.Z - 2f * num * normal.Z;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0001AA4C File Offset: 0x00018C4C
		public static Vector3 Min(Vector3 value1, Vector3 value2)
		{
			Vector3 result;
			result.X = ((value1.X < value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
			result.Z = ((value1.Z < value2.Z) ? value1.Z : value2.Z);
			return result;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0001AAC4 File Offset: 0x00018CC4
		public static void Min(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = ((value1.X < value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
			result.Z = ((value1.Z < value2.Z) ? value1.Z : value2.Z);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0001AB38 File Offset: 0x00018D38
		public static Vector3 Max(Vector3 value1, Vector3 value2)
		{
			Vector3 result;
			result.X = ((value1.X > value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
			result.Z = ((value1.Z > value2.Z) ? value1.Z : value2.Z);
			return result;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0001ABB0 File Offset: 0x00018DB0
		public static void Max(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = ((value1.X > value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
			result.Z = ((value1.Z > value2.Z) ? value1.Z : value2.Z);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0001AC24 File Offset: 0x00018E24
		public static Vector3 Clamp(Vector3 value1, Vector3 min, Vector3 max)
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
			Vector3 result;
			result.X = num;
			result.Y = num2;
			result.Z = num3;
			return result;
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0001ACD4 File Offset: 0x00018ED4
		public static void Clamp(ref Vector3 value1, ref Vector3 min, ref Vector3 max, out Vector3 result)
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
			result.X = num;
			result.Y = num2;
			result.Z = num3;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0001AD80 File Offset: 0x00018F80
		public static Vector3 Lerp(Vector3 value1, Vector3 value2, float amount)
		{
			Vector3 result;
			result.X = value1.X + (value2.X - value1.X) * amount;
			result.Y = value1.Y + (value2.Y - value1.Y) * amount;
			result.Z = value1.Z + (value2.Z - value1.Z) * amount;
			return result;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0001ADE8 File Offset: 0x00018FE8
		public static void Lerp(ref Vector3 value1, ref Vector3 value2, float amount, out Vector3 result)
		{
			result.X = value1.X + (value2.X - value1.X) * amount;
			result.Y = value1.Y + (value2.Y - value1.Y) * amount;
			result.Z = value1.Z + (value2.Z - value1.Z) * amount;
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0001AE4C File Offset: 0x0001904C
		public static Vector3 Barycentric(Vector3 value1, Vector3 value2, Vector3 value3, float amount1, float amount2)
		{
			Vector3 result;
			result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
			result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
			result.Z = value1.Z + amount1 * (value2.Z - value1.Z) + amount2 * (value3.Z - value1.Z);
			return result;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0001AEE4 File Offset: 0x000190E4
		public static void Barycentric(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, float amount1, float amount2, out Vector3 result)
		{
			result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
			result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
			result.Z = value1.Z + amount1 * (value2.Z - value1.Z) + amount2 * (value3.Z - value1.Z);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0001AF7C File Offset: 0x0001917C
		public static Vector3 SmoothStep(Vector3 value1, Vector3 value2, float amount)
		{
			amount = ((amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount));
			amount = amount * amount * (3f - 2f * amount);
			Vector3 result;
			result.X = value1.X + (value2.X - value1.X) * amount;
			result.Y = value1.Y + (value2.Y - value1.Y) * amount;
			result.Z = value1.Z + (value2.Z - value1.Z) * amount;
			return result;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0001B018 File Offset: 0x00019218
		public static void SmoothStep(ref Vector3 value1, ref Vector3 value2, float amount, out Vector3 result)
		{
			amount = ((amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount));
			amount = amount * amount * (3f - 2f * amount);
			result.X = value1.X + (value2.X - value1.X) * amount;
			result.Y = value1.Y + (value2.Y - value1.Y) * amount;
			result.Z = value1.Z + (value2.Z - value1.Z) * amount;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0001B0B0 File Offset: 0x000192B0
		public static Vector3 CatmullRom(Vector3 value1, Vector3 value2, Vector3 value3, Vector3 value4, float amount)
		{
			float num = amount * amount;
			float num2 = amount * num;
			Vector3 result;
			result.X = 0.5f * (2f * value2.X + (-value1.X + value3.X) * amount + (2f * value1.X - 5f * value2.X + 4f * value3.X - value4.X) * num + (-value1.X + 3f * value2.X - 3f * value3.X + value4.X) * num2);
			result.Y = 0.5f * (2f * value2.Y + (-value1.Y + value3.Y) * amount + (2f * value1.Y - 5f * value2.Y + 4f * value3.Y - value4.Y) * num + (-value1.Y + 3f * value2.Y - 3f * value3.Y + value4.Y) * num2);
			result.Z = 0.5f * (2f * value2.Z + (-value1.Z + value3.Z) * amount + (2f * value1.Z - 5f * value2.Z + 4f * value3.Z - value4.Z) * num + (-value1.Z + 3f * value2.Z - 3f * value3.Z + value4.Z) * num2);
			return result;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0001B25C File Offset: 0x0001945C
		public static void CatmullRom(ref Vector3 value1, ref Vector3 value2, ref Vector3 value3, ref Vector3 value4, float amount, out Vector3 result)
		{
			float num = amount * amount;
			float num2 = amount * num;
			result.X = 0.5f * (2f * value2.X + (-value1.X + value3.X) * amount + (2f * value1.X - 5f * value2.X + 4f * value3.X - value4.X) * num + (-value1.X + 3f * value2.X - 3f * value3.X + value4.X) * num2);
			result.Y = 0.5f * (2f * value2.Y + (-value1.Y + value3.Y) * amount + (2f * value1.Y - 5f * value2.Y + 4f * value3.Y - value4.Y) * num + (-value1.Y + 3f * value2.Y - 3f * value3.Y + value4.Y) * num2);
			result.Z = 0.5f * (2f * value2.Z + (-value1.Z + value3.Z) * amount + (2f * value1.Z - 5f * value2.Z + 4f * value3.Z - value4.Z) * num + (-value1.Z + 3f * value2.Z - 3f * value3.Z + value4.Z) * num2);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0001B408 File Offset: 0x00019608
		public static Vector3 Hermite(Vector3 value1, Vector3 tangent1, Vector3 value2, Vector3 tangent2, float amount)
		{
			float num = amount * amount;
			float num2 = amount * num;
			float num3 = 2f * num2 - 3f * num + 1f;
			float num4 = -2f * num2 + 3f * num;
			float num5 = num2 - 2f * num + amount;
			float num6 = num2 - num;
			Vector3 result;
			result.X = value1.X * num3 + value2.X * num4 + tangent1.X * num5 + tangent2.X * num6;
			result.Y = value1.Y * num3 + value2.Y * num4 + tangent1.Y * num5 + tangent2.Y * num6;
			result.Z = value1.Z * num3 + value2.Z * num4 + tangent1.Z * num5 + tangent2.Z * num6;
			return result;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0001B4E0 File Offset: 0x000196E0
		public static void Hermite(ref Vector3 value1, ref Vector3 tangent1, ref Vector3 value2, ref Vector3 tangent2, float amount, out Vector3 result)
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
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0001B5B8 File Offset: 0x000197B8
		public static Vector3 Transform(Vector3 position, Matrix matrix)
		{
			float x = position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41;
			float y = position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42;
			float z = position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43;
			Vector3 result;
			result.X = x;
			result.Y = y;
			result.Z = z;
			return result;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0001B674 File Offset: 0x00019874
		public static void Transform(ref Vector3 position, ref Matrix matrix, out Vector3 result)
		{
			float x = position.X * matrix.M11 + position.Y * matrix.M21 + position.Z * matrix.M31 + matrix.M41;
			float y = position.X * matrix.M12 + position.Y * matrix.M22 + position.Z * matrix.M32 + matrix.M42;
			float z = position.X * matrix.M13 + position.Y * matrix.M23 + position.Z * matrix.M33 + matrix.M43;
			result.X = x;
			result.Y = y;
			result.Z = z;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0001B72C File Offset: 0x0001992C
		public static Vector3 TransformNormal(Vector3 normal, Matrix matrix)
		{
			float x = normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31;
			float y = normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32;
			float z = normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33;
			Vector3 result;
			result.X = x;
			result.Y = y;
			result.Z = z;
			return result;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0001B7D0 File Offset: 0x000199D0
		public static void TransformNormal(ref Vector3 normal, ref Matrix matrix, out Vector3 result)
		{
			float x = normal.X * matrix.M11 + normal.Y * matrix.M21 + normal.Z * matrix.M31;
			float y = normal.X * matrix.M12 + normal.Y * matrix.M22 + normal.Z * matrix.M32;
			float z = normal.X * matrix.M13 + normal.Y * matrix.M23 + normal.Z * matrix.M33;
			result.X = x;
			result.Y = y;
			result.Z = z;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0001B870 File Offset: 0x00019A70
		public static void Transform(Vector3[] sourceArray, ref Matrix matrix, Vector3[] destinationArray)
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
				destinationArray[i].X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31 + matrix.M41;
				destinationArray[i].Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32 + matrix.M42;
				destinationArray[i].Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33 + matrix.M43;
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0001B970 File Offset: 0x00019B70
		public static void Transform(Vector3[] sourceArray, int sourceIndex, ref Matrix matrix, Vector3[] destinationArray, int destinationIndex, int length)
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
				destinationArray[destinationIndex].X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31 + matrix.M41;
				destinationArray[destinationIndex].Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32 + matrix.M42;
				destinationArray[destinationIndex].Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33 + matrix.M43;
				sourceIndex++;
				destinationIndex++;
				length--;
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0001BA98 File Offset: 0x00019C98
		public static void TransformNormal(Vector3[] sourceArray, ref Matrix matrix, Vector3[] destinationArray)
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
				destinationArray[i].X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31;
				destinationArray[i].Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32;
				destinationArray[i].Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0001BB84 File Offset: 0x00019D84
		public static void TransformNormal(Vector3[] sourceArray, int sourceIndex, ref Matrix matrix, Vector3[] destinationArray, int destinationIndex, int length)
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
				destinationArray[destinationIndex].X = x * matrix.M11 + y * matrix.M21 + z * matrix.M31;
				destinationArray[destinationIndex].Y = x * matrix.M12 + y * matrix.M22 + z * matrix.M32;
				destinationArray[destinationIndex].Z = x * matrix.M13 + y * matrix.M23 + z * matrix.M33;
				sourceIndex++;
				destinationIndex++;
				length--;
			}
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0001BC94 File Offset: 0x00019E94
		public static Vector3 Negate(Vector3 value)
		{
			Vector3 result;
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			return result;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0001BCCC File Offset: 0x00019ECC
		public static void Negate(ref Vector3 value, out Vector3 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0001BCF8 File Offset: 0x00019EF8
		public static Vector3 Add(Vector3 value1, Vector3 value2)
		{
			Vector3 result;
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			return result;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0001BD42 File Offset: 0x00019F42
		public static void Add(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0001BD80 File Offset: 0x00019F80
		public static Vector3 Subtract(Vector3 value1, Vector3 value2)
		{
			Vector3 result;
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			return result;
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0001BDCA File Offset: 0x00019FCA
		public static void Subtract(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0001BE08 File Offset: 0x0001A008
		public static Vector3 Multiply(Vector3 value1, Vector3 value2)
		{
			Vector3 result;
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
			return result;
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0001BE52 File Offset: 0x0001A052
		public static void Multiply(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0001BE90 File Offset: 0x0001A090
		public static Vector3 Multiply(Vector3 value1, float scaleFactor)
		{
			Vector3 result;
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
			return result;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0001BECB File Offset: 0x0001A0CB
		public static void Multiply(ref Vector3 value1, float scaleFactor, out Vector3 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			result.Z = value1.Z * scaleFactor;
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0001BEF8 File Offset: 0x0001A0F8
		public static Vector3 Divide(Vector3 value1, Vector3 value2)
		{
			Vector3 result;
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
			return result;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0001BF42 File Offset: 0x0001A142
		public static void Divide(ref Vector3 value1, ref Vector3 value2, out Vector3 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
		}

		// Token: 0x060001EB RID: 491 RVA: 0x0001BF80 File Offset: 0x0001A180
		public static Vector3 Divide(Vector3 value1, float value2)
		{
			float num = 1f / value2;
			Vector3 result;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			result.Z = value1.Z * num;
			return result;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x0001BFC4 File Offset: 0x0001A1C4
		public static void Divide(ref Vector3 value1, float value2, out Vector3 result)
		{
			float num = 1f / value2;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			result.Z = value1.Z * num;
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0001C004 File Offset: 0x0001A204
		public static Vector3 operator -(Vector3 value)
		{
			Vector3 result;
			result.X = -value.X;
			result.Y = -value.Y;
			result.Z = -value.Z;
			return result;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0001A4B1 File Offset: 0x000186B1
		public static bool operator ==(Vector3 value1, Vector3 value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y && value1.Z == value2.Z;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x0001C03C File Offset: 0x0001A23C
		public static bool operator !=(Vector3 value1, Vector3 value2)
		{
			return value1.X != value2.X || value1.Y != value2.Y || value1.Z != value2.Z;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x0001C070 File Offset: 0x0001A270
		public static Vector3 operator +(Vector3 value1, Vector3 value2)
		{
			Vector3 result;
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			result.Z = value1.Z + value2.Z;
			return result;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0001C0BC File Offset: 0x0001A2BC
		public static Vector3 operator -(Vector3 value1, Vector3 value2)
		{
			Vector3 result;
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			result.Z = value1.Z - value2.Z;
			return result;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0001C108 File Offset: 0x0001A308
		public static Vector3 operator *(Vector3 value1, Vector3 value2)
		{
			Vector3 result;
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			result.Z = value1.Z * value2.Z;
			return result;
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0001C154 File Offset: 0x0001A354
		public static Vector3 operator *(Vector3 value, float scaleFactor)
		{
			Vector3 result;
			result.X = value.X * scaleFactor;
			result.Y = value.Y * scaleFactor;
			result.Z = value.Z * scaleFactor;
			return result;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0001C190 File Offset: 0x0001A390
		public static Vector3 operator *(float scaleFactor, Vector3 value)
		{
			Vector3 result;
			result.X = value.X * scaleFactor;
			result.Y = value.Y * scaleFactor;
			result.Z = value.Z * scaleFactor;
			return result;
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0001C1CC File Offset: 0x0001A3CC
		public static Vector3 operator /(Vector3 value1, Vector3 value2)
		{
			Vector3 result;
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			result.Z = value1.Z / value2.Z;
			return result;
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0001C218 File Offset: 0x0001A418
		public static Vector3 operator /(Vector3 value, float divider)
		{
			float num = 1f / divider;
			Vector3 result;
			result.X = value.X * num;
			result.Y = value.Y * num;
			result.Z = value.Z * num;
			return result;
		}

		// Token: 0x04000165 RID: 357
		public float X;

		// Token: 0x04000166 RID: 358
		public float Y;

		// Token: 0x04000167 RID: 359
		public float Z;

		// Token: 0x04000168 RID: 360
		private static Vector3 _zero = default(Vector3);

		// Token: 0x04000169 RID: 361
		private static Vector3 _one = new Vector3(1f, 1f, 1f);

		// Token: 0x0400016A RID: 362
		private static Vector3 _unitX = new Vector3(1f, 0f, 0f);

		// Token: 0x0400016B RID: 363
		private static Vector3 _unitY = new Vector3(0f, 1f, 0f);

		// Token: 0x0400016C RID: 364
		private static Vector3 _unitZ = new Vector3(0f, 0f, 1f);

		// Token: 0x0400016D RID: 365
		private static Vector3 _up = new Vector3(0f, 1f, 0f);

		// Token: 0x0400016E RID: 366
		private static Vector3 _down = new Vector3(0f, -1f, 0f);

		// Token: 0x0400016F RID: 367
		private static Vector3 _right = new Vector3(1f, 0f, 0f);

		// Token: 0x04000170 RID: 368
		private static Vector3 _left = new Vector3(-1f, 0f, 0f);

		// Token: 0x04000171 RID: 369
		private static Vector3 _forward = new Vector3(0f, 0f, -1f);

		// Token: 0x04000172 RID: 370
		private static Vector3 _backward = new Vector3(0f, 0f, 1f);
	}
}
