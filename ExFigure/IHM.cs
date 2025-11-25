using System;
using System.Collections.Generic;
using ExFigure.Classes;

namespace ExFigure
{
    internal class IHM
    {
        // IDE0044: Rendre le champ readonly
        // IDE0028: L'initialisation des collections peut être simplifiée
        // IDE0090: L'expression 'new' peut être simplifiée
        private readonly List<Figure> figures = [];

        // Méthode pour démarrer l'IHM
        public void Start()
        {
            while (true)
            {
                Console.WriteLine("\n=== Gestion des figures ===");
                Console.WriteLine("1 - Ajouter un carré");
                Console.WriteLine("2 - Ajouter un rectangle");
                Console.WriteLine("3 - Ajouter un triangle");
                Console.WriteLine("4 - Afficher toutes les figures");
                Console.WriteLine("5 - Déplacer une figure");
                Console.WriteLine("0 - Quitter");
                Console.Write("Votre choix : ");
                string choix = Console.ReadLine()!;

                switch (choix)
                {
                    case "1":
                        figures.Add(Carre.AjouterCarre());
                        break;
                    case "2":
                        figures.Add(Rectangle.AjouterRectangle());
                        break;
                    case "3":
                        figures.Add(Triangle.AjouterTriangle());
                        break;
                    case "4":
                        Figure.AfficherFigures(figures);
                        break;
                    case "5":
                        DeplacerFigure();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Erreur de saisie !");
                        break;
                }
            }
        }


        private void DeplacerFigure()
        {
            if (figures.Count == 0)
            {
                Console.WriteLine("Aucune figure à déplacer !");
                return;
            }

            Figure.AfficherFigures(figures);
            Console.Write("Sélectionnez une figure par numéro : ");
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > figures.Count)
            {
                Console.WriteLine("Figure invalide !");
                return;
            }

            Figure figure = figures[index - 1];
            Console.Write("Déplacement en X : ");
            double dx = double.Parse(Console.ReadLine()!);
            Console.Write("Déplacement en Y : ");
            double dy = double.Parse(Console.ReadLine()!);

            figure.Deplacement(dx, dy);
            Console.WriteLine("Figure déplacée !");
        }
    }
}
