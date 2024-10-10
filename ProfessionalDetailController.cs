using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universal_Task.Models;

namespace Universal_Task.Controllers
{
    public class ProfessionalDetailController : Controller
    {
        private UniversalEntities db = new UniversalEntities();
        // GET: Employees
        public ActionResult Index()
        {
            var data = db.ProfessionalDetail_Table.ToList();
            return View(data);
        }
        public ActionResult delete(int id)
        {
            var emp = db.ProfessionalDetail_Table.Find(id);
            db.ProfessionalDetail_Table.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            ViewData.Model = new ProfessionalDetail_Table();
            return View();
        }
        public ActionResult Edit(int id)
        {
            ProfessionalDetail_Table emp = db.ProfessionalDetail_Table.Find(id);
            return View("Create", emp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProfessionalDetail_Table emp)
        {
            if (emp.id > 0)
            {
                db.Entry(emp).State = EntityState.Modified;
            }
            else
            {
                db.ProfessionalDetail_Table.Add(emp);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}