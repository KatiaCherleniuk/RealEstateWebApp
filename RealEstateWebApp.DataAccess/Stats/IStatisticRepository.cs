using RealEstateWebApp.Models.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.DataAccess.Repositories.Stats
{
    public interface IStatisticRepository
    {
        Task<IEnumerable<StatsRecordWithStatus>> GetRecordsCountByStatuses(DateTime startDate);
        Task<IEnumerable<StatsUsersRegisteredCountByTime>> GetUsersStatistic(IntervalType intervalType, int count);
        Task<IEnumerable<StatsRecordByCategories>> GetRecordsByCategory(DateTime startDate);
        Task<IEnumerable<StatsAveragePrices>> GetAveragePrices(IntervalType intervalType, int count);
    }
}
