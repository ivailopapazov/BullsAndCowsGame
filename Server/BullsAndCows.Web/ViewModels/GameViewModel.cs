namespace BullsAndCows.Web.ViewModels
{
    using BullsAndCows.Web.ViewModels.Validation;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GameViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Number must contain 4 digits!")]
        [MaxLength(4, ErrorMessage = "Number must contain 4 digits!")]
        [DistinctCharacters(ErrorMessage = "All digits must be distinct!")]
        [RegularExpression("^[1-9]*$", ErrorMessage = "Use only digits from 1 to 9")]
        [Display(Name = "Secret Number")]
        public string PlayerNumber { get; set; }

        public DateTime DateCreated { get; set; }

        public bool IsVictory { get; set; }
    }
}