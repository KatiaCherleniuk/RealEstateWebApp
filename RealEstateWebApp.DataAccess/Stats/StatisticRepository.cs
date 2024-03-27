using Microsoft.Extensions.Configuration;
using RealEstateWebApp.Models.RecordViewModels;
using RealEstateWebApp.Models.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.DataAccess.Repositories.Stats
{
    public class StatisticRepository : DataController, IStatisticRepository
    {
        public StatisticRepository(IConfiguration configuration) : base(configuration, "Stats")
        {
        }

        public Task<IEnumerable<StatsAveragePrices>> GetAveragePrices(IntervalType intervalType, int count)
        {
            return GetManyAsync<StatsAveragePrices>("GetAveragePriceByCategory", new { IntervalType = intervalType.ToString(), IntervalValue = count });
        }

        public Task<IEnumerable<StatsRecordByCategories>> GetRecordsByCategory(DateTime startDate)
        {
            return GetManyAsync<StatsRecordByCategories>("GetRecordsByCategories", new { StartDate = startDate });
        }

        public Task<IEnumerable<StatsRecordWithStatus>> GetRecordsCountByStatuses(DateTime startDate)
        {
            return GetManyAsync<StatsRecordWithStatus>("GetRecordsCountByStatuses", new { StartDate = startDate});
        }

        public Task<IEnumerable<StatsUsersRegisteredCountByTime>> GetUsersStatistic(IntervalType intervalType, int count)
        {
            return GetManyAsync<StatsUsersRegisteredCountByTime>("GetUsersRegisteredAfterDate", new {IntervalType = intervalType.ToString(), IntervalValue = count});
        }
    }
}
