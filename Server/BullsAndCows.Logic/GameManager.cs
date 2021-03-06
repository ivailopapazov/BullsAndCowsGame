﻿namespace BullsAndCows.Logic
{
    using BullsAndCows.Logic.Common;
    using BullsAndCows.Logic.Contracts;
    using BullsAndCows.Logic.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class GameManager : IGameManager
    {
        private INumberGenerator numberGenerator;

        public GameManager(INumberGenerator numberGenerator)
        {
            this.numberGenerator = numberGenerator;
        }

        public string GenerateGameNumber()
        {
            return this.numberGenerator.GenerateGameNumber();
        }

        public GuessResult CheckNumber(string guess, string number)
        {
            GuessResult result = new GuessResult()
            {
                GuessNumber = guess
            };

            for (int i = 0; i < number.Length; i++)
            {
                if (guess[i] == number[i])
                {
                    result.BullsCount++;
                }
            }

            for (int i = 0; i < number.Length; i++)
            {
                for (int j = 0; j < guess.Length; j++)
                {
                    if (number[i] == guess[j] && i != j)
                    {
                        result.CowsCount++;
                    }
                }
            }

            return result;
        }

        public string GenerateGuessNumber(IEnumerable<GuessResult> guesses)
        {
            var numberList = this.numberGenerator.GenerateNumberList();

            foreach (var actualGuess in guesses)
            {
                var currentNode = numberList.First;

                while (currentNode != null)
                {
                    var nextNode = currentNode.Next;

                    var actualGuessNumber = actualGuess.GuessNumber;
                    var possibleGuessNumber = currentNode.Value;
                    var possibleGuess = this.CheckNumber(possibleGuessNumber, actualGuessNumber);

                    if (possibleGuess.BullsCount != actualGuess.BullsCount
                        || possibleGuess.CowsCount != actualGuess.CowsCount)
                    {
                        numberList.Remove(currentNode);
                    }

                    currentNode = nextNode;
                }
            }

            return numberList.First.Value;
        }
    }
}
