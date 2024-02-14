namespace RpgSaga.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using RpgSaga.Core.BattleLogic;
    using RpgSaga.Core.Deserialization;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;
    using RPGSaga.Core.Loggers;

    public class DIConfig
    {
        public static void CreateListOfDI(IServiceCollection services)
        {
            services.AddHostedService<Game>();
            services.AddScoped<IHeroGenerator, HeroGenerator>();
            services.AddScoped<IHeroLoader, HeroLoader>();
            services.AddScoped<IHeroJsonReader, HeroJsonReader>();
            services.AddScoped<IHeroJsonWriter, HeroJsonWriter>();
            services.AddScoped<IProcessArgumentsReader, ProcessArgumentsReader>();
            services.AddScoped<IRandomNumberGenerator, RandomNumberGenerator>();
            services.AddScoped<IUserInputReader, UserInputReader>();
            services.AddScoped<IEventLogger, EventLogger>();
            services.AddScoped<ILogger, Logger>();
            services.AddScoped<INameGenerator, NameGenerator>();
            services.AddScoped<IRound, Round>();
            services.AddScoped<IDuel, Duel>();
            services.AddTransient<Barbarian>();
            services.AddTransient<Undead>();
            services.AddTransient<Witcher>();
        }
    }
}
