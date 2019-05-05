using System;
using osu.Beatmap;

namespace osu.DiffCalc
{
	// Token: 0x0200004F RID: 79
	internal class DifficultyHitObjectTaiko
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000268D7 File Offset: 0x00024AD7
		internal bool IsBlue
		{
			get
			{
				return !this.BaseHitObject.isDon;
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000268E8 File Offset: 0x00024AE8
		internal DifficultyHitObjectTaiko(HitObject BaseHitObject)
		{
			this.BaseHitObject = BaseHitObject;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00026954 File Offset: 0x00024B54
		internal void CalculateStrains(DifficultyHitObjectTaiko PreviousHitObject, double TimeRate)
		{
			this.TimeElapsed = (double)(this.BaseHitObject.StartTime - PreviousHitObject.BaseHitObject.StartTime) / TimeRate;
			double num = Math.Pow(DifficultyHitObjectTaiko.DECAY_BASE, this.TimeElapsed / 1000.0);
			double num2 = 1.0;
			if (PreviousHitObject.BaseHitObject.IsType(HitObjectType.Normal) && this.BaseHitObject.IsType(HitObjectType.Normal) && this.BaseHitObject.StartTime - PreviousHitObject.BaseHitObject.StartTime < 1000)
			{
				num2 += this.ColorChangeAddition(PreviousHitObject);
				num2 += this.RhythmChangeAddition(PreviousHitObject);
			}
			double num3 = 1.0;
			if (this.TimeElapsed < 50.0)
			{
				num3 = 0.4 + 0.6 * this.TimeElapsed / 50.0;
			}
			this.Strain = PreviousHitObject.Strain * num + num2 * num3;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00026A48 File Offset: 0x00024C48
		internal double ColorChangeAddition(DifficultyHitObjectTaiko PreviousHitObject)
		{
			if (PreviousHitObject.IsBlue != this.IsBlue)
			{
				this.LastColorSwitchEven = ((PreviousHitObject.SameColorSince % 2 == 0) ? DifficultyHitObjectTaiko.ColorSwitch.Even : DifficultyHitObjectTaiko.ColorSwitch.Odd);
				DifficultyHitObjectTaiko.ColorSwitch lastColorSwitchEven = PreviousHitObject.LastColorSwitchEven;
				if (lastColorSwitchEven != DifficultyHitObjectTaiko.ColorSwitch.Even)
				{
					if (lastColorSwitchEven == DifficultyHitObjectTaiko.ColorSwitch.Odd)
					{
						if (this.LastColorSwitchEven == DifficultyHitObjectTaiko.ColorSwitch.Even)
						{
							return this.COLOR_CHANGE_BONUS;
						}
					}
				}
				else if (this.LastColorSwitchEven == DifficultyHitObjectTaiko.ColorSwitch.Odd)
				{
					return this.COLOR_CHANGE_BONUS;
				}
			}
			else
			{
				this.LastColorSwitchEven = PreviousHitObject.LastColorSwitchEven;
				this.SameColorSince = PreviousHitObject.SameColorSince + 1;
			}
			return 0.0;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00026ACC File Offset: 0x00024CCC
		internal double RhythmChangeAddition(DifficultyHitObjectTaiko PreviousHitObject)
		{
			if (this.TimeElapsed == 0.0 || PreviousHitObject.TimeElapsed == 0.0)
			{
				return 0.0;
			}
			double num = Math.Max(PreviousHitObject.TimeElapsed / this.TimeElapsed, this.TimeElapsed / PreviousHitObject.TimeElapsed);
			if (num >= 8.0)
			{
				return 0.0;
			}
			double num2 = Math.Log(num, this.RHYTHM_CHANGE_BASE) % 1.0;
			if (num2 > this.RHYTHM_CHANGE_BASE_THRESHOLD && num2 < 1.0 - this.RHYTHM_CHANGE_BASE_THRESHOLD)
			{
				return this.RHYTHM_CHANGE_BONUS;
			}
			return 0.0;
		}

		// Token: 0x04000270 RID: 624
		internal static readonly double DECAY_BASE = 0.3;

		// Token: 0x04000271 RID: 625
		internal HitObject BaseHitObject;

		// Token: 0x04000272 RID: 626
		internal double Strain = 1.0;

		// Token: 0x04000273 RID: 627
		private int SameColorSince = 1;

		// Token: 0x04000274 RID: 628
		private DifficultyHitObjectTaiko.ColorSwitch LastColorSwitchEven;

		// Token: 0x04000275 RID: 629
		private double TimeElapsed;

		// Token: 0x04000276 RID: 630
		private readonly double COLOR_CHANGE_BONUS = 0.75;

		// Token: 0x04000277 RID: 631
		private readonly double RHYTHM_CHANGE_BONUS = 1.0;

		// Token: 0x04000278 RID: 632
		private readonly double RHYTHM_CHANGE_BASE_THRESHOLD = 0.2;

		// Token: 0x04000279 RID: 633
		private readonly double RHYTHM_CHANGE_BASE = 2.0;

		// Token: 0x02000098 RID: 152
		private enum ColorSwitch
		{
			// Token: 0x0400032A RID: 810
			None,
			// Token: 0x0400032B RID: 811
			Even,
			// Token: 0x0400032C RID: 812
			Odd
		}
	}
}
