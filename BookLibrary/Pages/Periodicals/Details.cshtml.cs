using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;

namespace BookLibrary.Pages.Periodicals;

public class PeriodicalDetailsModel : PageModel
{
    private readonly Repository _repository;

    public PeriodicalDetailsModel(Repository repository)
    {
        _repository = repository;
    }

    public Periodical? Periodical { get; set; }

    public IActionResult OnGet(int id)
    {
        Periodical = _repository.GetPeriodicalById(id);

        if (Periodical == null)
        {
            return NotFound(); // HTTP 404
        }

        return Page();
    }
}
