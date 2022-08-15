using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Discord.WebSocket;
using Discord;
using IkEvil;
using Discord.Interactions;

public class Bot
{
    public static Task Main(string[] args) => new Bot().MainAsync();

    private static ServiceProvider ConfigureServices()
    {
        return new ServiceCollection()
            
            .AddMediatR(typeof(Bot))
            .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100,
                GatewayIntents = GatewayIntents.AllUnprivileged,
                LogLevel = LogSeverity.Info
            }))
            .AddSingleton<DiscordEventListener>()
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            .BuildServiceProvider();
    }

    public async Task MainAsync()
    {
    }
}