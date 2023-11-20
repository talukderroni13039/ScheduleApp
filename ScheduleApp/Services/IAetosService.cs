using Azure;
using Polly;
using Refit;
using ScheduleApp.Refit;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp.Services
{
    internal interface IAetosService
    {
         Task<IEnumerable<AetosModel>> GetAllData();
    }
    public class AetosService : IAetosService
    {
        public async Task<IEnumerable<AetosModel>> GetAllData()
        {

            var api = RestService.For<IAetosRefit>("https://6529b51b55b137ddc83f1ac9.mockapi.io");
            var response = await api.GetAetosData();
            var httpStatus = response.StatusCode;

            //Determining if a success status code was received
            if (response.IsSuccessStatusCode)
            {
                return response.Content;
            }
            else
            {
                return Enumerable.Empty<AetosModel>();
            }
           
        }
    }
}
