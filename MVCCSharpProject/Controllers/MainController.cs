using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCSharpProject.Models;
using MVCCSharpProject.ViewModels;


namespace MVCCSharpProject.Controllers
{
    public class MainController : Controller
    {

        Repository _repository  = new Repository();


        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Customers customerForm)
        {
            if (ModelState.IsValid)
            {
                _repository.Create(customerForm);
                TempData["Success"] = "Seccussfully Saved";
            }
            return View();
        }

        public ActionResult CustomersList()
        {
            var customers = _repository.GetCustomers();
            return View(customers);
        }

public ActionResult TestAction(){
return View();
}




        public ActionResult CustomersReport(string searchTerm)
        {
            var customersReport = _repository.GetCustomerReport(searchTerm);
            return View(customersReport);
        }




        public ActionResult Update(string ID)
        {
            var customerInfo = _repository.GetCustomerForEdit(ID);
            return View(customerInfo);
        }

        [HttpPost]
        public ActionResult Update(Customers customer)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateCustomer(customer);
                TempData["Success"] = "Successfull Updated Customer";
            }
            return View();
        }
    }
}
