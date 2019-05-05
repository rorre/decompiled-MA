using System;

namespace Modding_assistant.Utility
{
	// Token: 0x0200000E RID: 14
	public static class ut_Math
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00007F14 File Offset: 0x00006114
		public static float Lerp(float a, float b, float t)
		{
			return a * (1f - t) + b * t;
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00007F23 File Offset: 0x00006123
		public static float Clamp(float value, float min, float max)
		{
			if (float.IsNaN(min) || float.IsNaN(max))
			{
				return float.NaN;
			}
			if (value > max)
			{
				return max;
			}
			if (value < min)
			{
				return min;
			}
			return value;
		}
	}
}
