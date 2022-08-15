using Discord.WebSocket;
using IkEvil.Notifications;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkEvil
{
    public class DiscordEventListener
    {
        private readonly CancellationToken _cancellationToken;
        private readonly DiscordSocketClient _client;
        private readonly IServiceScopeFactory _serviceScope;
        public DiscordEventListener(DiscordSocketClient client, IServiceScopeFactory serviceScope)
        {
            _client = client;
            _serviceScope = serviceScope;
            _cancellationToken = new CancellationTokenSource().Token;
        }
        private IMediator Mediator
        {
            get
            {
                var scope = _serviceScope.CreateScope();
                return scope.ServiceProvider.GetRequiredService<IMediator>();
            }
        }

        public Task StartAsync()
        {
            _client.Ready += OnReadyAsync;
            _client.MessageReceived += OnMessageReceivedAsync;

            return Task.CompletedTask;
        }
        private Task OnMessageReceivedAsync(SocketMessage arg)
        {
            return Mediator.Publish(new MessageReceivedNotification(arg), _cancellationToken);
        }

        private Task OnReadyAsync()
        {
            return Mediator.Publish(ReadyNotification.Default, _cancellationToken);
        }
    }
}
