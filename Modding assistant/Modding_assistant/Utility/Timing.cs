using System;

namespace Modding_assistant.Utility
{
	// Token: 0x0200000C RID: 12
	public static class Timing
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00007E83 File Offset: 0x00006083
		public static bool isEqualTiming(int time1, int time2, int tolerance)
		{
			return Math.Abs(time1 - time2) <= tolerance;
		}
	}
}
