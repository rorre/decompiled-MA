using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Navigation;
using Modding_assistant.Utility;
using osu;
using osu.Beatmap;
using osu.Processor;

namespace Modding_assistant.Report
{
	// Token: 0x02000015 RID: 21
	internal static class RunReport
	{
		// Token: 0x06000090 RID: 144 RVA: 0x0000B5A0 File Offset: 0x000097A0
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

		// Token: 0x06000091 RID: 145 RVA: 0x0000B5F8 File Offset: 0x000097F8
		public static Report CtBHyper(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			report.DifficultyIndex = diffIndex;
			report.Description = new Paragraph();
			new List<int>();
			if (bi.Difficluties[diffIndex].DifficlutyMode != DifficlutyModeType.CtB)
			{
				return report;
			}
			for (int i = 0; i < bi.Difficluties[diffIndex].HyperdashesList.Count; i++)
			{
				TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)bi.Difficluties[diffIndex].HyperdashesList[i]);
				string text = string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
				report.Description.Inlines.Add(RunReport.CreateHL("osu://edit/" + text, text));
				report.Description.Inlines.Add("\n");
			}
			if (bi.Difficluties[diffIndex].HyperdashesList.Count > 0)
			{
				report.Brief = Strings.CtBHyper_Brief;
			}
			return report;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000B718 File Offset: 0x00009918
		public static Report CtBSpinnerRecoveryTime(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			report.DifficultyIndex = diffIndex;
			report.Description = new Paragraph();
			List<float> list = new List<float>();
			if (!bi.Difficluties[diffIndex].isSpinner)
			{
				return report;
			}
			if (bi.Difficluties[diffIndex].DifficlutyMode != DifficlutyModeType.CtB)
			{
				return report;
			}
			for (int i = 0; i < bi.Difficluties[diffIndex].Objects.Count - 1; i++)
			{
				if (bi.Difficluties[diffIndex].Objects[i].IsType(HitObjectType.Spinner))
				{
					int endTime = bi.Difficluties[diffIndex].Objects[i].EndTime;
					double startTime = (double)bi.Difficluties[diffIndex].Objects[i + 1].StartTime;
					int bpmtimeLineIndex = ObjectsProcessor.getBPMTimeLineIndex(bi.Difficluties[diffIndex], endTime);
					double msPerBeat = bi.Difficluties[diffIndex].TimingPoints[bpmtimeLineIndex].msPerBeat;
					if ((startTime - (double)endTime) / msPerBeat <= 0.48)
					{
						list.Add((float)i);
					}
				}
			}
			int count = list.Count;
			for (int j = 0; j < list.Count; j++)
			{
				string text = bi.Difficluties[diffIndex].Objects[Convert.ToInt32(list[j])].ToString();
				report.Description.Inlines.Add(RunReport.CreateHL("osu://edit/" + text, text));
				report.Description.Inlines.Add("\n");
			}
			if (list.Count > 0)
			{
				report.Brief = Strings.CtBSpinnerRecoveryTime_Brief;
				report.Description.Inlines.Add(new Run("\n"));
				report.Description.Inlines.Add(RunReport.CreateHL(Strings.CtBSpinnerRecoveryTime_HLurl, Strings.CtBSpinnerRecoveryTime_HL));
				report.Description.Inlines.Add(new Bold(new Run("\n" + Strings.CtBSpinnerRecoveryTime_Desc2 + " ")));
				report.Description.Inlines.Add(new Run(Strings.CtBSpinnerRecoveryTime_Desc3));
			}
			return report;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000B968 File Offset: 0x00009B68
		public static Report SpinnerMaxiumLength(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			report.DifficultyIndex = diffIndex;
			report.Description = new Paragraph();
			List<float> list = new List<float>();
			if (!bi.Difficluties[diffIndex].isSpinner)
			{
				return report;
			}
			if (bi.Difficluties[diffIndex].DifficlutyMode != DifficlutyModeType.Standard)
			{
				return report;
			}
			for (int i = 0; i < bi.Difficluties[diffIndex].Objects.Count; i++)
			{
				if (bi.Difficluties[diffIndex].Objects[i].IsType(HitObjectType.Spinner))
				{
					int totalLength = bi.Difficluties[diffIndex].Objects[i].TotalLength;
					if (bi.Difficluties[diffIndex].Objects[i].TotalLength >= 7000)
					{
						list.Add((float)i);
					}
				}
			}
			for (int j = 0; j < list.Count; j++)
			{
				string text = bi.Difficluties[diffIndex].Objects[Convert.ToInt32(list[j])].ToString();
				report.Description.Inlines.Add(RunReport.CreateHL("osu://edit/" + text, text));
				report.Description.Inlines.Add(" " + bi.Difficluties[diffIndex].Objects[Convert.ToInt32(list[j])].TotalLength + "ms");
				report.Description.Inlines.Add("\n");
			}
			if (list.Count > 0)
			{
				report.Brief = Strings.SpinnerMaxiumLength_Brief;
				report.Description.Inlines.Add(new Run("\n"));
				report.Description.Inlines.Add(RunReport.CreateHL(Strings.SpinnerMaxiumLength_HLurl, Strings.SpinnerPresence_HL));
				report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.SpinnerMaxiumLength_Desc2 + " ")));
				report.Description.Inlines.Add(new Run(Strings.SpinnerMaxiumLength_Desc3));
			}
			return report;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000BBB4 File Offset: 0x00009DB4
		public static Report SpinnerRecoveryTime(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			report.DifficultyIndex = diffIndex;
			report.Description = new Paragraph();
			List<double> list = new List<double>();
			if (!bi.Difficluties[diffIndex].isSpinner)
			{
				return report;
			}
			if (bi.Difficluties[diffIndex].DifficlutyMode != DifficlutyModeType.Standard)
			{
				return report;
			}
			for (int i = 0; i < bi.Difficluties[diffIndex].Objects.Count - 1; i++)
			{
				if (bi.Difficluties[diffIndex].Objects[i].IsType(HitObjectType.Spinner))
				{
					int endTime = bi.Difficluties[diffIndex].Objects[i].EndTime;
					int startTime = bi.Difficluties[diffIndex].Objects[i + 1].StartTime;
					int bpmtimeLineIndex = ObjectsProcessor.getBPMTimeLineIndex(bi.Difficluties[diffIndex], endTime);
					double msPerBeat = bi.Difficluties[diffIndex].TimingPoints[bpmtimeLineIndex].msPerBeat;
					double num = (double)(startTime - endTime) / msPerBeat;
					if ((double)bi.Difficluties[diffIndex].strains.TotalSR <= 1.6 && num < 3.9)
					{
						list.Add((double)i);
						list.Add(num);
						list.Add((double)(startTime - endTime));
					}
					else if ((double)bi.Difficluties[diffIndex].strains.TotalSR <= 2.4 && num < 1.9)
					{
						list.Add((double)i);
						list.Add(num);
						list.Add((double)(startTime - endTime));
					}
					else if ((double)bi.Difficluties[diffIndex].strains.TotalSR <= 3.75 && num < 0.9)
					{
						list.Add((double)i);
						list.Add(num);
						list.Add((double)(startTime - endTime));
					}
				}
			}
			if (list.Count > 0)
			{
				report.Brief = Strings.SpinnerRecoveryTime_Brief;
				report.Description.Inlines.Add(new Italic(new Run(Strings.SpinnerRecoveryTime_Desc1 + "\n")));
			}
			for (int j = 0; j < list.Count - 2; j += 3)
			{
				string text = bi.Difficluties[diffIndex].Objects[Convert.ToInt32(list[j])].ToString();
				report.Description.Inlines.Add(RunReport.CreateHL("osu://edit/" + text, text));
				report.Description.Inlines.Add(string.Concat(new object[]
				{
					" ",
					Math.Round(list[j + 1], 2),
					"beats ",
					list[j + 2],
					"ms"
				}));
				report.Description.Inlines.Add("\n");
			}
			if (list.Count > 0)
			{
				report.Description.Inlines.Add(new Bold(new Run("\n" + Strings.SpinnerRecoveryTime_Desc2 + " ")));
				report.Description.Inlines.Add(new Run(Strings.SpinnerRecoveryTime_Desc3));
			}
			return report;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x0000BF2C File Offset: 0x0000A12C
		public static Report SpinnerPresence(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.DifficultyIndex = diffIndex;
			report.Level = ReportLevel.Difficulty;
			if (bi.Difficluties[diffIndex].DifficlutyMode != DifficlutyModeType.Standard)
			{
				return report;
			}
			if (bi.Difficluties[diffIndex].isSpinner)
			{
				return report;
			}
			report.Brief = Strings.SpinnerPresence_Brief;
			report.DifficultyIndex = diffIndex;
			report.Description = new Paragraph();
			report.Description.Inlines.Add(RunReport.CreateHL(Strings.SpinnerPresence_HLurl, Strings.SpinnerPresence_HL));
			report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.SpinnerPresence_Desc2 + " ")));
			report.Description.Inlines.Add(new Run(Strings.SpinnerPresence_Desc3));
			return report;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000C004 File Offset: 0x0000A204
		public static Report SpinnerMinimumLength(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			report.DifficultyIndex = diffIndex;
			report.Description = new Paragraph();
			List<double> list = new List<double>();
			if (!bi.Difficluties[diffIndex].isSpinner)
			{
				return report;
			}
			if (bi.Difficluties[diffIndex].DifficlutyMode != DifficlutyModeType.Standard)
			{
				return report;
			}
			for (int i = 0; i < bi.Difficluties[diffIndex].Objects.Count; i++)
			{
				if (bi.Difficluties[diffIndex].Objects[i].IsType(HitObjectType.Spinner))
				{
					int bpmtimeLineIndex = ObjectsProcessor.getBPMTimeLineIndex(bi.Difficluties[diffIndex], bi.Difficluties[diffIndex].Objects[i].StartTime);
					double msPerBeat = bi.Difficluties[diffIndex].TimingPoints[bpmtimeLineIndex].msPerBeat;
					double num = (double)bi.Difficluties[diffIndex].Objects[i].TotalLength / msPerBeat;
					if ((double)bi.Difficluties[diffIndex].strains.TotalSR <= 1.6 && (num < 3.9 || bi.Difficluties[diffIndex].Objects[i].TotalLength < 1000))
					{
						list.Add((double)i);
						list.Add(num);
					}
					else if ((double)bi.Difficluties[diffIndex].strains.TotalSR <= 2.4 && (num < 2.9 || bi.Difficluties[diffIndex].Objects[i].TotalLength < 750))
					{
						list.Add((double)i);
						list.Add(num);
					}
					else if ((double)bi.Difficluties[diffIndex].strains.TotalSR <= 3.75 && (num < 1.9 || bi.Difficluties[diffIndex].Objects[i].TotalLength < 600))
					{
						list.Add((double)i);
						list.Add(num);
					}
					else if (num < 1.9 || bi.Difficluties[diffIndex].Objects[i].TotalLength < 600)
					{
						list.Add((double)i);
						list.Add(num);
					}
				}
			}
			if (list.Count > 0)
			{
				report.Brief = Strings.SpinnerMinimumLength_Brief;
				report.Description.Inlines.Add(new Italic(new Run(Strings.SpinnerMinimumLength_Desc1 + "\n")));
			}
			for (int j = 0; j < list.Count - 1; j += 2)
			{
				string text = bi.Difficluties[diffIndex].Objects[Convert.ToInt32(list[j])].ToString();
				report.Description.Inlines.Add(RunReport.CreateHL("osu://edit/" + text, text));
				report.Description.Inlines.Add(string.Concat(new object[]
				{
					" ",
					Math.Round(list[j + 1], 2),
					"beats ",
					bi.Difficluties[diffIndex].Objects[Convert.ToInt32(list[j])].TotalLength,
					"ms"
				}));
				report.Description.Inlines.Add("\n");
			}
			return report;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		public static Report OffscreenObjects(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			List<int> list = new List<int>();
			report.Description = new Paragraph();
			report.Level = ReportLevel.Difficulty;
			report.DifficultyIndex = diffIndex;
			if (bi.Difficluties[diffIndex].DifficlutyMode == DifficlutyModeType.Standard)
			{
				int num = Convert.ToInt32(Math.Ceiling((double)((10f - bi.Difficluties[diffIndex].CircleSize) * 8f))) + 24;
				num = Convert.ToInt32(Math.Ceiling((double)((float)num / 2f)));
				for (int i = 0; i < bi.Difficluties[diffIndex].Objects.Count; i++)
				{
					if (bi.Difficluties[diffIndex].Objects[i].IsType(HitObjectType.Normal))
					{
						if (bi.Difficluties[diffIndex].Objects[i].Position.X - (float)num <= -66f || bi.Difficluties[diffIndex].Objects[i].Position.X + (float)num >= 578f || bi.Difficluties[diffIndex].Objects[i].Position.Y - (float)num <= -58f || bi.Difficluties[diffIndex].Objects[i].Position.Y + (float)num >= 426f)
						{
							list.Add(i);
						}
					}
					else if (bi.Difficluties[diffIndex].Objects[i].IsType(HitObjectType.Slider))
					{
						if (bi.Difficluties[diffIndex].Objects[i].Position.X - (float)num <= -66f || bi.Difficluties[diffIndex].Objects[i].Position.X + (float)num >= 578f || bi.Difficluties[diffIndex].Objects[i].Position.Y - (float)num <= -58f || bi.Difficluties[diffIndex].Objects[i].Position.Y + (float)num >= 426f)
						{
							list.Add(i);
						}
						else
						{
							for (int j = 0; j < bi.Difficluties[diffIndex].Objects[i].curve.curve.Count; j++)
							{
								if (bi.Difficluties[diffIndex].Objects[i].curve.curve[j].X - (float)num <= -66f || bi.Difficluties[diffIndex].Objects[i].curve.curve[j].X + (float)num >= 578f || bi.Difficluties[diffIndex].Objects[i].curve.curve[j].Y - (float)num <= -58f || bi.Difficluties[diffIndex].Objects[i].curve.curve[j].Y + (float)num >= 426f)
								{
									list.Add(i);
									break;
								}
							}
						}
					}
				}
				if (list.Count > 0)
				{
					report.Brief = Strings.OffscreenObjects_Brief;
					report.Description.Inlines.Add(new Italic(new Run(Strings.OffscreenObjects_Desc1 + "\n")));
					for (int k = 0; k < list.Count; k++)
					{
						string text = bi.Difficluties[diffIndex].Objects[list[k]].ToString();
						report.Description.Inlines.Add(RunReport.CreateHL("osu://edit/" + text, text));
						report.Description.Inlines.Add("\n");
					}
					report.Description.Inlines.Add("\n");
					report.Description.Inlines.Add(RunReport.CreateHL(Strings.OffscreenObjects_HLurl, Strings.OffscreenObjects_HL));
					report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.OffscreenObjects_Desc2 + " ")));
					report.Description.Inlines.Add(new Run(Strings.OffscreenObjects_Desc3));
				}
			}
			return report;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000C890 File Offset: 0x0000AA90
		public static Report StoryboardedHitsounds(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Description = new Paragraph();
			report.Level = ReportLevel.Global;
			string str = string.Empty;
			List<string> list = new List<string>();
			Func<string, bool> <>9__0;
			for (int i = 0; i < bi.Difficluties.Count; i++)
			{
				for (int j = 0; j < bi.Difficluties[i].StoryboardFilesList.Count; j++)
				{
					string extension = Path.GetExtension(bi.Difficluties[i].StoryboardFilesList[j]);
					if (string.Compare(extension, ".wav", true) == 0 || string.Compare(extension, ".mp3", true) == 0 || string.Compare(extension, ".ogg", true) == 0)
					{
						str = bi.Difficluties[i].StoryboardFilesList[j] + " : " + bi.Difficluties[i].Version;
						IEnumerable<string> source = list;
						Func<string, bool> predicate;
						if ((predicate = <>9__0) == null)
						{
							predicate = (<>9__0 = ((string s) => s.Equals(str, StringComparison.CurrentCultureIgnoreCase)));
						}
						if (!source.Any(predicate))
						{
							list.Add(str);
						}
					}
				}
			}
			Func<string, bool> <>9__1;
			for (int k = 0; k < bi.StoryboardUFL.Count; k++)
			{
				string extension2 = Path.GetExtension(bi.StoryboardUFL[k]);
				if (string.Compare(extension2, ".wav", true) == 0 || string.Compare(extension2, ".mp3", true) == 0 || string.Compare(extension2, ".ogg", true) == 0)
				{
					str = bi.StoryboardUFL[k] + " : .osb";
					IEnumerable<string> source2 = list;
					Func<string, bool> predicate2;
					if ((predicate2 = <>9__1) == null)
					{
						predicate2 = (<>9__1 = ((string s) => s.Equals(str, StringComparison.CurrentCultureIgnoreCase)));
					}
					if (!source2.Any(predicate2))
					{
						list.Add(str);
					}
				}
			}
			if (list.Count > 0)
			{
				report.Brief = Strings.StoryboardedHitsounds_Brief;
				report.Description.Inlines.Add(new Italic(new Run(Strings.StoryboardedHitsounds_Desc1 + ":\n")));
			}
			for (int l = 0; l < list.Count; l++)
			{
				report.Description.Inlines.Add(new Run(list[l] + "\n"));
			}
			if (list.Count > 0)
			{
				report.Description.Inlines.Add("\n");
				report.Description.Inlines.Add(RunReport.CreateHL(Strings.StoryboardedHitsounds_HLurl, Strings.StoryboardedHitsounds_HL));
				report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.StoryboardedHitsounds_Desc2 + " ")));
				report.Description.Inlines.Add(new Run(Strings.StoryboardedHitsounds_Desc3));
			}
			return report;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000CB84 File Offset: 0x0000AD84
		public static Report EndTimeConsistency(BeatmapInfo bi, int diffIndex)
		{
			bool flag = true;
			Report report = default(Report);
			report.Level = ReportLevel.Global;
			for (int i = 1; i < bi.Difficluties.Count; i++)
			{
				if (!Timing.isEqualTiming(bi.Difficluties[0].EndTime, bi.Difficluties[i].EndTime, 5))
				{
					flag = false;
				}
			}
			if (!flag)
			{
				report.Brief = Strings.EndTimeConsistency_Brief;
				report.Description = new Paragraph();
				report.Description.Inlines.Add(new Italic(new Run(Strings.EndTimeConsistency_Desc1 + "\n")));
				for (int j = 0; j < bi.Difficluties.Count; j++)
				{
					TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)bi.Difficluties[j].EndTime);
					string str = string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
					report.Description.Inlines.Add(str + " - ");
					report.Description.Inlines.Add(new Run(bi.Difficluties[j].Version + "\n"));
				}
				report.Description.Inlines.Add("\n");
				report.Description.Inlines.Add(RunReport.CreateHL(Strings.EndTimeConsistency_HLurl, Strings.EndTimeConsistency_HL));
				report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.EndTimeConsistency_Desc2 + " ")));
				report.Description.Inlines.Add(new Run(Strings.EndTimeConsistency_Desc3));
			}
			return report;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000CD5C File Offset: 0x0000AF5C
		public static Report HSDelay(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Global;
			report.Description = new Paragraph();
			bool flag = true;
			for (int i = 0; i < bi.SoundFiles.Count; i++)
			{
				if (bi.SoundFiles[i].Type == AFType.wav && bi.SoundFiles[i].isDelay)
				{
					if (flag)
					{
						report.Brief = Strings.HSDelay_Brief;
						report.Description.Inlines.Add(new Italic(new Run(Strings.HSDelay_Desc3 + "\n")));
						flag = false;
					}
					report.Description.Inlines.Add(new Run(bi.SoundFiles[i].rFilePath + "\n"));
				}
			}
			report.Description.Inlines.Add(new Run("\n"));
			report.Description.Inlines.Add(RunReport.CreateHL(Strings.HSDelay_HLurl, Strings.HSDelay_HL));
			report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.HSDelay_Desc1 + " ")));
			report.Description.Inlines.Add(new Run(Strings.HSDelay_Desc2));
			return report;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000CEB8 File Offset: 0x0000B0B8
		public static Report HSLength(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Global;
			report.Description = new Paragraph();
			bool flag = true;
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			for (int i = 0; i < bi.SoundFiles.Count; i++)
			{
				if (bi.SoundFiles[i].Type == AFType.wav)
				{
					if (bi.SoundFiles[i].Length > 5f && bi.SoundFiles[i].Length < 100f)
					{
						list2.Add(bi.SoundFiles[i].rFilePath + " " + Convert.ToString(Math.Round((double)bi.SoundFiles[i].Length, 2)) + "ms");
					}
					if (bi.SoundFiles[i].Length > 0f && bi.SoundFiles[i].Length <= 5f)
					{
						list.Add(bi.SoundFiles[i].rFilePath + " " + Convert.ToString(Math.Round((double)bi.SoundFiles[i].Length, 2)) + "ms");
					}
				}
			}
			for (int j = 0; j < list2.Count; j++)
			{
				if (flag)
				{
					report.Brief = Strings.HSLength_Brief;
					report.Description.Inlines.Add(new Italic(new Run(Strings.HSLength_Desc1 + "\n")));
					flag = false;
				}
				report.Description.Inlines.Add(new Run(list2[j] + "\n"));
			}
			flag = true;
			for (int k = 0; k < list.Count; k++)
			{
				if (flag)
				{
					report.Brief = Strings.HSLength_Brief;
					if (report.Description.Inlines.Count > 0)
					{
						report.Description.Inlines.Add(new Run("\n"));
					}
					report.Description.Inlines.Add(new Italic(new Run(Strings.HSLength_Desc2 + "\n")));
					flag = false;
				}
				report.Description.Inlines.Add(new Run(list[k] + "\n"));
			}
			report.Description.Inlines.Add(new Run("\n"));
			report.Description.Inlines.Add(RunReport.CreateHL(Strings.HSLength_HLurl, Strings.HSLength_HL));
			report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.HSLength_Desc3 + " ")));
			report.Description.Inlines.Add(new Run(Strings.HSLength_Desc4));
			return report;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		public static Report Mp3Bitrate(BeatmapInfo bi, int diffIndex)
		{
			RunReport.<>c__DisplayClass13_0 CS$<>8__locals1 = new RunReport.<>c__DisplayClass13_0();
			CS$<>8__locals1.bi = bi;
			Report report = default(Report);
			report.Level = ReportLevel.Global;
			report.Description = new Paragraph();
			bool flag = true;
			List<string> list = new List<string>();
			int i;
			int k;
			for (i = 0; i < CS$<>8__locals1.bi.Difficluties.Count; i = k + 1)
			{
				if (!list.Any((string s) => s.Equals(CS$<>8__locals1.bi.Difficluties[i].AudioFilename, StringComparison.CurrentCultureIgnoreCase)))
				{
					list.Add(CS$<>8__locals1.bi.Difficluties[i].AudioFilename);
				}
				k = i;
			}
			for (int j = 0; j < list.Count; j++)
			{
				int mp3Bitrate = AudioFile.GetMp3Bitrate(Path.Combine(CS$<>8__locals1.bi.HomeDirectory, list[j]));
				if (mp3Bitrate > 192)
				{
					if (flag)
					{
						report.Description.Inlines.Add(new Italic(new Run(Strings.Mp3Bitrate_Desc1 + "\n")));
						flag = false;
					}
					report.Description.Inlines.Add(new Run(CS$<>8__locals1.bi.Difficluties[j].AudioFilename + " " + Convert.ToString(mp3Bitrate) + "kbps \n\n"));
				}
			}
			if (!flag)
			{
				report.Brief = Strings.Mp3Bitrate_Brief;
				report.Description.Inlines.Add(RunReport.CreateHL(Strings.Mp3Bitrate_HLurl, Strings.Mp3Bitrate_HL));
				report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.Mp3Bitrate_Desc2 + " ")));
				report.Description.Inlines.Add(new Run(Strings.Mp3Bitrate_Desc3));
			}
			return report;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000D3AC File Offset: 0x0000B5AC
		public static Report UnusedFiles(BeatmapInfo bi, int diffIndex)
		{
			RunReport.<>c__DisplayClass14_0 CS$<>8__locals1 = new RunReport.<>c__DisplayClass14_0();
			CS$<>8__locals1.bi = bi;
			Report report = default(Report);
			report.Level = ReportLevel.Global;
			report.Description = new Paragraph();
			List<string> list = new List<string>();
			bool flag = true;
			for (int l = 0; l < CS$<>8__locals1.bi.ImageFiles.Count; l++)
			{
				list.Add(CS$<>8__locals1.bi.ImageFiles[l]);
			}
			for (int j = 0; j < CS$<>8__locals1.bi.OtherFiles.Count; j++)
			{
				list.Add(CS$<>8__locals1.bi.OtherFiles[j]);
			}
			int m;
			int i2;
			for (m = 0; m < CS$<>8__locals1.bi.StoryboardUFL.Count; m = i2 + 1)
			{
				list.RemoveAll((string n) => n.Equals(CS$<>8__locals1.bi.StoryboardUFL[m], StringComparison.CurrentCultureIgnoreCase));
				i2 = m;
			}
			int i;
			for (i = 0; i < CS$<>8__locals1.bi.Difficluties.Count; i = i2 + 1)
			{
				list.RemoveAll((string n) => n.Equals(CS$<>8__locals1.bi.Difficluties[i].Background, StringComparison.CurrentCultureIgnoreCase));
				list.RemoveAll((string n) => n.Equals(CS$<>8__locals1.bi.Difficluties[i].Video, StringComparison.CurrentCultureIgnoreCase));
				i2 = i;
			}
			Skinning.Reduce(new List<Skinning.SkinSet>(Skinning.Init()), list);
			for (int k = 0; k < list.Count; k++)
			{
				if (flag)
				{
					report.Brief = Strings.UnusedFiles_Brief;
					flag = false;
				}
				report.Description.Inlines.Add(new Run(list[k] + "\n"));
			}
			return report;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000D58C File Offset: 0x0000B78C
		public static Report StoryboardFiles(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Global;
			report.Description = new Paragraph();
			bool flag = true;
			for (int i = 0; i < bi.StoryboardUFL.Count; i++)
			{
				if (!File.Exists(Path.Combine(bi.HomeDirectory, bi.StoryboardUFL[i])))
				{
					if (flag)
					{
						report.Brief = Strings.StoryboardFiles_Brief;
						report.Description.Inlines.Add(new Italic(new Run(Strings.StoryboardFiles_Desc1 + "\n")));
						flag = false;
					}
					report.Description.Inlines.Add(new Run(bi.StoryboardUFL[i] + "\n"));
				}
			}
			return report;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000D658 File Offset: 0x0000B858
		public static Report VideoAudioStream(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Description = new Paragraph();
			report.Level = ReportLevel.Global;
			List<string> list = new List<string>();
			for (int i = 0; i < bi.Difficluties.Count; i++)
			{
				if (bi.Difficluties[i].isVideo)
				{
					list.Add(bi.Difficluties[i].Video);
				}
			}
			list = list.Distinct<string>().ToList<string>();
			if (list.Count < 1)
			{
				return report;
			}
			MediaPlayer mediaPlayer = new MediaPlayer();
			for (int j = 0; j < list.Count; j++)
			{
				if (File.Exists(Path.Combine(bi.HomeDirectory, list[j])))
				{
					bool flag = false;
					Uri source = new Uri(Path.Combine(bi.HomeDirectory, list[j]));
					try
					{
						mediaPlayer.Open(source);
						TimeSpan t = TimeSpan.FromSeconds(3.0);
						DateTime t2 = DateTime.Now + t;
						while (DateTime.Now < t2 && !mediaPlayer.HasVideo)
						{
							Thread.Sleep(100);
						}
						flag = mediaPlayer.HasAudio;
						mediaPlayer.Close();
					}
					catch
					{
					}
					if (flag)
					{
						report.Brief = Strings.VideoAudioStream_Brief;
						report.Description.Inlines.Add(new Italic(new Run(list[j] + "\n")));
					}
				}
			}
			report.Description.Inlines.Add(new Run("\n"));
			report.Description.Inlines.Add(RunReport.CreateHL(Strings.VideoAudioStream_HLurl, Strings.VideoAudioStream_HL));
			report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.VideoAudioStream_Desc1 + " ")));
			report.Description.Inlines.Add(new Run(Strings.VideoAudioStream_Desc2));
			return report;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000D860 File Offset: 0x0000BA60
		public static Report VideoDimensions(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Description = new Paragraph();
			report.Level = ReportLevel.Global;
			List<string> list = new List<string>();
			for (int i = 0; i < bi.Difficluties.Count; i++)
			{
				if (bi.Difficluties[i].isVideo)
				{
					list.Add(bi.Difficluties[i].Video);
				}
			}
			list = list.Distinct<string>().ToList<string>();
			if (list.Count < 1)
			{
				return report;
			}
			MediaPlayer mediaPlayer = new MediaPlayer();
			for (int j = 0; j < list.Count; j++)
			{
				if (File.Exists(Path.Combine(bi.HomeDirectory, list[j])))
				{
					int num = -1;
					int num2 = -1;
					Uri source = new Uri(Path.Combine(bi.HomeDirectory, list[j]));
					try
					{
						mediaPlayer.Open(source);
						TimeSpan t = TimeSpan.FromSeconds(5.0);
						DateTime t2 = DateTime.Now + t;
						while (DateTime.Now < t2 && !mediaPlayer.HasVideo)
						{
							Thread.Sleep(100);
						}
						num = mediaPlayer.NaturalVideoHeight;
						num2 = mediaPlayer.NaturalVideoWidth;
						mediaPlayer.Close();
					}
					catch
					{
					}
					if (num2 > 1280 || num > 720)
					{
						report.Brief = Strings.VideoDimensions_Brief;
						report.Description.Inlines.Add(new Italic(new Run(list[j] + ": ")));
						report.Description.Inlines.Add(new Run(Convert.ToString(num2) + "x" + Convert.ToString(num) + "\n"));
					}
				}
			}
			report.Description.Inlines.Add(new Run("\n"));
			report.Description.Inlines.Add(RunReport.CreateHL(Strings.VideoDimensions_HLurl, Strings.VideoDimensions_HL));
			report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.VideoDimensions_Desc1 + " ")));
			report.Description.Inlines.Add(new Run(Strings.VideoDimensions_Desc2));
			return report;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x0000DAB4 File Offset: 0x0000BCB4
		public static Report ObjectsSnapping(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			if (bi.Difficluties[diffIndex].UsnappedList.Count > 0)
			{
				report.Brief = Strings.ObjectsSnapping_Brief;
				report.DifficultyIndex = diffIndex;
				report.Description = new Paragraph();
				for (int i = 0; i < bi.Difficluties[diffIndex].UsnappedList.Count; i++)
				{
					TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)bi.Difficluties[diffIndex].UsnappedList[i].time);
					string text = string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
					report.Description.Inlines.Add(RunReport.CreateHL("osu://edit/" + text, text));
					string str = string.Empty;
					switch (bi.Difficluties[diffIndex].UsnappedList[i].type)
					{
					case UnsnappedType.Circle:
						if (bi.Difficluties[diffIndex].DifficlutyMode == DifficlutyModeType.CtB)
						{
							str = "CtB score node";
						}
						else
						{
							str = "Circle";
						}
						break;
					case UnsnappedType.SliderStart:
						str = "Slider start";
						break;
					case UnsnappedType.SliderRepeat:
						str = "Slider repeat";
						break;
					case UnsnappedType.SliderEnd:
						str = "Slider end";
						break;
					case UnsnappedType.LongStart:
						str = "Long note start";
						break;
					case UnsnappedType.LongEnd:
						str = "Long note end";
						break;
					case UnsnappedType.SpinnerStart:
						str = "Spinner start";
						break;
					case UnsnappedType.SpinnerEnd:
						str = "Spinner end";
						break;
					}
					report.Description.Inlines.Add(new Run(" - " + str + "\n"));
				}
			}
			return report;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000DC80 File Offset: 0x0000BE80
		public static Report TLSnapping(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			bool flag = true;
			report.Description = new Paragraph();
			for (int i = 0; i < bi.Difficluties[diffIndex].TimingPoints.Count; i++)
			{
				if (!bi.Difficluties[diffIndex].TimingPoints[i].unInherited && !ObjectsProcessor.isSnapped(bi.Difficluties[diffIndex], bi.Difficluties[diffIndex].TimingPoints[i].time, 16, 12))
				{
					if (flag)
					{
						report.Brief = Strings.TLSnapping_Brief;
						report.DifficultyIndex = diffIndex;
						flag = false;
					}
					TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)bi.Difficluties[diffIndex].TimingPoints[i].time);
					string text = string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
					report.Description.Inlines.Add(RunReport.CreateHL("osu://edit/" + text, text));
					report.Description.Inlines.Add(new Run("\n"));
				}
			}
			return report;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000DDD4 File Offset: 0x0000BFD4
		public static Report MinimumDrainingTime(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			if (bi.Difficluties[diffIndex].TotalDrainTime < 30000)
			{
				report.Brief = Strings.MinimumDrainingTime_Brief;
				report.DifficultyIndex = diffIndex;
				report.Description = new Paragraph();
				report.Description.Inlines.Add(new Italic(new Run(Strings.MinimumDrainingTime_Desc1)));
				TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)bi.Difficluties[diffIndex].TotalDrainTime);
				string str = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
				report.Description.Inlines.Add(new Run(" " + str + "\n\n"));
				report.Description.Inlines.Add(RunReport.CreateHL(Strings.MinimumDrainingTime_HLurl, Strings.MinimumDrainingTime_HL));
				report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.MinimumDrainingTime_Desc2 + " ")));
				report.Description.Inlines.Add(new Run(Strings.MinimumDrainingTime_Desc3));
			}
			return report;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000DF20 File Offset: 0x0000C120
		public static Report DuplicateTL(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			bool flag = false;
			bool flag2 = true;
			string empty = string.Empty;
			report.DifficultyIndex = diffIndex;
			report.Description = new Paragraph();
			report.Level = ReportLevel.Difficulty;
			if (bi.Difficluties[diffIndex].TimingPoints.Count < 2)
			{
				return report;
			}
			for (int i = 0; i < bi.Difficluties[diffIndex].TimingPoints.Count - 1; i++)
			{
				RunReport.DuplicateTLType duplicateTLType = (RunReport.DuplicateTLType)0;
				int time = bi.Difficluties[diffIndex].TimingPoints[i].time;
				int time2 = bi.Difficluties[diffIndex].TimingPoints[i + 1].time;
				if (time == time2)
				{
					if (bi.Difficluties[diffIndex].TimingPoints[i].unInherited == bi.Difficluties[diffIndex].TimingPoints[i + 1].unInherited)
					{
						flag = true;
					}
					else if (i + 2 < bi.Difficluties[diffIndex].TimingPoints.Count && time == bi.Difficluties[diffIndex].TimingPoints[i + 2].time && bi.Difficluties[diffIndex].TimingPoints[i].unInherited == bi.Difficluties[diffIndex].TimingPoints[i + 2].unInherited)
					{
						duplicateTLType = RunReport.DuplicateTLType.HSSetNumber;
						flag = true;
					}
					if (bi.Difficluties[diffIndex].TimingPoints[i].unInherited != bi.Difficluties[diffIndex].TimingPoints[i + 1].unInherited)
					{
						if (bi.Difficluties[diffIndex].TimingPoints[i].HSsetNumber != bi.Difficluties[diffIndex].TimingPoints[i + 1].HSsetNumber)
						{
							duplicateTLType = RunReport.DuplicateTLType.HSSetNumber;
							flag = true;
						}
						if (bi.Difficluties[diffIndex].TimingPoints[i].HSsetType != bi.Difficluties[diffIndex].TimingPoints[i + 1].HSsetType)
						{
							duplicateTLType |= RunReport.DuplicateTLType.HSSet;
							flag = true;
						}
						if (bi.Difficluties[diffIndex].TimingPoints[i].volume != bi.Difficluties[diffIndex].TimingPoints[i + 1].volume)
						{
							duplicateTLType |= RunReport.DuplicateTLType.Volume;
							flag = true;
						}
						if (i != 0 && bi.Difficluties[diffIndex].TimingPoints[i].unInherited && bi.Difficluties[diffIndex].TimingPoints[i].kiai.HasFlag(KiaiType.Kiai) != bi.Difficluties[diffIndex].TimingPoints[i + 1].kiai.HasFlag(KiaiType.Kiai))
						{
							duplicateTLType |= RunReport.DuplicateTLType.Kiai;
							flag = true;
						}
					}
					if (flag)
					{
						flag = false;
						if (flag2)
						{
							report.Description.Inlines.Add(new Italic(new Run(Strings.DuplicateTL_Desc3 + "\n")));
							flag2 = false;
						}
						TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)bi.Difficluties[diffIndex].TimingPoints[i].time);
						string text = string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
						report.Description.Inlines.Add(RunReport.CreateHL("osu://edit/" + text, text));
						if (duplicateTLType.HasFlag(RunReport.DuplicateTLType.Volume))
						{
							report.Description.Inlines.Add(new Run(" " + Strings.DuplicateTL_T1));
						}
						if (duplicateTLType.HasFlag(RunReport.DuplicateTLType.HSSet))
						{
							report.Description.Inlines.Add(new Run(" " + Strings.DuplicateTL_T2));
						}
						if (duplicateTLType.HasFlag(RunReport.DuplicateTLType.HSSetNumber))
						{
							report.Description.Inlines.Add(new Run(" " + Strings.DuplicateTL_T3));
						}
						if (duplicateTLType.HasFlag(RunReport.DuplicateTLType.Kiai))
						{
							report.Description.Inlines.Add(new Run(" " + Strings.DuplicateTL_T4));
						}
						report.Description.Inlines.Add(new Run("\n"));
					}
				}
			}
			if (!flag2)
			{
				report.Description.Inlines.Add(new Run("\n"));
				report.Brief = Strings.DuplicateTL_Brief;
				report.Description.Inlines.Add(RunReport.CreateHL(Strings.DuplicateTL_HLurl, Strings.DuplicateTL_HL));
				report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.DuplicateTL_Desc1 + " ")));
				report.Description.Inlines.Add(new Run(Strings.DuplicateTL_Desc2 + "\n\n"));
			}
			return report;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000E480 File Offset: 0x0000C680
		public static Report ConcurentObjects(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Description = new Paragraph();
			bool flag = true;
			string empty = string.Empty;
			report.DifficultyIndex = diffIndex;
			report.Level = ReportLevel.Difficulty;
			if (bi.Difficluties[diffIndex].DifficlutyMode == DifficlutyModeType.Mania)
			{
				return report;
			}
			if (bi.Difficluties[diffIndex].Objects.Count < 2)
			{
				return report;
			}
			for (int i = 0; i < bi.Difficluties[diffIndex].Objects.Count - 1; i++)
			{
				if (bi.Difficluties[diffIndex].Objects[i + 1].StartTime - bi.Difficluties[diffIndex].Objects[i].EndTime < 11)
				{
					if (flag)
					{
						report.Description.Inlines.Add(new Italic(new Run(Strings.ConcurentObjects_Desc3 + "\n")));
						flag = false;
					}
					string empty2 = string.Empty;
					string text = bi.Difficluties[diffIndex].Objects[i].ToString();
					report.Description.Inlines.Add(RunReport.CreateHL("osu://edit/" + text, text));
					report.Description.Inlines.Add(new Run(" and "));
					text = bi.Difficluties[diffIndex].Objects[i + 1].ToString();
					report.Description.Inlines.Add(RunReport.CreateHL("osu://edit/" + text, text));
					report.Description.Inlines.Add(new Run("\n"));
				}
			}
			if (!flag)
			{
				report.Description.Inlines.Add(new Run("\n"));
				report.Brief = Strings.ConcurentObjects_Brief;
				report.Description.Inlines.Add(RunReport.CreateHL(Strings.ConcurentObjects_HLurl, Strings.ConcurentObjects_HL));
				report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.ConcurentObjects_Desc1 + " ")));
				report.Description.Inlines.Add(new Run(Strings.ConcurentObjects_Desc2 + "\n\n"));
			}
			return report;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		public static Report EpilepsyWarning(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			if (bi.Difficluties[diffIndex].EpilepsyWarning == 0 && (bi.isStoryBoard || bi.Difficluties[diffIndex].isStoryBoard))
			{
				report.Brief = Strings.EpilepsyWarning_Brief;
				report.DifficultyIndex = diffIndex;
				report.Description = new Paragraph();
				report.Description.Inlines.Add(new Italic(new Run(Strings.EpilepsyWarning_Desc1 + "\n\n")));
				report.Description.Inlines.Add(RunReport.CreateHL(Strings.EpilepsyWarning_HLurl, Strings.EpilepsyWarning_HL));
				report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.EpilepsyWarning_Desc2 + " ")));
				report.Description.Inlines.Add(new Run(Strings.EpilepsyWarning_Desc3));
			}
			return report;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		public static Report SkinPreference(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			if (!string.IsNullOrEmpty(bi.Difficluties[diffIndex].SkinPreference))
			{
				report.Brief = Strings.SkinPreference_Brief;
				report.DifficultyIndex = diffIndex;
				report.Description = new Paragraph();
				report.Description.Inlines.Add(new Italic(new Run(Strings.SkinPreference_Desc1 + " " + bi.Difficluties[diffIndex].SkinPreference + "\n\n")));
				report.Description.Inlines.Add(new Run(Strings.SkinPreference_Desc2));
			}
			return report;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000E890 File Offset: 0x0000CA90
		public static Report ImageDimensions(BeatmapInfo bi, int diffIndex)
		{
			RunReport.<>c__DisplayClass25_0 CS$<>8__locals1 = new RunReport.<>c__DisplayClass25_0();
			CS$<>8__locals1.bi = bi;
			Report report = default(Report);
			report.Level = ReportLevel.Global;
			report.Description = new Paragraph();
			bool flag = true;
			int i;
			int k;
			for (i = 0; i < CS$<>8__locals1.bi.Difficluties.Count; i = k + 1)
			{
				string filename = Path.Combine(CS$<>8__locals1.bi.HomeDirectory, CS$<>8__locals1.bi.Difficluties[i].Background);
				if (CS$<>8__locals1.bi.ImageFiles.Any((string s) => s.Equals(CS$<>8__locals1.bi.Difficluties[i].Background, StringComparison.CurrentCultureIgnoreCase)))
				{
					int num = -1;
					int num2 = -1;
					try
					{
						Image image = Image.FromFile(filename);
						num = image.Width;
						num2 = image.Height;
					}
					catch
					{
					}
					if (num > 1366 || num2 > 768)
					{
						report.Brief = Strings.ImageDimensions_Brief;
						if (flag)
						{
							report.Description.Inlines.Add(new Bold(new Run(Strings.ImageDimensions_Desc1 + "\n")));
							flag = false;
						}
						report.Description.Inlines.Add(new Run(string.Concat(new object[]
						{
							CS$<>8__locals1.bi.Difficluties[i].Version,
							": ",
							CS$<>8__locals1.bi.Difficluties[i].Background,
							" (",
							num,
							"x",
							num2,
							")\n"
						})));
					}
				}
				k = i;
			}
			if (!string.IsNullOrEmpty(report.Brief))
			{
				report.Description.Inlines.Add(new Run("\n"));
			}
			List<string> source = new List<string>(new string[]
			{
				".png",
				".jpg",
				".jpeg",
				".gif",
				".tif",
				".tiff",
				".jpe",
				".bpm"
			});
			bool flag2 = true;
			for (int j = 0; j < CS$<>8__locals1.bi.StoryboardUFL.Count; j++)
			{
				string ext = CS$<>8__locals1.bi.StoryboardUFL[j];
				try
				{
					ext = Path.GetExtension(ext);
					if (source.Any((string s) => s.Equals(ext, StringComparison.CurrentCultureIgnoreCase)) && File.Exists(Path.Combine(CS$<>8__locals1.bi.HomeDirectory, CS$<>8__locals1.bi.StoryboardUFL[j])))
					{
						int num3 = -1;
						int num4 = -1;
						try
						{
							Image image2 = Image.FromFile(Path.Combine(CS$<>8__locals1.bi.HomeDirectory, CS$<>8__locals1.bi.StoryboardUFL[j]));
							num3 = image2.Width;
							num4 = image2.Height;
						}
						catch
						{
						}
						if ((num3 > 640 || num4 > 1440) && (num3 > 1920 || num4 > 480) && (num3 > 1366 || num4 > 768))
						{
							report.Brief = Strings.ImageDimensions_Brief;
							if (flag2)
							{
								report.Description.Inlines.Add(new Bold(new Run(Strings.ImageDimensions_Desc2 + "\n")));
								flag2 = false;
							}
							report.Description.Inlines.Add(new Run(string.Concat(new object[]
							{
								CS$<>8__locals1.bi.StoryboardUFL[j],
								" (",
								num3,
								"x",
								num4,
								")\n"
							})));
						}
					}
				}
				catch
				{
				}
			}
			return report;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		public static Report PreviewPointSet(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			if (bi.Difficluties[diffIndex].PreviewTime == -1)
			{
				report.Brief = Strings.PreviewPointSet_Brief;
				report.DifficultyIndex = diffIndex;
				report.Description = new Paragraph();
				report.Description.Inlines.Add(new Italic(new Run(Strings.PreviewPointSet_Desc1 + "\n\n")));
				report.Description.Inlines.Add(RunReport.CreateHL(Strings.PreviewPointSet_HLurl, Strings.PreviewPointSet_HL));
				report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.PreviewPointSet_Desc2 + " ")));
				report.Description.Inlines.Add(new Run(Strings.PreviewPointSet_Desc3));
			}
			return report;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000EDF4 File Offset: 0x0000CFF4
		public static Report VideoInTaiko(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			if (bi.Difficluties[diffIndex].isVideo && bi.Difficluties[diffIndex].DifficlutyMode == DifficlutyModeType.Taiko)
			{
				report.Brief = Strings.VideoInTaiko_Brief;
				report.DifficultyIndex = diffIndex;
				report.Description = new Paragraph();
				report.Description.Inlines.Add(new Italic(new Run(Strings.VideoInTaiko_Desc1 + " " + bi.Difficluties[diffIndex].Video + "\n\n")));
				report.Description.Inlines.Add(new Run(Strings.VideoInTaiko_Desc2));
			}
			return report;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000EEB8 File Offset: 0x0000D0B8
		public static Report NotWaveHS(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Global;
			report.Description = new Paragraph();
			bool flag = true;
			for (int i = 0; i < bi.SoundFiles.Count; i++)
			{
				AFType type = bi.SoundFiles[i].Type;
				if (type == AFType.mp3 || type == AFType.ogg)
				{
					report.Brief = Strings.NotWaveHS_Brief;
					if (flag)
					{
						report.Description.Inlines.Add(new Italic(new Run(Strings.NotWaveHS_Desc1 + "\n")));
						flag = false;
					}
					report.Description.Inlines.Add(new Run(bi.SoundFiles[i].rFilePath + "\n"));
				}
			}
			flag = true;
			for (int j = 0; j < bi.SoundFiles.Count; j++)
			{
				if (bi.SoundFiles[j].Type == AFType.none)
				{
					report.Brief = Strings.NotWaveHS_Brief;
					if (flag)
					{
						if (report.Description.Inlines.Count > 0)
						{
							report.Description.Inlines.Add(new Run("\n"));
						}
						report.Description.Inlines.Add(new Italic(new Run(Strings.NotWaveHS_Desc2 + "\n")));
						flag = false;
					}
					report.Description.Inlines.Add(new Run(bi.SoundFiles[j].rFilePath + "\n"));
				}
			}
			return report;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000F058 File Offset: 0x0000D258
		public static Report UnusedHitsounds(BeatmapInfo bi, int diffIndex)
		{
			RunReport.<>c__DisplayClass29_0 CS$<>8__locals1 = new RunReport.<>c__DisplayClass29_0();
			CS$<>8__locals1.bi = bi;
			Report report = default(Report);
			report.Level = ReportLevel.Global;
			CS$<>8__locals1.newSoundFiles = new List<string>(CS$<>8__locals1.bi.SoundFiles.Count);
			CS$<>8__locals1.bi.SoundFiles.ForEach(delegate(AudioFile item)
			{
				CS$<>8__locals1.newSoundFiles.Add(new string(item.rFilePath.ToCharArray()));
			});
			int l;
			int k;
			for (l = 0; l < CS$<>8__locals1.bi.StoryboardUFL.Count; l = k + 1)
			{
				CS$<>8__locals1.newSoundFiles.RemoveAll((string n) => n.Equals(CS$<>8__locals1.bi.StoryboardUFL[l], StringComparison.InvariantCultureIgnoreCase));
				k = l;
			}
			Skinning.Reduce(new List<Skinning.SkinSet>(Skinning.Init()), CS$<>8__locals1.newSoundFiles);
			int i;
			for (i = 0; i < CS$<>8__locals1.bi.HitSoundsUFL.Count; i = k + 1)
			{
				int num = CS$<>8__locals1.newSoundFiles.FindIndex((string x) => x.Equals(CS$<>8__locals1.bi.HitSoundsUFL[i] + ".wav", StringComparison.InvariantCultureIgnoreCase));
				if (num != -1)
				{
					CS$<>8__locals1.newSoundFiles.RemoveAt(num);
				}
				else
				{
					num = CS$<>8__locals1.newSoundFiles.FindIndex((string x) => x.Equals(CS$<>8__locals1.bi.HitSoundsUFL[i] + ".mp3", StringComparison.InvariantCultureIgnoreCase));
					if (num != -1)
					{
						CS$<>8__locals1.newSoundFiles.RemoveAt(num);
					}
					else
					{
						num = CS$<>8__locals1.newSoundFiles.FindIndex((string x) => x.Equals(CS$<>8__locals1.bi.HitSoundsUFL[i] + ".ogg", StringComparison.InvariantCultureIgnoreCase));
						if (num != -1)
						{
							CS$<>8__locals1.newSoundFiles.RemoveAt(num);
						}
					}
				}
				k = i;
			}
			if (CS$<>8__locals1.newSoundFiles.Count > 0)
			{
				report.Brief = Strings.UnusedHitsounds_Brief;
				report.Description = new Paragraph();
				report.Description.Inlines.Add(new Italic(new Run(Strings.UnusedHitsounds_Desc1 + "\n")));
				for (int j = 0; j < CS$<>8__locals1.newSoundFiles.Count; j++)
				{
					report.Description.Inlines.Add(new Run(CS$<>8__locals1.newSoundFiles[j] + "\n"));
				}
			}
			return report;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
		public static Report DuplicateOsb(BeatmapInfo bi, int diffIndex)
		{
			CultureInfo cultureInfo = new CultureInfo("en-US");
			Report report = default(Report);
			report.Level = ReportLevel.Global;
			report.Description = new Paragraph();
			report.Description.Inlines.Add(new Italic(new Run(Strings.DuplicateOsb_Desc1 + "\n")));
			if (bi.OsbFiles.Count > 0)
			{
				for (int i = 0; i < bi.OsbFiles.Count; i++)
				{
					if (cultureInfo.CompareInfo.Compare(bi.OsbFiles[i], bi.osbFile, CompareOptions.IgnoreCase) != 0)
					{
						report.Brief = Strings.DuplicateOsb_Brief;
						report.Description.Inlines.Add(new Run(bi.OsbFiles[i] + "\n"));
					}
				}
			}
			return report;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000F3A0 File Offset: 0x0000D5A0
		public static Report BackgroundSet(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			if (string.IsNullOrEmpty(bi.Difficluties[diffIndex].Background))
			{
				report.Brief = Strings.Background_Brief;
				report.DifficultyIndex = diffIndex;
				report.Description = new Paragraph();
				report.Description.Inlines.Add(new Italic(new Run(Strings.Background_Desc1 + "\n\n")));
				report.Description.Inlines.Add(RunReport.CreateHL(Strings.Background_HLurl, Strings.Background_HL));
				report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.Background_Desc2 + " ")));
				report.Description.Inlines.Add(new Run(Strings.Background_Desc3));
			}
			return report;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000F48C File Offset: 0x0000D68C
		public static Report ComboColorsSet(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Difficulty;
			if (bi.Difficluties[diffIndex].DifficlutyMode == DifficlutyModeType.Standard && !bi.Difficluties[diffIndex].isComboColor)
			{
				report.Brief = Strings.ComboColorsSet_Brief;
				report.DifficultyIndex = diffIndex;
				report.Description = new Paragraph();
				report.Description.Inlines.Add(new Italic(new Run(Strings.ComboColorsSet_Desc1)));
			}
			return report;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000F510 File Offset: 0x0000D710
		public static Report Consistency(BeatmapInfo bi, int diffIndex)
		{
			Report report = default(Report);
			report.Level = ReportLevel.Global;
			report.Brief = Strings.Consistency_Brief;
			report.Description = new Paragraph();
			bool flag = true;
			bool flag2 = true;
			bool flag3 = true;
			bool flag4 = true;
			bool flag5 = true;
			bool flag6 = true;
			bool flag7 = true;
			bool flag8 = true;
			bool flag9 = true;
			bool flag10 = true;
			bool flag11 = true;
			bool flag12 = true;
			bool flag13 = true;
			if (bi.Difficluties.Count > 1)
			{
				for (int i = 1; i < bi.Difficluties.Count; i++)
				{
					if (flag && string.Compare(bi.Difficluties[0].AudioFilename, bi.Difficluties[i].AudioFilename) != 0)
					{
						report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C1 + "\n")));
						for (int j = 0; j < bi.Difficluties.Count; j++)
						{
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[j].Version + ": ")));
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[j].AudioFilename + "\n")));
						}
						report.Description.Inlines.Add(new LineBreak());
						flag = false;
					}
					if (flag2 && bi.Difficluties[0].AudioLeadIn != bi.Difficluties[i].AudioLeadIn)
					{
						report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C2 + "\n")));
						for (int k = 0; k < bi.Difficluties.Count; k++)
						{
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[k].Version + ": ")));
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[k].AudioLeadIn + "\n")));
						}
						report.Description.Inlines.Add(new LineBreak());
						flag2 = false;
					}
					if (flag3 && bi.Difficluties[0].PreviewTime != bi.Difficluties[i].PreviewTime)
					{
						report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C3 + "\n")));
						for (int l = 0; l < bi.Difficluties.Count; l++)
						{
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[l].Version + ": ")));
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[l].PreviewTime + "\n")));
						}
						report.Description.Inlines.Add(new LineBreak());
						flag3 = false;
					}
					if (flag4 && bi.Difficluties[0].Countdown != bi.Difficluties[i].Countdown)
					{
						report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C4 + "\n")));
						for (int m = 0; m < bi.Difficluties.Count; m++)
						{
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[m].Version + ": ")));
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[m].Countdown + "\n")));
						}
						report.Description.Inlines.Add(new LineBreak());
						flag4 = false;
					}
					if (flag5 && bi.Difficluties[0].LetterboxInBreaks != bi.Difficluties[i].LetterboxInBreaks)
					{
						report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C5 + "\n")));
						for (int n = 0; n < bi.Difficluties.Count; n++)
						{
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[n].Version + ": ")));
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[n].LetterboxInBreaks + "\n")));
						}
						report.Description.Inlines.Add(new LineBreak());
						flag5 = false;
					}
					if (flag6 && bi.Difficluties[0].CountdownOffset != bi.Difficluties[i].CountdownOffset)
					{
						report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C6 + "\n")));
						for (int num = 0; num < bi.Difficluties.Count; num++)
						{
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num].Version + ": ")));
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num].CountdownOffset + "\n")));
						}
						report.Description.Inlines.Add(new LineBreak());
						flag6 = false;
					}
					if (flag7 && string.Compare(bi.Difficluties[0].Title, bi.Difficluties[i].Title) != 0)
					{
						report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C7 + "\n")));
						for (int num2 = 0; num2 < bi.Difficluties.Count; num2++)
						{
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num2].Version + ": ")));
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num2].Title + "\n")));
						}
						report.Description.Inlines.Add(new LineBreak());
						flag7 = false;
					}
					if (flag8 && string.Compare(bi.Difficluties[0].TitleUnicode, bi.Difficluties[i].TitleUnicode) != 0)
					{
						report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C8 + "\n")));
						for (int num3 = 0; num3 < bi.Difficluties.Count; num3++)
						{
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num3].Version + ": ")));
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num3].TitleUnicode + "\n")));
						}
						report.Description.Inlines.Add(new LineBreak());
						flag8 = false;
					}
					if (flag9 && string.Compare(bi.Difficluties[0].Artist, bi.Difficluties[i].Artist) != 0)
					{
						report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C9 + "\n")));
						for (int num4 = 0; num4 < bi.Difficluties.Count; num4++)
						{
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num4].Version + ": ")));
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num4].Artist + "\n")));
						}
						report.Description.Inlines.Add(new LineBreak());
						flag9 = false;
					}
					if (flag10 && string.Compare(bi.Difficluties[0].ArtistUnicode, bi.Difficluties[i].ArtistUnicode) != 0)
					{
						report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C10 + "\n")));
						for (int num5 = 0; num5 < bi.Difficluties.Count; num5++)
						{
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num5].Version + ": ")));
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num5].ArtistUnicode + "\n")));
						}
						report.Description.Inlines.Add(new LineBreak());
						flag10 = false;
					}
					if (flag11 && string.Compare(bi.Difficluties[0].Creator, bi.Difficluties[i].Creator) != 0)
					{
						report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C11 + "\n")));
						for (int num6 = 0; num6 < bi.Difficluties.Count; num6++)
						{
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num6].Version + ": ")));
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num6].Creator + "\n")));
						}
						report.Description.Inlines.Add(new LineBreak());
						flag11 = false;
					}
					if (flag12 && string.Compare(bi.Difficluties[0].Source, bi.Difficluties[i].Source) != 0)
					{
						report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C12 + "\n")));
						for (int num7 = 0; num7 < bi.Difficluties.Count; num7++)
						{
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num7].Version + ": ")));
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num7].Source + "\n")));
						}
						report.Description.Inlines.Add(new LineBreak());
						flag12 = false;
					}
					if (flag13 && string.Compare(bi.Difficluties[0].Tags, bi.Difficluties[i].Tags) != 0)
					{
						report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C13 + "\n")));
						for (int num8 = 0; num8 < bi.Difficluties.Count; num8++)
						{
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num8].Version + ": ")));
							report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num8].Tags + "\n")));
						}
						report.Description.Inlines.Add(new LineBreak());
						flag13 = false;
					}
				}
				CultureInfo cultureInfo = new CultureInfo("en-US");
				int num9 = -1;
				for (int num10 = 0; num10 < bi.Difficluties.Count; num10++)
				{
					if (bi.Difficluties[num10].DifficlutyMode != DifficlutyModeType.Taiko)
					{
						num9 = num10;
						num10 = bi.Difficluties.Count;
					}
				}
				if (num9 != -1)
				{
					for (int num11 = 0; num11 < bi.Difficluties.Count; num11++)
					{
						if (cultureInfo.CompareInfo.Compare(bi.Difficluties[num9].Video, bi.Difficluties[num11].Video, CompareOptions.IgnoreCase) != 0 && (bi.Difficluties[num11].DifficlutyMode == DifficlutyModeType.CtB || bi.Difficluties[num11].DifficlutyMode == DifficlutyModeType.Standard || bi.Difficluties[num11].DifficlutyMode == DifficlutyModeType.Mania))
						{
							report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C14 + "\n")));
							for (int num12 = 0; num12 < bi.Difficluties.Count; num12++)
							{
								if (bi.Difficluties[num12].DifficlutyMode != DifficlutyModeType.Taiko)
								{
									report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num12].Version + ": ")));
									report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num12].Video + "\n")));
								}
							}
							report.Description.Inlines.Add(new LineBreak());
							num11 = bi.Difficluties.Count;
						}
					}
				}
				num9 = -1;
				for (int num13 = 0; num13 < bi.Difficluties.Count; num13++)
				{
					if (bi.Difficluties[num13].isVideo)
					{
						num9 = num13;
						num13 = bi.Difficluties.Count;
					}
				}
				if (num9 != -1)
				{
					for (int num14 = 0; num14 < bi.Difficluties.Count; num14++)
					{
						if (bi.Difficluties[num14].isVideo && bi.Difficluties[num9].VideoOffset != bi.Difficluties[num14].VideoOffset)
						{
							report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C15 + "\n")));
							for (int num15 = 0; num15 < bi.Difficluties.Count; num15++)
							{
								if (bi.Difficluties[num15].DifficlutyMode != DifficlutyModeType.Taiko)
								{
									report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num15].Version + ": ")));
									report.Description.Inlines.Add(new Italic(new Run(bi.Difficluties[num15].VideoOffset + "\n")));
								}
							}
							report.Description.Inlines.Add(new LineBreak());
							num14 = bi.Difficluties.Count;
						}
					}
				}
				num9 = -1;
				for (int num16 = 0; num16 < bi.Difficluties.Count; num16++)
				{
					if (bi.Difficluties[num16].DifficlutyMode == DifficlutyModeType.Standard)
					{
						num9 = num16;
						num16 = bi.Difficluties.Count;
					}
				}
				if (num9 != -1)
				{
					for (int num17 = 0; num17 < bi.Difficluties.Count; num17++)
					{
						if (bi.Difficluties[num17].DifficlutyMode == DifficlutyModeType.Standard && !bi.Difficluties[num9].SliderBorder.Equals(bi.Difficluties[num17].SliderBorder))
						{
							report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C16 + "\n")));
							report.Description.Inlines.Add(new LineBreak());
							num17 = bi.Difficluties.Count;
						}
					}
					for (int num18 = 0; num18 < bi.Difficluties.Count; num18++)
					{
						if (bi.Difficluties[num18].DifficlutyMode == DifficlutyModeType.Standard && !bi.Difficluties[num9].SliderTrackOverride.Equals(bi.Difficluties[num18].SliderTrackOverride))
						{
							report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C17 + "\n")));
							report.Description.Inlines.Add(new LineBreak());
							num18 = bi.Difficluties.Count;
						}
					}
				}
				List<TimingPoint> list = new List<TimingPoint>();
				List<TimingPoint> list2 = new List<TimingPoint>();
				bool flag14 = false;
				for (int num19 = 0; num19 < bi.Difficluties[0].TimingPoints.Count; num19++)
				{
					if (bi.Difficluties[0].TimingPoints[num19].unInherited)
					{
						list.Add(bi.Difficluties[0].TimingPoints[num19]);
					}
				}
				for (int num20 = 1; num20 < bi.Difficluties.Count; num20++)
				{
					list2.Clear();
					for (int num21 = 0; num21 < bi.Difficluties[num20].TimingPoints.Count; num21++)
					{
						if (bi.Difficluties[num20].TimingPoints[num21].unInherited)
						{
							list2.Add(bi.Difficluties[num20].TimingPoints[num21]);
						}
					}
					if (list.Count != list2.Count)
					{
						flag14 = true;
					}
					else
					{
						for (int num22 = 0; num22 < list.Count; num22++)
						{
							if (list[num22].time != list2[num22].time)
							{
								flag14 = true;
							}
							if (list[num22].msPerBeat != list2[num22].msPerBeat)
							{
								flag14 = true;
							}
						}
					}
				}
				if (flag14)
				{
					report.Description.Inlines.Add(new Bold(new Run(Strings.Consistency_C18 + "\n")));
					report.Description.Inlines.Add(new LineBreak());
				}
			}
			if (report.Description.Inlines.Count == 0)
			{
				report.Brief = string.Empty;
			}
			return report;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x0001095C File Offset: 0x0000EB5C
		public static Report DirectorySize(BeatmapInfo bi, int diffIndex)
		{
			bool flag = false;
			Report report = default(Report);
			report.Level = ReportLevel.Global;
			for (int i = 0; i < bi.Difficluties.Count; i++)
			{
				if (bi.Difficluties[i].isVideo || bi.Difficluties[i].isStoryBoard)
				{
					flag = true;
				}
			}
			if (bi.isStoryBoard)
			{
				flag = true;
			}
			if ((flag && bi.BeatmapSize >= 30f) || (!flag && bi.BeatmapSize >= 10f))
			{
				report.Brief = Strings.DirectorySize_Brief;
				report.Description = new Paragraph();
				report.Description.Inlines.Add(new Italic(new Run(string.Concat(new object[]
				{
					Strings.DirectorySize_Desc1,
					" ",
					bi.BeatmapSize,
					"mb\n\n"
				}))));
				report.Description.Inlines.Add(RunReport.CreateHL(Strings.DirectorySize_HLurl, Strings.DirectorySize_HL));
				report.Description.Inlines.Add(new Bold(new Run("\n\n" + Strings.DirectorySize_Desc2 + " ")));
				report.Description.Inlines.Add(new Run(Strings.DirectorySize_Desc3));
			}
			return report;
		}

		// Token: 0x02000064 RID: 100
		[Flags]
		private enum DuplicateTLType
		{
			// Token: 0x040002B2 RID: 690
			Volume = 1,
			// Token: 0x040002B3 RID: 691
			HSSet = 2,
			// Token: 0x040002B4 RID: 692
			HSSetNumber = 4,
			// Token: 0x040002B5 RID: 693
			Kiai = 8,
			// Token: 0x040002B6 RID: 694
			Duplicate = 16
		}
	}
}
