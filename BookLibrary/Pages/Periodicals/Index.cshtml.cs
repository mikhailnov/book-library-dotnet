using Microsoft.AspNetCore.Mvc.RazorPages;
using BookLibrary.Models;

namespace BookLibrary.Pages.Periodicals;

public class PeriodicalsIndexModel : PageModel
{
    private readonly Repository _repository;

    public PeriodicalsIndexModel(Repository repository)
    {
        _repository = repository;
    }

    public List<Periodical> Periodicals { get; set; } = [];

    public void OnGet()
    {
        Periodicals = _repository.GetAllPeriodicals().ToList();
    }
}
