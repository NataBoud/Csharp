using System;
using System.Collections.Generic;
using System.Text;

namespace test2.Classes
{
    internal class Test
    {

        // Propriété simple
        public string Nom { get; set; }

        // Constructeur
        public Test()
        {
            Nom = "Objet Test";
        }

        // Méthode pour afficher les informations
        public void AfficherInfos()
        {
            Console.WriteLine($"Nom de l'objet : {Nom}");
        }

        // Méthode pour tester une action
        public void Action()
        {
            Console.WriteLine($"{Nom} effectue une action !");
        }
    }
}
