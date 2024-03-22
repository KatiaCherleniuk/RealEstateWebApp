using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateWebApp.Models.FilterAndOrder;
using RealEstateWebApp.Models.Record;
using RealEstateWebApp.Models.RecordViewModels;

namespace RealEstateWebApp.DataAccess.Repositories.Records
{
    public interface IRecordRepository
    {
        Task<bool> Create(RecordEditModel recordModel);
        Task<bool> Delete(int recordId);
        Task<RecordEditModel> GetRecordForEdit(int recordId);
        Task<bool> Update(RecordEditModel recordModel);
        Task<IEnumerable<int>> GetRecordsIdByFiltersAndOrder(int categoryId, IEnumerable<BaseFilterValueModel> filters, BaseOrderModel order);
        Task<IEnumerable<RecordSimplifiedViewModel>> GetRecordForViewSimplifiedByIdList(IEnumerable<int> idList);
    }
}