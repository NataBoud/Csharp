using test2.Classes;

//Console.WriteLine("== Test de la classe Chaise ==");

//Test t = new Test();
//t.AfficherInfos();
//t.Action();

//Chaise chaise1 = new Chaise();
//Console.WriteLine(chaise1);

//// Chaise avec constructeur avec paramètres
//Chaise chaise2 = new Chaise(3, "Métal", "Noir");
//Console.WriteLine(chaise2);

//// Chaise personnalisée
//Chaise chaise3 = new Chaise(5, "Plastique", "Rouge");
//Console.WriteLine(chaise3);

//Console.WriteLine("== Test de la classe Salarié ==");

//Salarie s1 = new Salarie("001", "Comptabilité", "A", "Dupont", 2500);
//Salarie s2 = new Salarie("002", "Informatique", "B", "Martin", 3200);
//Salarie s3 = new Salarie(); // Utilise le constructeur par défaut

//s1.AfficherSalaire();
//s2.AfficherSalaire();
//s3.AfficherSalaire();

//Salarie.AfficherTotaux();

//Salarie.RemettreAZero();
//Salarie.AfficherTotaux();

//Console.WriteLine("== Jeu du Pendu ==");

//GenerateurMots gen = new GenerateurMots();
//string mot = gen.ObtenirMot();

//Pendu pendu = new Pendu(mot, 10);

//while (pendu.EssaisRestants > 0 && !pendu.TestWin())
//{
//    Console.WriteLine($"Mot : {pendu.Masque}");
//    Console.Write("Entrez une lettre : ");
//    string input = Console.ReadLine();

//    if (!string.IsNullOrEmpty(input) && char.IsLetter(input[0]))
//    {
//        pendu.TestChar(input[0]);
//    }
//    else
//    {
//        Console.WriteLine("Veuillez entrer une lettre valide !");
//    }
//}

//if (pendu.TestWin())
//{
//    Console.WriteLine($"Bravo ! Vous avez trouvé le mot : {pendu.MotATrouver}");
//}
//else
//{
//    Console.WriteLine($"Perdu ! Le mot était : {pendu.MotATrouver}");
//}

Console.WriteLine("== Test de la classe WaterTank ==");

// Création de deux citernes
WaterTank citerne1 = new WaterTank(100, 500);  // 100 kg vide, 500 litres max
WaterTank citerne2 = new WaterTank(120, 300, 50); // 50 litres déjà

Console.WriteLine(citerne1);
Console.WriteLine(citerne2);

// Remplissage
double exces = citerne1.Remplir(600); // essaye de remplir 600 litres
Console.WriteLine($"Exces après remplissage citerne1 : {exces} litres");
Console.WriteLine(citerne1);

// Vider
double vide = citerne2.Vider(100); // essaye de vider 100 litres
Console.WriteLine($"Quantité réellement vidée citerne2 : {vide} litres");
Console.WriteLine(citerne2);

// Volume total de toutes les citernes
Console.WriteLine($"Volume total de toutes les citernes : {WaterTank.VolumeTotalToutesCiternes} litres");