using BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            if (ModelState.IsValid)
            {
                //Looks if the logged in is an employee or a customer
                if (CustomersManager.GetCustomers(loginVM.Email, loginVM.Password) != null)
                {
                    var customer = CustomersManager.GetCustomers(loginVM.Email, loginVM.Password);
                    //Sets the different sessions
                    HttpContext.Session.SetInt32("_IdCustomer", customer.IdCustomer);
                    HttpContext.Session.SetString("_NameCustomer", customer.Lastname);
                    //Redirection to the home page for the customer
                    return RedirectToAction("Index", "Restaurant");
                }
                else if (EmployeesManager.GetEmployee(loginVM.Email, loginVM.Password) != null)
                {
                    var employee = EmployeesManager.GetEmployee(loginVM.Email, loginVM.Password);
                    //Sets the different sessions
                    HttpContext.Session.SetInt32("_IdEmployee", employee.IdEmployee);
                    HttpContext.Session.SetString("_NameEmployee", employee.Lastname + " " + employee.Firstname);
                    //Redirection to the home page for the employee/driver
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    //Returns error message
                    ModelState.AddModelError("", "Invalid email or password");
                }
            }

            return View(loginVM);
        }

        public ActionResult Logout()
        {
            //Deletes the entire session so that the user is logged out
            HttpContext.Session.Clear();
            //Redirection to the login page
            return RedirectToAction(nameof(Index));
        }
    }
}
