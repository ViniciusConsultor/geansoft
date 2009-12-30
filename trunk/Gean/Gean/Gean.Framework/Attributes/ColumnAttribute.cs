using System;

namespace Gean
{
    /// <summary>
    /// һ����ʵ��O/R��ϵ����������
    /// </summary>
    /// <remarks>
    /// ��� <see cref="Gean.Data.DataTransfer"/> ��ʵ�ֶ�������ݵ�ת��
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ColumnAttribute()
        {
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="columnName">����</param>
        public ColumnAttribute(string columnName)
        {
            this.ColumnName = columnName;
        }

        /// <summary>
        /// ����/��ȡ����
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// ����/��ȡ�������������
        /// </summary>
        public string ColumnGroup { get; set; }

        /// <summary>
        /// ����/��ȡ�е�����
        /// </summary>
        public Type ColumnType { get; set; }

        /// <summary>
        /// ����/��ȡ�����͵ĳ���
        /// </summary>
        public int ColumnSize { get; set; }

        /// <summary>
        /// ����/��ȡ�е�ȱʡֵ
        /// </summary>
        public object DefaultValue { get; set; }

        /// <summary>
        /// ����/��ȡָʾ�����Ƿ�ֻ��
        /// </summary>
        public bool ReadOnly { get; set; }

        /// <summary>
        /// ����/��ȡָʾ�����Ƿ�ֻд
        /// </summary>
        public bool WriteOnly { get; set; }

        /// <summary>
        /// �Ƿ�Ϊ����
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// �Ƿ��<c>null</c>
        /// </summary>
        public bool IsNullable { get; set; }
    }
}
