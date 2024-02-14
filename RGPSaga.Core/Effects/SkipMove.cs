namespace RpgSaga.Core.Effects
{
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;

    public class SkipMove : IEffect
    {
        private readonly IEventLogger _eventLogger;

        public SkipMove(int duration, IEventLogger eventLogger)
        {
            Duration = duration;
            _eventLogger = eventLogger;
        }

        public int Duration { get; set; }

        public void GetEffect(Hero hero)
        {
            hero.ShouldSkipMove = true;
            Duration--;
            string effectInfo = $"Hero skip move!";

            _eventLogger.LogEffect(hero, this, effectInfo);
        }
    }
}
