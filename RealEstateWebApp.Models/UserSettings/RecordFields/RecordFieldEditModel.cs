using System;

namespace RealEstateWebApp.Models.UserSettings.RecordFields
{
    public class RecordFieldEditModel
    {

        public enum Type
        {
            OwnField,
            Property
        }
        
        public int Width { get; set; }
        public bool IsShown { get; set; }
        public int Order { get; set; }
        
        public Type ValueType { get; set; }
        public int PropertyId { get; set; }
        public string FieldName { get; set; }

        public static RecordFieldEditModel MakeDefaultProperty(int order, int propertyId)
        {
            var res = MakeDefault(order, Type.Property);
            res.PropertyId = propertyId;
            return res;
        }

        public static RecordFieldEditModel MakeDefaultField(int order, string fieldName)
        {
            var res = MakeDefault(order, Type.OwnField);
            res.FieldName = fieldName;
            return res;
        }

        private static RecordFieldEditModel MakeDefault(int order, Type valueType)
        {
            return new RecordFieldEditModel()
            {
                Order = order,
                Width = 100,
                IsShown = true,
                ValueType = valueType
            };
        }

        public static bool IsSameField(RecordFieldEditModel a, RecordFieldEditModel b)
        {
            if (a.ValueType != b.ValueType)
                return false;
            switch (a.ValueType)
            {
                case Type.OwnField:
                    return a.FieldName == b.FieldName;
                case Type.Property:
                    return a.PropertyId == b.PropertyId;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public RecordFieldEditModel(RecordOwnField recordOwnField) : this((BaseRecordProperty)recordOwnField)
        {
            ValueType = Type.OwnField;
            FieldName = recordOwnField.FieldName;
        }
        
        public RecordFieldEditModel(RecordProperty recordProperty) : this((BaseRecordProperty)recordProperty)
        {
            ValueType = Type.Property;
            PropertyId = recordProperty.PropertyWithValues.Id;
        }

        public RecordFieldEditModel() {}

        private RecordFieldEditModel(BaseRecordProperty property)
        {
            Order = property.Order;
            Width = property.Width;
            IsShown = property.IsShown;
        }
        
    }
}