namespace RealEstateWebApp.Models.FilterAndOrder
{
    public class NumberFilterValueModel : BaseFilterValueModel
    {
        public decimal? Min { get; set; }
        public decimal? Max { get; set; }
        
        public NumberFilterValueModel(int propertyId)
        {
            PropertyId = propertyId;
        }
    }
}