using System;

namespace DTO
{
    public class Orders
    {
        public int IdOrders { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public float TotalPrice { get; set; }
        public int IdCustomers { get; set; }
        public int IdEmployee { get; set; }
        public int IdOrderStatus { get; set; }
    }
}
