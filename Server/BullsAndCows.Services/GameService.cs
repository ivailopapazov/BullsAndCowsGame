﻿namespace BullsAndCows.Services
{
    using BullsAndCows.Data;
    using BullsAndCows.Logic.Contracts;
    using BullsAndCows.Logic.Models;
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

        public Game StartGame(string playerNumber, string userId)
        {
            string computerNumber = this.gameManager.GenerateGameNumber();

            Game newGame = new Game()
            {
                PlayerNumber = playerNumber,
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

        public Game GetLastGame(string userId)
        {
            Game lastGame = this.games.All()
                .Where(g => g.IsFinished == true && g.UserId == userId)
                .OrderByDescending(g => g.DateCreated)
                .FirstOrDefault();

            return lastGame;
        }

        // TODO: Remove code repetition
        public Guess MakeComputerGuess(string userId)
        {
            Game currentGame = this.GetCurrentGame(userId);

            var computerGuessList = currentGame.Guesses
                .Where(g => g.UserId == null)
                .Select(g => new GuessResult()
                {
                    GuessNumber = g.Number,
                    BullsCount = g.BullsCount,
                    CowsCount = g.CowsCount
                });

            var computerGuessNumber = this.gameManager.GenerateGuessNumber(computerGuessList);
            var guessResult = this.gameManager.CheckNumber(computerGuessNumber, currentGame.PlayerNumber);

            Guess newGuess = new Guess()
            {
                GameId = currentGame.Id,
                Number = guessResult.GuessNumber,
                DateCreated = DateTime.UtcNow,
                UserId = null,
                BullsCount = guessResult.BullsCount,
                CowsCount = guessResult.CowsCount,
            };

            this.guesses.Add(newGuess);
            this.guesses.SaveChanges();

            return newGuess;
        }

        public Guess MakeUserGuess(string userId, string guessNumber)
        {
            Game currentGame = this.GetCurrentGame(userId);

            GuessResult guessResult = this.gameManager.CheckNumber(guessNumber, currentGame.ComputerNumber);

            Guess newGuess = new Guess()
            {
                GameId = currentGame.Id,
                Number = guessResult.GuessNumber,
                DateCreated = DateTime.UtcNow,
                UserId = userId,
                BullsCount = guessResult.BullsCount,
                CowsCount = guessResult.CowsCount,
            };

            this.guesses.Add(newGuess);
            this.guesses.SaveChanges();

            return newGuess;
        }

        public void EndGame(string userId, bool isVictory)
        {
            Game currentGame = this.GetCurrentGame(userId);

            currentGame.IsFinished = true;
            currentGame.IsVictory = isVictory;

            this.games.SaveChanges();
        }
    }
}
