using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WinSrv
{
    static class Server
    {
        public static HttpListener listener;
        public static string url = "http://localhost:8000/";
        public static int pageViews = 0, requestCount = 0;
        public static string pageData =
    "<!DOCTYPE>" +
    "<html>" +
    "  <head>" +
    "    <title>Server</title>" +
    "  </head>" +
    "  <body>" +
    "    <p>Server</p>" + 
    "    <p>Wyświetlenia: {0}</p>" +
    "    <p>Nazwa hosta: {1}</p>" +
    "    <form method=\"post\" action=\"shutdown\">" +
    "      <input type=\"submit\" value=\"Wyłącz\" {1}>" +
    "    </form>" +
    "  </body>" +
    "</html>";


        public static async Task HandleIncomingConnections()
        {
            bool runServer = true;

            while (runServer)
            {
                // Oczekiwanie na połączenie
                HttpListenerContext ctx = await listener.GetContextAsync();

                // Obiekty Request i Response
                HttpListenerRequest req = ctx.Request;
                HttpListenerResponse resp = ctx.Response;

                // Info o połączeniu w konsoli
                Console.WriteLine("Request #: {0}", ++requestCount);
                Console.WriteLine(req.Url.ToString());
                Console.WriteLine(req.HttpMethod);
                Console.WriteLine(req.UserHostName);
                Console.WriteLine(req.UserAgent);
                Console.WriteLine();

                if ((req.HttpMethod == "POST") && (req.Url.AbsolutePath == "/shutdown"))
                {
                    Console.WriteLine("Zażądano wyłączenia");
                    runServer = false;
                }

                if (req.Url.AbsolutePath != "/favicon.ico")
                    pageViews += 1;

                // Wyślij odpowiedź
                string disableSubmit = !runServer ? "disabled" : "";
                byte[] data = Encoding.UTF8.GetBytes(String.Format(pageData, pageViews, Dns.GetHostName(), disableSubmit));
                resp.ContentType = "text/html";
                resp.ContentEncoding = Encoding.UTF8;
                resp.ContentLength64 = data.LongLength;
                
                await resp.OutputStream.WriteAsync(data, 0, data.Length);
                resp.Close();
            }
        }
    }
}
