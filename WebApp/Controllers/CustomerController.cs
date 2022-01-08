using BLL;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CustomerController : Controller
    {
        private IVillagesManager VillagesManager { get; }
        private IOrdersManager OrdersManager { get; }
        private ICustomersManager CustomersManager { get; }

        public CustomerController(IVillagesManager VillagesManager, IOrdersManager OrdersManager, ICustomersManager CustomersManager)
        {
            this.VillagesManager = VillagesManager;
            this.OrdersManager = OrdersManager;
            this.CustomersManager = CustomersManager;
        }

        public ActionResult Index()
        {
            //Checks if a session exists, if not back to login page
            if (HttpContext.Session.GetInt32("_IdCustomer") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            //Stores the Id of the currently logged in customer
            int IdCustomer = HttpContext.Session.GetInt32("_IdCustomer").Value;

            //Returns the open orders of the customer
            List<Orders> Orders = OrdersManager.GetOpenOrdersCustomer(IdCustomer);

            return View(Orders);
        }

        [HttpPost]
        public ActionResult DeleteOrder(int OrderID)
        {
            //Deletes the order via the BLL manager
            OrdersManager.DeleteOrders(OrderID);
            return RedirectToAction("Index");
        }

        public ActionResult Edit()
        {
            //Checks if a session exists, if not back to login page
            if (HttpContext.Session.GetInt32("_IdCustomer") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            //Stores the Id of the currently logged in customer
            int IdCustomer = HttpContext.Session.GetInt32("_IdCustomer").Value;

            Models.CustomerVM Customer = new()
            {
                CustomerId = IdCustomer,
                Firstname = CustomersManager.GetCustomerById(IdCustomer).Firstname,
                Lastname = CustomersManager.GetCustomerById(IdCustomer).Lastname,
                Address = CustomersManager.GetCustomerById(IdCustomer).Address,
                PhoneNumber = CustomersManager.GetCustomerById(IdCustomer).PhoneNumber,
                Email = CustomersManager.GetCustomerById(IdCustomer).Email,
                Password = CustomersManager.GetCustomerById(IdCustomer).Password,
                Postalcode = VillagesManager.GetVillagesById(CustomersManager.GetCustomerById(IdCustomer).IdVillage).PostalCode,
                Village = VillagesManager.GetVillagesById(CustomersManager.GetCustomerById(IdCustomer).IdVillage).Name
            };

            //Object is passed to the view
            return View(Customer);
        }

        [HttpPost]
        public ActionResult Edit(CustomerVM Customer)
        {
            DTO.Customer customerEdit = new()
            {
                IdCustomer = Customer.CustomerId,
                Firstname = Customer.Firstname,
                Lastname = Customer.Lastname,
                Address = Customer.Address,
                PhoneNumber = Customer.PhoneNumber,
                Email = Customer.Email,
                Password = Customer.Password,
                IdVillage = VillagesManager.GetVillageByName(Customer.Village).IdVillage,
                IdDistrict = VillagesManager.GetVillageByName(Customer.Village).IdDistrict
            };

            //Resets the session for the name of the logged in client.  So that the correct name is also displayed.
            HttpContext.Session.SetString("_NameCustomer", Customer.Lastname);

            //The method updates the client with the new entries
            CustomersManager.UpdateCustomer(customerEdit);

            return RedirectToAction("Edit");
        }
    }
}
