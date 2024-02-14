namespace RpgSaga.Core.Deserialization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using RpgSaga.Core.Data;
    using RpgSaga.Core.Interfaces;

    public class HeroJsonReader : IHeroJsonReader
    {
        private readonly ILogger _logger;

        public HeroJsonReader(ILogger logger)
        {
            _logger = logger;
        }

        public List<HeroDto> DeserializeHeroFromJson(string filename)
        {
            string directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string path = @$"{directory}\{filename}";
            string data = File.ReadAllText(path);
            string errorMessage;

            List<HeroDto> models = new List<HeroDto>();

            try
            {
                models = JsonConvert.DeserializeObject<List<HeroDto>>(data);

                int initValue = 1;
                while (initValue < models.Count)
                {
                    initValue *= 2;
                }

                if (initValue > models.Count || models.Count < 2)
                {
                    throw new Exception();
                }

                foreach (HeroDto model in models)
                {
                    if (model.Hp < 1 || model.Power < 1 || string.IsNullOrWhiteSpace(model.Name))
                    {
                        throw new Exception();
                    }
                }
            }
            catch (JsonReaderException)
            {
                errorMessage = "Error reading Json";
                _logger.LogError(errorMessage);
                return null;
            }
            catch (JsonSerializationException)
            {
                errorMessage = "Json is incorrect, check data";
                _logger.LogError(errorMessage);
                return null;
            }
            catch (Exception)
            {
                errorMessage = "Data is incorrect format. Model count must be great or equal 2. Health and strenght must be greater 0";
                _logger.LogError(errorMessage);
                return null;
            }

            return models;
        }
    }
}