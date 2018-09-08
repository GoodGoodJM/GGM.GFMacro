using GGM.GFMarcro.Forms;
using System;
using System.Windows.Forms;

namespace GGM.GFMarcro
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
