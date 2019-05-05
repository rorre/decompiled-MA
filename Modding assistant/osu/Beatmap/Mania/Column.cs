using System;

namespace osu.Beatmap.Mania
{
	// Token: 0x02000040 RID: 64
	internal static class Column
	{
		// Token: 0x06000298 RID: 664 RVA: 0x000233AC File Offset: 0x000215AC
		public static int GetColumn(int Count, int PositionX)
		{
			int num = Convert.ToInt32(Math.Floor((double)(512f / (float)Count)));
			if (num == 0)
			{
				return 0;
			}
			return Convert.ToInt32(Math.Floor((double)(Convert.ToSingle(PositionX) / Convert.ToSingle(num))));
		}
	}
}
