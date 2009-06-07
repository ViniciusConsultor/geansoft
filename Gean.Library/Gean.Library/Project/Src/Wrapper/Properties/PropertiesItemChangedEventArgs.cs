using System;

namespace Gean
{

    public class PropertiesItemChangedEventArgs : EventArgs
    {
        /// <returns>
        /// returns the changed property object
        /// </returns>
        public Properties Properties { get; private set; }

        /// <returns>
        /// The key of the changed property
        /// </returns>
        public string Key { get; private set; }

        /// <returns>
        /// The new value of the property
        /// </returns>
        public object NewValue { get; private set; }

        /// <returns>
        /// The new value of the property
        /// </returns>
        public object OldValue { get; private set; }

        public PropertiesItemChangedEventArgs(Properties properties, string key, object oldValue, object newValue)
        {
            this.Properties = properties;
            this.Key = key;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }
}
