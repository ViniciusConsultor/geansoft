using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Common;

namespace Gean.Data.Demo
{
    public class Main03_SQLTextBuilder
    {
        internal static void Do()
        {
            SQLWhereTextBuilder ctb = new SQLWhereTextBuilder();

            SQLWhereTextBuilder b1 = new SQLWhereTextBuilder();
            b1.Set(CompareOperation.Equal, DbType.String, "name", "%kkp%");

            SQLWhereTextBuilder b2 = new SQLWhereTextBuilder();
            b2.Set(CompareOperation.Like, DbType.String, "nickName", "%'kkp'%");

            SQLWhereTextBuilder b3 = new SQLWhereTextBuilder();
            b3.Set(CompareOperation.Equal, DbType.Date, "age", DateTime.Now);

            SQLWhereTextBuilder b4 = new SQLWhereTextBuilder();
            b4.Set(CompareOperation.Equal, DbType.DateTime, "signTime", DateTime.Now);

            SQLWhereTextBuilder b5 = new SQLWhereTextBuilder();
            b5.Set(CompareOperation.MoreThan, DbType.Int32, "count", 88888);

            SQLWhereTextBuilder b51 = new SQLWhereTextBuilder();
            b51.Set(CompareOperation.MoreThan, DbType.Decimal, "length", 88888);

            SQLWhereTextBuilder b52 = new SQLWhereTextBuilder();
            b52.Set(CompareOperation.MoreThan, DbType.Double, "price", 888.88);

            b1.Add(LogicOperation.And, b2);
            b2.Add(LogicOperation.And, b3);
            b3.Add(LogicOperation.And, b4);
            b4.Add(LogicOperation.And, b5);
            b5.Add(LogicOperation.And, b51);
            b5.Add(LogicOperation.And, b52);

            ctb.Add(LogicOperation.Or, b1);

            Console.WriteLine(ctb.ToSqlText());
            Console.WriteLine();
            Console.WriteLine(ctb.ToSqlTempletText());

            Console.WriteLine();
            DbParameter[] sqls = ctb.GetDbParameters();
            foreach (var item in sqls)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
