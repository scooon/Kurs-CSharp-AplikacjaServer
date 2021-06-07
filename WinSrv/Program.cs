using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


/*
 * 
 * kontrolki
 * wyciszanie dźwięku
 * integracja z telegramem
 */

namespace WinSrv
{
    class Program
    {

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();


        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);


        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        

        static void Main(string[] args)
        {
            Telegram bot = new Telegram("1812366377:AAH3P0aiiRl_eVmO_r5KOd6eORrskC05oM0");
            bot.helloWorld();

            var handle = GetConsoleWindow();


            // Hide
            //ShowWindow(handle, SW_HIDE);


            // Show
            ShowWindow(handle, SW_SHOW);

            Funkcje.muteSound();

            //Funkcje.SetStartup(false);

            Funkcje.fire(19, 30);
            

            Server.listener = new System.Net.HttpListener();
            Server.listener.Prefixes.Add(Server.url);
            Server.listener.Start();

            Console.WriteLine("Nasłuchiwanie adresu {0}", Server.url);

            Task listen = Server.HandleIncomingConnections();

            Console.ReadLine();

            bot.telegramStop();

            Display.Rotate(1, Display.Orientations.DEGREES_CW_0);
        }
    }
}
