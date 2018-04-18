namespace BullsAndCows.Web.ViewModels
{
    using BullsAndCows.Web.ViewModels.Validation;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GameViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        [DistinctCharacters]
        [RegularExpression("^[1-9]*$", ErrorMessage = "Use only digits from 1 to 9")]
        public string PlayerNumber { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsVictory { get; set; }
    }
}