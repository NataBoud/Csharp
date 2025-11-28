using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.Helpers
{
    internal class InputHelper
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

        public static string? AskOptionalString(string message, string? defaultValue = null)
        {
            Console.Write(message);
            string input = Console.ReadLine()!;
            return string.IsNullOrWhiteSpace(input) ? defaultValue : input;
        }

        public static decimal? AskDecimalOrEmpty(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(input))
                return null;

            decimal value;
            while (!decimal.TryParse(input, out value))
            {
                Console.Write("Valeur invalide ! Saisir un nombre (ou vide pour annuler) : ");
                input = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(input))
                    return null;
            }

            return value;
        }

        public static DateTime? AskDateOrEmpty(string message)
        {
            Console.Write(message);
            string input = Console.ReadLine()!;

            if (string.IsNullOrWhiteSpace(input))
                return null;

            DateTime value;
            while (!DateTime.TryParse(input, out value))
            {
                Console.Write("Date invalide ! Réessayer (jj/mm/aaaa) ou vide pour annuler : ");
                input = Console.ReadLine()!;

                if (string.IsNullOrWhiteSpace(input))
                    return null;
            }

            return value;
        }

    }
}
