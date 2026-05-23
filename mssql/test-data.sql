USE booklibrary;
GO

IF NOT EXISTS (SELECT 1 FROM Authors WHERE FullName = N'Фёдор Достоевский')
INSERT INTO Authors (FullName, Description) VALUES (
  N'Фёдор Достоевский',
  N'Русский писатель, мыслитель и философ. Один из самых читаемых и известных русских писателей в мире.'
);
IF NOT EXISTS (SELECT 1 FROM Authors WHERE FullName = N'Лев Толстой')
INSERT INTO Authors (FullName, Description) VALUES (
  N'Лев Толстой',
  N'Русский писатель, граф. Один из величайших писателей мира, автор романов-эпопей, повестей и рассказов.'
);
IF NOT EXISTS (SELECT 1 FROM Authors WHERE FullName = N'Олифер Виктор Григорьевич')
INSERT INTO Authors (FullName, Description) VALUES (
  N'Олифер Виктор Григорьевич',
  N'Специалист в области компьютерных сетей и телекоммуникаций.'
);
IF NOT EXISTS (SELECT 1 FROM Authors WHERE FullName = N'Олифер Наталья Викторовна')
INSERT INTO Authors (FullName, Description) VALUES (
  N'Олифер Наталья Викторовна',
  N'Специалист в области компьютерных сетей и сетевых технологий.'
);
GO

-- CHAR(10) — символ перевода строки (LF), нужен для Markdown, так как SQL-строки не могут содержать реальный перенос
IF NOT EXISTS (SELECT 1 FROM Books WHERE Title = N'Преступление и наказание')
INSERT INTO Books (Title, PageCount, Description) VALUES (
  N'Преступление и наказание',
  671,
  N'## Великий роман Достоевского' + CHAR(10) +
  N'Роман о бедном студенте **Родионе Раскольникове**, который совершает убийство и мучается угрызениями совести.'
);
IF NOT EXISTS (SELECT 1 FROM Books WHERE Title = N'Война и мир')
INSERT INTO Books (Title, PageCount, Description) VALUES (
  N'Война и мир',
  1225,
  N'## Эпопея о судьбах России' + CHAR(10) +
  N'Роман-эпопея, повествующий о судьбах нескольких семей на фоне **Отечественной войны 1812 года**.'
);
IF NOT EXISTS (SELECT 1 FROM Books WHERE Title = N'Идиот')
INSERT INTO Books (Title, PageCount, Description) VALUES (
  N'Идиот',
  640,
  N'## История «положительно прекрасного человека»' + CHAR(10) +
  N'Роман о князе **Льве Мышкине**, человеке необыкновенной доброты и чистоты, возвращающемся в Россию из швейцарской клиники.'
);
GO

-- Id авторов и книг определяются динамически, чтобы не зависеть от конкретных значений
IF NOT EXISTS (
  SELECT 1 FROM BookAuthors ba
  JOIN Books b ON ba.BookId = b.Id
  JOIN Authors a ON ba.AuthorId = a.Id
  WHERE b.Title = N'Преступление и наказание' AND a.FullName = N'Фёдор Достоевский'
)
INSERT INTO BookAuthors (BookId, AuthorId)
SELECT b.Id, a.Id FROM Books b, Authors a
WHERE b.Title = N'Преступление и наказание' AND a.FullName = N'Фёдор Достоевский';

IF NOT EXISTS (
  SELECT 1 FROM BookAuthors ba
  JOIN Books b ON ba.BookId = b.Id
  JOIN Authors a ON ba.AuthorId = a.Id
  WHERE b.Title = N'Война и мир' AND a.FullName = N'Лев Толстой'
)
INSERT INTO BookAuthors (BookId, AuthorId)
SELECT b.Id, a.Id FROM Books b, Authors a
WHERE b.Title = N'Война и мир' AND a.FullName = N'Лев Толстой';

