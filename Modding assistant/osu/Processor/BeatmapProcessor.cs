using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using Modding_assistant.Utility;
using osu.Beatmap;

namespace osu.Processor
{
	// Token: 0x02000038 RID: 56
	public static class BeatmapProcessor
	{
		// Token: 0x06000269 RID: 617 RVA: 0x0001F290 File Offset: 0x0001D490
		public static bool BeatmapFolderInit(BeatmapInfo bi)
		{
			if (!BeatmapProcessor.FindAndSortFiles(bi, bi.HomeDirectory))
			{
				return false;
			}
			DirectoryInfo dInfo = new DirectoryInfo(bi.HomeDirectory);
			try
			{
				double num = (double)BeatmapProcessor.DirectorySize(dInfo, true);
				bi.BeatmapSize = Convert.ToSingle(Math.Round(num / 1048576.0, 2));
			}
			catch (Exception ex)
			{
				MessageBox.Show("BeatmapFolderInit error: " + ex.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				return false;
			}
			return true;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0001F318 File Offset: 0x0001D518
		private static long DirectorySize(DirectoryInfo dInfo, bool includeSubDir)
		{
			long num = dInfo.EnumerateFiles().Sum((FileInfo file) => file.Length);
			if (includeSubDir)
			{
				num += dInfo.EnumerateDirectories().Sum((DirectoryInfo dir) => BeatmapProcessor.DirectorySize(dir, true));
			}
			return num;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0001F384 File Offset: 0x0001D584
		public static void CreateUniqueHSList(BeatmapInfo bi)
		{
			BeatmapProcessor.<>c__DisplayClass2_0 CS$<>8__locals1 = new BeatmapProcessor.<>c__DisplayClass2_0();
			CS$<>8__locals1.bi = bi;
			int i;
			int num;
			for (i = 0; i < CS$<>8__locals1.bi.Difficluties.Count; i = num + 1)
			{
				int y2;
				for (y2 = 0; y2 < CS$<>8__locals1.bi.Difficluties[i].HitSoundsUniqueList.Count; y2 = num + 1)
				{
					if (!CS$<>8__locals1.bi.HitSoundsUFL.Any((string s) => s.Equals(CS$<>8__locals1.bi.Difficluties[i].HitSoundsUniqueList[y2], StringComparison.CurrentCultureIgnoreCase)))
					{
						CS$<>8__locals1.bi.HitSoundsUFL.Add(CS$<>8__locals1.bi.Difficluties[i].HitSoundsUniqueList[y2]);
					}
					num = y2;
				}
				int y;
				for (y = 0; y < CS$<>8__locals1.bi.Difficluties[i].CustomSamplesManiaList.Count; y = num + 1)
				{
					if (!CS$<>8__locals1.bi.ManiaCustomSamplesUFL.Any((string s) => s.Equals(CS$<>8__locals1.bi.Difficluties[i].CustomSamplesManiaList[y], StringComparison.CurrentCultureIgnoreCase)))
					{
						CS$<>8__locals1.bi.ManiaCustomSamplesUFL.Add(CS$<>8__locals1.bi.Difficluties[i].CustomSamplesManiaList[y]);
					}
					num = y;
				}
				CS$<>8__locals1.bi.Difficluties[i].HitSoundsUniqueList.Clear();
				CS$<>8__locals1.bi.Difficluties[i].CustomSamplesManiaList.Clear();
				num = i;
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0001F5F4 File Offset: 0x0001D7F4
		private static bool FindAndSortFiles(BeatmapInfo bi, string directory)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directory);
			try
			{
				FileInfo[] files = directoryInfo.GetFiles("*");
				int i = 0;
				while (i < files.Length)
				{
					FileInfo fileInfo = files[i];
					string text = fileInfo.Extension.ToLower();
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 1421611506u)
					{
						if (num <= 1128223456u)
						{
							if (num != 175576948u)
							{
								if (num != 592705037u)
								{
									if (num != 1128223456u)
									{
										goto IL_32B;
									}
									if (!(text == ".png"))
									{
										goto IL_32B;
									}
									goto IL_2CE;
								}
								else
								{
									if (!(text == ".mp3"))
									{
										goto IL_32B;
									}
									goto IL_2F7;
								}
							}
							else
							{
								if (!(text == ".bmp"))
								{
									goto IL_32B;
								}
								goto IL_2CE;
							}
						}
						else if (num != 1384894805u)
						{
							if (num != 1388056268u)
							{
								if (num != 1421611506u)
								{
									goto IL_32B;
								}
								if (!(text == ".jpe"))
								{
									goto IL_32B;
								}
								goto IL_2CE;
							}
							else
							{
								if (!(text == ".jpg"))
								{
									goto IL_32B;
								}
								goto IL_2CE;
							}
						}
						else
						{
							if (!(text == ".gif"))
							{
								goto IL_32B;
							}
							goto IL_2CE;
						}
					}
					else if (num <= 2776613419u)
					{
						if (num != 2194571213u)
						{
							if (num != 2561755776u)
							{
								if (num != 2776613419u)
								{
									goto IL_32B;
								}
								if (!(text == ".osb"))
								{
									goto IL_32B;
								}
								bi.OsbFiles.Add(ut_Path.Normalize(fileInfo.FullName.Remove(0, bi.HomeDirectory.Length)));
							}
							else
							{
								if (!(text == ".ogg"))
								{
									goto IL_32B;
								}
								goto IL_2F7;
							}
						}
						else
						{
							if (!(text == ".wav"))
							{
								goto IL_32B;
							}
							goto IL_2F7;
						}
					}
					else if (num <= 3560597182u)
					{
						if (num != 3128943418u)
						{
							if (num != 3560597182u)
							{
								goto IL_32B;
							}
							if (!(text == ".tiff"))
							{
								goto IL_32B;
							}
							goto IL_2CE;
						}
						else
						{
							if (!(text == ".osu"))
							{
								goto IL_32B;
							}
							if (string.Compare(ut_Path.Normalize(fileInfo.FullName), ut_Path.Normalize(Path.Combine(bi.HomeDirectory, fileInfo.Name)), true) == 0)
							{
								Difficulty difficulty = new Difficulty();
								difficulty.osuFile = ut_Path.Normalize(fileInfo.FullName);
								bi.Difficluties.Add(difficulty);
							}
							else
							{
								bi.OtherFiles.Add(ut_Path.Normalize(fileInfo.FullName.Remove(0, bi.HomeDirectory.Length)));
							}
						}
					}
					else if (num != 4100894060u)
					{
						if (num != 4178554255u)
						{
							goto IL_32B;
						}
						if (!(text == ".jpeg"))
						{
							goto IL_32B;
						}
						goto IL_2CE;
					}
					else
					{
						if (!(text == ".tif"))
						{
							goto IL_32B;
						}
						goto IL_2CE;
					}
					IL_352:
					i++;
					continue;
					IL_2CE:
					bi.ImageFiles.Add(ut_Path.Normalize(fileInfo.FullName.Remove(0, bi.HomeDirectory.Length)));
					goto IL_352;
					IL_2F7:
					bi.SoundFiles.Add(new AudioFile(ut_Path.Normalize(fileInfo.FullName.Remove(0, bi.HomeDirectory.Length)), bi.HomeDirectory));
					goto IL_352;
					IL_32B:
					bi.OtherFiles.Add(ut_Path.Normalize(fileInfo.FullName.Remove(0, bi.HomeDirectory.Length)));
					goto IL_352;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("FindAndSortFiles error: " + ex.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				return false;
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				BeatmapProcessor.FindAndSortFiles(bi, ut_Path.Normalize(directoryInfo2.FullName));
			}
			return true;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0001F9D8 File Offset: 0x0001DBD8
		public static void CreateSBFilesList(BeatmapInfo bi)
		{
			BeatmapProcessor.<>c__DisplayClass4_0 CS$<>8__locals1 = new BeatmapProcessor.<>c__DisplayClass4_0();
			CS$<>8__locals1.bi = bi;
			int i;
			int num;
			for (i = 0; i < CS$<>8__locals1.bi.Difficluties.Count; i = num + 1)
			{
				int y;
				for (y = 0; y < CS$<>8__locals1.bi.Difficluties[i].StoryboardFilesList.Count; y = num + 1)
				{
					if (!CS$<>8__locals1.bi.StoryboardUFL.Any((string s) => s.Equals(CS$<>8__locals1.bi.Difficluties[i].StoryboardFilesList[y], StringComparison.CurrentCultureIgnoreCase)))
					{
						CS$<>8__locals1.bi.StoryboardUFL.Add(CS$<>8__locals1.bi.Difficluties[i].StoryboardFilesList[y]);
					}
					num = y;
				}
				num = i;
			}
			string text = CS$<>8__locals1.bi.Difficluties[0].Version;
			text = Path.GetInvalidFileNameChars().Aggregate(text, (string current, char c) => current.Replace(c.ToString(), string.Empty));
			CS$<>8__locals1.osbName = CS$<>8__locals1.bi.Difficluties[0].osuFile.Remove(CS$<>8__locals1.bi.Difficluties[0].osuFile.Length - Convert.ToString(" [" + text + "].osu").Length, Convert.ToString(" [" + text + "].osu").Length) + ".osb";
			CS$<>8__locals1.osbName = Path.GetFileName(CS$<>8__locals1.osbName);
			if (!CS$<>8__locals1.bi.OsbFiles.Any((string s) => s.Equals(CS$<>8__locals1.osbName, StringComparison.CurrentCultureIgnoreCase)))
			{
				return;
			}
			CS$<>8__locals1.bi.osbFile = CS$<>8__locals1.osbName;
			List<string> list = new List<string>(new osuReader(Path.Combine(CS$<>8__locals1.bi.HomeDirectory, CS$<>8__locals1.osbName)).GetKeys("events"));
			for (int k = 0; k < list.Count; k++)
			{
				List<string> str = new List<string>(list[k].Split(new char[]
				{
					','
				}));
				if (str.Count > 0)
				{
					if (string.Compare(str[0], "sample", true) == 0)
					{
						if (str.Count > 3 && !CS$<>8__locals1.bi.StoryboardUFL.Any((string s) => s.Equals(ut_Path.Normalize(str[3].Replace("\"", "")), StringComparison.CurrentCultureIgnoreCase)))
						{
							CS$<>8__locals1.bi.StoryboardUFL.Add(ut_Path.Normalize(str[3].Replace("\"", "")));
						}
						CS$<>8__locals1.bi.isStoryBoard = true;
					}
					else if (string.Compare(str[0], "animation", true) == 0)
					{
						if (k < list.Count - 1)
						{
							List<string> strNext = new List<string>(list[k + 1].Split(new char[]
							{
								','
							}));
							strNext[0] = strNext[0].TrimStart(new char[]
							{
								'_'
							});
							if (Constants.SBEvents.FindIndex((string x) => x.Equals(strNext[0], StringComparison.InvariantCultureIgnoreCase)) != -1)
							{
								int num2 = 0;
								string text2 = string.Empty;
								if (str.Count > 6)
								{
									int.TryParse(str[6], out num2);
								}
								if (str.Count > 3)
								{
									text2 = ut_Path.Normalize(str[3].Replace("\"", ""));
								}
								string extension = Path.GetExtension(text2);
								text2 = text2.Substring(0, text2.Length - extension.Length);
								for (int j = 0; j < num2; j++)
								{
									string filename = text2 + Convert.ToString(j) + extension;
									if (!CS$<>8__locals1.bi.StoryboardUFL.Any((string s) => s.Equals(filename, StringComparison.CurrentCultureIgnoreCase)))
									{
										CS$<>8__locals1.bi.StoryboardUFL.Add(filename);
									}
								}
								CS$<>8__locals1.bi.isStoryBoard = true;
							}
						}
					}
					else if (string.Compare(str[0], "sprite", true) == 0 && k < list.Count - 1)
					{
						List<string> strNext = new List<string>(list[k + 1].Split(new char[]
						{
							','
						}));
						strNext[0] = strNext[0].TrimStart(new char[]
						{
							'_'
						});
						if (Constants.SBEvents.FindIndex((string x) => x.Equals(strNext[0], StringComparison.InvariantCultureIgnoreCase)) != -1)
						{
							if (str.Count > 3 && !CS$<>8__locals1.bi.StoryboardUFL.Any((string s) => s.Equals(ut_Path.Normalize(str[3].Replace("\"", "")), StringComparison.CurrentCultureIgnoreCase)))
							{
								CS$<>8__locals1.bi.StoryboardUFL.Add(ut_Path.Normalize(str[3].Replace("\"", "")));
							}
							CS$<>8__locals1.bi.isStoryBoard = true;
						}
					}
				}
			}
		}
	}
}
