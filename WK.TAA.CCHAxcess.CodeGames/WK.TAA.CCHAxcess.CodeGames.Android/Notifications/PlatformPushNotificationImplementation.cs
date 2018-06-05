using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using WK.TAA.CCHAxcess.CodeGames.Notifications;

namespace WK.TAA.CCHAxcess.CodeGames.Droid.Notifications
{
    public class PlatformPushNotificationImplementation
    {
        public static AzurePushNotificationsImplementation Platform
        {
            get
            {
                var ret = CrossAzurePushNotifications.Current;
                if (ret == null)
                {
                    throw NotImplementedInReferenceAssembly();
                }
                return (AzurePushNotificationsImplementation)ret;
            }
        }

        private static Exception NotImplementedInReferenceAssembly()
        {
            return
                new NotImplementedException();
        }
    }
}