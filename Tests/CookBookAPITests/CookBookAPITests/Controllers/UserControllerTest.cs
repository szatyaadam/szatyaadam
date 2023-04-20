using CookBook.API.Controllers;
using CookBook.API.Controllers.DTO;
using CookBook.API.Data;
using CookBook.API.Tests.TestRepository;
using Microsoft.AspNetCore.Mvc;

namespace CookBookAPITests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController controller = new UserController(new cookbookContext());

        [TestMethod]
        public async Task SignUpUserTest()
        {
            //create test user
            var user = UserExample.CreateTestUser();
            //http response
            var response = await controller.SignUpUser(user);

            var result = response as OkObjectResult;

            //válaszként visszakapott UserDTO
            //var userResult = result.Value as UserDTO;


            //a response nem null
            Assert.IsNotNull(result);
            //a felhasználó létrehozásakor visszakapott elérési út
            //Assert.AreEqual(result.ActionName, "profile");
            //status kód
            Assert.AreEqual(result.StatusCode, 200);
            //visszakapott objektum UserDTO-e
        //    Assert.IsInstanceOfType(userResult, typeof(UserDTO));
        }

    }
}
