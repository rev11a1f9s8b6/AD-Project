using AD_Project_Iteration_1_Main.Models_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_Project_Iteration_1_Main.Services
{
    interface IStationaryRequest
    {
        Emp_Request CreateRequestForm(string sessionId);

        bool AddProductsToRequest(Emp_Request request,string Item_Num,int Quantity);

        List<Emp_Request_Details> GetEmp_Request_Details_Temporary(string sessionId);

        bool SubmitReqForm(int Emp_Req_Id);
        bool UpdateReqForm(int Emp_Req_Details_Id, int Quantity);

        bool DeleteReqForm(int Emp_Req_Id);


        Emp_Request ManagRequest(int Emp_Req_Id, string Comments,string type,string sessionId);
    }
}
