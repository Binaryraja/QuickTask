using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WK.TAA.CCHAxcess.CodeGames.Models;
using WK.TAA.CCHAxcess.CodeGames.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(MsgService))]
namespace WK.TAA.CCHAxcess.CodeGames.Services
{
    public class MsgService : IMsgService
    {
        HttpClient serviceClient;

        public MsgService()
        {
            serviceClient = new HttpClient();
            serviceClient.MaxResponseContentBufferSize = 256000;
        }

        /// <summary>
        /// Registers the specified base URI.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="registrationId">The registration identifier.</param>
        /// <param name="deviceHandle">The device handle.</param>
        /// <param name="platformType">Type of the platform.</param>
        /// <param name="userGuid">The user unique identifier.</param>
        /// <returns></returns>
        public async Task<string> Register(string baseUri, string registrationId, string deviceHandle, string platformType, string userGuid)
        {
            string validRegistrationId = string.Empty;
            try
            {
                MSGPNSRegistrationRequest requestObject = new MSGPNSRegistrationRequest(registrationId, deviceHandle, platformType, userGuid);
                var uri = new Uri(baseUri + string.Format("/api/messaging/notifications/register"));
                HttpContent requestContent = new StringContent(JsonConvert.SerializeObject(requestObject).ToString(), Encoding.UTF8, "application/json");
                HttpResponseMessage responseObject = await serviceClient.PostAsync(uri, requestContent);
                responseObject.EnsureSuccessStatusCode();
                validRegistrationId = JsonConvert.DeserializeObject<string>(responseObject.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
            return validRegistrationId;
        }

        /// <summary>
        /// Unregisters the specified base URI.
        /// </summary>
        /// <param name="baseUri">The base URI.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task Unregister(string baseUri, string registrationId)
        {
            try
            {
                var uri = new Uri(baseUri + string.Format("/api/messaging/notifications/unregister?registrationId={0}", registrationId));
                HttpResponseMessage responseObject = await serviceClient.DeleteAsync(uri);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }

        public async Task UnregisterAll(string baseUri, string pnsHandle)
        {
            try
            {
                var uri = new Uri(baseUri + string.Format("/api/messaging/notifications/unregisterAll?pnsHandle={0}", pnsHandle));
                HttpResponseMessage responseObject = await serviceClient.DeleteAsync(uri);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }
        }
    }
}
