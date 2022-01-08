namespace DTO
{
    public class Restaurants
    {
        public int IdRestaurant { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int IdVillage { get; set; }
        public int IdDistrict { get; set; }
        public int IdCategoryRestaurant { get; set; }
        public string RestaurantImage { get; set; }
    }
}
