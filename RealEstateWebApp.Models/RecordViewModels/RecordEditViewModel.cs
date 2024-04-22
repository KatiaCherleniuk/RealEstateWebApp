using System.Collections.Generic;
using RealEstateWebApp.Models.Address;
using RealEstateWebApp.Models.Record;
using RealEstateWebApp.Models.RecordValue;

namespace RealEstateWebApp.Models.RecordViewModels
{
    public class RecordEditViewModel : RecordEditModel
    {
        public List<RecordPropertyValueBasicModel> Values { get; set; }
        public RecordEditViewModel() {}
        
        public RecordEditViewModel(RecordEditModel baseModel)
        {
            Id = baseModel.Id;
            CategoryId = baseModel.CategoryId;
            Price = baseModel.Price;
            Square = baseModel.Square;
            Address = baseModel.Address;
            CreatedAt = baseModel.CreatedAt;
            Type = baseModel.Type;
        }
    }
}