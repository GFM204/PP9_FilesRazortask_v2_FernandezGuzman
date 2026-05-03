using VideogameManager.Models;

namespace VideogameManager.Services
{
    public class GameService
    {
        private readonly GameRepository _repository = new();
        private readonly GamesExporter _csvExporter = new();
        private readonly GamesRanking _rankingService = new();

        private readonly List<Game> _games;
        private int _nextId;

        private readonly string _logPath = Path.Combine(Directory.GetCurrentDirectory(), "activity_log.txt");

        public GameService()
        {
            _games = _repository.LoadAll();

            _nextId = _games.Any() ? _games.Max(g => g.Id) + 1 : 1;
        }

        public List<Game> GetAll() => _games;

        public Game? GetById(int id) => _games.FirstOrDefault(g => g.Id == id);

        public void Add(Game game)
        {
            game.Id = _nextId++;
            _games.Add(game);
            LogActivity("CREATED", game.Title);

            ExportToJson();
        }

        public void Update(Game game)
        {
            var index = _games.FindIndex(g => g.Id == game.Id);
            if (index >= 0)
            {
                _games[index] = game;
                LogActivity("UPDATED", game.Title);
                ExportToJson();
            }
        }

        public void Delete(int id)
        {
            var game = GetById(id);
            if (game != null)
            {
                LogActivity("DELETED", game.Title);
                _games.Remove(game);
                ExportToJson(); 
            }
        }

        public void ExportToJson() => _repository.SaveAll(_games);

        public void ImportFromJson()
        {
            var importedGames = _repository.LoadAll();
            _games.Clear();
            _games.AddRange(importedGames);
            _nextId = _games.Any() ? _games.Max(g => g.Id) + 1 : 1;
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
        public byte[] GenerateAndGetCsv()
        {
            _csvExporter.ExportToCsv(_games);
            return _csvExporter.GetCsvBytes();
        }
        public void GenerateRankingXml()
        {
            _rankingService.ExportToXml(_games);
        }
    }
}