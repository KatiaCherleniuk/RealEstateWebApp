using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models;
using RealEstateWebApp.Models.FilterAndOrder;
using RealEstateWebApp.Models.Record;
using RealEstateWebApp.Models.RecordViewModels;

namespace RealEstateWebApp.DataAccess.Repositories.Records
{
    public interface IRecordRepository
    {
        Task<bool> Create(RecordSQLModel recordModel);
        Task<bool> Delete(int recordId);
        Task<RecordSQLModel> GetRecordForEdit(int recordId);
        Task<bool> Update(RecordSQLModel recordModel);
        Task<IEnumerable<int>> GetRecordsIdByFiltersAndOrder(int categoryId, IEnumerable<BaseFilterValueModel> filters, BaseOrderModel order, ServiceType? type = null);
        Task<IEnumerable<RecordSimplifiedViewModel>> GetRecordForViewSimplifiedByIdList(IEnumerable<int> idList);
        Task<RecordSimplifiedViewModel> GetRecordForViewSimplified(int recordId);
    }
}