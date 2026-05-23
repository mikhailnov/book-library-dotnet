using Microsoft.Data.SqlClient;
using Dapper;
using BookLibrary.Models;

namespace BookLibrary;

public class Repository
{
    private readonly string _connectionString;

    public const int MaxTitleLength = 500;
    public const int MaxDescriptionLength = 100000;
    public const int MaxFullNameLength = 500;

    public Repository(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Книги

    public IEnumerable<Book> GetAllBooks()
    {
        using var connection = new SqlConnection(_connectionString);
        var books = connection.Query<Book>("SELECT * FROM Books ORDER BY Title").ToList();

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
        var book = connection.QueryFirstOrDefault<Book>("SELECT * FROM Books WHERE Id = @Id", new { Id = id });

        if (book == null)
        {
            return null;
        }

        book.AuthorNames = connection.Query<string>(
            "SELECT a.FullName FROM BookAuthors ba JOIN Authors a ON ba.AuthorId = a.Id WHERE ba.BookId = @Id",
            new { Id = id }
        ).ToList();

        return book;
    }

    public List<string> CreateBook(Book book)
    {
        var errors = ValidateBook(book);
        if (errors.Count > 0)
        {
            return errors;
        }

        using var connection = new SqlConnection(_connectionString);
        CheckDuplicateBook(connection, book.Title, null, errors);
        if (errors.Count > 0)
        {
            return errors;
        }

        connection.Execute(
            "INSERT INTO Books (Title, PageCount, Description) VALUES (@Title, @PageCount, @Description)",
            book);
        return errors;
    }

    public List<string> UpdateBook(Book book)
    {
        var errors = ValidateBook(book);
        if (errors.Count > 0)
        {
            return errors;
        }

        using var connection = new SqlConnection(_connectionString);
        CheckDuplicateBook(connection, book.Title, book.Id, errors);
        if (errors.Count > 0)
        {
            return errors;
        }

        connection.Execute(
            "UPDATE Books SET Title = @Title, PageCount = @PageCount, Description = @Description WHERE Id = @Id",
            book);
        return errors;
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
        var authors = connection.Query<Author>("SELECT * FROM Authors ORDER BY FullName").ToList();

        var authorBooks = connection.Query<(int AuthorId, string Title)>(
            "SELECT ba.AuthorId, b.Title FROM BookAuthors ba JOIN Books b ON ba.BookId = b.Id"
        ).ToList();

        var bookLookup = authorBooks.ToLookup(ab => ab.AuthorId, ab => ab.Title);

        var authorPeriodicals = connection.Query<(int AuthorId, string Title)>(
            "SELECT pa.AuthorId, p.Title FROM PeriodicalAuthors pa JOIN Periodicals p ON pa.PeriodicalId = p.Id"
        ).ToList();

        var periodicalLookup = authorPeriodicals.ToLookup(ap => ap.AuthorId, ap => ap.Title);

        foreach (var author in authors)
        {
            author.BookNames = bookLookup[author.Id].ToList();
            author.PeriodicalNames = periodicalLookup[author.Id].ToList();
        }

        return authors;
    }

    public Author? GetAuthorById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.QueryFirstOrDefault<Author>("SELECT * FROM Authors WHERE Id = @Id", new { Id = id });
    }

    public List<string> CreateAuthor(Author author)
    {
        var errors = ValidateAuthor(author);
        if (errors.Count > 0)
        {
            return errors;
        }

        using var connection = new SqlConnection(_connectionString);
        CheckDuplicateAuthor(connection, author.FullName, null, errors);
        if (errors.Count > 0)
        {
            return errors;
        }

        connection.Execute(
            "INSERT INTO Authors (FullName, Description) VALUES (@FullName, @Description)",
            author);
        return errors;
    }

    public List<string> UpdateAuthor(Author author)
    {
        var errors = ValidateAuthor(author);
        if (errors.Count > 0)
        {
            return errors;
        }

        using var connection = new SqlConnection(_connectionString);
        CheckDuplicateAuthor(connection, author.FullName, author.Id, errors);
        if (errors.Count > 0)
        {
            return errors;
        }

        connection.Execute(
            "UPDATE Authors SET FullName = @FullName, Description = @Description WHERE Id = @Id",
            author);
        return errors;
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
        var periodicals = connection.Query<Periodical>("SELECT * FROM Periodicals ORDER BY Title").ToList();

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
        var periodical = connection.QueryFirstOrDefault<Periodical>("SELECT * FROM Periodicals WHERE Id = @Id", new { Id = id });

        if (periodical == null)
        {
            return null;
        }

        periodical.AuthorNames = connection.Query<string>(
            "SELECT a.FullName FROM PeriodicalAuthors pa JOIN Authors a ON pa.AuthorId = a.Id WHERE pa.PeriodicalId = @Id",
            new { Id = id }
        ).ToList();

        return periodical;
    }

    public List<string> CreatePeriodical(Periodical periodical)
    {
        var errors = ValidatePeriodical(periodical);
        if (errors.Count > 0)
        {
            return errors;
        }

        using var connection = new SqlConnection(_connectionString);
        CheckDuplicatePeriodical(connection, periodical.Title, null, errors);
        if (errors.Count > 0)
        {
            return errors;
        }

        connection.Execute(
            "INSERT INTO Periodicals (Title, Description) VALUES (@Title, @Description)",
            periodical);
        return errors;
    }

