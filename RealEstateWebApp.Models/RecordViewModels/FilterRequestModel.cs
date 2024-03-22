using System.Collections.Generic;
using RealEstateWebApp.Models.FilterAndOrder;

namespace RealEstateWebApp.Models.RecordViewModels
{
    public class FilterRequestModel
    {
        public int CategoryId { get; set; }
        public List<BaseFilterValueModel> Filters { get; set; }
        public BaseOrderModel Order { get; set; }
        public int PageSize { get; set; }
        public int Page { get; set; }
    }
}