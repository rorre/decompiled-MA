using System;

namespace osu.Beatmap
{
	// Token: 0x0200003F RID: 63
	public static class Extensions
	{
		// Token: 0x06000296 RID: 662 RVA: 0x000233A4 File Offset: 0x000215A4
		public static bool IsType(this HitObjectType Type, HitObjectType type)
		{
			return (Type & type) > (HitObjectType)0;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000233A4 File Offset: 0x000215A4
		public static bool IsType(this HitObjectHSType Type, HitObjectHSType type)
		{
			return (Type & type) > HitObjectHSType.None;
		}
	}
}
