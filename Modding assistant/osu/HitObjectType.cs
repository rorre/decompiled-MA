using System;

namespace osu
{
	// Token: 0x0200002A RID: 42
	[Flags]
	public enum HitObjectType
	{
		// Token: 0x0400018C RID: 396
		Normal = 1,
		// Token: 0x0400018D RID: 397
		Slider = 2,
		// Token: 0x0400018E RID: 398
		NewCombo = 4,
		// Token: 0x0400018F RID: 399
		NormalNewCombo = 5,
		// Token: 0x04000190 RID: 400
		SliderNewCombo = 6,
		// Token: 0x04000191 RID: 401
		Spinner = 8,
		// Token: 0x04000192 RID: 402
		ColourHax = 112,
		// Token: 0x04000193 RID: 403
		Hold = 128
	}
}
