using Polly;
using Polly.Extensions.Http;
using Quartz;
using Refit;
using ScheduleApp.Job;
using ScheduleApp.Refit;
using ScheduleApp.Services;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScheduleApp
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddAetosService(this IServiceCollection services)
        {
            services.AddSingleton<IAetosService, AetosService>();
            return services;
        }

        public static IServiceCollection AddRefitPolly(this IServiceCollection services, IConfiguration configuration)
        {
            string Retryapi = configuration.GetValue<string>("RetryPolly:Retryapi");
            int RetryCount = configuration.GetValue<int>("RetryPolly:RetryCount");
            double WaitTime =configuration.GetValue<double>("RetryPolly:WaitTime");


            services.AddHttpClient("client2", c =>
            {
                c.BaseAddress = new Uri(Retryapi);
                c.DefaultRequestHeaders.Add("Accept", "application/json");

            })
            .AddTypedClient(c => RestService.For<IAetosRefit>(c))
            .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(RetryCount, _ => TimeSpan.FromMilliseconds(WaitTime), (result, timeSpan, retryCount, context) =>
            {
                Log.Warning($"Request failed: {result.Exception.Message}. Wait {timeSpan.TotalSeconds}s before retry. Retry attempt {retryCount}.");
            }));


            return services;
        }
        public static IServiceCollection AddQuartzScheduler(this IServiceCollection services, IConfiguration configuration)
        {
            string backgroundJobInterval = configuration.GetValue<string>("Quartz:BackgroundJobInterVal");

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = new JobKey("BackgroundJob");
                q.AddJob<BackgroundJob>(opts => opts.WithIdentity(jobKey));

                q.AddTrigger(opts => opts
                    .ForJob(jobKey)
                    .WithIdentity("BackgroundJob-trigger")
                    .WithCronSchedule(backgroundJobInterval));
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            return services;
        }
    }
}
