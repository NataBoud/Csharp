using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADONET
{
    internal class Etudiant
    {
        private static readonly string connectionString =
            "Data Source=(localdb)\\CoursSQL;Initial Catalog=CoursSQL;Integrated Security=True";

        public int Id { get; set; }
        public string Nom { get; set; } = "";
        public string Prenom { get; set; } = "";
        public int NumeroClasse { get; set; }
        public DateTime? DateDiplome { get; set; }

        public Etudiant() { }


        public Etudiant(string nom, string prenom, int numeroClasse, DateTime? dateDiplome)
        {
            Nom = nom;
            Prenom = prenom;
            NumeroClasse = numeroClasse;
            DateDiplome = dateDiplome;
        }


        // -------------------------
        //  SAVE = insert ou update
        // -------------------------
        public bool Save()
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd;

            if (Id == 0) // INSERT
            {
                cmd = new SqlCommand(
                    @"INSERT INTO Etudiant (nom, prenom, numero_classe, date_diplome)
                  VALUES (@nom, @prenom, @classe, @date);
                  SELECT SCOPE_IDENTITY();", conn);

                cmd.Parameters.AddWithValue("@nom", Nom);
                cmd.Parameters.AddWithValue("@prenom", Prenom);
                cmd.Parameters.AddWithValue("@classe", NumeroClasse);
                cmd.Parameters.AddWithValue("@date", (object?)DateDiplome ?? DBNull.Value);

                Id = Convert.ToInt32(cmd.ExecuteScalar());
                return Id > 0;
            }
            else // UPDATE
            {
                cmd = new SqlCommand(
                    @"UPDATE Etudiant SET nom=@nom, prenom=@prenom,
                  numero_classe=@classe, date_diplome=@date
                  WHERE id=@id", conn);

                cmd.Parameters.AddWithValue("@id", Id);
                cmd.Parameters.AddWithValue("@nom", Nom);
                cmd.Parameters.AddWithValue("@prenom", Prenom);
                cmd.Parameters.AddWithValue("@classe", NumeroClasse);
                cmd.Parameters.AddWithValue("@date", (object?)DateDiplome ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // -------------------------
        //  DELETE
        // -------------------------
        public bool Delete()
        {
            if (Id == 0) return false;

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM Etudiant WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", Id);

            return cmd.ExecuteNonQuery() > 0;
        }

        // -------------------------
        //  GET BY ID
        // -------------------------
        public static Etudiant? GetById(int id)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                "SELECT id, nom, prenom, numero_classe, date_diplome FROM Etudiant WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (!reader.Read()) return null;

            return new Etudiant
            {
                Id = reader.GetInt32(0),
                Nom = reader.GetString(1),
                Prenom = reader.GetString(2),
                NumeroClasse = reader.GetInt32(3),
                DateDiplome = reader.IsDBNull(4) ? null : reader.GetDateTime(4)
            };
        }

        // -------------------------
        //  GET LIST + FILTER
        // -------------------------
        public static List<Etudiant> GetEtudiants(int? numeroClasse = null)
        {
            List<Etudiant> resultat = new();

            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            string query = "SELECT id, nom, prenom, numero_classe, date_diplome FROM Etudiant";
            if (numeroClasse != null)
                query += " WHERE numero_classe=@classe";

            SqlCommand cmd = new SqlCommand(query, conn);
            if (numeroClasse != null)
                cmd.Parameters.AddWithValue("@classe", numeroClasse);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                resultat.Add(new Etudiant
                {
                    Id = reader.GetInt32(0),
                    Nom = reader.GetString(1),
                    Prenom = reader.GetString(2),
                    NumeroClasse = reader.GetInt32(3),
                    DateDiplome = reader.IsDBNull(4) ? null : reader.GetDateTime(4)
                });
            }

            return resultat;
        }

        // -------------------------
        //  EDIT depuis un objet
        // -------------------------
        public static bool EditEtudiant(int id, Etudiant data)
        {
            using SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = new SqlCommand(
                @"UPDATE Etudiant SET nom=@nom, prenom=@prenom,
              numero_classe=@classe, date_diplome=@date
              WHERE id=@id", conn);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nom", data.Nom);
            cmd.Parameters.AddWithValue("@prenom", data.Prenom);
            cmd.Parameters.AddWithValue("@classe", data.NumeroClasse);
            cmd.Parameters.AddWithValue("@date", (object?)data.DateDiplome ?? DBNull.Value);

            return cmd.ExecuteNonQuery() > 0;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Nom: {Nom}, Prénom: {Prenom}, Classe: {NumeroClasse}, Diplôme: {(DateDiplome?.ToString("yyyy-MM-dd") ?? "N/A")}";
        }

    }
}
