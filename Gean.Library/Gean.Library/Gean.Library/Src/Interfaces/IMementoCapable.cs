using System;

namespace Gean
{
    /// <summary>
    /// 这个接口标志着实该接口的对象的状态是可以保存的。
    /// Memento:纪念品, 令人回忆的东西；
    /// Capable:有能力的, 有技能的；
    /// </summary>
    public interface IMementoCapable
    {
        /// <summary>
        /// 为实现该接口的对象创建可保存的Properties。
        /// </summary>
        Properties CreateMemento();

        /// <summary>
        /// 从Properties中返回实现该接口的对象的状态。
        /// </summary>
        void SetMemento(Properties memento);
    }
}
