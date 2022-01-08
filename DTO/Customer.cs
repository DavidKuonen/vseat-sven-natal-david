using System;

namespace DTO
{
    public class Customer
    {
        public int IdCustomer { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Registered { get; set; }
        public int IdVillage { get; set; }
        public int IdDistrict { get; set; }
        public int IdUserRole { get; set; }
    }
}
