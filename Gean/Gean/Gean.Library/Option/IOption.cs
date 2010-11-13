using System;
namespace Gean.Options
{
    interface IOption<T>
    {
        /// <summary>
        /// 返回按原始数据格式重新组装数据改变后的报文.
        /// </summary>
        /// <returns>按原始数据格式重新组装的数据</returns>
        T GetChangedDatagram();

        object Entity { get; }
        Option<T> SetOptionValue(string key, object value);

        #region event

        /// <summary>
        /// 当选项载入(Load)完成后发生的事件
        /// </summary>
        public event OptionLoadedEventHandler OptionLoadedEvent;
        private void OnOptionLoaded(OptionLoadedEventArgs e)
        {
            if (OptionLoadedEvent != null)
                OptionLoadedEvent(this, e);
        }

        /// <summary>
        /// 当选项改变后发生的事件
        /// </summary>
        public event OptionChangedEventHandler OptionChangedEvent;
        private void OnOptionChanged(OptionChangeEventArgs e)
        {
            if (OptionChangedEvent != null)
                OptionChangedEvent(this, e);
        }

        /// <summary>
        /// 当选项改变前发生的事件(注意：此事件发生后，选项存在保存发生异常的可能性)
        /// </summary>
        public event OptionChangingEventHandler OptionChangingEvent;
        private void OnOptionChanging(OptionChangeEventArgs e)
        {
            if (OptionChangingEvent != null)
                OptionChangingEvent(this, e);
        }

        #endregion
    }

    public delegate void OptionChangedEventHandler(Object sender, OptionChangeEventArgs e);
    public delegate void OptionChangingEventHandler(Object sender, OptionChangeEventArgs e);
    public delegate void OptionLoadedEventHandler(Object sender, OptionLoadedEventArgs e);


    /// <summary>
    /// 选项值发生改变时的包含事件数据的类
    /// </summary>
    public class OptionLoadedEventArgs : EventArgs
    {
        public IOption Option { get; private set; }
        public OptionLoadedEventArgs(IOption option)
        {
            this.Option = option;
        }
    }

    /// <summary>
    /// 选项值发生改变时的包含事件数据的类
    /// </summary>
    public class OptionChangeEventArgs : EventArgs
    {
        public IOption Option { get; private set; }
        public String OptionValueName { get; private set; }
        public Object OptionValue { get; private set; }
        public OptionChangeEventArgs(IOption option, String key, Object value)
        {
            this.Option = option;
            this.OptionValueName = key;
            this.OptionValue = value;
        }
    }
}
