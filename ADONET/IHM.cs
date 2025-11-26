using System;
using ADONET.Models;
using ADONET.Repositories;

namespace ADONET
{
    internal class IHM
    {
        private readonly EtudiantRepository repo = new EtudiantRepository();

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
                    default: Console.WriteLine("Choix invalide."); break;
                }
            }
        }

        private void Ajouter()
        {
            Console.Write("Nom : ");
            string nom = Console.ReadLine()!;
            Console.Write("Prénom : ");
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

            if (repo.Save(e))
                Console.WriteLine("Étudiant ajouté !");
            else
                Console.WriteLine("Erreur lors de l'ajout.");
        }

        private void AfficherTous()
        {
            var etudiants = repo.GetEtudiants();
            if (etudiants.Count == 0)
            {
                Console.WriteLine("Aucun étudiant trouvé.");
                return;
            }

            foreach (var e in etudiants)
                Console.WriteLine(e);
        }

        private void Filtrer()
        {
            Console.Write("Numéro de classe : ");
            int classe = int.Parse(Console.ReadLine()!);

            var etudiants = repo.GetEtudiants(classe);
            if (etudiants.Count == 0)
            {
                Console.WriteLine("Aucun étudiant trouvé pour cette classe.");
                return;
            }

            foreach (var e in etudiants)
                Console.WriteLine(e);
        }

        private void Supprimer()
        {
            Console.Write("ID à supprimer : ");
            int id = int.Parse(Console.ReadLine()!);

            if (repo.Delete(id))
                Console.WriteLine("Supprimé !");
            else
                Console.WriteLine("Étudiant non trouvé ou erreur lors de la suppression.");
        }

        private void Modifier()
        {
            Console.Write("ID à modifier : ");
            int id = int.Parse(Console.ReadLine()!);

            var e = repo.GetById(id);
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

            Console.Write("Nouvelle date Diplôme (yyyy-mm-dd) ou vide : ");
            string? date = Console.ReadLine();
            e.DateDiplome = string.IsNullOrEmpty(date) ? null : DateTime.Parse(date);

            if (repo.Save(e))
                Console.WriteLine("Modifié !");
            else
                Console.WriteLine("Erreur lors de la modification.");
        }
    }
}
