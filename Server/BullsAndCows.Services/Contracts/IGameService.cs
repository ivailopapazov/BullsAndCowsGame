namespace BullsAndCows.Services.Contracts
{
    using BullsAndCows.Models;

    public interface IGameService
    {
        Game StartGame(string number, string userId);
    }
}
