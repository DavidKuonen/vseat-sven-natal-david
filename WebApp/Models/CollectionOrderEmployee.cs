using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
  public class CollectionOrderEmployee
  {
        public OrderDetailsEmployee OrderDetails { get; set; }
        public List<Models.DishVM> Dishes { get; set; }
  }
}
