using System;
using System.Collections.Generic;
using System.Text;

namespace test2.Classes
{
    internal class Chaise
    {
        public int NbPieds { get; set; }
        public string Materiau { get; set; }
        public string Couleur { get; set; }

        // Constructeur par défaut
        public Chaise()
        {
            NbPieds = 4;
            Materiau = "Bois";
            Couleur = "Marron";
        }

        // Constructeur avec paramètres
        public Chaise(int nbPieds, string materiau, string couleur)
        {
            NbPieds = nbPieds;
            Materiau = materiau;
            Couleur = couleur;
        }

        // Surcharge de ToString pour afficher les infos
        public override string ToString()
        {
            return $"Chaise : {NbPieds} pieds, matériau = {Materiau}, couleur = {Couleur}";
        }
    }
}
