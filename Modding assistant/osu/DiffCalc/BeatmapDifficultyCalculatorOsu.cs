using System;
using System.Collections.Generic;
using osu.Beatmap;

namespace osu.DiffCalc
{
	// Token: 0x0200004A RID: 74
	public class BeatmapDifficultyCalculatorOsu
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x000257A8 File Offset: 0x000239A8
		public BeatmapDifficultyCalculatorOsu(Difficulty bd)
		{
			this.bd = bd;
			this.ComputeDifficulty();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000257BE File Offset: 0x000239BE
		public double AdjustDifficulty(double difficulty)
		{
			return (BeatmapDifficultyCalculatorOsu.ApplyModsToDifficulty(difficulty, 1.3) - 5.0) / 5.0;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00024FBF File Offset: 0x000231BF
		public static double ApplyModsToDifficulty(double difficulty, double hardRockFactor)
		{
			return difficulty;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000257E4 File Offset: 0x000239E4
		protected double ComputeDifficulty()
		{
			this.DifficultyHitObjects = new List<DifficultyHitObjectOsu>(this.bd.Objects.Count);
			float circleRadius = 32f * (1f - 0.7f * (float)this.AdjustDifficulty((double)this.bd.CircleSize));
			foreach (HitObject baseHitObject in this.bd.Objects)
			{
				this.DifficultyHitObjects.Add(new DifficultyHitObjectOsu(baseHitObject, circleRadius));
			}
			this.DifficultyHitObjects.Sort((DifficultyHitObjectOsu a, DifficultyHitObjectOsu b) => a.BaseHitObject.StartTime - b.BaseHitObject.StartTime);
			if (!this.CalculateStrainValues())
			{
				return 0.0;
			}
			double d = this.CalculateDifficulty(BeatmapDifficultyCalculatorOsu.DifficultyType.Speed);
			double d2 = this.CalculateDifficulty(BeatmapDifficultyCalculatorOsu.DifficultyType.Aim);
			double num = Math.Sqrt(d) * 0.0675;
			double num2 = Math.Sqrt(d2) * 0.0675;
			double num3 = num + num2 + Math.Abs(num - num2) * 0.5;
			this.bd.strains.AimSR = Convert.ToSingle(Math.Round(num2, 2));
			this.bd.strains.SpeedSR = Convert.ToSingle(Math.Round(num, 2));
			this.bd.strains.TotalSR = Convert.ToSingle(Math.Round(num3, 2));
			return num3;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00025968 File Offset: 0x00023B68
		protected bool CalculateStrainValues()
		{
			List<DifficultyHitObjectOsu>.Enumerator enumerator = this.DifficultyHitObjects.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return false;
			}
			DifficultyHitObjectOsu previousHitObject = enumerator.Current;
			while (enumerator.MoveNext())
			{
				DifficultyHitObjectOsu difficultyHitObjectOsu = enumerator.Current;
				difficultyHitObjectOsu.CalculateStrains(previousHitObject, 1.0);
				previousHitObject = difficultyHitObjectOsu;
			}
			return true;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000259B8 File Offset: 0x00023BB8
		protected double CalculateDifficulty(BeatmapDifficultyCalculatorOsu.DifficultyType Type)
		{
			double num = 400.0;
			List<double> list = new List<double>();
			double num2 = num;
			double num3 = 0.0;
			DifficultyHitObjectOsu difficultyHitObjectOsu = null;
			foreach (DifficultyHitObjectOsu difficultyHitObjectOsu2 in this.DifficultyHitObjects)
			{
				while ((double)difficultyHitObjectOsu2.BaseHitObject.StartTime > num2)
				{
					list.Add(num3);
					if (difficultyHitObjectOsu == null)
					{
						num3 = 0.0;
					}
					else
					{
						double num4 = Math.Pow(DifficultyHitObjectOsu.DECAY_BASE[(int)Type], (num2 - (double)difficultyHitObjectOsu.BaseHitObject.StartTime) / 1000.0);
						num3 = difficultyHitObjectOsu.Strains[(int)Type] * num4;
					}
					num2 += num;
				}
				if (difficultyHitObjectOsu2.Strains[(int)Type] > num3)
				{
					num3 = difficultyHitObjectOsu2.Strains[(int)Type];
				}
				difficultyHitObjectOsu = difficultyHitObjectOsu2;
			}
			double num5 = 0.0;
			double num6 = 1.0;
			for (int i = 0; i < list.Count - 1; i++)
			{
				if (Type == BeatmapDifficultyCalculatorOsu.DifficultyType.Speed)
				{
					this.bd.strains.StrainsSpeed.Add(Convert.ToSingle(list[i]));
				}
				else
				{
					this.bd.strains.StrainsAim.Add(Convert.ToSingle(list[i]));
				}
			}
			list.Sort((double a, double b) => b.CompareTo(a));
			foreach (double num7 in list)
			{
				num5 += num6 * num7;
				num6 *= 0.9;
			}
			return num5;
		}

		// Token: 0x04000248 RID: 584
		private Difficulty bd;

		// Token: 0x04000249 RID: 585
		private List<DifficultyHitObjectOsu> DifficultyHitObjects;

		// Token: 0x0400024A RID: 586
		private const double STAR_SCALING_FACTOR = 0.0675;

		// Token: 0x0400024B RID: 587
		private const double EXTREME_SCALING_FACTOR = 0.5;

		// Token: 0x0400024C RID: 588
		private const float PLAYFIELD_WIDTH = 512f;

		// Token: 0x0400024D RID: 589
		protected const double STRAIN_STEP = 400.0;

		// Token: 0x0400024E RID: 590
		protected const double DECAY_WEIGHT = 0.9;

		// Token: 0x02000095 RID: 149
		public enum DifficultyType
		{
			// Token: 0x04000321 RID: 801
			Speed,
			// Token: 0x04000322 RID: 802
			Aim
		}
	}
}
