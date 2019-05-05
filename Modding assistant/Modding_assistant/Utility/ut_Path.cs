using System;
using System.IO;
using System.Linq;

namespace Modding_assistant.Utility
{
	// Token: 0x0200000D RID: 13
	public static class ut_Path
	{
		// Token: 0x06000072 RID: 114 RVA: 0x00007E94 File Offset: 0x00006094
		public static string Normalize(string path)
		{
			path = path.Trim();
			path = path.TrimStart(new char[]
			{
				Path.DirectorySeparatorChar
			});
			path = path.TrimStart(new char[]
			{
				Path.AltDirectorySeparatorChar
			});
			path = path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
			return path;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00007EE8 File Offset: 0x000060E8
		public static string CleanFileName(string fileName)
		{
			return Path.GetInvalidPathChars().Aggregate(fileName, (string current, char c) => current.Replace(c.ToString(), string.Empty));
		}
	}
}
