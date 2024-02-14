namespace RpgSaga.Core.Interfaces
{
    using System.Collections.Generic;
    using RpgSaga.Core.Data;

    public interface IHeroJsonWriter
    {
        public void SerializeHeroToJson(List<HeroDto> heroes, string filename);
    }
}
