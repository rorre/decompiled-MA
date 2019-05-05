using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Modding_assistant.Utility
{
	// Token: 0x0200000F RID: 15
	public class osuReader
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00007F48 File Offset: 0x00006148
		public osuReader(string file)
		{
			string empty = string.Empty;
			string text;
			try
			{
				text = File.ReadAllText(file, Encoding.UTF8);
			}
			catch
			{
				return;
			}
			osuTree.treeNode treeNode = new osuTree.treeNode();
			this.osuT.Items.Add(treeNode);
			foreach (string text2 in from t in text.Split(new string[]
			{
				"\n"
			}, StringSplitOptions.RemoveEmptyEntries)
			where !string.IsNullOrWhiteSpace(t)
			select t.Trim())
			{
				if (!text2.StartsWith("//"))
				{
					if (text2.StartsWith("[") && text2.EndsWith("]"))
					{
						treeNode = new osuTree.treeNode();
						treeNode.SectionName = text2.Substring(1, text2.LastIndexOf("]") - 1);
						this.osuT.Items.Add(treeNode);
					}
					else
					{
						int num = text2.IndexOf(":");
						if (num == -1)
						{
							treeNode.Keys.Add(new osuTree.treeItem(text2.Trim(), string.Empty));
						}
						else
						{
							treeNode.Keys.Add(new osuTree.treeItem(text2.Substring(0, num).Trim(), text2.Substring(num + 1).Trim()));
						}
					}
				}
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000080F4 File Offset: 0x000062F4
		public string GetValue(string key, string section, string @default)
		{
			for (int i = 0; i < this.osuT.Items.Count; i++)
			{
				if (string.Compare(this.osuT.Items[i].SectionName, section, true) == 0)
				{
					for (int j = 0; j < this.osuT.Items[i].Keys.Count; j++)
					{
						if (string.Compare(this.osuT.Items[i].Keys[j].Name, key, true) == 0)
						{
							return this.osuT.Items[i].Keys[j].Value;
						}
					}
				}
			}
			return @default;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000081B4 File Offset: 0x000063B4
		public string[] GetKeys(string section)
		{
			for (int i = 0; i < this.osuT.Items.Count; i++)
			{
				if (string.Compare(this.osuT.Items[i].SectionName, section, true) == 0)
				{
					List<string> list = new List<string>(this.osuT.Items[i].Keys.Count);
					for (int j = 0; j < this.osuT.Items[i].Keys.Count; j++)
					{
						list.Add(this.osuT.Items[i].Keys[j].Name);
					}
					return list.ToArray();
				}
			}
			return new string[0];
		}

		// Token: 0x0400005E RID: 94
		private osuTree osuT = new osuTree();
	}
}
