using System;

namespace Gean
{

    public class DefinerCollectionItemChangedEventArgs : EventArgs
    {
        /// <returns>
        /// returns the changed property object
        /// </returns>
        public DefinerCollection DefinerCollection { get; private set; }

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

        public DefinerCollectionItemChangedEventArgs(DefinerCollection definerCollection, string key, object oldValue, object newValue)
        {
            this.DefinerCollection = definerCollection;
            this.Key = key;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }
}
