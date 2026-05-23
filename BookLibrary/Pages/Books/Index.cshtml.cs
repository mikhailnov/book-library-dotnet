using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;

namespace BookLibrary.Pages.Books;

public class BooksIndexModel : PageModel
{
    // Альтернативный вариант без подчёркивания:
    // private readonly Repository repository;
    // public BooksIndexModel(Repository repository)
    // {
    //     this.repository = repository;
    // }
    private readonly Repository _repository;

    public BooksIndexModel(Repository repository)
    {
        _repository = repository;
    }

    public List<Book> Books { get; set; } = [];

    public void OnGet()
    {
        Books = _repository.GetAllBooks().ToList();
    }
}
