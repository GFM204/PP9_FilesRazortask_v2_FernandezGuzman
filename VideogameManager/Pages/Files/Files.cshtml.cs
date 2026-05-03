using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using VideogameManager.Services;

namespace VideogameManager.Pages.Files
{
    public class FilesModel : PageModel
    {
        private readonly GameService _gameService;
        public string[] LogEntries { get; private set; } = Array.Empty<string>();

        public FilesModel(GameService gameService)
        {
            _gameService = gameService;
        }

        public void OnGet()
        {
            LogEntries = _gameService.GetLogEntries();
        }

        public IActionResult OnPostExportJson()
        {
            _gameService.ExportToJson();
            return RedirectToPage();
        }

        public IActionResult OnPostImportJson()
        {
            _gameService.ImportFromJson();
            return RedirectToPage();
        }
        public IActionResult OnPostExportCsv()
        {
            byte[] bytes = _gameService.GenerateAndGetCsv();

            if (bytes.Length == 0)
            {
                return RedirectToPage(); 
            }

            return File(bytes, "text/csv", "games.csv");
        }
        public IActionResult OnPostExportXml()
        {
            _gameService.GenerateRankingXml();
            return RedirectToPage();
        }
    }
}