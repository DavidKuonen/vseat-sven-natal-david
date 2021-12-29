using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class LoginController : Controller
    {
        private ICustomersManager CustomersManager { get; }

        public LoginController(ICustomersManager CustomersManager)
        {
            this.CustomersManager = CustomersManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(LoginVM loginVM)
        {
            if(ModelState.IsValid)
            {
                var customer = CustomersManager.GetCustomers(loginVM.Email, loginVM.Password);
                if(customer != null)
                {
                    HttpContext.Session.SetInt32("IdCustomer", customer.IdCustomer);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "invalid email or password");
                }
            }
            return View(loginVM);
        }
    }
}
