using System;
using System.Collections.Generic;
using System.Text;

namespace ExoLibrary.Classes
{
    internal class Member
    {

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime CreatedAt { get; set; }     
        public DateTime? UpdatedAt { get; set; }    

        public Member(string lastName, string firstName, string email, DateTime registrationDate)
        {
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            RegistrationDate = registrationDate;
            CreatedAt = DateTime.Now;
            UpdatedAt = null;
        }

        public Member(int id, string lastName, string firstName, string email, DateTime registrationDate, DateTime createdAt, DateTime? updatedAt)
        {
            Id = id;
            LastName = lastName;
            FirstName = firstName;
            Email = email;
            RegistrationDate = registrationDate;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"        Nom        : {LastName}");
            sb.AppendLine($"        Prénom     : {FirstName}");
            sb.AppendLine($"        Email      : {Email}");
            sb.AppendLine($"        Inscrit le : {RegistrationDate:dd/MM/yyyy}");
            sb.AppendLine($"        Créé le    : {CreatedAt:dd/MM/yyyy HH:mm}");

            if (UpdatedAt != null)
                sb.AppendLine($"        Modifié le : {UpdatedAt:dd/MM/yyyy HH:mm}");

            return sb.ToString();
        }

    }
}

