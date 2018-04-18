namespace BullsAndCows.Web.Controllers
{
    using BullsAndCows.Models;
    using BullsAndCows.Services.Contracts;
    using BullsAndCows.Web.Hubs;
    using BullsAndCows.Web.ViewModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using System.Linq;
    using System.Net;
    using System.Web.Helpers;
    using System.Web.Mvc;

    public class GameController : Controller
    {
        private IGameService games;
        private IHubContext hubContext;

        public GameController(IGameService games)
        {
            this.games = games;
            this.hubContext = GlobalHost.ConnectionManager.GetHubContext<GameHub>(); // TODO: inject
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

        public ActionResult Start(GameViewModel gameViewModel) //string playerNumber
        {
            // TODO: Validate number

            var newGame = this.games.StartGame(gameViewModel.PlayerNumber, this.User.Identity.GetUserId());

            gameViewModel.Id = newGame.Id;
            gameViewModel.PlayerNumber = newGame.PlayerNumber;
            gameViewModel.DateCreated = newGame.DateCreated;

            return PartialView("_NumberControlsGameStarted", gameViewModel);
        }

        public ActionResult MakeGuess(GuessViewModel guess)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { message = "Invalid guess number" });
            }

            var userId = this.User.Identity.GetUserId();

            var newGuess = this.games.MakeUserGuess(userId, guess.Number);
            if (newGuess.BullsCount == 4)
            {
                this.games.EndGame(userId, true);
 
                this.hubContext.Clients.User(this.User.Identity.Name).endGame();
            }

            guess.Id = newGuess.Id;
            guess.BullsCount = newGuess.BullsCount;
            guess.CowsCount = newGuess.CowsCount;
            guess.DateCreated = newGuess.DateCreated;

            return PartialView("_GuessRow", guess);
        }
    }
}