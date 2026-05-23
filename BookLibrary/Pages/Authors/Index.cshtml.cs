using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;

namespace BookLibrary.Pages.Authors;

public class AuthorsIndexModel : PageModel
{
    private readonly Repository _repository;

    public AuthorsIndexModel(Repository repository)
    {
        _repository = repository;
    }

    public List<Author> Authors { get; set; } = [];

    public void OnGet()
    {
        Authors = _repository.GetAllAuthors().ToList();
    }
}
