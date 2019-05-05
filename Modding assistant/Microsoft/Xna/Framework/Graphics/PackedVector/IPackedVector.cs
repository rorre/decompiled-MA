using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x02000023 RID: 35
	public interface IPackedVector
	{
		// Token: 0x06000242 RID: 578
		Vector4 ToVector4();

		// Token: 0x06000243 RID: 579
		void PackFromVector4(Vector4 vector);
	}
}
