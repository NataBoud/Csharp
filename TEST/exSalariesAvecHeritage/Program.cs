using exSalariesAvecHeritage.Classe;
using System;
using System.Collections.Generic;

Console.WriteLine("== Gestion des employés ==");

List<Salarie> employes = new List<Salarie>();

// Ajouter quelques employés
employes.Add(new Salarie("S001", "Alice", "RH", "A", 2000));
employes.Add(new Salarie("S002", "Bob", "IT", "B", 2200));
employes.Add(new Commercial("C001", "Charlie", "Ventes", "C", 1800, 10000, 5));
employes.Add(new Commercial("C002", "Diane", "Ventes", "C", 1900, 8000, 7));

// Afficher tous les salaires
Console.WriteLine("\n-- Salaires de tous les employés --");
foreach (var e in employes)
{
    e.AfficherSalaire();
}

// Recherche par début de nom
Console.WriteLine("\n-- Recherche par début de nom 'Ch' --");
string debutNom = "Ch";
foreach (var e in employes)
{
    if (e.Nom.StartsWith(debutNom, StringComparison.OrdinalIgnoreCase))
    {
        Console.WriteLine($"{e.Nom} trouvé :");
        e.AfficherSalaire();
    }
}

// Statistiques globales
Console.WriteLine($"\nNombre total d'employés : {Salarie.NombreEmployes}");
Console.WriteLine($"Salaire total : {Salarie.SalaireTotal}€");

