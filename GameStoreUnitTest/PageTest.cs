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

namespace GameStoreUnitTest
{

    [TestClass]
    public class PageTest
    {
        [TestMethod]
        public void Can_Paginate()
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
            GameController controller = new GameController(mock.Object);
            //ilosc gier na 1 stronie
            controller.pageSize = 3;

            // 2 listy
            GamesListViewModel result = (GamesListViewModel)controller.List(null,2).Model;

            // na 2 liście będzie tylko 2 gry
            List<Game> games = result.Games.ToList();
            Assert.IsTrue(games.Count == 2);
            Assert.AreEqual(games[0].Name, "Gra4");
            Assert.AreEqual(games[1].Name, "Gra5");
        }

        [TestMethod]
        public void Can_Filter_Games()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
    {
        new Game { GameId = 1, Name = "Gra1", Category="Shooter"},
        new Game { GameId = 2, Name = "Gra2", Category="Action"},
        new Game { GameId = 3, Name = "Gra3", Category="Shooter"},
        new Game { GameId = 4, Name = "Gra4", Category="Action"},
        new Game { GameId = 5, Name = "Gra5", Category="RPG"}
    });
            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            List<Game> result = ((GamesListViewModel)controller.List("Action", 1).Model)
                .Games.ToList();

            Assert.AreEqual(result.Count(), 2);
            Assert.IsTrue(result[0].Name == "Gra2" && result[0].Category == "Action");
            Assert.IsTrue(result[1].Name == "Gra4" && result[1].Category == "Action");
        }
        [TestMethod]
        public void Can_Create_Categories()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> {
        new Game { GameId = 1, Name = "Gra1", Category="Simulator"},
        new Game { GameId = 2, Name = "Gra2", Category="Simulator"},
        new Game { GameId = 3, Name = "Gra3", Category="Shooter"},
        new Game { GameId = 4, Name = "Gra4", Category="RPG"},
    });

            NavController target = new NavController(mock.Object);

            List<string> results = ((IEnumerable<string>)target.Menu().Model).ToList();

            Assert.AreEqual(results.Count(), 3);
            Assert.AreEqual(results[0], "RPG");
            Assert.AreEqual(results[1], "Shooter");
            Assert.AreEqual(results[2], "Simulator");
        }
        [TestMethod]
        public void Generate_Category_Specific_Game_Count()
        {
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
    {
        new Game { GameId = 1, Name = "Gra1", Category="RPG"},
        new Game { GameId = 2, Name = "Gra2", Category="Simulator"},
        new Game { GameId = 3, Name = "Gra3", Category="RPG"},
        new Game { GameId = 4, Name = "Gra4", Category="Simulator"},
        new Game { GameId = 5, Name = "Gra5", Category="Shooter"}
    });
            GameController controller = new GameController(mock.Object);
            controller.pageSize = 3;

            int res1 = ((GamesListViewModel)controller.List("RPG").Model).PagingInfo.TotalItems;
            int res2 = ((GamesListViewModel)controller.List("Simulator").Model).PagingInfo.TotalItems;
            int res3 = ((GamesListViewModel)controller.List("Shooter").Model).PagingInfo.TotalItems;
            int resAll = ((GamesListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            Assert.AreEqual(res1, 2);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
            Assert.AreEqual(resAll, 5);
        }
    }
}