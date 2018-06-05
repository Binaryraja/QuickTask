using Android.App;
using Android.Content;
using Android.Gms.Iid;
using Android.Util;
using WK.TAA.CCHAxcess.CodeGames.Helpers;

namespace WK.TAA.CCHAxcess.CodeGames.Droid.Notifications
{
    [Service(Exported = false)]
    [IntentFilter(new[] { InstanceID.IntentFilterAction })]
    public class PNInstanceIDListenerService : InstanceIDListenerService
    {
        public override void OnTokenRefresh()
        {
            Log.Info(AppConstants.TAG, "Token Refreshed");
            var intent = new Intent(this, typeof(PNRegistrationService));
        }
    }
}