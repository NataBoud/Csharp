using System;
using System.Collections.Generic;
using System.Text;

namespace ExPile.Classes
{
    // la nouvelle syntaxe C# 12 appelée “Primary Constructors for classes”.
    internal class Personne(string nom, string prenom, int age)
    {
        public string Nom { get; set; } = nom;
        public string Prenom { get; set; } = prenom;
        public int Age { get; set; } = age;


        // Méthode pour créer une personne via saisie console
        public static Personne SaisiePersonne()
        {
            Console.Write("Nom : ");
            string nom = Console.ReadLine()!;
            Console.Write("Prénom : ");
            string prenom = Console.ReadLine()!;
            Console.Write("Age : ");
            int age = int.Parse(Console.ReadLine()!);

            return new Personne(nom, prenom, age);
        }

        public override string ToString()
        {
            return $"{Nom} {Prenom} ({Age} ans)";
        }
    }
}
