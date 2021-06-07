using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace WinSrv
{
    class Telegram
    {

        static ITelegramBotClient botClient;

        public Telegram(string apiKey){
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
                      text: doThings(e.Message.Text)
                    );
                }
            }
        }

        private static string doThings(string command)
        {
            string cmd = command.ToLower();
            string reply = "";
            if (cmd.Contains("open cd"))
            {
                Funkcje.openCD();
                reply += "Otworzono napęd! ";
            }
            if(reply == "")
            {
                reply = "Zła komenda! (" + cmd + ")";
            }
            return reply;

        }
    }
}
