using RealEstateWebApp.Models.Address;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace RealEstateWebApp.Models.RecordValue
{
    public class RecordPropertyValueEditModel
    {
        public int RecordId { get; set; }
        public int PropertyId { get; set; }
        public decimal? ValueNumber { get; set; }
        public string? ValueString { get; set; }
        public int? ValueId { get; set; }
        public string? ValueList { get; set; }
        public string? ValueAddress { get; set; }

        public RecordPropertyValueBasicModel ToBasicModel()
        {
            List<int> valueList = null;
            if (ValueList != null)
            {
                valueList = new List<int>();
                foreach (var s in ValueList.Split(","))
                {
                    if (int.TryParse(s, out var v))
                        valueList.Add(v);
                }
            }
            AddressModel addressAsModel = null;
            if (ValueAddress != null)
                addressAsModel = JsonConvert.DeserializeObject<AddressModel>(ValueAddress);

            return new RecordPropertyValueBasicModel()
            {
                RecordId = RecordId,
                PropertyId = PropertyId,
                ValueId = ValueId,
                ValueNumber = ValueNumber,
                ValueString = ValueString,
                ValueList = valueList
                //ValueAddress = addressAsModel
            };
        }
        
        public RecordPropertyValueEditModel() {}

        public RecordPropertyValueEditModel(RecordPropertyValueBasicModel basicModel)
        {
            RecordId = basicModel.RecordId;
            PropertyId = basicModel.PropertyId;
            ValueId = basicModel.ValueId;
            ValueNumber = basicModel.ValueNumber;
            ValueString = basicModel.ValueString;
            ValueList = basicModel.ValueList == null ? null : string.Join(',', basicModel.ValueList);
            //ValueAddress = JsonConvert.SerializeObject(basicModel.ValueAddress);
        }
        
    }
}