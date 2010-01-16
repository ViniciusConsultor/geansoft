using System;
using System.Collections.Generic;
using System.Text;
using Runner.DAL;
using Runner.DAL.Factory;
using Runner.Entity;
using System.Data;
using Newtonsoft.Json;

namespace Gean.Data.Demo
{
    class Main05_ORM
    {
        internal static void Do()
        {
            DalFactory.Initialize(AppDomain.CurrentDomain.BaseDirectory, "Runner.DAL.SQLServer");
            

            IEmployeeDataAcess acess = DalFactory.EmployeeDataAcess;


            Employee ee = new Employee();
            ee.Address = "Address";
            ee.BirthDate = DateTime.Now.AddDays(-1000);
            ee.City = "City";
            ee.Country = "Country";
            ee.EmployeeID = 33433;
            ee.Extension = "Extension";
            ee.FirstName = "FirstName";
            ee.HireDate = DateTime.Now.AddDays(444);
            ee.HomePhone = "HomePhone";
            ee.LastName = "LastName";
            ee.Notes = "Notes";
            ee.Photo = Encoding.UTF8.GetBytes("Photo");
            ee.PhotoPath = "PhotoPath";
            ee.Region = "Region";
            ee.ReportsTo = 99;
            ee.Title = "Title";
            ee.TitleOfCourtesy = "TitleOfCourtesy";



            Console.WriteLine(JsonConvert.SerializeObject(ee));
            

            Console.WriteLine(acess.ToString());

            DataSet ds = new DataSet();
        }
    }
}
