namespace BullsAndCows.Web.Controllers
{
    using BullsAndCows.Services.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class GameController : Controller
    {
        private IGameService gameService;
        private List<string> numbers;

        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
            this.numbers = new List<string>() { "1234", "2345", "3456" };
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

        public ActionResult MakeGuess(string number)
        {
            this.numbers.Add(number);

            return PartialView("_GuessRow", number);
        }
    }
}