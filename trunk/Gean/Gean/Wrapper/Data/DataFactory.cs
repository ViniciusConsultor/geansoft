/** 1. 功能：数据库公共操作类工厂
 *  2. 作者：何平 
 *  3. 创建日期：2008-3-18
 *  4. 最后修改日期：2008-10-6
**/
#region 引用命名空间
using System;
using System.Data;
using System.Configuration;
#endregion

namespace Gean.DataBase
{
    /// <summary>
    /// 数据库公共操作类的工厂，用于实例化指定的数据库公共类对象。
    /// </summary>
    public sealed class DataFactory
    {
        #region 创建数据库公共操作类的实例( 重载方法一 )
        /// <summary>
        /// 创建数据库公共操作类的实例。
        /// </summary>        
        public static IDataBase CreateProvider()
        {
            //获取配置文件中数据库公共操作类的名称
            string dbHelperName = BaseInfo.DBHelperName;

            //动态实例化数据库公共类对象
            return ReflectionHelper.CreateInstance<IDataBase>(typeof(IDataBase).Namespace + "." + dbHelperName);
        }
        #endregion

        #region 创建数据库公共操作类的实例( 重载方法二 )
        /// <summary>
        /// 创建数据库公共操作类的实例。
        /// </summary>
        /// <param name="conStr">连接字符串</param>        
        public static IDataBase CreateProvider(string conStr)
        {
            //获取配置文件中数据库公共操作类的名称
            string dbHelperName = BaseInfo.DBHelperName;

            //动态实例化数据库公共类对象
            return ReflectionHelper.CreateInstance<IDataBase>(typeof(IDataBase).Namespace + "." + dbHelperName, conStr);
        }
        #endregion
    }
}