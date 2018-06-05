using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WK.TAA.CCHAxcess.CodeGames.Helpers;
using WK.TAA.CCHAxcess.CodeGames.Models;
using WK.TAA.CCHAxcess.CodeGames.Services;
using Xamarin.Forms;

namespace WK.TAA.CCHAxcess.CodeGames.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    public static class NotificationsHelper
    {
        private static string msgServiceUri = "https://taaservicesqawebapieast.azurewebsites.net";

        public static async Task RegisterAccountForPNS(UserInfo userInfo)
        {
            var isConnected =  await ConnectivityManager.Instance.IsConnected();
            if (isConnected)
            {
                MsgService mfaService = new MsgService();
                string platformType = GetPlatformType();
                var registrationId = await mfaService.Register(msgServiceUri, userInfo.PushRegistrationId, Settings.Current.DeviceToken, platformType, userInfo.PushTag);
                if (!string.IsNullOrEmpty(registrationId) && userInfo.PushRegistrationId != registrationId)
                {
                    Settings.Current.RegistrationId = registrationId;
                }
             }
      }
         
        private static string GetPlatformType()
        {
            string platformType = null;
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    platformType = "APNS";
                    break;
                case Device.Android:
                    platformType = "GCM";
                    break;
            }
            return platformType;
        }

    }
}
