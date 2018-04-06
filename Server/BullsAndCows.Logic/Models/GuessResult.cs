namespace BullsAndCows.Logic.Models
{
    public class GuessResult
    {
        public string GuessNumber { get; set; }

        public string SecretNumber { get; set; }

        public int BullsCount { get; set; }

        public int CowsCount { get; set; }
    }
}
