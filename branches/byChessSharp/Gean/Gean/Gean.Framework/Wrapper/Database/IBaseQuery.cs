#region 引用命名空间
using System;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;
#endregion

namespace Gean.Data
{
    /// <summary>
    /// 数据库查询基础接口
    /// </summary>
    public interface IBaseQuery
    {
        #region 属性

        #region 返回的字段列表( 查询专用 )
        /// <summary>
        /// 返回的字段列表( 查询专用 )
        /// </summary>
        string ColumnNames
        {
            get;
            set;
        }
        #endregion

        #region 排序字段列表( 查询专用 )
        /// <summary>
        /// 排序字段列表( 查询专用 ),范例： "field1,field2 desc,field3"
        /// </summary>
        string SortColumnNames
        {
            get;
            set;
        }
        #endregion

        #endregion

        #region 添加Select子句
        /// <summary>
        /// 添加Select子句( 查询专用 )
        /// </summary>
        /// <param name="tableNames">表名</param>
        /// <param name="pkColumnName">主键字段名</param>
        void AddSelect(string tableNames, string pkColumnName);

        /// <summary>
        /// 添加Select子句( 查询专用 )
        /// </summary>
        /// <param name="tableNames">表名</param>
        /// <param name="pkColumnName">主键字段名</param>
        /// <param name="columnNames">要返回的字段列表,如果未传入值，则默认为"*",
        /// 注意：字段列表中不允许存在列名为"RowNumber"的字段</param>
        void AddSelect(string tableNames, string pkColumnName, string columnNames);
        #endregion

        #region 添加Where子句

        #region SQL参数化方式

        #region 主函数

        #region 重载1
        /// <summary>
        /// 添加Where子句( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="controlType">输入控件的类型</param>
        /// <param name="ignoreValue">用于忽略默认值，当该参数与value相同，则对该条件忽略。
        /// 范例：value参数为dropDownList.SelectedValue,ignoreValue参数为0,当value=0时，则被忽略。</param>
        /// <param name="selected">该参数仅用于单选框和复选框控件，当该参数为true,则添加条件.
        /// 范例：checkbox.Checked</param>
        /// <param name="queryOperator">查询运算符</param>
        /// <param name="pattern">模糊查询的模式字符串,必须使用{0}表示输入值，默认形式为 '%{0}%'</param>
        /// <param name="unSelectedValue">该参数仅用于复选框，如果为未选中状态，使用该值添加条件</param>
        void AddWhere(string columnName, string value, DbType dbType, DbEnums.ControlType controlType, string ignoreValue, bool selected,
            DbEnums.QueryOperator queryOperator, string pattern, string unSelectedValue);
        #endregion

        #region 重载2
        /// <summary>
        /// 添加Where子句( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="controlType">输入控件的类型</param>        
        /// <param name="queryOperator">查询运算符</param>
        /// <param name="pattern">模糊查询的模式字符串,必须使用{0}表示输入值，默认形式为 '%{0}%'</param>
        void AddWhere(string columnName, string value, DbType dbType, DbEnums.ControlType controlType, DbEnums.QueryOperator queryOperator, string pattern);
        #endregion

        #region 重载3
        /// <summary>
        /// 添加Where子句( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="controlType">输入控件的类型</param>        
        /// <param name="queryOperator">查询运算符</param>
        void AddWhere(string columnName, string value, DbType dbType, DbEnums.ControlType controlType, DbEnums.QueryOperator queryOperator);
        #endregion

        #region 重载4
        /// <summary>
        /// 添加Where子句( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="controlType">输入控件的类型</param>        
        void AddWhere(string columnName, string value, DbType dbType, DbEnums.ControlType controlType);
        #endregion

        #region 重载5
        /// <summary>
        /// 添加Where子句( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="controlType">输入控件的类型</param>
        /// <param name="ignoreValue">用于忽略默认值，当该参数与value相同，则对该条件忽略。
        /// 范例：value参数为dropDownList.SelectedValue,ignoreValue参数为0,当value=0时，则被忽略。</param>
        /// <param name="queryOperator">查询运算符</param>
        /// <param name="pattern">模糊查询的模式字符串,必须使用{0}表示输入值，默认形式为 '%{0}%'</param>
        void AddWhere(string columnName, string value, DbType dbType, DbEnums.ControlType controlType, string ignoreValue,
            DbEnums.QueryOperator queryOperator, string pattern);
        #endregion

