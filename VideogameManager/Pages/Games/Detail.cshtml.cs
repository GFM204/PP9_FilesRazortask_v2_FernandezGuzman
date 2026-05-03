using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideogameManager.Models;
using VideogameManager.Services;

namespace VideogameManager.Pages.Games
{
    public class DetailModel : PageModel
    {
        private readonly GameService _gameService;
        public Game CurrentGame { get; private set; } = default!;

        public DetailModel(GameService gameService)
        {
            _gameService = gameService;
        }
        public IActionResult OnGet(int id)
        {
            Game game = _gameService.GetById(id);
            if (game == null)
            {
                return RedirectToPage("./Index");
            }
            CurrentGame = game;
            return Page();
        }
    }
}
