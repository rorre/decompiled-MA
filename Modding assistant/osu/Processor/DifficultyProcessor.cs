using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Modding_assistant.Utility;
using osu.Beatmap;

namespace osu.Processor
{
	// Token: 0x02000039 RID: 57
	public static class DifficultyProcessor
	{
		// Token: 0x0600026E RID: 622 RVA: 0x0001FFD4 File Offset: 0x0001E1D4
		public static bool ParseDifficulty(Difficulty bd)
		{
			osuReader osuReader = new osuReader(bd.osuFile);
			bd.osuVersion = osuReader.GetKeys("")[0];
			bd.AudioFilename = ut_Path.Normalize(osuReader.GetValue("audiofilename", "general", string.Empty));
			bd.SkinPreference = osuReader.GetValue("skinpreference", "general", string.Empty);
			if (!string.IsNullOrEmpty(bd.SkinPreference))
			{
				bd.isSkinPreference = true;
			}
			bd.AudioLeadIn = Convert.ToInt32(osuReader.GetValue("audioleadin", "general", "0"));
			bd.PreviewTime = Convert.ToInt32(osuReader.GetValue("previewtime", "general", "-1"));
			bd.CountdownOffset = Convert.ToInt32(osuReader.GetValue("countdownoffset", "general", "0"));
			bd.Countdown = Convert.ToInt32(osuReader.GetValue("countdown", "general", "0"));
			bd.LetterboxInBreaks = Convert.ToInt32(osuReader.GetValue("letterboxinbreaks", "general", "0"));
			bd.StoryFireInFront = Convert.ToInt32(osuReader.GetValue("storyfireinfront", "general", "0"));
			bd.EpilepsyWarning = Convert.ToInt32(osuReader.GetValue("epilepsywarning", "general", "0"));
			bd.WidescreenStoryboard = Convert.ToInt32(osuReader.GetValue("widescreenstoryboard", "general", "0"));
			int value = Convert.ToInt32(osuReader.GetValue("mode", "general", "0"));
			bd.StackLeniency = Convert.ToSingle(osuReader.GetValue("stackleniency", "general", "0"));
			bd.DifficlutyMode = (DifficlutyModeType)Enum.ToObject(typeof(DifficlutyModeType), value);
			bd.Title = osuReader.GetValue("title", "metadata", string.Empty);
			bd.TitleUnicode = osuReader.GetValue("titleunicode", "metadata", string.Empty);
			bd.Artist = osuReader.GetValue("artist", "metadata", string.Empty);
			bd.ArtistUnicode = osuReader.GetValue("artistunicode", "metadata", string.Empty);
			bd.Creator = osuReader.GetValue("creator", "metadata", string.Empty);
			bd.Version = osuReader.GetValue("version", "metadata", string.Empty);
			bd.Source = osuReader.GetValue("source", "metadata", string.Empty);
			bd.Tags = osuReader.GetValue("tags", "metadata", string.Empty);
			bd.BeatmapID = Convert.ToInt32(osuReader.GetValue("beatmapid", "metadata", "0"));
			bd.BeatmapSetID = Convert.ToInt32(osuReader.GetValue("beatmapsetid", "metadata", "0"));
			bd.HPDrainRate = Convert.ToSingle(osuReader.GetValue("hpdrainrate", "difficulty", "0"));
			bd.CircleSize = Convert.ToSingle(osuReader.GetValue("circlesize", "difficulty", "0"));
			bd.OverallDifficulty = Convert.ToSingle(osuReader.GetValue("overalldifficulty", "difficulty", "0"));
			bd.ApproachRate = Convert.ToSingle(osuReader.GetValue("approachrate", "difficulty", "0"));
			bd.SliderMultiplier = Convert.ToSingle(osuReader.GetValue("slidermultiplier", "difficulty", "0"));
			bd.SliderTickRate = Convert.ToSingle(osuReader.GetValue("slidertickrate", "difficulty", "0"));
			List<string> list = new List<string>(osuReader.GetKeys("events"));
			for (int i2 = 0; i2 < list.Count; i2++)
			{
				List<string> list2 = new List<string>(list[i2].Split(new char[]
				{
					','
				}));
				if (list2.Count > 2)
				{
					if (string.Compare(list2[0], "0", true) == 0)
					{
						bd.Background = ut_Path.Normalize(list2[2].Replace("\"", ""));
					}
					else if (string.Compare(list2[0], "1", true) == 0 || string.Compare(list2[0], "video", true) == 0)
					{
						bd.Video = ut_Path.Normalize(list2[2].Replace("\"", ""));
						int.TryParse(list2[1], out bd.VideoOffset);
						if (!string.IsNullOrEmpty(bd.Video))
						{
							bd.isVideo = true;
						}
					}
					else if (string.Compare(list2[0], "2", true) == 0)
					{
						Break @break = new Break();
						int.TryParse(list2[1], out @break.startTime);
						int.TryParse(list2[2], out @break.endTime);
						bd.Breaks.Add(@break);
					}
					else if (string.Compare(list2[0], "sample", true) == 0)
					{
						if (list2.Count > 3)
						{
							bd.StoryboardFilesList.Add(ut_Path.Normalize(list2[3].Replace("\"", "")));
						}
						bd.isStoryBoard = true;
					}
					else if (string.Compare(list2[0], "animation", true) == 0)
					{
						if (i2 < list.Count - 1)
						{
							List<string> strNext = new List<string>(list[i2 + 1].Split(new char[]
							{
								','
							}));
							strNext[0] = strNext[0].TrimStart(new char[]
							{
								'_'
							});
							if (Constants.SBEvents.FindIndex((string x) => x.Equals(strNext[0], StringComparison.InvariantCultureIgnoreCase)) != -1)
							{
								int num = 0;
								string text = string.Empty;
								if (list2.Count > 6)
								{
									int.TryParse(list2[6], out num);
								}
								if (list2.Count > 3)
								{
									text = ut_Path.Normalize(list2[3].Replace("\"", ""));
								}
								string extension = Path.GetExtension(text);
								text = text.Substring(0, text.Length - extension.Length);
								for (int j = 0; j < num; j++)
								{
									string item = text + Convert.ToString(j) + extension;
									bd.StoryboardFilesList.Add(item);
								}
								bd.isStoryBoard = true;
							}
						}
					}
					else if (string.Compare(list2[0], "sprite", true) == 0 && i2 < list.Count - 1)
					{
						List<string> strNext = new List<string>(list[i2 + 1].Split(new char[]
						{
							','
						}));
						strNext[0] = strNext[0].TrimStart(new char[]
						{
							'_'
						});
						if (Constants.SBEvents.FindIndex((string x) => x.Equals(strNext[0], StringComparison.InvariantCultureIgnoreCase)) != -1)
						{
							if (list2.Count > 3)
							{
								bd.StoryboardFilesList.Add(ut_Path.Normalize(list2[3].Replace("\"", "")));
							}
							bd.isStoryBoard = true;
						}
					}
				}
			}
			List<string> list3 = new List<string>(osuReader.GetKeys("timingpoints"));
			for (int k = 0; k < list3.Count; k++)
			{
				bd.TimingPoints.Add(new TimingPoint(list3[k]));
			}
			List<TimingPoint> timingPoints = (from i in bd.TimingPoints
			orderby i.time, !i.unInherited
			select i).ToList<TimingPoint>();
			bd.TimingPoints = timingPoints;
			List<string> list4 = new List<string>(osuReader.GetKeys("colours"));
			for (int l = 0; l < list4.Count; l++)
			{
				if (list4[l].IndexOf("combo", StringComparison.CurrentCultureIgnoreCase) >= 0)
				{
					try
					{
						bd.ComboColors.Add(new Color(osuReader.GetValue(list4[l], "colours", string.Empty)));
					}
					catch
					{
					}
					bd.isComboColor = true;
				}
				else if (string.Compare(list4[l], "sliderborder", true) == 0)
				{
					try
					{
						bd.SliderBorder = new Color(osuReader.GetValue("sliderborder", "colours", string.Empty));
					}
					catch
					{
					}
					bd.isSliderBorder = true;
				}
				else if (string.Compare(list4[l], "slidertrackoverride", true) == 0)
				{
					try
					{
						bd.SliderTrackOverride = new Color(osuReader.GetValue("slidertrackoverride", "colours", string.Empty));
					}
					catch
					{
					}
					bd.isSliderTrackOverride = true;
				}
			}
			List<string> list5 = new List<string>(osuReader.GetKeys("hitobjects"));
			int num2 = 1;
			for (int m = 0; m < list5.Count; m++)
			{
				HitObject hitObject = new HitObject(list5[m] + ":" + osuReader.GetValue(list5[m], "hitobjects", string.Empty), bd);
				if (hitObject.IsType(HitObjectType.NewCombo) || m == 0 || hitObject.IsType(HitObjectType.Spinner))
				{
					num2 = 1;
				}
				if (m != 0 && bd.Objects[bd.Objects.Count - 1].IsType(HitObjectType.Spinner))
				{
					num2 = 1;
				}
				hitObject.ComboNumber = num2;
				num2++;
				bd.Objects.Add(hitObject);
			}
			List<HitObject> objects = (from i in bd.Objects
			orderby i.StartTime, i.Position.X
			select i).ToList<HitObject>();
			bd.Objects = objects;
			string empty = string.Empty;
			if (bd.Objects.Count > 0)
			{
				bd.TotalDrainTime = bd.Objects[bd.Objects.Count - 1].EndTime;
				bd.TotalDrainTime -= bd.Objects[0].StartTime;
				bd.StartTime = bd.Objects[0].StartTime;
				bd.EndTime = bd.Objects[bd.Objects.Count - 1].EndTime;
			}
			for (int n = 0; n < bd.Breaks.Count; n++)
			{
				bd.TotalDrainTime -= bd.Breaks[n].endTime - bd.Breaks[n].startTime;
			}
			return true;
		}
	}
}
