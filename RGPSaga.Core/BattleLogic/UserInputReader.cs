namespace RpgSaga.Core.BattleLogic
{
    using System;
    using RpgSaga.Core.Interfaces;

    public class UserInputReader : IUserInputReader
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}