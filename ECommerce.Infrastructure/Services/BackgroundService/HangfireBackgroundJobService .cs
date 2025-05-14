using ECommerce.Application.Interfaces.Services;
using Hangfire;
using System.Linq.Expressions;

namespace ECommerce.Infrastructure.Services.HangfireBackgroundJobService
{
    public class HangfireBackgroundJobService : IBackgroundService
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        //private readonly IRecurringJobManager _recurringJobManager;

        public HangfireBackgroundJobService(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
            //_recurringJobManager = recurringJobManager;
        }


        public void AddOrUpdateRecurring<T>(string jobId, Expression<Action<T>> methodCal, string cronExpression)
        {
            RecurringJob.AddOrUpdate<T>(jobId, methodCal, cronExpression);
        }

        public string ContinueWith<T>(string parentJobId, Expression<Action<T>> methodCal)
        {
            return _backgroundJobClient.ContinueJobWith<T>(parentJobId, methodCal);
        }

        public string Enqueue<T>(Expression<Action<T>> methodCall)
        {
            return _backgroundJobClient.Enqueue<T>(methodCall);
        }

        public string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay)
        {
            return _backgroundJobClient.Schedule<T>(methodCall, delay);
        }
    }
}
