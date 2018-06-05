using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WK.TAA.CCHAxcess.CodeGames.Notifications
{
    public class PushNoticationMessage
    {
        public string Type { get; set; }

        public string Title { get; set; }

        public string DetailedMessage { get; set; }

        public string Environment { get; set; }

        public string Agent { get; set; }

        public string IPAddress { get; set; }

        public string ActionUri { get; set; }

        public string UserGuid { get; set; }
    }
}
