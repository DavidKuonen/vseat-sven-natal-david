using System;

namespace WebApp.Models
{
    public class OrderDetailsEmployee
    {
        public int RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public string ResaurantAddress { get; set; }
        public string RestaurantCity { get; set; }


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
