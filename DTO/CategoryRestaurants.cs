using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CategoryRestaurants
    {
        public int idCategoryRestaurant { get; set; }
        public string name { get; set; }



        public override string ToString()
        {
            return "idCategoryRestaurant: " + idCategoryRestaurant +
                   " name: " + name;
        }
    }
}
