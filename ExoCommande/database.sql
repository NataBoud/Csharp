CREATE TABLE client (
	id INT IDENTITY(1,1) PRIMARY KEY,
	nom NVARCHAR(100) NOT NULL,
    prenom NVARCHAR(100) NOT NULL,
    adresse NVARCHAR(255) NOT NULL,
    codePostal NVARCHAR(10) NOT NULL,
    ville NVARCHAR(100) NOT NULL,
    telephone NVARCHAR(20) NULL,
    created_at DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    updated_at DATETIME2 NULL
);

CREATE TABLE commande (
    id INT IDENTITY(1,1) PRIMARY KEY,
    client_id INT NOT NULL,
    date_commande DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    total DECIMAL(10,2) NOT NULL,
    CONSTRAINT fk_commande_client_client_id FOREIGN KEY (client_id) REFERENCES client(id),
    created_at DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
    updated_at DATETIME2 NULL
);

SELECT * FROM client;
