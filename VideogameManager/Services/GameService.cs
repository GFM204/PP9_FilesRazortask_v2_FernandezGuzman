using System.IO;
using VideogameManager.Models;

namespace VideogameManager.Services
{
    public class GameService
    {
        private readonly string _logPath = Path.Combine(Directory.GetCurrentDirectory(), "activity_log.txt");

        private readonly List<Game> _games = new()
        {
            new() { Id=1, Title="The Legend of Zelda: TotK", Genre="Adventure",
                    Year=2023, Score=9.8, Description="Open-world action RPG" },
            new() { Id=2, Title="Elden Ring", Genre="RPG",
                    Year=2022, Score=9.5, Description="Open-world soulslike" },
            new() { Id=3, Title="Celeste", Genre="Platformer",
                    Year=2018, Score=9.0, Description="Precision platformer" },
        };
        private int _nextId = 4;

        public List<Game> GetAll() => _games;
        public Game? GetById(int id) => _games.FirstOrDefault(g => g.Id == id);
        public void Add(Game game)
        {
            game.Id = _nextId++;
            _games.Add(game);
            LogActivity("CREATED", game.Title);
        }
        public void Update(Game game)
        {
            var index = _games.FindIndex(g => g.Id == game.Id);
            if (index >= 0)
            {
                _games[index] = game;
                LogActivity("UPDATED", game.Title);
            }

        }
        public void Delete(int id)
        {
            Game game = GetById(id);
            if (game != null)
            {
                LogActivity("DELETED", game.Title);
                _games.Remove(game);
            }
        }

        private void LogActivity(string action, string title)
        {
            string timestamp = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            string line = $"[{timestamp}]  [{action}]  [{title}]{Environment.NewLine}";
            File.AppendAllText(_logPath, line);
        }

        public string[] GetLogEntries()
        {
            if (!File.Exists(_logPath)) return Array.Empty<string>();

            return File.ReadAllLines(_logPath);
        }
    }
}
