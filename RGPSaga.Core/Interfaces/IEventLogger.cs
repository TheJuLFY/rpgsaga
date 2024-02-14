namespace RpgSaga.Core.Interfaces
{
    using System.Collections.Generic;
    using RpgSaga.Core.Entities;

    public interface IEventLogger
    {
        public void LogRoundAnnouncement(List<Hero> heroes);

        public void LogDuelAnnouncement(Hero hero1, Hero hero2);

        public void LogHit(Hero hero1, Hero hero2);

        public void LogSkill(Hero hero1, Hero hero2, ISkill skill, string skillInfo);

        public void LogEffect(Hero hero, IEffect effect, string effectInfo);

        public void LogWinner(Hero hero);

        public void LogDraw(Hero hero1, int hero1Luck, Hero hero2, int hero2Luck);
    }
}
