USE CoursSQL;

CREATE TABLE People (
   id INT PRIMARY KEY IDENTITY(1,1), -- Unique ID for each person
   first_name NVARCHAR(50) NOT NULL, -- First name of the person
   last_name NVARCHAR(50) NOT NULL, -- Last name of the person
   age INT, -- Age of the person
   phone_number NVARCHAR(20), -- Phone number of the person
   address NVARCHAR(100), -- Address of the person
);

SELECT * FROM [People];

CREATE TABLE Dogs (
   id INT PRIMARY KEY IDENTITY(1,1), -- Unique ID for each dog
   name NVARCHAR(50) NOT NULL, -- Name of the dog
   breed NVARCHAR(50), -- Breed of the dog
   age INT, -- Age of the dog
   size DECIMAL(5,2), -- Size (in cm) of the dog
   weight DECIMAL(5,2), -- Weight (in kg) of the dog
   owner_id INT, -- Foreign key referencing Person
   FOREIGN KEY (owner_id) REFERENCES People(id)
);

SELECT * FROM [Dogs];

INSERT INTO People (first_name, last_name, age, phone_number, address)
VALUES 
   ('Tintin', 'Dupont', 30, '1234567890', '123 Rue du Temple'),
   ('Asterix', 'Gaulois', 40, '9876543210', '456 Village Gaulois'),
   ('Sherlock', 'Holmes', 45, '5554443333', '123 Main St'),
   ('Hercule', 'Poirot', 50, '4443332222', '11 Rue du Luxembourg'),
   ('Gandalf', 'Le Gris', 1000, '1112223333', 'Hobbiton'),
   ('Luke', 'Skywalker', 28, '9988776655', 'Tatooine'),
   ('Harry', 'Potter', 35, '5556667777', '4 Privet Drive'),
   ('Daenerys', 'Targaryen', 32, '8887776666', 'Meereen'),
   ('Frodo', 'Baggins', 33, '1237894560', 'Bag End'),
   ('Waldo', 'Rosenbaum', 50, '7778889999', 'Nowhere Street');

INSERT INTO Dogs (name, breed, age, size, weight, owner_id)
VALUES 
   ('Milou', 'Fox Terrier', 5, 30.0, 8.0, 1),
   ('Idefix', 'Dogmatix', 4, 25.0, 6.0, 2), 
   ('Watson', 'Bulldog', 6, 60.0, 30.0, 3), 
   ('Hercules', 'Pitbull', 3, 60.0, 28.0, 4), 
   ('Gandalf', 'Great Dane', 8, 80.0, 50.0, 5),
   ('Chewie', 'Malamute', 7, 70.0, 40.0, 6), 
   ('Buck', 'Saint Bernard', 6, 70.0, 50.0, 7),
   ('Drogo', 'Dobermann', 5, 55.0, 35.0, 8), 
   ('Baggins', 'Shiba Inu', 4, 30.0, 10.0, NULL),
   ('Waldo', 'Chihuahua', 3, 20.0, 2.5, 10), 
   ('Rex', 'Chihuahua', 3, 20.0, 3.0, NULL), 
   ('Pepette', 'Rottweiler', 6, 60.0, 40.0, 5), 
   ('Princesse', 'Dobermann', 4, 50.0, 30.0, 5), 
   ('Rex', 'Dalmatian', 2, 45.0, 25.0, 5), 
   ('Trixie', 'Poodle', 5, 30.0, 12.0, 5), 
   ('Nina', 'Boxer', 4, 50.0, 35.0, NULL), 
   ('Pikachu', 'Corgi', 2, 25.0, 10.0, 8), 
   ('Rolo', 'Dachshund', 3, 28.0, 8.5, NULL), 
   ('Fifi', 'Maltese', 4, 25.0, 6.0, NULL), 
   ('Charlie', 'Beagle', 6, 40.0, 15.0, NULL), 
   ('Max', 'Labrador', 5, 55.0, 30.0, NULL), 
   ('Biscuit', 'Shih Tzu', 2, 25.0, 6.0, 8),
   ('Daisy', 'Pug', 3, 35.0, 10.0, NULL), 
   ('Oscar', 'Terrier', 4, 28.0, 8.0, NULL), 
   ('Nala', 'Pitbull', 4, 50.0, 30.0, NULL); 

--Selectionnez tous les chiens avec leur nom, leur race et leur poids.
SELECT name, breed, weight
FROM Dogs;

-- Sélectionnez tous les maîtres avec leur prénom et leur nom de famille.
SELECT first_name, last_name
FROM People;

-- Sélectionnez tous les chiens qui n'ont pas de maître.
SELECT *
FROM Dogs
WHERE owner_id IS NULL;

-- Sélectioner tous les chiens de race "Labrador".
SELECT *
FROM Dogs
WHERE breed = 'Labrador';

-- Affichez le nom des chiens avec le pr�nom et le nom de leur ma�tre.
SELECT 
	d.name AS DogName,
	p.first_name AS OwnerFirstName,
	p.last_name AS OwnerLastName
FROM Dogs d
JOIN People p ON d.owner_id = p.id;	

