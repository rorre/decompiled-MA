using System;
using System.Collections.Generic;
using osu.Beatmap;

namespace osu.DiffCalc
{
	// Token: 0x02000049 RID: 73
	public class BeatmapDifficultyCalculatorMania
	{
		// Token: 0x060002CC RID: 716 RVA: 0x00025470 File Offset: 0x00023670
		public BeatmapDifficultyCalculatorMania(Difficulty bd)
		{
			this.bd = bd;
			this.ComputeDifficulty();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00025488 File Offset: 0x00023688
		protected double ComputeDifficulty()
		{
			this.DifficultyHitObjects = new List<DifficultyHitObjectMania>(this.bd.Objects.Count);
			foreach (HitObject baseHitObject in this.bd.Objects)
			{
				this.DifficultyHitObjects.Add(new DifficultyHitObjectMania(baseHitObject));
			}
			this.DifficultyHitObjects.Sort((DifficultyHitObjectMania a, DifficultyHitObjectMania b) => a.BaseHitObject.StartTime - b.BaseHitObject.StartTime);
			if (!this.CalculateStrainValues())
			{
				return 0.0;
			}
			double num = this.CalculateDifficulty() * 0.018;
			this.bd.strains.TotalSR = Convert.ToSingle(Math.Round(num, 2));
			return num;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00025570 File Offset: 0x00023770
		protected bool CalculateStrainValues()
		{
			List<DifficultyHitObjectMania>.Enumerator enumerator = this.DifficultyHitObjects.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return false;
			}
			DifficultyHitObjectMania previousHitObject = enumerator.Current;
			while (enumerator.MoveNext())
			{
				DifficultyHitObjectMania difficultyHitObjectMania = enumerator.Current;
				difficultyHitObjectMania.CalculateStrains(previousHitObject, 1.0);
				previousHitObject = difficultyHitObjectMania;
			}
			return true;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000255C0 File Offset: 0x000237C0
		protected double CalculateDifficulty()
		{
			double num = 400.0;
			List<double> list = new List<double>();
			double num2 = num;
			double num3 = 0.0;
			DifficultyHitObjectMania difficultyHitObjectMania = null;
			foreach (DifficultyHitObjectMania difficultyHitObjectMania2 in this.DifficultyHitObjects)
			{
				while ((double)difficultyHitObjectMania2.BaseHitObject.StartTime > num2)
				{
					list.Add(num3);
					if (difficultyHitObjectMania == null)
					{
						num3 = 0.0;
					}
					else
					{
						double num4 = Math.Pow(DifficultyHitObjectMania.INDIVIDUAL_DECAY_BASE, (num2 - (double)difficultyHitObjectMania.BaseHitObject.StartTime) / 1000.0);
						double num5 = Math.Pow(DifficultyHitObjectMania.OVERALL_DECAY_BASE, (num2 - (double)difficultyHitObjectMania.BaseHitObject.StartTime) / 1000.0);
						num3 = difficultyHitObjectMania.IndividualStrain * num4 + difficultyHitObjectMania.OverallStrain * num5;
					}
					num2 += num;
				}
				double num6 = difficultyHitObjectMania2.IndividualStrain + difficultyHitObjectMania2.OverallStrain;
				if (num6 > num3)
				{
					num3 = num6;
				}
				difficultyHitObjectMania = difficultyHitObjectMania2;
			}
			double num7 = 0.0;
			double num8 = 1.0;
			for (int i = 0; i < list.Count - 1; i++)
			{
				this.bd.strains.StrainsSpeed.Add(Convert.ToSingle(list[i]));
			}
			list.Sort((double a, double b) => b.CompareTo(a));
			foreach (double num9 in list)
			{
				num7 += num8 * num9;
				num8 *= 0.9;
			}
			return num7;
		}

		// Token: 0x04000243 RID: 579
		private Difficulty bd;

		// Token: 0x04000244 RID: 580
		private const double STAR_SCALING_FACTOR = 0.018;

		// Token: 0x04000245 RID: 581
		private List<DifficultyHitObjectMania> DifficultyHitObjects;

		// Token: 0x04000246 RID: 582
		protected const double STRAIN_STEP = 400.0;

		// Token: 0x04000247 RID: 583
		protected const double DECAY_WEIGHT = 0.9;
	}
}
