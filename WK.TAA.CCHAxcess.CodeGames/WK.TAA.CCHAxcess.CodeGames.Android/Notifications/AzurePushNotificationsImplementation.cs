using System;
using WK.TAA.CCHAxcess.CodeGames.Notifications;
using Xamarin.Forms;
using WK.TAA.CCHAxcess.CodeGames.Droid.Notifications;
using WK.TAA.CCHAxcess.CodeGames.Helpers;

using Android.App;
using System.Threading.Tasks;

[assembly: Dependency(typeof(AzurePushNotificationsImplementation))]
namespace WK.TAA.CCHAxcess.CodeGames.Droid.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="WK.TAA.Mfa.OTPAuthenticator.Common.Notifications.IRemoteNotifications" />
    public class AzurePushNotificationsImplementation : IRemoteNotifications
    {
        public event EventHandler<ReceivedMessageEventArgs> OnMessageReceived;
        public event EventHandler<ReceivedMessageEventArgs> OnMessageOpened;

        public static Activity MainActivity;

        public static void Init(Activity contextObject)
        {
            MainActivity = contextObject;
        }

        public bool IsRegistered
        {
            get
            {
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.N)
                {
                    return NotificationManager.FromContext(MainActivity).AreNotificationsEnabled();
                }
                return NotificationHelper.IsNotificationEnabled(MainActivity);
            }
        }

        public void PushNotificationReceived(string content, object rawContent)
        {
            var conent = new ReceivedMessageEventArgs(content, rawContent);
            var message = OnMessageReceived;
            message?.Invoke(null, conent);
        }

        public void PushNotificationOpened(string recdMessage)
        {
            var conent = new ReceivedMessageEventArgs(recdMessage, recdMessage);
            var message = OnMessageOpened;
            message?.Invoke(null, conent);
        }

        public Task<bool> RegisterForNotifications()
        {
            return Task.FromResult(true);
        }
    }
}