using System;
namespace Gean.Option
{
    interface IOption
    {
        object Entity { get; }
        event Option.OptionChangedEventHandler OptionChangedEvent;
        event Option.OptionChangingEventHandler OptionChangingEvent;
        event Option.OptionLoadedEventHandler OptionLoadedEvent;
        Option SetOptionValue(string key, object value);
    }
}
