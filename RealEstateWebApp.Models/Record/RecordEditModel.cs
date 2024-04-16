using Newtonsoft.Json;
using RealEstateWebApp.Models.Address;

namespace RealEstateWebApp.Models.Record
{
    public class RecordEditModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public double Square { get; set; }
        public string AddressJson { get; set; }
        public DateTime CreatedAt { get; set; }
        [JsonIgnore]
        public AddressModel Address
        {
            get
            {
                if (string.IsNullOrEmpty(AddressJson))
                    return null; 
                else
                    return JsonConvert.DeserializeObject<AddressModel>(AddressJson);
            }
            set { AddressJson = JsonConvert.SerializeObject(value); }
        }
    }
}