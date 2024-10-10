using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Universal_Task.Models;

namespace Universal_Task.Controllers
{
    public class EmployeeController : Controller
    {
        private UniversalEntities db = new UniversalEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Employee_Table log)
        {
            var user = db.Employee_Table.FirstOrDefault(x => x.UserName == log.UserName);
            if (user != null && user.Password == log.Password)
            {
                FormsAuthentication.SetAuthCookie(log.UserName, false);
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View();
            }
        }
        public ActionResult Signup()
        {
            ViewData.Model = new State_Table();
            var Statedata = db.State_Table.Select(n => new SelectListItem
            {
                Value = n.StateCode.ToString(),
                Text = n.StateName
            }).ToList();
            ViewBag.StateCode = Statedata;

            ViewData.Model = new City_Table();
            var Citydata = db.City_Table.Select(n => new SelectListItem
            {
                Value = n.CityCode.ToString(),
                Text = n.CityName
            }).ToList();
            ViewBag.CityCode = Citydata;

            var data = new Employee_Table();
            return View(data);
        }
        [HttpPost]
        public ActionResult Signup(Employee_Table model, HttpPostedFileBase ImageFile)
        {

            if (ImageFile != null && ImageFile.ContentLength > 0)
            {
                using (var binaryReader = new BinaryReader(ImageFile.InputStream))
                {
                    model.Image = binaryReader.ReadBytes(ImageFile.ContentLength);
                }
            }
            db.Employee_Table.Add(model);
            db.SaveChanges();
            return RedirectToAction("Login");
        }
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            List<Employee_Table> allEmployees = db.Employee_Table.ToList();

            int totalEmployees = allEmployees.Count;
            int totalPages = (int)Math.Ceiling((double)totalEmployees / pageSize);
            List<Employee_Table> paginatedEmployees = allEmployees
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;

            return View(paginatedEmployees);
        }
        public ActionResult Delete(int id)
        {
            var emp = db.Employee_Table.Find(id);
            if (emp != null)
            {
                db.Employee_Table.Remove(emp);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            ViewData.Model = new Employee_Table();
            return View();
        }
        public ActionResult Edit(int id)
        {
            Employee_Table emp = db.Employee_Table.Find(id);

            var Statedata = db.State_Table.Select(n => new SelectListItem
            {
                Value = n.StateCode.ToString(),
                Text = n.StateName,
                Selected = (n.StateCode == emp.StateCode) 
            }).ToList();
            ViewBag.StateCode = Statedata;

            List<SelectListItem> list = db.City_Table
                 .Select(c => new SelectListItem { Text = c.CityName, Value = c.CityCode.ToString() })
                 .ToList();

            ViewBag.CityCode = new SelectList(list, "Value", "Text", emp.CityCode);

            return View("Create", emp);        
        }
        [HttpPost]
        public ActionResult Edit(Employee_Table employee, HttpPostedFileBase ImageFile)
        {
            var existingEmployee = db.Employee_Table.Find(employee.EmpId);

            if (existingEmployee != null)
            {
                // Update employee fields
                existingEmployee.UserName = employee.UserName;
                existingEmployee.Address = employee.Address;
                existingEmployee.CityCode = employee.CityCode;
                existingEmployee.State = employee.State;
                existingEmployee.DoB = employee.DoB;
                existingEmployee.UniqueCode = employee.UniqueCode;
                existingEmployee.Password = employee.Password;
                existingEmployee.Date = employee.Date;

                // Check if a new image has been uploaded
                if (ImageFile != null && ImageFile.ContentLength > 0)
                {
                    using (var binaryReader = new BinaryReader(ImageFile.InputStream))
                    {
                        existingEmployee.Image = binaryReader.ReadBytes(ImageFile.ContentLength);
                    }
                }

                // Save changes to the database
                db.Entry(existingEmployee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employee);
        }
        [HttpPost]
        public ActionResult Create(Employee_Table emp)
        {
            if (emp.EmpId > 0)
            {
                db.Entry(emp).State = EntityState.Modified;
            }
            else
            {
                db.Employee_Table.Add(emp);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult GetDropDown(int Coun_id)
        {
            var res = db.City_Table
                         .Where(m => m.StateCode == Coun_id)
                         .ToList()
                         .Select(n => new SelectListItem
                         {
                             Value = n.CityCode.ToString(),
                             Text = n.CityName
                         })
                         .ToList();
            return Json(res);
        }
    }
}
