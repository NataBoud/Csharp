using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using ExHeritageCompteBancaire.Enums;

namespace ExHeritageCompteBancaire.Classes
{
    internal class Operation
    {
        public int Numero { get; set; }
        public double Montant { get; set; }
        public StatutOperation Statut { get; set; }

        public Operation(int numero, double montant, StatutOperation statut)
        {
            Numero = numero;
            Montant = montant;
            Statut = statut;
        }

        public override string ToString()
        {
            return $"Opération {Numero} - {Statut} de {Montant}€";
        }
    }
}
