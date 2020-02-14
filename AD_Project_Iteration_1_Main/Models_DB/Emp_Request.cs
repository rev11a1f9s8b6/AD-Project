using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AD_Project_Iteration_1_Main.Models_DB
{
    public class Emp_Request
    {   [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Emp_Req_Id { get; set; }
        //foreign Key 
        public String Emp_Id { get; set; }
        public String  Approved_By { get; set; }
        public DateTime? Date_Approved { get; set; }
        public String Recieved_By { get; set; }
        public DateTime? Date_Recieved { get; set; }
        public String Comment { get; set; }
        public String Status { get; set; }

        public virtual Employee Employee { get; set; }
      //  public virtual List<Emp_Request_Details> Emp_Request_Details { get; set; }

    }
}