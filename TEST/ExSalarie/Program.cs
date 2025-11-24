using ExSalarie.Classes;


Console.WriteLine("== Test de la classe Salarié ==");

Salarie s1 = new Salarie("001", "Comptabilité", "A", "Dupont", 2500);
Salarie s2 = new Salarie("002", "Informatique", "B", "Martin", 3200);
Salarie s3 = new Salarie(); // Utilise le constructeur par défaut

s1.AfficherSalaire();
s2.AfficherSalaire();
s3.AfficherSalaire();

Salarie.AfficherTotaux();

Salarie.RemettreAZero();
Salarie.AfficherTotaux();

