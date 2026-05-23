using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLibrary.Pages.Admin.Books;

public class CreateBookModel : PageModel
{
    private readonly Repository _repository;

    public CreateBookModel(Repository repository)
    {
        _repository = repository;
    }

    public List<string> Errors { get; set; } = [];
    public bool Success { get; set; }

    public void OnGet()
    {
    }

    public void OnPost(string title, int pageCount, string description, string? authors)
    {
        string authorsValue;
        if (authors != null)
        {
            authorsValue = authors;
        }
        else
        {
            authorsValue = "";
        }

        var authorNames = authorsValue
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(a => a.Trim())
            .Where(a => !string.IsNullOrEmpty(a))
            .ToList();

        Errors = _repository.CreateBookWithAuthors(title, pageCount, description, authorNames);

        if (Errors.Count == 0)
        {
            Success = true;
        }
    }
}
