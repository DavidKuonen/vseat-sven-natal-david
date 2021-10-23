using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
  public class CategoryDishes
  {
    public int idCategorie { get; set; }
    public string name { get; set; }

    public override string ToString()
    {
      return "idCategorie " + idCategorie +
        "name" + name;
    }

  }
}
