namespace BullsAndCows.Logic.Contracts
{
    using BullsAndCows.Logic.Models;
    using System.Collections.Generic;

    public interface IGameManager
    {
        string GenerateGameNumber();

        GuessResult CheckNumber(string guess, string number);

        string GenerateGuessNumber(IEnumerable<GuessResult> currentGuesses);
    }
}
