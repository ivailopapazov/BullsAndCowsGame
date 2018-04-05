namespace BullsAndCows.Web.Controllers
{
    using BullsAndCows.Models;
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

            return View();
        }

        public ActionResult Play()
        {
            GameStateViewModel gameState;

            // TODO: Check if user has already started game
            var currentGame = this.games.GetCurrentGame(this.User.Identity.GetUserId());
            if (currentGame != null)
            {
                gameState = this.GetGameState(currentGame);
            }
            else
            {
                gameState = new GameStateViewModel();
            }


            return View(gameState);
        }

        private GameStateViewModel GetGameState(Game currentGame)
        {
            var gameViewModel = new GameViewModel()
            {
                Id = currentGame.Id,
                PlayerNumber = currentGame.PlayerNumber,
                DateCreated = currentGame.DateCreated,
            };

            // TODO: separate guesses when computer guesses
            var playerGuessList = currentGame.Guesses
                .AsQueryable()
                .Select(GuessViewModel.FromGuess)
                .ToList();

            var result = new GameStateViewModel()
            {
                GameViewModel = gameViewModel,
                PlayerGuesses = playerGuessList
            };

            return result;
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