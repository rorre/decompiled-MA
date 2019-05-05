using System;
using System.Globalization;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	public struct Rectangle : IEquatable<Rectangle>
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00018B09 File Offset: 0x00016D09
		public int Left
		{
			get
			{
				return this.X;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00018B11 File Offset: 0x00016D11
		public int Right
		{
			get
			{
				return this.X + this.Width;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00018B20 File Offset: 0x00016D20
		public int Top
		{
			get
			{
				return this.Y;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00018B28 File Offset: 0x00016D28
		public int Bottom
		{
			get
			{
				return this.Y + this.Height;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00018B37 File Offset: 0x00016D37
		public static Rectangle Empty
		{
			get
			{
				return Rectangle._empty;
			}
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00018B3E File Offset: 0x00016D3E
		public Rectangle(int x, int y, int width, int height)
		{
			this.X = x;
			this.Y = y;
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00018B5D File Offset: 0x00016D5D
		public void Offset(Point offset)
		{
			this.X += offset.X;
			this.Y += offset.Y;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00018B85 File Offset: 0x00016D85
		public void Offset(int offsetX, int offsetY)
		{
			this.X += offsetX;
			this.Y += offsetY;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00018BA3 File Offset: 0x00016DA3
		public void Inflate(int horizontalAmount, int verticalAmount)
		{
			this.X -= horizontalAmount;
			this.Y -= verticalAmount;
			this.Width += horizontalAmount * 2;
			this.Height += verticalAmount * 2;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00018BE1 File Offset: 0x00016DE1
		public bool Contains(int x, int y)
		{
			return this.X <= x && x < this.X + this.Width && this.Y <= y && y < this.Y + this.Height;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00018C18 File Offset: 0x00016E18
		public bool Contains(Point value)
		{
			return this.X <= value.X && value.X < this.X + this.Width && this.Y <= value.Y && value.Y < this.Y + this.Height;
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00018C70 File Offset: 0x00016E70
		public void Contains(ref Point value, out bool result)
		{
			result = (this.X <= value.X && value.X < this.X + this.Width && this.Y <= value.Y && value.Y < this.Y + this.Height);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00018CC8 File Offset: 0x00016EC8
		public bool Contains(Rectangle value)
		{
			return this.X <= value.X && value.X + value.Width <= this.X + this.Width && this.Y <= value.Y && value.Y + value.Height <= this.Y + this.Height;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00018D30 File Offset: 0x00016F30
		public void Contains(ref Rectangle value, out bool result)
		{
			result = (this.X <= value.X && value.X + value.Width <= this.X + this.Width && this.Y <= value.Y && value.Y + value.Height <= this.Y + this.Height);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x00018D9C File Offset: 0x00016F9C
		public bool Intersects(Rectangle value)
		{
			return value.X < this.X + this.Width && this.X < value.X + value.Width && value.Y < this.Y + this.Height && this.Y < value.Y + value.Height;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00018E00 File Offset: 0x00017000
		public void Intersects(ref Rectangle value, out bool result)
		{
			result = (value.X < this.X + this.Width && this.X < value.X + value.Width && value.Y < this.Y + this.Height && this.Y < value.Y + value.Height);
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00018E66 File Offset: 0x00017066
		public bool Equals(Rectangle other)
		{
			return this.X == other.X && this.Y == other.Y && this.Width == other.Width && this.Height == other.Height;
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00018EA4 File Offset: 0x000170A4
		public override bool Equals(object obj)
		{
			bool result = false;
			if (obj is Rectangle)
			{
				result = this.Equals((Rectangle)obj);
			}
			return result;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00018ECC File Offset: 0x000170CC
		public override string ToString()
		{
			CultureInfo currentCulture = CultureInfo.CurrentCulture;
			return string.Format(currentCulture, "{{X:{0} Y:{1} Width:{2} Height:{3}}}", new object[]
			{
				this.X.ToString(currentCulture),
				this.Y.ToString(currentCulture),
				this.Width.ToString(currentCulture),
				this.Height.ToString(currentCulture)
			});
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00018F2C File Offset: 0x0001712C
		public override int GetHashCode()
		{
			return this.X.GetHashCode() + this.Y.GetHashCode() + this.Width.GetHashCode() + this.Height.GetHashCode();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00018E66 File Offset: 0x00017066
		public static bool operator ==(Rectangle a, Rectangle b)
		{
			return a.X == b.X && a.Y == b.Y && a.Width == b.Width && a.Height == b.Height;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00018F5D File Offset: 0x0001715D
		public static bool operator !=(Rectangle a, Rectangle b)
		{
			return a.X != b.X || a.Y != b.Y || a.Width != b.Width || a.Height != b.Height;
		}

		// Token: 0x0400015A RID: 346
		public int X;

		// Token: 0x0400015B RID: 347
		public int Y;

		// Token: 0x0400015C RID: 348
		public int Width;

		// Token: 0x0400015D RID: 349
		public int Height;

		// Token: 0x0400015E RID: 350
		private static Rectangle _empty;
	}
}
