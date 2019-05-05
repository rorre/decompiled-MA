using System;

namespace osu.Beatmap.diffCalc
{
	// Token: 0x02000047 RID: 71
	internal static class ppValTaiko
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x00024D0C File Offset: 0x00022F0C
		private static float ComputeStrainValue(float strain, int TotalHits)
		{
			float num = (float)Math.Pow((double)(5f * Math.Max(1f, strain / 0.0075f) - 4f), 2.0) / 100000f;
			float num2 = 1f + 0.1f * Math.Min(1f, Convert.ToSingle(TotalHits) / 1500f);
			return num * num2 * (float)Math.Pow(0.98500001430511475, 0.0) * (float)Math.Min(Math.Pow((double)TotalHits, 0.5) / Math.Pow((double)TotalHits, 0.5), 1.0) * Math.Min((float)(Math.Pow((double)TotalHits, 0.5) / Math.Pow((double)TotalHits, 0.5)), 1f) * 1f;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00024DF0 File Offset: 0x00022FF0
		private static float ComputeAccValue(float OD, int TotalHits, float hitWindow300)
		{
			return (float)Math.Pow((double)(150f / hitWindow300), 1.1000000238418579) * (float)Math.Pow(1.0, 15.0) * 22f * Math.Min(1.15f, (float)Math.Pow((double)(Convert.ToSingle(TotalHits) / 1500f), 0.30000001192092896));
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00024E5C File Offset: 0x0002305C
		private static double MapDifficultyRange(double difficulty, double min, double mid, double max)
		{
			if (difficulty > 5.0)
			{
				return mid + (max - mid) * (difficulty - 5.0) / 5.0;
			}
			if (difficulty < 5.0)
			{
				return mid - (mid - min) * (5.0 - difficulty) / 5.0;
			}
			return mid;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00024EBC File Offset: 0x000230BC
		public static float Calculate(Difficulty bd)
		{
			float result;
			try
			{
				float hitWindow = (float)ppValTaiko.MapDifficultyRange((double)bd.OverallDifficulty, 50.0, 35.0, 20.0);
				float num = 1.1f;
				result = (float)Math.Round((double)Convert.ToSingle(Math.Pow(Math.Pow((double)ppValTaiko.ComputeStrainValue(bd.strains.TotalSR, bd.Objects.Count), 1.1000000238418579) + Math.Pow((double)ppValTaiko.ComputeAccValue(bd.OverallDifficulty, bd.Objects.Count, hitWindow), 1.1000000238418579), 0.90909087657928467) * (double)num), 2);
			}
			catch
			{
				result = 0f;
			}
			return result;
		}
	}
}
