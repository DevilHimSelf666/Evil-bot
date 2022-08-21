using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using IkEvil.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IkEvil
{
    public class DiscordEventListener
    {
        private readonly CancellationToken _cancellationToken;
        private readonly DiscordSocketClient _client;
        private readonly IServiceProvider _services;
        private readonly InteractionService _handler;
        public DiscordEventListener(DiscordSocketClient client, InteractionService handler, IServiceProvider services)
        {
            _client = client;
            _services = services;
            _handler = handler;
            _cancellationToken = new CancellationTokenSource().Token;
        }

        public async Task StartAsync()
        {
            _client.Ready += ReadyAsync;
            _client.InteractionCreated += HandleInteraction;
            _client.SelectMenuExecuted += _client_SelectMenuExecuted;
            await _handler.AddModulesAsync(Assembly.GetEntryAssembly(), _services);

        }

        private async Task _client_SelectMenuExecuted(SocketMessageComponent arg)
        {
            switch (arg.Data.CustomId)
            {
                case "menu-1":
                    await SetEmoji(arg);
                    break;
            }

        }
        public async Task SetEmoji(SocketMessageComponent arg)
        {
            var menuBuilder = new SelectMenuBuilder()
         .WithPlaceholder("Select the emoji")
         .WithCustomId("menu-2")
         .WithMinValues(1)
         .WithMaxValues(1)
         .AddOption("red", "\U0001F534")
         .AddOption("blue ", "\U0001F535")
         .AddOption("orange ", "\U0001F536");




            var builder = new ComponentBuilder()
                .WithSelectMenu(menuBuilder);

            await arg.RespondAsync($"choise the emoji for role {arg.Data.Values.First()}?", components: builder.Build());
            return;
        }

        private async Task ReadyAsync()
        {
            // Context & Slash commands can be automatically registered, but this process needs to happen after the client enters the READY state.
            // Since Global Commands take around 1 hour to register, we should use a test guild to instantly update and test our commands.
            await _handler.RegisterCommandsGloballyAsync(true);
        }

        private async Task HandleInteraction(SocketInteraction interaction)
        {
            try
            {
                // Create an execution context that matches the generic type parameter of your InteractionModuleBase<T> modules.
                var context = new SocketInteractionContext(_client, interaction);
                var SocketCommandBase = ((Discord.WebSocket.SocketCommandBase)interaction);
                Log.Write(Serilog.Events.LogEventLevel.Information, "[{Source}] {Type} {Message}", interaction.User.Username, SocketCommandBase.CommandName, SocketCommandBase.Type.ToString());
                // Execute the incoming command.
                var result = await _handler.ExecuteCommandAsync(context, _services);

                if (!result.IsSuccess)
                    switch (result.Error)
                    {
                        case InteractionCommandError.UnmetPrecondition:
                            // implement
                            break;
                        default:
                            break;
                    }
            }
            catch
            {
                // If Slash Command execution fails it is most likely that the original interaction acknowledgement will persist. It is a good idea to delete the original
                // response, or at least let the user know that something went wrong during the command execution.
                if (interaction.Type is InteractionType.ApplicationCommand)
                    await interaction.GetOriginalResponseAsync().ContinueWith(async (msg) => await msg.Result.DeleteAsync());
            }
        }

    }
}
