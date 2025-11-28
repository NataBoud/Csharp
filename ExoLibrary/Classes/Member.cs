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
            return $"{FirstName} {LastName}, Email : {Email}, Inscrit le : {RegistrationDate}, Créé le : {CreatedAt}, Modifié le : {UpdatedAt}";
        }

    }
}

