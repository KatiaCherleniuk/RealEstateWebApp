using System.Collections.Generic;

namespace RealEstateWebApp.Models.FilterAndOrder
{
    public class ListFilterValueModel : BaseFilterValueModel
    {
        public IEnumerable<int> Values { get; set; }
        
        public ListFilterValueModel(int propertyId)
        {
            PropertyId = propertyId;
        }
    }
}