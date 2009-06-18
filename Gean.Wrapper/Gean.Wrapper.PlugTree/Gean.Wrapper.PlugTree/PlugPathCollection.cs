using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.PlugTree
{
    public sealed class PlugPathCollection : OutList<PlugPath>
    {

        /// <summary>
        /// 尝试用一个PlugPath的名字(PlugPath.Name)来获取该PlugPath。
        /// 如果该PlugPath存在则返回true，并out该PlugPath。不存在返回false。
        /// </summary>
        public bool TryGetValue(string name, out PlugPath plagpath)
        {
            plagpath = null;
            foreach (PlugPath path in _List)
            {
                if (path.Name.Equals(name))
                {
                    plagpath = path;
                    return true;
                }
            }
            return false;
        }

        public bool Contains(string name)
        {
            foreach (PlugPath path in _List)
            {
                if (path.Name.Equals(name))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
