namespace RpgSaga.Core.Interfaces
{
    using RpgSaga.Core.Entities;

    public interface ISkill
    {
        public bool SkillCanBeUsed { get; set; }

        public int ChanceOfUsing { get; set; }

        public void UseSkill(Hero attacker, Hero defender);
    }
}
