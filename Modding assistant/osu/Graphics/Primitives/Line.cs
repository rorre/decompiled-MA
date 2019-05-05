using System;
using Microsoft.Xna.Framework;

namespace osu.Graphics.Primitives
{
	// Token: 0x02000050 RID: 80
	internal class Line : ICloneable
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00026B94 File Offset: 0x00024D94
		internal float rho
		{
			get
			{
				return (this.p2 - this.p1).Length();
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00026BBA File Offset: 0x00024DBA
		internal float theta
		{
			get
			{
				return (float)Math.Atan2((double)(this.p2.Y - this.p1.Y), (double)(this.p2.X - this.p1.X));
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x00026BF2 File Offset: 0x00024DF2
		internal Line(Vector2 p1, Vector2 p2)
		{
			this.p1 = p1;
			this.p2 = p2;
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x00026C08 File Offset: 0x00024E08
		internal float DistanceSquaredToPoint(Vector2 p)
		{
			return Vector2.DistanceSquared(p, this.ClosestPointTo(p));
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00026C17 File Offset: 0x00024E17
		internal float DistanceToPoint(Vector2 p)
		{
			return Vector2.Distance(p, this.ClosestPointTo(p));
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00026C28 File Offset: 0x00024E28
		internal Vector2 ClosestPointTo(Vector2 p)
		{
			Vector2 vector = this.p2 - this.p1;
			float num = Vector2.Dot(p - this.p1, vector);
			if (num <= 0f)
			{
				return this.p1;
			}
			float num2 = Vector2.Dot(vector, vector);
			if (num2 <= num)
			{
				return this.p2;
			}
			float scaleFactor = num / num2;
			return this.p1 + scaleFactor * vector;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x00026C94 File Offset: 0x00024E94
		internal Matrix WorldMatrix()
		{
			Matrix matrix = Matrix.CreateRotationZ(this.theta);
			Matrix matrix2 = Matrix.CreateTranslation(this.p1.X, this.p1.Y, 0f);
			return matrix * matrix2;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x00026CD3 File Offset: 0x00024ED3
		internal Matrix EndWorldMatrix()
		{
			return Matrix.CreateRotationZ(this.theta) * Matrix.CreateTranslation(this.p2.X, this.p2.Y, 0f);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x00026D05 File Offset: 0x00024F05
		public object Clone()
		{
			return base.MemberwiseClone();
		}

		// Token: 0x0400027A RID: 634
		internal Vector2 p1;

		// Token: 0x0400027B RID: 635
		internal Vector2 p2;

		// Token: 0x0400027C RID: 636
		internal bool forceEnd;

		// Token: 0x0400027D RID: 637
		internal bool straight;
	}
}
