namespace RpgSaga.Core.Interfaces
{
    using RpgSaga.Core.Data;
    using RpgSaga.Core.Entities;

    public interface IHeroGenerator
    {
        public Hero Generate();

        public Hero Generate(HeroDto model);
    }
}
