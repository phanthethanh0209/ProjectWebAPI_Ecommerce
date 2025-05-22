using ECommerce.Application.Features.Orders.Queries.GetOrders;
using ECommerce.Application.Interfaces.QueryFilters;

namespace ECommerce.Infrastructure.QueryFilters
{
    public class DateFilterServices : IDateFilterService
    {

        public (DateTime? StartDate, DateTime? EndDate) DateFilter(OrderFilterType filterType, DateTime? startDate, DateTime? endDate)
        {
            if (filterType == OrderFilterType.None)
                return (startDate, endDate != null ? endDate.Value.AddTicks(TimeSpan.TicksPerDay - 1) : null);

            DateTime now = DateTime.UtcNow;
            if (filterType == OrderFilterType.LastWeek)
            {
                int daysSinceMonday = now.DayOfWeek - DayOfWeek.Monday; // số ngày từ t2 -> htai
                if (daysSinceMonday < 0) daysSinceMonday += 7; // đảm bảo ngày chủ nhật cũng đúng, vì sunday là 0

                startDate = now.AddDays(-daysSinceMonday - 7).Date;
                endDate = startDate.Value.AddDays(6).Date.AddTicks(TimeSpan.TicksPerDay - 1); // đặt thời gian thành 23:59:59.9999999 của ngày
            }
            else if (filterType == OrderFilterType.LastMonth)
            {
                DateTime lastMonth = now.AddMonths(-1);
                int daysInMonth = DateTime.DaysInMonth(lastMonth.Year, lastMonth.Month);

                startDate = new(lastMonth.Year, lastMonth.Month, 1); // lấy ngày đầu tiên của tháng
                endDate = startDate.Value.AddDays(daysInMonth - 1).Date.AddTicks(TimeSpan.TicksPerDay - 1); // lấy hết ngày cuối cùng của tháng
            }
            else if (filterType == OrderFilterType.Last3Month)
            {
                endDate = now;
                startDate = now.AddMonths(-3);
            }

            return (startDate, endDate);
        }
    }
}
