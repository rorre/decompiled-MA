using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows.Documents;
using System.Windows.Navigation;
using Modding_assistant.maStrings;
using osu;
using osu.Beatmap;

namespace Modding_assistant.Utility
{
	// Token: 0x02000013 RID: 19
	public static class Snapshot
	{
		// Token: 0x06000081 RID: 129 RVA: 0x0000850C File Offset: 0x0000670C
		public static bool CreateSnapshot(BeatmapInfo bi, string BeatmapDirectoryName, string SnapshotName, bool FullBackup)
		{
			DateTime now = DateTime.Now;
			List<string> list = new List<string>();
			string text = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Snapshots", BeatmapDirectoryName, now.Ticks.ToString());
			list.Add("[Main]");
			list.Add("Name = " + SnapshotName);
			list.Add("[Files]");
			try
			{
				Snapshot.ListFiles(bi.HomeDirectory, list, false, bi.HomeDirectory.Length);
				Directory.CreateDirectory(text);
			}
			catch
			{
				return false;
			}
			if (FullBackup)
			{
				if (!Snapshot.CreateBackup(bi.HomeDirectory, text))
				{
					return false;
				}
			}
			else
			{
				try
				{
					for (int i = 0; i < bi.Difficluties.Count; i++)
					{
						File.Copy(bi.Difficluties[i].osuFile, Path.Combine(text, Path.GetFileName(bi.Difficluties[i].osuFile)));
					}
					if (bi.isStoryBoard)
					{
						File.Copy(bi.osbFile, Path.Combine(text, Path.GetFileName(bi.osbFile)));
					}
				}
				catch
				{
					return false;
				}
			}
			try
			{
				File.WriteAllLines(Path.Combine(text, "snapshot"), list);
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00008674 File Offset: 0x00006874
		public static Paragraph Compare(BeatmapInfo bi, string SnapshotDir)
		{
			Paragraph paragraph = paragraph = new Paragraph();
			if (!File.Exists(Path.Combine(SnapshotDir, "snapshot")))
			{
				paragraph.Inlines.Add(new Run(ap.SS_1));
				return paragraph;
			}
			BeatmapInfo beatmapInfo;
			try
			{
				beatmapInfo = new BeatmapInfo(SnapshotDir);
			}
			catch
			{
				paragraph.Inlines.Add(new Run(ap.SS_2));
				return paragraph;
			}
			IniReader iniReader = new IniReader(Path.Combine(SnapshotDir, "snapshot"), '=');
			List<string> list = new List<string>(iniReader.GetKeys("Files"));
			List<string> list2 = new List<string>();
			Snapshot.ListFiles(bi.HomeDirectory, list2, true, bi.HomeDirectory.Length);
			for (int i = 0; i < list2.Count; i++)
			{
				List<string> oFile = list2[i].Split(new char[]
				{
					'='
				}).ToList<string>();
				if (oFile.Count < 2)
				{
					oFile.Add(string.Empty);
				}
				int num = list.FindIndex((string x) => x.Equals(oFile[0], StringComparison.CurrentCultureIgnoreCase));
				if (num == -1)
				{
					paragraph.Inlines.Add(new Run(ap.SS_4 + oFile[0] + "\n"));
				}
				else
				{
					if (!string.Equals(iniReader.GetValue(list[num], "Files", "0"), oFile[1]))
					{
						paragraph.Inlines.Add(new Run(ap.SS_3 + oFile[0] + "\n"));
					}
					list.RemoveAt(num);
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				paragraph.Inlines.Add(new Run(ap.SS_5 + list[0] + "\n"));
			}
			bool flag = false;
			List<string> list3 = new List<string>();
			for (int k = 0; k < bi.Difficluties.Count; k++)
			{
				bool flag2 = false;
				int beatmapID = bi.Difficluties[k].BeatmapID;
				string version = bi.Difficluties[k].Version;
				if (beatmapID == 0 || beatmapID == -1)
				{
					flag = true;
				}
				for (int l = 0; l < beatmapInfo.Difficluties.Count; l++)
				{
					if (!flag)
					{
						if (beatmapID == beatmapInfo.Difficluties[l].BeatmapID)
						{
							Snapshot.CompareDifficulties(bi.Difficluties[k], beatmapInfo.Difficluties[l], paragraph);
							beatmapInfo.Difficluties.RemoveAt(l);
							flag2 = true;
							break;
						}
					}
					else if (string.Equals(version, beatmapInfo.Difficluties[l].Version))
					{
						Snapshot.CompareDifficulties(bi.Difficluties[k], beatmapInfo.Difficluties[l], paragraph);
						beatmapInfo.Difficluties.RemoveAt(l);
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					list3.Add(ap.SS_8 + bi.Difficluties[k].Version);
				}
			}
			for (int m = 0; m < beatmapInfo.Difficluties.Count; m++)
			{
				list3.Add(ap.SS_9 + beatmapInfo.Difficluties[m].Version);
			}
			for (int n = 0; n < list3.Count; n++)
			{
				paragraph.Inlines.Add(new Run("\n" + list3[n]));
			}
			return paragraph;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00008A40 File Offset: 0x00006C40
		public static SnapshotList GetSnapshots(string SnapshotDir)
		{
			SnapshotList snapshotList = new SnapshotList();
			if (!Directory.Exists(SnapshotDir))
			{
				return snapshotList;
			}
			try
			{
				foreach (DirectoryInfo directoryInfo in new DirectoryInfo(SnapshotDir).GetDirectories())
				{
					IniReader iniReader = new IniReader(Path.Combine(directoryInfo.FullName, "snapshot"), '=');
					snapshotList.Name.Add(iniReader.GetValue("name", "main"));
					snapshotList.Directory.Add(directoryInfo.FullName);
				}
			}
			catch
			{
				return snapshotList;
			}
			return snapshotList;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00008AE0 File Offset: 0x00006CE0
		private static bool CreateBackup(string SourcePath, string DestinationPath)
		{
			try
			{
				string[] array = Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories);
				for (int i = 0; i < array.Length; i++)
				{
					Directory.CreateDirectory(array[i].Replace(SourcePath, DestinationPath));
				}
				foreach (string text in Directory.GetFiles(SourcePath, "*.*", SearchOption.AllDirectories))
				{
					File.Copy(text, text.Replace(SourcePath, DestinationPath), true);
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00008B60 File Offset: 0x00006D60
		private static void CompareDifficulties(Difficulty di, Difficulty di2, Paragraph rtP)
		{
			if (rtP.Inlines.Count != 0)
			{
				rtP.Inlines.Add(new Run("\n"));
			}
			rtP.Inlines.Add(new Bold(new Run(string.Concat(new object[]
			{
				"[",
				di.BeatmapID,
				"] ",
				di.Version,
				"\n"
			}))));
			if (string.Equals(Snapshot.GetHash(di.osuFile), Snapshot.GetHash(di2.osuFile)))
			{
				return;
			}
			if (di.DifficlutyMode != di2.DifficlutyMode)
			{
				rtP.Inlines.Add(new Run("Difficluty mode changed. Comparation aborted.\n"));
				return;
			}
			if (!string.Equals(di.AudioFilename, di2.AudioFilename, StringComparison.CurrentCultureIgnoreCase))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Audio file changed: ",
					di2.AudioFilename,
					" -> ",
					di.AudioFilename,
					"\n"
				})));
			}
			if (!string.Equals(di.osuVersion, di2.osuVersion, StringComparison.CurrentCultureIgnoreCase))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"*.osu file verison changed: ",
					di2.osuVersion,
					" -> ",
					di.osuVersion,
					"\n"
				})));
			}
			if (!string.Equals(di.Version, di2.Version, StringComparison.CurrentCultureIgnoreCase))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Name changed: ",
					di2.Version,
					" -> ",
					di.Version,
					"\n"
				})));
			}
			if (di.AudioLeadIn != di2.AudioLeadIn)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Audio leadin changed: ",
					di2.AudioLeadIn,
					" -> ",
					di.AudioLeadIn,
					"\n"
				})));
			}
			if (di.PreviewTime != di2.PreviewTime)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Preview time changed: ",
					di2.PreviewTime,
					" -> ",
					di.PreviewTime,
					"\n"
				})));
			}
			if (di.CountdownOffset != di2.CountdownOffset)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Countdown offset changed: ",
					di2.CountdownOffset,
					" -> ",
					di.CountdownOffset,
					"\n"
				})));
			}
			if (di.Countdown != di2.Countdown)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Countdown changed: ",
					di2.Countdown,
					" -> ",
					di.Countdown,
					"\n"
				})));
			}
			if (di.StoryFireInFront != di2.StoryFireInFront)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Story fire in front changed: ",
					di2.StoryFireInFront,
					" -> ",
					di.StoryFireInFront,
					"\n"
				})));
			}
			if (di.LetterboxInBreaks != di2.LetterboxInBreaks)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Letterbox in breaks changed: ",
					di2.LetterboxInBreaks,
					" -> ",
					di.LetterboxInBreaks,
					"\n"
				})));
			}
			if (di.WidescreenStoryboard != di2.WidescreenStoryboard)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Widescreen storyboard changed: ",
					di2.WidescreenStoryboard,
					" -> ",
					di.WidescreenStoryboard,
					"\n"
				})));
			}
			if (di.EpilepsyWarning != di2.EpilepsyWarning)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Epilepsy warning changed: ",
					di2.EpilepsyWarning,
					" -> ",
					di.EpilepsyWarning,
					"\n"
				})));
			}
			if (di.StackLeniency != di2.StackLeniency)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Stack leniency changed: ",
					di2.StackLeniency,
					" -> ",
					di.StackLeniency,
					"\n"
				})));
			}
			if (!string.Equals(di.SkinPreference, di2.SkinPreference))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Skin preference changed: ",
					di2.SkinPreference,
					" -> ",
					di.SkinPreference,
					"\n"
				})));
			}
			if (!string.Equals(di.Title, di2.Title))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Title changed: ",
					di2.Title,
					" -> ",
					di.Title,
					"\n"
				})));
			}
			if (!string.Equals(di.TitleUnicode, di2.TitleUnicode))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Title unicode changed: ",
					di2.TitleUnicode,
					" -> ",
					di.TitleUnicode,
					"\n"
				})));
			}
			if (!string.Equals(di.Artist, di2.Artist))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Artist changed: ",
					di2.Artist,
					" -> ",
					di.Artist,
					"\n"
				})));
			}
			if (!string.Equals(di.ArtistUnicode, di2.ArtistUnicode))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Artist unicode changed: ",
					di2.ArtistUnicode,
					" -> ",
					di.ArtistUnicode,
					"\n"
				})));
			}
			if (!string.Equals(di.Creator, di2.Creator))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Creator changed: ",
					di2.Creator,
					" -> ",
					di.Creator,
					"\n"
				})));
			}
			if (!string.Equals(di.Source, di2.Source))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Source changed: ",
					di2.Source,
					" -> ",
					di.Source,
					"\n"
				})));
			}
			if (!string.Equals(di.Tags, di2.Tags))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Tags changed: ",
					di2.Tags,
					" -> ",
					di.Tags,
					"\n"
				})));
			}
			if (di.BeatmapID != di2.BeatmapID)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Beatmap ID changed: ",
					di2.BeatmapID,
					" -> ",
					di.BeatmapID,
					"\n"
				})));
			}
			if (di.BeatmapSetID != di2.BeatmapSetID)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Beatmap set ID changed: ",
					di2.BeatmapSetID,
					" -> ",
					di.BeatmapSetID,
					"\n"
				})));
			}
			if (di.HPDrainRate != di2.HPDrainRate)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"HP drain rate changed: ",
					di2.HPDrainRate,
					" -> ",
					di.HPDrainRate,
					"\n"
				})));
			}
			if (di.CircleSize != di2.CircleSize)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Circle size changed: ",
					di2.CircleSize,
					" -> ",
					di.CircleSize,
					"\n"
				})));
			}
			if (di.OverallDifficulty != di2.OverallDifficulty)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Overall difficulty changed: ",
					di2.OverallDifficulty,
					" -> ",
					di.OverallDifficulty,
					"\n"
				})));
			}
			if (di.ApproachRate != di2.ApproachRate)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Approach rate changed: ",
					di2.ApproachRate,
					" -> ",
					di.ApproachRate,
					"\n"
				})));
			}
			if (di.SliderMultiplier != di2.SliderMultiplier)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Slider multiplier changed: ",
					di2.SliderMultiplier,
					" -> ",
					di.SliderMultiplier,
					"\n"
				})));
			}
			if (di.SliderTickRate != di2.SliderTickRate)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Slider tick rate changed: ",
					di2.SliderTickRate,
					" -> ",
					di.SliderTickRate,
					"\n"
				})));
			}
			if (!string.Equals(di.Background, di2.Background))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Background changed: ",
					di2.Background,
					" -> ",
					di.Background,
					"\n"
				})));
			}
			if (!string.Equals(di.Video, di2.Video))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Video changed: ",
					di2.Video,
					" -> ",
					di.Video,
					"\n"
				})));
			}
			if (di.VideoOffset != di2.VideoOffset)
			{
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					"Video offset changed: ",
					di2.VideoOffset,
					" -> ",
					di.VideoOffset,
					"\n"
				})));
			}
			if (!di.SliderBorder.Equals(di2.SliderBorder))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Slider border changed: ",
					di2.SliderBorder.ToString(),
					" -> ",
					di.SliderBorder.ToString(),
					"\n"
				})));
			}
			if (!di.SliderTrackOverride.Equals(di2.SliderTrackOverride))
			{
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					"Slider border changed: ",
					di2.SliderTrackOverride.ToString(),
					" -> ",
					di.SliderTrackOverride.ToString(),
					"\n"
				})));
			}
			for (int i = 0; i < di.Breaks.Count; i++)
			{
				bool flag = false;
				for (int j = 0; j < di2.Breaks.Count; j++)
				{
					if (di.Breaks[i].Equals(di2.Breaks[j]))
					{
						di2.Breaks.RemoveAt(j);
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					rtP.Inlines.Add(new Run("Break added: " + di.Breaks[i].ToString() + "\n"));
				}
			}
			for (int k = 0; k < di2.Breaks.Count; k++)
			{
				rtP.Inlines.Add(new Run("Break deleted: " + di2.Breaks[k].ToString() + "\n"));
			}
			for (int l = 0; l < di.ComboColors.Count; l++)
			{
				if (di2.ComboColors.Count > l)
				{
					if (!di.ComboColors[l].Equals(di2.ComboColors[l]))
					{
						rtP.Inlines.Add(new Run(string.Concat(new string[]
						{
							"Combo color ",
							(l + 1).ToString(),
							" changed:\t",
							di2.ComboColors[l].ToString(),
							" -> ",
							di.ComboColors[l].ToString(),
							"\n"
						})));
					}
				}
				else
				{
					rtP.Inlines.Add(new Run(string.Concat(new string[]
					{
						"Combo color ",
						(l + 1).ToString(),
						" added:\t",
						di.ComboColors[l].ToString(),
						"\n"
					})));
				}
			}
			if (di2.ComboColors.Count > di.ComboColors.Count)
			{
				for (int m = di.ComboColors.Count; m < di2.ComboColors.Count; m++)
				{
					rtP.Inlines.Add(new Run(string.Concat(new string[]
					{
						"Combo color ",
						(m + 1).ToString(),
						" deleted:\t",
						di2.ComboColors[m].ToString(),
						"\n"
					})));
				}
			}
			for (int n = 0; n < di.TimingPoints.Count; n++)
			{
				bool flag2 = false;
				for (int num = di2.TimingPoints.Count - 1; num > -1; num--)
				{
					if (di.TimingPoints[n].time == di2.TimingPoints[num].time && ((di.TimingPoints[n].unInherited && di2.TimingPoints[num].unInherited) || (!di.TimingPoints[n].unInherited && !di2.TimingPoints[num].unInherited)))
					{
						Snapshot.CompareTimingPoint(di.TimingPoints[n], di2.TimingPoints[num], rtP);
						di2.TimingPoints.RemoveAt(num);
						flag2 = true;
					}
				}
				if (!flag2)
				{
					rtP.Inlines.Add(new Run("Timing point added: "));
					rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + di.TimingPoints[n].ToString(), di.TimingPoints[n].ToString()));
					rtP.Inlines.Add(new Run("\n"));
				}
			}
			for (int num2 = 0; num2 < di2.TimingPoints.Count; num2++)
			{
				rtP.Inlines.Add(new Run("Timing point deleted: "));
				rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + di2.TimingPoints[num2].ToString(), di2.TimingPoints[num2].ToString()));
				rtP.Inlines.Add(new Run("\n"));
			}
			for (int num3 = 0; num3 < di.Objects.Count; num3++)
			{
				bool flag3 = false;
				for (int num4 = di2.Objects.Count - 1; num4 > -1; num4--)
				{
					if (di.Objects[num3].StartTime == di2.Objects[num4].StartTime && ((di.Objects[num3].IsType(HitObjectType.Normal) && di2.Objects[num4].IsType(HitObjectType.Normal)) || (di.Objects[num3].IsType(HitObjectType.Slider) && di2.Objects[num4].IsType(HitObjectType.Slider)) || (di.Objects[num3].IsType(HitObjectType.Spinner) && di2.Objects[num4].IsType(HitObjectType.Spinner))))
					{
						Snapshot.CompareObjects(di.Objects[num3], di2.Objects[num4], rtP, di.DifficlutyMode);
						di2.Objects.RemoveAt(num4);
						flag3 = true;
					}
				}
				if (!flag3)
				{
					rtP.Inlines.Add(new Run(Snapshot.GetObjectTypeString(di.Objects[num3], di.DifficlutyMode)));
					rtP.Inlines.Add(new Run(" added: "));
					rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + di.Objects[num3].ToString(), di.Objects[num3].ToString()));
					if (di.DifficlutyMode == DifficlutyModeType.Taiko)
					{
						rtP.Inlines.Add(new Run(" " + di.Objects[num3].TaikoStr));
					}
					rtP.Inlines.Add(new Run("\n"));
				}
			}
			for (int num5 = 0; num5 < di2.Objects.Count; num5++)
			{
				rtP.Inlines.Add(new Run(Snapshot.GetObjectTypeString(di2.Objects[num5], di2.DifficlutyMode)));
				rtP.Inlines.Add(new Run(" deleted: "));
				rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + di2.Objects[num5].ToString(), di2.Objects[num5].ToString()));
				rtP.Inlines.Add(new Run("\n"));
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00009EF8 File Offset: 0x000080F8
		private static void CompareObjects(HitObject t1, HitObject t2, Paragraph rtP, DifficlutyModeType mode)
		{
			switch (mode)
			{
			case DifficlutyModeType.Standard:
			case DifficlutyModeType.CtB:
				if (t1.IsType(HitObjectType.Normal) || t1.IsType(HitObjectType.Slider))
				{
					if (t1.Position != t2.Position)
					{
						rtP.Inlines.Add(new Run(Snapshot.GetObjectTypeString(t1, mode) + " "));
						rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
						rtP.Inlines.Add(new Run(string.Concat(new string[]
						{
							" position changed: ",
							t2.Position.ToString(),
							" -> ",
							t1.Position.ToString(),
							"\n"
						})));
					}
					if (t1.NewCombo != t2.NewCombo)
					{
						rtP.Inlines.Add(new Run(Snapshot.GetObjectTypeString(t1, mode) + " "));
						rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
						if (t1.NewCombo)
						{
							rtP.Inlines.Add(new Run(" new combo added\n"));
						}
						else
						{
							rtP.Inlines.Add(new Run(" new combo removed\n"));
						}
					}
				}
				if (t1.HitSound != t2.HitSound)
				{
					rtP.Inlines.Add(new Run(Snapshot.GetObjectTypeString(t1, mode) + " "));
					rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
					rtP.Inlines.Add(new Run(string.Concat(new string[]
					{
						" hitsound changed: ",
						t2.HitSound.ToString(),
						" -> ",
						t1.HitSound.ToString(),
						"\n"
					})));
				}
				if (t1.Additions.mainSet != t2.Additions.mainSet)
				{
					rtP.Inlines.Add(new Run(Snapshot.GetObjectTypeString(t1, mode) + " "));
					rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
					rtP.Inlines.Add(new Run(string.Concat(new string[]
					{
						" sampleset changed: ",
						t2.Additions.mainSet.ToString(),
						" -> ",
						t1.Additions.mainSet.ToString(),
						"\n"
					})));
				}
				if (t1.Additions.secondarySet != t2.Additions.secondarySet)
				{
					rtP.Inlines.Add(new Run(Snapshot.GetObjectTypeString(t1, mode) + " "));
					rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
					rtP.Inlines.Add(new Run(string.Concat(new string[]
					{
						" additions changed: ",
						t2.Additions.secondarySet.ToString(),
						" -> ",
						t1.Additions.secondarySet.ToString(),
						"\n"
					})));
				}
				if ((t1.IsType(HitObjectType.Spinner) || t1.IsType(HitObjectType.Slider)) && t1.EndTime != t2.EndTime)
				{
					rtP.Inlines.Add(new Run(Snapshot.GetObjectTypeString(t1, mode) + " "));
					rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
					rtP.Inlines.Add(new Run(string.Concat(new string[]
					{
						" end time changed: ",
						t2.EndTimeStr,
						" -> ",
						t1.EndTimeStr,
						"\n"
					})));
				}
				if (t1.IsType(HitObjectType.Slider))
				{
					if (t1.SpatialLength != t2.SpatialLength)
					{
						rtP.Inlines.Add(new Run("Slider "));
						rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
						rtP.Inlines.Add(new Run(string.Concat(new string[]
						{
							" pixel length changed: ",
							t2.SpatialLength.ToString(),
							" -> ",
							t1.SpatialLength.ToString(),
							"\n"
						})));
					}
					if (t1.SegmentCount != t2.SegmentCount)
					{
						rtP.Inlines.Add(new Run("Slider "));
						rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
						rtP.Inlines.Add(new Run(string.Concat(new string[]
						{
							" repeats count changed: ",
							(t2.SegmentCount - 1).ToString(),
							" -> ",
							(t1.SegmentCount - 1).ToString(),
							"\n"
						})));
					}
					Snapshot.XYCompare(t1, t2, rtP);
					Snapshot.EdgesHSCompare(t1, t2, rtP);
					Snapshot.EdgesAddCompare(t1, t2, rtP);
					return;
				}
				break;
			case DifficlutyModeType.Taiko:
				if (t1.IsType(HitObjectType.Normal) && (t1.isDon != t2.isDon || t1.Finish != t2.Finish))
				{
					rtP.Inlines.Add(new Run("Note "));
					rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
					rtP.Inlines.Add(new Run(string.Concat(new string[]
					{
						" changed: ",
						t2.TaikoStr,
						" -> ",
						t1.TaikoStr,
						"\n"
					})));
				}
				if (t1.IsType(HitObjectType.Slider))
				{
					if (t1.Finish != t2.Finish)
					{
						rtP.Inlines.Add(new Run("Drumroll "));
						rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
						rtP.Inlines.Add(new Run(string.Concat(new string[]
						{
							" changed: ",
							t2.TaikoStr,
							" -> ",
							t1.TaikoStr,
							"\n"
						})));
					}
					if (t1.EndTime != t2.EndTime)
					{
						rtP.Inlines.Add(new Run("Drumroll "));
						rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
						rtP.Inlines.Add(new Run(string.Concat(new string[]
						{
							" end time changed: ",
							t2.EndTimeStr,
							" -> ",
							t1.EndTimeStr,
							"\n"
						})));
					}
				}
				if (t1.IsType(HitObjectType.Spinner) && t1.EndTime != t2.EndTime)
				{
					rtP.Inlines.Add(new Run("Shaker "));
					rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
					rtP.Inlines.Add(new Run(string.Concat(new string[]
					{
						" end time changed: ",
						t2.EndTimeStr,
						" -> ",
						t1.EndTimeStr,
						"\n"
					})));
				}
				break;
			case DifficlutyModeType.Mania:
				break;
			default:
				return;
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000A740 File Offset: 0x00008940
		private static void EdgesAddCompare(HitObject t1, HitObject t2, Paragraph rtP)
		{
			if (t1.EdgesHS_Additions.Count != t2.EdgesHS_Additions.Count)
			{
				rtP.Inlines.Add(new Run("Slider "));
				rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
				rtP.Inlines.Add(new Run(" edges count changed, edges addittions comparison aborted for this slider\n"));
				return;
			}
			for (int i = 0; i < t1.EdgesHS_Additions.Count; i++)
			{
				if (t1.EdgesHS_Additions[i].mainSet != t2.EdgesHS_Additions[i].mainSet)
				{
					rtP.Inlines.Add(new Run("Slider "));
					rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
					rtP.Inlines.Add(new Run(string.Concat(new string[]
					{
						" edge #",
						(i + 1).ToString(),
						" sampleset changed: ",
						t2.EdgesHS_Additions[i].mainSet.ToString(),
						" -> ",
						t1.EdgesHS_Additions[i].mainSet.ToString(),
						"\n"
					})));
				}
				if (t1.EdgesHS_Additions[i].secondarySet != t2.EdgesHS_Additions[i].secondarySet)
				{
					rtP.Inlines.Add(new Run("Slider "));
					rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
					rtP.Inlines.Add(new Run(string.Concat(new string[]
					{
						" edge #",
						(i + 1).ToString(),
						" addittions changed: ",
						t2.EdgesHS_Additions[i].secondarySet.ToString(),
						" -> ",
						t1.EdgesHS_Additions[i].secondarySet.ToString(),
						"\n"
					})));
				}
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000A9A4 File Offset: 0x00008BA4
		private static void EdgesHSCompare(HitObject t1, HitObject t2, Paragraph rtP)
		{
			if (t1.EdgesHS.Count != t2.EdgesHS.Count)
			{
				rtP.Inlines.Add(new Run("Slider "));
				rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
				rtP.Inlines.Add(new Run(" edges count changed, edges hitsound comparison aborted for this slider\n"));
				return;
			}
			for (int i = 0; i < t1.EdgesHS.Count; i++)
			{
				if (t1.EdgesHS[i] != t2.EdgesHS[i])
				{
					rtP.Inlines.Add(new Run("Slider "));
					rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
					rtP.Inlines.Add(new Run(string.Concat(new string[]
					{
						" edge #",
						(i + 1).ToString(),
						" hitsound changed: ",
						t2.EdgesHS[i].ToString(),
						" -> ",
						t1.EdgesHS[i].ToString(),
						"\n"
					})));
				}
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x0000AB14 File Offset: 0x00008D14
		private static void XYCompare(HitObject t1, HitObject t2, Paragraph rtP)
		{
			if (t1.sliderCurvePoints.Count != t2.sliderCurvePoints.Count)
			{
				rtP.Inlines.Add(new Run("Slider "));
				rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
				rtP.Inlines.Add(new Run(" points count changed, points comparison aborted for this slider\n"));
				return;
			}
			for (int i = 0; i < t1.sliderCurvePoints.Count; i++)
			{
				if (t1.sliderCurvePoints[i] != t2.sliderCurvePoints[i])
				{
					rtP.Inlines.Add(new Run("Slider "));
					rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t1.ToString(), t1.ToString()));
					rtP.Inlines.Add(new Run(string.Concat(new string[]
					{
						" point #",
						(i + 1).ToString(),
						" changed: ",
						t2.sliderCurvePoints[i].ToString(),
						" -> ",
						t1.sliderCurvePoints[i].ToString(),
						"\n"
					})));
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000AC88 File Offset: 0x00008E88
		private static string GetObjectTypeString(HitObject obj, DifficlutyModeType mode)
		{
			if (obj.IsType(HitObjectType.Normal))
			{
				if (mode == DifficlutyModeType.Standard)
				{
					return "Circle";
				}
				return "Note";
			}
			else
			{
				if (obj.IsType(HitObjectType.Slider))
				{
					switch (mode)
					{
					case DifficlutyModeType.Standard:
					case DifficlutyModeType.CtB:
						return "Slider";
					case DifficlutyModeType.Taiko:
						return "Drumroll";
					case DifficlutyModeType.Mania:
						return "Hold";
					}
				}
				if (obj.IsType(HitObjectType.Hold) && mode == DifficlutyModeType.Mania)
				{
					return "Hold";
				}
				if (obj.IsType(HitObjectType.Spinner))
				{
					switch (mode)
					{
					case DifficlutyModeType.Standard:
					case DifficlutyModeType.CtB:
						return "Spinner";
					case DifficlutyModeType.Taiko:
						return "Shaker";
					}
				}
				return "Hit object";
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000AD28 File Offset: 0x00008F28
		private static void CompareTimingPoint(TimingPoint t1, TimingPoint t2, Paragraph rtP)
		{
			if (t1.unInherited != t2.unInherited)
			{
				rtP.Inlines.Add(new Run("Timing point "));
				rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t2.ToString(), t2.ToString()));
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					" type changed: ",
					t2.GetTypeStr(),
					" -> ",
					t1.GetTypeStr(),
					"\n"
				})));
			}
			if (t1.kiai != t2.kiai)
			{
				rtP.Inlines.Add(new Run("Timing point "));
				rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t2.ToString(), t2.ToString()));
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					" settings changed: ",
					t2.GetSettingsStr(),
					" -> ",
					t1.GetSettingsStr(),
					"\n"
				})));
			}
			if (t1.volume != t2.volume)
			{
				rtP.Inlines.Add(new Run("Timing point "));
				rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t2.ToString(), t2.ToString()));
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					" volume level changed: ",
					t2.volume,
					" -> ",
					t1.volume,
					"\n"
				})));
			}
			if (t1.HSsetNumber != t2.HSsetNumber)
			{
				rtP.Inlines.Add(new Run("Timing point "));
				rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t2.ToString(), t2.ToString()));
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					" hitsounds set number changed: ",
					t2.HSsetNumber,
					" -> ",
					t1.HSsetNumber,
					"\n"
				})));
			}
			if (t1.HSsetType != t2.HSsetType)
			{
				rtP.Inlines.Add(new Run("Timing point "));
				rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t2.ToString(), t2.ToString()));
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					" hitsound set number changed: ",
					t2.GetHSSetStr(),
					" -> ",
					t1.GetHSSetStr(),
					"\n"
				})));
			}
			if (t1.measure != t2.measure)
			{
				rtP.Inlines.Add(new Run("Timing point "));
				rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t2.ToString(), t2.ToString()));
				rtP.Inlines.Add(new Run(string.Concat(new object[]
				{
					" measure changed: ",
					t2.measure,
					"/4 -> ",
					t1.measure,
					"/4\n"
				})));
			}
			if (t1.unInherited == t2.unInherited && t1.msPerBeat != t2.msPerBeat)
			{
				if (t1.unInherited)
				{
					rtP.Inlines.Add(new Run("Timing point "));
					rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t2.ToString(), t2.ToString()));
					rtP.Inlines.Add(new Run(string.Concat(new string[]
					{
						" bpm changed: ",
						t2.GetBpm().ToString(),
						" -> ",
						t1.GetBpm().ToString(),
						"\n"
					})));
					return;
				}
				rtP.Inlines.Add(new Run("Timing point "));
				rtP.Inlines.Add(Snapshot.CreateHL("osu://edit/" + t2.ToString(), t2.ToString()));
				rtP.Inlines.Add(new Run(string.Concat(new string[]
				{
					" SV multiplier changed: ",
					t2.GetSV().ToString(),
					" -> ",
					t1.GetSV().ToString(),
					"\n"
				})));
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x0000B208 File Offset: 0x00009408
		private static string GetHash(string strFile)
		{
			string result = string.Empty;
			try
			{
				HashAlgorithm hashAlgorithm = MD5.Create();
				FileStream fileStream = File.OpenRead(strFile);
				result = BitConverter.ToString(hashAlgorithm.ComputeHash(fileStream)).Replace("-", "").ToLower();
				fileStream.Close();
			}
			catch
			{
				return string.Empty;
			}
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000B26C File Offset: 0x0000946C
		private static void ListFiles(string directory, List<string> mainF, bool ignoreS, int rootDirLength)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(directory);
			try
			{
				foreach (FileInfo fileInfo in directoryInfo.GetFiles("*"))
				{
					if (!(fileInfo.Extension.ToLower() == ".osu") && (!ignoreS || !(fileInfo.Name.ToLower() == "snapshot")))
					{
						string hash = Snapshot.GetHash(fileInfo.FullName);
						mainF.Add(ut_Path.Normalize(fileInfo.FullName.Remove(0, rootDirLength)) + "=" + hash);
					}
				}
			}
			catch
			{
			}
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			for (int i = 0; i < directories.Length; i++)
			{
				Snapshot.ListFiles(ut_Path.Normalize(directories[i].FullName), mainF, ignoreS, rootDirLength);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000B344 File Offset: 0x00009544
		private static Hyperlink CreateHL(string url, string name)
		{
			Hyperlink hyperlink = new Hyperlink();
			hyperlink.IsEnabled = true;
			hyperlink.Inlines.Add(name);
			hyperlink.NavigateUri = new Uri(url);
			hyperlink.RequestNavigate += delegate(object sender, RequestNavigateEventArgs args)
			{
				Process.Start(args.Uri.ToString());
			};
			return hyperlink;
		}
	}
}
