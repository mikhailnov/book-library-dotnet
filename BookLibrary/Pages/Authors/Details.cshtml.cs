using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;

namespace BookLibrary.Pages.Authors;

public class AuthorDetailsModel : PageModel
{
    private readonly Repository _repository;

    public AuthorDetailsModel(Repository repository)
    {
        _repository = repository;
    }

    public Author? Author { get; set; }

    public IActionResult OnGet(int id)
    {
        Author = _repository.GetAuthorById(id);

        if (Author == null)
        {
            return NotFound(); // HTTP 404
        }

        return Page();
    }
}
