using AD_Project_Iteration_1_Main.Models_DB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace AD_Project_Iteration_1_Main.Context
{
    public class DataLoader
    {
     public static void LoadProducts()
        {
            string query;
            var fileStream = new FileStream(@"C:\Users\65911\Documents\GDipSA Modules\AD Project\AD_Iteration_withLogin\AD_Project_Iteration_1_Main\Context\Products.sql", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))  
            {
                query = streamReader.ReadToEnd();
            }

            using (SqlConnection connection = new SqlConnection("Server=.; Database=AD_Project; Integrated Security = True"))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    int result = command.ExecuteNonQuery();

                    // Check Error
                    if (result < 0)
                        Console.WriteLine("Error inserting data into Database!");
                }

            }
        }
            public static DatabaseContext LoadData(DatabaseContext context)
            {
     
            DataLoader.LoadProducts();
            Employee Test1 = new Employee()
            {
                Emp_Id = "EMP01",
                User_Name = "khprasad123",
                Password = "123",
                Email = "Khprasad123@gmail.com",
                Role = "DEPT_EMPLOYEE",
                Full_Name = "Hariprasad",
            };


            //Department Head 
            Employee Test2 = new Employee()
            {
                Emp_Id = "EMP02",
                User_Name = "logu",
                Password = "123",
                Email = "logu@gmail.com",
                Role = "DEPT_HEAD",
                Full_Name = "Logu Rajeswari",

            };

            Employee Test3 = new Employee()
            {
                Emp_Id = "EMP03",
                User_Name = "chad",
                Password = "123",
                Email = "chad@gmail.com",
                Role = "DEPT_EMPLOYEE",
                Full_Name = "Chad Gosh Coder",

            };

            Employee Test4 = new Employee()
            {
                Emp_Id = "EMP04",
                User_Name = "alvin",
                Password = "123",
                Email = "alvin@gmail.com",
                Role = "STORE_CLERK",
                Full_Name = "Alvin Thalaiva",
            };
            Employee Test5 = new Employee()
            {
                Emp_Id = "EMP05",
                User_Name = "anu",
                Password = "123",
                Email = "anu@gmail.com",
                Role = "STORE_MANAGER",
                Full_Name = "Annapoorni Mahadevan",
            };
            Employee Test6 = new Employee()
            {
                Emp_Id = "EMP06",
                User_Name = "daniel",
                Password = "123",
                Email = "daniel@gmail.com",
                Role = "STORE_SUPERVISOR",
                Full_Name = "Daniel The Coder",
            };
            Employee Test7 = new Employee()
            {
                Emp_Id = "EMP07",
                User_Name = "ariel",
                Password = "123",
                Email = "ariel@gmail.com",
                Role = "DEPT_EMPLOYEE",
                Full_Name = "Ariel the Angel",
            };

            Employee Test8 = new Employee()
            {
                Emp_Id = "EMP08",
                User_Name = "logesh",
                Password = "123",
                Email = "logesh@gmail.com",
                Role = "DEPT_EMPLOYEE",
                Full_Name = "Lokesh Android King",
            };

            context.employees.AddOrUpdate(Test1);
            context.employees.AddOrUpdate(Test2);
            context.employees.AddOrUpdate(Test3);
            context.employees.AddOrUpdate(Test4);
            context.employees.AddOrUpdate(Test5);
            context.employees.AddOrUpdate(Test6);
            context.employees.AddOrUpdate(Test7);
            context.employees.AddOrUpdate(Test8);
            context.SaveChanges();

            Department Dept = new Department()
            {
                Dep_Name = "CSE",
                Dept_id = "DEP001",
                Phone = "234324234",
                CollectionPoint = "Morning 6 30 At PlayGround",
                Dept_Head_Id = Test2.Emp_Id,    // Employee logu is the Department Head
                Dept_Rep_Id = Test3.Emp_Id    // as of now chad is the department rep
            };

            //ASSSIGNED CHAD AS THE DEFAULT DEPARTMENT REP

            Test1.Department = Dept; Test1.Dept_Id = Dept.Dept_id;
            Test2.Department = Dept; Test2.Dept_Id = Dept.Dept_id;
            Test3.Department = Dept; Test3.Dept_Id = Dept.Dept_id;
            Test4.Department = Dept; Test4.Dept_Id = Dept.Dept_id;
            Test5.Department = Dept; Test5.Dept_Id = Dept.Dept_id;
            Test6.Department = Dept; Test6.Dept_Id = Dept.Dept_id;
            Test7.Department = Dept; Test7.Dept_Id = Dept.Dept_id;
            Test8.Department = Dept; Test8.Dept_Id = Dept.Dept_id;




            Dept.Employees.Add(Test1);
            Dept.Employees.Add(Test2);
            Dept.Employees.Add(Test3);
            Dept.Employees.Add(Test4);
            Dept.Employees.Add(Test5);
            Dept.Employees.Add(Test6);
            Dept.Employees.Add(Test7);
            Dept.Employees.Add(Test8);


            context.employees.AddOrUpdate(Test1);
            context.employees.AddOrUpdate(Test2);
            context.employees.AddOrUpdate(Test3);
            context.employees.AddOrUpdate(Test4);
            context.employees.AddOrUpdate(Test5);
            context.employees.AddOrUpdate(Test6);
            context.employees.AddOrUpdate(Test7);
            context.employees.AddOrUpdate(Test8);

            context.departments.AddOrUpdate(Dept);
            context.SaveChanges();



            return context;
        }
    }
}