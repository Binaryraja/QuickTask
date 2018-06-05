using System;
using System.Collections.Generic;
using System.Text;
using WindowsAzure.Messaging;
using Android.App;
using Android.Content;
using Android.Util;
using Gcm.Client;
using WK.TAA.Mfa.OTPAuthenticator.Common.Notifications;
using Newtonsoft.Json;
using Android.Graphics;
using Android.OS;

namespace WK.TAA.Mfa.OTPAuthenticator.Droid.Notifications
{
    [Service] // Must use the service tag
    public class PushHandlerService : GcmServiceBase
    {
        public static string RegistrationID { get; private set; }

        private static NotificationHub Hub { get; set; }

        public static Context Context;

        public PushHandlerService() : base(PushNotificationCredentials.GoogleApiSenderId)
        {
            Log.Info(AzurePushBroadcastReceiver.TAG, "PushHandlerService() constructor");
        }

        protected override void OnRegistered(Context context, string registrationId)
        {
            Log.Verbose(AzurePushBroadcastReceiver.TAG, "GCM Registered: " + registrationId);
            RegistrationID = registrationId;
            Hub = new NotificationHub(PushNotificationCredentials.AzureNotificationHubName, PushNotificationCredentials.AzureListenConnectionString,context);
            try
            {
                Hub.UnregisterAll(registrationId);
            }
            catch (Exception ex)
            {
                Log.Error(AzurePushBroadcastReceiver.TAG, ex.Message);
            }
            var tags = new List<string>() { };
            try
            {
                var hubRegistration = Hub.Register(registrationId, tags.ToArray());
            }
            catch (Exception ex)
            {
                Log.Error(AzurePushBroadcastReceiver.TAG, ex.Message);
            }
        }

        protected override void OnMessage(Context context, Intent intent)
        {
            //PowerManager.WakeLock sWakeLock;
            //var pm = PowerManager.FromContext(context);
            //sWakeLock = pm.NewWakeLock(WakeLockFlags.Partial, "GCM Broadcast Reciever Tag");
            //sWakeLock.Acquire();
            var msg = new StringBuilder();
            if (intent != null && intent.Extras != null)
            {
                foreach (var key in intent.Extras.KeySet())
                    msg.AppendLine(key + "=" + intent.Extras.Get(key));
                string messageText = intent.Extras.GetString("message");
                if (!string.IsNullOrEmpty(messageText))
                {
                    var recdMessage = messageText;
                    Common.Helpers.Settings.LastMessage = recdMessage;
                    PushNoticationMessage loginAttemptMessage = JsonConvert.DeserializeObject<PushNoticationMessage>(recdMessage);
                    PlatformPushNotificationImplementation.Platform.PushNotificationReceived(recdMessage, recdMessage);
                    CreateNotification(loginAttemptMessage);
                }
            }
            //sWakeLock.Release();
        }

        void CreateNotification(PushNoticationMessage pushedMessage)
        {
            // Get the notifications manager:
            NotificationManager notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
            // Instantiate the notification builder:
            Notification.Builder builder = new Notification.Builder(this)
                        .SetContentTitle("WK Authenticator")
                        .SetContentText(pushedMessage.Title)
                        .SetAutoCancel(true)
                        .SetSmallIcon(Resource.Drawable.icon)
                        .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.icon));
            builder.SetVisibility(NotificationVisibility.Public);
            builder.SetPriority((int)NotificationPriority.High);
            builder.SetCategory(Notification.CategoryMessage);
            //Add Message Information to the Main Activity Intent - This will be shown on Tap Of the Notified Message
            Intent secondIntent = new Intent(this, typeof(MainActivity));
            // Pass the current notification string value to SecondActivity
            secondIntent.PutExtra("recdMessage", Common.Helpers.Settings.LastMessage);
            // Pressing the Back button in SecondActivity exits the app:
            Android.App.TaskStackBuilder stackBuilder = Android.App.TaskStackBuilder.Create(this);
            // Add the back stack for the intent:
            stackBuilder.AddParentStack(Java.Lang.Class.FromType(typeof(MainActivity)));
            // Push the intent (that starts SecondActivity) onto the stack. The
            // pending intent can be used only once (one shot):
            stackBuilder.AddNextIntent(secondIntent);
            const int pendingIntentId = 0;
            PendingIntent pendingIntent = stackBuilder.GetPendingIntent(pendingIntentId, PendingIntentFlags.OneShot);
            builder.SetContentIntent(pendingIntent);
           // Build the notification:
            Notification notification = builder.Build();
            notification.Defaults |= NotificationDefaults.Sound;
            // Notification ID used for all notifications in this app.
            // Reusing the notification ID prevents the creation of numerous different notifications as the user experiments with different notification settings -- each launch reuses
            // and updates the same notification.
            const int notificationId = 0;
            // Launch notification:
            notificationManager.Notify(notificationId, notification);
        }

        protected override void OnUnRegistered(Context context, string registrationId)
        {
            Log.Verbose(AzurePushBroadcastReceiver.TAG, "GCM Unregistered: " + registrationId);
        }

        protected override bool OnRecoverableError(Context context, string errorId)
        {
            Log.Warn(AzurePushBroadcastReceiver.TAG, "Recoverable Error: " + errorId);
            return base.OnRecoverableError(context, errorId);
        }

        protected override void OnError(Context context, string errorId)
        {
            Log.Error(AzurePushBroadcastReceiver.TAG, "GCM Error: " + errorId);
        }

        public static void SubscribeByTags(string pushToken, string[] pushTags)
        {
            if (Hub != null)
            {
                try
                {
                    var registration = Hub.Register(pushToken, pushTags);
                }
                catch
                {

                }
            }
        }

        public static void Unregister()
        {
            if (Hub != null)
            {
                try
                {
                        Hub.UnregisterAll(RegistrationID);
                }
                catch
                {

                }
            }
        }
    }
}