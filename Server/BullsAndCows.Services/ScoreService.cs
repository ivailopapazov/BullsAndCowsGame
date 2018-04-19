namespace BullsAndCows.Services
{
    using BullsAndCows.Data;
    using BullsAndCows.Models;
    using BullsAndCows.Services.Contracts;
    using BullsAndCows.Services.DTO;
    using System.Collections.Generic;
    using System.Linq;

    public class ScoreService : IScoreService
    {
        private IRepository<Game> games;

        public ScoreService(IRepository<Game> games)
        {
            this.games = games;
        }

        public IEnumerable<HighScoreDto> GetHighScores()
        {
            var highScoreResult = this.games
                .All()
                .Where(g => g.IsFinished)
                .GroupBy(g => g.User)
                .Select(group => new HighScoreDto
                {
                    UserName = group.Key.UserName,
                    WinCount = group.Count(g => g.IsVictory),
                    LoseCount = group.Count(g => !g.IsVictory)
                })
                .OrderByDescending(r => r.WinCount)
                .Take(25);

            return highScoreResult;
        }
    }
}
