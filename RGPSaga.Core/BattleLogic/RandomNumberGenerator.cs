namespace RpgSaga.Core.BattleLogic
{
    using System;
    using RpgSaga.Core.Interfaces;

    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        public int CreateRandomNumber(int lowerBound, int upperBound)
        {
            Random rnd = new Random();
            return rnd.Next(lowerBound, upperBound);
        }
    }
}