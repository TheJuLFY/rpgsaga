namespace RpgSaga.Core.Interfaces
{
    using RpgSaga.Core.Entities;

    public interface IDuel
    {
        public Hero StartDuel(Hero hero1, Hero hero2);
    }
}
