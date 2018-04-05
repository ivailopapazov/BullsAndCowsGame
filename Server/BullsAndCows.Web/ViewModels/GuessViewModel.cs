namespace BullsAndCows.Web.ViewModels
{
    using System;

    public class GuessViewModel
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public int BullsCount { get; set; }

        public int CowsCount { get; set; }

        public DateTime DateCreated { get; set; }
    }
}