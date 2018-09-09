using GGM.GFMarcro.Core.Handler;
using GGM.GFMarcro.Core.Recognition;
using GGM.GFMarcro.Core.Util;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace GGM.GFMarcro.Forms
{
    public partial class MainForm : Form
    {
        private const string DEFAULT_PATH = @"C:\Users\허준무\work\macro_image";
        private const int TIMER_INTERVAL = 1500;
        private const double THRESH_HOLD = 0.8d;
        private static readonly string[] TARGET_PATHS = {
            GetFilePath("wait_panel.PNG"),
            GetFilePath("retry_button.PNG")
        };
        private static readonly TimeZoneInfo KOREAN_TIME_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time");

        IApplicationHandler _applicationHandler = new MomoHandler("[MOMO]앱플레이어-1");
        IRecognition _recognition = new OpenCVRecognition();
        Timer _timer;
        Random _random = new Random();

        public MainForm()
        {
            InitializeComponent();
        }

        private bool Execute(Bitmap screenImage, Bitmap targetImage)
        {
            try
            {
                pictureBox1.Size = screenImage.Size;
                var result = _recognition.SearchImage(screenImage, targetImage);
                pictureBox1.Image = screenImage;
                if (result.MaximumSimilarity < THRESH_HOLD)
                    return false;

                var clickPosition = result.MatchedPoint.GetRandomizedPoint(targetImage.Size);
                Console.WriteLine(JsonConvert.SerializeObject(result));

                using (var screenImageGraphics = Graphics.FromImage(screenImage))
                {
                    screenImageGraphics.DrawRectangle(Pens.Red, new Rectangle(result.MatchedPoint, targetImage.Size));
                    screenImageGraphics.DrawRectangle(Pens.Blue, new Rectangle(clickPosition, new Size(3, 3)));
                }
                screenImage.Save(GetFilePath($"result/{DateTime.Now.Ticks}.PNG"), ImageFormat.Png);
                _applicationHandler.SendClick(clickPosition);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }

        private void ExecuteEvent(object sender, EventArgs e)
        {
            Console.WriteLine($"Invoked ExecuteEvent! Time : {TimeZoneInfo.ConvertTime(DateTime.Now, KOREAN_TIME_ZONE)}");
            try
            {
                var screenImage = _applicationHandler.GetScreenImage();
                foreach (var targetPath in TARGET_PATHS)
                {
                    if (Execute(screenImage, new Bitmap(targetPath)))
                    {
                        Console.WriteLine($"Find target : {targetPath}");
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void StartEvent(object sender, EventArgs e)
        {
            Console.WriteLine("Start timer.");
            if (_timer != null)
                StopTimer(_timer);
            _timer = new Timer();
            _timer.Tick += new EventHandler(TickEvent);
            _timer.Interval = TIMER_INTERVAL;
            _timer.Start();
        }

        private void StopEvent(object sender, EventArgs e)
        {
            Console.WriteLine("Stop timer.");
            StopTimer(_timer);
            _timer = null;
        }

        private void TickEvent(object sender, EventArgs e)
        {
            ExecuteEvent(null, null);
            _timer.Interval = _random.Next(1500, 3000);
        }

        private static void StopTimer(Timer timer)
        {
            if (timer == null)
                return;
            timer.Stop();
            timer.Dispose();
        }

        private static string GetFilePath(string self) => Path.Combine(DEFAULT_PATH, self);
    }
}