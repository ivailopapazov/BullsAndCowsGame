namespace BullsAndCows.Services.Contracts
{
    using BullsAndCows.Models;

    public interface IGameService
    {
        Game StartGame(string playerNumber, string userId);

        Game GetCurrentGame(string userId);

        Game GetLastGame(string userId);

        Guess MakeGuess(string userId, bool isComputerGuess = true, string guessNumber = null);

        void EndGame(string userId, bool isVictory);
    }
}
