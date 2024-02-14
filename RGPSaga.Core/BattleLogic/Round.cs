namespace RpgSaga.Core.BattleLogic
{
    using System.Collections.Generic;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;

    public class Round : IRound
    {
        private readonly IEventLogger _eventLogger;
        private readonly IDuel _duel;
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public Round(IEventLogger eventLogger, IDuel duel, IRandomNumberGenerator randomNumberGenerator)
        {
            _eventLogger = eventLogger;
            _duel = duel;
            _randomNumberGenerator = randomNumberGenerator;
        }

        public List<Hero> StartRound(List<Hero> heroes)
        {
            List<Hero> roundHeroes = new List<Hero>(heroes);
            List<Hero> winners = new List<Hero>();

            _eventLogger.LogRoundAnnouncement(heroes);

            int numberOfDuels = heroes.Count / 2;

            for (int i = 0; i < numberOfDuels; i++)
            {
                winners.Add(CreateDuel(roundHeroes));
            }

            return winners;
        }

        private Hero ChooseRandomFighter(List<Hero> heroes)
        {
            int randomIndex = _randomNumberGenerator.CreateRandomNumber(0, heroes.Count - 1);

            Hero fighter = heroes[randomIndex];
            heroes.RemoveAt(randomIndex);

            return fighter;
        }

        private Hero CreateDuel(List<Hero> heroes)
        {
            Hero hero1 = ChooseRandomFighter(heroes);
            Hero hero2 = ChooseRandomFighter(heroes);

            Hero winner = _duel.StartDuel(hero1, hero2);

            return winner;
        }
    }
}
