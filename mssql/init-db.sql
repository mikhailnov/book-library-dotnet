CREATE DATABASE booklibrary;
GO

USE booklibrary;
GO

CREATE LOGIN booklibrary WITH PASSWORD = 'Change_Me123!';
CREATE USER booklibrary FOR LOGIN booklibrary;
GO

ALTER ROLE db_owner ADD MEMBER booklibrary;
GO

CREATE TABLE Books (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(500) NOT NULL,
    PageCount INT NOT NULL,
    Description NVARCHAR(MAX)
);
GO

CREATE TABLE Authors (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(500) NOT NULL,
    Description NVARCHAR(MAX)
);
GO

CREATE TABLE BookAuthors (
    BookId INT NOT NULL,
    AuthorId INT NOT NULL,
    PRIMARY KEY (BookId, AuthorId),
    -- ON DELETE CASCADE: при удалении книги или автора связанные записи из BookAuthors удаляются автоматически
    FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE,
    FOREIGN KEY (AuthorId) REFERENCES Authors(Id) ON DELETE CASCADE
);
GO

CREATE TABLE Periodicals (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(500) NOT NULL,
    Description NVARCHAR(MAX)
);
GO

CREATE TABLE PeriodicalAuthors (
    PeriodicalId INT NOT NULL,
    AuthorId INT NOT NULL,
    PRIMARY KEY (PeriodicalId, AuthorId),
    FOREIGN KEY (PeriodicalId) REFERENCES Periodicals(Id) ON DELETE CASCADE,
    FOREIGN KEY (AuthorId) REFERENCES Authors(Id) ON DELETE CASCADE
);
GO
