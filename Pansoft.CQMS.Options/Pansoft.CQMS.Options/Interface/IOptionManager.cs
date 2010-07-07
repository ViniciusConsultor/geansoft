namespace Pansoft.CQMS.Options
{
	/// <summary>
	/// 选项管理器接口
	/// </summary>
	public interface IOptionManager
	{
        /// <summary>
        /// 获得选项节
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns><see cref="IOption"/></returns>
        OptionCollection GetOption(string param);

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
		void ReLoad();

        void Save();
	}
}
