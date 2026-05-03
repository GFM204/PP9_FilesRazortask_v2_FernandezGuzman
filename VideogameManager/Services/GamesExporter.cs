using System.Text;
using VideogameManager.Models;

namespace VideogameManager.Services
{
    public class GamesExporter
    {
        private readonly string _csvPath = Path.Combine(Directory.GetCurrentDirectory(), "games.csv");
        private readonly string[] _header = { "Id", "Title", "Genre", "Year", "Score" };

        public void ExportToCsv(IEnumerable<Game> games)
        {
            var builder = new StringBuilder();

            builder.AppendLine(string.Join(",", _header));

            foreach (var game in games)
            {
                var row = new[]
                {
                    game.Id.ToString(),
                    $"\"{game.Title}\"",
                    $"\"{game.Genre}\"",
                    game.Year.ToString(),
                    game.Score.ToString()
                };
                builder.AppendLine(string.Join(",", row));
            }

            File.WriteAllText(_csvPath, builder.ToString(), Encoding.UTF8);
        }

        public byte[] GetCsvBytes()
        {
            if (!File.Exists(_csvPath)) return Array.Empty<byte>();
            return File.ReadAllBytes(_csvPath);
        }
    }
} 