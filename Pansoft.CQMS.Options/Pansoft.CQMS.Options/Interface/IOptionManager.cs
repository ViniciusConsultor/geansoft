namespace Pansoft.CQMS.Options
{
	/// <summary>
	/// ѡ��������ӿ�
	/// </summary>
	public interface IOptionManager
	{
        /// <summary>
        /// ���ѡ���
        /// </summary>
        /// <param name="param">The param.</param>
        /// <returns><see cref="IOption"/></returns>
        OptionCollection GetOption(string param);

        /// <summary>
        /// ��ʼ�����ṩ��ģ����ã����й������ĳ�ʼ����������������ѡ���ļ��ȵ�
        /// </summary>
        /// <param name="optionFile">ѡ���ļ���Ϣ</param>
        /// <param name="monitor">�Ƿ�Ҫ���Ӵ�ѡ��ı仯</param>
        /// <remarks>
        /// ����<paramref name="monitor"/>��ʾ������ѡ���ļ��仯���Ը���ѡ����Ϣ
        /// </remarks>
        void Initializes(string optionFile, bool monitor);

        /// <summary>
        /// �����������ѡ����Ϣ
        /// </summary>
		void ReLoad();

        void Save();
	}
}
