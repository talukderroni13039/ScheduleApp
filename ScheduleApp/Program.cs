using Quartz;

using ScheduleApp.Job;
using Microsoft.Extensions.Configuration;
using ScheduleApp.Refit;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using ScheduleApp.Services;
using System.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using Serilog.Formatting.Json;

namespace ScheduleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                                           .AddJsonFile("appsettings.json")
                                           .AddEnvironmentVariables()
                                           .Build();   // Configuration setup

            Log.Logger = new LoggerConfiguration().ReadFrom    //Initialize Logger   
                                                  .Configuration(config)
                                                  .CreateLogger();

            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                 
                    services.AddAetosService(); //Configure DI
                   // services.AddRefitPolly(config); //Configure RefitPolly
                    services.AddQuartzScheduler(config); //Configure Job
                    


                }).UseSerilog()
                .Build();

          

    
            host.Run();
        }
   
    
    
    }
}