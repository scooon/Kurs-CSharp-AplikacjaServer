using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


/*
 * 
 * tapeta pobieranie do program data
 * dodawanie slowa
 * kontrolki
 * obracanie
 * przeniesienie paska narzedzi
 * wyłączenie komputera
 * wyciszanie dźwięku
 * sprawdzanie czy okno nie zostało zamknięte
 * automatyczny autostart
 */

namespace WinSrv
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Funkcje.fire(19, 30);
            

            Server.listener = new System.Net.HttpListener();
            Server.listener.Prefixes.Add(Server.url);
            Server.listener.Start();

            Console.WriteLine("Nasłuchiwanie adresu {0}", Server.url);

            Task listen = Server.HandleIncomingConnections();

            Console.ReadLine();
        }
    }
}
