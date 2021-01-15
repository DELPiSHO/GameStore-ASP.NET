using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
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
    public class AdminTest
    {
        [TestMethod]
        public void Index_Contains_All_Games()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Gra1"},
                new Game { GameId = 2, Name = "Gra2"},
                new Game { GameId = 3, Name = "Gra3"},
                new Game { GameId = 4, Name = "Gra4"},
                new Game { GameId = 5, Name = "Gra5"}
            });

            AdminController controller = new AdminController(mock.Object);

            List<Game> result = ((IEnumerable<Game>)controller.Index().
                ViewData.Model).ToList();

            Assert.AreEqual(result.Count(), 5);
            Assert.AreEqual("Gra1", result[0].Name);
            Assert.AreEqual("Gra2", result[1].Name);
            Assert.AreEqual("Gra3", result[2].Name);
        }
        [TestMethod]
        public void Can_Edit_Game()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
    {
        new Game { GameId = 1, Name = "Gra1"},
        new Game { GameId = 2, Name = "Gra2"},
        new Game { GameId = 3, Name = "Gra3"},
        new Game { GameId = 4, Name = "Gra4"},
        new Game { GameId = 5, Name = "Gra5"}
    });

            AdminController controller = new AdminController(mock.Object);

            Game game1 = controller.Edit(1).ViewData.Model as Game;
            Game game2 = controller.Edit(2).ViewData.Model as Game;
            Game game3 = controller.Edit(3).ViewData.Model as Game;

            Assert.AreEqual(1, game1.GameId);
            Assert.AreEqual(2, game2.GameId);
            Assert.AreEqual(3, game3.GameId);
        }
        [TestMethod]
        public void Can_Delete_Valid_Games()
        {
            // Tworzenie objekta Game
            Game game = new Game { GameId = 2, Name = "Gra2" };

            // tworzenie symulowanej bazy danych
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
    {
        new Game { GameId = 1, Name = "Gra1"},
        new Game { GameId = 2, Name = "Gra2"},
        new Game { GameId = 3, Name = "Gra3"},
        new Game { GameId = 4, Name = "Gra4"},
        new Game { GameId = 5, Name = "Gra5"}
    });

            // Tworzenie kontrolera
            AdminController controller = new AdminController(mock.Object);

            // Usuwanie gry
            controller.Delete(game.GameId);

            //Sprawdzamy czy metoda usuwania gry z bazy danych wyzywa się dla konkretnego objektu Game
            mock.Verify(m => m.DeleteGame(game.GameId));
        }
    }
}
