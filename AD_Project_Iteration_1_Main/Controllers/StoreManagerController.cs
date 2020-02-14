using AD_Project_Iteration_1_Main.Autherizaion_and_Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AD_Project_Iteration_1_Main.Controllers
{
    [AuthenticationFilter]
    public class StoreManagerController : Controller
    {
        // GET: StoreManager
        public ActionResult Index(string sessionId)
        {
            ViewData["SessionId"] = sessionId;
            ViewData["USER"] = LoginServices.GetUserBySessionId(sessionId);
            return View();
        }

        // GET: StoreManager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StoreManager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StoreManager/Create
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

        // GET: StoreManager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: StoreManager/Edit/5
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

        // GET: StoreManager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StoreManager/Delete/5
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
