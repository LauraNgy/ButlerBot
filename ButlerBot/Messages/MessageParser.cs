using DSharpPlus;
using DSharpPlus.EventArgs;

namespace ButlerBot.Messages
{
    public static class MessageParser
    {
        public static MessageParsingResult Parse(DiscordClient client, MessageCreateEventArgs ev)
        {
            if (ev.Message.Content.ToLower().Equals("ping"))
            {
                MessageParsingResult res = new MessageParsingResult(client, ev, ParsingResultType.Ping);
                return res;
            }


            return new MessageParsingResult(client, ev, ParsingResultType.Unknown);
        }
    }
}
