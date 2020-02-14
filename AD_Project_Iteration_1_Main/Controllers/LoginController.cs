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
{  //whole login have two options using session object and sessionid passing .....
    //we can decide later
    public class LoginController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Login
        public ActionResult Index(UserLogin user)
        {
            return View(user);
        }
        [HttpPost]
        public ActionResult Autherize(string Username,string Password)
        {
            UserLogin user = new UserLogin();
            user.UserName = Username;
            user.Password = Password;

            string sessionId = "";

            Employee emp= LoginServices.AuthenticateUser(user);
            if (emp!=null)
            {
                sessionId = LoginServices.CreateSessionForUser(emp);
                Session["SessionId"] = sessionId;// for double protection 
                Session["USER"] = emp;//just using this thing for basic implementation 
                return RedirectToAction("RouteUser", new { sessionId });
            }
            user.LoginErrorMessage = "Login Failed --- invalid Credentials";
            return RedirectToAction("index",user);
        }
        public ActionResult Logout(string sessionId)
        {
            //just for double checking the security
           // sessionId = (string)Session["SessionId"];
            LoginServices.RemoveSession(sessionId);
            Session.Abandon();
            return RedirectToAction("index");
        }
        public ActionResult RouteUser(string sessionId)
        {
            Employee Cur_Employee = LoginServices.GetUserBySessionId(sessionId);

            //if(Cur_Employee.Emp_Id==Cur_Employee.Department.Dept_Rep_Id)
            //     return RedirectToAction("index", "DeptRep", new { sessionId });  // if he is Department Rep

            switch (Cur_Employee.Role)
            {
                case "DEPT_EMPLOYEE": return RedirectToAction("index", "Employee", new { sessionId });
                case "DEPT_HEAD": return RedirectToAction("index", "DeptHead", new { sessionId });
                case "STORE_CLERK": return RedirectToAction("index", "StoreClerk", new { sessionId });
                case "STORE_MANAGER": return RedirectToAction("index", "StoreManager", new { sessionId });
                case "STORE_SUPERVISOR": return RedirectToAction("index", "StoreSupervisor", new { sessionId });
            }
            return RedirectToAction("Logout",new { sessionId });   
        }
    }
}