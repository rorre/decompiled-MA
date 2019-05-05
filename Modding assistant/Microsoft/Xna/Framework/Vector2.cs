using System;
using System.Globalization;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	public struct Vector2 : IEquatable<Vector2>
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00018F9C File Offset: 0x0001719C
		public static Vector2 Zero
		{
			get
			{
				return Vector2._zero;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00018FA3 File Offset: 0x000171A3
		public static Vector2 One
		{
			get
			{
				return Vector2._one;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00018FAA File Offset: 0x000171AA
		public static Vector2 UnitX
		{
			get
			{
				return Vector2._unitX;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00018FB1 File Offset: 0x000171B1
		public static Vector2 UnitY
		{
			get
			{
				return Vector2._unitY;
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00018FB8 File Offset: 0x000171B8
		public Vector2(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00018FC8 File Offset: 0x000171C8
		public Vector2(float value)
		{
			this.Y = value;
			this.X = value;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00018FD8 File Offset: 0x000171D8
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{X:{0} Y:{1}}}", new object[]
			{
				this.X.ToString(currentCulture),
				this.Y.ToString(currentCulture)
			});
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0001901A File Offset: 0x0001721A
		public bool Equals(Vector2 other)
		{
			return this.X == other.X && this.Y == other.Y;
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0001903C File Offset: 0x0001723C
		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj is Vector2)
			{
				result = this.Equals((Vector2)obj);
			}
			return result;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00019061 File Offset: 0x00017261
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0001907A File Offset: 0x0001727A
		public float Length()
		{
			return (float)Math.Sqrt((double)(this.X * this.X + this.Y * this.Y));
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0001909E File Offset: 0x0001729E
		public float LengthSquared()
		{
			return this.X * this.X + this.Y * this.Y;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000190BC File Offset: 0x000172BC
		public static float Distance(Vector2 value1, Vector2 value2)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			return (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000190F4 File Offset: 0x000172F4
		public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			float num3 = num * num + num2 * num2;
			result = (float)Math.Sqrt((double)num3);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00019130 File Offset: 0x00017330
		public static float DistanceSquared(Vector2 value1, Vector2 value2)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			return num * num + num2 * num2;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00019160 File Offset: 0x00017360
		public static void DistanceSquared(ref Vector2 value1, ref Vector2 value2, out float result)
		{
			float num = value1.X - value2.X;
			float num2 = value1.Y - value2.Y;
			result = num * num + num2 * num2;
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00019192 File Offset: 0x00017392
		public static float Dot(Vector2 value1, Vector2 value2)
		{
			return value1.X * value2.X + value1.Y * value2.Y;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000191AF File Offset: 0x000173AF
		public static void Dot(ref Vector2 value1, ref Vector2 value2, out float result)
		{
			result = value1.X * value2.X + value1.Y * value2.Y;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x000191D0 File Offset: 0x000173D0
		public void Normalize()
		{
			float num = this.X * this.X + this.Y * this.Y;
			float num2 = 1f / (float)Math.Sqrt((double)num);
			this.X *= num2;
			this.Y *= num2;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00019224 File Offset: 0x00017424
		public static Vector2 Normalize(Vector2 value)
		{
			float num = value.X * value.X + value.Y * value.Y;
			float num2 = 1f / (float)Math.Sqrt((double)num);
			Vector2 result;
			result.X = value.X * num2;
			result.Y = value.Y * num2;
			return result;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0001927C File Offset: 0x0001747C
		public static void Normalize(ref Vector2 value, out Vector2 result)
		{
			float num = value.X * value.X + value.Y * value.Y;
			float num2 = 1f / (float)Math.Sqrt((double)num);
			result.X = value.X * num2;
			result.Y = value.Y * num2;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000192D0 File Offset: 0x000174D0
		public static Vector2 Min(Vector2 value1, Vector2 value2)
		{
			Vector2 result;
			result.X = ((value1.X < value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
			return result;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00019324 File Offset: 0x00017524
		public static void Min(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = ((value1.X < value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y < value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00019378 File Offset: 0x00017578
		public static Vector2 Max(Vector2 value1, Vector2 value2)
		{
			Vector2 result;
			result.X = ((value1.X > value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
			return result;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000193CC File Offset: 0x000175CC
		public static void Max(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = ((value1.X > value2.X) ? value1.X : value2.X);
			result.Y = ((value1.Y > value2.Y) ? value1.Y : value2.Y);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00019420 File Offset: 0x00017620
		public static Vector2 Clamp(Vector2 value1, Vector2 min, Vector2 max)
		{
			float num = value1.X;
			num = ((num > max.X) ? max.X : num);
			num = ((num < min.X) ? min.X : num);
			float num2 = value1.Y;
			num2 = ((num2 > max.Y) ? max.Y : num2);
			num2 = ((num2 < min.Y) ? min.Y : num2);
			Vector2 result;
			result.X = num;
			result.Y = num2;
			return result;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00019498 File Offset: 0x00017698
		public static void Clamp(ref Vector2 value1, ref Vector2 min, ref Vector2 max, out Vector2 result)
		{
			float num = value1.X;
			num = ((num > max.X) ? max.X : num);
			num = ((num < min.X) ? min.X : num);
			float num2 = value1.Y;
			num2 = ((num2 > max.Y) ? max.Y : num2);
			num2 = ((num2 < min.Y) ? min.Y : num2);
			result.X = num;
			result.Y = num2;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00019510 File Offset: 0x00017710
		public static Vector2 Lerp(Vector2 value1, Vector2 value2, float amount)
		{
			Vector2 result;
			result.X = value1.X + (value2.X - value1.X) * amount;
			result.Y = value1.Y + (value2.Y - value1.Y) * amount;
			return result;
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00019558 File Offset: 0x00017758
		public static void Lerp(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
		{
			result.X = value1.X + (value2.X - value1.X) * amount;
			result.Y = value1.Y + (value2.Y - value1.Y) * amount;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00019594 File Offset: 0x00017794
		public static Vector2 Barycentric(Vector2 value1, Vector2 value2, Vector2 value3, float amount1, float amount2)
		{
			Vector2 result;
			result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
			result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
			return result;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00019600 File Offset: 0x00017800
		public static void Barycentric(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, float amount1, float amount2, out Vector2 result)
		{
			result.X = value1.X + amount1 * (value2.X - value1.X) + amount2 * (value3.X - value1.X);
			result.Y = value1.Y + amount1 * (value2.Y - value1.Y) + amount2 * (value3.Y - value1.Y);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0001966C File Offset: 0x0001786C
		public static Vector2 SmoothStep(Vector2 value1, Vector2 value2, float amount)
		{
			amount = ((amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount));
			amount = amount * amount * (3f - 2f * amount);
			Vector2 result;
			result.X = value1.X + (value2.X - value1.X) * amount;
			result.Y = value1.Y + (value2.Y - value1.Y) * amount;
			return result;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x000196E8 File Offset: 0x000178E8
		public static void SmoothStep(ref Vector2 value1, ref Vector2 value2, float amount, out Vector2 result)
		{
			amount = ((amount > 1f) ? 1f : ((amount < 0f) ? 0f : amount));
			amount = amount * amount * (3f - 2f * amount);
			result.X = value1.X + (value2.X - value1.X) * amount;
			result.Y = value1.Y + (value2.Y - value1.Y) * amount;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00019764 File Offset: 0x00017964
		public static Vector2 CatmullRom(Vector2 value1, Vector2 value2, Vector2 value3, Vector2 value4, float amount)
		{
			float num = amount * amount;
			float num2 = amount * num;
			Vector2 result;
			result.X = 0.5f * (2f * value2.X + (-value1.X + value3.X) * amount + (2f * value1.X - 5f * value2.X + 4f * value3.X - value4.X) * num + (-value1.X + 3f * value2.X - 3f * value3.X + value4.X) * num2);
			result.Y = 0.5f * (2f * value2.Y + (-value1.Y + value3.Y) * amount + (2f * value1.Y - 5f * value2.Y + 4f * value3.Y - value4.Y) * num + (-value1.Y + 3f * value2.Y - 3f * value3.Y + value4.Y) * num2);
			return result;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0001988C File Offset: 0x00017A8C
		public static void CatmullRom(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, float amount, out Vector2 result)
		{
			float num = amount * amount;
			float num2 = amount * num;
			result.X = 0.5f * (2f * value2.X + (-value1.X + value3.X) * amount + (2f * value1.X - 5f * value2.X + 4f * value3.X - value4.X) * num + (-value1.X + 3f * value2.X - 3f * value3.X + value4.X) * num2);
			result.Y = 0.5f * (2f * value2.Y + (-value1.Y + value3.Y) * amount + (2f * value1.Y - 5f * value2.Y + 4f * value3.Y - value4.Y) * num + (-value1.Y + 3f * value2.Y - 3f * value3.Y + value4.Y) * num2);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x000199B0 File Offset: 0x00017BB0
		public static Vector2 Hermite(Vector2 value1, Vector2 tangent1, Vector2 value2, Vector2 tangent2, float amount)
		{
			float num = amount * amount;
			float num2 = amount * num;
			float num3 = 2f * num2 - 3f * num + 1f;
			float num4 = -2f * num2 + 3f * num;
			float num5 = num2 - 2f * num + amount;
			float num6 = num2 - num;
			Vector2 result;
			result.X = value1.X * num3 + value2.X * num4 + tangent1.X * num5 + tangent2.X * num6;
			result.Y = value1.Y * num3 + value2.Y * num4 + tangent1.Y * num5 + tangent2.Y * num6;
			return result;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00019A5C File Offset: 0x00017C5C
		public static void Hermite(ref Vector2 value1, ref Vector2 tangent1, ref Vector2 value2, ref Vector2 tangent2, float amount, out Vector2 result)
		{
			float num = amount * amount;
			float num2 = amount * num;
			float num3 = 2f * num2 - 3f * num + 1f;
			float num4 = -2f * num2 + 3f * num;
			float num5 = num2 - 2f * num + amount;
			float num6 = num2 - num;
			result.X = value1.X * num3 + value2.X * num4 + tangent1.X * num5 + tangent2.X * num6;
			result.Y = value1.Y * num3 + value2.Y * num4 + tangent1.Y * num5 + tangent2.Y * num6;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00019B08 File Offset: 0x00017D08
		public static Vector2 Transform(Vector2 position, Matrix matrix)
		{
			float x = position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41;
			float y = position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42;
			Vector2 result;
			result.X = x;
			result.Y = y;
			return result;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00019B6C File Offset: 0x00017D6C
		public static void Transform(ref Vector2 position, ref Matrix matrix, out Vector2 result)
		{
			float x = position.X * matrix.M11 + position.Y * matrix.M21 + matrix.M41;
			float y = position.X * matrix.M12 + position.Y * matrix.M22 + matrix.M42;
			result.X = x;
			result.Y = y;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00019BD0 File Offset: 0x00017DD0
		public static Vector2 TransformNormal(Vector2 normal, Matrix matrix)
		{
			float x = normal.X * matrix.M11 + normal.Y * matrix.M21;
			float y = normal.X * matrix.M12 + normal.Y * matrix.M22;
			Vector2 result;
			result.X = x;
			result.Y = y;
			return result;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00019C28 File Offset: 0x00017E28
		public static void TransformNormal(ref Vector2 normal, ref Matrix matrix, out Vector2 result)
		{
			float x = normal.X * matrix.M11 + normal.Y * matrix.M21;
			float y = normal.X * matrix.M12 + normal.Y * matrix.M22;
			result.X = x;
			result.Y = y;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00019C7C File Offset: 0x00017E7C
		public static void Transform(Vector2[] sourceArray, ref Matrix matrix, Vector2[] destinationArray)
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
				destinationArray[i].X = x * matrix.M11 + y * matrix.M21 + matrix.M41;
				destinationArray[i].Y = x * matrix.M12 + y * matrix.M22 + matrix.M42;
			}
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00019D28 File Offset: 0x00017F28
		public static void Transform(Vector2[] sourceArray, int sourceIndex, ref Matrix matrix, Vector2[] destinationArray, int destinationIndex, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("values");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("results");
			}
			if ((long)destinationArray.Length < (long)destinationIndex + (long)length)
			{
				throw new IndexOutOfRangeException(MathResources.NotEnoughTargetSize);
			}
			while (length > 0)
			{
				float x = sourceArray[sourceIndex].X;
				float y = sourceArray[sourceIndex].Y;
				destinationArray[destinationIndex].X = x * matrix.M11 + y * matrix.M21 + matrix.M41;
				destinationArray[destinationIndex].Y = x * matrix.M12 + y * matrix.M22 + matrix.M42;
				sourceIndex++;
				destinationIndex++;
				length--;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00019DE8 File Offset: 0x00017FE8
		public static void TransformNormal(Vector2[] sourceArray, ref Matrix matrix, Vector2[] destinationArray)
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
				destinationArray[i].X = x * matrix.M11 + y * matrix.M21;
				destinationArray[i].Y = x * matrix.M12 + y * matrix.M22;
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00019E88 File Offset: 0x00018088
		public static void TransformNormal(Vector2[] sourceArray, int sourceIndex, ref Matrix matrix, Vector2[] destinationArray, int destinationIndex, int length)
		{
			if (sourceArray == null)
			{
				throw new ArgumentNullException("values");
			}
			if (destinationArray == null)
			{
				throw new ArgumentNullException("results");
			}
			if ((long)destinationArray.Length < (long)destinationIndex + (long)length)
			{
				throw new IndexOutOfRangeException(MathResources.NotEnoughTargetSize);
			}
			while (length > 0)
			{
				float x = sourceArray[sourceIndex].X;
				float y = sourceArray[sourceIndex].Y;
				destinationArray[destinationIndex].X = x * matrix.M11 + y * matrix.M21;
				destinationArray[destinationIndex].Y = x * matrix.M12 + y * matrix.M22;
				sourceIndex++;
				destinationIndex++;
				length--;
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00019F38 File Offset: 0x00018138
		public static Vector2 Negate(Vector2 value)
		{
			Vector2 result;
			result.X = -value.X;
			result.Y = -value.Y;
			return result;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00019F62 File Offset: 0x00018162
		public static void Negate(ref Vector2 value, out Vector2 result)
		{
			result.X = -value.X;
			result.Y = -value.Y;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00019F80 File Offset: 0x00018180
		public static Vector2 Add(Vector2 value1, Vector2 value2)
		{
			Vector2 result;
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			return result;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00019FB6 File Offset: 0x000181B6
		public static void Add(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00019FE0 File Offset: 0x000181E0
		public static Vector2 Subtract(Vector2 value1, Vector2 value2)
		{
			Vector2 result;
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			return result;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0001A016 File Offset: 0x00018216
		public static void Subtract(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0001A040 File Offset: 0x00018240
		public static Vector2 Multiply(Vector2 value1, Vector2 value2)
		{
			Vector2 result;
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			return result;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0001A076 File Offset: 0x00018276
		public static void Multiply(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0001A0A0 File Offset: 0x000182A0
		public static Vector2 Multiply(Vector2 value1, float scaleFactor)
		{
			Vector2 result;
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
			return result;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0001A0CC File Offset: 0x000182CC
		public static void Multiply(ref Vector2 value1, float scaleFactor, out Vector2 result)
		{
			result.X = value1.X * scaleFactor;
			result.Y = value1.Y * scaleFactor;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0001A0EC File Offset: 0x000182EC
		public static Vector2 Divide(Vector2 value1, Vector2 value2)
		{
			Vector2 result;
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			return result;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0001A122 File Offset: 0x00018322
		public static void Divide(ref Vector2 value1, ref Vector2 value2, out Vector2 result)
		{
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0001A14C File Offset: 0x0001834C
		public static Vector2 Divide(Vector2 value1, float divider)
		{
			float num = 1f / divider;
			Vector2 result;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			return result;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0001A180 File Offset: 0x00018380
		public static void Divide(ref Vector2 value1, float divider, out Vector2 result)
		{
			float num = 1f / divider;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0001A1B4 File Offset: 0x000183B4
		public static Vector2 operator -(Vector2 value)
		{
			Vector2 result;
			result.X = -value.X;
			result.Y = -value.Y;
			return result;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0001901A File Offset: 0x0001721A
		public static bool operator ==(Vector2 value1, Vector2 value2)
		{
			return value1.X == value2.X && value1.Y == value2.Y;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0001A1DE File Offset: 0x000183DE
		public static bool operator !=(Vector2 value1, Vector2 value2)
		{
			return value1.X != value2.X || value1.Y != value2.Y;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0001A204 File Offset: 0x00018404
		public static Vector2 operator +(Vector2 value1, Vector2 value2)
		{
			Vector2 result;
			result.X = value1.X + value2.X;
			result.Y = value1.Y + value2.Y;
			return result;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0001A23C File Offset: 0x0001843C
		public static Vector2 operator -(Vector2 value1, Vector2 value2)
		{
			Vector2 result;
			result.X = value1.X - value2.X;
			result.Y = value1.Y - value2.Y;
			return result;
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0001A274 File Offset: 0x00018474
		public static Vector2 operator *(Vector2 value1, Vector2 value2)
		{
			Vector2 result;
			result.X = value1.X * value2.X;
			result.Y = value1.Y * value2.Y;
			return result;
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0001A2AC File Offset: 0x000184AC
		public static Vector2 operator *(Vector2 value, float scaleFactor)
		{
			Vector2 result;
			result.X = value.X * scaleFactor;
			result.Y = value.Y * scaleFactor;
			return result;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0001A2D8 File Offset: 0x000184D8
		public static Vector2 operator *(float scaleFactor, Vector2 value)
		{
			Vector2 result;
			result.X = value.X * scaleFactor;
			result.Y = value.Y * scaleFactor;
			return result;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0001A304 File Offset: 0x00018504
		public static Vector2 operator /(Vector2 value1, Vector2 value2)
		{
			Vector2 result;
			result.X = value1.X / value2.X;
			result.Y = value1.Y / value2.Y;
			return result;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0001A33C File Offset: 0x0001853C
		public static Vector2 operator /(Vector2 value1, float divider)
		{
			float num = 1f / divider;
			Vector2 result;
			result.X = value1.X * num;
			result.Y = value1.Y * num;
			return result;
		}

		// Token: 0x0400015F RID: 351
		public float X;

		// Token: 0x04000160 RID: 352
		public float Y;

		// Token: 0x04000161 RID: 353
		private static Vector2 _zero = default(Vector2);

		// Token: 0x04000162 RID: 354
		private static Vector2 _one = new Vector2(1f, 1f);

		// Token: 0x04000163 RID: 355
		private static Vector2 _unitX = new Vector2(1f, 0f);

		// Token: 0x04000164 RID: 356
		private static Vector2 _unitY = new Vector2(0f, 1f);
	}
}
