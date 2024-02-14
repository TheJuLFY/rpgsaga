namespace RpgSaga.Core.Skills
{
    using RpgSaga.Core.Effects;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;

    public class FearSkill : ISkill
    {
        private readonly IEventLogger _eventLogger;

        public FearSkill(IEventLogger eventLogger)
        {
            SkillCanBeUsed = true;
            ChanceOfUsing = 5;
            _eventLogger = eventLogger;
            HpCost = 5;
            TimeOfAction = 1;
        }

        public int TimeOfAction { get; set; }

        public int HpCost { get; set; }

        public bool SkillCanBeUsed { get; set; }

        public int ChanceOfUsing { get; set; }

        public void UseSkill(Hero attacker, Hero defender)
        {
            defender.Effects.Add(new SkipMove(TimeOfAction, _eventLogger));
            attacker.Hp -= HpCost;

            string skillInfo = $"Enemy skip his next move, but hero pays {HpCost} HP for using skill!";
            _eventLogger.LogSkill(attacker, defender, this, skillInfo);
        }
    }
}
