using System;
using System.Collections.Generic;
using System.Text;

namespace Gean.Module.Chess
{
    /// <summary>
    /// 变招(变招同样也是一个Step的序列)。
    /// </summary>
    public class Variation : Steps, IItem
    {
        #region ISequenceItem 成员

        public string Value
        {
            get { return this.ToString(); }
        }

        #endregion
    }
}
