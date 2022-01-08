using BLL;
using Microsoft.AspNetCore.Mvc;
using System;
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
            RegisterVM RegisterVM = new();
            //Passes all villages to the model. So you can create a dropdown later
            RegisterVM.Villages = VillagesManager.GetAllVillages();
            return View(RegisterVM);
        }

        [HttpPost]
        public ActionResult Index(RegisterVM RegisterVM)
        {
            if (ModelState.IsValid)
            {
                if(RegisterVM != null)
                {
                    DTO.Customer customer = new()
                    {
                        Firstname = RegisterVM.Firstname,
                        Lastname = RegisterVM.Lastname,
                        Address = RegisterVM.Address,
                        PhoneNumber = RegisterVM.PhoneNumber,
                        Email = RegisterVM.Email,
                        Password = RegisterVM.Password,
                        Registered = DateTime.Now,
                        IdVillage = RegisterVM.Village,
                        IdDistrict = VillagesManager.GetVillagesById(RegisterVM.Village).IdDistrict,
                        IdUserRole = 1
                    };

                    //Adds the new customer to the database
                    CustomersManager.AddCustomer(customer);
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    //For Display the Error Message
                    ModelState.AddModelError("", "Not all filled correctly");
                }
            }
            return View(RegisterVM);
        }
    }
}
