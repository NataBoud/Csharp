using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Contacts.models
{
    internal class Contact
    {
        //[Key]
        public int Id { get; set; }

        [Required] 
        [MaxLength(50)] 
        public string Nom { get; set; }

        [Required]
        [MaxLength(50)]
        public string Prenom { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateNaissance { get; set; }

        [NotMapped] // EF Core ne mappe pas cette propriété dans la DB
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - DateNaissance.Year;
                if (DateNaissance.Date > today.AddYears(-age)) age--;
                return age;
            }
        }

        [Required]
        [MaxLength(10)]
        public string Genre { get; set; } 

        [Required]
        [Phone]
        public string NumeroTelephone { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
