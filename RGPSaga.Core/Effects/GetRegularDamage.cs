namespace RpgSaga.Core.Effects
{
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;

    public class GetRegularDamage : IEffect
    {
        private readonly IEventLogger _eventLogger;

        public GetRegularDamage(int regularDamage, IEventLogger eventLogger)
        {
            RegularDamage = regularDamage;
            Duration = 1;
            _eventLogger = eventLogger;
        }

        public int RegularDamage { get; set; }

        public int Duration { get; set; }

        public void GetEffect(Hero hero)
        {
            hero.Hp -= RegularDamage;
            string effectInfo = $"Hero lose {RegularDamage} HP. His left HP is {hero.Hp}!";

            _eventLogger.LogEffect(hero, this, effectInfo);
        }
    }
}