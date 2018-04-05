namespace BullsAndCows.Web.Controllers
{
    using BullsAndCows.Services.Contracts;
    using BullsAndCows.Web.ViewModels;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class GameController : Controller
    {
        private IGameService games;

        public GameController(IGameService games)
        {
            this.games = games;
        }

        public ActionResult Index()
        {
            // TODO: Check if user has already started game

            return View();
        }

        public ActionResult Play()
        {
            var gameState = new GameStateViewModel();

            return View(gameState);
        }

        public ActionResult Start(string playerNumber)
        {
            // TODO: Validate number

            var newGame = this.games.StartGame(playerNumber, this.User.Identity.GetUserId());

            var result = new GameViewModel()
            {
                Id = newGame.Id,
                PlayerNumber = newGame.PlayerNumber,
                DateCreated = newGame.DateCreated
            };

            return PartialView("_NumberControlsGameStarted", result);
        }

        public ActionResult MakeGuess(string playerGuess)
        {
            var newGuess = this.games.MakeGuess(playerGuess, this.User.Identity.GetUserId());

            var result = new GuessViewModel()
            {
                Id = newGuess.Id,
                Number = newGuess.Number,
                BullsCount = newGuess.BullsCount,
                CowsCount = newGuess.CowsCount,
                DateCreated = newGuess.DateCreated
            };

            return PartialView("_GuessRow", result);
        }
    }
}