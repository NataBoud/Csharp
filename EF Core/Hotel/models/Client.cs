using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hotel.models
{
  
    internal class Client
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Nom { get; set; }

        [Required]
        [MaxLength(50)]
        public string Prenom { get; set; }

        [Required]
        [Phone]
        public string NumeroTelephone { get; set; }



        public override string ToString()
        {
            return $"{Nom} {Prenom} - {NumeroTelephone}";
        }
    }
}
