using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.Integration;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;
using Modding_assistant.maStrings;
using Modding_assistant.Properties;
using Modding_assistant.Report;
using Modding_assistant.Utility;
using Modding_assistant.windows;
using osu;
using osu.Beatmap;
using osu.Processor;

namespace Modding_assistant
{
	// Token: 0x02000005 RID: 5
	public partial class MainWindow : Window
	{
		// Token: 0x0600001A RID: 26 RVA: 0x000032E8 File Offset: 0x000014E8
		private void LoadUP()
		{
			this.wnd.Height = Settings.Default.Height;
			this.wnd.Width = Settings.Default.Width;
			if (string.IsNullOrEmpty(Settings.Default.BeatmapsDir))
			{
				this.textBox_btmDir.Text = this.beatmapsLocate();
			}
			else
			{
				this.textBox_btmDir.Text = Settings.Default.BeatmapsDir;
			}
			this.checkBox_sett_getFI.IsChecked = new bool?(Settings.Default.GetFullNameData);
			this.checkBox_sett_autoUP.IsChecked = new bool?(Settings.Default.CheckForUpdates);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000338C File Offset: 0x0000158C
		private void SaveUP()
		{
			if (base.WindowState == WindowState.Maximized)
			{
				Settings.Default.Height = this.wnd.RestoreBounds.Height;
				Settings.Default.Width = base.RestoreBounds.Width;
			}
			else
			{
				Settings.Default.Height = this.wnd.Height;
				Settings.Default.Width = this.wnd.Width;
			}
			Settings.Default.Save();
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003410 File Offset: 0x00001610
		private string beatmapsLocate()
		{
			string text = string.Empty;
			string text2 = string.Empty;
			try
			{
				text = Registry.GetValue("HKEY_CLASSES_ROOT\\osu\\shell\\open\\command", string.Empty, string.Empty).ToString();
				if (text == string.Empty)
				{
					text = Registry.GetValue("HKEY_CLASSES_ROOT\\osu!\\shell\\open\\command", string.Empty, string.Empty).ToString();
				}
				if (text != string.Empty)
				{
					text = text.Remove(0, 1);
					text = text.Split(new char[]
					{
						'"'
					})[0];
					text = Path.GetDirectoryName(text);
					StreamReader streamReader = File.OpenText(Path.Combine(text, "osu!." + Environment.UserName + ".cfg"));
					while (!streamReader.EndOfStream && !(text2 != string.Empty))
					{
						string text3 = streamReader.ReadLine();
						if (!text3.StartsWith("#"))
						{
							string[] separator = new string[]
							{
								" = "
							};
							string[] array = text3.Split(separator, StringSplitOptions.None);
							if (array[0] == "BeatmapDirectory")
							{
								text2 = array[1];
							}
						}
					}
					if (text2 == string.Empty)
					{
						text2 = Path.Combine(text, "Songs");
					}
				}
			}
			catch
			{
			}
			if (Path.IsPathRooted(text2))
			{
				return text2;
			}
			return Path.Combine(text, text2);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003564 File Offset: 0x00001764
		private bool InitFolder(string songsPath, bool showError)
		{
			this._g_dirList = new MainWindow.dirS();
			this.listBox_btm.Items.Clear();
			this.textBox_btmSelect.Clear();
			this.textBox_btmDir.Text = songsPath;
			Modding_assistant.windows.ProgressBar Bar = new Modding_assistant.windows.ProgressBar();
			Bar.Owner = this;
			Bar.bar.Value = 0.0;
			Bar.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			try
			{
				string[] directories = Directory.GetDirectories(this.textBox_btmDir.Text);
				Bar.bar.Maximum = (double)directories.Length;
				Bar.Show();
				base.IsEnabled = false;
				Thread.Sleep(300);
				Action <>9__0;
				foreach (string path in directories)
				{
					string name = new DirectoryInfo(path).Name;
					this._g_dirList.dirList.Add(name);
					if (Settings.Default.GetFullNameData)
					{
						string text;
						if (FastName.Detect(path, out text))
						{
							this.listBox_btm.Items.Add(text);
							this._g_dirList.showList.Add(text);
						}
						else
						{
							this.listBox_btm.Items.Add(name);
							this._g_dirList.showList.Add(name);
						}
					}
					else
					{
						this.listBox_btm.Items.Add(name);
						this._g_dirList.showList.Add(name);
					}
					Dispatcher dispatcher = base.Dispatcher;
					DispatcherPriority priority = DispatcherPriority.Render;
					Action method;
					if ((method = <>9__0) == null)
					{
						method = (<>9__0 = delegate()
						{
							Bar.bar.Value += 1.0;
							Bar.bar.UpdateLayout();
						});
					}
					dispatcher.Invoke(priority, method);
				}
				Bar.Close();
				base.IsEnabled = true;
			}
			catch (Exception ex)
			{
				base.IsEnabled = true;
				Bar.Close();
				if (showError)
				{
					System.Windows.MessageBox.Show(ap.ErrorSome + ex.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003788 File Offset: 0x00001988
		private void button_btmDirBrowse_Click(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK && this.InitFolder(folderBrowserDialog.SelectedPath, false))
			{
				Settings.Default.BeatmapsDir = this.textBox_btmDir.Text;
				Settings.Default.Save();
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000037D2 File Offset: 0x000019D2
		private void button_btmReload_Click(object sender, RoutedEventArgs e)
		{
			if (this.InitFolder(this.textBox_btmDir.Text, true))
			{
				Settings.Default.BeatmapsDir = this.textBox_btmDir.Text;
				Settings.Default.Save();
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003807 File Offset: 0x00001A07
		private void listBox_btm_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.listBox_btm.SelectedIndex > -1)
			{
				this.textBox_btmSelect.Text = this.listBox_btm.Items[this.listBox_btm.SelectedIndex].ToString();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003844 File Offset: 0x00001A44
		private void textBox_btmSelect_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!string.IsNullOrEmpty(this.textBox_btmSelect.Text.Trim()))
			{
				this.listBox_btm.Items.Clear();
				List<string> list = new List<string>(this.textBox_btmSelect.Text.Trim().Split(new char[]
				{
					' '
				}));
				if (list.Count < 1)
				{
					return;
				}
				bool flag = false;
				if (list.Count == 1)
				{
					flag = true;
				}
				using (List<string>.Enumerator enumerator = this._g_dirList.showList.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						if (text.IndexOf(list[0], StringComparison.CurrentCultureIgnoreCase) >= 0)
						{
							for (int i = 1; i < list.Count; i++)
							{
								if (text.IndexOf(list[i], StringComparison.CurrentCultureIgnoreCase) < 0)
								{
									flag = false;
									break;
								}
								flag = true;
							}
							if (flag)
							{
								this.listBox_btm.Items.Add(text);
							}
						}
					}
					return;
				}
			}
			if (this.textBox_btmSelect.Text.Trim() == string.Empty)
			{
				this.listBox_btm.Items.Clear();
				foreach (string newItem in this._g_dirList.showList)
				{
					this.listBox_btm.Items.Add(newItem);
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000039D4 File Offset: 0x00001BD4
		private void button_btmOpen_Click(object sender, RoutedEventArgs e)
		{
			this.tabitem_bi.IsEnabled = false;
			string text = string.Empty;
			int num = this._g_dirList.showList.IndexOf(this.textBox_btmSelect.Text);
			if (num != -1)
			{
				text = this._g_dirList.dirList[num];
			}
			else
			{
				text = this.textBox_btmSelect.Text;
			}
			string text2 = Path.Combine(ut_Path.CleanFileName(this.textBox_btmDir.Text), ut_Path.CleanFileName(text));
			if (text == string.Empty)
			{
				return;
			}
			if (!Directory.Exists(text2))
			{
				System.Windows.MessageBox.Show(ap.ErrorFind + text2, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			this.clean_BI_UI();
			this.ReportsList = new List<Report>();
			try
			{
				this.bi = new BeatmapInfo(text2);
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				return;
			}
			this.RunCheks();
			this.DisplayReport();
			this.BeatmapDirectoryName = this._g_dirList.dirList[num];
			this.textBlock_biInfo.Text = this._g_dirList.dirList[num];
			TextBlock textBlock = this.textBlock_biInfo;
			textBlock.Text = string.Concat(new object[]
			{
				textBlock.Text,
				"\n",
				ap.Diffs,
				this.bi.Difficluties.Count
			});
			textBlock = this.textBlock_biInfo;
			textBlock.Text = string.Concat(new object[]
			{
				textBlock.Text,
				"\n",
				ap.oszFS,
				this.bi.BeatmapSize,
				ap.mb
			});
			TextBlock textBlock2 = this.textBlock_biInfo;
			textBlock2.Text = textBlock2.Text + "\n" + ap.osbSB + this.BoolToYN(this.bi.isStoryBoard);
			this.LoadDifficultyList();
			this.listBox_diffs.SelectedIndex = 0;
			this.LoadSnapshots();
			this.TaikoScroll.Content = null;
			this.TaikoCurrentZoom = 10000;
			if (this.Taikoslider.Value != 0.0)
			{
				this.Taikoslider.Value = 0.0;
			}
			try
			{
				new TaikoDraw(this.bi, this.TaikoScroll, this.TaikoCurrentZoom, (int)this.Taikoslider.Value);
			}
			catch
			{
			}
			if (this.TaikoScroll.Content == null)
			{
				this.TaikoTab.IsEnabled = false;
			}
			else
			{
				this.TaikoTab.IsEnabled = true;
			}
			this.tabitem_bi.IsEnabled = true;
			this.tabControl_main.SelectedIndex = 1;
			this.tabControl_btmInfo.SelectedIndex = 0;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003CA8 File Offset: 0x00001EA8
		private void button_btmOpenOsu_Click(object sender, RoutedEventArgs e)
		{
			string nowPlaying = MSNListener.NowPlaying;
			if (nowPlaying == null)
			{
				System.Windows.MessageBox.Show(ap.ErrorMSN, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			string[] array = nowPlaying.Split(new string[]
			{
				"\\0"
			}, StringSplitOptions.None);
			string text = array[5] + " - " + array[4];
			foreach (string oldValue in new string[]
			{
				"\\",
				"/",
				":",
				"*",
				"?",
				"\"",
				"<",
				">",
				"|",
				"."
			})
			{
				text = text.Replace(oldValue, "");
			}
			this.textBox_btmSelect.Text = text;
			if (this.listBox_btm.Items.Count == 1)
			{
				this.textBox_btmSelect.Text = this.listBox_btm.Items[0].ToString();
				this.button_btmOpen.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003DD0 File Offset: 0x00001FD0
		private void RunCheks()
		{
			Modding_assistant.windows.ProgressBar progressBar = new Modding_assistant.windows.ProgressBar();
			progressBar.Owner = this;
			progressBar.bar.Value = 0.0;
			progressBar.WindowStartupLocation = WindowStartupLocation.CenterOwner;
			progressBar.bar.Maximum = (double)(15 + 18 * this.bi.Difficluties.Count + 1);
			progressBar.Show();
			base.IsEnabled = false;
			Thread.Sleep(300);
			try
			{
				this.ReportsList.Add(RunReport.DirectorySize(this.bi, 0));
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show("[DirectorySize] " + ex.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.Consistency(this.bi, 0));
			}
			catch (Exception ex2)
			{
				System.Windows.MessageBox.Show("[Consistency] " + ex2.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.ImageDimensions(this.bi, 0));
			}
			catch (Exception ex3)
			{
				System.Windows.MessageBox.Show("[ImageDimensions] " + ex3.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.DuplicateOsb(this.bi, 0));
			}
			catch (Exception ex4)
			{
				System.Windows.MessageBox.Show("[DuplicateOsb] " + ex4.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.UnusedHitsounds(this.bi, 0));
			}
			catch (Exception ex5)
			{
				System.Windows.MessageBox.Show("[UnusedHitsounds] " + ex5.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.UnusedFiles(this.bi, 0));
			}
			catch (Exception ex6)
			{
				System.Windows.MessageBox.Show("[UnusedFiles] " + ex6.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.NotWaveHS(this.bi, 0));
			}
			catch (Exception ex7)
			{
				System.Windows.MessageBox.Show("[NotWaveHS] " + ex7.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.VideoDimensions(this.bi, 0));
			}
			catch (Exception ex8)
			{
				System.Windows.MessageBox.Show("[VideoDimensions] " + ex8.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.VideoAudioStream(this.bi, 0));
			}
			catch (Exception ex9)
			{
				System.Windows.MessageBox.Show("[VideoAudioStream] " + ex9.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.StoryboardFiles(this.bi, 0));
			}
			catch (Exception ex10)
			{
				System.Windows.MessageBox.Show("[StoryboardFiles] " + ex10.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.Mp3Bitrate(this.bi, 0));
			}
			catch (Exception ex11)
			{
				System.Windows.MessageBox.Show("[Mp3Bitrate] " + ex11.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.HSLength(this.bi, 0));
			}
			catch (Exception ex12)
			{
				System.Windows.MessageBox.Show("[HSLength] " + ex12.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.HSDelay(this.bi, 0));
			}
			catch (Exception ex13)
			{
				System.Windows.MessageBox.Show("[HSDelay] " + ex13.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.EndTimeConsistency(this.bi, 0));
			}
			catch (Exception ex14)
			{
				System.Windows.MessageBox.Show("[EndTimeConsistency] " + ex14.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			try
			{
				this.ReportsList.Add(RunReport.StoryboardedHitsounds(this.bi, 0));
			}
			catch (Exception ex15)
			{
				System.Windows.MessageBox.Show("[StoryboardedHitsounds] " + ex15.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.IncBar(progressBar);
			for (int i = 0; i < this.bi.Difficluties.Count; i++)
			{
				try
				{
					this.ReportsList.Add(RunReport.SkinPreference(this.bi, i));
				}
				catch (Exception ex16)
				{
					System.Windows.MessageBox.Show("[SkinPreference] " + ex16.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.EpilepsyWarning(this.bi, i));
				}
				catch (Exception ex17)
				{
					System.Windows.MessageBox.Show("[EpilepsyWarning] " + ex17.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.BackgroundSet(this.bi, i));
				}
				catch (Exception ex18)
				{
					System.Windows.MessageBox.Show("[BackgroundSet] " + ex18.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.VideoInTaiko(this.bi, i));
				}
				catch (Exception ex19)
				{
					System.Windows.MessageBox.Show("[VideoInTaiko] " + ex19.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.ComboColorsSet(this.bi, i));
				}
				catch (Exception ex20)
				{
					System.Windows.MessageBox.Show("[ComboColorsSet] " + ex20.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.PreviewPointSet(this.bi, i));
				}
				catch (Exception ex21)
				{
					System.Windows.MessageBox.Show("[PreviewPointSet] " + ex21.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.ConcurentObjects(this.bi, i));
				}
				catch (Exception ex22)
				{
					System.Windows.MessageBox.Show("[ConcurentObjects] " + ex22.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.DuplicateTL(this.bi, i));
				}
				catch (Exception ex23)
				{
					System.Windows.MessageBox.Show("[DuplicateTL] " + ex23.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.MinimumDrainingTime(this.bi, i));
				}
				catch (Exception ex24)
				{
					System.Windows.MessageBox.Show("[MinimumDrainingTime] " + ex24.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.ObjectsSnapping(this.bi, i));
				}
				catch (Exception ex25)
				{
					System.Windows.MessageBox.Show("[ObjectsSnapping] " + ex25.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.TLSnapping(this.bi, i));
				}
				catch (Exception ex26)
				{
					System.Windows.MessageBox.Show("[TLSnapping] " + ex26.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.OffscreenObjects(this.bi, i));
				}
				catch (Exception ex27)
				{
					System.Windows.MessageBox.Show("[OffscreenObjects] " + ex27.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.SpinnerMinimumLength(this.bi, i));
				}
				catch (Exception ex28)
				{
					System.Windows.MessageBox.Show("[SpinnerMinimumLength] " + ex28.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.SpinnerPresence(this.bi, i));
				}
				catch (Exception ex29)
				{
					System.Windows.MessageBox.Show("[SpinnerPresence] " + ex29.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.SpinnerRecoveryTime(this.bi, i));
				}
				catch (Exception ex30)
				{
					System.Windows.MessageBox.Show("[SpinnerRecoveryTime] " + ex30.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.SpinnerMaxiumLength(this.bi, i));
				}
				catch (Exception ex31)
				{
					System.Windows.MessageBox.Show("[SpinnerMaxiumLength] " + ex31.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.CtBSpinnerRecoveryTime(this.bi, i));
				}
				catch (Exception ex32)
				{
					System.Windows.MessageBox.Show("[CtBSpinnerRecoveryTime] " + ex32.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
				try
				{
					this.ReportsList.Add(RunReport.CtBHyper(this.bi, i));
				}
				catch (Exception ex33)
				{
					System.Windows.MessageBox.Show("[CtBSpinnerRecoveryTime] " + ex33.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				this.IncBar(progressBar);
			}
			this.ClearReportsList();
			progressBar.Close();
			base.IsEnabled = true;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00004A44 File Offset: 0x00002C44
		private void IncBar(Modding_assistant.windows.ProgressBar Bar)
		{
			base.Dispatcher.Invoke(DispatcherPriority.Render, new Action(delegate()
			{
				Bar.bar.Value += 1.0;
				Bar.bar.UpdateLayout();
			}));
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00004A78 File Offset: 0x00002C78
		private void ClearReportsList()
		{
			int num = this.ReportsList.Count;
			for (int i = 0; i < num; i++)
			{
				if (this.ReportsList[i].Brief == null || this.ReportsList[i].Brief == string.Empty)
				{
					this.ReportsList.RemoveAt(i);
					num--;
					i--;
				}
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00004AE4 File Offset: 0x00002CE4
		private void DisplayReport()
		{
			this.richTextBox_warningsText.Document.Blocks.Clear();
			this.ti_Root = new List<TreeViewItem>();
			this.ti_Root.Add(new TreeViewItem());
			this.treeView_warnings.Items.Clear();
			this.ti_Root[0].Header = ap.Global;
			for (int i = 0; i < this.bi.Difficluties.Count; i++)
			{
				this.ti_Root.Add(new TreeViewItem());
				this.ti_Root[i + 1].Header = this.bi.Difficluties[i].Version;
			}
			for (int j = 0; j < this.ReportsList.Count; j++)
			{
				string empty = string.Empty;
				if (this.ReportsList[j].Level == ReportLevel.Global)
				{
					TreeViewItem treeViewItem = new TreeViewItem();
					treeViewItem.Header = this.ReportsList[j].Brief;
					this.ti_Root[0].Items.Add(treeViewItem);
				}
				else
				{
					TreeViewItem treeViewItem2 = new TreeViewItem();
					treeViewItem2.Header = this.ReportsList[j].Brief;
					this.ti_Root[this.ReportsList[j].DifficultyIndex + 1].Items.Add(treeViewItem2);
				}
			}
			for (int k = 0; k < this.ti_Root.Count; k++)
			{
				if (!this.ti_Root[k].HasItems)
				{
					this.ti_Root.RemoveAt(k);
					k--;
				}
			}
			for (int l = 0; l < this.ti_Root.Count; l++)
			{
				this.treeView_warnings.Items.Add(this.ti_Root[l]);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00004CC8 File Offset: 0x00002EC8
		private void LoadDifficultyList()
		{
			for (int i = 0; i < this.bi.Difficluties.Count; i++)
			{
				ListBoxItem listBoxItem = new ListBoxItem();
				string version = this.bi.Difficluties[i].Version;
				listBoxItem.Content = version;
				this.listBox_diffs.Items.Add(listBoxItem);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00004D28 File Offset: 0x00002F28
		private void clean_BI_UI()
		{
			this.textBlock_biInfo.Text = string.Empty;
			this.textBox_diffInfo.Text = string.Empty;
			this.listBox_diffs.Items.Clear();
			this.slider_snap_3.Value = 4.0;
			this.slider_snap_4.Value = 5.0;
			this.comboBox_snapshots.Items.Clear();
			this._g_SnapshotList = new SnapshotList();
			this.richTextBox_snapshots.Document.Blocks.Clear();
			this.comboBox_snapshots.SelectedIndex = -1;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00004DCC File Offset: 0x00002FCC
		private void LoadDifficultyInformation(int diffIndex)
		{
			TimeSpan timeSpan = TimeSpan.FromMilliseconds((double)this.bi.Difficluties[diffIndex].TotalDrainTime);
			string str = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
			double num = 0.0;
			if (this.bi.Difficluties[diffIndex].Objects.Count != 0)
			{
				num = (double)this.bi.Difficluties[diffIndex].TotalDrainTime / (double)this.bi.Difficluties[diffIndex].Objects.Count;
				num = 1000.0 / num;
				num = Convert.ToDouble(Math.Round(num, 2));
			}
			this.textBox_diffInfo.Text = this.bi.Difficluties[diffIndex].Version;
			System.Windows.Controls.TextBox textBox = this.textBox_diffInfo;
			textBox.Text = textBox.Text + "\n" + ap.Mode + this.bi.Difficluties[diffIndex].DifficlutyMode.ToString();
			System.Windows.Controls.TextBox textBox2 = this.textBox_diffInfo;
			textBox2.Text = textBox2.Text + "\n" + ap.Creator + this.bi.Difficluties[diffIndex].Creator;
			System.Windows.Controls.TextBox textBox3 = this.textBox_diffInfo;
			textBox3.Text = textBox3.Text + "\n\n[" + ap.Metadata + "]";
			System.Windows.Controls.TextBox textBox4 = this.textBox_diffInfo;
			textBox4.Text = textBox4.Text + "\n" + ap.Artist + this.bi.Difficluties[diffIndex].Artist;
			System.Windows.Controls.TextBox textBox5 = this.textBox_diffInfo;
			textBox5.Text = textBox5.Text + "\n" + ap.ArtistU + this.bi.Difficluties[diffIndex].ArtistUnicode;
			System.Windows.Controls.TextBox textBox6 = this.textBox_diffInfo;
			textBox6.Text = textBox6.Text + "\n" + ap.Title + this.bi.Difficluties[diffIndex].Title;
			System.Windows.Controls.TextBox textBox7 = this.textBox_diffInfo;
			textBox7.Text = textBox7.Text + "\n" + ap.TitleU + this.bi.Difficluties[diffIndex].TitleUnicode;
			System.Windows.Controls.TextBox textBox8 = this.textBox_diffInfo;
			textBox8.Text = textBox8.Text + "\n" + ap.Source + this.bi.Difficluties[diffIndex].Source;
			System.Windows.Controls.TextBox textBox9 = this.textBox_diffInfo;
			textBox9.Text = textBox9.Text + "\n" + ap.Tags + this.bi.Difficluties[diffIndex].Tags;
			System.Windows.Controls.TextBox textBox10 = this.textBox_diffInfo;
			textBox10.Text = textBox10.Text + "\n\n[" + ap.Settings + "]";
			System.Windows.Controls.TextBox textBox11 = this.textBox_diffInfo;
			textBox11.Text = textBox11.Text + "\nHP: " + this.bi.Difficluties[diffIndex].HPDrainRate;
			System.Windows.Controls.TextBox textBox12 = this.textBox_diffInfo;
			textBox12.Text = textBox12.Text + " CS: " + this.bi.Difficluties[diffIndex].CircleSize;
			System.Windows.Controls.TextBox textBox13 = this.textBox_diffInfo;
			textBox13.Text = textBox13.Text + " AR: " + this.bi.Difficluties[diffIndex].ApproachRate;
			System.Windows.Controls.TextBox textBox14 = this.textBox_diffInfo;
			textBox14.Text = textBox14.Text + " OD: " + this.bi.Difficluties[diffIndex].OverallDifficulty;
			System.Windows.Controls.TextBox textBox15 = this.textBox_diffInfo;
			textBox15.Text = textBox15.Text + " SV: " + this.bi.Difficluties[diffIndex].SliderMultiplier;
			System.Windows.Controls.TextBox textBox16 = this.textBox_diffInfo;
			textBox16.Text = textBox16.Text + " STR: " + this.bi.Difficluties[diffIndex].SliderTickRate;
			System.Windows.Controls.TextBox textBox17 = this.textBox_diffInfo;
			textBox17.Text = textBox17.Text + "\n\n[" + ap.Other + "]";
			System.Windows.Controls.TextBox textBox18 = this.textBox_diffInfo;
			textBox18.Text = string.Concat(new object[]
			{
				textBox18.Text,
				"\n",
				ap.ObjectsC,
				this.bi.Difficluties[diffIndex].Objects.Count
			});
			System.Windows.Controls.TextBox textBox19 = this.textBox_diffInfo;
			textBox19.Text = textBox19.Text + "\n" + ap.TDT + str;
			textBox18 = this.textBox_diffInfo;
			textBox18.Text = string.Concat(new object[]
			{
				textBox18.Text,
				"\n",
				ap.ARD,
				num
			});
			if (this.bi.Difficluties[diffIndex].DifficlutyMode == DifficlutyModeType.Standard)
			{
				System.Windows.Controls.TextBox textBox20 = this.textBox_diffInfo;
				textBox20.Text = textBox20.Text + "\n\n[" + ap.SR + "]";
				textBox18 = this.textBox_diffInfo;
				textBox18.Text = string.Concat(new object[]
				{
					textBox18.Text,
					"\n",
					ap.SR_total,
					this.bi.Difficluties[diffIndex].strains.TotalSR,
					" "
				});
				textBox18 = this.textBox_diffInfo;
				textBox18.Text = string.Concat(new object[]
				{
					textBox18.Text,
					ap.SR_aim,
					this.bi.Difficluties[diffIndex].strains.AimSR,
					" "
				});
				textBox18 = this.textBox_diffInfo;
				textBox18.Text = string.Concat(new object[]
				{
					textBox18.Text,
					ap.SR_speed,
					this.bi.Difficluties[diffIndex].strains.SpeedSR,
					" "
				});
				System.Windows.Controls.TextBox textBox21 = this.textBox_diffInfo;
				textBox21.Text = textBox21.Text + ap.PP_total + this.bi.Difficluties[diffIndex].strains.PP;
			}
			else
			{
				System.Windows.Controls.TextBox textBox22 = this.textBox_diffInfo;
				textBox22.Text = textBox22.Text + "\n\n[" + ap.SR + "]";
				textBox18 = this.textBox_diffInfo;
				textBox18.Text = string.Concat(new object[]
				{
					textBox18.Text,
					"\n",
					ap.SR_total,
					this.bi.Difficluties[diffIndex].strains.TotalSR,
					" "
				});
				System.Windows.Controls.TextBox textBox23 = this.textBox_diffInfo;
				textBox23.Text = textBox23.Text + ap.PP_total + this.bi.Difficluties[diffIndex].strains.PP;
			}
			System.Windows.Controls.TextBox textBox24 = this.textBox_diffInfo;
			textBox24.Text = textBox24.Text + "\n\n[" + ap.Elements + "]";
			System.Windows.Controls.TextBox textBox25 = this.textBox_diffInfo;
			textBox25.Text = textBox25.Text + "\n" + ap.ComboC + this.BoolToYN(this.bi.Difficluties[diffIndex].isComboColor);
			System.Windows.Controls.TextBox textBox26 = this.textBox_diffInfo;
			textBox26.Text = textBox26.Text + "\n" + ap.SliderB + this.BoolToYN(this.bi.Difficluties[diffIndex].isSliderBorder);
			System.Windows.Controls.TextBox textBox27 = this.textBox_diffInfo;
			textBox27.Text = textBox27.Text + "\n" + ap.SliderT + this.BoolToYN(this.bi.Difficluties[diffIndex].isSliderTrackOverride);
			System.Windows.Controls.TextBox textBox28 = this.textBox_diffInfo;
			textBox28.Text = textBox28.Text + "\n" + ap.Spinner + this.BoolToYN(this.bi.Difficluties[diffIndex].isSpinner);
			System.Windows.Controls.TextBox textBox29 = this.textBox_diffInfo;
			textBox29.Text = textBox29.Text + "\n" + ap.SB + this.BoolToYN(this.bi.Difficluties[diffIndex].isStoryBoard);
			System.Windows.Controls.TextBox textBox30 = this.textBox_diffInfo;
			textBox30.Text = textBox30.Text + "\n" + ap.Video + this.BoolToYN(this.bi.Difficluties[diffIndex].isVideo);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000056A8 File Offset: 0x000038A8
		private int GetSlideIndex(int snapValue)
		{
			switch (snapValue)
			{
			case 1:
				return 1;
			case 2:
				return 2;
			case 3:
				return 2;
			case 4:
				return 3;
			case 5:
			case 7:
			case 9:
			case 10:
			case 11:
				break;
			case 6:
				return 3;
			case 8:
				return 4;
			case 12:
				return 4;
			default:
				if (snapValue == 16)
				{
					return 5;
				}
				break;
			}
			return 1;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00005705 File Offset: 0x00003905
		private int GetSnapValue(int sliderIndex, bool is4)
		{
			switch (sliderIndex)
			{
			case 1:
				return 1;
			case 2:
				if (is4)
				{
					return 2;
				}
				return 3;
			case 3:
				if (is4)
				{
					return 4;
				}
				return 6;
			case 4:
				if (is4)
				{
					return 8;
				}
				return 12;
			case 5:
				return 16;
			default:
				return 1;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00005744 File Offset: 0x00003944
		private void treeView_warnings_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (this.treeView_warnings.SelectedItem == null)
			{
				return;
			}
			TreeViewItem treeViewItem = (TreeViewItem)this.treeView_warnings.SelectedItem;
			if (!treeViewItem.HasItems)
			{
				TreeViewItem treeViewItem2 = (TreeViewItem)treeViewItem.Parent;
				for (int i = 0; i < this.ReportsList.Count; i++)
				{
					if (this.ReportsList[i].Brief == treeViewItem.Header.ToString() && (treeViewItem2.Header.ToString() == "Global" || this.bi.Difficluties[this.ReportsList[i].DifficultyIndex].Version == treeViewItem2.Header.ToString()))
					{
						this.richTextBox_warningsText.Document.Blocks.Clear();
						this.richTextBox_warningsText.Document.Blocks.Add(this.ReportsList[i].Description);
					}
				}
				for (int j = 0; j < this.bi.Difficluties.Count; j++)
				{
					if (this.bi.Difficluties[j].Version == treeViewItem2.Header.ToString())
					{
						this.listBox_diffs.SelectedIndex = j;
						j = this.bi.Difficluties.Count;
					}
				}
				return;
			}
			this.richTextBox_warningsText.Document.Blocks.Clear();
			for (int k = 0; k < this.bi.Difficluties.Count; k++)
			{
				if (this.bi.Difficluties[k].Version == treeViewItem.Header.ToString())
				{
					this.listBox_diffs.SelectedIndex = k;
					k = this.bi.Difficluties.Count;
				}
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00005930 File Offset: 0x00003B30
		private void button_btmDirOpen_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				if (Directory.Exists(this.bi.HomeDirectory))
				{
					Process.Start("explorer.exe", this.bi.HomeDirectory);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000597C File Offset: 0x00003B7C
		private void button_btmSpread_Click(object sender, RoutedEventArgs e)
		{
			w_spread w_spread = new w_spread();
			w_spread.textBlock.Text = ap.Spread_exp;
			w_spread.textBlock2.Text = ap.Spread_exp1;
			w_spread.Owner = this;
			System.Windows.Point mousePositionWindowsForms = this.GetMousePositionWindowsForms();
			w_spread.Left = mousePositionWindowsForms.X;
			w_spread.Top = mousePositionWindowsForms.Y;
			List<MainWindow.SpreadDifficulty> list = new List<MainWindow.SpreadDifficulty>();
			for (int k = 0; k < this.bi.Difficluties.Count; k++)
			{
				if (this.bi.Difficluties[k].DifficlutyMode == DifficlutyModeType.Standard)
				{
					MainWindow.SpreadDifficulty spreadDifficulty = new MainWindow.SpreadDifficulty();
					spreadDifficulty.Version = this.bi.Difficluties[k].Version;
					spreadDifficulty.SR = (double)this.bi.Difficluties[k].strains.TotalSR;
					spreadDifficulty.PosVal = (double)this.bi.Difficluties[k].strains.PP;
					spreadDifficulty.File.BeginInit();
					if (spreadDifficulty.SR <= 1.5)
					{
						spreadDifficulty.File.UriSource = new Uri("../Icons/difficulties/Easy-s.png", UriKind.Relative);
					}
					else if (spreadDifficulty.SR <= 2.25)
					{
						spreadDifficulty.File.UriSource = new Uri("../Icons/difficulties/Normal-s.png", UriKind.Relative);
					}
					else if (spreadDifficulty.SR <= 3.75)
					{
						spreadDifficulty.File.UriSource = new Uri("../Icons/difficulties/Hard-s.png", UriKind.Relative);
					}
					else if (spreadDifficulty.SR <= 5.25)
					{
						spreadDifficulty.File.UriSource = new Uri("../Icons/difficulties/Insane-s.png", UriKind.Relative);
					}
					else
					{
						spreadDifficulty.File.UriSource = new Uri("../Icons/difficulties/Expert-s.png", UriKind.Relative);
					}
					spreadDifficulty.File.EndInit();
					spreadDifficulty.Image.Width = 16.0;
					spreadDifficulty.Image.Height = 16.0;
					spreadDifficulty.Image.Stretch = Stretch.Fill;
					spreadDifficulty.Image.Source = spreadDifficulty.File;
					spreadDifficulty.Image.ToolTip = this.bi.Difficluties[k].Version;
					spreadDifficulty.Image.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
					list.Add(spreadDifficulty);
				}
			}
			if (list.Count < 3)
			{
				System.Windows.MessageBox.Show(ap.Spread_err, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				return;
			}
			list = (from i in list
			orderby i.PosVal
			select i).ToList<MainWindow.SpreadDifficulty>();
			list[0].Image.Margin = new Thickness(0.0, 0.0, 0.0, 0.0);
			w_spread.grid.Children.Add(list[0].Image);
			list[list.Count - 1].Image.Margin = new Thickness(w_spread.grid.Width - 16.0, 0.0, 0.0, 0.0);
			w_spread.grid.Children.Add(list[list.Count - 1].Image);
			double posVal = list[0].PosVal;
			double posVal2 = list[list.Count - 1].PosVal;
			for (int j = 1; j < list.Count - 1; j++)
			{
				double num = list[j].PosVal - posVal;
				double left = (w_spread.grid.Width - 16.0) / (posVal2 - posVal) * num;
				list[j].Image.Margin = new Thickness(left, 0.0, 0.0, 0.0);
				w_spread.grid.Children.Add(list[j].Image);
			}
			w_spread.ShowDialog();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00005DC4 File Offset: 0x00003FC4
		private void button_btmSS_Click(object sender, RoutedEventArgs e)
		{
			string text = DateTime.Now.ToString(new CultureInfo("en-GB"));
			snapshot snapshot = new snapshot();
			snapshot.Owner = this;
			System.Windows.Point mousePositionWindowsForms = this.GetMousePositionWindowsForms();
			snapshot.Left = mousePositionWindowsForms.X;
			snapshot.Top = mousePositionWindowsForms.Y;
			snapshot.textBox.Text = text;
			if (snapshot.ShowDialog() == true)
			{
				if (!Snapshot.CreateSnapshot(this.bi, this.BeatmapDirectoryName, snapshot.textBox.Text, snapshot.checkBox_fullBkp.IsChecked.Value))
				{
					System.Windows.MessageBox.Show(ap.SS_6, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				else
				{
					System.Windows.MessageBox.Show(ap.SS_7, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Asterisk);
				}
				this.LoadSnapshots();
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00005EA8 File Offset: 0x000040A8
		private void LoadSnapshots()
		{
			this.comboBox_snapshots.SelectedIndex = -1;
			this.comboBox_snapshots.Items.Clear();
			this.richTextBox_snapshots.Document.Blocks.Clear();
			string snapshotDir = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Snapshots", this.BeatmapDirectoryName);
			this._g_SnapshotList = Snapshot.GetSnapshots(snapshotDir);
			for (int i = 0; i < this._g_SnapshotList.Directory.Count; i++)
			{
				this.comboBox_snapshots.Items.Add(this._g_SnapshotList.Name[i]);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00005F50 File Offset: 0x00004150
		private void comboBox_snapshots_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.richTextBox_snapshots.Document.Blocks.Clear();
			if (this.comboBox_snapshots.SelectedIndex == -1)
			{
				this.button_ssDirOpen.IsEnabled = false;
				this.button_ssDelete.IsEnabled = false;
				return;
			}
			this.button_ssDirOpen.IsEnabled = true;
			this.button_ssDelete.IsEnabled = true;
			this.richTextBox_snapshots.Document.Blocks.Add(Snapshot.Compare(this.bi, this._g_SnapshotList.Directory[this.comboBox_snapshots.SelectedIndex]));
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00005FEC File Offset: 0x000041EC
		private void button_btmWeb_Click(object sender, RoutedEventArgs e)
		{
			string text = "https://osu.ppy.sh/s/";
			for (int i = 0; i < this.bi.Difficluties.Count; i++)
			{
				if (this.bi.Difficluties[0].BeatmapSetID != 0)
				{
					text += this.bi.Difficluties[0].BeatmapSetID;
					break;
				}
			}
			Process.Start(text);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000605D File Offset: 0x0000425D
		private void button_btmDirRF_Click(object sender, RoutedEventArgs e)
		{
			this.textBox_btmSelect.Text = this.textBlock_biInfo.Text.Split(new char[]
			{
				'\n'
			})[0];
			this.button_btmOpen.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000609C File Offset: 0x0000429C
		private void listBox_diffs_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.listBox_diffs.SelectedIndex == -1)
			{
				this.groupBox_settings.IsEnabled = false;
				this.groupBox_betmap.IsEnabled = false;
				return;
			}
			this.groupBox_settings.IsEnabled = true;
			this.groupBox_betmap.IsEnabled = true;
			this.LoadDifficultyInformation(this.listBox_diffs.SelectedIndex);
			this.slider_snap_4.Value = (double)this.GetSlideIndex(this.bi.Difficluties[this.listBox_diffs.SelectedIndex].UsnappedSettings.slider_snapping16);
			this.slider_snap_3.Value = (double)this.GetSlideIndex(this.bi.Difficluties[this.listBox_diffs.SelectedIndex].UsnappedSettings.slider_snapping12);
			this.STui_SelectionChanged();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00006170 File Offset: 0x00004370
		private void button_snapRF_Click(object sender, RoutedEventArgs e)
		{
			this.bi.Difficluties[this.listBox_diffs.SelectedIndex].UsnappedSettings.slider_snapping16 = this.GetSnapValue(Convert.ToInt32(this.slider_snap_4.Value), true);
			this.bi.Difficluties[this.listBox_diffs.SelectedIndex].UsnappedSettings.slider_snapping12 = this.GetSnapValue(Convert.ToInt32(this.slider_snap_3.Value), false);
			ObjectsProcessor.ProcessUnsnap(this.bi.Difficluties[this.listBox_diffs.SelectedIndex], this.bi.Difficluties[this.listBox_diffs.SelectedIndex].UsnappedSettings.slider_snapping16, this.bi.Difficluties[this.listBox_diffs.SelectedIndex].UsnappedSettings.slider_snapping12);
			int num = this.ReportsList.Count;
			for (int i = 0; i < num; i++)
			{
				if (this.ReportsList[i].DifficultyIndex == this.listBox_diffs.SelectedIndex && this.ReportsList[i].Brief == Strings.ObjectsSnapping_Brief)
				{
					this.ReportsList.RemoveAt(i);
					num--;
					i--;
				}
			}
			this.ReportsList.Add(RunReport.ObjectsSnapping(this.bi, this.listBox_diffs.SelectedIndex));
			this.ClearReportsList();
			this.DisplayReport();
			foreach (object obj in ((IEnumerable)this.treeView_warnings.Items))
			{
				TreeViewItem treeViewItem = (TreeViewItem)obj;
				if (treeViewItem.Header.ToString() == this.bi.Difficluties[this.listBox_diffs.SelectedIndex].Version)
				{
					treeViewItem.IsExpanded = true;
					if (treeViewItem.HasItems)
					{
						foreach (object obj2 in ((IEnumerable)treeViewItem.Items))
						{
							TreeViewItem treeViewItem2 = (TreeViewItem)obj2;
							if (treeViewItem2.Header.ToString() == Strings.ObjectsSnapping_Brief)
							{
								treeViewItem2.IsSelected = true;
							}
						}
					}
				}
			}
			this.tabControl_btmInfo.SelectedIndex = 3;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000063FC File Offset: 0x000045FC
		private void slider_snap_4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (this.label_snap_4 != null)
			{
				int num = 0;
				int.TryParse(e.NewValue.ToString(), out num);
				switch (num)
				{
				case 1:
					this.label_snap_4.Content = "1/1";
					break;
				case 2:
					this.label_snap_4.Content = "1/2";
					return;
				case 3:
					this.label_snap_4.Content = "1/4";
					return;
				case 4:
					this.label_snap_4.Content = "1/8";
					return;
				case 5:
					this.label_snap_4.Content = "1/16";
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000064A0 File Offset: 0x000046A0
		private void slider_snap_3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (this.label_snap_3 != null)
			{
				int num = 0;
				int.TryParse(e.NewValue.ToString(), out num);
				switch (num)
				{
				case 1:
					this.label_snap_3.Content = "1/1";
					break;
				case 2:
					this.label_snap_3.Content = "1/3";
					return;
				case 3:
					this.label_snap_3.Content = "1/6";
					return;
				case 4:
					this.label_snap_3.Content = "1/12";
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000652C File Offset: 0x0000472C
		private void STui_SelectionChanged()
		{
			this.groupBoxSpd.Header = "Intense ";
			this.groupBoxAim.Header = "Aim ";
			if (this.listBox_diffs.Items.Count < 1)
			{
				return;
			}
			try
			{
				this.Chart.Series[0].Points.Clear();
				this.Chart1.Series[1].Points.Clear();
				if (this.bi.Difficluties[this.listBox_diffs.SelectedIndex].DifficlutyMode == DifficlutyModeType.Standard)
				{
					this.groupBoxAim.Visibility = Visibility.Visible;
				}
				if (this.bi.Difficluties[this.listBox_diffs.SelectedIndex].DifficlutyMode == DifficlutyModeType.Taiko || this.bi.Difficluties[this.listBox_diffs.SelectedIndex].DifficlutyMode == DifficlutyModeType.Mania || this.bi.Difficluties[this.listBox_diffs.SelectedIndex].DifficlutyMode == DifficlutyModeType.CtB)
				{
					this.groupBoxAim.Visibility = Visibility.Hidden;
				}
				for (int i = 0; i < this.bi.Difficluties[this.listBox_diffs.SelectedIndex].strains.StrainsSpeed.Count; i++)
				{
					this.Chart.Series[0].Points.AddXY((double)(i + 1), (double)this.bi.Difficluties[this.listBox_diffs.SelectedIndex].strains.StrainsSpeed[i]);
				}
				for (int j = 0; j < this.bi.Difficluties[this.listBox_diffs.SelectedIndex].strains.StrainsAim.Count; j++)
				{
					this.Chart1.Series[1].Points.AddXY((double)(j + 1), (double)this.bi.Difficluties[this.listBox_diffs.SelectedIndex].strains.StrainsAim[j]);
				}
				this.Chart.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
				this.Chart.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
				this.Chart.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
				this.Chart.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
				this.Chart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
				this.Chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
				this.Chart1.ChartAreas[0].AxisX.MajorTickMark.Enabled = false;
				this.Chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
				this.Chart1.ChartAreas[0].AxisY.MajorGrid.LineWidth = 0;
				this.Chart1.ChartAreas[0].AxisX.MajorGrid.LineWidth = 0;
				this.Chart.ChartAreas[0].AxisX.Minimum = 0.0;
				this.Chart.ChartAreas[0].AxisX.Maximum = (double)this.bi.Difficluties[this.listBox_diffs.SelectedIndex].strains.StrainsSpeed.Count;
				this.Chart1.ChartAreas[0].AxisX.Minimum = 0.0;
				this.Chart1.ChartAreas[0].AxisX.Maximum = (double)this.bi.Difficluties[this.listBox_diffs.SelectedIndex].strains.StrainsAim.Count;
			}
			catch
			{
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00006988 File Offset: 0x00004B88
		private void Chart_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				this.Chart.ChartAreas[0].CursorX.SetCursorPixelPosition(e.Location, true);
				double num = this.Chart.ChartAreas[0].CursorX.Position + 1.0;
				int num2 = 400;
				if (this.bi.Difficluties[this.listBox_diffs.SelectedIndex].DifficlutyMode == DifficlutyModeType.CtB)
				{
					num2 = 750;
				}
				TimeSpan timeSpan = TimeSpan.FromMilliseconds(num * (double)num2 - (double)num2);
				TimeSpan timeSpan2 = TimeSpan.FromMilliseconds(num * (double)num2);
				this.groupBoxSpd.Header = "Intense " + string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds) + " - " + string.Format("{0:D2}:{1:D2}", timeSpan2.Minutes, timeSpan2.Seconds);
			}
			catch
			{
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00006A9C File Offset: 0x00004C9C
		private void Chart1_MouseMove(object sender, MouseEventArgs e)
		{
			try
			{
				this.Chart1.ChartAreas[0].CursorX.SetCursorPixelPosition(e.Location, true);
				double num = this.Chart1.ChartAreas[0].CursorX.Position + 1.0;
				int num2 = 400;
				if (this.bi.Difficluties[this.listBox_diffs.SelectedIndex].DifficlutyMode == DifficlutyModeType.CtB)
				{
					num2 = 750;
				}
				TimeSpan timeSpan = TimeSpan.FromMilliseconds(num * (double)num2 - (double)num2);
				TimeSpan timeSpan2 = TimeSpan.FromMilliseconds(num * (double)num2);
				this.groupBoxAim.Header = "Aim " + string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds) + " - " + string.Format("{0:D2}:{1:D2}", timeSpan2.Minutes, timeSpan2.Seconds);
			}
			catch
			{
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00006BB0 File Offset: 0x00004DB0
		public MainWindow()
		{
			this.SetDefaultCulture(new CultureInfo("en-US"));
			this.InitializeComponent();
			this.LoadUP();
			this.msnl = new MSNListener();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00006C16 File Offset: 0x00004E16
		private void Window_Closing(object sender, CancelEventArgs e)
		{
			this.SaveUP();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00006C20 File Offset: 0x00004E20
		public void SetDefaultCulture(CultureInfo culture)
		{
			Type typeFromHandle = typeof(CultureInfo);
			try
			{
				typeFromHandle.InvokeMember("s_userDefaultCulture", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.SetField, null, culture, new object[]
				{
					culture
				});
				typeFromHandle.InvokeMember("s_userDefaultUICulture", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.SetField, null, culture, new object[]
				{
					culture
				});
			}
			catch
			{
			}
			try
			{
				typeFromHandle.InvokeMember("m_userDefaultCulture", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.SetField, null, culture, new object[]
				{
					culture
				});
				typeFromHandle.InvokeMember("m_userDefaultUICulture", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.SetField, null, culture, new object[]
				{
					culture
				});
			}
			catch
			{
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00006CD4 File Offset: 0x00004ED4
		private string BoolToYN(bool bl)
		{
			if (bl)
			{
				return "Yes";
			}
			return "No";
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00006CE4 File Offset: 0x00004EE4
		private void button_settings_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://osu.ppy.sh/forum/p/4439879");
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00006CF1 File Offset: 0x00004EF1
		private void button_update_Click(object sender, RoutedEventArgs e)
		{
			this.checkUpdate(false);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00006CFC File Offset: 0x00004EFC
		private string checkUpdate(bool isSilent)
		{
			int num = 0;
			string address = "https://raw.githubusercontent.com/xSigi/osuModdingAssistant/master/Modding%20assistant/latest";
			string text = Assembly.GetExecutingAssembly().Location;
			text = Path.GetDirectoryName(text);
			text += "omaUpdatedata.ini";
			WebClient webClient = new WebClient();
			try
			{
				webClient.DownloadFile(address, text);
			}
			catch
			{
				if (!isSilent)
				{
					System.Windows.MessageBox.Show(ap.Update_1, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				return ap.Update_1;
			}
			try
			{
				IniReader iniReader = new IniReader(text, '=');
				File.Delete(text);
				int.TryParse(iniReader.GetValue("Latest", "Verison", MainWindow.OMA_CURRENT_VERSION.ToString()), out num);
			}
			catch
			{
				if (!isSilent)
				{
					System.Windows.MessageBox.Show(ap.Update_2, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
				}
				return ap.Update_2;
			}
			if (num > MainWindow.OMA_CURRENT_VERSION)
			{
				if (System.Windows.MessageBox.Show(ap.Update_3, "Modding Assistant", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
				{
					Process.Start("https://github.com/xSigi/osuModdingAssistant/releases");
				}
				return string.Empty;
			}
			if (!isSilent)
			{
				System.Windows.MessageBox.Show(ap.Update_4, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Asterisk);
			}
			return ap.Update_4;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00006E20 File Offset: 0x00005020
		private void button_openRC_Click(object sender, RoutedEventArgs e)
		{
			Process.Start("https://osu.ppy.sh/wiki/Ranking_Criteria");
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00006E30 File Offset: 0x00005030
		private System.Windows.Point GetMousePositionWindowsForms()
		{
			System.Drawing.Point mousePosition = System.Windows.Forms.Control.MousePosition;
			return new System.Windows.Point((double)mousePosition.X, (double)mousePosition.Y);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00006E58 File Offset: 0x00005058
		private void button_ssDirOpen_Click(object sender, RoutedEventArgs e)
		{
			if (this.comboBox_snapshots.SelectedIndex == -1)
			{
				return;
			}
			try
			{
				if (Directory.Exists(this._g_SnapshotList.Directory[this.comboBox_snapshots.SelectedIndex]))
				{
					Process.Start("explorer.exe", this._g_SnapshotList.Directory[this.comboBox_snapshots.SelectedIndex]);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00006ED4 File Offset: 0x000050D4
		private void button_ssDelete_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				Directory.Delete(this._g_SnapshotList.Directory[this.comboBox_snapshots.SelectedIndex], true);
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message, "Modding assistant", MessageBoxButton.OK, MessageBoxImage.Hand);
			}
			this.LoadSnapshots();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00006F30 File Offset: 0x00005130
		private void wnd_Loaded(object sender, RoutedEventArgs e)
		{
			this.InitFolder(this.textBox_btmDir.Text, false);
			if (Settings.Default.CheckForUpdates)
			{
				this.checkUpdate(true);
			}
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00006F5C File Offset: 0x0000515C
		private void checkBox_sett_getFI_Checked(object sender, RoutedEventArgs e)
		{
			Settings.Default.GetFullNameData = this.checkBox_sett_getFI.IsChecked.Value;
			Settings.Default.Save();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00006F90 File Offset: 0x00005190
		private void checkBox_sett_autoUP_Unchecked(object sender, RoutedEventArgs e)
		{
			Settings.Default.CheckForUpdates = this.checkBox_sett_autoUP.IsChecked.Value;
			Settings.Default.Save();
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00006FC4 File Offset: 0x000051C4
		private void TaikoZoomIn_Click(object sender, RoutedEventArgs e)
		{
			this.TaikoCurrentZoom += 1000;
			if (this.TaikoCurrentZoom >= 5000)
			{
				this.TaikoZoomOut.IsEnabled = true;
			}
			try
			{
				new TaikoDraw(this.bi, this.TaikoScroll, this.TaikoCurrentZoom, (int)this.Taikoslider.Value);
			}
			catch
			{
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00007038 File Offset: 0x00005238
		private void TaikoZoomOut_Click(object sender, RoutedEventArgs e)
		{
			this.TaikoCurrentZoom -= 1000;
			if (this.TaikoCurrentZoom <= 5000)
			{
				this.TaikoZoomOut.IsEnabled = false;
			}
			try
			{
				new TaikoDraw(this.bi, this.TaikoScroll, this.TaikoCurrentZoom, (int)this.Taikoslider.Value);
			}
			catch
			{
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000070AC File Offset: 0x000052AC
		private void Taikoslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			switch ((int)this.Taikoslider.Value)
			{
			case 0:
				this.labelTaiko.Content = "1/1";
				break;
			case 1:
				this.labelTaiko.Content = "1/2";
				break;
			case 2:
				this.labelTaiko.Content = "1/3";
				break;
			case 3:
				this.labelTaiko.Content = "1/4";
				break;
			case 4:
				this.labelTaiko.Content = "1/6";
				break;
			case 5:
				this.labelTaiko.Content = "1/8";
				break;
			}
			try
			{
				new TaikoDraw(this.bi, this.TaikoScroll, this.TaikoCurrentZoom, (int)this.Taikoslider.Value);
			}
			catch
			{
			}
		}

		// Token: 0x04000011 RID: 17
		private static int OMA_CURRENT_VERSION = 158;

		// Token: 0x04000012 RID: 18
		private int TaikoCurrentZoom = 10000;

		// Token: 0x04000013 RID: 19
		private MainWindow.dirS _g_dirList = new MainWindow.dirS();

		// Token: 0x04000014 RID: 20
		private BeatmapInfo bi;

		// Token: 0x04000015 RID: 21
		private MSNListener msnl;

		// Token: 0x04000016 RID: 22
		private List<Report> ReportsList;

		// Token: 0x04000017 RID: 23
		private List<TreeViewItem> ti_Root;

		// Token: 0x04000018 RID: 24
		private string BeatmapDirectoryName = string.Empty;

		// Token: 0x04000019 RID: 25
		private SnapshotList _g_SnapshotList = new SnapshotList();

		// Token: 0x02000056 RID: 86
		private class dirS
		{
			// Token: 0x0400028E RID: 654
			public List<string> dirList = new List<string>();

			// Token: 0x0400028F RID: 655
			public List<string> showList = new List<string>();
		}

		// Token: 0x02000057 RID: 87
		private class SpreadDifficulty
		{
			// Token: 0x04000290 RID: 656
			public System.Windows.Controls.Image Image = new System.Windows.Controls.Image();

			// Token: 0x04000291 RID: 657
			public BitmapImage File = new BitmapImage();

			// Token: 0x04000292 RID: 658
			public string Version;

			// Token: 0x04000293 RID: 659
			public double SR;

			// Token: 0x04000294 RID: 660
			public double PosVal;
		}
	}
}
