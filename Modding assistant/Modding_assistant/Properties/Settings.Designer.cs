using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Modding_assistant.Properties
{
	// Token: 0x02000007 RID: 7
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00007886 File Offset: 0x00005A86
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000788D File Offset: 0x00005A8D
		// (set) Token: 0x06000056 RID: 86 RVA: 0x0000789F File Offset: 0x00005A9F
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("800")]
		public double Width
		{
			get
			{
				return (double)this["Width"];
			}
			set
			{
				this["Width"] = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000078B2 File Offset: 0x00005AB2
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000078C4 File Offset: 0x00005AC4
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("600")]
		public double Height
		{
			get
			{
				return (double)this["Height"];
			}
			set
			{
				this["Height"] = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000078D7 File Offset: 0x00005AD7
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000078E9 File Offset: 0x00005AE9
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("")]
		public string BeatmapsDir
		{
			get
			{
				return (string)this["BeatmapsDir"];
			}
			set
			{
				this["BeatmapsDir"] = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000078F7 File Offset: 0x00005AF7
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00007909 File Offset: 0x00005B09
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool GetFullNameData
		{
			get
			{
				return (bool)this["GetFullNameData"];
			}
			set
			{
				this["GetFullNameData"] = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600005D RID: 93 RVA: 0x0000791C File Offset: 0x00005B1C
		// (set) Token: 0x0600005E RID: 94 RVA: 0x0000792E File Offset: 0x00005B2E
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool CheckForUpdates
		{
			get
			{
				return (bool)this["CheckForUpdates"];
			}
			set
			{
				this["CheckForUpdates"] = value;
			}
		}

		// Token: 0x04000050 RID: 80
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
