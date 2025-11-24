using System;
using System.Collections.Generic;
using System.Text;

namespace exSalariesAvecHeritage.Classe
{
    internal class Commercial : Salarie
    {
        // Propriétés supplémentaires
        public double ChiffreAffaire { get; set; }
        public double CommissionPct { get; set; }

        // Constructeur par défaut
        public Commercial() : base()
        {
        }

        // Constructeur avec paramètres
        public Commercial(string matricule, string nom, string service, string categorie, double salaire,
            double chiffreAffaire, double commissionPct)
            : base(matricule, nom, service, categorie, salaire)
        {
            ChiffreAffaire = chiffreAffaire;
            CommissionPct = commissionPct;
        }

        // Surcharge de AfficherSalaire
        public override void AfficherSalaire()
        {
            double salaireReel = Salaire + ChiffreAffaire * (CommissionPct / 100);
            Console.WriteLine($"{Nom} ({Matricule}) : {salaireReel} € (Fixe: {Salaire} + Commission: {ChiffreAffaire * CommissionPct / 100}€)");
        }

        public override string ToString()
        {
            return $"[{Matricule}] {Nom} - Service: {Service}, Catégorie: {Categorie}, Salaire fixe: {Salaire}€, CA: {ChiffreAffaire}€, Commission: {CommissionPct}%";
        }
    }
}
