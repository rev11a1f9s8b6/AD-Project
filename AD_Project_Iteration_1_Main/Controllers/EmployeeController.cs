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
    public class EmployeeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        private IStationaryRequest requestService = new StationaryRequestService();
        // GET: Employee

        public ActionResult index(string sessionId)
        {
          //  sessionId = (string)Session["SessionId"];
            ViewData["SessionId"] = sessionId;
            ViewData["USER"]= LoginServices.GetUserBySessionId(sessionId);
            return View();
        }

       
        public ActionResult MakeRequest(string sessionId,bool submitted=false)
        {
        //    sessionId = (string)Session["SessionId"];
            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId); 

            ViewData["Products"] = db.catalogues.ToList();
           
            ViewData["isSubmitted"] = submitted;
            return View();
        }       
  
        public ActionResult AddToForm(int Quantity_Needed,string Item_Num,string sessionId)
        {
            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId);

            Emp_Request RequestitionForm = requestService.CreateRequestForm(sessionId);

            requestService.AddProductsToRequest(RequestitionForm, Item_Num, Quantity_Needed);
            return RedirectToAction("MakeRequest",new { sessionId });
        }
       
        public ActionResult CheckOut(string sessionId)
        {
            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId);


            ViewData["Emp_Req_Id"] = -1;
            List<Emp_Request_Details> list2 = requestService.GetEmp_Request_Details_Temporary(sessionId);
            Emp_Request_Details temp = list2.FirstOrDefault();
            if(temp!=null)
                ViewData["Emp_Req_Id"] = temp.Emp_Req_Id;
            
            return View(list2);
        }
      
        public ActionResult Update(string sessionId,int id,int Quantity)
        {
            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId);

            requestService.UpdateReqForm(id, Quantity);
            return RedirectToAction("CheckOut", new { sessionId });
        
        }
   
        public ActionResult SubmitReqForm(string sessionId,int id)
        {

            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId);

            bool submitted = requestService.SubmitReqForm(id);
            return RedirectToAction("MakeRequest",new { sessionId ,submitted});
        }

        public ActionResult ViewRequests(string sessionId)
        {
            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId);
            return View(db.emp_Requests.Where(x=>x.Status!="STATUS_0").ToList());
        }

        public ActionResult Details(string sessionId,int id)
        {
            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId);

            ViewData["Emp_Req"] = db.emp_Requests.Where(x => x.Emp_Req_Id == id).FirstOrDefault();

            return View(db.emp_Request_Details.Where(x=>x.Emp_Req_Id==id).ToList());
        }
 
        public ActionResult UpdateMain(string sessionId, int id, int Quantity)
        {
            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId);
            
            requestService.UpdateReqForm(id, Quantity);
            Emp_Request_Details temp =db.emp_Request_Details.Where(x => x.Emp_Req_Details_Id == id).FirstOrDefault();
            if (temp == null)
            {
                //Emp_Request request=db.emp_Requests.Where(x=>x.Emp_Req_Id==)

                return RedirectToAction("ViewRequests", new { sessionId });
            }
               
            id = temp.Emp_Req_Id;
            return RedirectToAction("Details", new { sessionId ,id});

        }

        public ActionResult Delete(string sessionId,int id)
        {
            requestService.DeleteReqForm(id);
            return RedirectToAction("ViewRequests", new { sessionId });
        }
    }
}