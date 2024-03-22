using System.Collections.Generic;

namespace RealEstateWebApp.Models.Property
{
    public class PropertyWithValuesModel
    {
        public int Id { get; set; }
        public PropertyType Type { get; set; }
        public string Title { get; set; }
        public IEnumerable<TitleAndIdModel> Values { get; set; }
    }
}