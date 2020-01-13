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
            Customer customer = new Customer();
            var userId = User.Identity.GetUserId();
            if(employee.Zip == null)
            {
                employee = context.Employees.Where(e => e.ApplicationId == userId).FirstOrDefault(); 
            }

            string currentDay = DateTime.Now.DayOfWeek.ToString();
            string todaysDate = DateTime.Now.ToString("MM/dd/yyyy");

            var customersInEmployeeZip = context.Customers.Where(c => c.Zip == employee.Zip && c.PickupDay == currentDay || c.Zip == employee.Zip && c.ExtraPickupDate == todaysDate).ToList();
            
            return View(customersInEmployeeZip);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult EnterDesiredDay(int id)
        {
            Employee employee = new Employee();
            var userId = User.Identity.GetUserId();
            if (employee.ApplicationId == null)
            {
                employee = context.Employees.Where(e => e.ApplicationId == userId).FirstOrDefault();
            }
            return View(employee);
        }

        [HttpPost]
        public ActionResult EnterDesiredDay(int id, Employee employee)
        {
            var employeeToBeUpdated = context.Employees.Where(e => e.ApplicationId == employee.ApplicationId).FirstOrDefault();
            
            
            employeeToBeUpdated.DesiredDayToView = employee.DesiredDayToView;
            context.SaveChanges();
            return RedirectToAction("ShowByDayOfWeek"); //go to the ShowByDayOfWeek which will redirect to teh view that shows by selected day
        }

        //TODO - GET THIS SHIT DONE - Create View similar to CustomerController
        public ActionResult ShowByDayOfWeek(Employee employee)
        {
            //make sure employee has employee data
            //make sure employee is returned correctly -weve adjusted the day of the week to be searched now and this should return with the object
            var userId = User.Identity.GetUserId();
            if (employee.ApplicationId == null)
            {
                employee = context.Employees.Where(e => e.ApplicationId == userId).FirstOrDefault();
            } 
           
            string selectedDay = employee.DesiredDayToView;
            ViewBag.SearchedDay = selectedDay;
           var customerListBySelectedDay = context.Customers.Where(c => c.Zip == employee.Zip && c.PickupDay == selectedDay).ToList();
            return View(customerListBySelectedDay);
        }
        public ActionResult Confirm(int id)
        {
            var confirmedPickup = context.Customers.Where(c => c.Id == id).FirstOrDefault();
            confirmedPickup.isPickedUp = true;
            if(confirmedPickup.ExtraPickupDate == DateTime.Now.ToShortDateString()) //if today is their extra pickup, charge a special fee
            {
                confirmedPickup.Balance += 10;
            }
            else //otherwise, this is their normal charge
            {
                confirmedPickup.Balance += 1.50;
            }
            
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
