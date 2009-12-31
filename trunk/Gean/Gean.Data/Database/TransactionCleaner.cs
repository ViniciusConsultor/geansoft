using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Gean.Data
{
    /// <summary>
    /// Cleans a transaction when a error occurs to prevent locking tables.
    /// </summary>
    public class TransactionCleaner
    {
        IDbTransaction _transaction;
        System.Timers.Timer _timer = new System.Timers.Timer();

        /// <summary>
        /// Cleans a transaction when a error occurs to prevent locking tables.
        /// </summary>
        /// <param name="transaction">A transaction which is returned calling BeginTransaction method.</param>
        /// <param name="activeTime">The duration in which the transaction is active.</param>
        public TransactionCleaner(IDbTransaction transaction,double activeTime)
        {
            Init(transaction, activeTime);
        }

        private void Init(IDbTransaction transaction,double activeTime)
        {
            if (transaction == null)
            {
                return;
            }
            if (activeTime < 1)
            {
                return;
            }
            _timer.Elapsed += new System.Timers.ElapsedEventHandler(_timer_Elapsed);
            _transaction = transaction;
            _timer.Interval = activeTime;
            _timer.Enabled = true;
        }

        void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _timer.Enabled = false;
            if (_transaction != null && _transaction.Connection!=null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
                _transaction = null;
            }
        }
    }
}
