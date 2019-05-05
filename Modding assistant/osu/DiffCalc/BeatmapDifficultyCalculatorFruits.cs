using System;
using System.Collections.Generic;
using osu.Beatmap;
using osu.Beatmap.Fruits;

namespace osu.DiffCalc
{
	// Token: 0x02000048 RID: 72
	internal class BeatmapDifficultyCalculatorFruits
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x00024F84 File Offset: 0x00023184
		public BeatmapDifficultyCalculatorFruits(Difficulty bd)
		{
			this.bd = bd;
			this.ComputeDifficulty();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00024F9A File Offset: 0x0002319A
		private double AdjustDifficulty(double difficulty)
		{
			return (BeatmapDifficultyCalculatorFruits.ApplyModsToDifficulty(difficulty, 1.3) - 5.0) / 5.0;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00024FBF File Offset: 0x000231BF
		private static double ApplyModsToDifficulty(double difficulty, double hardRockFactor)
		{
			return difficulty;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00024FC4 File Offset: 0x000231C4
		protected double ComputeDifficulty()
		{
			List<HitObject> list = new List<HitObject>();
			List<HitObject> objects = this.bd.Objects;
			foreach (HitObject hitObject in this.bd.Objects)
			{
				if (!hitObject.IsType(HitObjectType.Spinner))
				{
					list.Add(hitObject);
					if (hitObject.IsType(HitObjectType.Slider))
					{
						List<HitObject> list2 = new List<HitObject>(hitObject.Fruit.GetBonusObjects());
						for (int i = 0; i < list2.Count; i++)
						{
							list.Add(list2[i]);
						}
					}
				}
			}
			this.DifficultyHitObjects = new List<DifficultyHitObjectFruits>(list.Count);
			float num = (float)(64.0 * (1.0 - 0.699999988079071 * this.AdjustDifficulty((double)this.bd.CircleSize))) / 128f;
			float num2 = 305f * num * 0.7f;
			float num3 = num2 / 2f;
			this.bd.Objects = list;
			this.bd.FruitsHitCount = this.bd.Objects.Count;
			FruitObjectManager.InitializeHyperDash(num2, this.bd);
			num3 *= 0.8f;
			foreach (HitObject baseHitObject in this.bd.Objects)
			{
				this.DifficultyHitObjects.Add(new DifficultyHitObjectFruits(baseHitObject, num3));
			}
			this.DifficultyHitObjects.Sort((DifficultyHitObjectFruits a, DifficultyHitObjectFruits b) => a.BaseHitObject.StartTime - b.BaseHitObject.StartTime);
			if (!this.CalculateStrainValues())
			{
				return 0.0;
			}
			double num4 = Math.Sqrt(this.CalculateDifficulty()) * 0.145;
			this.bd.strains.TotalSR = Convert.ToSingle(Math.Round(num4, 2));
			for (int j = 0; j < this.bd.Objects.Count; j++)
			{
				if (this.bd.Objects[j].Fruit.DistanceToHyperDash == 0f && this.bd.Objects[j].Fruit.HyperDash)
				{
					this.bd.HyperdashesList.Add(this.bd.Objects[j].StartTime);
				}
			}
			this.bd.Objects = objects;
			return num4;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00025274 File Offset: 0x00023474
		protected bool CalculateStrainValues()
		{
			List<DifficultyHitObjectFruits>.Enumerator enumerator = this.DifficultyHitObjects.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return false;
			}
			DifficultyHitObjectFruits previousHitObject = enumerator.Current;
			while (enumerator.MoveNext())
			{
				DifficultyHitObjectFruits difficultyHitObjectFruits = enumerator.Current;
				difficultyHitObjectFruits.CalculateStrains(previousHitObject, 1.0);
				previousHitObject = difficultyHitObjectFruits;
			}
			return true;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x000252C4 File Offset: 0x000234C4
		protected double CalculateDifficulty()
		{
			double num = 750.0;
			List<double> list = new List<double>();
			double num2 = num;
			double num3 = 0.0;
			DifficultyHitObjectFruits difficultyHitObjectFruits = null;
			foreach (DifficultyHitObjectFruits difficultyHitObjectFruits2 in this.DifficultyHitObjects)
			{
				while ((double)difficultyHitObjectFruits2.BaseHitObject.StartTime > num2)
				{
					list.Add(num3);
					if (difficultyHitObjectFruits == null)
					{
						num3 = 0.0;
					}
					else
					{
						double num4 = Math.Pow(DifficultyHitObjectFruits.DECAY_BASE, (num2 - (double)difficultyHitObjectFruits.BaseHitObject.StartTime) / 1000.0);
						num3 = difficultyHitObjectFruits.Strain * num4;
					}
					num2 += num;
				}
				if (difficultyHitObjectFruits2.Strain > num3)
				{
					num3 = difficultyHitObjectFruits2.Strain;
				}
				difficultyHitObjectFruits = difficultyHitObjectFruits2;
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
				num6 *= 0.94;
			}
			return num5;
		}

		// Token: 0x0400023D RID: 573
		private Difficulty bd;

		// Token: 0x0400023E RID: 574
		private List<DifficultyHitObjectFruits> DifficultyHitObjects;

		// Token: 0x0400023F RID: 575
		private const double STAR_SCALING_FACTOR = 0.145;

		// Token: 0x04000240 RID: 576
		private const float PLAYFIELD_WIDTH = 512f;

		// Token: 0x04000241 RID: 577
		protected const double STRAIN_STEP = 750.0;

		// Token: 0x04000242 RID: 578
		protected const double DECAY_WEIGHT = 0.94;
	}
}
