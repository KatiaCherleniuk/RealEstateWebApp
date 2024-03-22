using RealEstateWebApp.Models.Property;

namespace RealEstateWebApp.Models.FilterAndOrder
{
    public class OrderByPropertyModel : BaseOrderModel
    {
        public int PropertyId { get; set; }
        public PropertyType PropertyType { get; set; }
    }
}