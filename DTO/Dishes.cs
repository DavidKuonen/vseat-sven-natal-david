namespace DTO
{
    public class Dishes
    {
        public int IdDishes { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Calories { get; set; }
        public string Image { get; set; }
        public int IdCategoryDishes { get; set; }
        public int IdRestaurant { get; set; }
    }
}
