using Microsoft.EntityFrameworkCore;
using VideogameManager.Models;

namespace VideogameManager.Data
{
    public class GameStoreContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }

        public GameStoreContext(DbContextOptions<GameStoreContext> options) : base(options) { }

    }
}
