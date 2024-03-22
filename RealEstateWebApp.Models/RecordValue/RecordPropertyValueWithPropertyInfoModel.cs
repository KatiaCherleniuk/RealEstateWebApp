using RealEstateWebApp.Models.Property;

namespace RealEstateWebApp.Models.RecordValue
{
    public class RecordPropertyValueWithPropertyInfoModel : RecordPropertyValueBasicModel
    {
        public string PropertyTitle { get; set; }
        public PropertyType PropertyType { get; set; }
    }
}