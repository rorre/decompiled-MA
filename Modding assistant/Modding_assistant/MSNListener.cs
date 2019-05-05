using System;
using System.Runtime.InteropServices;

namespace Modding_assistant
{
	// Token: 0x02000002 RID: 2
	public class MSNListener : IDisposable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002057 File Offset: 0x00000257
		public static string NowPlaying
		{
			get
			{
				return MSNListener.nowPlaying;
			}
			set
			{
				MSNListener.nowPlaying = value;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public MSNListener()
		{
			MSNListener.WNDCLASS wndclass = new MSNListener.WNDCLASS
			{
				lpszClassName = "MsnMsgrUIManager",
				lpfnWndProc = Marshal.GetFunctionPointerForDelegate(MSNListener.callbackDelegate)
			};
			bool flag = MSNListener.RegisterClassW(ref wndclass) != 0;
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!flag && lastWin32Error != 1410)
			{
				throw new Exception("Could not register window class");
			}
			this.handle = MSNListener.CreateWindowExW(0u, "MsnMsgrUIManager", string.Empty, 0u, 0, 0, 0, 0, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020EB File Offset: 0x000002EB
		private static IntPtr CustomWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
		{
			if (msg == 74u)
			{
				MSNListener.NowPlaying = ((MSNListener.CopyDataStruct)Marshal.PtrToStructure(lParam, typeof(MSNListener.CopyDataStruct))).lpData;
			}
			return MSNListener.DefWindowProcW(hWnd, msg, wParam, lParam);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000211A File Offset: 0x0000031A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000006 RID: 6
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr CreateWindowExW(uint dwExStyle, [MarshalAs(UnmanagedType.LPWStr)] string lpClassName, [MarshalAs(UnmanagedType.LPWStr)] string lpWindowName, uint dwStyle, int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

		// Token: 0x06000007 RID: 7
		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr DefWindowProcW(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x06000008 RID: 8
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool DestroyWindow(IntPtr hWnd);

		// Token: 0x06000009 RID: 9
		[DllImport("user32.dll", SetLastError = true)]
		private static extern ushort RegisterClassW([In] ref MSNListener.WNDCLASS lpWndClass);

		// Token: 0x0600000A RID: 10 RVA: 0x00002129 File Offset: 0x00000329
		private void Dispose(bool disposing)
		{
			if (this.handle != IntPtr.Zero)
			{
				MSNListener.DestroyWindow(this.handle);
				this.handle = IntPtr.Zero;
			}
		}

		// Token: 0x04000001 RID: 1
		private const int ERROR_CLASS_ALREADY_EXISTS = 1410;

		// Token: 0x04000002 RID: 2
		private const uint WM_COPYDATA = 74u;

		// Token: 0x04000003 RID: 3
		private const string class_name = "MsnMsgrUIManager";

		// Token: 0x04000004 RID: 4
		private static string nowPlaying;

		// Token: 0x04000005 RID: 5
		private IntPtr handle;

		// Token: 0x04000006 RID: 6
		private static MSNListener.WndProc callbackDelegate = new MSNListener.WndProc(MSNListener.CustomWndProc);

		// Token: 0x02000052 RID: 82
		// (Invoke) Token: 0x060002FD RID: 765
		private delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x02000053 RID: 83
		public struct CopyDataStruct
		{
			// Token: 0x0400027F RID: 639
			public IntPtr dwData;

			// Token: 0x04000280 RID: 640
			public int cbData;

			// Token: 0x04000281 RID: 641
			[MarshalAs(UnmanagedType.LPWStr)]
			public string lpData;
		}

		// Token: 0x02000054 RID: 84
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		private struct WNDCLASS
		{
			// Token: 0x04000282 RID: 642
			private readonly uint style;

			// Token: 0x04000283 RID: 643
			public IntPtr lpfnWndProc;

			// Token: 0x04000284 RID: 644
			private readonly int cbClsExtra;

			// Token: 0x04000285 RID: 645
			private readonly int cbWndExtra;

			// Token: 0x04000286 RID: 646
			private readonly IntPtr hInstance;

			// Token: 0x04000287 RID: 647
			private readonly IntPtr hIcon;

			// Token: 0x04000288 RID: 648
			private readonly IntPtr hCursor;

			// Token: 0x04000289 RID: 649
			private readonly IntPtr hbrBackground;

			// Token: 0x0400028A RID: 650
			[MarshalAs(UnmanagedType.LPWStr)]
			private readonly string lpszMenuName;

			// Token: 0x0400028B RID: 651
			[MarshalAs(UnmanagedType.LPWStr)]
			public string lpszClassName;
		}
	}
}
