using System.Collections;
using System.Data;

namespace Gean.Data
{
    /// <summary>
    /// 对查询时Where参数的封装。从MyGeneration.dOOdads移植。2010-01-01 23:41:02
    /// </summary>
    /// <code>
    /// emps.Where.LastName.Value = "%A%";
    /// emps.Where.LastName.Operator = WhereParameter.Operand.Like;
    /// </code>
    /// </remarks>
    public class WhereParameter
    {
        public WhereParameter(string column, IDataParameter param)
        {
            this.Column = column;
            this.Param = param;
            this._operator = Operand.Equal;
        }

        /// <summary>
        /// 判断<see cref="WhereParameters "/>是否有值
        /// </summary>
        public bool IsDirty
        {
            get { return _isDirty; }
        }
        private bool _isDirty = false;

        public string Column { get; private set; }
        public IDataParameter Param { get; private set; }

        /// <summary>
        /// The value that will be placed into the Parameter
        /// </summary>
        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                _isDirty = true;
            }
        }
        private object _value = null;

        /// <summary>
        /// The type of comparison desired
        /// </summary>
        public Operand Operator
        {
            get { return _operator; }
            set
            {
                _operator = value;
                _isDirty = true;
            }
        }
        private Operand _operator;

        /// <summary>
        /// The type of conjuction to use, "AND" or "OR"
        /// </summary>
        public Between Conjuction
        {
            get { return _conjuction; }
            set
            {
                _conjuction = value;
                _isDirty = true;
            }
        }
        private Between _conjuction = Between.UseDefault;

        /// <summary>
        /// Used when use the Operand.Between comparison
        /// </summary>
        public object BetweenBeginValue
        {
            get
            {
                return _betweenBeginValue;
            }
            set
            {
                _betweenBeginValue = value;
                _isDirty = true;
            }
        }
        private object _betweenBeginValue = null;

        /// <summary>
        /// Used when use the Operand.Between comparison
        /// </summary>
        public object BetweenEndValue
        {
            get { return _betweenEndValue; }
            set
            {
                _betweenEndValue = value;
                _isDirty = true;
            }
        }
        private object _betweenEndValue = null;

        #region Enum: Operand, OrderBy, Between

        /// <summary>
        /// 操作方法
        /// </summary>
        public enum Operand
        {
            /// <summary>
            /// Equal Comparison
            /// </summary>
            Equal = 1,
            /// <summary>
            /// Not Equal Comparison
            /// </summary>
            NotEqual,
            /// <summary>
            /// Greater Than Comparison
            /// </summary>
            GreaterThan,
            /// <summary>
            /// Greater Than or Equal Comparison
            /// </summary>
            GreaterThanOrEqual,
            /// <summary>
            /// Less Than Comparison
            /// </summary>
            LessThan,
            /// <summary>
            /// Less Than or Equal Comparison
            /// </summary>
            LessThanOrEqual,
            /// <summary>
            /// Like Comparison, "%s%" does it have an 's' in it? "s%" does it begin with 's'?
            /// </summary>
            Like,
            /// <summary>
            /// Is the value null in the database
            /// </summary>
            IsNull,
            /// <summary>
            /// Is the value non-null in the database
            /// </summary>
            IsNotNull,
            /// <summary>
            /// Is the value between two parameters? see <see cref="BetweenBeginValue"/> and <see cref="BetweenEndValue"/>. 
            /// Note that Between can be for other data types than just dates.
            /// </summary>
            Between,
            /// <summary>
            /// Is the value in a list, ie, "4,5,6,7,8"
            /// </summary>
            In,
            /// <summary>
            /// NOT in a list, ie not in, "4,5,6,7,8"
            /// </summary>
            NotIn,
            /// <summary>
            /// Not Like Comparison, "%s%", anything that does not it have an 's' in it.
            /// </summary>
            NotLike
        };

        /// <summary>
        /// The direction used by DynamicQuery.AddOrderBy
        /// </summary>
        public enum OrderBy
        {
            /// <summary>
            /// Ascending
            /// </summary>
            ASC = 1,
            /// <summary>
            /// Descending
            /// </summary>
            DESC
        };

        /// <summary>
        /// The conjuction used between WhereParameters
        /// </summary>
        public enum Between
        {
            /// <summary>
            /// WhereParameters are joined via "And"
            /// </summary>
            And = 1,
            /// <summary>
            /// WhereParameters are joined via "Or"
            /// </summary>
            Or,
            /// <summary>
            /// WhereParameters are used via the default passed into DynamicQuery.Load.
            /// </summary>
            UseDefault
        };

        #endregion
    }
}
