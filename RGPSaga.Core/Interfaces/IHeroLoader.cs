namespace RpgSaga.Core.Interfaces
{
    using System.Collections.Generic;
    using RpgSaga.Core.Entities;

    public interface IHeroLoader
    {
        public List<Hero> LoadHeroes();
    }
}