        #region 重载6
        /// <summary>
        /// 添加Where子句( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="controlType">输入控件的类型</param>
        /// <param name="ignoreValue">用于忽略默认值，当该参数与value相同，则对该条件忽略。
        /// 范例：value参数为dropDownList.SelectedValue,ignoreValue参数为0,当value=0时，则被忽略。</param>
        /// <param name="queryOperator">查询运算符</param>
        void AddWhere(string columnName, string value, DbType dbType, DbEnums.ControlType controlType, string ignoreValue, DbEnums.QueryOperator queryOperator);
        #endregion

        #region 重载7
        /// <summary>
        /// 添加Where子句( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="controlType">输入控件的类型</param>
        /// <param name="ignoreValue">用于忽略默认值，当该参数与value相同，则对该条件忽略。
        /// 范例：value参数为dropDownList.SelectedValue,ignoreValue参数为0,当value=0时，则被忽略。</param>
        void AddWhere(string columnName, string value, DbType dbType, DbEnums.ControlType controlType, string ignoreValue);
        #endregion

        #region 重载8
        /// <summary>
        /// 添加Where子句( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="controlType">输入控件的类型</param>
        /// <param name="selected">该参数仅用于单选框和复选框控件，当该参数为true,则添加条件.
        /// 范例：checkbox.Checked</param>
        /// <param name="queryOperator">查询运算符</param>
        /// <param name="pattern">模糊查询的模式字符串,必须使用{0}表示输入值，默认形式为 '%{0}%'</param>
        void AddWhere(string columnName, string value, DbType dbType, DbEnums.ControlType controlType, bool selected,
            DbEnums.QueryOperator queryOperator, string pattern);
        #endregion

        #region 重载9
        /// <summary>
        /// 添加Where子句( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="controlType">输入控件的类型</param>
        /// <param name="selected">该参数仅用于单选框和复选框控件，当该参数为true,则添加条件.
        /// 范例：checkbox.Checked</param>
        void AddWhere(string columnName, string value, DbType dbType, DbEnums.ControlType controlType, bool selected);
        #endregion

        #region 重载10
        /// <summary>
        /// 添加Where子句( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="controlType">输入控件的类型</param>
        /// <param name="selected">该参数仅用于单选框和复选框控件，当该参数为true,则添加条件.
        /// 范例：checkbox.Checked</param>
        /// <param name="unSelectedValue">该参数仅用于复选框，如果为未选中状态，使用该值添加条件</param>
        /// <param name="queryOperator">查询运算符</param>
        /// <param name="pattern">模糊查询的模式字符串,必须使用{0}表示输入值，默认形式为 '%{0}%'</param>
        void AddWhere(string columnName, string value, DbType dbType, DbEnums.ControlType controlType, bool selected, string unSelectedValue,
            DbEnums.QueryOperator queryOperator, string pattern);
        #endregion

        #region 重载11
        /// <summary>
        /// 添加Where子句( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="controlType">输入控件的类型</param>
        /// <param name="selected">该参数仅用于单选框和复选框控件，当该参数为true,则添加条件.
        /// 范例：checkbox.Checked</param>
        /// <param name="unSelectedValue">该参数仅用于复选框，如果为未选中状态，使用该值添加条件</param>
        void AddWhere(string columnName, string value, DbType dbType, DbEnums.ControlType controlType, bool selected, string unSelectedValue);
        #endregion

        #endregion

        #region 添加从文本框输入的Where条件

        #region 重载1
        /// <summary>
        /// 添加从文本框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="queryOperator">查询运算符</param>
        /// <param name="pattern">模糊查询的模式字符串,必须使用{0}表示输入值，默认形式为 '%{0}%'</param>
        void AddWhereByTextBox(string columnName, string value, DbType dbType, DbEnums.QueryOperator queryOperator, string pattern);
        #endregion

        #region 重载2
        /// <summary>
        /// 添加从文本框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="queryOperator">查询运算符</param>
        void AddWhereByTextBox(string columnName, string value, DbType dbType, DbEnums.QueryOperator queryOperator);
        #endregion

