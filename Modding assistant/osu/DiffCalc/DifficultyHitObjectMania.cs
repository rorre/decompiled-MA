using System;
using osu.Beatmap;

namespace osu.DiffCalc
{
	// Token: 0x0200004D RID: 77
	internal class DifficultyHitObjectMania
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0002617B File Offset: 0x0002437B
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0002618F File Offset: 0x0002438F
		internal double IndividualStrain
		{
			get
			{
				return this.IndividualStrains[this.BaseHitObject.Column];
			}
			set
			{
				this.IndividualStrains[this.BaseHitObject.Column] = value;
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000261A4 File Offset: 0x000243A4
		internal DifficultyHitObjectMania(HitObject BaseHitObject)
		{
			this.BaseHitObject = BaseHitObject;
			this.IndividualStrains = new double[BaseHitObject.parentBD.ColumnsCount];
			this.HeldUntil = new double[BaseHitObject.parentBD.ColumnsCount];
			for (int i = 0; i < BaseHitObject.parentBD.ColumnsCount; i++)
			{
				this.IndividualStrains[i] = 0.0;
				this.HeldUntil[i] = 0.0;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00026234 File Offset: 0x00024434
		internal void CalculateStrains(DifficultyHitObjectMania PreviousHitObject, double TimeRate)
		{
			double num = 1.0;
			double num2 = (double)(this.BaseHitObject.StartTime - PreviousHitObject.BaseHitObject.StartTime) / TimeRate;
			double num3 = Math.Pow(DifficultyHitObjectMania.INDIVIDUAL_DECAY_BASE, num2 / 1000.0);
			double num4 = Math.Pow(DifficultyHitObjectMania.OVERALL_DECAY_BASE, num2 / 1000.0);
			double num5 = 1.0;
			double num6 = 0.0;
			for (int i = 0; i < this.BaseHitObject.parentBD.ColumnsCount; i++)
			{
				this.HeldUntil[i] = PreviousHitObject.HeldUntil[i];
				if ((double)this.BaseHitObject.StartTime < this.HeldUntil[i] && (double)this.BaseHitObject.EndTime > this.HeldUntil[i])
				{
					num6 = 1.0;
				}
				if ((double)this.BaseHitObject.EndTime == this.HeldUntil[i])
				{
					num6 = 0.0;
				}
				if (this.HeldUntil[i] > (double)this.BaseHitObject.EndTime)
				{
					num5 = 1.25;
				}
			}
			this.HeldUntil[this.BaseHitObject.Column] = (double)this.BaseHitObject.EndTime;
			for (int j = 0; j < this.BaseHitObject.parentBD.ColumnsCount; j++)
			{
				this.IndividualStrains[j] = PreviousHitObject.IndividualStrains[j] * num3;
			}
			this.IndividualStrain += 2.0 * num5;
			this.OverallStrain = PreviousHitObject.OverallStrain * num4 + (num + num6) * num5;
		}

		// Token: 0x0400025E RID: 606
		internal static readonly double INDIVIDUAL_DECAY_BASE = 0.125;

		// Token: 0x0400025F RID: 607
		internal static readonly double OVERALL_DECAY_BASE = 0.3;

		// Token: 0x04000260 RID: 608
		internal HitObject BaseHitObject;

		// Token: 0x04000261 RID: 609
		private double[] HeldUntil;

		// Token: 0x04000262 RID: 610
		private double[] IndividualStrains;

		// Token: 0x04000263 RID: 611
		internal double OverallStrain = 1.0;
	}
}
