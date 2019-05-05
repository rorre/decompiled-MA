using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using osu.Beatmap.curves;
using osu.Beatmap.Fruits;
using osu.Beatmap.Mania;
using osu.Processor;

namespace osu.Beatmap
{
	// Token: 0x0200003E RID: 62
	public class HitObject
	{
		// Token: 0x06000283 RID: 643 RVA: 0x000228C7 File Offset: 0x00020AC7
		public bool IsType(HitObjectType type)
		{
			return this.Type.IsType(type);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000284 RID: 644 RVA: 0x000228D5 File Offset: 0x00020AD5
		public int TotalLength
		{
			get
			{
				return this.EndTime - this.StartTime;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000285 RID: 645 RVA: 0x000228E4 File Offset: 0x00020AE4
		public int EdgesCount
		{
			get
			{
				return this.SegmentCount + 1;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000286 RID: 646 RVA: 0x000228EE File Offset: 0x00020AEE
		// (set) Token: 0x06000287 RID: 647 RVA: 0x000228FC File Offset: 0x00020AFC
		public bool Whistle
		{
			get
			{
				return this.HitSound.IsType(HitObjectHSType.Whistle);
			}
			set
			{
				if (value)
				{
					this.HitSound |= HitObjectHSType.Whistle;
					return;
				}
				this.HitSound &= ~HitObjectHSType.Whistle;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0002291F File Offset: 0x00020B1F
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0002292D File Offset: 0x00020B2D
		public bool Finish
		{
			get
			{
				return this.HitSound.IsType(HitObjectHSType.Finish);
			}
			set
			{
				if (value)
				{
					this.HitSound |= HitObjectHSType.Finish;
					return;
				}
				this.HitSound &= ~HitObjectHSType.Finish;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600028A RID: 650 RVA: 0x00022950 File Offset: 0x00020B50
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0002295E File Offset: 0x00020B5E
		public bool Clap
		{
			get
			{
				return this.HitSound.IsType(HitObjectHSType.Clap);
			}
			set
			{
				if (value)
				{
					this.HitSound |= HitObjectHSType.Clap;
					return;
				}
				this.HitSound &= ~HitObjectHSType.Clap;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600028C RID: 652 RVA: 0x00022981 File Offset: 0x00020B81
		public bool isDon
		{
			get
			{
				return !this.Clap && !this.Whistle;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600028D RID: 653 RVA: 0x00022998 File Offset: 0x00020B98
		public string TaikoStr
		{
			get
			{
				if (this.isDon && this.Finish)
				{
					return "D";
				}
				if (this.isDon && !this.Finish)
				{
					return "d";
				}
				if (!this.isDon && this.Finish)
				{
					return "K";
				}
				if (!this.isDon && !this.Finish)
				{
					return "k";
				}
				return "?";
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600028E RID: 654 RVA: 0x00022A02 File Offset: 0x00020C02
		// (set) Token: 0x0600028F RID: 655 RVA: 0x00022A0B File Offset: 0x00020C0B
		public bool NewCombo
		{
			get
			{
				return this.IsType(HitObjectType.NewCombo);
			}
			set
			{
				if (value)
				{
					this.Type |= HitObjectType.NewCombo;
					return;
				}
				this.Type &= ~HitObjectType.NewCombo;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00022A2E File Offset: 0x00020C2E
		public Curve getSliderCurve()
		{
			return new Curve(this);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00022A38 File Offset: 0x00020C38
		public HitObject()
		{
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00022AD4 File Offset: 0x00020CD4
		public HitObject(string line, Difficulty parentBD)
		{
			this.parentBD = parentBD;
			List<string> list = new List<string>(from str in line.Split(Constants.Splitter.Comma, StringSplitOptions.RemoveEmptyEntries)
			select str.Trim());
			for (int i = 0; i < list.Count; i++)
			{
				list[i] = list[i].TrimEnd(new char[]
				{
					':'
				});
			}
			float num;
			float.TryParse(list[0], out num);
			float num2;
			float.TryParse(list[1], out num2);
			this.Position.X = (float)Math.Floor((double)num);
			this.Position.Y = (float)Math.Floor((double)num2);
			this.EndPosition = new Vector2(this.Position.X, this.Position.Y);
			float num3;
			float.TryParse(list[2], out num3);
			this.StartTime = Convert.ToInt32(Math.Floor((double)num3));
			this.EndTime = this.StartTime;
			Enum.TryParse<HitObjectType>(list[3], out this.Type);
			Enum.TryParse<HitObjectHSType>(list[4], true, out this.HitSound);
			if (this.Type.IsType(HitObjectType.Normal))
			{
				if (list.Count > 5)
				{
					HitObject.GetAdditions(list[5], this);
				}
			}
			else if (this.Type.IsType(HitObjectType.Spinner))
			{
				parentBD.isSpinner = true;
				if (list.Count > 5)
				{
					int.TryParse(list[5], out this.EndTime);
				}
				if (list.Count > 6)
				{
					HitObject.GetAdditions(list[6], this);
				}
			}
			else if (this.Type.IsType(HitObjectType.Hold))
			{
				if (list.Count > 5)
				{
					HitObject.GetAdditions(list[5], this);
				}
			}
			else if (this.Type.IsType(HitObjectType.Slider))
			{
				if (list.Count > 6)
				{
					int.TryParse(list[6], out this.SegmentCount);
				}
				if (list.Count > 7)
				{
					float.TryParse(list[7], out this.SpatialLength);
				}
				if (list.Count > 5)
				{
					List<string> list2 = new List<string>(from str in list[5].Split(Constants.Splitter.Pipe, StringSplitOptions.RemoveEmptyEntries)
					select str.Trim());
					string text = list2[0];
					uint num4 = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num4 <= 3574337935u)
					{
						if (num4 <= 3339451269u)
						{
							if (num4 != 3322673650u)
							{
								if (num4 != 3339451269u)
								{
									goto IL_420;
								}
								if (!(text == "B"))
								{
									goto IL_420;
								}
								goto IL_40E;
							}
							else if (!(text == "C"))
							{
								goto IL_420;
							}
						}
						else if (num4 != 3373006507u)
						{
							if (num4 != 3574337935u)
							{
								goto IL_420;
							}
							if (!(text == "P"))
							{
								goto IL_420;
							}
							goto IL_41A;
						}
						else
						{
							if (!(text == "L"))
							{
								goto IL_420;
							}
							goto IL_414;
						}
					}
					else if (num4 <= 3876335077u)
					{
						if (num4 != 3859557458u)
						{
							if (num4 != 3876335077u)
							{
								goto IL_420;
							}
							if (!(text == "b"))
							{
								goto IL_420;
							}
							goto IL_40E;
						}
						else if (!(text == "c"))
						{
							goto IL_420;
						}
					}
					else if (num4 != 3909890315u)
					{
						if (num4 != 4111221743u)
						{
							goto IL_420;
						}
						if (!(text == "p"))
						{
							goto IL_420;
						}
						goto IL_41A;
					}
					else
					{
						if (!(text == "l"))
						{
							goto IL_420;
						}
						goto IL_414;
					}
					SliderType sliderType = SliderType.Catmull;
					goto IL_424;
					IL_40E:
					sliderType = SliderType.Bezier;
					goto IL_424;
					IL_414:
					sliderType = SliderType.Linear;
					goto IL_424;
					IL_41A:
					sliderType = SliderType.PSlider;
					goto IL_424;
					IL_420:
					sliderType = SliderType.Linear;
					IL_424:
					this.SliderType = sliderType;
					this.sliderCurvePoints.Add(new Vector2(this.Position.X, this.Position.Y));
					for (int j = 1; j < list2.Count; j++)
					{
						float.TryParse(list2[j].Split(Constants.Splitter.Colon)[0], out num);
						float.TryParse(list2[j].Split(Constants.Splitter.Colon)[1], out num2);
						this.sliderCurvePoints.Add(new Vector2((float)Math.Floor((double)num), (float)Math.Floor((double)num2)));
					}
					if (list.Count > 8)
					{
						List<string> list3 = new List<string>(from str in list[8].Split(Constants.Splitter.Pipe, StringSplitOptions.RemoveEmptyEntries)
						select str.Trim());
						for (int k = 0; k < list3.Count; k++)
						{
							this.EdgesHS.Add((HitObjectHSType)int.Parse(list3[k]));
						}
					}
					else
					{
						for (int l = 0; l < this.EdgesCount; l++)
						{
							this.EdgesHS.Add(HitObjectHSType.Normal);
						}
					}
					if (list.Count > 9)
					{
						List<string> list4 = new List<string>(from str in list[9].Split(Constants.Splitter.Pipe, StringSplitOptions.RemoveEmptyEntries)
						select str.Trim());
						for (int m = 0; m < list4.Count; m++)
						{
							string[] array = list4[m].Split(new char[]
							{
								':'
							});
							int mSet = 0;
							int sSet = 0;
							int.TryParse(array[0], out mSet);
							int.TryParse(array[1], out sSet);
							this.EdgesHS_Additions.Add(new HitObject_Additions((HitsoundSetType)mSet, (HitsoundSetType)sSet));
						}
					}
					else
					{
						for (int n = 0; n < this.EdgesCount; n++)
						{
							this.EdgesHS_Additions.Add(new HitObject_Additions(HitsoundSetType.Auto, HitsoundSetType.Auto));
						}
					}
					if (list.Count > 10)
					{
						HitObject.GetAdditions(list[10], this);
					}
					this.EndTime = ObjectsProcessor.getSliderEdgeTiming(parentBD, this, this.EdgesCount);
					this.curve = this.getSliderCurve();
					this.EndPosition = this.curve.curve[this.curve.curve.Count - 1];
				}
			}
			if (parentBD.DifficlutyMode == DifficlutyModeType.Mania)
			{
				this.Column = osu.Beatmap.Mania.Column.GetColumn(parentBD.ColumnsCount, Convert.ToInt32(Math.Floor((double)this.Position.X)));
			}
			if (parentBD.DifficlutyMode == DifficlutyModeType.CtB)
			{
				this.Fruit = new FruitObject(this);
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000231B4 File Offset: 0x000213B4
		private static void GetAdditions(string part, HitObject ho)
		{
			if (string.IsNullOrEmpty(part))
			{
				return;
			}
			List<string> list = new List<string>(from s in part.Split(Constants.Splitter.Colon, StringSplitOptions.RemoveEmptyEntries)
			select s.Trim());
			if (ho.IsType(HitObjectType.Hold))
			{
				if (list.Count < 1)
				{
					return;
				}
				int.TryParse(list[0], out ho.EndTime);
				if (list.Count < 3)
				{
					return;
				}
				Enum.TryParse<HitsoundSetType>(list[1], out ho.Additions.mainSet);
				Enum.TryParse<HitsoundSetType>(list[2], out ho.Additions.secondarySet);
				if (list.Count < 6)
				{
					return;
				}
				int.TryParse(list[4], out ho.maniaSampleVolume);
				ho.ManiaSample = list[5];
				return;
			}
			else
			{
				if (list.Count < 2)
				{
					return;
				}
				Enum.TryParse<HitsoundSetType>(list[0], out ho.Additions.mainSet);
				Enum.TryParse<HitsoundSetType>(list[1], out ho.Additions.secondarySet);
				if (list.Count < 5)
				{
					return;
				}
				int.TryParse(list[3], out ho.maniaSampleVolume);
				ho.ManiaSample = list[4];
				return;
			}
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000232F8 File Offset: 0x000214F8
		public override string ToString()
		{
			TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)this.StartTime);
			return string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds) + " (" + this.ComboNumber.ToString() + ")";
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0002335C File Offset: 0x0002155C
		public string EndTimeStr
		{
			get
			{
				TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)this.EndTime);
				return string.Format("{0:D2}:{1:D2}:{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
			}
		}

		// Token: 0x04000218 RID: 536
		public Difficulty parentBD;

		// Token: 0x04000219 RID: 537
		public Vector2 Position = new Vector2(0f, 0f);

		// Token: 0x0400021A RID: 538
		public int StartTime;

		// Token: 0x0400021B RID: 539
		public float SpatialLength;

		// Token: 0x0400021C RID: 540
		public HitObjectType Type = HitObjectType.Normal;

		// Token: 0x0400021D RID: 541
		public HitObjectHSType HitSound;

		// Token: 0x0400021E RID: 542
		public HitObject_Additions Additions = new HitObject_Additions(HitsoundSetType.Auto, HitsoundSetType.Auto);

		// Token: 0x0400021F RID: 543
		public int EndTime;

		// Token: 0x04000220 RID: 544
		public Vector2 EndPosition = new Vector2(0f, 0f);

		// Token: 0x04000221 RID: 545
		public int ComboNumber = 1;

		// Token: 0x04000222 RID: 546
		public int maniaSampleVolume;

		// Token: 0x04000223 RID: 547
		public string ManiaSample = string.Empty;

		// Token: 0x04000224 RID: 548
		public int Column = 1;

		// Token: 0x04000225 RID: 549
		public SliderType SliderType = SliderType.None;

		// Token: 0x04000226 RID: 550
		public List<Vector2> sliderCurvePoints = new List<Vector2>();

		// Token: 0x04000227 RID: 551
		public int SegmentCount = 1;

		// Token: 0x04000228 RID: 552
		public List<HitObjectHSType> EdgesHS = new List<HitObjectHSType>();

		// Token: 0x04000229 RID: 553
		public List<HitObject_Additions> EdgesHS_Additions = new List<HitObject_Additions>();

		// Token: 0x0400022A RID: 554
		public Curve curve;

		// Token: 0x0400022B RID: 555
		public FruitObject Fruit;
	}
}