-- Affichez les prénoms et noms des maîtres qui possèdent des chiens pesant plus de 20 kg.
SELECT 
   p.first_name,
   p.last_name
FROM People p
JOIN Dogs d ON d.owner_id = p.id
WHERE d.weight > 20;

-- Affichez tous les maîtres et leurs chiens, y compris les maîtres sans chiens et les chiens sans maîtres.
SELECT
	p.first_name as OwnerFirstName,
	p.last_name as OwnerLastName,
	d.name as DogName
FROM People p
LEFT JOIN Dogs d ON d.owner_id = p.id;

-- Affichez tous les chiens avec le nom de leur maître, en remplaçant les valeurs NULL par 'No Owner'.
SELECT
	d.name as DogName,
	COALESCE(p.first_name, 'No owner') AS first_name,
	COALESCE(p.last_name, 'No owner') AS last_name
FROM Dogs d
LEFT JOIN People p ON d.owner_id = p.id;

-- Affichez une liste combinée de tous les chiens et de tous les maîtres, même s'ils n'ont pas de correspondance.
SELECT
	d.name as DogName,
	d.breed as Breed,
	p.first_name as OwnerFirstName,
	p.last_name as OwnerLastName
FROM Dogs d
FULL OUTER JOIN People p ON d.owner_id = p.id;

-- Affichez les chiens dont le poids est supérieur à 10 kg et inférieur à 30 kg.
SELECT *
FROM Dogs
WHERE weight BETWEEN 10 AND 30;

-- Affichez les chiens appartenant aux maîtres vivant à l'adresse '123 Main St'.
SELECT d.name AS DogName, d.breed, d.weight,
       p.first_name, p.last_name, p.address
FROM Dogs d
INNER JOIN People p
    ON d.owner_id = p.id
WHERE p.address = '123 Main St';

-- Agrégation - Nombre de chiens par maître
SELECT 
	CONCAT_WS(' ', p.first_name, p.last_name) AS Maitre,
	COUNT(d.id) AS number_of_dogs
FROM 
	People p
LEFT JOIN 
	Dogs d ON d.owner_id = p.id
GROUP BY 
	p.id, p.first_name, p.last_name
ORDER BY
	number_of_dogs DESC;

-- Agrégation - Poids total des chiens par maître
SELECT 
    CONCAT_WS(' ', p.first_name, p.last_name) AS Maitre,
    SUM(d.weight) AS poids_total_chiens
FROM
	Dogs d
INNER JOIN
	People p ON p.id = d.owner_id
GROUP BY 
    p.first_name, p.last_name
ORDER BY
	poids_total_chiens DESC;

-- Sous-requête - Récupérer les maîtres qui possèdent le chien le plus lourd
SELECT
	p.first_name,
	p.last_name
FROM
	People p
INNER JOIN
	Dogs d ON d.owner_id = p.id
WHERE
	d.[weight] = (SELECT MAX([weight]) FROM Dogs);

-- Afficher les chiens qui ont un maître dont l’âge est supérieur à 40 ans
SELECT 
	d.name, d.breed, d.age, d.size, d.weight, p.first_name, p.last_name, p.age
FROM Dogs d
JOIN People p ON d.owner_id = p.id
WHERE p.age > 40;

-- Listez les maitres n'ayant pas de chien.
SELECT p.first_name, p.last_name
FROM People p
LEFT JOIN Dogs d ON p.id = d.owner_id
WHERE d.id IS NULL;

-- Race la plus courante parmi les chiens
SELECT TOP 1 d.breed, COUNT(*) AS breed_count
FROM Dogs d
GROUP BY d.breed
ORDER BY breed_count DESC;

-- Maîtres possédant au moins deux chiens
SELECT p.first_name, p.last_name, COUNT(d.id) AS number_of_dogs
FROM People p
JOIN Dogs d ON p.id = d.owner_id
GROUP BY p.id, p.first_name, p.last_name
HAVING COUNT(d.id) >= 2;

-- Liste combinée de chiens sans maîtres et de maîtres sans chiens
-- SELECT 
   -- p.first_name AS owner_first_name,
    --p.last_name AS owner_last_name,
    -- d.name AS dog_name,
   --  d.breed AS dog_breed
-- FROM People p
-- FULL OUTER JOIN Dogs d
    -- ON p.id = d.owner_id
-- ORDER BY owner_last_name, dog_name;

SELECT
	d.name,
	p.first_name,
	p.last_name
FROM
	Dogs d
FULL OUTER JOIN
	People p ON p.id = d.owner_id
WHERE
	d.owner_id IS NULL OR p.id IS NULL;

-- Maître et chiens associés avec somme de leurs tailles
SELECT 
    p.first_name,
    p.last_name,
    SUM(COALESCE(d.size, 0)) AS total_dog_size --ISNULL(d.size, 0) sert à gérer le cas où un maître n’a pas de chien.
FROM People p
LEFT JOIN Dogs d
    ON p.id = d.owner_id
GROUP BY p.id, p.first_name, p.last_name
ORDER BY p.last_name, p.first_name;
