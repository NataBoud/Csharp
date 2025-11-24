using System;
using System.Collections.Generic;
using System.Text;

namespace testdino.Classes
{
    internal class Dinosaure
    {
        public string Nom { get; set; }
        public int Age { get; set; }
        public double Poids { get; set; }
        public bool PeutVoler { get; set; }

        // Constructeur simple
        public Dinosaure()
        {
            Nom = "Dinosaure";
            Age = 10;
            Poids = 500;
            PeutVoler = false;
        }

        // Méthode d'affichage
        public void AfficherInfos()
        {
            Console.WriteLine($"Nom : {Nom}");
            Console.WriteLine($"Age : {Age} ans");
            Console.WriteLine($"Poids : {Poids} kg");
            Console.WriteLine($"Peut voler : {(PeutVoler ? "Oui" : "Non")}");
        }

        // Méthode voler
        public void Senvoler()
        {
            if (PeutVoler)
            {
                Console.WriteLine($"{Nom} s'envole !");
            }
            else
            {
                Console.WriteLine($"{Nom} ne peut pas voler.");
            }
        }
    }
}
