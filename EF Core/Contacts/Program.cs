// See https://aka.ms/new-console-template for more information
using Contacts.data;
using Contacts.repository;
using Contacts.models;

Console.WriteLine("Hello, World!");


using var context = new ApplicationDbContext();

var produitRepository = new ContactRepository(context);

// Instancier le repository
var contactRepository = new ContactRepository(context);

// Créer un contact
Contact contact1 = new Contact
{
    Nom = "Doe",
    Prenom = "John",
    DateNaissance = new DateTime(1990, 1, 1),
    Genre = "Masculin",
    NumeroTelephone = "1234567890",
    Email = "john.doe@example.com"
};

// Ajouter le contact à la base
contactRepository.Add(contact1);

// Afficher l’âge calculé
Console.WriteLine($"L'âge de {contact1.Prenom} est {contact1.Age}");