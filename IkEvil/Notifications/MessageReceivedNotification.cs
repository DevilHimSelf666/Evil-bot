using Discord.WebSocket;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkEvil.Notifications
{
    public class MessageReceivedNotification : INotification
    {
        public MessageReceivedNotification(SocketMessage message)
        {
            Message = message ?? throw new ArgumentNullException(nameof(message));
        }

        public SocketMessage Message { get; }
    }
}
