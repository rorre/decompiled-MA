using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x02000025 RID: 37
	internal static class PackUtils
	{
		// Token: 0x06000246 RID: 582 RVA: 0x0001E812 File Offset: 0x0001CA12
		public static uint PackUnsigned(float bitmask, float value)
		{
			return (uint)PackUtils.ClampAndRound(value, 0f, bitmask);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0001E824 File Offset: 0x0001CA24
		public static uint PackSigned(uint bitmask, float value)
		{
			float num = bitmask >> 1;
			float min = -num - 1f;
			return (uint)((int)PackUtils.ClampAndRound(value, min, num) & (int)bitmask);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0001E84B File Offset: 0x0001CA4B
		public static uint PackUNorm(float bitmask, float value)
		{
			value *= bitmask;
			return (uint)PackUtils.ClampAndRound(value, 0f, bitmask);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0001E85F File Offset: 0x0001CA5F
		public static float UnpackUNorm(uint bitmask, uint value)
		{
			value &= bitmask;
			return value / bitmask;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0001E86C File Offset: 0x0001CA6C
		public static uint PackSNorm(uint bitmask, float value)
		{
			float num = bitmask >> 1;
			value *= num;
			return (uint)((int)PackUtils.ClampAndRound(value, -num, num) & (int)bitmask);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0001E890 File Offset: 0x0001CA90
		public static float UnpackSNorm(uint bitmask, uint value)
		{
			uint num = bitmask + 1u >> 1;
			if ((value & num) != 0u)
			{
				if ((value & bitmask) == num)
				{
					return -1f;
				}
				value |= ~bitmask;
			}
			else
			{
				value &= bitmask;
			}
			float num2 = bitmask >> 1;
			return value / num2;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0001E8CC File Offset: 0x0001CACC
		private static double ClampAndRound(float value, float min, float max)
		{
			if (float.IsNaN(value))
			{
				return 0.0;
			}
			if (float.IsInfinity(value))
			{
				return (double)(float.IsNegativeInfinity(value) ? min : max);
			}
			if (value < min)
			{
				return (double)min;
			}
			if (value > max)
			{
				return (double)max;
			}
			return Math.Round((double)value);
		}
	}
}
