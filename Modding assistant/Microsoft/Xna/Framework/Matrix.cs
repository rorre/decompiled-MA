using System;
using System.Globalization;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public struct Matrix : IEquatable<Matrix>
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00011C85 File Offset: 0x0000FE85
		public static Matrix Identity
		{
			get
			{
				return Matrix._identity;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00011C8C File Offset: 0x0000FE8C
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00011CC1 File Offset: 0x0000FEC1
		public Vector3 Up
		{
			get
			{
				Vector3 result;
				result.X = this.M21;
				result.Y = this.M22;
				result.Z = this.M23;
				return result;
			}
			set
			{
				this.M21 = value.X;
				this.M22 = value.Y;
				this.M23 = value.Z;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00011CE8 File Offset: 0x0000FEE8
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00011D20 File Offset: 0x0000FF20
		public Vector3 Down
		{
			get
			{
				Vector3 result;
				result.X = -this.M21;
				result.Y = -this.M22;
				result.Z = -this.M23;
				return result;
			}
			set
			{
				this.M21 = -value.X;
				this.M22 = -value.Y;
				this.M23 = -value.Z;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00011D4C File Offset: 0x0000FF4C
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00011D81 File Offset: 0x0000FF81
		public Vector3 Right
		{
			get
			{
				Vector3 result;
				result.X = this.M11;
				result.Y = this.M12;
				result.Z = this.M13;
				return result;
			}
			set
			{
				this.M11 = value.X;
				this.M12 = value.Y;
				this.M13 = value.Z;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00011DA8 File Offset: 0x0000FFA8
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00011DE0 File Offset: 0x0000FFE0
		public Vector3 Left
		{
			get
			{
				Vector3 result;
				result.X = -this.M11;
				result.Y = -this.M12;
				result.Z = -this.M13;
				return result;
			}
			set
			{
				this.M11 = -value.X;
				this.M12 = -value.Y;
				this.M13 = -value.Z;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00011E0C File Offset: 0x0001000C
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00011E44 File Offset: 0x00010044
		public Vector3 Forward
		{
			get
			{
				Vector3 result;
				result.X = -this.M31;
				result.Y = -this.M32;
				result.Z = -this.M33;
				return result;
			}
			set
			{
				this.M31 = -value.X;
				this.M32 = -value.Y;
				this.M33 = -value.Z;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00011E70 File Offset: 0x00010070
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00011EA5 File Offset: 0x000100A5
		public Vector3 Backward
		{
			get
			{
				Vector3 result;
				result.X = this.M31;
				result.Y = this.M32;
				result.Z = this.M33;
				return result;
			}
			set
			{
				this.M31 = value.X;
				this.M32 = value.Y;
				this.M33 = value.Z;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00011ECC File Offset: 0x000100CC
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00011F01 File Offset: 0x00010101
		public Vector3 Translation
		{
			get
			{
				Vector3 result;
				result.X = this.M41;
				result.Y = this.M42;
				result.Z = this.M43;
				return result;
			}
			set
			{
				this.M41 = value.X;
				this.M42 = value.Y;
				this.M43 = value.Z;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00011F28 File Offset: 0x00010128
		public Matrix(float m11, float m12, float m13, float m14, float m21, float m22, float m23, float m24, float m31, float m32, float m33, float m34, float m41, float m42, float m43, float m44)
		{
			this.M11 = m11;
			this.M12 = m12;
			this.M13 = m13;
			this.M14 = m14;
			this.M21 = m21;
			this.M22 = m22;
			this.M23 = m23;
			this.M24 = m24;
			this.M31 = m31;
			this.M32 = m32;
			this.M33 = m33;
			this.M34 = m34;
			this.M41 = m41;
			this.M42 = m42;
			this.M43 = m43;
			this.M44 = m44;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00011FB4 File Offset: 0x000101B4
		public static Matrix CreateBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 cameraUpVector, Vector3? cameraForwardVector)
		{
			Vector3 vector;
			vector.X = objectPosition.X - cameraPosition.X;
			vector.Y = objectPosition.Y - cameraPosition.Y;
			vector.Z = objectPosition.Z - cameraPosition.Z;
			float num = vector.LengthSquared();
			if (num < 0.0001f)
			{
				vector = ((cameraForwardVector != null) ? (-cameraForwardVector.Value) : Vector3.Forward);
			}
			else
			{
				Vector3.Multiply(ref vector, 1f / (float)Math.Sqrt((double)num), out vector);
			}
			Vector3 vector2;
			Vector3.Cross(ref cameraUpVector, ref vector, out vector2);
			vector2.Normalize();
			Vector3 vector3;
			Vector3.Cross(ref vector, ref vector2, out vector3);
			Matrix result;
			result.M11 = vector2.X;
			result.M12 = vector2.Y;
			result.M13 = vector2.Z;
			result.M14 = 0f;
			result.M21 = vector3.X;
			result.M22 = vector3.Y;
			result.M23 = vector3.Z;
			result.M24 = 0f;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = 0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00012130 File Offset: 0x00010330
		public static void CreateBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 cameraUpVector, Vector3? cameraForwardVector, out Matrix result)
		{
			Vector3 vector;
			vector.X = objectPosition.X - cameraPosition.X;
			vector.Y = objectPosition.Y - cameraPosition.Y;
			vector.Z = objectPosition.Z - cameraPosition.Z;
			float num = vector.LengthSquared();
			if (num < 0.0001f)
			{
				vector = ((cameraForwardVector != null) ? (-cameraForwardVector.Value) : Vector3.Forward);
			}
			else
			{
				Vector3.Multiply(ref vector, 1f / (float)Math.Sqrt((double)num), out vector);
			}
			Vector3 vector2;
			Vector3.Cross(ref cameraUpVector, ref vector, out vector2);
			vector2.Normalize();
			Vector3 vector3;
			Vector3.Cross(ref vector, ref vector2, out vector3);
			result.M11 = vector2.X;
			result.M12 = vector2.Y;
			result.M13 = vector2.Z;
			result.M14 = 0f;
			result.M21 = vector3.X;
			result.M22 = vector3.Y;
			result.M23 = vector3.Z;
			result.M24 = 0f;
			result.M31 = vector.X;
			result.M32 = vector.Y;
			result.M33 = vector.Z;
			result.M34 = 0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1f;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000122A8 File Offset: 0x000104A8
		public static Matrix CreateConstrainedBillboard(Vector3 objectPosition, Vector3 cameraPosition, Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector)
		{
			Vector3 vector;
			vector.X = objectPosition.X - cameraPosition.X;
			vector.Y = objectPosition.Y - cameraPosition.Y;
			vector.Z = objectPosition.Z - cameraPosition.Z;
			float num = vector.LengthSquared();
			if (num < 0.0001f)
			{
				vector = ((cameraForwardVector != null) ? (-cameraForwardVector.Value) : Vector3.Forward);
			}
			else
			{
				Vector3.Multiply(ref vector, 1f / (float)Math.Sqrt((double)num), out vector);
			}
			Vector3 vector2 = rotateAxis;
			float value;
			Vector3.Dot(ref rotateAxis, ref vector, out value);
			Vector3 vector3;
			Vector3 vector4;
			if (Math.Abs(value) > 0.998254657f)
			{
				if (objectForwardVector != null)
				{
					vector3 = objectForwardVector.Value;
					Vector3.Dot(ref rotateAxis, ref vector3, out value);
					if (Math.Abs(value) > 0.998254657f)
					{
						value = rotateAxis.X * Vector3.Forward.X + rotateAxis.Y * Vector3.Forward.Y + rotateAxis.Z * Vector3.Forward.Z;
						vector3 = ((Math.Abs(value) > 0.998254657f) ? Vector3.Right : Vector3.Forward);
					}
				}
				else
				{
					value = rotateAxis.X * Vector3.Forward.X + rotateAxis.Y * Vector3.Forward.Y + rotateAxis.Z * Vector3.Forward.Z;
					vector3 = ((Math.Abs(value) > 0.998254657f) ? Vector3.Right : Vector3.Forward);
				}
				Vector3.Cross(ref rotateAxis, ref vector3, out vector4);
				vector4.Normalize();
				Vector3.Cross(ref vector4, ref rotateAxis, out vector3);
				vector3.Normalize();
			}
			else
			{
				Vector3.Cross(ref rotateAxis, ref vector, out vector4);
				vector4.Normalize();
				Vector3.Cross(ref vector4, ref vector2, out vector3);
				vector3.Normalize();
			}
			Matrix result;
			result.M11 = vector4.X;
			result.M12 = vector4.Y;
			result.M13 = vector4.Z;
			result.M14 = 0f;
			result.M21 = vector2.X;
			result.M22 = vector2.Y;
			result.M23 = vector2.Z;
			result.M24 = 0f;
			result.M31 = vector3.X;
			result.M32 = vector3.Y;
			result.M33 = vector3.Z;
			result.M34 = 0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00012544 File Offset: 0x00010744
		public static void CreateConstrainedBillboard(ref Vector3 objectPosition, ref Vector3 cameraPosition, ref Vector3 rotateAxis, Vector3? cameraForwardVector, Vector3? objectForwardVector, out Matrix result)
		{
			Vector3 vector;
			vector.X = objectPosition.X - cameraPosition.X;
			vector.Y = objectPosition.Y - cameraPosition.Y;
			vector.Z = objectPosition.Z - cameraPosition.Z;
			float num = vector.LengthSquared();
			if (num < 0.0001f)
			{
				vector = ((cameraForwardVector != null) ? (-cameraForwardVector.Value) : Vector3.Forward);
			}
			else
			{
				Vector3.Multiply(ref vector, 1f / (float)Math.Sqrt((double)num), out vector);
			}
			Vector3 vector2 = rotateAxis;
			float value;
			Vector3.Dot(ref rotateAxis, ref vector, out value);
			Vector3 vector3;
			Vector3 vector4;
			if (Math.Abs(value) > 0.998254657f)
			{
				if (objectForwardVector != null)
				{
					vector3 = objectForwardVector.Value;
					Vector3.Dot(ref rotateAxis, ref vector3, out value);
					if (Math.Abs(value) > 0.998254657f)
					{
						value = rotateAxis.X * Vector3.Forward.X + rotateAxis.Y * Vector3.Forward.Y + rotateAxis.Z * Vector3.Forward.Z;
						vector3 = ((Math.Abs(value) > 0.998254657f) ? Vector3.Right : Vector3.Forward);
					}
				}
				else
				{
					value = rotateAxis.X * Vector3.Forward.X + rotateAxis.Y * Vector3.Forward.Y + rotateAxis.Z * Vector3.Forward.Z;
					vector3 = ((Math.Abs(value) > 0.998254657f) ? Vector3.Right : Vector3.Forward);
				}
				Vector3.Cross(ref rotateAxis, ref vector3, out vector4);
				vector4.Normalize();
				Vector3.Cross(ref vector4, ref rotateAxis, out vector3);
				vector3.Normalize();
			}
			else
			{
				Vector3.Cross(ref rotateAxis, ref vector, out vector4);
				vector4.Normalize();
				Vector3.Cross(ref vector4, ref vector2, out vector3);
				vector3.Normalize();
			}
			result.M11 = vector4.X;
			result.M12 = vector4.Y;
			result.M13 = vector4.Z;
			result.M14 = 0f;
			result.M21 = vector2.X;
			result.M22 = vector2.Y;
			result.M23 = vector2.Z;
			result.M24 = 0f;
			result.M31 = vector3.X;
			result.M32 = vector3.Y;
			result.M33 = vector3.Z;
			result.M34 = 0f;
			result.M41 = objectPosition.X;
			result.M42 = objectPosition.Y;
			result.M43 = objectPosition.Z;
			result.M44 = 1f;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000127E0 File Offset: 0x000109E0
		public static Matrix CreateTranslation(Vector3 position)
		{
			Matrix result;
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000128B4 File Offset: 0x00010AB4
		public static void CreateTranslation(ref Vector3 position, out Matrix result)
		{
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = position.X;
			result.M42 = position.Y;
			result.M43 = position.Z;
			result.M44 = 1f;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00012974 File Offset: 0x00010B74
		public static Matrix CreateTranslation(float xPosition, float yPosition, float zPosition)
		{
			Matrix result;
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = xPosition;
			result.M42 = yPosition;
			result.M43 = zPosition;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00012A38 File Offset: 0x00010C38
		public static void CreateTranslation(float xPosition, float yPosition, float zPosition, out Matrix result)
		{
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = xPosition;
			result.M42 = yPosition;
			result.M43 = zPosition;
			result.M44 = 1f;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00012AEC File Offset: 0x00010CEC
		public static Matrix CreateScale(float xScale, float yScale, float zScale)
		{
			Matrix result;
			result.M11 = xScale;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = yScale;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = zScale;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00012BB0 File Offset: 0x00010DB0
		public static void CreateScale(float xScale, float yScale, float zScale, out Matrix result)
		{
			result.M11 = xScale;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = yScale;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = zScale;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00012C64 File Offset: 0x00010E64
		public static Matrix CreateScale(Vector3 scales)
		{
			float x = scales.X;
			float y = scales.Y;
			float z = scales.Z;
			Matrix result;
			result.M11 = x;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = y;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = z;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00012D3C File Offset: 0x00010F3C
		public static void CreateScale(ref Vector3 scales, out Matrix result)
		{
			float x = scales.X;
			float y = scales.Y;
			float z = scales.Z;
			result.M11 = x;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = y;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = z;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00012E04 File Offset: 0x00011004
		public static Matrix CreateScale(float scale)
		{
			Matrix result;
			result.M11 = scale;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = scale;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = scale;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00012EC8 File Offset: 0x000110C8
		public static void CreateScale(float scale, out Matrix result)
		{
			result.M11 = scale;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = scale;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = scale;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00012F7C File Offset: 0x0001117C
		public static Matrix CreateRotationX(float radians)
		{
			float num = (float)Math.Cos((double)radians);
			float num2 = (float)Math.Sin((double)radians);
			Matrix result;
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = num;
			result.M23 = num2;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = -num2;
			result.M33 = num;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00013050 File Offset: 0x00011250
		public static void CreateRotationX(float radians, out Matrix result)
		{
			float num = (float)Math.Cos((double)radians);
			float num2 = (float)Math.Sin((double)radians);
			result.M11 = 1f;
			result.M12 = 0f;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = num;
			result.M23 = num2;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = -num2;
			result.M33 = num;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00013110 File Offset: 0x00011310
		public static Matrix CreateRotationY(float radians)
		{
			float num = (float)Math.Cos((double)radians);
			float num2 = (float)Math.Sin((double)radians);
			Matrix result;
			result.M11 = num;
			result.M12 = 0f;
			result.M13 = -num2;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = num2;
			result.M32 = 0f;
			result.M33 = num;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x000131E4 File Offset: 0x000113E4
		public static void CreateRotationY(float radians, out Matrix result)
		{
			float num = (float)Math.Cos((double)radians);
			float num2 = (float)Math.Sin((double)radians);
			result.M11 = num;
			result.M12 = 0f;
			result.M13 = -num2;
			result.M14 = 0f;
			result.M21 = 0f;
			result.M22 = 1f;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = num2;
			result.M32 = 0f;
			result.M33 = num;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x000132A4 File Offset: 0x000114A4
		public static Matrix CreateRotationZ(float radians)
		{
			float num = (float)Math.Cos((double)radians);
			float num2 = (float)Math.Sin((double)radians);
			Matrix result;
			result.M11 = num;
			result.M12 = num2;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = -num2;
			result.M22 = num;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00013378 File Offset: 0x00011578
		public static void CreateRotationZ(float radians, out Matrix result)
		{
			float num = (float)Math.Cos((double)radians);
			float num2 = (float)Math.Sin((double)radians);
			result.M11 = num;
			result.M12 = num2;
			result.M13 = 0f;
			result.M14 = 0f;
			result.M21 = -num2;
			result.M22 = num;
			result.M23 = 0f;
			result.M24 = 0f;
			result.M31 = 0f;
			result.M32 = 0f;
			result.M33 = 1f;
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00013438 File Offset: 0x00011638
		public static Matrix CreateFromAxisAngle(Vector3 axis, float angle)
		{
			float x = axis.X;
			float y = axis.Y;
			float z = axis.Z;
			float num = (float)Math.Sin((double)angle);
			float num2 = (float)Math.Cos((double)angle);
			float num3 = x * x;
			float num4 = y * y;
			float num5 = z * z;
			float num6 = x * y;
			float num7 = x * z;
			float num8 = y * z;
			Matrix result;
			result.M11 = num3 + num2 * (1f - num3);
			result.M12 = num6 - num2 * num6 + num * z;
			result.M13 = num7 - num2 * num7 - num * y;
			result.M14 = 0f;
			result.M21 = num6 - num2 * num6 - num * z;
			result.M22 = num4 + num2 * (1f - num4);
			result.M23 = num8 - num2 * num8 + num * x;
			result.M24 = 0f;
			result.M31 = num7 - num2 * num7 + num * y;
			result.M32 = num8 - num2 * num8 - num * x;
			result.M33 = num5 + num2 * (1f - num5);
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00013594 File Offset: 0x00011794
		public static void CreateFromAxisAngle(ref Vector3 axis, float angle, out Matrix result)
		{
			float x = axis.X;
			float y = axis.Y;
			float z = axis.Z;
			float num = (float)Math.Sin((double)angle);
			float num2 = (float)Math.Cos((double)angle);
			float num3 = x * x;
			float num4 = y * y;
			float num5 = z * z;
			float num6 = x * y;
			float num7 = x * z;
			float num8 = y * z;
			result.M11 = num3 + num2 * (1f - num3);
			result.M12 = num6 - num2 * num6 + num * z;
			result.M13 = num7 - num2 * num7 - num * y;
			result.M14 = 0f;
			result.M21 = num6 - num2 * num6 - num * z;
			result.M22 = num4 + num2 * (1f - num4);
			result.M23 = num8 - num2 * num8 + num * x;
			result.M24 = 0f;
			result.M31 = num7 - num2 * num7 + num * y;
			result.M32 = num8 - num2 * num8 - num * x;
			result.M33 = num5 + num2 * (1f - num5);
			result.M34 = 0f;
			result.M41 = 0f;
			result.M42 = 0f;
			result.M43 = 0f;
			result.M44 = 1f;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000136DC File Offset: 0x000118DC
		public static Matrix CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance)
		{
			if (fieldOfView <= 0f || fieldOfView >= 3.14159274f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.OutRangeFieldOfView, new object[]
				{
					"fieldOfView"
				}));
			}
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.NegativePlaneDistance, new object[]
				{
					"nearPlaneDistance"
				}));
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.NegativePlaneDistance, new object[]
				{
					"farPlaneDistance"
				}));
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException(MathResources.OppositePlanes);
			}
			float num = 1f / (float)Math.Tan((double)(fieldOfView * 0.5f));
			float m = num / aspectRatio;
			Matrix result;
			result.M11 = m;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = num;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M31 = (result.M32 = 0f);
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -1f;
			result.M41 = (result.M42 = (result.M44 = 0f));
			result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			return result;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00013848 File Offset: 0x00011A48
		public static void CreatePerspectiveFieldOfView(float fieldOfView, float aspectRatio, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
		{
			if (fieldOfView <= 0f || fieldOfView >= 3.14159274f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.OutRangeFieldOfView, new object[]
				{
					"fieldOfView"
				}));
			}
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.NegativePlaneDistance, new object[]
				{
					"nearPlaneDistance"
				}));
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.NegativePlaneDistance, new object[]
				{
					"farPlaneDistance"
				}));
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException(MathResources.OppositePlanes);
			}
			float num = 1f / (float)Math.Tan((double)(fieldOfView * 0.5f));
			float m = num / aspectRatio;
			result.M11 = m;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = num;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M31 = (result.M32 = 0f);
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -1f;
			result.M41 = (result.M42 = (result.M44 = 0f));
			result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000139B4 File Offset: 0x00011BB4
		public static Matrix CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance)
		{
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.NegativePlaneDistance, new object[]
				{
					"nearPlaneDistance"
				}));
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.NegativePlaneDistance, new object[]
				{
					"farPlaneDistance"
				}));
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException(MathResources.OppositePlanes);
			}
			Matrix result;
			result.M11 = 2f * nearPlaneDistance / width;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f * nearPlaneDistance / height;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M31 = (result.M32 = 0f);
			result.M34 = -1f;
			result.M41 = (result.M42 = (result.M44 = 0f));
			result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			return result;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00013AE4 File Offset: 0x00011CE4
		public static void CreatePerspective(float width, float height, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
		{
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.NegativePlaneDistance, new object[]
				{
					"nearPlaneDistance"
				}));
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.NegativePlaneDistance, new object[]
				{
					"farPlaneDistance"
				}));
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException(MathResources.OppositePlanes);
			}
			result.M11 = 2f * nearPlaneDistance / width;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f * nearPlaneDistance / height;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M31 = (result.M32 = 0f);
			result.M34 = -1f;
			result.M41 = (result.M42 = (result.M44 = 0f));
			result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00013C14 File Offset: 0x00011E14
		public static Matrix CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance)
		{
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.NegativePlaneDistance, new object[]
				{
					"nearPlaneDistance"
				}));
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.NegativePlaneDistance, new object[]
				{
					"farPlaneDistance"
				}));
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException(MathResources.OppositePlanes);
			}
			Matrix result;
			result.M11 = 2f * nearPlaneDistance / (right - left);
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f * nearPlaneDistance / (top - bottom);
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M31 = (left + right) / (right - left);
			result.M32 = (top + bottom) / (top - bottom);
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -1f;
			result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M41 = (result.M42 = (result.M44 = 0f));
			return result;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00013D5C File Offset: 0x00011F5C
		public static void CreatePerspectiveOffCenter(float left, float right, float bottom, float top, float nearPlaneDistance, float farPlaneDistance, out Matrix result)
		{
			if (nearPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.NegativePlaneDistance, new object[]
				{
					"nearPlaneDistance"
				}));
			}
			if (farPlaneDistance <= 0f)
			{
				throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, MathResources.NegativePlaneDistance, new object[]
				{
					"farPlaneDistance"
				}));
			}
			if (nearPlaneDistance >= farPlaneDistance)
			{
				throw new ArgumentOutOfRangeException(MathResources.OppositePlanes);
			}
			result.M11 = 2f * nearPlaneDistance / (right - left);
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f * nearPlaneDistance / (top - bottom);
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M31 = (left + right) / (right - left);
			result.M32 = (top + bottom) / (top - bottom);
			result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M34 = -1f;
			result.M43 = nearPlaneDistance * farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
			result.M41 = (result.M42 = (result.M44 = 0f));
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00013EA4 File Offset: 0x000120A4
		public static Matrix CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane)
		{
			Matrix result;
			result.M11 = 2f / width;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f / height;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M33 = 1f / (zNearPlane - zFarPlane);
			result.M31 = (result.M32 = (result.M34 = 0f));
			result.M41 = (result.M42 = 0f);
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = 1f;
			return result;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00013F6C File Offset: 0x0001216C
		public static void CreateOrthographic(float width, float height, float zNearPlane, float zFarPlane, out Matrix result)
		{
			result.M11 = 2f / width;
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f / height;
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M33 = 1f / (zNearPlane - zFarPlane);
			result.M31 = (result.M32 = (result.M34 = 0f));
			result.M41 = (result.M42 = 0f);
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = 1f;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00014034 File Offset: 0x00012234
		public static Matrix CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane)
		{
			Matrix result;
			result.M11 = 2f / (right - left);
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f / (top - bottom);
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M33 = 1f / (zNearPlane - zFarPlane);
			result.M31 = (result.M32 = (result.M34 = 0f));
			result.M41 = (left + right) / (left - right);
			result.M42 = (top + bottom) / (bottom - top);
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = 1f;
			return result;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x0001410C File Offset: 0x0001230C
		public static void CreateOrthographicOffCenter(float left, float right, float bottom, float top, float zNearPlane, float zFarPlane, out Matrix result)
		{
			result.M11 = 2f / (right - left);
			result.M12 = (result.M13 = (result.M14 = 0f));
			result.M22 = 2f / (top - bottom);
			result.M21 = (result.M23 = (result.M24 = 0f));
			result.M33 = 1f / (zNearPlane - zFarPlane);
			result.M31 = (result.M32 = (result.M34 = 0f));
			result.M41 = (left + right) / (left - right);
			result.M42 = (top + bottom) / (bottom - top);
			result.M43 = zNearPlane / (zNearPlane - zFarPlane);
			result.M44 = 1f;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000141E4 File Offset: 0x000123E4
		public static Matrix CreateLookAt(Vector3 cameraPosition, Vector3 cameraTarget, Vector3 cameraUpVector)
		{
			Vector3 vector = Vector3.Normalize(cameraPosition - cameraTarget);
			Vector3 vector2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, vector));
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			Matrix result;
			result.M11 = vector2.X;
			result.M12 = vector3.X;
			result.M13 = vector.X;
			result.M14 = 0f;
			result.M21 = vector2.Y;
			result.M22 = vector3.Y;
			result.M23 = vector.Y;
			result.M24 = 0f;
			result.M31 = vector2.Z;
			result.M32 = vector3.Z;
			result.M33 = vector.Z;
			result.M34 = 0f;
			result.M41 = -Vector3.Dot(vector2, cameraPosition);
			result.M42 = -Vector3.Dot(vector3, cameraPosition);
			result.M43 = -Vector3.Dot(vector, cameraPosition);
			result.M44 = 1f;
			return result;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000142E8 File Offset: 0x000124E8
		public static void CreateLookAt(ref Vector3 cameraPosition, ref Vector3 cameraTarget, ref Vector3 cameraUpVector, out Matrix result)
		{
			Vector3 vector = Vector3.Normalize(cameraPosition - cameraTarget);
			Vector3 vector2 = Vector3.Normalize(Vector3.Cross(cameraUpVector, vector));
			Vector3 vector3 = Vector3.Cross(vector, vector2);
			result.M11 = vector2.X;
			result.M12 = vector3.X;
			result.M13 = vector.X;
			result.M14 = 0f;
			result.M21 = vector2.Y;
			result.M22 = vector3.Y;
			result.M23 = vector.Y;
			result.M24 = 0f;
			result.M31 = vector2.Z;
			result.M32 = vector3.Z;
			result.M33 = vector.Z;
			result.M34 = 0f;
			result.M41 = -Vector3.Dot(vector2, cameraPosition);
			result.M42 = -Vector3.Dot(vector3, cameraPosition);
			result.M43 = -Vector3.Dot(vector, cameraPosition);
			result.M44 = 1f;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000143F8 File Offset: 0x000125F8
		public static Matrix CreateShadow(Vector3 lightDirection, Plane plane)
		{
			Plane plane2;
			Plane.Normalize(ref plane, out plane2);
			float num = plane2.Normal.X * lightDirection.X + plane2.Normal.Y * lightDirection.Y + plane2.Normal.Z * lightDirection.Z;
			float num2 = -plane2.Normal.X;
			float num3 = -plane2.Normal.Y;
			float num4 = -plane2.Normal.Z;
			float num5 = -plane2.D;
			Matrix result;
			result.M11 = num2 * lightDirection.X + num;
			result.M21 = num3 * lightDirection.X;
			result.M31 = num4 * lightDirection.X;
			result.M41 = num5 * lightDirection.X;
			result.M12 = num2 * lightDirection.Y;
			result.M22 = num3 * lightDirection.Y + num;
			result.M32 = num4 * lightDirection.Y;
			result.M42 = num5 * lightDirection.Y;
			result.M13 = num2 * lightDirection.Z;
			result.M23 = num3 * lightDirection.Z;
			result.M33 = num4 * lightDirection.Z + num;
			result.M43 = num5 * lightDirection.Z;
			result.M14 = 0f;
			result.M24 = 0f;
			result.M34 = 0f;
			result.M44 = num;
			return result;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00014568 File Offset: 0x00012768
		public static void CreateShadow(ref Vector3 lightDirection, ref Plane plane, out Matrix result)
		{
			Plane plane2;
			Plane.Normalize(ref plane, out plane2);
			float num = plane2.Normal.X * lightDirection.X + plane2.Normal.Y * lightDirection.Y + plane2.Normal.Z * lightDirection.Z;
			float num2 = -plane2.Normal.X;
			float num3 = -plane2.Normal.Y;
			float num4 = -plane2.Normal.Z;
			float num5 = -plane2.D;
			result.M11 = num2 * lightDirection.X + num;
			result.M21 = num3 * lightDirection.X;
			result.M31 = num4 * lightDirection.X;
			result.M41 = num5 * lightDirection.X;
			result.M12 = num2 * lightDirection.Y;
			result.M22 = num3 * lightDirection.Y + num;
			result.M32 = num4 * lightDirection.Y;
			result.M42 = num5 * lightDirection.Y;
			result.M13 = num2 * lightDirection.Z;
			result.M23 = num3 * lightDirection.Z;
			result.M33 = num4 * lightDirection.Z + num;
			result.M43 = num5 * lightDirection.Z;
			result.M14 = 0f;
			result.M24 = 0f;
			result.M34 = 0f;
			result.M44 = num;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000146C4 File Offset: 0x000128C4
		public static Matrix CreateReflection(Plane value)
		{
			value.Normalize();
			float x = value.Normal.X;
			float y = value.Normal.Y;
			float z = value.Normal.Z;
			float num = -2f * x;
			float num2 = -2f * y;
			float num3 = -2f * z;
			Matrix result;
			result.M11 = num * x + 1f;
			result.M12 = num2 * x;
			result.M13 = num3 * x;
			result.M14 = 0f;
			result.M21 = num * y;
			result.M22 = num2 * y + 1f;
			result.M23 = num3 * y;
			result.M24 = 0f;
			result.M31 = num * z;
			result.M32 = num2 * z;
			result.M33 = num3 * z + 1f;
			result.M34 = 0f;
			result.M41 = num * value.D;
			result.M42 = num2 * value.D;
			result.M43 = num3 * value.D;
			result.M44 = 1f;
			return result;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000147EC File Offset: 0x000129EC
		public static void CreateReflection(ref Plane value, out Matrix result)
		{
			Plane plane;
			Plane.Normalize(ref value, out plane);
			value.Normalize();
			float x = plane.Normal.X;
			float y = plane.Normal.Y;
			float z = plane.Normal.Z;
			float num = -2f * x;
			float num2 = -2f * y;
			float num3 = -2f * z;
			result.M11 = num * x + 1f;
			result.M12 = num2 * x;
			result.M13 = num3 * x;
			result.M14 = 0f;
			result.M21 = num * y;
			result.M22 = num2 * y + 1f;
			result.M23 = num3 * y;
			result.M24 = 0f;
			result.M31 = num * z;
			result.M32 = num2 * z;
			result.M33 = num3 * z + 1f;
			result.M34 = 0f;
			result.M41 = num * plane.D;
			result.M42 = num2 * plane.D;
			result.M43 = num3 * plane.D;
			result.M44 = 1f;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0001490C File Offset: 0x00012B0C
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Concat(new string[]
			{
				"{ ",
				string.Format(currentCulture, "{{M11:{0} M12:{1} M13:{2} M14:{3}}} ", new object[]
				{
					this.M11.ToString(currentCulture),
					this.M12.ToString(currentCulture),
					this.M13.ToString(currentCulture),
					this.M14.ToString(currentCulture)
				}),
				string.Format(currentCulture, "{{M21:{0} M22:{1} M23:{2} M24:{3}}} ", new object[]
				{
					this.M21.ToString(currentCulture),
					this.M22.ToString(currentCulture),
					this.M23.ToString(currentCulture),
					this.M24.ToString(currentCulture)
				}),
				string.Format(currentCulture, "{{M31:{0} M32:{1} M33:{2} M34:{3}}} ", new object[]
				{
					this.M31.ToString(currentCulture),
					this.M32.ToString(currentCulture),
					this.M33.ToString(currentCulture),
					this.M34.ToString(currentCulture)
				}),
				string.Format(currentCulture, "{{M41:{0} M42:{1} M43:{2} M44:{3}}} ", new object[]
				{
					this.M41.ToString(currentCulture),
					this.M42.ToString(currentCulture),
					this.M43.ToString(currentCulture),
					this.M44.ToString(currentCulture)
				}),
				"}"
			});
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00014A7C File Offset: 0x00012C7C
		public bool Equals(Matrix other)
		{
			return this.M11 == other.M11 && this.M22 == other.M22 && this.M33 == other.M33 && this.M44 == other.M44 && this.M12 == other.M12 && this.M13 == other.M13 && this.M14 == other.M14 && this.M21 == other.M21 && this.M23 == other.M23 && this.M24 == other.M24 && this.M31 == other.M31 && this.M32 == other.M32 && this.M34 == other.M34 && this.M41 == other.M41 && this.M42 == other.M42 && this.M43 == other.M43;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00014B80 File Offset: 0x00012D80
		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj is Matrix)
			{
				result = this.Equals((Matrix)obj);
			}
			return result;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00014BA8 File Offset: 0x00012DA8
		public override int GetHashCode()
		{
			return this.M11.GetHashCode() + this.M12.GetHashCode() + this.M13.GetHashCode() + this.M14.GetHashCode() + this.M21.GetHashCode() + this.M22.GetHashCode() + this.M23.GetHashCode() + this.M24.GetHashCode() + this.M31.GetHashCode() + this.M32.GetHashCode() + this.M33.GetHashCode() + this.M34.GetHashCode() + this.M41.GetHashCode() + this.M42.GetHashCode() + this.M43.GetHashCode() + this.M44.GetHashCode();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00014C74 File Offset: 0x00012E74
		public static Matrix Transpose(Matrix matrix)
		{
			Matrix result;
			result.M11 = matrix.M11;
			result.M12 = matrix.M21;
			result.M13 = matrix.M31;
			result.M14 = matrix.M41;
			result.M21 = matrix.M12;
			result.M22 = matrix.M22;
			result.M23 = matrix.M32;
			result.M24 = matrix.M42;
			result.M31 = matrix.M13;
			result.M32 = matrix.M23;
			result.M33 = matrix.M33;
			result.M34 = matrix.M43;
			result.M41 = matrix.M14;
			result.M42 = matrix.M24;
			result.M43 = matrix.M34;
			result.M44 = matrix.M44;
			return result;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00014D54 File Offset: 0x00012F54
		public static void Transpose(ref Matrix matrix, out Matrix result)
		{
			float m = matrix.M11;
			float m2 = matrix.M12;
			float m3 = matrix.M13;
			float m4 = matrix.M14;
			float m5 = matrix.M21;
			float m6 = matrix.M22;
			float m7 = matrix.M23;
			float m8 = matrix.M24;
			float m9 = matrix.M31;
			float m10 = matrix.M32;
			float m11 = matrix.M33;
			float m12 = matrix.M34;
			float m13 = matrix.M41;
			float m14 = matrix.M42;
			float m15 = matrix.M43;
			float m16 = matrix.M44;
			result.M11 = m;
			result.M12 = m5;
			result.M13 = m9;
			result.M14 = m13;
			result.M21 = m2;
			result.M22 = m6;
			result.M23 = m10;
			result.M24 = m14;
			result.M31 = m3;
			result.M32 = m7;
			result.M33 = m11;
			result.M34 = m15;
			result.M41 = m4;
			result.M42 = m8;
			result.M43 = m12;
			result.M44 = m16;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00014E5C File Offset: 0x0001305C
		public float Determinant()
		{
			float m = this.M11;
			float m2 = this.M12;
			float m3 = this.M13;
			float m4 = this.M14;
			float m5 = this.M21;
			float m6 = this.M22;
			float m7 = this.M23;
			float m8 = this.M24;
			float m9 = this.M31;
			float m10 = this.M32;
			float m11 = this.M33;
			float m12 = this.M34;
			float m13 = this.M41;
			float m14 = this.M42;
			float m15 = this.M43;
			float m16 = this.M44;
			float num = m11 * m16 - m12 * m15;
			float num2 = m10 * m16 - m12 * m14;
			float num3 = m10 * m15 - m11 * m14;
			float num4 = m9 * m16 - m12 * m13;
			float num5 = m9 * m15 - m11 * m13;
			float num6 = m9 * m14 - m10 * m13;
			return m * (m6 * num - m7 * num2 + m8 * num3) - m2 * (m5 * num - m7 * num4 + m8 * num5) + m3 * (m5 * num2 - m6 * num4 + m8 * num6) - m4 * (m5 * num3 - m6 * num5 + m7 * num6);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00014F78 File Offset: 0x00013178
		public static Matrix Invert(Matrix matrix)
		{
			float m = matrix.M11;
			float m2 = matrix.M12;
			float m3 = matrix.M13;
			float m4 = matrix.M14;
			float m5 = matrix.M21;
			float m6 = matrix.M22;
			float m7 = matrix.M23;
			float m8 = matrix.M24;
			float m9 = matrix.M31;
			float m10 = matrix.M32;
			float m11 = matrix.M33;
			float m12 = matrix.M34;
			float m13 = matrix.M41;
			float m14 = matrix.M42;
			float m15 = matrix.M43;
			float m16 = matrix.M44;
			float num = m11 * m16 - m12 * m15;
			float num2 = m10 * m16 - m12 * m14;
			float num3 = m10 * m15 - m11 * m14;
			float num4 = m9 * m16 - m12 * m13;
			float num5 = m9 * m15 - m11 * m13;
			float num6 = m9 * m14 - m10 * m13;
			float num7 = m6 * num - m7 * num2 + m8 * num3;
			float num8 = -(m5 * num - m7 * num4 + m8 * num5);
			float num9 = m5 * num2 - m6 * num4 + m8 * num6;
			float num10 = -(m5 * num3 - m6 * num5 + m7 * num6);
			float num11 = 1f / (m * num7 + m2 * num8 + m3 * num9 + m4 * num10);
			Matrix result;
			result.M11 = num7 * num11;
			result.M21 = num8 * num11;
			result.M31 = num9 * num11;
			result.M41 = num10 * num11;
			result.M12 = -(m2 * num - m3 * num2 + m4 * num3) * num11;
			result.M22 = (m * num - m3 * num4 + m4 * num5) * num11;
			result.M32 = -(m * num2 - m2 * num4 + m4 * num6) * num11;
			result.M42 = (m * num3 - m2 * num5 + m3 * num6) * num11;
			float num12 = m7 * m16 - m8 * m15;
			float num13 = m6 * m16 - m8 * m14;
			float num14 = m6 * m15 - m7 * m14;
			float num15 = m5 * m16 - m8 * m13;
			float num16 = m5 * m15 - m7 * m13;
			float num17 = m5 * m14 - m6 * m13;
			result.M13 = (m2 * num12 - m3 * num13 + m4 * num14) * num11;
			result.M23 = -(m * num12 - m3 * num15 + m4 * num16) * num11;
			result.M33 = (m * num13 - m2 * num15 + m4 * num17) * num11;
			result.M43 = -(m * num14 - m2 * num16 + m3 * num17) * num11;
			float num18 = m7 * m12 - m8 * m11;
			float num19 = m6 * m12 - m8 * m10;
			float num20 = m6 * m11 - m7 * m10;
			float num21 = m5 * m12 - m8 * m9;
			float num22 = m5 * m11 - m7 * m9;
			float num23 = m5 * m10 - m6 * m9;
			result.M14 = -(m2 * num18 - m3 * num19 + m4 * num20) * num11;
			result.M24 = (m * num18 - m3 * num21 + m4 * num22) * num11;
			result.M34 = -(m * num19 - m2 * num21 + m4 * num23) * num11;
			result.M44 = (m * num20 - m2 * num22 + m3 * num23) * num11;
			return result;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000152A0 File Offset: 0x000134A0
		public static void Invert(ref Matrix matrix, out Matrix result)
		{
			float m = matrix.M11;
			float m2 = matrix.M12;
			float m3 = matrix.M13;
			float m4 = matrix.M14;
			float m5 = matrix.M21;
			float m6 = matrix.M22;
			float m7 = matrix.M23;
			float m8 = matrix.M24;
			float m9 = matrix.M31;
			float m10 = matrix.M32;
			float m11 = matrix.M33;
			float m12 = matrix.M34;
			float m13 = matrix.M41;
			float m14 = matrix.M42;
			float m15 = matrix.M43;
			float m16 = matrix.M44;
			float num = m11 * m16 - m12 * m15;
			float num2 = m10 * m16 - m12 * m14;
			float num3 = m10 * m15 - m11 * m14;
			float num4 = m9 * m16 - m12 * m13;
			float num5 = m9 * m15 - m11 * m13;
			float num6 = m9 * m14 - m10 * m13;
			float num7 = m6 * num - m7 * num2 + m8 * num3;
			float num8 = -(m5 * num - m7 * num4 + m8 * num5);
			float num9 = m5 * num2 - m6 * num4 + m8 * num6;
			float num10 = -(m5 * num3 - m6 * num5 + m7 * num6);
			float num11 = 1f / (m * num7 + m2 * num8 + m3 * num9 + m4 * num10);
			result.M11 = num7 * num11;
			result.M21 = num8 * num11;
			result.M31 = num9 * num11;
			result.M41 = num10 * num11;
			result.M12 = -(m2 * num - m3 * num2 + m4 * num3) * num11;
			result.M22 = (m * num - m3 * num4 + m4 * num5) * num11;
			result.M32 = -(m * num2 - m2 * num4 + m4 * num6) * num11;
			result.M42 = (m * num3 - m2 * num5 + m3 * num6) * num11;
			float num12 = m7 * m16 - m8 * m15;
			float num13 = m6 * m16 - m8 * m14;
			float num14 = m6 * m15 - m7 * m14;
			float num15 = m5 * m16 - m8 * m13;
			float num16 = m5 * m15 - m7 * m13;
			float num17 = m5 * m14 - m6 * m13;
			result.M13 = (m2 * num12 - m3 * num13 + m4 * num14) * num11;
			result.M23 = -(m * num12 - m3 * num15 + m4 * num16) * num11;
			result.M33 = (m * num13 - m2 * num15 + m4 * num17) * num11;
			result.M43 = -(m * num14 - m2 * num16 + m3 * num17) * num11;
			float num18 = m7 * m12 - m8 * m11;
			float num19 = m6 * m12 - m8 * m10;
			float num20 = m6 * m11 - m7 * m10;
			float num21 = m5 * m12 - m8 * m9;
			float num22 = m5 * m11 - m7 * m9;
			float num23 = m5 * m10 - m6 * m9;
			result.M14 = -(m2 * num18 - m3 * num19 + m4 * num20) * num11;
			result.M24 = (m * num18 - m3 * num21 + m4 * num22) * num11;
			result.M34 = -(m * num19 - m2 * num21 + m4 * num23) * num11;
			result.M44 = (m * num20 - m2 * num22 + m3 * num23) * num11;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000155B8 File Offset: 0x000137B8
		public static Matrix Lerp(Matrix matrix1, Matrix matrix2, float amount)
		{
			Matrix result;
			result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
			result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
			result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
			result.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14) * amount;
			result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
			result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
			result.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23) * amount;
			result.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24) * amount;
			result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
			result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
			result.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33) * amount;
			result.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34) * amount;
			result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
			result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
			result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
			result.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44) * amount;
			return result;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00015798 File Offset: 0x00013998
		public static void Lerp(ref Matrix matrix1, ref Matrix matrix2, float amount, out Matrix result)
		{
			result.M11 = matrix1.M11 + (matrix2.M11 - matrix1.M11) * amount;
			result.M12 = matrix1.M12 + (matrix2.M12 - matrix1.M12) * amount;
			result.M13 = matrix1.M13 + (matrix2.M13 - matrix1.M13) * amount;
			result.M14 = matrix1.M14 + (matrix2.M14 - matrix1.M14) * amount;
			result.M21 = matrix1.M21 + (matrix2.M21 - matrix1.M21) * amount;
			result.M22 = matrix1.M22 + (matrix2.M22 - matrix1.M22) * amount;
			result.M23 = matrix1.M23 + (matrix2.M23 - matrix1.M23) * amount;
			result.M24 = matrix1.M24 + (matrix2.M24 - matrix1.M24) * amount;
			result.M31 = matrix1.M31 + (matrix2.M31 - matrix1.M31) * amount;
			result.M32 = matrix1.M32 + (matrix2.M32 - matrix1.M32) * amount;
			result.M33 = matrix1.M33 + (matrix2.M33 - matrix1.M33) * amount;
			result.M34 = matrix1.M34 + (matrix2.M34 - matrix1.M34) * amount;
			result.M41 = matrix1.M41 + (matrix2.M41 - matrix1.M41) * amount;
			result.M42 = matrix1.M42 + (matrix2.M42 - matrix1.M42) * amount;
			result.M43 = matrix1.M43 + (matrix2.M43 - matrix1.M43) * amount;
			result.M44 = matrix1.M44 + (matrix2.M44 - matrix1.M44) * amount;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00015968 File Offset: 0x00013B68
		public static Matrix Negate(Matrix matrix)
		{
			Matrix result;
			result.M11 = -matrix.M11;
			result.M12 = -matrix.M12;
			result.M13 = -matrix.M13;
			result.M14 = -matrix.M14;
			result.M21 = -matrix.M21;
			result.M22 = -matrix.M22;
			result.M23 = -matrix.M23;
			result.M24 = -matrix.M24;
			result.M31 = -matrix.M31;
			result.M32 = -matrix.M32;
			result.M33 = -matrix.M33;
			result.M34 = -matrix.M34;
			result.M41 = -matrix.M41;
			result.M42 = -matrix.M42;
			result.M43 = -matrix.M43;
			result.M44 = -matrix.M44;
			return result;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00015A58 File Offset: 0x00013C58
		public static void Negate(ref Matrix matrix, out Matrix result)
		{
			result.M11 = -matrix.M11;
			result.M12 = -matrix.M12;
			result.M13 = -matrix.M13;
			result.M14 = -matrix.M14;
			result.M21 = -matrix.M21;
			result.M22 = -matrix.M22;
			result.M23 = -matrix.M23;
			result.M24 = -matrix.M24;
			result.M31 = -matrix.M31;
			result.M32 = -matrix.M32;
			result.M33 = -matrix.M33;
			result.M34 = -matrix.M34;
			result.M41 = -matrix.M41;
			result.M42 = -matrix.M42;
			result.M43 = -matrix.M43;
			result.M44 = -matrix.M44;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00015B38 File Offset: 0x00013D38
		public static Matrix Add(Matrix matrix1, Matrix matrix2)
		{
			Matrix result;
			result.M11 = matrix1.M11 + matrix2.M11;
			result.M12 = matrix1.M12 + matrix2.M12;
			result.M13 = matrix1.M13 + matrix2.M13;
			result.M14 = matrix1.M14 + matrix2.M14;
			result.M21 = matrix1.M21 + matrix2.M21;
			result.M22 = matrix1.M22 + matrix2.M22;
			result.M23 = matrix1.M23 + matrix2.M23;
			result.M24 = matrix1.M24 + matrix2.M24;
			result.M31 = matrix1.M31 + matrix2.M31;
			result.M32 = matrix1.M32 + matrix2.M32;
			result.M33 = matrix1.M33 + matrix2.M33;
			result.M34 = matrix1.M34 + matrix2.M34;
			result.M41 = matrix1.M41 + matrix2.M41;
			result.M42 = matrix1.M42 + matrix2.M42;
			result.M43 = matrix1.M43 + matrix2.M43;
			result.M44 = matrix1.M44 + matrix2.M44;
			return result;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00015C88 File Offset: 0x00013E88
		public static void Add(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
		{
			result.M11 = matrix1.M11 + matrix2.M11;
			result.M12 = matrix1.M12 + matrix2.M12;
			result.M13 = matrix1.M13 + matrix2.M13;
			result.M14 = matrix1.M14 + matrix2.M14;
			result.M21 = matrix1.M21 + matrix2.M21;
			result.M22 = matrix1.M22 + matrix2.M22;
			result.M23 = matrix1.M23 + matrix2.M23;
			result.M24 = matrix1.M24 + matrix2.M24;
			result.M31 = matrix1.M31 + matrix2.M31;
			result.M32 = matrix1.M32 + matrix2.M32;
			result.M33 = matrix1.M33 + matrix2.M33;
			result.M34 = matrix1.M34 + matrix2.M34;
			result.M41 = matrix1.M41 + matrix2.M41;
			result.M42 = matrix1.M42 + matrix2.M42;
			result.M43 = matrix1.M43 + matrix2.M43;
			result.M44 = matrix1.M44 + matrix2.M44;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00015DC8 File Offset: 0x00013FC8
		public static Matrix Subtract(Matrix matrix1, Matrix matrix2)
		{
			Matrix result;
			result.M11 = matrix1.M11 - matrix2.M11;
			result.M12 = matrix1.M12 - matrix2.M12;
			result.M13 = matrix1.M13 - matrix2.M13;
			result.M14 = matrix1.M14 - matrix2.M14;
			result.M21 = matrix1.M21 - matrix2.M21;
			result.M22 = matrix1.M22 - matrix2.M22;
			result.M23 = matrix1.M23 - matrix2.M23;
			result.M24 = matrix1.M24 - matrix2.M24;
			result.M31 = matrix1.M31 - matrix2.M31;
			result.M32 = matrix1.M32 - matrix2.M32;
			result.M33 = matrix1.M33 - matrix2.M33;
			result.M34 = matrix1.M34 - matrix2.M34;
			result.M41 = matrix1.M41 - matrix2.M41;
			result.M42 = matrix1.M42 - matrix2.M42;
			result.M43 = matrix1.M43 - matrix2.M43;
			result.M44 = matrix1.M44 - matrix2.M44;
			return result;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00015F18 File Offset: 0x00014118
		public static void Subtract(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
		{
			result.M11 = matrix1.M11 - matrix2.M11;
			result.M12 = matrix1.M12 - matrix2.M12;
			result.M13 = matrix1.M13 - matrix2.M13;
			result.M14 = matrix1.M14 - matrix2.M14;
			result.M21 = matrix1.M21 - matrix2.M21;
			result.M22 = matrix1.M22 - matrix2.M22;
			result.M23 = matrix1.M23 - matrix2.M23;
			result.M24 = matrix1.M24 - matrix2.M24;
			result.M31 = matrix1.M31 - matrix2.M31;
			result.M32 = matrix1.M32 - matrix2.M32;
			result.M33 = matrix1.M33 - matrix2.M33;
			result.M34 = matrix1.M34 - matrix2.M34;
			result.M41 = matrix1.M41 - matrix2.M41;
			result.M42 = matrix1.M42 - matrix2.M42;
			result.M43 = matrix1.M43 - matrix2.M43;
			result.M44 = matrix1.M44 - matrix2.M44;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00016058 File Offset: 0x00014258
		public static Matrix Multiply(Matrix matrix1, Matrix matrix2)
		{
			Matrix result;
			result.M11 = matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21 + matrix1.M13 * matrix2.M31 + matrix1.M14 * matrix2.M41;
			result.M12 = matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22 + matrix1.M13 * matrix2.M32 + matrix1.M14 * matrix2.M42;
			result.M13 = matrix1.M11 * matrix2.M13 + matrix1.M12 * matrix2.M23 + matrix1.M13 * matrix2.M33 + matrix1.M14 * matrix2.M43;
			result.M14 = matrix1.M11 * matrix2.M14 + matrix1.M12 * matrix2.M24 + matrix1.M13 * matrix2.M34 + matrix1.M14 * matrix2.M44;
			result.M21 = matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21 + matrix1.M23 * matrix2.M31 + matrix1.M24 * matrix2.M41;
			result.M22 = matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22 + matrix1.M23 * matrix2.M32 + matrix1.M24 * matrix2.M42;
			result.M23 = matrix1.M21 * matrix2.M13 + matrix1.M22 * matrix2.M23 + matrix1.M23 * matrix2.M33 + matrix1.M24 * matrix2.M43;
			result.M24 = matrix1.M21 * matrix2.M14 + matrix1.M22 * matrix2.M24 + matrix1.M23 * matrix2.M34 + matrix1.M24 * matrix2.M44;
			result.M31 = matrix1.M31 * matrix2.M11 + matrix1.M32 * matrix2.M21 + matrix1.M33 * matrix2.M31 + matrix1.M34 * matrix2.M41;
			result.M32 = matrix1.M31 * matrix2.M12 + matrix1.M32 * matrix2.M22 + matrix1.M33 * matrix2.M32 + matrix1.M34 * matrix2.M42;
			result.M33 = matrix1.M31 * matrix2.M13 + matrix1.M32 * matrix2.M23 + matrix1.M33 * matrix2.M33 + matrix1.M34 * matrix2.M43;
			result.M34 = matrix1.M31 * matrix2.M14 + matrix1.M32 * matrix2.M24 + matrix1.M33 * matrix2.M34 + matrix1.M34 * matrix2.M44;
			result.M41 = matrix1.M41 * matrix2.M11 + matrix1.M42 * matrix2.M21 + matrix1.M43 * matrix2.M31 + matrix1.M44 * matrix2.M41;
			result.M42 = matrix1.M41 * matrix2.M12 + matrix1.M42 * matrix2.M22 + matrix1.M43 * matrix2.M32 + matrix1.M44 * matrix2.M42;
			result.M43 = matrix1.M41 * matrix2.M13 + matrix1.M42 * matrix2.M23 + matrix1.M43 * matrix2.M33 + matrix1.M44 * matrix2.M43;
			result.M44 = matrix1.M41 * matrix2.M14 + matrix1.M42 * matrix2.M24 + matrix1.M43 * matrix2.M34 + matrix1.M44 * matrix2.M44;
			return result;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00016448 File Offset: 0x00014648
		public static void Multiply(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
		{
			float m = matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21 + matrix1.M13 * matrix2.M31 + matrix1.M14 * matrix2.M41;
			float m2 = matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22 + matrix1.M13 * matrix2.M32 + matrix1.M14 * matrix2.M42;
			float m3 = matrix1.M11 * matrix2.M13 + matrix1.M12 * matrix2.M23 + matrix1.M13 * matrix2.M33 + matrix1.M14 * matrix2.M43;
			float m4 = matrix1.M11 * matrix2.M14 + matrix1.M12 * matrix2.M24 + matrix1.M13 * matrix2.M34 + matrix1.M14 * matrix2.M44;
			float m5 = matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21 + matrix1.M23 * matrix2.M31 + matrix1.M24 * matrix2.M41;
			float m6 = matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22 + matrix1.M23 * matrix2.M32 + matrix1.M24 * matrix2.M42;
			float m7 = matrix1.M21 * matrix2.M13 + matrix1.M22 * matrix2.M23 + matrix1.M23 * matrix2.M33 + matrix1.M24 * matrix2.M43;
			float m8 = matrix1.M21 * matrix2.M14 + matrix1.M22 * matrix2.M24 + matrix1.M23 * matrix2.M34 + matrix1.M24 * matrix2.M44;
			float m9 = matrix1.M31 * matrix2.M11 + matrix1.M32 * matrix2.M21 + matrix1.M33 * matrix2.M31 + matrix1.M34 * matrix2.M41;
			float m10 = matrix1.M31 * matrix2.M12 + matrix1.M32 * matrix2.M22 + matrix1.M33 * matrix2.M32 + matrix1.M34 * matrix2.M42;
			float m11 = matrix1.M31 * matrix2.M13 + matrix1.M32 * matrix2.M23 + matrix1.M33 * matrix2.M33 + matrix1.M34 * matrix2.M43;
			float m12 = matrix1.M31 * matrix2.M14 + matrix1.M32 * matrix2.M24 + matrix1.M33 * matrix2.M34 + matrix1.M34 * matrix2.M44;
			float m13 = matrix1.M41 * matrix2.M11 + matrix1.M42 * matrix2.M21 + matrix1.M43 * matrix2.M31 + matrix1.M44 * matrix2.M41;
			float m14 = matrix1.M41 * matrix2.M12 + matrix1.M42 * matrix2.M22 + matrix1.M43 * matrix2.M32 + matrix1.M44 * matrix2.M42;
			float m15 = matrix1.M41 * matrix2.M13 + matrix1.M42 * matrix2.M23 + matrix1.M43 * matrix2.M33 + matrix1.M44 * matrix2.M43;
			float m16 = matrix1.M41 * matrix2.M14 + matrix1.M42 * matrix2.M24 + matrix1.M43 * matrix2.M34 + matrix1.M44 * matrix2.M44;
			result.M11 = m;
			result.M12 = m2;
			result.M13 = m3;
			result.M14 = m4;
			result.M21 = m5;
			result.M22 = m6;
			result.M23 = m7;
			result.M24 = m8;
			result.M31 = m9;
			result.M32 = m10;
			result.M33 = m11;
			result.M34 = m12;
			result.M41 = m13;
			result.M42 = m14;
			result.M43 = m15;
			result.M44 = m16;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00016860 File Offset: 0x00014A60
		public static Matrix Multiply(Matrix matrix1, float scaleFactor)
		{
			Matrix result;
			result.M11 = matrix1.M11 * scaleFactor;
			result.M12 = matrix1.M12 * scaleFactor;
			result.M13 = matrix1.M13 * scaleFactor;
			result.M14 = matrix1.M14 * scaleFactor;
			result.M21 = matrix1.M21 * scaleFactor;
			result.M22 = matrix1.M22 * scaleFactor;
			result.M23 = matrix1.M23 * scaleFactor;
			result.M24 = matrix1.M24 * scaleFactor;
			result.M31 = matrix1.M31 * scaleFactor;
			result.M32 = matrix1.M32 * scaleFactor;
			result.M33 = matrix1.M33 * scaleFactor;
			result.M34 = matrix1.M34 * scaleFactor;
			result.M41 = matrix1.M41 * scaleFactor;
			result.M42 = matrix1.M42 * scaleFactor;
			result.M43 = matrix1.M43 * scaleFactor;
			result.M44 = matrix1.M44 * scaleFactor;
			return result;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00016960 File Offset: 0x00014B60
		public static void Multiply(ref Matrix matrix1, float scaleFactor, out Matrix result)
		{
			result.M11 = matrix1.M11 * scaleFactor;
			result.M12 = matrix1.M12 * scaleFactor;
			result.M13 = matrix1.M13 * scaleFactor;
			result.M14 = matrix1.M14 * scaleFactor;
			result.M21 = matrix1.M21 * scaleFactor;
			result.M22 = matrix1.M22 * scaleFactor;
			result.M23 = matrix1.M23 * scaleFactor;
			result.M24 = matrix1.M24 * scaleFactor;
			result.M31 = matrix1.M31 * scaleFactor;
			result.M32 = matrix1.M32 * scaleFactor;
			result.M33 = matrix1.M33 * scaleFactor;
			result.M34 = matrix1.M34 * scaleFactor;
			result.M41 = matrix1.M41 * scaleFactor;
			result.M42 = matrix1.M42 * scaleFactor;
			result.M43 = matrix1.M43 * scaleFactor;
			result.M44 = matrix1.M44 * scaleFactor;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00016A50 File Offset: 0x00014C50
		public static Matrix Divide(Matrix matrix1, Matrix matrix2)
		{
			Matrix result;
			result.M11 = matrix1.M11 / matrix2.M11;
			result.M12 = matrix1.M12 / matrix2.M12;
			result.M13 = matrix1.M13 / matrix2.M13;
			result.M14 = matrix1.M14 / matrix2.M14;
			result.M21 = matrix1.M21 / matrix2.M21;
			result.M22 = matrix1.M22 / matrix2.M22;
			result.M23 = matrix1.M23 / matrix2.M23;
			result.M24 = matrix1.M24 / matrix2.M24;
			result.M31 = matrix1.M31 / matrix2.M31;
			result.M32 = matrix1.M32 / matrix2.M32;
			result.M33 = matrix1.M33 / matrix2.M33;
			result.M34 = matrix1.M34 / matrix2.M34;
			result.M41 = matrix1.M41 / matrix2.M41;
			result.M42 = matrix1.M42 / matrix2.M42;
			result.M43 = matrix1.M43 / matrix2.M43;
			result.M44 = matrix1.M44 / matrix2.M44;
			return result;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00016BA0 File Offset: 0x00014DA0
		public static void Divide(ref Matrix matrix1, ref Matrix matrix2, out Matrix result)
		{
			result.M11 = matrix1.M11 / matrix2.M11;
			result.M12 = matrix1.M12 / matrix2.M12;
			result.M13 = matrix1.M13 / matrix2.M13;
			result.M14 = matrix1.M14 / matrix2.M14;
			result.M21 = matrix1.M21 / matrix2.M21;
			result.M22 = matrix1.M22 / matrix2.M22;
			result.M23 = matrix1.M23 / matrix2.M23;
			result.M24 = matrix1.M24 / matrix2.M24;
			result.M31 = matrix1.M31 / matrix2.M31;
			result.M32 = matrix1.M32 / matrix2.M32;
			result.M33 = matrix1.M33 / matrix2.M33;
			result.M34 = matrix1.M34 / matrix2.M34;
			result.M41 = matrix1.M41 / matrix2.M41;
			result.M42 = matrix1.M42 / matrix2.M42;
			result.M43 = matrix1.M43 / matrix2.M43;
			result.M44 = matrix1.M44 / matrix2.M44;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00016CE0 File Offset: 0x00014EE0
		public static Matrix Divide(Matrix matrix1, float divider)
		{
			float num = 1f / divider;
			Matrix result;
			result.M11 = matrix1.M11 * num;
			result.M12 = matrix1.M12 * num;
			result.M13 = matrix1.M13 * num;
			result.M14 = matrix1.M14 * num;
			result.M21 = matrix1.M21 * num;
			result.M22 = matrix1.M22 * num;
			result.M23 = matrix1.M23 * num;
			result.M24 = matrix1.M24 * num;
			result.M31 = matrix1.M31 * num;
			result.M32 = matrix1.M32 * num;
			result.M33 = matrix1.M33 * num;
			result.M34 = matrix1.M34 * num;
			result.M41 = matrix1.M41 * num;
			result.M42 = matrix1.M42 * num;
			result.M43 = matrix1.M43 * num;
			result.M44 = matrix1.M44 * num;
			return result;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00016DE8 File Offset: 0x00014FE8
		public static void Divide(ref Matrix matrix1, float divider, out Matrix result)
		{
			float num = 1f / divider;
			result.M11 = matrix1.M11 * num;
			result.M12 = matrix1.M12 * num;
			result.M13 = matrix1.M13 * num;
			result.M14 = matrix1.M14 * num;
			result.M21 = matrix1.M21 * num;
			result.M22 = matrix1.M22 * num;
			result.M23 = matrix1.M23 * num;
			result.M24 = matrix1.M24 * num;
			result.M31 = matrix1.M31 * num;
			result.M32 = matrix1.M32 * num;
			result.M33 = matrix1.M33 * num;
			result.M34 = matrix1.M34 * num;
			result.M41 = matrix1.M41 * num;
			result.M42 = matrix1.M42 * num;
			result.M43 = matrix1.M43 * num;
			result.M44 = matrix1.M44 * num;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00016EE0 File Offset: 0x000150E0
		public static Matrix operator -(Matrix matrix1)
		{
			Matrix result;
			result.M11 = -matrix1.M11;
			result.M12 = -matrix1.M12;
			result.M13 = -matrix1.M13;
			result.M14 = -matrix1.M14;
			result.M21 = -matrix1.M21;
			result.M22 = -matrix1.M22;
			result.M23 = -matrix1.M23;
			result.M24 = -matrix1.M24;
			result.M31 = -matrix1.M31;
			result.M32 = -matrix1.M32;
			result.M33 = -matrix1.M33;
			result.M34 = -matrix1.M34;
			result.M41 = -matrix1.M41;
			result.M42 = -matrix1.M42;
			result.M43 = -matrix1.M43;
			result.M44 = -matrix1.M44;
			return result;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00016FD0 File Offset: 0x000151D0
		public static bool operator ==(Matrix matrix1, Matrix matrix2)
		{
			return matrix1.M11 == matrix2.M11 && matrix1.M22 == matrix2.M22 && matrix1.M33 == matrix2.M33 && matrix1.M44 == matrix2.M44 && matrix1.M12 == matrix2.M12 && matrix1.M13 == matrix2.M13 && matrix1.M14 == matrix2.M14 && matrix1.M21 == matrix2.M21 && matrix1.M23 == matrix2.M23 && matrix1.M24 == matrix2.M24 && matrix1.M31 == matrix2.M31 && matrix1.M32 == matrix2.M32 && matrix1.M34 == matrix2.M34 && matrix1.M41 == matrix2.M41 && matrix1.M42 == matrix2.M42 && matrix1.M43 == matrix2.M43;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000170D4 File Offset: 0x000152D4
		public static bool operator !=(Matrix matrix1, Matrix matrix2)
		{
			return matrix1.M11 != matrix2.M11 || matrix1.M12 != matrix2.M12 || matrix1.M13 != matrix2.M13 || matrix1.M14 != matrix2.M14 || matrix1.M21 != matrix2.M21 || matrix1.M22 != matrix2.M22 || matrix1.M23 != matrix2.M23 || matrix1.M24 != matrix2.M24 || matrix1.M31 != matrix2.M31 || matrix1.M32 != matrix2.M32 || matrix1.M33 != matrix2.M33 || matrix1.M34 != matrix2.M34 || matrix1.M41 != matrix2.M41 || matrix1.M42 != matrix2.M42 || matrix1.M43 != matrix2.M43 || matrix1.M44 != matrix2.M44;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000171DC File Offset: 0x000153DC
		public static Matrix operator +(Matrix matrix1, Matrix matrix2)
		{
			Matrix result;
			result.M11 = matrix1.M11 + matrix2.M11;
			result.M12 = matrix1.M12 + matrix2.M12;
			result.M13 = matrix1.M13 + matrix2.M13;
			result.M14 = matrix1.M14 + matrix2.M14;
			result.M21 = matrix1.M21 + matrix2.M21;
			result.M22 = matrix1.M22 + matrix2.M22;
			result.M23 = matrix1.M23 + matrix2.M23;
			result.M24 = matrix1.M24 + matrix2.M24;
			result.M31 = matrix1.M31 + matrix2.M31;
			result.M32 = matrix1.M32 + matrix2.M32;
			result.M33 = matrix1.M33 + matrix2.M33;
			result.M34 = matrix1.M34 + matrix2.M34;
			result.M41 = matrix1.M41 + matrix2.M41;
			result.M42 = matrix1.M42 + matrix2.M42;
			result.M43 = matrix1.M43 + matrix2.M43;
			result.M44 = matrix1.M44 + matrix2.M44;
			return result;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0001732C File Offset: 0x0001552C
		public static Matrix operator -(Matrix matrix1, Matrix matrix2)
		{
			Matrix result;
			result.M11 = matrix1.M11 - matrix2.M11;
			result.M12 = matrix1.M12 - matrix2.M12;
			result.M13 = matrix1.M13 - matrix2.M13;
			result.M14 = matrix1.M14 - matrix2.M14;
			result.M21 = matrix1.M21 - matrix2.M21;
			result.M22 = matrix1.M22 - matrix2.M22;
			result.M23 = matrix1.M23 - matrix2.M23;
			result.M24 = matrix1.M24 - matrix2.M24;
			result.M31 = matrix1.M31 - matrix2.M31;
			result.M32 = matrix1.M32 - matrix2.M32;
			result.M33 = matrix1.M33 - matrix2.M33;
			result.M34 = matrix1.M34 - matrix2.M34;
			result.M41 = matrix1.M41 - matrix2.M41;
			result.M42 = matrix1.M42 - matrix2.M42;
			result.M43 = matrix1.M43 - matrix2.M43;
			result.M44 = matrix1.M44 - matrix2.M44;
			return result;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0001747C File Offset: 0x0001567C
		public static Matrix operator *(Matrix matrix1, Matrix matrix2)
		{
			Matrix result;
			result.M11 = matrix1.M11 * matrix2.M11 + matrix1.M12 * matrix2.M21 + matrix1.M13 * matrix2.M31 + matrix1.M14 * matrix2.M41;
			result.M12 = matrix1.M11 * matrix2.M12 + matrix1.M12 * matrix2.M22 + matrix1.M13 * matrix2.M32 + matrix1.M14 * matrix2.M42;
			result.M13 = matrix1.M11 * matrix2.M13 + matrix1.M12 * matrix2.M23 + matrix1.M13 * matrix2.M33 + matrix1.M14 * matrix2.M43;
			result.M14 = matrix1.M11 * matrix2.M14 + matrix1.M12 * matrix2.M24 + matrix1.M13 * matrix2.M34 + matrix1.M14 * matrix2.M44;
			result.M21 = matrix1.M21 * matrix2.M11 + matrix1.M22 * matrix2.M21 + matrix1.M23 * matrix2.M31 + matrix1.M24 * matrix2.M41;
			result.M22 = matrix1.M21 * matrix2.M12 + matrix1.M22 * matrix2.M22 + matrix1.M23 * matrix2.M32 + matrix1.M24 * matrix2.M42;
			result.M23 = matrix1.M21 * matrix2.M13 + matrix1.M22 * matrix2.M23 + matrix1.M23 * matrix2.M33 + matrix1.M24 * matrix2.M43;
			result.M24 = matrix1.M21 * matrix2.M14 + matrix1.M22 * matrix2.M24 + matrix1.M23 * matrix2.M34 + matrix1.M24 * matrix2.M44;
			result.M31 = matrix1.M31 * matrix2.M11 + matrix1.M32 * matrix2.M21 + matrix1.M33 * matrix2.M31 + matrix1.M34 * matrix2.M41;
			result.M32 = matrix1.M31 * matrix2.M12 + matrix1.M32 * matrix2.M22 + matrix1.M33 * matrix2.M32 + matrix1.M34 * matrix2.M42;
			result.M33 = matrix1.M31 * matrix2.M13 + matrix1.M32 * matrix2.M23 + matrix1.M33 * matrix2.M33 + matrix1.M34 * matrix2.M43;
			result.M34 = matrix1.M31 * matrix2.M14 + matrix1.M32 * matrix2.M24 + matrix1.M33 * matrix2.M34 + matrix1.M34 * matrix2.M44;
			result.M41 = matrix1.M41 * matrix2.M11 + matrix1.M42 * matrix2.M21 + matrix1.M43 * matrix2.M31 + matrix1.M44 * matrix2.M41;
			result.M42 = matrix1.M41 * matrix2.M12 + matrix1.M42 * matrix2.M22 + matrix1.M43 * matrix2.M32 + matrix1.M44 * matrix2.M42;
			result.M43 = matrix1.M41 * matrix2.M13 + matrix1.M42 * matrix2.M23 + matrix1.M43 * matrix2.M33 + matrix1.M44 * matrix2.M43;
			result.M44 = matrix1.M41 * matrix2.M14 + matrix1.M42 * matrix2.M24 + matrix1.M43 * matrix2.M34 + matrix1.M44 * matrix2.M44;
			return result;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0001786C File Offset: 0x00015A6C
		public static Matrix operator *(Matrix matrix, float scaleFactor)
		{
			Matrix result;
			result.M11 = matrix.M11 * scaleFactor;
			result.M12 = matrix.M12 * scaleFactor;
			result.M13 = matrix.M13 * scaleFactor;
			result.M14 = matrix.M14 * scaleFactor;
			result.M21 = matrix.M21 * scaleFactor;
			result.M22 = matrix.M22 * scaleFactor;
			result.M23 = matrix.M23 * scaleFactor;
			result.M24 = matrix.M24 * scaleFactor;
			result.M31 = matrix.M31 * scaleFactor;
			result.M32 = matrix.M32 * scaleFactor;
			result.M33 = matrix.M33 * scaleFactor;
			result.M34 = matrix.M34 * scaleFactor;
			result.M41 = matrix.M41 * scaleFactor;
			result.M42 = matrix.M42 * scaleFactor;
			result.M43 = matrix.M43 * scaleFactor;
			result.M44 = matrix.M44 * scaleFactor;
			return result;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0001796C File Offset: 0x00015B6C
		public static Matrix operator *(float scaleFactor, Matrix matrix)
		{
			Matrix result;
			result.M11 = matrix.M11 * scaleFactor;
			result.M12 = matrix.M12 * scaleFactor;
			result.M13 = matrix.M13 * scaleFactor;
			result.M14 = matrix.M14 * scaleFactor;
			result.M21 = matrix.M21 * scaleFactor;
			result.M22 = matrix.M22 * scaleFactor;
			result.M23 = matrix.M23 * scaleFactor;
			result.M24 = matrix.M24 * scaleFactor;
			result.M31 = matrix.M31 * scaleFactor;
			result.M32 = matrix.M32 * scaleFactor;
			result.M33 = matrix.M33 * scaleFactor;
			result.M34 = matrix.M34 * scaleFactor;
			result.M41 = matrix.M41 * scaleFactor;
			result.M42 = matrix.M42 * scaleFactor;
			result.M43 = matrix.M43 * scaleFactor;
			result.M44 = matrix.M44 * scaleFactor;
			return result;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00017A6C File Offset: 0x00015C6C
		public static Matrix operator /(Matrix matrix1, Matrix matrix2)
		{
			Matrix result;
			result.M11 = matrix1.M11 / matrix2.M11;
			result.M12 = matrix1.M12 / matrix2.M12;
			result.M13 = matrix1.M13 / matrix2.M13;
			result.M14 = matrix1.M14 / matrix2.M14;
			result.M21 = matrix1.M21 / matrix2.M21;
			result.M22 = matrix1.M22 / matrix2.M22;
			result.M23 = matrix1.M23 / matrix2.M23;
			result.M24 = matrix1.M24 / matrix2.M24;
			result.M31 = matrix1.M31 / matrix2.M31;
			result.M32 = matrix1.M32 / matrix2.M32;
			result.M33 = matrix1.M33 / matrix2.M33;
			result.M34 = matrix1.M34 / matrix2.M34;
			result.M41 = matrix1.M41 / matrix2.M41;
			result.M42 = matrix1.M42 / matrix2.M42;
			result.M43 = matrix1.M43 / matrix2.M43;
			result.M44 = matrix1.M44 / matrix2.M44;
			return result;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00017BBC File Offset: 0x00015DBC
		public static Matrix operator /(Matrix matrix1, float divider)
		{
			float num = 1f / divider;
			Matrix result;
			result.M11 = matrix1.M11 * num;
			result.M12 = matrix1.M12 * num;
			result.M13 = matrix1.M13 * num;
			result.M14 = matrix1.M14 * num;
			result.M21 = matrix1.M21 * num;
			result.M22 = matrix1.M22 * num;
			result.M23 = matrix1.M23 * num;
			result.M24 = matrix1.M24 * num;
			result.M31 = matrix1.M31 * num;
			result.M32 = matrix1.M32 * num;
			result.M33 = matrix1.M33 * num;
			result.M34 = matrix1.M34 * num;
			result.M41 = matrix1.M41 * num;
			result.M42 = matrix1.M42 * num;
			result.M43 = matrix1.M43 * num;
			result.M44 = matrix1.M44 * num;
			return result;
		}

		// Token: 0x04000140 RID: 320
		public float M11;

		// Token: 0x04000141 RID: 321
		public float M12;

		// Token: 0x04000142 RID: 322
		public float M13;

		// Token: 0x04000143 RID: 323
		public float M14;

		// Token: 0x04000144 RID: 324
		public float M21;

		// Token: 0x04000145 RID: 325
		public float M22;

		// Token: 0x04000146 RID: 326
		public float M23;

		// Token: 0x04000147 RID: 327
		public float M24;

		// Token: 0x04000148 RID: 328
		public float M31;

		// Token: 0x04000149 RID: 329
		public float M32;

		// Token: 0x0400014A RID: 330
		public float M33;

		// Token: 0x0400014B RID: 331
		public float M34;

		// Token: 0x0400014C RID: 332
		public float M41;

		// Token: 0x0400014D RID: 333
		public float M42;

		// Token: 0x0400014E RID: 334
		public float M43;

		// Token: 0x0400014F RID: 335
		public float M44;

		// Token: 0x04000150 RID: 336
		private static Matrix _identity = new Matrix(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);

		// Token: 0x02000072 RID: 114
		private struct CanonicalBasis
		{
			// Token: 0x040002CE RID: 718
			public Vector3 Row0;

			// Token: 0x040002CF RID: 719
			public Vector3 Row1;

			// Token: 0x040002D0 RID: 720
			public Vector3 Row2;
		}
	}
}
