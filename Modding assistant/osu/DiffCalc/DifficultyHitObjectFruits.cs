using System;
using osu.Beatmap;

namespace osu.DiffCalc
{
	// Token: 0x0200004C RID: 76
	internal class DifficultyHitObjectFruits
	{
		// Token: 0x060002DA RID: 730 RVA: 0x00007F23 File Offset: 0x00006123
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

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00025E90 File Offset: 0x00024090
		private float ActualNormalizedPosition
		{
			get
			{
				return this.NormalizedPosition + this.PlayerPositionOffset;
			}
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00025EA0 File Offset: 0x000240A0
		internal DifficultyHitObjectFruits(HitObject BaseHitObject, float catcherWidthHalf)
		{
			this.BaseHitObject = BaseHitObject;
			float num = 41f / catcherWidthHalf;
			this.playerPositioningError = 16f;
			this.NormalizedPosition = BaseHitObject.Position.X * num;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00025F00 File Offset: 0x00024100
		internal void CalculateStrains(DifficultyHitObjectFruits PreviousHitObject, double TimeRate)
		{
			double num = (double)(this.BaseHitObject.StartTime - PreviousHitObject.BaseHitObject.StartTime) / TimeRate;
			double num2 = Math.Pow(DifficultyHitObjectFruits.DECAY_BASE, num / 1000.0);
			this.PlayerPositionOffset = DifficultyHitObjectFruits.Clamp(PreviousHitObject.ActualNormalizedPosition, this.NormalizedPosition - (41f - this.playerPositioningError), this.NormalizedPosition + (41f - this.playerPositioningError)) - this.NormalizedPosition;
			this.LastMovement = this.DistanceTo(PreviousHitObject);
			double num3 = DifficultyHitObjectFruits.SpacingWeight(this.LastMovement);
			if (this.NormalizedPosition < PreviousHitObject.NormalizedPosition)
			{
				this.LastMovement = -this.LastMovement;
			}
			HitObject baseHitObject = PreviousHitObject.BaseHitObject;
			double num4 = 0.0;
			double num5 = Math.Sqrt(Math.Max(num, 25.0));
			if ((double)Math.Abs(this.LastMovement) > 0.1)
			{
				if ((double)Math.Abs(PreviousHitObject.LastMovement) > 0.1 && Math.Sign(this.LastMovement) != Math.Sign(PreviousHitObject.LastMovement))
				{
					double num6 = this.DIRECTION_CHANGE_BONUS / num5;
					double num7 = (double)(Math.Min(this.playerPositioningError, Math.Abs(this.LastMovement)) / this.playerPositioningError);
					num3 += num6 * num7;
					if (baseHitObject != null && baseHitObject.Fruit.DistanceToHyperDash <= 10f)
					{
						num4 += 0.3 * num7;
					}
				}
				num3 += 7.5 * (double)Math.Min(Math.Abs(this.LastMovement), 82f) / 246.0 / num5;
			}
			if (baseHitObject != null && baseHitObject.Fruit.DistanceToHyperDash <= 10f)
			{
				if (!baseHitObject.Fruit.HyperDash)
				{
					num4 += 1.0;
				}
				else
				{
					this.PlayerPositionOffset = 0f;
				}
				num3 *= 1.0 + num4 * (double)((10f - baseHitObject.Fruit.DistanceToHyperDash) / 10f);
			}
			num3 *= 850.0 / Math.Max(num, 25.0);
			this.Strain = PreviousHitObject.Strain * num2 + num3;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0002613B File Offset: 0x0002433B
		private static double SpacingWeight(float distance)
		{
			return Math.Pow((double)distance, 1.3) / 500.0;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00026157 File Offset: 0x00024357
		internal float DistanceTo(DifficultyHitObjectFruits other)
		{
			return Math.Abs(this.ActualNormalizedPosition - other.ActualNormalizedPosition);
		}

		// Token: 0x04000254 RID: 596
		internal static readonly double DECAY_BASE = 0.2;

		// Token: 0x04000255 RID: 597
		private const float NORMALIZED_HITOBJECT_RADIUS = 41f;

		// Token: 0x04000256 RID: 598
		private const float ABSOLUTE_PLAYER_POSITIONING_ERROR = 16f;

		// Token: 0x04000257 RID: 599
		private float playerPositioningError;

		// Token: 0x04000258 RID: 600
		internal HitObject BaseHitObject;

		// Token: 0x04000259 RID: 601
		internal double Strain = 1.0;

		// Token: 0x0400025A RID: 602
		internal float PlayerPositionOffset;

		// Token: 0x0400025B RID: 603
		internal float LastMovement;

		// Token: 0x0400025C RID: 604
		private float NormalizedPosition;

		// Token: 0x0400025D RID: 605
		private readonly double DIRECTION_CHANGE_BONUS = 12.5;
	}
}
