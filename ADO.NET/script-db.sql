CREATE TABLE Etudiant (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nom NVARCHAR(50) NOT NULL,
    prenom NVARCHAR(50) NOT NULL,
    numero_classe INT NOT NULL,
    date_diplome DATE
);

SELECT * FROM Etudiant;

INSERT INTO Etudiant (nom, prenom, numero_classe, date_diplome)
VALUES ('Dupont', 'Jean', 101, '2024-06-30');