using System;
using System.Collections.Generic;
using System.IO;

namespace Modding_assistant.Utility
{
	// Token: 0x0200000B RID: 11
	internal static class FastName
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00007B78 File Offset: 0x00005D78
		public static bool Detect(string Path, out string outStr)
		{
			outStr = string.Empty;
			FastName.BeatmapInfo beatmapInfo;
			try
			{
				beatmapInfo = new FastName.BeatmapInfo(Path);
			}
			catch
			{
				return false;
			}
			if (beatmapInfo.Difficluties.Count < 1)
			{
				return false;
			}
			string creator = beatmapInfo.Difficluties[0].Creator;
			string artist = beatmapInfo.Difficluties[0].Artist;
			string title = beatmapInfo.Difficluties[0].Title;
			int beatmapSetID = beatmapInfo.Difficluties[0].BeatmapSetID;
			for (int i = 1; i < beatmapInfo.Difficluties.Count; i++)
			{
				if (beatmapSetID != beatmapInfo.Difficluties[i].BeatmapSetID)
				{
					return false;
				}
				if (!string.Equals(creator, beatmapInfo.Difficluties[i].Creator))
				{
					return false;
				}
				if (!string.Equals(artist, beatmapInfo.Difficluties[i].Artist))
				{
					return false;
				}
				if (!string.Equals(title, beatmapInfo.Difficluties[i].Title))
				{
					return false;
				}
			}
			if (beatmapSetID != 0)
			{
				outStr = beatmapSetID.ToString("D6") + " | ";
			}
			else
			{
				outStr = "000000 | ";
			}
			outStr = string.Concat(new string[]
			{
				outStr,
				artist,
				" - ",
				title,
				" | ",
				creator
			});
			return true;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00007CF0 File Offset: 0x00005EF0
		private static bool BeatmapFolderInit(FastName.BeatmapInfo bi)
		{
			if (!FastName.FindAndSortFiles(bi, bi.HomeDirectory))
			{
				return false;
			}
			new DirectoryInfo(bi.HomeDirectory);
			return true;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00007D10 File Offset: 0x00005F10
		private static bool FindAndSortFiles(FastName.BeatmapInfo bi, string directory)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directory);
			try
			{
				foreach (FileInfo fileInfo in directoryInfo.GetFiles("*"))
				{
					string a = fileInfo.Extension.ToLower();
					if (a == ".osu" && string.Compare(ut_Path.Normalize(fileInfo.FullName), ut_Path.Normalize(Path.Combine(bi.HomeDirectory, fileInfo.Name)), true) == 0)
					{
						FastName.Difficulty difficulty = new FastName.Difficulty();
						difficulty.osuFile = ut_Path.Normalize(fileInfo.FullName);
						bi.Difficluties.Add(difficulty);
					}
				}
			}
			catch
			{
				return false;
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				FastName.FindAndSortFiles(bi, ut_Path.Normalize(directoryInfo2.FullName));
			}
			return true;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00007DF8 File Offset: 0x00005FF8
		private static bool ParseDifficulty(FastName.Difficulty bd)
		{
			osuReader osuReader = new osuReader(bd.osuFile);
			bd.Artist = osuReader.GetValue("artist", "metadata", string.Empty);
			bd.Title = osuReader.GetValue("title", "metadata", string.Empty);
			bd.Creator = osuReader.GetValue("creator", "metadata", string.Empty);
			bd.BeatmapSetID = Convert.ToInt32(osuReader.GetValue("beatmapsetid", "metadata", "0"));
			return true;
		}

		// Token: 0x0200005B RID: 91
		private class BeatmapInfo
		{
			// Token: 0x0600030A RID: 778 RVA: 0x00026E1C File Offset: 0x0002501C
			public BeatmapInfo(string path)
			{
				this.HomeDirectory = path;
				if (!FastName.BeatmapFolderInit(this))
				{
					throw new Exception("Unable to process beatmap: " + this.HomeDirectory);
				}
				if (this.Difficluties.Count == 0)
				{
					throw new Exception("No difficulties found:  " + this.HomeDirectory);
				}
				for (int i = 0; i < this.Difficluties.Count; i++)
				{
					FastName.ParseDifficulty(this.Difficluties[i]);
				}
			}

			// Token: 0x0400029A RID: 666
			public string HomeDirectory = string.Empty;

			// Token: 0x0400029B RID: 667
			public List<FastName.Difficulty> Difficluties = new List<FastName.Difficulty>();
		}

		// Token: 0x0200005C RID: 92
		private class Difficulty
		{
			// Token: 0x0400029C RID: 668
			public string osuFile = string.Empty;

			// Token: 0x0400029D RID: 669
			public string Creator = string.Empty;

			// Token: 0x0400029E RID: 670
			public string Artist = string.Empty;

			// Token: 0x0400029F RID: 671
			public string Title = string.Empty;

			// Token: 0x040002A0 RID: 672
			public int BeatmapSetID;
		}
	}
}
