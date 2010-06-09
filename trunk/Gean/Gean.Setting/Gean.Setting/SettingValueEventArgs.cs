using System;

namespace Gean.Setting
{
    public delegate void SettingValueEventHandler(object sender, SettingValueEventArgs e);
    public class SettingValueEventArgs : EventArgs
    {
        public SettingValueEventArgs(ISetting setting, string name, object value)
        {
            if (setting == null)
            {
                throw new ArgumentNullException("setting");
            }
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            this.setting = setting;
            this.name = name;
            this.value = value;
            this.targetValue = value;
        }

        public ISetting Setting
        {
            get { return this.setting; }
        }

        public string Name
        {
            get { return this.name; }
        }

        public bool HasValue
        {
            get { return this.value != null; }
        }

        public object Value
        {
            get { return this.value; }
        }

        public object TargetValue
        {
            get { return this.targetValue; }
            set { this.targetValue = value; }
        }

        // members
        private readonly ISetting setting;
        private readonly string name;
        private readonly object value;
        private object targetValue;

    }
}