using Android.App;
using Android.Content;
using Android.OS;
using Android.Gms.Gcm;
using WK.TAA.CCHAxcess.CodeGames.Notifications;
using Newtonsoft.Json;
using Android.Graphics;
using Android.Util;
using WK.TAA.CCHAxcess.CodeGames.Helpers;
using System;

namespace WK.TAA.CCHAxcess.CodeGames.Droid.Notifications
{
    [Service(Exported = false)]
    [IntentFilter(new[] { GoogleCloudMessaging.IntentFilterActionReceive })]
    public class PNListenerService : GcmListenerService
    {
        public override void OnMessageReceived(string from, Bundle data)
        {
            Log.Info(AppConstants.TAG, "Message Received From " + from);
            var recdMessage = data.GetString("message");
            Settings.Current.LastMessage = recdMessage;
            PushNoticationMessage loginAttemptMessage = JsonConvert.DeserializeObject<PushNoticationMessage>(recdMessage);
            //PlatformPushNotificationImplementation.Platform.PushNotificationReceived(recdMessage, recdMessage);
            Log.Info(AppConstants.TAG, "Creating Local Notification");
            CreateNotification(loginAttemptMessage);
            Log.Info(AppConstants.TAG, "Created Local Notification");
        }

        void CreateNotification(PushNoticationMessage pushMessage)
        {
            try
            {
                // Get the notifications manager:
                NotificationManager notificationManager = GetSystemService(Context.NotificationService) as NotificationManager;
                // Instantiate the notification builder:
                //TODO: Get the Name of  the App from Manifest
                Notification.Builder builder = new Notification.Builder(this)
                            .SetContentTitle("Quick Task")
                            .SetContentText(pushMessage.Title)
                            .SetAutoCancel(true)
                            .SetSmallIcon(Resource.Drawable.icon)
                            .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Drawable.icon));
                builder.SetVisibility(NotificationVisibility.Public);
                builder.SetPriority((int)NotificationPriority.High);
                builder.SetCategory(Notification.CategoryMessage);
                //Setup an intent for the Main Activity:
                Intent secondIntent = new Intent(this, typeof(MainActivity));
                secondIntent.PutExtra("recdMessage", Settings.Current.LastMessage);
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
                // Reusing the notification ID prevents the creation of 
                // numerous different notifications as the user experiments
                // with different notification settings -- each launch reuses
                // and updates the same notification.
                const int notificationId = 6;
                // Launch notification:
                notificationManager.Notify(notificationId, notification);
                Log.Info(AppConstants.TAG, "Local Notification Sent");
            }
            catch(Exception ex)
            {
                Log.Info(AppConstants.TAG, "Local Notify Failed with " + ex.Message);
            }
        }
    }
}