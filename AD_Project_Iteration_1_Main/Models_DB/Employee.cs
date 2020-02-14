using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AD_Project_Iteration_1_Main.Models_DB
{
    public class Employee
    {   [Key]
        public String Emp_Id { get; set; }
        public String Full_Name { get; set; }
        
        public String User_Name { get; set; }

        public String Password { get; set; }

        public String Role { get; set; }
        //foreign key
        public String Dept_Id { get; set; }

        public String Email { get; set; }

        public String SessionId { get; set; }
        
        public virtual Department Department { get; set; }
    }
}