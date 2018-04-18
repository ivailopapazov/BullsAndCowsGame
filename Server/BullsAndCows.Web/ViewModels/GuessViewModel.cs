namespace BullsAndCows.Web.ViewModels
{
    using BullsAndCows.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    public class GuessViewModel
    {
        public static Expression<Func<Guess, GuessViewModel>> FromGuess
        {
            get
            {
                return guess => new GuessViewModel
                {
                    Id = guess.Id,
                    Number = guess.Number,
                    BullsCount = guess.BullsCount,
                    CowsCount = guess.CowsCount,
                    DateCreated = guess.DateCreated
                };
            }
        }

        public int Id { get; set; }

        [MinLength(4)]
        [MaxLength(4)]
        [RegularExpression(@"^[1-9]", ErrorMessage = "Use only digits from 1 to 9")]
        public string Number { get; set; }

        public int BullsCount { get; set; }

        public int CowsCount { get; set; }

        public DateTime DateCreated { get; set; }
    }
}