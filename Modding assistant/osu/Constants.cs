using System;
using System.Collections.Generic;

namespace osu
{
	// Token: 0x02000029 RID: 41
	internal class Constants
	{
		// Token: 0x0400018A RID: 394
		public static readonly List<string> SBEvents = new List<string>
		{
			"F",
			"M",
			"MX",
			"MY",
			"S",
			"V",
			"R",
			"C",
			"L",
			"T",
			"P"
		};

		// Token: 0x02000073 RID: 115
		internal static class Splitter
		{
			// Token: 0x1700004C RID: 76
			// (get) Token: 0x0600033B RID: 827 RVA: 0x0002717C File Offset: 0x0002537C
			public static char Space
			{
				get
				{
					return ' ';
				}
			}

			// Token: 0x1700004D RID: 77
			// (get) Token: 0x0600033C RID: 828 RVA: 0x00027180 File Offset: 0x00025380
			public static char[] Comma
			{
				get
				{
					return new char[]
					{
						','
					};
				}
			}

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x0600033D RID: 829 RVA: 0x0002718D File Offset: 0x0002538D
			public static char[] Colon
			{
				get
				{
					return new char[]
					{
						':'
					};
				}
			}

			// Token: 0x1700004F RID: 79
			// (get) Token: 0x0600033E RID: 830 RVA: 0x0002719A File Offset: 0x0002539A
			public static char[] Pipe
			{
				get
				{
					return new char[]
					{
						'|'
					};
				}
			}

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x0600033F RID: 831 RVA: 0x000271A7 File Offset: 0x000253A7
			public static char[] Bracket
			{
				get
				{
					return new char[]
					{
						'['
					};
				}
			}
		}
	}
}
