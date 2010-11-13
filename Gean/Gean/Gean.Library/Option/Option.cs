using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Options
{
    public abstract class Option<T> : IOption<T>
    {
        public Option()
        {
            this.Validation();
        }

        /// <summary>
        /// 数据是否被修改
        /// </summary>
        protected bool IsModified = true;

        /// <summary>
        /// 内部校验
        /// </summary>
        protected abstract void Validation();

        /// <summary>
        /// Initializeses本类型的具体数据。
        /// </summary>
        /// <param name="source">The source.</param>
        protected abstract void Initializes(T source);

        /// <summary>
        /// 返回按原始数据格式重新组装数据改变后的报文.
        /// </summary>
        /// <returns>按原始数据格式重新组装的数据</returns>
        public abstract T GetChangedDatagram();


    }
}