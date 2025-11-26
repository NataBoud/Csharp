using System;
using System.Collections.Generic;
using System.Text;


namespace ADONET
{
    internal class IHM
    {
        public void Demarrer()
        {
            while (true)
            {
                Console.WriteLine("\n=== MENU ETUDIANT ===");
                Console.WriteLine("1 - Ajouter étudiant");
                Console.WriteLine("2 - Afficher tous");
                Console.WriteLine("3 - Filtrer par classe");
                Console.WriteLine("4 - Supprimer");
                Console.WriteLine("5 - Modifier");
                Console.WriteLine("0 - Quitter");
                Console.Write("Choix : ");

                switch (Console.ReadLine())
                {
                    case "1": Ajouter(); break;
                    case "2": AfficherTous(); break;
                    case "3": Filtrer(); break;
                    case "4": Supprimer(); break;
                    case "5": Modifier(); break;
                    case "0": return;
                }
            }
        }

        private void Ajouter()
        {
            Console.Write("Nom : ");
            string nom = Console.ReadLine()!;
            Console.Write("Prenom : ");
            string prenom = Console.ReadLine()!;
            Console.Write("Classe : ");
            int classe = int.Parse(Console.ReadLine()!);
            Console.Write("Date Diplôme (yyyy-mm-dd) ou vide : ");
            string? date = Console.ReadLine();

            Etudiant e = new Etudiant(
                nom,
                prenom,
                classe,
                string.IsNullOrEmpty(date) ? null : DateTime.Parse(date)
            );

            e.Save();
            Console.WriteLine("✔ Étudiant ajouté !");
        }

        private void AfficherTous()
        {
            foreach (var e in Etudiant.GetEtudiants())
                Console.WriteLine(e);
        }

        private void Filtrer()
        {
            Console.Write("Numéro de classe : ");
            int classe = int.Parse(Console.ReadLine()!);

            foreach (var e in Etudiant.GetEtudiants(classe))
                Console.WriteLine(e);
        }

        private void Supprimer()
        {
            Console.Write("ID à supprimer : ");
            int id = int.Parse(Console.ReadLine()!);

            var etu = Etudiant.GetById(id);
            if (etu == null)
            {
                Console.WriteLine("Non trouvé");
                return;
            }

            etu.Delete();
            Console.WriteLine("✔ Supprimé !");
        }

        private void Modifier()
        {
            Console.Write("ID à modifier : ");
            int id = int.Parse(Console.ReadLine()!);
            var e = Etudiant.GetById(id);

            if (e == null)
            {
                Console.WriteLine("Non trouvé");
                return;
            }

            Console.Write("Nouveau nom : ");
            e.Nom = Console.ReadLine()!;

            Console.Write("Nouveau prénom : ");
            e.Prenom = Console.ReadLine()!;

            Console.Write("Nouvelle classe : ");
            e.NumeroClasse = int.Parse(Console.ReadLine()!);

            e.Save();
            Console.WriteLine("✔ Modifié !");
        }
    }
}
