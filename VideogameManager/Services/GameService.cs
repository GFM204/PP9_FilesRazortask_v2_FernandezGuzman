using VideogameManager.Models;

namespace VideogameManager.Services
{
    public class GameService
    {
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
        public void Add(Game game) { game.Id = _nextId++; _games.Add(game); }
        public void Update(Game game)
        {
            var index = _games.FindIndex(g => g.Id == game.Id);
            if (index >= 0) _games[index] = game;
        }
        public void Delete(int id) => _games.RemoveAll(g => g.Id == id);

    }
}
