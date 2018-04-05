namespace BullsAndCows.Services
{
    using BullsAndCows.Data;
    using BullsAndCows.Logic.Contracts;
    using BullsAndCows.Models;
    using BullsAndCows.Services.Contracts;
    using System;
    using System.Linq;

    public class GameService : IGameService
    {
        private IRepository<Game> games;
        private IRepository<Guess> guesses;
        private IGameManager gameManager;

        public GameService(IRepository<Game> games, IRepository<Guess> guesses, IGameManager gameManager)
        {
            this.games = games;
            this.guesses = guesses;
            this.gameManager = gameManager;
        }

        public Game StartGame(string number, string userId)
        {
            string computerNumber = this.gameManager.GenerateGameNumber();

            Game newGame = new Game()
            {
                PlayerNumber = number,
                UserId = userId,
                DateCreated = DateTime.UtcNow,
                ComputerNumber = computerNumber
            };

            this.games.Add(newGame);
            this.games.SaveChanges();

            return newGame;
        }

        public Game GetCurrentGame(string userId)
        {
            Game startedGame = this.games.All()
                .FirstOrDefault(game => game.UserId == userId && game.IsFinished == false);

            return startedGame;
        }

        public Guess MakeGuess(string number, string userId)
        {
            Game currentGame = this.GetCurrentGame(userId);

            // TODO: Call game logic layer to handle the guess

            Guess newGuess = new Guess()
            {
                GameId = currentGame.Id,
                Number = number,
                DateCreated = DateTime.UtcNow,
                UserId = userId,
                BullsCount = 1, // TODO: computed values here
                CowsCount = 2, // TODO: computed values here
                
            };

            this.guesses.Add(newGuess);
            this.guesses.SaveChanges();

            return newGuess;
        }
    }
}
