using RealEstateWebApp.Models.FilterAndOrder;
using RealEstateWebApp.Models.Record;
using RealEstateWebApp.Models.RecordViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.DataAccess.Repositories.Records
{
    public interface IRecordStatusesRepository
    {
        Task<bool> Create(RecordEditModel recordModel);
        Task<bool> Delete(int recordId);
        Task<bool> Update(RecordEditModel recordModel);
        //Task<IEnumerable<int>> GetRecordsIdByFiltersAndOrder(int categoryId, IEnumerable<BaseFilterValueModel> filters, BaseOrderModel order);
        //Task<IEnumerable<RecordSimplifiedViewModel>> GetRecordForViewSimplifiedByIdList(IEnumerable<int> idList);
    }
}
