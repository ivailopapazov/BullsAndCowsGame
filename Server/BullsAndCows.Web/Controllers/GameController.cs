namespace BullsAndCows.Web.Controllers
{
    using BullsAndCows.Models;
    using BullsAndCows.Services.Contracts;
    using BullsAndCows.Web.ViewModels;
    using Microsoft.AspNet.Identity;
    using System.Linq;
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

            var currentGame = this.games.GetCurrentGame(this.User.Identity.GetUserId());
            if (currentGame != null)
            {
                gameState = this.RebuildGameState(currentGame);
            }
            else
            {
                gameState = new GameStateViewModel();
            }


            return View(gameState);
        }

        public ActionResult Results()
        {
            var lastGame = this.games.GetLastGame(this.User.Identity.GetUserId());
            var gameStateViewModel = this.RebuildGameState(lastGame);

            return View(gameStateViewModel);
        }

        private GameStateViewModel RebuildGameState(Game game)
        {
            var gameViewModel = new GameViewModel()
            {
                Id = game.Id,
                PlayerNumber = game.PlayerNumber,
                DateCreated = game.DateCreated,
                IsVictory = game.IsVictory
            };

            var playerGuessList = game.Guesses
                .AsQueryable()
                .Where(guess => guess.UserId != null)
                .Select(GuessViewModel.FromGuess)
                .ToList();

            var computerGuessList = game.Guesses
                .AsQueryable()
                .Where(guess => guess.UserId == null)
                .Select(GuessViewModel.FromGuess)
                .ToList();

            var result = new GameStateViewModel()
            {
                GameViewModel = gameViewModel,
                PlayerGuesses = playerGuessList,
                ComputerGuesses = computerGuessList
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

        public ActionResult MakeGuess(bool isComputerGuess = true, string guessNumber = null)
        {
            var userId = this.User.Identity.GetUserId();
            var newGuess = this.games.MakeGuess(userId, isComputerGuess, guessNumber);
            if (newGuess.BullsCount == 4)
            {
                bool isVictory = !isComputerGuess;
                this.games.EndGame(userId, isVictory);

                // TODO: Send to score service

                return JavaScript("window.location = '/Game/Results'");
            }

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