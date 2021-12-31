using BLL;
using DAL;
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
    public class EmployeeController : Controller
    {
    private IEmployeesManager EmployeesManager { get; }
    private IDishesManager DishesManager { get; }
    private IRestaurantsManager RestaurantsManager { get; }
    private IVillagesManager VillagesManager { get; }
    private IOrdersManager OrdersManager { get; }
    private ICustomersManager CustomersManager { get; }
    private IOrder_DishesManager Order_DishesManager { get; }
  


    public EmployeeController(IEmployeesManager EmployeesManager)
    {
      this.EmployeesManager = EmployeesManager;
    }

    public ActionResult Index(int idEmployee)
        {
      try
      {
        if (HttpContext.Session.GetInt32("IdEmployee") == null)
        {
          return RedirectToAction("index", "Login");
        }



        /* Mein Versuch gemäss RestaurantController aber gleiche Ausgabe wie andere Variante -> NullpointerException.
         * Vieleicht etwas falsch?
         * 
        List<Models.EmployeeVM> employee = new List<Models.EmployeeVM>();
        List<Orders> orders = new List<Orders>();
        orders = OrdersManager.GetAllOrders();


        for (int i = 0; i < orders.Count; i++)
        {
          if (orders[i].FK_Staff == idEmployee)
          {
            Models.EmployeeVM employei = new();
            employei.CustomerFirstname = CustomersManager.GetAllCustomers()[i].Firstname;
            employei.CustomerLastname = CustomersManager.GetAllCustomers()[i].Lastname;
            employei.CustomerAddress = CustomersManager.GetAllCustomers()[i].Address;
            employei.CustomerVillage = VillagesManager.GetAllVillages()[CustomersManager.GetAllCustomers()[i].IdVillage].name;
            employei.CustomerPhoneNumber = CustomersManager.GetAllCustomers()[i].PhoneNumber;
            employei.DishName = DishesManager.GetAllDishes()[i].name;
            employei.Dishprice = DishesManager.GetAllDishes()[i].price;
            employei.RestaurantName = RestaurantsManager.GetAllRestaurants()[i].name;
            employei.RestaurantAddress = RestaurantsManager.GetAllRestaurants()[i].address;
            employei.RestaurantVillage = VillagesManager.GetAllVillages()[RestaurantsManager.GetAllRestaurants()[i].idVillage].name;
            employei.RestaurantPhoneNumber = RestaurantsManager.GetAllRestaurants()[i].phoneNumber;
            employee.Add(employei);
          }
        }

        return View(employee); */


        /*Variante gemäss OrderMetricForEmployee versuch. Leider keine korrekte Ausgabe -> Nullpointerexception */
        var orderss = OrdersManager.GetOrdersByStaffId(idEmployee);

          List<EmployeeVM> getOrder = new List<EmployeeVM>();

          List<int> idCustomers = new List<int>();
          List<int> idOrder = new List<int>();
          List<int> idDish = new List<int>();
          List<int> idRestaurant = new List<int>();
          List<int> idVillage = new List<int>();

          foreach (var o in orderss)
          {
            idCustomers.Add(o.FK_Customers);
            idOrder.Add(o.idOrders);
          }

          foreach (var idC in idCustomers)
          {
            idVillage.Add(CustomersManager.GetAllCustomers()[idC].IdVillage);
          }


          foreach (var idO in idOrder)
          {
            Console.WriteLine("-----------------ID ORDER: " + idO);
            var all = Order_DishesManager.GetAllOrder_Dishes();
            List<Order_Dishes> orderDishes = null;

           for(int i = 0; i < all.Count; i++)
          {
            if(all[i].FK_Orders == idO)
            {
              orderDishes.Add(all[i]);
            }
          }
            foreach (var idOD in orderDishes)
            {
              Console.WriteLine("-----------------ID DISH: " + idOD.FK_Dishes);
              idDish.Add(idOD.FK_Dishes);
            }
          }

          foreach (var idD in idDish)
          {
            idRestaurant.Add(DishesManager.GetAllDishes()[idD].FK_Restaurant);
          }

          for (int i = 0; i < idCustomers.Count; i++)
          {
            Console.WriteLine(idDish[i]);

            var metric = new EmployeeVM
            {
              CustomerFirstname = CustomersManager.GetAllCustomers()[i].Firstname,
              CustomerLastname = CustomersManager.GetAllCustomers()[i].Lastname,
              CustomerAddress = CustomersManager.GetAllCustomers()[i].Address,
              CustomerVillage = VillagesManager.GetAllVillages()[CustomersManager.GetAllCustomers()[i].IdVillage].name,
              CustomerPhoneNumber = CustomersManager.GetAllCustomers()[i].PhoneNumber,
              DishName = DishesManager.GetAllDishes()[i].name,
              Dishprice = DishesManager.GetAllDishes()[i].price,
              RestaurantName = RestaurantsManager.GetAllRestaurants()[i].name,
              RestaurantAddress = RestaurantsManager.GetAllRestaurants()[i].address,
              RestaurantVillage = VillagesManager.GetAllVillages()[RestaurantsManager.GetAllRestaurants()[i].idVillage].name,
              RestaurantPhoneNumber = RestaurantsManager.GetAllRestaurants()[i].phoneNumber

        };

            getOrder.Add(metric);
          } 
          return View(getOrder);    
        

      }
      catch (NullReferenceException)
      {

      }
      return View(); 
    }

  }
}
