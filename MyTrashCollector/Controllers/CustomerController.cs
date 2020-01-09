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
    }
}
