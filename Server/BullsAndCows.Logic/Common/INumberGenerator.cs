namespace BullsAndCows.Logic.Common
{
    using System.Collections.Generic;

    public interface INumberGenerator
    {
        string GenerateGameNumber();

        LinkedList<string> GenerateNumberList();
    }
}
