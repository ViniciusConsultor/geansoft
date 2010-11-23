using System;
namespace Gean.Options.Interfaces
{
    /// <summary>
    /// 这是一个描述Option(选项、配置)的接口。
    /// </summary>
    public interface IOption
    {
        bool Save();

        /// <summary>
        /// 当选项载入(Load)完成后发生的事件
        /// </summary>
        event OptionLoadedEventHandler OptionLoadedEvent;
        /// <summary>
        /// 当选项改变后发生的事件
        /// </summary>
        event OptionChangedEventHandler OptionChangedEvent;
        /// <summary>
        /// 当选项改变前发生的事件(注意：此事件发生后，选项存在保存发生异常的可能性)
        /// </summary>
        event OptionChangingEventHandler OptionChangingEvent;
    }

    public delegate void OptionChangedEventHandler(Object sender, OptionChangeEventArgs e);
    public delegate void OptionChangingEventHandler(Object sender, OptionChangeEventArgs e);
    public delegate void OptionLoadedEventHandler(Object sender, OptionLoadedEventArgs e);

    /// <summary>
    /// 选项值发生改变时的包含事件数据的类
    /// </summary>
    public class OptionLoadedEventArgs : EventArgs
    {
        public IOption OptionObject { get; private set; }
        public object OptionValue { get; private set; }
        public OptionLoadedEventArgs(IOption optionObject, object source)
        {
            this.OptionObject = optionObject;
            this.OptionValue = source;
        }
    }

    /// <summary>
    /// 选项值发生改变时的包含事件数据的类
    /// </summary>
    public class OptionChangeEventArgs : EventArgs
    {
        public string OptionValueName { get; private set; }
        public Object OptionValue { get; private set; }
        public OptionChangeEventArgs(string key, object value)
        {
            this.OptionValueName = key;
            this.OptionValue = value;
        }
    }
}
