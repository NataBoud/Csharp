using System;
using System.Collections.Generic;
using System.Text;

namespace test2.Classes
{
    internal class GenerateurMots
    {
        private string[] mots = { "dinosaure", "ordinateur", "chaise", "voiture", "maison" };
        private Random random = new Random();

        // Méthode pour récupérer un mot aléatoire
        public string ObtenirMot()
        {
            int index = random.Next(mots.Length);
            return mots[index];
        }
    }
}
