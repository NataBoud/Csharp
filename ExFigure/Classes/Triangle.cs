using System;
using System.Collections.Generic;
using System.Text;

namespace ExFigure.Classes
{
    internal class Triangle : Figure
    {
        public double Base { get; set; }
        public double Hauteur { get; set; }

        public Triangle(double b, double h, double x = 0, double y = 0) : base(x, y)
        {
            Base = b;
            Hauteur = h;
        }

        public static Triangle AjouterTriangle()
        {
            Console.Write("Base du triangle : ");
            double b = double.Parse(Console.ReadLine()!);
            Console.Write("Hauteur du triangle : ");
            double h = double.Parse(Console.ReadLine()!);
            Console.Write("Origine X : ");
            double x = double.Parse(Console.ReadLine()!);
            Console.Write("Origine Y : ");
            double y = double.Parse(Console.ReadLine()!);

            return new Triangle(b, h, x, y);
        }

        public override string ToString()
        {
            return $"Triangle isocèle de base {Base} et hauteur {Hauteur}, origine en {Origine}";
        }
    }
}
