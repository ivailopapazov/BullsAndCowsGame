namespace BullsAndCows.Services
{
    using BullsAndCows.Data;
    using BullsAndCows.Models;
    using BullsAndCows.Services.Contracts;
    using System;

    public class GameService : IGameService
    {
        private IRepository<Game> games;

        public GameService(IRepository<Game> games)
        {
            this.games = games;
        }

        public Game StartGame(string number, string userId)
        {
            Game newGame = new Game()
            {
                PlayerNumber = number,
                UserId = userId,
                DateCreated = DateTime.UtcNow,
                ComputerNumber = "1234", // TODO: Generate computer numer
            };

            this.games.Add(newGame);
            this.games.SaveChanges();

            return newGame;
        }
    }
}
