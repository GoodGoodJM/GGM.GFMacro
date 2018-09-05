using System;

namespace GGM.GFMarcro.Kernel
{
    static class User32
    {
        [System.Runtime.InteropServices.DllImport(nameof(User32), EntryPoint = nameof(FindWindow))]
        static extern IntPtr FindWindow(string className, string windowName);

        [System.Runtime.InteropServices.DllImport(nameof(User32), EntryPoint = nameof(PrintWindow))]
        static extern IntPtr PrintWindow(IntPtr hWnd, IntPtr  hdcBlt, int flags);
    }
}
