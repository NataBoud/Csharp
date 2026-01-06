using Microsoft.Data.SqlClient;
using ADONET.Models;
using System;
using System.Collections.Generic;

namespace ADONET.Repositories
{
    internal class EtudiantRepository
    {
        private readonly string connectionString =
            "Data Source=(localdb)\\CoursSQL;Initial Catalog=CoursSQL;Integrated Security=True";

        // -------------------------
        // INSERT ou UPDATE
        // -------------------------
        public bool Save(Etudiant etu)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();

            SqlCommand cmd;

            if (etu.Id == 0) // INSERT
            {
                cmd = new SqlCommand(
                    @"INSERT INTO Etudiant (nom, prenom, numero_classe, date_diplome)
                      VALUES (@nom, @prenom, @classe, @date);
                      SELECT SCOPE_IDENTITY();", conn);

                cmd.Parameters.AddWithValue("@nom", etu.Nom);
                cmd.Parameters.AddWithValue("@prenom", etu.Prenom);
                cmd.Parameters.AddWithValue("@classe", etu.NumeroClasse);
                cmd.Parameters.AddWithValue("@date", (object?)etu.DateDiplome ?? DBNull.Value);

                etu.Id = Convert.ToInt32(cmd.ExecuteScalar());
                return etu.Id > 0;
            }
            else // UPDATE
            {
                cmd = new SqlCommand(
                    @"UPDATE Etudiant SET nom=@nom, prenom=@prenom,
                      numero_classe=@classe, date_diplome=@date
                      WHERE id=@id", conn);

                cmd.Parameters.AddWithValue("@id", etu.Id);
                cmd.Parameters.AddWithValue("@nom", etu.Nom);
                cmd.Parameters.AddWithValue("@prenom", etu.Prenom);
                cmd.Parameters.AddWithValue("@classe", etu.NumeroClasse);
                cmd.Parameters.AddWithValue("@date", (object?)etu.DateDiplome ?? DBNull.Value);

                return cmd.ExecuteNonQuery() > 0;
            }
        }

        // -------------------------
        // DELETE
        // -------------------------
        public bool Delete(int id)
        {
            if (id == 0) return false;

            using SqlConnection conn = new(connectionString);
            conn.Open();

            SqlCommand cmd = new("DELETE FROM Etudiant WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            return cmd.ExecuteNonQuery() > 0;
        }

        // -------------------------
        // GET BY ID
        // -------------------------
        public Etudiant? GetById(int id)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();

            SqlCommand cmd = new(
                "SELECT id, nom, prenom, numero_classe, date_diplome FROM Etudiant WHERE id=@id", conn);
            cmd.Parameters.AddWithValue("@id", id);

            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new Etudiant(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetInt32(3),
                reader.IsDBNull(4) ? null : reader.GetDateTime(4)
            );
        }

        // -------------------------
        // GET LIST + FILTER
        // -------------------------
        public List<Etudiant> GetEtudiants(int? numeroClasse = null)
        {
            List<Etudiant> resultat = [];

            using SqlConnection conn = new(connectionString);
            conn.Open();

            string query = "SELECT id, nom, prenom, numero_classe, date_diplome FROM Etudiant";
            if (numeroClasse != null)
                query += " WHERE numero_classe=@classe";

            SqlCommand cmd = new(query, conn);
            if (numeroClasse != null)
                cmd.Parameters.AddWithValue("@classe", numeroClasse);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                resultat.Add(new Etudiant(
                    reader.GetInt32(0),
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetInt32(3),
                    reader.IsDBNull(4) ? null : reader.GetDateTime(4)
                ));
            }

            return resultat;
        }

        // -------------------------
        // EDIT depuis un objet
        // -------------------------
        public bool EditEtudiant(int id, Etudiant data)
        {
            using SqlConnection conn = new(connectionString);
            conn.Open();

            SqlCommand cmd = new(
                @"UPDATE Etudiant SET nom=@nom, prenom=@prenom,
                  numero_classe=@classe, date_diplome=@date
                  WHERE id=@id", conn);

            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@nom", data.Nom);
            cmd.Parameters.AddWithValue("@prenom", data.Prenom);
            cmd.Parameters.AddWithValue("@classe", data.NumeroClasse);
            cmd.Parameters.AddWithValue("@date", (object?)data.DateDiplome ?? DBNull.Value);

            return cmd.ExecuteNonQuery() == 1;
        }
    }
}
