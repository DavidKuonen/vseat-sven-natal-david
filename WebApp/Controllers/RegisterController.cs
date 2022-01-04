using BLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class RegisterController : Controller
    {
        private ICustomersManager CustomersManager { get; }
        private IVillagesManager VillagesManager { get; }
        private IDistrictsManager DistrictsManager { get; }

        public RegisterController(ICustomersManager CustomersManager, IVillagesManager VillagesManager, IDistrictsManager DistrictsManager)
        {
            this.CustomersManager = CustomersManager;
            this.VillagesManager = VillagesManager;
            this.DistrictsManager = DistrictsManager;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                DTO.Customer customer = new()
                {
                    Firstname = registerVM.Firstname,
                    Lastname = registerVM.Lastname,
                    Address = registerVM.Address,
                    PhoneNumber = registerVM.PhoneNumber,
                    Email = registerVM.Email,
                    Password = registerVM.Password,
                    Registered = DateTime.Now,
                    IdVillage = VillagesManager.GetVillageByName(registerVM.Village).idVillage,
                    IdDistrict = VillagesManager.GetVillageByName(registerVM.Village).idDistrict,
                    IdUserRole = 1
                };

                CustomersManager.AddCustomer(customer);
                return RedirectToAction("Index", "Login");

            }
            return View(registerVM);
        }
    }
}
