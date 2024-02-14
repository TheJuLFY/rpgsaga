namespace RpgSaga.Core.Interfaces
{
    using System.Collections.Generic;
    using RpgSaga.Core.Data;

    public interface IHeroJsonReader
    {
        public List<HeroDto> DeserializeHeroFromJson(string filename);
    }
}
