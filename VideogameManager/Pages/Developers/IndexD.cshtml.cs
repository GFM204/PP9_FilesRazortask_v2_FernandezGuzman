using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideogameManager.Data;
using VideogameManager.Models;

namespace VideogameManager.Pages.Developers
{
    public class IndexDModel : PageModel
    {
        private readonly GameStoreContext _context;
        public IndexDModel(GameStoreContext service) => _context = service;


        public List<Developer> Developers { get; set; } = new();

        public async Task OnGet()
        {

            Developers = await _context.Developers.Include(d => d.Games).ToListAsync();

        }
    }
}
