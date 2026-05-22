using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLibrary.Pages.Books;

public class BooksIndexModel : PageModel
{
    public List<BookItem> Books { get; set; } = [];

    public void OnGet()
    {
        // Заглушка — позже заменим на данные из БД
    }
}

public class BookItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int PageCount { get; set; }
    public List<string> Authors { get; set; } = [];
}
