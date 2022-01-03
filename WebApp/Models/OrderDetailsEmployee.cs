using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class OrderDetailsEmployee
    {
        public string CustomerLastname { get; set; }
        public string CustomerFirstname { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerVillage { get; set; }
        public string CustomerPhoneNumber { get; set; }

        public int OrderId { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public float OrderTotalPrice { get; set; }
    }
}
