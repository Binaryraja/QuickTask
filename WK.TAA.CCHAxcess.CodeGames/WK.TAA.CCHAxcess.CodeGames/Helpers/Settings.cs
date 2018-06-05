using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WK.TAA.CCHAxcess.CodeGames.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public class Settings : INotifyPropertyChanged
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        static Settings settings;

        /// <summary>
        /// Gets or sets the current settings. This should always be used
        /// </summary>
        /// <value>The current.</value>
        public static Settings Current
        {
            get { return settings ?? (settings = new Settings()); }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName]string name = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        #endregion

        const string SettingsKey = "settings_key";
        static readonly string SettingsDefault = string.Empty;

        public string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        const string DeviceTokenKey = "devicetoken_key";
        static readonly string DeviceTokenValue = string.Empty;

        public string DeviceToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(DeviceTokenKey, DeviceTokenValue);
            }
            set
            {
                AppSettings.AddOrUpdateValue(DeviceTokenKey, value);
            }
        }

        const string RegistrationIdKey = "pushtoken_key";
        static readonly string RegistrationIdValue = string.Empty;

        public string RegistrationId
        {
            get
            {
                return AppSettings.GetValueOrDefault(RegistrationIdKey, RegistrationIdValue);
            }
            set
            {
                AppSettings.AddOrUpdateValue(RegistrationIdKey, value);
            }
        }

        const string LastMsgKey = "lastmsg_key";
        static readonly string LastMsgValue = string.Empty;

        public string LastMessage
        {
            get
            {
                return AppSettings.GetValueOrDefault(LastMsgKey, LastMsgValue);
            }
            set
            {
                AppSettings.AddOrUpdateValue(LastMsgKey, value);
            }
        }

        const string IsNewInstallationKey = "isnewinstallation_key";
        static readonly bool IsNewInstallationValue = true;

        public bool IsNewInstallation
        {
            get
            {
                return AppSettings.GetValueOrDefault(IsNewInstallationKey, IsNewInstallationValue);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IsNewInstallationKey, value);
            }
        }

        const string IsPushEnabledKey = "ispushenabled_key";
        static readonly bool IsPushEnabledValue = false;

        public bool IsPushEnabled
        {
            get
            {
                return AppSettings.GetValueOrDefault(IsPushEnabledKey, IsPushEnabledValue);
            }
            set
            {
                if (AppSettings.AddOrUpdateValue(IsPushEnabledKey, value))
                    OnPropertyChanged();
            }
        }

        const string IsTouchIDEnabledKey = "istouchidenabled_key";
        static readonly bool IsTouchIDEnabledValue = false;

        public bool IsTouchIDEnabled
        {
            get
            {
                return AppSettings.GetValueOrDefault(IsTouchIDEnabledKey, IsTouchIDEnabledValue);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IsTouchIDEnabledKey, value);
            }
        }

        const string GooglePlayCheckedKey = "play_checked";
        static readonly bool GooglePlayCheckedDefault = false;

        public bool GooglePlayChecked
        {
            get { return AppSettings.GetValueOrDefault(GooglePlayCheckedKey, GooglePlayCheckedDefault); }
            set
            {
                AppSettings.AddOrUpdateValue(GooglePlayCheckedKey, value);
            }
        }

        const string AttemptedPushKey = "attempted_push";
        static readonly bool AttemptedPushDefault = false;

        public bool AttemptedPush
        {
            get { return AppSettings.GetValueOrDefault(AttemptedPushKey, AttemptedPushDefault); }
            set
            {
                AppSettings.AddOrUpdateValue(AttemptedPushKey, value);
            }
        }

        const string UserLoggedInKey = "userloggedin_key";
        static readonly bool UserLoggedInDefault = false;

        public bool UserLoggedIn
        {
            get { return AppSettings.GetValueOrDefault(UserLoggedInKey, UserLoggedInDefault); }
            set
            {
                AppSettings.AddOrUpdateValue(UserLoggedInKey, value);
            }
        }


    }
}
