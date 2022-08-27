using Microsoft.Extensions.DependencyInjection;
using Discord.WebSocket;
using Discord;
using IkEvil;
using Discord.Interactions;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;


public class Bot
{

    private readonly IConfiguration _configuration;
    public Bot()
    {
        _configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
              .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true)
              .Build();
    }
    private  ServiceProvider ConfigureServices()
    {
        var provider = new ServiceCollection()
            .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                AlwaysDownloadUsers = true,
                MessageCacheSize = 100,
                GatewayIntents = GatewayIntents.AllUnprivileged,
                LogLevel = LogSeverity.Info
            }))
            .AddSingleton<DiscordEventListener>()
            .AddSingleton(_configuration)
            .AddSingleton(x => new InteractionService(x.GetRequiredService<DiscordSocketClient>()))
            .AddInfrastructureServices()
            .BuildServiceProvider();
        
        return provider;

    }

    public static async Task Main()
    {
        
        await new Bot().RunAsync();
    }

  
  


    private async Task RunAsync()
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .CreateLogger();

        await using var services = ConfigureServices();
        
        var client = services.GetRequiredService<DiscordSocketClient>();
        client.Log += LogAsync;

        var listener = services.GetRequiredService<DiscordEventListener>();
        await listener.StartAsync();
        var botSetting = _configuration.GetSection("BotSettings").Get<BotSetting>();
        await client.LoginAsync(TokenType.Bot, botSetting.Token);
        await client.StartAsync();
        await Task.Delay(Timeout.Infinite);
    }

    private static Task LogAsync(LogMessage message)
    {
        var severity = message.Severity switch
        {
            LogSeverity.Critical => LogEventLevel.Fatal,
            LogSeverity.Error => LogEventLevel.Error,
            LogSeverity.Warning => LogEventLevel.Warning,
            LogSeverity.Info => LogEventLevel.Information,
            LogSeverity.Verbose => LogEventLevel.Verbose,
            LogSeverity.Debug => LogEventLevel.Debug,
            _ => LogEventLevel.Information
        };

        Log.Write(severity, message.Exception, "[{Source}] {Message}", message.Source, message.Message);

        return Task.CompletedTask;
    }

    public static bool IsDebug()
    {
#if DEBUG
        return true;
#else
                return false;
#endif
    }

}