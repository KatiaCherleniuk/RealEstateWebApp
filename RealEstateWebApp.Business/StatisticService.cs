using Microsoft.Extensions.Logging;
using RealEstateWebApp.DataAccess.Repositories.Records;
using RealEstateWebApp.DataAccess.Repositories.Stats;
using RealEstateWebApp.Models.RecordViewModels;
using RealEstateWebApp.Models.Stats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.Business
{
    public class StatisticService
    {
        private readonly IStatisticRepository _statisticRepository;
        public StatisticService(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        public async Task<List<StatsRecordWithStatus>> GetRecordsByStatuses(DateTime startDate)
        {
            var result = await _statisticRepository.GetRecordsCountByStatuses(startDate);
            return result.ToList();
        }

        public async Task<List<StatsUsersRegisteredCountByTime>> GetStatisticByUsers(IntervalType intervalType, int count)
        {
            var result = await _statisticRepository.GetUsersStatistic(intervalType, count);
            return result.ToList();
        }
        
        public async Task<List<StatsRecordByCategories>> GetRecordsByCategories(DateTime startDate)
        {
            var result = await _statisticRepository.GetRecordsByCategory(startDate);
            return result.ToList();
        }

        public async Task<List<StatsAveragePrices>> GetAveragePrices(IntervalType intervalType, int count)
        {
            var result = await _statisticRepository.GetAveragePrices(intervalType, count);
            return result.ToList();
        }

    }
}
