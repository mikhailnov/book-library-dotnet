using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLibrary.Pages.Authors;

public class AuthorsIndexModel : PageModel
{
    public List<AuthorItem> Authors { get; set; } = [];

    public void OnGet()
    {
        // Заглушка — позже заменим на данные из БД
    }
}

public class AuthorItem
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public List<string> Books { get; set; } = [];
}
