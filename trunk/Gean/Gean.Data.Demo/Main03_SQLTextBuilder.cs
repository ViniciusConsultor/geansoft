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
            b1.Set("name", CompareOperation.Equal, DbType.String, "%kkp%");

            SQLWhereTextBuilder b2 = new SQLWhereTextBuilder();
            b2.Set("nickName", CompareOperation.Like, DbType.String, "%'kkp'%");

            SQLWhereTextBuilder b3 = new SQLWhereTextBuilder();
            b3.Set("age", CompareOperation.Equal, DbType.Date, DateTime.Now);

            SQLWhereTextBuilder b4 = new SQLWhereTextBuilder();
            b4.Set("signTime", CompareOperation.Equal, DbType.DateTime, DateTime.Now);

            SQLWhereTextBuilder b5 = new SQLWhereTextBuilder();
            b5.Set("count", CompareOperation.MoreThan, DbType.Int32, 88888);

            SQLWhereTextBuilder b51 = new SQLWhereTextBuilder();
            b51.Set("length", CompareOperation.MoreThan, DbType.Decimal, 88888);

            SQLWhereTextBuilder b52 = new SQLWhereTextBuilder();
            b52.Set("price", CompareOperation.MoreThan, DbType.Double, 888.88);

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

            Console.WriteLine("............................");

            SQLFieldTextBuilder ftb = new SQLFieldTextBuilder();
            ftb.Set("a", "b", "c", "d");
            Console.WriteLine(ftb.ToSqlText());

            ftb = new SQLFieldTextBuilder();
            ftb.Set(
                new Pair<string, string>("aaa", "AAA"),
                new Pair<string, string>("bbb", "BBB"),
                new Pair<string, string>("ccc", "CCC")
                );
            ftb.Set(
                new Pair<string, string>("ddd", "DDD"),
                new Pair<string, string>("eee", "EEE")
                );
            Console.WriteLine(ftb.ToSqlText());
        }
    }
}
