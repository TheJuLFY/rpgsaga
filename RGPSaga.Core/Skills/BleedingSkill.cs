namespace RpgSaga.Core.Skills
{
    using RpgSaga.Core.Effects;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;

    public class BleedingSkill : ISkill
    {
        private readonly IEventLogger _eventLogger;

        public BleedingSkill(IEventLogger eventLogger)
        {
            SkillCanBeUsed = true;
            ChanceOfUsing = 2;
            _eventLogger = eventLogger;
            DamageOneTime = 10;
            DamagePerMove = 5;
        }

        public int DamageOneTime { get; set; }

        public int DamagePerMove { get; set; }

        public bool SkillCanBeUsed { get; set; }

        public int ChanceOfUsing { get; set; }

        public void UseSkill(Hero attacker, Hero defender)
        {
            if (SkillCanBeUsed)
            {
                defender.Hp -= DamageOneTime;
                defender.Effects.Add(new GetRegularDamage(DamagePerMove, _eventLogger));
                SkillCanBeUsed = false;

                string skillInfo = $"Enemy lose {DamagePerMove} HP every own move!";
                _eventLogger.LogSkill(attacker, defender, this, skillInfo);
            }
        }
    }
}
