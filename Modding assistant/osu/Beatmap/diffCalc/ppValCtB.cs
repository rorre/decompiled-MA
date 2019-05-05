using System;

namespace osu.Beatmap.diffCalc
{
	// Token: 0x02000045 RID: 69
	internal static class ppValCtB
	{
		// Token: 0x060002B4 RID: 692 RVA: 0x000248F4 File Offset: 0x00022AF4
		private static float ComputeStrainValue(float strain, int TotalHits)
		{
			float num = (float)Math.Pow((double)(5f * Math.Max(1f, strain / 0.0825f) - 4f), 3.0) / 110000f;
			float num2 = 1f + 0.1f * Math.Min(1f, Convert.ToSingle(TotalHits) / 1500f);
			return num * num2 * 1f;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00024960 File Offset: 0x00022B60
		private static float ComputeAccValue(float OD, int TotalHits, float hitWindow300, Difficulty bd)
		{
			return (float)Math.Pow((double)(150f / hitWindow300) * Math.Pow((double)ppValCtB.Accuracy(bd), 16.0), 1.7999999523162842) * 2.5f * Math.Min(1.15f, (float)Math.Pow((double)(Convert.ToSingle(TotalHits) / 1500f), 0.30000001192092896));
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x000249C7 File Offset: 0x00022BC7
		private static float VariablesCalcu(double val)
		{
			return (float)((int)val);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x000249CC File Offset: 0x00022BCC
		private static float Accuracy(Difficulty bd)
		{
			return 1f;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00007F23 File Offset: 0x00006123
		private static float Clamp(float value, float min, float max)
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

		// Token: 0x060002B9 RID: 697 RVA: 0x000249D3 File Offset: 0x00022BD3
		private static int TotalHits(Difficulty bd)
		{
			return bd.FruitsHitCount;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x000249DC File Offset: 0x00022BDC
		public static float Calculate(Difficulty bd)
		{
			float result;
			try
			{
				double num = (double)((float)Math.Pow((double)(5f * Math.Max(1f, bd.strains.TotalSR / 0.0049f) - 4f), 2.0) / 100000f);
				int num2 = ppValCtB.TotalHits(bd);
				float num3 = (float)((double)(0.95f + 0.4f * Math.Min(1f, (float)num2 / 3000f)) + ((num2 > 3000) ? (Math.Log10((double)((float)num2 / 3000f)) * 0.5) : 0.0));
				double num4 = num * (double)num3;
				float approachRate = bd.ApproachRate;
				float num5 = 1f;
				if (approachRate > 9f)
				{
					num5 += 0.1f * (approachRate - 9f);
				}
				else if (approachRate < 8f)
				{
					num5 += 0.025f * (8f - approachRate);
				}
				result = (float)Math.Round(num4 * (double)num5 * (double)((float)Math.Pow((double)ppValCtB.Accuracy(bd), 5.5)), 2);
			}
			catch
			{
				result = 0f;
			}
			return result;
		}
	}
}
