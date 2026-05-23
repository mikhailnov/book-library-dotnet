using Microsoft.Data.SqlClient;
using Dapper;
using BookLibrary.Models;

namespace BookLibrary;

public class Repository
{
    private readonly string _connectionString;

    public Repository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Книги

    public IEnumerable<Book> GetAllBooks()
    {
        using var connection = new SqlConnection(_connectionString);
        var books = connection.Query<Book>("SELECT * FROM Books").ToList();

        var bookAuthors = connection.Query<(int BookId, string FullName)>(
            "SELECT ba.BookId, a.FullName FROM BookAuthors ba JOIN Authors a ON ba.AuthorId = a.Id"
        ).ToList();

        var authorLookup = bookAuthors.ToLookup(ba => ba.BookId, ba => ba.FullName);

        foreach (var book in books)
        {
            book.AuthorNames = authorLookup[book.Id].ToList();
        }

        return books;
    }

    public Book? GetBookById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QueryFirstOrDefault<Book>("SELECT * FROM Books WHERE Id = @Id", new { Id = id });
    }

    public void CreateBook(Book book)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "INSERT INTO Books (Title, PageCount, Description) VALUES (@Title, @PageCount, @Description)",
            book);
    }

    public void UpdateBook(Book book)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "UPDATE Books SET Title = @Title, PageCount = @PageCount, Description = @Description WHERE Id = @Id",
            book);
    }

    public void DeleteBook(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute("DELETE FROM Books WHERE Id = @Id", new { Id = id });
    }

    // Авторы

    public IEnumerable<Author> GetAllAuthors()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Author>("SELECT * FROM Authors");
    }

    public Author? GetAuthorById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QueryFirstOrDefault<Author>("SELECT * FROM Authors WHERE Id = @Id", new { Id = id });
    }

    public void CreateAuthor(Author author)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "INSERT INTO Authors (FullName, Description) VALUES (@FullName, @Description)",
            author);
    }

    public void UpdateAuthor(Author author)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "UPDATE Authors SET FullName = @FullName, Description = @Description WHERE Id = @Id",
            author);
    }

    public void DeleteAuthor(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute("DELETE FROM Authors WHERE Id = @Id", new { Id = id });
    }

    // Периодические издания

    public IEnumerable<Periodical> GetAllPeriodicals()
    {
        using var connection = new SqlConnection(_connectionString);
        var periodicals = connection.Query<Periodical>("SELECT * FROM Periodicals").ToList();

        var periodicalAuthors = connection.Query<(int PeriodicalId, string FullName)>(
            "SELECT pa.PeriodicalId, a.FullName FROM PeriodicalAuthors pa JOIN Authors a ON pa.AuthorId = a.Id"
        ).ToList();

        var authorLookup = periodicalAuthors.ToLookup(pa => pa.PeriodicalId, pa => pa.FullName);

        foreach (var periodical in periodicals)
        {
            periodical.AuthorNames = authorLookup[periodical.Id].ToList();
        }

        return periodicals;
    }

    public Periodical? GetPeriodicalById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QueryFirstOrDefault<Periodical>("SELECT * FROM Periodicals WHERE Id = @Id", new { Id = id });
    }

    public void CreatePeriodical(Periodical periodical)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "INSERT INTO Periodicals (Title, Description) VALUES (@Title, @Description)",
            periodical);
    }

    public void UpdatePeriodical(Periodical periodical)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute(
            "UPDATE Periodicals SET Title = @Title, Description = @Description WHERE Id = @Id",
            periodical);
    }

    public void DeletePeriodical(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute("DELETE FROM Periodicals WHERE Id = @Id", new { Id = id });
    }
}
