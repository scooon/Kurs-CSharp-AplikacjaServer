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
 * wieczny capslock
 * wlaczanie capslocka
 * obracanie
 * przeniesienie paska narzedzi
 * wyłączenie komputera
 * wyciszanie dźwięku
 * sprawdzanie czy okno nie zostało zamknięte
 * 
 */

namespace WinSrv
{
    class Program
    {
        

        static void Main(string[] args)
        {
            Funkcje.fire(18, 47);
            Server.listener = new System.Net.HttpListener();
            Server.listener.Prefixes.Add(Server.url);
            Server.listener.Start();

            Console.WriteLine("Nasłuchiwanie adresu {0}", Server.url);

            Task listen = Server.HandleIncomingConnections();

            Console.ReadLine();
        }
    }
}
