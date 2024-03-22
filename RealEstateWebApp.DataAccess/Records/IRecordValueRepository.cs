using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models.RecordValue;

namespace RealEstateWebApp.DataAccess.Repositories.Records
{
    public interface IRecordValueRepository
    {
        Task<IEnumerable<RecordPropertyValueBasicModel>> GetRecordValuesByRecordId(int recordId);
        Task<bool> UpdateOrCreate(RecordPropertyValueEditModel recordValue);
        Task<IEnumerable<RecordPropertyValueBasicModel>> GetRecordValuesByIdList(IEnumerable<int> recordIdList);
    }
}