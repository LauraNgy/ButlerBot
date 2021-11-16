using ButlerBot.Messages;
using ButlerBot.Utilities;
using DSharpPlus;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ButlerBot
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            EncryptionHelper.ProtectCustomConfig("DiscordClientConfig");
            EncryptionHelper.ProtectCustomConfig("DbConfig");

            Database db = new Database();

            string discordClientToken = AppConfigHelper.GetSetting<string>("DiscordClientConfig", "token");


            var client = new DiscordClient(new DiscordConfiguration()
            {
                Token = discordClientToken,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.AllUnprivileged
            });

            new DiscordConfiguration()
            {
                MinimumLogLevel = LogLevel.Debug,
                //LogTimestampFormat = "MMM dd yyyy - hh:mm:ss tt"
            };

            client.MessageCreated += async (s, e) =>
            {
                await db.Client.Document.PostDocumentAsync("Messages", e.Message);

                var parseResult = MessageParser.Parse(s, e);
                
                //await ActOnParseResult(parseResult);

                if (e.Message.Content.ToLower().Equals("ping"))
                {
                    await e.Message.RespondAsync("pong!");
                }
            };

            client.MessageCreated += (s, e) =>
            {
                _ = Task.Run(async () =>
                {
                    if (e.Message.Content.ToLower().StartsWith("ping 10000"))
                    {
                        Thread.Sleep(10000);
                        await e.Message.RespondAsync("pong!");
                    }
                });

                return Task.CompletedTask;
            };


            await client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
