namespace BullsAndCows.Web.ViewModels
{
    using System.Collections.Generic;

    public class GameStateViewModel
    {
        public GameStateViewModel()
        {
            this.GameViewModel = new GameViewModel();
            this.PlayerGuesses = new HashSet<GuessViewModel>();
            this.ComputerGuesses = new HashSet<GuessViewModel>();
        }

        public GameViewModel GameViewModel { get; set; }

        public ICollection<GuessViewModel> PlayerGuesses { get; set; }

        public ICollection<GuessViewModel> ComputerGuesses { get; set; }
    }
}