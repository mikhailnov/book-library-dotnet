using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;

namespace BookLibrary.Pages.Books;

public class BookDetailsModel : PageModel
{
    private readonly Repository _repository;

    public BookDetailsModel(Repository repository)
    {
        _repository = repository;
    }

    public Book? Book { get; set; }

    public IActionResult OnGet(int id)
    {
        Book = _repository.GetBookById(id);

        if (Book == null)
        {
            return NotFound(); // HTTP 404
        }

        return Page();
    }
}
