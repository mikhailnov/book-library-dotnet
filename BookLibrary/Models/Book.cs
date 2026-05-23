namespace BookLibrary.Models;

public class Book
{
    public int Id { get; set; }

    // public string Title
    // {
    //     get { return _title; }
    //     set { _title = value; }
    // }
    // private string _title = "";
    public string Title { get; set; } = "";

    public int PageCount { get; set; }
    public string Description { get; set; } = "";
    public List<string> AuthorNames { get; set; } = [];
}
