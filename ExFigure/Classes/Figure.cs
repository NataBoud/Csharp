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

        public abstract override string ToString();
    }
}
