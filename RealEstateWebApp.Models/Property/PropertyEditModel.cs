namespace RealEstateWebApp.Models.Property
{
    public class PropertyEditModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public PropertyType Type { get; set; }
    }
}