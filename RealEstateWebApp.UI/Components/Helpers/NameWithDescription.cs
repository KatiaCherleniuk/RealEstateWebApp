namespace RealEstateWebApp.UI.Components.Helpers
{
    public class NameWithDescription
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public NameWithDescription(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
