using System;

namespace osu.Beatmap.diffCalc
{
	// Token: 0x02000044 RID: 68
	internal static class ppVal
	{
		// Token: 0x060002B2 RID: 690 RVA: 0x000246D4 File Offset: 0x000228D4
		private static float base_strain(float strain)
		{
			return (float)Math.Pow((double)(5f * Math.Max(1f, strain / 0.0675f) - 4f), 3.0) / 100000f;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0002470C File Offset: 0x0002290C
		public static float Calculate(float AimStars, float SpeedStars, Difficulty bd)
		{
			float overallDifficulty = bd.OverallDifficulty;
			float approachRate = bd.ApproachRate;
			int num = 0;
			for (int i = 0; i < bd.Objects.Count; i++)
			{
				if (bd.Objects[i].IsType(HitObjectType.Normal))
				{
					num++;
				}
			}
			int count = bd.Objects.Count;
			double num2 = (double)ppVal.base_strain(bd.strains.AimSR);
			float num3 = (float)count / 2000f;
			float num4 = 0.95f + 0.4f * Math.Min(1f, num3) + (((float)count > 2000f) ? ((float)Math.Log10((double)num3) * 0.5f) : 0f);
			double num5 = num2 * (double)num4;
			float num6 = 1f;
			if (approachRate < 8f)
			{
				float num7 = 0.01f * (8f - approachRate);
				num6 += num7;
			}
			double num8 = num5 * (double)num6;
			float num9 = 1f;
			float num10 = 0.98f + (float)Math.Pow((double)overallDifficulty, 2.0) / 2500f;
			double num11 = num8 * (double)num9 * (double)num10;
			float num12 = ppVal.base_strain(bd.strains.SpeedSR);
			num12 *= num4;
			num12 *= num9;
			num12 *= num10;
			float num13 = (float)Math.Pow(1.5216300487518311, (double)overallDifficulty) * (float)Math.Pow(1.0, 24.0) * 2.83f;
			num13 *= Math.Min(1.15f, (float)Math.Pow((double)((float)num / 1000f), 0.30000001192092896));
			float num14 = 1.12f;
			return (float)Math.Round((double)((float)Math.Pow(Math.Pow(num11, 1.1000000238418579) + Math.Pow((double)num12, 1.1000000238418579) + Math.Pow((double)num13, 1.1000000238418579), 0.90909087657928467) * num14 * 100f)) / 100f;
		}
	}
}
