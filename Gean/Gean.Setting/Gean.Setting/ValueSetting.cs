using System;

namespace Gean.Setting
{
    public class ValueSetting : ValueSettingBase
    {
        public ValueSetting(string name, Type valueType) :
            this(name, valueType, null, null)
        {
        } // ValueSetting

        public ValueSetting(string name, Type valueType, object value) :
            this(name, valueType, value, null)
        {
        } // ValueSetting

        public ValueSetting(string name, Type valueType, object value, object defaultValue) :
            base(name, defaultValue)
        {
            if (valueType == null)
            {
                throw new ArgumentNullException("valueType");
            }
            if (defaultValue != null && !defaultValue.GetType().Equals(valueType))
            {
                throw new ArgumentException("defaultValue");
            }

            this.valueType = valueType;
            ChangeValue(value);
        } // ValueSetting

        public override object OriginalValue
        {
            get { return LoadValue(Name, this.valueType, SerializeAs, DefaultValue); }
        } // OriginalValue

        public override object Value
        {
            get { return this.value; }
            set { ChangeValue(value); }
        } // Value

        public override void Load()
        {
            try
            {
                object originalValue = OriginalValue;
                if (originalValue == null && LoadUndefinedValue == false)
                {
                    return;
                }
                Value = originalValue;
            }
            catch
            {
                if (ThrowOnErrorLoading)
                {
                    throw;
                }
            }
        }

        public override void Save()
        {
            try
            {
                object toSaveValue = Value;
                if (toSaveValue == null && SaveUndefinedValue == false)
                {
                    return;
                }
                SaveValue(Name, this.valueType, SerializeAs, toSaveValue, DefaultValue);
            }
            catch
            {
                if (ThrowOnErrorSaving)
                {
                    throw;
                }
            }
        } // Save

        private void ChangeValue(object newValue)
        {
            if (newValue != null && !newValue.GetType().Equals(this.valueType))
            {
                throw new ArgumentException("newValue");
            }
            this.value = newValue;
        } // ChangeValue

        // members
        private readonly Type valueType;
        private object value;

    }
}