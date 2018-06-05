using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WK.TAA.CCHAxcess.CodeGames.Services
{
    public interface IMsgService
    {
        Task<string> Register(string baseUri, string registrationId, string deviceHandle, string platformType, string userGuid);

        Task Unregister(string baseUri, string registrationId);

        Task UnregisterAll(string baseUri, string pnsHandle);

    }
}
