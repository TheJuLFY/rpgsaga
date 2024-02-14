namespace RpgSaga.Core
{
    using RpgSaga.Core.Interfaces;

    public class NameGenerator : INameGenerator
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;

        private string[] _heroesNames =
        {
            "Archimonde", "Illidan", "Kel'Thuzad", "LichKing", "Ner'zhul", "Deathwing", "Arthas", "Velen",
            "Thunder-Skins", "Garona", "Hellscream", "Wrathion", "Grom", "Gul'dan", "Khadgar", "Sunstrider",
            "Kil'Jaeden", "Forgotten", "Zul'jin", "Shatt", "Deadeye", "Shadowsong", "Skullcrusher", "Doomhammer",
        };

        public NameGenerator(IRandomNumberGenerator randomNumberGenerator)
        {
            _randomNumberGenerator = randomNumberGenerator;
        }

        public string GetRandomName()
        {
            return _heroesNames[_randomNumberGenerator.CreateRandomNumber(0, _heroesNames.Length - 1)];
        }
    }
}
