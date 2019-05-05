using System;
using System.Collections.Generic;
using System.Linq;
using osu.Processor;

namespace osu.Beatmap
{
	// Token: 0x0200003C RID: 60
	public class BeatmapInfo
	{
		// Token: 0x0600027F RID: 639 RVA: 0x00022460 File Offset: 0x00020660
		public BeatmapInfo(string path)
		{
			this.HomeDirectory = path;
			if (!BeatmapProcessor.BeatmapFolderInit(this))
			{
				throw new Exception("Unable to process beatmap: " + this.HomeDirectory);
			}
			if (this.Difficluties.Count == 0)
			{
				throw new Exception("No difficulties found:  " + this.HomeDirectory);
			}
			for (int j = 0; j < this.Difficluties.Count; j++)
			{
				DifficultyProcessor.ParseDifficulty(this.Difficluties[j]);
				ObjectsProcessor.ProcessHitsounds(this.Difficluties[j]);
				ObjectsProcessor.CreateStrainsList(this.Difficluties[j]);
				BeatmapInfo.SetBeatmapSnapping(this.Difficluties[j]);
				ObjectsProcessor.ProcessUnsnap(this.Difficluties[j], this.Difficluties[j].UsnappedSettings.slider_snapping16, this.Difficluties[j].UsnappedSettings.slider_snapping12);
			}
			BeatmapProcessor.CreateUniqueHSList(this);
			BeatmapProcessor.CreateSBFilesList(this);
			int i;
			int i2;
			for (i = 0; i < this.Difficluties.Count; i = i2 + 1)
			{
				int num = this.SoundFiles.FindIndex((AudioFile x) => x.rFilePath.Equals(this.Difficluties[i].AudioFilename, StringComparison.CurrentCultureIgnoreCase));
				if (num != -1)
				{
					this.SoundFiles.RemoveAt(num);
				}
				i2 = i;
			}
			List<Difficulty> difficluties = (from i in this.Difficluties
			orderby i.DifficlutyMode, i.Objects.Count
			select i).ToList<Difficulty>();
			this.Difficluties = difficluties;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00022694 File Offset: 0x00020894
		private static void SetBeatmapSnapping(Difficulty bd)
		{
			bd.UsnappedSettings.slider_snapping16 = 16;
			bd.UsnappedSettings.slider_snapping12 = 12;
			if ((double)bd.strains.TotalSR <= 1.5)
			{
				bd.UsnappedSettings.slider_snapping16 = 2;
				bd.UsnappedSettings.slider_snapping12 = 1;
				return;
			}
			if ((double)bd.strains.TotalSR <= 2.25)
			{
				bd.UsnappedSettings.slider_snapping16 = 2;
				bd.UsnappedSettings.slider_snapping12 = 3;
				return;
			}
			if ((double)bd.strains.TotalSR <= 3.75)
			{
				bd.UsnappedSettings.slider_snapping16 = 4;
				bd.UsnappedSettings.slider_snapping12 = 3;
				return;
			}
			if ((double)bd.strains.TotalSR <= 5.25)
			{
				bd.UsnappedSettings.slider_snapping16 = 8;
				bd.UsnappedSettings.slider_snapping12 = 6;
			}
		}

		// Token: 0x040001D3 RID: 467
		public string HomeDirectory = string.Empty;

		// Token: 0x040001D4 RID: 468
		public float BeatmapSize;

		// Token: 0x040001D5 RID: 469
		public string osbFile = string.Empty;

		// Token: 0x040001D6 RID: 470
		public List<Difficulty> Difficluties = new List<Difficulty>();

		// Token: 0x040001D7 RID: 471
		public bool isStoryBoard;

		// Token: 0x040001D8 RID: 472
		public List<string> ImageFiles = new List<string>();

		// Token: 0x040001D9 RID: 473
		public List<AudioFile> SoundFiles = new List<AudioFile>();

		// Token: 0x040001DA RID: 474
		public List<string> OsbFiles = new List<string>();

		// Token: 0x040001DB RID: 475
		public List<string> OtherFiles = new List<string>();

		// Token: 0x040001DC RID: 476
		public List<string> StoryboardUFL = new List<string>();

		// Token: 0x040001DD RID: 477
		public List<string> HitSoundsUFL = new List<string>();

		// Token: 0x040001DE RID: 478
		public List<string> ManiaCustomSamplesUFL = new List<string>();
	}
}
