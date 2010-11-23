using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Gean.Options.Interfaces;

namespace Gean.Options
{
    /// <summary>
    /// 由GeanSoft设计的应用程序 Option（选项）框架的选项节点类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Option<T> : IOption
    {
        /// <summary>
        /// Initializeses本类型的具体数据。该方法将被反射调用。
        /// </summary>
        /// <param name="source">The source.</param>
        protected virtual void Initializes(T source)
        {
            this.Load(source);
            OnOptionLoaded(new OptionLoadedEventArgs(this, source));
        }

        /// <summary>
        /// 从指定的源(一般是一个XmlElement，也可以是任何类型)载入本类型的各个属性值
        /// </summary>
        /// <param name="source">指定的源(一般是一个XmlElement)</param>
        protected abstract void Load(T source);

        /// <summary>
        /// 保存设定的选项值
        /// </summary>
        public abstract bool Save();

        /// <summary>
        /// 返回按原始数据格式重新组装数据改变后的报文.
        /// </summary>
        /// <returns>按原始数据格式重新组装的数据</returns>
        public abstract T GetChangedDatagram();

        /// <summary>
        /// 当选项载入(Load)完成后发生的事件
        /// </summary>
        public event OptionLoadedEventHandler OptionLoadedEvent;
        /// <summary>
        /// Raises the <see cref="E:OptionLoadedEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Gean.Options.OptionLoadedEventArgs"/> instance containing the event data.</param>
        protected virtual void OnOptionLoaded(OptionLoadedEventArgs e)
        {
            if (OptionLoadedEvent != null)
                OptionLoadedEvent(this, e);
        }

        /// <summary>
        /// 当选项改变后发生的事件
        /// </summary>
        public event OptionChangedEventHandler OptionChangedEvent;
        /// <summary>
        /// Raises the <see cref="E:OptionChangedEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Gean.Options.OptionChangeEventArgs"/> instance containing the event data.</param>
        protected virtual void OnOptionChanged(OptionChangeEventArgs e)
        {
            if (OptionChangedEvent != null)
                OptionChangedEvent(this, e);
        }

        /// <summary>
        /// 当选项改变前发生的事件(注意：此事件发生后，选项存在保存发生异常的可能性，请注意处理)
        /// </summary>
        public event OptionChangingEventHandler OptionChangingEvent;
        /// <summary>
        /// Raises the <see cref="E:OptionChangingEvent"/> event.
        /// </summary>
        /// <param name="e">The <see cref="Gean.Options.OptionChangeEventArgs"/> instance containing the event data.</param>
        protected virtual void OnOptionChanging(OptionChangeEventArgs e)
        {
            if (OptionChangingEvent != null)
                OptionChangingEvent(this, e);
        }

    }
}