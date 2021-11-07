using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class OrderMetricForEmployee
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

        public override string ToString()
        {
            return "CustomerLastname: " + CustomerLastname +
              "\nCustomerFirstname: " + CustomerFirstname +
              "\nCustomerAddress: " + CustomerAddress +
              "\nCustomerVillage: " + CustomerVillage +
              "\nCustomerPhoneNumber: " + CustomerPhoneNumber +
              "\nDishName: " + DishName +
              "\nRestaurantName: " + RestaurantName +
              "\nRestaurantVillage: " + RestaurantVillage;
        }

    }
}
