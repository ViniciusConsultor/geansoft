using System;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 关于Plug状态的枚举
    /// </summary>
    public enum PlugAction
    {
        /// <summary>
        /// 可使用
        /// </summary>
        Enable,
        /// <summary>
        /// 不能使用
        /// </summary>
        Disable,
        /// <summary>
        /// 已安装
        /// </summary>
        Install,
        /// <summary>
        /// 未安装
        /// </summary>
        Uninstall,
        /// <summary>
        /// 刷新
        /// </summary>
        Update,
        /// <summary>
        /// 再次安装
        /// </summary>
        InstalledTwice,
        /// <summary>
        /// 附加Plug时错误
        /// </summary>
        DependencyError,
        /// <summary>
        /// 自定义错误
        /// </summary>
        CustomError
    }
}
