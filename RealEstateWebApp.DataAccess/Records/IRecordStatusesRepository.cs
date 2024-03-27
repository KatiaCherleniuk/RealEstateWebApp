using RealEstateWebApp.Models.FilterAndOrder;
using RealEstateWebApp.Models.Record;
using RealEstateWebApp.Models.RecordStatus;
using RealEstateWebApp.Models.RecordViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.DataAccess.Repositories.Records
{
    public interface IRecordStatusesRepository
    {
        Task<bool> Create(RecordStatusModel recordModel);
        Task<bool> Delete(int recordId);
        Task<RecordStatusModel> GetByRecordId(int recordId);

    }
}
