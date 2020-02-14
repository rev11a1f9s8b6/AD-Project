using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AD_Project_Iteration_1_Main.Models_DB
{
    public class Emp_Request_Details
    {   [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Emp_Req_Details_Id { get; set; }
        public int Emp_Req_Id { get; set; }
        public String Item_Num { get; set; }
        public int Quantity { get; set; }
        public String Remarks { get; set; }

        public virtual Catalogue Product { get; set; }
        public virtual Emp_Request Emp_Request { get; set; }
    }
}