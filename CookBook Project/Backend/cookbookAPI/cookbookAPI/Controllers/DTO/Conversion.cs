using CookBook.Models.Models;

namespace CookBook.API.Controllers.DTO
{
    public static class Conversion
    {
        public static UserDTO ToUserDTO(this User user) => new UserDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Role = user.Role?.RoleName
        };
        public static UserWithMealsDTO ToUserWithMealsDTO(this User user) => new UserWithMealsDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Password = user.Password,
            Role = user.Role?.RoleName,
            Meals = user.Meals?.ToListMealsDTO()
        };
        public static UserWithMealsAndFavoritesDTO ToUserWithMealsAndFavoritesDTO(this User user) => new UserWithMealsAndFavoritesDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            Password = user.Password,
            Role = user.Role?.RoleName,
            Meals = user.Meals?.ToListMealsDTO(),
            Favorites = user.Favorites?.ToHashSet()
        };
        public static MaterialDTO ToMaterialDTO(this Material material) => new MaterialDTO
        {
            Id = material.Id,
            UnitOfMeasure = material.UnitOfMeasure,
            IngredientName = material?.IngredientName
        };
        public static IngredientDTO ToIngredientDTO(this Ingredient ingredient) => new IngredientDTO
        {
            Id = ingredient.Id,
            Quantity = ingredient.Quantity,
            Materials = ingredient.Materials?.ToMaterialDTO()
        };
        public static MealDTO ToMealDTO(this Meal meal) => new MealDTO
        {
            Id = meal.Id,
            MealName = meal.MealName,
            PreperationTime = meal.PreperationTime,
            Price = meal.Price,
            Photo = meal.Photo,
            Discription = meal.Discription,
            Ingredients = meal.Ingredients.ToListIngredientsDTO(),
            MealType = meal.MealType,
            Privacy = meal.Privacy,
            UserId = meal.UserId
        };
        public static TopMealDTO ToTopMealDTO(this Meal meal, int likes, int rank) => new TopMealDTO
        {
            Id = meal.Id,
            MealName = meal.MealName,
            PreperationTime = meal.PreperationTime,
            Price = meal.Price,
            Photo = meal.Photo,
            Discription = meal.Discription,
            Ingredients = meal.Ingredients.ToListIngredientsDTO(),
            MealType = meal.MealType,
            Likes = likes,
            Rank = rank
        };
        
        public static List<UserDTO> ToListUsersDTO(this IEnumerable<User> source) => source.Select(src => src.ToUserDTO()).ToList();
        public static List<MaterialDTO> ToListMaterialsDTO(this IEnumerable<Material> source) => source.Select(src => src.ToMaterialDTO()).ToList();
        public static List<IngredientDTO> ToListIngredientsDTO(this IEnumerable<Ingredient> source) => source.Select(src => src.ToIngredientDTO()).ToList();
        public static List<MealDTO> ToListMealsDTO(this IEnumerable<Meal> source) => source.Select(src => src.ToMealDTO()).ToList(); 
    }
}
