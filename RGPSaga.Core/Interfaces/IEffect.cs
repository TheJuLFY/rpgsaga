namespace RpgSaga.Core.Interfaces
{
    using RpgSaga.Core.Entities;

    public interface IEffect
    {
        public int Duration { get; set; }

        public void GetEffect(Hero hero);
    }
}
