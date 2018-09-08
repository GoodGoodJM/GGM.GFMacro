using System;
using System.Drawing;

namespace GGM.GFMarcro.Core.Util
{
    public static class ClickUtils
    {
        private static readonly Random _rootRandom = new Random();
        public static Point GetRandomizedPoint(this Point self, Size size)
        {
            var random = new Random(_rootRandom.Next(0, int.MaxValue));
            var x = random.Next(self.X, self.X + size.Width);
            var y = random.Next(self.Y, self.Y + size.Height);
            return new Point(x, y);
        }
    }
}
