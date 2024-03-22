using RealEstateWebApp.Models.Property;
using RealEstateWebApp.Models.UserSettings.RecordFields;

namespace RealEstateWebApp.Models.UserSettings
{
    public class RecordProperty : BaseRecordProperty
    {
        public PropertyWithValuesModel PropertyWithValues { get; set; }
        
        public RecordProperty(RecordFieldEditModel setting, PropertyWithValuesModel property) : base(setting)
        {
            PropertyWithValues = property;
        }
    }
}