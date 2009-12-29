using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Data.Common;

namespace Gean.Data
{
    /// <summary>
    /// ���ӳ�״̬
    /// </summary>
    public enum PoolState
    {
        /// <summary>
        /// �ոմ����Ķ��󣬱�ʾ�ö���δ�����ù�StartSeivice������
        /// </summary>
        UnInitialize,
        /// <summary>
        /// ��ʼ���У���״̬�·������ڰ��ղ�����ʼ�����ӳء�
        /// </summary>
        Initialize,
        /// <summary>
        /// ������
        /// </summary>
        Run,
        /// <summary>
        /// ֹͣ״̬
        /// </summary>
        Stop
    }

    /// <summary>
    /// Ҫ�������ӵļ���
    /// </summary>
    public enum ConnLevel
    {
        /// <summary>
        /// ��ռ��ʽ������ȫ�µ�������Դ�����Ҹ�������Դ�ڱ���ʹ���ͷŻ����ӳ�֮ǰ�����ڷ����ȥ��
        /// ������ӳ�ֻ�ܷ������ü�������������Դ��ü��𽫲���һ���쳣����־���ӳ���Դ�ľ���
        /// </summary>
        ReadOnly,
        /// <summary>
        /// ���ȼ�-�ߣ�����ȫ�µ�������Դ����ʹ�����ü���������
        /// ע���˼��𲻱�֤�ڷ�����������Դ����Ȼ���ֶ���ռ����Դ���������ռ����Դ��ʹ��ReadOnely��
        /// </summary>
        High,
        /// <summary>
        /// ���ȼ�-�У��ʵ�Ӧ�����ü���������������
        /// </summary>
        None,
        /// <summary>
        /// ���ȼ�-�ף�������ʹ�����ü���������������
        /// </summary>
        Bottom
    }

    /// <summary>
    /// ��������
    /// </summary>
    public enum ConnTypeEnum
    {
        /// <summary>
        /// ODBC ����Դ
        /// </summary>
        Odbc,
        /// <summary>
        /// OLE DB ����Դ
        /// </summary>
        OleDb,
        /// <summary>
        /// SqlServer ���ݿ�����
        /// </summary>
        SqlClient,
        /// <summary>
        /// Ĭ�ϣ��޷��䣩
        /// </summary>
        None
    }

}
