using System;

namespace osu.Beatmap.diffCalc
{
	// Token: 0x02000046 RID: 70
	internal static class ppValMania
	{
		// Token: 0x060002BB RID: 699 RVA: 0x00024AFC File Offset: 0x00022CFC
		private static float ComputeStrainValue(float strain, int TotalHits)
		{
			float num = (float)Math.Pow((double)(5f * Math.Max(1f, strain / 0.0825f) - 4f), 3.0) / 110000f;
			float num2 = 1f + 0.1f * Math.Min(1f, Convert.ToSingle(TotalHits) / 1500f);
			return num * num2 * 1f;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00024B68 File Offset: 0x00022D68
		private static float ComputeAccValue(float OD, int TotalHits, float hitWindow300, Difficulty bd)
		{
			return (float)Math.Pow((double)(150f / hitWindow300) * Math.Pow((double)ppValMania.Accuracy(bd), 16.0), 1.7999999523162842) * 2.5f * Math.Min(1.15f, (float)Math.Pow((double)(Convert.ToSingle(TotalHits) / 1500f), 0.30000001192092896));
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000249C7 File Offset: 0x00022BC7
		private static float VariablesCalcu(double val)
		{
			return (float)((int)val);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00024BCF File Offset: 0x00022DCF
		private static float Accuracy(Difficulty bd)
		{
			return ppValMania.Clamp((float)ppValMania.TotalHits(bd) * 300f / ((float)ppValMania.TotalHits(bd) * 300f), 0f, 1f);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00007F23 File Offset: 0x00006123
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

		// Token: 0x060002C0 RID: 704 RVA: 0x00024BFC File Offset: 0x00022DFC
		private static int TotalHits(Difficulty bd)
		{
			int num = 0;
			for (int i = 0; i < bd.Objects.Count; i++)
			{
				if (bd.Objects[i].IsType(HitObjectType.Normal))
				{
					num += 2;
				}
				if (bd.Objects[i].IsType(HitObjectType.Slider))
				{
					num += 4;
				}
			}
			return num;
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00024C54 File Offset: 0x00022E54
		public static float Calculate(Difficulty bd)
		{
			float result;
			try
			{
				float hitWindow = ppValMania.VariablesCalcu((double)(34f + 3f * bd.OverallDifficulty));
				float num = 1.1f;
				result = (float)Math.Round((double)Convert.ToSingle(Math.Pow(Math.Pow((double)ppValMania.ComputeStrainValue(bd.strains.TotalSR, bd.Objects.Count), 1.1000000238418579) + Math.Pow((double)ppValMania.ComputeAccValue(bd.OverallDifficulty, ppValMania.TotalHits(bd), hitWindow, bd), 1.1000000238418579), 0.90909087657928467) * (double)num), 2);
			}
			catch
			{
				result = 0f;
			}
			return result;
		}
	}
}
