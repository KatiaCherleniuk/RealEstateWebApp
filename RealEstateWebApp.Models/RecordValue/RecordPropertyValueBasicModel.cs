using RealEstateWebApp.Models.Address;
using System.Collections.Generic;

namespace RealEstateWebApp.Models.RecordValue
{
    public class RecordPropertyValueBasicModel
    {
        public int RecordId { get; set; }
        public int PropertyId { get; set; }
        public decimal? ValueNumber { get; set; }
        public string? ValueString { get; set; }
        public int? ValueId { get; set; }
        public List<int> ValueList { get; set; }
        //public AddressModel ValueAddress { get; set; }
    }
}