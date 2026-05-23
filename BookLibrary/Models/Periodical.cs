namespace BookLibrary.Models;

public class Periodical
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public List<string> AuthorNames { get; set; } = [];
}
