using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Gean.Data
{
    /// <summary>
    /// 清理数据库事务的方法类，以防止发生错误锁定表。
    /// 调用<see cref="TransactionCleaner.Start()"/>方法启动清理的动作。
    /// </summary>
    public sealed class TransactionCleaner
    {
        IDbTransaction _transaction;
        System.Timers.Timer _timer = new System.Timers.Timer();

        /// <summary>
        /// Cleans a transaction when a error occurs to prevent locking tables.
        /// </summary>
        /// <param name="transaction">A transaction which is returned calling BeginTransaction method.</param>
        /// <param name="activeTime">The duration in which the transaction is active.</param>
        public TransactionCleaner(IDbTransaction transaction, double activeTime)
        {
            Init(transaction, activeTime);
        }

        private void Init(IDbTransaction transaction, double activeTime)
        {
            if (transaction == null)
            {
                return;
            }
            if (activeTime < 1)
            {
                return;
            }
            _transaction = transaction;

            _timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
            _timer.Interval = activeTime;
        }

        /// <summary>
        /// 开始清理数据库事务
        /// </summary>
        public void Start()
        {
            if (_timer.Enabled == false)
            {
                _timer.Start();
            }
        }

        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            if (_transaction != null && _transaction.Connection != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
