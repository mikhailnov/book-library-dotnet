using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookLibrary.Pages.Admin.Periodicals;

public class CreatePeriodicalModel : PageModel
{
    private readonly Repository _repository;

    public CreatePeriodicalModel(Repository repository)
    {
        _repository = repository;
    }

    public List<string> Errors { get; set; } = [];
    public bool Success { get; set; }

    public void OnGet()
    {
    }

    public void OnPost(string title, string description, string? authors)
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

        Errors = _repository.CreatePeriodicalWithAuthors(title, description, authorNames);

        if (Errors.Count == 0)
        {
            Success = true;
        }
    }
}
