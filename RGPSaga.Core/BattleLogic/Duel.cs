namespace RpgSaga.Core.BattleLogic
{
    using System;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;

    public class Duel : IDuel
    {
        private readonly IEventLogger _eventLogger;
        private readonly ILogger _logger;
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        public Duel(IEventLogger eventLogger, ILogger logger, IRandomNumberGenerator randomNumberGenerator)
        {
            _eventLogger = eventLogger;
            _logger = logger;
            _randomNumberGenerator = randomNumberGenerator;
        }

        public Hero StartDuel(Hero hero1, Hero hero2)
        {
            _eventLogger.LogDuelAnnouncement(hero1, hero2);

            while (hero1.Hp > 0 && hero2.Hp > 0)
            {
                try
                {
                    hero1.DoMove(hero2);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    hero1.Hp = 0;
                    break;
                }

                if (hero2.Hp <= 0)
                {
                    break;
                }

                try
                {
                    hero2.DoMove(hero1);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    hero2.Hp = 0;
                }
            }

            Hero winner = ReturnTheWinnerBack(hero1, hero2);
            winner.RefreshAfterFight();

            _eventLogger.LogWinner(winner);

            return winner;
        }

        private Hero ReturnTheWinnerBack(Hero hero1, Hero hero2)
        {
            if (hero1.Hp > 0)
            {
                return hero1;
            }

            if (hero2.Hp > 0)
            {
                return hero2;
            }

            return ChooseRandomWinner(hero1, hero2);
        }

        private Hero ChooseRandomWinner(Hero hero1, Hero hero2)
        {
            int hero1Luck;
            int hero2Luck;

            Hero winner;

            while (true)
            {
                hero1Luck = _randomNumberGenerator.CreateRandomNumber(1, 7);
                hero2Luck = _randomNumberGenerator.CreateRandomNumber(1, 7);

                if (hero1Luck > hero2Luck)
                {
                    winner = hero1;
                    break;
                }

                if (hero2Luck > hero1Luck)
                {
                    winner = hero2;
                    break;
                }

                _eventLogger.LogDraw(hero1, hero1Luck, hero2, hero2Luck);
            }

            _eventLogger.LogDraw(hero1, hero1Luck, hero2, hero2Luck);

            return winner;
        }
    }
}