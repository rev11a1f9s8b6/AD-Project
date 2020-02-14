using AD_Project_Iteration_1_Main.Autherizaion_and_Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AD_Project_Iteration_1_Main.Controllers
{
    [AuthenticationFilter]
    public class DeptRepController : Controller
    {
        // GET: DeptRep
        public ActionResult Index(string sessionId)
        {
            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId);

            return View();
        }

        // GET: DeptRep/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeptRep/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeptRep/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DeptRep/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeptRep/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: DeptRep/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeptRep/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
