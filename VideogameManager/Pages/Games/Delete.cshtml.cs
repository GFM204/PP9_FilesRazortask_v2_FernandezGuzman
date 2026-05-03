using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideogameManager.Models;
using VideogameManager.Services;

namespace VideogameManager.Pages.Games
{
    public class DeleteModel : PageModel
    {
        private readonly GameService _gameService;

        public Game Game { get; private set; } = default!;

        public DeleteModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public IActionResult OnGet(int id)
        {
            var game = _gameService.GetById(id);

            if (game == null)
            {
                return RedirectToPage("./Index");
            }

            Game = game;
            return Page();
        }

        public IActionResult OnPost(int id)
        {
            _gameService.Delete(id);
            return RedirectToPage("./Index");
        }
    }
}