using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideogameManager.Data;
using VideogameManager.Models;
using VideogameManager.Services;

namespace VideogameManager.Pages.Games
{
    public class CreateModel : PageModel
    {
        private readonly GameStoreContext _context;

        [BindProperty]
        public Game Game { get; set; }

        public SelectList Developers  { get; set; }


        public CreateModel(GameStoreContext context) => _context = context;

        public async Task<IActionResult> OnGetAsync() 
        {
            List<Developer> developersList = await _context.Developers.ToListAsync();
            Developers = new SelectList(developersList, "Id", "Name");
            return Page();

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                List<Developer> developersList = await _context.Developers.ToListAsync();
                Developers = new SelectList(developersList, "Id", "Name");
                return Page(); 
            }
            try
            {
                _context.Add(Game);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "No se pudo guardar en la base de datos.");
                return Page();
            }
        }
    }
}