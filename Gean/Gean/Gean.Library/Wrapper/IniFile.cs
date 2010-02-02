using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Gean
{
    /// <summary>
    /// һ������ͨ��ϵͳ������дINI�ļ�������
    /// </summary>
    public class IniFile
    {
        /// <summary>
        /// Gets or sets INI�ļ���·��
        /// </summary>
        /// <value>The file path.</value>
        public string FilePath { get; private set; }

        public IniFile(string filePath)
        {
            FilePath = filePath;
        }

        public void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.FilePath);
        }

        public void WriteValue(string Section, string Key, int Value)
        {
            WritePrivateProfileString(Section, Key, Value.ToString(), this.FilePath);
        }

        public string ReadValue(string Section, string Key, string Default)
        {
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, Default, sb, 255, this.FilePath);

            return sb.ToString();
        }

        public int ReadValue(string Section, string Key, int Default)
        {
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(Section, Key, Default.ToString(), sb, 255, this.FilePath);

            return int.Parse(sb.ToString());
        }

        #region DllImport

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        #endregion
    }
}
