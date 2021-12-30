using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
  public class EmployeeVM
  {
    public string CustomerLastname { get; set; }
    public string CustomerFirstname { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerVillage { get; set; }
    public string CustomerPhoneNumber { get; set; }

    public string DishName { get; set; }
    public float Dishprice { get; set; }

    public string RestaurantName { get; set; }
    public string RestaurantAddress { get; set; }
    public string RestaurantVillage { get; set; }
    public string RestaurantPhoneNumber { get; set; }

  
  }
}
