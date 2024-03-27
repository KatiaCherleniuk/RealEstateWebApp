using RealEstateWebApp.Models.Address;

namespace RealEstateWebApp.Models.Record
{
    public class RecordEditModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public double Square { get; set; }
        public AddressModel Address { get; set; }
    }
}