namespace BullsAndCows.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Game
    {
        private ICollection<Guess> guesses;

        public Game()
        {
            this.guesses = new HashSet<Guess>();
            this.IsFinished = false;
            this.IsVictory = false;
        }

        [Key]
        public int Id { get; set; }
        
        public bool IsFinished { get; set; }

        public bool IsVictory { get; set; }

        public string PlayerNumber { get; set; }

        public string ComputerNumber { get; set; }

        public DateTime DateCreated { get; set; }

        [Required]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<Guess> Guesses
        {
            get { return guesses; }
            set { guesses = value; }
        }
    }
}
