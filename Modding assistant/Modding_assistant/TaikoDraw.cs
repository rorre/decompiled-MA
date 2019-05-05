using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using osu;
using osu.Beatmap;

namespace Modding_assistant
{
	// Token: 0x02000003 RID: 3
	internal class TaikoDraw
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00002168 File Offset: 0x00000368
		public TaikoDraw(BeatmapInfo bi, ScrollViewer parentSW, int zoom, int DetailIndex)
		{
			int num = 0;
			for (int i = 0; i < bi.Difficluties.Count; i++)
			{
				if (bi.Difficluties[i].DifficlutyMode == DifficlutyModeType.Taiko)
				{
					num++;
				}
			}
			if (num == 0 || parentSW == null)
			{
				return;
			}
			this.parentSP = new StackPanel();
			this.parentSP.HorizontalAlignment = HorizontalAlignment.Left;
			parentSW.Content = this.parentSP;
			this.parentSW = parentSW;
			this.zoom = Math.Max(1000, zoom);
			this.bi = bi;
			this.DetailIndex = DetailIndex;
			this.parentSP.Width = (double)zoom;
			this.Difficulties = new TaikoDraw.diffs(num);
			for (int j = 0; j < bi.Difficluties.Count; j++)
			{
				if (bi.Difficluties[j].DifficlutyMode == DifficlutyModeType.Taiko)
				{
					this.Difficulties.DiffIndex.Add(j);
				}
			}
			for (int k = 0; k < this.Difficulties.DiffIndex.Count; k++)
			{
				this.SongLength = Math.Max(this.SongLength, bi.Difficluties[this.Difficulties.DiffIndex[k]].EndTime);
			}
			this.CreateCanvas();
			this.CreateTimeLineBase();
			this.AddCaptions();
			this.DrawDownbeats();
			this.DrawObjects();
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022D4 File Offset: 0x000004D4
		private void CreateCanvas()
		{
			for (int i = 0; i < this.Difficulties.CanvasList.Capacity; i++)
			{
				Canvas canvas = new Canvas();
				canvas.Height = (double)this.CanvasHeight;
				canvas.Width = (double)this.zoom;
				canvas.Background = new SolidColorBrush(Colors.Gray);
				this.Difficulties.CanvasList.Add(canvas);
				this.parentSP.Children.Add(canvas);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002350 File Offset: 0x00000550
		private void CreateTimeLineBase()
		{
			for (int i = 0; i < this.Difficulties.CanvasList.Count; i++)
			{
				Line line = new Line();
				line.Stroke = Brushes.LightSteelBlue;
				line.X1 = 0.0;
				line.X2 = (double)this.zoom;
				line.Y1 = (double)this.CanvasHeight;
				line.Y2 = (double)this.CanvasHeight;
				line.StrokeThickness = 2.0;
				this.Difficulties.CanvasList[i].Children.Add(line);
				this.parentSP.Children[i] = this.Difficulties.CanvasList[i];
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002414 File Offset: 0x00000614
		private void AddCaptions()
		{
			for (int i = 0; i < this.Difficulties.CanvasList.Count; i++)
			{
				TextBlock textBlock = new TextBlock();
				textBlock.Text = this.bi.Difficluties[this.Difficulties.DiffIndex[i]].Version;
				textBlock.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(250, 231, 181));
				textBlock.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0));
				Canvas.SetLeft(textBlock, 5.0);
				Canvas.SetTop(textBlock, 20.0);
				this.Difficulties.CanvasList[i].Children.Add(textBlock);
				this.parentSP.Children[i] = this.Difficulties.CanvasList[i];
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002504 File Offset: 0x00000704
		private void DrawDownbeats()
		{
			for (int i = 0; i < this.Difficulties.CanvasList.Count; i++)
			{
				bool flag = true;
				for (int j = 0; j < this.bi.Difficluties[this.Difficulties.DiffIndex[i]].TimingPoints.Count; j++)
				{
					if (this.bi.Difficluties[this.Difficulties.DiffIndex[i]].TimingPoints[j].unInherited)
					{
						TimingPoint timingPoint = this.bi.Difficluties[this.Difficulties.DiffIndex[i]].TimingPoints[j];
						TimingPoint timingPoint2 = null;
						if (flag)
						{
							flag = false;
							double msPerBeat = timingPoint.msPerBeat;
							double num = (double)timingPoint.time;
							for (num -= msPerBeat; num >= 0.0; num -= msPerBeat)
							{
								this.Difficulties.CanvasList[i].Children.Add(this.CreateDownbeat(num, false));
								this.parentSP.Children[i] = this.Difficulties.CanvasList[i];
							}
						}
						for (int k = j + 1; k < this.bi.Difficluties[this.Difficulties.DiffIndex[i]].TimingPoints.Count; k++)
						{
							if (this.bi.Difficluties[this.Difficulties.DiffIndex[i]].TimingPoints[k].unInherited)
							{
								timingPoint2 = this.bi.Difficluties[this.Difficulties.DiffIndex[i]].TimingPoints[k];
								break;
							}
						}
						if (timingPoint2 != null)
						{
							double msPerBeat2 = timingPoint.msPerBeat;
							double num2 = (double)timingPoint.time;
							int num3 = 0;
							while (num2 < (double)timingPoint2.time)
							{
								double num4 = msPerBeat2;
								int num5 = 1;
								switch (this.DetailIndex)
								{
								case 1:
									num4 = msPerBeat2 / 2.0;
									break;
								case 2:
									num4 = msPerBeat2 / 3.0;
									break;
								case 3:
									num4 = msPerBeat2 / 4.0;
									break;
								case 4:
									num4 = msPerBeat2 / 6.0;
									break;
								case 5:
									num4 = msPerBeat2 / 8.0;
									break;
								}
								double num6 = num2 + num4;
								while (num6 < num2 + msPerBeat2 - 5.0)
								{
									switch (this.DetailIndex)
									{
									case 1:
										this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num6, Brushes.Red));
										break;
									case 2:
										this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num6, Brushes.Violet));
										break;
									case 3:
										if (num5 % 2 == 0)
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num6, Brushes.Red));
										}
										else
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num6, Brushes.Blue));
										}
										break;
									case 4:
										if (num5 % 3 == 0)
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num6, Brushes.Red));
										}
										else
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num6, Brushes.Violet));
										}
										break;
									case 5:
										if (num5 % 4 == 0)
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num6, Brushes.Red));
										}
										else if (num5 % 2 == 0)
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num6, Brushes.Blue));
										}
										else
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num6, Brushes.Yellow));
										}
										break;
									}
									this.parentSP.Children[i] = this.Difficulties.CanvasList[i];
									num6 += num4;
									num5++;
								}
								this.Difficulties.CanvasList[i].Children.Add(this.CreateDownbeat(num2, num3 % timingPoint.measure == 0));
								this.parentSP.Children[i] = this.Difficulties.CanvasList[i];
								if (num3 % timingPoint.measure == 0)
								{
									this.Difficulties.CanvasList[i].Children.Add(this.CreateTimeStamp(num2));
									this.parentSP.Children[i] = this.Difficulties.CanvasList[i];
								}
								num3++;
								num2 += msPerBeat2;
							}
						}
						else if (timingPoint2 == null)
						{
							double msPerBeat3 = timingPoint.msPerBeat;
							double num7 = (double)timingPoint.time;
							int num3 = 0;
							while (num7 < (double)this.SongLength)
							{
								double num8 = msPerBeat3;
								int num9 = 1;
								switch (this.DetailIndex)
								{
								case 1:
									num8 = msPerBeat3 / 2.0;
									break;
								case 2:
									num8 = msPerBeat3 / 3.0;
									break;
								case 3:
									num8 = msPerBeat3 / 4.0;
									break;
								case 4:
									num8 = msPerBeat3 / 6.0;
									break;
								case 5:
									num8 = msPerBeat3 / 8.0;
									break;
								}
								double num10 = num7 + num8;
								while (num10 < num7 + msPerBeat3 - 5.0)
								{
									switch (this.DetailIndex)
									{
									case 1:
										this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num10, Brushes.Red));
										break;
									case 2:
										this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num10, Brushes.Violet));
										break;
									case 3:
										if (num9 % 2 == 0)
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num10, Brushes.Red));
										}
										else
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num10, Brushes.Blue));
										}
										break;
									case 4:
										if (num9 % 3 == 0)
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num10, Brushes.Red));
										}
										else
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num10, Brushes.Violet));
										}
										break;
									case 5:
										if (num9 % 4 == 0)
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num10, Brushes.Red));
										}
										else if (num9 % 2 == 0)
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num10, Brushes.Blue));
										}
										else
										{
											this.Difficulties.CanvasList[i].Children.Add(this.CreateBeat(num10, Brushes.Yellow));
										}
										break;
									}
									this.parentSP.Children[i] = this.Difficulties.CanvasList[i];
									num10 += num8;
									num9++;
								}
								this.Difficulties.CanvasList[i].Children.Add(this.CreateDownbeat(num7, num3 % timingPoint.measure == 0));
								this.parentSP.Children[i] = this.Difficulties.CanvasList[i];
								if (num3 % timingPoint.measure == 0)
								{
									this.Difficulties.CanvasList[i].Children.Add(this.CreateTimeStamp(num7));
									this.parentSP.Children[i] = this.Difficulties.CanvasList[i];
								}
								num3++;
								num7 += msPerBeat3;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002E00 File Offset: 0x00001000
		private void DrawObjects()
		{
			for (int i = 0; i < this.Difficulties.CanvasList.Count; i++)
			{
				for (int j = 0; j < this.bi.Difficluties[this.Difficulties.DiffIndex[i]].Objects.Count; j++)
				{
					HitObject hitObject = this.bi.Difficluties[this.Difficulties.DiffIndex[i]].Objects[j];
					if (hitObject.IsType(HitObjectType.Normal))
					{
						this.Difficulties.CanvasList[i].Children.Add(this.CreateEllips(hitObject));
						this.parentSP.Children[i] = this.Difficulties.CanvasList[i];
					}
					if (hitObject.IsType(HitObjectType.Slider) || hitObject.IsType(HitObjectType.Spinner))
					{
						this.Difficulties.CanvasList[i].Children.Add(this.CreateRectangle(hitObject));
						this.parentSP.Children[i] = this.Difficulties.CanvasList[i];
					}
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002F3C File Offset: 0x0000113C
		private Ellipse CreateEllips(HitObject HO)
		{
			double num = (double)HO.StartTime / ((double)this.SongLength / (double)this.zoom);
			Ellipse ellipse = new Ellipse();
			SolidColorBrush solidColorBrush = new SolidColorBrush();
			if (HO.isDon)
			{
				solidColorBrush.Color = System.Windows.Media.Color.FromArgb(100, 211, 33, 45);
			}
			else
			{
				solidColorBrush.Color = System.Windows.Media.Color.FromArgb(100, 0, 48, 143);
			}
			int num2 = 30;
			if (HO.Finish)
			{
				num2 = 40;
			}
			ellipse.Fill = solidColorBrush;
			ellipse.StrokeThickness = 2.0;
			ellipse.Stroke = Brushes.White;
			ellipse.Width = (double)num2;
			ellipse.Height = (double)num2;
			Canvas.SetLeft(ellipse, num - (double)(num2 / 2));
			Canvas.SetTop(ellipse, (double)(this.pHeight - num2));
			return ellipse;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002FFC File Offset: 0x000011FC
		private Rectangle CreateRectangle(HitObject HO)
		{
			double num = (double)HO.StartTime / ((double)this.SongLength / (double)this.zoom);
			double num2 = (double)HO.EndTime / ((double)this.SongLength / (double)this.zoom);
			Rectangle rectangle = new Rectangle();
			SolidColorBrush solidColorBrush = new SolidColorBrush();
			solidColorBrush.Color = System.Windows.Media.Color.FromRgb(202, 224, 13);
			int num3 = 15;
			if (HO.Finish)
			{
				num3 = 25;
			}
			if (HO.IsType(HitObjectType.Spinner))
			{
				solidColorBrush.Color = System.Windows.Media.Color.FromRgb(254, 111, 94);
				num3 = 25;
			}
			rectangle.Fill = solidColorBrush;
			rectangle.StrokeThickness = 2.0;
			rectangle.Stroke = Brushes.White;
			rectangle.Width = num2 - num;
			rectangle.Height = (double)num3;
			Canvas.SetLeft(rectangle, num);
			Canvas.SetTop(rectangle, (double)(this.pHeight - num3));
			return rectangle;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000030D4 File Offset: 0x000012D4
		private Line CreateDownbeat(double offset, bool downbeat)
		{
			double num = offset / ((double)this.SongLength / (double)this.zoom);
			Line line = new Line();
			line.Stroke = Brushes.White;
			line.X1 = num;
			line.X2 = num;
			line.Y1 = (double)this.pHeight;
			line.Y2 = (double)this.pHeight - 15.0;
			if (downbeat)
			{
				line.Y2 = (double)this.pHeight - 25.0;
			}
			line.StrokeThickness = 1.0;
			if (downbeat)
			{
				line.StrokeThickness = 2.0;
			}
			return line;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003174 File Offset: 0x00001374
		private Line CreateBeat(double offset, SolidColorBrush clr)
		{
			double num = offset / ((double)this.SongLength / (double)this.zoom);
			return new Line
			{
				Stroke = clr,
				X1 = num,
				X2 = num,
				Y1 = (double)this.pHeight,
				Y2 = (double)this.pHeight - 10.0,
				StrokeThickness = 1.0
			};
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000031E0 File Offset: 0x000013E0
		private TextBlock CreateTimeStamp(double offset)
		{
			double length = offset / ((double)this.SongLength / (double)this.zoom);
			TextBlock textBlock = new TextBlock();
			TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)((int)offset));
			textBlock.Text = string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
			textBlock.Foreground = new SolidColorBrush(System.Windows.Media.Color.FromRgb(byte.MaxValue, byte.MaxValue, byte.MaxValue));
			textBlock.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0, 0, 0, 0));
			Canvas.SetLeft(textBlock, length);
			Canvas.SetTop(textBlock, 5.0);
			return textBlock;
		}

		// Token: 0x04000007 RID: 7
		private int pHeight = 70;

		// Token: 0x04000008 RID: 8
		private int CanvasHeight = 70;

		// Token: 0x04000009 RID: 9
		private int SongLength = 1;

		// Token: 0x0400000A RID: 10
		private int DetailIndex;

		// Token: 0x0400000B RID: 11
		private TaikoDraw.diffs Difficulties;

		// Token: 0x0400000C RID: 12
		private int zoom;

		// Token: 0x0400000D RID: 13
		private ScrollViewer parentSW;

		// Token: 0x0400000E RID: 14
		private StackPanel parentSP;

		// Token: 0x0400000F RID: 15
		private BeatmapInfo bi;

		// Token: 0x02000055 RID: 85
		private class diffs
		{
			// Token: 0x06000300 RID: 768 RVA: 0x00026D48 File Offset: 0x00024F48
			public diffs(int capacity)
			{
				this.CanvasList = new List<Canvas>(capacity);
				this.DiffIndex = new List<int>(capacity);
			}

			// Token: 0x0400028C RID: 652
			public List<Canvas> CanvasList;

			// Token: 0x0400028D RID: 653
			public List<int> DiffIndex;
		}
	}
}
