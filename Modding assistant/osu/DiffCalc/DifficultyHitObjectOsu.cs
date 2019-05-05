using System;
using Microsoft.Xna.Framework;
using osu.Beatmap;

namespace osu.DiffCalc
{
	// Token: 0x0200004E RID: 78
	internal class DifficultyHitObjectOsu
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x000263FC File Offset: 0x000245FC
		internal DifficultyHitObjectOsu(HitObject BaseHitObject, float CircleRadius)
		{
			this.BaseHitObject = BaseHitObject;
			BaseHitObject.IsType(HitObjectType.Slider);
			float num = 52f / CircleRadius;
			if (CircleRadius < 30f)
			{
				float num2 = Math.Min(30f - CircleRadius, 5f) / 50f;
				num *= 1f + num2;
			}
			this.NormalizedStartPosition = BaseHitObject.Position * num;
			if (BaseHitObject.IsType(HitObjectType.Slider))
			{
				float num3 = CircleRadius * 3f;
				int num4 = Math.Min(BaseHitObject.TotalLength / BaseHitObject.SegmentCount, 60000);
				int num5 = BaseHitObject.StartTime + num4;
				Vector2 vector = BaseHitObject.Position;
				for (int i = BaseHitObject.StartTime + 10; i < num5; i += 10)
				{
					Vector2 value = BaseHitObject.curve.PositionAtTime(i) - vector;
					float num6 = value.Length();
					if (num6 > num3)
					{
						value.Normalize();
						num6 -= num3;
						vector += value * num6;
						this.LazySliderLengthFirst += num6;
					}
				}
				this.LazySliderLengthFirst *= num;
				if (BaseHitObject.SegmentCount % 2 == 1)
				{
					this.NormalizedEndPosition = vector * num;
				}
				if (BaseHitObject.SegmentCount > 1)
				{
					num5 += num4;
					for (int j = num5 - num4 + 10; j < num5; j += 10)
					{
						Vector2 value2 = BaseHitObject.curve.PositionAtTime(j) - vector;
						float num7 = value2.Length();
						if (num7 > num3)
						{
							value2.Normalize();
							num7 -= num3;
							vector += value2 * num7;
							this.LazySliderLengthSubsequent += num7;
						}
					}
					this.LazySliderLengthSubsequent *= num;
					if (BaseHitObject.SegmentCount % 2 == 0)
					{
						this.NormalizedEndPosition = vector * num;
						return;
					}
				}
			}
			else
			{
				this.NormalizedEndPosition = this.NormalizedStartPosition;
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00026606 File Offset: 0x00024806
		internal void CalculateStrains(DifficultyHitObjectOsu PreviousHitObject, double TimeRate)
		{
			this.CalculateSpecificStrain(PreviousHitObject, BeatmapDifficultyCalculatorOsu.DifficultyType.Speed, TimeRate);
			this.CalculateSpecificStrain(PreviousHitObject, BeatmapDifficultyCalculatorOsu.DifficultyType.Aim, TimeRate);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0002661C File Offset: 0x0002481C
		private static double SpacingWeight(double distance, BeatmapDifficultyCalculatorOsu.DifficultyType Type)
		{
			if (Type == BeatmapDifficultyCalculatorOsu.DifficultyType.Speed)
			{
				double result;
				if (distance > 125.0)
				{
					result = 2.5;
				}
				else if (distance > 110.0)
				{
					result = 1.6 + 0.9 * (distance - 110.0) / 15.0;
				}
				else if (distance > 90.0)
				{
					result = 1.2 + 0.4 * (distance - 90.0) / 20.0;
				}
				else if (distance > 45.0)
				{
					result = 0.95 + 0.25 * (distance - 45.0) / 45.0;
				}
				else
				{
					result = 0.95;
				}
				return result;
			}
			if (Type != BeatmapDifficultyCalculatorOsu.DifficultyType.Aim)
			{
				return 0.0;
			}
			return Math.Pow(distance, 0.99);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00026720 File Offset: 0x00024920
		private void CalculateSpecificStrain(DifficultyHitObjectOsu PreviousHitObject, BeatmapDifficultyCalculatorOsu.DifficultyType Type, double TimeRate)
		{
			double num = 0.0;
			double num2 = (double)(this.BaseHitObject.StartTime - PreviousHitObject.BaseHitObject.StartTime) / TimeRate;
			double num3 = Math.Pow(DifficultyHitObjectOsu.DECAY_BASE[(int)Type], num2 / 1000.0);
			if (!this.BaseHitObject.IsType(HitObjectType.Spinner))
			{
				if (this.BaseHitObject.IsType(HitObjectType.Slider))
				{
					if (Type != BeatmapDifficultyCalculatorOsu.DifficultyType.Speed)
					{
						if (Type == BeatmapDifficultyCalculatorOsu.DifficultyType.Aim)
						{
							num = (DifficultyHitObjectOsu.SpacingWeight((double)PreviousHitObject.LazySliderLengthFirst, Type) + DifficultyHitObjectOsu.SpacingWeight((double)PreviousHitObject.LazySliderLengthSubsequent, Type) * (double)(PreviousHitObject.BaseHitObject.SegmentCount - 1) + DifficultyHitObjectOsu.SpacingWeight(this.DistanceTo(PreviousHitObject), Type)) * DifficultyHitObjectOsu.SPACING_WEIGHT_SCALING[(int)Type];
						}
					}
					else
					{
						num = DifficultyHitObjectOsu.SpacingWeight((double)(PreviousHitObject.LazySliderLengthFirst + PreviousHitObject.LazySliderLengthSubsequent * (float)(PreviousHitObject.BaseHitObject.SegmentCount - 1)) + this.DistanceTo(PreviousHitObject), Type) * DifficultyHitObjectOsu.SPACING_WEIGHT_SCALING[(int)Type];
					}
				}
				else if (this.BaseHitObject.IsType(HitObjectType.Normal))
				{
					num = DifficultyHitObjectOsu.SpacingWeight(this.DistanceTo(PreviousHitObject), Type) * DifficultyHitObjectOsu.SPACING_WEIGHT_SCALING[(int)Type];
				}
			}
			num /= Math.Max(num2, 50.0);
			this.Strains[(int)Type] = PreviousHitObject.Strains[(int)Type] * num3 + num;
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0002685C File Offset: 0x00024A5C
		internal double DistanceTo(DifficultyHitObjectOsu other)
		{
			return (double)(this.NormalizedStartPosition - other.NormalizedEndPosition).Length();
		}

		// Token: 0x04000264 RID: 612
		internal static readonly double[] DECAY_BASE = new double[]
		{
			0.3,
			0.15
		};

		// Token: 0x04000265 RID: 613
		private const double ALMOST_DIAMETER = 90.0;

		// Token: 0x04000266 RID: 614
		private const double STREAM_SPACING_TRESHOLD = 110.0;

		// Token: 0x04000267 RID: 615
		private const double SINGLE_SPACING_TRESHOLD = 125.0;

		// Token: 0x04000268 RID: 616
		private static readonly double[] SPACING_WEIGHT_SCALING = new double[]
		{
			1400.0,
			26.25
		};

		// Token: 0x04000269 RID: 617
		private const int LAZY_SLIDER_STEP_LENGTH = 10;

		// Token: 0x0400026A RID: 618
		internal HitObject BaseHitObject;

		// Token: 0x0400026B RID: 619
		internal double[] Strains = new double[]
		{
			1.0,
			1.0
		};

		// Token: 0x0400026C RID: 620
		private Vector2 NormalizedStartPosition;

		// Token: 0x0400026D RID: 621
		private Vector2 NormalizedEndPosition;

		// Token: 0x0400026E RID: 622
		private float LazySliderLengthFirst;

		// Token: 0x0400026F RID: 623
		private float LazySliderLengthSubsequent;
	}
}
