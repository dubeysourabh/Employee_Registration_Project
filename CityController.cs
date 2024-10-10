using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Universal_Task.Models;

namespace Universal_Task.Controllers
{
    public class CityController : Controller
    {
        // GET: City
        private UniversalEntities db = new UniversalEntities();
        // GET: Employees
        public ActionResult Index()
        {
            var data = db.City_Table.ToList();
            return View(data);
        }
        public ActionResult delete(int id)
        {
            var emp = db.City_Table.Find(id);
            db.City_Table.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            ViewData.Model = new State_Table();
            var Statedata = db.State_Table.Select(n => new SelectListItem
            {
                Value = n.StateCode.ToString(),
                Text = n.StateName
            }).ToList();
            ViewBag.StateCode = Statedata;
            var data = new City_Table();
            return View(data);
        }
        public ActionResult Edit(int id)
        {
            ViewData.Model = new State_Table();
            City_Table Data = db.City_Table.Find(id);

            var Countrydata = db.State_Table.Select(n => new SelectListItem
            {
                Value = n.StateCode.ToString(),
                Text = n.StateName,
                Selected = n.StateCode == Data.StateCode
            }).ToList();
            ViewBag.StateCode = Countrydata;

            return View("Create", Data);
        }
        [HttpPost]
        public ActionResult Create(City_Table emp)
        {
            if (emp.CityId > 0)
            {
                db.Entry(emp).State = EntityState.Modified;
            }
            else
            {
                db.City_Table.Add(emp);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
