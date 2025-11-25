using System;
using System.Collections.Generic;
using System.Text;
using ExFigure.Interfaces;

namespace ExFigure.Classes
{
    internal abstract class Figure : IDeplacable
    {
        public Point Origine { get; set; }

        public Figure(double x = 0, double y = 0)
        {
            Origine = new Point(x, y);
        }

        public virtual void Deplacement(double deltaX, double deltaY)
        {
            Origine.PosX += deltaX;
            Origine.PosY += deltaY;
        }

        // Méthode statique pour afficher une liste de figures
        public static void AfficherFigures(List<Figure> figures)
        {
            if (figures.Count == 0)
            {
                Console.WriteLine("Aucune figure enregistrée !");
                return;
            }

            Console.WriteLine("\nListe des figures :");
            for (int i = 0; i < figures.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {figures[i]}");
            }
        }


        public abstract override string ToString();
    }
}
