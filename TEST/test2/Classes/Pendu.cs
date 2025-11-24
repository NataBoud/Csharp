using System;
using System.Collections.Generic;
using System.Text;

namespace test2.Classes
{
    internal class Pendu
    {
        public string MotATrouver { get; private set; }
        public string Masque { get; private set; }
        public int EssaisRestants { get; private set; }

        // Constructeur
        public Pendu(string mot, int nbEssais = 10)
        {
            MotATrouver = mot.ToLower();
            EssaisRestants = nbEssais;
            GenererMasque();
        }

        // Générer le masque avec des _ pour chaque lettre
        public void GenererMasque()
        {
            Masque = new string('_', MotATrouver.Length);
        }

        // Tester une lettre
        public void TestChar(char lettre)
        {
            lettre = char.ToLower(lettre);
            bool trouve = false;
            char[] masqueArray = Masque.ToCharArray();

            for (int i = 0; i < MotATrouver.Length; i++)
            {
                if (MotATrouver[i] == lettre)
                {
                    masqueArray[i] = lettre;
                    trouve = true;
                }
            }

            Masque = new string(masqueArray);

            if (!trouve)
            {
                EssaisRestants--;
                Console.WriteLine($"Lettre incorrecte ! Il reste {EssaisRestants} essais.");
            }
        }

        // Vérifier si le joueur a gagné
        public bool TestWin()
        {
            return Masque == MotATrouver;
        }
    }
}
