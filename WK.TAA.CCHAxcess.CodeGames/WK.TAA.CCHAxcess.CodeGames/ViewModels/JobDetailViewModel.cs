using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WK.TAA.CCHAxcess.CodeGames.Models;
using WK.TAA.CCHAxcess.CodeGames.Services;
using Xamarin.Forms;

namespace WK.TAA.CCHAxcess.CodeGames.ViewModels
{
	public class JobDetailViewModel : ViewModelBase
	{
        private string _batchGuid;

        public string BatchGuid
        {
            get { return _batchGuid; }
            private set { SetProperty(ref _batchGuid, value); }
        }

        private List<Child> _jobDetails;

        public List<Child> JobDetails
        {
            get { return _jobDetails; }
            private set
            {
                SetProperty(ref _jobDetails, value, () => RaisePropertyChanged(nameof(JobDetails)));
            }
        }
        public ICommand PageAppearingCommand { protected set; get; }

        public ICommand PageDisappearingCommand { protected set; get; }


        public JobDetailViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.PageAppearingCommand = new Command(async () =>
            {
                await LoadJobDetails();
            });
         }

        private async Task LoadJobDetails()
        {
            TaxService _taxService = new TaxService();
            var OpenRequests = (await _taxService.GetAllBatchStatus()).ToList();
            JobDetails = OpenRequests.Where(a => a.BatchGuid == Guid.Parse(BatchGuid)).FirstOrDefault().Items;
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("BatchGuid"))
                BatchGuid = (string)parameters["BatchGuid"];
        }
    }
}
