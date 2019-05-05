using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Windows;
using Microsoft.WindowsAPICodePack.Shell;
using NAudio.Wave;
using NVorbis;

namespace osu
{
	// Token: 0x02000027 RID: 39
	public class AudioFile
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0001E90B File Offset: 0x0001CB0B
		public string FullFilePath
		{
			get
			{
				return Path.Combine(this.BeatmapPath, this.rFilePath);
			}
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0001E920 File Offset: 0x0001CB20
		public AudioFile(string rPath, string Dir)
		{
			this.rFilePath = rPath;
			this.BeatmapPath = Dir;
			this.Type = AudioFile.DetectAF(this.FullFilePath);
			if (this.Type == AFType.wav)
			{
				try
				{
					this.Length = Convert.ToSingle(AudioFile.GetLength(this.FullFilePath));
				}
				catch (Exception ex)
				{
					MessageBox.Show(rPath + " processing error: " + ex.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
					this.Length = 0f;
				}
				try
				{
					this.isDelay = AudioFile.DetectSilence(this.FullFilePath);
				}
				catch (Exception ex2)
				{
					MessageBox.Show(rPath + " processing error: " + ex2.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
					this.isDelay = false;
				}
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0001EA14 File Offset: 0x0001CC14
		private static AFType DetectAF(string FullFilePath)
		{
			try
			{
				new WaveFileReader(FullFilePath).Dispose();
				return AFType.wav;
			}
			catch
			{
			}
			try
			{
				new Mp3FileReader(FullFilePath).Dispose();
				return AFType.mp3;
			}
			catch
			{
			}
			try
			{
				new VorbisReader(FullFilePath).Dispose();
				return AFType.ogg;
			}
			catch
			{
			}
			return AFType.none;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0001EA84 File Offset: 0x0001CC84
		[HandleProcessCorruptedStateExceptions]
		private static double GetLength(string FullFilePath)
		{
			double num = -1.0;
			try
			{
				double.TryParse(ShellFile.FromFilePath(FullFilePath).Properties.System.Media.Duration.Value.ToString(), out num);
				num /= 10000.0;
			}
			catch
			{
				num = AudioFile.GetLengthNA(FullFilePath);
			}
			return num;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0001EAFC File Offset: 0x0001CCFC
		private static double GetLengthNA(string strPath)
		{
			double result;
			try
			{
				WaveFileReader waveFileReader = new WaveFileReader(strPath);
				double totalMilliseconds = waveFileReader.TotalTime.TotalMilliseconds;
				waveFileReader.Dispose();
				result = totalMilliseconds;
			}
			catch
			{
				result = -1.0;
			}
			return result;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0001EB48 File Offset: 0x0001CD48
		private static bool DetectSilence(string strPath)
		{
			List<float> list = new List<float>();
			float num = -0.12f;
			float num2 = 0.12f;
			float num3 = 0f;
			short num4 = 0;
			WaveFileReader waveFileReader = new WaveFileReader(strPath);
			WaveChannel32 waveChannel = new WaveChannel32(waveFileReader);
			byte[] array = new byte[waveChannel.Length];
			bool flag = false;
			bool flag2 = true;
			if (waveFileReader.WaveFormat.Channels == 2)
			{
				flag = true;
			}
			float num5 = 0f;
			while (waveChannel.Position < waveChannel.Length)
			{
				int num6 = waveChannel.Read(array, 0, Convert.ToInt32(waveChannel.Length));
				for (int i = 0; i < num6 / 4; i++)
				{
					float num7 = BitConverter.ToSingle(array, i * 4);
					if (flag)
					{
						if (flag2)
						{
							num3 = num7;
							flag2 = false;
						}
						else
						{
							if (Math.Abs(num7) > Math.Abs(num3))
							{
								list.Add(num7);
							}
							else
							{
								list.Add(num3);
							}
							flag2 = true;
						}
					}
					else
					{
						list.Add(num7);
					}
					if (Math.Abs(num7) > num5)
					{
						num5 = Math.Abs(num7);
					}
				}
			}
			float num8 = 1f / num5;
			for (int j = 0; j < list.Count; j++)
			{
				list[j] *= num8;
			}
			waveChannel.Dispose();
			if (list.Count > 220)
			{
				for (int k = 0; k < 220; k++)
				{
					if (list[k] > num && list[k] < num2)
					{
						num4 += 1;
					}
				}
			}
			return num4 > 200;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x0001ECD8 File Offset: 0x0001CED8
		public static int GetMp3Bitrate(string strPath)
		{
			int result;
			try
			{
				Mp3FileReader mp3FileReader = new Mp3FileReader(strPath);
				int num = Convert.ToInt32(Math.Floor((double)(mp3FileReader.Mp3WaveFormat.AverageBytesPerSecond * 8) / 1000.0));
				mp3FileReader.Dispose();
				result = num;
			}
			catch
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x04000182 RID: 386
		public string rFilePath = string.Empty;

		// Token: 0x04000183 RID: 387
		private string BeatmapPath = string.Empty;

		// Token: 0x04000184 RID: 388
		public AFType Type;

		// Token: 0x04000185 RID: 389
		public float Length;

		// Token: 0x04000186 RID: 390
		public bool isDelay;
	}
}
