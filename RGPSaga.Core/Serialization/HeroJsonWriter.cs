namespace RpgSaga.Core.Deserialization
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;
    using RpgSaga.Core.Data;
    using RpgSaga.Core.Interfaces;

    public class HeroJsonWriter : IHeroJsonWriter
    {
        public void SerializeHeroToJson(List<HeroDto> heroes, string filename)
        {
            string directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName;
            string path = @$"{directory}\{filename}";
            string serializedObject = JsonConvert.SerializeObject(heroes);
            File.WriteAllText(path, serializedObject);
        }
    }
}
