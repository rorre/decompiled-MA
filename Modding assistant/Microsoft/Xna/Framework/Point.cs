using System;
using System.Globalization;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200001E RID: 30
	[Serializable]
	public struct Point : IEquatable<Point>
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00018A20 File Offset: 0x00016C20
		public static Point Zero
		{
			get
			{
				return Point._zero;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00018A27 File Offset: 0x00016C27
		public Point(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00018A37 File Offset: 0x00016C37
		public bool Equals(Point other)
		{
			return this.X == other.X && this.Y == other.Y;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00018A58 File Offset: 0x00016C58
		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj is Point)
			{
				result = this.Equals((Point)obj);
			}
			return result;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00018A7D File Offset: 0x00016C7D
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00018A98 File Offset: 0x00016C98
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{X:{0} Y:{1}}}", new object[]
			{
				this.X.ToString(currentCulture),
				this.Y.ToString(currentCulture)
			});
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00018ADA File Offset: 0x00016CDA
		public static bool operator ==(Point a, Point b)
		{
			return a.Equals(b);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00018AE4 File Offset: 0x00016CE4
		public static bool operator !=(Point a, Point b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		// Token: 0x04000157 RID: 343
		public int X;

		// Token: 0x04000158 RID: 344
		public int Y;

		// Token: 0x04000159 RID: 345
		private static Point _zero;
	}
}
