using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IkEvil.Notifications
{
    public class ReadyNotification : INotification
    {
        public static readonly ReadyNotification Default
            = new();

        private ReadyNotification()
        {
        }
    }
}
