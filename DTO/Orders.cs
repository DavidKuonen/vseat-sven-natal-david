using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
  public class Orders
  {
    public int idOrders { get; set; }
    public DateTime OrderTime { get; set; }
    public DateTime DeliveryTime { get; set; }
    public float TotalPrice { get; set; }
    public int FK_Customers { get; set; }
    public int FK_Staff { get; set; }
    public int FK_OrderStatus { get; set; }


    public override string ToString()
    {
      return "idOrders " + idOrders +
        " OrderTime " + OrderTime +
        " DeliveryTime " + DeliveryTime +
        " TotalPrice " + TotalPrice +
        " FK_Customers " + FK_Customers +
        "FK_Staff " + FK_Staff +
        "FK_OrderStatus " + FK_OrderStatus;
    }
  }
}
