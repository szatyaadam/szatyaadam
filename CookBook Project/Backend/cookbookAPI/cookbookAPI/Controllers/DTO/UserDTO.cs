using CookBook.JWTSecurity.Models;
using CookBook.Models.Models;

namespace CookBook.API.Controllers.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }

    public class UserWithMealsDTO : UserDTO
    {
        public UserWithMealsDTO()
        {

        }
        public UserWithMealsDTO(int id,string email, string username, string password, string role, ICollection<MealDTO> meals)
        {
            Id = id;
            UserName = username;
            Email= email;
            Password = password;
            Role = role;
            Meals = meals;
        }
        public string Email { get; set; }
        public ICollection<MealDTO>? Meals { get; set; }
    }

    public class UserWithMealsAndFavoritesDTO : UserWithMealsDTO
    {
        public UserWithMealsAndFavoritesDTO()
        {

        }

        public UserWithMealsAndFavoritesDTO(int id, string username, string password, string role, ICollection<MealDTO> meals, ICollection<Favorite> favorites)
        {
            Id = id;
            UserName = username;
            Password = password;
            Role = role;
            Meals = meals;
            Favorites= favorites;
        }

        public ICollection<Favorite>? Favorites { get; set; }
    }

    public class LoginDTO : UserDTO
    {
        public LoginDTO(int id, string username, string password, string role, JwtToken jwtToken)
        {
            Id = id;
            UserName = username;
            Password= password;
            Role = role;
            Access_Token = jwtToken.Access_Token;
            Refresh_Token = jwtToken.Refresh_Token;
        }
        public string? Access_Token { get; set; }
        public string? Refresh_Token { get; set; }
    }
         
}
