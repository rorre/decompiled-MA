using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using osu.Processor;

namespace osu.Beatmap.Fruits
{
	// Token: 0x02000041 RID: 65
	public class FruitObject
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000299 RID: 665 RVA: 0x000233EA File Offset: 0x000215EA
		public bool HyperDash
		{
			get
			{
				return this.HyperDashTarget != null;
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x000233F5 File Offset: 0x000215F5
		public FruitObject(HitObject parentHO)
		{
			this.parentHO = parentHO;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00023404 File Offset: 0x00021604
		internal void MakeHyperDash(HitObject target)
		{
			if (this.HyperDash)
			{
				return;
			}
			this.DistanceToHyperDash = 0f;
			this.HyperDashTarget = target;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00023424 File Offset: 0x00021624
		private HitObject CreateDummy(int Time, Vector2 Position, FruitType Type)
		{
			HitObject hitObject = new HitObject();
			hitObject.StartTime = Time;
			hitObject.EndTime = hitObject.StartTime;
			hitObject.Position = new Vector2(Position.X, Position.Y);
			hitObject.Type = HitObjectType.Normal;
			hitObject.Fruit = new FruitObject(hitObject);
			hitObject.Fruit.Type = Type;
			return hitObject;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00023480 File Offset: 0x00021680
		public List<HitObject> GetBonusObjects()
		{
			List<HitObject> list = new List<HitObject>();
			if (this.parentHO.IsType(HitObjectType.Slider))
			{
				Difficulty parentBD = this.parentHO.parentBD;
				if (parentBD.TimingPoints.Count < 1)
				{
					return list;
				}
				ObjectsProcessor.getTimeLineIndex(parentBD, this.parentHO.StartTime, 5);
				int sliderEdgeTiming = ObjectsProcessor.getSliderEdgeTiming(parentBD, this.parentHO, this.parentHO.EdgesCount);
				Vector2 position = this.parentHO.curve.PositionAtTime(sliderEdgeTiming);
				list.Add(this.CreateDummy(sliderEdgeTiming, position, FruitType.HitCircleFruitsNormal));
				List<int> list2 = new List<int>();
				for (int i2 = 0; i2 < this.parentHO.EdgesCount; i2++)
				{
					int sliderEdgeTiming2 = ObjectsProcessor.getSliderEdgeTiming(parentBD, this.parentHO, i2 + 1);
					ObjectsProcessor.getTimeLineIndex(parentBD, sliderEdgeTiming2, 5);
					list2.Add(sliderEdgeTiming2);
					if (sliderEdgeTiming2 != this.parentHO.StartTime && sliderEdgeTiming2 != sliderEdgeTiming)
					{
						position = this.parentHO.curve.PositionAtTime(sliderEdgeTiming2);
						list.Add(this.CreateDummy(sliderEdgeTiming2, position, FruitType.HitCircleFruitsNormal));
					}
				}
				List<int> list3 = new List<int>();
				double num = parentBD.TimingPoints[ObjectsProcessor.getTimeLineIndex(parentBD, this.parentHO.StartTime, 0)].msPerBeat;
				if (num < 0.0)
				{
					num = num * -1.0 / 100.0;
				}
				else
				{
					num = 1.0;
				}
				Convert.ToInt32(Math.Floor((double)(this.parentHO.SpatialLength / 100f) * num / (double)parentBD.SliderMultiplier * (double)parentBD.SliderTickRate));
				double msPerBeat = parentBD.TimingPoints[ObjectsProcessor.getBPMTimeLineIndex(parentBD, this.parentHO.StartTime)].msPerBeat;
				bool flag = true;
				List<float> list4 = new List<float>();
				List<float> list5 = new List<float>();
				float num2 = Convert.ToSingle(Math.Ceiling(msPerBeat / (double)parentBD.SliderTickRate));
				int num3 = 2;
				while ((float)this.parentHO.StartTime + num2 < (float)list2[1])
				{
					list4.Add(num2);
					num2 = Convert.ToSingle(Math.Floor(msPerBeat / (double)parentBD.SliderTickRate * (double)num3));
					num3++;
				}
				for (int j = list4.Count - 1; j >= 0; j--)
				{
					list5.Add((float)(list2[1] - this.parentHO.StartTime) - list4[j]);
				}
				if (list4.Count > 0)
				{
					for (int k = 0; k < list2.Count - 1; k++)
					{
						if (flag)
						{
							flag = false;
							for (int l = 0; l < list4.Count; l++)
							{
								int item = Convert.ToInt32(Math.Floor((double)((float)list2[k] + list4[l])));
								if (!list2.Contains(item))
								{
									list3.Add(item);
								}
							}
						}
						else
						{
							flag = true;
							for (int m = 0; m < list5.Count; m++)
							{
								int item = Convert.ToInt32(Math.Floor((double)((float)list2[k] + list5[m])));
								if (!list2.Contains(item))
								{
									list3.Add(item);
								}
							}
						}
					}
				}
				for (int n = 0; n < list3.Count; n++)
				{
					position = this.parentHO.curve.PositionAtTime(list3[n]);
					list.Add(this.CreateDummy(list3[n], position, FruitType.HitCircleFruitsNormal));
				}
			}
			this.parentHO.EndPosition = this.parentHO.Position;
			this.parentHO.Type = HitObjectType.Normal;
			this.parentHO.EndTime = this.parentHO.StartTime;
			return (from i in list
			orderby i.StartTime
			select i).ToList<HitObject>();
		}

		// Token: 0x0400022C RID: 556
		public HitObject parentHO;

		// Token: 0x0400022D RID: 557
		public float DistanceToHyperDash;

		// Token: 0x0400022E RID: 558
		public HitObject HyperDashTarget;

		// Token: 0x0400022F RID: 559
		public FruitType Type;
	}
}
