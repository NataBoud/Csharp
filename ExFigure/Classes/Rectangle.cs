using System;
using System.Collections.Generic;
using System.Text;

namespace ExFigure.Classes
{
    internal class Rectangle : Figure
    {
        public double Longueur { get; set; }
        public double Largeur { get; set; }

        public Rectangle(double longueur, double largeur, double x = 0, double y = 0) : base(x, y)
        {
            Longueur = longueur;
            Largeur = largeur;
        }

        public static Rectangle AjouterRectangle()
        {
            Console.Write("Longueur : ");
            double longueur = double.Parse(Console.ReadLine()!);
            Console.Write("Largeur : ");
            double largeur = double.Parse(Console.ReadLine()!);
            Console.Write("Origine X : ");
            double x = double.Parse(Console.ReadLine()!);
            Console.Write("Origine Y : ");
            double y = double.Parse(Console.ReadLine()!);

            return new Rectangle(longueur, largeur, x, y);
        }

        public override string ToString()
        {
            return $"Rectangle {Longueur} x {Largeur}, origine en {Origine}";
        }
    }
}
