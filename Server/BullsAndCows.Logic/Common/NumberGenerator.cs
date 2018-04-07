namespace BullsAndCows.Logic.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NumberGenerator : INumberGenerator
    {
        private const int GameNumberLength = 4;
        public const int LowestGuessNumber = 1234;
        public const int HighestGuessNumber = 9876;

        private Random random;

        public NumberGenerator()
        {
            this.random = new Random();
        }

        public string GenerateGameNumber()
        {
            List<int> digits = new List<int>();

            while(digits.Count < 4)
            {
                int newDigit = this.random.Next(1, 10); // TODO: Extract as constants
                if (!digits.Contains(newDigit))
                {
                    digits.Add(newDigit);
                }
            }

            string result = string.Join("", digits);

            return result;
        }

        public LinkedList<string> GenerateNumberList()
        {
            var numberList = new LinkedList<string>();

            for (int i = LowestGuessNumber; i <= HighestGuessNumber; i++)
            {
                if (this.IsValidGuessNumber(i))
                {
                    numberList.AddLast(i.ToString());
                }
            }

            return numberList;
        }

        private bool IsValidGuessNumber(int number)
        {
            string digits = number.ToString();
            bool isValidNumber = false;

            if (digits.Distinct().Count() == 4)
            {
                isValidNumber = true;
            }

            return isValidNumber;
        }
    }
}
