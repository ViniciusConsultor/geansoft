﻿using System;
namespace Gean
{
    /// <summary>
    /// 定义一个全局ID生成器的接口
    /// </summary>
    public interface IIDGenerator
    {
        /// <summary>
        /// 主方法。根据当前类的输出规则生成一个全局不重复的ID。
        /// </summary>
        /// <returns>一个全局不重复的ID字符串</returns>
        string Generate();
    }
}
