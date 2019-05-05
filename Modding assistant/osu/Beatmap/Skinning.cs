using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Modding_assistant.Utility;

namespace osu.Beatmap
{
	// Token: 0x0200003B RID: 59
	public static class Skinning
	{
		// Token: 0x0600027C RID: 636 RVA: 0x00022030 File Offset: 0x00020230
		public static void Reduce(List<Skinning.SkinSet> SkinList, List<string> ReduceList)
		{
			for (int i = 0; i < SkinList.Count; i++)
			{
				Skinning.CheckSimpleSet(SkinList[i], ReduceList);
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0002205C File Offset: 0x0002025C
		private static void CheckSimpleSet(Skinning.SkinSet Set, List<string> ReduceList)
		{
			Skinning.<>c__DisplayClass6_0 CS$<>8__locals1 = new Skinning.<>c__DisplayClass6_0();
			CS$<>8__locals1.Set = Set;
			int j;
			int num3;
			for (j = 0; j < CS$<>8__locals1.Set.Images.Count; j = num3 + 1)
			{
				if (!CS$<>8__locals1.Set.Images[j].isAnimated)
				{
					int num = ReduceList.FindIndex((string s) => s.Equals(CS$<>8__locals1.Set.Images[j].Name + ".png", StringComparison.CurrentCultureIgnoreCase));
					if (num != -1)
					{
						ReduceList.RemoveAt(num);
					}
				}
				else
				{
					int num2 = ReduceList.FindIndex((string s) => s.Equals(CS$<>8__locals1.Set.Images[j].Name + CS$<>8__locals1.Set.Images[j].AnimationAddon + "0.png", StringComparison.CurrentCultureIgnoreCase));
					if (num2 == -1)
					{
						num2 = ReduceList.FindIndex((string s) => s.Equals(CS$<>8__locals1.Set.Images[j].Name + ".png", StringComparison.CurrentCultureIgnoreCase));
						if (num2 != -1)
						{
							ReduceList.RemoveAt(num2);
						}
					}
					else
					{
						ReduceList.RemoveAt(num2);
						if (CS$<>8__locals1.Set.Images[j].MaximumFrames == 0)
						{
							int imageIndex = 1;
							while (num2 != -1)
							{
								num2 = ReduceList.FindIndex((string s) => s.Equals(CS$<>8__locals1.Set.Images[j].Name + CS$<>8__locals1.Set.Images[j].AnimationAddon + Convert.ToString(imageIndex) + ".png", StringComparison.CurrentCultureIgnoreCase));
								if (num2 != -1)
								{
									ReduceList.RemoveAt(num2);
								}
								num3 = imageIndex;
								imageIndex = num3 + 1;
							}
						}
						else
						{
							int imageIndex;
							for (imageIndex = 1; imageIndex < CS$<>8__locals1.Set.Images[j].MaximumFrames; imageIndex = num3 + 1)
							{
								num2 = ReduceList.FindIndex((string s) => s.Equals(CS$<>8__locals1.Set.Images[j].Name + CS$<>8__locals1.Set.Images[j].AnimationAddon + Convert.ToString(imageIndex) + ".png", StringComparison.CurrentCultureIgnoreCase));
								if (num2 == -1)
								{
									break;
								}
								ReduceList.RemoveAt(num2);
								num3 = imageIndex;
							}
						}
					}
				}
				num3 = j;
			}
			int i;
			for (i = 0; i < CS$<>8__locals1.Set.Sounds.Count; i = num3 + 1)
			{
				int num4 = ReduceList.FindIndex((string s) => s.Equals(CS$<>8__locals1.Set.Sounds[i].Name + ".wav", StringComparison.CurrentCultureIgnoreCase));
				if (num4 == -1)
				{
					num4 = ReduceList.FindIndex((string s) => s.Equals(CS$<>8__locals1.Set.Sounds[i].Name + ".mp3", StringComparison.CurrentCultureIgnoreCase));
					if (num4 == -1)
					{
						num4 = ReduceList.FindIndex((string s) => s.Equals(CS$<>8__locals1.Set.Sounds[i].Name + ".ogg", StringComparison.CurrentCultureIgnoreCase));
						if (num4 != -1)
						{
							ReduceList.RemoveAt(num4);
						}
					}
					else
					{
						ReduceList.RemoveAt(num4);
					}
				}
				else
				{
					ReduceList.RemoveAt(num4);
				}
				num3 = i;
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00022300 File Offset: 0x00020500
		public static List<Skinning.SkinSet> Init()
		{
			List<Skinning.SkinSet> list = new List<Skinning.SkinSet>();
			string text = Assembly.GetExecutingAssembly().Location;
			text = Path.Combine(Path.GetDirectoryName(text), Path.GetFileNameWithoutExtension(text)) + " Skins";
			text = Path.ChangeExtension(text, "ini");
			IniReader iniReader = new IniReader(text, '=');
			string[] keys = iniReader.GetKeys("SkinSets");
			for (int i = 0; i < keys.Length; i++)
			{
				Skinning.SkinSet skinSet = new Skinning.SkinSet();
				skinSet.Name = keys[i];
				string[] keys2 = iniReader.GetKeys(skinSet.Name);
				for (int j = 0; j < keys2.Length; j++)
				{
					string[] array = keys2[j].Split(new char[]
					{
						'|'
					});
					if (array.Length >= 2 && !string.IsNullOrEmpty(array[0]))
					{
						char c = array[0][0];
						if (c <= 'S')
						{
							if (c == 'A')
							{
								goto IL_EF;
							}
							if (c != 'I')
							{
								if (c != 'S')
								{
									goto IL_118;
								}
								goto IL_10D;
							}
						}
						else
						{
							if (c == 'a')
							{
								goto IL_EF;
							}
							if (c != 'i')
							{
								if (c != 's')
								{
									goto IL_118;
								}
								goto IL_10D;
							}
						}
						skinSet.AddImage(array[1]);
						goto IL_118;
						IL_EF:
						try
						{
							skinSet.AddImage(array[1], array[3], Convert.ToInt32(array[2]));
							goto IL_118;
						}
						catch
						{
							goto IL_118;
						}
						IL_10D:
						skinSet.AddSound(array[1]);
					}
					IL_118:;
				}
				list.Add(skinSet);
			}
			return list;
		}

		// Token: 0x02000084 RID: 132
		public class Image
		{
			// Token: 0x06000363 RID: 867 RVA: 0x0002735A File Offset: 0x0002555A
			public Image()
			{
			}

			// Token: 0x06000364 RID: 868 RVA: 0x00027378 File Offset: 0x00025578
			public Image(string name, string animationStr, bool animated, int maxFrames)
			{
				this.Name = name;
				this.AnimationAddon = animationStr;
				this.isAnimated = animated;
				this.MaximumFrames = maxFrames;
			}

			// Token: 0x040002F2 RID: 754
			public string Name = string.Empty;

			// Token: 0x040002F3 RID: 755
			public string AnimationAddon = string.Empty;

			// Token: 0x040002F4 RID: 756
			public bool isAnimated;

			// Token: 0x040002F5 RID: 757
			public int MaximumFrames;
		}

		// Token: 0x02000085 RID: 133
		public class Sound
		{
			// Token: 0x06000365 RID: 869 RVA: 0x000273B3 File Offset: 0x000255B3
			public Sound()
			{
			}

			// Token: 0x06000366 RID: 870 RVA: 0x000273C6 File Offset: 0x000255C6
			public Sound(string name)
			{
				this.Name = name;
			}

			// Token: 0x040002F6 RID: 758
			public string Name = string.Empty;
		}

		// Token: 0x02000086 RID: 134
		public class Special
		{
			// Token: 0x06000367 RID: 871 RVA: 0x00007843 File Offset: 0x00005A43
			public Special()
			{
			}

			// Token: 0x06000368 RID: 872 RVA: 0x000273E0 File Offset: 0x000255E0
			public Special(Skinning.SkinSpecialItemType type)
			{
				this.Type = type;
			}

			// Token: 0x040002F7 RID: 759
			public Skinning.SkinSpecialItemType Type;
		}

		// Token: 0x02000087 RID: 135
		public enum SkinSpecialItemType
		{
			// Token: 0x040002F9 RID: 761
			SliderBorder = 1
		}

		// Token: 0x02000088 RID: 136
		public class SkinSet
		{
			// Token: 0x06000369 RID: 873 RVA: 0x000273EF File Offset: 0x000255EF
			public void AddImage(string Name)
			{
				this.Images.Add(new Skinning.Image(Name, string.Empty, false, 0));
			}

			// Token: 0x0600036A RID: 874 RVA: 0x00027409 File Offset: 0x00025609
			public void AddImage(string Name, string NameExt, int MaxFrames)
			{
				this.Images.Add(new Skinning.Image(Name, NameExt, true, MaxFrames));
			}

			// Token: 0x0600036B RID: 875 RVA: 0x0002741F File Offset: 0x0002561F
			public void AddSound(string Name)
			{
				this.Sounds.Add(new Skinning.Sound(Name));
			}

			// Token: 0x0600036C RID: 876 RVA: 0x00027434 File Offset: 0x00025634
			public bool InSet(string File)
			{
				string strB = Path.ChangeExtension(File, string.Empty);
				for (int i = 0; i < this.Images.Count; i++)
				{
					if (!this.Images[i].isAnimated && string.Compare(this.Images[i].Name, strB, true) == 0)
					{
						return true;
					}
					if (this.Images[i].isAnimated)
					{
						if (string.Compare(this.Images[i].Name, strB, true) == 0)
						{
							return true;
						}
						if (string.Compare(this.Images[i].Name + this.Images[i].AnimationAddon, strB, true) == 0)
						{
							return true;
						}
					}
				}
				for (int j = 0; j < this.Sounds.Count; j++)
				{
					if (string.Compare(this.Sounds[j].Name, strB, true) == 0)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x040002FA RID: 762
			public string Name = string.Empty;

			// Token: 0x040002FB RID: 763
			public List<Skinning.Image> Images = new List<Skinning.Image>();

			// Token: 0x040002FC RID: 764
			public List<Skinning.Sound> Sounds = new List<Skinning.Sound>();

			// Token: 0x040002FD RID: 765
			public List<Skinning.Special> SpecialType = new List<Skinning.Special>();
		}
	}
}
