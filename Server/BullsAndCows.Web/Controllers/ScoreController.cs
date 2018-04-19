namespace BullsAndCows.Web.Controllers
{
    using BullsAndCows.Services.Contracts;
    using BullsAndCows.Web.ViewModels;
    using System.Linq;
    using System.Web.Mvc;

    public class ScoreController : Controller
    {
        private IScoreService scores;

        public ScoreController(IScoreService scores)
        {
            this.scores = scores;
        }

        public ActionResult Index()
        {
            var highScores = this.scores.GetHighScores()
                .AsQueryable()
                .Select(HighScoreViewModel.FromHighScoreDto)
                .ToList();

            return View(highScores);
        }
    }
}