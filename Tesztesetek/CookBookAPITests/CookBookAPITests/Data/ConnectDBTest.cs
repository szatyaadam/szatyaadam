using CookBook.API.Data;
namespace CookBookAPITests.Data
{
    [TestClass]
    public class CookBookAPITest
    {
        [TestMethod]
        public void ConnectionStringTest()
        {
            cookbookContext context = new cookbookContext();

            bool canConnect = context.Database.CanConnect();

            Assert.IsTrue(canConnect);
        }
    }
}