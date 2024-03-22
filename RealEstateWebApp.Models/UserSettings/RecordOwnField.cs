using RealEstateWebApp.Models.UserSettings.RecordFields;

namespace RealEstateWebApp.Models.UserSettings
{
    public class RecordOwnField : BaseRecordProperty
    {
        public string FieldName { get; set; }
        
        public RecordOwnField(RecordFieldEditModel setting) : base(setting)
        {
            FieldName = setting.FieldName = setting.FieldName;
        }
    }
}