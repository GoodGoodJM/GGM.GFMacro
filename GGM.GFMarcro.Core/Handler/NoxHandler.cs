using GGM.GFMarcro.Core.Handler.Exception;
using System;
using System.Drawing;
using System.Threading;

namespace GGM.GFMarcro.Core.Handler
{
    // Warning : This class is not thread-safe.
    public class NoxHandler : BaseApplicationHandler
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

            TargetHandle = secondNode;
            if (TargetHandle == IntPtr.Zero)
                throw new ApplicationHandlerException(ApplicationHandlerExceptionType.CANNOT_FIND_APPLICATION);
        }

        public override string ApplicationName { get; }
        public override IntPtr ApplicationWindow { get; }
        public override IntPtr TargetHandle { get; }

        public override void SendClick(Point point)
        {
            var transformedPoint = new Point(point.X / 2, point.Y / 2);
            base.SendClick(transformedPoint);
        }
    }
}