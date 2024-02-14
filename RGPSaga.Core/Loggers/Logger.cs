namespace RPGSaga.Core.Loggers
{
    using System;
    using RpgSaga.Core.Interfaces;

    public class Logger : ILogger
    {
        public void LogMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
