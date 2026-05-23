namespace BookLibrary.Models;

public class Author
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Description { get; set; } = "";
    public List<string> BookNames { get; set; } = [];
    public List<string> PeriodicalNames { get; set; } = [];
    public List<LinkedItem> Books { get; set; } = [];
    public List<LinkedItem> Periodicals { get; set; } = [];
}

public class LinkedItem
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
}
