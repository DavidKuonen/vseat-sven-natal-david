using BLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class DishesController : Controller
    {
        private IDishesManager DishesManager { get; }

        public DishesController(IDishesManager DishesManager)
        {
            this.DishesManager = DishesManager;
        }

        public ActionResult Index()
        {
            var dishes = DishesManager.GetAllDishes();
            return View(dishes);
        }

        public ActionResult Details(int idDish)
        {
            var dish = DishesManager.GetDishesById(idDish);
            return View(dish);
        }

        [HttpPost]
        public ActionResult Details()
        {
            if(ModelState.IsValid)
            {

                return RedirectToAction(nameof(Details));
            }
            return View();
        }
    }
}
