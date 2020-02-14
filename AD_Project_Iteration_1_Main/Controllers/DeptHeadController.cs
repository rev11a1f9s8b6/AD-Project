using AD_Project_Iteration_1_Main.Autherizaion_and_Authentication;
using AD_Project_Iteration_1_Main.Context;
using AD_Project_Iteration_1_Main.Models;
using AD_Project_Iteration_1_Main.Models_DB;
using AD_Project_Iteration_1_Main.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AD_Project_Iteration_1_Main.Controllers
{
    [AuthenticationFilter]
    public class DeptHeadController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private IStationaryRequest request = new StationaryRequestService();
        private List<SelectListItem> employeeList = new List<SelectListItem>();

        public ActionResult Index(string sessionId)
        {
            DelegationServices.ValidateDelegateStatus(sessionId);
            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId);

            return View();
        }

        public ActionResult ViewRequest(string sessionId)
        {
            DelegationServices.ValidateDelegateStatus(sessionId);
            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId);
            Employee employee = LoginServices.GetUserBySessionId(sessionId);
            return View(db.emp_Requests.Where(x => x.Employee.Dept_Id == employee.Dept_Id && x.Status == "PENDING").ToList());
        }


        public ActionResult Details(string sessionId,int id)
        {
            DelegationServices.ValidateDelegateStatus(sessionId);

            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId);

            Emp_Request temp = db.emp_Requests.Where(x => x.Emp_Req_Id == id).FirstOrDefault();
            ViewData["FormSubmittedUser"] = temp.Employee;
            ViewData["Emp_Req_Id"] = temp.Emp_Req_Id;

            return View(db.emp_Request_Details.Where(x=> x.Emp_Req_Id==id).ToList());
        }

        public ActionResult ManageRequest(string sessionId,int id,string comments,string type)
        {
            DelegationServices.ValidateDelegateStatus(sessionId);
  
            request.ManagRequest(id, comments, type, sessionId);

            return RedirectToAction("ViewRequest",new { sessionId });
        }

        public ActionResult Delegate(string sessionId,bool isDelegated=false)
        {
            DelegationServices.ValidateDelegateStatus(sessionId);

            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId);

            ViewData["IsDelegated"] = isDelegated;
            ViewData["Current_Delegate"] = DelegationServices.GetCurrentDelegate(sessionId);
            ViewData["Past_Delegates"] = DelegationServices.GetAllDelegates(sessionId);
            //getting all employees where role DEPT_EMPLOYEE
            ViewData["Dept_Employees"] = DelegationServices.GetAllEmployeesForDept(sessionId);

            return View(employeeList);
        }

        public ActionResult AddDelegation(string sessionId,DateTime start,DateTime end,string Emp_Id)
        {
            DelegationServices.ValidateDelegateStatus(sessionId);

            bool isDelegated = DelegationServices.AddDelegation(sessionId,Emp_Id,start,end);

            return RedirectToAction("Delegate",new { sessionId, isDelegated });
        }

        public ActionResult InActivateDelegate(string sessionId,int Del_Id)
        {
            DelegationServices.InActivateDelegate(Del_Id);

            return RedirectToAction("Delegate",new { sessionId });
        }

        
    }
}
