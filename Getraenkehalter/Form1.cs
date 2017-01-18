using System;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace Getraenkehalter
{

    public partial class Form1 : Form
    {


        public string GetDrive()
        {
           var drives = DriveInfo.GetDrives();

            foreach (var d in drives)
            {
                if (d.DriveType == DriveType.CDRom)
                {
                    return d.Name;
                }
            }
            return "noDrive";          
        }

        [DllImport("winmm.dll", EntryPoint = "mciSendStringA")]
        public static extern void mciSendStringA(string lpstrCommand, string lpstrReturnString, long uReturnLength, long hwndCallback);
        string callback = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string driveLetter = GetDrive();
            
            if (driveLetter == "noDrive") {
                button1.Text = "No Drive found";
            }
            else
            {
                mciSendStringA("open " + driveLetter + ": type CDaudio alias drive" + driveLetter, callback, 127, 0);
                mciSendStringA("set drive" + driveLetter + " door open", callback, 0, 0);
            }

        }
    }
}
