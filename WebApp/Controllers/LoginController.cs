using BLL;
using DTO;
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
        private IEmployeesManager EmployeesManager { get; }

        public LoginController(ICustomersManager CustomersManager, IEmployeesManager EmployeesManager)
        {
            this.CustomersManager = CustomersManager;
            this.EmployeesManager = EmployeesManager;
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

                if (CustomersManager.GetCustomers(loginVM.Email, loginVM.Password) != null)
                {
                    var customer = CustomersManager.GetCustomers(loginVM.Email, loginVM.Password);
                    HttpContext.Session.SetInt32("_IdCustomer", customer.IdCustomer);
                    HttpContext.Session.SetString("_NameCustomer", customer.Lastname);
                    return RedirectToAction("Index", "Restaurant");
                }
                else if(EmployeesManager.GetEmployee(loginVM.Email, loginVM.Password) != null)
                {
                    var employee = EmployeesManager.GetEmployee(loginVM.Email, loginVM.Password);
                    HttpContext.Session.SetInt32("_IdEmployee", employee.IdEmployee);
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }

            }
            return View(loginVM);
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Index));
        }
    }
}
