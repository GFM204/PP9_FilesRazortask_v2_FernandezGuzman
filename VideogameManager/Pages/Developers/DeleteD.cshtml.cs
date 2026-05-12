using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VideogameManager.Data;
using VideogameManager.Models;

namespace VideogameManager.Pages.Developers
{
    public class DeleteDModel : PageModel
    {
        private readonly GameStoreContext _context;

        [BindProperty]
        public Developer Developer { get; set; }

        public DeleteDModel(GameStoreContext context) => _context = context;

        public IActionResult OnGet(int id)
        {
            Developer = _context.Developers.Include(d => d.Games).FirstOrDefault(m => m.Id == id);
            if (Developer == null) return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            Developer developerToDelete = await _context.Developers.Include(d => d.Games).FirstOrDefaultAsync(m => m.Id == Developer.Id);

            if (developerToDelete.Games != null && developerToDelete.Games.Any())
            {
                ModelState.AddModelError(string.Empty, "Cannot delete a developer that is associated to existing games.");
                Developer = developerToDelete;
                return Page();
            }
            _context.Developers.Remove(developerToDelete);
            await _context.SaveChangesAsync();
            return RedirectToPage("./IndexD");

        }

    }
}
