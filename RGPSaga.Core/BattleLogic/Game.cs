namespace RpgSaga.Core.BattleLogic
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;

    public class Game : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _hostApplicationLifetime;
        private readonly IRound _round;
        private readonly IHeroLoader _heroLoader;

        public Game(
            ILogger logger,
            IHostApplicationLifetime hostApplicationLifetime,
            IRound round,
            IHeroLoader heroLoader)
        {
            _logger = logger;
            _hostApplicationLifetime = hostApplicationLifetime;
            _round = round;
            _heroLoader = heroLoader;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            List<Hero> allHeroes = _heroLoader.LoadHeroes();
            if (allHeroes != null)
            {
                BeginGame(allHeroes);
            }

            _hostApplicationLifetime.StopApplication();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void BeginGame(List<Hero> allHeroes)
        {
            while (allHeroes.Count > 1)
            {
                allHeroes = _round.StartRound(allHeroes);
            }

            _logger.LogMessage($"GAMEOVER! The winner of this game is {allHeroes[0].GetType().Name} {allHeroes[0].Name}!");
        }
    }
}