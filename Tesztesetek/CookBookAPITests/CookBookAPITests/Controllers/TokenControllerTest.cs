using CookBook.API.Controllers.DTO;
using CookBook.API.Data;
using CookBook.API.Tests.TestRepository;
using CookBook.JWTSecurity.Models;
using CookBook.JWTSecurity.Services;
using CookBook.Models.Controllers;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;

namespace CookBookAPITests.Controllers
{
    [TestClass]
    public class TokenControllerTest
    {
        private TokenController controller = new TokenController(
            new cookbookContext(),
            new JwtManagerService(
                "1F70B533-78B3-40C9-8D47-5BC754D0064B", 
                "JwtServer", 
                "JwtClient"));

        [TestMethod]
        public async Task LoginTest()
        {
            UserLogin userLogin = UserExample.CreateUserLoginTest();
            var response = controller.Login(userLogin);

            Assert.IsNotNull(response);

            Assert.IsInstanceOfType(response.Result, typeof(UserLogin));
        }
    }
}
