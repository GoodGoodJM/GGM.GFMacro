using System;
using System.Runtime.InteropServices;

namespace GGM.GFMarcro.Core.Kernel
{
    static class User32
    {
        public static class PrintWindowFlag
        {
            public const int WM_PRINT = 0x00;
            public const int WM_PRINTCLIENT = 0x01;
            // Warring : This flag only for windows 8.1+
            public const int PW_RENDERFULLCONTENT = 0x02;
        }

        public static class MouseEventFlag
        {
            public const int WM_LBUTTONDOWN = 0x201;
            public const int WM_LBUTTONUP = 0x202;
        }

        [DllImport(nameof(User32), EntryPoint = nameof(FindWindow))]
        public static extern IntPtr FindWindow(string className, string windowName);

        [DllImport(nameof(User32), EntryPoint = nameof(PrintWindow))]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PrintWindow(IntPtr hWnd, IntPtr hdcBlt, int flags);

        [DllImport(nameof(User32), EntryPoint = nameof(SendMessage))]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, IntPtr lParam);

        [DllImport(nameof(User32), EntryPoint = nameof(FindWindowEx))]
        public static extern IntPtr FindWindowEx(IntPtr parentHWnd, IntPtr childHWnd, string className, string windowNamwe);

    }
}
