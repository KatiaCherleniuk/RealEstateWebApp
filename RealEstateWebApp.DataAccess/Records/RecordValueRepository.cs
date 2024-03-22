using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealEstateWebApp.Models.RecordValue;
using Microsoft.Extensions.Configuration;

namespace RealEstateWebApp.DataAccess.Repositories.Records
{
    public class RecordValueRepository : DataController, IRecordValueRepository
    {
        public RecordValueRepository(IConfiguration configuration) : base(configuration, "RecordPropertyValues")
        {
        }

        public async Task<IEnumerable<RecordPropertyValueBasicModel>> GetRecordValuesByRecordId(int recordId)
        {
            var res = await GetManyAsync<RecordPropertyValueEditModel>(
                "GetRecordValuesByRecordId", 
                new { RecordId = recordId });

            return res.Select(i => i.ToBasicModel());
        }

        public Task<bool> UpdateOrCreate(RecordPropertyValueEditModel recordValue)
        {
            return PerformNonQuerySafe("UpdateOrCreate", recordValue);
        }

        public async Task<IEnumerable<RecordPropertyValueBasicModel>> GetRecordValuesByIdList(IEnumerable<int> recordIdList)
        {
            var res = await GetManyAsync<RecordPropertyValueEditModel>(
                "GetRecordForViewSimplifiedByIdList", 
                recordIdList.AsSqlIdTableParameters("@RecordIdList"));
            return res.Select(i => i.ToBasicModel());
        }
    }
}