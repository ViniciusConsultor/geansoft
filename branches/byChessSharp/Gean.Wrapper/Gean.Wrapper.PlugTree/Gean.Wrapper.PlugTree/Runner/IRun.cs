﻿using System;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// PlugTree的核心接口之一，Run方法将实现插件的激活动作点。
    /// </summary>
    public interface IRun
    {
        /// <summary>
        /// 返回Run的拥有者
        /// </summary>
        object Owner { get; set; }

        /// <summary>
        /// 功能入口的核心方法, 运行该接口的实际功能。
        /// </summary>
        void Run();

        /// <summary>
        /// 当Run的拥有者发生改变时发生
        /// </summary>
        event EventHandler OwnerChanged;

    }
}