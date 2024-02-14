namespace RpgSaga.Core.Interfaces
{
    using System.Collections.Generic;
    using RpgSaga.Core.Entities;

    public interface IRound
    {
        public List<Hero> StartRound(List<Hero> heroes);
    }
}
