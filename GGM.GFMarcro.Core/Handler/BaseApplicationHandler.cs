using System;
using System.Drawing;

namespace GGM.GFMarcro.Core.Handler
{
    public abstract class BaseApplicationHandler : IApplicationHandler
    {
        public abstract string ApplicationName { get; }
        public abstract IntPtr ApplicationWindow { get; }
        public abstract IntPtr TargetHandle { get; }

        public virtual Bitmap GetScreenImage()
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

        public virtual void SendClick(Point point)
        {
            var lparam = new IntPtr(point.X | (point.Y << 16));
            Kernel.User32.SendMessage(TargetHandle, Kernel.User32.MouseEventFlag.WM_LBUTTONDOWN, 1, lparam);
            Kernel.User32.SendMessage(TargetHandle, Kernel.User32.MouseEventFlag.WM_LBUTTONUP, 0, lparam);
        }
    }
}