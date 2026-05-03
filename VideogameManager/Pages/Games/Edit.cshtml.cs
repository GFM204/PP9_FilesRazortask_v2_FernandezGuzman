using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideogameManager.Models;
using VideogameManager.Services;

namespace VideogameManager.Pages.Games
{
    public class EditModel : PageModel
    {
        private readonly GameService _gameService;

        [BindProperty]
        public Game Game { get; set; } = default!;

        public EditModel(GameService gameService) => _gameService = gameService;

        public IActionResult OnGet(int id)
        {
            var game = _gameService.GetById(id);
            if (game == null) return RedirectToPage("./Index");

            Game = game;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            _gameService.Update(Game);
            return RedirectToPage("./Index");
        }
    }
}