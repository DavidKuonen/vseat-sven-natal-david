using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ShoppingCartVM
    {
        public int DishId { get; set; }
        public string DishName { get; set; }
        public float Price { get; set; }
        public float PriceDishTotal { get; set; }
        public int CustomerId { get; set; }
        public int RestaurantId { get; set; }
        public int DishQuantity { get; set; }
    }
}
