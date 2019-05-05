using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Modding_assistant.windows
{
	// Token: 0x02000009 RID: 9
	public partial class snapshot : Window
	{
		// Token: 0x06000064 RID: 100 RVA: 0x000079BA File Offset: 0x00005BBA
		public snapshot()
		{
			this.InitializeComponent();
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000079C8 File Offset: 0x00005BC8
		private void button_cancel_Click(object sender, RoutedEventArgs e)
		{
			base.DialogResult = new bool?(false);
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000079D6 File Offset: 0x00005BD6
		private void button_ok_Click(object sender, RoutedEventArgs e)
		{
			base.DialogResult = new bool?(true);
		}
	}
}
