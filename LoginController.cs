using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universal_Task.Models;

namespace Universal_Task.Controllers
{
    public class LoginController : Controller
    {
        private UniversalEntities db = new UniversalEntities();
        // GET: Employees
        public ActionResult Index()
        {
            var data = db.Login_Table.ToList();
            return View(data);
        }
        public ActionResult delete(int id)
        {
            var emp = db.Login_Table.Find(id);
            db.Login_Table.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            //ViewData.Model = new Login_Table();
            return View();
        }
        public ActionResult Edit(int id)
        {
            Login_Table emp = db.Login_Table.Find(id);
            return View("Create", emp);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Login_Table emp)
        {
            if (emp.Id > 0)
            {
                db.Entry(emp).State = EntityState.Modified;
            }
            else
            {
                db.Login_Table.Add(emp);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}