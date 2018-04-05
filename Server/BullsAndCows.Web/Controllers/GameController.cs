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
        private List<string> numbers;

        public GameController(IGameService games)
        {
            this.games = games;
            this.numbers = new List<string>();
        }

        public ActionResult Index()
        {
            // TODO: Check if user has already started game


            return View();
        }

        public ActionResult Play()
        {
            return View(this.numbers);
        }

        public ActionResult Start(string number)
        {
            // TODO: Validate number

            var newGame = this.games.StartGame(number, this.User.Identity.GetUserId());

            var result = new StartGameViewModel()
            {
                Id = newGame.Id,
                PlayerNumber = newGame.PlayerNumber,
                DateCreated = newGame.DateCreated
            };

            return PartialView("_SecretNumberEntered", result);
        }

        public ActionResult MakeGuess(string number)
        {
            this.numbers.Add(number);

            return PartialView("_GuessRow", number);
        }
    }
}