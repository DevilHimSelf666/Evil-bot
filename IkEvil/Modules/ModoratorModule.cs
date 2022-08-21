using Discord;
using Discord.Interactions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
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
            IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(messageCount + 1).FlattenAsync();
            await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages);
            const int delay = 3000;
            IUserMessage m = await ReplyAsync($"I have deleted {messageCount} messages for ya. :)");
            await Task.Delay(delay);
            await m.DeleteAsync();
        }

        [SlashCommand("reaction-role", "choose your role")]
        public async Task ReactionRole()
        {

            var menuBuilder = new SelectMenuBuilder()
           .WithPlaceholder("Select an option")
           .WithCustomId("menu-1")
           .WithMinValues(1)
           .WithMaxValues(1);


            foreach (var role in Context.Guild.Roles)
                menuBuilder.AddOption(role.Name, role.Name);


            var builder = new ComponentBuilder()
                .WithSelectMenu(menuBuilder);

            await RespondAsync("Whos really lying?", components: builder.Build());
            return;
        }


       

    }
}

