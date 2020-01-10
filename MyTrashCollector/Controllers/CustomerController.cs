using Microsoft.AspNet.Identity;
using MyTrashCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTrashCollector.Controllers
{
    
    public class CustomerController : Controller
    {
        ApplicationDbContext context;

        public CustomerController()
        {
            context = new ApplicationDbContext();
        }
        // GET: Customer
        public ActionResult Index(Customer customer)
        {
            return View(customer);
        }

        // GET: Customer/Details/5
        public ActionResult Details(Customer customer)
        {
            var userId = User.Identity.GetUserId();
            if (customer.ApplicationId == null)
            {
                customer = context.Customers.Where(c => c.ApplicationId == userId).FirstOrDefault();
            }
            var customers = context.Customers.Where(c => c.ApplicationId == customer.ApplicationId).ToList();
            return View(customers);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {

            Customer customer = new Customer();
            return View(customer);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            try
            {
                string userId = User.Identity.GetUserId();
                customer.ApplicationId = userId;

                context.Customers.Add(customer);
                context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
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

        public ActionResult EditPickupDay(int id)
        {             
            return View("PickupDateChanger");            
        }

        [HttpPost]
        public ActionResult EditPickupDay(int id, Customer customer) {
            var customerToBeChanged = context.Customers.Where(c => c.Id == id).FirstOrDefault();
            customerToBeChanged.PickupDay = customer.PickupDay;
            context.SaveChanges();
            return RedirectToAction("Details");
        }
        public ActionResult EditSuspendDates(int id)
        {

            return View("SuspendDatesChanger");
            //TODO: create a view called SuspendDatesChanger
        }

        [HttpPost]
        public ActionResult EditSuspendDates(int id, Customer customer)
        {
            var customerToBeChanged = context.Customers.Where(c => c.Id == id).FirstOrDefault();
            customerToBeChanged.SuspendStart = customer.SuspendStart;
            customerToBeChanged.SuspendEnd = customer.SuspendEnd;
            context.SaveChanges();
            return RedirectToAction("Details");
        }
        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
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

        public ActionResult BalanceBreakdown(Customer customer)
        {
            var userId = User.Identity.GetUserId();


            int year = DateTime.Now.Year;
            int currentMonth = DateTime.Now.Month;            
            if (customer.ApplicationId == null)
            {
                customer = context.Customers.Where(c => c.ApplicationId == userId).FirstOrDefault();
            }
            string dayOfWeek = customer.PickupDay;


            //use year, current month, day of week to pass through GetDates function to have dates list return all instances of pickup day in month 
            List<DateTime> datesRange = GetDates(year, currentMonth, dayOfWeek);  //this is a list of every pickup day with accompanying date that occurred in the current month for customer to see itemized reciept of in balance

            double count = datesRange.Count;
            string stringCurrentMonth = "";
            switch (currentMonth)
            {
                case 1:
                    stringCurrentMonth = "January";                   
                    break;
                case 2:
                    stringCurrentMonth = "February";
                    break;
                case 3:
                    stringCurrentMonth = "March";
                    break;
                case 4:
                    stringCurrentMonth = "April";
                    break;
                case 5:
                    stringCurrentMonth = "May";
                    break;
                case 6:
                    stringCurrentMonth = "June";
                    break;
                case 7:
                    stringCurrentMonth = "July";
                    break;
                case 8:
                    stringCurrentMonth = "August";
                    break;
                case 9:
                    stringCurrentMonth = "September";
                    break;
                case 10:
                    stringCurrentMonth = "October";
                    break;
                case 11:
                    stringCurrentMonth = "November";
                    break;
                case 12:
                    stringCurrentMonth = "December";
                    break;
            }

            string extraPickupDateTwoDigits = customer.ExtraPickupDate.Substring(0, 2);
            if(extraPickupDateTwoDigits == "01")
            {
                ViewBag.ExtraPickupDateMonth = "January";
            } else if (extraPickupDateTwoDigits == "02")
            {
                ViewBag.ExtraPickupDateMonth = "February";
            }
            else if (extraPickupDateTwoDigits == "03")
            {
                ViewBag.ExtraPickupDateMonth = "March";
            }
            else if (extraPickupDateTwoDigits == "04")
            {
                ViewBag.ExtraPickupDateMonth = "April";
            }
            else if (extraPickupDateTwoDigits == "05")
            {
                ViewBag.ExtraPickupDateMonth = "May";
            }
            else if (extraPickupDateTwoDigits == "06")
            {
                ViewBag.ExtraPickupDateMonth = "June";
            }
            else if (extraPickupDateTwoDigits == "07")
            {
                ViewBag.ExtraPickupDateMonth = "July";
            }
            else if (extraPickupDateTwoDigits == "08")
            {
                ViewBag.ExtraPickupDateMonth = "August";
            }
            else if (extraPickupDateTwoDigits == "09")
            {
                ViewBag.ExtraPickupDateMonth = "September";
            }
            else if (extraPickupDateTwoDigits == "10")
            {
                ViewBag.ExtraPickupDateMonth = "October";
            }
            else if (extraPickupDateTwoDigits == "11")
            {
                ViewBag.ExtraPickupDateMonth = "November";
            }
            else if (extraPickupDateTwoDigits == "12")
            {
                ViewBag.ExtraPickupDateMonth = "December";
            }

            bool extraPickupDateFallsInCurrentMonth;
            string twoDigitCurrentMonth = DateTime.Now.Month.ToString("d2");
            if (extraPickupDateTwoDigits == twoDigitCurrentMonth)
            {
                ViewBag.TotalSum = (count * 1.5) + 10; //if its the month of an extra pickup, apply the extra pickup charge
                extraPickupDateFallsInCurrentMonth = true;
            } else
            {
                ViewBag.TotalSum = count * 1.5;
                extraPickupDateFallsInCurrentMonth = false; 
            }




            ViewBag.ExtraPickupDate = customer.ExtraPickupDate;
            ViewBag.ExtraPickupDateApplied = extraPickupDateFallsInCurrentMonth;
            ViewBag.DatesRange = datesRange;
            ViewBag.CurrentMonth = stringCurrentMonth;

            //TODO UNFUCK
            //ViewBag.ExtraPcikupDateMonth = customer.ExtraPickupDate; //TODO - fix this. Make it display a formatted month only
            return View();
        }

        public static List<DateTime> GetDates(int year, int month, string day)
        {
            return Enumerable.Range(1, DateTime.DaysInMonth(year, month))
                 .Where(d => new DateTime(year, month, d).ToString("dddd").Equals(day))
                .Select(d => new DateTime(year, month, d)).ToList();
        }
    }

    
}
