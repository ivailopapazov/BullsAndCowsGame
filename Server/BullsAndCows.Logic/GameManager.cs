namespace BullsAndCows.Logic
{
    using BullsAndCows.Logic.Common;
    using BullsAndCows.Logic.Contracts;
    using BullsAndCows.Logic.Models;

    public class GameManager : IGameManager
    {
        private INumberGenerator numberGenerator;

        public GameManager(INumberGenerator numberGenerator)
        {
            this.numberGenerator = numberGenerator;
        }

        public string GenerateGameNumber()
        {
            return this.numberGenerator.GenerateGameNumber();
        }

        public GuessResult CheckNumber(string guess, string number)
        {
            GuessResult result = new GuessResult()
            {
                GuessNumber = guess,
                SecretNumber = number
            };

            for (int i = 0; i < number.Length; i++)
            {
                if (guess[i] == number[i])
                {
                    result.BullsCount++;
                }
            }

            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < guess.Length; j++)
                {
                    if (number[i] == guess[j] && i != j)
                    {
                        result.CowsCount++;
                    }
                }
            }

            return result;
        }

        public GuessResult ComputerMakeGuess(string playerNumber)
        {
            // TODO: Implement something smart here
            var computerGuess = this.numberGenerator.GenerateGameNumber();
            var result = this.CheckNumber(computerGuess, playerNumber);

            return result;
        }
    }
}
