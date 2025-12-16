using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hotel.models
{
  
    public class Client
    {
        [Key]
        public int Identifiant { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; }

        [Required]
        [MaxLength(50)]
        public string Prenom { get; set; }

        [Required]
        [Phone]
        public string? Telephone { get; set; }

        public List<Reservation> Reservations { get; set; }


        public override string ToString()
        {
            return $"{Nom} {Prenom} - {Telephone}";
        }
    }
}
