using System;
using System.Collections.Generic;

namespace osu.Beatmap
{
	// Token: 0x0200003D RID: 61
	public class Difficulty
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0002277A File Offset: 0x0002097A
		public virtual int ColumnsCount
		{
			get
			{
				return Convert.ToInt32(Math.Ceiling((double)this.CircleSize));
			}
		}

		// Token: 0x040001DF RID: 479
		public List<HitObject> Objects = new List<HitObject>();

		// Token: 0x040001E0 RID: 480
		public int FruitsHitCount;

		// Token: 0x040001E1 RID: 481
		public string osuVersion = string.Empty;

		// Token: 0x040001E2 RID: 482
		public string osuFile = string.Empty;

		// Token: 0x040001E3 RID: 483
		public string AudioFilename = string.Empty;

		// Token: 0x040001E4 RID: 484
		public int TotalDrainTime;

		// Token: 0x040001E5 RID: 485
		public int StartTime;

		// Token: 0x040001E6 RID: 486
		public int EndTime;

		// Token: 0x040001E7 RID: 487
		public DifficlutyModeType DifficlutyMode;

		// Token: 0x040001E8 RID: 488
		public string Version = string.Empty;

		// Token: 0x040001E9 RID: 489
		public List<string> StoryboardFilesList = new List<string>();

		// Token: 0x040001EA RID: 490
		public List<string> HitSoundsUniqueList = new List<string>();

		// Token: 0x040001EB RID: 491
		public List<string> CustomSamplesManiaList = new List<string>();

		// Token: 0x040001EC RID: 492
		public List<Color> ComboColors = new List<Color>();

		// Token: 0x040001ED RID: 493
		public List<TimingPoint> TimingPoints = new List<TimingPoint>();

		// Token: 0x040001EE RID: 494
		public List<Break> Breaks = new List<Break>();

		// Token: 0x040001EF RID: 495
		public List<Usnapped> UsnappedList = new List<Usnapped>();

		// Token: 0x040001F0 RID: 496
		public List<int> HyperdashesList = new List<int>();

		// Token: 0x040001F1 RID: 497
		public UsnappedS UsnappedSettings;

		// Token: 0x040001F2 RID: 498
		public Strains strains = new Strains();

		// Token: 0x040001F3 RID: 499
		public bool isSpinner;

		// Token: 0x040001F4 RID: 500
		public bool isSliderBorder;

		// Token: 0x040001F5 RID: 501
		public bool isComboColor;

		// Token: 0x040001F6 RID: 502
		public bool isStoryBoard;

		// Token: 0x040001F7 RID: 503
		public bool isVideo;

		// Token: 0x040001F8 RID: 504
		public bool isSkinPreference;

		// Token: 0x040001F9 RID: 505
		public bool isSliderTrackOverride;

		// Token: 0x040001FA RID: 506
		public int AudioLeadIn;

		// Token: 0x040001FB RID: 507
		public int PreviewTime;

		// Token: 0x040001FC RID: 508
		public int CountdownOffset;

		// Token: 0x040001FD RID: 509
		public int Countdown;

		// Token: 0x040001FE RID: 510
		public int StoryFireInFront;

		// Token: 0x040001FF RID: 511
		public int LetterboxInBreaks;

		// Token: 0x04000200 RID: 512
		public int WidescreenStoryboard;

		// Token: 0x04000201 RID: 513
		public int EpilepsyWarning;

		// Token: 0x04000202 RID: 514
		public float StackLeniency;

		// Token: 0x04000203 RID: 515
		public string SkinPreference = string.Empty;

		// Token: 0x04000204 RID: 516
		public string Title = string.Empty;

		// Token: 0x04000205 RID: 517
		public string TitleUnicode = string.Empty;

		// Token: 0x04000206 RID: 518
		public string Artist = string.Empty;

		// Token: 0x04000207 RID: 519
		public string ArtistUnicode = string.Empty;

		// Token: 0x04000208 RID: 520
		public string Creator = string.Empty;

		// Token: 0x04000209 RID: 521
		public string Source = string.Empty;

		// Token: 0x0400020A RID: 522
		public string Tags = string.Empty;

		// Token: 0x0400020B RID: 523
		public int BeatmapID;

		// Token: 0x0400020C RID: 524
		public int BeatmapSetID;

		// Token: 0x0400020D RID: 525
		public float HPDrainRate;

		// Token: 0x0400020E RID: 526
		public float CircleSize;

		// Token: 0x0400020F RID: 527
		public float OverallDifficulty;

		// Token: 0x04000210 RID: 528
		public float ApproachRate;

		// Token: 0x04000211 RID: 529
		public float SliderMultiplier;

		// Token: 0x04000212 RID: 530
		public float SliderTickRate;

		// Token: 0x04000213 RID: 531
		public string Background = string.Empty;

		// Token: 0x04000214 RID: 532
		public string Video = string.Empty;

		// Token: 0x04000215 RID: 533
		public int VideoOffset;

		// Token: 0x04000216 RID: 534
		public Color SliderBorder = new Color(0, 0, 0);

		// Token: 0x04000217 RID: 535
		public Color SliderTrackOverride = new Color(0, 0, 0);
	}
}
