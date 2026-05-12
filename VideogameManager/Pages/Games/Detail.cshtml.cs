using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideogameManager.Data;
using VideogameManager.Models;
using VideogameManager.Services;

namespace VideogameManager.Pages.Games
{
    public class DetailModel : PageModel
    {
        private readonly GameStoreContext _context;
        public Game CurrentGame { get; private set; } = default!;

        public DetailModel(GameStoreContext context) => _context = context;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            CurrentGame = await _context.Games.Include(g => g.Developer).FirstOrDefaultAsync(m => m.Id == id);


            if (CurrentGame == null)
            {
                return RedirectToPage("./Index");
            }


            return Page();
        }
    }
}
