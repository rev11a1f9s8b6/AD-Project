using AD_Project_Iteration_1_Main.Models_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AD_Project_Iteration_1_Main.Models
{
    public class DropDownEmployee
    {
        public Employee employee { get; set; }
        public bool isSelected { get; set; }

        public DropDownEmployee()
        {
            isSelected = false;
        }
    }
}