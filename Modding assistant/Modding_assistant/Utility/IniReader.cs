using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Modding_assistant.Utility
{
	// Token: 0x02000010 RID: 16
	public class IniReader
	{
		// Token: 0x06000079 RID: 121 RVA: 0x0000827C File Offset: 0x0000647C
		public IniReader(string file, char Separator)
		{
			this.filename = file;
			string text;
			try
			{
				text = File.ReadAllText(file, Encoding.UTF8);
			}
			catch
			{
				return;
			}
			this.separator = Separator;
			Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
			this.ini[""] = dictionary;
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
						dictionary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
						this.ini[text2.Substring(1, text2.LastIndexOf("]") - 1)] = dictionary;
					}
					else
					{
						int num = text2.IndexOf(this.separator);
						if (num == -1)
						{
							dictionary[text2] = "";
						}
						else
						{
							dictionary[text2.Substring(0, num).Trim()] = text2.Substring(num + 1).Trim();
						}
					}
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000841C File Offset: 0x0000661C
		public string GetValue(string key)
		{
			return this.GetValue(key, "", "");
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000842F File Offset: 0x0000662F
		public string GetValue(string key, string section)
		{
			return this.GetValue(key, section, "");
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000843E File Offset: 0x0000663E
		public string GetValue(string key, string section, string @default)
		{
			if (!this.ini.ContainsKey(section))
			{
				return @default;
			}
			if (!this.ini[section].ContainsKey(key))
			{
				return @default;
			}
			return this.ini[section][key];
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00008478 File Offset: 0x00006678
		public string[] GetKeys(string section)
		{
			if (!this.ini.ContainsKey(section))
			{
				return new string[0];
			}
			return this.ini[section].Keys.ToArray<string>();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000084A5 File Offset: 0x000066A5
		public string[] GetSections()
		{
			return (from t in this.ini.Keys
			where t != ""
			select t).ToArray<string>();
		}

		// Token: 0x0400005F RID: 95
		private Dictionary<string, Dictionary<string, string>> ini = new Dictionary<string, Dictionary<string, string>>(StringComparer.InvariantCultureIgnoreCase);

		// Token: 0x04000060 RID: 96
		private char separator;

		// Token: 0x04000061 RID: 97
		private string filename;
	}
}
