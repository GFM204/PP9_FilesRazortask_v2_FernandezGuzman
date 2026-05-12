using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VideogameManager.Data;
using VideogameManager.Models;
using VideogameManager.Services;
using Microsoft.EntityFrameworkCore;
namespace VideogameManager.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly GameStoreContext _context;
        public SelectList Developers { get; set; }

        [BindProperty]
        public Game Game { get; set; }

        public EditModel(GameStoreContext context) => _context = context;
        private bool GameExists(int id) => _context.Games.Any(g => g.Id == id);

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Game = await _context.Games.FindAsync(id);

            if (Game == null) return NotFound();

            await LoadDevAsync();


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDevAsync();
                return Page();
            }

            _context.Attach(Game).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private async Task LoadDevAsync()
        {
            var developers = await _context.Developers.ToListAsync();
            Developers = new SelectList(developers, "Id", "Name");
        }

    }
}