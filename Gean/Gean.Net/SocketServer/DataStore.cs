using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using Gean.Net.CSUST.Net;

namespace Gean.Net
{
    /// <summary>
    /// 对一次Socket连接发生数据的操作
    /// </summary>
    public class DataStore : TOleDatabaseBase
    {
        private OleDbCommand _command;

        /// <summary>
        /// 重写 Open 方法
        /// </summary>
        public override void Open()
        {
            base.Open();  // 打开数据库

            _command = new OleDbCommand();
            _command.Connection = (OleDbConnection)this.DbConnection;

            // OleDbCommand 不能像 SqlCommand 在 CommandText 使用参数名称
            _command.CommandText = "insert into DatagramTextTable(SessionIP, SessionName, DatagramSize) values (?, ?, ?)";

            _command.Parameters.Add(new OleDbParameter("SessionIP", OleDbType.VarChar));
            _command.Parameters.Add(new OleDbParameter("SessionName", OleDbType.VarChar));
            _command.Parameters.Add(new OleDbParameter("DatagramSize", OleDbType.Integer));
        }

        /// <summary>
        /// 自定义数据存储方法
        /// </summary>
        public override void Store(byte[] datagramBytes, TSessionBase session)
        {
            string datagramText = Encoding.ASCII.GetString(datagramBytes);
            try
            {
                _command.Parameters["SessionIP"].Value = session.IP;
                _command.Parameters["SessionName"].Value = session.Name;
                _command.Parameters["DatagramSize"].Value = datagramBytes.Length;

                _command.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                this.OnDatabaseException(err);
            }
        }
    }
}
