using Microsoft.Extensions.Configuration;
using RealEstateWebApp.DataAccess.Repositories.Records;
using RealEstateWebApp.Models.Record;
using RealEstateWebApp.Models.RecordStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.DataAccess.Records
{
    public class RecordStatusesRepository : DataController, IRecordStatusesRepository
    {
        public RecordStatusesRepository(IConfiguration configuration) : base(configuration, "RecordStatusesHistory")
        {
        }

        public Task<bool> Create(RecordStatusModel recordModel)
        {
            return InsertWithIdAsync(recordModel);
        }

        public Task<bool> Delete(int recordId)
        {
            return DeleteByIdAsync(recordId);
        }

        public Task<RecordStatusModel> GetByRecordId(int recordId)
        {
            return GetByIdAsync<int, RecordStatusModel>(recordId);
        }
    }
}
