using CookBook.JWTSecurity.Models;
using CookBook.Models.Models;

namespace CookBook.API.Tests.TestRepository
{
    internal static class UserExample
    {
        internal static User CreateTestUser()
        {
            return new User
            {
                UserName = "TeszElek",
                Email = "teszt@example.dev",
                Password = "teszt12345"

            };
        }

        internal static User CreateWrongTestUser()
        {
            return new User
            {
                UserName = "",
                Email = null,
                Password = "1010"
            };
        }

        internal static UserLogin CreateUserLoginTest()
        {
            return new UserLogin
            {
                Username = "TeszElek",
                Password = "teszt12345"
            };
        }

        internal static UserLogin CreateWrongUserLoginTest()
        {
            return new UserLogin
            {
                Username = "NemTeszElek",
                Password = "Nemteszt12345"
            };
        }

        //szabályoknak megfelelő tesztadatok
        internal static List<User> CreateTestUsers()
        {
            List<User> users = new()
            {
                new User
                {
                    UserName = "fasírtgolyó",
                    Email = "ittvan@valami.com",
                    Password = "012012"
                },

                new User
                {
                    UserName = "6451lfkjasdlfajdfjk32",
                    Email = "asdasd@teszt.com",
                    Password = "$áß@@hallo"
                },

                new User
                {
                    UserName = "0115",
                    Email = "kovacs.rozsa@gmail.com",
                    Password = "Santor0101"
                }
            };

            return users;
        }

        //rendhagyó tesztadatok

        internal static List<User> CreateWrongTestUsers() 
        {
            List<User> users = new()
            {
                new User
                {
                    UserName = "12",
                    Email = null,
                    Password = "10"
                },

                new User
                {
                    UserName = "őaa3ladfkl",
                    Email = "lad",
                    Password = null
                }
            };

            return users;
        }

    }
}
