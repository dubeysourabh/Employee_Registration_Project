using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universal_Task.Models;

namespace Universal_Task.Controllers
{
    public class StateController : Controller
    {
        private UniversalEntities db = new UniversalEntities();
        // GET: Employees
        public ActionResult Index()
        {
            var data = db.State_Table.ToList();
            return View(data);
        }
        public ActionResult delete(int id)
        {
            var emp = db.State_Table.Find(id);
            db.State_Table.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            ViewData.Model = new State_Table();
            return View();
        }
        public ActionResult Edit(int id)
        {
            State_Table emp = db.State_Table.Find(id);
            return View("Create", emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(State_Table emp)
        {
            if (emp.StateId > 0)
            {
                db.Entry(emp).State = EntityState.Modified;
            }
            else
            {
                db.State_Table.Add(emp);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}