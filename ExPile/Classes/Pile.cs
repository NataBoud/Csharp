using System;
using System.Collections.Generic;
using System.Text;

namespace ExPile.Classes
{
    internal class Pile<T>
    {
        private T[] elements; // ne peut pas changer de taille une fois créé.
        public int Count => elements.Length;

        public Pile()
        {
            elements = [];
        }

        public void Empiler(T element)
        {
            Array.Resize(ref elements, elements.Length + 1);
            // Agrandit le tableau de 1 case supplémentaire,
            // Copie automatiquement toutes les anciennes valeurs dedans, 
            // Le paramètre ref veut dire :  remplace l’ancien tableau par le nouveau 
            elements[^1] = element; // elements[^1] Le dernier élément du tableau
        }

        public T Depiler()
        {
            if (elements.Length == 0)
                throw new Exception("La pile est vide !");

            T valeur = elements[^1];

            Array.Resize(ref elements, elements.Length - 1);

            return valeur;
        }

      
        public T RetirerParIndex(int index)
        {
            if (index < 0 || index >= elements.Length)
                throw new Exception("Index invalide !");

            T valeur = elements[index];

            for (int i = index; i < elements.Length - 1; i++)
                // Le dernier élément n’a pas de "élément suivant".
                elements[i] = elements[i + 1]; // Sinon elements[i + 1] plante.

            Array.Resize(ref elements, elements.Length - 1);

            return valeur;
        }

        public override string ToString()
        {
            if (elements.Length == 0)
                return "[Pile vide]";

            string contenu = string.Join(", ", elements);
            return $"[{contenu}]";
        }

        public void Afficher()
        {
            if (elements.Length == 0)
            {
                Console.WriteLine("Pile vide.");
                return;
            }

            Console.WriteLine("\nContenu de la pile :");
            for (int i = elements.Length - 1; i >= 0; i--)
                Console.WriteLine($"[{i}] => {elements[i]}");
        }
    }
}
