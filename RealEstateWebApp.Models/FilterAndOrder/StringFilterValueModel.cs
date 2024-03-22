namespace RealEstateWebApp.Models.FilterAndOrder
{
    public class StringFilterValueModel : BaseFilterValueModel
    {
        public string Value { get; set; }
        
        public StringFilterValueModel(int propertyId, string value)
        {
            PropertyId = propertyId;
            Value = value;
        }
    }
}