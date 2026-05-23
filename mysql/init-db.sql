CREATE DATABASE IF NOT EXISTS booklibrary;
CREATE USER IF NOT EXISTS 'booklibrary'@'localhost' IDENTIFIED BY 'пароль';
GRANT ALL PRIVILEGES ON booklibrary.* TO 'booklibrary'@'localhost';
FLUSH PRIVILEGES;

USE booklibrary;

CREATE TABLE IF NOT EXISTS Books (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(500) NOT NULL,
    PageCount INT NOT NULL,
    Description TEXT
);

CREATE TABLE IF NOT EXISTS Authors (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    FullName VARCHAR(500) NOT NULL,
    Description TEXT
);

CREATE TABLE IF NOT EXISTS BookAuthors (
    BookId INT NOT NULL,
    AuthorId INT NOT NULL,
    PRIMARY KEY (BookId, AuthorId),
    -- ON DELETE CASCADE: при удалении книги или автора связанные записи из BookAuthors удаляются автоматически
    FOREIGN KEY (BookId) REFERENCES Books(Id) ON DELETE CASCADE,
    FOREIGN KEY (AuthorId) REFERENCES Authors(Id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS Periodicals (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Title VARCHAR(500) NOT NULL,
    Description TEXT
);

CREATE TABLE IF NOT EXISTS PeriodicalAuthors (
    PeriodicalId INT NOT NULL,
    AuthorId INT NOT NULL,
    PRIMARY KEY (PeriodicalId, AuthorId),
    FOREIGN KEY (PeriodicalId) REFERENCES Periodicals(Id) ON DELETE CASCADE,
    FOREIGN KEY (AuthorId) REFERENCES Authors(Id) ON DELETE CASCADE
);
