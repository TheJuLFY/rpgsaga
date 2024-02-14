namespace RpgSaga.Core.Skills
{
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;

    public class UsualPunch : ISkill
    {
        private readonly IEventLogger _eventLogger;

        public UsualPunch(IEventLogger eventLogger)
        {
            SkillCanBeUsed = true;
            ChanceOfUsing = 10;
            _eventLogger = eventLogger;
        }

        public bool SkillCanBeUsed { get; set; }

        public int ChanceOfUsing { get; set; }

        public void UseSkill(Hero hero1, Hero hero2)
        {
            hero2.Hp -= hero1.Power;

            _eventLogger.LogHit(hero1, hero2);
        }
    }
}
