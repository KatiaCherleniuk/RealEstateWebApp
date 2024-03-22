using System.Collections.Generic;

namespace RealEstateWebApp.Models.RecordViewModels
{
    public class FilterResponseModel
    {
        public int TotalItems { get; set; }
        public IEnumerable<RecordSimplifiedViewModel> Records { get; set; }
    }
}