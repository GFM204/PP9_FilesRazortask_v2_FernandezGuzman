using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideogameManager.Data;
using VideogameManager.Models;

namespace VideogameManager.Pages.Stats
{
    public class IndexSModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Threshold { get; set; } = 0;

        private readonly GameStoreContext _context;
        public IndexSModel(GameStoreContext context) => _context = context;

        public IList<Game> FilteredGames { get; set; } //
        public IList<Game> TopFiveGames { get; set; }
        public IList<Game> AllGames { get; set; }
        public IList<Game> FilteredByNameGames { get; set; } // 
        public List<dynamic> ByDecade { get; set; }
        public List<dynamic> AvgPerDev { get; set; }



        [BindProperty(SupportsGet = true)]
        public string GenreSelected { get; set; } //
        public SelectList Genres { get; set; }

        [BindProperty(SupportsGet = true)]
        public string TitleFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? MinYear { get; set; }

        public IList<Developer> producDevs { get; set; }

        public async Task OnGetAsync()
        {
            //genres
            var genres = await _context.Games
                .Select(g => g.Genre)
                .Distinct()
                .OrderBy(g => g)
                .ToListAsync();

            Genres = new SelectList(genres);

            var query = _context.Games.AsQueryable();

            if (!string.IsNullOrWhiteSpace(GenreSelected))
            {
                query = query.Where(g => g.Genre == GenreSelected);
            }

            FilteredGames = await query.OrderByDescending(g => g.Score).ToListAsync();

            //select all games for following querys
            AllGames = await _context.Games
                .ToListAsync();

            //top 5 games
            TopFiveGames = await _context.Games
                .Include(g => g.Developer)
                .OrderByDescending(g => g.Score)
                .Take(5)
                .ToListAsync();

            //Count games by decade
            ByDecade = await _context.Games
                .GroupBy(g => (g.Year / 10) * 10)
                .Select(grp => (dynamic)new { Decade = grp.Key, Count = grp.Count() })
                .ToListAsync();

            //Advanced querys

            //average score dev
            var devDataRecap = await _context.Developers
                .Include(d => d.Games)
                .Where(d => d.Games.Any())
                .Select(d => new {
                    d.Name,
                    GameCount = d.Games.Count,
                    AvgScore = d.Games.Average(g => g.Score)
                })
                .OrderByDescending(x => x.AvgScore)
                .ToListAsync();

            AvgPerDev = devDataRecap.Select(d => (dynamic)d).ToList();

            //advance query
            var queryAdv = _context.Games.Include(g => g.Developer).AsQueryable();

            if (!string.IsNullOrWhiteSpace(TitleFilter)) queryAdv = queryAdv.Where(g => g.Title.Contains(TitleFilter));

            if (!string.IsNullOrWhiteSpace(GenreSelected)) queryAdv = queryAdv.Where(g => g.Genre == GenreSelected);

            if (MinYear.HasValue) queryAdv = queryAdv.Where(g => g.Year >= MinYear.Value);

            FilteredByNameGames = await queryAdv.OrderBy(g => g.Title).ToListAsync();

            //Dev with more than ...
            producDevs = await _context.Developers
                .Include(d => d.Games)
                .Where(d => d.Games.Count > Threshold)
                .OrderByDescending(d => d.Games.Count)
                .ToListAsync();


        }
    }
}
