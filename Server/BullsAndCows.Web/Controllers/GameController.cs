namespace BullsAndCows.Web.Controllers
{
    using BullsAndCows.Models;
    using BullsAndCows.Services.Contracts;
    using BullsAndCows.Web.Hubs;
    using BullsAndCows.Web.ViewModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.SignalR;
    using System.Linq;
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

        public ActionResult MakeGuess(string guessNumber)
        {
            var userId = this.User.Identity.GetUserId();

            var newGuess = this.games.MakeUserGuess(userId, guessNumber);
            if (newGuess.BullsCount == 4)
            {
                this.games.EndGame(userId, true);
 
                this.hubContext.Clients.User(this.User.Identity.Name).endGame();
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