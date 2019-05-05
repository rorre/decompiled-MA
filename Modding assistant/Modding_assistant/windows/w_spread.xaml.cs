using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Modding_assistant.windows
{
	// Token: 0x0200000A RID: 10
	public partial class w_spread : Window
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00007AB5 File Offset: 0x00005CB5
		public w_spread()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00007AC3 File Offset: 0x00005CC3
		private void button_Click(object sender, RoutedEventArgs e)
		{
			base.Close();
		}
	}
}
