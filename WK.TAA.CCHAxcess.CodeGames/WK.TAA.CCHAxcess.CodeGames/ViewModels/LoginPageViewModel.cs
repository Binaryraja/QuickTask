using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;


using System.Threading.Tasks;
using WK.TAA.CCHAxcess.CodeGames.Helpers;

namespace WK.TAA.CCHAxcess.CodeGames.ViewModels
{
	public class LoginPageViewModel : ViewModelBase
	{
        private string accountId = "";
        private string userId = "";
        private string passWord = "";

        private bool areCredentialsInvalid = false;

        private bool canEnable = false;

        public string AccountId
        {
            set
            {
                if (accountId != value)
                {
                    accountId = value;
                    RaisePropertyChanged(nameof(AccountId));
                    if ((!string.IsNullOrEmpty(accountId) && !string.IsNullOrEmpty(userId)) && !string.IsNullOrEmpty(passWord))
                        canEnable = true;
                    else
                        canEnable = false;
                    // Perhaps the delete button must be enabled/disabled.
                    ((Command)this.LoginCommand).ChangeCanExecute();
                }
            }

            get { return accountId; }
        }

        public string UserId
        {
            set
            {
                if (userId != value)
                {
                    userId = value;
                    RaisePropertyChanged(nameof(UserId));
                    if ((!string.IsNullOrEmpty(accountId) && !string.IsNullOrEmpty(userId)) && !string.IsNullOrEmpty(passWord))
                        canEnable = true;
                    else
                        canEnable = false;
                    // Perhaps the delete button must be enabled/disabled.
                    ((Command)this.LoginCommand).ChangeCanExecute();
                }
            }
            get { return userId; }
        }

        public string Password
        {
            set
            {
                if (passWord != value)
                {
                    passWord = value;
                    RaisePropertyChanged(nameof(Password));
                    if ((!string.IsNullOrEmpty(accountId) && !string.IsNullOrEmpty(userId)) && !string.IsNullOrEmpty(passWord))
                        canEnable = true;
                    else
                        canEnable = false;
                    // Perhaps the delete button must be enabled/disabled.
                    ((Command)this.LoginCommand).ChangeCanExecute();
                }
            }
            get { return passWord; }
        }

        public bool AreCredentialsInvalid
        {
            set
            {
                if (areCredentialsInvalid != value)
                {
                    areCredentialsInvalid = value;
                    RaisePropertyChanged(nameof(AreCredentialsInvalid));
                }
            }
            get { return areCredentialsInvalid; }
        }

        public ICommand LoginCommand { protected set; get; }

        public LoginPageViewModel(INavigationService navigationService) :  base(navigationService)
        {
            this.LoginCommand = new Command(async () => await DoUserLogin(), () => canEnable);
        }

        private async Task DoUserLogin()
        {
            if (AccountId== "123456" && UserId == "123456" && Password == "test1234")
            {
                await NavigationService.NavigateAsync("/Index/Navigation/Home");
                Settings.Current.UserLoggedIn = true;
            }
            else
                AreCredentialsInvalid = true;
        }
    }
}
