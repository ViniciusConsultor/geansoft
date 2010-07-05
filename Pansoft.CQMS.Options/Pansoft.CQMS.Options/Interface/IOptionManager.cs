namespace Pansoft.CQMS.Options
{
	/// <summary>
	/// 选项管理器接口
	/// </summary>
	public interface IOptionManager
	{
        /// <summary>
        /// 此管理器所管理的选项
        /// </summary>
        /// <value>The option.</value>
		IOption Option { get; }

        /// <summary>
        /// 获得选项节
        /// </summary>
        /// <param name="xpath">选项节的XPath，如果为<c>null</c>，则返回根选项节</param>
        /// <returns><see cref="IOption"/></returns>
        IOption GetOption(string xpath);

        /// <summary>
        /// 初始化，提供给模块调用，进行管理器的初始化工作，比如载入选项文件等等
        /// </summary>
        /// <param name="optionFile">选项文件信息</param>
        /// <param name="monitor">是否要监视此选项的变化</param>
        /// <remarks>
        /// 参数<paramref name="monitor"/>表示，监视选项文件变化，以更新选项信息
        /// </remarks>
        void Initializes(string optionFile, bool monitor);

		/// <summary>
		/// 重新载入相关选项信息
		/// </summary>
		void Reset();
	}
}
