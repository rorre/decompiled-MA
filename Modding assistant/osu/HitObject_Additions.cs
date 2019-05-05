using System;

namespace osu
{
	// Token: 0x0200002F RID: 47
	public class HitObject_Additions
	{
		// Token: 0x0600025A RID: 602 RVA: 0x0001EECC File Offset: 0x0001D0CC
		public HitObject_Additions(HitsoundSetType mSet, HitsoundSetType sSet)
		{
			this.mainSet = mSet;
			this.secondarySet = sSet;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00007843 File Offset: 0x00005A43
		public HitObject_Additions()
		{
		}

		// Token: 0x040001A9 RID: 425
		public HitsoundSetType mainSet;

		// Token: 0x040001AA RID: 426
		public HitsoundSetType secondarySet;
	}
}
