using Discord;
using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkEvil.Modules
{
    public class ModoratorModule : InteractionModuleBase<SocketInteractionContext>
    {


        [SlashCommand("ping", "Pings the bot and returns its latency.")]
        public async Task Ping()
             => await RespondAsync(text: $":ping_pong: It took me {Context.Client.Latency}ms to respond to you!", ephemeral: true);


        [SlashCommand("delete", "Delete Messages.")]
        public async Task DeleteMessages(int messageCount = 1)
        {
            var messages = await Context.Channel.GetMessagesAsync(messageCount).FirstAsync();
            foreach (var msg in messages)
            {
                await Context.Channel.DeleteMessageAsync(msg.Id);
            }
            await RespondAsync(text: $"Last {messageCount} messages has been deleted", ephemeral: true);
        }

        [SlashCommand("delete-all", "Delete all Messages in the channel")]
        public async Task DeleteAllMessages()
        {
            var hasMessage = true;
            while (hasMessage)
            {
                var messages = await Context.Channel.GetMessagesAsync(100).FirstAsync();
                if (messages.Count == 0)
                    hasMessage = false;
                else
                    foreach (var msg in messages)
                        await Context.Channel.DeleteMessageAsync(msg.Id);

            }

            await RespondAsync(text: $"All Last messages has been deleted", ephemeral: true);
        }

    }
}
