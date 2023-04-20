using Microsoft.VisualStudio.TestTools.UnitTesting;
using CookBook.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace CookBook.WPF.ViewModels.Tests
{
    [TestClass()]
    public class MealsViewModelTests
    {

        HttpResponseMessage message1 = new HttpResponseMessage() { ReasonPhrase = "Ok" };
        
        [TestMethod()]

        public void MessageTest(HttpResponseMessage message)
        {
            var response = message.Content.ReadAsStringAsync().Result;

            Assert.IsNotNull(response);
            //a felhasználó létrehozásakor visszakapott elérési út
            Assert.AreEqual(message.ReasonPhrase, "OK");
            //status kód
            Assert.AreEqual(message.StatusCode, 201);
        }
    }
}