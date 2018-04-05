namespace BullsAndCows.Services.Contracts
{
    using BullsAndCows.Models;

    public interface IGameService
    {
        Game StartGame(string number, string userId);

        Game GetCurrentGame(string userId);

        Guess MakeGuess(string number, string userId);
    }
}
