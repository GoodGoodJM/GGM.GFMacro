using System;
using System.Drawing;

namespace GGM.GFMarcro.Core.Handler
{
    public interface IApplicationHandler
    {
        string ApplicationName { get; }

        IntPtr ApplicationWindow { get; }
        Bitmap GetScreenImage();
        void SendClick(Point point);
    }
}
