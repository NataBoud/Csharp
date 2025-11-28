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