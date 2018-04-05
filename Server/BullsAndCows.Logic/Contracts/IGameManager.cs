namespace BullsAndCows.Logic.Contracts
{
    using BullsAndCows.Logic.Models;

    public interface IGameManager
    {
        string GenerateGameNumber();

        GuessResult CheckNumber(string guess, string number);
    }
}
