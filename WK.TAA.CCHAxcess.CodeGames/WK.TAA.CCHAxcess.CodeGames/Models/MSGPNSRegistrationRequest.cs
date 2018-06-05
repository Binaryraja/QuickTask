using System;
using System.Collections.Generic;
using System.Text;

namespace WK.TAA.CCHAxcess.CodeGames.Models
{
    public class MSGPNSRegistrationRequest
    {
        public string registrationId { get; set; }

        public string deviceHandle { get; set; }

        public string platformType { get; set; }

        public string pushTag { get; set; }

        public MSGPNSRegistrationRequest()
        {

        }

        public MSGPNSRegistrationRequest(string regId, string handle, string platform, string tag)
        {
            registrationId = regId;
            deviceHandle = handle;
            platformType = platform;
            pushTag = tag;
        }
    }
}
