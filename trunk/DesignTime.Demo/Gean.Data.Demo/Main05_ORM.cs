using System;
using System.Collections.Generic;
using System.Text;
using Runner.DAL;
using Runner.DAL.Factory;

namespace Gean.Data.Demo
{
    class Main05_ORM
    {
        internal static void Do()
        {
            DalFactory.Initialize(AppDomain.CurrentDomain.BaseDirectory, "Runner.DAL.SQLServer");
            IEmployeeDataAcess acess = DalFactory.EmployeeDataAcess;

            
            Console.WriteLine(acess.ToString());
        }
    }
}
