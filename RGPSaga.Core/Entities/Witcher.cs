namespace RpgSaga.Core.Entities
{
    using System.Collections.Generic;
    using RpgSaga.Core.Data;
    using RpgSaga.Core.Interfaces;
    using RpgSaga.Core.Skills;

    public class Witcher : Hero
    {
        private const int _minHp = 90;
        private const int _maxHp = 110;
        private const int _minPower = 15;
        private const int _maxPower = 25;

        private readonly IEventLogger _eventLogger;
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public Witcher(IEventLogger eventLogger, IRandomNumberGenerator randomNumberGenerator)
            : base(eventLogger, randomNumberGenerator)
        {
            _eventLogger = eventLogger;
            _randomNumberGenerator = randomNumberGenerator;
        }

        public override void SetupHero(string name)
        {
            Name = name;
            int hp = _randomNumberGenerator.CreateRandomNumber(_minHp, _maxHp);
            Hp = hp;
            StartHp = hp;
            Power = _randomNumberGenerator.CreateRandomNumber(_minPower, _maxPower);
            Skills = new List<ISkill> { new UsualPunch(_eventLogger), new BleedingSkill(_eventLogger) };
        }

        public override void SetupHeroFromJson(HeroDto model)
        {
            Name = model.Name;
            int hp = model.Hp;
            Hp = hp;
            StartHp = hp;
            Power = model.Power;
            Skills = new List<ISkill> { new UsualPunch(_eventLogger), new BleedingSkill(_eventLogger) };
        }

        public override HeroDto ConvertHeroToHeroDto()
        {
            return new HeroDto() { Type = "Witcher", Name = this.Name, Hp = this.Hp, Power = this.Power };
        }
    }
}
