using System;

namespace Gean.Wrapper.PlugTree
{
    public delegate void DefinerChangedEventHandler(object sender, DefinerChangedEventArgs e);

    public class DefinerChangedEventArgs : EventArgs
    {
        public Definer Definer { get; private set; }
        public string Key { get; private set; }
        public object NewValue { get; private set; }
        public object OldValue { get; private set; }

        public DefinerChangedEventArgs(Definer definer, string key, object oldValue, object newValue)
        {
            this.Definer = definer;
            this.Key = key;
            this.OldValue = oldValue;
            this.NewValue = newValue;
        }
    }
}
