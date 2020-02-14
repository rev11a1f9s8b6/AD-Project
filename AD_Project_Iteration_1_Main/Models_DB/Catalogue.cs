using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AD_Project_Iteration_1_Main.Models_DB
{
    public class Catalogue
    {
        [Key]
        public String Item_Num { get; set; }
        public String Category { get; set; }
        public String Description { get; set; }

        public int?  Reorder_Level { get; set; }
        public int? Reorder_Quantity { get; set; }
        public int? Predicted_Quantity { get; set; }
        public int? Available_Quantity { get; set; }
        public String UOM { get; set; }
        public int? Price { get; set; }

        public String Bin_Number { get; set; }
        public String Supplier_1 { get; set; }
        public String Supplier_2 { get; set; }

        public String Supplier_3 { get; set; }

    }
}