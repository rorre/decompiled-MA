using System;
using System.Collections.Generic;

namespace osu
{
	// Token: 0x02000028 RID: 40
	public class Color
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0001ED30 File Offset: 0x0001CF30
		public Color(int red, int green, int blue)
		{
			this.Red = red;
			this.Green = green;
			this.Blue = blue;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0001ED50 File Offset: 0x0001CF50
		public Color(string strColor)
		{
			List<string> list = new List<string>(strColor.Split(new char[]
			{
				','
			}));
			try
			{
				this.Red = Convert.ToInt32(list[0].Trim());
				this.Green = Convert.ToInt32(list[1].Trim());
				this.Blue = Convert.ToInt32(list[2].Trim());
			}
			catch
			{
				throw new Exception("Error parsing color string.");
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0001EDE0 File Offset: 0x0001CFE0
		public bool Equals(Color c)
		{
			return this.Red == c.Red && this.Green == c.Green && this.Blue == c.Blue;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0001EE0E File Offset: 0x0001D00E
		public override string ToString()
		{
			return string.Format("{0},{1},{2}", this.Red, this.Green, this.Blue);
		}

		// Token: 0x04000187 RID: 391
		public int Red;

		// Token: 0x04000188 RID: 392
		public int Green;

		// Token: 0x04000189 RID: 393
		public int Blue;
	}
}
