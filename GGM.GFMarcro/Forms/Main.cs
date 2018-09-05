using GGM.GFMarcro.Core.Handler;
using GGM.GFMarcro.Core.Handler.Exception;
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
            }
            catch (ApplicationHandlerException ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
