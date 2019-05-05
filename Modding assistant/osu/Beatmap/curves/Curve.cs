using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using osu.Graphics.Primitives;

namespace osu.Beatmap.curves
{
	// Token: 0x02000043 RID: 67
	public class Curve
	{
		// Token: 0x060002A1 RID: 673 RVA: 0x000239A8 File Offset: 0x00021BA8
		protected internal Curve(HitObject hitObject)
		{
			this.hitObject = hitObject;
			this.x = hitObject.Position.X;
			this.y = hitObject.Position.Y;
			this.sliderXY = new List<Vector2>(hitObject.sliderCurvePoints);
			this.UpdateCalculations();
			this.FillPoints();
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00023A24 File Offset: 0x00021C24
		internal virtual void FillPoints()
		{
			for (int i = this.hitObject.StartTime; i <= this.hitObject.EndTime; i++)
			{
				this.curve.Add(this.PositionAtTime(i));
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00023A64 File Offset: 0x00021C64
		public Vector2 PositionAtTime(int time)
		{
			if (time == this.hitObject.EndTime)
			{
				if (this.hitObject.SegmentCount % 2 == 0)
				{
					return new Vector2(this.hitObject.Position.X, this.hitObject.Position.Y);
				}
				return this.positionAtLength(this.hitObject.SpatialLength);
			}
			else
			{
				if (time == this.hitObject.StartTime)
				{
					return new Vector2(this.hitObject.Position.X, this.hitObject.Position.Y);
				}
				if (time < this.hitObject.StartTime || time > this.hitObject.EndTime)
				{
					return new Vector2(0f, 0f);
				}
				float num = (float)(time - this.hitObject.StartTime) / ((float)this.hitObject.TotalLength / (float)this.hitObject.SegmentCount);
				if (num % 2f > 1f)
				{
					num = 1f - num % 1f;
				}
				else
				{
					num %= 1f;
				}
				float length = this.hitObject.SpatialLength * num;
				return this.positionAtLength(length);
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00023B8C File Offset: 0x00021D8C
		private Vector2 positionAtLength(float length)
		{
			if (this.sliderCurveSmoothLines.Count == 0 || this.cumulativeLengths.Count == 0)
			{
				return this.hitObject.Position;
			}
			if (length == 0f)
			{
				return this.sliderCurveSmoothLines[0].p1;
			}
			double num = this.cumulativeLengths[this.cumulativeLengths.Count - 1];
			if ((double)length >= num)
			{
				return this.sliderCurveSmoothLines[this.sliderCurveSmoothLines.Count - 1].p2;
			}
			int num2 = this.cumulativeLengths.BinarySearch((double)length);
			if (num2 < 0)
			{
				num2 = Math.Min(~num2, this.cumulativeLengths.Count - 1);
			}
			double num3 = this.cumulativeLengths[num2];
			double num4 = (num2 == 0) ? 0.0 : this.cumulativeLengths[num2 - 1];
			Vector2 vector = this.sliderCurveSmoothLines[num2].p1;
			if (num3 != num4)
			{
				vector += (this.sliderCurveSmoothLines[num2].p2 - this.sliderCurveSmoothLines[num2].p1) * (float)(((double)length - num4) / (num3 - num4));
			}
			return vector;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00023CBD File Offset: 0x00021EBD
		public virtual float getX(int i)
		{
			if (i != 0)
			{
				return this.sliderXY[i - 1].X;
			}
			return this.x;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00023CDC File Offset: 0x00021EDC
		public virtual float getY(int i)
		{
			if (i != 0)
			{
				return this.sliderXY[i - 1].Y;
			}
			return this.y;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00023CFC File Offset: 0x00021EFC
		internal static List<Vector2> CreateBezierBernstein(List<Vector2> controlPoints)
		{
			List<Vector2> list = new List<Vector2>();
			int num = Curve.CURVE_POINTS_SEPERATION * controlPoints.Count;
			double num2 = 1.0 / (double)(num - 1);
			for (int i = 0; i < num; i++)
			{
				double t = num2 * (double)i;
				double num3 = 0.0;
				double num4 = 0.0;
				for (int j = 0; j < controlPoints.Count; j++)
				{
					double num5 = Curve.Bernstein(controlPoints.Count - 1, j, t);
					num3 += num5 * (double)controlPoints[j].X;
					num4 += num5 * (double)controlPoints[j].Y;
				}
				list.Add(new Vector2((float)num3, (float)num4));
			}
			return list;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00023DC0 File Offset: 0x00021FC0
		private static double Bernstein(int n, int i, double t)
		{
			double num;
			if (t == 0.0 && i == 0)
			{
				num = 1.0;
			}
			else
			{
				num = Math.Pow(t, (double)i);
			}
			double num2;
			if (n == i && t == 1.0)
			{
				num2 = 1.0;
			}
			else
			{
				num2 = Math.Pow(1.0 - t, (double)(n - i));
			}
			return Curve.Ni(n, i) * num * num2;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00023E30 File Offset: 0x00022030
		private static double Ni(int n, int k)
		{
			double num = Curve.factorial(n);
			double num2 = Curve.factorial(k);
			double num3 = Curve.factorial(n - k);
			return num / (num2 * num3);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00023E57 File Offset: 0x00022057
		private static double factorial(int n)
		{
			return Curve.FactorialLookup[n];
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00023E60 File Offset: 0x00022060
		internal static List<Vector2> CreateBezier(List<Vector2> input)
		{
			return new Curve.BezierApproximator(input).CreateBezier();
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00023E6D File Offset: 0x0002206D
		internal static bool IsStraightLine(Vector2 a, Vector2 b, Vector2 c)
		{
			return (b.X - a.X) * (c.Y - a.Y) - (c.X - a.X) * (b.Y - a.Y) == 0f;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00023EB0 File Offset: 0x000220B0
		internal static void CircleThroughPoints(Vector2 A, Vector2 B, Vector2 C, out Vector2 centre, out float radius, out double t_initial, out double t_final)
		{
			float num = 2f * (A.X * (B.Y - C.Y) + B.X * (C.Y - A.Y) + C.X * (A.Y - B.Y));
			float num2 = A.LengthSquared();
			float num3 = B.LengthSquared();
			float num4 = C.LengthSquared();
			centre = new Vector2((num2 * (B.Y - C.Y) + num3 * (C.Y - A.Y) + num4 * (A.Y - B.Y)) / num, (num2 * (C.X - B.X) + num3 * (A.X - C.X) + num4 * (B.X - A.X)) / num);
			radius = Vector2.Distance(centre, A);
			t_initial = Curve.CircleTAt(A, centre);
			double num5 = Curve.CircleTAt(B, centre);
			t_final = Curve.CircleTAt(C, centre);
			while (num5 < t_initial)
			{
				num5 += 6.2831854820251465;
			}
			while (t_final < t_initial)
			{
				t_final += 6.2831854820251465;
			}
			if (num5 > t_final)
			{
				t_final -= 6.2831854820251465;
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0002400A File Offset: 0x0002220A
		internal static double CircleTAt(Vector2 pt, Vector2 centre)
		{
			return Math.Atan2((double)(pt.Y - centre.Y), (double)(pt.X - centre.X));
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0002402D File Offset: 0x0002222D
		internal static Vector2 CirclePoint(Vector2 centre, float radius, double t)
		{
			return new Vector2((float)(Math.Cos(t) * (double)radius), (float)(Math.Sin(t) * (double)radius)) + centre;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00024050 File Offset: 0x00022250
		internal virtual void UpdateCalculations()
		{
			SliderType sliderType = this.hitObject.SliderType;
			if (sliderType > SliderType.Catmull)
			{
				if (sliderType != SliderType.Linear)
				{
					if (sliderType != SliderType.PSlider)
					{
						goto IL_575;
					}
					if (this.hitObject.sliderCurvePoints.Count >= 3)
					{
						if (this.hitObject.sliderCurvePoints.Count > 3)
						{
							goto IL_197;
						}
						Vector2 vector = this.hitObject.sliderCurvePoints[0];
						Vector2 b = this.hitObject.sliderCurvePoints[1];
						Vector2 vector2 = this.hitObject.sliderCurvePoints[2];
						if (!Curve.IsStraightLine(vector, b, vector2))
						{
							Vector2 centre;
							float num;
							double num2;
							double num3;
							Curve.CircleThroughPoints(vector, b, vector2, out centre, out num, out num2, out num3);
							this.curveLength = Math.Abs((num3 - num2) * (double)num);
							int num4 = (int)(this.curveLength * 0.125);
							Vector2 p = vector;
							for (int i = 1; i < num4; i++)
							{
								double num5 = (double)i / (double)num4;
								double t = num3 * num5 + num2 * (1.0 - num5);
								Vector2 vector3 = Curve.CirclePoint(centre, num, t);
								this.path.Add(new Line(p, vector3));
								p = vector3;
							}
							this.path.Add(new Line(p, vector2));
							goto IL_575;
						}
					}
				}
				for (int j = 1; j < this.hitObject.sliderCurvePoints.Count; j++)
				{
					Line line = new Line(this.hitObject.sliderCurvePoints[j - 1], this.hitObject.sliderCurvePoints[j]);
					int num6 = 1;
					for (int k = 0; k < num6; k++)
					{
						Line line2 = new Line(line.p1 + (line.p2 - line.p1) * ((float)k / (float)num6), line.p1 + (line.p2 - line.p1) * ((float)(k + 1) / (float)num6));
						line2.straight = true;
						this.path.Add(line2);
					}
					this.path[this.path.Count - 1].forceEnd = true;
				}
				goto IL_575;
			}
			if (sliderType != SliderType.Bezier)
			{
				if (sliderType != SliderType.Catmull)
				{
					goto IL_575;
				}
				for (int l = 0; l < this.hitObject.sliderCurvePoints.Count - 1; l++)
				{
					Vector2 vector4 = (l - 1 >= 0) ? this.hitObject.sliderCurvePoints[l - 1] : this.hitObject.sliderCurvePoints[l];
					Vector2 vector5 = this.hitObject.sliderCurvePoints[l];
					Vector2 vector6 = (l + 1 < this.hitObject.sliderCurvePoints.Count) ? this.hitObject.sliderCurvePoints[l + 1] : (vector5 + (vector5 - vector4));
					Vector2 value = (l + 2 < this.hitObject.sliderCurvePoints.Count) ? this.hitObject.sliderCurvePoints[l + 2] : (vector6 + (vector6 - vector5));
					for (int m = 0; m < Curve.CURVE_POINTS_SEPERATION; m++)
					{
						this.path.Add(new Line(Vector2.CatmullRom(vector4, vector5, vector6, value, (float)m / (float)Curve.CURVE_POINTS_SEPERATION), Vector2.CatmullRom(vector4, vector5, vector6, value, (float)(m + 1) / (float)Curve.CURVE_POINTS_SEPERATION)));
					}
					this.path[this.path.Count - 1].forceEnd = true;
				}
				goto IL_575;
			}
			IL_197:
			int num7 = 0;
			for (int n = 0; n < this.hitObject.sliderCurvePoints.Count; n++)
			{
				bool flag = n < this.hitObject.sliderCurvePoints.Count - 2 && this.hitObject.sliderCurvePoints[n] == this.hitObject.sliderCurvePoints[n + 1];
				if (flag || n == this.hitObject.sliderCurvePoints.Count - 1)
				{
					List<Vector2> range = this.hitObject.sliderCurvePoints.GetRange(num7, n - num7 + 1);
					if (range.Count == 2)
					{
						Line line3 = new Line(range[0], range[1]);
						int num8 = 1;
						for (int num9 = 0; num9 < num8; num9++)
						{
							Line line4 = new Line(line3.p1 + (line3.p2 - line3.p1) * ((float)num9 / (float)num8), line3.p1 + (line3.p2 - line3.p1) * ((float)(num9 + 1) / (float)num8));
							line4.straight = true;
							this.path.Add(line4);
						}
					}
					else
					{
						List<Vector2> list = Curve.CreateBezier(range);
						for (int num10 = 1; num10 < list.Count; num10++)
						{
							this.path.Add(new Line(list[num10 - 1], list[num10]));
						}
					}
					this.path[this.path.Count - 1].forceEnd = true;
					if (flag)
					{
						n++;
					}
					num7 = n;
				}
			}
			IL_575:
			double num11 = 0.0;
			int count = this.path.Count;
			SliderType sliderType2 = this.hitObject.SliderType;
			for (int num12 = 0; num12 < count; num12++)
			{
				num11 += (double)this.path[num12].rho;
			}
			this.curveLength = num11;
			if (count > 0)
			{
				this.sliderCurveSmoothLines = this.path;
				if (this.cumulativeLengths == null)
				{
					this.cumulativeLengths = new List<double>(count);
				}
				else
				{
					this.cumulativeLengths.Clear();
				}
				num11 = 0.0;
				foreach (Line line5 in this.path)
				{
					num11 += (double)line5.rho;
					this.cumulativeLengths.Add(num11);
				}
			}
		}

		// Token: 0x04000231 RID: 561
		protected internal static int CURVE_POINTS_SEPERATION = 50;

		// Token: 0x04000232 RID: 562
		internal double curveLength;

		// Token: 0x04000233 RID: 563
		internal List<Line> sliderCurveSmoothLines;

		// Token: 0x04000234 RID: 564
		internal List<double> cumulativeLengths;

		// Token: 0x04000235 RID: 565
		protected internal HitObject hitObject;

		// Token: 0x04000236 RID: 566
		protected internal float x;

		// Token: 0x04000237 RID: 567
		protected internal float y;

		// Token: 0x04000238 RID: 568
		protected internal List<Vector2> sliderXY = new List<Vector2>();

		// Token: 0x04000239 RID: 569
		protected internal List<Vector2> curve = new List<Vector2>();

		// Token: 0x0400023A RID: 570
		private List<Line> path = new List<Line>();

		// Token: 0x0400023B RID: 571
		public const float Pi = 3.14159274f;

		// Token: 0x0400023C RID: 572
		private static readonly double[] FactorialLookup = new double[]
		{
			1.0,
			1.0,
			2.0,
			6.0,
			24.0,
			120.0,
			720.0,
			5040.0,
			40320.0,
			362880.0,
			3628800.0,
			39916800.0,
			479001600.0,
			6227020800.0,
			87178291200.0,
			1307674368000.0,
			20922789888000.0,
			355687428096000.0,
			6.402373705728E+15,
			1.21645100408832E+17,
			2.43290200817664E+18,
			5.109094217170944E+19,
			1.1240007277776077E+21,
			2.5852016738884978E+22,
			6.2044840173323941E+23,
			1.5511210043330986E+25,
			4.0329146112660565E+26,
			1.0888869450418352E+28,
			3.0488834461171387E+29,
			8.8417619937397019E+30,
			2.6525285981219107E+32,
			8.2228386541779224E+33,
			2.6313083693369352E+35
		};

		// Token: 0x02000092 RID: 146
		private class BezierApproximator
		{
			// Token: 0x0600038B RID: 907 RVA: 0x000277EC File Offset: 0x000259EC
			public BezierApproximator(List<Vector2> controlPoints)
			{
				this.controlPoints = controlPoints;
				this.count = controlPoints.Count;
				this.subdivisionBuffer1 = new Vector2[this.count];
				this.subdivisionBuffer2 = new Vector2[this.count * 2 - 1];
			}

			// Token: 0x0600038C RID: 908 RVA: 0x00027838 File Offset: 0x00025A38
			private static bool IsFlatEnough(Vector2[] controlPoints)
			{
				for (int i = 1; i < controlPoints.Length - 1; i++)
				{
					if ((controlPoints[i - 1] - 2f * controlPoints[i] + controlPoints[i + 1]).LengthSquared() > 0.25f)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600038D RID: 909 RVA: 0x00027894 File Offset: 0x00025A94
			private void Subdivide(Vector2[] controlPoints, Vector2[] l, Vector2[] r)
			{
				Vector2[] array = this.subdivisionBuffer1;
				for (int i = 0; i < this.count; i++)
				{
					array[i] = controlPoints[i];
				}
				for (int j = 0; j < this.count; j++)
				{
					l[j] = array[0];
					r[this.count - j - 1] = array[this.count - j - 1];
					for (int k = 0; k < this.count - j - 1; k++)
					{
						array[k] = (array[k] + array[k + 1]) / 2f;
					}
				}
			}

			// Token: 0x0600038E RID: 910 RVA: 0x00027944 File Offset: 0x00025B44
			private void Approximate(Vector2[] controlPoints, List<Vector2> output)
			{
				Vector2[] array = this.subdivisionBuffer2;
				Vector2[] array2 = this.subdivisionBuffer1;
				this.Subdivide(controlPoints, array, array2);
				for (int i = 0; i < this.count - 1; i++)
				{
					array[this.count + i] = array2[i + 1];
				}
				output.Add(controlPoints[0]);
				for (int j = 1; j < this.count - 1; j++)
				{
					int num = 2 * j;
					Vector2 item = 0.25f * (array[num - 1] + 2f * array[num] + array[num + 1]);
					output.Add(item);
				}
			}

			// Token: 0x0600038F RID: 911 RVA: 0x000279FC File Offset: 0x00025BFC
			public List<Vector2> CreateBezier()
			{
				List<Vector2> list = new List<Vector2>();
				if (this.count == 0)
				{
					return list;
				}
				Stack<Vector2[]> stack = new Stack<Vector2[]>();
				Stack<Vector2[]> stack2 = new Stack<Vector2[]>();
				stack.Push(this.controlPoints.ToArray());
				Vector2[] array = this.subdivisionBuffer2;
				while (stack.Count > 0)
				{
					Vector2[] array2 = stack.Pop();
					if (Curve.BezierApproximator.IsFlatEnough(array2))
					{
						this.Approximate(array2, list);
						stack2.Push(array2);
					}
					else
					{
						Vector2[] array3 = (stack2.Count > 0) ? stack2.Pop() : new Vector2[this.count];
						this.Subdivide(array2, array, array3);
						for (int i = 0; i < this.count; i++)
						{
							array2[i] = array[i];
						}
						stack.Push(array3);
						stack.Push(array2);
					}
				}
				list.Add(this.controlPoints[this.count - 1]);
				return list;
			}

			// Token: 0x04000314 RID: 788
			private int count;

			// Token: 0x04000315 RID: 789
			private List<Vector2> controlPoints;

			// Token: 0x04000316 RID: 790
			private Vector2[] subdivisionBuffer1;

			// Token: 0x04000317 RID: 791
			private Vector2[] subdivisionBuffer2;

			// Token: 0x04000318 RID: 792
			private const float TOLERANCE = 0.5f;

			// Token: 0x04000319 RID: 793
			private const float TOLERANCE_SQ = 0.25f;
		}
	}
}
