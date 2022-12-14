using Discord;
using Discord.Interactions;
using Handlres.PingCommand;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace IkEvil.Modules
{
    public class ModeratorModule : InteractionModuleBase<SocketInteractionContext>
    {

        private readonly IConfiguration _configuration;
        private readonly IMediator mediator;

        public ModeratorModule(IConfiguration configuration, IMediator mediator)
        {
            _configuration = configuration;
            this.mediator = mediator;
        }

        [SlashCommand("ping", "Pings the bot and returns its latency.")]
        public async Task Ping()
        {

            var pong = await mediator.Send(new PingCommand() { Latency = Context.Client.Latency });
            await RespondAsync(text: pong.Message, ephemeral: true);
        }


        [SlashCommand("delete", "Delete Messages.")]
        public async Task DeleteMessages(int messageCount = 1)
        {
            IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(messageCount + 1).FlattenAsync();
            await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages);
            await RespondAsync($"I have deleted {messageCount} messages for ya. :)", ephemeral: true);

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

            await RespondAsync("Chose your Role?", components: builder.Build());
            return;
        }


        [SlashCommand("invite-link", "Get invite Link")]
        public async Task GetInviteLink()
        {
            var botSetting = _configuration.GetSection("BotSettings").Get<BotSetting>();
            await RespondAsync($"https://discordapp.com/oauth2/authorize?&client_id={botSetting.ApplicationId}&scope=bot&permissions={botSetting.Permissions}");
            return;
        }




    }
}

