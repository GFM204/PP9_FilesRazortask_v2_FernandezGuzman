using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideogameManager.Models;
using VideogameManager.Services;

namespace VideogameManager.Pages.Games
{
    public class IndexModel : PageModel
    {
        private readonly GameService _service;
        public IndexModel(GameService service) => _service = service;

        public List<Game> Games { get; set; } = new();

        public void OnGet() => Games = _service.GetAll();
    }
}
