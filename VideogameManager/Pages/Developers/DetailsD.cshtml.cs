using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VideogameManager.Data;
using VideogameManager.Models;

namespace VideogameManager.Pages.Developers
{
    public class DetailsDModel : PageModel
    {
        private readonly GameStoreContext _context;
        public Developer Developer { get; set; }
        public SelectList Games { get; set; }

        public DetailsDModel(GameStoreContext context) => _context = context;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Developer = await _context.Developers.Include(d => d.Games).FirstOrDefaultAsync(m => m.Id == id);

            if (Developer == null) return NotFound();

            return Page();
        }
    }
}
