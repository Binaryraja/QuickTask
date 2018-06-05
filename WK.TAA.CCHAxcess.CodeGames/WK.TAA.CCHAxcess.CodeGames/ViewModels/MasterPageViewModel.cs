using Newtonsoft.Json;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WK.TAA.CCHAxcess.CodeGames.Notifications;
using Xamarin.Forms;

namespace WK.TAA.CCHAxcess.CodeGames.ViewModels
{
	public class MasterPageViewModel : ViewModelBase
	{
        public ICommand MasterHomeCommand { protected set; get; }

        public ICommand SettingsCommand { protected set; get; }

        public ICommand AboutCommand { protected set; get; }

        public ICommand PageAppearingCommand { protected set; get; }

        public ICommand PageDisappearingCommand { protected set; get; }

        public MasterPageViewModel(INavigationService navigationService): base(navigationService)
        {

            MasterHomeCommand = new Command(async () => {
                await NavigationService.NavigateAsync("/Index/Navigation/Home");
            });

            this.PageAppearingCommand = new Command(() => {
                    CrossAzurePushNotifications.Current.OnMessageReceived += Current_OnMessageReceived;
                    CrossAzurePushNotifications.Current.OnMessageOpened += Current_OnMessageOpened;
            });

            this.PageDisappearingCommand = new Command(() => {
                CrossAzurePushNotifications.Current.OnMessageReceived -= Current_OnMessageReceived;
                CrossAzurePushNotifications.Current.OnMessageOpened -= Current_OnMessageOpened;
            });

            SettingsCommand = new Command(async () => {
                await NavigationService.NavigateAsync("/Index/Navigation/Settings");
            });

            AboutCommand = new Command(async () => {
                await NavigationService.NavigateAsync("/Index/Navigation/About");

            });
        }

        private void Current_OnMessageOpened(object sender, ReceivedMessageEventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () => {
                await ProcessNotification(e);
            });
        }

        private async Task ProcessNotification(ReceivedMessageEventArgs e)
        {
            if (e != null && e.Content != null)
            {
                PushNoticationMessage recdMessage = JsonConvert.DeserializeObject<PushNoticationMessage>(e.Content);
                await NavigationService.NavigateAsync("/Index/Navigation/Home");
            }
        }

        private void Current_OnMessageReceived(object sender, ReceivedMessageEventArgs e)
        {

        }
    }
}
