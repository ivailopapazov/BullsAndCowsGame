namespace BullsAndCows.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Guess
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(4)]
        public string Number { get; set; }

        public int BullsCount { get; set; }

        public int CowsCount { get; set; }

        public DateTime DateCreated { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}
