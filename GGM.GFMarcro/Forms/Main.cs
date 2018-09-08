using GGM.GFMarcro.Core.Handler;
using GGM.GFMarcro.Core.Handler.Exception;
using GGM.GFMarcro.Core.Recognition;
using GGM.GFMarcro.Core.Recognition.Exception;
using GGM.GFMarcro.Core.Util;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GGM.GFMarcro.Forms
{
    public partial class MainForm : Form
    {
        private const string RESULT_PATH = @"C:\Users\허준무\work\macro_image";
        IApplicationHandler _applicationHandler = new MomoHandler("[MOMO]앱플레이어-1");

        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var screenImage = _applicationHandler.GetScreenImage();
            screenImage.Save(Path.Combine(RESULT_PATH, "result_image.PNG"));
            pictureBox1.Image = screenImage;
            Console.WriteLine($"TEST : {pictureBox1.Image.Width} : {pictureBox1.Image.Height}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var screenImage = _applicationHandler.GetScreenImage();
                pictureBox1.Size = screenImage.Size;
                var targetImage = new Bitmap(Clipboard.GetImage());
                var result = new OpenCVRecognition().SearchImage(screenImage, targetImage);
                Console.WriteLine(JsonConvert.SerializeObject(result));
                using (var screenImageGraphics = Graphics.FromImage(screenImage))
                {
                    screenImageGraphics.DrawRectangle(Pens.Red, new Rectangle(result.MatchedPoint, targetImage.Size));
                    screenImageGraphics.DrawRectangle(Pens.Blue, new Rectangle(result.MatchedPoint.GetRandomizedPoint(targetImage.Size), new Size(3, 3)));
                }
                pictureBox1.Image = screenImage;

                _applicationHandler.SendClick(result.MatchedPoint);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
