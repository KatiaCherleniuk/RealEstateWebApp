namespace RealEstateWebApp.Models.Property
{
    public class PropertyWithValuesFlatModel
    {
        public int Id { get; set; }
        public PropertyType Type { get; set; }
        public string Title { get; set; }
        public int ValueId { get; set; }
        public string ValueTitle { get; set; }
    }
}