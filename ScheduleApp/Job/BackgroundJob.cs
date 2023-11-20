using Microsoft.Extensions.Logging;
using Quartz;
using ScheduleApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ScheduleApp.Job
{
    internal class BackgroundJob : IJob
    {

        private readonly ILogger<BackgroundJob> _logger;
        private readonly IAetosService _IAetosService;
        
        public BackgroundJob(ILogger<BackgroundJob> logger, IAetosService IAetosService)
        {
            _logger = logger;
            _IAetosService=IAetosService;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Job has been started!");

            var data= _IAetosService.GetAllData();

             string jsonData = JsonSerializer.Serialize(data);
            _logger.LogInformation(jsonData);
   

            return Task.CompletedTask;
        }
    }
}
