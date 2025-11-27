using ExoCommande.Classes;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace ExoCommande.Dao
{
    internal class ClientDao : BaseDao<Client>
    {
        // GET ALL
        public override List<Client> GetAll()
        {
            List<Client> clients = new List<Client>();

            request = @"SELECT id, nom, prenom, adresse, codePostal, ville, telephone, created_at, updated_at
                        FROM Client";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    clients.Add(new Client(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.IsDBNull(6) ? null : reader.GetString(6),
                        reader.GetDateTime(7),
                        reader.IsDBNull(8) ? null : reader.GetDateTime(8)
                    ));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans GetAll : " + ex.Message);
            }

            return clients;
        }

        // GET BY ID
        public override Client? getOneById(int id)
        {
            request = @"SELECT id, nom, prenom, adresse, codePostal, ville, telephone, created_at, updated_at
                        FROM Client
                        WHERE id = @id";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);

                command.Parameters.AddWithValue("@id", id);
                connection.Open();

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    return new Client(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetString(4),
                        reader.GetString(5),
                        reader.IsDBNull(6) ? null : reader.GetString(6),
                        reader.GetDateTime(7),
                        reader.IsDBNull(8) ? null : reader.GetDateTime(8)
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans getOneById : " + ex.Message);
            }

            return null;
        }

        // INSERT
        public override Client Save(Client entity)
        {
            request = @"INSERT INTO Client (nom, prenom, adresse, codePostal, ville, telephone, created_at)
                        OUTPUT INSERTED.Id
                        VALUES (@nom, @prenom, @adresse, @codePostal, @ville, @telephone, SYSDATETIME())";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);

                command.Parameters.AddWithValue("@nom", entity.Nom);
                command.Parameters.AddWithValue("@prenom", entity.Prenom);
                command.Parameters.AddWithValue("@adresse", entity.Adresse);
                command.Parameters.AddWithValue("@codePostal", entity.CodePostal);
                command.Parameters.AddWithValue("@ville", entity.Ville);
                command.Parameters.AddWithValue("@telephone", (object?)entity.Telephone ?? DBNull.Value);

                connection.Open();
                entity.Id = (int)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans Save : " + ex.Message);
            }

            return entity;
        }

        // UPDATE
        public override Client Update(Client entity)
        {
            request = @"UPDATE Client
                        SET nom=@nom,
                            prenom=@prenom,
                            adresse=@adresse,
                            codePostal=@codePostal,
                            ville=@ville,
                            telephone=@telephone,
                            updated_at = SYSDATETIME()
                        WHERE id = @id";

            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);

                command.Parameters.AddWithValue("@nom", entity.Nom);
                command.Parameters.AddWithValue("@prenom", entity.Prenom);
                command.Parameters.AddWithValue("@adresse", entity.Adresse);
                command.Parameters.AddWithValue("@codePostal", entity.CodePostal);
                command.Parameters.AddWithValue("@ville", entity.Ville);
                command.Parameters.AddWithValue("@telephone", (object?)entity.Telephone ?? DBNull.Value);
                command.Parameters.AddWithValue("@id", entity.Id);

                connection.Open();
                command.ExecuteNonQuery();

                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans Update : " + ex.Message);
                return entity;
            }
        }

        // DELETE
        public override bool Delete(int id)
        {
            request = @"DELETE FROM Client WHERE id = @id";
            try
            {
                using SqlConnection connection = DataConnection.GetConnection;
                using SqlCommand command = new SqlCommand(request, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur dans Delete : " + ex.Message);
                return false;
            }
        }

    }
}
