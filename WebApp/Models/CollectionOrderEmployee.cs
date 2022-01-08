using System.Collections.Generic;

namespace WebApp.Models
{
    public class CollectionOrderEmployee
    {
        public OrderDetailsEmployee OrderDetails { get; set; }
        public List<Models.DishVM> Dishes { get; set; }
    }
}
