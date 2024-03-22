using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstateWebApp.Models.Address
{
    public class AddressModel
    {
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }
        public string DisplayName { get; set; }
        public bool IsEnteredManually { get; set; }
        public DateTime? UpdatedAt { get; set; }

    }
}
