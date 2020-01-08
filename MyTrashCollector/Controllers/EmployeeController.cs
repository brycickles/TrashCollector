using Microsoft.AspNet.Identity;
using MyTrashCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTrashCollector.Controllers
{
    public class EmployeeController : Controller
    {
        ApplicationDbContext context;
        public EmployeeController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Employee
        public ActionResult Index(Employee employee)
        {
            var userId = User.Identity.GetUserId();
            if(employee.Zip == null)
            {
                employee = context.Employees.Where(e => e.ApplicationId == userId).FirstOrDefault(); 
            }
            string currentDay = DateTime.Now.DayOfWeek.ToString();
            string todaysDate = DateTime.Now.ToShortDateString();
            var customersInEmployeeZip = context.Customers.Where(c => c.Zip == employee.Zip && c.PickupDay == currentDay || c.Zip == employee.Zip && c.ExtraPickupDate == todaysDate).ToList();
            return View(customersInEmployeeZip);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Confirm(int id)
        {
            var confirmedPickup = context.Customers.Where(c => c.Id == id).FirstOrDefault();
            confirmedPickup.isPickedUp = true;
            context.SaveChanges();

            return RedirectToAction("Index", "Employee");
        }
        // GET: Employee/Create
        public ActionResult Create()
        {
            Employee employee = new Employee();
            return View(employee);
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                employee.ApplicationId = userId;

                context.Employees.Add(employee);
                context.SaveChanges(); 

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
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

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
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
