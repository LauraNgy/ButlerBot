using DSharpPlus;
using DSharpPlus.EventArgs;

namespace ButlerBot.Messages
{
    public class MessageParsingResult
    {
        public ParsingResultType ResultType { get; private set; }
        public DiscordClient Client { get; private set; }
        public MessageCreateEventArgs Message { get; private set; }

        public MessageParsingResult(DiscordClient client, MessageCreateEventArgs msg, ParsingResultType resultType)
        {
            Client = client;
            Message = msg;
            ResultType = resultType;
        }
    }
}
