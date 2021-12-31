using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
  public  class Order_Dishes
  {
    public int idOrder_Dishes { get; set; }
    public int Quantity { get; set; }
    public int FK_Dishes { get; set; }
    public int FK_Orders { get; set; }

    public override string ToString()
    {
      return "idOrder_Dishes " + idOrder_Dishes +
        "Quantity " + Quantity +
        "FK_Dishes " + FK_Dishes +
        "FK_Orders " + FK_Orders;
    }

    }
}
