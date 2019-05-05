using System;

namespace osu.Beatmap.Fruits
{
	// Token: 0x02000042 RID: 66
	public class FruitObjectManager
	{
		// Token: 0x0600029E RID: 670 RVA: 0x00023874 File Offset: 0x00021A74
		public static void InitializeHyperDash(float catcherWidth, Difficulty bd)
		{
			int num = 0;
			float num2 = catcherWidth / 2f;
			float num3 = num2;
			for (int i = 0; i < bd.Objects.Count - 1; i++)
			{
				HitObject hitObject = bd.Objects[i];
				if (!hitObject.IsType(HitObjectType.Spinner))
				{
					HitObject hitObject2 = bd.Objects[i + 1];
					while (hitObject2.IsType(HitObjectType.Spinner) && ++i != bd.Objects.Count - 1)
					{
						hitObject2 = bd.Objects[i + 1];
					}
					int num4 = (hitObject2.Position.X > hitObject.Position.X) ? 1 : -1;
					float num5 = (float)(hitObject2.StartTime - hitObject.EndTime) - 4.16666651f;
					float num6 = Math.Abs(hitObject2.Position.X - hitObject.Position.X) - ((num == num4) ? num3 : num2);
					if (num5 < num6)
					{
						hitObject.Fruit.MakeHyperDash(hitObject2);
						num3 = num2;
					}
					else
					{
						hitObject.Fruit.DistanceToHyperDash = num5 - num6;
						num3 = FruitObjectManager.Clamp(num5 - num6, 0f, num2);
					}
					num = num4;
				}
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00007F23 File Offset: 0x00006123
		internal static float Clamp(float value, float min, float max)
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

		// Token: 0x04000230 RID: 560
		internal const double SIXTY_FRAME_TIME = 16.666666666666668;
	}
}
