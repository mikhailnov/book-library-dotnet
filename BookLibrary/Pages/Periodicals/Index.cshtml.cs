using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLibrary.Pages.Periodicals;

public class PeriodicalsIndexModel : PageModel
{
    public List<PeriodicalItem> Periodicals { get; set; } = [];

    public void OnGet()
    {
        // Заглушка — позже заменим на данные из БД
    }
}

public class PeriodicalItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public List<string> Authors { get; set; } = [];
}
