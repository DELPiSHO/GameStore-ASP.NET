using GameStore.WebUI.Controllers;
using GameStore.WebUI.Infrastructure.Abstract;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace GameStoreUnitTest
{
    [TestClass]
    public class AdminSequrityTests
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            // Tworzymy imitacje logowania
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("Jim_Carry", "1798")).Returns(true);

            //Tworzenie modelu z prawidłowymi danymi do logowania
            LoginViewModel model = new LoginViewModel
            {
                UserName = "Jim_Carry",
                Password = "1798"
            };

            AccountController target = new AccountController(mock.Object);

            ActionResult result = target.Login(model, "/MyURL");

            //czy uda się zalogować się
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {
            Mock<IAuthProvider> mock = new Mock<IAuthProvider>();
            mock.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);

            //zle dane
            LoginViewModel model = new LoginViewModel
            {
                UserName = "badUser",
                Password = "badPass"
            };

            AccountController target = new AccountController(mock.Object);

            ActionResult result = target.Login(model, "/MyURL");

            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
