using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace Gean.Data
{
    /// <summary>
    /// 连接池中的一个连接类型
    /// </summary>
    public class ConnectionStruct : IDisposable
    {
        /// <summary>
        /// 连接池中的连接
        /// </summary>
        /// <param name="dbc">数据库连接</param>
        /// <param name="cte">连接类型</param>
        public ConnectionStruct(DbConnection dbc, ConnTypeEnum cte)
        {
            createTime = DateTime.Now;
            connect = dbc;
            connType = cte;
        }
        /// <summary>
        /// 连接池中的连接
        /// </summary>
        /// <param name="dt">连接创建时间</param>
        /// <param name="dbc">数据库连接</param>
        /// <param name="cte">连接类型</param>
        public ConnectionStruct(DbConnection dbc, ConnTypeEnum cte, DateTime dt)
        {
            createTime = dt;
            connect = dbc;
            connType = cte;
        }
        //--------------------------------------------------------------------
        private bool enable = true;//是否失效
        private bool use = false;//是否正在被使用中
        private bool allot = true;//表示该连接是否可以被分配
        private DateTime createTime = DateTime.Now;//创建时间
        private int useDegree = 0;//被使用次数
        private int repeatNow = 0;//当前连接被重复引用多少
        private bool isRepeat = true;//连接是否可以被重复引用，当被分配出去的连接可能使用事务时，该属性被标识为true
        private ConnTypeEnum connType = ConnTypeEnum.None;//连接类型
        private DbConnection connect = null;//连接对象
        private object obj = null;//连接附带的信息

        #region 属性部分
        /// <summary>
        /// 表示该连接是否可以被分配
        /// </summary>
        public bool Allot
        {
            get { return allot; }
            set { allot = value; }
        }
        /// <summary>
        /// 是否失效；false表示失效，只读
        /// </summary>
        public bool Enable
        { get { return enable; } }
        /// <summary>
        /// 是否正在被使用中，只读
        /// </summary>
        public bool IsUse
        { get { return use; } }
        /// <summary>
        /// 创建时间，只读
        /// </summary>
        public DateTime CreateTime
        { get { return createTime; } }
        /// <summary>
        /// 被使用次数，只读
        /// </summary>
        public int UseDegree
        { get { return useDegree; } }
        /// <summary>
        /// 当前连接被重复引用多少，只读
        /// </summary>
        public int RepeatNow
        { get { return repeatNow; } }
        /// <summary>
        /// 得到数据库连接状态，只读
        /// </summary>
        public ConnectionState State
        { get { return connect.State; } }
        /// <summary>
        /// 得到该连接，只读
        /// </summary>
        public DbConnection Connection
        { get { return connect; } }
        /// <summary>
        /// 连接是否可以被重复引用
        /// </summary>
        public bool IsRepeat
        {
            get { return isRepeat; }
            set { isRepeat = value; }
        }
        /// <summary>
        /// 连接类型，只读
        /// </summary> 
        public ConnTypeEnum ConnType
        { get { return connType; } }
        /// <summary>
        /// 连接附带的信息
        /// </summary>
        public object Obj
        {
            get { return obj; }
            set { obj = value; }
        }
        #endregion
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open()
        { connect.Open(); }
        /// <summary>
        /// 关闭数据库连接 
        /// </summary>
        public void Close()
        { connect.Close(); }
        /// <summary>
        /// 无条件将连接设置为失效
        /// </summary>
        public void SetConnectionLost()
        { enable = false; allot = false; }
        /// <summary>
        /// 被分配出去，线程安全的
        /// </summary>
        public void Repeat()
        {
            lock (this)
            {
                if (enable == false)//连接可用
                    throw new ResLostnExecption();//连接资源已经失效
                if (allot == false)//是否可以被分配
                    throw new AllotExecption();//连接资源不可以被分配
                if (use == true && isRepeat == false)
                    throw new AllotAndRepeatExecption();//连接资源已经被分配并且不允许重复引用
                repeatNow++;//引用记数+1
                useDegree++;//被使用次数+1
                use = true;//被使用
            }
        }
        /// <summary>
        /// 被释放回来，线程安全的
        /// </summary>
        public void Remove()
        {
            lock (this)
            {
                if (enable == false)//连接可用
                    throw new ResLostnExecption();//连接资源已经失效
                if (repeatNow == 0)
                    throw new RepeatIsZeroExecption();//引用记数已经为0
                repeatNow--;//引用记数-1
                if (repeatNow == 0)
                    use = false;//未使用
                else
                    use = true;//使用中
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            enable = false;
            connect.Close();
            connect = null;
        }
    }
}
