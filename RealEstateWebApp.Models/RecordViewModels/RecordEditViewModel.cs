using System.Collections.Generic;
using RealEstateWebApp.Models.Address;
using RealEstateWebApp.Models.Record;
using RealEstateWebApp.Models.RecordValue;

namespace RealEstateWebApp.Models.RecordViewModels
{
    public class RecordEditViewModel : RecordEditModel
    {
        public List<RecordPropertyValueBasicModel> Values { get; set; }
        public AddressModel Address { get; set; }
        public double Price { get; set; }
        public double Square { get; set; }
        
        public RecordEditViewModel() {}
        
        public RecordEditViewModel(RecordEditModel baseModel)
        {
            Id = baseModel.Id;
            CategoryId = baseModel.CategoryId;
        }
    }
}