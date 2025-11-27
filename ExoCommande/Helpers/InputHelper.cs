using System;
using System.Collections.Generic;
using System.Text;

namespace ExoCommande.Helpers
{
    internal static class InputHelper
    {
        public static int AskInt(string message)
        {
            int value;
            Console.Write(message);
            while (!int.TryParse(Console.ReadLine(), out value))
                Console.Write("Erreur de saisie, réessayez : ");
            return value;
        }

        public static decimal AskDecimal(string message)
        {
            decimal value;
            Console.Write(message);
            while (!decimal.TryParse(Console.ReadLine(), out value))
                Console.Write("Erreur de saisie, réessayez : ");
            return value;
        }

        public static string AskString(string message, bool allowEmpty = false)
        {
            Console.Write(message);
            string input = Console.ReadLine()!;
            while (!allowEmpty && string.IsNullOrWhiteSpace(input))
            {
                Console.Write("Erreur de saisie, réessayez : ");
                input = Console.ReadLine()!;
            }
            return input;
        }

        public static string? AskOptionalString(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine()!;
            return string.IsNullOrWhiteSpace(input) ? null : input;
        }
    }

}
