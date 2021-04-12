using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSrv
{
    class Program
    {
        static void Main(string[] args)
        {
            Server.listener = new System.Net.HttpListener();
            Server.listener.Prefixes.Add(Server.url);
            Server.listener.Start();

            Console.WriteLine("Nasłuchiwanie adresu {0}", Server.url);

            Task listen = Server.HandleIncomingConnections();

            Console.ReadLine();
        }
    }
}
