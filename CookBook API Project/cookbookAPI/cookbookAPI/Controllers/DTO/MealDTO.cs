using CookBook.Models.Models;

namespace CookBook.API.Controllers.DTO
{
    public class MealDTO
    {
        public MealDTO()
        {
        }

        public MealDTO(int id, string mealName, TimeSpan? preperationTime, float? price, string? photo, string? discription,sbyte privacy, ICollection<IngredientDTO> ingredients ,Mealtype mealtype)
        {
            Id = id;
            MealName = mealName;
            PreperationTime = preperationTime;
            Price = price;
            Photo = photo;
            Discription = discription;
            Privacy = privacy;
            Ingredients = ingredients;
            Mealtype = mealtype;
        }
        public int Id { get; set; }
        public string MealName { get; set; } = null!;
        public TimeSpan? PreperationTime { get; set; }
        public float? Price { get; set; }
        public string? Photo { get; set; }
        public string? Discription { get; set; }
        public sbyte Privacy { get; set; }
        public ICollection<IngredientDTO>? Ingredients { get; set; }
        public Mealtype Mealtype { get; set; }
    }
}
