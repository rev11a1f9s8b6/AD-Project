using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using AD_Project_Iteration_1_Main.Autherizaion_and_Authentication;
using AD_Project_Iteration_1_Main.Context;
using AD_Project_Iteration_1_Main.Controllers;
using AD_Project_Iteration_1_Main.Models_DB;

namespace AD_Project_Iteration_1_Main.Services
{
    public class StationaryRequestService : IStationaryRequest
    {   
        private DatabaseContext db = new DatabaseContext();

        public bool AddProductsToRequest(Emp_Request request, string Item_Num, int Quantity)
        {   if (Quantity == 0)
                return true;
            //check if existing records are there or not
            if (db.emp_Request_Details.Any(x=>x.Emp_Req_Id==request.Emp_Req_Id && x.Item_Num == Item_Num))
            {
                Emp_Request_Details temp=db.emp_Request_Details.Where(x => x.Emp_Req_Id == request.Emp_Req_Id && x.Item_Num == Item_Num).FirstOrDefault();
                temp.Quantity += Quantity;
                db.emp_Request_Details.AddOrUpdate(temp);
                db.SaveChanges();
                return true;
            }
            //creating new Record
            Emp_Request_Details NewItem = new Emp_Request_Details();
            NewItem.Emp_Req_Id = request.Emp_Req_Id;
            NewItem.Item_Num = Item_Num;
            Catalogue item = db.catalogues.Where(x => x.Item_Num == Item_Num).FirstOrDefault();
            NewItem.Emp_Request = request;
            NewItem.Product = item;
            NewItem.Quantity = Quantity;
            db.emp_Request_Details.Add(NewItem);
            db.SaveChanges();
            return true;
            throw new NotImplementedException();
        }

        public Emp_Request CreateRequestForm(string sessionId)
        { //get the temperary request where status is 0 if there is already one ...else will get a new request with status code 0 
            Employee user = LoginServices.GetUserBySessionId(sessionId);
            if (db.emp_Requests.Any(x => x.Status == "STATUS_0" && x.Emp_Id==user.Emp_Id))
            {
                return db.emp_Requests.Where(x => x.Emp_Id == user.Emp_Id && x.Status == "STATUS_0").FirstOrDefault();
            }

            Emp_Request emp_Request = new Emp_Request();
            emp_Request.Emp_Id = user.Emp_Id;
            emp_Request.Status = "STATUS_0";
            emp_Request.Employee = db.employees.Where(x => x.Emp_Id == user.Emp_Id).FirstOrDefault();
            db.emp_Requests.Add(emp_Request);
            db.SaveChanges();
            return emp_Request;

            throw new NotImplementedException();
        }

        public bool DeleteReqForm(int Emp_Req_Id)
        {
            Emp_Request temp = db.emp_Requests.Where(x => x.Emp_Req_Id == Emp_Req_Id).FirstOrDefault();
            db.emp_Requests.Remove(temp);
            db.SaveChanges();
            return true;


            throw new NotImplementedException();
        }

        public List<Emp_Request_Details> GetEmp_Request_Details_Temporary(string sessionId)
        { 
            Employee user = LoginServices.GetUserBySessionId(sessionId);
            Emp_Request temp = db.emp_Requests.Where(x => x.Status == "STATUS_0" && x.Emp_Id == user.Emp_Id).FirstOrDefault();
            if (temp != null)
            {
                return db.emp_Request_Details.Where(x => x.Emp_Req_Id == temp.Emp_Req_Id).ToList();
            }
            else
                return new List<Emp_Request_Details>();
            throw new NotImplementedException();
        }

        public Emp_Request ManagRequest(int Emp_Req_Id, string Comments, string type, string sessionId)
        {
            Employee user = LoginServices.GetUserBySessionId(sessionId);
            Emp_Request temp = db.emp_Requests.Where(x=>x.Emp_Req_Id == Emp_Req_Id).FirstOrDefault();
            if (type == "ACCEPT")
            {
                temp.Status = "ACCEPTED";
            }
            else
            {
                temp.Status = "REJECTED";
            }
            temp.Date_Approved = DateTime.Now;
            temp.Approved_By = user.Full_Name;
            temp.Comment = Comments;
            db.emp_Requests.AddOrUpdate(temp);
            db.SaveChanges();
            return temp;

            throw new NotImplementedException();
        }

        public bool SubmitReqForm(int Emp_Req_Id)
        {
            
            Emp_Request temp= db.emp_Requests.Where(x => x.Emp_Req_Id==Emp_Req_Id).FirstOrDefault();
            if (temp != null)
            {
               temp.Status = "PENDING";
                db.emp_Requests.AddOrUpdate(temp);
                db.SaveChanges();
                return true;
            }
            return false;
           

            throw new NotImplementedException();
        }

        public bool UpdateReqForm(int Emp_Req_Details_Id, int Quantity)
        {
            
            Emp_Request_Details temp = db.emp_Request_Details.Where(x => x.Emp_Req_Details_Id == Emp_Req_Details_Id).FirstOrDefault();
            
            if (Quantity == 0)
            {
                db.emp_Request_Details.Remove(temp);
                db.SaveChanges();
                return true;
            }
           
               temp.Quantity = Quantity;
               db.emp_Request_Details.AddOrUpdate(temp);
               db.SaveChanges();
            return true;

            throw new NotImplementedException();
        }
        
    }
}