IF NOT EXISTS (
  SELECT 1 FROM BookAuthors ba
  JOIN Books b ON ba.BookId = b.Id
  JOIN Authors a ON ba.AuthorId = a.Id
  WHERE b.Title = N'Идиот' AND a.FullName = N'Фёдор Достоевский'
)
INSERT INTO BookAuthors (BookId, AuthorId)
SELECT b.Id, a.Id FROM Books b, Authors a
WHERE b.Title = N'Идиот' AND a.FullName = N'Фёдор Достоевский';

IF NOT EXISTS (SELECT 1 FROM Books WHERE Title = N'Компьютерные сети. Принципы, технологии, протоколы')
INSERT INTO Books (Title, PageCount, Description) VALUES (
  N'Компьютерные сети. Принципы, технологии, протоколы',
  992,
  N'Издание предназначено для студентов, аспирантов и технических специалистов, которые хотели бы получить базовые знания о принципах построения компьютерных сетей, понять особенности традиционных и перспективных технологий локальных и глобальных сетей, изучить способы создания крупных составных сетей и управления такими сетями.'
);
GO

IF NOT EXISTS (
  SELECT 1 FROM BookAuthors ba
  JOIN Books b ON ba.BookId = b.Id
  JOIN Authors a ON ba.AuthorId = a.Id
  WHERE b.Title = N'Компьютерные сети. Принципы, технологии, протоколы' AND a.FullName = N'Олифер Виктор Григорьевич'
)
INSERT INTO BookAuthors (BookId, AuthorId)
SELECT b.Id, a.Id FROM Books b, Authors a
WHERE b.Title = N'Компьютерные сети. Принципы, технологии, протоколы' AND a.FullName = N'Олифер Виктор Григорьевич';

IF NOT EXISTS (
  SELECT 1 FROM BookAuthors ba
  JOIN Books b ON ba.BookId = b.Id
  JOIN Authors a ON ba.AuthorId = a.Id
  WHERE b.Title = N'Компьютерные сети. Принципы, технологии, протоколы' AND a.FullName = N'Олифер Наталья Викторовна'
)
INSERT INTO BookAuthors (BookId, AuthorId)
SELECT b.Id, a.Id FROM Books b, Authors a
WHERE b.Title = N'Компьютерные сети. Принципы, технологии, протоколы' AND a.FullName = N'Олифер Наталья Викторовна';
GO

-- Периодические издания

IF NOT EXISTS (SELECT 1 FROM Authors WHERE FullName = N'Кузнецов А.В.')
INSERT INTO Authors (FullName, Description) VALUES (
  N'Кузнецов А.В.',
  N''
);
IF NOT EXISTS (SELECT 1 FROM Authors WHERE FullName = N'Иванов П.И.')
INSERT INTO Authors (FullName, Description) VALUES (
  N'Иванов П.И.',
  N''
);
IF NOT EXISTS (SELECT 1 FROM Authors WHERE FullName = N'Юрова А.Б.')
INSERT INTO Authors (FullName, Description) VALUES (
  N'Юрова А.Б.',
  N''
);
IF NOT EXISTS (SELECT 1 FROM Authors WHERE FullName = N'Издательский дом «Северная Москва»')
INSERT INTO Authors (FullName, Description) VALUES (
  N'Издательский дом «Северная Москва»',
  N''
);
IF NOT EXISTS (SELECT 1 FROM Authors WHERE FullName = N'Петров А.Г.')
INSERT INTO Authors (FullName, Description) VALUES (
  N'Петров А.Г.',
  N''
);
GO

IF NOT EXISTS (SELECT 1 FROM Periodicals WHERE Title = N'Моя любимая дача')
INSERT INTO Periodicals (Title, Description) VALUES (
  N'Моя любимая дача',
  N'Журнал для дачников и садоводов.'
);
IF NOT EXISTS (SELECT 1 FROM Periodicals WHERE Title = N'Северная Москва')
INSERT INTO Periodicals (Title, Description) VALUES (
  N'Северная Москва',
  N'Газета о жизни Северного округа Москвы.'
);
GO

