
using AD_Project_Iteration_1_Main.Models_DB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace AD_Project_Iteration_1_Main.Context
{
    public class DatabaseContext:DbContext
    {
        public DbSet<Employee> employees { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Catalogue> catalogues { get; set; }
        public DbSet<Emp_Request> emp_Requests { get; set; }
        public DbSet<Emp_Request_Details> emp_Request_Details { get; set; }
        public DbSet<Emp_Delegation> emp_Delegations { get; set; }
        public DatabaseContext() : base("Server=.; Database=AD_Project; Integrated Security = True")
        {
            Database.SetInitializer(new InventoryDbInitializer<DatabaseContext>());
        }

        private class InventoryDbInitializer<T> : DropCreateDatabaseIfModelChanges<DatabaseContext>
        {
            protected override void Seed(DatabaseContext context)
            {
               //Loading the test Data in the static Functions
                base.Seed(DataLoader.LoadData(context));
            }
        }
    }
}