    public List<string> UpdatePeriodical(Periodical periodical)
    {
        var errors = ValidatePeriodical(periodical);
        if (errors.Count > 0)
        {
            return errors;
        }

        using var connection = new SqlConnection(_connectionString);
        CheckDuplicatePeriodical(connection, periodical.Title, periodical.Id, errors);
        if (errors.Count > 0)
        {
            return errors;
        }

        connection.Execute(
            "UPDATE Periodicals SET Title = @Title, Description = @Description WHERE Id = @Id",
            periodical);
        return errors;
    }

    public void DeletePeriodical(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Execute("DELETE FROM Periodicals WHERE Id = @Id", new { Id = id });
    }

    private static List<string> ValidateBook(Book book)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(book.Title))
        {
            errors.Add("Название книги не может быть пустым.");
        }
        else if (book.Title.Length > MaxTitleLength)
        {
            errors.Add("Название книги не может быть длиннее " + MaxTitleLength + " символов.");
        }

        CheckHtml(book.Title, "Название книги", errors);

        if (book.PageCount <= 0)
        {
            errors.Add("Количество страниц должно быть положительным числом.");
        }

        if (book.Description.Length > MaxDescriptionLength)
        {
            errors.Add("Описание книги не может быть длиннее " + MaxDescriptionLength + " символов.");
        }

        CheckHtml(book.Description, "Описание книги", errors);

        return errors;
    }

    private static List<string> ValidateAuthor(Author author)
    {
        var errors = new List<string>();

        // Нормализация: "Иванов И. П." -> "Иванов И.П."
        author.FullName = System.Text.RegularExpressions.Regex.Replace(
            author.FullName, @"(\.) ([А-ЯЁA-Z]\.)", "$1$2");

        if (string.IsNullOrWhiteSpace(author.FullName))
        {
            errors.Add("ФИО автора не может быть пустым.");
        }
        else if (author.FullName.Length > MaxFullNameLength)
        {
            errors.Add("ФИО автора не может быть длиннее " + MaxFullNameLength + " символов.");
        }
        // Разрешены: русские и латинские буквы, точки, пробелы, кавычки (« » ")
        else if (!System.Text.RegularExpressions.Regex.IsMatch(
            author.FullName, @"^[а-яА-ЯёЁa-zA-Z. ""«»]+$"))
        {
            errors.Add("ФИО автора может содержать только буквы, точки и пробелы.");
        }

        if (author.Description.Length > MaxDescriptionLength)
        {
            errors.Add("Описание автора не может быть длиннее " + MaxDescriptionLength + " символов.");
        }

        CheckHtml(author.Description, "Описание автора", errors);

        return errors;
    }

    private static List<string> ValidatePeriodical(Periodical periodical)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(periodical.Title))
        {
            errors.Add("Название издания не может быть пустым.");
        }
        else if (periodical.Title.Length > MaxTitleLength)
        {
            errors.Add("Название издания не может быть длиннее " + MaxTitleLength + " символов.");
        }

        CheckHtml(periodical.Title, "Название издания", errors);

        if (periodical.Description.Length > MaxDescriptionLength)
        {
            errors.Add("Описание издания не может быть длиннее " + MaxDescriptionLength + " символов.");
        }

        CheckHtml(periodical.Description, "Описание издания", errors);

        return errors;
    }

    // Проверка на наличие HTML-тегов
    private static void CheckHtml(string value, string fieldName, List<string> errors)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(value, @"<\s*[a-zA-Z/]"))
        {
            errors.Add(fieldName + " не должно содержать HTML-теги.");
        }
    }

    // Проверка дубликатов: excludeId — Id записи, которую обновляем (null при создании)
    // SQL-инъекция невозможна: Dapper использует параметризованные запросы,
    // а данные уже прошли валидацию (пустота, длина, HTML-теги)
    private static void CheckDuplicateBook(System.Data.Common.DbConnection connection, string title, int? excludeId, List<string> errors)
    {
        var sql = excludeId.HasValue
            ? "SELECT COUNT(*) FROM Books WHERE Title = @Title AND Id != @ExcludeId"
            : "SELECT COUNT(*) FROM Books WHERE Title = @Title";

        var count = connection.QuerySingle<int>(sql, new { Title = title, ExcludeId = excludeId });

        if (count > 0)
        {
            errors.Add("Книга с таким названием уже существует.");
        }
    }

    private static void CheckDuplicateAuthor(System.Data.Common.DbConnection connection, string fullName, int? excludeId, List<string> errors)
    {
        var sql = excludeId.HasValue
            ? "SELECT COUNT(*) FROM Authors WHERE FullName = @FullName AND Id != @ExcludeId"
            : "SELECT COUNT(*) FROM Authors WHERE FullName = @FullName";

        var count = connection.QuerySingle<int>(sql, new { FullName = fullName, ExcludeId = excludeId });

        if (count > 0)
        {
            errors.Add("Автор с таким ФИО уже существует.");
        }
    }

    private static void CheckDuplicatePeriodical(System.Data.Common.DbConnection connection, string title, int? excludeId, List<string> errors)
    {
        var sql = excludeId.HasValue
            ? "SELECT COUNT(*) FROM Periodicals WHERE Title = @Title AND Id != @ExcludeId"
            : "SELECT COUNT(*) FROM Periodicals WHERE Title = @Title";

        var count = connection.QuerySingle<int>(sql, new { Title = title, ExcludeId = excludeId });

        if (count > 0)
        {
            errors.Add("Периодическое издание с таким названием уже существует.");
        }
    }
}
