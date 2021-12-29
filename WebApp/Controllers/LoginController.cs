﻿using BLL;
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
                    HttpContext.Session.SetInt32("IdCustomer", customer.IdCustomer);
                    return RedirectToAction("Index", "Home");
                }
                else if(EmployeesManager.GetEmployee(loginVM.Email, loginVM.Password) != null)
                {
                    var employee = EmployeesManager.GetEmployee(loginVM.Email, loginVM.Password);
                    HttpContext.Session.SetInt32("IdEmployee", employee.IdEmployee);
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid email or password");
                }

            }
            return View(loginVM);
        }
    }
}
