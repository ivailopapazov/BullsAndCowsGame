namespace BullsAndCows.Services.Contracts
{
    using BullsAndCows.Models;

    public interface IGameService
    {
        Game StartGame(string playerNumber, string userId);

        Game GetCurrentGame(string userId);

        Guess MakeGuess(string guessNumber, string userId);
    }
}
