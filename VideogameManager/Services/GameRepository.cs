using System.Text.Json;
using VideogameManager.Models;

namespace VideogameManager.Services
{
    public class GameRepository
    {
        private readonly string _filePath = Path.Combine(Directory.GetCurrentDirectory(), "games.json");
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public void SaveAll(IEnumerable<Game> games)
        {
            string jsonString = JsonSerializer.Serialize(games, _options);
            File.WriteAllText(_filePath, jsonString);
        }

        public List<Game> LoadAll()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Game>();
            }

            string jsonString = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Game>>(jsonString) ?? new List<Game>();
        }
    }
}