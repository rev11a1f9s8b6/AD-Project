using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AD_Project_Iteration_1_Main.Models_DB
{
    public class Department
    {   [Key]
        public String Dept_id { get; set; }

        public String Dep_Name{ get; set; }
        
        public String CollectionPoint { get; set; }
        //foreign key
        
        public String Dept_Head_Id { get; set; }
        //foreign key
        
        public String Dept_Rep_Id { get; set; }
        
        public String Phone { get; set; }

        public virtual List<Employee> Employees { get; set; }
        
        public Department()
        {
            Employees = new List<Employee>();
        }
      
    }
}