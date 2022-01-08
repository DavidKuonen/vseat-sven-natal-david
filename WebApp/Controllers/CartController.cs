using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CartController : Controller
    {
        public ActionResult Index()
        {
            //Checks if a session exists, if not back to login page
            if (HttpContext.Session.GetInt32("_IdCustomer") == null)
            {
                return RedirectToAction("Index", "Login");
            }

            //Passes the view the list from the session
            return View(HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List"));
        }

        [HttpPost]
        public ActionResult DeleteDishesCart(int DishId)
        {
            List<ShoppingCartVM> Cart = HttpContext.Session.GetComplexData<List<ShoppingCartVM>>("_List");

            //Resets the list and the list size so that the list also contains the current values in the session
            var RemoveItem = Cart.Single(cart => cart.DishId == DishId);
            Cart.Remove(RemoveItem);

            //Resets the list and the list size so that the list also contains the current values in the session
            HttpContext.Session.SetComplexData("_List", Cart);
            HttpContext.Session.SetInt32("_CountList", Cart.Count);

            return RedirectToAction("Index", "Cart");
        }

        [HttpPost]
        public ActionResult ShoppingCartView()
        {
            //Forwarding to the order confirmation page
            return RedirectToAction("Confirmation", "Order");
        }
    }
}
