using Newtonsoft.Json;
using RealEstateWebApp.Models.Address;
using RealEstateWebApp.Models.RecordValue;
using RealEstateWebApp.Models.RecordViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateWebApp.Models.Record
{
    public class RecordSQLModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public double Square { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }

        public RecordEditModel ToBasicModel()
        {
            AddressModel addressAsModel = null;
            if (Address != null)
                addressAsModel = JsonConvert.DeserializeObject<AddressModel>(Address);

            return new RecordEditModel()
            {
                Id = Id,
                Price = Price,
                Square = Square,
                Address = addressAsModel,
                CategoryId = CategoryId,
                CreatedAt = CreatedAt
            };
        }
        public RecordSQLModel() { }

        public RecordSQLModel(RecordEditModel basicModel)
        {
            Id = basicModel.Id;
            Price = basicModel.Price;
            Square = basicModel.Square;
            Address = JsonConvert.SerializeObject(basicModel.Address);
            CategoryId = basicModel.CategoryId;
            CreatedAt = basicModel.CreatedAt;
        }
    }
}
