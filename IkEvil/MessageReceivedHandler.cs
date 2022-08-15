using IkEvil.Notifications;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkEvil
{
    public class MessageReceivedHandler : INotificationHandler<MessageReceivedNotification>
    {
        public async Task Handle(MessageReceivedNotification notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"MediatR works! (Received a message by {notification.Message.Author.Username})");
        }
    }
}
