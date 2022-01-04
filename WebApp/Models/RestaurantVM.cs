using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class RestaurantVM
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string ResaurantAddress { get; set; }
        public string RestaurantCity { get; set; }
        public string RestaurantDistrict { get; set; }
        public string RestaurantCategory { get; set; }
        public int OrderID { get; set; }
        public string RestaurantImage { get; set; }

    }
}
