using System;
using System.Collections.Generic;
using osu.Beatmap;
using osu.Beatmap.diffCalc;
using osu.DiffCalc;

namespace osu.Processor
{
	// Token: 0x0200003A RID: 58
	public static class ObjectsProcessor
	{
		// Token: 0x0600026F RID: 623 RVA: 0x00020B18 File Offset: 0x0001ED18
		public static bool ProcessHitsounds(Difficulty bd)
		{
			if (bd.TimingPoints.Count < 1 || bd.Objects.Count < 1)
			{
				return false;
			}
			for (int i = 0; i < bd.Objects.Count; i++)
			{
				ObjectsProcessor.objHitSound objHitSound = default(ObjectsProcessor.objHitSound);
				int timeLineIndex = ObjectsProcessor.getTimeLineIndex(bd, bd.Objects[i].StartTime, 5);
				HitsoundSetType hssetType = bd.TimingPoints[timeLineIndex].HSsetType;
				int hssetNumber = bd.TimingPoints[timeLineIndex].HSsetNumber;
				if (bd.Objects[i].IsType(HitObjectType.Normal) || bd.Objects[i].IsType(HitObjectType.Hold))
				{
					if (!string.IsNullOrEmpty(bd.Objects[i].ManiaSample))
					{
						if (!bd.CustomSamplesManiaList.Contains(bd.Objects[i].ManiaSample))
						{
							bd.CustomSamplesManiaList.Add(bd.Objects[i].ManiaSample);
						}
					}
					else
					{
						objHitSound.customSetNum = hssetNumber;
						if (objHitSound.customSetNum != 0)
						{
							objHitSound.MainSet = ObjectsProcessor.CalcMainHSType(bd.Objects[i].Additions.mainSet, hssetType);
							objHitSound.SecondarySet = ObjectsProcessor.CalcSecHSType(bd.Objects[i].Additions.mainSet, bd.Objects[i].Additions.secondarySet, hssetType);
							objHitSound.objType = HitObjectType.Normal;
							objHitSound.HitSound = bd.Objects[i].HitSound;
							ObjectsProcessor.AddHitsound(bd, objHitSound);
						}
					}
				}
				if (bd.Objects[i].IsType(HitObjectType.Spinner))
				{
					if (!bd.HitSoundsUniqueList.Contains("spinnerspin"))
					{
						bd.HitSoundsUniqueList.Add("spinnerspin");
					}
					if (!bd.HitSoundsUniqueList.Contains("spinnerbonus"))
					{
						bd.HitSoundsUniqueList.Add("spinnerbonus");
					}
					int timeLineIndex2 = ObjectsProcessor.getTimeLineIndex(bd, bd.Objects[i].EndTime, 5);
					objHitSound.customSetNum = bd.TimingPoints[timeLineIndex2].HSsetNumber;
					if (objHitSound.customSetNum != 0)
					{
						objHitSound.MainSet = ObjectsProcessor.CalcMainHSType(bd.Objects[i].Additions.mainSet, hssetType);
						objHitSound.SecondarySet = ObjectsProcessor.CalcSecHSType(bd.Objects[i].Additions.mainSet, bd.Objects[i].Additions.secondarySet, hssetType);
						objHitSound.objType = HitObjectType.Normal;
						objHitSound.HitSound = bd.Objects[i].HitSound;
						ObjectsProcessor.AddHitsound(bd, objHitSound);
					}
				}
				if (bd.Objects[i].IsType(HitObjectType.Slider))
				{
					objHitSound.objType = HitObjectType.Slider;
					objHitSound.HitSound = HitObjectHSType.Slide;
					if (bd.Objects[i].Whistle)
					{
						objHitSound.HitSound = HitObjectHSType.Whistle;
					}
					int sliderEdgeTiming = ObjectsProcessor.getSliderEdgeTiming(bd, bd.Objects[i], bd.Objects[i].EdgesCount);
					for (int j = bd.TimingPoints.Count - 1; j >= 0; j--)
					{
						if (bd.TimingPoints[j].time < sliderEdgeTiming && bd.TimingPoints[j].time >= bd.Objects[i].StartTime)
						{
							objHitSound.customSetNum = bd.TimingPoints[j].HSsetNumber;
							hssetType = bd.TimingPoints[j].HSsetType;
							objHitSound.MainSet = ObjectsProcessor.CalcMainHSType(bd.Objects[i].Additions.mainSet, hssetType);
							objHitSound.SecondarySet = ObjectsProcessor.CalcSecHSType(bd.Objects[i].Additions.mainSet, bd.Objects[i].Additions.secondarySet, hssetType);
							ObjectsProcessor.AddHitsound(bd, objHitSound);
						}
					}
					List<int> list = new List<int>();
					for (int k = 0; k < bd.Objects[i].EdgesCount; k++)
					{
						int sliderEdgeTiming2 = ObjectsProcessor.getSliderEdgeTiming(bd, bd.Objects[i], k + 1);
						int timeLineIndex3 = ObjectsProcessor.getTimeLineIndex(bd, sliderEdgeTiming2, 5);
						list.Add(sliderEdgeTiming2);
						objHitSound.customSetNum = bd.TimingPoints[timeLineIndex3].HSsetNumber;
						if (objHitSound.customSetNum > 0)
						{
							hssetType = bd.TimingPoints[timeLineIndex3].HSsetType;
							objHitSound.MainSet = ObjectsProcessor.CalcMainHSType(bd.Objects[i].EdgesHS_Additions[k].mainSet, bd.Objects[i].Additions.mainSet);
							objHitSound.MainSet = ObjectsProcessor.CalcMainHSType(objHitSound.MainSet, hssetType);
							objHitSound.SecondarySet = ObjectsProcessor.CalcSecHSType(bd.Objects[i].EdgesHS_Additions[k].mainSet, bd.Objects[i].EdgesHS_Additions[k].secondarySet, bd.Objects[i].Additions.mainSet);
							objHitSound.SecondarySet = ObjectsProcessor.CalcSecHSType(objHitSound.SecondarySet, bd.Objects[i].Additions.secondarySet, hssetType);
							objHitSound.objType = HitObjectType.Normal;
							objHitSound.HitSound = bd.Objects[i].EdgesHS[k];
							if (objHitSound.HitSound == HitObjectHSType.None)
							{
								objHitSound.HitSound = bd.Objects[i].HitSound;
							}
							ObjectsProcessor.AddHitsound(bd, objHitSound);
						}
					}
					double num = bd.TimingPoints[ObjectsProcessor.getTimeLineIndex(bd, bd.Objects[i].StartTime, 0)].msPerBeat;
					if (num < 0.0)
					{
						num = num * -1.0 / 100.0;
					}
					else
					{
						num = 1.0;
					}
					Convert.ToInt32(Math.Floor((double)(bd.Objects[i].SpatialLength / 100f) * num / (double)bd.SliderMultiplier * (double)bd.SliderTickRate));
					double msPerBeat = bd.TimingPoints[ObjectsProcessor.getBPMTimeLineIndex(bd, bd.Objects[i].StartTime)].msPerBeat;
					bool flag = true;
					List<float> list2 = new List<float>();
					List<float> list3 = new List<float>();
					float num2 = Convert.ToSingle(Math.Ceiling(msPerBeat / (double)bd.SliderTickRate));
					int num3 = 2;
					while ((float)bd.Objects[i].StartTime + num2 < (float)list[1])
					{
						list2.Add(num2);
						num2 = Convert.ToSingle(Math.Floor(msPerBeat / (double)bd.SliderTickRate * (double)num3));
						num3++;
					}
					for (int l = list2.Count - 1; l >= 0; l--)
					{
						list3.Add((float)(list[1] - bd.Objects[i].StartTime) - list2[l]);
					}
					if (list2.Count > 0)
					{
						for (int m = 0; m < list.Count - 1; m++)
						{
							if (flag)
							{
								flag = false;
								for (int n = 0; n < list2.Count; n++)
								{
									int num4 = Convert.ToInt32(Math.Floor((double)((float)list[m] + list2[n])));
									if (!list.Contains(num4))
									{
										int timeLineIndex4 = ObjectsProcessor.getTimeLineIndex(bd, num4, 5);
										objHitSound.customSetNum = bd.TimingPoints[timeLineIndex4].HSsetNumber;
										if (objHitSound.customSetNum > 0)
										{
											objHitSound.MainSet = ObjectsProcessor.CalcMainHSType(bd.Objects[i].Additions.mainSet, bd.TimingPoints[timeLineIndex4].HSsetType);
											objHitSound.SecondarySet = ObjectsProcessor.CalcSecHSType(bd.Objects[i].Additions.mainSet, bd.Objects[i].Additions.secondarySet, bd.TimingPoints[timeLineIndex4].HSsetType);
											objHitSound.objType = HitObjectType.Slider;
											objHitSound.HitSound = HitObjectHSType.Tick;
											ObjectsProcessor.AddHitsound(bd, objHitSound);
										}
									}
								}
							}
							else
							{
								flag = true;
								for (int num5 = 0; num5 < list3.Count; num5++)
								{
									int num4 = Convert.ToInt32(Math.Floor((double)((float)list[m] + list3[num5])));
									if (!list.Contains(num4))
									{
										int timeLineIndex5 = ObjectsProcessor.getTimeLineIndex(bd, num4, 5);
										objHitSound.customSetNum = bd.TimingPoints[timeLineIndex5].HSsetNumber;
										if (objHitSound.customSetNum > 0)
										{
											objHitSound.MainSet = ObjectsProcessor.CalcMainHSType(bd.Objects[i].Additions.mainSet, bd.TimingPoints[timeLineIndex5].HSsetType);
											objHitSound.SecondarySet = ObjectsProcessor.CalcSecHSType(bd.Objects[i].Additions.mainSet, bd.Objects[i].Additions.secondarySet, bd.TimingPoints[timeLineIndex5].HSsetType);
											objHitSound.objType = HitObjectType.Slider;
											objHitSound.HitSound = HitObjectHSType.Tick;
											ObjectsProcessor.AddHitsound(bd, objHitSound);
										}
									}
								}
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000214ED File Offset: 0x0001F6ED
		private static HitsoundSetType CalcMainHSType(HitsoundSetType main, HitsoundSetType tl)
		{
			if (main == HitsoundSetType.Auto)
			{
				return tl;
			}
			return main;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000214F5 File Offset: 0x0001F6F5
		private static HitsoundSetType CalcSecHSType(HitsoundSetType main, HitsoundSetType sec, HitsoundSetType tl)
		{
			if (sec != HitsoundSetType.Auto)
			{
				return sec;
			}
			if (main == HitsoundSetType.Auto)
			{
				return tl;
			}
			return main;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00021504 File Offset: 0x0001F704
		public static void ProcessUnsnap(Difficulty bd, int s16, int s12)
		{
			bd.UsnappedList.Clear();
			bd.UsnappedSettings.slider_snapping16 = s16;
			bd.UsnappedSettings.slider_snapping12 = s12;
			for (int i = 0; i < bd.Objects.Count; i++)
			{
				if (bd.Objects[i].IsType(HitObjectType.Hold))
				{
					if (!ObjectsProcessor.isSnapped(bd, bd.Objects[i].StartTime, s16, s12))
					{
						Usnapped item = default(Usnapped);
						item.time = bd.Objects[i].StartTime;
						item.type = UnsnappedType.LongStart;
						bd.UsnappedList.Add(item);
					}
					if (!ObjectsProcessor.isSnapped(bd, bd.Objects[i].EndTime, s16, s12))
					{
						Usnapped item2 = default(Usnapped);
						item2.time = bd.Objects[i].EndTime;
						item2.type = UnsnappedType.LongEnd;
						bd.UsnappedList.Add(item2);
					}
				}
				if (bd.Objects[i].IsType(HitObjectType.Normal) && !ObjectsProcessor.isSnapped(bd, bd.Objects[i].StartTime, s16, s12))
				{
					Usnapped item3 = default(Usnapped);
					item3.time = bd.Objects[i].StartTime;
					item3.type = UnsnappedType.Circle;
					bd.UsnappedList.Add(item3);
				}
				if (bd.Objects[i].IsType(HitObjectType.Slider))
				{
					if (!ObjectsProcessor.isSnapped(bd, bd.Objects[i].StartTime, s16, s12))
					{
						Usnapped item4 = default(Usnapped);
						item4.time = bd.Objects[i].StartTime;
						item4.type = UnsnappedType.SliderStart;
						bd.UsnappedList.Add(item4);
					}
					double msPerBeat = bd.TimingPoints[ObjectsProcessor.getBPMTimeLineIndex(bd, bd.Objects[i].StartTime)].msPerBeat;
					for (int j = 1; j < bd.Objects[i].EdgesCount; j++)
					{
						int sliderEdgeTiming = ObjectsProcessor.getSliderEdgeTiming(bd, bd.Objects[i], j + 1);
						if (!ObjectsProcessor.isSnapped(bd, sliderEdgeTiming, s16, s12))
						{
							Usnapped item5 = default(Usnapped);
							item5.time = sliderEdgeTiming;
							if (j == bd.Objects[i].EdgesCount - 1)
							{
								item5.type = UnsnappedType.SliderEnd;
							}
							else
							{
								item5.type = UnsnappedType.SliderRepeat;
							}
							bd.UsnappedList.Add(item5);
						}
					}
				}
				if (bd.Objects[i].IsType(HitObjectType.Spinner))
				{
					if (!ObjectsProcessor.isSnapped(bd, bd.Objects[i].StartTime, s16, s12))
					{
						Usnapped item6 = default(Usnapped);
						item6.time = bd.Objects[i].StartTime;
						item6.type = UnsnappedType.SpinnerStart;
						bd.UsnappedList.Add(item6);
					}
					if (!ObjectsProcessor.isSnapped(bd, bd.Objects[i].EndTime, s16, s12))
					{
						Usnapped item7 = default(Usnapped);
						item7.time = bd.Objects[i].StartTime;
						item7.type = UnsnappedType.SpinnerEnd;
						bd.UsnappedList.Add(item7);
					}
				}
			}
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00021840 File Offset: 0x0001FA40
		public static void CreateStrainsList(Difficulty bd)
		{
			if (bd.DifficlutyMode == DifficlutyModeType.Standard)
			{
				new BeatmapDifficultyCalculatorOsu(bd);
				bd.strains.PP = ppVal.Calculate(bd.strains.AimSR, bd.strains.SpeedSR, bd);
			}
			if (bd.DifficlutyMode == DifficlutyModeType.Taiko)
			{
				new BeatmapDifficultyCalculatorTaiko(bd);
				bd.strains.PP = ppValTaiko.Calculate(bd);
			}
			if (bd.DifficlutyMode == DifficlutyModeType.Mania)
			{
				new BeatmapDifficultyCalculatorMania(bd);
				bd.strains.PP = ppValMania.Calculate(bd);
			}
			if (bd.DifficlutyMode == DifficlutyModeType.CtB)
			{
				new BeatmapDifficultyCalculatorFruits(bd);
				bd.strains.PP = ppValCtB.Calculate(bd);
			}
		}

		// Token: 0x06000274 RID: 628 RVA: 0x000218E8 File Offset: 0x0001FAE8
		private static void AddHitsound(Difficulty bd, ObjectsProcessor.objHitSound hs)
		{
			if (hs.customSetNum == 0)
			{
				return;
			}
			string text = string.Empty;
			if (bd.DifficlutyMode == DifficlutyModeType.Taiko)
			{
				text = "taiko-";
			}
			string str = text;
			string text2 = string.Empty;
			if (hs.customSetNum != 1)
			{
				text2 = Convert.ToString(hs.customSetNum);
			}
			switch (hs.MainSet)
			{
			case HitsoundSetType.Normal:
				text += "normal-";
				break;
			case HitsoundSetType.Soft:
				text += "soft-";
				break;
			case HitsoundSetType.Drum:
				text += "drum-";
				break;
			}
			switch (hs.SecondarySet)
			{
			case HitsoundSetType.Auto:
				str = text;
				break;
			case HitsoundSetType.Normal:
				str += "normal-";
				break;
			case HitsoundSetType.Soft:
				str += "soft-";
				break;
			case HitsoundSetType.Drum:
				str += "drum-";
				break;
			}
			if (hs.objType.HasFlag(HitObjectType.Normal))
			{
				text += "hitnormal";
				str += "hit";
				if (!bd.HitSoundsUniqueList.Contains(text + text2))
				{
					bd.HitSoundsUniqueList.Add(text + text2);
				}
				if (hs.HitSound.HasFlag(HitObjectHSType.Whistle) && !bd.HitSoundsUniqueList.Contains(str + "whistle" + text2))
				{
					bd.HitSoundsUniqueList.Add(str + "whistle" + text2);
				}
				if (hs.HitSound.HasFlag(HitObjectHSType.Clap) && !bd.HitSoundsUniqueList.Contains(str + "clap" + text2))
				{
					bd.HitSoundsUniqueList.Add(str + "clap" + text2);
				}
				if (hs.HitSound.HasFlag(HitObjectHSType.Finish) && !bd.HitSoundsUniqueList.Contains(str + "finish" + text2))
				{
					bd.HitSoundsUniqueList.Add(str + "finish" + text2);
				}
			}
			if (hs.objType.HasFlag(HitObjectType.Slider))
			{
				text += "slider";
				if (hs.HitSound.HasFlag(HitObjectHSType.Whistle))
				{
					text += "whistle";
					if (!bd.HitSoundsUniqueList.Contains(text + text2))
					{
						bd.HitSoundsUniqueList.Add(text + text2);
					}
				}
				if (hs.HitSound.HasFlag(HitObjectHSType.Slide))
				{
					text += "slide";
					if (!bd.HitSoundsUniqueList.Contains(text + text2))
					{
						bd.HitSoundsUniqueList.Add(text + text2);
					}
				}
				if (hs.HitSound.HasFlag(HitObjectHSType.Tick))
				{
					text += "tick";
					if (!bd.HitSoundsUniqueList.Contains(text + text2))
					{
						bd.HitSoundsUniqueList.Add(text + text2);
					}
				}
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00021BF8 File Offset: 0x0001FDF8
		public static int getTimeLineIndex(Difficulty bd, int Timing, int Allowance)
		{
			for (int i = bd.TimingPoints.Count - 1; i >= 0; i--)
			{
				if (Timing >= bd.TimingPoints[i].time - Allowance)
				{
					return i;
				}
				if (i == 0 && bd.TimingPoints.Count > 0)
				{
					return 0;
				}
			}
			return -1;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00021C4C File Offset: 0x0001FE4C
		public static int getBPMTimeLineIndex(Difficulty bd, int Timing)
		{
			for (int i = bd.TimingPoints.Count - 1; i >= 0; i--)
			{
				if (bd.TimingPoints[i].unInherited && Timing + 1 >= bd.TimingPoints[i].time)
				{
					return i;
				}
				if (i == 0)
				{
					for (int j = 0; j < bd.TimingPoints.Count; j++)
					{
						if (bd.TimingPoints[j].unInherited)
						{
							return i;
						}
					}
				}
			}
			return -1;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00021CCC File Offset: 0x0001FECC
		public static int getSliderEdgeTiming(Difficulty bd, HitObject slider, int edge)
		{
			int startTime = slider.StartTime;
			if (edge == 1)
			{
				return startTime;
			}
			double num = bd.TimingPoints[ObjectsProcessor.getTimeLineIndex(bd, slider.StartTime, 0)].msPerBeat;
			if (num < 0.0)
			{
				num = num * -1.0 / 100.0;
			}
			else
			{
				num = 1.0;
			}
			return Convert.ToInt32(Math.Floor((double)startTime + (double)(slider.SpatialLength / 100f / bd.SliderMultiplier) * num * bd.TimingPoints[ObjectsProcessor.getBPMTimeLineIndex(bd, slider.StartTime)].msPerBeat * (double)(edge - 1)));
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00021D84 File Offset: 0x0001FF84
		public static bool isSnapped(Difficulty bd, int time, int s16, int s12)
		{
			int bpmtimeLineIndex = ObjectsProcessor.getBPMTimeLineIndex(bd, time);
			double num = bd.TimingPoints[bpmtimeLineIndex].msPerBeat / (double)s16;
			double num2 = bd.TimingPoints[bpmtimeLineIndex].msPerBeat / (double)s12;
			int num3 = time - bd.TimingPoints[bpmtimeLineIndex].time;
			int num4 = Convert.ToInt32(Math.Round((double)num3 / num));
			if (Math.Floor((double)num4 * num) <= (double)(num3 + 3) && Math.Floor((double)num4 * num) >= (double)(num3 - 3))
			{
				return true;
			}
			num4 = Convert.ToInt32(Math.Round((double)num3 / num2));
			return Math.Floor((double)num4 * num2) <= (double)(num3 + 3) && Math.Floor((double)num4 * num2) >= (double)(num3 - 3);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00021E3C File Offset: 0x0002003C
		private static int BeatSnapValue(double time, int snapDivisor, Difficulty bd)
		{
			if (bd.TimingPoints.Count == 0)
			{
				return (int)time;
			}
			double num = ObjectsProcessor.BeatOffsetCloseToZeroAt(time, bd);
			double num2 = ObjectsProcessor.BeatLengthAt(time, false, bd) / (double)snapDivisor;
			int num3;
			if (time - num < 0.0)
			{
				num3 = (int)((time - num) / num2) - 1;
			}
			else
			{
				num3 = (int)((time - num) / num2);
			}
			int num4 = (int)((double)num3 * num2 + num);
			int num5 = (int)((double)(num3 + 1) * num2 + num);
			if (time - (double)num4 >= (double)num5 - time)
			{
				return num5;
			}
			return num4;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00021EB0 File Offset: 0x000200B0
		private static double BeatOffsetCloseToZeroAt(double time, Difficulty bd)
		{
			if (bd.TimingPoints == null || bd.TimingPoints.Count == 0)
			{
				return 0.0;
			}
			int num = 0;
			for (int i = bd.TimingPoints.Count - 1; i > -1; i--)
			{
				if (bd.TimingPoints[i].unInherited && (double)bd.TimingPoints[i].time <= time)
				{
					num = i;
				}
			}
			double msPerBeat = bd.TimingPoints[num].msPerBeat;
			double num2 = (double)bd.TimingPoints[num].time;
			if (num == 0 && msPerBeat > 0.0)
			{
				while (num2 > 0.0)
				{
					num2 -= msPerBeat;
				}
			}
			return num2;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00021F68 File Offset: 0x00020168
		private static double BeatLengthAt(double time, bool allowMultiplier, Difficulty bd)
		{
			if (bd.TimingPoints == null || bd.TimingPoints.Count == 0)
			{
				return 0.0;
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < bd.TimingPoints.Count; i++)
			{
				if ((double)bd.TimingPoints[i].time <= time)
				{
					if (bd.TimingPoints[i].unInherited)
					{
						num = i;
					}
					else
					{
						num2 = i;
					}
				}
			}
			double num3 = 1.0;
			if (allowMultiplier && num2 > num && bd.TimingPoints[num2].msPerBeat < 0.0)
			{
				num3 = (double)bd.TimingPoints[num2].BpmMultiplier;
			}
			return bd.TimingPoints[num].msPerBeat * num3;
		}

		// Token: 0x02000083 RID: 131
		private struct objHitSound
		{
			// Token: 0x040002ED RID: 749
			public HitsoundSetType MainSet;

			// Token: 0x040002EE RID: 750
			public HitsoundSetType SecondarySet;

			// Token: 0x040002EF RID: 751
			public HitObjectType objType;

			// Token: 0x040002F0 RID: 752
			public HitObjectHSType HitSound;

			// Token: 0x040002F1 RID: 753
			public int customSetNum;
		}
	}
}
