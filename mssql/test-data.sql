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
  N'## Преступление и наказание' + CHAR(10) +
  N'Роман о бедном студенте **Родионе Раскольникове**, который совершает убийство и мучается угрызениями совести.'
);
IF NOT EXISTS (SELECT 1 FROM Books WHERE Title = N'Война и мир')
INSERT INTO Books (Title, PageCount, Description) VALUES (
  N'Война и мир',
  1225,
  N'## Война и мир' + CHAR(10) +
  N'Роман-эпопея, повествующий о судьбах нескольких семей на фоне **Отечественной войны 1812 года**.'
);
IF NOT EXISTS (SELECT 1 FROM Books WHERE Title = N'Идиот')
INSERT INTO Books (Title, PageCount, Description) VALUES (
  N'Идиот',
  640,
  N'## Идиот' + CHAR(10) +
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
