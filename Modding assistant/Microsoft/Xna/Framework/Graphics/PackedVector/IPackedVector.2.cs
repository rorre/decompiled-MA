using System;

namespace Microsoft.Xna.Framework.Graphics.PackedVector
{
	// Token: 0x02000024 RID: 36
	public interface IPackedVector<TPacked> : IPackedVector
	{
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000244 RID: 580
		// (set) Token: 0x06000245 RID: 581
		TPacked PackedValue { get; set; }
	}
}
