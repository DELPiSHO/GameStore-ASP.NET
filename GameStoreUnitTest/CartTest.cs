using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
using GameStore.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace GameStoreUnitTest
{
    [TestClass]
    public class CartTest
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            Game game1 = new Game { GameId = 1, Name = "Gra1" };
            Game game2 = new Game { GameId = 2, Name = "Gra2" };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            List<CartLine> results = cart.Lines.ToList();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Game, game1);
            Assert.AreEqual(results[1].Game, game2);
        }
        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Game game1 = new Game { GameId = 1, Name = "Gra1" };
            Game game2 = new Game { GameId = 2, Name = "Gra2" };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Game.GameId).ToList();

            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);    // 6 egzemplarzy dodano do schowka
            Assert.AreEqual(results[1].Quantity, 1);
        }
        [TestMethod]
        public void Can_Remove_Line()
        {
            Game game1 = new Game { GameId = 1, Name = "Gra1" };
            Game game2 = new Game { GameId = 2, Name = "Gra2" };
            Game game3 = new Game { GameId = 3, Name = "Gra3" };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 4);
            cart.AddItem(game3, 2);
            cart.AddItem(game2, 1);

            cart.RemoveLine(game2);

            Assert.AreEqual(cart.Lines.Where(c => c.Game == game2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }
        [TestMethod]
        public void Calculate_Cart_Total()
        {
            Game game1 = new Game { GameId = 1, Name = "Gra1", Price = 100 };
            Game game2 = new Game { GameId = 2, Name = "Gra2", Price = 55 };

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            decimal result = cart.ComputeTotalValue();

            Assert.AreEqual(result, 655);
        }
        [TestMethod]
        public void Can_Clear_Contents()
        {
            Game game1 = new Game { GameId = 1, Name = "Gra1", Price = 100};
            Game game2 = new Game { GameId = 2, Name = "Gra2", Price = 55};

            Cart cart = new Cart();

            cart.AddItem(game1, 1);
            cart.AddItem(game2, 1);
            cart.AddItem(game1, 5);
            cart.Clear();

            Assert.AreEqual(cart.Lines.Count(), 0);
        }
    }
}
