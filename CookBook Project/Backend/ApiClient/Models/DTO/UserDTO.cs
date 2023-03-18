

namespace CookBook.API.Controllers.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }




    
  

    public class LoginDTO : UserDTO
    {
        public LoginDTO(int id, string username, string password, string role, ApiClient.Models.JwtToken jwtToken)
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
