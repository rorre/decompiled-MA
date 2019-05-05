using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Modding_assistant.Properties
{
	// Token: 0x02000006 RID: 6
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00007843 File Offset: 0x00005A43
		internal Resources()
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000784B File Offset: 0x00005A4B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("Modding_assistant.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00007877 File Offset: 0x00005A77
		// (set) Token: 0x06000053 RID: 83 RVA: 0x0000787E File Offset: 0x00005A7E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x0400004E RID: 78
		private static ResourceManager resourceMan;

		// Token: 0x0400004F RID: 79
		private static CultureInfo resourceCulture;
	}
}
