namespace BookLibrary.Models;

public class Author
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string Description { get; set; } = "";
    public List<string> BookNames { get; set; } = [];
}
