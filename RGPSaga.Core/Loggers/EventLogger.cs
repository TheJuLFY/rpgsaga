namespace RpgSaga.Core
{
    using System.Collections.Generic;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;

    public class EventLogger : IEventLogger
    {
        private readonly ILogger _logger;

        public EventLogger(ILogger logger)
        {
            _logger = logger;
        }

        public void LogRoundAnnouncement(List<Hero> heroes)
        {
            string message = "+++++++++++++++++++++++++++++++++++++++++++++++++" +
                             $"\nNEW ROUND: There are {heroes.Count} heroes!" +
                             $"\n+++++++++++++++++++++++++++++++++++++++++++++++++";
            _logger.LogMessage(message);
        }

        public void LogDuelAnnouncement(Hero hero1, Hero hero2)
        {
            string message = "-------------------------------------------------" +
                             $"\nNEW DUEL: {hero1.GetType().Name} {hero1.Name} vs {hero2.GetType().Name} {hero2.Name}!" +
                             $"\n-------------------------------------------------";
            _logger.LogMessage(message);
        }

        public void LogHit(Hero hero1, Hero hero2)
        {
            string hero1FullName = $"\"{hero1.GetType().Name.Substring(0, 1)}.{hero1.Name}\"";
            string hero2FullName = $"\"{hero2.GetType().Name.Substring(0, 1)}.{hero2.Name}\"";
            string message = $"{hero1FullName} hits {hero2FullName} on {hero1.Power} HP, {hero2FullName}'s HP left {hero2.Hp}.";
            _logger.LogMessage(message);
        }

        public void LogSkill(Hero hero1, Hero hero2, ISkill skill, string skillInfo)
        {
            string hero1FullName = $"\"{hero1.GetType().Name.Substring(0, 1)}.{hero1.Name}\"";
            string hero2FullName = $"\"{hero2.GetType().Name.Substring(0, 1)}.{hero2.Name}\"";
            string message = $"*SKILL* {hero1FullName} uses {skill.GetType().Name} on {hero2FullName}, he has {hero2.Hp} HP left!\n*SKILL* {skillInfo}";
            _logger.LogMessage(message);
        }

        public void LogEffect(Hero hero, IEffect effect, string effectInfo)
        {
            string heroFullName = $"\"{hero.GetType().Name.Substring(0, 1)}.{hero.Name}\"";
            string message = $"*EFFECT* {heroFullName} is under effect {effect.GetType().Name}!\n*EFFECT* {effectInfo}";
            _logger.LogMessage(message);
        }

        public void LogWinner(Hero hero)
        {
            string heroFullName = $"\"{hero.GetType().Name.ToUpper()} {hero.Name.ToUpper()}\"";
            string message = $"----{heroFullName} IS A WINNER!";
            _logger.LogMessage(message);
        }

        public void LogDraw(Hero hero1, int hero1Luck, Hero hero2, int hero2Luck)
        {
            string hero1FullName = $"\"{hero1.GetType().Name.Substring(0, 1)}.{hero1.Name}\"";
            string hero2FullName = $"\"{hero2.GetType().Name.Substring(0, 1)}.{hero2.Name}\"";
            string announcement = $"Nobody is a winner, both {hero1FullName} and {hero2FullName} will roll the dice.";
            string result = string.Empty;

            if (hero1Luck > hero2Luck)
            {
                result = $"{hero1FullName} got {hero1Luck} points vs {hero2FullName} {hero2Luck} points.\n{hero1FullName} goes further.";
            }
            else if (hero2Luck > hero1Luck)
            {
                result = $"{hero2FullName} got {hero2Luck} points vs {hero1FullName} {hero1Luck} points.\n{hero2FullName} goes further.";
            }
            else if (hero1Luck == hero2Luck)
            {
                result = $"Both rolled the same point, reroll!";
            }

            string message = $"{announcement}\n{result}";
            _logger.LogMessage(message);
        }
    }
}
