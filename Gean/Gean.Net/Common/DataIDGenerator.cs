using System;
using System.Collections.Generic;
using System.Text;
using Gean;

namespace Gean.Net.Common
{
    public class DataIDGenerator : IDGenerator
    {
        #region 单件实例

        private DataIDGenerator() { }

        /// <summary>
        /// 获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static DataIDGenerator ME
        {
            get { return Singleton.Instance; }
        }

        private class Singleton
        {
            static Singleton() { Instance = new DataIDGenerator(); }
            internal static readonly DataIDGenerator Instance = null;
        }

        #endregion
    }
}
