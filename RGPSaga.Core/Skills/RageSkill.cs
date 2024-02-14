namespace RpgSaga.Core.Skills
{
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;

    public class RageSkill : ISkill
    {
        private readonly IEventLogger _eventLogger;
        private int _damageFromSkill;

        public RageSkill(IEventLogger eventLogger)
        {
            SkillCanBeUsed = true;
            ChanceOfUsing = 1;
            _eventLogger = eventLogger;
            DamageCoefficient = 1.3m;
        }

        public decimal DamageCoefficient { get; set; }

        public bool SkillCanBeUsed { get; set; }

        public int ChanceOfUsing { get; set; }

        public void UseSkill(Hero attacker, Hero defender)
        {
            _damageFromSkill = (int)(attacker.Power * DamageCoefficient);
            defender.Hp -= _damageFromSkill;

            string skillInfo = $"Hero's damage increased by {DamageCoefficient} times!";
            _eventLogger.LogSkill(attacker, defender, this, skillInfo);
        }
    }
}
