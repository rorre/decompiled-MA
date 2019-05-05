using System;
using System.Collections.Generic;
using osu.Beatmap;

namespace osu.DiffCalc
{
	// Token: 0x0200004B RID: 75
	public class BeatmapDifficultyCalculatorTaiko
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00025B94 File Offset: 0x00023D94
		public BeatmapDifficultyCalculatorTaiko(Difficulty bd)
		{
			this.bd = bd;
			this.ComputeDifficulty();
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00025BAC File Offset: 0x00023DAC
		protected double ComputeDifficulty()
		{
			this.DifficultyHitObjects = new List<DifficultyHitObjectTaiko>(this.bd.Objects.Count);
			foreach (HitObject baseHitObject in this.bd.Objects)
			{
				this.DifficultyHitObjects.Add(new DifficultyHitObjectTaiko(baseHitObject));
			}
			this.DifficultyHitObjects.Sort((DifficultyHitObjectTaiko a, DifficultyHitObjectTaiko b) => a.BaseHitObject.StartTime - b.BaseHitObject.StartTime);
			if (!this.CalculateStrainValues())
			{
				return 0.0;
			}
			double num = this.CalculateDifficulty() * 0.04125;
			this.bd.strains.TotalSR = Convert.ToSingle(Math.Round(num, 2));
			return num;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00025C94 File Offset: 0x00023E94
		protected bool CalculateStrainValues()
		{
			List<DifficultyHitObjectTaiko>.Enumerator enumerator = this.DifficultyHitObjects.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return false;
			}
			DifficultyHitObjectTaiko previousHitObject = enumerator.Current;
			while (enumerator.MoveNext())
			{
				DifficultyHitObjectTaiko difficultyHitObjectTaiko = enumerator.Current;
				difficultyHitObjectTaiko.CalculateStrains(previousHitObject, 1.0);
				previousHitObject = difficultyHitObjectTaiko;
			}
			return true;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00025CE4 File Offset: 0x00023EE4
		protected double CalculateDifficulty()
		{
			double num = 400.0;
			List<double> list = new List<double>();
			double num2 = num;
			double num3 = 0.0;
			DifficultyHitObjectTaiko difficultyHitObjectTaiko = null;
			foreach (DifficultyHitObjectTaiko difficultyHitObjectTaiko2 in this.DifficultyHitObjects)
			{
				while ((double)difficultyHitObjectTaiko2.BaseHitObject.StartTime > num2)
				{
					list.Add(num3);
					if (difficultyHitObjectTaiko == null)
					{
						num3 = 0.0;
					}
					else
					{
						double num4 = Math.Pow(DifficultyHitObjectTaiko.DECAY_BASE, (num2 - (double)difficultyHitObjectTaiko.BaseHitObject.StartTime) / 1000.0);
						num3 = difficultyHitObjectTaiko.Strain * num4;
					}
					num2 += num;
				}
				if (difficultyHitObjectTaiko2.Strain > num3)
				{
					num3 = difficultyHitObjectTaiko2.Strain;
				}
				difficultyHitObjectTaiko = difficultyHitObjectTaiko2;
			}
			double num5 = 0.0;
			double num6 = 1.0;
			for (int i = 0; i < list.Count - 1; i++)
			{
				this.bd.strains.StrainsSpeed.Add(Convert.ToSingle(list[i]));
			}
			list.Sort((double a, double b) => b.CompareTo(a));
			foreach (double num7 in list)
			{
				num5 += num6 * num7;
				num6 *= 0.9;
			}
			return num5;
		}

		// Token: 0x0400024F RID: 591
		private Difficulty bd;

		// Token: 0x04000250 RID: 592
		private const double STAR_SCALING_FACTOR = 0.04125;

		// Token: 0x04000251 RID: 593
		private List<DifficultyHitObjectTaiko> DifficultyHitObjects;

		// Token: 0x04000252 RID: 594
		protected const double STRAIN_STEP = 400.0;

		// Token: 0x04000253 RID: 595
		protected const double DECAY_WEIGHT = 0.9;
	}
}
