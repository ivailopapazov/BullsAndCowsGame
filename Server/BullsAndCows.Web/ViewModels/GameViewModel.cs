namespace BullsAndCows.Web.ViewModels
{
    using System;

    public class GameViewModel
    {
        public int Id { get; set; }

        public string PlayerNumber { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsVictory { get; set; }
    }
}