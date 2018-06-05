using Android.App;
using Android.Content.PM;
using Android.OS;
using Prism;
using Prism.Ioc;
using WK.TAA.CCHAxcess.CodeGames.Droid.Notifications;
using Gcm.Client;
using Android.Gms.Common;
using WK.TAA.CCHAxcess.CodeGames.Helpers;
using Android.Content;
using WK.TAA.CCHAxcess.CodeGames.Notifications;

namespace WK.TAA.CCHAxcess.CodeGames.Droid
{
    [Activity(Label = "@string/applicationname", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            AzurePushNotificationsImplementation.Init(this);
            //Validate if Play Services are supported in the system.
            var gpsAvailable = IsPlayServicesAvailable();
            //Settings.Current.IsPushEnabled = gpsAvailable;
            if (gpsAvailable)
            {
                // Check to ensure everything's set up right
                GcmClient.CheckDevice(this);
                GcmClient.CheckManifest(this);
                // Register for push notifications
                StartService(new Intent(this, typeof(PNRegistrationService)));
            }

            LoadApplication(new App(new AndroidInitializer()));
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            Intent = intent;
        }

        protected override void OnPostResume()
        {
            base.OnPostResume();
            if (Intent.Extras != null)
            {
                if (CrossAzurePushNotifications.Current.IsRegistered)
                {
                    string recdMessage = Intent.Extras.GetString("recdMessage");
                    if (!string.IsNullOrEmpty(recdMessage))
                    {
                        PlatformPushNotificationImplementation.Platform.PushNotificationOpened(recdMessage);
                    }
                }
                Intent.RemoveExtra("type");
            }
        }
        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    if (Settings.Current.GooglePlayChecked)
                        return false;

                    Settings.Current.GooglePlayChecked = true;
                }
                else
                {
                    //Settings.Current.IsPushEnabled = false;
                }
                return false;
            }
            else
            {
                //Settings.Current.IsPushEnabled = true;
                return true;
            }
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry container)
        {
            // Register any platform specific implementations
        }
    }
}

