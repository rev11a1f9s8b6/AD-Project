using AD_Project_Iteration_1_Main.Context;
using AD_Project_Iteration_1_Main.Models;
using AD_Project_Iteration_1_Main.Models_DB;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace AD_Project_Iteration_1_Main.Autherizaion_and_Authentication
{
    public class LoginServices
    { 
        public static String CreateSessionForUser(Employee user) {
            DatabaseContext db = new DatabaseContext();
            user.SessionId= Guid.NewGuid().ToString();
            db.employees.AddOrUpdate(user);
            db.SaveChanges();
            return user.SessionId;
        }


        public static Employee AuthenticateUser(UserLogin user)
        {
            DatabaseContext db = new DatabaseContext();
            if (db.employees.Any(x => x.User_Name == user.UserName && x.Password == user.Password))
            {
                Employee USER = db.employees.Where(x => x.User_Name == user.UserName && x.Password == user.Password).FirstOrDefault();
                return USER;
            }
            return null;
        }

        
        public static Employee GetUserBySessionId(string sessionId)
        {
            DatabaseContext db = new DatabaseContext();
            if (db.employees.Any(x => x.SessionId == sessionId))
            {
                Employee USER = db.employees.Where(x => x.SessionId == sessionId).FirstOrDefault();
                return USER;
            }
            return null;
        }

        public static void RemoveSession(string sessionId)
        {
            DatabaseContext db = new DatabaseContext();
            if (db.employees.Any(x => x.SessionId == sessionId))
            {
                Employee USER = db.employees.Where(x => x.SessionId == sessionId).FirstOrDefault();
                USER.SessionId = null;
                db.employees.AddOrUpdate(USER);
                db.SaveChanges();
            }
        }
    }
}