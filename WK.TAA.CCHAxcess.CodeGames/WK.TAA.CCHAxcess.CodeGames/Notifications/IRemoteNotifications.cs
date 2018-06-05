using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WK.TAA.CCHAxcess.CodeGames.Notifications
{
    public interface IRemoteNotifications
    {
        Task<bool> RegisterForNotifications();

        bool IsRegistered { get; }

        event EventHandler<ReceivedMessageEventArgs> OnMessageReceived;

        event EventHandler<ReceivedMessageEventArgs> OnMessageOpened;
    }
}