        #region 重载3
        /// <summary>
        /// 添加从文本框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="queryOperator">查询运算符</param>
        /// <param name="pattern">模糊查询的模式字符串,必须使用{0}表示输入值，默认形式为 '%{0}%'</param>
        void AddWhereByTextBox(string columnName, string value, DbEnums.QueryOperator queryOperator, string pattern);
        #endregion

        #region 重载4
        /// <summary>
        /// 添加从文本框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="queryOperator">查询运算符</param>
        void AddWhereByTextBox(string columnName, string value, DbEnums.QueryOperator queryOperator);
        #endregion

        #region 重载5
        /// <summary>
        /// 添加从文本框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        void AddWhereByTextBox(string columnName, string value, DbType dbType);
        #endregion

        #region 重载6
        /// <summary>
        /// 添加从文本框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        void AddWhereByTextBox(string columnName, string value);
        #endregion

        #endregion

        #region 添加从下拉列表框输入的Where条件

        #region 重载1
        /// <summary>
        /// 添加从下拉列表框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="ignoreValue">用于忽略默认值，当该参数与value相同，则对该条件忽略。
        /// 范例：value参数为dropDownList.SelectedValue,ignoreValue参数为0,当value=0时，则被忽略。</param>
        /// <param name="queryOperator">查询运算符</param>
        /// <param name="pattern">模糊查询的模式字符串,必须使用{0}表示输入值，默认形式为 '%{0}%'</param>
        void AddWhereByDDL(string columnName, string value, DbType dbType, string ignoreValue, DbEnums.QueryOperator queryOperator, string pattern);
        #endregion

        #region 重载2
        /// <summary>
        /// 添加从下拉列表框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="ignoreValue">用于忽略默认值，当该参数与value相同，则对该条件忽略。
        /// 范例：value参数为dropDownList.SelectedValue,ignoreValue参数为0,当value=0时，则被忽略。</param>
        /// <param name="queryOperator">查询运算符</param>
        void AddWhereByDDL(string columnName, string value, DbType dbType, string ignoreValue, DbEnums.QueryOperator queryOperator);
        #endregion

        #region 重载3
        /// <summary>
        /// 添加从下拉列表框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="ignoreValue">用于忽略默认值，当该参数与value相同，则对该条件忽略。
        /// 范例：value参数为dropDownList.SelectedValue,ignoreValue参数为0,当value=0时，则被忽略。</param>
        void AddWhereByDDL(string columnName, string value, DbType dbType, string ignoreValue);
        #endregion

        #region 重载4
        /// <summary>
        /// 添加从下拉列表框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="ignoreValue">用于忽略默认值，当该参数与value相同，则对该条件忽略。
        /// 范例：value参数为dropDownList.SelectedValue,ignoreValue参数为0,当value=0时，则被忽略。</param>
        void AddWhereByDDL(string columnName, string value, string ignoreValue);
        #endregion

        #endregion

        #region 添加从单选框输入的Where条件

        #region 重载1
        /// <summary>
        /// 添加从单选框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="selected">该参数仅用于单选框和复选框控件，当该参数为true,则添加条件.
        /// 范例：radioBtn.Checked</param>
        /// <param name="queryOperator">查询运算符</param>
        /// <param name="pattern">模糊查询的模式字符串,必须使用{0}表示输入值，默认形式为 '%{0}%'</param>
        void AddWhereByRadioBtn(string columnName, string value, DbType dbType, bool selected,
            DbEnums.QueryOperator queryOperator, string pattern);
        #endregion

        #region 重载2
        /// <summary>
        /// 添加从单选框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="selected">该参数仅用于单选框和复选框控件，当该参数为true,则添加条件.
        /// 范例：radioBtn.Checked</param>
        void AddWhereByRadioBtn(string columnName, string value, DbType dbType, bool selected);
        #endregion

        #endregion

        #region 添加从复选框输入的Where条件

        #region 重载1
        /// <summary>
        /// 添加从复选框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="selected">该参数仅用于单选框和复选框控件，当该参数为true,则添加条件.
        /// 范例：checkbox.Checked</param>
        /// <param name="unSelectedValue">该参数仅用于复选框，如果为未选中状态，使用该值添加条件</param>
        /// <param name="queryOperator">查询运算符</param>
        /// <param name="pattern">模糊查询的模式字符串,必须使用{0}表示输入值，默认形式为 '%{0}%'</param>
        void AddWhereByCheckBox(string columnName, string value, DbType dbType, bool selected, string unSelectedValue,
            DbEnums.QueryOperator queryOperator, string pattern);
        #endregion

