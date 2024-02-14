namespace RpgSaga.Core.Interfaces
{
    public interface ILogger
    {
        public void LogMessage(string message);

        public void LogError(string message);
    }
}
