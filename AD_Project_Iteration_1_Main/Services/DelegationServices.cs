using AD_Project_Iteration_1_Main.Autherizaion_and_Authentication;
using AD_Project_Iteration_1_Main.Context;
using AD_Project_Iteration_1_Main.Models_DB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace AD_Project_Iteration_1_Main.Services
{
    public class DelegationServices
    {
        public static DatabaseContext db = new DatabaseContext();

        public static Employee GetCurrentDelegate(string sessionId)
        {
            DelegationServices.ValidateDelegateStatus(sessionId);
            var cur = DateTime.Now;
            var user = LoginServices.GetUserBySessionId(sessionId);
            var list = db.emp_Delegations.Where(x => cur >= x.Start_Date && cur >= x.Start_Date && cur <= x.End_Date && x.Employee.Department.Dept_id == user.Dept_Id && x.Status=="ACTIVE").FirstOrDefault();
            if(list==null)
                return null;
            return list.Employee; 
        }

        public static List<Emp_Delegation> GetAllDelegates(string sessionId)
        {
            var Dept_Head = LoginServices.GetUserBySessionId(sessionId);
            return db.emp_Delegations.Where(x => x.Employee.Dept_Id == Dept_Head.Department.Dept_id).ToList();
        }

        public static void ValidateDelegateStatus(string sessionId)
        {
            var user = LoginServices.GetUserBySessionId(sessionId);
            var list = db.emp_Delegations.Where(x => x.Employee.Dept_Id == user.Dept_Id).ToList(); //have all the delegations for a particular department

            foreach(Emp_Delegation x in list)
            {
                if (x.End_Date < DateTime.Now)
                {
                    x.Status = "INACTIVE";
                    db.emp_Delegations.AddOrUpdate(x);
                    db.SaveChanges();
                }
            }
        }

        public static bool CheckIsDelegated(string sessionId)
        {
            DelegationServices.ValidateDelegateStatus(sessionId);
            var User = LoginServices.GetUserBySessionId(sessionId);

            if (db.emp_Delegations.Any(x => x.Emp_Id == User.Emp_Id && x.Status == "ACTIVE")){
                return true;
            }
            //Not Delegated For Particular Time Period
            return false;
        }

        public static bool AddDelegation(string sessionId,string Emp_Id,DateTime start,DateTime end)
        {
            if (Emp_Id == null || Emp_Id =="")
                return false;
            if (!DelegationServices.IsDateValid(start, end))
                    return false;

            if (!DelegationServices.CheckDelegationPossibleOrNot(sessionId,start,end))
                return false;
            //if no one in the range the new Delegate can be added for that date range 
            Emp_Delegation temp = new Emp_Delegation();
            //db is a DAO. 
            temp.Employee = db.employees.Where(x => x.Emp_Id == Emp_Id).FirstOrDefault(); 
            temp.Emp_Id = Emp_Id;
            temp.Start_Date = start;
            temp.End_Date = end;
            temp.Status = "ACTIVE";
            db.emp_Delegations.AddOrUpdate(temp);
            db.SaveChanges();   
            return true;
        }

        public static bool InActivateDelegate(int Del_Id)
        {
            Emp_Delegation temp = db.emp_Delegations.Where(x => x.Delegate_Id == Del_Id).FirstOrDefault();
            temp.Status = "INACTIVE";
            db.emp_Delegations.AddOrUpdate(temp);
            db.SaveChanges();
            return true;
        }

   

        public static bool CheckDelegationPossibleOrNot(string sessionId,DateTime start,DateTime end)
        {
            var user = LoginServices.GetUserBySessionId(sessionId);
            //checking whether any person allocated in that date range
            var listOfActiveDelegates = db.emp_Delegations.Where(x => x.Employee.Department.Dept_id == user.Dept_Id && 
                                        x.Status == "ACTIVE");

            if (listOfActiveDelegates.Any(x => start >= x.Start_Date && start <= x.End_Date))
                return false;
            if (listOfActiveDelegates.Any(x => end >= x.Start_Date && end <= x.End_Date))
                return false;
            //if all the above conditions are false then the delegation can proceed
            return true;
        }
        public static bool IsDateValid(DateTime start,DateTime end)
        {
            if (start > end)
                return false; //not valid
            if (start.Date < DateTime.Now.Date)
                return false;//past date
            return true;
        }


        public static List<Employee> GetAllEmployeesForDept(string sessionId)
        {
            var user = LoginServices.GetUserBySessionId(sessionId);
            List<Employee> list = db.employees.Where(x => x.Role == "DEPT_EMPLOYEE" && x.Dept_Id==user.Dept_Id ).ToList();
            return list;
        }
    }
}