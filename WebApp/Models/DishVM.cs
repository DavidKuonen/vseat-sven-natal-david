using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class DishVM
    {
        public int DishId { get; set; }
        public string DishName { get; set; }
        public float DishPrice { get; set; }
        public int DishQuantity { get; set; }
        public int DishCalories { get; set; }
        public string DishCategory { get; set; }
        public int RestaurantId { get; set; }
    }
}
