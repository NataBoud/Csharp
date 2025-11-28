CREATE TABLE Book (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(255) NOT NULL,
    Author NVARCHAR(255) NOT NULL,
    ISBN NVARCHAR(50) NOT NULL UNIQUE,
    PublicationYear INT NOT NULL,
    IsAvailable BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL
);

CREATE TABLE Member (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    LastName NVARCHAR(255) NOT NULL,
    FirstName NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL UNIQUE,
    RegistrationDate DATE NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL
);

CREATE TABLE Borrow (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BookId INT NOT NULL,
    MemberId INT NOT NULL,
    BorrowDate DATE NOT NULL,
    ReturnDate DATE NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UpdatedAt DATETIME2 NULL,
    CONSTRAINT FK_Borrow_Book
        FOREIGN KEY (BookId) REFERENCES Book(Id)
        ON DELETE CASCADE,
    CONSTRAINT FK_Borrow_Member
        FOREIGN KEY (MemberId) REFERENCES Member(Id)
        ON DELETE CASCADE
);

-- Supprimer la table Borrow d'abord car elle référence Book et Member
IF OBJECT_ID('Borrow', 'U') IS NOT NULL
    DROP TABLE Borrow;

-- Supprimer la table Book
IF OBJECT_ID('Book', 'U') IS NOT NULL
    DROP TABLE Book;

-- Supprimer la table Member
IF OBJECT_ID('Member', 'U') IS NOT NULL
    DROP TABLE Member;

SELECT * FROM Book;
SELECT * FROM Member;
SELECT * FROM Borrow;

INSERT INTO Book (Title, Author, ISBN, PublicationYear)
VALUES 
('Le Petit Prince', 'Antoine de Saint-Exupéry', '978-0156012195', 1943),
('1984', 'George Orwell', '978-0451524935', 1949),
('Harry Potter à l''école des sorciers', 'J.K. Rowling', '978-0747532699', 1997),
('Les Misérables', 'Victor Hugo', '978-2070409187', 1862),
('Le Seigneur des Anneaux', 'J.R.R. Tolkien', '978-0261102385', 1954);

INSERT INTO Member (LastName, FirstName, Email, RegistrationDate)
VALUES
('Dupont', 'Jean', 'jean.dupont@email.com', '2023-01-10'),
('Martin', 'Sophie', 'sophie.martin@email.com', '2023-02-15'),
('Durand', 'Paul', 'paul.durand@email.com', '2023-03-05'),
('Lefevre', 'Claire', 'claire.lefevre@email.com', '2023-04-12'),
('Moreau', 'Lucas', 'lucas.moreau@email.com', '2023-05-20');

INSERT INTO Borrow (BookId, MemberId, BorrowDate, ReturnDate)
VALUES
(1, 1, '2025-11-01', NULL),   
(3, 2, '2025-11-05', '2025-11-20'), 
(2, 3, '2025-11-10', NULL),   
(5, 4, '2025-11-12', NULL);
