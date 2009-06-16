using System;

namespace Gean
{

    public class DefinersItemChangedEventArgs : EventArgs
    {
        /// <returns>
        /// returns the changed property object
        /// </returns>
        public Definers DefinerCollection { get; private set; }

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

        public DefinersItemChangedEventArgs(Definers definers, string key, object oldValue, object newValue)
        {
            this.DefinerCollection = definers;
            this.Key = key;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }
}
