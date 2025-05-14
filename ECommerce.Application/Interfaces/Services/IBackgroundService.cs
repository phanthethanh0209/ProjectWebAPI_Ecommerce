using System.Linq.Expressions;

namespace ECommerce.Application.Interfaces.Services
{
    public interface IBackgroundService
    {
        string Enqueue<T>(Expression<Action<T>> methodCall); // fire and forget
        string Schedule<T>(Expression<Action<T>> methodCall, TimeSpan delay); // delayed job
        string ContinueWith<T>(string parentJobId, Expression<Action<T>> methodCal); // continue job
        void AddOrUpdateRecurring<T>(string jobId, Expression<Action<T>> methodCal, string cronExpression); // continue job
    }
}
