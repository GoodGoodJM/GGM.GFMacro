using GGM.GFMarcro.Core.Handler.Exception;
using System;
using System.Drawing;

namespace GGM.GFMarcro.Core.Handler
{
    // Warning : This class is not thread-safe.
    public class MomoHandler : BaseApplicationHandler
    {
        private const string LD_PLAYER_MAIN_FRAME = "LDPlayerMainFrame";
        private const string RENDER_WINDOW = "RenderWindow";
        private const string THE_RENDER = "TheRender";

        public MomoHandler(string applicationName)
        {
            ApplicationName = applicationName;

            ApplicationWindow = Kernel.User32.FindWindow(LD_PLAYER_MAIN_FRAME, applicationName);
            if (ApplicationWindow == IntPtr.Zero)
                throw new ApplicationHandlerException(ApplicationHandlerExceptionType.CANNOT_FIND_APPLICATION);

            var renderHandler = Kernel.User32.FindWindowEx(ApplicationWindow, IntPtr.Zero, RENDER_WINDOW, THE_RENDER);
            if (renderHandler == IntPtr.Zero)
                throw new ApplicationHandlerException(ApplicationHandlerExceptionType.CANNOT_FIND_APPLICATION);
            TargetHandle = renderHandler;
        }

        public override string ApplicationName { get; }
        public override IntPtr ApplicationWindow { get; }
        public override IntPtr TargetHandle { get; }
    }
}
