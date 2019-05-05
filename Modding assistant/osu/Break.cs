using System;

namespace osu
{
	// Token: 0x02000036 RID: 54
	public class Break
	{
		// Token: 0x0600025D RID: 605 RVA: 0x0001EF00 File Offset: 0x0001D100
		public bool Equals(Break b)
		{
			return this.startTime == b.startTime && this.endTime == b.endTime;
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0001EF20 File Offset: 0x0001D120
		public override string ToString()
		{
			TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)this.startTime);
			string str = string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds) + " - ";
			timeSpan = TimeSpan.FromMilliseconds((double)this.endTime);
			return str + string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
		}

		// Token: 0x040001C9 RID: 457
		public int startTime;

		// Token: 0x040001CA RID: 458
		public int endTime;
	}
}
