using HeroSimulator.Core.Models.Entities;
using System.Text.Json;

namespace HeroSimulator.Core.Services
{
    public class SaveLoadService
    {
        private readonly JsonSerializerOptions _options;

        public SaveLoadService()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
        }

        public void SaveGame(Hero hero, string filePath)
        {
            string jsonString = JsonSerializer.Serialize(hero, _options);
            File.WriteAllText(filePath, jsonString);
        }

        public Hero LoadGame(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return null;
            }

            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Hero>(jsonString, _options);
        }
    }
}