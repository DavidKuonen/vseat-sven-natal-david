using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Dishes
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Calories { get; set; }
    }
}
