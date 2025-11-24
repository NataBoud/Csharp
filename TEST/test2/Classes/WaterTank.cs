using System;
using System.Collections.Generic;
using System.Text;

namespace test2.Classes
{
    internal class WaterTank
    {
        // Attributs d'instance
        public double PoidsVide { get; private set; }        // kg
        public double Capacite { get; private set; }        // litres
        public double Niveau { get; private set; }          // litres

        // Attribut statique pour total des volumes
        public static double VolumeTotalToutesCiternes { get; private set; } = 0;

        // Constructeur
        public WaterTank(double poidsVide, double capacite, double niveauInitial = 0)
        {
            PoidsVide = poidsVide;
            Capacite = capacite;
            Niveau = Math.Min(niveauInitial, capacite); // éviter de dépasser la capacité
            VolumeTotalToutesCiternes += Niveau;
        }

        // Méthode pour poids total (citerne + eau)
        public double PoidsTotal()
        {
            //  1 litre d'eau = 1 kg
            return PoidsVide + Niveau;
        }

        // Méthode pour remplir la citerne
        // Retourne l'excès si on dépasse la capacité
        public double Remplir(double litres)
        {
            if (litres <= 0) return 0;

            double espaceDisponible = Capacite - Niveau;
            double ajoutReel = Math.Min(litres, espaceDisponible);
            Niveau += ajoutReel;
            VolumeTotalToutesCiternes += ajoutReel;

            double exces = litres - ajoutReel;
            return exces; // 0 si pas de débordement
        }

        // Méthode pour vider la citerne
        // Retourne la quantité réellement vidée (max ce qu'il y a)
        public double Vider(double litres)
        {
            if (litres <= 0) return 0;

            double quantiteVidee = Math.Min(litres, Niveau);
            Niveau -= quantiteVidee;
            VolumeTotalToutesCiternes -= quantiteVidee;
            return quantiteVidee;
        }

        // Affichage de l'état de la citerne
        public override string ToString()
        {
            return $"Citerne : {Niveau}/{Capacite} litres, Poids total = {PoidsTotal()} kg";
        }
    }
}
