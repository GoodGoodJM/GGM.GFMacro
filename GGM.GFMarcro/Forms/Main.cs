using GGM.GFMarcro.Core.Handler;
using GGM.GFMarcro.Core.Handler.Exception;
using GGM.GFMarcro.Core.Recognition;
using GGM.GFMarcro.Core.Recognition.Exception;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GGM.GFMarcro.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                IApplicationHandler applicationHandler = new NoxHandler("NoxPlayer_62001");
                Console.WriteLine($"Success to create ApplicationHandler. MainHandle : {applicationHandler.ApplicationWindow}");
                pictureBox1.Image = applicationHandler.GetScreenImage();
                Console.WriteLine($"TEST : {pictureBox1.Image.Width} : {pictureBox1.Image.Height}");
                var targetPoint = new Point(153, 291);
                applicationHandler.SendClick(targetPoint);

                var result = new OpenCVRecognition().SearchImage(null, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
