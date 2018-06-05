using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WK.TAA.CCHAxcess.CodeGames.Configuration;
using Xamarin.Forms;

namespace WK.TAA.CCHAxcess.CodeGames.ViewModels
{
	public class SettingsViewModel : ViewModelBase
	{
        bool PreviousNotificationStateValue = false;
        bool IsPageDisappearing = false;

        public ICommand EnableRemoteNotificationsCommand { protected set; get; }

        private bool _isPushChecked;

        public bool IsPushChecked
        {
            get { return _isPushChecked; }
            private set
            {
                SetProperty(ref _isPushChecked, value);
                RaisePropertyChanged(nameof(IsPushChecked));
            }
        }

        private GridLength _dynHeight;

        public GridLength DynHeight
        {
            get { return _dynHeight; }
            private set
            {
                SetProperty(ref _dynHeight, value);
                RaisePropertyChanged(nameof(DynHeight));
            }
        }

        private string _pnsStatusColor = AppConfiguration.WK_GREEN_COLOR;

        public string PNSStatusColor
        {
            get { return _pnsStatusColor; }
            private set
            {
                SetProperty(ref _pnsStatusColor, value);
                RaisePropertyChanged(nameof(PNSStatusColor));
            }
        }

        public ICommand PageAppearingCommand { protected set; get; }

        public ICommand PageDisappearingCommand { protected set; get; }

        public SettingsViewModel(INavigationService navigationService) : base(navigationService)
        {

        }
	}
}
