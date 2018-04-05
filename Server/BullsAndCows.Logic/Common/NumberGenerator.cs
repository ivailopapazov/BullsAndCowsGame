namespace BullsAndCows.Logic.Common
{
    using System;
    using System.Collections.Generic;

    public class NumberGenerator : INumberGenerator
    {
        private const int GameNumberLength = 4;

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
    }
}
