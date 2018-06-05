using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WK.TAA.CCHAxcess.CodeGames.Models;
using WK.TAA.CCHAxcess.CodeGames.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(TaxService))]
namespace WK.TAA.CCHAxcess.CodeGames.Services
{
    public class TaxService : ITaxService
    {
       HttpClient serviceClient;

        public TaxService()
        {
            serviceClient = new HttpClient();
            serviceClient.MaxResponseContentBufferSize = 256000;
        }

        public async Task<Parent> GetBatchStatus(Guid batchGuid)
        {
            var uri = new Uri("http://13.90.213.31/PlatformCommon/v1.0/BatchStatus/GetAllBatchStatus");
            HttpResponseMessage responseObject = await serviceClient.GetAsync(uri);
            responseObject.EnsureSuccessStatusCode();
            Parent[] submittedJobs = JsonConvert.DeserializeObject<Parent[]>(responseObject.Content.ReadAsStringAsync().Result);
            return submittedJobs[0];
        }

        public async Task<Parent[]> GetAllBatchStatus()
        {
            try
            {
                var uri = new Uri("http://13.90.213.31/PlatformCommon/v1.0/BatchStatus/GetAllBatchStatus");
                HttpResponseMessage responseObject = await serviceClient.GetAsync(uri);
                responseObject.EnsureSuccessStatusCode();
                var submittedJobs = JsonConvert.DeserializeObject<Parent[]>(responseObject.Content.ReadAsStringAsync().Result);
                return submittedJobs;
            }
            catch(Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
