using System;
using System.Collections.Generic;
using System.Text;

namespace ExFigure.Classes
{
    internal class Carre : Figure
    {
        public double Cote { get; set; }
        public Carre(double cote, double x = 0, double y = 0) : base(x, y)
        {
            Cote = cote;
        }

        public static Carre AjouterCarre()
        {
            Console.Write("Côté du carré : ");
            double cote = double.Parse(Console.ReadLine()!);
            Console.Write("Origine X : ");
            double x = double.Parse(Console.ReadLine()!);
            Console.Write("Origine Y : ");
            double y = double.Parse(Console.ReadLine()!);

            return new Carre(cote, x, y);
        }
        public override string ToString()
        {
            return $"Carré de côté {Cote} avec origine en {Origine}";
        }
    }
}