        #region 重载2
        /// <summary>
        /// 添加从复选框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="selected">该参数仅用于单选框和复选框控件，当该参数为true,则添加条件.
        /// 范例：checkbox.Checked</param>
        /// <param name="unSelectedValue">该参数仅用于复选框，如果为未选中状态，使用该值添加条件</param>
        void AddWhereByCheckBox(string columnName, string value, DbType dbType, bool selected, string unSelectedValue);
        #endregion

        #region 重载3
        /// <summary>
        /// 添加从复选框输入的Where条件( 查询专用 ),自动创建参数化SQL防注入攻击
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="value">输入值( 用户输入的数据 )</param>
        /// <param name="dbType">输入值的数据类型</param>
        /// <param name="selected">该参数仅用于单选框和复选框控件，当该参数为true,则添加条件.
        /// 范例：checkbox.Checked</param>
        void AddWhereByCheckBox(string columnName, string value, DbType dbType, bool selected);
        #endregion

        #endregion

        #endregion

        #region SQL拼接字符串方式
        /// <summary>
        /// 拼接字符串方式添加Where子句( 查询专用 ),
        /// 注意：如果对SQL安全要求较高，应使用AddWhere方法
        /// </summary>
        /// <param name="condition">条件字符串,不要添加 and 关键字,范例: column1 = 'value1'</param>
        void AddFilter(string condition);

        /// <summary>
        /// 拼接字符串方式添加条件子句( 查询专用 ),专用于TextBox控件，条件中必须使用模式字符{0}.
        /// 注意：如果对SQL安全要求较高，应使用AddWhereByTextBox方法
        /// </summary>
        /// <param name="condition">条件字符串,不要加and,使用模式字符{0}表示变量。形式： "列名 运算符 {0}"。
        /// 范例1："Column > '{0}'" ,范例2："Column like '%{0}%'"</param>
        /// <param name="value">TextBox控件的值，范例：TextBox1.Text</param>
        void AddFilterByTextBox(string condition, string value);

        /// <summary>
        /// 拼接字符串方式添加条件子句( 查询专用 ),专用于DropDownList控件，条件中必须使用模式字符{0}.
        /// 注意：如果对SQL安全要求较高，应使用AddWhereByDDL方法
        /// </summary>
        /// <param name="condition">条件字符串,不要加and,使用模式字符{0}表示变量。形式： "列名 运算符 {0}
        ///  范例1："Column > '{0}'" ,范例2："Column like '%{0}%'"</param>
        /// <param name="value">DropDownList控件的值，范例：dropDownList.SelectedValue</param>
        /// <param name="ignoreValue">用于忽略默认选择项，当该参数与value参数相同，则对该条件忽略。
        /// 范例：value参数为dropDownList.SelectedValue,ignoreValue参数为0,当两个参数相同，表示被忽略。</param>
        void AddFilterByDDL(string condition, string value, string ignoreValue);

        /// <summary>
        /// 拼接字符串方式添加条件子句( 查询专用 ),专用于RadioButton控件,
        /// 注意：如果对SQL安全要求较高，应使用AddWhereByRadioBtn方法
        /// </summary>
        /// <param name="condition">条件字符串,不要加and</param>
        /// <param name="selected">当该参数为true,则添加条件.范例：radioBtn.Checked</param>
        void AddFilterByRadioBtn(string condition, bool selected);

        /// <summary>
        /// 拼接字符串方式添加条件子句( 查询专用 ),专用于CheckBox控件,
        /// 注意：如果对SQL安全要求较高，应使用AddWhereByCheckBox方法
        /// </summary>
        /// <param name="condition">条件字符串,不要加and</param>
        /// <param name="selected">当该参数为true,则添加条件.范例：checkbox.Checked</param>
        void AddFilterByCheckBox(string condition, bool selected);
        #endregion

        #endregion

        #region 添加Group子句
        /// <summary>
        /// 添加Group子句( 查询专用 )
        /// </summary>
        /// <param name="columnNames">用于分组的字段列表</param>
        void AddGroup(string columnNames);
        #endregion
    }
}
