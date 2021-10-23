using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
 public class Dishes
  {
    public int idDishes { get; set; }
    public string name { get; set; }
    public float price { get; set; }
    public int calories { get; set; }
    public string Image { get; set; }
    public int FK_CategoryDishes { get; set; }
    public int FK_Restaurant { get; set; }

    public override string ToString()
    {
      return "idDishes " + idDishes +
        "name " + name +
        "price " + price +
        "calories " + calories +
        "Image " + Image +
        "FK_CategoryDishes " + FK_CategoryDishes +
        "FK_Restaurant " + FK_Restaurant;
    }

  }
}
