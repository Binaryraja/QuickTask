using System;

using Android.App;
using Android.Content;
using Android.Gms.Iid;
using Android.Gms.Gcm;
using Android.Support.V4.Content;
using WK.TAA.CCHAxcess.CodeGames.Helpers;
using Android.Util;
using WK.TAA.CCHAxcess.CodeGames.Notifications;

namespace WK.TAA.CCHAxcess.CodeGames.Droid.Notifications
{
    [Service]
    public class PNRegistrationService : IntentService
    {
        public PNRegistrationService() : base("RegistrationIntentService")
        {
        }

        protected override void OnHandleIntent(Intent intent)
        {
            try
            {
                lock (this)
                {
                    var instanceID = InstanceID.GetInstance(this);
                    //instanceID.DeleteInstanceID();
                    var _deviceToken = instanceID.GetToken(PushNotificationCredentials.GoogleApiSenderId, GoogleCloudMessaging.InstanceIdScope, null);
                    Settings.Current.DeviceToken = _deviceToken;
                    Settings.Current.AttemptedPush = true;
                }
                if (intent.Extras != null)
                {
                    //Only do this when the service is started from a WakefulBroadcastReceiver
                    WakefulBroadcastReceiver.CompleteWakefulIntent(intent);
                }
            }
            catch(Exception ex)
            {
                Log.Info(AppConstants.TAG,ex.Message);
            }
        }

    }
}