using CookBook.Models.Models;
using System.Security.Claims;

namespace CookBook.API.Services
{
    public static class UserService
    {
        //Bejelentkezett felhasználó id kikérése
        public static int GetUserId(ClaimsPrincipal userPrincipals)
        {
            var claimId = userPrincipals.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            int.TryParse(claimId.Value, out int userId);
            return userId;
        }

        //frissíti az oldUserData adatait a newUserData adataival ha változott
        public static User SwapUser(User oldUserData, User newUserData)
        {
            if (oldUserData.UserName != newUserData.UserName && !string.IsNullOrEmpty(newUserData.UserName))
                oldUserData.UserName = newUserData.UserName;

            if (oldUserData.Email != newUserData.Email && !string.IsNullOrEmpty(newUserData.Email))
                oldUserData.Email = newUserData.Email;

            if (oldUserData.Password != newUserData.Password && !string.IsNullOrEmpty(newUserData.Password))
                oldUserData.Password = BCrypt.Net.BCrypt.HashPassword(newUserData.Password);

            //if (oldUserData.RoleId != newUserData.RoleId && newUserData.RoleId != 0)
            //{
            //    oldUserData.RoleId = newUserData.RoleId;
            //}
            
            return oldUserData;
        }
    }
}
