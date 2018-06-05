using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WK.TAA.CCHAxcess.CodeGames.Helpers;
using WK.TAA.CCHAxcess.CodeGames.Models;
using WK.TAA.CCHAxcess.CodeGames.Notifications;
using WK.TAA.CCHAxcess.CodeGames.Services;
using Xamarin.Forms;

namespace WK.TAA.CCHAxcess.CodeGames.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private List<Parent> _openRequests;

        public List<Parent> OpenRequests
        {
            get { return _openRequests; }
            private set
            {
                SetProperty(ref _openRequests, value, () => RaisePropertyChanged(nameof(OpenRequests)));
            }
        }

        public ICommand PageAppearingCommand { protected set; get; }

        public ICommand PageDisappearingCommand { protected set; get; }

        public ICommand RefreshCommand { protected set; get; }

        public ICommand SelectedCommand { protected set; get; }
        

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { SetProperty(ref _isRefreshing, value, () => RaisePropertyChanged(nameof(IsRefreshing))); }
        }

        public MainPageViewModel(INavigationService navigationService) 
            : base (navigationService)
        {
            Title = "Quick Task";
            this.PageAppearingCommand = new Command(async () =>
            {
                IsBusy = true;
                await LoadOpenRequests();
                IsBusy = false;
                UserInfo userInfo = new UserInfo()
                {
                    UserId = "123456",
                    Password = "test1234",
                    AccountId = "123456",
                    PushRegistrationId = Settings.Current.RegistrationId,
                };
                await NotificationsHelper.RegisterAccountForPNS(userInfo);
            });

            this.RefreshCommand = new Command(async () =>
            {
                IsRefreshing = true;
                await LoadOpenRequests();
                IsRefreshing = false;
            });

            this.SelectedCommand = new Command<Parent>(async (parentInfo) =>
            {
                if(parentInfo != null)
                {
                    NavigationParameters _navParameters = new NavigationParameters();
                    _navParameters.Add("BatchGuid", parentInfo.BatchGuid.ToString());
                    await NavigationService.NavigateAsync("JobDetail", _navParameters);
                }
            });
        }

        private async Task LoadOpenRequests()
        {
            TaxService _taxService = new TaxService();
            var batchJobs= (await _taxService.GetAllBatchStatus()).ToList();
            OpenRequests = batchJobs.OrderByDescending(a => a.SubmittedDtTm).ToList();
        }
    }
}
