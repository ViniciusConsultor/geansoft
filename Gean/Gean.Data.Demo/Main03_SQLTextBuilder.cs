using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Gean.Data.Demo
{
    public class Main03_SQLTextBuilder
    {
        internal static void Do()
        {
            SQLConditionTextBuilder ctb = new SQLConditionTextBuilder();

            SQLConditionTextBuilder b1 = new SQLConditionTextBuilder();
            b1.Set(CompareOperation.Equal, DbType.String, "name", "%kkp%");

            SQLConditionTextBuilder b2 = new SQLConditionTextBuilder();
            b2.Set(CompareOperation.Like, DbType.String, "nickName", "%'kkp'%");

            SQLConditionTextBuilder b3 = new SQLConditionTextBuilder();
            b3.Set(CompareOperation.Equal, DbType.Date, "age", DateTime.Now);

            SQLConditionTextBuilder b4 = new SQLConditionTextBuilder();
            b4.Set(CompareOperation.Equal, DbType.DateTime, "signTime", DateTime.Now);

            SQLConditionTextBuilder b5 = new SQLConditionTextBuilder();
            b5.Set(CompareOperation.MoreThan, DbType.Int32, "count", 88888);

            SQLConditionTextBuilder b51 = new SQLConditionTextBuilder();
            b51.Set(CompareOperation.MoreThan, DbType.Decimal, "length", 88888);

            SQLConditionTextBuilder b52 = new SQLConditionTextBuilder();
            b52.Set(CompareOperation.MoreThan, DbType.Double, "price", 888.88);

            b1.AddCondition(LogicOperation.And, b2);
            b2.AddCondition(LogicOperation.And, b3);
            b3.AddCondition(LogicOperation.And, b4);
            b4.AddCondition(LogicOperation.And, b5);
            b5.AddCondition(LogicOperation.And, b51);
            b5.AddCondition(LogicOperation.And, b52);

            ctb.AddCondition(LogicOperation.Or, b1);

            Console.WriteLine(ctb.ToSqlText());
            Console.WriteLine();
            Console.WriteLine(ctb.ToSqlTempletText());

            Console.WriteLine();
            SqlParameter[] sqls = ctb.GetSqlParameters();
            foreach (var item in sqls)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
