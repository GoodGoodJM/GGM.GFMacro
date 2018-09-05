using GGM.GFMarcro.Core.Handler.Exception;
using System;
using System.Drawing;
using System.Threading;

namespace GGM.GFMarcro.Core.Handler
{
    // Warning : This class is not thread-safe.
    public class NoxHandler : IApplicationHandler
    {
        private const string QT5_WINDOW_ICON = "Qt5QWindowIcon";
        private const string SCREEN_BOARD_CLASS_WINDOW = "ScreenBoardClassWindow";
        private const string Q_WIDGET_CLASS_WINDOW = "QWidgetClassWindow";
        private const string SUB_WIN = "subWin";
        private const string SUB = "sub";

        public NoxHandler(string applicationName)
        {
            ApplicationName = applicationName;

            ApplicationWindow = Kernel.User32.FindWindow(QT5_WINDOW_ICON, applicationName);
            if (ApplicationWindow == IntPtr.Zero)
                throw new ApplicationHandlerException(ApplicationHandlerExceptionType.CANNOT_FIND_APPLICATION);

            var firstNode = Kernel.User32.FindWindowEx(ApplicationWindow, IntPtr.Zero, QT5_WINDOW_ICON, SCREEN_BOARD_CLASS_WINDOW);
            if (firstNode == IntPtr.Zero)
                throw new ApplicationHandlerException(ApplicationHandlerExceptionType.CANNOT_FIND_APPLICATION);

            var secondNode = Kernel.User32.FindWindowEx(firstNode, IntPtr.Zero, QT5_WINDOW_ICON, Q_WIDGET_CLASS_WINDOW);
            if (secondNode == IntPtr.Zero)
                throw new ApplicationHandlerException(ApplicationHandlerExceptionType.CANNOT_FIND_APPLICATION);

            TargetHandle = secondNode; // Kernel.User32.FindWindowEx(secondNode, IntPtr.Zero, SUB_WIN, SUB);
            if (TargetHandle == IntPtr.Zero)
                throw new ApplicationHandlerException(ApplicationHandlerExceptionType.CANNOT_FIND_APPLICATION);
        }

        public string ApplicationName { get; }
        public IntPtr ApplicationWindow { get; }
        public IntPtr TargetHandle { get; }

        public Bitmap GetScreenImage()
        {
            var applicationWindowGraphics = Graphics.FromHwnd(ApplicationWindow);
            var rect = Rectangle.Round(applicationWindowGraphics.VisibleClipBounds);
            Console.WriteLine($"ScreenSize is {rect}");
            var image = new Bitmap(rect.Width, rect.Height);

            using (var bitmapGraphics = Graphics.FromImage(image))
            {
                var hdc = bitmapGraphics.GetHdc();
                bool isSucces = Kernel.User32.PrintWindow(TargetHandle, hdc, Kernel.User32.PrintWindowFlag.PW_RENDERFULLCONTENT);
                if (!isSucces)
                {
                    image.Dispose();
                    return null;
                }
                bitmapGraphics.ReleaseHdc(hdc);
            }
            return image;
        }

        public void SendClick(Point point)
        {
            var transformedPoint = new Point(point.X / 2, point.Y / 2);
            var lparam = new IntPtr(transformedPoint.X | (transformedPoint.Y << 16));
            Kernel.User32.SendMessage(TargetHandle, Kernel.User32.MouseEventFlag.WM_LBUTTONDOWN, 1, lparam);
            Kernel.User32.SendMessage(TargetHandle, Kernel.User32.MouseEventFlag.WM_LBUTTONUP, 0, lparam);
        }
    }
}
