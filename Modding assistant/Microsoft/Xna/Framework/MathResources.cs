using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Microsoft.Xna.Framework
{
	// Token: 0x0200001A RID: 26
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class MathResources
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00011B6E File Offset: 0x0000FD6E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (MathResources.resourceMan == null)
				{
					MathResources.resourceMan = new ResourceManager("Microsoft.Xna.Framework.MathResources", typeof(MathResources).Assembly);
				}
				return MathResources.resourceMan;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00011B9A File Offset: 0x0000FD9A
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00011BA1 File Offset: 0x0000FDA1
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return MathResources.resourceCulture;
			}
			set
			{
				MathResources.resourceCulture = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00011BA9 File Offset: 0x0000FDA9
		internal static string BoundingBoxZeroPoints
		{
			get
			{
				return MathResources.ResourceManager.GetString("BoundingBoxZeroPoints", MathResources.resourceCulture);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00011BBF File Offset: 0x0000FDBF
		internal static string BoundingSphereZeroPoints
		{
			get
			{
				return MathResources.ResourceManager.GetString("BoundingSphereZeroPoints", MathResources.resourceCulture);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00011BD5 File Offset: 0x0000FDD5
		internal static string InvalidStringFormat
		{
			get
			{
				return MathResources.ResourceManager.GetString("InvalidStringFormat", MathResources.resourceCulture);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00011BEB File Offset: 0x0000FDEB
		internal static string NegativePlaneDistance
		{
			get
			{
				return MathResources.ResourceManager.GetString("NegativePlaneDistance", MathResources.resourceCulture);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00011C01 File Offset: 0x0000FE01
		internal static string NegativeRadius
		{
			get
			{
				return MathResources.ResourceManager.GetString("NegativeRadius", MathResources.resourceCulture);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00011C17 File Offset: 0x0000FE17
		internal static string NotEnoughCorners
		{
			get
			{
				return MathResources.ResourceManager.GetString("NotEnoughCorners", MathResources.resourceCulture);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00011C2D File Offset: 0x0000FE2D
		internal static string NotEnoughSourceSize
		{
			get
			{
				return MathResources.ResourceManager.GetString("NotEnoughSourceSize", MathResources.resourceCulture);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00011C43 File Offset: 0x0000FE43
		internal static string NotEnoughTargetSize
		{
			get
			{
				return MathResources.ResourceManager.GetString("NotEnoughTargetSize", MathResources.resourceCulture);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00011C59 File Offset: 0x0000FE59
		internal static string OppositePlanes
		{
			get
			{
				return MathResources.ResourceManager.GetString("OppositePlanes", MathResources.resourceCulture);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00011C6F File Offset: 0x0000FE6F
		internal static string OutRangeFieldOfView
		{
			get
			{
				return MathResources.ResourceManager.GetString("OutRangeFieldOfView", MathResources.resourceCulture);
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007843 File Offset: 0x00005A43
		internal MathResources()
		{
		}

		// Token: 0x0400013E RID: 318
		private static ResourceManager resourceMan;

		// Token: 0x0400013F RID: 319
		private static CultureInfo resourceCulture;
	}
}
