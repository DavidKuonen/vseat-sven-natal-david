using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Restaurants
    {
        public int idRestaurant { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phoneNumber { get; set; }
        public int idVillage { get; set; }
        public int idDistrict { get; set; }
        public int idCategoryRestaurant { get; set; }
        public string RestaurantImage { get; set; }
            


        public override string ToString()
        {
            return "idRestaurant: " + idRestaurant +
                   " name: " + name +
                   " adress: " + address +
                   " phoneNumber: " + phoneNumber +
                   " idVillage: " + idVillage +
                   " idDistrict: " + idDistrict + 
                   " idCategoryRestaurant: " + idCategoryRestaurant;
        }
    }
}