IF NOT EXISTS (
  SELECT 1 FROM PeriodicalAuthors pa
  JOIN Periodicals p ON pa.PeriodicalId = p.Id
  JOIN Authors a ON pa.AuthorId = a.Id
  WHERE p.Title = N'Моя любимая дача' AND a.FullName = N'Кузнецов А.В.'
)
INSERT INTO PeriodicalAuthors (PeriodicalId, AuthorId)
SELECT p.Id, a.Id FROM Periodicals p, Authors a
WHERE p.Title = N'Моя любимая дача' AND a.FullName = N'Кузнецов А.В.';

IF NOT EXISTS (
  SELECT 1 FROM PeriodicalAuthors pa
  JOIN Periodicals p ON pa.PeriodicalId = p.Id
  JOIN Authors a ON pa.AuthorId = a.Id
  WHERE p.Title = N'Моя любимая дача' AND a.FullName = N'Иванов П.И.'
)
INSERT INTO PeriodicalAuthors (PeriodicalId, AuthorId)
SELECT p.Id, a.Id FROM Periodicals p, Authors a
WHERE p.Title = N'Моя любимая дача' AND a.FullName = N'Иванов П.И.';

IF NOT EXISTS (
  SELECT 1 FROM PeriodicalAuthors pa
  JOIN Periodicals p ON pa.PeriodicalId = p.Id
  JOIN Authors a ON pa.AuthorId = a.Id
  WHERE p.Title = N'Моя любимая дача' AND a.FullName = N'Юрова А.Б.'
)
INSERT INTO PeriodicalAuthors (PeriodicalId, AuthorId)
SELECT p.Id, a.Id FROM Periodicals p, Authors a
WHERE p.Title = N'Моя любимая дача' AND a.FullName = N'Юрова А.Б.';

IF NOT EXISTS (
  SELECT 1 FROM PeriodicalAuthors pa
  JOIN Periodicals p ON pa.PeriodicalId = p.Id
  JOIN Authors a ON pa.AuthorId = a.Id
  WHERE p.Title = N'Северная Москва' AND a.FullName = N'Издательский дом «Северная Москва»'
)
INSERT INTO PeriodicalAuthors (PeriodicalId, AuthorId)
SELECT p.Id, a.Id FROM Periodicals p, Authors a
WHERE p.Title = N'Северная Москва' AND a.FullName = N'Издательский дом «Северная Москва»';

IF NOT EXISTS (SELECT 1 FROM Periodicals WHERE Title = N'Журнал «Мой Савеловский район»')
INSERT INTO Periodicals (Title, Description) VALUES (
  N'Журнал «Мой Савеловский район»',
  N'Журнал о прошлом и настоящем Савёловского района Москвы.'
);
GO

IF NOT EXISTS (
  SELECT 1 FROM PeriodicalAuthors pa
  JOIN Periodicals p ON pa.PeriodicalId = p.Id
  JOIN Authors a ON pa.AuthorId = a.Id
  WHERE p.Title = N'Журнал «Мой Савеловский район»' AND a.FullName = N'Юрова А.Б.'
)
INSERT INTO PeriodicalAuthors (PeriodicalId, AuthorId)
SELECT p.Id, a.Id FROM Periodicals p, Authors a
WHERE p.Title = N'Журнал «Мой Савеловский район»' AND a.FullName = N'Юрова А.Б.';

IF NOT EXISTS (
  SELECT 1 FROM PeriodicalAuthors pa
  JOIN Periodicals p ON pa.PeriodicalId = p.Id
  JOIN Authors a ON pa.AuthorId = a.Id
  WHERE p.Title = N'Журнал «Мой Савеловский район»' AND a.FullName = N'Петров А.Г.'
)
INSERT INTO PeriodicalAuthors (PeriodicalId, AuthorId)
SELECT p.Id, a.Id FROM Periodicals p, Authors a
WHERE p.Title = N'Журнал «Мой Савеловский район»' AND a.FullName = N'Петров А.Г.';
GO
