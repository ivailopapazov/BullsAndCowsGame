namespace BullsAndCows.Services.Contracts
{
    using BullsAndCows.Models;

    public interface IGameService
    {
        Game StartGame(string playerNumber, string userId);

        Game GetCurrentGame(string userId);

        Game GetLastGame(string userId);

        Guess MakeComputerGuess(string userId);

        Guess MakeUserGuess(string userId, string guessNumber);

        void EndGame(string userId, bool isVictory);
    }
}
