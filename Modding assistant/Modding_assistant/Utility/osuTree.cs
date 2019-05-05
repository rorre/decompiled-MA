using System;
using System.Collections.Generic;

namespace Modding_assistant.Utility
{
	// Token: 0x02000011 RID: 17
	internal class osuTree
	{
		// Token: 0x04000062 RID: 98
		public List<osuTree.treeNode> Items = new List<osuTree.treeNode>();

		// Token: 0x02000060 RID: 96
		public class treeNode
		{
			// Token: 0x040002AA RID: 682
			public string SectionName = string.Empty;

			// Token: 0x040002AB RID: 683
			public List<osuTree.treeItem> Keys = new List<osuTree.treeItem>();
		}

		// Token: 0x02000061 RID: 97
		public class treeItem
		{
			// Token: 0x06000319 RID: 793 RVA: 0x00026F5F File Offset: 0x0002515F
			public treeItem(string _Key, string _Value)
			{
				this.Name = _Key;
				this.Value = _Value;
			}

			// Token: 0x040002AC RID: 684
			public string Name = string.Empty;

			// Token: 0x040002AD RID: 685
			public string Value = string.Empty;
		}
	}
}
