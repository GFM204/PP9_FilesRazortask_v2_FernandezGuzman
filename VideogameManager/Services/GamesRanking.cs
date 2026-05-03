using System.Xml.Linq;
using VideogameManager.Models;

namespace VideogameManager.Services
{
    public class GamesRanking
    {
        private readonly string _xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "games_ranking.xml");

        public void ExportToXml(IEnumerable<Game> games)
        {
            var rankedGames = games.OrderByDescending(g => g.Score).ToList();

            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("AppConfig",
                    new XElement("AppTitle", "VideoGame Ranking"),
                    new XElement("Games",
                        rankedGames.Select(g => new XElement("Game",
                            new XElement("id", g.Id),
                            new XElement("score", g.Score),
                            new XElement("title", g.Title),
                            new XElement("genre", g.Genre),
                            new XElement("year", g.Year),
                            new XElement("description", g.Description)
                        ))
                    )
                )
            );

            doc.Save(_xmlPath);
        }

        public byte[] GetXmlBytes()
        {
            if (!File.Exists(_xmlPath)) return Array.Empty<byte>();
            return File.ReadAllBytes(_xmlPath);
        }
    }
}