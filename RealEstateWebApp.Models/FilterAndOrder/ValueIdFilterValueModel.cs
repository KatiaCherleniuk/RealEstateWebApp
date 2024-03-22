namespace RealEstateWebApp.Models.FilterAndOrder
{
    public class ValueIdFilterValueModel : BaseFilterValueModel
    {
        public int ValueId { get; set; }
        
        public ValueIdFilterValueModel(int propertyId)
        {
            PropertyId = propertyId;
        }
    }
}