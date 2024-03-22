using RealEstateWebApp.Models.UserSettings.RecordFields;

namespace RealEstateWebApp.Models.UserSettings
{
    public abstract class BaseRecordProperty
    {
        public int Width { get; set; }
        public bool IsShown { get; set; }
        public int Order { get; set; }

        protected BaseRecordProperty(RecordFieldEditModel setting)
        {
            Width = setting.Width;
            IsShown = setting.IsShown;
            Order = setting.Order;
        }
    }
}