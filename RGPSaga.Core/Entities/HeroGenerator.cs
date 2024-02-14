namespace RpgSaga.Core.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RpgSaga.Core.Data;
    using RpgSaga.Core.Interfaces;

    public class HeroGenerator : IHeroGenerator
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly INameGenerator _nameGenerator;
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly IEventLogger _eventLogger;
        private List<Type> _types = new List<Type>();

        public HeroGenerator(IServiceProvider serviceProvider, INameGenerator nameGenerator, IRandomNumberGenerator randomNumberGenerator, IEventLogger eventLogger)
        {
            _serviceProvider = serviceProvider;
            _nameGenerator = nameGenerator;
            _randomNumberGenerator = randomNumberGenerator;
            _eventLogger = eventLogger;

            RegisterType<Undead>();
            RegisterType<Witcher>();
            RegisterType<Barbarian>();
        }

        public Hero Generate()
        {
            int randomIndex = _randomNumberGenerator.CreateRandomNumber(0, _types.Count);
            Type type = _types[randomIndex];
            Hero hero = (Hero)_serviceProvider.GetService(type);
            string name = _nameGenerator.GetRandomName();
            hero!.SetupHero(name);

            return hero;
        }

        public Hero Generate(HeroDto model)
        {
            Type type = _types.Single(x => x.Name == model.Type);
            Hero hero = (Hero)_serviceProvider.GetService(type);
            hero!.SetupHeroFromJson(model);

            return hero;
        }

        private void RegisterType<THero>()
            where THero : Hero
        {
            _types.Add(typeof(THero));
        }
    }
}
