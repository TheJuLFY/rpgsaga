namespace RpgSaga.Core.Entities
{
    using System.Collections.Generic;
    using RpgSaga.Core.Data;
    using RpgSaga.Core.Effects;
    using RpgSaga.Core.Interfaces;

    public abstract class Hero
    {
        private readonly IEventLogger _eventLogger;
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private int _hp;

        public Hero(IEventLogger eventLogger, IRandomNumberGenerator randomNumberGenerator)
        {
            Effects = new List<IEffect>();
            ShouldSkipMove = false;
            _eventLogger = eventLogger;
            _randomNumberGenerator = randomNumberGenerator;
        }

        public string Name { get; set; }

        public int Hp
        {
            get
            {
                return _hp;
            }

            set
            {
                _hp = value > 0 ? value : 0;
            }
        }

        public int StartHp { get; set; }

        public int Power { get; set; }

        public List<ISkill> Skills { get; set; }

        public List<IEffect> Effects { get; set; }

        public bool ShouldSkipMove { get; set; }

        public abstract void SetupHero(string name);

        public abstract void SetupHeroFromJson(HeroDto model);

        public abstract HeroDto ConvertHeroToHeroDto();

        public void DoMove(Hero enemy)
        {
            CheckSkipMove();
            HitTheEnemy(enemy);
            TriggerEffects();
        }

        public void RefreshAfterFight()
        {
            Hp = StartHp;
            Effects = new List<IEffect>();
        }

        protected void CheckSkipMove()
        {
            foreach (IEffect effect in Effects)
            {
                if (effect is SkipMove)
                {
                    effect.GetEffect(this);
                    break;
                }
            }
        }

        protected virtual void HitTheEnemy(Hero enemy)
        {
            if (ShouldSkipMove)
            {
                return;
            }

            while (true)
            {
                ISkill currentSkill = Skills[_randomNumberGenerator.CreateRandomNumber(0, Skills.Count)];
                if (currentSkill.SkillCanBeUsed && _randomNumberGenerator.CreateRandomNumber(1, 11) <= currentSkill.ChanceOfUsing)
                {
                    currentSkill.UseSkill(this, enemy);
                    break;
                }
            }
        }

        protected void TriggerEffects()
        {
            Effects.RemoveAll(effect => effect.Duration == 0);
            ShouldSkipMove = false;

            foreach (IEffect effect in Effects)
            {
                if (effect is SkipMove)
                {
                    continue;
                }

                effect.GetEffect(this);
            }
        }
    }
}
