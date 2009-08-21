namespace Gean.Data
{
    public class DbEnums
    {
        #region 基础控件的类型
        /// <summary>
        /// 基础控件的类型
        /// </summary>
        public enum ControlType
        {
            /// <summary>
            /// 文本框
            /// </summary>
            TextBox = 0,
            /// <summary>
            /// 下拉列表框
            /// </summary>
            DropDownList = 1,
            /// <summary>
            /// 增强的文本框
            /// </summary>
            TextBoxControl = 2,
            /// <summary>
            /// 增强的下拉列表框
            /// </summary>
            DropDownListControl = 3,
            /// <summary>
            /// 复选框
            /// </summary>
            CheckBox = 4,
            /// <summary>
            /// 单选按钮
            /// </summary>
            RadioButton = 5
        }
        #endregion

        #region 列表控件的类型
        /// <summary>
        /// 列表控件的类型
        /// </summary>
        public enum ListControlType
        {
            /// <summary>
            /// 表格控件
            /// </summary>
            GridView = 0,
            /// <summary>
            /// 列表控件
            /// </summary>
            DataList = 1,
            /// <summary>
            /// 轻量级模板控件
            /// </summary>
            Repeater = 2,
            /// <summary>
            /// 列表框
            /// </summary>
            ListBox = 3
        }
        #endregion

        #region 查询运算符
        /// <summary>
        /// 查询运算符
        /// </summary>
        public enum QueryOperator
        {
            /// <summary>
            /// 等于( = )
            /// </summary>
            Equal = 0,
            /// <summary>
            /// 不等于( &lt;> )
            /// </summary>
            NotEqual = 1,
            /// <summary>
            /// 大于( > )
            /// </summary>
            Greater = 2,
            /// <summary>
            /// 大于等于( >= )
            /// </summary>
            GreaterEqual = 3,
            /// <summary>
            /// 小于( &lt; )
            /// </summary>
            Less = 4,
            /// <summary>
            /// 小于等于( &lt;= )
            /// </summary>
            LessEqual = 5,
            /// <summary>
            /// 模糊查询( Like )
            /// </summary>
            Like = 6,
            /// <summary>
            /// In
            /// </summary>
            In = 7
        }
        #endregion

        #region 数据操作类型
        /// <summary>
        /// 数据操作类型
        /// </summary>
        public enum DataOperatorType
        {
            /// <summary>
            /// 数据库操作
            /// </summary>
            DataBase = 0,
            /// <summary>
            /// 对象操作
            /// </summary>
            Object = 1
        }
        #endregion

        #region 数据库操作类型
        /// <summary>
        /// 数据库操作类型
        /// </summary>
        public enum DataBaseOperatorType
        {
            /// <summary>
            /// 编辑操作，即新增和修改
            /// </summary>
            Edit = 0,
            /// <summary>
            /// 查询操作
            /// </summary>
            Query = 1
        }
        #endregion

        #region 数据库类型
        /// <summary>
        /// 数据库类型
        /// </summary>
        public enum DataBaseType
        {
            SQLite = 0,
            MySql = 1,
            SQLServer = 2,
            Oracle = 4,
            Access = 8
        }
        #endregion

        #region 数据类型
        /// <summary>
        /// 数据类型
        /// </summary>
        public enum DataType
        {
            /// <summary>
            /// 字符串
            /// </summary>
            String = 0,
            /// <summary>
            /// 数字
            /// </summary>
            Number = 1,
            /// <summary>
            /// 日期
            /// </summary>
            Date = 2,
            /// <summary>
            /// 布尔值
            /// </summary>
            Boolean = 3,
            /// <summary>
            /// 二进制流
            /// </summary>
            Binary = 4
        }
        #endregion
    }
}
