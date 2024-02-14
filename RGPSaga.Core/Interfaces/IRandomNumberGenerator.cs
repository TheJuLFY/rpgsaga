namespace RpgSaga.Core.Interfaces
{
    public interface IRandomNumberGenerator
    {
        public int CreateRandomNumber(int lowerBound, int upperBound);
    }
}
