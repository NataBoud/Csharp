namespace CaisseEnregistreuse.Models
{
    public class PanierItem
    {
        public int ProduitId { get; set; }
        public string Nom { get; set; } = string.Empty;
        public decimal Prix { get; set; }
        public int Quantite { get; set; } = 1;

        public decimal Total => Prix * Quantite;
    }
}
