namespace RpgSaga.Core.BattleLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RpgSaga.Core.Data;
    using RpgSaga.Core.Entities;
    using RpgSaga.Core.Interfaces;

    public class HeroLoader : IHeroLoader
    {
        private readonly IHeroGenerator _heroGenerator;
        private readonly IHeroJsonReader _heroJsonReader;
        private readonly IHeroJsonWriter _heroJsonWriter;
        private readonly IProcessArgumentsReader _processArgumentsReader;
        private readonly ILogger _logger;
        private readonly IUserInputReader _userInputReader;
        private string _loadFileName;
        private string _saveFileName;

        public HeroLoader(
            IHeroGenerator herogenerator,
            IHeroJsonReader heroJsonReader,
            IHeroJsonWriter heroJsonWriter,
            IProcessArgumentsReader processArgumentsReader,
            ILogger logger,
            IUserInputReader userInputReader)
        {
            _heroGenerator = herogenerator;
            _heroJsonReader = heroJsonReader;
            _heroJsonWriter = heroJsonWriter;
            _processArgumentsReader = processArgumentsReader;
            _logger = logger;
            _userInputReader = userInputReader;
        }

        public List<Hero> LoadHeroes()
        {
            string[] args = _processArgumentsReader.GetProcessArguments();

            return GenerateHeroesFromJson(args) ??
                   GenerateHeroesFromArgs(args) ??
                   GenerateHeroesFromUserInput(args);
        }

        private List<Hero> GenerateHeroesFromJson(string[] args)
        {
            if (!args.Contains("-i"))
            {
                return null;
            }

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-i")
                {
                    _loadFileName = args[i + 1];
                    break;
                }
            }

            List<Hero> heroes = new List<Hero>();
            List<HeroDto> heroesDto = _heroJsonReader.DeserializeHeroFromJson(_loadFileName);

            foreach (HeroDto heroDto in heroesDto)
            {
                heroes.Add(_heroGenerator.Generate(heroDto));
            }

            if (args.Contains("-s"))
            {
                SaveHeroesToJson(heroes, args);
            }

            return heroes;
        }

        private List<Hero> GenerateHeroesFromArgs(string[] args)
        {
            if (!args.Contains("-k"))
            {
                return null;
            }

            string numberOfHeroesArg = string.Empty;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-k")
                {
                    numberOfHeroesArg = args[i + 1];
                    break;
                }
            }

            List<Hero> heroes = new List<Hero>();

            try
            {
                int numberOfHeroes = int.Parse(numberOfHeroesArg);
                if (ValidNumberOfHeroes(numberOfHeroes))
                {
                    heroes = GenerateHeroes(numberOfHeroes);
                }
                else
                {
                    throw new Exception("Invalid count of heroes! Input it by youself");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                int numberOfHeroes = AskNumberOfHeroes();

                if (numberOfHeroes == 0)
                {
                    return null;
                }

                heroes = GenerateHeroes(numberOfHeroes);
            }

            if (args.Contains("-s"))
            {
                SaveHeroesToJson(heroes, args);
            }

            return heroes;
        }

        private List<Hero> GenerateHeroesFromUserInput(string[] args)
        {
            List<Hero> heroes = new List<Hero>();

            int numberOfHeroes = AskNumberOfHeroes();

            if (numberOfHeroes == 0)
            {
                return null;
            }

            heroes = GenerateHeroes(numberOfHeroes);

            if (args.Contains("-s"))
            {
                SaveHeroesToJson(heroes, args);
            }

            return heroes;
        }

        private List<Hero> GenerateHeroes(int numberOfHeroes)
        {
            List<Hero> heroes = new List<Hero>();

            for (int i = 0; i < numberOfHeroes; i++)
            {
                heroes.Add(_heroGenerator.Generate());
            }

            return heroes;
        }

        private int AskNumberOfHeroes()
        {
            while (true)
            {
                _logger.LogMessage("Enter the number of heroes to the power of 2");
                string enter = _userInputReader.ReadLine();
                if (enter == "exit")
                {
                    return 0;
                }

                if (int.TryParse(enter, out int result))
                {
                    if (ValidNumberOfHeroes(result))
                    {
                        _logger.LogMessage("Let's the Game begin!");
                        return result;
                    }
                    else
                    {
                        _logger.LogError("You entered the number not to the power of 2, please, try again");
                        continue;
                    }
                }

                _logger.LogError("You entered not the number, please, try again");
            }
        }

        private bool ValidNumberOfHeroes(int numberOfHeroes)
        {
            int initValue = 1;
            while (initValue < numberOfHeroes)
            {
                initValue *= 2;
            }

            if (initValue == numberOfHeroes && numberOfHeroes > 1)
            {
                return true;
            }

            return false;
        }

        private void SaveHeroesToJson(List<Hero> heroes, string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-s")
                {
                    _saveFileName = args[i + 1];
                    break;
                }
            }

            List<HeroDto> dtoModels = new List<HeroDto>();

            foreach (Hero hero in heroes)
            {
                dtoModels.Add(hero.ConvertHeroToHeroDto());
            }

            _heroJsonWriter.SerializeHeroToJson(dtoModels, _saveFileName);
            _logger.LogMessage("Heroes successfully saved to set file");
        }
    }
}
