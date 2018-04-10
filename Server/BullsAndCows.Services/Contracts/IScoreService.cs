namespace BullsAndCows.Services.Contracts
{
    using BullsAndCows.Services.DTO;
    using System.Collections.Generic;

    public interface IScoreService
    {
        IEnumerable<HighScoreDto> GetHighScores();
    }
}
