namespace BullsAndCows.Logic
{
    using BullsAndCows.Logic.Common;
    using BullsAndCows.Logic.Contracts;

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
    }
}
