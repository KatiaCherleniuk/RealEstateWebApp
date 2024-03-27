using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using RealEstateWebApp.Models.Property;
using RealEstateWebApp.Models.Record;
using RealEstateWebApp.Models.RecordValue;

namespace RealEstateWebApp.Models.RecordViewModels
{
    public class RecordSimplifiedViewModel : RecordEditModel
    {
        public IEnumerable<RecordPropertyValueBasicModel> Values { get; set; }

        public string GetValue(PropertyWithValuesModel property)
        {
            var value = Values.FirstOrDefault(v => v.PropertyId == property.Id);
            if (value == null)
                return "";
            return property.Type switch
            {
                PropertyType.Text => value.ValueString,
                PropertyType.Int => value.ValueNumber?.ToString() ?? "",
                PropertyType.Float => value.ValueNumber?.ToString("F2") ?? "",
                PropertyType.ListSingle => property.Values?.FirstOrDefault(v => v.Id == value.ValueId)?.Title ?? "",
                //PropertyType.Address => value.ValueAddress.DisplayName,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public IEnumerable<TitleAndIdModel> GetValueList(PropertyWithValuesModel property)
        {
            var value = Values.FirstOrDefault(v => v.PropertyId == property.Id);
            var res = new List<TitleAndIdModel>();
            if (value?.ValueList == null)
                return res;
            foreach (var valueListItemId in value.ValueList)
            {
                var propertyValue = property.Values?.FirstOrDefault(v => v.Id == valueListItemId);
                if (propertyValue == null)
                {
                    Console.WriteLine($"Can't find tag with such id (tagId:{valueListItemId}, recordId:{Id}, propertyId:{property.Id})");
                    continue;
                }
                res.Add(propertyValue);
            }
            return res;
        }

        public string GetValue(string fieldName)
        {
            return fieldName switch
            {
                nameof(Id) => Id.ToString(),
                _ => ""
            };
        }
    }
}